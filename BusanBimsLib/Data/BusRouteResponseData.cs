using BusanBimsLib.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


namespace BusanBimsLib.Data;

public class BusRouteResponseData : IEnumerable<BusRouteNode>
{
    private readonly List<BusRouteNode> list = new();

    internal BusRouteResponseData(XmlElement element)
    {
        foreach (XmlElement item in element.GetElementsByTagName("item"))
            list.Add(new(item));
    }

    public IEnumerator<BusRouteNode> GetEnumerator()
    {
        return ((IEnumerable<BusRouteNode>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }
}

public class BusRouteNode
{
    /// <summary>
    /// 버스정류장 ARS 번호
    /// </summary>
    public int? ARS { get; }
    /// <summary>
    /// 정류장간 이동시간
    /// </summary>
    public TimeSpan? TravelTime { get; }
    /// <summary>
    /// 정류장 순서
    /// </summary>
    public int? Order { get; }
    /// <summary>
    /// 버스정류장 이름
    /// </summary>
    public string? BusStopName { get; }
    /// <summary>
    /// 차량번호
    /// </summary>
    public string? CarPlate { get; }
    /// <summary>
    /// 버스 행선지
    /// </summary>
    public int? Direction { get; }
    /// <summary>
    /// 정류장 위치
    /// </summary>
    public Geolocation? Location { get; }
    /// <summary>
    /// 마커 API ID
    /// </summary>
    public string? NodeID { get; }
    /// <summary>
    /// 마커 유형
    /// </summary>
    public BusRouteNodeType? NodeType { get; }
    /// <summary>
    /// 회차지점 여부
    /// </summary>
    public bool? IsReturningPoint { get; }
    /// <summary>
    /// 저상버스 여부
    /// </summary>
    public bool? IsLowPlateBus { get; }


    internal BusRouteNode(XmlElement element)
    {
        if (int.TryParse(element["arsno"]?.InnerNormalizedText(), out int arsno))
            ARS = arsno;

        if (int.TryParse(element["avgym"]?.InnerNormalizedText(), out int avgym))
            TravelTime = TimeSpan.FromSeconds(avgym);

        if (int.TryParse(element["bstopidx"]?.InnerNormalizedText(), out int bstopidx))
            Order = bstopidx;

        BusStopName = element["bstopnm"]?.InnerNormalizedText();

        CarPlate = element["carno"]?.InnerNormalizedText();

        if (int.TryParse(element["direction"]?.InnerNormalizedText(), out int direction))
            Direction = direction;

        if (double.TryParse(element["lat"]?.InnerNormalizedText(), out double lat) && double.TryParse(element["lin"]?.InnerNormalizedText(), out double lin))
            Location = new(lat, lin);

        
        NodeID = element["node"]?.InnerNormalizedText();

        if (byte.TryParse(element["nodekn"]?.InnerNormalizedText(), out byte nodekn))
            NodeType = (BusRouteNodeType)nodekn;

        if (byte.TryParse(element["rpoint"]?.InnerNormalizedText(), out byte rpoint))
            IsReturningPoint = rpoint == 1;

        if (byte.TryParse(element["lowplate"]?.InnerNormalizedText(), out byte lowplate))
            IsLowPlateBus = lowplate == 1;
    }
}

public enum BusRouteNodeType : byte 
{ 
    /// <summary>교차로</summary>
    Intersection = 0, 
    /// <summary>버스정류장</summary>
    BusStop = 3 
}
