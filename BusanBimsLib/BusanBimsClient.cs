using BusanBimsLib.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BusanBimsLib;

/// <summary>
/// 부산시 버스정보시스템 API 클라이언트
/// </summary>
public partial class BusanBimsClient
{
    private readonly HttpClient http;

    private static readonly Uri baseAddress = new Uri("http://apis.data.go.kr/6260000/BusanBIMS/");

    private readonly string serviceKey;

    /// <summary>
    /// 주어진 인증키(<paramref name="serviceKey"/>)로 클라이언트 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="serviceKey">data.or.kr에서 발급받은 Decoded 인증키</param>
    public BusanBimsClient(string serviceKey)
    {
        http = new HttpClient();
        this.serviceKey = serviceKey;
    }

#pragma warning disable CS8602
    private async Task<BusanBimsResult> CallAsync(string serviceName, RequestDataBase qd)
    {
        
        Uri req = new(baseAddress, serviceName + qd.ToQueryString(serviceKey));
#if DEBUG
        Debug.WriteLine("Request URL: " + req);
#endif

        HttpResponseMessage res;

        try
        {
            res = await http.GetAsync(req);
            XmlDocument xd = new();
            xd.Load(new StreamReader(await res.Content.ReadAsStreamAsync(), Encoding.UTF8));
#if DEBUG
            Trace.WriteLine("API Response Body:");
            Trace.WriteLine(XDocument.Parse(xd.InnerXml));
#endif

            if (res.IsSuccessStatusCode)
            {
                return new BusanBimsResult(BusanBimsStatus.Success, result: xd["response"]["body"]);
            }
            else
            {
                BusanBimsStatus status = (BusanBimsStatus)byte.Parse(xd["OpenAPI_ServiceResponse"]["cmmMsgHeader"]["returnReasonCode"].InnerText);
                return new BusanBimsResult(status, exception: new BusanBimsException(status));
            }
        }
        catch(HttpRequestException e)
        {
            return new BusanBimsResult(BusanBimsStatus.NetworkProblem, exception: e);
        }
        catch(Exception e)
        {
            return new BusanBimsResult(BusanBimsStatus.Unknown, exception: e);
        }

    }
}
#pragma warning restore CS8602

internal struct BusanBimsResult
{
    internal BusanBimsStatus Status { get; set; }
    internal bool Succeeded => Status == BusanBimsStatus.Success;
    internal XmlElement? ResultData { get; set; }
    internal Exception? Exception { get; set; }

    internal BusanBimsResult(BusanBimsStatus status, XmlElement? result = null, Exception? exception = null)
    {
        Status = status;
        ResultData = result;
        Exception = exception;
    }
}

public enum BusanBimsStatus : byte
{
    /// <summary>데이터를 성공적으로 가져왔습니다.</summary>
    Success = 0,
    /// <summary>애플리케이션 에러가 발생했습니다.</summary>
    ApplicationError = 1,
    InvalidParameters = 10,
    NoLongerService = 12,
    AccessDenied = 20,
    QuotaExceeded = 22,
    UnregisteredServiceKey = 30,
    DeadlineExpired = 31,
    UnregisteredIPAddress = 32,
    NetworkProblem = 90,
    Unknown = 99
}
