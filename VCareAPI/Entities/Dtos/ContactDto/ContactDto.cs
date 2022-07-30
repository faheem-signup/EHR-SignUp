using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ContactDto
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ContactTypeId { get; set; }
        public string Description { get; set; }
        public int? ID { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountyId { get; set; }
        public string County { get; set; }
        public int? StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int? ZipId { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ContactTypeDescription { get; set; }
    }
}
