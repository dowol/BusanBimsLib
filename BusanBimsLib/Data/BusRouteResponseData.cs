using BusanBimsLib.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace BusanBimsLib.Data
{
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
        public int? ARS { get; }
        public TimeSpan? Interval { get; }
        public int? Order { get; }
        public string? BusStopName { get; }
        public string? CarPlate { get; }
        public int? Direction { get; }
        // public DateTime? GPSTime { get; } // Not Implemented
        public Geolocation? Location { get; }
        public string? NodeID { get; }
        public BusRouteNodeType? NodeType { get; }
        public bool? IsReturningPoint { get; }
        public bool? IsLowPlateBus { get; }


        public BusRouteNode(XmlElement element)
        {
            if (int.TryParse(element["arsno"]?.InnerNormalizedText(), out int arsno))
                ARS = arsno;

            if (int.TryParse(element["avgym"]?.InnerNormalizedText(), out int avgym))
                Interval = TimeSpan.FromSeconds(avgym);

            if (int.TryParse(element["bstopidx"]?.InnerNormalizedText(), out int bstopidx))
                Order = bstopidx;

            BusStopName = element["bstopnm"]?.InnerNormalizedText();

            CarPlate = element["carno"]?.InnerNormalizedText();

            if (int.TryParse(element["direction"]?.InnerNormalizedText(), out int direction))
                Direction = direction;

            /*{
                string? gpsym = element["gpsym"]?.InnerNormalizedText();
                if(!string.IsNullOrWhiteSpace(gpsym))
                {
                    DateTime today = DateTime.Today;
                    GPSTime = new(
                        year:  today.Year,
                        month: today.Month,
                        day:   today.Day,
                        hour: int.Parse(gpsym[1..2]) % 24,
                        minute: int.Parse(gpsym[3..4]),
                        second: int.Parse(gpsym[5..])
                    );
                }
            }*/

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

    public enum BusRouteNodeType : byte { Intersection = 0, BusStop = 3 }
}
