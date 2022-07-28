using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Base64FileExtension;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IPatientInsuranceTypeRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using DataAccess.Services.UploadDocument;
using Entities.Concrete;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Concrete.ReferralProviderEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsurances.Commands
{
    public class UpdatePatientInsuranceCommand : IRequest<IResult>
    {
        public int PatientInsuranceId { get; set; }
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
        public class UpdatePatientInsuranceCommandHandler : IRequestHandler<UpdatePatientInsuranceCommand, IResult>
        {
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IPatientInsuranceTypeRepository _patientInsuranceTypeRepository;
            private readonly IUploadFile _uploadFile;

            public UpdatePatientInsuranceCommandHandler(IPatientInsuranceRepository patientInsuranceRepository,
                IHttpContextAccessor contextAccessor,
                IUploadFile uploadFile,
                IPatientInsuranceTypeRepository patientInsuranceTypeRepository)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
                _contextAccessor = contextAccessor;
                _patientInsuranceTypeRepository = patientInsuranceTypeRepository;
                _uploadFile = uploadFile;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientInsurance), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientInsuranceCommand request, CancellationToken cancellationToken)
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

                    //string fileName = "insurancename" + request.InsuranceName.ToString() + DateTime.Now.Ticks;

                    //var _task = Task.Run(() => this.UploadFileToBlobAsync(fileName, bytes, fileExtention, containerName));
                    //_task.Wait();
                    //fileUrl = _task.Result;
                    #endregion
                }

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var patientInsuranceObj = await _patientInsuranceRepository.GetAsync(x => x.PatientInsuranceId == request.PatientInsuranceId);
                if(patientInsuranceObj != null)
                {
                    patientInsuranceObj.PatientInsuranceId = request.PatientInsuranceId;
                    patientInsuranceObj.InsuranceType = insuranceTypeId;
                    patientInsuranceObj.InsuredPay = request.InsuredPay;
                    patientInsuranceObj.InsuranceName = request.InsuranceName;
                    patientInsuranceObj.EligibilityDate = request.EligibilityDate;
                    patientInsuranceObj.InsuranceAddress = request.InsuranceAddress;
                    patientInsuranceObj.City = request.City;
                    patientInsuranceObj.State = request.State;
                    patientInsuranceObj.ZIP = request.ZIP;
                    patientInsuranceObj.InsurancePhone = request.InsurancePhone;
                    patientInsuranceObj.Copay = request.Copay;
                    patientInsuranceObj.PolicyNumber = request.PolicyNumber;
                    patientInsuranceObj.GroupNumber = request.GroupNumber;
                    patientInsuranceObj.SubscriberName = request.SubscriberName;
                    patientInsuranceObj.SubscriberPhone = request.SubscriberPhone;
                    patientInsuranceObj.SubscriberRelationship = request.SubscriberRelationship;
                    patientInsuranceObj.SubscriberCity = request.SubscriberCity;
                    patientInsuranceObj.SubscriberState = request.SubscriberState;
                    patientInsuranceObj.SubscriberAddress = request.SubscriberAddress;
                    patientInsuranceObj.SubscriberZip = request.SubscriberZip;
                    patientInsuranceObj.RxPayerId = request.RxPayerId;
                    patientInsuranceObj.RxGroupNo = request.RxGroupNo;
                    patientInsuranceObj.RxBinNo = request.RxBinNo;
                    patientInsuranceObj.RxPCN = request.RxPCN;
                    patientInsuranceObj.StartDate = request.StartDate;
                    patientInsuranceObj.EndDate = request.EndDate;
                    patientInsuranceObj.PatientId = request.PatientId;
                    patientInsuranceObj.ModifiedBy = int.Parse(userId);
                    patientInsuranceObj.ModifiedDate = DateTime.Now;

                    if (request.InsuranceImage != null && request.InsuranceImage != "")
                    {
                        patientInsuranceObj.InsuranceImage = bytes;
                        patientInsuranceObj.InsuranceImagePath = fileUrl;
                    }
                        
                    _patientInsuranceRepository.Update(patientInsuranceObj);
                    await _patientInsuranceRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
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
