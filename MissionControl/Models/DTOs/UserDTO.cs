using System.Collections.Generic;

namespace MissionControl.Models.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string code_name { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }


        public IEnumerable<MissionReportDTO> missionreports { get; set; }
    }
}
/**/