using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract.Search
{
    public class AccnSearchCmd
    {
        public string Accn { get; set; }
        public string Title { get; set; }
        public int? PubYear { get; set; }
        public string BillNo { get; set; }
        public string Vendor { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int PageSize { get; set; } = 350;
        public int PageNo { get; set; } = 1;
    }
}
