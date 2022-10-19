using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControl.Models.DB
{
    [Table("MissionReports")]
    public class MissionReport
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime MissionDate { get; set; }
        public DateTime FinalisationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        //[Required]
        public long UserId { get; set; }



        public User user { get; set; }
        public List<MissionImage> mission_images { get; set; }
    }
}