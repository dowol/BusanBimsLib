using BusanBimsLib.Resources;
using System;
using System.Globalization;

namespace BusanBimsLib;

public class BusanBimsException : Exception
{
#pragma warning disable CS8600, CS8603, CS8605
    public BusanBimsStatus Status => (BusanBimsStatus)Data["Status"];

    public override string Message => (string)Data["Message"];
#pragma warning restore CS8600, CS8603, CS8605

    internal BusanBimsException(BusanBimsStatus status)
    {
        Data["Status"] = status;
        Messages.Culture = CultureInfo.CurrentUICulture;
        Data["Message"] = Messages.ResourceManager.GetString($"Status:{status}");
    }
}
