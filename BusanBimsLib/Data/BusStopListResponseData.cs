using BusanBimsLib.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace BusanBimsLib.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed record class BusStopListResponseData : IEnumerable<BusStopInfo>
    {
        private readonly List<BusStopInfo> list = new();

        /// <summary>
        /// 페이지당 데이터 개수
        /// </summary>
        public int ItemsPerPage => list.Count;
        /// <summary>
        /// 페이지 번호
        /// </summary>
        public int Page { get; }
        /// <summary>
        /// 전체 데이터 개수
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 버스정류장 데이터
        /// </summary>
        public IReadOnlyList<BusStopInfo> BusStops => list.ToArray();

        internal BusStopListResponseData(XmlElement elememnt)
        {
#pragma warning disable CS8602
            Page = int.Parse(elememnt["pageNo"].InnerText);
            Count = int.Parse(elememnt["totalCount"].InnerText);
            foreach (XmlElement item in elememnt.GetElementsByTagName("item"))
                list.Add(new BusStopInfo(item));
#pragma warning restore CS8602
        }

        public IEnumerator<BusStopInfo> GetEnumerator()
        {
            return ((IEnumerable<BusStopInfo>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }
    }

    public sealed record class BusStopInfo
    {
        /// <summary>
        /// 버스정류장 ARS 번호
        /// </summary>
        public int? ARS { get; }
        /// <summary>
        /// 버스정류장 API ID
        /// </summary>
        public string? BusStopID { get; }
        /// <summary>
        /// 버스정류장 이름
        /// </summary>
        public string? BusStopName { get; }
        /// <summary>
        /// 버스정류장 위치
        /// </summary>
        public Geolocation? Location { get; }
        /// <summary>
        /// 버스정류장 종류
        /// </summary>
        public string? BusStopKind { get; }

        internal BusStopInfo(XmlElement item)
        {
            if (int.TryParse(item["arsno"]?.InnerText, out int arsno))
                ARS = arsno;
            BusStopID = item["bstopid"]?.InnerNormalizedText();
            BusStopName = item["bstopnm"]?.InnerNormalizedText();

            if (double.TryParse(item["gpsx"]?.InnerNormalizedText(), out double gpsx) && double.TryParse(item["gpsy"]?.InnerNormalizedText(), out double gpsy))
                Location = new Geolocation(gpsy, gpsx);

            BusStopKind = item["stoptype"]?.InnerText;
        }
    }

}
