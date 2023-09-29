using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Model
{
    public class CircClassMasterMod
    {
        public string classname { get; set; }
        public int totalissueddays { get; set; }
        public int noofbookstobeissued { get; set; }
        public decimal finperday { get; set; }
        public int reservedays { get; set; }
        public int totalissueddays_jour { get; set; }
        public int noofjournaltobeissued { get; set; }
        public decimal fineperday_jour { get; set; }
        public int reservedays_jour { get; set; }
        public string Status { get; set; }
        public string canRequest { get; set; }
        public decimal valueLimit { get; set; }
        public decimal Days_1Phase { get; set; }
        public decimal Amt_1Phase { get; set; }
        public decimal Days_2Phase { get; set; }
        public decimal Amt_2Phase { get; set; }
        public decimal Days_1Phasej { get; set; }
        public decimal Amt_1Phasej { get; set; }
        public decimal Days_2Phasej { get; set; }
        public decimal Amt_2Phasej { get; set; }
        public string shortname { get; set; }
        public string userid { get; set; }
        public string policystatus { get; set; }
        public string MembershipType { get; set; }
        public string Security { get; set; }
    }
}
