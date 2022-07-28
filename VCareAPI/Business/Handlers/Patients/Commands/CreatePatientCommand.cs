using AutoMapper;
using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Base64FileExtension;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientGuardiansRepository;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Services.UploadDocument;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientGuardiansEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Commands
{
    public class CreatePatientCommand : IRequest<IDataResult<object>>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime? DOB { get; set; }
        public int? Gender { get; set; }
        public string SSN { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? County { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public int? Race { get; set; }
        public int? Ethnicity { get; set; }
        public int? MaritalStatus { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public int? PreferredCom { get; set; }
        public int? PreferredLanguage { get; set; }
        public string EmergencyConName { get; set; }
        public string EmergencyConAddress { get; set; }
        public string EmergencyConRelation { get; set; }
        public string EmergencyConPhone { get; set; }
        public string Comment { get; set; }
        public string PatientImage { get; set; }
        public int? StatusId { get; set; }
        public List<PatientGuardianDto> patientGuardian { get; set; }
        public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, IDataResult<object>>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IPatientGuardiansRepository _patientGuardiansRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IUploadFile _uploadFile;

            public CreatePatientCommandHandler(IPatientRepository patientRepository,
                IPatientGuardiansRepository patientGuardiansRepository,
                IMediator mediator,
                IMapper mapper,
                IUploadFile uploadFile,
                IHttpContextAccessor contextAccessor)
            {
                _patientRepository = patientRepository;
                _patientGuardiansRepository = patientGuardiansRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _uploadFile = uploadFile;
            }

            [ValidationAspect(typeof(ValidatorPatient), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<object>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
            {
                string fileUrl = string.Empty;
                byte[] bytes = null;
                if (!string.IsNullOrEmpty(request.PatientImage))
                {
                    string fileName = "patientname" + request.FirstName.ToString();

                    var doc = await _uploadFile.Upload(request.PatientImage, "Patient", "PatientDocuments", fileName);

                    if (doc == null)
                    {
                        //return new ErrorResult(Messages.NoContentFound);
                    }

                    fileUrl = doc.FilePath;
                    bytes = doc.DocumentData;

                    #region comment azure code
                    //string fileData = request.PatientImage.Split(',')[1];
                    //string fileExtention = GetFileExtension.GetBase64FileExtension(fileData);
                    //bytes = Convert.FromBase64String(fileData);

                    //BlobServiceClient blobServiceClient = new BlobServiceClient(GlobalStatuses._azureConnectionString);
                    //string containerName = "patientdocument" + DateTime.Now.Ticks;
                    //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                    //string fileName = "patientname" + request.FirstName.ToString() + DateTime.Now.Ticks + "." + fileExtention;
                    //var _task = Task.Run(() => this.UploadFileToBlobAsync(fileName, bytes, fileExtention, containerName));
                    //_task.Wait();
                    //fileUrl = _task.Result;
                    #endregion
                }

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Patient patientObj = new Patient
                {
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Suffix = request.Suffix,
                    DOB = request.DOB,
                    Gender = request.Gender,
                    SSN = request.SSN,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    County = request.County,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    Race = request.Race,
                    Ethnicity = request.Ethnicity,
                    MaritalStatus = request.MaritalStatus,
                    CellPhone = request.CellPhone,
                    HomePhone = request.HomePhone,
                    WorkPhone = request.WorkPhone,
                    Email = request.Email,
                    PreferredCom = request.PreferredCom,
                    PreferredLanguage = request.PreferredLanguage,
                    EmergencyConName = request.EmergencyConName,
                    EmergencyConAddress = request.EmergencyConAddress,
                    EmergencyConRelation = request.EmergencyConRelation,
                    EmergencyConPhone = request.EmergencyConPhone,
                    PatientImage = bytes,
                    PatientImagePath = fileUrl,
                    Comment = request.Comment,
                    StatusId = request.StatusId == null ? 1 : request.StatusId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _patientRepository.Add(patientObj);
                await _patientRepository.SaveChangesAsync();

                if (request.patientGuardian.Count() > 0)
                {
                    List<PatientGuardian> patientGuardianList = request.patientGuardian.ConvertAll(a =>
                    {
                        return new PatientGuardian()
                        {
                            GuardianName = a.GuardianName,
                            GuardianRelation = a.GuardianRelation,
                            GuardianAddress = a.GuardianAddress,
                            GuardianPhone = a.GuardianPhone,
                        };
                    });

                    patientGuardianList.ToList().ForEach(x => x.PatientId = patientObj.PatientId);
                    var existingList = await _patientGuardiansRepository.GetListAsync(x => x.PatientId == patientObj.PatientId);

                    _patientGuardiansRepository.BulkInsert(existingList, patientGuardianList);
                    await _patientGuardiansRepository.SaveChangesAsync();
                }

                return new SuccessDataResult<object>(patientObj.PatientId);
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
            //        string extension = fileMimeType;
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
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
        }
    }
}
