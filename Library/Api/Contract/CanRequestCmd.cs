using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class CanRequestCmd
    {
        public int? DeptId { get; set; }
        public string CanRequest { get; set; }
    }
}
