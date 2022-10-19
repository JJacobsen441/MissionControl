using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControl.Models.DB
{
    [Table("MissionImages")]
    public class MissionImage
    {
        [Key]
        public long Id { get; set; }
        public string CameraName { get; set; }
        public string RoverName { get; set; }
        public string RoverStatus { get; set; }
        public string Img { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        //[Required]
        public long MissionReportId { get; set; }



        public MissionReport mission_report { get; set; }
    }
}