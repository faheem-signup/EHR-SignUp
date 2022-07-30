using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.ProviderDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AppointmentDto
{
   public class AppointmentSlotsDetailDto
    {
        public List<AppointmentByDateRangeDto> BookedSlotsDetail { get; set; }
        public GetProviderWorkConfigDto ProviderWorkConfigDetail { get; set; }
    }
}
