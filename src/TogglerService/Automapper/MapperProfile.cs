using AutoMapper;
using TogglerService.Models;
using TogglerService.ViewModels;

namespace TogglerService.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GlobalToggleVM, GlobalToggle>().ReverseMap();
            CreateMap<SaveGlobalToggleVM, GlobalToggle>(MemberList.Source);
            CreateMap<ServiceToggleVM, ServiceToggle>().ReverseMap();
            CreateMap<SaveServiceToggleVM, ServiceToggle>(MemberList.Source);

            CreateMap<Toggle, ToggleVM>();
        }
    }
}
