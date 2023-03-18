using BusanBimsLib.Data;
using System.Threading.Tasks;

namespace BusanBimsLib;

#pragma warning disable CS8604
public sealed partial class BusanBimsClient
{
    /// <summary>
    /// 주어진 키워드로 버스정류장 정보를 검색합니다.
    /// </summary>
    /// <param name="busStopName">버스정류장 이름 또는 키워드</param>
    /// <param name="itemsPerPage">페이지당 데이터 개수</param>
    /// <param name="page">페이지 번호</param>
    /// <returns></returns>
    public async Task<BusStopListResponseData> GetBusStopList(string busStopName, int itemsPerPage = 10, int page = 1)
    {
        BusanBimsResult result = await CallAsync("busStopList", new BusStopListRequestData
        {
            BusStopName = busStopName,
            ItemsPerPage = itemsPerPage,
            PageNo = page
        });

        if (result.Succeeded)
        {
            return new BusStopListResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    /// <summary>
    /// 주어진 ARS 고유번호에 해당하는 버스정류장 정보를 가져옵니다.
    /// </summary>
    /// <param name="arsNo">버스정류장 ARS 번호</param>
    /// <param name="itemsPerPage">페이지당 데이터 개수</param>
    /// <param name="page">페이지 번호</param>
    /// <returns></returns>
    public async Task<BusStopListResponseData> GetBusStopList(int arsNo, int itemsPerPage = 10, int page = 1)
    {
        BusanBimsResult result = await CallAsync("busStopList", new BusStopListRequestData
        {
            ARSBusStopNo = arsNo.ToString().PadLeft(5, '0'),
            ItemsPerPage = itemsPerPage,
            PageNo = page
        });

        if (result.Succeeded)
        {
            return new BusStopListResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }

    }

    /// <summary>
    /// 주어진 키워드로 버스노선 정보를 검색합니다.
    /// </summary>
    /// <param name="busName">버스 노선번호</param>
    /// <param name="busID">버스노선 API ID</param>
    /// <returns></returns>
    public async Task<BusInfoResponseData> GetBusInfo(string? busName = null, string? busID = null)
    {
        BusanBimsResult result = await CallAsync("busInfo", new BusInfoRequestData
        {
            BusID = busID,
            BusName = busName
        });

        if (result.Succeeded)
        {
            return new BusInfoResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    /// <summary>
    /// 버스 노선의 전체 정류장 리스트를 가져옵니다.
    /// </summary>
    /// <param name="busID">버스노선 API ID</param>
    /// <returns></returns>
    public async Task<BusRouteResponseData> GetBusRoute(string busID)
    {
        BusanBimsResult result = await CallAsync("busInfoByRouteId", new BusRouteRequestData
        {
            BusID = busID
        });

        if (result.Succeeded)
        {
            return new BusRouteResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    /// <summary>
    /// 주어진 버스 정류장의 도착정보를 가져옵니다.
    /// </summary>
    /// <param name="busStopID">버스정류장 API ID</param>
    /// <returns></returns>
    public async Task<BusServiceInfoResponseData> GetBusServiceInfo(string busStopID)
    {
        BusanBimsResult result = await CallAsync("stopArrByBstopid", new BusServiceInfoRequestData
        {
            BusStopID = busStopID
        });

        if (result.Succeeded)
        {
            return new BusServiceInfoResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    /// <summary>
    /// 주어진 버스 정류장과 버스 노선의 도착 정보를 가져옵니다.
    /// </summary>
    /// <param name="busStopID">버스정류장 API ID</param>
    /// <param name="busID">버스노선 API ID</param>
    /// <returns></returns>
    public async Task<BusServiceInfoResponseData> GetBusServiceInfo(string busStopID, string busID)
    {
        BusanBimsResult result = await CallAsync("busStopArrByBstopidLineid", new BusServiceInfoRequestData
        {
            BusStopID = busStopID,
            BusID = busID
        });

        if (result.Succeeded)
        {
            return new BusServiceInfoResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    /// <summary>
    /// 주어진 버스정류장(ARS 번호)의 도착정보를 가져옵니다.
    /// </summary>
    /// <param name="ars">버스정류장 ARS 번호</param>
    /// <returns></returns>
    public async Task<BusServiceInfoResponseData> GetBusServiceInfo(int ars)
    {
        BusanBimsResult result = await CallAsync("bitArrByArsno", new BusServiceInfoRequestData
        {
            ARS = ars.ToString().PadLeft(5, '0')
        });

        if (result.Succeeded)
        {
            return new BusServiceInfoResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

}

#pragma warning restore

