using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequestDataAttribute : Attribute
    {
        public string? Key { get; }



        public RequestDataAttribute(string? key = null)
        {
            Key = key;
        }
    }
}
