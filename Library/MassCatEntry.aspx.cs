using Audit;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class MassCatEntry : BaseClass
    {
        DBIStructure DBI = new DBIStructure();
        libGeneralFunctions LibObj = new libGeneralFunctions();
        DataSet dsGen = new DataSet();
        GlobClassTr gCla = new GlobClassTr();
        GlobClassTr con = new GlobClassTr();
        messageLibrary msgLibrary = new messageLibrary();
        dbUtilities message = new dbUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                // booknumber of call number is parameterized to be stored in accessionmaster based on featuresper-15
                string qer = " select * from featuresper where fid=15";
                var dtFet = new DataTable();
                var sFilt = new FillDsTables();
                sFilt.FillDs(qer,ref dtFet);
                if (dtFet.Rows.Count > 0)
                {
                           this.hdBookNumAccn.Value = "1";
                         this.labBookN.ToolTip = "Stored for individual Accession Copy";
                }
                else
                {
                       this.hdBookNumAccn.Value = "";
                     this.labBookN.ToolTip = "Stored same for all Accession Copies";
                }
                string eventArgs = Request["__EVENTARGUMENT"];
                if (eventArgs == "AccNoSelected")
                {
                    AccNoSelected(sender, e);
                }
                if (eventArgs == "BookCollectionSelect")
                {
                    
                    TitleSelected(sender, e);
                }

                this.hdIpAddress.Value = Request.UserHostAddress;

            }

            if (!(IsPostBack == true))
            {
                if (string.IsNullOrEmpty(this.hdnStime.Value))
                {
                    if (ViewState["T"] is null)
                    {
                        this.hdnStime.Value = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;
                        ViewState["T"] = this.hdnStime.Value;
                    }
                }
                FRFIDPermission();
                // Hidetab()
                Refreshloc();
                Searchshow();
                if (string.IsNullOrEmpty(this.txtVend.Text))
                {
                    var dsVend = new DataSet();
                    var Vend = new FillDsTables();
                    // Vend.FillDs("select * from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname='NA' and percity='NA'", dsVend, "Vend")
                    // Vend.FillDs("select * from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname='NA' ", dsVend, "Vend")
                    // If dsVend.Tables(0).Rows.Count = 0 Then
                    // lblMsg.Text = "Vendor Master must have a record with 'NA' as Vendor Name and City."
                    // lblMsg.Visible = True
                    // LibObj.MsgBox1("Vendor Master must have a record with 'NA' as Vendor Name and City.", Me)
                    // Exit Sub
                    // End If
                    // txtVend.Text = dsVend.Tables(0).Rows(0)("vendorname")
                    // txtVend.Text += ", " & dsVend.Tables(0).Rows(0)("percity")
                }
                this.btnAddCopy.Enabled = false;

                //this.lblMsg.Visible = false;

                // LibObj.MsgBox1(Session("user_id"), Me)
                this.txtDt.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Today);
           //     this.lblHead.Text = Request.QueryString["title"].ToString();
                Session["rblDBOption"] = this.rblDBOption.SelectedValue;

                var dat = DateTime.Now;
                this.lblTod.Text = "As On " + string.Format("{0:dd-MMM-yyyy}", dat);
                string Err = "";
                var GetDt = new FillDsTables();
                Err = GetDt.FillDs("select count(*) Catalogued from bookaccessionmaster where ctrl_no<>0;select count(*) Accessioned from bookaccessionmaster ; ", ref dsGen, "TCats");
                if (!string.IsNullOrEmpty(Err))
                {
                    // lblMsg.Text = Err
                    // lblMsg.Visible = True
                    // LibObj.MsgBox1(Err, Me)
                    message.PageMesg(Err, this, dbUtilities.MsgLevel.Failure);

                    return;
                }
                this.lblTotC.Text = Convert.ToString(dsGen.Tables["TCats"].Rows[0][0]);
                this.lblTotA.Text = Convert.ToString(dsGen.Tables[1].Rows[0][0]);
                var argddl = this.ddlCat;
                Err = this.FillDDL("select id,category_loadingstatus from categoryloadingstatus order by id", ref argddl);
                this.ddlCat = argddl;
                if (!string.IsNullOrEmpty(Err))
                {
                    if (!string.IsNullOrEmpty(Err))
                    {
                        // LibObj.MsgBox1("Error in Category DDL: " & Err, Me)
                        message.PageMesg("Error in Category DDL: " + Err, this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                }
                // 
                var argddl1 = this.ddlCategCP;
                this.FillDDL("select id,category_loadingstatus from categoryloadingstatus order by id", ref argddl1);
                this.ddlCategCP = argddl1;
                var argddl2 = this.ddlIType;
                Err = this.FillDDL("select id,item_type from item_type order by id", ref argddl2);
                this.ddlIType = argddl2;
                if (!string.IsNullOrEmpty(Err))
                {
                    // If Err <> "" Then
                    // LibObj.MsgBox1("Error in Item Type DDL: " & Err, Me)
                    message.PageMesg("Error in Item stat DDL: " + Err, this, dbUtilities.MsgLevel.Warning);
                    return;
                    // End If
                }
                var argddl3 = this.ddlItemTypeCP;
                Err = this.FillDDL("select id,item_type from item_type order by id", ref argddl3);
                this.ddlItemTypeCP = argddl3;

                var argddl4 = this.ddlIStat;
                Err = this.FillDDL("select itemstatusid,itemstatus from itemstatusmaster order by itemstatusid", ref argddl4);
                this.ddlIStat = argddl4;
                if (!string.IsNullOrEmpty(Err))
                {
                    // If Err <> "" Then
                    // LibObj.MsgBox1("Error in Item stat DDL: " & Err, Me)
                    message.PageMesg("Error in Item stat DDL: " + Err, this, dbUtilities.MsgLevel.Warning);

                    return;
                    // End If
                }
                var argddl5 = this.ddlCurr;
                Err = this.FillDDL("select currencycode,currencyname from exchangemaster order by currencycode", ref argddl5);
                this.ddlCurr = argddl5;
                if (!string.IsNullOrEmpty(Err))
                {
                    // If Err() <> "" Then
                    // LibObj.MsgBox1("Error in Currency DDL: " & Err, Me)
                    message.PageMesg("Error in Currency DDL: " + Err, this, dbUtilities.MsgLevel.Warning);
                    return;
                    // End If
                }
                var argddl6 = this.ddlLang;
                Err = this.FillDDL("select language_id,language_name from translation_language ", ref argddl6);
                this.ddlLang = argddl6;
                if (!string.IsNullOrEmpty(Err))
                {
                    // LibObj.MsgBox1("Error in Lang DDL: " & Err, Me)
                    message.PageMesg("Error in Lang DDL: " + Err, this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                var argddl7 = this.ddlMedia;
                Err = this.FillDDL("select media_id,media_name from media_type ", ref argddl7);
                this.ddlMedia = argddl7;
                if (!string.IsNullOrEmpty(Err))
                {
                    // LibObj.MsgBox1("Error in Media DDL: " & Err, Me)
                    message.PageMesg("Error in Media DDL: " + Err, this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                var argddl8 = this.ddlDept;
                Err = this.FillDDL("select departmentcode,departmentname from departmentmaster ", ref argddl8);
                this.ddlDept = argddl8;
                if (!string.IsNullOrEmpty(Err))
                {
                    // LibObj.MsgBox1("Error in Dept DDL: " & Err, Me)
                    message.PageMesg("Error in Dept DDL: " + Err, this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                // ddlDeptCP
                var argddl9 = this.ddlDeptCP;
                this.FillDDL("select departmentcode,departmentname from departmentmaster ", ref argddl9);
                this.ddlDeptCP = argddl9;
                if (!string.IsNullOrEmpty(this.hdnItype.Value))
                {
                    this.ddlIType.SelectedIndex = Convert.ToInt32(this.hdnItype.Value);
                }
                if (!string.IsNullOrEmpty(this.hdnIStat.Value))
                {
                    this.ddlIStat.SelectedIndex = Convert.ToInt32(this.hdnIStat.Value);
                }
                if (!string.IsNullOrEmpty(this.hdnCurr.Value))
                {
                    this.ddlCurr.SelectedIndex = Convert.ToInt32(this.hdnCurr.Value);
                }
                if (!string.IsNullOrEmpty(this.hdnLang.Value))
                {
                    this.ddlLang.SelectedIndex = Convert.ToInt32(this.hdnLang.Value);
                }
                if (!string.IsNullOrEmpty(this.hdnDept.Value))
                {
                    this.ddlDept.SelectedIndex = Convert.ToInt32(this.hdnDept.Value);
                }
                if (!string.IsNullOrEmpty(this.hdnMedia.Value))
                {
                    this.ddlMedia.SelectedIndex = Convert.ToInt32(this.hdnMedia.Value);
                }



                // ExtAccNo.Enabled = True



                this.txtCopyNo.Text = "1";
                string Qer = "select count(*) from FeaturesPer where fid=12 "; // class number from server sugestions
                var dtClasNo = new DataTable();
                Err = GetDt.FillDs(Qer,ref dtClasNo);
                if (dtClasNo.Rows.Count > 0)
                {
                    if (dtClasNo.Rows[0][0].ToString()== "1")
                    {
                        this.AncClasNo.Style.Add("display", "inline");
                    }
                    // txt_TestSuggoftite.Attributes.Add("click", "javascript:fillClasSer(this)")
                    // txtBillNo.Attributes.Add("blur()", "fillClasSer(this)")
                }
                string cnf = ConfigurationManager.AppSettings["z39path"];
                this.hdz39path.Value = cnf;
                if (!string.IsNullOrEmpty(this.hdz39path.Value))
                {
                    this.btnZ39.Visible = true;
                }
                else
                {
                    this.btnZ39.Visible = false;
                }


                // ddlCat.Attributes.Add("onBlur", "javascript:ddlServer(this)")
            }  // postback line
            ButtonVisibility();
        }
        public string FillDDL(string Qry, ref DropDownList ddl)
        {
            var GetDDL = new FillDsTables();
            var dsLoc = new DataSet();
            string Err = GetDDL.FillDs(Qry, ref dsLoc, "Zero");
            if (!string.IsNullOrEmpty(Err))
            {
                return Err;
            }
            string[] QryS = Qry.Split(' ');
            string[] IdPart = QryS[1].Split(',');
            ddl.DataSource = dsLoc;
            ddl.DataTextField = IdPart[1];          // "institutename"
            ddl.DataValueField = IdPart[0];              // "institutecode"
            ddl.DataBind();
            return "";
        }
        public void Refreshloc()
        {
            try
            {
                var dt = new DataTable();
                gCla.TrOpen();
                try
                {
                    dt = gCla.DataT("select * from TempLocation");
                }
                catch (Exception ex)
                {
                    string Str = Convert.ToString(gCla.ExScaler("create table TempLocation (Location nvarchar(400),LocaId int)"));
                }
                string Del = Convert.ToString(gCla.ExScaler("Delete from TempLocation"));

                // Dim Insert As String = gCla.ExScaler("insert into TempLocation(Location,LocaId)select  distinct  dbo.getlocation_String(location_path) lc,Loc_id lid FROM bookaccessionmaster a inner join bookauthor b on a.ctrl_no = b.ctrl_no  inner join mapped_location ml on ml.Id=a.Loc_id")
                string Insert = Convert.ToString(gCla.ExScaler("insert into TempLocation(Location,LocaId) select dbo.LocDecode2(id),id from Mapped_Location"));
                gCla.TrClose();
            }
            catch (Exception ex)
            {
                gCla.TrRollBack();
            }
        }
        private void ResetAll()
        {
            Session["auditdata"] = null;
            this.hdTransNo.Value = "";
            this.labCorrect.Text = "";
            resetCpRegion();
            this.hdnBookId.Value = "";
            this.txtLocation.Text = "";
            this.txtLoc2.Text = "0";
            this.txSearchText.Text = "";
            this.ddlCat.SelectedIndex = 0;
            this.ddlIStat.SelectedIndex = 0;
            this.ddlIType.SelectedIndex = 0;
            this.ddlLang.SelectedIndex = 0;
            this.ddlDept.SelectedIndex = 0;
            this.dvattachments.Style.Add("display", "none");

            this.txtVAcc.Text = "";
            this.txtVAcc.Enabled = false;
            this.txtVolume2.Text = "";
            this.txtVolume2.Enabled = false;
            this.btnAddV.Enabled = true;
            this.txtBillNo.Text = "";
            this.txtBillDt.Text = "";
            this.txtVAcc.Text = "";
            this.TxtSetOffVol.Text = "";
            this.txtVAcc.Enabled = false;
            // txtVol.Enabled = False
            // txtVol.Text = ""
            this.btnAddV.Enabled = false;
            this.hdnInsUpd.Value = "I";
            this.hdnPublId.Value = (-1).ToString();
            this.hdnCtrl_no.Value = 0.ToString();
      //      this.lblMsg.Visible = false;
        //    this.lblMsg.Text = "";
            this.btnCopyMore.Enabled = false;

            // txtVend.Text = "NA, NA"
            this.txtVend.Text = "";
            Session["ctrl_no"] = null;
            Session["AccNo"] = null;
            this.txtAccNoCopy.Enabled = false;
            this.txtCopyNo.Enabled = false;
            this.txtAccNo.Text = "";
            this.btnDel.Enabled = false;
            this.btnDel.Text = "Delete";
            this.txtAccNoCopy.Text = "";
            this.txtAuthF1.Text = "";
            this.txtAuthF2.Text = "";
            this.txtAuthF3.Text = "";
            this.txtAuthL1.Text = "";
            this.txtAuthL2.Text = "";
            this.txtAuthL3.Text = "";
            this.txtAuthM1.Text = "";
            this.txtAuthM2.Text = "";
            this.txtAuthM3.Text = "";
            this.txtBookNo.Text = "";
            this.txtClassNo.Text = "";
            this.txtPart.Text = "";
            // txtBPages.Text = ""
            this.txtCopyNo.Text = "";
            this.txtEdition.Text = "";
            this.txtEditionYear.Text = "";
            // txtInitPg.Text = ""
            this.txtISBN.Text = "";
            this.txtISSN.Text = "";
            // txtParts.Text = ""
            this.txtPages.Text = "";
            this.txtPrice.Text = "";
            this.txtPubl.Text = "";
            this.txtPubYear.Text = "";
            this.txtSTitle.Text = "";
            this.txtSub1.Text = "";
            this.txtSub2.Text = "";
            this.txtSub3.Text = "";
            // txtVend.Text = ""
            this.txt_TestSuggoftite.Text = "";
            // txtVolNo.Text = ""
            this.txtVolume.Text = "";
            this.txtCopyNo.Text = "1";
            // Extended area
            this.txtEdF1.Text = "";
            this.txtEdM1.Text = "";
            this.txtEdL1.Text = "";
            this.txtCompF1.Text = "";
            this.txtCompM1.Text = "";
            this.txtCompL1.Text = "";
            this.txtIlF1.Text = "";
            this.txtIlM1.Text = "";
            this.txtIlL1.Text = "";
            this.txtTranF1.Text = "";
            this.txtTranM1.Text = "";
            this.txtTranL1.Text = "";
            this.txtEdF2.Text = "";
            this.txtEdM2.Text = "";
            this.txtEdL2.Text = "";
            this.txtCompF2.Text = "";
            this.txtCompM2.Text = "";
            this.txtCompL2.Text = "";
            this.txtIlF2.Text = "";
            this.txtIlM2.Text = "";
            this.txtIlL2.Text = "";
            this.txtTranF2.Text = "";
            this.txtTranM2.Text = "";
            this.txtTranL2.Text = "";
            this.txtEdF3.Text = "";
            this.txtEdM3.Text = "";
            this.txtEdL3.Text = "";
            this.txtCompF3.Text = "";
            this.txtCompM3.Text = "";
            this.txtCompL3.Text = "";
            this.txtIlF3.Text = "";
            this.txtIlM3.Text = "";
            this.txtIlL3.Text = "";
            this.txtTranF3.Text = "";
            this.txtTranM3.Text = "";
            this.txtTranL3.Text = "";

            this.txtDt.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            this.txtacccopydt.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            this.btnSave.Text = "Save";
            this.txtCopyNo.Enabled = false;
            this.txtAccNoCopy.Enabled = false;
            this.btnAddCopy.Enabled = false;
            this.txtacccopydt.Enabled = false;
            // txtLocation.Text = ""
            this.UPCnt.Update();
          //  this.UPMsg.Update();
            this.updMain.Update();
            this.txtAccNo.Focus();

        }
        protected void AccNoSelected(object sender, EventArgs e) // Handles btnAdhoc.Click
        {

            // Return
            this.dvattachments.Style.Add("display", "none");


            string[] val = this.hdnAccNo.Value.Split('|');
            this.txtAccNo.Text = this.hdnAccNo.Value;
            string sQer;
            try
            {
                sQer = "select booktitle,biilno,billdate, copynumber, subtitle, classnumber, c.booknumber, volume, parts,part,a.catalogdate, edition, editionyear," + "  pubyear, itemcategorycode, a.item_type,isnull( a.booknumber,'') booknumbercp, f.id item_typeid, status, a.originalcurrency,g.currencycode, bookprice, language_id, a.DeptCode,b.publishercode," + "  h.firstname, i.percity, vendor_source, isbn, issn, pages, bibliopages, volumenumber, initpages, materialdesignation, media_id, form, subject1,a.ctrl_no, " + "  subject2, subject3, firstname1, middlename1, lastname1,firstname2, middlename2, lastname2,firstname3, middlename3, lastname3,/*buildingid,floorid,almiraid,rackid,*/loc_id, " + " 		editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3," + "CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3, " + "illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3," + " TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3 " + "  from bookaccessionmaster a, bookcatalog b, catalogdata c,  bookauthor d,bookconference e,bookrelators e2, item_type f, exchangemaster g, " + "  publishermaster h, addresstable i,media_type j where a.ctrl_no=b.ctrl_no and b.ctrl_no=c.ctrl_no and a.ctrl_no=d.ctrl_no and a.ctrl_no=e2.ctrl_no " + " and a.ctrl_no=c.ctrl_no and a.ctrl_no=e.ctrl_no and g.currencyname=a.originalcurrency and f.item_type=a.item_type " + " and b.publishercode=h.publisherid and /* b.publishercode=i.addid*/ h.publisherid=i.addid  and addrelation='publisher' and j.media_name=b.materialdesignation" + " and accessionnumber='" + this.txtAccNo.Text + "' ";
                var dsRetrCat = new DataSet();

                // Return
                var GetRetrCat = new FillDsTables();
                string Err = GetRetrCat.FillDs(sQer, ref dsRetrCat, "RetrCat");
                if (!string.IsNullOrEmpty(Err))
                {
                    // lblMsg.Text = Err
                    // lblMsg.Visible = True
                    message.PageMesg(Err, this, dbUtilities.MsgLevel.Failure);

                    // lblMsg.Value = Err
                    //this.UPMsg.Update();

                    // LibObj.MsgBox1("Could'nt retrieve Catalog detail: " && Err, Me)
                    return;
                }
                resetCpRegion();




                this.hdnCtrl_no.Value = dsRetrCat.Tables[0].Rows[0]["ctrl_no"].ToString();
                if (string.IsNullOrEmpty(this.txtAccNo.Text.Trim()) || dsRetrCat.Tables["RetrCat"].Rows.Count == 0)
                {
                    // lblMsg.Text = "No records found or Error in Acc No."
                    // lblMsg.Visible = True
                    // LibObj.MsgBox1("No records found or Error in Acc No.", Me)
                    message.PageMesg("No records found or Error in Acc No.", this, dbUtilities.MsgLevel.Failure);

                    return;
                }
                dsRetrCat.Tables[0].Rows[0]["loc_id"] = dsRetrCat.Tables[0].Rows[0].IsNull("loc_id") == true ? 0 : Convert.ToInt32(dsRetrCat.Tables[0].Rows[0]["loc_id"]);

                // for auditing
                var audw = new AccnAudit();
                var updc = new UpdCatalog();
                var audaccn = new BookAccn();
                var audbookcatg = new BookCatalog();
                var audbookauth = new BookAuth();
                var audcatdata = new CatalogData();
                var audconf = new BookConf();
                var lsaudaccn = new List<BookAccn>();
                var lsaudbookcatg = new List<BookCatalog>();
                var lsaudbookauth = new List<BookAuth>();
                var lsaudcatdata = new List<CatalogData>();
                var lsaudconf = new List<BookConf>();

                var dsLoc = new DataSet();
                string[] LocMaps;
                string SLoc = "";
                if (Convert.ToInt32(dsRetrCat.Tables[0].Rows[0]["loc_id"]) != 0)
                {
                    sQer = "select location_path from mapped_location where id=" + dsRetrCat.Tables[0].Rows[0]["loc_id"].ToString();
                    Err = GetRetrCat.FillDs(sQer, ref dsLoc, "Loc");
                    if (!string.IsNullOrEmpty(Err))
                    {
                        // lblMsg.Text = Err
                        // lblMsg.Visible = True
                        message.PageMesg(Err, this, dbUtilities.MsgLevel.Failure);

                        // lblMsg.Value = Err
                        //                        this.UPMsg.Update();

                        // LibObj.MsgBox1("Could'nt retrieve Catalog detail: " && Err, Me)
                        return;
                    }
                    LocMaps = dsLoc.Tables[0].Rows[0][0].ToString().Split('-');
                    for (int ii = 0, loopTo = LocMaps.Length - 1; ii <= loopTo; ii++)
                    {
                        sQer = "select locationobjectitem from locationobject_items where id=" + LocMaps[ii];
                        Err = GetRetrCat.FillDs(sQer, ref dsLoc, "Loc2");
                        if (!string.IsNullOrEmpty(Err))
                        {
                            // lblMsg.Text = Err
                            // lblMsg.Visible = True
                            message.PageMesg(Err, this, dbUtilities.MsgLevel.Failure);
                            // lblMsg.Value = Err
                            //                            this.UPMsg.Update();

                            // LibObj.MsgBox1("Could'nt retrieve Catalog detail: " && Err, Me)
                            return;
                        }
                        SLoc = SLoc + dsLoc.Tables["Loc2"].Rows[0][0] + "-".ToString();
                        dsLoc.Tables["Loc2"].Clear();
                    }
                    SLoc = SLoc.Substring(0, SLoc.Length - 1);
                    this.txtLocation.Text = SLoc;
                    this.txtLoc2.Text = dsRetrCat.Tables[0].Rows[0]["loc_id"].ToString();
                    this.hdLocidCP.Value = this.txtLoc2.Text;

                    //                    audaccn.Loc_id = Conversions.ToInteger(this.hdLocidCP.Value);

                    this.txLocationCP.Text = SLoc;
                }
                if (string.IsNullOrEmpty(SLoc))
                {
                    this.txtLocation.Text = "";
                    this.hdLocidCP.Value = "";
                    this.txLocationCP.Text = "";
                }



                this.txtBillNo.Text = dsRetrCat.Tables[0].Rows[0]["biilno"].ToString();
                //                audaccn.accessionnumber = this.txtAccNo.Text.Trim();
                //              audaccn.biilNo = Conversions.ToString(dsRetrCat.Tables[0].Rows[0]["biilno"]);
                this.txBillNoCP.Text = this.txtBillNo.Text;
                // Dim ddT As String = Year(CType(dsRetrCat.Tables(0).Rows(0)("billdate"), Date))
                if (dsRetrCat.Tables[0].Rows[0]["billdate"] is DBNull == false)
                {
                    if (!dsRetrCat.Tables[0].Rows[0]["billdate"].ToString().Contains("1900"))
                    {
                        this.txtBillDt.Text = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dsRetrCat.Tables[0].Rows[0]["billdate"]));
                        //                        audaccn.billDate = Conversions.ToDate(dsRetrCat.Tables[0].Rows[0]["billdate"]);
                        this.txBillDtCP.Text = this.txtBillDt.Text;
                    }
                }
                // 

                //          this.lblMsg.Text = "";
                //              // lblMsg.Value = ""
                this.txt_TestSuggoftite.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["booktitle"].ToString();
                            audaccn.booktitle = dsRetrCat.Tables["RetrCat"].Rows[0]["booktitle"].ToString();

                this.txFramTitle.Text = this.txt_TestSuggoftite.Text;
                this.txtCopyNo.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["copynumber"].ToString();
                    audaccn.Copynumber = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["copynumber"]);
                this.txCpBookno.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["booknumbercp"].ToString();
                         audaccn.BookNumber = dsRetrCat.Tables["RetrCat"].Rows[0]["booknumbercp"].ToString();
                this.txtSTitle.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["subtitle"].ToString();
                   audconf.Subtitle = dsRetrCat.Tables["RetrCat"].Rows[0] ["subtitle"].ToString();
                this.txtClassNo.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["classnumber"].ToString();
                audcatdata.classnumber = dsRetrCat.Tables["RetrCat"].Rows[0]["classnumber"].ToString();
                this.txtBookNo.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["booknumber"].ToString();
                audcatdata.booknumber = dsRetrCat.Tables["RetrCat"].Rows[0]["classnumber"].ToString();
                this.txtVolume.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["volume"].ToString();
                audbookcatg.Volume = dsRetrCat.Tables["RetrCat"].Rows[0]["volume"].ToString();
                // txtParts.Text = IIf(IsDBNull(dsRetrCat.Tables("RetrCat").Rows(0)("parts")), "", dsRetrCat.Tables("RetrCat").Rows(0)("parts"))
                this.txtPart.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["part"].ToString();
                audbookcatg.part = dsRetrCat.Tables["RetrCat"].Rows[0]["part"].ToString();
                this.txtEdition.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["edition"].ToString();
                audbookcatg.edition = dsRetrCat.Tables["RetrCat"].Rows[0]["edition"].ToString();
                this.txtEditionYear.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editionyear"].ToString();
                 audaccn.editionyear = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["editionyear"]);
                this.txtPubYear.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["pubyear"].ToString();
                  audaccn.pubYear = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["pubyear"]);


                this.ddlCat.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["itemcategorycode"].ToString();
                   audaccn.ItemCategoryCode = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["itemcategorycode"]);
                this.ddlCategCP.SelectedValue = this.ddlCat.SelectedValue;
                this.ddlIType.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["item_typeid"].ToString();
                 audaccn.Item_type = dsRetrCat.Tables["RetrCat"].Rows[0]["item_typeid"].ToString();
                this.ddlItemTypeCP.SelectedValue = this.ddlIType.SelectedValue;
                this.ddlIStat.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["status"].ToString();
                 audaccn.Status = dsRetrCat.Tables["RetrCat"].Rows[0]["status"].ToString();
                this.ddlCurr.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["currencycode"].ToString();
                //this.ddlCurr.Items.FindByValue(dsRetrCat.Tables["RetrCat"].Rows[0]["currencycode"].ToString()).Selected = true;

                  audaccn.OriginalCurrency = dsRetrCat.Tables["RetrCat"].Rows[0]["currencycode"].ToString();

                this.txtPrice.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["bookprice"].ToString();
                this.txCpPrice.Text = this.txtPrice.Text;
                   audaccn.OriginalPrice = Convert.ToDecimal(dsRetrCat.Tables["RetrCat"].Rows[0]["bookprice"]);
                this.ddlLang.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["language_id"].ToString();
                 audbookcatg.language_id = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["language_id"]);
                this.ddlDept.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["DeptCode"].ToString();
                 audaccn.DeptCode = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["DeptCode"]);
                this.ddlDeptCP.SelectedValue = this.ddlDept.SelectedValue;
                this.txtPubl.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["firstname"].ToString() + ", " + dsRetrCat.Tables["RetrCat"].Rows[0]["percity"].ToString();
                // ??????? publisdherid and vendorid required
                this.hdnPublId.Value = dsRetrCat.Tables["RetrCat"].Rows[0]["publishercode"].ToString();
                  audbookcatg.publishercode = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["publishercode"]);
                this.txtVend.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["vendor_source"].ToString();
                  audaccn.vendor_source = dsRetrCat.Tables["RetrCat"].Rows[0]["vendor_source"].ToString();
                this.txVendorCP.Text = this.txtVend.Text;
                this.txtISBN.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["isbn"].ToString();
                   audbookcatg.isbn = dsRetrCat.Tables["RetrCat"].Rows[0]["isbn"].ToString();
                this.txtISSN.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["issn"].ToString();
                  audbookcatg.issn = dsRetrCat.Tables["RetrCat"].Rows[0]["issn"].ToString();
                this.txISBNClas.Text = this.txtISBN.Text;

                this.txtPages.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["pages"].ToString();
                audbookcatg.pages = Convert.ToInt32(dsRetrCat.Tables["RetrCat"].Rows[0]["pages"]);
                //    // txtBPages.Text = IIf(IsDBNull(dsRetrCat.Tables("RetrCat").Rows(0)("bibliopages")), "", dsRetrCat.Tables("RetrCat").Rows(0)("bibliopages"))
                //   // txtVolNo.Text = IIf(IsDBNull(dsRetrCat.Tables("RetrCat").Rows(0)("volumenumber")), "", dsRetrCat.Tables("RetrCat").Rows(0)("volumenumber"))
                //  // txtInitPg.Text = IIf(IsDBNull(dsRetrCat.Tables("RetrCat").Rows(0)("initpages")), "", dsRetrCat.Tables("RetrCat").Rows(0)("initpages"))
                this.ddlMedia.SelectedValue = dsRetrCat.Tables["RetrCat"].Rows[0]["media_id"].ToString();
                //  // ddlForm.SelectedValue = dsRetrCat.Tables("RetrCat").Rows(0)("form")
                 audbookcatg.materialdesignation = this.ddlMedia.SelectedItem.Text;
                switch (dsRetrCat.Tables["RetrCat"].Rows[0]["form"].ToString().ToUpper() ?? "")
                {
                    case "SOFT BOUND":
                        {
                            this.ddlForm.SelectedValue = 0.ToString();
                            break;
                        }
                    case "HARD BOUND":
                        {
                            this.ddlForm.SelectedValue = 1.ToString();
                            break;
                        }

                    default:
                        {
                            this.ddlForm.SelectedValue = 0.ToString();
                            break;
                        }
                }
                this.txtDt.Text = string.Format("{0:dd-MMM-yyyy}", dsRetrCat.Tables["RetrCat"].Rows[0]["catalogdate"]);
                               audbookcatg.catalogdate1 = Convert.ToDateTime(string.Format("{0:dd-MMM-yyyy}", dsRetrCat.Tables["RetrCat"].Rows[0]["catalogdate"]));
                this.txtSub1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["subject1"].ToString();
                             audbookcatg.subject1 = this.txtSub1.Text;
                this.txtSub2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["subject2"].ToString();
                           audbookcatg.subject2 = this.txtSub2.Text;
                this.txtSub3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["subject3"].ToString();
                         audbookcatg.subject3 = this.txtSub3.Text;
                this.txtAuthF1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["firstname1"].ToString();
                       audbookauth.firstname1 = this.txtAuthF1.Text;
                this.txtAuthM1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["middlename1"].ToString();
                     audbookauth.middlename1 = this.txtAuthM1.Text;
                this.txtAuthL1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["lastname1"].ToString();
                   audbookauth.lastname1 = this.txtAuthL1.Text;
                this.txtAuthF2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["firstname2"].ToString();
                this.txtAuthM2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["middlename2"].ToString();
                this.txtAuthL2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["lastname2"].ToString();
                     audbookauth.firstname2 = this.txtAuthF2.Text;
                    audbookauth.middlename2 = this.txtAuthM2.Text;
                   audbookauth.lastname2 = this.txtAuthL2.Text;

                this.txtAuthF3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["firstname3"].ToString();
                this.txtAuthM3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["middlename3"].ToString();
                this.txtAuthL3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["lastname3"].ToString();
                     audbookauth.firstname3 = this.txtAuthF3.Text;
                    audbookauth.middlename3 = this.txtAuthM3.Text;
                   audbookauth.lastname3 = this.txtAuthL3.Text;


                this.txtAuth.Text = this.txtAuthL1.Text;
                // add lines for author extended
                this.txtEdF1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorFname1"].ToString();
                this.txtEdM1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorMname1"].ToString();
                this.txtEdL1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorLname1"].ToString();
                this.txtEdF2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorFname2"].ToString();
                this.txtEdM2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorMname2"].ToString();
                this.txtEdL2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorLname2"].ToString();
                this.txtEdF3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorFname3"].ToString();
                this.txtEdM3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorMname3"].ToString();
                this.txtEdL3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["editorLname3"].ToString();

                this.txtCompF1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerFname1"].ToString();
                this.txtCompM1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerMname1"].ToString();
                this.txtCompL1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerLname1"].ToString();
                this.txtCompF2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerFname2"].ToString();
                this.txtCompM2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerMname2"].ToString();
                this.txtCompL2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerLname2"].ToString();
                this.txtCompF3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerFname3"].ToString();
                this.txtCompM3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerMname3"].ToString();
                this.txtCompL3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["CompilerLname3"].ToString();

                this.txtIlF1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusFname1"].ToString();
                this.txtIlM1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusMname1"].ToString();
                this.txtIlL1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusLname1"].ToString();
                this.txtIlF2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusFname2"].ToString();
                this.txtIlM2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusMname2"].ToString();
                this.txtIlL2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusrLname2"].ToString();
                this.txtIlF3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusFname3"].ToString();
                this.txtIlM3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusMname3"].ToString();
                this.txtIlL3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["illusLname3"].ToString();

                this.txtTranF1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorFname1"].ToString();
                this.txtTranM1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorMname11"].ToString();
                this.txtTranL1.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorLname1"].ToString();
                this.txtTranF2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorFname2"].ToString();
                this.txtTranM2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorMname2"].ToString();
                this.txtTranL2.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorLname2"].ToString();
                this.txtTranF3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorFname3"].ToString();
                this.txtTranM3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorMname3"].ToString();
                this.txtTranL3.Text = dsRetrCat.Tables["RetrCat"].Rows[0]["TranslatorLname3"].ToString();

                if (this.hdBookNumAccn.Value == "1")
                {
                    sQer = "select isnull(booknumber,'') booknumber from bookaccessionmaster where accessionnumber='" + this.txtAccNo.Text.Trim() + "'";
                    var dtBn = new DataTable();
                    string serr = GetRetrCat.FillDs(sQer, ref dtBn);
                    if (string.IsNullOrEmpty(serr))
                    {
                        this.txtBookNo.Text = dtBn.Rows[0][0].ToString();

                    }
                }
                // LibObj.MsgBox1(ddlIStat.SelectedItem.ToString, Me)
                this.hdnInsUpd.Value = "U";
                this.btnSave.Text = "Update";
                this.btnAddCopy.Enabled = true;
                this.txtAccNoCopy.Enabled = true;
                this.txtAccNo.Enabled = true;
                this.txtCopyNo.Enabled = true;
                this.btnDel.Enabled = true;
                this.btnCopyMore.Enabled = true;
                this.txtVAcc.Enabled = true;
                this.txtVolume2.Enabled = true;
                this.txtacccopydt.Enabled = true;
                this.txtacccopydt.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                this.btnAddV.Enabled = true;
                Session["AccNo"] = this.txtAccNo.Text;
                /*
                try
                {
                    string sqery = " select distinct a.FileName,a.Remark from Libfiles a join LibFilesFor b on a.FileId=b.FileId and b.Number='" + this.hdnCtrl_no.Value + "' And b.NumberType ='catalog' ";
                    var dtcatg = new DataTable();

                    GetRetrCat.FillDs(sqery,ref dtcatg);
                    if (dtcatg.Rows.Count > 0)
                    {
                        this.dvattachments.Style.Add("display", "inline");
                        var txt = new HtmlGenericControl("div");
                        string cntt = "<h5>Catalog(all copies) have attachements:</h5><ul>";
                        for (int indx = 0, loopTo1 = dtcatg.Rows.Count - 1; indx <= loopTo1; indx++)
                            cntt = Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(cntt + "<li>File:<b>", dtcatg.Rows[indx][0]), "</b>, "), dtcatg.Rows[indx][1]), "</li>"));
                        cntt = cntt + "</ul>";
                        txt.InnerHtml = cntt;
                        this.dvattachlists.Controls.Add(txt);
                    }
                    sqery = "select  a.FileName,a.Remark from Libfiles a join LibFilesFor b on a.FileId=b.FileId and b.NumberType='accession' and b.number='" + this.txtAccNo.Text + "'";
                    var dtaccn = new DataTable();
                    GetRetrCat.FillDs(sqery,ref dtaccn);
                    if (dtaccn.Rows.Count > 0)
                    {
                        this.dvattachments.Style.Add("display", "inline");
                        var txt = new HtmlGenericControl("div");
                        string cntt = "<h5>Accn attachements:</h5><ul>";
                        for (int indx = 0, loopTo2 = dtaccn.Rows.Count - 1; indx <= loopTo2; indx++)
                            cntt = Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(cntt + "<li>File:<b>", dtaccn.Rows[indx][0]), "</b>, "), dtaccn.Rows[indx][1]), "</li>"));
                        cntt = cntt + "</ul>";
                        txt.InnerHtml = cntt;
                        this.dvattachlists.Controls.Add(txt);
                    }
                }
                catch (Exception ex)
                {

                }
                */
                
                try
                {
                    gCla.TrOpen();
                    var CopyData = gCla.DataT("select accessionnumber,booktitle,bookprice,Copynumber,vendor_source,pubYear,biilNo,REPLACE(CONVERT(nvarchar,billDate,106),' ','-')billdate,Location from bookaccessionmaster b left join TempLocation t on t.LocaId=b.Loc_id  where ctrl_no=(select ctrl_no from bookaccessionmaster where accessionnumber='" + this.txtAccNo.Text.Trim() + "') order by Copynumber");
                    // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && txtAccNo.Text.Trim() && "'")
                    // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='Select',userid='" && Session("user_id") && "' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                    gCla.TrClose();
                    if (CopyData.Rows.Count > 0)
                    {
                        this.LnkBookCopy.Text = CopyData.Rows.Count.ToString();
                        this.GrdCopyAcc.DataSource = CopyData;
                        this.GrdCopyAcc.DataBind();
                    }

                    gCla.TrOpen();
                    string qery;
                    if (this.labCorrect.Visible == true)
                    {
                        qery = "select * from AccnQC  where accnno='" + this.txtAccNo.Text + "'";
                        var dtqc = gCla.DataT(qery);
                        if (dtqc.Rows.Count > 0)
                        {
                            if (dtqc.Rows[0]["correct"].ToString().ToLower()== "y")
                            {
                                this.labCorrect.Text = "QC Correct";
                            }
                            else
                            {
                                this.labCorrect.Text = "QC Incorrect";

                            }
                        }

                        else
                        {
                            this.labCorrect.Text = "";

                        }

                    }

                    var gclas = new GlobClassTr();

                    gclas.TrOpen();
                    qery = "  select ISNULL(max(TransNo),0)+1 from AuditTriggerMaster  ";
                    string transno = gclas.ExScaler(qery).ToString();
                    qery = "  select ISNULL(max(id),0)+1 from AuditTriggerMaster  ";
                    string id = gclas.ExScaler(qery).ToString();
                    qery = "  select ISNULL(max(id),0)+1 from AuditTriggerchild  ";
                    string idchild = gclas.ExScaler(qery).ToString();
                    // 'hdTransNo.Value = gCla.ExScaler(qery).ToString
                    // '        qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + transno + ",'CatalogDetail', 'bookaccessionmaster','select','accessionnumber','" + txtacc.Value + "' ) "
                    qery = "  insert into AuditTriggerMaster (id,transno,AppName,TableName,Operation,ipaddress,userid) values (  " + id.ToString() + "," + transno.ToString() + ",'masscateentry', 'bookaccessionmaster','select','" + Request.UserHostAddress.ToString() + "','" + LoggedUser.Logged().User_Id + "' ) ";
                    gclas.IUD(qery);
                    // qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + transno + ",'CatalogDetail', 'bookaccessionmaster','select','userid','" + Session("user_id").ToString() + "' ) "
                    qery = "  insert into AuditTriggerChild (id,transno,masterid,columnname,valuebefore ) values ( " + idchild.ToString() + "," + transno.ToString() + "," + id.ToString() + ",'accessionnumber','" + this.txtAccNo.Text + "')";
                    gclas.IUD(qery);
                    gclas.TrClose();
                }

                catch (Exception ex)
                {
                    gCla.TrRollBack();

                }
                
                                var audcatg = new UpdCatalog();
                                lsaudaccn.Add(audaccn);
                                audcatg.lsAccnb4 = lsaudaccn;
                                lsaudbookcatg.Add(audbookcatg);
                                audcatg.lsCatloagb4 = lsaudbookcatg;
                                lsaudcatdata.Add(audcatdata);
                                audcatg.lscatagdatab4 = lsaudcatdata;
                                lsaudbookauth.Add(audbookauth);
                                audcatg.lsauthb4 = lsaudbookauth;
                                lsaudconf.Add(audconf);
                                audcatg.lsbookconf4 = lsaudconf;

                                Session["auditdata"] = audcatg;
                
                this.updMain.Update();
                try
                {
                    gCla.TrOpen();
                    var CopyData = gCla.DataT("select accessionnumber,booktitle,bookprice,Copynumber,vendor_source,pubYear,biilNo,REPLACE(CONVERT(nvarchar,billDate,106),' ','-')billdate,Location from bookaccessionmaster b left join TempLocation t on t.LocaId=b.Loc_id  where ctrl_no=(select ctrl_no from bookaccessionmaster where accessionnumber='" + this.txtAccNo.Text.Trim() + "') order by Copynumber");
                    // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && txtAccNo.Text.Trim() && "'")
                    // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='Select',userid='" && Session("user_id") && "' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                    gCla.TrClose();
                    if (CopyData.Rows.Count > 0)
                    {
                        this.LnkBookCopy.Text = CopyData.Rows.Count.ToString();
                        this.GrdCopyAcc.DataSource = CopyData;
                        this.GrdCopyAcc.DataBind();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                // lblMsg.Text = ex.Message
                // lblMsg.Value = ex.Message
                // lblMsg.Visible = True
                //                this.UPMsg.Update();
                // LibObj.MsgBox1("Couldn't retrieve Catalog data: " && ex.Message, Me)
                message.PageMesg("Couldn't retrieve Catalog data: " + ex.Message, this, dbUtilities.MsgLevel.Failure);

                return;
            }


            // LibObj.MsgBox1(hdnAccNo.Value, Me)
            // txtTitle.Text = val(1)
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        protected void TitleSelected(object sender, EventArgs e)
        {
            // MsgBox(hdnBookId.Value, vbSystemModal + MsgBoxStyle.Information, Me.ToString)

            // 
            // *** being changed
            // "title","author","pages","volume","part","edition","classno","bookno","isbn","issn","lang","subjects","price","currency","publisher","pubcity","editionyr","pubyear"#"
            // 
            // 
            // 
            // 





            string[] BookStr = this.hdnBookId.Value.Split('|');
            if (!string.IsNullOrEmpty(BookStr[1]))
            {
                this.txtAccNo.Text = BookStr[1].ToString();
                this.hdnAccNo.Value = BookStr[1].ToString();
                AccNoSelected(sender, e);
                return;
            }
            // LibObj.MsgBox1(hdnBookId.Value, Me)
            Reset();
            var dsMssplB = new DataSet();
            var objPV = new FillDsTables();

            string MssplB;
            // QPublVend = "select publisherid,firstname,percity from publishermaster a, addresstable b where a.publisherid=b.addid and addrelation='publisher' and firstname='" && BookStr(17).Replace("'", "''") && "' and percity='" && BookStr(18).Replace("'", "''") && "';"
            MssplB = " select * from mssplbooks.dbo.bookcollection where bookid=" + BookStr[2];
            string Err = objPV.FillDs(MssplB, ref dsMssplB, "P");
            if (!string.IsNullOrEmpty(Err))
            {
                // LibObj.MsgBox1(Err, Me)
                message.PageMesg(Err, this, dbUtilities.MsgLevel.Warning);
                return;
            }

            this.txt_TestSuggoftite.Text = dsMssplB.Tables[0].Rows[0]["title"].ToString(); // BookStr(2)
            string Fname = "";
            string Mname = "";
            string Lname = "";
            string Auth = dsMssplB.Tables[0].Rows[0]["author"].ToString();
            Auth = Auth.ToString();
            // Auth = "X    " && Auth
            Auth = Auth.ToString().Replace(".", " ");
            Auth = Regex.Replace(Auth, @"\s+", " ");
            int ii;
            int kk;
            if (Auth.ToString().Contains(" "))
            {
                var loopTo = Auth.ToString().Length;
                for (ii = 0; ii <= loopTo; ii++)
                {
                    if (Auth[ii] != ' ')
                    {
                        Fname += Auth[ii];
                    }
                    else
                    {
                        break;
                    }
                }
                for (kk = (Auth).Length - 1; kk >= 0; kk -= 1)
                {
                    if (Auth[kk] != ' ')
                    {
                        Lname += Auth[kk];
                    }
                    else
                    {
                        break;
                    }
                }
                Lname = Reverse(Lname);
                kk -= 1;
                var loopTo1 = kk;
                for (ii = ii; ii <= loopTo1; ii++)
                    Mname += Auth[ii];
            }
            else
            {
                Fname = Auth;
            }

            this.txtAuthF1.Text = Fname;
            this.txtAuthM1.Text = Mname;
            this.txtAuthL1.Text = Lname;
            this.txtCopyNo.Text = 1.ToString();

            this.txtPages.Text = dsMssplB.Tables[0].Rows[0]["pages"].ToString();
            this.txtVolume.Text = dsMssplB.Tables[0].Rows[0]["volume"].ToString();
            this.txtPart.Text = dsMssplB.Tables[0].Rows[0]["part"].ToString();
            this.txtEdition.Text = dsMssplB.Tables[0].Rows[0]["edition"].ToString();
            this.txtClassNo.Text = dsMssplB.Tables[0].Rows[0]["classno"].ToString();
            this.txtBookNo.Text = dsMssplB.Tables[0].Rows[0]["bookno"].ToString();
            this.txtISBN.Text = dsMssplB.Tables[0].Rows[0]["isbn"].ToString();
            this.txtISSN.Text = dsMssplB.Tables[0].Rows[0]["issn"].ToString();
            foreach (ListItem L in this.ddlLang.Items)
            {
                if (L.Text==dsMssplB.Tables[0].Rows[0]["lang"].ToString())
                {
                    this.ddlLang.SelectedValue = L.Value;
                    break;
                }
            }
            var dsPublVend = new DataSet();
            this.txtSub1.Text = dsMssplB.Tables[0].Rows[0]["subjects"].ToString();
            this.txtPrice.Text = dsMssplB.Tables[0].Rows[0]["price"].ToString();
            foreach (ListItem C in this.ddlCurr.Items)
            {
                if (C.Text== dsMssplB.Tables[0].Rows[0]["currency"].ToString())
                {
                    this.ddlCurr.SelectedValue = C.Value;
                }
            }
            if (!string.IsNullOrEmpty(BookStr[1]))
            {

            }
            string QPublVend = "select publisherid,firstname,percity from publishermaster a, addresstable b where a.publisherid=b.addid and addrelation='publisher' ";
            QPublVend = QPublVend + " and firstname='"+ dsMssplB.Tables[0].Rows[0]["publisher"].ToString().Replace("'", "''")+ "' and percity='"+ dsMssplB.Tables[0].Rows[0]["pubcity"].ToString().Replace("'", "''")+"';";

            Err = objPV.FillDs(QPublVend, ref dsMssplB, "Publ");
            if (!string.IsNullOrEmpty(Err))
            {
                // lblMsg.Text = Err
                message.PageMesg(Err, this, dbUtilities.MsgLevel.Warning);

                // MsgBox(Err, vbSystemModal + MsgBoxStyle.Critical, Me.ToString)
                return;
            }


            if (dsMssplB.Tables["Publ"].Rows.Count > 0)
            {
                this.hdnPublId.Value = dsMssplB.Tables["Publ"].Rows[0]["publisherid"].ToString();
            }
            else
            {
                this.hdnPublId.Value = (-1).ToString();
            } // checked  on save
            this.txtPubl.Text = dsMssplB.Tables[0].Rows[0]["publisher"].ToString();
            this.txtPubl.Text = this.txtPubl.Text + ", "+ dsMssplB.Tables[0].Rows[0]["pubcity"].ToString();
            if (dsMssplB.Tables["Publ"].Rows.Count == 0)
            {
                this.lblPubl.Text = this.txtPubl.Text;
                Session["Publ"] = dsMssplB.Tables[0].Rows[0]["publisher"].ToString();
                Session["PublCity"] = dsMssplB.Tables[0].Rows[0]["pubcity"].ToString();
                this.MPEPubl.Show();
            }
            else
            {
                Session["Publ"] = null;  // stored proc receives publisherid by hidden field
                Session["PublCity"] = null;
            }
            // **************
            // same way manage vendor by inserting values!!!!
            // *****************
            this.txtEditionYear.Text = dsMssplB.Tables[0].Rows[0]["editionyr"].ToString();
            this.txtPubYear.Text = dsMssplB.Tables[0].Rows[0]["pubyear"].ToString();
            this.hdnInsUpd.Value = "U";
            var dsBCPart = new DataSet();
            if (!string.IsNullOrEmpty(this.txtAccNo.Text))
            {
                Err = objPV.FillDs("select part from bookaccessionmaster a, bookcatalog b where a.ctrl_no=b.ctrl_no and accessionnumber='" + this.txtAccNo.Text + "'", ref dsBCPart, "part");
                if (!string.IsNullOrEmpty(Err))
                {
                    // LibObj.MsgBox1(Err, Me)
                    message.PageMesg(Err, this, dbUtilities.MsgLevel.Failure);

                    return;
                }
                // txtPart.Text = IIf(IsDBNull(dsBCPart.Tables(0).Rows(0)("part")), "", dsBCPart.Tables(0).Rows(0)("part"))
            }
            this.hdnCtrl_no.Value = 0.ToString();
            this.txtPart.Text = dsMssplB.Tables[0].Rows[0]["part"].ToString();

            if (string.IsNullOrEmpty(this.txtAccNo.Text))
            {
                this.btnSave.Text = "Save";
            }
            else
            {
                this.btnSave.Text = "Update";
            }
            if (!string.IsNullOrEmpty(this.txtAccNo.Text))
            {
                this.btnAddCopy.Enabled = true;
                this.btnAddV.Enabled = true;
            }
            // txtAccNo.Focus()


        }
        [WebMethod(EnableSession = true)]
        public static string[] GetDataForTitle(string Prefix)
        {
            // Dim dbi As New MultipleFrameworks.DBIStructure
            // Dim dt As DataTable = dbi.GetDataTable("select top 100 title from BookCollection where title like '%" && Prefix.Replace("'", "") && "%'", dbi.GetConnectionString("MSSPLConnectionString"))
            // Dim returnData(dt.Rows.Count - 1) As String
            // Dim val As String = String.Empty
            // If dt.Rows.Count > 0 Then
            // For i As Integer = 0 To dt.Rows.Count - 1
            // val = dt.Rows(i)("title").ToString
            // returnData.SetValue(val, i)
            // Next
            // End If
            // '   Return returnData
            int jj = 0;
            jj = 0;
            // Dim RetArray As New List(Of String)()
            // Dim RetErr = New Exception
            int rblDBOption;
            rblDBOption = Convert.ToInt32(HttpContext.Current.Session["rblDBOption"]);

            string ConString = retConstr("");
            // This connection is for MSSPL Book Collection Data
            string ConStringMSSPL = ConfigurationManager.ConnectionStrings["MSSPLConnectionString"].ConnectionString;
            var Connektion = new OleDbConnection(ConString);
            var ConnektionMSSPL = new OleDbConnection(ConStringMSSPL);
            // ConnektionMSSPL.ConnectionTimeout = 2000
            OleDbCommand Comm;
            OleDbCommand Comm2;
            string ErrAccno;
            int ErrCnt;
            var RetStrErr = new string[2];
            try
            {

                Connektion.Open();
                Prefix = Prefix.Trim();
                var ds = new DataSet();
                var DSK = new DataSet();
                string qry_str;


                string SQry;
                if (rblDBOption == 0)
                {
                    SQry = "select   top 300 cast(accessionnumber as varchar) accessionnumber, substring(booktitle,1,165) title from bookaccessionmaser ";
                    SQry += " where booktitle like N'%" + Prefix + "%' order by booktitle,cast(accessionnumber as varchar)";
                    Comm2 = new OleDbCommand(SQry);
                    Comm2.Connection = Connektion;
                    Comm2.CommandType = CommandType.Text;
                    var da2 = new OleDbDataAdapter(SQry, Connektion);
                    da2.Fill(DSK, "Books");
                }
                if (rblDBOption == 1)
                {
                    SQry = "select  distinct top 250 cast(accessionnumber as varchar) accessionnumber,   substring(booktitle,1,165) title from bookaccessionmaster ";
                    SQry += " where booktitle like N'%" + Prefix + "%' order by title,cast(accessionnumber as varchar) ";

                    Comm2 = new OleDbCommand(SQry);
                    Comm2.Connection = Connektion;
                    Comm2.CommandType = CommandType.Text;
                    var drLoc = Comm2.ExecuteReader();
                    var da2 = new OleDbDataAdapter(SQry, Connektion);
                    foreach (DataTable tn in DSK.Tables)
                    {
                        if (tn.TableName == "Books")
                        {
                            DSK.Tables.Remove(tn);
                        }
                    }
                    da2.Fill(DSK, "Books");
                    string[] LocConn = ConString.Split('=');
                    string[] LocConn2 = LocConn[5].Split(';');
                    qry_str = "select top 250 'MSSPL:'+cast(bookid as varchar) accessionnumber, title FROM BookCollection ";
                    qry_str += " Where title like N'%" + Prefix + "%'  ";
                    qry_str += " order by title asc";
                    try
                    {
                        ConnektionMSSPL.Open();
                        Comm = new OleDbCommand(qry_str);
                        Comm.Connection = ConnektionMSSPL;
                        Comm.CommandType = CommandType.Text;
                        var drMSSPL = Comm.ExecuteReader();
                        var da = new OleDbDataAdapter(qry_str, ConnektionMSSPL);
                        da.Fill(DSK, "BooksMSSPL");
                        if (rblDBOption == 1)
                        {
                            DSK.Tables["Books"].Merge(DSK.Tables["BooksMSSPL"]);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                var dvK = new DataView();
                foreach (DataTable tn in DSK.Tables)
                {
                    if (tn.TableName == "Books")
                    {
                        dvK = DSK.Tables["Books"].DefaultView;
                        dvK.Sort = "title asc,accessionnumber ";

                        int kk = DSK.Tables["Books"].Rows.Count;
                        int kk2 = dvK.Count;
                    }
                }
                ErrCnt = 0;
                string ssTitle2 = "";
                string ssAccno2 = "";
                int ii = 0;
                var RetStr = new string[DSK.Tables["Books"].Rows.Count];
                foreach (DataRowView dv in dvK)
                {
                    string ssAccno = dv["accessionnumber"].ToString();

                    string ssBookTitle = dv["title"].ToString();
                    switch (rblDBOption)
                    {
                        case 0:
                            {
                                // RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ssBookTitle + "|" + ssAccno, ssBookTitle + "," + ssAccno))
                                RetStr.SetValue(ssBookTitle.Trim() + "(" + ssAccno.Trim() + ")", ii);
                                ii += 1;
                                break;
                            }
                        case 1:
                            {
                                if ((ssTitle2 ?? "") == (ssBookTitle ?? "") && (ssAccno2 ?? "") == (ssAccno ?? ""))
                                {
                                    ii = ii;
                                }
                                else
                                {
                                    // RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ssBookTitle + "(" + ssAccno + ")", ssBookTitle + "," + ssAccno))
                                    RetStr.SetValue(ssBookTitle.Trim() + "(" + ssAccno.Trim() + ")", ii);
                                    ii += 1;
                                }

                                break;
                            }
                    }

                    ssTitle2 = ssBookTitle;
                    ssAccno2 = ssAccno;
                }

                Connektion.Close();
                ConnektionMSSPL.Close();

                for (int i = 0, loopTo = RetStr.Length - 1; i <= loopTo; i++)
                {
                    if (RetStr[i] is null)
                    {
                        RetStr[i] = string.Empty;
                    }
                }

                return RetStr;
            }
            // If RetArray.Count > 0 Then
            // Return RetArray.ToArray
            // Else
            // Return Nothing
            // End If


            catch (Exception ex)
            {
                Connektion.Close();
                ConnektionMSSPL.Close();
                // RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion:" + ex.Message + "(" + ErrAccno + ":" + ErrCnt.ToString + ")", ex.Message))
                // Return RetArray.ToArray
                RetStrErr.SetValue(ex.Message, 0);
                return RetStrErr;
            }














        }
        [WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetPublisher(string prefixText, int count)
        {
            // Dim _RetArray() As String = Nothing '  
            var RetArray = new List<string>();    // Notice: list of string is used where .add can be used to add string
            var GetDDL = new FillDsTables();
            try
            {

                var dsPubl = new DataSet();

                prefixText = prefixText.Trim();
                string Err; // = GetDDL.FillDs("select publisherid,firstname,percity  from publishermaster a,addresstable b where a.publisherid=b.addid and addrelation='publisher' and firstname like '%" & prefixText.Trim().Replace("'", "''") & "%' order by firstname", dsPubl, "Publ")

                if (prefixText.Contains(","))
                {
                    if (prefixText.Contains(", "))
                    {
                        Err = GetDDL.FillDs("select top 250 publisherid,firstname,percity  from publishermaster a,addresstable b where a.publisherid=b.addid and addrelation='publisher' and firstname+', '+percity like N'%" + prefixText.Trim().Replace("'", "''") + "%' order by firstname", ref dsPubl, "Publ");
                    }
                    // Err = GetDDL.FillDs("select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname+', '+percity like '%" & prefixText.Replace("'", "''") & "%' order by vendorname", dsVend, "Vend")
                    else
                    {
                        Err = GetDDL.FillDs("select top 250 publisherid,firstname,percity  from publishermaster a,addresstable b where a.publisherid=b.addid and addrelation='publisher' and firstname+','+percity like N'%" + prefixText.Trim().Replace("'", "''") + "%' order by firstname", ref dsPubl, "Publ");
                        // Err = GetDDL.FillDs("select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname+','+percity like '%" & prefixText.Replace("'", "''") & "%' order by vendorname", dsVend, "Vend")
                    }
                }
                else
                {
                    Err = GetDDL.FillDs("select top 250 publisherid,firstname,percity  from publishermaster a,addresstable b where a.publisherid=b.addid and addrelation='publisher' and firstname+percity like N'%" + prefixText.Trim().Replace("'", "''") + "%' order by firstname", ref dsPubl, "Publ");
                    // Err = GetDDL.FillDs("select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname+percity like '%" & prefixText.Replace("'", "''") & "%' order by vendorname", dsVend, "Vend")
                }

                if (!string.IsNullOrEmpty(Err))
                {
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + Err, 0.ToString()));
                    return RetArray.ToArray();
                }
                for (int ii = 0, loopTo = dsPubl.Tables[0].Rows.Count - 1; ii <= loopTo; ii++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dsPubl.Tables[0].Rows[ii][1].ToString()    +", " + dsPubl.Tables[0].Rows[ii][2].ToString(), dsPubl.Tables[0].Rows[ii][0].ToString()));
                return RetArray.ToArray();
            }
            catch (Exception ex)
            {
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion", 0.ToString()));
                return RetArray.ToArray();
            }
        }

        private void resetCpRegion()
        {
            this.txCpPrice.Text = "";
            this.txtAccNoCopy.Text = "";
            this.txBillNoCP.Text = "";
            this.txBillDtCP.Text = "";
            // ddlDeptCP.Items.FindByText("NA").Selected = True
            // ddlCategCP.Items.FindByText("None").Selected = True

            for (int ix = 0, loopTo = this.ddlCategCP.Items.Count - 1; ix <= loopTo; ix++)
            {
                this.ddlCategCP.Items[ix].Selected = false;
                if (this.ddlCategCP.Items[ix].Value.ToLower() == "none")
                {
                    this.ddlCategCP.Items[ix].Selected = true;
                    break;
                }
            }
            for (int ix = 0, loopTo1 = this.ddlDeptCP.Items.Count - 1; ix <= loopTo1; ix++)
            {
                this.ddlDeptCP.Items[ix].Selected = false;
                if (this.ddlDeptCP.Items[ix].Value.ToLower() == "none" | this.ddlDeptCP.Items[ix].Value.ToLower() == "na")
                {
                    this.ddlDeptCP.Items[ix].Selected = true;
                    break;
                }
            }
            this.hdLocidCP.Value = "";
            this.txLocationCP.Text = "";
            this.txCpBookno.Text = "";
            this.txVendorCP.Text = "";
        }

        private void Reset()
        {
            Session["auditdata"] = null;
            this.hdTransNo.Value = "";
            this.labCorrect.Text = "";
            resetCpRegion();
            this.hdnBookId.Value = "";
            this.txtLocation.Text = "";
            this.txtLoc2.Text = "0";
            this.txSearchText.Text = "";
            this.ddlCat.SelectedIndex = 0;
            this.ddlIStat.SelectedIndex = 0;
            this.ddlIType.SelectedIndex = 0;
            this.ddlLang.SelectedIndex = 0;
            this.ddlDept.SelectedIndex = 0;
            this.dvattachments.Style.Add("display", "none");

            this.txtVAcc.Text = "";
            this.txtVAcc.Enabled = false;
            this.txtVolume2.Text = "";
            this.txtVolume2.Enabled = false;
            this.btnAddV.Enabled = true;
            this.txtBillNo.Text = "";
            this.txtBillDt.Text = "";
            this.txtVAcc.Text = "";
            this.TxtSetOffVol.Text = "";
            this.txtVAcc.Enabled = false;
            // txtVol.Enabled = False
            // txtVol.Text = ""
            this.btnAddV.Enabled = false;
            this.hdnInsUpd.Value = "I";
            this.hdnPublId.Value = (-1).ToString();
            this.hdnCtrl_no.Value = 0.ToString();
       //     this.lblMsg.Visible = false;
       //     this.lblMsg.Text = "";
            this.btnCopyMore.Enabled = false;

            // txtVend.Text = "NA, NA"
            this.txtVend.Text = "";
            Session["ctrl_no"] = null;
            Session["AccNo"] = null;
            this.txtAccNoCopy.Enabled = false;
            this.txtCopyNo.Enabled = false;
            this.txtAccNo.Text = "";
            this.btnDel.Enabled = false;
            this.btnDel.Text = "Delete";
            this.txtAccNoCopy.Text = "";
            this.txtAuthF1.Text = "";
            this.txtAuthF2.Text = "";
            this.txtAuthF3.Text = "";
            this.txtAuthL1.Text = "";
            this.txtAuthL2.Text = "";
            this.txtAuthL3.Text = "";
            this.txtAuthM1.Text = "";
            this.txtAuthM2.Text = "";
            this.txtAuthM3.Text = "";
            this.txtBookNo.Text = "";
            this.txtClassNo.Text = "";
            this.txtPart.Text = "";
            // txtBPages.Text = ""
            this.txtCopyNo.Text = "";
            this.txtEdition.Text = "";
            this.txtEditionYear.Text = "";
            // txtInitPg.Text = ""
            this.txtISBN.Text = "";
            this.txtISSN.Text = "";
            // txtParts.Text = ""
            this.txtPages.Text = "";
            this.txtPrice.Text = "";
            this.txtPubl.Text = "";
            this.txtPubYear.Text = "";
            this.txtSTitle.Text = "";
            this.txtSub1.Text = "";
            this.txtSub2.Text = "";
            this.txtSub3.Text = "";
            // txtVend.Text = ""
            this.txt_TestSuggoftite.Text = "";
            // txtVolNo.Text = ""
            this.txtVolume.Text = "";
            this.txtCopyNo.Text = "1";
            // Extended area
            this.txtEdF1.Text = "";
            this.txtEdM1.Text = "";
            this.txtEdL1.Text = "";
            this.txtCompF1.Text = "";
            this.txtCompM1.Text = "";
            this.txtCompL1.Text = "";
            this.txtIlF1.Text = "";
            this.txtIlM1.Text = "";
            this.txtIlL1.Text = "";
            this.txtTranF1.Text = "";
            this.txtTranM1.Text = "";
            this.txtTranL1.Text = "";
            this.txtEdF2.Text = "";
            this.txtEdM2.Text = "";
            this.txtEdL2.Text = "";
            this.txtCompF2.Text = "";
            this.txtCompM2.Text = "";
            this.txtCompL2.Text = "";
            this.txtIlF2.Text = "";
            this.txtIlM2.Text = "";
            this.txtIlL2.Text = "";
            this.txtTranF2.Text = "";
            this.txtTranM2.Text = "";
            this.txtTranL2.Text = "";
            this.txtEdF3.Text = "";
            this.txtEdM3.Text = "";
            this.txtEdL3.Text = "";
            this.txtCompF3.Text = "";
            this.txtCompM3.Text = "";
            this.txtCompL3.Text = "";
            this.txtIlF3.Text = "";
            this.txtIlM3.Text = "";
            this.txtIlL3.Text = "";
            this.txtTranF3.Text = "";
            this.txtTranM3.Text = "";
            this.txtTranL3.Text = "";

            this.txtDt.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            this.txtacccopydt.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            this.btnSave.Text = "Save";
            this.txtCopyNo.Enabled = false;
            this.txtAccNoCopy.Enabled = false;
            this.btnAddCopy.Enabled = false;
            this.txtacccopydt.Enabled = false;
            // txtLocation.Text = ""
            this.UPCnt.Update();
          //  this.UPMsg.Update();
            this.updMain.Update();
            this.txtAccNo.Focus();

        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {

        }

        protected void lnkTitl_Click(object sender, EventArgs e)
        {

        }

        protected void BtnDeleteCopy_Click(object sender, EventArgs e)
        {
            string accNList = "";
            string conStr = retConstr("");
            var coNN = new OleDbConnection(conStr);
            OleDbCommand coMM; // = New OleDbCommand

            var clas = new GlobClassTr();
            clas.TrOpen();
            try
            {


                for (int Df = 0, loopTo = this.GrdCopyAcc.Rows.Count - 1; Df <= loopTo; Df++)
                {
                    if ((this.GrdCopyAcc.Rows[Df].FindControl("ChkCopyDelete") as CheckBox).Checked == true)
                    {
                        HiddenField HdnAccCopy = this.GrdCopyAcc.Rows[Df].FindControl("HdnAccNo") as HiddenField;
                        accNList += HdnAccCopy.Value + "|";
                        var lsps = new List<SqlParameter>();
                        var p1 = new SqlParameter();
                        p1.ParameterName = "accessionnumber";
                        p1.DbType = (DbType)SqlDbType.VarChar;
                        p1.Value = HdnAccCopy.Value;
                        lsps.Add(p1);
                        var p2 = new SqlParameter();
                        p2.ParameterName = "AppName";
                        p2.DbType = (DbType)SqlDbType.VarChar;
                        p2.Value = "masscatentry";
                        lsps.Add(p2);
                        var p3 = new SqlParameter();
                        p3.ParameterName = "Uid";
                        p3.DbType = (DbType)SqlDbType.VarChar;
                        p3.Value = LoggedUser.Logged().User_Id;
                        lsps.Add(p3);
                        // sp_DeleteCatalog
                        var p4 = new SqlParameter();
                        p4.ParameterName = "IpAddress";
                        p4.DbType = (DbType)SqlDbType.VarChar;
                        p4.Value = Request.UserHostAddress;
                        lsps.Add(p4);

                        clas.ExProc("sp_DeleteCatalog", lsps);


                    }
                }
                clas.TrClose();
                message.PageMesg("Select accns deleted.", this, dbUtilities.MsgLevel.Success);
            }
            catch (Exception ex)
            {
                clas.TrRollBack();
                message.PageMesg("Deletion failed: " + ex.Message, this, dbUtilities.MsgLevel.Failure);

            }

            // Dim dTran As OleDbTransaction
            // Try
            // Dim uiD As String = Session("user_id").ToString
            // Dim Ipa As String = Request.UserHostAddress
            // Dim Sess As String = Session("session")

            // coNN.Open()
            // 'dTran = coNN.BeginTransaction
            // 'coMM.Transaction = dTran
            // coMM = New OleDbCommand
            // coMM.CommandText = "sp_DeleteCatalog" '??drop Temp SP and use correct [insert_DirectCatalog]
            // coMM = New OleDbCommand(coMM.CommandText, coNN)
            // coMM.CommandType = CommandType.StoredProcedure
            // coMM.CommandTimeout = 300
            // coMM.Parameters.AddWithValue("@accessionnumber", OleDbType.VarChar).Value = txtAccNo.Text.Trim
            // coMM.Parameters.AddWithValue("@AppName", OleDbType.VarChar).Value = "masscatentry"
            // coMM.Parameters.AddWithValue("@Uid", OleDbType.VarChar).Value = Session("user_id")
            // coMM.Parameters.AddWithValue("@OutParameter", OleDbType.VarChar) 'during copy addition this value comes from SP
            // coMM.Parameters("@OutParameter").Value = ""
            // coMM.Parameters("@OutParameter").Direction = ParameterDirection.InputOutput
            // coMM.Parameters("@OutParameter").Size = 500
            // coMM.ExecuteNonQuery()


            // 'coMM.CommandText = "DelCatalog"
            // 'coMM.Connection = coNN
            // 'coMM.CommandType = CommandType.StoredProcedure
            // 'coMM.Parameters.Add("@accNos", OleDbType.VarChar).Value = accNList
            // 'coMM.Parameters.Add("@Uid", OleDbType.VarChar).Value = uiD
            // 'coMM.Parameters.Add("@Ipa", OleDbType.VarChar).Value = Ipa
            // 'coMM.Parameters.Add("@Sess", OleDbType.VarChar).Value = Sess

            // 'coMM.Parameters.Add("@Res", OleDbType.VarChar).Value = ""
            // 'coMM.Parameters("@Res").Direction = ParameterDirection.InputOutput
            // 'coMM.Parameters("@Res").Size = 500
            // 'coMM.ExecuteNonQuery()
            // ' msglabel.Text = coMM.Parameters("@Res").Value
            // message.PageMesg(coMM.Parameters("@Res").Value, Me, dbUtilities.MsgLevel.Success)
            // coNN.Close()
            // Reset()
            // Catch ex As Exception
            // coNN.Close()
            // message.PageMesg(ex.Message, Me, dbUtilities.MsgLevel.Warning)
            // LibObj.MsgBox(ex.Message, Me)
            // End Try
        }

        protected void BtnInsertAllCopy_Click(object sender, EventArgs e)
        {
            int countupdate = 0;
            for (int d = 0, loopTo = this.GrdCopyAcc.Rows.Count - 1; d <= loopTo; d++)
            {
                if ((this.GrdCopyAcc.Rows[d].FindControl("ChkCopyDelete") as CheckBox).Checked == true)
                {
                    HiddenField HdnAccCopy = this.GrdCopyAcc.Rows[d].FindControl("HdnAccNo") as HiddenField;
                    TextBox txtGrdPrice = this.GrdCopyAcc.Rows[d].FindControl("txtGrdPrice") as TextBox;
                    TextBox txtGrdVendor = this.GrdCopyAcc.Rows[d].FindControl("txtGrdVendor") as TextBox;
                    TextBox txtGrdPubyear = this.GrdCopyAcc.Rows[d].FindControl("txtGrdPubyear") as TextBox;
                    TextBox txtGrdbiilNo = this.GrdCopyAcc.Rows[d].FindControl("txtGrdbiilNo") as TextBox;
                    TextBox txtGrdbilldate = this.GrdCopyAcc.Rows[d].FindControl("txtGrdbilldate") as TextBox;
                    TextBox txtGrdLocation = this.GrdCopyAcc.Rows[d].FindControl("txtGrdLocation") as TextBox;
                    string[] sv = txtGrdVendor.Text.Trim().Split(',');
                    string Vendor = "select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname = '" + sv[0].Trim() + "' and percity='" + sv[1].Trim() + "'";
                    gCla.TrOpen();
                    var vendordata = gCla.DataT(Vendor);
                    string vendaorname = "";
                    int Locid = 0;
                    if (vendordata.Rows.Count > 0)
                    {
                        vendaorname = txtGrdVendor.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtGrdLocation.Text.Trim()))
                    {
                        var lociddata = gCla.DataT("select * from TempLocation where Location='" + txtGrdLocation.Text.Trim() + "'");
                        if (lociddata.Rows.Count > 0)
                        {
                            Locid = Convert.ToInt32(lociddata.Rows[0]["LocaId"]);
                        }
                    }
                    string update = "";
                    if (!string.IsNullOrEmpty(vendaorname))
                    {
                        update = update + " vendor_source=N'" + txtGrdVendor.Text.Trim() + "'";
                    }
                    if (Locid != 0)
                    {
                        if (!string.IsNullOrEmpty(update))
                        {
                            update = update + ",Loc_id='" + Locid + "'";
                        }
                        else
                        {
                            update = update + " Loc_id='" + Locid + "'";
                        }

                    }
                    if (!string.IsNullOrEmpty(txtGrdPrice.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(update))
                        {
                            update = update + ",bookprice='" + txtGrdPrice.Text.Trim() + "'";
                        }
                        else
                        {
                            update = update + " bookprice='" + txtGrdPrice.Text.Trim() + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(txtGrdPubyear.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(update))
                        {
                            update = update + ",pubYear='" + txtGrdPubyear.Text.Trim() + "'";
                        }
                        else
                        {
                            update = update + " pubYear='" + txtGrdPubyear.Text.Trim() + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(txtGrdbiilNo.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(update))
                        {
                            update = update + ",biilNo=N'" + txtGrdbiilNo.Text.Trim() + "'";
                        }
                        else
                        {
                            update = update + " biilNo=N'" + txtGrdbiilNo.Text.Trim() + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(txtGrdbilldate.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(update))
                        {
                            update = update + ",billDate=N'" + txtGrdbilldate.Text.Trim() + "'";
                        }
                        else
                        {
                            update = update + " billDate=N'" + txtGrdbilldate.Text.Trim() + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(update))
                    {
                        string qery1 = "  select ISNULL(max(TransNo),0)+1 from AuditTriggerMaster  ";
                        string transno1 = gCla.ExScaler(qery1).ToString();
                        // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && HdnAccCopy.Value && "'")
                        // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='PreData' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                        // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && HdnAccCopy.Value && "'")
                        // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='U',userid='" && Session("user_id") && "' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                        gCla.IUD("update bookaccessionmaster set " + update + ",userid='" + LoggedUser.Logged().User_Id + "',catalogdate=getdate(),AppName='masscatentry',IpAddress='" + Request.UserHostAddress + "',TransNo='" + transno1 + "' where accessionnumber='" + HdnAccCopy.Value + "'");

                        countupdate = countupdate + 1;
                    }
                    gCla.TrClose();
                }
            }
            if (countupdate > 0)
            {
                message.PageMesg(countupdate.ToString() + " Records Update Successfully...!!!!!!", this, dbUtilities.MsgLevel.Success);
            }
        }

        protected void lblbookcopy_Click(object sender, EventArgs e)
        {

        }

        protected void LnkBookCopy_Click(object sender, EventArgs e)
        {
            try
            {
                this.MPShowCopy.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GrdCopyAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
           var cpAccn= GrdCopyAcc.SelectedDataKey.Value.ToString();
            this.hdnAccNo.Value = cpAccn;// ((GridView) GrdCopyAcc).SelectedDataKey.Value;
            AccNoSelected(sender, e);
        }

        protected void btnAccNoSel_Click(object sender, EventArgs e)
        {
            AccNoSelected(sender , e);

        }

        protected void btnTitleSel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchLoc_Click(object sender, EventArgs e)
        {

        }

        protected void BtnRfidReset_Click(object sender, EventArgs e)
        {
            try
            {
                gCla.TrOpen();
                gCla.IUD("delete RFIDLOG where ANTNUM='" + this.ddlantenna.SelectedValue + "'");
                gCla.TrClose();
            }
            catch (Exception ex)
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, ex.Message, Me)
                message.PageMesg(ex.Message, this,  dbUtilities.MsgLevel.Failure);
            }

        }

        protected void btnPBG_Click(object sender, EventArgs e)
        {

        }

        protected void btnFin_Click(object sender, EventArgs e)
        {

        }

        protected void btnFinISB_Click(object sender, EventArgs e)
        {

        }

        protected void btnResClas_Click(object sender, EventArgs e)
        {

        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                gCla.TrOpen();
                // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && txtAccNo.Text.Trim() && "'")'
                // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='Check',userid='" && Session("user_id") && "' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                string qer = "select count(*)  from AccnQC where accnno='" + this.txtAccNo.Text.Trim() + "'";
                int exi = Convert.ToInt32(gCla.ExScaler(qer));
                if (exi == 0)
                {
                    qer = "select isnull(max(id),0)+1  from AccnQC";
                    int id = Convert.ToInt32(gCla.ExScaler(qer));
                    qer = "insert into accnqc (id,accnno,Correct,userid  ) values (" + id.ToString() + ",'" + this.txtAccNo.Text.Trim() + "','Y','"+ LoggedUser.Logged().User_Id+ "') ";
                }
                else
                {
                    qer = "select id  from AccnQC where accnno='" + this.txtAccNo.Text + "'";
                    int id = Convert.ToInt32(gCla.ExScaler(qer));
                    qer = "update accnqc set correct='Y',qcdate=getdate()  where id=" + id.ToString();

                }

                gCla.IUD(qer);



                gCla.TrClose();
                message.PageMesg("Book Information is Correct...!!!!", this, dbUtilities.MsgLevel.Success);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // [insert_DirectCatalog]
            // txtAccNo.Style.Add("onload", "alert('Hi5');")
            var btnS = new Button();
            btnS = (Button)sender;
            if (btnS.ID == "btnAddCopy")
            {
                if (string.IsNullOrEmpty(this.txtacccopydt.Text.Trim()))
                {
                    // LibObj.MsgBox1("Enter valid Date for Copy Accession no.!", Me)
                    message.PageMesg("Enter valid Date for Copy Accession no.!", this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(this.txtacccopydt);
                    return;
                }
            }
            if (ValidateMass() == false && btnS.ID != "btnAddV")
            {
                // LibObj.MsgBox1("Enter valid values in Accno, Title, Publ., Vend, Auth etc.", Me)
                message.PageMesg("Enter valid values in Accno, Title, Publ., Vend, Auth etc.", this, dbUtilities.MsgLevel.Warning);
                return;
            }

            if (!string.IsNullOrEmpty((this.txtBillDt.Text))  && btnS.ID != "btnAddV")
            {
                // lblMsg.Text = "Bill Date is not in correct format."
                // lblMsg.Visible = True
                try
                {
                    var d = Convert.ToDateTime(txtBillDt.Text);
                }
                catch
                {
                    message.PageMesg("Bill Date is not in correct format.", this, dbUtilities.MsgLevel.Warning);

                    //                this.UPMsg.Update();
                    return;

                }
            }
            var Comm = new OleDbCommand();
            string ConStr = retConstr("");
            var Connekt = new OleDbConnection(ConStr);
            try
            {
                Connekt.Open();
                if (btnS.ID == "btnDel")
                {
                    Comm.CommandText = "sp_DeleteCatalog"; // ??drop Temp SP and use correct [insert_DirectCatalog]
                    Comm = new OleDbCommand(Comm.CommandText, Connekt);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.CommandTimeout = 300;
                    Comm.Parameters.AddWithValue("@accessionnumber", OleDbType.VarChar).Value = this.txtAccNo.Text.Trim();
                    Comm.Parameters.AddWithValue("@AppName", OleDbType.VarChar).Value = "masscatentry";
                    Comm.Parameters.AddWithValue("@Uid", OleDbType.VarChar).Value = LoggedUser.Logged().User_Id;// Session["user_id"];
                    Comm.Parameters.AddWithValue("@IpAddress", OleDbType.VarChar).Value = Request.UserHostAddress;
                    Comm.ExecuteNonQuery();
                    message.PageMesg("Accn Deleted.", this, dbUtilities.MsgLevel.Warning);
                    Connekt.Close();

                    return;
                }
                int tno;
                string qers = "select isnull(max(transno),0)+1 from AuditTriggerMaster ";
                Comm.CommandText = qers;
                Comm = new OleDbCommand(Comm.CommandText, Connekt);
                Comm.CommandType = CommandType.Text;
                tno = Convert.ToInt32(Comm.ExecuteScalar());

                Comm.CommandText = "insert_DirectCatalog_Express"; // ??drop Temp SP and use correct [insert_DirectCatalog]
                Comm = new OleDbCommand(Comm.CommandText, Connekt);
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.CommandTimeout = 300;
                Comm.Parameters.AddWithValue("@accessionnumber_1", OleDbType.VarChar);
                Comm.Parameters["@accessionnumber_1"].Value = (this.txtAccNo.Text);
                Comm.Parameters.AddWithValue("@accessionnumber_1_Cp", OleDbType.VarChar);
                Comm.Parameters["@accessionnumber_1_Cp"].Value = (this.txtAccNoCopy.Text);
                // 
                Comm.Parameters.AddWithValue("@cp_booknumber", OleDbType.VarChar);
                Comm.Parameters["@cp_booknumber"].Value = (this.txCpBookno.Text).ToUpper();

                Comm.Parameters.AddWithValue("@form_4", OleDbType.VarChar);
                Comm.Parameters["@form_4"].Value = (this.ddlForm.SelectedItem.ToString());
                Comm.Parameters.AddWithValue("@accessionid_5", OleDbType.Integer);
                Comm.Parameters["@accessionid_5"].Value = 0; // do not update but ??
                Comm.Parameters.AddWithValue("@accessioneddate_6", OleDbType.Date);
                Comm.Parameters["@accessioneddate_6"].Value = (btnS.ID == "btnAddCopy"? this.txtacccopydt.Text: this.txtDt.Text);
                Comm.Parameters.AddWithValue("@booktitle_7", OleDbType.VarChar);
                Comm.Parameters["@booktitle_7"].Value = (this.txt_TestSuggoftite.Text);
                Comm.Parameters.AddWithValue("@bookprice_8", OleDbType.Double);
                Comm.Parameters["@bookprice_8"].Value = string.IsNullOrEmpty(this.txtPrice.Text)? "0": this.txtPrice.Text;
                Comm.Parameters.AddWithValue("@status_9", OleDbType.Integer);
                Comm.Parameters["@status_9"].Value = this.ddlIStat.SelectedValue;
                Comm.Parameters.AddWithValue("@issuestatus_10", OleDbType.VarChar);
                string qeri = " select isIsued from ItemStatusMaster where ItemStatusID='" + this.ddlIStat.SelectedValue + "'";
                var dtFeti = new DataTable();
                var sFilti = new FillDsTables();
                sFilti.FillDs(qeri, ref dtFeti);
                if (dtFeti.Rows.Count > 0)
                {
                    Comm.Parameters["@issuestatus_10"].Value = dtFeti.Rows[0]["isIsued"].ToString();
                }
                else
                {
                    Comm.Parameters["@issuestatus_10"].Value = "Y";
                }

                Comm.Parameters.AddWithValue("@checkstatus_11", OleDbType.VarChar);
                Comm.Parameters["@checkstatus_11"].Value = "A";
                Comm.Parameters.AddWithValue("@editionyear_12", OleDbType.Integer);
                Comm.Parameters["@editionyear_12"].Value = string.IsNullOrEmpty(this.txtEditionYear.Text)? "0": txtEditionYear.Text;
                Comm.Parameters.AddWithValue("@copynumber_13", OleDbType.Integer); // during copy addition this value comes from SP
                Comm.Parameters["@copynumber_13"].Value = this.txtCopyNo.Text;
                Comm.Parameters["@copynumber_13"].Direction = ParameterDirection.InputOutput;
                Comm.Parameters["@copynumber_13"].Size = 3;
                Comm.Parameters.AddWithValue("@pubyear_14", OleDbType.Integer);
                Comm.Parameters["@pubyear_14"].Value = string.IsNullOrEmpty(this.txtPubYear.Text) ? "0" : txtPubYear.Text;
                Comm.Parameters.AddWithValue("@biilno_15", OleDbType.VarChar);
                Comm.Parameters["@biilno_15"].Value = (this.txtBillNo.Text);
                Comm.Parameters.AddWithValue("@billdate_16", OleDbType.Date);
                Comm.Parameters["@billdate_16"].Value = this.txtBillDt.Text;
                Comm.Parameters.AddWithValue("@item_type_17", OleDbType.VarChar);
                Comm.Parameters["@item_type_17"].Value = this.ddlIType.SelectedItem.ToString();
                Comm.Parameters.AddWithValue("@originalcurrency_18", OleDbType.VarChar);
                Comm.Parameters["@originalcurrency_18"].Value = this.ddlCurr.SelectedItem.ToString();
                Comm.Parameters.AddWithValue("@userid_19", OleDbType.VarChar);
                Comm.Parameters["@userid_19"].Value = LoggedUser.Logged().User_Id;//  Session["user_id"];

                Comm.Parameters.AddWithValue("@vendor_source_20", OleDbType.VarChar);  // correct it!!!!!!!!!11
                Comm.Parameters["@vendor_source_20"].Value = (this.txtVend.Text);
                Comm.Parameters.AddWithValue("@deptcode_21", OleDbType.Integer);
                Comm.Parameters["@deptcode_21"].Value = this.ddlDept.SelectedValue;
                // LibObj.MsgBox1(ddlDept.SelectedValue.ToString, Me)
                Comm.Parameters.AddWithValue("@itemcategorycode_22", OleDbType.Integer);
                Comm.Parameters["@itemcategorycode_22"].Value = this.ddlCat.SelectedValue;
                Comm.Parameters.AddWithValue("@bookstitle_23", OleDbType.VarChar);
                Comm.Parameters["@bookstitle_23"].Value = (this.txtSTitle.Text);
                Comm.Parameters.AddWithValue("@volumenumber_24", OleDbType.VarChar);
                Comm.Parameters["@volumenumber_24"].Value = ""; // Trim(txtVolume.Text) 'error here this value is not used in this form
                Comm.Parameters.AddWithValue("@part_24_2", OleDbType.Integer);
                Comm.Parameters["@part_24_2"].Value = (this.txtPart.Text);
                Comm.Parameters.AddWithValue("@initpages_25", OleDbType.VarChar);  // @part_24_2
                Comm.Parameters["@initpages_25"].Value = 0; // Trim(txtInitPg.Text)
                Comm.Parameters.AddWithValue("@parts_26", OleDbType.VarChar);
                Comm.Parameters["@parts_26"].Value = 0; // Trim(txtParts.Text)
                Comm.Parameters.AddWithValue("@publishercode_27", OleDbType.Integer);
                Comm.Parameters["@publishercode_27"].Value = this.hdnPublId.Value;
                Comm.Parameters.AddWithValue("@edition_28", OleDbType.VarChar);
                Comm.Parameters["@edition_28"].Value = (this.txtEdition.Text);
                Comm.Parameters.AddWithValue("@isbn_29", OleDbType.VarChar);
                Comm.Parameters["@isbn_29"].Value = (this.txtISBN.Text);
                Comm.Parameters.AddWithValue("@subject1_30", OleDbType.VarChar);
                Comm.Parameters["@subject1_30"].Value = (this.txtSub1.Text);
                Comm.Parameters.AddWithValue("@subject2_31", OleDbType.VarChar);
                Comm.Parameters["@subject2_31"].Value = (this.txtSub2.Text);
                Comm.Parameters.AddWithValue("@subject3_32", OleDbType.VarChar);
                Comm.Parameters["@subject3_32"].Value = (this.txtSub3.Text);
                Comm.Parameters.AddWithValue("@bibliopages_33", OleDbType.VarChar);
                Comm.Parameters["@bibliopages_33"].Value = 0; // Trim(txtBPages.Text)
                Comm.Parameters.AddWithValue("@issn_34", OleDbType.VarChar);
                Comm.Parameters["@issn_34"].Value = (this.txtISSN.Text);
                Comm.Parameters.AddWithValue("@volume_35", OleDbType.VarChar);
                Comm.Parameters["@volume_35"].Value = (this.txtVolume.Text);
                Comm.Parameters.AddWithValue("@language_36", OleDbType.Integer);
                Comm.Parameters["@language_36"].Value = this.ddlLang.SelectedValue;
                Comm.Parameters.AddWithValue("@firstname1_37", OleDbType.VarChar);
                Comm.Parameters["@firstname1_37"].Value = (this.txtAuthF1.Text);
                Comm.Parameters.AddWithValue("@middlename1_38", OleDbType.VarChar);
                Comm.Parameters["@middlename1_38"].Value = (this.txtAuthM1.Text);
                Comm.Parameters.AddWithValue("@lastname1_39", OleDbType.VarChar);
                Comm.Parameters["@lastname1_39"].Value = (this.txtAuthL1.Text);

                Comm.Parameters.AddWithValue("@firstname2_40", OleDbType.VarChar);
                Comm.Parameters["@firstname2_40"].Value = (this.txtAuthF2.Text);
                Comm.Parameters.AddWithValue("@middlename2_41", OleDbType.VarChar);
                Comm.Parameters["@middlename2_41"].Value = (this.txtAuthM2.Text);
                Comm.Parameters.AddWithValue("@lastname2_42", OleDbType.VarChar);
                Comm.Parameters["@lastname2_42"].Value = (this.txtAuthL2.Text);

                Comm.Parameters.AddWithValue("@firstname3_43", OleDbType.VarChar);
                Comm.Parameters["@firstname3_43"].Value = (this.txtAuthF3.Text);
                Comm.Parameters.AddWithValue("@middlename3_44", OleDbType.VarChar);
                Comm.Parameters["@middlename3_44"].Value = (this.txtAuthM3.Text);
                Comm.Parameters.AddWithValue("@lastname3_45", OleDbType.VarChar);
                Comm.Parameters["@lastname3_45"].Value = (this.txtAuthL3.Text);

                Comm.Parameters.AddWithValue("@classno_46", OleDbType.VarChar);
                Comm.Parameters["@classno_46"].Value = (this.txtClassNo.Text);
                Comm.Parameters.AddWithValue("@bookno_47", OleDbType.VarChar);
                Comm.Parameters["@bookno_47"].Value = (this.txtBookNo.Text);
                Comm.Parameters.AddWithValue("@pages_48", OleDbType.Integer);
                Comm.Parameters["@pages_48"].Value = (this.txtPages.Text);
                Comm.Parameters.AddWithValue("@media_type_49", OleDbType.VarChar);
                Comm.Parameters["@media_type_49"].Value = this.ddlMedia.SelectedItem.ToString();  // tostring required
                Comm.Parameters.AddWithValue("@searchtext_49_1", OleDbType.VarChar);
                Comm.Parameters["@searchtext_49_1"].Value = (this.txSearchText.Text);


                Comm.Parameters.AddWithValue("@volumenumberV_50", OleDbType.VarChar);
                Comm.Parameters["@volumenumberV_50"].Value = "";

                string AudOper = "";


                switch (btnS.ID ?? "")
                {
                    case "btnAddCopy":
                        {
                            Comm.Parameters.AddWithValue("@InsUpd48", OleDbType.VarChar);
                            Comm.Parameters["@InsUpd48"].Value = "A";   // 
                            AudOper = "A";
                            break;
                        }
                    case "btnSave":
                        {
                            if (this.btnSave.Text == "Update")
                            {
                                Comm.Parameters.AddWithValue("@InsUpd48", OleDbType.VarChar);
                                Comm.Parameters["@InsUpd48"].Value = "U";
                                Comm.Parameters["@accessioneddate_6"].Value = DateTime.Today;
                                AudOper = "U";
                            }
                            if (this.btnSave.Text == "Save")
                            {
                                Comm.Parameters.AddWithValue("@InsUpd48", OleDbType.VarChar);
                                Comm.Parameters["@InsUpd48"].Value = "I";
                                AudOper = "I";

                            }

                            break;
                        }

                    case "btnDel":
                        {
                            // abandoned - see another proc
                            Comm.Parameters.AddWithValue("@InsUpd48", OleDbType.VarChar);
                            Comm.Parameters["@InsUpd48"].Value = "D";
                            AudOper = "D";
                            break;
                        }

                    case "btnAddV":
                        {
                            if (string.IsNullOrEmpty((this.txtVAcc.Text)) | string.IsNullOrEmpty((this.txtVolume2.Text)))
                            {
                                throw new ApplicationException("Vol Accession Number or Volume is empty.");
                            }
                            Comm.Parameters["@accessionnumber_1_Cp"].Value = (this.txtVAcc.Text);  // Trim(txtVol.Text)
                            Comm.Parameters["@volumenumberV_50"].Value = this.txtVolume2.Text;

                            Comm.Parameters.AddWithValue("@InsUpd48", OleDbType.VarChar);

                            Comm.Parameters["@InsUpd48"].Value = "V";
                            break;
                        }

                }

                // If btnAddCopy.Enabled = True Then
                // Comm.Parameters.AddWithValue("@InsUpd48", OleDbType.VarChar)
                // Comm.Parameters("@InsUpd48").Value = "A"   '??????????????????Not completed yet - error on execution - DUEEEEEE
                // End If
                // If Session("Publ") Is Nothing Then
                // LibObj.MsgBox1("Publisher error in session variable. Login again!", Me)
                // Connekt.Close()
                // Exit Sub
                // End If
                // If Session("PublCity") Is Nothing Then
                // LibObj.MsgBox1("Publisher error in session variable. Login again.", Me)
                // Connekt.Close()
                // Exit Sub
                // End If
                Comm.Parameters.AddWithValue("@firstname_49", OleDbType.VarChar);
                string PubCity = "";
                string Publ;
                int ii;
                int jj;
                this.txtPubl.Text = (this.txtPubl.Text);
                for (ii = txtPubl.Text.Length - 1; ii >= 0; ii -= 1)
                {
                    if (this.txtPubl.Text[ii] == ',')
                    {
                        break;
                    }
                    PubCity += this.txtPubl.Text[ii].ToString();
                }
                PubCity = Reverse(PubCity);
                Publ = this.txtPubl.Text.Substring(0, ii);

                Comm.Parameters["@firstname_49"].Value = Publ;
                Comm.Parameters.AddWithValue("@PubCity_50", OleDbType.VarChar);
                Comm.Parameters["@PubCity_50"].Value = PubCity;

                if (string.IsNullOrEmpty(this.txtLocation.Text.Trim()))
                {
                    this.txtLoc2.Text = "0";
                }
                Comm.Parameters.AddWithValue("@Location_51", OleDbType.Integer);
                Comm.Parameters["@Location_51"].Value = (this.txtLoc2.Text=="" ? "0" : this.txtLoc2.Text);

                // following three lines are ok!!!
                Comm.Parameters.AddWithValue("@Result55", OleDbType.VarChar);
                Comm.Parameters["@Result55"].Value = "";
                Comm.Parameters["@Result55"].Direction = ParameterDirection.InputOutput;
                Comm.Parameters["@Result55"].Size = 5000;

                Comm.Parameters.AddWithValue("@OCtrl_no", OleDbType.Integer);
                Comm.Parameters["@OCtrl_no"].Value = 0;
                Comm.Parameters["@OCtrl_no"].Direction = ParameterDirection.InputOutput;
                Comm.Parameters["@OCtrl_no"].Size = 50;
                string ipa = "";
                string Sess = "";
                string Audi = LoggedUser.Logged().IsAudit;//  Conversions.ToString(Application["Audit"]);
                string Tit = "";
                // If Audi = "Y" Then
                ipa = Request.UserHostAddress;
                Tit = Request.QueryString["title"];
                Sess = LoggedUser.Logged().Session;//  Conversions.ToString(Session["session"]);
                // End If
                Comm.Parameters.AddWithValue("@Audi_101", OleDbType.VarChar);
                Comm.Parameters["@Audi_101"].Value = Audi;
                Comm.Parameters.AddWithValue("@Sess_102", OleDbType.VarChar);
                Comm.Parameters["@Sess_102"].Value = Sess;
                Comm.Parameters.AddWithValue("@IPA_103", OleDbType.VarChar);
                Comm.Parameters["@IPA_103"].Value = ipa;
                Comm.Parameters.AddWithValue("@Title_104", OleDbType.VarChar);
                Comm.Parameters["@Title_104"].Value = Tit;
                // Author extended part added
                Comm.Parameters.AddWithValue("@EdF1", OleDbType.VarChar);
                Comm.Parameters["@EdF1"].Value = (this.txtEdF1.Text);
                Comm.Parameters.AddWithValue("@EdM1", OleDbType.VarChar);
                Comm.Parameters["@EdM1"].Value = (this.txtEdM1.Text);
                Comm.Parameters.AddWithValue("@EdL1", OleDbType.VarChar);
                Comm.Parameters["@EdL1"].Value = (this.txtEdL1.Text);
                Comm.Parameters.AddWithValue("@EdF2", OleDbType.VarChar);
                Comm.Parameters["@EdF2"].Value = (this.txtEdF2.Text);
                Comm.Parameters.AddWithValue("@EdM2", OleDbType.VarChar);
                Comm.Parameters["@EdM2"].Value = (this.txtEdM2.Text);
                Comm.Parameters.AddWithValue("@EdL2", OleDbType.VarChar);
                Comm.Parameters["@EdL2"].Value = (this.txtEdL2.Text);
                Comm.Parameters.AddWithValue("@EdF3", OleDbType.VarChar);
                Comm.Parameters["@EdF3"].Value = (this.txtEdF3.Text);
                Comm.Parameters.AddWithValue("@EdM3", OleDbType.VarChar);
                Comm.Parameters["@EdM3"].Value = (this.txtEdM3.Text);
                Comm.Parameters.AddWithValue("@EdL3", OleDbType.VarChar);
                Comm.Parameters["@EdL3"].Value = (this.txtEdL3.Text);

                Comm.Parameters.AddWithValue("@CompF1", OleDbType.VarChar);
                Comm.Parameters["@CompF1"].Value = (this.txtCompF1.Text);
                Comm.Parameters.AddWithValue("@CompM1", OleDbType.VarChar);
                Comm.Parameters["@CompM1"].Value = (this.txtCompM1.Text);
                Comm.Parameters.AddWithValue("@CompL1", OleDbType.VarChar);
                Comm.Parameters["@CompL1"].Value = (this.txtCompL1.Text);
                Comm.Parameters.AddWithValue("@CompF2", OleDbType.VarChar);
                Comm.Parameters["@CompF2"].Value = (this.txtCompF2.Text);
                Comm.Parameters.AddWithValue("@CompM2", OleDbType.VarChar);
                Comm.Parameters["@CompM2"].Value = (this.txtCompM2.Text);
                Comm.Parameters.AddWithValue("@CompL2", OleDbType.VarChar);
                Comm.Parameters["@CompL2"].Value = this.txtCompL2.Text;
                Comm.Parameters.AddWithValue("@CompF3", OleDbType.VarChar);
                Comm.Parameters["@CompF3"].Value = this.txtCompF3.Text;
                Comm.Parameters.AddWithValue("@CompM3", OleDbType.VarChar);
                Comm.Parameters["@CompM3"].Value = this.txtCompM3.Text;
                Comm.Parameters.AddWithValue("@CompL3", OleDbType.VarChar);
                Comm.Parameters["@CompL3"].Value = (this.txtCompL3.Text);

                Comm.Parameters.AddWithValue("@IlF1", OleDbType.VarChar);
                Comm.Parameters["@IlF1"].Value = (this.txtIlF1.Text);
                Comm.Parameters.AddWithValue("@IlM1", OleDbType.VarChar);
                Comm.Parameters["@IlM1"].Value = (this.txtIlM1.Text);
                Comm.Parameters.AddWithValue("@IlL1", OleDbType.VarChar);
                Comm.Parameters["@IlL1"].Value = (this.txtIlL1.Text);
                Comm.Parameters.AddWithValue("@IlF2", OleDbType.VarChar);
                Comm.Parameters["@IlF2"].Value = (this.txtIlF2.Text);
                Comm.Parameters.AddWithValue("@IlM2", OleDbType.VarChar);
                Comm.Parameters["@IlM2"].Value = (this.txtIlM2.Text);
                Comm.Parameters.AddWithValue("@IlL2", OleDbType.VarChar);
                Comm.Parameters["@IlL2"].Value = (this.txtIlL2.Text);
                Comm.Parameters.AddWithValue("@IlF3", OleDbType.VarChar);
                Comm.Parameters["@IlF3"].Value = (this.txtIlF3.Text);
                Comm.Parameters.AddWithValue("@IlM3", OleDbType.VarChar);
                Comm.Parameters["@IlM3"].Value = (this.txtIlM3.Text);
                Comm.Parameters.AddWithValue("@IlL3", OleDbType.VarChar);
                Comm.Parameters["@IlL3"].Value = (this.txtIlL3.Text);

                Comm.Parameters.AddWithValue("@TranF1", OleDbType.VarChar);
                Comm.Parameters["@TranF1"].Value = (this.txtTranF1.Text);
                Comm.Parameters.AddWithValue("@TranM1", OleDbType.VarChar);
                Comm.Parameters["@TranM1"].Value = (this.txtTranM1.Text);
                Comm.Parameters.AddWithValue("@TranL1", OleDbType.VarChar);
                Comm.Parameters["@TranL1"].Value = (this.txtTranL1.Text);
                Comm.Parameters.AddWithValue("@TranF2", OleDbType.VarChar);
                Comm.Parameters["@TranF2"].Value = (this.txtTranF2.Text);
                Comm.Parameters.AddWithValue("@TranM2", OleDbType.VarChar);
                Comm.Parameters["@TranM2"].Value = (this.txtTranM2.Text);
                Comm.Parameters.AddWithValue("@TranL2", OleDbType.VarChar);
                Comm.Parameters["@TranL2"].Value = (this.txtTranL2.Text);
                Comm.Parameters.AddWithValue("@TranF3", OleDbType.VarChar);
                Comm.Parameters["@TranF3"].Value = (this.txtTranF3.Text);
                Comm.Parameters.AddWithValue("@TranM3", OleDbType.VarChar);
                Comm.Parameters["@TranM3"].Value = (this.txtTranM3.Text);
                Comm.Parameters.AddWithValue("@TranL3", OleDbType.VarChar);
                Comm.Parameters["@TranL3"].Value = (this.txtTranL3.Text);
                Comm.Parameters.AddWithValue("@SetOFbooks", OleDbType.VarChar);
                Comm.Parameters["@SetOFbooks"].Value = (this.TxtSetOffVol.Text.Trim());
                Comm.Parameters.AddWithValue("@AppName", OleDbType.VarChar);
                Comm.Parameters["@AppName"].Value = "masscatentry";
                Comm.Parameters.AddWithValue("@TransNo", OleDbType.Integer);
                Comm.Parameters["@TransNo"].Value = tno;
                Comm.ExecuteNonQuery();
                // Comm.ExecuteNonQuery()
                var OutCtrl_no = default(int);
                if (btnS.ID.ToString() != "btnDel")
                {
                    OutCtrl_no = Convert.ToInt32(Comm.Parameters["@OCtrl_no"].Value);
                    Session["ctrl_no"] = Comm.Parameters["@OCtrl_no"].Value;
                }
                string resP = Comm.Parameters["@Result55"].Value.ToString();

                string qery = "select count(*) from FeaturesPer where fid=19 ";  // record marc 21
                int marcount;
                Comm = new OleDbCommand(qery, Connekt);
                marcount = Convert.ToInt32(Comm.ExecuteScalar());

                // save marc data
                if (resP.ToLower().Contains("succ") && marcount > 0)
                {
//                    var marc = new MarcAddMod();
  //                  try
    //                {
      //                  marc.MarcSave(this.txtAccNo.Text);
        //                resP = resP + "; Marc 21 data also saved.";
          //          }

            //        catch (Exception ex)
              //      {

                //        resP = resP + "; Marc 21 data Not saved:" + ex.Message;

                  //  }
                }
                Connekt.Close();

                try
                {

                    gCla.TrOpen();
                    var CopyData = gCla.DataT("select accessionnumber,booktitle,bookprice,Copynumber,vendor_source,pubYear,biilNo,REPLACE(CONVERT(nvarchar,billDate,106),' ','-')billdate,Location from bookaccessionmaster b left join TempLocation t on t.LocaId=b.Loc_id  where ctrl_no=(select ctrl_no from bookaccessionmaster where accessionnumber='" + this.txtAccNo.Text.Trim() + "') order by Copynumber");
                    // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && txtAccNo.Text.Trim() && "'")
                    // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='Select',userid='" && Session("user_id") && "' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                    gCla.TrClose();
                    if (CopyData.Rows.Count > 0)
                    {
                        this.LnkBookCopy.Text = CopyData.Rows.Count.ToString();
                        this.GrdCopyAcc.DataSource = CopyData;
                        this.GrdCopyAcc.DataBind();
                    }

                    gCla.TrOpen();
                    // 'This transno and inserted no is same 
                    // qery = "  select ISNULL(max(TransNo),0)+1 from AuditTriggerMaster  "
                    // Dim transno As String = gCla.ExScaler(qery).ToString
                    // 'hdTransNo.Value = gCla.ExScaler(qery).ToString
                    // If AudOper = "I" Then
                    // qery = " insert into AuditTriggerMaster (transno,AppName,TableName,Operation,ipaddress,userid) values (  " + transno + ",'masscatentry', 'bookaccessionmaster','insert','" + Request.UserHostAddress.ToString() + "','" + Session("user_id").ToString() + "') "
                    // gCla.IUD(qery)  '
                    // qery = "declare @id int select @id=scope_identity();  insert into AuditTriggerChild( TransNo , MasterId , ColumnName , ValueBefore , ValueAfter ) values (" + transno.ToString() + ",@id,'accessionnumber','" + txtAccNo.Text.Trim() + "' ,null  ) "
                    // gCla.IUD(qery)  '

                    // '                   qery = "update [bookaccessionmaster] Set transno= " + transno + "where accessionnumber='" + txtAccNo.Text + "'"
                    // '                  gCla.IUD(qery)  '
                    // '                    qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + transno + ",'masscatentry','bookaccessionmaster','insert','ipaddress','" + Request.UserHostAddress + "' ) "
                    // '                    gCla.IUD(qery)  '
                    // End If
                    
                    if (AudOper == "I")
                    {
                        var audclas = new AccnAudit();
                        var accno = new string[2];
                        accno[0] = this.txtAccNo.Text;

                        // tno = Convert.ToInt32(gCla.ExScaler(qery))

                        audclas.InsertAudit(accno, "masscateentry", "bookaccessionmaster", Request.UserHostAddress, tno, LoggedUser.Logged().User_Id);
                    }
                    
                    
                    if (AudOper == "U")
                    {
                        var audaccn = new BookAccn();
                        var audbookcatg = new BookCatalog();
                        var audcatdata = new CatalogData();
                        var audbookauth = new BookAuth();
                        var audconf = new BookConf();
                        audaccn.accessionnumber = this.txtAccNo.Text.Trim();

                        audaccn.biilNo = (this.txtBillNo.Text);
                        if (!string.IsNullOrEmpty(this.txtBillDt.Text.Trim()))
                        {
                            audaccn.billDate = Convert.ToDateTime(this.txtBillDt.Text);

                        }
                        audaccn.booktitle = (this.txt_TestSuggoftite.Text);
                        audaccn.Copynumber = Convert.ToInt32(this.txtCopyNo.Text);
                        audaccn.BookNumber = (this.txCpBookno.Text).ToUpper();
                        audconf.Subtitle = this.txtSTitle.Text;
                        audcatdata.classnumber = this.txtClassNo.Text;
                        audcatdata.booknumber = this.txtBookNo.Text;

                        audbookcatg.Volume = this.txtVolume.Text;
                        audbookcatg.part = this.txtPart.Text;
                        audbookcatg.edition = this.txtEdition.Text;
                        audaccn.editionyear = Convert.ToInt32(string.IsNullOrEmpty(this.txtEditionYear.Text)? 0: Convert.ToInt32(this.txtEditionYear.Text));
                        audaccn.pubYear = Convert.ToInt32(this.txtPubYear.Text);
                        audaccn.ItemCategoryCode = Convert.ToInt32(this.ddlCat.SelectedValue);
                        audaccn.Item_type = this.ddlIType.SelectedValue;
                        audaccn.Status = this.ddlIStat.SelectedValue;
                        audaccn.OriginalCurrency = this.ddlCurr.SelectedValue;
                        audaccn.OriginalPrice = Convert.ToDecimal(string.IsNullOrEmpty(this.txtPrice.Text)? 0:Convert.ToDecimal (this.txtPrice.Text));
                        audbookcatg.language_id = Convert.ToInt32(this.ddlLang.SelectedValue);
                        audaccn.DeptCode = Convert.ToInt32(this.ddlDept.SelectedValue);
                        audbookcatg.publishercode = Convert.ToInt32(this.hdnPublId.Value);
                        audaccn.vendor_source = this.txtVend.Text;
                        audbookcatg.isbn = this.txtISBN.Text;
                        audbookcatg.issn = this.txtISSN.Text;
                        audbookcatg.pages = Convert.ToInt32(this.txtPages.Text);
                        audbookcatg.materialdesignation = this.ddlMedia.SelectedItem.Text;
                        audbookcatg.catalogdate1 = Convert.ToDateTime(this.txtDt.Text); // String.Format("{0:dd-MMM-yyyy}", dsRetrCat.Tables("RetrCat").Rows(0)("catalogdate"))
                        audbookcatg.subject1 = this.txtSub1.Text;
                        audbookcatg.subject2 = this.txtSub2.Text;
                        audbookcatg.subject3 = this.txtSub3.Text;
                        audbookauth.firstname1 = this.txtAuthF1.Text;
                        audbookauth.middlename1 = this.txtAuthM1.Text;
                        audbookauth.lastname1 = this.txtAuthL1.Text;
                        audbookauth.firstname2 = this.txtAuthF2.Text;
                        audbookauth.middlename2 = this.txtAuthM2.Text;
                        audbookauth.lastname2 = this.txtAuthL2.Text;
                        audbookauth.firstname3 = this.txtAuthF3.Text;
                        audbookauth.middlename3 = this.txtAuthM3.Text;
                        audbookauth.lastname3 = this.txtAuthL3.Text;

                        audaccn.Loc_id = Convert.ToInt32(this.hdLocidCP.Value);

                        var lsaccnaft = new List<BookAccn>();
                        lsaccnaft.Add(audaccn);
                        var lsb4accn = new List<BookAccn>();
                        var lsbookcatg = new List<BookCatalog>();
                        lsbookcatg.Add(audbookcatg);
                        var lscatdats = new List<CatalogData>();
                        lscatdats.Add(audcatdata);
                        var lsbookauth = new List<BookAuth>();
                        lsbookauth.Add(audbookauth);
                        var lsbookconf = new List<BookConf>();
                        lsbookconf.Add(audconf);

                        var auddats = new UpdCatalog();
                        auddats = (UpdCatalog)Session["auditdata"];
                        auddats.lsAccnaft = lsaccnaft;
                        auddats.lsCatloagaft = lsbookcatg;
                        auddats.lscatagdataaft = lscatdats;
                        auddats.lsauthaft = lsbookauth;
                        auddats.lsbookconfaft = lsbookconf;
                        var audclas = new AccnAudit();
                        // Dim trno As Integer = Convert.ToInt32(gCla.ExScaler("select isnull(max(transno),0)+1 from AuditTriggerMaster"))

                        string resaud = audclas.UpdateAudit(auddats, "masscateentry", Request.UserHostAddress, tno, LoggedUser.Logged().User_Id);

                        Session["auditdata"] = null;
                    }
                    
                    if (AudOper == "D")
                    {

                    }

                    if (AudOper == "A")
                    {
                        // qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + transno + ",'masscatentry', 'bookaccessionmaster','add copy','accessionnumber','" + txtAccNo.Text + "' ) "
                        // gCla.IUD(qery)  'this does not run as separate button is used

                    }
                    

                    gCla.TrClose();
                }
                catch (Exception ex)
                {
                    gCla.TrRollBack();

                }

                // ****Note copyno is cleared or user will not notice current copyno while "Update" is tried
                string Err = "";
                var GetDt = new FillDsTables();
                Err = GetDt.FillDs("select count(*) Catalogued from bookaccessionmaster where ctrl_no<>0;select count(*) Accessioned from bookaccessionmaster ; ", ref dsGen, "TCats");
                this.lblTotC.Text = Convert.ToString(dsGen.Tables["TCats"].Rows[0][0]);
                this.lblTotA.Text = Convert.ToString(dsGen.Tables[1].Rows[0][0]);
                if (!string.IsNullOrEmpty(Err))
                {
                    // LibObj.MsgBox1(Err, Me)
                    message.PageMesg(Err, this, dbUtilities.MsgLevel.Warning);

                    return;
                }
                this.UPCnt.Update();
                this.updMain.Update();
                // LibObj.MsgBox1(resP, Me)
                // lblMsg.Text = resP
                // lblMsg.Value = resP
                // lblMsg.Visible = True
                message.PageMesg(resP, this, dbUtilities.MsgLevel.Warning);
                // Reset()
                // lblMsg.Text = resP
                // lblMsg.Value = resP
                // lblMsg.Visible = True
                message.PageMesg(resP, this, dbUtilities.MsgLevel.Warning);

                if (!resP.Contains("Invalid"))
                {
                    if (Comm.Parameters["@InsUpd48"].Value== "A")
                    {
                        // txtCopyNo.Text = CType(txtCopyNo.Text, Integer) + 1
                        this.txtCopyNo.Text = Convert.ToString(Comm.Parameters["@copynumber_13"].Value);
                    }
                    else
                    {
                        this.txtCopyNo.Text = "1";
                    }
                }
                Comm.Parameters.Clear();
                // !!!!!!!!!!!!!! Note above txtCopyNo setting is overridden here
                if (btnS.ID.ToString() == "btnDel")
                {
                    Reset();
                }
                else
                {
                    this.btnAddCopy.Enabled = true;
                    this.txtAccNoCopy.Enabled = true;
                    this.btnCopyMore.Enabled = true;
                    this.txtCopyNo.Enabled = true;
                    this.txtacccopydt.Enabled = true;
                    this.hdnCtrl_no.Value = OutCtrl_no.ToString();
                    this.txtCopyNo.Text = "";
                }
                // txtAccNo.Text = ""
//                this.UPMsg.Update();
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, resP, Me)
                message.PageMesg(resP, this, dbUtilities.MsgLevel.Success);
  //              this.txtAccNo.Focus();
                return;
            }
            catch (Exception ex)
            {
                Connekt.Close();
                // LibObj.MsgBox1("Insert data failed: " && ex.Message.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, ex.Message, Me)
                // lblMsg.Text = ex.Message
                // lblMsg.Value = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

                // lblMsg.Visible = True
//                this.UPMsg.Update();
  //              this.txtAccNo.Focus();
                return;
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
            btnResClas_Click(sender, e);
            ButtonVisibility();

        }
        public void ButtonVisibility()
        {
            try
            {
                gCla.TrOpen();
                string BtnOkButton = gCla.ExScaler("select btnok from librarysetupinformation").ToString();
                if (BtnOkButton == "Y")
                {
                    this.BtnOk.Visible = true;
                    this.labCorrect.Visible = true;
                }
                else
                {
                    this.BtnOk.Visible = false;
                    this.labCorrect.Visible = false;
                }
                string BtnIncoButton = gCla.ExScaler("select BtnIncorrect from librarysetupinformation").ToString();
                if (BtnIncoButton == "Y")
                {
                    this.BtnIncorrect.Visible = true;

                    this.labCorrect.Visible = true;
                }

                else
                {
                    this.BtnIncorrect.Visible = false;
                    this.labCorrect.Visible = false;

                }
            }
            catch (Exception ex)
            {

            }
            gCla.TrClose();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

        }

        protected void BtnOk_Click1(object sender, EventArgs e)
        {

        }

        protected void BtnIncorrect_Click(object sender, EventArgs e)
        {
            try
            {
                gCla.TrOpen();
                // gCla.IUD("insert into  bookaccessionmasterCopy select * from bookaccessionmaster where accessionnumber='" && txtAccNo.Text.Trim() && "'")
                // gCla.IUD("update bookaccessionmastercopy set catalogdate=getdate(),SearchText='Incorrect',userid='" && Session("user_id") && "' where SerialNo=(select max(SerialNo) from bookaccessionmastercopy) ")
                string qer = "select count(*)  from AccnQC where accnno='" + this.txtAccNo.Text.Trim() + "'";
                int exi = Convert.ToInt32(gCla.ExScaler(qer));
                if (exi == 0)
                {
                    qer = "select isnull(max(id),0)+1  from AccnQC";
                    int id = Convert.ToInt32(gCla.ExScaler(qer));
                    qer = "insert into accnqc (id,accnno,Correct,userid  ) values (" + id.ToString() + ",'" + this.txtAccNo.Text.Trim() + "','N','"+ LoggedUser.Logged().User_Id+ "') ";
                }
                else
                {
                    qer = "select id  from AccnQC where accnno='" + this.txtAccNo.Text + "'";
                    int id = Convert.ToInt32(gCla.ExScaler(qer));
                    qer = "update accnqc set correct='N',qcdate=getdate()  where id=" + id.ToString();

                }

                gCla.IUD(qer);

                gCla.TrClose();
                message.PageMesg("Book Information is InCorrect...!!!!", this, dbUtilities.MsgLevel.Success);
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure   );
            }
        }

        protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hdnCat.Value = this.ddlCat.SelectedValue;
        }

        protected void ddlIType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hdnItype.Value = this.ddlIType.SelectedIndex.ToString();
        }

        protected void ddlIStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hdnIStat.Value = this.ddlIStat.SelectedIndex.ToString();
        }
        private bool ValidateMass()
        {
            if (string.IsNullOrEmpty(this.txtAccNo.Text) || string.IsNullOrEmpty(this.txt_TestSuggoftite.Text) || string.IsNullOrEmpty(this.txtPubl.Text) || string.IsNullOrEmpty(this.txtVend.Text) || string.IsNullOrEmpty(this.txtAuthF1.Text) || string.IsNullOrEmpty(this.txtCopyNo.Text))
            {
                return false;
            }
            return true;
        }
        protected void ddlCurr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hdnCurr.Value = this.ddlCurr.SelectedIndex.ToString();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hdnDept.Value = this.ddlDept.SelectedIndex.ToString();
        }

        protected void ddlMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hdnMedia.Value = this.ddlMedia.SelectedIndex.ToString();
        }

        protected void rblDBOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If rblDBOption.SelectedValue = 1 Then
            // ExtTitle.Enabled = True
            // Else
            // ExtTitle.Enabled = False
            // End If
            Session["rblDBOption"] = this.rblDBOption.SelectedValue;
            Reset();
        }

        protected void ChkShowSearch_CheckedChanged(object sender, EventArgs e)
        {
            Searchshow();
        }
        public void Searchshow()
        {
            if (this.ChkShowSearch.Checked == true)
            {
                this.pnlSearch.Visible = true;
            }
            else
            {
                this.pnlSearch.Visible = false;
            }
        }

        protected void lnkY_Click(object sender, EventArgs e)
        {
            // txtPubl.Text
            var Comm = new OleDbCommand();
            string ConStr = retConstr("");
            var Connekt = new OleDbConnection(ConStr);
            try
            {
                int publId;
                Connekt.Open();
                Comm.CommandText = "select isnull(max(cast(publisherid as int)),0)+1 from publishermaster";
                Comm = new OleDbCommand(Comm.CommandText, Connekt);
                Comm.CommandType = CommandType.Text;
                Comm.CommandTimeout = 300;
                // Comm.ExecuteNonQuery()
                publId = Convert.ToInt32(Comm.ExecuteScalar());
                string PQry;
                // LibObj.MsgBox1(Session("Publ").ToString, Me)


                PQry = " INSERT INTO [dbo].[publishermaster] ( [PublisherId], [PublisherCode], [firstname], [PublisherPhone1]," + " [PublisherPhone2],	 [EmailID],	 [webaddress],	[PublisherType]," + " [userid])  VALUES (" + publId.ToString() + ",'P-" + publId.ToString() + "', '" + Session["Publ"].ToString() + "','','','','','','" +LoggedUser.Logged().User_Id + "')";
                // @PublisherId_1,	 @PublisherCode_2,	 @firstname_3,	 @PublisherPhone1_4,	 @PublisherPhone2_5,	 @EmailID_6,	 @webaddress_7,	 @PublisherType_8, '"local"     @userid_9)
                Comm.CommandText = PQry;
                Comm = new OleDbCommand(Comm.CommandText, Connekt);
                Comm.CommandType = CommandType.Text;
                Comm.CommandTimeout = 300;
                Comm.ExecuteNonQuery();
                PQry = "insert into addresstable values ('" + publId.ToString() + "','','','','','India','','" + Session["PublCity"].ToString() + "','','India','','Publisher')";
                Comm.CommandText = PQry;
                Comm = new OleDbCommand(Comm.CommandText, Connekt);
                Comm.CommandType = CommandType.Text;
                Comm.CommandTimeout = 300;
                Comm.ExecuteNonQuery();
                this.hdnPublId.Value = publId.ToString();
                this.txtPubl.Text = Session["Publ"].ToString() + ", " + Session["PublCity"].ToString();
                Connekt.Close();
            }
            catch (Exception ex)
            {
                Connekt.Close();
                // LibObj.MsgBox1("Error in publisher entry, " && ex.Message.ToString, Me)
                message.PageMesg("Error in publisher entry, " + ex.Message.ToString(), this, dbUtilities.MsgLevel.Failure);

                return;
            }


        }

        protected void lnkN_Click(object sender, EventArgs e)
        {

        }

        protected void btnscan_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlantenna.SelectedIndex == 0)
                {
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Please Select Antenna!!!", Me)
                    message.PageMesg("Please Select Antenna!!!", this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                // Dim GetEPC As DataTable = New DataTable()
                string epcId = "";
                gCla.TrOpen();
                try
                {
                    epcId = gCla.ExScaler("select EPC from RFIDLOG where ANTNUM='" + this.ddlantenna.SelectedValue + "'").ToString();
                }
                catch (Exception ex)
                {
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, ex.Message, Me)
                    message.PageMesg("Book Not Found...!!!!", this, dbUtilities.MsgLevel.Warning);
                    // gCla.TrClose()
                    return;
                }
                gCla.TrClose();
                if (!string.IsNullOrEmpty(epcId))
                {
                    try
                    {
                        gCla.TrOpen();
                        string Acc = gCla.ExScaler("select accessionnumber from bookaccessionmaster where RfidId='" + epcId + "'").ToString();
                        if (!string.IsNullOrEmpty(Acc))
                        {
                            this.txtAccNo.Text = Acc;
                            this.hdnAccNo.Value = Acc;
                            AccNoSelected(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, ex.Message, Me)
                        message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                    }
                }
            }

            catch (Exception ex)
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, ex.Message, Me)
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                gCla.TrClose();
                return;
            }
        }
        public void FRFIDPermission()
        {

            // RFID  Process
            int RFID = Convert.ToInt32(DBI.ExecuteScalar("select count(*) from FeaturesPer where FID = 13", DBI.GetConnectionString(DBI.GetConnectionName())));
            if (RFID > 0)
            {
                this.RFidProcess.Visible = true;
            }
            else
            {
                this.RFidProcess.Visible = false;
            }

        }

        protected void btnAddCopy_Click(object sender, EventArgs e)
        {

            var Comm = new OleDbCommand();
            string ConStr = retConstr("");
            var Connekt = new OleDbConnection(ConStr);


            try
            {
                Connekt.Open();
                string qery = "  select ISNULL(max(TransNo),0)+1 from AuditTriggerMaster  ";
                Comm = new OleDbCommand(qery, Connekt);
                this.hdTransNo.Value = Comm.ExecuteScalar().ToString();
                Comm.CommandText = "insert_Accession_ExpressCP"; // 
                Comm = new OleDbCommand(Comm.CommandText, Connekt);
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.CommandTimeout = 300;
                Comm.Parameters.AddWithValue("@AccessionNo", OleDbType.VarChar).Value = this.txtAccNo.Text.Trim();
                Comm.Parameters.AddWithValue("@AccessionNoCp", OleDbType.VarChar).Value = this.txtAccNoCopy.Text.Trim();
                Comm.Parameters.AddWithValue("@Copynnumber", OleDbType.VarChar).Value = this.txtCopyNo.Text;
                Comm.Parameters.AddWithValue("@Ctrl_no", OleDbType.VarChar).Value = this.hdnCtrl_no.Value;
                Comm.Parameters.AddWithValue("@BookPrice", OleDbType.VarChar).Value = string.IsNullOrEmpty(this.txCpPrice.Text.Trim())? "0": this.txCpPrice.Text;
                Comm.Parameters.AddWithValue("@BookNumberCP", OleDbType.VarChar).Value = this.txCpBookno.Text.Trim();
                Comm.Parameters.AddWithValue("@Item_type", OleDbType.VarChar).Value = this.ddlItemTypeCP.SelectedItem.Text;
                Comm.Parameters.AddWithValue("@AccnCpDate", OleDbType.VarChar).Value = this.txtacccopydt.Text;
                Comm.Parameters.AddWithValue("@BiilNo", OleDbType.VarChar).Value = this.txBillNoCP.Text;
                Comm.Parameters.AddWithValue("@BillDate", OleDbType.VarChar).Value = this.txBillDtCP.Text;
                Comm.Parameters.AddWithValue("@DeptId", OleDbType.VarChar).Value = this.ddlDeptCP.SelectedValue;
                Comm.Parameters.AddWithValue("@ItemCategory", OleDbType.VarChar).Value = this.ddlCategCP.SelectedValue;
                Comm.Parameters.AddWithValue("@Vendor", OleDbType.VarChar).Value = this.txVendorCP.Text;
                Comm.Parameters.AddWithValue("@LocId", OleDbType.VarChar).Value = string.IsNullOrEmpty(this.hdLocidCP.Value)? "0": this.hdLocidCP.Value;
                Comm.Parameters.AddWithValue("@userid", OleDbType.VarChar).Value = LoggedUser.Logged().User_Id;
                Comm.Parameters.AddWithValue("@IpAddress", OleDbType.VarChar).Value = Request.UserHostAddress;
                Comm.Parameters.AddWithValue("@AppName", OleDbType.VarChar).Value = "masscatentry";
                Comm.Parameters.AddWithValue("@TransNo", OleDbType.Integer).Value = this.hdTransNo.Value;
                // @Copynnumber
                // @Ctrl_no
                // @BookPrice
                // @BookNumberCP
                // @Item_type
                // @AccnCpDate
                // @BiilNo
                // @BillDate
                // @DeptId
                // @ItemCategory
                // @Vendor
                // @LocId  @userid

                // 


                Comm.ExecuteNonQuery();

                gCla.TrOpen();

                // qery = " insert into AuditTriggerMaster (transno,AppName,TableName,Operation,ipaddress,userid) values (  " + hdTransNo.Value + ",'masscatentry', 'bookaccessionmaster','add copy','" + Request.UserHostAddress.ToString() + "','" + Session("user_id").ToString() + "' ) "
                // gCla.IUD(qery)  '
                // qery = "declare @id int select @id=scope_identity();  insert into AuditTriggerChild (transno,masterid,columnname,valuebefore ) values ( " + hdTransNo.Value + ",@id,'accessionnumber','" + txtAccNoCopy.Text + "')"
                // gCla.IUD(qery)  '
                gCla.TrClose();


                // Dim qery As String = "  select ISNULL(max(TransNo),0)+1 from DataUpdateTrigger  "
                // Comm = New OleDbCommand(qery, Connekt)
                // hdTransNo.Value = Comm.ExecuteScalar
                // qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + hdTransNo.Value + ",'masscatentry', 'bookaccessionmaster','add copy','accessionnumber','" + txtAccNoCopy.Text + "' ) "
                // Comm = New OleDbCommand(qery, Connekt)
                // qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + hdTransNo.Value + ",'masscatentry', 'bookaccessionmaster','add copy','userid','" + Session("user_id").ToString() + "' ) "
                // Comm = New OleDbCommand(qery, Connekt)
                // Comm.ExecuteNonQuery()

                message.PageMesg(this.txtAccNoCopy.Text + " Copy added", this, dbUtilities.MsgLevel.Success);
            }

            catch (Exception ex)
            {
                // lblMsg.Text = ex.Message
                // lblMsg.Visible = True 
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

                // lblMsg.Value = Err
//                this.UPMsg.Update();
            }

            Connekt.Close();

            btnReset_Click(sender, e);
            // being stopped 
            // btnSave_Click(sender, e)
        }
    }
}