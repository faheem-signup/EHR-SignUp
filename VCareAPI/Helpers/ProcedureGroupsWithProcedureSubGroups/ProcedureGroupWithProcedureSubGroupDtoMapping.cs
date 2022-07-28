using AutoMapper;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using Entities.Dtos.ProcedureGroupsToPractice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Location
{
    public class ProcedureGroupWithProcedureSubGroupDtoMapping : Profile
    {

        public ProcedureGroupWithProcedureSubGroupDtoMapping()
        {
            CreateMap<ProcedureGroupToPracticeListDto, ProcedureSubGroupDto>()
                .ForMember(x => x.ProcedureSubGroupId, opt => opt.MapFrom(x => x.ProcedureSubGroupId))
                .ForMember(x => x.ProcedureSubGroupName, opt => opt.MapFrom(x => x.ProcedureSubGroupName))
                .ForMember(x => x.ProcedureSubGroupCode, opt => opt.MapFrom(x => x.ProcedureSubGroupCode))
                .ForMember(x => x.ProcedureGroupId, opt => opt.MapFrom(x => x.ProcedureGroupId));
        }
    }
}
