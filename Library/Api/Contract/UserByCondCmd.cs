using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class UserByCondCmd
    {
        public string UserCode { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ClassName { get; set; }
        public int? ProgramId { get; set; }
        public int? DeptCode { get; set; }
        public bool CanRequest { get; set; }

    }
}
