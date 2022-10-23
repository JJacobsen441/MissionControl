using MissionControl.Filters;
using MissionControl.Migrations;
using MissionControl.Models;
using MissionControl.Models.DataAccessLayer;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Umbraco.Web.WebApi;

namespace MissionControl.Controllers
{

    
    //[Authorize]
    public class ApiReportController : UmbracoApiController
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







        [BasicAuthenticationFilter("MissionControl", "asdf123456", BasicRealm = "mydomain")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("mission/missionreports")]
        public JsonResult<ResultReport1> GetMissionReports()
        {
            /*
             * endpoint name and function name, does not have to match
             * */

            try
            {
                ResultReport1 res = new ResultReport1();
                res.reports = new List<MissionReportDTO>();
                res.Message = "";

                DAL dal = new DAL();
                List<MissionReportDTO> dtos = dal.GetMissionReports();
                res.reports = dtos;

                return new JsonHttpStatusResult<ResultReport1>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultReport1 res = new ResultReport1 { reports = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultReport1>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [BasicAuthenticationFilter("MissionControl", "asdf123456", BasicRealm = "mydomain")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("mission/missionreport/{id}")]
        public JsonResult<ResultReport2> GetMissionReport(long id)
        {
            /*
             * endpoint name and function name, does not have to match
             * */

            try
            {
                ResultReport2 res = new ResultReport2();
                res.report = new MissionReportDTO();
                res.Message = "";

                DAL dal = new DAL();
                MissionReportDTO dto = dal.GetMissionReport(id);
                res.report = dto;

                return new JsonHttpStatusResult<ResultReport2>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultReport2 res = new ResultReport2 { report = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultReport2>(res, this, HttpStatusCode.BadRequest);
            }
        }
                
        [BasicAuthenticationFilter("MissionControl", "asdf123456", BasicRealm = "mydomain")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("mission/missionreport")]
        public JsonResult<ResultReport3> CreateMissionReport([ModelBinder(typeof(MissionPostCustomBinder))] ViewModelMissionreportPost _m)
        {
            /*
             * should be url encoded
             * endpoint name and function name, does not have to match
             * remember to sanitize inputs. invalid characters, html tags...
             * this isnt fully sanitized, we should also check for XSS
             * EF handles sql injections
             * */

            try
            {
                ResultReport3 res = new ResultReport3();
                res.Message = "wrong input";
                
                if (ModelState.IsValid)
                {
                    if (CheckHelper.CheckMissionReport(_m))
                    {
                        DAL dal = new DAL();
                        dal.CreateMissionReport(_m);
                        res.Message = "report created";

                        return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.OK);
                    }
                }

                return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultReport3 res = new ResultReport3 { Message = _e.Message };
                return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [BasicAuthenticationFilter("MissionControl", "asdf123456", BasicRealm = "mydomain")]
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("mission/missionreport")]
        public JsonResult<ResultReport3> UpdateMissionReport([ModelBinder(typeof(MissionPutCustomBinder))] ViewModelMissionreportPut _m)
        {
            /*
             * should be url encoded
             * endpoint name and function name, does not have to match
             * remember to sanitize inputs. invalid characters, html tags...
             * this isnt fully sanitized, we should also check for XSS
             * EF handles sql injections
             * */

            try
            {
                ResultReport3 res = new ResultReport3();
                res.Message = "wrong input";

                if(ModelState.IsValid)
                {
                    if (CheckHelper.CheckMissionReport(_m))
                    {
                        DAL dal = new DAL();
                        dal.UpdateMissionReport(_m);
                        res.Message = "report updated";

                        return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.OK);
                    }
                }

                return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultReport3 res = new ResultReport3 { Message = _e.Message };
                return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [BasicAuthenticationFilter("MissionControl", "asdf123456", BasicRealm = "mydomain")]
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("mission/missionreport/{id}")]
        public JsonResult<ResultReport3> DeleteMissionReport(long id)
        {
            /*
             * should be url encoded
             * endpoint name and function name, does not have to match
             * remember to sanitize inputs. invalid characters, html tags...
             * this isnt fully sanitized, we should also check for XSS
             * EF handles sql injections
             * */

            try
            {
                ResultReport3 res = new ResultReport3();
                res.Message = "wrong input";

                DAL dal = new DAL();
                dal.DeleteMissionReport(id);
                res.Message = "report deleted";

                return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultReport3 res = new ResultReport3 { Message = _e.Message };
                return new JsonHttpStatusResult<ResultReport3>(res, this, HttpStatusCode.BadRequest);
            }
        }
    }
}
