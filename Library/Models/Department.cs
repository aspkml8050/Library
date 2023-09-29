using System;
namespace Library.Models
{
    public class Department
    {
        public int? departmentcode { get; set; }
        public string departmentname { get; set; }
        public string shortname { get; set; }
        public int? institutecode { get; set; }
        public string institutename { get; set; }

        public int? CurrentPosition { get; set; }
        public int? CurrJrnlPosition { get; set; }
        public string userid { get; set; }

    }
}
