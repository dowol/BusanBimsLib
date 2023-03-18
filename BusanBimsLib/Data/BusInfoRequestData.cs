namespace BusanBimsLib.Data;


public class BusInfoRequestData : RequestDataBase
{
    /// <summary xml:lang="ko">
    /// 버스노선 API ID
    /// </summary>
    /// <summary>
    /// API identify for the bus service
    /// </summary>
    [RequestData("lineid")]
    public string? BusID { get; set; }

    /// <summary xml:lang="ko">
    /// 버스 노선번호
    /// </summary>
    /// <summary>
    /// Bus line name
    /// </summary>
    [RequestData("lineno")]
    public string? BusName { get; set; }

    internal BusInfoRequestData() { }
}
