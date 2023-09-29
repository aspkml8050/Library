using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    public class CircUserManagementCmd // also as model
    {
        public string usercode { get; set; }
        public string userid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public int? departmentcode { get; set; }
        public DateTime? validupto { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public int? issuedbookstatus { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string gender { get; set; }
        public DateTime? doj { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public byte[]  memberPic { get; set; }
        public int? issuedjournalstatus { get; set; }
        public string classname { get; set; }
        public string passwd { get; set; }
        public string saltVc { get; set; }
        public DateTime? Deactivatedon { get; set; }
        public string Fathername { get; set; }
        public DateTime? Dob { get; set; }
        public string Joinyear { get; set; }
        public int? cat_id { get; set; }
        public int? program_id { get; set; }
        public string subjects { get; set; }
        public string userid1 { get; set; }
        public string passwd1 { get; set; }
        public string saltVc1 { get; set; }
        public string YearSem { get; set; }
        public string Section { get; set; }
        public int? BloodGrp { get; set; }
        public string Session { get; set; }
        public string affiliation { get; set; }
        public byte[]  fingerPrint { get; set; }
        public byte[]  memberSign { get; set; }
        public string printing_status { get; set; }
        public string image_status { get; set; }
        public string opac_status { get; set; }
        public byte[]  StudentThumb { get; set; }
        public string IsThumb { get; set; }
        public byte[]  ThumbTemplate1 { get; set; }
        public byte[]  ThumbTemplate2 { get; set; }
        public byte[]  StudentThumb2 { get; set; }
        public string IsThumb2 { get; set; }
        public byte[]  ThumbTemplate3 { get; set; }
        public byte[]  ThumbTemplate4 { get; set; }
        public string mothername { get; set; }
        public string pan_no { get; set; }
        public string RfIdId { get; set; }
        public string AadharNo { get; set; }
        public string photoname { get; set; }
        public string Signname { get; set; }
        public string FingerPrints { get; set; }
        public string SearchText { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
