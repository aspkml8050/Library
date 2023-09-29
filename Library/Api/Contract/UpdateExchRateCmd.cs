using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class UpdateExchRateCmd
    {
        public decimal ApplicableExchangeRate { get; set; }
        public decimal orderexchangerate { get; set; }
        public string indentid { get; set; }
    }
}
