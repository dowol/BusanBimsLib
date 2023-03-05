using BusanBimsLib.Data;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace BusanBimsLib;


#pragma warning disable CS8604
public partial class BusanBimsClient
{
    public async Task<BusStopListResponseData> GetBusStopList(BusStopListRequestData req)
    {
        BusanBimsResult result = await CallAsync("busStopList", req);

        if (result.Succeeded)
        {
            return new BusStopListResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    public Task<BusStopListResponseData> GetBusStopList(string busStopName)
    {
        return GetBusStopList(new BusStopListRequestData { BusStopName = busStopName });
    }

    public Task<BusStopListResponseData> GetBusStopList(int arsNo)
    {
        return GetBusStopList(new BusStopListRequestData { ARSBusStopNo = arsNo });
    }

    public async Task<BusInfoResponseData> GetBusInfo(BusInfoRequestData req)
    {
        BusanBimsResult result = await CallAsync("busInfo", req);

        if (result.Succeeded)
        {
            return new BusInfoResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    public Task<BusInfoResponseData> GetBusInfo(string busName)
    {
        return GetBusInfo(new BusInfoRequestData { LineName = busName });
    }

    
    public async Task<BusRouteResponseData> GetBusRoute(BusRouteRequestData req)
    {
        BusanBimsResult result = await CallAsync("busInfoByRouteId", req);

        if (result.Succeeded)
        {
            return new BusRouteResponseData(result.ResultData);
        }
        else
        {
            throw result.Exception ?? new BusanBimsException(BusanBimsStatus.Unknown);
        }
    }

    public Task<BusRouteResponseData> GetBusRoute(string busID)
    {
        return GetBusRoute(new BusRouteRequestData { BusID = busID });
    }
    
}

#pragma warning restore

