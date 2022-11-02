using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionControl.Models
{
    public class ViewModelUserPost
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string code_name { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }


        //public IEnumerable<MissionReportDTO> missionreports { get; set; }
    }

    public class ViewModelUserPut
    {
        public long id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string code_name { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }


        //public IEnumerable<MissionReportDTO> missionreports { get; set; }
    }

    public class ViewModelMissionreportPost
    {
        //public long user_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public long mission_date { get; set; }
        public long finalization_date { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public long deleted_at { get; set; }
        public long user_id { get; set; }
    }

    public class ViewModelMissionreportPut
    {
        //public long user_id { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public long mission_date { get; set; }
        public long finalization_date { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public long deleted_at { get; set; }
        public long user_id { get; set; }
    }

    public class ViewModelApiKeyPost
    {
        //public long user_id { get; set; }
        public string email { get; set; }
    }
}