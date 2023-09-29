using Library.App_Code.CSharp;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Library.Validation;
using System.Windows.Forms;
using System.Security.AccessControl;
using Microsoft.SqlServer.Management.Smo.Agent;
using Audit;
using System.Net.Http;
using Model.Shared;
using Newtonsoft.Json;

namespace Library
{
    public partial class ItemDisplayEdit : BaseClass
    {
        CatalogClass webModel=new CatalogClass();
        bookaccessionmaster accn=new bookaccessionmaster();
        SelectedData basData=new SelectedData();
        dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txCopynumber.Attributes.Add("onblur", "return IntegerNumber(this,event)");
               txbookprice .Attributes.Add("onblur", "return IntegerNumber(this,event)");
                txOriginalPrice.Attributes.Add("onblur", "return IntegerNumber(this,event)");
                txspecialprice.Attributes.Add("onblur", "return IntegerNumber(this,event)");
                ReqdData rd =new ReqdData();
                rd.Media = true;
                rd.Categ = true;
                rd.ItemStatus = true;
                rd.Lang = true;
                rd.Currency = true;
                rd.ItemType=true;
                rd.Dept = true;
                rd.Progs = true;
                BasicData bData = new BasicData();
                basData = bData.GetBasicData(rd);
                 bData.GetDDLItemType(ddlItemtype,basData.itemTypes);
                 bData.GetDDLItemStat(ddlItemStat,basData.itemStatuses);
                bData.GetDDLMedia(ddlMedia, basData.mediaTypes);
                bData.GetDDLDept(ddlDeptCode, basData.departments);
                bData.GetDDLExch(ddlOriginalCurrency, basData.Currencies);
                bData.GetDDLExch(ddl2OriginalCurrency, basData.Currencies);
                bData.GetDDLProg(ddlprogram_id,basData.programs);
                bData.GetDDLCateg(ddlItemCategoryCode, basData.categories);
                bData.GetDDLItemType(ddlbooktype, basData.itemTypes);
                bData.GetDDLLang(ddllanguage_id, basData.Languages);
                bData.GetDDLDept(ddldept, basData.departments);

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            hdVendorid.Value = "";
            hdissuestatus.Value = "";
            hdlocationid.Value = "";
            txaccessionnumber.Text = "";
            txbillDate.Text = "";
            txclassnumber.Text = "";
            txbooknumber2.Text = "";
            txbiilNo.Text = "";
             txindentnumber.Text = "";
            ddlForm.SelectedIndex = 0;
            txaccessiondate.Text = "";
            txbooktitle.Text = "";
            txbookprice.Text = "";
            ddlItemStat.SelectedIndex = 0;
            txreleasedate.Text = "";
            txLoadingDate.Text = "";
            labCheckStatus.Text = "";
            txeditionyear.Text = "";
            txCopynumber.Text = "";
            txspecialprice.Text = "";
            txpubYear.Text = "";
            txbiilNo.Text = "";
            txbillDate.Text = "";
            txcatalogdate.Text = "";
            ddlItemtype.SelectedIndex = 0;
            txOriginalPrice.Text = "";
            ddlOriginalCurrency.SelectedIndex = 0;
            txVendor.Text = "";
            ddlprogram_id.SelectedIndex = 0;
            ddlDeptCode.SelectedIndex = 0;
            ddlItemCategoryCode.SelectedIndex = 0;
            ddllanguage_id.SelectedIndex = 0;
            txLocation.Text = "";
            txRfidid.Text = "";
            txBookNumber.Text = "";
            //          txSetOFbooks.Text = ""; //assign default value
            //        txSearchText.Text = "";
            //      txIpAddress.Text = "";
            //    txTransNo.Text = "";
            //  txAppName.Text = "";
            txPubSource.Text = "";
            ddlbooktype.SelectedIndex = 0;
            txvolumenumber.Text = "";
            txinitpages.Text = "";
            txpages.Text = "";
            txparts.Text = "";
            txleaves.Text = "";
            ddlBound.SelectedIndex = 0;
            txtitle.Text = "";
            hdpublishercode.Value = "";
            txpublisher.Text = "";
            txedition.Text = "";
            txisbn.Text = "";
            txsubject1.Text = "";
            txsubject2.Text = "";
            txBooksize.Text = "";
            txsubject3.Text = "";
            txLCCN.Text = "";
            txVolumepages.Text = "";
            txbiblioPages.Text = "";
            ddlbookindex.SelectedIndex = 0;
            ddlillustration.SelectedIndex = 0;
            ddlvariouspaging.SelectedIndex = 0;
            txmaps.Text = "";

            txfirstname1.Text = "";
            txmiddlename1.Text = "";
            txlastname1.Text = "";
            txfirstname2.Text = "";
            txmiddlename2.Text = "";
            txlastname2.Text = "";
            txfirstname3.Text = "";
            txmiddlename3.Text = "";
            txlastname3.Text = "";

            txPersonalName.Text="";
            txDateAssociated.Text = "";
            //            txRelatorTermP.Text = ""; assign default value
            txCorporateName.Text = "";
  //          txRelatorTermC.Text = "";
            txlastname3.Text = "";
            txPersonalName.Text = "";
            txDateAssociated.Text = "";
    //        txRelatorTermP.Text = "";
            txCorporateName.Text = "";
            //txRelatorTermC.Text = "";
            //      txUniFormTitle.Text = "";
            //    txDateofWork.Text = "";
            //  txLanguageofWork.Text = "";
            //set etalauthor y
            //txstmtofResponsibility.Text = "";
            //  txAddedPersonalName.Text = "";
            //txNLMCN //set default  
            //ddlETalAuthor.Text = ""; make yes

            //            txTransNo.Text = "";
            ddlETalEditor.SelectedIndex = 0;
            ddlETalCompiler.SelectedIndex   = 0;
            ddlillustration.SelectedIndex = 0;
            ddlETalTrans.SelectedIndex = 0;
            ddl2OriginalCurrency.SelectedIndex = 0;
            txeditorFname1.Text = "";
            txeditorMname1.Text = "";
            txeditorLname1.Text = "";
            txeditorFname2.Text = "";
            txeditorMname2.Text = "";
            txeditorLname2.Text = "";
            txeditorFname3.Text = "";
            txeditorMname3.Text = "";
            txeditorLname3.Text = "";
            txCompilerFname1.Text = "";
            txCompilerMname1.Text = "";
            txCompilerLname1.Text = "";
            txCompilerFname2.Text = "";
            txCompilerMname2.Text = "";
            txCompilerLname2.Text = "";
            txCompilerFname3.Text = "";
            txCompilerMname3.Text = "";
            txCompilerLname3.Text = "";
            txillusFname1.Text = "";
            txillusMname1.Text = "";
            txillusLname1.Text = "";
            txillusFname2.Text = "";
            txillusMname2.Text = "";
            txillusrLname2.Text = "";
            txillusFname3.Text = "";
            txillusMname3.Text = "";
            txillusLname3.Text = "";
            txTranslatorFname1.Text = "";
            txTranslatorMname11.Text = "";
            txTranslatorLname1.Text = "";
            txTranslatorFname2.Text = "";
            txTranslatorMname2.Text = "";
            txTranslatorLname2.Text = "";
            txTranslatorFname3.Text = "";
            txTranslatorMname3.Text = "";
            txTranslatorLname3.Text = "";
            txcatalogdate1.Text = "";
            ddlbooktype.SelectedIndex = 0;
            txvolumenumber.Text = "";
            txinitpages.Text = "";
            txpages.Text = "";
            txparts.Text = "";
            txleaves.Text = "";
            ddl2OriginalCurrency.SelectedIndex =0;
          
            txtitle.Text = "";
            hdpublishercode.Value = "";
            txedition.Text = "";
            txisbn.Text = "";
            txsubject1.Text = "";
            txsubject2.Text = "";
            txsubject3.Text = "";
            txBooksize.Text = "";
            txLCCN.Text = "";
            txVolumepages.Text = "";
            txbiblioPages.Text = "";
            ddlillustration.SelectedIndex = 0;
            ddlvariouspaging.SelectedIndex = 0;
            txmaps.Text = "";
            ddlETalEditor.SelectedIndex = 0;
            ddlETalCompiler.SelectedIndex = 0;
            ddlETalIllus.SelectedIndex = 0;
            ddlETalTrans.SelectedIndex = 0;
            txaccmaterialhistory.Text = "";
            txMaterialDesignation.Text = "";
            txissn.Text = "";
            txVolume.Text = "";
            ddldept.SelectedIndex = 0;
            
            txpart.Text = "";
            txeBookURL.Text = "";
            txFixedData.Text = "";
            txcat_Source.Text = "";
            txIdentifier.Text = "";
            txfirstname.Text = "";
            txpercity.Text = "";
            txperstate.Text = "";
            txpercountry.Text = "";
            txperaddress.Text = "";
            txdepartmentname.Text = "";
            //txBtype.Text = "";
            txlanguage_name.Text = "";
            txPublisherNo.Text = "";
            txPubSource.Text = "";
            //            txSysCtrlNo.Text = ""; set default value
            //          txNLMCN.Text = "";
            txGeoArea.Text = "";
            txPhyExtent.Text = "";
            txPhyOther.Text = "";
            txpubDate.Text = "";
            txBookCost.Text = "";
            txlatestTransDate.Text = "";
            txItemCategory.Text = "";
            ddlOriginalCurrency.SelectedIndex   = 0;
            ddl2OriginalCurrency.SelectedIndex = 0;
            txOriginalPrice.Text = "";
            //            txSearchText.Text = "";
            //          txTransNo.Text = "";
            txseriesNo.Text = "";
            txSeriesName.Text = "";//set all empty in rest of series in db
            txSubtitle.Text = "";
            txParalleltype.Text = "";
            txConfName.Text = "";
            txConfYear.Text = "";
            txBNNote.Text = "";
            txCNNote.Text = "";
            //txGNNotes.Text = "";
            //txVNNotes.Text = "";
            //txSNNotes.Text = "";
            //txANNotes.Text = "";
            //txCourse.Text = "";
            //txAdFname1.Text = "";
            //txAdMname1.Text = "";
            //txAdLname1.Text = "";
            //txAdFname2.Text = "";
            //txAdMname2.Text = "";
            //txAdLname2.Text = "";
            //txAdFname3.Text = "";
            //txAdMname3.Text = "";
            //txAdLName3.Text = "";
            txAbstract.Text = "";
            txProgram_name.Text = "";
            txConfPlace.Text = "";
            txSubtitle.Text = "";
            txParalleltype.Text = "";
            txConfName.Text = "";
            txConfYear.Text = "";
            txBNNote.Text = ""  ;
            txCNNote.Text       = "";
            txAbstract.Text = "";
            txProgram_name.Text = "";
            txConfPlace.Text = "";
            txControl008.Text="";
        }

        protected void btnPostBack_Click(object sender, EventArgs e)
        {
            GlobClassTr clas = new GlobClassTr();
            clas.TrOpen();
            CatalogClass catgd = new CatalogClass();
            SqlParameter p1 = new SqlParameter("@Accn", DBNull.Value);
            SqlParameter p2 = new SqlParameter("@ctrl_no", hdctrl_no.Value);
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(p1);
            paras.Add(p2);
            var d = clas.ExProcDS("getCatelogFull", paras);

            clas.TrClose();
            if (d.Tables.Count > 0)
            {
                var accn = ExtConvert.ConvertTo<bookaccessionmaster>(d.Tables[0]);
                foreach (var ac in accn)
                    ac.billDate = ac.billDate == null|| Convert.ToDateTime( ac.billDate).Year==1900?null:ac.billDate;
                grdCopies.DataSource = accn;
                grdCopies.DataBind();
                txaccessionnumber.Text = accn[0].accessionnumber;
                txbiilNo.Text = accn[0].biilNo?? accn[0].biilNo;
                txindentnumber.Text = accn[0].indentnumber ?? accn[0].indentnumber;
                ddlForm.SelectedValue = ddlForm.Items.FindByText(accn[0].form).Value;

                hdaccessionid.Value = accn[0].accessionid.ToString();
                txaccessiondate.Text = accn[0].accessioneddate==null?"":Convert.ToDateTime( accn[0].accessioneddate).ToString("dd-MMM-yyyy");
                txbooktitle.Text = accn[0].booktitle;  //make it from bookcatalog
                hdsrno.Value = accn[0].srno.ToString();
                hdreleased.Value = accn[0].released.ToString();
                txbookprice.Text = accn[0].bookprice.ToString();
                hdsrNoOld.Value = accn[0].srNoOld.ToString();
                ddlItemStat.SelectedValue = accn[0].Status.ToString();
                txreleasedate.Text = accn[0].ReleaseDate == null ? "" : Convert.ToDateTime(accn[0].ReleaseDate).ToString("dd-MMM-yyyy"); 
                /*txIssueStatus.Text = "";*/
                txLoadingDate.Text = accn[0].LoadingDate == null ? "" : Convert.ToDateTime(accn[0].LoadingDate).ToString("dd-MMM-yyyy"); 
                labCheckStatus.Text = accn[0].CheckStatus;
                hdctrl_no.Value = accn[0].ctrl_no.ToString();
                txeditionyear.Text = accn[0].editionyear.ToString();
                txCopynumber.Text = accn[0].Copynumber.ToString();
                txspecialprice.Text = accn[0].specialprice.ToString();
                txpubYear.Text = accn[0].pubYear.ToString();
                txbiilNo.Text = accn[0].biilNo;
                txbillDate.Text = accn[0].billDate!=null? string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime( accn[0].billDate)):"";
                txcatalogdate.Text = accn[0].catalogdate == null ? "" : Convert.ToDateTime(accn[0].catalogdate).ToString("dd-MMM-yyyy"); 
                ddlItemtype.SelectedValue = ddlItemtype.Items.FindByText(accn[0].Item_type).Value;
                txOriginalPrice.Text = accn[0].OriginalPrice.ToString();
                ddlOriginalCurrency.SelectedValue = ddlOriginalCurrency.Items.FindByText(accn[0].OriginalCurrency.Trim()).Value;
                hdVendorid.Value = accn[0].VendorId.ToString();
                 txVendor.Text = accn[0].vendor_source;
                hdissuestatus.Value = accn[0].IssueStatus;
                hdcheckstatus.Value = accn[0].CheckStatus;
                ddlDeptCode.SelectedValue = accn[0].DeptCode.ToString();
                 hdDSrno.Value = "";
                ddlItemCategoryCode.SelectedValue = accn[0].ItemCategoryCode.ToString();
                txLocation.Text = accn[0].Location;
                txRfidid.Text = accn[0].RfidId;
                ddlOriginalCurrency.SelectedValue = ddlOriginalCurrency.Items.FindByText(accn[0].OriginalCurrency).Value;
                txOriginalPrice.Text = accn[0].OriginalPrice.ToString();

                txBookNumber.Text = accn[0].BookNumber;
                //txSetOFbooks.Text = accn[0].setofbooks;
                //txSearchText.Text = accn[0].searchtext;
                //txIpAddress.Text = accn[0].ipaddress;
                //txTransNo.Text = accn[0].transno;
                //txAppName.Text = accn[0].appname;
                hdVendorid.Value = accn[0].VendorId.ToString();
                txVendor.Text = accn[0].vendor_source;
                ddlprogram_id.SelectedValue = accn[0].program_id.ToString();
                //                txItemCategory.Text = "";
                /*              txSetOFbooks.Text = "";
                              txSearchText.Text = "";
                              txIpAddress.Text = "";
                              txTransNo.Text = "";
                              txAppName.Text = "";
                              txVendorId.Text = "";*/
                var ctg = ExtConvert.ConvertTo<Models.BookCatalogLib>(d.Tables[2])[0];
                if (ctg.catalogdate1 !=null)
                txcatalogdate1.Text = Convert.ToDateTime( ctg.catalogdate1).ToString("dd-MMM-yyyy");
               // txctrl_no.Text = ctg.ctrl_no;
                ddlbooktype.SelectedValue = ctg.booktype.ToString();
                txvolumenumber.Text = ctg.volumenumber;
                txinitpages.Text = ctg.initpages;
                txpages.Text = ctg.pages.ToString();
                txparts.Text = ctg.parts;
                txleaves.Text = ctg.leaves;
                ddlBound.SelectedValue=ctg.boundind.ToString();

                txtitle.Text = ctg.title;
                hdpublishercode.Value = ctg.publishercode.ToString();
                txedition.Text = ctg.edition;
                txisbn.Text = ctg.isbn;
                txsubject1.Text = ctg.subject1;
                txsubject2.Text = ctg.subject2;
                txsubject3.Text = ctg.subject3;
                txBooksize.Text = ctg.Booksize;
                txLCCN.Text = ctg.LCCN;
                ddlMedia.SelectedValue=ddlMedia.Items.FindByText(ctg.MaterialDesignation).Value; 
                txVolumepages.Text = ctg.Volumepages;
                txbiblioPages.Text = ctg.biblioPages;
                ddlbookindex.SelectedValue = ctg.bookindex;
                ddlillustration.SelectedValue = ctg.illustration;
                ddlvariouspaging.SelectedValue = ctg.variouspaging;
                txmaps.Text = ctg.maps.ToString();
                ddlETalEditor.SelectedValue = ctg.ETalEditor;
                ddlETalCompiler.SelectedValue = ctg.ETalCompiler;
                ddlETalIllus.SelectedValue = ctg.ETalIllus;
                ddlETalTrans.SelectedValue = ctg.ETalTrans;
//                ddlETalAuthor.se = ctg.ETalAuthor;
                txaccmaterialhistory.Text = ctg.accmaterialhistory;
                txMaterialDesignation.Text = ctg.MaterialDesignation;
                txissn.Text = ctg.issn;
                txVolume.Text = ctg.Volume;
                ddldept.SelectedValue = ctg.dept.ToString();
                ddllanguage_id.Text = ctg.language_id.ToString();
                txpart.Text = ctg.part;
                txeBookURL.Text = ctg.eBookURL;
                txFixedData.Text = ctg.FixedData;
                txcat_Source.Text = ctg.cat_Source;
                txIdentifier.Text = ctg.Identifier;
                txfirstname.Text = ctg.firstname;
                txpercity.Text = ctg.percity;
                txperstate.Text = ctg.perstate;
                txpercountry.Text = ctg.percountry;
                txperaddress.Text = ctg.peraddress;
                txdepartmentname.Text = ctg.departmentname;
                // txBtype.Text = ctg.Btype; //save text for ite_type
                txlanguage_name.Text = ctg.language_name;
                txPublisherNo.Text = ctg.PublisherNo;
                txPubSource.Text = ctg.PubSource;
//                txSysCtrlNo.Text = ctg.SysCtrlNo;
  //              txNLMCN.Text = ctg.NLMCN;
                txGeoArea.Text = ctg.GeoArea;
                txPhyExtent.Text = ctg.PhyExtent;
                txPhyOther.Text = ctg.PhyOther;
                txpubDate.Text = ctg.pubDate;
                txBookCost.Text = ctg.BookCost;
                txlatestTransDate.Text = string.IsNullOrEmpty( ctg.latestTransDate)?"" : string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ctg.latestTransDate) );
                txItemCategory.Text = ctg.ItemCategory;
                //                txSearchText.Text = ctg.SearchText; //save the search text
                //              txTransNo.Text = ctg.TransNo;
                var ath = ExtConvert.ConvertTo<BookAuthor>(d.Tables[1])[0];
                txfirstname1.Text = ath.firstname1;
                txmiddlename1.Text = ath.middlename1;
                txlastname1.Text = ath.lastname1;
                txfirstname2.Text = ath.firstname2;
                txmiddlename2.Text = ath.middlename2;
                txlastname2.Text = ath.lastname2;
                txfirstname3.Text = ath.firstname3;
                txmiddlename3.Text = ath.middlename3;
                txlastname3.Text = ath.lastname3;
                txPersonalName.Text = ath.PersonalName;
                txDateAssociated.Text = ath.DateAssociated;
//                txRelatorTermP.Text = ath.RelatorTermP;
                txCorporateName.Text = ath.CorporateName;
                //              txRelatorTermC.Text = ath.RelatorTermC;
                //            txUniFormTitle.Text = ath.UniFormTitle;
                //          txDateofWork.Text = ath.DateofWork;
                //txLanguageofWork.Text = ath.LanguageofWork;
                //txstmtofResponsibility.Text = ath.stmtofResponsibility;
                // txAddedPersonalName.Text = ath.AddedPersonalName;
                // txTransNo.Text = ath.TransNo;
                var sers = ExtConvert.ConvertTo<BookSeries>(d.Tables[5])[0];//see marc rec - 490 and add relevant fields
                txSeriesName.Text = sers.SeriesName;
                txseriesNo.Text = sers.seriesNo;
                var cnf = ExtConvert.ConvertTo<BookConference>(d.Tables[3])[0];//see marc rec - 490 and add relevant fields
                txSubtitle.Text = cnf.Subtitle;
                txParalleltype.Text = cnf.Paralleltype;
                txConfName.Text = cnf.ConfName;
                txConfYear.Text = cnf.ConfYear;
                txBNNote.Text = cnf.BNNote;
                txCNNote.Text = cnf.CNNote;
                txAbstract.Text = cnf.Abstract;
                txProgram_name.Text = cnf.Program_name;
                txConfPlace.Text = cnf.ConfPlace;
            }

        }

        protected void labaccessionnumber_Click(object sender, EventArgs e)
        {

        }
        private (string,bookaccessionmaster) ValidateAccn()
        {
            Validatation vald = new Validatation();
            bookaccessionmaster bookaccn = new bookaccessionmaster();
            bookaccn.accessionnumber = txaccessionnumber.Text;
            bookaccn.ordernumber = txordernumber.Text;
            bookaccn.indentnumber = txindentnumber.Text;
            bookaccn.form = ddlForm.SelectedValue;
            if (hdaccessionid.Value != "")
                bookaccn.accessionid = Convert.ToInt32(hdaccessionid.Value);
            if (txaccessiondate.Text.Trim() != "")
                bookaccn.accessioneddate = Convert.ToDateTime(txaccessiondate.Text);
            bookaccn.booktitle = txbooktitle.Text;
            if (hdsrno.Value != "")
                bookaccn.srno = Convert.ToDecimal(hdsrno.Value);
            bookaccn.released = hdreleased.Value;
            if (txbookprice.Text != "")
                bookaccn.bookprice = Convert.ToDecimal(txbookprice.Text);
            if (hdsrNoOld.Value != "")
                bookaccn.srNoOld = Convert.ToDecimal(hdsrNoOld.Value);
            bookaccn.Status = ddlItemStat.SelectedValue;
            if (txreleasedate.Text != "")
                bookaccn.ReleaseDate = Convert.ToDateTime(txreleasedate.Text);
            bookaccn.IssueStatus = hdissuestatus.Value;
            if (txreleasedate.Text != "")
                bookaccn.LoadingDate = Convert.ToDateTime(txLoadingDate.Text);
            bookaccn.CheckStatus = hdcheckstatus.Value;
            if (hdctrl_no.Value != "")
                bookaccn.ctrl_no = Convert.ToInt64( hdctrl_no.Value);
            if (txeditionyear.Text !="")
             bookaccn.editionyear =Convert.ToInt16(txeditionyear.Text) ;
            if (txCopynumber.Text != "")
                bookaccn.Copynumber = Convert.ToInt16(txCopynumber.Text);

            if (txspecialprice.Text == "")
                bookaccn.specialprice = 0.0M;
            else
              bookaccn.specialprice = Convert.ToDecimal( txspecialprice.Text);
            if (txpubYear.Text!="")
              bookaccn.pubYear = Convert.ToInt16( txpubYear.Text);
            bookaccn.biilNo = txbiilNo.Text;
            if (txbillDate.Text != "")
                bookaccn.billDate = Convert.ToDateTime(txbillDate.Text);
            if (txcatalogdate.Text != "")
                bookaccn.catalogdate = Convert.ToDateTime(txcatalogdate.Text);

            bookaccn.Item_type = ddlItemtype.SelectedItem.Text;
            if (txOriginalPrice.Text == "")
                bookaccn.specialprice = 0.0M;
            else
                bookaccn.OriginalPrice = Convert.ToDecimal(txOriginalPrice.Text);
            if (ddlOriginalCurrency.SelectedItem.Text != "---Select---")

                bookaccn.OriginalCurrency = ddlOriginalCurrency.SelectedItem.Text;
            var usr = LoggedUser.Logged();
            bookaccn.userid = usr.User_Id;
            bookaccn.vendor_source = txVendor.Text;
            if (ddlprogram_id.SelectedItem.Text!="---Select---")
              bookaccn.program_id = Convert.ToInt32( ddlprogram_id.SelectedValue);
            if (ddlDeptCode.SelectedItem.Text != "---Select---")
                bookaccn.DeptCode = Convert.ToInt32(ddlDeptCode.SelectedValue);
            if (hdDSrno.Value != "")
             bookaccn.DSrno = Convert.ToDecimal(hdDSrno.Value);
            if (ddlItemCategoryCode.SelectedItem.Text != "---Select---")
              bookaccn.ItemCategoryCode = Convert.ToInt32(ddlItemCategoryCode.SelectedValue);
            if (hdlocationid.Value != "")
                bookaccn.Loc_id = Convert.ToInt32(hdlocationid.Value);
            else
                bookaccn.Loc_id = 0;
            bookaccn.RfidId = txRfidid.Text;
            bookaccn.BookNumber = txBookNumber.Text;

            bookaccn.SetOFbooks = 0;
            bookaccn.SearchText = "";
            bookaccn.IpAddress = usr.ipaddrss;
            bookaccn.TransNo = 0;
            bookaccn.AppName = "ItemDisplayEdit.aspx";
            if  (hdVendorid.Value!="")
             bookaccn.VendorId = Convert.ToInt32(hdVendorid.Value);
            List<bookaccessionmaster> lst = new List<bookaccessionmaster>();
            lst.Add(bookaccn);
            var res = vald.ValidateAccn(lst);
            string ers = "";
            if (res.Item1.Count > 0)
            {
                
                ers = string.Join(";" ,res.Item1.ToArray());
            }
            return (ers, bookaccn);

        }
        private (string,Library.Models. BookCatalogLib) VCatelog()
        {
            Validatation vald = new Validatation();
            Library.Models. BookCatalogLib categ = new Library.Models.BookCatalogLib();
            if (txcatalogdate1.Text!="")
            categ.catalogdate1 = Convert.ToDateTime( txcatalogdate1.Text);
            if (ddlbooktype.SelectedItem.Text != "---Select---")
                categ.booktype = Convert.ToInt32( ddlbooktype.SelectedValue);
            categ.volumenumber = txvolumenumber.Text;
            categ.initpages = txinitpages.Text;
            if (txpages.Text!="")
             categ.pages =Convert.ToInt32( txpages.Text);
            categ.parts = txparts.Text;
            categ.leaves = txleaves.Text;
            if (ddlBound.SelectedItem.Text!="---Select---")
             categ.boundind = ddlBound.SelectedItem.Text;
            categ.title = txtitle.Text.Trim();
            if (hdpublishercode.Value != "")
                categ.publishercode = Convert.ToInt16(hdpublishercode.Value);
            categ.edition = txedition.Text.Trim();
            categ.isbn = txisbn.Text.Trim();
            categ.subject1 = txsubject1.Text.Trim();
            categ.subject2 = txsubject2.Text.Trim();
            categ.subject3 = txsubject3.Text.Trim();
            categ.Booksize = txBooksize.Text.Trim();
            categ.LCCN = txLCCN.Text.Trim();
            categ.Volumepages = txVolumepages.Text.Trim();
            categ.biblioPages = txbiblioPages.Text.Trim();
            categ.bookindex = ddlbookindex.SelectedValue;
            categ.illustration = ddlillustration.SelectedValue;
            categ.variouspaging = ddlvariouspaging.SelectedValue;
            //            categ.maps = txmaps.Text;
            categ.ETalEditor = ddlETalEditor.SelectedValue;
            categ.ETalCompiler = ddlETalCompiler.SelectedValue;
            categ.ETalIllus = ddlETalIllus.SelectedValue;
            categ.ETalTrans = ddlETalTrans.SelectedValue;
            categ.ETalAuthor = "N";
            categ.accmaterialhistory = txaccmaterialhistory.Text;
            categ.MaterialDesignation = txMaterialDesignation.Text;
            categ.issn = txissn.Text;
            categ.Volume = txVolume.Text;
            if (ddldept.SelectedItem.Text!="---Select---")
             categ.dept = Convert.ToInt32( ddldept.SelectedValue);

            if (ddllanguage_id.SelectedItem.Text != "---Select---")
                categ.language_id = Convert.ToInt32(ddllanguage_id.SelectedValue);
            categ.part = txpart.Text;
            categ.eBookURL = txeBookURL.Text;
            categ.FixedData = txFixedData.Text;
            categ.cat_Source = txcat_Source.Text;
            categ.Identifier = txIdentifier.Text;
            categ.firstname = txfirstname.Text;
            categ.percity = txpercity.Text;
            categ.perstate = txperstate.Text;
            categ.percountry = txpercountry.Text;
            categ.peraddress = txperaddress.Text;
            categ.departmentname = txdepartmentname.Text;
            categ.Btype = ddlbooktype.SelectedItem.Text;
            categ.language_name = txlanguage_name.Text;
            categ.PublisherNo = txPublisherNo.Text;
            categ.PubSource = txPubSource.Text;
            //            categ.NLMCN = txNLMCN.Text;
            categ.GeoArea = txGeoArea.Text;
            categ.PhyExtent = txPhyExtent.Text;
            categ.PhyOther = txPhyOther.Text;
            categ.pubDate = txpubDate.Text;
            categ.BookCost = txBookCost.Text;
            categ.latestTransDate = txlatestTransDate.Text;
            categ.ItemCategory = txItemCategory.Text;
            if (ddl2OriginalCurrency.SelectedItem.Text!="---Select---")
            categ.OriginalCurrency = ddl2OriginalCurrency.SelectedItem.Text;
            if (tx2OriginalPrice.Text!="")
                categ.OriginalPrice = Convert.ToDecimal(tx2OriginalPrice.Text);
            else
                categ.OriginalPrice = 0;
            categ.Control008=txControl008.Text;
            var rv= vald.ValidateBCateg(categ);
            //categ.SearchText = txSearchText.Text;
            string ers = "";
            if (rv.Item1.Count > 0)
            {
                ers = string.Join(";", rv.Item1.ToArray());
            }
            return (ers, categ);
        }

        private (string,BookAuthor) VBookauthor()
        {
            List<string> rv = new List<string>();
            Validatation vald = new Validatation();
            BookAuthor author = new BookAuthor();
            author.firstname1 = txfirstname1.Text;
            author.middlename1 = txmiddlename1.Text;
            author.lastname1 = txlastname1.Text;
            author.firstname2 = txfirstname2.Text;
            author.middlename2 = txmiddlename2.Text;
            author.lastname2 = txlastname2.Text;
            author.firstname3 = txfirstname3.Text;
            author.middlename3 = txmiddlename3.Text;
            author.lastname3 = txlastname3.Text;
            var rvd= vald.ValidateAuthor(author);
            string ers = "";
            if (rvd.Item1.Count > 0)
            {

                ers = string.Join(";", rvd.Item1.ToArray());
            }
            return (ers, author);

        }
        private (string, BookConference) VBookConf()
        {
            List<string> rv = new List<string>();
            Validatation vald = new Validatation();
            BookConference conf = new BookConference();
            conf.Subtitle = txSubtitle.Text;
            conf.Paralleltype = txParalleltype.Text;
            conf.ConfName = txConfName.Text;
            conf.ConfYear = txConfYear.Text;
            conf.ConfPlace = txConfPlace.Text;
            var rvd = vald.ValidateConf(conf);
            string ers = "";
            if (rvd.Item1.Count > 0)
            {
                ers = string.Join(";", rvd.Item1.ToArray());
            }
            return (ers, conf);
        }
        private (string, Library.Models. CatalogDataLib) VCatalogData()
        {
            List<string> rv = new List<string>();
            Validatation vald = new Validatation();
            CatalogDataLib conf = new CatalogDataLib();
            conf.classnumber=txclassnumber.Text;
            conf.booknumber = txbooknumber2.Text;
            var rvd = vald.ValidateConf(conf);
            string ers = "";
            if (rvd.Item1.Count > 0)
            {
                ers = string.Join(";", rvd.Item1.ToArray());
            }
            return (ers, conf);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            var dval = ValidateAccn();
            if (dval.Item1 != "")
            {
                message.PageMesg("Accn: "+ dval.Item1, this, dbUtilities.MsgLevel.Warning);
                return;
            }
            var dcatg = VCatelog();
            if (dcatg.Item1 != "")
            {
                message.PageMesg("Catalog: "+ dcatg.Item1, this, dbUtilities.MsgLevel.Warning);
                return;
            }
            var dauth = VBookauthor();
            if (dauth.Item1 != "")
            {
                message.PageMesg("Author: " + dauth.Item1, this, dbUtilities.MsgLevel.Warning);
                return;
            }
            var dconf = VBookConf();

            var dcatdatag = VCatalogData();

           
            var ctrl = "138968";
            GlobClassTr clas = new GlobClassTr();
            clas.TrOpen();
            CatalogClass catgd = new CatalogClass();
            SqlParameter p1 = new SqlParameter("@Accn", DBNull.Value);
            SqlParameter p2 = new SqlParameter("@ctrl_no", ctrl);
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(p1);
            paras.Add(p2);
//            var d = clas.ExProcDS("getCatelogFull", paras);
//            var dtaccn = ExtConvert.ConvertTo<bookaccessionmaster>(d.Tables[0]);
            DataTable dttmp=new DataTable();
            dttmp.Columns.Add("accessionnumber");
            dttmp.Columns.Add("ordernumber");
            dttmp.Columns.Add("indentnumber");
            dttmp.Columns.Add("form");
            dttmp.Columns.Add("accessionid",typeof(decimal));
            dttmp.Columns.Add("accessioneddate",typeof(DateTime));
            dttmp.Columns.Add("booktitle");
            dttmp.Columns.Add("srno",typeof(decimal));
            dttmp.Columns.Add("released");
            dttmp.Columns.Add("bookprice", typeof(decimal));
            dttmp.Columns.Add("srNoOld", typeof(decimal));
            dttmp.Columns.Add("Status");
            dttmp.Columns.Add("ReleaseDate", typeof(DateTime));
            dttmp.Columns.Add("IssueStatus");
            dttmp.Columns.Add("LoadingDate", typeof(DateTime));
            dttmp.Columns.Add("CheckStatus");
            dttmp.Columns.Add("ctrl_no", typeof(long));
            dttmp.Columns.Add("editionyear", typeof(short));
            dttmp.Columns.Add("Copynumber", typeof(short));
            dttmp.Columns.Add("specialprice", typeof(decimal));
            dttmp.Columns.Add("pubYear", typeof(short));
            dttmp.Columns.Add("biilNo");
            dttmp.Columns.Add("billDate", typeof(DateTime));
            dttmp.Columns.Add("catalogdate", typeof(DateTime));
            dttmp.Columns.Add("Item_type");
            dttmp.Columns.Add("OriginalPrice", typeof(decimal));
            dttmp.Columns.Add("OriginalCurrency");
            dttmp.Columns.Add("userid");
            dttmp.Columns.Add("vendor_source");
            dttmp.Columns.Add("VendorId", typeof(int));
            dttmp.Columns.Add("program_id", typeof(int));
            dttmp.Columns.Add("DeptCode", typeof(int));
            dttmp.Columns.Add("DSrno", typeof(decimal));
            dttmp.Columns.Add("DeptName");
            dttmp.Columns.Add("ItemCategoryCode", typeof(int));
            dttmp.Columns.Add("ItemCategory");
            dttmp.Columns.Add("Loc_id", typeof(int));
            dttmp.Columns.Add("RfidId");
            dttmp.Columns.Add("BookNumber");
            dttmp.Columns.Add("SetOFbooks", typeof(int));
            dttmp.Columns.Add("SearchText");
            dttmp.Columns.Add("IpAddress");
            dttmp.Columns.Add("TransNo", typeof(int));
            dttmp.Columns.Add("AppName");
            DataRow dr =dttmp.NewRow();
            dr[0] = dval.Item2.accessionnumber;
            dr[1] = dval.Item2.ordernumber;
            dr[2] = dval.Item2.indentnumber;
            dr[3] = dval.Item2.form;
            dr[4] = dval.Item2.accessionid;
            dr[5] = dval.Item2.accessioneddate;
            dr[6] = dval.Item2.booktitle;
            dr[7] = dval.Item2.srno;
            dr[8] = dval.Item2.released;
            dr[9] = dval.Item2.bookprice;
            dr[10] = dval.Item2.srNoOld;
            dr[11] = dval.Item2.Status;
            dr[12] = dval.Item2.ReleaseDate;
            dr[13] = dval.Item2.IssueStatus;
            dr[14] = dval.Item2.LoadingDate;
            dr[15] = dval.Item2.CheckStatus;
            dr[16] = dval.Item2.ctrl_no;
            dr[17] = dval.Item2.editionyear;
            dr[18] = dval.Item2.Copynumber;
            dr[19] = dval.Item2.specialprice;
            dr[20] = dval.Item2.pubYear;
            dr[21] = dval.Item2.biilNo;
            dr[22] = dval.Item2.billDate;
            dr[23] = dval.Item2.catalogdate;
            dr[24] = dval.Item2.Item_type;
            dr[25] = dval.Item2.OriginalPrice;
            dr[26] = dval.Item2.OriginalCurrency;
            dr[27] = dval.Item2.userid;
            dr[28] = dval.Item2.vendor_source;
            dr[29] = dval.Item2.VendorId;
            dr[30] = dval.Item2.program_id;
            dr[31] = dval.Item2.DeptCode;
            dr[32] = dval.Item2.DSrno;
            dr[33] = dval.Item2.DeptName;
            dr[34] = dval.Item2.ItemCategoryCode;
            dr[35] = dval.Item2.ItemCategory;
            dr[36] = dval.Item2.Loc_id;
            dr[37] = dval.Item2.RfidId;
            dr[38] = dval.Item2.BookNumber;
            dr[39] = dval.Item2.SetOFbooks;
            dr[40] = dval.Item2.SearchText;
            dr[41] = dval.Item2.IpAddress;
            dr[42] = dval.Item2.TransNo;
            dr[43] = dval.Item2.AppName;


            dttmp.Rows.Add(dr);
            //d.Tables[0].Columns.Remove("location");

            //var dtcatg = ExtConvert.ConvertTo<bookaccessionmaster>(d.Tables[2]);
            List<SqlParameter> lsp = new List<SqlParameter>();
            try
            {
                lsp.Add(new SqlParameter { ParameterName = "@Accession", Value = dttmp, SqlDbType = System.Data.SqlDbType.Structured });
                lsp.Add(new SqlParameter { ParameterName = "@Bookcatalog", Value = dcatg.Item2, SqlDbType = System.Data.SqlDbType.Structured });
                lsp.Add(new SqlParameter { ParameterName = "@Bookauthor", Value = dauth.Item2, SqlDbType = System.Data.SqlDbType.Structured });
                var t = clas.ExProcReturn("webLibCatalog", lsp);
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message+";"+ex.InnerException??ex.InnerException.Message, this,dbUtilities.MsgLevel.Failure);
 
            }
            //            d.Tables[2].Rows[0]["maps"] = DBNull.Value;
            clas.TrClose();



        }

        protected async void btnApiFind_Click(object sender, EventArgs e)
        {
            if (txApiAccn.Text.Trim() != "")
            {
                try
                {
                    

                var uri = new Uri(LibApiUrl+"Catalog/GetCategDetail?Accn="+txApiAccn.Text.Trim());
                HttpClient client = new HttpClient();
                client.BaseAddress = uri;
                var resp = client.GetAsync(uri).Result;
                var data = await resp.Content.ReadAsStringAsync();

                var dlen=data.Length;
               ReturnData< CatalogClass> mdata=new ReturnData<CatalogClass>();
                var categ=JsonConvert.DeserializeObject<ReturnData<CatalogClass>>(data);
                    message.PageMesg(categ.Data.Accession.Count().ToString(), this);
                    if (categ.isSuccess)
                    {
                            var accn = categ.Data.Accession;
                            foreach (var ac in accn)
                                ac.billDate = ac.billDate == null || Convert.ToDateTime(ac.billDate).Year == 1900 ? null : ac.billDate;
                            grdCopies.DataSource = accn;
                            grdCopies.DataBind();
                            txaccessionnumber.Text = accn[0].accessionnumber;
                            txbiilNo.Text = accn[0].biilNo ?? accn[0].biilNo;
                            txindentnumber.Text = accn[0].indentnumber ?? accn[0].indentnumber;
                            ddlForm.SelectedValue = ddlForm.Items.FindByText(accn[0].form).Value;

                            hdaccessionid.Value = accn[0].accessionid.ToString();
                            txaccessiondate.Text = accn[0].accessioneddate == null ? "" : Convert.ToDateTime(accn[0].accessioneddate).ToString("dd-MMM-yyyy");
                            txbooktitle.Text = accn[0].booktitle;  //make it from bookcatalog
                            hdsrno.Value = accn[0].srno.ToString();
                            hdreleased.Value = accn[0].released.ToString();
                            txbookprice.Text = accn[0].bookprice.ToString();
                            hdsrNoOld.Value = accn[0].srNoOld.ToString();
                            ddlItemStat.SelectedValue = accn[0].Status.ToString();
                            txreleasedate.Text = accn[0].ReleaseDate == null ? "" : Convert.ToDateTime(accn[0].ReleaseDate).ToString("dd-MMM-yyyy");
                            /*txIssueStatus.Text = "";*/
                            txLoadingDate.Text = accn[0].LoadingDate == null ? "" : Convert.ToDateTime(accn[0].LoadingDate).ToString("dd-MMM-yyyy");
                            labCheckStatus.Text = accn[0].CheckStatus;
                            hdctrl_no.Value = accn[0].ctrl_no.ToString();
                            txeditionyear.Text = accn[0].editionyear.ToString();
                            txCopynumber.Text = accn[0].Copynumber.ToString();
                            txspecialprice.Text = accn[0].specialprice.ToString();
                            txpubYear.Text = accn[0].pubYear.ToString();
                            txbiilNo.Text = accn[0].biilNo;
                            txbillDate.Text = accn[0].billDate != null ? string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(accn[0].billDate)) : "";
                            txcatalogdate.Text = accn[0].catalogdate == null ? "" : Convert.ToDateTime(accn[0].catalogdate).ToString("dd-MMM-yyyy");
                            ddlItemtype.SelectedValue = ddlItemtype.Items.FindByText(accn[0].Item_type).Value;
                            txOriginalPrice.Text = accn[0].OriginalPrice.ToString();
                            ddlOriginalCurrency.SelectedValue = ddlOriginalCurrency.Items.FindByText(accn[0].OriginalCurrency.Trim()).Value;
                            hdVendorid.Value = accn[0].VendorId.ToString();
                            txVendor.Text = accn[0].vendor_source;
                            hdissuestatus.Value = accn[0].IssueStatus;
                            hdcheckstatus.Value = accn[0].CheckStatus;
                            ddlDeptCode.SelectedValue = accn[0].DeptCode.ToString();
                            hdDSrno.Value = "";
                            ddlItemCategoryCode.SelectedValue = accn[0].ItemCategoryCode.ToString();
                            txLocation.Text = accn[0].Location;
                            txRfidid.Text = accn[0].RfidId;
                            ddlOriginalCurrency.SelectedValue = ddlOriginalCurrency.Items.FindByText(accn[0].OriginalCurrency).Value;
                            txOriginalPrice.Text = accn[0].OriginalPrice.ToString();

                            txBookNumber.Text = accn[0].BookNumber;
                            //txSetOFbooks.Text = accn[0].setofbooks;
                            //txSearchText.Text = accn[0].searchtext;
                            //txIpAddress.Text = accn[0].ipaddress;
                            //txTransNo.Text = accn[0].transno;
                            //txAppName.Text = accn[0].appname;
                            hdVendorid.Value = accn[0].VendorId.ToString();
                            txVendor.Text = accn[0].vendor_source;
                            ddlprogram_id.SelectedValue = accn[0].program_id.ToString();
                        //                txItemCategory.Text = "";
                        /*              txSetOFbooks.Text = "";
                                      txSearchText.Text = "";
                                      txIpAddress.Text = "";
                                      txTransNo.Text = "";
                                      txAppName.Text = "";
                                      txVendorId.Text = "";*/
                        var ctg = categ.Data.Catalog;//  ExtConvert.ConvertTo<Models.BookCatalogLib>(d.Tables[2])[0];
                            if (ctg.catalogdate1 != null)
                                txcatalogdate1.Text = Convert.ToDateTime(ctg.catalogdate1).ToString("dd-MMM-yyyy");
                            // txctrl_no.Text = ctg.ctrl_no;
                            ddlbooktype.SelectedValue = ctg.booktype.ToString();
                            txvolumenumber.Text = ctg.volumenumber;
                            txinitpages.Text = ctg.initpages;
                            txpages.Text = ctg.pages.ToString();
                            txparts.Text = ctg.parts;
                            txleaves.Text = ctg.leaves;
                            ddlBound.SelectedValue = ctg.boundind.ToString();

                            txtitle.Text = ctg.title;
                            hdpublishercode.Value = ctg.publishercode.ToString();
                            txedition.Text = ctg.edition;
                            txisbn.Text = ctg.isbn;
                            txsubject1.Text = ctg.subject1;
                            txsubject2.Text = ctg.subject2;
                            txsubject3.Text = ctg.subject3;
                            txBooksize.Text = ctg.Booksize;
                            txLCCN.Text = ctg.LCCN;
                            ddlMedia.SelectedValue = ddlMedia.Items.FindByText(ctg.MaterialDesignation).Value;
                            txVolumepages.Text = ctg.Volumepages;
                            txbiblioPages.Text = ctg.biblioPages;
                            ddlbookindex.SelectedValue = ctg.bookindex;
                            ddlillustration.SelectedValue = ctg.illustration;
                            ddlvariouspaging.SelectedValue = ctg.variouspaging;
                            txmaps.Text = ctg.maps.ToString();
                            ddlETalEditor.SelectedValue = ctg.ETalEditor;
                            ddlETalCompiler.SelectedValue = ctg.ETalCompiler;
                            ddlETalIllus.SelectedValue = ctg.ETalIllus;
                            ddlETalTrans.SelectedValue = ctg.ETalTrans;
                            //                ddlETalAuthor.se = ctg.ETalAuthor;
                            txaccmaterialhistory.Text = ctg.accmaterialhistory;
                            txMaterialDesignation.Text = ctg.MaterialDesignation;
                            txissn.Text = ctg.issn;
                            txVolume.Text = ctg.Volume;
                            ddldept.SelectedValue = ctg.dept.ToString();
                            ddllanguage_id.Text = ctg.language_id.ToString();
                            txpart.Text = ctg.part;
                            txeBookURL.Text = ctg.eBookURL;
                            txFixedData.Text = ctg.FixedData;
                            txcat_Source.Text = ctg.cat_Source;
                            txIdentifier.Text = ctg.Identifier;
                            txfirstname.Text = ctg.firstname;
                            txpercity.Text = ctg.percity;
                            txperstate.Text = ctg.perstate;
                            txpercountry.Text = ctg.percountry;
                            txperaddress.Text = ctg.peraddress;
                            txdepartmentname.Text = ctg.departmentname;
                            // txBtype.Text = ctg.Btype; //save text for ite_type
                            txlanguage_name.Text = ctg.language_name;
                            txPublisherNo.Text = ctg.PublisherNo;
                            txPubSource.Text = ctg.PubSource;
                            //                txSysCtrlNo.Text = ctg.SysCtrlNo;
                            //              txNLMCN.Text = ctg.NLMCN;
                            txGeoArea.Text = ctg.GeoArea;
                            txPhyExtent.Text = ctg.PhyExtent;
                            txPhyOther.Text = ctg.PhyOther;
                            txpubDate.Text = ctg.pubDate;
                            txBookCost.Text = ctg.BookCost;
                            txlatestTransDate.Text = string.IsNullOrEmpty(ctg.latestTransDate) ? "" : string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ctg.latestTransDate));
                            txItemCategory.Text = ctg.ItemCategory;
                        var catgdata = categ.Data.CatalogData;
                        txclassnumber.Text = catgdata.classnumber;
                        txbooknumber2.Text= catgdata.booknumber;
                        //                txSearchText.Text = ctg.SearchText; //save the search text
                        //              txTransNo.Text = ctg.TransNo;
                        var ath = categ.Data.Author;// ExtConvert.ConvertTo<BookAuthor>(d.Tables[1])[0];
                            txfirstname1.Text = ath.firstname1;
                            txmiddlename1.Text = ath.middlename1;
                            txlastname1.Text = ath.lastname1;
                            txfirstname2.Text = ath.firstname2;
                            txmiddlename2.Text = ath.middlename2;
                            txlastname2.Text = ath.lastname2;
                            txfirstname3.Text = ath.firstname3;
                            txmiddlename3.Text = ath.middlename3;
                            txlastname3.Text = ath.lastname3;
                            txPersonalName.Text = ath.PersonalName;
                            txDateAssociated.Text = ath.DateAssociated;
                            //                txRelatorTermP.Text = ath.RelatorTermP;
                            txCorporateName.Text = ath.CorporateName;
                        //              txRelatorTermC.Text = ath.RelatorTermC;
                        //            txUniFormTitle.Text = ath.UniFormTitle;
                        //          txDateofWork.Text = ath.DateofWork;
                        //txLanguageofWork.Text = ath.LanguageofWork;
                        //txstmtofResponsibility.Text = ath.stmtofResponsibility;
                        // txAddedPersonalName.Text = ath.AddedPersonalName;
                        // txTransNo.Text = ath.TransNo;
                        var sers = categ.Data.Series;// ExtConvert.ConvertTo<BookSeries>(d.Tables[5])[0];//see marc rec - 490 and add relevant fields
                            txSeriesName.Text = sers.SeriesName;
                            txseriesNo.Text = sers.seriesNo;
                        var cnf = categ.Data.Conference;// ExtConvert.ConvertTo<BookConference>(d.Tables[3])[0];//see marc rec - 490 and add relevant fields
                            txSubtitle.Text = cnf.Subtitle;
                            txParalleltype.Text = cnf.Paralleltype;
                            txConfName.Text = cnf.ConfName;
                            txConfYear.Text = cnf.ConfYear;
                            txBNNote.Text = cnf.BNNote;
                            txCNNote.Text = cnf.CNNote;
                            txAbstract.Text = cnf.Abstract;
                            txProgram_name.Text = cnf.Program_name;
                            txConfPlace.Text = cnf.ConfPlace;
                        

                    }
                }
                catch (Exception exc)
                {
                    message.PageMesg(exc.Message + ";" + exc.InnerException ?? exc.InnerException.Message, this, dbUtilities.MsgLevel.Failure);
                }

            }
        }
    }
}