using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionControl.Models
{
    public class FacilityCentre
    {
        public string location { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int distance { get; set; }
        public long timestamp { get; set; }
    }
}