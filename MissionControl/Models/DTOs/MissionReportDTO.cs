using System;
using System.Collections.Generic;

namespace MissionControl.Models.DTOs
{
    public class MissionReportDTO
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public DateTime mission_date { get; set; }
        public DateTime finalization_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long user_id { get; set; }



        public UserDTO user { get; set; }
        public IEnumerable<MissionImageDTO> images { get; set; }
    }
}