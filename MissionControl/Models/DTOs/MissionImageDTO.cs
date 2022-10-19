using System;

namespace MissionControl.Models.DTOs
{
    public class MissionImageDTO
    {
        public long Id { get; set; }
        public string camera_name { get; set; }
        public string rover_name { get; set; }
        public string rover_status { get; set; }
        public string img { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long mission_report_id { get; set; }



        public MissionReportDTO mission_report { get; set; }
    }
}