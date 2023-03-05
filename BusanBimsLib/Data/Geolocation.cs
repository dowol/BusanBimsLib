using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Data
{
    public struct Geolocation
    {
        public double Latitude { get; internal set; }
        public string LatitudeFlag
        {
            get
            {
                if (Latitude > 0) return "N";
                else if (Latitude < 0) return "S";
                else return "";
            }
        }

        public double Longitude { get; internal set; } 

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
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }

    }
}
