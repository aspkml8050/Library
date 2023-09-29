using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class OrderOpt
    {
        /*available combos 
          normal advance dept
          normal advance vend
          normal order 
          normal
          gift advance dept
          gift advance vend
          gift order 
          gift 
        */
        public bool Normal { get; set; }=true; //or gift
        public bool Gift { get; set; }  
        public bool Advnance { get; set; } //eithor of advance/order
        public bool Order { get; set; }

        public bool Dept { get; set; }//eithor of dept/vend
        public bool Vend { get; set; }
        public string SearchTxt { get; set; }




    }
}
