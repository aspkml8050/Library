using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class IndentCmd
    {
        public string VendorId { get; set; } //mandatory for indent only
        public int? Departmentcode { get; set; } //optional, applicable for gift indent only
    }
}
