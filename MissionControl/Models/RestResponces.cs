using MissionControl.Models.DTOs;
using MissionControl.Statics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MissionControl.Models
{
    public class RestResponces
    {
    }

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

    /*
     * I could have used inheritance also
     * */
    public interface IResult { string Message { get; set; } }

    public class ResultUsers1 : IResult
    {
        public List<UserDTO> users { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    public class ResultUser2 : IResult
    {
        public UserDTO user { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    public class ResultUser3 : IResult
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }






    public class ResultReport1 : IResult
    {
        public List<MissionReportDTO> reports { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    public class ResultReport2 : IResult
    {
        public MissionReportDTO report { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    public class ResultReport3 : IResult
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }









    public class ResultForecast1 : IResult
    {
        public string location { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double iss_latitude { get; set; }
        public double iss_longitude { get; set; }
        public int distance { get; set; }
        public List<Res> results { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }







    public class ResultApiKey1 : IResult
    {
        public string key { get; set; }
        
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }







    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class ISS
    {
        public string name { get; set; }
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double velocity { get; set; }
        public string visibility { get; set; }
        public double footprint { get; set; }
        public int timestamp { get; set; }
        public double daynum { get; set; }
        public double solar_lat { get; set; }
        public double solar_lon { get; set; }
        public string units { get; set; }
    }
}



// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Hourly
{
    public List<string> time { get; set; }
    public List<double> temperature_2m { get; set; }
}

public class HourlyUnits
{
    public string time { get; set; }
    public string temperature_2m { get; set; }
}

public class Forecast
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public double elevation { get; set; }
    public HourlyUnits hourly_units { get; set; }
    public Hourly hourly { get; set; }
}