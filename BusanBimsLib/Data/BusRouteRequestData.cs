using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusanBimsLib.Data
{
    public class BusRouteRequestData : RequestDataBase
    {
        /// <summary>
        /// 버스 API ID
        /// </summary>
        [RequestData("lineid")]
        public string BusID { get; set; }
    }
}
