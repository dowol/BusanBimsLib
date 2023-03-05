using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace BusanBimsLib.Data
{
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
        public string BusID { get; }
        public string BusName { get; }
        public string BusKind { get; }
        public string Company { get; }

        public string BeginingAt { get; }
        public string EndingAt { get; }
        
        public TimeOnly FirstBus { get; }
        public TimeOnly LastBus { get; }
        public TimeSpan Interval { get; }
        public TimeSpan RushHourInterval { get; }
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
}
