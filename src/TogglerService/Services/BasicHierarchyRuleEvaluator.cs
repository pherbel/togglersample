using System;
using System.Collections.Generic;
using System.Linq;
using TogglerService.Models;

namespace TogglerService.Services
{
    public class BasicHierarchyRuleEvaluator : IHierarchyRuleEvaluator
    {
        public List<Toggle> Eval(string serviceId, string version, List<GlobalToggle> globaltoggles, List<ServiceToggle> serviceToggles)
        {
            List<Toggle> result = new List<Toggle>();

            foreach (GlobalToggle item in globaltoggles)
            {
                if (IsIncluded(serviceId, item))
                {
                    result.Add(Override(item, serviceId, serviceToggles));
                }
            }

            return result;
        }

        private Toggle Override(Toggle item, string serviceId, List<ServiceToggle> serviceToggles)
        {
            var serviceToggle = serviceToggles.SingleOrDefault(s => s.ServiceId == serviceId && s.Id == item.Id);
            if (serviceToggle is null)
            {
                return item;
            }
            else
            {
                item.Value = serviceToggle.Value;
                //TODO: Need to implement version range evaluation
                return item;
            }
        }

        private bool IsIncluded(string serviceId, GlobalToggle item)
        {
            return !item.ExcludedServices?.Exists(e => e.ServiceId == serviceId) ?? true;
        }
    }
}
