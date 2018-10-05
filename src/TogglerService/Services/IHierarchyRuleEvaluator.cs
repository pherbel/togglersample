using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TogglerService.Models;

namespace TogglerService.Services
{
    public interface IHierarchyRuleEvaluator
    {
        List<Toggle> Eval(string serviceId, string version, List<GlobalToggle> globaltoggles, List<ServiceToggle> serviceToggles);
    }
}
