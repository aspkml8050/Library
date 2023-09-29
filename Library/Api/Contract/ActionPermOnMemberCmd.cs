using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class ActionPermOnMemberCmd
    {
        public string MemberId { get; set; }
        public int ActionId { get; set; }
        public int SubmenuId { get; set; }
    }
}
