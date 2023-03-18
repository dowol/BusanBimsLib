namespace BusanBimsLib.Data;

public class BusRouteRequestData : RequestDataBase
{
    /// <summary>
    /// 버스 API ID
    /// </summary>
    [RequestData("lineid")]
#pragma warning disable CS8618
    public string BusID { get; set; }
#pragma warning restore CS8618
}
