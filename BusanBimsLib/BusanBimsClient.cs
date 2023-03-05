using BusanBimsLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BusanBimsLib
{
    public partial class BusanBimsClient
    {
        private readonly HttpClient http;

        private static readonly Uri baseAddress = new Uri("http://apis.data.go.kr/6260000/BusanBIMS/");

        private readonly string serviceKey;

        public BusanBimsClient(string serviceKey)
        {
            http = new HttpClient();
            this.serviceKey = serviceKey;
        }


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
        Success = 0,
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
}
