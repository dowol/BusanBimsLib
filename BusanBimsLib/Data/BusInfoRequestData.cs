using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Data
{
    public class BusInfoRequestData : RequestDataBase
    {
        /// <summary>
        /// 
        /// </summary>
        [RequestData("lineid")]
        public string? BusID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [RequestData("lineno")]
        public string? BusName { get; set; }

        internal BusInfoRequestData() { }
    }
}
