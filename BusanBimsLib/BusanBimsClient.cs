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
public sealed partial class BusanBimsClient
{
    private readonly HttpClient http;

    private static readonly Uri baseAddress = new Uri("http://apis.data.go.kr/6260000/BusanBIMS/");

    private readonly string serviceKey;

    /// <summary>
    /// 주어진 인증키(<paramref name="serviceKey"/>)로 클라이언트 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="serviceKey">공공데이터포털에서 발급받은 Decoding 인증키</param>
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

/// <summary>
/// 버스정보시스템 API 응답 코드를 나타냅니다.
/// </summary>
public enum BusanBimsStatus : byte
{
    /// <summary>데이터를 성공적으로 가져왔습니다.</summary>
    Success = 0,
    /// <summary>애플리케이션 에러가 발생했습니다.</summary>
    ApplicationError = 1,
    /// <summary>입력하신 파라메터가 잘못되었습니다.</summary>
    InvalidParameters = 10,
    /// <summary>더 이상 API 서비스를 제공하지 않습니다.</summary>
    NoLongerService = 12,
    /// <summary>서비스가 거부되었습니다.</summary>
    AccessDenied = 20,
    /// <summary>일일 요청한도가 초과되었습니다.</summary>
    QuotaExceeded = 22,
    /// <summary>등록되지 않은 Service Key 입니다.</summary>
    UnregisteredServiceKey = 30,
    /// <summary>활용기간이 만료되었습니다.</summary>
    DeadlineExpired = 31,
    /// <summary>등록되지 않은 IP주소에서 서비스를 이용할 수 없습니다.</summary>
    UnregisteredIPAddress = 32,
    /// <summary>네트워크 에러로 데이터를 가져올 수 없습니다.</summary>
    NetworkProblem = 90,
    /// <summary>알 수 없는 에러가 발생했습니다.</summary>
    Unknown = 99
}
