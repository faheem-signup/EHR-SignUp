using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientSearchParamDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string StatusName { get; set; }
    }
}
