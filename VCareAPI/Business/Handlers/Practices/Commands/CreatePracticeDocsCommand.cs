using AutoMapper;
using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Base64FileExtension;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticeDocsRepository;
using DataAccess.Abstract.IPracticesRepository;
using DataAccess.Services.UploadDocument;
using Entities.Concrete.PracticeDocsEntity;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Practices.Commands
{
    public class CreatePracticeDocsCommand : IRequest<IResult>
    {
        public string DocumentData { get; set; }
        public string Description { get; set; }
        public int? PracticeId { get; set; }
       // public string HostUrl { get;set; }
        public class CreatePracticeDocsCommandHandler : IRequestHandler<CreatePracticeDocsCommand, IResult>
        {
            //private readonly ILogger _logger;
            private readonly IPracticeDocsRepository _practiceDocsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IPracticesRepository _practiceRepository;
            private readonly IUploadFile _uploadFile;
            public CreatePracticeDocsCommandHandler(IPracticeDocsRepository practiceDocsRepository, 
                IMediator mediator, 
                IMapper mapper, 
                IPracticesRepository practiceRepository,
                IUploadFile uploadFile
                )
            {
                _practiceDocsRepository = practiceDocsRepository;
                _mediator = mediator;
                _mapper = mapper;
                _practiceRepository = practiceRepository;
                _uploadFile = uploadFile;
            }

            [ValidationAspect(typeof(ValidatorsPracticeDocs), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePracticeDocsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var practicedata = await _practiceRepository.GetAsync(x => x.PracticeId == request.PracticeId);
                    if (practicedata == null)
                    {
                        return new ErrorResult("No Practice Exists.");
                    }

                    string fileName = "PracticeDoc" + request.PracticeId.ToString() + practicedata.LegalBusinessName;
                    var doc = await _uploadFile.Upload(request.DocumentData, "Practice", "PracticeDocuments", fileName);

                    if (doc == null)
                    {
                        return new ErrorResult(Messages.NoContentFound);
                    }

                    #region comment azure code
                    //string fileData = request.DocumentData.Split(',')[1];
                    //string fileExtention = GetFileExtension.GetBase64FileExtension(fileData);
                    ////  practiceDocObj.FileType = fileExtention;
                    //byte[] bytes = Convert.FromBase64String(fileData);

                    //BlobServiceClient blobServiceClient = new BlobServiceClient(GlobalStatuses._azureConnectionString);
                    //string containerName = "practicedoc" + practicedata.FirstName.ToLower() + practicedata.LastName.ToLower() + request.PracticeId.ToString() + DateTime.Now.Ticks;
                    //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                    //fileName = fileName + "." + fileExtention;

                    //var _task = Task.Run(() => this.UploadFileToBlobAsync(fileName, bytes, fileExtention, containerName));
                    //_task.Wait();
                    //string fileUrl = _task.Result;
                    #endregion

                    //string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
                    //string localFilePath = Path.Combine(localPath, fileName);

                    //// Write text to the file
                    //await File.WriteAllTextAsync(localFilePath, "Hello, World!");

                    //// Get a reference to a blob
                    //BlobClient blobClient = containerClient.GetBlobClient(fileName);


                    //// Upload data from the local file
                    //await blobClient.UploadAsync(localFilePath, true);
                    //string folderName = "PracticeDocuments";
                    //
                    //string folderpath = Path.GetFullPath("~/UploadedFiles/" + folderName).Replace("~\\", "");

                    //if (!Directory.Exists(folderpath))
                    //{
                    //    Directory.CreateDirectory(folderpath);
                    //}

                    //string filePath = "/UploadedFiles/PracticeDocuments/" + fileName + request.PracticeId.ToString() + "-" + DateTime.Now.Ticks + "." + fileExtention;

                    //string fullpath = Path.GetFullPath("~" + filePath).Replace("~\\", "");
                    ////_logger.Debug(fullpath);

                    //using (FileStream fs = File.Create(fullpath, 1024))
                    //{
                    //    fs.Write(bytes, 0, bytes.Length);
                    //}

                    PracticeDoc practiceDocObj = new PracticeDoc
                    {
                        DocumentName = practicedata.LegalBusinessName + '-' + request.PracticeId.ToString(),
                        CreatedDate = DateTime.Now,
                        Description = request.Description,
                        PracticeId = request.PracticeId,
                        DocumentData = doc.DocumentData,
                        isDeleted = false,
                        DocumnetPath = doc.FilePath,//+ filePath, //fileName + request.PracticeId.ToString() + "-" + DateTime.Now.Ticks + "." + fileExtention, /*"https://vcaredevapi.scm.azurewebsites.net/dev/api/files/wwwroot/"*/
                        FileType = doc.FileExtention,
                    };
                    //practiceDocObj.DocumentData = bytes;
                    // practiceDocObj.DocumnetPath = filePath;

                    _practiceDocsRepository.Add(practiceDocObj);
                    await _practiceDocsRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
                catch (Exception ex)
                {
                    return new ErrorResult(ex.Message);
                }
            }

            //private async Task<string> UploadFileToBlobAsync(string strFileName, byte[] fileData, string fileMimeType, string strContainerName)
            //{
            //    try
            //    {
            //        BlobServiceClient blobServiceClient = new BlobServiceClient(GlobalStatuses._azureConnectionString);
            //        BlobContainerClient containerClient =  blobServiceClient.GetBlobContainerClient(strContainerName);



            //        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(GlobalStatuses._azureConnectionString);
            //        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            //       // string strContainerName = "uploads";
            //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            //      //  string fileName = this.GenerateFileName(strFileName);

            //        if (await cloudBlobContainer.CreateIfNotExistsAsync())
            //        {
            //            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            //        }

            //        //if (strFileName != null && fileData != null)
            //        //{
            //        //    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(strFileName);
            //        //    cloudBlockBlob.Properties.ContentType = ".jpg";
            //        //    await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
            //        //    return cloudBlockBlob.Uri.AbsoluteUri;
            //        //}


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
            //            var uploadedBlob = await blob.UploadAsync(fileStream, blobHttpHeader);
                   
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
