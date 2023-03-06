using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Data
{
    /// <summary>
    /// GPS 좌표 값을 나타냅니다.
    /// </summary>
    public struct Geolocation
    {
        /// <summary>위도</summary>
        public double Latitude { get; internal set; }

        /// <summary>위도(북위 'N' / 남위 'S')</summary>
        public string LatitudeFlag
        {
            get
            {
                if (Latitude > 0) return "N";
                else if (Latitude < 0) return "S";
                else return "";
            }
        }

        /// <summary>경도</summary>
        public double Longitude { get; internal set; }

        /// <summary>경도(동경 'E' / 서경 'W')</summary>
        public string LongitudeFlag
        {
            get
            {
                if (Longitude > 0) return "E";
                else if (Longitude < 0) return "W";
                else return "";
            }
        }

        public Geolocation(double latitude, double longitude)
        {
            Latitude = Math.Round(latitude, 6);
            Longitude = Math.Round(longitude, 6);
        }

        public override string ToString()
        {
            return $"{Math.Abs(Latitude):.000000} {LatitudeFlag}, {Math.Abs(Longitude):.000000} {LongitudeFlag}".Trim();
        }

    }
}
