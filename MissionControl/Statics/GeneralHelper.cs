using MissionControl.Common;
using MissionControl.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MissionControl.Statics
{
    public class Res
    {
        public string date { get; set; }
        public string temp { get; set; }
    }

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
             * finds lowest index
             * */


            double test = double.MaxValue;
            int res = int.MaxValue;
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
             * finds lowest index
             * */
            double test = double.MaxValue;
            int res = int.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < test)
                {
                    test = list[i];
                    res = i;
                }
            }

            /*
             * we set it to something high, so it wont count in next iteration
             * */
            list[res] = double.MaxValue - 1.0d;
            return res;
        }


        public static List<Res> GetLowestTemps(Forecast fcast_a, Forecast fcast_b, int number) 
        {
            /*
             * this algorithm return the correct result, but has very poor performance
             * since i have just discovered this, I wont implement it, since it will just be copy/paste for the heap implementetion
             * the BigO notation for this is O(nlogn)
             * 
             * for each element x:
             *     if (heap.size() < k):
             *         heap.add(x)
             *     else if x < heap.max():
             *         heap.pop()
             *         heap.add(x)
             * */

            List<Res> res = new List<Res>();
            List<int> index = new List<int>();
            List<string> times = fcast_a.hourly.time;
            List<double> temps_a = fcast_a.hourly.temperature_2m;
            HeapMax heap = new HeapMax(number);

            if (number > temps_a.Count)
                throw new Exception("to high number");

            if (number < 0)
                throw new Exception("to low number");

            
            foreach (double x in temps_a)
            {
                if (heap.Length < number)
                    heap.InsertElement(x);
                else if (x < heap.PeekOfHeap())
                {
                    heap.RemoveMaximum();
                    heap.InsertElement(x);
                }
            }

            for(int i = 0; i < temps_a.Count; i++)
            {
                if (heap.Array.Contains(temps_a[i]))
                    index.Add(i);
            }

            foreach (int i in index)
                res.Add(new Res() { date = "" + times[i], temp = "" + temps_a[i] });

            return res.OrderBy(x=>double.Parse(x.temp)).Take(number).ToList();



            /*Dictionary<string, string> res = new Dictionary<string, string>();
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

            return res;/**/
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

        public static List<FacilityDTO> Locations() 
        {
            List<FacilityDTO> list = new List<FacilityDTO>();
            list.Add(new FacilityDTO() { Location = "Europe", Latitude = 55.68474022214539, Longitude = 12.50971483525464 });
            list.Add(new FacilityDTO() { Location = "China", Latitude = 41.14962602664463, Longitude = 119.33727554032843 });
            list.Add(new FacilityDTO() { Location = "America", Latitude = 40.014407426017335, Longitude = -103.68329704730307 });
            list.Add(new FacilityDTO() { Location = "Africa", Latitude = -21.02973667221353, Longitude = 23.77076788325546 });
            list.Add(new FacilityDTO() { Location = "Australia", Latitude = -33.00702098732439, Longitude = 117.83314818861444 });
            list.Add(new FacilityDTO() { Location = "India", Latitude = 19.330540162912126, Longitude = 79.14236662251713 });
            list.Add(new FacilityDTO() { Location = "Argentina", Latitude = -34.050351176517886, Longitude = -65.92682965568743 });

            return list;
        }
    }
}