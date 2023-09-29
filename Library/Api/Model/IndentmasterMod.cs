using System;
namespace LibData.Model
{
    public class IndentmasterMod
    {
        public string  indentnumber { get; set; } 
        public DateTime? indentdate { get; set; }
        public int? mediatype { get; set; }//fk
        public string  requestercode { get; set; }
        public int? departmentcode { get; set; }//fk
        public string  title { get; set; } = "";
        public string  authortype { get; set; } = "";
        public string  firstname1 { get; set; } = "";
        public string  middlename1 { get; set; } = "";
        public string  lastname1 { get; set; } = "";
        public string  firstname2 { get; set; } = "";
        public string  middlename2 { get; set; } = "";
        public string  lastname2 { get; set; } = "";
        public string  firstname3 { get; set; } = "";
        public string  middlename3 { get; set; } = "";
        public string  lastname3 { get; set; } = "";
        public string  edition { get; set; } = "";
        public string  yearofedition { get; set; } = "";
        public string  volumeno { get; set; } = "";
        public string  isbn { get; set; } = "";
        public int? category { get; set; }
        public int? currencycode { get; set; }
        public string  go_bank { get; set; }
        public decimal? exchangerate { get; set; }
        public int? noofcopies { get; set; }
        public string  approval { get; set; }
        public decimal? price { get; set; }
        public decimal? totalamount { get; set; }
        public string  coursenumber { get; set; } = "";
        public int? noofstudents { get; set; }
        public string  publisherid { get; set; }
        public string  vendorid { get; set; }
        public DateTime? recordingdate { get; set; }
        public string  gifted { get; set; }
        public string  indenttype { get; set; }
        public DateTime? indenttime { get; set; }
        public string  seriesname { get; set; } = "";
        public int? order_check_code { get; set; }
        public decimal? ApplicableExchangeRate { get; set; }
        public decimal? discount { get; set; }
        public string  status { get; set; } = "Ordering Awaited";
        public decimal? orderexchangerate { get; set; }
        public string  yearofPublication { get; set; } = "";
        public string  isSatnding { get; set; }
        public string  IndentId { get; set; }
        public string  Vpart { get; set; } = "";
        public int  ItemNo { get; set; }
        public string  subtitle { get; set; } = "";
        public int? Language_Id { get; set; }//fk
        public string  Userid { get; set; }
        public string  refno { get; set; } = "";
        public string  Indentstatus { get; set; } = "0";
        public string  DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public string  PrintStatus { get; set; } = "N";
        public int? OnlinePStatus { get; set; }

    }
}
