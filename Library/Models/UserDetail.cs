using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int userType { get; set; }
        public string Password { get; set; }
        public string MemberId { get; set; }
        public string SaltVc { get; set; }
        public string Status { get; set; }
        public string Status1 { get; set; }
        public DateTime? ValidUpto { get; set; }
    }
}