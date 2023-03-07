using BusanBimsLib.Resources;
using System;
using System.Globalization;

namespace BusanBimsLib;

public class BusanBimsException : Exception
{
    public BusanBimsStatus Status => (BusanBimsStatus)Data["Status"];

    public override string Message => (string)Data["Message"];

    internal BusanBimsException(BusanBimsStatus status)
    {
        Data["Status"] = status;
        Messages.Culture = CultureInfo.CurrentUICulture;
        Data["Message"] = Messages.ResourceManager.GetString($"Status:{status}");
    }
}
