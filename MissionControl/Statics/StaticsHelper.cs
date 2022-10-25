using System;
using System.Configuration;

namespace MissionControl.Statics
{
    public class StaticsHelper
    {
        public static string AppSettings(string name)
        {
            if (name.IsNull())
                throw new Exception();

            string val = ConfigurationManager.AppSettings[name];
            return val;
        }
    }
}