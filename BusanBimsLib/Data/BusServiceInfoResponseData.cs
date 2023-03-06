using BusanBimsLib.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusanBimsLib.Data;

public class BusServiceInfoResponseData : IEnumerable<BusServiceInfo>
{
    private readonly List<BusServiceInfo> list = new();

    internal BusServiceInfoResponseData(XmlElement element)
    {
        foreach (XmlElement item in element.GetElementsByTagName("item"))
            list.Add(new BusServiceInfo(item));
    }

    public IEnumerator<BusServiceInfo> GetEnumerator()
    {
        return ((IEnumerable<BusServiceInfo>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }
}

public class BusServiceInfo
{
    /// <summary>
    /// 버스정류장 API ID
    /// </summary>
    public string? BusStopID { get; }
    /// <summary>
    /// 버스 종류
    /// </summary>
    public string? BusKind { get; }
    /// <summary>
    /// 정류장 순번
    /// </summary>
    public int? Order { get; }
    /// <summary>
    /// 버스 노선번호
    /// </summary>
    public string? BusName { get; }
    /// <summary>
    /// 버스노선 API ID
    /// </summary>
    public string? BusID { get; }
    /// <summary>
    /// 정류장 위치
    /// </summary>
    public Geolocation? Location { get; }
    /// <summary>
    /// 버스정류장 이름
    /// </summary>
    public string? BusStopName { get; }
    /// <summary>
    /// 버스 도착정보
    /// </summary>
    public IReadOnlyList<BusServiceDetail> ServiceInfo { get; }

    internal BusServiceInfo(XmlElement element)
    {
        BusStopID = element["bstopid"]?.InnerNormalizedText();

        BusKind = element["bustype"]?.InnerNormalizedText();

        if (int.TryParse(element["bstopidx"]?.InnerNormalizedText(), out int bstopidx))
            Order = bstopidx;

        BusName = element["lineno"]?.InnerNormalizedText();

        BusID = element["lineid"]?.InnerNormalizedText();

        if (double.TryParse(element["gpsx"]?.InnerNormalizedText(), out double gpsx) && double.TryParse(element["gpsy"]?.InnerNormalizedText(), out double gpsy))
            Location = new(gpsy, gpsx);

        List<BusServiceDetail> services = new();
        if (element["carno1"] is not null)
            services.Add(new BusServiceDetail
            {
                BusPlate = element["carno1"]?.InnerNormalizedText(),
                LeftTime = int.TryParse(element["min1"]?.InnerNormalizedText(), out int min1) ? TimeSpan.FromMinutes(min1) : null,
                LeftStops = int.TryParse(element["station1"]?.InnerNormalizedText(), out int station1) ? station1 : null,
                IsLowPlate = byte.TryParse(element["lowplate1"]?.InnerNormalizedText(), out byte lowplate1) ? lowplate1 == 1 : null,
                LeftSeats = int.TryParse(element["seat1"]?.InnerNormalizedText(), out int seat1) && seat1 >= 0 ? seat1 : null
            });
        if (element["carno2"] is not null)
            services.Add(new BusServiceDetail
            {
                BusPlate = element["carno2"]?.InnerNormalizedText(),
                LeftTime = int.TryParse(element["min2"]?.InnerNormalizedText(), out int min2) ? TimeSpan.FromMinutes(min2) : null,
                LeftStops = int.TryParse(element["station2"]?.InnerNormalizedText(), out int station2) ? station2 : null,
                IsLowPlate = byte.TryParse(element["lowplate2"]?.InnerNormalizedText(), out byte lowplate2) ? lowplate2 == 1 : null,
                LeftSeats = int.TryParse(element["seat2"]?.InnerNormalizedText(), out int seat2) && seat2 >= 0 ? seat2 : null

            });

        ServiceInfo = services;
    }

}

public class BusServiceDetail
{
    /// <summary>
    /// 차량번호
    /// </summary>
    public string? BusPlate { get; internal init; }
    /// <summary>
    /// 도착까지 남은 시간
    /// </summary>
    public TimeSpan? LeftTime { get; internal init; }
    /// <summary>
    /// 도착까지 남은 정류장 수
    /// </summary>
    public int? LeftStops { get; internal init; }
    /// <summary>
    /// 저상버스 여부
    /// </summary>
    public bool? IsLowPlate { get; internal init; }
    /// <summary>
    /// 남은 좌석 수
    /// </summary>
    public int? LeftSeats { get; internal init; }

    internal BusServiceDetail() { }

}