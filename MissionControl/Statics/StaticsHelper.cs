using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MissionControl.Statics
{
    public class StaticsHelper
    {
        public static string AppSettings(string name)
        {
            //using reflection fetch the value and return it
            //This code changes whether Properties is a static or instance, let me know if you need help here

            if (name.IsNull())
                throw new Exception();

            string val = ConfigurationManager.AppSettings[name];
            return val;
        }
    }
}