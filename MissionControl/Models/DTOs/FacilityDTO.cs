using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionControl.Models.DTOs
{
    public class FacilityDTO
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Timestamp { get; set; }
        public int Distance { get; set; }
    }
}/**/