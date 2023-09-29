using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class ProgramCmd
    {
        public string ProgramName { get; set; }
        public string Shortname { get; set; }
        public int? deptcode { get; set; }
    }
}
