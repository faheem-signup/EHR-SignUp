using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete;
using Entities.Concrete.ReferralProviderEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReferralProviders.Commands
{
    public class UpdateReferralProviderCommand : IRequest<IResult>
    {
        public int ReferralProviderId { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int ZIP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Speciality { get; set; }
        public string TaxID { get; set; }
        public string License { get; set; }
        public string SSN { get; set; }
        public string NPI { get; set; }
        public string ContactPerson { get; set; }
        public string Comments { get; set; }
        public string ReferralProviderType { get; set; }
        public class UpdateReferralProviderCommandHandler : IRequestHandler<UpdateReferralProviderCommand, IResult>
        {
            private readonly IReferralProviderRepository _referralProviderRepository;
            public UpdateReferralProviderCommandHandler(IReferralProviderRepository referralProviderRepository)
            {
                _referralProviderRepository = referralProviderRepository;
            }

            [ValidationAspect(typeof(ValidatorUpdateReferralProvider), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateReferralProviderCommand request, CancellationToken cancellationToken)
            {
                var referralProviderObj = await _referralProviderRepository.GetAsync(x => x.ReferralProviderId == request.ReferralProviderId);
                if(referralProviderObj != null)
                {
                    referralProviderObj.ReferralProviderId = request.ReferralProviderId;
                    referralProviderObj.FirstName = request.FirstName;
                    referralProviderObj.MI = request.MI;
                    referralProviderObj.LastName = request.LastName;
                    referralProviderObj.Address = request.Address;
                    referralProviderObj.City = request.City;
                    referralProviderObj.State = request.State;
                    referralProviderObj.ZIP = request.ZIP;
                    referralProviderObj.Phone = request.Phone;
                    referralProviderObj.Fax = request.Fax;
                    referralProviderObj.Email = request.Email;
                    referralProviderObj.Speciality = request.Speciality;
                    referralProviderObj.TaxID = request.TaxID;
                    referralProviderObj.License = request.License;
                    referralProviderObj.SSN = request.SSN;
                    referralProviderObj.NPI = request.NPI;
                    referralProviderObj.ContactPerson = request.ContactPerson;
                    referralProviderObj.Comments = request.Comments;
                    referralProviderObj.ReferralProviderType = request.ReferralProviderType;

                    _referralProviderRepository.Update(referralProviderObj);
                    await _referralProviderRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
