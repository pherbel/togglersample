using AutoMapper;
using System.Collections.Generic;
using TogglerService.Models;
using System.Linq;

namespace TogglerService.Automapper
{
    public class ExcludeServicesTypeConverter : ITypeConverter<List<ExcludedService>, HashSet<string>>, ITypeConverter<HashSet<string>, List<ExcludedService>>
    {
        public List<ExcludedService> Convert(HashSet<string> source, List<ExcludedService> destination, ResolutionContext context)
        {
            destination = new List<ExcludedService>();
            destination.AddRange(source.Select(s => new ExcludedService { ServiceId = s }).ToList());

            return destination;
        }

        public HashSet<string> Convert(List<ExcludedService> source, HashSet<string> destination, ResolutionContext context)
        {
            destination = new HashSet<string>();

            foreach (ExcludedService item in source)
            {
                destination.Add(item.ServiceId);
            }

            return destination;
        }
    }
}
