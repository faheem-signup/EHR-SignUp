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
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IPatientInsuranceTypeRepository;
using DataAccess.Services.UploadDocument;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Dtos.MessageStatusDto;
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

namespace Business.Handlers.PatientInsurances.Commands
{
    public class CreatePatientInsuranceCommand : IRequest<IResult>
    {

        //public int? InsuranceType { get; set; }
        public int? InsuredPay { get; set; }
        public string InsuranceName { get; set; }
        public DateTime? EligibilityDate { get; set; }
        public string InsuranceAddress { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string InsurancePhone { get; set; }
        public string Copay { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
        public string SubscriberName { get; set; }
        public string SubscriberPhone { get; set; }
        public int? SubscriberRelationship { get; set; }
        public int? SubscriberCity { get; set; }
        public int? SubscriberState { get; set; }
        public string SubscriberAddress { get; set; }
        public int? SubscriberZip { get; set; }
        public string RxPayerId { get; set; }
        public string RxGroupNo { get; set; }
        public string RxBinNo { get; set; }
        public string RxPCN { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientId { get; set; }
        public string InsuranceImage { get; set; }
        public string InsuranceTypeName { get; set; }

        public class CreatePatientInsuranceCommandHandler : IRequestHandler<CreatePatientInsuranceCommand, IResult>
        {
            private readonly IPatientInsuranceTypeRepository _patientInsuranceTypeRepository;
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IUploadFile _uploadFile;

            public CreatePatientInsuranceCommandHandler(IPatientInsuranceRepository patientInsuranceRepository,
                IMediator mediator,
                IMapper mapper,
                IUploadFile uploadFile,
                IPatientInsuranceTypeRepository patientInsuranceTypeRepository,
                IHttpContextAccessor contextAccessor)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
                _mediator = mediator;
                _mapper = mapper;
                _patientInsuranceTypeRepository = patientInsuranceTypeRepository;
                _contextAccessor = contextAccessor;
                _uploadFile = uploadFile;
            }

            [ValidationAspect(typeof(ValidatorPatientInsurance), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientInsuranceCommand request, CancellationToken cancellationToken)
            {
                var obj = await _patientInsuranceTypeRepository.GetAsync(x => x.Description == request.InsuranceTypeName);
                var insuranceTypeId = 0;
                if (obj != null)
                {
                    insuranceTypeId = obj.PatientInsuranceTypeId;
                }

                string fileUrl = string.Empty;
                byte[] bytes = null;
                string fileName = string.Empty;
                if (!string.IsNullOrEmpty(request.InsuranceImage))
                {
                    fileName = "insurancename" + request.InsuranceName.ToString();

                    var doc = await _uploadFile.Upload(request.InsuranceImage, "PatientInsurance", "PatientDocuments", fileName);

                    if (doc == null)
                    {
                        return new ErrorResult(Messages.NoContentFound);
                    }

                    fileUrl = doc.FilePath;
                    bytes = doc.DocumentData;

                    #region comment azure code
                    //string fileData = request.InsuranceImage.Split(',')[1];
                    //string fileExtention = GetFileExtension.GetBase64FileExtension(fileData);
                    //bytes = Convert.FromBase64String(fileData);

                    //BlobServiceClient blobServiceClient = new BlobServiceClient(GlobalStatuses._azureConnectionString);
                    //string containerName = "patientinsurancedocument" + DateTime.Now.Ticks;
                    //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                    //string fileName = "insurancename" + request.InsuranceName.ToString()+ DateTime.Now.Ticks + "." + fileExtention;

                    //var _task = Task.Run(() => this.UploadFileToBlobAsync(fileName, bytes, fileExtention, containerName));
                    //_task.Wait();
                    //fileUrl = _task.Result;
                    #endregion
                }

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                PatientInsurance patientInsuranceObj = new PatientInsurance
                {
                    InsuranceType = insuranceTypeId,
                    InsuredPay = request.InsuredPay,
                    InsuranceName = request.InsuranceName,
                    EligibilityDate = request.EligibilityDate,
                    InsuranceAddress = request.InsuranceAddress,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    InsurancePhone = request.InsurancePhone,
                    Copay = request.Copay,
                    PolicyNumber = request.PolicyNumber,
                    GroupNumber = request.GroupNumber,
                    SubscriberName = request.SubscriberName,
                    SubscriberPhone = request.SubscriberPhone,
                    SubscriberRelationship = request.SubscriberRelationship,
                    SubscriberCity = request.SubscriberCity,
                    SubscriberState = request.SubscriberState,
                    SubscriberAddress = request.SubscriberAddress,
                    SubscriberZip = request.SubscriberZip,
                    RxPayerId = request.RxPayerId,
                    RxGroupNo = request.RxGroupNo,
                    RxBinNo = request.RxBinNo,
                    RxPCN = request.RxPCN,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    PatientId = request.PatientId,
                    InsuranceImage = bytes,
                    InsuranceImagePath = fileUrl,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _patientInsuranceRepository.Add(patientInsuranceObj);
                await _patientInsuranceRepository.SaveChangesAsync();

                return new SuccessResult(patientInsuranceObj.PatientInsuranceId != 0 ? patientInsuranceObj.PatientInsuranceId.ToString() : null);
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
