using BusanBimsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusanBimsLib.Data;

public abstract class RequestDataBase
{
    
    public string ToQueryString(string serviceKey)
    {
        IEnumerable<PropertyInfo> props =
            from prop in GetType().GetProperties()
            where Attribute.IsDefined(prop, typeof(RequestDataAttribute)) && prop.GetValue(this) != null
            orderby prop.Name ascending select prop;

        List<string> qs = new List<string>
        {
            $"serviceKey={Uri.EscapeDataString(serviceKey)}"
        };

        foreach (PropertyInfo prop in props)
        {
            RequestDataAttribute attr = prop.GetCustomAttributes<RequestDataAttribute>().FirstOrDefault();
            string key = attr.Key ?? prop.Name.ToCamelCase();
            object? value = prop.GetValue(this).ToString();
            if(value != null)
                qs.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.ToString())}");
        }

        return "?" + string.Join('&', qs);
    }
}
