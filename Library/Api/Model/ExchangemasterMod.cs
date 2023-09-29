using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Model
{
    public class ExchangemasterMod
    {
        public int? CurrencyCode { get; set; }
        public string ShortName { get; set; }
        public string CurrencyName { get; set; }
        public decimal? GocRate { get; set; }
        public decimal? BankRate { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public string userid { get; set; }
    }
}
