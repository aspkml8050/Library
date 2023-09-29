using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Model
{
    public class ClassmasterloadingstatusMod
    {
        public string classname { get; set; }
        public int LoadingStatus { get; set; }
        public string status { get; set; }
        public int totalissueddays { get; set; }
        public int noofbookstobeissued { get; set; }
        public decimal finperday { get; set; }
        public int reservedays { get; set; }
        public int totalissueddays_jour { get; set; }
        public int noofjournaltobeissued { get; set; }
        public decimal fineperday_jour { get; set; }
        public int reservedays_jour { get; set; }
        public decimal ValueLimit { get; set; }
        public decimal days_1phase { get; set; }
        public decimal amt_1phase { get; set; }
        public decimal days_2phase { get; set; }
        public decimal amt_2phase { get; set; }
        public decimal days_1phasej { get; set; }
        public decimal amt_1phasej { get; set; }
        public decimal days_2phasej { get; set; }
        public decimal amt_2phasej { get; set; }
        public string shortname { get; set; }
    }
}
