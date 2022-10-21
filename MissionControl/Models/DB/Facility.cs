using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace MissionControl.Models.DB
{
    //[TableName("Facilitys")]
    public class Facilitys
    {
        [Key]
        public long Id { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double ISSLatitude { get; set; }
        public double ISSLongitude { get; set; }
        public long Timestamp { get; set; }
        /*
         * should probably have been a long
         * */
        public int Distance { get; set; }
    }
}