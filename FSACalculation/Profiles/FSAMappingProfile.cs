using AutoMapper;
using FSACalculation.Entities;
using FSACalculation.ViewModels;

namespace FSACalculation.Profiles
{
    public class FSAMappingProfile : Profile
    {
        public FSAMappingProfile()
        {
            CreateMap<Claims, ClaimsForUpdateViewModel>()
                .ForMember(c => c.ClaimId, e => e.MapFrom(i => i.ID))
                .ReverseMap();
            CreateMap<Employee, EmployeeClaimsViewModel>()
                .ReverseMap();
            CreateMap<Claims, ClaimsForCreateViewModel>().ReverseMap();
            CreateMap<Claims, ClaimsViewModel>()
                .ForMember(c => c.ClaimId, e => e.MapFrom(i => i.ID))
                .ForMember(e => e.EmpId, c => c.MapFrom(i => i.EmployeeId))
                .ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
