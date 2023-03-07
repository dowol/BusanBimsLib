namespace BusanBimsLib.Data;

public class BusServiceInfoRequestData : RequestDataBase
{
    [RequestData("bstopid")]
    public string? BusStopID { get; set; }

    [RequestData("lineid")]
    public string? BusID { get; set; }

    [RequestData("arsno")]
    public string? ARS { get; set; }

    internal BusServiceInfoRequestData() { }
}
