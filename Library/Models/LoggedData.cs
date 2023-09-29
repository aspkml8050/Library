using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class LoggedData
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string User_Id { get; set; }
        public string IsAudit { get; set; }
        public string Session { get; set; }
        public string ipaddrss { get; set; }
        public DateTime sessionStartDate { get; set; }
        public DateTime sessionEndDate { get; set; }
        public DateTime LoginTime { get; set; }
    }
}