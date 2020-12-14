/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using AudioWorks.Common;

namespace AudioWorks.Commands
{
    static class SettingAdapter
    {
        internal static SettingDictionary? ParametersToSettings(RuntimeDefinedParameterDictionary? parameters)
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

        internal static RuntimeDefinedParameterDictionary SettingInfoToParameters(SettingInfoDictionary settingInfos)
        {
            var result = new RuntimeDefinedParameterDictionary();
            foreach (var item in settingInfos)
            {
                var attributes = new Collection<Attribute> { new ParameterAttribute() };
                switch (item.Value)
                {
                    // Boolean settings are converted to switch parameters
                    case BoolSettingInfo:
                        result.Add(item.Key, new(item.Key, typeof(SwitchParameter), attributes));
                        break;

                    // Integer settings have range validation
                    case IntSettingInfo intSettingInfo:
                        attributes.Add(new ValidateRangeAttribute(intSettingInfo.MinValue, intSettingInfo.MaxValue));
                        goto default;

                    // String settings have set validation
                    case StringSettingInfo stringSettingInfo:
                        attributes.Add(new ValidateSetAttribute(stringSettingInfo.AcceptedValues.ToArray()));
                        goto default;

                    default:
                        result.Add(item.Key, new(item.Key, item.Value.ValueType, attributes));
                        break;
                }
            }
            return result;
        }
    }
}