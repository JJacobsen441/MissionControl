using MissionControl.Models;
using System;
using System.Collections.Generic;

namespace MissionControl.Statics
{
    public class GeneralHelper
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            /*
             * not ny code
             * */

            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static int GetIndex(List<double> list, List<double> _contains)
        {
            /*
             * not good
             * */
            double test = 1000.0;
            int res = 1000;
            for (int i = 0; i < list.Count; i++)
            {
                if (!_contains.Contains(list[i]) && list[i] < test)
                {
                    test = list[i];
                    res = i;
                }
            }

            _contains.Add(test);
            return res;
        }

        public static int GetIndex(List<double> list)
        {
            /*
             * good
             * */
            double test = 1000.0d;
            int res = 1000;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < test)
                {
                    test = list[i];
                    res = i;
                }
            }
                        
            list[res] = 999.0d;
            return res;
        }

        public static Dictionary<string, string> GetLowestTemps(Forecast fcast_a, Forecast fcast_b, int number) 
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            List<string> times = fcast_a.hourly.time;
            List<double> temps_a = fcast_a.hourly.temperature_2m;
            List<double> temps_b = fcast_b.hourly.temperature_2m;

            if (number > temps_a.Count)
                throw new Exception("to high number");

            if (number < 0)
                throw new Exception("to low number");

            int[] index = new int[number];
            for (int i = 0; i < number; i++)
                index[i] = GeneralHelper.GetIndex(temps_a);

            foreach (int i in index)
                res.Add(times[i], "" + temps_b[i]);

            return res;
        }

        public static Forecast Copy(Forecast _b)
        {
            Forecast _a = new Forecast();
            _a.hourly = new Hourly();
            _a.hourly.time = new List<string>();
            _a.hourly.temperature_2m = new List<double>();

            foreach (string _s in _b.hourly.time)
                _a.hourly.time.Add("" + _s);

            foreach (double _d in _b.hourly.temperature_2m)
                _a.hourly.temperature_2m.Add(_d);

            return _a;
        }

        public static List<FacilityCentre> Locations() 
        {
            List<FacilityCentre> list = new List<FacilityCentre>();
            list.Add(new FacilityCentre() { location = "Europe", latitude = 55.68474022214539, longitude = 12.50971483525464 });
            list.Add(new FacilityCentre() { location = "China", latitude = 41.14962602664463, longitude = 119.33727554032843 });
            list.Add(new FacilityCentre() { location = "America", latitude = 40.014407426017335, longitude = -103.68329704730307 });
            list.Add(new FacilityCentre() { location = "Africa", latitude = -21.02973667221353, longitude = 23.77076788325546 });
            list.Add(new FacilityCentre() { location = "Australia", latitude = -33.00702098732439, longitude = 117.83314818861444 });
            list.Add(new FacilityCentre() { location = "India", latitude = 19.330540162912126, longitude = 79.14236662251713 });
            list.Add(new FacilityCentre() { location = "Argentina", latitude = -34.050351176517886, longitude = -65.92682965568743 });

            return list;
        }
    }
}