using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace BusanBimsLib.Data;

/// <summary>
/// 
/// </summary>
public record class BusInfoResponseData : IEnumerable<BusInfo>
{
    private readonly List<BusInfo> list = new();

    public IEnumerator<BusInfo> GetEnumerator()
    {
        return ((IEnumerable<BusInfo>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }

    internal BusInfoResponseData(XmlElement element)
    {
        list.Clear();
        foreach (XmlElement item in element.GetElementsByTagName("item"))
            if(item is not null) list.Add(new(item));
    }
}

public record class BusInfo
{
    /// <summary>
    /// 버스 API ID
    /// </summary>
    public string BusID { get; }
    /// <summary>
    /// 노선번호
    /// </summary>
    public string BusName { get; }
    /// <summary>
    /// 버스 종류
    /// </summary>
    public string BusKind { get; }
    /// <summary>
    /// 운수회사
    /// </summary>
    public string Company { get; }
    /// <summary>
    /// 기점
    /// </summary>
    public string BeginingAt { get; }
    /// <summary>
    /// 종점
    /// </summary>
    public string EndingAt { get; }
    /// <summary>
    /// 기점 첫차출발시각
    /// </summary>
    public TimeOnly FirstBus { get; }
    /// <summary>
    /// 기점 막차출발시각
    /// </summary>
    public TimeOnly LastBus { get; }
    /// <summary>
    /// 배차간격
    /// </summary>
    public TimeSpan Interval { get; }
    /// <summary>
    /// 출퇴근시간 배차간격
    /// </summary>
    public TimeSpan RushHourInterval { get; }
    /// <summary>
    /// 주말/공휴일 배차간격
    /// </summary>
    public TimeSpan HolidayInterval { get; }

    internal BusInfo(XmlElement element)
    {
#pragma warning disable CS8602, CS8604
        BusID = element["lineid"].InnerText.Normalize().Trim();
        BusName = element["buslinenum"].InnerText.Normalize().Trim();
        BusKind = element["bustype"].InnerText.Normalize().Trim();

        BeginingAt = element["startpoint"].InnerText.Normalize().Trim();
        EndingAt = element["endpoint"].InnerText.Normalize().Trim();
        Company = element["companyid"].InnerText.Normalize().Trim();

        FirstBus = TimeOnly.ParseExact(element["firsttime"].InnerText.Normalize().Trim(), "HH:mm", null);
        LastBus = TimeOnly.ParseExact(element["endtime"].InnerText.Normalize().Trim(), "HH:mm", null);

        Debug.WriteLine(BusName + ": " + element["headwaynorm"]?.InnerText);

        

        Interval = TimeSpan.FromMinutes(GetInterval(element["headwaynorm"]));
        RushHourInterval = TimeSpan.FromMinutes(GetInterval(element["headwaypeak"]));
        HolidayInterval = TimeSpan.FromMinutes(GetInterval(element["headwayholi"]));
#pragma warning restore CS8602, CS8604
    }

    private static int GetInterval(XmlElement? element)
    {
        if (element is null) return 0;
        string text = element.InnerText.Trim().Normalize().Replace('~', '-');
        if (int.TryParse(text, out int result))
            return result;
        else if (int.TryParse(text[..text.IndexOf('-')], out result))
            return result;
        else
            return 0;

    }
}
