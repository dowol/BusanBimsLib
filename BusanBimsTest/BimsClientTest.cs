using BusanBimsLib;
using BusanBimsLib.Data;

namespace BusanBimsTest
{
    [TestClass]
    public class BimsClientTest
    {
        public BusanBimsClient bis = new(File.ReadAllText("./api-accesskey.txt").Trim());
        
        [DataTestMethod]
        [DataRow("부산외국어대학교")]
        [DataRow("부산역")]
        [DataRow("범어사역")]
        [DataRow("해운대해수욕장")]
        public async Task BusStopList(string busStopName)
        {
            
            try
            {
                BusStopListResponseData res = await bis.GetBusStopList(busStopName);
                Console.WriteLine(res.Describe());
            }
            catch(Exception e) { Console.Error.WriteLine(e); }
            
        }

        [DataTestMethod]
        [DataRow("80")]
        [DataRow("51")]
        [DataRow("금정구3")]
        [DataRow("301")]
        [DataRow("6")]
        [DataRow("심야")]
        public async Task BusInfo(string lineName)
        {
            try
            {
                BusInfoResponseData res = await bis.GetBusInfo(lineName);
                Console.WriteLine(res.Describe());
            }
            catch(Exception e) { Console.Error.WriteLine(e); }
        }

        [DataTestMethod]
        [DataRow("5200080000")]
        [DataRow("5200301000")]
        [DataRow("5200051000")]
        [DataRow("5201002000")]
        [DataRow("5201002F00")]
        [DataRow("5291103000")]
        public async Task BusRoute(string busID)
        {
            try
            {
                BusRouteResponseData res = await bis.GetBusRoute(busID);
                Console.WriteLine(res.Describe());
            }
            catch(Exception e) { Console.Error.WriteLine(e); }
        }
    }

    
}