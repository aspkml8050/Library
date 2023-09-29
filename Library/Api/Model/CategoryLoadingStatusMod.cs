using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Model
{
    public class CategoryLoadingStatusMod
    {
        public int? Id { get; set; }
        public string Category_LoadingStatus { get; set; }
        public string Abbreviation { get; set; }
        public byte[] cat_icon { get; set; }
        public string userid { get; set; }

    }
}
