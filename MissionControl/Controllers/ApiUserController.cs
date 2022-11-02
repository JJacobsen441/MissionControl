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
    //[Authorize]
    public class ApiUserController : UmbracoApiController
    {
        [AuthenticationFilter()]
        [HttpGet]
        [Route("mission/users")]
        public JsonResult<ResultUsers1> GetUsers()
        {
            /*
             * endpoint name and function name, does not have to match
             * */

            try
            {
                ResultUsers1 res = new ResultUsers1();
                res.users = new List<UserDTO>();
                res.Message = "";

                DAL dal = new DAL();
                List<UserDTO> dtos = dal.GetUsers();
                res.users = dtos;
                
                return new JsonHttpStatusResult<ResultUsers1>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultUsers1 res = new ResultUsers1 {  users = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultUsers1>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [AuthenticationFilter()]
        [HttpGet]
        [Route("mission/user/{id}")]
        public JsonResult<ResultUser2> GetUser(long id)
        {
            /*
             * endpoint name and function name, does not have to match
             * */

            try
            {
                ResultUser2 res = new ResultUser2();
                res.user = new UserDTO();
                res.Message = "";

                DAL dal = new DAL();
                UserDTO dto = dal.GetUser(id);
                res.user = dto;

                return new JsonHttpStatusResult<ResultUser2>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultUser2 res = new ResultUser2 { user = null, Message = _e.Message };
                return new JsonHttpStatusResult<ResultUser2>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [AuthenticationFilter()]
        [HttpPost]
        [Route("mission/user")]
        public JsonResult<ResultUser3> CreateUser(ViewModelUserPost _u)
        {
            /*
             * endpoint name and function name, does not have to match
             * remember to sanitize inputs. invalid characters, html tags...
             * EF handles sql injections
             * */

            try
            {
                ResultUser3 res = new ResultUser3();
                res.Message = "wrong input";

                if (CheckHelper.CheckUser(_u))
                {
                    DAL dal = new DAL();
                    dal.CreateUser(_u);
                    res.Message = "user created";

                    return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.OK);
                }

                return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultUser3 res = new ResultUser3 { Message = _e.Message };
                return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [AuthenticationFilter()]
        [HttpPut]
        [Route("mission/user")]
        public JsonResult<ResultUser3> UpdateUser(ViewModelUserPut _u)
        {
            /*
             * endpoint name and function name, does not have to match
             * remember to sanitize inputs. invalid characters, html tags...
             * EF handles sql injections
             * */

            try
            {
                ResultUser3 res = new ResultUser3();
                res.Message = "wrong input";

                if (CheckHelper.CheckUser(_u))
                {
                    DAL dal = new DAL();
                    dal.UpdateUser(_u);
                    res.Message = "user updated";

                    return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.OK);
                }

                return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.OK);
            }
            catch (Exception _e)
            {
                ResultUser3 res = new ResultUser3 { Message = _e.Message };
                return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.BadRequest);
            }
        }

        [AuthenticationFilter()]
        [HttpDelete]
        [Route("mission/user/{id}")]
        public JsonResult<ResultUser3> DeleteUser(long id)
        {
            /*
             * endpoint name and function name, does not have to match
             * remember to sanitize inputs. invalid characters, html tags...
             * EF handles sql injections
             * */

            try
            {
                ResultUser3 res = new ResultUser3();
                res.Message = "wrong input";

                DAL dal = new DAL();
                dal.DeleteUser(id);
                res.Message = "user deleted";
                
                return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.OK);                
            }
            catch (Exception _e)
            {
                ResultUser3 res = new ResultUser3 { Message = _e.Message };
                return new JsonHttpStatusResult<ResultUser3>(res, this, HttpStatusCode.BadRequest);
            }
        }
    }
}
