using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionControl.Models.DTOs
{
    public class ApiKeyDTO
    {
        /*
         * Id could be int
         * */
        public long Id { get; set; }

        public string Key { get; set; }

        public string Email { get; set; }
    }
}