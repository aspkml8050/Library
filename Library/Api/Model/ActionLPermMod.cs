using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Model
{
    public class ActionLPermMod
    {
        public int userTypeId { get; set; }
        public int actionId { get; set; }
        public string permission { get; set; }
        public int? submenu_id { get; set; }
        public int? child { get; set; }
    }
}
