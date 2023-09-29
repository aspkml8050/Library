using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class CatalogClass
    {
        public List<bookaccessionmaster> Accession { get; set; }
        public BookAuthor Author { get; set; }
        public BookCatalogLib Catalog { get; set; }
        public BookConference Conference { get; set; }
        public BookRelators Relators { get; set; }
        public BookSeries Series { get; set; }
        public CatalogDataLib CatalogData { get; set; }
        public BookImage Image { get; set; }
        public Operation operate { get; set; }
    }
    public class Response<T>
    {
        public T data { get; set; }
        public bool IsSuccess  { get; set; }
        public string Messg { get; set; }
    }
    public class SpResponse
    {
        public bool IsSuccess { get; set; }
        public string Messg { get; set; }
    }

    public enum Operation
    {
        Add=1, Update=2
    }
    public class bookaccessionmaster
    {
        public string accessionnumber { get; set; }
        public string ordernumber { get; set; }
        public string indentnumber { get; set; }
        public string form { get; set; }
        public decimal? accessionid { get; set; }
        public DateTime? accessioneddate { get; set; }
        public string booktitle { get; set; }
        public decimal? srno { get; set; }
        public string released { get; set; }
        public decimal? bookprice { get; set; }
        public decimal? srNoOld { get; set; }
        public string Status { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string IssueStatus { get; set; }
        public DateTime? LoadingDate { get; set; }
        public string CheckStatus { get; set; }
        public long? ctrl_no { get; set; }
        public short? editionyear { get; set; }
        public short Copynumber { get; set; }
        public decimal specialprice { get; set; }
        public short? pubYear { get; set; }
        public string biilNo { get; set; }
        public DateTime? billDate { get; set; }
        public DateTime? catalogdate { get; set; }
        public string Item_type { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string OriginalCurrency { get; set; }
        public string userid { get; set; }
        public string vendor_source { get; set; }
        public int? VendorId { get; set; }
        public int? program_id { get; set; }
        public int? DeptCode { get; set; }
        public decimal? DSrno { get; set; }
        public string DeptName { get; set; }
        public int? ItemCategoryCode { get; set; }
        public string ItemCategory { get; set; }
        public int? Loc_id { get; set; }
        public string Location { get; set; }
        public string RfidId { get; set; }
        public string BookNumber { get; set; }
        public int? SetOFbooks { get; set; }
        public string SearchText { get; set; }
        public string IpAddress { get; set; }
        public int? TransNo { get; set; }
        public string AppName { get; set; }
    }
    public class BookAuthor
    {
        public long ctrl_no { get; set; }
        public string firstname1 { get; set; }
        public string middlename1 { get; set; }
        public string lastname1 { get; set; }
        public string firstname2 { get; set; }
        public string middlename2 { get; set; }
        public string lastname2 { get; set; }
        public string firstname3 { get; set; }
        public string middlename3 { get; set; }
        public string lastname3 { get; set; }
        public string PersonalName { get; set; }
        public string DateAssociated { get; set; }
                public string CorporateName { get; set; }
        /*        
        public string RelatorTermP { get; set; }
                public string RelatorTermC { get; set; }
                public string UniFormTitle { get; set; }
                public string DateofWork { get; set; }
                public string LanguageofWork { get; set; }
                public string stmtofResponsibility { get; set; }
                public string AddedPersonalName { get; set; }
          */
        public int? TransNo { get; set; }
    }
    public class BookCatalogLib
    {
        public long ctrl_no { get; set; }
        public DateTime? catalogdate1 { get; set; }
        public int? booktype { get; set; }
        public string volumenumber { get; set; }
        public string initpages { get; set; }
        public int? pages { get; set; }
        public string parts { get; set; }
        public string leaves { get; set; }
        public string boundind { get; set; }
        public string title { get; set; }
        public int? publishercode { get; set; }
        public string edition { get; set; }
        public string isbn { get; set; }
        public string subject1 { get; set; }
        public string subject2 { get; set; }
        public string subject3 { get; set; }
        public string Booksize { get; set; }
        public string LCCN { get; set; }
        public string Volumepages { get; set; }
        public string biblioPages { get; set; }
        public string bookindex { get; set; }
        public string illustration { get; set; }
        public string variouspaging { get; set; }
        public int? maps { get; set; }
        public string ETalEditor { get; set; }
        public string ETalCompiler { get; set; }
        public string ETalIllus { get; set; }
        public string ETalTrans { get; set; }
        public string ETalAuthor { get; set; }
        public string accmaterialhistory { get; set; }
        public string MaterialDesignation { get; set; }
        public string issn { get; set; }
        public string Volume { get; set; }
        public int? dept { get; set; }
        public int? language_id { get; set; }
        public string part { get; set; }
        public string eBookURL { get; set; }
        public string FixedData { get; set; }
        public string cat_Source { get; set; }
        public string Identifier { get; set; }
        public string firstname { get; set; }
        public string percity { get; set; }
        public string perstate { get; set; }
        public string percountry { get; set; }
        public string peraddress { get; set; }
        public string departmentname { get; set; }
        public string Btype { get; set; }
        public string language_name { get; set; }
        public string PublisherNo { get; set; }
        public string PubSource { get; set; }
//        public string SysCtrlNo { get; set; } //set in sp
        public string NLMCN { get; set; }
        public string GeoArea { get; set; }
        public string PhyExtent { get; set; }
        public string PhyOther { get; set; }
        public string pubDate { get; set; }
        public string BookCost { get; set; }
        public string latestTransDate { get; set; }
        public string ItemCategory { get; set; }
        public string OriginalCurrency { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string SearchText { get; set; }
        public int? TransNo { get; set; }
        public string Control008 { get; set; }
    }
    public class BookConference
    {
        public long ctrl_no { get; set; }
        public string Subtitle { get; set; }
        public string Paralleltype { get; set; }
        public string ConfName { get; set; }
        public string ConfYear { get; set; }
        public string BNNote { get; set; }
        public string CNNote { get; set; }
        public string GNNotes { get; set; }
        public string VNNotes { get; set; }
        public string SNNotes { get; set; }
        public string ANNotes { get; set; }
        public string Course { get; set; }
        public string AdFname1 { get; set; }
        public string AdMname1 { get; set; }
        public string AdLname1 { get; set; }
        public string AdFname2 { get; set; }
        public string AdMname2 { get; set; }
        public string AdLname2 { get; set; }
        public string AdFname3 { get; set; }
        public string AdMname3 { get; set; }
        public string AdLName3 { get; set; }
        public string Abstract { get; set; }
        public string Program_name { get; set; }
        public string ConfPlace { get; set; }
        public string loccallno { get; set; }
        public int? transno { get; set; }
    }
    public class BookImage
    {
        public long ctrl_no { get; set; }
        public byte[] CoverPage { get; set; }
    }
    public class BookRelators
    {
        public long ctrl_no { get; set; }
        public string editorFname1 { get; set; }
        public string editorMname1 { get; set; }
        public string editorLname1 { get; set; }
        public string editorFname2 { get; set; }
        public string editorMname2 { get; set; }
        public string editorLname2 { get; set; }
        public string editorFname3 { get; set; }
        public string editorMname3 { get; set; }
        public string editorLname3 { get; set; }
        public string CompilerFname1 { get; set; }
        public string CompilerMname1 { get; set; }
        public string CompilerLname1 { get; set; }
        public string CompilerFname2 { get; set; }
        public string CompilerMname2 { get; set; }
        public string CompilerLname2 { get; set; }
        public string CompilerFname3 { get; set; }
        public string CompilerMname3 { get; set; }
        public string CompilerLname3 { get; set; }
        public string illusFname1 { get; set; }
        public string illusMname1 { get; set; }
        public string illusLname1 { get; set; }
        public string illusFname2 { get; set; }
        public string illusMname2 { get; set; }
        public string illusrLname2 { get; set; }
        public string illusFname3 { get; set; }
        public string illusMname3 { get; set; }
        public string illusLname3 { get; set; }
        public string TranslatorFname1 { get; set; }
        public string TranslatorMname11 { get; set; }
        public string TranslatorLname1 { get; set; }
        public string TranslatorFname2 { get; set; }
        public string TranslatorMname2 { get; set; }
        public string TranslatorLname2 { get; set; }
        public string TranslatorFname3 { get; set; }
        public string TranslatorMname3 { get; set; }
        public string TranslatorLname3 { get; set; }
    }
    public class BookSeries
    {
        public long ctrl_no { get; set; }
        public string SeriesName { get; set; }
        public string seriesNo { get; set; }
        public string seriesPart { get; set; }
        public string etal { get; set; }
        public int Svolume { get; set; }
        public string af1 { get; set; }
        public string am1 { get; set; }
        public string al1 { get; set; }
        public string af2 { get; set; }
        public string am2 { get; set; }
        public string al2 { get; set; }
        public string af3 { get; set; }
        public string am3 { get; set; }
        public string al3 { get; set; }
        public string SSeriesName { get; set; }
        public string SseriesNo { get; set; }
        public string SseriesPart { get; set; }
        public string Setal { get; set; }
        public int SSvolume { get; set; }
        public string Saf1 { get; set; }
        public string sam1 { get; set; }
        public string Sal1 { get; set; }
        public string Saf2 { get; set; }
        public string Sam2 { get; set; }
        public string Sal2 { get; set; }
        public string Saf3 { get; set; }
        public string Sam3 { get; set; }
        public string Sal3 { get; set; }
        public string SeriesParallelTitle { get; set; }
        public string SSeriesParallelTitle { get; set; }
        public string SubSeriesName { get; set; }
        public string SubseriesNo { get; set; }
        public string SubSeriesPart { get; set; }
        public string SubEtal { get; set; }
        public int SubSvolume { get; set; }
        public string Subaf1 { get; set; }
        public string Subam1 { get; set; }
        public string Subal1 { get; set; }
        public string Subaf2 { get; set; }
        public string Subam2 { get; set; }
        public string Subal2 { get; set; }
        public string Subaf3 { get; set; }
        public string Subam3 { get; set; }
        public string Subal3 { get; set; }
        public string SubSeriesParallelTitle { get; set; }
        public string ISSNMain { get; set; }
        public string ISSNSub { get; set; }
        public string ISSNSecond { get; set; }
    }
    public class CatalogDataLib
    {
        public long ctrl_no { get; set; }
        public string classnumber { get; set; }
        public string booknumber { get; set; }
        public string classnumber_l1 { get; set; }
        public string booknumber_l1 { get; set; }
        public string classnumber_l2 { get; set; }
        public string booknumber_l2 { get; set; }
        public int? transno { get; set; }

    }
    
}