using MissionControl.Filters;
using MissionControl.Models;
using MissionControl.Models.DataAccessLayer;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Umbraco.Web.WebApi;

namespace MissionControl.Controllers
{
    public class WeatherController : UmbracoApiController
    {
        [AuthenticationFilter()]
        [HttpGet]
        [Route("weather/forecast/{number}")]
        public JsonResult<ResultForecast1> GetForecast(int number)
        {
            /*
             * endpoint name and function name, does not have to match
             * */

            try
            {
                DAL dal = new DAL();
                ResultForecast1 _res = new ResultForecast1();
                List<Res> res = new List<Res>();

                FacilityDTO dto = dal.GetClosest5min();
                Forecast fcast_a = RestHelper.ForecastGET(dto.Latitude, dto.Longitude);
                Forecast fcast_b = GeneralHelper.Copy(fcast_a);
                res = GeneralHelper.GetLowestTemps(fcast_a, fcast_b, number);
                
                _res.results = res;
                _res.location = dto.Location;
                _res.latitude = dto.Latitude;
                _res.longitude = dto.Longitude;
                _res.iss_latitude = dto.ISSLatitude;
                _res.iss_longitude = dto.ISSLongitude;
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