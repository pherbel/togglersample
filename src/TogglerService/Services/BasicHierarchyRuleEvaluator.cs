using System.Collections.Generic;
using TogglerService.Models;

namespace TogglerService.Services
{
    public class BasicHierarchyRuleEvaluator : IHierarchyRuleEvaluator
    {
        public List<Toggle> Eval(List<GlobalToggle> globaltoggles, List<ServiceToggle> serviceToggles)
        {
            throw new System.NotImplementedException();
        }
    }
}
