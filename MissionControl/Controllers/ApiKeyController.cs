using MissionControl.Filters;
using MissionControl.Models;
using MissionControl.Models.DataAccessLayer;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Umbraco.Web.WebApi;

namespace MissionControl.Controllers
{
    public class ApiKeyController : UmbracoApiController
    {

        [AuthenticationFilter()]
        [HttpGet]
        [Route("apikey/get/{id}")]
        public JsonResult<ResultApiKey1> GetApiKey(long id)
        {
            try
            {
                ResultApiKey1 res = new ResultApiKey1();
                res.Message = "";

                DAL dal = new DAL();
                UserDTO _u = dal.GetUser(id);
                ApiKeyDTO _key = dal.GetApiKey(_u.email);
                
                res.key = _key.Key;
                
                return new JsonHttpStatusResult<ResultApiKey1>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultApiKey1 res = new ResultApiKey1 { key = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultApiKey1>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("apikey/generate")]
        public JsonResult<ResultApiKey1> GenerateApiKey(ViewModelApiKeyPost _m)
        {
            /*
             * generation of apikey should not be done here (as an open api call)
             *  ..but rather on a user/member profile page, with identity login (setup witn registration confirmation email)
             *  ..that way we would secure that api calls belong to a registered user
             * but this way we get a email linked up with the key
             *  ..for tracking and mayby deletion of key???
             *  ..for user to be able to get new apikey
             *  ..other?!?
             *  there are many ways to generate the apikey
             *   ..I dont have much experience in this, but a GUID/UUID could have been used
             *   ..a combination of personal and unguessable random data???
             *   ..other?!?
             * */
            try
            {
                ResultApiKey1 res = new ResultApiKey1();
                res.Message = "wrong input";

                if (CheckHelper.CheckApiKey(_m))
                {
                    //string key = ApiKeyGenerator.CreateApiKey();
                    //res.key = key;

                    DAL dal = new DAL();
                    string _key = dal.GenerateApiKey(_m.email);
                    if (_key.IsNull())
                        throw new Exception("something went wrong");

                    res.key = _key;
                    res.Message = "key created";

                    return new JsonHttpStatusResult<ResultApiKey1>(res, this, HttpStatusCode.OK);
                }

                return new JsonHttpStatusResult<ResultApiKey1>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultApiKey1 res = new ResultApiKey1 { key = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultApiKey1>(res, this, HttpStatusCode.BadRequest);
            }
        }
    }
}