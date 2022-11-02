using System.ComponentModel.DataAnnotations;

namespace MissionControl.Models.DB
{
    public class ApiKeys
    { 
        /*
         * Id could be int
         * */
        [Key]
        public long Id { get; set; }

        public string Key { get; set; }

        public string Email { get; set; }
    }
}