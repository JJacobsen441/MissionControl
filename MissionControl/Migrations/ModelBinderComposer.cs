using MissionControl.Models;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Umbraco.Core;
using Umbraco.Core.Composing;
using System.Web.Http.Controllers;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;

namespace MissionControl.Migrations
{
    /*
     * You could make more composer if that is needed
     * and then use: [ComposeBefore(typeof(xxxx))] ..or after
     * */
    
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class CustomBinderComposer : ComponentComposer<CustomBinderComponent>
    {
    }

    public class CustomBinderComponent : IComponent
    {

        public void Initialize()
        {
            /*
             * these lines were hard to figure out
             * */
            GlobalConfiguration.Configuration.Services.Insert(typeof(ModelBinderProvider), 0, new MissionPostCustomBinderProvider());
            GlobalConfiguration.Configuration.Services.Insert(typeof(ModelBinderProvider), 0, new MissionPutCustomBinderProvider());
            //for standart mvc use:
            //System.Web.Mvc.ModelBinders.Binders.Add(typeof(ViewModelMissionreportPost), new MissionPostCustomBinder());
        }

        public void Terminate()
        {
        }
    }

    public class MissionPostCustomBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return modelType == typeof(ViewModelMissionreportPost) ? new MissionPostCustomBinder() : null;
        }
    }


    public class MissionPostCustomBinder : IModelBinder
    {
        private double ConvertDouble(string _in)
        {
            if (string.IsNullOrEmpty(_in))
                throw new Exception();

            //checks for bad user input
            _in = _in.Replace(",", ".");
            return double.Parse(_in, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"));
        }

        private string ConvertString(string _in)
        {
            if (string.IsNullOrEmpty(_in))
                throw new Exception();

            return _in;
        }

        private long ConvertLong(string _in)
        {
            if (string.IsNullOrEmpty(_in))
                throw new Exception();

            return long.Parse(_in);
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            /*
             * I noticed that fx 5,5 was converted to to 55 in the action
             * this way we catch comma/dot errors
             * this took awhile to get working, again it was namaspace problems:
             * System.Web.Mvc vs System.Web.Http
             * */
            try
            {
                //string cookie = HttpContext.Current.Request.Cookies["culture"].Value;
                //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cookie);
                //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cookie);
                //throw new Exception();
                //var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "lat");

                HttpRequest req = HttpContext.Current.Request;

                string json = "";

                req.InputStream.Position = 0;
                using (var inputStream = new StreamReader(req.InputStream))
                {
                    json = inputStream.ReadToEnd();
                }

                JavaScriptSerializer js_ser = new JavaScriptSerializer();
                
                //this way we can do our own parsing
                //Dictionary<string, string> dict = js_ser.Deserialize<Dictionary<string, string>>(json);
                //ViewModelMissionreportPost res = new ViewModelMissionreportPost();
                //res.name = ConvertString(dict["name"]);
                //res.description = ConvertString(dict["description"]);
                //res.lat = ConvertDouble(dict["lat"]);
                //res.lng = ConvertDouble(dict["lng"]);
                //res.mission_date = ConvertLong(dict["mission_date"]);
                //res.finalization_date = ConvertLong(dict["finalization_date"]);
                //res.created_at = ConvertLong(dict["created_at"]);
                //res.updated_at = ConvertLong(dict["updated_at"]);
                //res.deleted_at = ConvertLong(dict["deleted_at"]);
                //res.user_id = ConvertLong(dict["user_id"]);
                //bindingContext.Model = res;

                //this way we just get an error if wrong format
                bindingContext.Model = js_ser.Deserialize<ViewModelMissionreportPost>(json);

                bindingContext.ValidationNode.ValidateAllProperties = true;
                bindingContext.ValidationNode.Validate(actionContext);
                
                //throw new Exception();
                
                return true;
            }
            catch (Exception _e)
            {
                bindingContext.ModelState.AddModelError("myerror", _e);
                return false;
            }
        }
    }

    public class MissionPutCustomBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return modelType == typeof(ViewModelMissionreportPost) ? new MissionPutCustomBinder() : null;
        }
    }

    public class MissionPutCustomBinder : IModelBinder
    {
        private double ConvertDouble(string _in)
        {
            if (string.IsNullOrEmpty(_in))
                throw new Exception();

            //checks for bad user input
            _in = _in.Replace(",", ".");
            return double.Parse(_in, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"));
        }

        private string ConvertString(string _in)
        {
            if (string.IsNullOrEmpty(_in))
                throw new Exception();

            return _in;
        }

        private long ConvertLong(string _in)
        {
            if (string.IsNullOrEmpty(_in))
                throw new Exception();

            return long.Parse(_in);
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            /*
             * I noticed that fx 5,5 was converted to to 55 in the action
             * this way we catch comma/dot errors
             * this took awhile to get working, again it was namaspace problems
             * System.Web.Mvc vs System.Web.Http
             * */
            try
            {
                HttpRequest req = HttpContext.Current.Request;

                string json = "";

                req.InputStream.Position = 0;
                using (var inputStream = new StreamReader(req.InputStream))
                {
                    json = inputStream.ReadToEnd();
                }

                JavaScriptSerializer js_ser = new JavaScriptSerializer();

                //this way we can do our own parsing
                //Dictionary<string, string> dict = js_ser.Deserialize<Dictionary<string, string>>(json);
                //ViewModelMissionreportPost res = new ViewModelMissionreportPost();
                //res.name = ConvertString(dict["name"]);
                //res.description = ConvertString(dict["description"]);
                //res.lat = ConvertDouble(dict["lat"]);
                //res.lng = ConvertDouble(dict["lng"]);
                //res.mission_date = ConvertLong(dict["mission_date"]);
                //res.finalization_date = ConvertLong(dict["finalization_date"]);
                //res.created_at = ConvertLong(dict["created_at"]);
                //res.updated_at = ConvertLong(dict["updated_at"]);
                //res.deleted_at = ConvertLong(dict["deleted_at"]);
                //res.user_id = ConvertLong(dict["user_id"]);
                //bindingContext.Model = res;

                //this way we just get an error if wrong format
                bindingContext.Model = js_ser.Deserialize<ViewModelMissionreportPut>(json);

                return true;
            }
            catch (Exception _e)
            {
                bindingContext.ModelState.AddModelError("error", _e);
                return false;
            }
        }
    }
}