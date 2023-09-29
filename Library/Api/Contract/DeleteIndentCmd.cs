using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class DeleteIndentCmd
    {
        public string indentnumber { get; set; }
        public string indentid { get; set; } //only to delete indent Item, check DeleteIndentItem and DeleteIndent
        public bool ChkBudget { get; set; } 
        public bool Approval { get; set; }
        public decimal TotalAmt { get; set; } //if budget applied
        public string Session { get; set; }
    }
}
