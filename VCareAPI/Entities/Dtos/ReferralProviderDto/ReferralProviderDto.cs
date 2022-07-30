using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ReferralProviderDto
{
    public class ReferralProviderDto
    {
        public int ReferralProviderId { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public int? ZIP { get; set; }
        public string Phone { get; set; }
        public string NPI { get; set; }
    }
}
