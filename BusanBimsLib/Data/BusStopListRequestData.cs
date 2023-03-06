using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Data
{
    public sealed class BusStopListRequestData : RequestDataBase
    {
        [RequestData("numOfRows")]
        public int ItemsPerPage { get; set; } = 10;

        [RequestData]
        public int PageNo { get; set; } = 1;

        [RequestData("bstopnm")]
        public string? BusStopName { get; set; }

        [RequestData("arsno")]
        public string? ARSBusStopNo { get; set; }

        internal BusStopListRequestData() { }

    }
}
