using System.Xml;

namespace BusanBimsLib.Utilities;

internal static class XmlExtensions
{
    internal static string? InnerNormalizedText(this XmlElement element)
    {
        return element?.InnerText.Normalize().Trim();
    }
}
