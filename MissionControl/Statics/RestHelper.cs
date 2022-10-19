using MissionControl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace MissionControl.Statics
{
    public class RestHelper
    {
        public static List<ISS> ISSGET(long timestamp)
        {
            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.wheretheiss.at",
                    "v1/satellites/25544/positions",
                    "timestamps=" + timestamp,//params
                    "",//accept
                    "",//"application/json;charset=utf-8",//contenttype
                    "",//api_key
                    ""//token
                    );

            string s_res = res.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(s_res))
            {
                List<ISS> _res = JsonConvert.DeserializeObject<List<ISS>>(s_res);
                return _res;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Forecast ForecastGET(double lat, double lng)
        {
            string _lat = ("" + lat).Replace(",", ".");
            string _lng = ("" + lng).Replace(",", ".");

            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.open-meteo.com",
                    "v1/forecast?latitude=" + _lat + "&longitude=" + _lng + "&hourly=temperature_2m",
                    //"https://api.open-meteo.com",
                    //"v1/forecast?latitude=" + ("" + lat).Substring(0, 4) + "&longitude=" + ("" + lng).Substring(0, 4) + "&hourly=temperature_2m",
                    "",//params
                    "",//accept
                    "",//"application/json;charset=utf-8",//contenttype
                    "",//api_key
                    ""//token
                    );

            string s_res = res.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(s_res))
            {
                Forecast _res = JsonConvert.DeserializeObject<Forecast>(s_res);
                return _res;
            }
            else
            {
                throw new Exception();
            }
        }

        public static HttpResponseMessage Send(HttpMethod method,
            string _json,
            string _base,
            string _path,
            string _params,
            string _accept,
            string _contenttype,
            string _apikey,
            string _token

            )
        {
            using (var client = new HttpClient())
            {
                _params = _params != "" ? "?" + _params : "";

                _apikey = _params == "" && _apikey != "" ? "?api_key=" + _apikey :
                          _params != "" && _apikey != "" ? "&api_key=" + _apikey : "";

                client.BaseAddress = new Uri(_base);
                HttpRequestMessage req = new HttpRequestMessage(method, "/" + _path + _params + _apikey);

                if (_json != "")
                    req.Content = new StringContent(_json, Encoding.UTF8, _contenttype);

                if (_accept != "")
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_accept));//ACCEPT header application/x-www-form-urlencoded

                if (_token != "")
                    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                if (_contenttype != "")
                    req.Content.Headers.ContentType = new MediaTypeHeaderValue(_contenttype);

                HttpResponseMessage response = client.SendAsync(req).Result;

                return response;
            }
        }
    }
}