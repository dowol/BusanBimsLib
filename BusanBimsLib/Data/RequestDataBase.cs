using BusanBimsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusanBimsLib.Data;

/// <summary>
/// 
/// </summary>
public abstract class RequestDataBase
{
    
    internal string ToQueryString(string serviceKey)
    {
        IEnumerable<PropertyInfo> props =
            from prop in GetType().GetProperties()
            where Attribute.IsDefined(prop, typeof(RequestDataAttribute)) && prop.GetValue(this) != null
            orderby prop.Name ascending select prop;

        List<string> qs = new()
        {
            $"serviceKey={Uri.EscapeDataString(serviceKey)}"
        };

        foreach (PropertyInfo prop in props)
        {
            RequestDataAttribute? attr = prop.GetCustomAttributes<RequestDataAttribute>().FirstOrDefault();
            string key = attr?.Key ?? prop.Name.ToCamelCase();
            object? value = prop.GetValue(this)?.ToString();
            if(value is not null)
#pragma warning disable CS8604
                qs.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.ToString())}");
#pragma warning restore CS8604
        }

        return "?" + string.Join('&', qs);
    }
}
