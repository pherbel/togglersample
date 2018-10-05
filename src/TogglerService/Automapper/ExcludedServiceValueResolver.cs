using AutoMapper;
using System.Collections.Generic;
using TogglerService.Models;
using TogglerService.ViewModels;

namespace TogglerService.Automapper
{
    public class ExcludedServiceValueResolver : IMemberValueResolver<GlobalToggleVM, GlobalToggle, HashSet<string>, List<ExcludedService>>
    {
        public List<ExcludedService> Resolve(GlobalToggleVM source, GlobalToggle destination, HashSet<string> sourceMember, List<ExcludedService> destMember, ResolutionContext context)
        {

            destMember = new List<ExcludedService>();

            foreach (var item in sourceMember)
            {
                destMember.Add(new ExcludedService { ServiceId = item, ToggleId = source.Id });
            }

            return destMember;
         
        }
    }
}
