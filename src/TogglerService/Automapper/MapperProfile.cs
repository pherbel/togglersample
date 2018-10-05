using AutoMapper;
using System.Collections.Generic;
using TogglerService.Models;
using TogglerService.ViewModels;

namespace TogglerService.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GlobalToggleVM, GlobalToggle>(MemberList.Destination)
                .ForMember(dest => dest.ExcludedServices,opt => opt.ResolveUsing<ExcludedServiceValueResolver,HashSet<string>>(src => src.ExcludedServices)).ReverseMap();
   //         CreateMap<HashSet<string>, List<ExcludedService>>().ConvertUsing<ExcludeServicesTypeConverter>();
            CreateMap<List<ExcludedService>, HashSet<string>>().ConvertUsing<ExcludeServicesTypeConverter>();
            CreateMap<SaveGlobalToggleVM, GlobalToggle>(MemberList.Source);
            CreateMap<ServiceToggleVM, ServiceToggle>().ReverseMap();
            CreateMap<SaveServiceToggleVM, ServiceToggle>(MemberList.Source);

            CreateMap<Toggle, ToggleVM>();
        }
    }
}
