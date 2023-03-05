using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Data
{
    public class BusInfoRequestData : RequestDataBase
    {
        [RequestData("lineid")]
        public string? LineID { get; set; }

        [RequestData("lineno")]
        public string? LineName { get; set; }


    }
}
