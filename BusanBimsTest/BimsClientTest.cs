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
        [DataRow("범어사")]
        [DataRow("해운대해수욕장")]
        [DataRow("서면")]
        [DataRow("공항")]
        [DataRow("터미널")]
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
        [DataRow("33")]
        [DataRow("심야")]
        [DataRow("131")]
        [DataRow("300")]
        [DataRow("29")]
        [DataRow("49")]
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


        [DataTestMethod]
        [DataRow("174890202")]
        [DataRow("509960000")]
        [DataRow("174130201")]
        [DataRow("185580201")]
        public async Task BusServiceInfoByBusStop(string busStopID)
        {
            try
            {
                BusServiceInfoResponseData res = await bis.GetBusServiceInfo(busStopID);
                Console.WriteLine(res.Describe());
            }
            catch(Exception e) { Console.Error.WriteLine(e); }
        }

        [DataTestMethod]
        [DataRow("164710101", "5200085000")]
        [DataRow("175290201", "5201002F00")]
        [DataRow("175290201", "5200049000")]
        [DataRow("175290201", "5200051000")]
        [DataRow("175290201", "5200080000")]
        public async Task BusServiceInfo(string busStopID, string busID)
        {
            try
            {
                BusServiceInfoResponseData res = await bis.GetBusServiceInfo(busStopID, busID);
                Console.WriteLine(res.Describe());
            }
            catch (Exception e) { Console.Error.WriteLine(e); }
        }

        [DataTestMethod]
        [DataRow(11007)]
        [DataRow( 5190)]
        [DataRow( 5365)]
        public async Task BusServiceInfoByARSNo(int ars)
        {
            try
            {
                BusServiceInfoResponseData res = await bis.GetBusServiceInfo(ars);
                Console.WriteLine(res.Describe());
            }
            catch (Exception e) { Console.Error.WriteLine(e); }
        }
    }

    
}