using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    static class SettingAdapter
    {
        [CanBeNull, ContractAnnotation("null => null")]
        internal static SettingDictionary ParametersToSettings([CanBeNull] RuntimeDefinedParameterDictionary parameters)
        {
            if (parameters == null) return null;

            var result = new SettingDictionary();
            foreach (var parameter in parameters.Where(p => p.Value.IsSet))
                result.Add(parameter.Key, parameter.Value.Value);
            return result;
        }

        [NotNull]
        internal static RuntimeDefinedParameterDictionary SettingInfoToParameters([NotNull] SettingInfoDictionary settingInfo)
        {
            var result = new RuntimeDefinedParameterDictionary();
            foreach (var item in settingInfo)
            {
                var attributes = new Collection<Attribute> { new ParameterAttribute() };
                if (item.Value is IntSettingInfo intValue)
                    attributes.Add(new ValidateRangeAttribute(intValue.MinValue, intValue.MaxValue));

                result.Add(item.Key, new RuntimeDefinedParameter(item.Key, item.Value.Type, attributes));
            }
            return result;
        }
    }
}