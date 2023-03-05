using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace BusanBimsLib.Data
{
    public sealed record class BusStopListResponseData : IEnumerable<BusStopInfo>
    {
        private readonly List<BusStopInfo> list = new();

        public int ItemsPerPage => list.Count;
        public int Page { get; }
        public int TotalCount { get; }
        public BusStopInfo[] BusStops => list.ToArray();

        internal BusStopListResponseData(XmlElement elememnt)
        {
            Page = int.Parse(elememnt["pageNo"].InnerText);
            TotalCount = int.Parse(elememnt["totalCount"].InnerText);
            foreach (XmlElement item in elememnt.GetElementsByTagName("item"))
                list.Add(new BusStopInfo(item));
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
        public int? ARS { get; }
        public string BusStopID { get; }
        public string BusStopName { get; }
        public Geolocation Geolocation { get; }
        public string BusStopKind { get; }

        internal BusStopInfo(XmlElement item)
        {
            if (int.TryParse(item["arsno"]?.InnerText, out int arsno))
                ARS = arsno;
            BusStopID = item["bstopid"].InnerText.Normalize();
            BusStopName = item["bstopnm"].InnerText.Normalize();
            Geolocation = new Geolocation(
                latitude: double.Parse(item["gpsy"].InnerText),
                longitude: double.Parse(item["gpsx"].InnerText)
                );
            BusStopKind = item["stoptype"].InnerText;
        }
    }

}
