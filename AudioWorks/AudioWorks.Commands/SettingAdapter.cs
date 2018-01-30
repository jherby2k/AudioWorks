using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using AudioWorks.Common;
using JetBrains.Annotations;

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
                result.Add(parameter.Key,
                    parameter.Value.ParameterType == typeof(SwitchParameter)
                        ? true
                        : parameter.Value.Value);
            return result;
        }

        [NotNull]
        internal static RuntimeDefinedParameterDictionary SettingInfoToParameters([NotNull] SettingInfoDictionary settingInfos)
        {
            var result = new RuntimeDefinedParameterDictionary();
            foreach (var item in settingInfos)
            {
                var attributes = new Collection<Attribute> { new ParameterAttribute() };
                switch (item.Value)
                {
                    case BoolSettingInfo _:
                        result.Add(item.Key, new RuntimeDefinedParameter(item.Key, typeof(SwitchParameter), attributes));
                        break;
                    case IntSettingInfo intSettingInfo:
                        attributes.Add(new ValidateRangeAttribute(intSettingInfo.MinValue, intSettingInfo.MaxValue));
                        result.Add(item.Key, new RuntimeDefinedParameter(item.Key, item.Value.ValueType, attributes));
                        break;
                    case StringSettingInfo stringSettingInfo:
                        attributes.Add(new ValidateSetAttribute(stringSettingInfo.AcceptedValues.ToArray()));
                        result.Add(item.Key, new RuntimeDefinedParameter(item.Key, item.Value.ValueType, attributes));
                        break;
                }
            }
            return result;
        }
    }
}