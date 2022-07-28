using AutoMapper;
using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Base64FileExtension;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocsFileUploadRepository;
using DataAccess.Abstract.IPatientDocumentRepository;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Services.UploadDocument;
using Entities.Concrete.PatientDocsFileUploadEntity;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Concrete.Role;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocsFileUploads.Commands
{

    public class CreatePatientDocsFileUploadCommand : IRequest<IResult>
    {
        public int PatientDocCateogryId { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public int PatientId { get; set; }
        public string DocumentData { get; set; }
        public class CreatePatientDocsFileUploadCommandHandler : IRequestHandler<CreatePatientDocsFileUploadCommand, IResult>
        {
            private readonly IPatientDocsFileUploadRepository _patientDocsFileUploadRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IPatientRepository _patientRepository;
            private readonly IPatientDocumentRepository _patientDocumentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IUploadFile _uploadFile;
            public CreatePatientDocsFileUploadCommandHandler(IPatientDocsFileUploadRepository patientDocsFileUploadRepository,
                IMediator mediator,
                IMapper mapper,
                IUploadFile uploadFile,
                IPatientRepository patientRepository,
                IPatientDocumentRepository patientDocumentRepository,
                IHttpContextAccessor contextAccessor)
            {
                _patientDocsFileUploadRepository = patientDocsFileUploadRepository;
                _mediator = mediator;
                _mapper = mapper;
                _uploadFile = uploadFile;
                _patientRepository = patientRepository;
                _patientDocumentRepository = patientDocumentRepository;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientDocsFileUploadCommand request, CancellationToken cancellationToken)
            {
                var patientData = await _patientRepository.GetAsync(x => x.PatientId == request.PatientId);
                var patientname = string.Empty;

                if (patientData != null)
                {
                    patientname = patientData.FirstName;
                }

                string fileUrl = string.Empty;
                byte[] bytes = null;
                string fileName = string.Empty;
                string fileType = string.Empty;
                if (!string.IsNullOrEmpty(request.DocumentData))
                {
                    fileName = "patientname" + patientname;

                    var doc = await _uploadFile.Upload(request.DocumentData, "Patient", "PatientDocuments", fileName);

                    if (doc == null)
                    {
                        return new ErrorResult(Messages.NoContentFound);
                    }

                    fileUrl = doc.FilePath;
                    bytes = doc.DocumentData;
                    fileType = doc.FileExtention;
                    #region comment azure code
                    //string fileData = request.DocumentData.Split(',')[1];
                    //string fileExtention = GetFileExtension.GetBase64FileExtension(fileData);
                    //string fileExtention1 = GetFileExtension.GetBase64FileExtension(request.DocumentData);
                    //bytes = Convert.FromBase64String(fileData);

                    //BlobServiceClient blobServiceClient = new BlobServiceClient(GlobalStatuses._azureConnectionString);
                    //string containerName = "patientdocument" + DateTime.Now.Ticks;
                    //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                    //fileName = "patientname" + patientname + DateTime.Now.Ticks + "." + fileExtention;

                    //var _task = Task.Run(() => this.UploadFileToBlobAsync(fileName, bytes, fileExtention, containerName));
                    //_task.Wait();
                    //fileUrl = _task.Result;
                    #endregion
                }
                else
                {
                    return new ErrorResult("Document File is not selected");
                }


                //string folderName = "PatientDocuments";
                //string folderpath = Path.GetFullPath("~/UploadedFiles/" + folderName + "/" + request.PatientId.ToString() + "-" + patientName).Replace("~\\", "");
                //if (!Directory.Exists(folderpath))
                //{
                //    Directory.CreateDirectory(folderpath);
                //}

                //string fullpath = Path.GetFullPath(folderpath + "/" +  request.PatientId.ToString() + "-" + patientName + DateTime.Now.Ticks + ".Png").Replace("~\\", "");

                //MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
                //ms.Write(bytes, 0, bytes.Length);
                //Image image = Image.FromStream(ms, true);
                //image.Save(fullpath, ImageFormat.Png);



                PatientDocsFileUpload patientDocsFileUploadObj = new PatientDocsFileUpload
                {
                    DocumentData = bytes,
                    DocumentPath = fileUrl,
                    DocumentName = fileName,
                    FileType = fileType,
                };

                _patientDocsFileUploadRepository.Add(patientDocsFileUploadObj);
                await _patientDocsFileUploadRepository.SaveChangesAsync();

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                PatientDocument patientDocumentObj = new PatientDocument
                {
                    PatientDocCateogryId = request.PatientDocCateogryId,
                    UploadDocumentId = patientDocsFileUploadObj.UploadDocumentId,
                    DateOfVisit = request.DateOfVisit,
                    PatientId = request.PatientId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _patientDocumentRepository.Add(patientDocumentObj);
                await _patientDocumentRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }

            //private async Task<string> UploadFileToBlobAsync(string strFileName, byte[] fileData, string fileMimeType, string strContainerName)
            //{
            //    try
            //    {
            //        BlobServiceClient blobServiceClient = new BlobServiceClient(GlobalStatuses._azureConnectionString);
            //        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(strContainerName);

            //        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(GlobalStatuses._azureConnectionString);
            //        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);

            //        if (await cloudBlobContainer.CreateIfNotExistsAsync())
            //        {
            //            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            //        }

            //        BlobClient blob = containerClient.GetBlobClient(strFileName);
            //        await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            //        // Why .NET Core doesn't have MimeMapping.GetMimeMapping()
            //        var blobHttpHeader = new Azure.Storage.Blobs.Models.BlobHttpHeaders();
            //        string extension = fileMimeType;// Path.GetExtension(blob.Uri.AbsoluteUri);
            //        switch (extension.ToLower())
            //        {
            //            case "jpg":
            //            case ".jpg":
            //            case ".jpeg":
            //                blobHttpHeader.ContentType = "image/jpeg";
            //                break;
            //            case "png":
            //            case ".png":
            //                blobHttpHeader.ContentType = "image/png";
            //                break;
            //            case "gif":
            //            case ".gif":
            //                blobHttpHeader.ContentType = "image/gif";
            //                break;
            //            case "tif":
            //            case ".tif":
            //            case "tiff":
            //            case ".tiff":
            //                blobHttpHeader.ContentType = "image/tiff";
            //                break;
            //            case "bmp":
            //            case ".bmp":
            //                blobHttpHeader.ContentType = "image/bmp";
            //                break;
            //            case "csv":
            //            case ".csv":
            //                blobHttpHeader.ContentType = "text/csv";
            //                break;
            //            case "txt":
            //            case ".txt":
            //                blobHttpHeader.ContentType = "text/plain";
            //                break;
            //            case "htm":
            //            case ".htm":
            //            case "html":
            //            case ".html":
            //                blobHttpHeader.ContentType = "text/html";
            //                break;
            //            case "pdf":
            //            case ".pdf":
            //                blobHttpHeader.ContentType = "application/pdf";
            //                break;
            //            case "doc":
            //            case ".doc":
            //            case "rtf":
            //            case ".rtf":
            //            case "docx":
            //            case ".docx":
            //                blobHttpHeader.ContentType = "application/msword";
            //                break;
            //            case "xls":
            //            case ".xls":
            //            case "xlsx":
            //            case ".xlsx":
            //                blobHttpHeader.ContentType = "application/x-msexcel";
            //                break;
            //            case "ppt":
            //            case ".ppt":
            //                blobHttpHeader.ContentType = "application/vnd.ms-powerpoint";
            //                break;
            //            default:
            //                break;
            //        }

            //        await using (var fileStream = new MemoryStream(fileData))
            //        {
            //          await blob.UploadAsync(fileStream, blobHttpHeader);

            //            return blob.Uri.AbsoluteUri;
            //        }

            //        return "";
            //    }
            //    catch (Exception ex)
            //    {
            //        throw (ex);
            //    }
            //}
        }
    }
}