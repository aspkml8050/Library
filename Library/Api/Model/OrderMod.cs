using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Model
{
    //No validation yet as api side
    public class OrderMod
    {
        public string  ordernumber { get; set; }
        public DateTime? exparivaldateapproval { get; set; }
        public DateTime? exparivaldatenonapproval { get; set; }
        public string  indentnumber { get; set; }
        public DateTime? orderdate { get; set; }
        public string  letternumber { get; set; }
        public DateTime? letterdate { get; set; }
        public bool cancelorder { get; set; }
        public int? itemnumber { get; set; }
        public int? departmentcode { get; set; }
        public decimal orderamount { get; set; }
        public string  vendorid { get; set; }  //fk
        public int? identityofordernumber { get; set; }
        public int? order_check_code { get; set; }
        public string  isAdvancePaid { get; set; } = "n";
        public string  status { get; set; } = "n";
        public DateTime? validitydate { get; set; }
        public string  userid { get; set; }
        public int? OrderStatus { get; set; }
        public string  IpAddress { get; set; }
    }
}
