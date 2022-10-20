using MissionControl.Filters;
using MissionControl.Models;
using MissionControl.Models.DataAccessLayer;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Umbraco.Web.WebApi;

namespace MissionControl.Controllers
{
    public class WeatherController : UmbracoApiController
    {
        public class JsonHttpStatusResult<T> : JsonResult<T>
        {
            /*
             * not my code
             * */
            private readonly HttpStatusCode _httpStatus;

            public JsonHttpStatusResult(T content, ApiController controller, HttpStatusCode httpStatus)
            : base(content, new JsonSerializerSettings(), new UTF8Encoding(), controller)
            {
                _httpStatus = httpStatus;
            }

            public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var returnTask = base.ExecuteAsync(cancellationToken);
                returnTask.Result.StatusCode = _httpStatus;// HttpStatusCode.BadRequest;
                return returnTask;
            }
        }

        


        [BasicAuthenticationFilter("WeatherControl", "asdf123456", BasicRealm = "mydomain")]
        [HttpGet]
        [Route("weather/forecast/{number}")]
        public JsonResult<ResultForecast1> Forecast(int number)
        {
            /*
             * endpoint name and function name, does not have to match
             * */

            try
            {
                DAL dal = new DAL();
                ResultForecast1 _res = new ResultForecast1();
                Dictionary<string, string> res = new Dictionary<string, string>();

                FacilityDTO dto = dal.GetClosest5min();
                Forecast fcast_a = RestHelper.ForecastGET(dto.Latitude, dto.Longitude);
                Forecast fcast_b = GeneralHelper.Copy(fcast_a);
                res = GeneralHelper.GetLowestTemps(fcast_a, fcast_b, number);
                
                _res.results = res;
                _res.location = dto.Location;
                _res.latitude = dto.Latitude;
                _res.longitude = dto.Longitude;
                _res.distance = dto.Distance;
                _res.Message = "";
                
                return new JsonHttpStatusResult<ResultForecast1>(_res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultForecast1 res = new ResultForecast1 { results = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultForecast1>(res, this, HttpStatusCode.BadRequest);
            }
        }        
    }
}