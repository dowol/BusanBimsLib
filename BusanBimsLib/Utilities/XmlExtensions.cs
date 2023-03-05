using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusanBimsLib.Utilities
{
    public static class XmlExtensions
    {
        public static string? InnerNormalizedText(this XmlElement element)
        {
            return element?.InnerText.Normalize().Trim();
        }
    }
}
