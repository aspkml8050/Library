using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Web.UI.HtmlControls;
using Audit;

namespace Library
{
    public partial class CatalogDetail1 : BaseClass
    {
        private int tot;
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        ClassFileCatelog ObjCatalog = new ClassFileCatelog();
        ClassFileBookAccession ObjBookAccession = new ClassFileBookAccession();
        //        ClassFileDirectcatalogifo ObjDirectCatelog = new ClassFileDirectcatalogifo();
        GlobClassTr gCla = new GlobClassTr();
        private messageLibrary msgLibrary = new messageLibrary();
        DBIStructure DBI = new DBIStructure();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();
        //        private spell obj = new spell();
        private static string aa1;

        protected global::System.Web.UI.HtmlControls.HtmlInputSubmit cmdReset1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
           {
                var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                //                hdIsMarc21.Value = myConfiguration.AppSettings.Settings["IsMarc21"].Value;

                //              hdCulture.Value = Request.Cookies["UserCulture"].Value;
                // Dim libObj As New Library.libGeneralFunctions
                //            msglabel.Visible = false;


                //var acnaudt = new AccnAudit();  //do it later


                // Dim acndata As New AccnData
                // acndata.accn = "423432"
                // acndata.title = "psoer deler"

                // acnaudt.accndata = acndata

                Label80.Visible = false;
                // chkSearch.Visible = False
                Hidden1.Value = "283";
                // hddeptcode.Value = Session["Dept"].ToString();
                Session["Dept"] = null;
                Session["back_k"] = "Catalog_Acc";
                cmdReset.Attributes.Add("ServerClick", "return cmdReset_ServerClick();");
                cmdReset1.Attributes.Add("ServerClick", "return cmdReset_ServerClick();");
                if (txtdate.Text == string.Empty)
                {
                    this.txtdate.Text = string.Format("{0:dd-MMM-yyyy}", System.DateTime.Today);
                }

                Label80.Visible = false;
                if (!Page.IsPostBack)
                {

                    panel1.Visible = false;
                    LinkButton1.BorderStyle = BorderStyle.None;
                    LinkButton2.BorderStyle = BorderStyle.None;
                    LinkButton3.BorderStyle = BorderStyle.None;
                    LinkButton4.BorderStyle = BorderStyle.None;
                    LinkButton5.BorderStyle = BorderStyle.None;
                    LinkButton6.BorderStyle = BorderStyle.None;
                    LinkButton7.BorderStyle = BorderStyle.Double;
                    LinkButton8.BorderStyle = BorderStyle.None;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    Panel5.Visible = false;
                    panel6.Visible = false;
                    panel7.Visible = true;
                    panel8.Visible = false;


                    cmdReset.Attributes.Add("ServerClick", "return cmdsubmit_ServerClick();");
                    this.cmdReset.CausesValidation = false;

                    var bookcatalogcon = new OleDbConnection(retConstr(""));
                    bookcatalogcon.Open();
                    LibObj.save_itemtype(bookcatalogcon, LoggedUser.Logged().User_Id);
                    var bookcategorycon = new OleDbConnection(retConstr(""));
                    bookcategorycon.Open();
                    // txtDocNo.Focus();
                    HCondition.Value = "Y";// Session["tmpcondition"]; // Request.QueryString("condition")
                    hdCtrlNo.Value = Request.QueryString["ctrl"];
                    hdctrlStatus.Value = Request.QueryString["nctrl"];
                    // cmdReturnMain.CausesValidation = False
                    // cmdReturnMainT.CausesValidation = False
                    // cmdReturnMain1.CausesValidation = False
                    // cmdReturnMain2.CausesValidation = False
                    // cmdReturnMain3.CausesValidation = False
                    // cmdReturnMain4.CausesValidation = False
                    // cmdReturnMain5.CausesValidation = False
                    cmdReturn.CausesValidation = false;
                    cmdReturnt.CausesValidation = false;
                    cmdReturn1.CausesValidation = false;
                    cmdReturn2.CausesValidation = false;
                    cmdReturn3.CausesValidation = false;
                    cmdReturn4.CausesValidation = false;
                    cmdReturn5.CausesValidation = false;
                    // cmdReturn7.CausesValidation = False
                    cmdReset.CausesValidation = false;
                    cmdResetT.CausesValidation = false;
                    cmdReset1.CausesValidation = false;
                    cmdReset2.CausesValidation = false;
                    cmdReset3.CausesValidation = false;
                    cmdReset4.CausesValidation = false;
                    cmdReset5.CausesValidation = false;
                    cmdreset7.CausesValidation = false;
                    cmdBack7.CausesValidation = false;

                    var dt = new DataTable();
                    grdcopy.DataSource = null;
                    grdcopy.DataBind();
                    dt.Dispose();
                    // --------Currency Fill
                    PopulateCurrency();
                    // ========
                    catentry(bookcatalogcon);
                    ////               TotalAccession(bookcatalogcon);

                    var bookcatalogcom = new OleDbCommand();
                    bookcatalogcom.Connection = bookcatalogcon;
                    bookcatalogcom.CommandType = CommandType.Text;

                    var bookcategorycom = new OleDbCommand();
                    bookcategorycom.Connection = bookcategorycon;
                    bookcategorycom.CommandType = CommandType.Text;
                    // LanguageComboFill Up
                    var ComboDs = new DataSet();
                    ComboDs = LibObj.PopulateDataset("select language_id,Language_name from translation_language order by language_Name", "Language", bookcatalogcon);
                    if (ComboDs.Tables["Language"].Rows.Count > 0)
                    {
                        cmbLanguage.DataSource = ComboDs.Tables["Language"];
                        cmbLanguage.DataTextField = "Language_name";
                        cmbLanguage.DataValueField = "Language_id";
                        cmbLanguage.DataBind();
                    }
                    else
                    {
                        cmbLanguage.Items.Clear();
                    }
                    // booknumber of call number is parameterized to be stored in accessionmaster based on featuresper-15
                    string qer = " select * from featuresper where fid=15";
                    var dtFet = new DataTable();
                    var ada = new OleDbDataAdapter(qer, bookcatalogcon);
                    ada.Fill(dtFet);
                    if (dtFet.Rows.Count > 0)
                    {
                        hdBookNumAccn.Value = "1";
                        labBookN.ToolTip = "Stored for individual Accession Copy";
                    }
                    else
                    {
                        hdBookNumAccn.Value = "";
                        labBookN.ToolTip = "Stored same for all Accession Copies";
                    }


                    cmbLanguage.Items.Add(HComboSelect.Value);
                    cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
                    ComboDs = LibObj.PopulateDataset("select id,category_loadingStatus from CategoryLoadingStatus order By  category_loadingstatus", "Category", bookcatalogcon);



                    if (ComboDs.Tables["Category"].Rows.Count > 0)
                    {
                        cmbbookcategory.DataSource = ComboDs.Tables["Category"];
                        cmbbookcategory.DataTextField = "category_loadingstatus";
                        cmbbookcategory.DataValueField = "id";
                        cmbbookcategory.DataBind();
                    }
                    else
                    {
                        cmbbookcategory.Items.Clear();
                    }

                    cmbbookcategory.Items.Add(HComboSelect.Value);
                    cmbbookcategory.SelectedIndex = cmbbookcategory.Items.Count - 1;

                    // libObj.populateDDL(cmbStatus, "select ItemStatusID,ItemStatus from Itemstatusmaster order by itemstatus", "Itemstatus", "Itemstatusid", HComboSelect.Value, bookcatalogcon)
                    LibObj.populateDDL(cmbtype, "select Item_Type as ITVal,Item_Type as ITText from Item_Type where id <> 0 order by Item_Type", "ITText", "ITVal", HComboSelect.Value, bookcatalogcon);
                    ComboDs = LibObj.PopulateDataset("select ItemStatusID,ItemStatus from Itemstatusmaster order by itemstatus", "Status", bookcatalogcon);
                    if (ComboDs.Tables["Status"].Rows.Count > 0)
                    {
                        cmbStatus.DataSource = ComboDs.Tables["Status"];
                        cmbStatus.DataTextField = "ItemStatus";
                        cmbStatus.DataValueField = "ItemStatusID";
                        cmbStatus.DataBind();
                    }
                    else
                    {
                        cmbStatus.Items.Clear();
                    }

                    cmbStatus.Items.Add(HComboSelect.Value);
                    cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;

                    ComboDs = LibObj.PopulateDataset("Select distinct program_id, Program_Master.program_name as programname from Program_Master order by program_name", "Course", bookcatalogcon);
                    if (ComboDs.Tables["Course"].Rows.Count > 0)
                    {
                        cmbCourse1.DataSource = ComboDs.Tables["Course"];
                        cmbCourse1.DataTextField = "programname";
                        cmbCourse1.DataValueField = "program_id";
                        cmbCourse1.DataBind();
                    }
                    else
                    {
                        cmbCourse1.Items.Clear();
                    }

                    cmbCourse1.Items.Add(HComboSelect.Value);
                    cmbCourse1.SelectedIndex = cmbCourse1.Items.Count - 1;

                    ComboDs = LibObj.PopulateDataset("Select media_id,media_name from media_type order by media_name", "Media", bookcatalogcon);
                    if (ComboDs.Tables["Media"].Rows.Count > 0)
                    {
                        cmbMediaType.DataSource = ComboDs.Tables["Media"];
                        cmbMediaType.DataTextField = "Media_name";
                        cmbMediaType.DataValueField = "Media_id";
                        cmbMediaType.DataBind();
                    }
                    else
                    {
                        cmbMediaType.Items.Clear();
                    }

                    cmbMediaType.Items.Add(HComboSelect.Value);
                    cmbMediaType.SelectedIndex = cmbMediaType.Items.Count - 1;
                    // bookcatalogcom.Dispose()

                    // Department ComboFillUp
                    // ****************
                    ComboDs = LibObj.PopulateDataset("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "dept", bookcatalogcon);
                    if (ComboDs.Tables["dept"].Rows.Count > 0)
                    {
                        cmbdept.DataSource = ComboDs.Tables["dept"];
                        cmbdept.DataTextField = "departmentname";
                        cmbdept.DataValueField = "departmentcode";
                        cmbdept.DataBind();
                    }
                    else
                    {
                        cmbdept.Items.Clear();
                    }
                    cmbdept.Items.Add(HComboSelect.Value);
                    cmbdept.SelectedIndex = cmbdept.Items.Count - 1;

                    txtbookno.Disabled = false;
                    txtclassno.Disabled = false;
                    // If Session("back") = "catalog" Then
                    // txtbookno.Disabled = True
                    // txtclassno.Disabled = True
                    // Else
                    // txtbookno.Disabled = False
                    // txtclassno.Disabled = False
                    // End If

                    txtbookno.Value = Session["BNumber"].ToString();
                    txtclassno.Value = Session["CNumber"].ToString();
                    txtacc.Text = Session["accno"].ToString();
                    Text1.Value = Session["accno"].ToString();


                    // If Not Request.QueryString("AdvAccNo").ToString = Nothing Then
                    // txtacc.Value = Request.QueryString("AdvAccNo").ToString
                    // End If
                    Label82.Text = Label82.Text + "(" + LibObj.getCurrency(retConstr("")) + ")";
                    Label190.Text = Label190.Text + "(" + LibObj.getCurrency(retConstr("")) + ")";
                    hdnInsUpd.Value = "";

                    // for auditing
                    //                    var audw = new AccnAudit();
                    //                  var updc = new UpdCatalog();
                    //                  var audaccn = new BookAccn();
                    //                 var audbookcatg = new BookCatalog();

                    //                    var audbookauth = new BookAuth();
                    //                   var audcatdata = new CatalogData();
                    //                  var audconf = new BookConf();
                    //                 var lsaudaccn = new List<BookAccn>();
                    //                var lsaudbookcatg = new List<BookCatalog>();
                    //               var lsaudbookauth = new List<BookAuth>();
                    //              var lsaudcatdata = new List<CatalogData>();
                    //             var lsaudconf = new List<BookConf>();

                    if (Session["back"].ToString() == Resources.ValidationResources.bSearch)   // Form has been loaded for editing purpose Or Trim(hdCtrlNo.Value) <> String.Empty
                    {
                        var tmpCon = new OleDbConnection(retConstr(""));
                        tmpCon.Open();
                        var tmpda = new OleDbDataAdapter("SELECT catalogdate,booktype,volumenumber,initpages,pages,parts,leaves,boundind,title,publishercode,edition,isbn,subject1,subject2,subject3,Booksize,LCCN,Volumepages,biblioPages,bookindex,illustration,variouspaging,maps,ETalEditor,ETalCompiler,ETalIllus,ETalTrans,ETalAuthor,accmaterialhistory,bookprice,MaterialDesignation,issn,Volume,bookaccessionmaster.deptcode as dept,language_id,part,eBookURL,editionyear,Copynumber,specialprice,pubYear,Bookaccessionmaster.ctrl_no, FixedData, cat_Source, Identifier,bookaccessionmaster.ItemCategory,bookaccessionmaster.ItemCategoryCode,item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate,bookaccessionmaster.status,bookaccessionMaster.ReleaseDate, vendor_Source, bookaccessionmaster.program_id, bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location,isnull(ipaddress,'') ipaddress,userid FROM BookCatalog INNER JOIN  bookaccessionmaster ON bookaccessionmaster.ctrl_no =BookCatalog.ctrl_no where BookCatalog.ctrl_no=" + hdCtrlNo.Value + " and bookaccessionmaster.accessionnumber=N'" + txtacc.Text + "'", tmpCon);
                        DataSet tmpds = new DataSet();
                        tmpda.Fill(tmpds, "result");
                        if (tmpds.Tables[0].Rows.Count > 0)
                        {
                            var cmdimg = new OleDbCommand("select CoverPage from BookImage where ctrl_no='" + hdCtrlNo.Value + "'", bookcatalogcon);
                            DataTable dtImg = new DataTable();
                            OleDbDataAdapter oda = new OleDbDataAdapter(cmdimg);
                            oda.Fill(dtImg);
                            if (dtImg.Rows.Count > 0)
                            {
                               var imgbin = (byte[])dtImg.Rows[0][0];
                                var imgsrc = "data:image/jpeg;base64" + Convert.ToBase64String(imgbin);
                                image1.Src = imgsrc;

                                //                    imgbin = (byte[])dtImg.Rows[0]["CoverPage"];
                            }

                            if (tmpds.Tables[0].Rows[0]["item_type"].ToString() != "journals")
                            {
                                if (tmpds.Tables[0].Rows[0].IsNull("catalogdate"))
                                {
                                    txtdate.Text = string.Empty;
                                }
                                else
                                {
                                    txtdate.Text = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(tmpds.Tables[0].Rows[0]["catalogdate"]));
                                }
                                this.cmbStatus.SelectedValue = "---Select---";
                                try
                                {
                                    cmbStatus.SelectedValue = tmpds.Tables[0].Rows[0]["status"].ToString();
                                }
                                catch { }
                                //if (tmpds.Tables[0].Rows[0]["status"].ToString() != "NA")
                                //{
                                //    this.cmbStatus.SelectedIndex = this.cmbStatus.Items.Count - 1;
                                //}
                                //else
                                //{
                                //    this.cmbStatus.SelectedIndex = cmbStatus.Items.IndexOf(cmbStatus.Items.FindByValue(tmpds.Tables[0].Rows[0]["status"].ToString()));
                                //}

                                string qry_str;
                                qry_str = "select isBardateApllicable FROM ITEMSTATUSMASTER Where Itemstatusid=" + cmbStatus.SelectedItem.Value;
                                var da = new OleDbDataAdapter(qry_str, tmpCon);
                                var ds = new DataSet();
                                da.Fill(ds, "load");
                                string isbardate = string.Empty;
                                if (ds.Tables["load"].Rows.Count > 0)
                                {
                                    isbardate = Convert.ToString(ds.Tables["load"].Rows[0]["isBardateApllicable"]);
                                }
                                //                                // BY Kaushal
                                if (isbardate == "Y")
                                {
                                    Labelb.Visible = true;
                                    txtrelease.Visible = true;
                                    this.Button1.Visible = true;
                                    if (tmpds.Tables[0].Rows[0].IsNull("ReleaseDate") || (tmpds.Tables[0].Rows[0]["ReleaseDate"].ToString() == ""))
                                    {
                                        this.txtrelease.Value = "";
                                    }
                                    else
                                    {
                                        this.txtrelease.Value = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(tmpds.Tables[0].Rows[0]["ReleaseDate"]));
                                    }
                                }

                                // Me.txtrelease.Value = IIf(tmpds.Tables(0).Rows(0).Item("ReleaseDate").ToString() = String.Empty, "", Format(tmpds.Tables(0).Rows(0).Item("ReleaseDate"), Me.hrDate.Value))
                                else
                                {
                                    Labelb.Visible = false;
                                    txtrelease.Visible = false;
                                    Button1.Visible = false;
                                }





                                //                                //// Else
                                //                                //// Labelb.Visible = False
                                //                                //// txtrelease.Visible = False
                                //                                //// Button1.Visible = False
                                //                                //// End If
                                if (tmpds.Tables[0].Rows[0].IsNull("program_id"))
                                {
                                    cmbCourse1.SelectedValue = "0";
                                }
                                else
                                {
                                    cmbCourse1.SelectedValue = tmpds.Tables[0].Rows[0]["program_id"].ToString();
                                }

                                //                                // cmbbookcategory.Value = tmpds.Tables(0).Rows(0).Item("booktype")
                                cmbbookcategory.SelectedValue = tmpds.Tables[0].Rows[0]["ItemCategoryCode"].ToString();

                                cmbLanguage.SelectedValue = tmpds.Tables[0].Rows[0]["language_id"].ToString();

                                txtPart.Value = tmpds.Tables[0].Rows[0]["part"].ToString();

                                this.txtvolno.Value = tmpds.Tables[0].Rows[0]["volumenumber"].ToString();

                                // txtSpecialPrice.Value = tmpds.Tables(0).Rows(0).Item("specialprice")
                                txtSpecialPrice.Value = tmpds.Tables[0].Rows[0]["specialprice"].ToString();
                                cmbdept.SelectedValue = tmpds.Tables[0].Rows[0]["dept"].ToString();
                                this.txtinitpages.Value = tmpds.Tables[0].Rows[0]["initpages"].ToString();

                                this.txtpages.Value = tmpds.Tables[0].Rows[0]["pages"].ToString();

                                this.txtparts.Value = tmpds.Tables[0].Rows[0]["parts"].ToString();

                                //                                this.txtleaves.Value =  tmpds.Tables[0].Rows[0]["leaves"].ToString();
                                //                                //// If tmpds.Tables(0).Rows(0).Item("boundind") = "s" Then
                                //                                //// tmpds.Tables(0).Rows(0).Item("boundind") = "Soft"
                                //                                //// ElseIf tmpds.Tables(0).Rows(0).Item("boundind") = "h" Then
                                //                                //// tmpds.Tables(0).Rows(0).Item("boundind") = "Hard"
                                //                                //// End If
                                txtLoc2.Text = tmpds.Tables[0].Rows[0]["loc_id"].ToString();

                                //                                //// 
                                txtlocation.Text = tmpds.Tables[0].Rows[0]["location"].ToString();
                                cmbboundind.SelectedIndex = cmbboundind.Items.IndexOf(cmbboundind.Items.FindByText(tmpds.Tables[0].Rows[0]["boundind"].ToString()));

                                this.txttitle.Value = tmpds.Tables[0].Rows[0]["title"].ToString();
                                //// HdBookTitle.Value = Me.txttitle.Value

                                string sqlstr = "Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'" + tmpds.Tables[0].Rows[0]["publishercode"].ToString() + "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'";
                                var cmd = new OleDbCommand(sqlstr, tmpCon);
                                string tmpstr = Convert.ToString(cmd.ExecuteScalar());
                                this.hdPublisherId.Value = tmpds.Tables[0].Rows[0]["publishercode"].ToString();
                                string qerct = "select isnull(Control008,'') from bookcatalog where ctrl_no=" + tmpds.Tables[0].Rows[0]["ctrl_no"].ToString();
                                var cmdCopy = new OleDbCommand(qerct);
                                cmdCopy.Connection = tmpCon;

                                txtControlNo.Text = cmdCopy.ExecuteScalar().ToString();


                                txtCmbPublisher.Text = tmpstr;
                                txtCmbVendor.Text = tmpds.Tables[0].Rows[0]["vendor_source"].ToString();
                                this.txtedition.Value = tmpds.Tables[0].Rows[0]["edition"].ToString();

                                if (!tmpds.Tables[0].Rows[0].IsNull("editionyear"))
                                {
                                    txteditionyear.Value = tmpds.Tables[0].Rows[0]["editionyear"].ToString();
                                }
                                else
                                {
                                    txteditionyear.Value = string.Empty;
                                }

                                this.txtisbn.Value = tmpds.Tables[0].Rows[0]["isbn"].ToString();

                                this.txtSub11.Text = tmpds.Tables[0].Rows[0]["subject1"].ToString();
                                this.txtsub2.Text = tmpds.Tables[0].Rows[0]["subject2"].ToString();
                                this.txtsub3.Text = tmpds.Tables[0].Rows[0]["subject3"].ToString();


                                this.txtbooksize.Value = tmpds.Tables[0].Rows[0]["Booksize"].ToString();
                                this.txtlccn.Value = tmpds.Tables[0].Rows[0]["LCCN"].ToString();
                                this.txtvolpages.Value = tmpds.Tables[0].Rows[0]["Volumepages"].ToString();
                                this.txtbiblpages.Value = tmpds.Tables[0].Rows[0]["biblioPages"].ToString();
                                this.cboBookIndex.SelectedIndex = cboBookIndex.Items.IndexOf(cboBookIndex.Items.FindByText(tmpds.Tables[0].Rows[0]["bookindex"].ToString()));
                                this.cboIllistration.SelectedIndex = cboIllistration.Items.IndexOf(cboIllistration.Items.FindByText(tmpds.Tables[0].Rows[0]["illustration"].ToString()));
                                this.cbovariouspaging.SelectedIndex = cbovariouspaging.Items.IndexOf(cbovariouspaging.Items.FindByText(tmpds.Tables[0].Rows[0]["variouspaging"].ToString()));
                                //this.txtmaps.Value = tmpds.Tables[0].Rows[0]["maps"]) | Operators.ConditionalCompareObjectEqual(tmpds.Tables[0].Rows[0]["maps"], 0, false), string.Empty, tmpds.Tables[0].Rows[0]["maps"]);
                                this.cboEditorETAL.SelectedIndex = cboEditorETAL.Items.IndexOf(cboEditorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalEditor"].ToString()));
                                this.cboCompilerETAL.SelectedIndex = cboCompilerETAL.Items.IndexOf(cboCompilerETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalCompiler"].ToString()));
                                this.cboILLustratorETAL.SelectedIndex = cboILLustratorETAL.Items.IndexOf(cboILLustratorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalIllus"].ToString()));
                                this.cboTranslatorETAL.SelectedIndex = cboTranslatorETAL.Items.IndexOf(cboTranslatorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalTrans"].ToString()));
                                this.cboAuthorETAL.SelectedIndex = cboAuthorETAL.Items.IndexOf(cboAuthorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalAuthor"].ToString()));
                                this.txtCopyNo.Value = tmpds.Tables[0].Rows[0]["copyNumber"].ToString();

                                this.txtmaterialinfo.Value = tmpds.Tables[0].Rows[0]["accmaterialhistory"].ToString();
                                this.txtbookprice.Value = tmpds.Tables[0].Rows[0]["bookprice"].ToString();

                                //                                //// .........................
                                txtDocNo.Value = tmpds.Tables["result"].Rows[0]["biilNo"].ToString();
                                txtDocDate.Text = tmpds.Tables["result"].Rows[0]["billDate"].ToString();
                                txtDocDate.Text = txtDocDate.Text.Contains("1900") ? "" : txtDocDate.Text;
                                txtVolumeNo.Value = tmpds.Tables[0].Rows[0]["Volume"].ToString();
                                //// P

                                cmbcurr.SelectedIndex = cmbcurr.Items.IndexOf(cmbcurr.Items.FindByText(tmpds.Tables[0].Rows[0]["OriginalCurrency"].ToString())); // IIf(IsDBNull(tmpds.Tables("result").Rows(0).Item("OriginalCurrency")), String.Empty, tmpds.Tables("result").Rows(0).Item("OriginalCurrency"))

                                txtForeignPrice.Value = tmpds.Tables[0].Rows[0]["originalprice"].ToString();
                                //                                //// set item ItemType Index positon By jeetendra Prajapati as on 28 dec 009 
                                //                                //// This Field is separated with Accession No. not With Control no
                                cmbtype.SelectedIndex = cmbtype.Items.IndexOf(cmbtype.Items.FindByValue(tmpds.Tables[0].Rows[0]["item_type"].ToString()));
//                                cmbtype.Items.Add("Journal");
                                //                                var litem = new ListItem();
                                //                              litem.Value = tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString();
                                //                            if (cmbMediaType.Items.Contains(litem) == true)
                                //                          {
                                //                            cmbMediaType.SelectedValue = tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString();
                                //                      }
                                //                    else
                                //                  {
                                //                    cmbMediaType.SelectedValue = "---Select--";// cmbMediaType.Items.Count - 1;
                                //              }
                                cmbMediaType.SelectedValue = "---Select--";// cmbMediaType.Items.Count - 1;
                                try
                                {
                                    cmbMediaType.SelectedValue = cmbMediaType.Items.FindByText( tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString()).Value;
                                }
                                catch
                                {

                                }
                                txtIssnNo.Value = tmpds.Tables[0].Rows[0]["issn"].ToString();

                                if (!tmpds.Tables[0].Rows[0].IsNull("pubYear"))
                                {
                                    txtPubYear.Value = tmpds.Tables[0].Rows[0]["pubYear"].ToString();
                                }
                                else
                                {
                                    txtPubYear.Value = string.Empty;
                                }
                            } //730
                              //                            else
                              //                          {
                              //                            if (tmpds.Tables[0].Rows[0].IsNull("catalogdate"))
                              //                          {
                              //                            txtdate.Text = string.Empty;
                              //                      }
                              //                    else
                              //                  {
                              //                    txtdate.Text = string.Format("{0:dd-MMM-yyyy}",Convert.ToDateTime( tmpds.Tables[0].Rows[0]["catalogdate"]));
                              //              }
                              //                                if (tmpds.Tables[0].Rows[0]["status"].ToString() == "NA")
                              //                                {
                              //                                    this.cmbStatus.SelectedIndex = this.cmbStatus.Items.Count - 1;
                              //                                }
                              //                                else
                              //                                {
                              //                                    this.cmbStatus.SelectedIndex = cmbStatus.Items.IndexOf(cmbStatus.Items.FindByValue(tmpds.Tables[0].Rows[0]["status"].ToString()));
                              //                                }
                              //                                string qry_str;
                              //                                qry_str = "select isBardateApllicable FROM ITEMSTATUSMASTER Where Itemstatusid=" + cmbStatus.SelectedItem.Value;
                              //                                var da = new OleDbDataAdapter(qry_str, tmpCon);
                              //                                var ds = new DataSet();
                              //                                da.Fill(ds, "load");
                              //                                // By Kaushal Release Date Not Complesury
                              //                                // --------------------------------------------
                              //                                string isbardate = string.Empty;
                              //                                if (ds.Tables["load"].Rows.Count > 0)
                              //                                {
                              //                                    isbardate = ds.Tables["load"].Rows[0]["isBardateApllicable"].ToString();
                              //                                }
                              //                                if (isbardate == "Y")
                              //                                {
                              //                                    Labelb.Visible = true;
                              //                                    txtrelease.Visible = true;
                              //                                    this.Button1.Visible = true;
                              //                                    if (tmpds.Tables[0].Rows[0].IsNull("ReleaseDate"))
                              //                                    {
                              //                                        this.txtrelease.Value = "";
                              //                                    }
                              //                                    else
                              //                                    {
                              //                                        this.txtrelease.Value = string.Format("{0:dd-MMM-yyyy}", tmpds.Tables[0].Rows[0]["ReleaseDate"]);
                              //                                    }
                              //                                }
                              //                                // IIf(tmpds.Tables(0).Rows(0).Item("ReleaseDate").ToString() = String.Empty, "", Format(tmpds.Tables(0).Rows(0).Item("ReleaseDate"), Me.hrDate.Value))
                              //                                else
                              //                                {
                              //                                    Labelb.Visible = false;
                              //                                    txtrelease.Visible = false;
                              //                                    Button1.Visible = false;
                              //                                }
                              //                                if (tmpds.Tables[0].Rows[0].IsNull("program_id"))
                              //                                {
                              //                                    cmbCourse1.SelectedValue = "0";
                              //                                }
                              //                                else
                              //                                {
                              //                                    cmbCourse1.SelectedValue = tmpds.Tables[0].Rows[0]["program_id"].ToString();
                              //                                }



                            //                                // cmbbookcategory.Value = tmpds.Tables(0).Rows(0).Item("booktype")
                            //                                cmbbookcategory.SelectedValue = tmpds.Tables[0].Rows[0].IsNull("ItemCategoryCode") ? "---Select---" : tmpds.Tables[0].Rows[0]["ItemCategoryCode"].ToString();
                            //                                cmbLanguage.SelectedValue = tmpds.Tables[0].Rows[0].IsNull("language_id") ? "---Select---" : tmpds.Tables[0].Rows[0]["language_id"].ToString();
                            //                                txtPart.Value = tmpds.Tables[0].Rows[0].IsNull("part") ? "" : tmpds.Tables[0].Rows[0]["part"].ToString();
                            //                                this.txtvolno.Value = tmpds.Tables[0].Rows[0].IsNull("volumenumber") ? "" : tmpds.Tables[0].Rows[0]["volumenumber"].ToString();
                            //                                txtSpecialPrice.Value = tmpds.Tables[0].Rows[0]["specialprice"].ToString();
                            //                                cmbdept.SelectedValue = tmpds.Tables[0].Rows[0]["dept"].ToString();
                            //                                this.txtinitpages.Value = tmpds.Tables[0].Rows[0]["initpages"].ToString();
                            //                                txtpages.Value = tmpds.Tables[0].Rows[0]["pages"].ToString();
                            //                                txtparts.Value = tmpds.Tables[0].Rows[0]["parts"].ToString();
                            //                                txtleaves.Value = tmpds.Tables[0].Rows[0]["leaves"].ToString(); 
                            //                                /// If tmpds.Tables(0).Rows(0).Item("boundind") = "s" Then
                            //                                //// tmpds.Tables(0).Rows(0).Item("boundind") = "Soft"
                            //                                //// ElseIf tmpds.Tables(0).Rows(0).Item("boundind") = "h" Then
                            //                                //// tmpds.Tables(0).Rows(0).Item("boundind") = "Hard"
                            //                                //// End If
                            //                                cmbboundind.SelectedIndex = cmbboundind.Items.IndexOf(cmbboundind.Items.FindByText(tmpds.Tables[0].Rows[0]["boundind"].ToString()));
                            //                                this.txttitle.Value = tmpds.Tables[0].Rows[0]["title"].ToString();
                            //                                string sqlstr ="Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'"+ tmpds.Tables[0].Rows[0]["publishercode"].ToString()+ "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'";
                            //                                var cmd = new OleDbCommand(sqlstr, tmpCon);
                            //                                string tmpstr = cmd.ExecuteScalar().ToString();
                            //                                hdPublisherId.Value = tmpds.Tables[0].Rows[0]["publishercode"].ToString();
                            //                                txtCmbPublisher.Text = tmpstr;
                            //                                txtedition.Value = tmpds.Tables[0].Rows[0].IsNull("edition") ? string.Empty : tmpds.Tables[0].Rows[0]["edition"].ToString();
                            //                                if (!tmpds.Tables[0].Rows[0].IsNull("editionyear"))
                            //                                {
                            //                                    txteditionyear.Value = tmpds.Tables[0].Rows[0]["editionyear"].ToString();
                            //                                }
                            //                                else
                            //                                {
                            //                                    txteditionyear.Value = string.Empty;
                            //                                }
                            //                                this.txtisbn.Value = tmpds.Tables[0].Rows[0]["isbn"].ToString();
                            //                                txtSub11.Text = tmpds.Tables[0].Rows[0]["subject1"].ToString();
                            //                                txtsub2.Text = tmpds.Tables[0].Rows[0]["subject2"].ToString();
                            //                                txtsub3.Text = tmpds.Tables[0].Rows[0]["subject3"].ToString();
                            //                                txtbooksize.Value = tmpds.Tables[0].Rows[0]["Booksize"].ToString();
                            //                                txtlccn.Value = tmpds.Tables[0].Rows[0]["LCCN"].ToString();
                            //                                txtvolpages.Value = tmpds.Tables[0].Rows[0]["Volumepages"].ToString();
                            //                                txtbiblpages.Value = tmpds.Tables[0].Rows[0]["biblioPages"].ToString();
                            //                                cboBookIndex.SelectedIndex = cboBookIndex.Items.IndexOf(cboBookIndex.Items.FindByText(tmpds.Tables[0].Rows[0]["bookindex"].ToString()));
                            //                                cboIllistration.SelectedIndex = cboIllistration.Items.IndexOf(cboIllistration.Items.FindByText(tmpds.Tables[0].Rows[0]["illustration"].ToString()));
                            //                                cbovariouspaging.SelectedIndex = cbovariouspaging.Items.IndexOf(cbovariouspaging.Items.FindByText(tmpds.Tables[0].Rows[0]["variouspaging"].ToString()));
                            ////                                txtmaps.Value = tmpds.Tables[0].Rows[0]["maps"]) | Operators.ConditionalCompareObjectEqual(tmpds.Tables[0].Rows[0]["maps"], 0, false), string.Empty, tmpds.Tables[0].Rows[0]["maps"]);
                            //                                cboEditorETAL.SelectedIndex = cboEditorETAL.Items.IndexOf(cboEditorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalEditor"].ToString()));
                            //                                cboCompilerETAL.SelectedIndex = cboCompilerETAL.Items.IndexOf(cboCompilerETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalCompiler"].ToString()));
                            //                                cboILLustratorETAL.SelectedIndex = cboILLustratorETAL.Items.IndexOf(cboILLustratorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalIllus"].ToString()));
                            //                                cboTranslatorETAL.SelectedIndex = cboTranslatorETAL.Items.IndexOf(cboTranslatorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalTrans"].ToString()));
                            //                                cboAuthorETAL.SelectedIndex = cboAuthorETAL.Items.IndexOf(cboAuthorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalAuthor"].ToString()));
                            //                                txtCopyNo.Value = tmpds.Tables[0].Rows[0]["copyNumber"].ToString();
                            //                                txtmaterialinfo.Value = tmpds.Tables[0].Rows[0]["accmaterialhistory"].ToString();
                            //                                txtbookprice.Value = tmpds.Tables[0].Rows[0]["bookprice"].ToString();
                            //                                //// .........................
                            //                                txtDocNo.Value = tmpds.Tables[0].Rows[0]["biilNo"].ToString();
                            //                                txtLoc2.Text = tmpds.Tables[0].Rows[0]["loc_id"].ToString();
                            //                                txtlocation.Text = tmpds.Tables[0].Rows[0]["location"].ToString();

                            //                                //// txtDocDate.Text = IIf(IsDBNull(tmpds.Tables(0).Rows(0).Item("billDate")), String.Empty, Format(tmpds.Tables(0).Rows(0).Item("billDate"), hrDate.Value))
                            //                                if (tmpds.Tables[0].Rows[0].IsNull("billDate"))
                            //                                {
                            //                                    txtDocDate.Text = string.Empty;
                            //                                }
                            //                                else
                            //                                {
                            //                                    txtDocDate.Text = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(tmpds.Tables[0].Rows[0]["billDate"]));
                            //                                }
                            //                                txtVolumeNo.Value = tmpds.Tables[0].Rows[0]["Volume"].ToString();
                            //                                //// Commented By jeetendra Prajapati as on 28 Dec 
                            //                                //// This Field is separated with Accession No. not With Control no
                            //                                cmbtype.SelectedIndex = cmbtype.Items.IndexOf(cmbtype.Items.FindByValue(tmpds.Tables[0].Rows[0]["item_type"].ToString()));
                            //                                var litem = new ListItem();
                            //                                litem.Value = tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString();
                            //                                if (cmbMediaType.Items.Contains(litem) == true)
                            //                                {
                            //                                    cmbMediaType.SelectedValue = tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString();
                            //                                }
                            //                                else
                            //                                {
                            ////                                    cmbMediaType.SelectedValue = 0;// cmbMediaType.Items.Count - 1;
                            //                                }
                            //                                txtIssnNo.Value = tmpds.Tables[0].Rows[0]["issn"].ToString();
                            //                                if (!tmpds.Tables[0].Rows[0].IsNull("pubYear"))
                            //                                {
                            //                                    txtPubYear.Value = tmpds.Tables[0].Rows[0]["pubYear"].ToString();
                            //                                }
                            //                                else
                            //                                {
                            //                                    txtPubYear.Value = string.Empty;
                            //                                }
                            //                                cmbcurr.SelectedIndex = cmbcurr.Items.IndexOf(cmbcurr.Items.FindByText(tmpds.Tables[0].Rows[0]["OriginalCurrency"].ToString())); // IIf(IsDBNull(tmpds.Tables("result").Rows(0).Item("OriginalCurrency")), String.Empty, tmpds.Tables("result").Rows(0).Item("OriginalCurrency"))
                            //                                txtForeignPrice.Value = tmpds.Tables[0].Rows[0]["originalprice"].ToString();
                            //                                //// ''''''''''''''''''jeetu
                            //                                txtCmbVendor.Text = tmpds.Tables[0].Rows[0].IsNull("vendor_source") ? "" : tmpds.Tables[0].Rows[0]["vendor_source"].ToString();
                            //                                //// '''''''''''''''''''''''
                            //                                //cmbvendor.SelectedIndex = (cmbvendor.Items.IndexOf(cmbvendor.Items.FindByText(tmpds.Tables(0).Rows(0).Item("cat_source")))) 'tmpds.Tables(0).Rows(0).Item("cat_source")

                            //                                //// for audit
                            //                                //audaccn.catalogdate = txtdate.Text;
                            //                                //audaccn.Status = cmbStatus.SelectedValue;
                            //                                //audaccn.Item_type = cmbtype.SelectedValue;
                            //                                //if (!string.IsNullOrEmpty(txtrelease.Value))
                            //                                //{
                            //                                //    audaccn.ReleaseDate = txtrelease.Value;

                            //                                //}
                            //                                //audaccn.booktitle = txttitle.Value;
                            //                                //audaccn.Item_type = cmbtype.SelectedValue;
                            //                                //audaccn.Copynumber = Convert.ToInt32(txtCopyNo.Value);
                            //                                //audbookcatg.title = txttitle.Value;
                            //                                //audaccn.ItemCategoryCode = cmbbookcategory.SelectedValue;
                            //                                //audbookcatg.language_id = cmbLanguage.SelectedValue;

                            //                                //audbookcatg.part = txtPart.Value;
                            //                                //audbookcatg.volumenumber = txtvolno.Value;
                            //                                //audaccn.DeptCode = cmbdept.SelectedValue;
                            //                                //if (!string.IsNullOrEmpty(Conversions.ToString(tmpds.Tables[0].Rows[0]["ipaddress"])))
                            //                                //{
                            //                                //    audaccn.IpAddress = tmpds.Tables[0].Rows[0]["ipaddress"];
                            //                                //}
                            //                                //if (!string.IsNullOrEmpty(Conversions.ToString(tmpds.Tables[0].Rows[0]["userid"])))
                            //                                //{
                            //                                //    audaccn.userid = tmpds.Tables[0].Rows[0]["userid"];
                            //                                //}
                            //                                //audaccn.accessionnumber = txtacc.Value;
                            //                                //audaccn.bookprice = txtbookprice.Value;
                            //                                //audaccn.indentnumber = txtDocNo.Value;
                            //                                //if (string.IsNullOrEmpty(txtDocDate.Text))
                            //                                //{
                            //                                //    audaccn.billDate = txtDocDate.Text;
                            //                                //}
                            //                                //audbookcatg.pages = txtpages.Value;
                            //                                //audbookcatg.parts = txtparts.Value;
                            //                                //audaccn.Loc_id = txtLoc2.Text;
                            //                                //audaccn.booktitle = txttitle.Value;
                            //                                //audaccn.vendor_source = txtCmbVendor.Text;
                            //                                //audbookcatg.title = txttitle.Value;
                            //                                //audbookcatg.publishercode = hdPublisherId.Value;
                            //                                //audbookcatg.edition = txtedition.Value;
                            //                                //if (!string.IsNullOrEmpty(txteditionyear.Value))
                            //                                //{
                            //                                //    audaccn.editionyear = Convert.ToInt32(txteditionyear.Value);
                            //                                //}
                            //                                //audbookcatg.isbn = txtisbn.Value;
                            //                                //audbookcatg.subject1 = txtSub11.Text;
                            //                                //audbookcatg.subject2 = txtsub2.Text;
                            //                                //audbookcatg.subject3 = txtsub3.Text;

                            //                                //audaccn.OriginalPrice = txtbookprice.Value;
                            //                                //audaccn.biilNo = txtDocNo.Value;
                            //                                //audaccn.billDate = txtDocDate.Text;
                            //                                //audaccn.Status = cmbStatus.SelectedValue;
                            //                                //audaccn.BookNumber = txtbookno.Value;
                            //                                //audbookcatg.Volume = txtVolumeNo.Value;
                            //                                //audaccn.OriginalCurrency = cmbcurr.SelectedValue;
                            //                                //audbookcatg.materialdesignation = cmbMediaType.SelectedValue;
                            //                                //audbookcatg.issn = txtIssnNo.Value;
                            //                                //audaccn.vendor_source = txtCmbVendor.Text;
                            //                                //if (!string.IsNullOrEmpty(txtPubYear.Value))
                            //                                //{
                            //                                //    audaccn.pubYear = IIf(txtPubYear.Value == "", 0, Convert.ToInt32(txtPubYear.Value));

                            //                                //}

                            //                            }


                            //                            hdnInsUpd.Value = "U";
                            //                            // =================================
                            //                            cmdPrint.Visible = true;
                        }
                        //435                       // =================================
                        ///   this.image1.Src = "imagehiddenform.aspx?cno=" + Trim(hdCtrlNo.Value.ToString);
                        // =====================================

                        var tmpda5 = new OleDbDataAdapter("Select classnumber,booknumber from catalogdata where ctrl_no=" + hdCtrlNo.Value, tmpCon);
                        var tmpds5 = new DataSet();
                        tmpda5.Fill(tmpds5, "detail");
                        if (tmpds5.Tables["detail"].Rows.Count > 0)
                        {
                            this.txtclassno.Value = tmpds5.Tables["detail"].Rows[0][0].ToString();
                            //    audcatdata.classnumber = txtclassno.Value;

                            this.txtbookno.Value = tmpds5.Tables["detail"].Rows[0][1].ToString();
                            //    audcatdata.booknumber = txtbookno.Value;
                        }
                        //tmpda5.Dispose();
                        //tmpds5.Dispose();
                        if (hdBookNumAccn.Value == "1")
                        {
                            var dtBook = new DataTable();
                            tmpda5 = new OleDbDataAdapter("select isnull(booknumber,'') from bookaccessionmaster where accessionnumber='" + txtacc.Text + "'", tmpCon);
                            tmpda5.Fill(dtBook);
                            this.txtbookno.Value = dtBook.Rows[0][0].ToString();
                            //    audaccn.BookNumber = txtbookno.Value;

                        }
                        var tmpda1 = new OleDbDataAdapter("SELECT * from bookauthor where ctrl_no=" + hdCtrlNo.Value, tmpCon);
                        var tmpds1 = new DataSet();
                        tmpda1.Fill(tmpds1, "Author");
                        if (tmpds1.Tables["Author"].Rows.Count > 0)
                        {
                            this.txtau1firstnm.Value = tmpds1.Tables["Author"].Rows[0][1].ToString();
                            this.txtau1midnm.Value = tmpds1.Tables["Author"].Rows[0][2].ToString();
                            this.txtau1surnm.Value = tmpds1.Tables["Author"].Rows[0][3].ToString();

                            this.txtau2firstnm.Value = tmpds1.Tables["Author"].Rows[0][4].ToString();
                            this.txtau2midnm.Value = tmpds1.Tables["Author"].Rows[0][5].ToString();
                            this.txtau2surnm.Value = tmpds1.Tables["Author"].Rows[0][6].ToString();

                            this.txtau3firstnm.Value = tmpds1.Tables["Author"].Rows[0][7].ToString();
                            this.txtau3midnm.Value = tmpds1.Tables["Author"].Rows[0][8].ToString();
                            this.txtau3surnm.Value = tmpds1.Tables["Author"].Rows[0][9].ToString();
                            this.txtUniformTitle.Value = tmpds1.Tables["Author"].Rows[0]["UniFormTitle"].ToString();
                            //    audbookauth.firstname1 = txtau1firstnm.Value;
                            //    audbookauth.middlename1 = txtau1midnm.Value;
                            //    audbookauth.lastname1 = txtau1surnm.Value;

                            //    audbookauth.firstname1 = txtau2firstnm.Value;
                            //    audbookauth.middlename1 = txtau2midnm.Value;
                            //    audbookauth.lastname1 = txtau2surnm.Value;
                            //    audbookauth.firstname1 = txtau3firstnm.Value;
                            //    audbookauth.middlename1 = txtau3midnm.Value;
                            //    audbookauth.lastname1 = txtau3surnm.Value;
                            //    audconf.Subtitle = txtSubtitle.Value;

                        }
                        tmpda1.Dispose();
                        tmpds1.Dispose();
                        var tmpda2 = new OleDbDataAdapter("SELECT * from bookseries where ctrl_no=" + hdCtrlNo.Value, tmpCon);
                        var tmpds2 = new DataSet();
                        tmpda2.Fill(tmpds2, "Series");
                        if (tmpds2.Tables["Series"].Rows.Count > 0)
                        {
                            this.txtseriesname.Value = tmpds2.Tables["Series"].Rows[0][1].ToString();
                            this.txtseriesno.Value = tmpds2.Tables["Series"].Rows[0][2].ToString();
                            this.txtseriespart.Value = tmpds2.Tables["Series"].Rows[0][3].ToString();
                            // txtSVolume.Value = tmpds2.Tables(0).Rows(0).Item("Svolume")) Or tmpds2.Tables(0).Rows(0).Item("Svolume") = 0, String.Empty, tmpds2.Tables(0).Rows(0).Item("Svolume"))
                            if (!tmpds2.Tables[0].Rows[0].IsNull("Svolume"))
                            {
                                txtSVolume.Value = tmpds2.Tables[0].Rows[0]["Svolume"].ToString();
                            }
                            else
                            {
                                txtSVolume.Value = string.Empty;
                            }


                            this.status.SelectedIndex = status.Items.IndexOf(status.Items.FindByText(tmpds2.Tables[0].Rows[0]["etal"].ToString()));
                            af1.Value = tmpds2.Tables[0].Rows[0]["af1"].ToString();
                            am1.Value = tmpds2.Tables[0].Rows[0]["am1"].ToString();
                            al1.Value = tmpds2.Tables[0].Rows[0]["al1"].ToString();
                            af2.Value = tmpds2.Tables[0].Rows[0]["af2"].ToString();
                            am2.Value = tmpds2.Tables[0].Rows[0]["am2"].ToString();
                            al2.Value = tmpds2.Tables[0].Rows[0]["al2"].ToString();
                            af3.Value = tmpds2.Tables[0].Rows[0]["af3"].ToString();
                            am3.Value = tmpds2.Tables[0].Rows[0]["am3"].ToString();
                            al3.Value = tmpds2.Tables[0].Rows[0]["al3"].ToString();
                            this.txtSecondSeriesTitle.Value = tmpds2.Tables["Series"].Rows[0]["SSeriesName"].ToString();
                            this.txtSecondSeriesNo.Value = tmpds2.Tables["Series"].Rows[0]["SseriesNo"].ToString();
                            this.txtSecondSeriesPart.Value = tmpds2.Tables["Series"].Rows[0]["SseriesPart"].ToString();
                            if (!tmpds2.Tables[0].Rows[0].IsNull("SSvolume"))
                            {
                                txtsecSeriesVol.Value = tmpds2.Tables[0].Rows[0]["SSvolume"].ToString();
                            }
                            else
                            {
                                txtsecSeriesVol.Value = string.Empty;
                            }
                            cmbSecetal.SelectedIndex = cmbSecetal.Items.IndexOf(cmbSecetal.Items.FindByText(tmpds2.Tables[0].Rows[0]["Setal"].ToString()));
                            txtSecFirstName1.Value = tmpds2.Tables[0].Rows[0]["Saf1"].ToString();
                            txtSecMidName1.Value = tmpds2.Tables[0].Rows[0]["Sam1"].ToString();
                            txtSecLastName1.Value = tmpds2.Tables[0].Rows[0]["Sal1"].ToString();
                            txtSecFirstName2.Value = tmpds2.Tables[0].Rows[0]["Saf2"].ToString();
                            txtSecMidName2.Value = tmpds2.Tables[0].Rows[0]["Sam2"].ToString();
                            txtSecLastName2.Value = tmpds2.Tables[0].Rows[0]["Sal2"].ToString();
                            txtSecFirstName3.Value = tmpds2.Tables[0].Rows[0]["Saf3"].ToString();
                            txtSecMidName3.Value = tmpds2.Tables[0].Rows[0]["Sam3"].ToString();
                            txtSecLastName3.Value = tmpds2.Tables[0].Rows[0]["Sal3"].ToString();

                            txtPTitle.Value = tmpds2.Tables[0].Rows[0]["SeriesParallelTitle"].ToString();
                            txtSubPTitle.Value = tmpds2.Tables[0].Rows[0]["SubSeriesParallelTitle"].ToString();
                            txtSecondParallelTitle.Value = tmpds2.Tables[0].Rows[0]["SSeriesParallelTitle"].ToString();
                            txtMainissn.Value = tmpds2.Tables[0].Rows[0]["ISSNMain"].ToString();
                            txtSubissn.Value = tmpds2.Tables[0].Rows[0]["ISSNSub"].ToString();
                            txtSecondissn.Value = tmpds2.Tables[0].Rows[0]["ISSNSecond"].ToString();
                            txtSubseriesname.Value = tmpds2.Tables["Series"].Rows[0]["SubSeriesName"].ToString();
                            txtSubseriesno.Value = tmpds2.Tables["Series"].Rows[0]["SubseriesNo"].ToString();
                            txtSubseriespart.Value = tmpds2.Tables["Series"].Rows[0]["SubSeriesPart"].ToString();
                            // txtSubSVolume.Value = tmpds2.Tables(0).Rows(0).Item("SubSvolume")) Or tmpds2.Tables(0).Rows(0).Item("SubSvolume") = 0, String.Empty, tmpds2.Tables(0).Rows(0).Item("SubSvolume"))

                            if (!tmpds2.Tables[0].Rows[0].IsNull("SubSvolume"))
                            {
                                txtSubSVolume.Value = tmpds2.Tables[0].Rows[0]["SubSvolume"].ToString();
                            }
                            else
                            {
                                txtSubSVolume.Value = string.Empty;
                            }

                            this.Substatus.SelectedIndex = status.Items.IndexOf(status.Items.FindByText(tmpds2.Tables[0].Rows[0]["Subetal"].ToString()));
                            Subaf1.Value = tmpds2.Tables[0].Rows[0]["Subaf1"].ToString();
                            Subam1.Value = tmpds2.Tables[0].Rows[0]["Subam1"].ToString();
                            Subal1.Value = tmpds2.Tables[0].Rows[0]["Subal1"].ToString();
                            Subaf2.Value = tmpds2.Tables[0].Rows[0]["Subaf2"].ToString();
                            Subam2.Value = tmpds2.Tables[0].Rows[0]["Subam2"].ToString();
                            Subal2.Value = tmpds2.Tables[0].Rows[0]["Subal2"].ToString();
                            Subaf3.Value = tmpds2.Tables[0].Rows[0]["Subaf3"].ToString();
                            Subam3.Value = tmpds2.Tables[0].Rows[0]["Subam3"].ToString();
                            Subal3.Value = tmpds2.Tables[0].Rows[0]["Subal3"].ToString();
                            txtSubPTitle.Value = tmpds2.Tables[0].Rows[0]["SubSeriesParallelTitle"].ToString();
                        }
                        tmpda2.Dispose();
                        tmpds2.Dispose();
                        var tmpda3 = new OleDbDataAdapter("SELECT * from bookrelators where ctrl_no=" + hdCtrlNo.Value, tmpCon);
                        var tmpds3 = new DataSet();
                        tmpda3.Fill(tmpds3, "Relators");
                        //if (tmpds3.Tables["Relators"].Rows.Count > 0)
                        //{
                        //    this.editor1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][1]), string.Empty, tmpds3.Tables[0].Rows[0][1]);
                        //    this.editor1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][2]), string.Empty, tmpds3.Tables[0].Rows[0][2]);
                        //    this.editor1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][3]), string.Empty, tmpds3.Tables[0].Rows[0][3]);

                        //    this.editor2fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][4]), string.Empty, tmpds3.Tables[0].Rows[0][4]);
                        //    this.editor2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][5]), string.Empty, tmpds3.Tables[0].Rows[0][5]);
                        //    this.editor2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][6]), string.Empty, tmpds3.Tables[0].Rows[0][6]);

                        //    this.editor3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][7]), string.Empty, tmpds3.Tables[0].Rows[0][7]);
                        //    this.editor3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][8]), string.Empty, tmpds3.Tables[0].Rows[0][8]);
                        //    this.editor3Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][9]), string.Empty, tmpds3.Tables[0].Rows[0][9]);

                        //    this.compiler1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][10]), string.Empty, tmpds3.Tables[0].Rows[0][10]);
                        //    this.compiler1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][11]), string.Empty, tmpds3.Tables[0].Rows[0][11]);
                        //    this.compiler1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][12]), string.Empty, tmpds3.Tables[0].Rows[0][12]);

                        //    this.compiler2Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][13]), string.Empty, tmpds3.Tables[0].Rows[0][13]);
                        //    this.compiler2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][14]), string.Empty, tmpds3.Tables[0].Rows[0][14]);
                        //    this.compiler2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][15]), string.Empty, tmpds3.Tables[0].Rows[0][15]);

                        //    this.compiler3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][16]), string.Empty, tmpds3.Tables[0].Rows[0][16]);
                        //    this.compiler3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][17]), string.Empty, tmpds3.Tables[0].Rows[0][17]);
                        //    this.compiler3Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][18]), string.Empty, tmpds3.Tables[0].Rows[0][18]);

                        //    this.Illustrator1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][19]), string.Empty, tmpds3.Tables[0].Rows[0][19]);
                        //    this.Illustrator1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][20]), string.Empty, tmpds3.Tables[0].Rows[0][20]);
                        //    this.Illustrator1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][21]), string.Empty, tmpds3.Tables[0].Rows[0][21]);

                        //    this.Illustrator2Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][22]), string.Empty, tmpds3.Tables[0].Rows[0][22]);
                        //    this.Illustrator2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][23]), string.Empty, tmpds3.Tables[0].Rows[0][23]);
                        //    this.Illustrator2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][24]), string.Empty, tmpds3.Tables[0].Rows[0][24]);

                        //    this.Illustrator3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][25]), string.Empty, tmpds3.Tables[0].Rows[0][25]);
                        //    this.Illustrator3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][26]), string.Empty, tmpds3.Tables[0].Rows[0][26]);
                        //    this.Illustrator3lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][27]), string.Empty, tmpds3.Tables[0].Rows[0][27]);

                        //    this.Translator1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][28]), string.Empty, tmpds3.Tables[0].Rows[0][28]);
                        //    this.Translator1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][29]), string.Empty, tmpds3.Tables[0].Rows[0][29]);
                        //    this.Translator1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][30]), string.Empty, tmpds3.Tables[0].Rows[0][30]);

                        //    this.Translator2Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][31]), string.Empty, tmpds3.Tables[0].Rows[0][31]);
                        //    this.Translator2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][32]), string.Empty, tmpds3.Tables[0].Rows[0][32]);
                        //    this.Translator2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][33]), string.Empty, tmpds3.Tables[0].Rows[0][33]);

                        //    this.Translator3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][34]), string.Empty, tmpds3.Tables[0].Rows[0][34]);
                        //    this.Translator3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][35]), string.Empty, tmpds3.Tables[0].Rows[0][35]);
                        //    this.Translator3Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][36]), string.Empty, tmpds3.Tables[0].Rows[0][36]);

                        //}
                        tmpda3.Dispose();
                        tmpds3.Dispose();

                        var tmpda4 = new OleDbDataAdapter("SELECT * from bookconference where ctrl_no=" + hdCtrlNo.Value, tmpCon);
                        var tmpds4 = new DataSet();
                        tmpda4.Fill(tmpds4, "Conference");
                        if (tmpds4.Tables["Conference"].Rows.Count > 0)
                        {
                            this.txtSubtitle.Value = tmpds4.Tables["Conference"].Rows[0][1].ToString();
                            this.txtParallelTitle.Value = tmpds4.Tables["Conference"].Rows[0][2].ToString();
                            this.txtConferenceName.Value = tmpds4.Tables["Conference"].Rows[0][3].ToString();
                            //                                this.txtConferenceYear.Value = IIf(Operators.ConditionalCompareObjectEqual(tmpds4.Tables["Conference"].Rows[0][4], string.Empty, false), string.Empty, tmpds4.Tables[0].Rows[0][4]);
                            //    this.txtBN.Value = tmpds4.Tables["Conference"].Rows[0][5]), string.Empty, tmpds4.Tables[0].Rows[0][5]);
                            //  this.txtcn.Value = tmpds4.Tables["Conference"].Rows[0][6]), string.Empty, tmpds4.Tables[0].Rows[0][6]);
                            //   this.txtgn.Value = tmpds4.Tables["Conference"].Rows[0][7]), string.Empty, tmpds4.Tables[0].Rows[0][7]);
                            //   this.txtvn.Value = tmpds4.Tables["Conference"].Rows[0][8]), string.Empty, tmpds4.Tables[0].Rows[0][8]);
                            //  this.txtsn.Value = tmpds4.Tables["Conference"].Rows[0][9]), string.Empty, tmpds4.Tables[0].Rows[0][9]);
                            // txtan.Value = tmpds4.Tables["Conference"].Rows[0]["ANNotes"]), string.Empty, tmpds4.Tables[0].Rows[0]["ANNotes"]);
                            //  audconf.Subtitle = txtSubtitle.Value;

                            // txtaname1.Value = tmpds4.Tables["Conference"].Rows[0]["AdFname1"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdFname1"]);
                            //   txtaname2.Value = tmpds4.Tables["Conference"].Rows[0]["AdMname1"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdMname1"]);
                            //  txtaname3.Value = tmpds4.Tables["Conference"].Rows[0]["AdLname1"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdLname1"]);

                            // txtfname2.Value = tmpds4.Tables["Conference"].Rows[0]["AdFname2"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdFname2"]);
                            //  txtmname2.Value = tmpds4.Tables["Conference"].Rows[0]["AdMname2"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdMname2"]);
                            // txtlname2.Value = tmpds4.Tables["Conference"].Rows[0]["AdLname2"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdLname2"]);

                            //   txtfname3.Value = tmpds4.Tables["Conference"].Rows[0]["AdFname3"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdFname3"]);
                            //  txtmname3.Value = tmpds4.Tables["Conference"].Rows[0]["AdMname3"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdMname3"]);
                            // txtaname9.Value = tmpds4.Tables["Conference"].Rows[0]["AdLName3"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdLName3"]);
                            // txtnarration.Value = tmpds4.Tables["Conference"].Rows[0]["abstract"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["abstract"]);
                            // txtProgram.Value = tmpds4.Tables["Conference"].Rows[0]["program_name"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["program_name"]);
                        }
                        //// To Populate dataGrid with available copies of book
                        cmdsave1.Value = "Update";

                        var CopyDs = new DataSet();
                        CopyDs = LibObj.PopulateDataset("select accessionnumber,copyNumber as copyno ,editionyear as year,pubYear,specialprice,bookprice,catalogdate,biilNo as DocNo,billDate as DocDate,OriginalCurrency  ,originalprice as OriginalPrice,cat_source, (location+','+cast(loc_id as varchar)  )  location,loc_id,Author from cataloguecardview where ctrl_no=" + hdCtrlNo.Value + " and accessionnumber<>N'" + txtacc.Text + "' order by copyno ", "Copy", tmpCon);

                        // notice - grdcopy is DataGrid and javascript is failing to get hidden field so id is included in location text
                        grdcopy.DataSource = CopyDs.Tables["Copy"];
                        grdcopy.DataBind();
                        int i1;
                        var loopTo = grdcopy.Items.Count - 1;
                        for (i1 = 0; i1 <= loopTo; i1++)
                        {
                            string str;
                            str = Convert.ToString(CopyDs.Tables[0].Rows[i1]["OriginalCurrency"]);
                            var cmb = new DropDownList();
                            cmb = (DropDownList)grdcopy.Items[i1].FindControl("cmbOriCurrency");
                            LibObj.populateDDL(cmb, "select distinct currencycode,CurrencyName from ExchangeMaster order by CurrencyName", "CurrencyName", "Currencycode", HComboSelect.Value, tmpCon);
                            cmb.Items.Remove(cmb.Items[cmb.Items.Count - 1]);
                            cmb.SelectedIndex = cmb.Items.IndexOf(cmb.Items.FindByText(str));

                            var cmb1 = new DropDownList();
                            cmb1 = (DropDownList)grdcopy.Items[i1].FindControl("cmbCourse");
                            LibObj.populateDDL(cmb1, "Select distinct program_id, Program_Master.program_name as programname from Program_Master order by program_name", "programname", "program_id", this.HComboSelect.Value, tmpCon);

                            bookcatalogcom.Parameters.Clear();
                            bookcatalogcom.CommandType = CommandType.Text;
                            bookcatalogcom.CommandText = "select program_id from bookaccessionmaster where accessionnumber='" + grdcopy.Items[i1].Cells[1].Text + "'";
                            cmb1.SelectedValue = "0";
                            try
                            {
                                cmb1.SelectedValue = bookcatalogcom.ExecuteScalar().ToString();
                                HiddenField shdcmbcourse = (HiddenField)grdcopy.Items[i1].FindControl("hdcmbcourse");
                                shdcmbcourse.Value = cmb1.SelectedValue;

                            }
                            catch { }

                            var cmbState = new DropDownList();
                            cmbState = (DropDownList)grdcopy.Items[i1].FindControl("cmbStatus");
                            LibObj.populateDDL(cmbState, "select ItemStatusID,ItemStatus from Itemstatusmaster order by itemstatus", "ItemStatus", "ItemStatusID", this.HComboSelect.Value, tmpCon);
                            bookcatalogcom.Parameters.Clear();
                            bookcatalogcom.CommandType = CommandType.Text;
                            bookcatalogcom.CommandText = "select Status from bookaccessionmaster where accessionnumber='" + grdcopy.Items[i1].Cells[1].Text + "'";
                            cmbState.SelectedValue = "0";
                            try
                            {
                                cmbState.SelectedValue = bookcatalogcom.ExecuteScalar().ToString();
                                HiddenField shdcmbStatus = (HiddenField)grdcopy.Items[i1].FindControl("hdcmbStatus");
                                shdcmbStatus.Value = cmbState.SelectedValue;
                            }
                            catch { }
                            var CmbGrdDeptObj = new DropDownList();
                            CmbGrdDeptObj = (DropDownList)this.grdcopy.Items[i1].FindControl("CmbGrdDept");
                            LibObj.populateDDL(CmbGrdDeptObj, "select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "departmentname", "departmentcode", this.HComboSelect.Value, tmpCon);
                            bookcatalogcom.Parameters.Clear();
                            bookcatalogcom.CommandType = CommandType.Text;
                            bookcatalogcom.CommandText = "select DeptCode from bookaccessionmaster where accessionnumber='" + grdcopy.Items[i1].Cells[1].Text + "'";
                            CmbGrdDeptObj.SelectedValue = cmbdept.SelectedValue;
                            try
                            {
                                CmbGrdDeptObj.SelectedValue = bookcatalogcom.ExecuteScalar().ToString();
                                HiddenField shdCmbGrdDept = (HiddenField)grdcopy.Items[i1].FindControl("hdCmbGrdDept");
                                shdCmbGrdDept.Value = CmbGrdDeptObj.SelectedValue;

                            }
                            catch { }
                            // ----------Set item Type for every Book--------- Kaushal


                            var CmbItemType = new DropDownList();
                            CmbItemType = (DropDownList)grdcopy.Items[i1].FindControl("CmbItemType");
                            LibObj.populateDDL(CmbItemType, "select Item_Type as ITVal,Item_Type as ITText from Item_Type  order by Item_Type", "ITText", "ITVal", HComboSelect.Value, bookcatalogcon);
                            // CmbItemType.SelectedValue = ""           
                            bookcatalogcom.Parameters.Clear();
                            bookcatalogcom.CommandType = CommandType.Text;
                            bookcatalogcom.CommandText = "select Item_Type from bookaccessionmaster where accessionnumber='" + grdcopy.Items[i1].Cells[1].Text + "'";
                            CmbItemType.SelectedValue = Resources.ValidationResources.ComboSelect;
                            try
                            {
                                CmbItemType.SelectedValue = bookcatalogcom.ExecuteScalar().ToString();
                                HiddenField shdCmbItemType = (HiddenField)grdcopy.Items[i1].FindControl("hdCmbItemType");
                                shdCmbItemType.Value = CmbItemType.SelectedValue;

                            }
                            catch { }
                            // by Kaushal
                            // '********************************
                            var CatItemType = new DropDownList();
                            CatItemType = (DropDownList)grdcopy.Items[i1].FindControl("CatItemType");
                            LibObj.populateDDL(CatItemType, "select id as CVal,category_Loadingstatus as CText from categoryLoadingstatus where 0=0 order by category_Loadingstatus", "CText", "CVal", HComboSelect.Value, bookcatalogcon);
                            bookcatalogcom.Parameters.Clear();
                            bookcatalogcom.CommandType = CommandType.Text;
                            bookcatalogcom.CommandText = "select ItemCategoryCode from bookaccessionmaster where accessionnumber='" + grdcopy.Items[i1].Cells[1].Text + "'";
                            CatItemType.SelectedValue = Resources.ValidationResources.ComboSelect;
                            try
                            {
                                CatItemType.SelectedValue = bookcatalogcom.ExecuteScalar().ToString();
                                HiddenField shdCatItemType = (HiddenField)grdcopy.Items[i1].FindControl("hdCatItemType");
                                shdCatItemType.Value = CatItemType.SelectedValue;

                            }
                            catch { }
                            // '********************************
                            // Dim shdcmbcourse As HiddenField = CType(grdcopy.Items(i1).FindControl("hdcmbcourse"), HiddenField)
                            // Dim shdcmbStatus As HiddenField = CType(grdcopy.Items(i1).FindControl("hdcmbStatus"), HiddenField)
                            // Dim shdCmbGrdDept As HiddenField = CType(grdcopy.Items(i1).FindControl("hdCmbGrdDept"), HiddenField)
                            // Dim shdCmbItemType As HiddenField = CType(grdcopy.Items(i1).FindControl("hdCmbItemType"), HiddenField)

                        }
                        //lsaudaccn.Add(audaccn);
                        //lsaudbookcatg.Add(audbookcatg);
                        //lsaudbookauth.Add(audbookauth);
                        //lsaudcatdata.Add(audcatdata);
                        //lsaudconf.Add(audconf);

                        //var audc = new Audit.UpdCatalog();
                        //audc.lsAccnb4 = lsaudaccn;
                        //audc.lsCatloagb4 = lsaudbookcatg;
                        //audc.lsauthb4 = lsaudbookauth;
                        //audc.lscatagdatab4 = lsaudcatdata;
                        //audc.lsbookconf4 = lsaudconf;
                        //Session["auditdata"] = audc;
                    }

                    else if (Session["back"].ToString() == "catalog")
                    {
                        Hidden1.Value = "282";
                        string AccessioningType = string.Empty; //To store the value that whether the selected accession no is generated through existing book accessioning or through new arrival
                        int CopyNoStart;
                        var acccon = new OleDbConnection(retConstr(""));
                        acccon.Open();
                        ///   if selected accession number has come from existing accessioning process 
                        string selectqry = string.Empty;
                        string strQu = "select released from bookaccessionmaster where accessionnumber=N'" + Session["accno"].ToString() + "'";
                        var ad = new OleDbDataAdapter(strQu, acccon);
                        var ds = new DataSet();
                        var sr = default(string);
                        ad.Fill(ds, "fill");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sr = ds.Tables[0].Rows[0][0].ToString();
                        } //Value is set to y when accessioning is done from indent path and is set to n when aceesioning is done from gift indent path
                        if (sr == "e")
                        {
                            AccessioningType = "E";
                            var davendor = new OleDbDataAdapter(("select vendor_id from existingbookkinfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN (N'---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, existingbookkinfo.volumeno, existingbookkinfo.category, existingbookkinfo.title, existingbookkinfo.authortype, existingbookkinfo.firstname1, existingbookkinfo.middlename1, existingbookkinfo.lastname1, existingbookkinfo.firstname2, existingbookkinfo.middlename2, existingbookkinfo.lastname2, existingbookkinfo.firstname3, existingbookkinfo.middlename3, existingbookkinfo.lastname3, existingbookkinfo.publisherid, existingbookkinfo.edition, existingbookkinfo.yearofedition, existingbookkinfo.isbn, existingbookkinfo.noofcopies,existingbookkinfo.price,subtitle=existingbookkinfo.subtitle,existingbookkinfo.yearofPublication,bookaccessionmaster.deptCode  as dept,language_id,existingbookkinfo.mediatype,part as part,seriesname as series,no_of_pages as pages ,page_size as size,existingbookkinfo.specialprice, adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, existingbookkinfo.vendor_id, bookaccessionmaster.biilNo, bookaccessionmaster.billDate,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM existingbookkinfo inner join bookaccessionmaster on existingbookkinfo.srNoOld = bookaccessionmaster.srNoOld inner join media_type on existingbookkinfo.mediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, existingbookkinfo.volumeno, existingbookkinfo.category, existingbookkinfo.title, existingbookkinfo.authortype, existingbookkinfo.firstname1, existingbookkinfo.middlename1, existingbookkinfo.lastname1, existingbookkinfo.firstname2, existingbookkinfo.middlename2, existingbookkinfo.lastname2, existingbookkinfo.firstname3, existingbookkinfo.middlename3, existingbookkinfo.lastname3, existingbookkinfo.publisherid, existingbookkinfo.edition, existingbookkinfo.yearofedition, existingbookkinfo.isbn, existingbookkinfo.noofcopies,existingbookkinfo.price,subtitle=existingbookkinfo.subtitle,existingbookkinfo.yearofPublication,bookaccessionmaster.deptcode  as dept,language_id,existingbookkinfo.mediatype,part as part,seriesname as series,no_of_pages as pages ,page_size as size,existingbookkinfo.specialprice, adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, existingbookkinfo.Vendor_id,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location  FROM existingbookkinfo inner join bookaccessionmaster on existingbookkinfo.srNoOld = bookaccessionmaster.srNoOld inner join media_type on existingbookkinfo.mediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }
                        else if (sr == "y")
                        {
                            AccessioningType = "N";
                            var davendor = new OleDbDataAdapter(("select vendor_id from existingbookkinfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN ('---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, indentmaster.volumeno, indentmaster.category, bookaccessionmaster.booktitle, indentmaster.authortype, indentmaster.firstname1, indentmaster.middlename1, indentmaster.lastname1, indentmaster.firstname2, indentmaster.middlename2, indentmaster.lastname2, indentmaster.firstname3, indentmaster.middlename3, indentmaster.lastname3, indentmaster.publisherid, indentmaster.edition, indentmaster.yearofedition, indentmaster.isbn, ModifiedOrderMaster.noofcopies,bookaccessionmaster.bookprice,subtitle=indentmaster.subtitle,indentmaster.yearofPublication,indentmaster.departmentcode as dept,language_id,media_id,Vpart as part,seriesname as series,pages='',size='',specialprice='', adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, indentmaster.vendorid as Vendor_id, bookaccessionmaster.biilNo, bookaccessionmaster.billDate,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN ModifiedOrderMaster ON bookaccessionmaster.srno = ModifiedOrderMaster.srno INNER JOIN indentmaster ON ModifiedOrderMaster.indentnumber = indentmaster.indentid inner join media_type on indentmaster.mediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, indentmaster.volumeno, indentmaster.category, bookaccessionmaster.booktitle, indentmaster.authortype, indentmaster.firstname1, indentmaster.middlename1, indentmaster.lastname1, indentmaster.firstname2, indentmaster.middlename2, indentmaster.lastname2, indentmaster.firstname3, indentmaster.middlename3, indentmaster.lastname3, indentmaster.publisherid, indentmaster.edition, indentmaster.yearofedition, indentmaster.isbn, ModifiedOrderMaster.noofcopies,bookaccessionmaster.bookprice,subtitle=indentmaster.subtitle,indentmaster.yearofPublication,indentmaster.departmentcode as dept,language_id,media_id,Vpart as part,seriesname as series,pages='',size='',specialprice='', adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, indentmaster.vendorid as Vendor_id,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN ModifiedOrderMaster ON bookaccessionmaster.srno = ModifiedOrderMaster.srno INNER JOIN indentmaster ON ModifiedOrderMaster.indentnumber = indentmaster.indentid inner join media_type on indentmaster.mediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }
                        else if (sr == "G")
                        {
                            AccessioningType = "G";
                            var davendor = new OleDbDataAdapter(("select vendor_id from existingbookkinfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN ('---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, giftindentmaster.volumeno, giftindentmaster.category, ModifiedOrderMaster.title, giftindentmaster.authortype, giftindentmaster.firstname1, giftindentmaster.middlename1, giftindentmaster.lastname1, giftindentmaster.firstname2, giftindentmaster.middlename2, giftindentmaster.lastname2, giftindentmaster.firstname3, giftindentmaster.middlename3, giftindentmaster.lastname3, giftindentmaster.publisherid, giftindentmaster.edition, giftindentmaster.yearofedition, giftindentmaster.isbn, ModifiedOrderMaster.noofcopies,bookaccessionmaster.bookprice,subtitle=giftindentmaster.subtitle ,giftindentmaster.yearofPublication,departmentcode as dept,language_id,media_id, Vpart as part, seriesname as series,pages='',size='',specialprice='', adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, giftindentmaster.giftedby as Vendor_id, bookaccessionmaster.biilNo, bookaccessionmaster.billDate,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN ModifiedOrderMaster ON bookaccessionmaster.srno = ModifiedOrderMaster.srno INNER JOIN giftindentmaster ON ModifiedOrderMaster.indentnumber = giftindentmaster.giftindentid inner join media_type on giftindentmaster.giftmediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, giftindentmaster.volumeno, giftindentmaster.category, ModifiedOrderMaster.title, giftindentmaster.authortype, giftindentmaster.firstname1, giftindentmaster.middlename1, giftindentmaster.lastname1, giftindentmaster.firstname2, giftindentmaster.middlename2, giftindentmaster.lastname2, giftindentmaster.firstname3, giftindentmaster.middlename3, giftindentmaster.lastname3, giftindentmaster.publisherid, giftindentmaster.edition, giftindentmaster.yearofedition, giftindentmaster.isbn, ModifiedOrderMaster.noofcopies,bookaccessionmaster.bookprice,subtitle=giftindentmaster.subtitle ,giftindentmaster.yearofPublication,departmentcode as dept,language_id,media_id, Vpart as part, seriesname as series,pages='',size='',specialprice='', adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, giftindentmaster.giftedby as Vendor_id,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN ModifiedOrderMaster ON bookaccessionmaster.srno = ModifiedOrderMaster.srno INNER JOIN giftindentmaster ON ModifiedOrderMaster.indentnumber = giftindentmaster.giftindentid inner join media_type on giftindentmaster.giftmediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }
                        else if (sr == "i")
                        {
                            AccessioningType = "i";
                            var davendor = new OleDbDataAdapter(("select vendor_id from existingbookkinfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN ('---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, Direct_invoice_transaction.volume as volumeno , Direct_invoice_transaction.category_id as category , bookaccessionmaster.booktitle as title, Direct_invoice_transaction.author_type as authortype, Direct_invoice_transaction.first_name as firstname1, Direct_invoice_transaction.middle_name as middlename1, Direct_invoice_transaction.last_name as   lastname1, firstname2='', middlename2='', lastname2='', firstname3='', middlename3='', lastname3='', Direct_invoice_transaction.publisher_id as publisherid, Direct_invoice_transaction.edition, yearofedition='', isbn='', Direct_invoice_transaction.noofcopy as noofcopies,bookaccessionmaster.bookprice,subtitle='' ,yearofPublication='', direct_invoice_transaction.dept,language_id, media_id, part, series='',pages='',size='',specialprice='', adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, direct_invoice_master.vedorid as Vendor_id,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN Direct_invoice_transaction ON bookaccessionmaster.srnoold = Direct_invoice_transaction.srnoold inner join media_type on direct_invoice_transaction.media_type=media_type.media_id inner join direct_invoice_master on direct_invoice_transaction.srno=direct_invoice_master.srno where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, Direct_invoice_transaction.volume as volumeno , Direct_invoice_transaction.category_id as category , bookaccessionmaster.booktitle as title, Direct_invoice_transaction.author_type as authortype, Direct_invoice_transaction.first_name as firstname1, Direct_invoice_transaction.middle_name as middlename1, Direct_invoice_transaction.last_name as   lastname1, firstname2='', middlename2='', lastname2='', firstname3='', middlename3='', lastname3='', Direct_invoice_transaction.publisher_id as publisherid, Direct_invoice_transaction.edition, yearofedition='', isbn='', Direct_invoice_transaction.noofcopy as noofcopies,bookaccessionmaster.bookprice,subtitle='' ,yearofPublication='', direct_invoice_transaction.dept,language_id, media_id, part, series='',pages='',size='',specialprice='', adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, direct_invoice_master.vedorid as Vendor_id,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN Direct_invoice_transaction ON bookaccessionmaster.srnoold = Direct_invoice_transaction.srnoold inner join media_type on direct_invoice_transaction.media_type=media_type.media_id inner join direct_invoice_master on direct_invoice_transaction.srno=direct_invoice_master.srno where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }
                        else if (sr == "T")
                        {
                            AccessioningType = "T";
                            var davendor = new OleDbDataAdapter(("select vendor_id from existingbookkinfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN ('---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, volumeno='' , thesis_accessioning.category_id as category , bookaccessionmaster.booktitle as title, authortype='Author',   thesis_accessioning.fname1 as firstname1, thesis_accessioning.mname1 as middlename1, thesis_accessioning.lname1 as   lastname1, fname2 as firstname2 , mname2 as middlename2, lname2 as lastname2,fname3 as  firstname3,mname3 as  middlename3, lname3 as lastname3, publisherid=0, edition='', yearofedition='', isbn='', thesis_accessioning.noofcopy as noofcopies,bookaccessionmaster.bookprice,subtitle='' ,yearofPublication='', thesis_accessioning.dept,language_id, media_id, part='', series='',pages='',size='',specialprice='' , gname1 as adf1,gname2 as  adm1, gname3 as adl1, gname4 as adf2,gname5 as adm2, gname6  as adl2, gname7 as adf3, gname8 as adm3,gname9 as adl3,program_name,naration as abstract,bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, Vendor_id=0,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN thesis_accessioning ON bookaccessionmaster.srnoold = thesis_accessioning.srnoold inner join media_type on thesis_accessioning.media_type=media_type.media_id inner join program_master on program_master.program_id=thesis_accessioning.programe_id  where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, volumeno='' , thesis_accessioning.category_id as category , bookaccessionmaster.booktitle as title, authortype='Author',   thesis_accessioning.fname1 as firstname1, thesis_accessioning.mname1 as middlename1, thesis_accessioning.lname1 as   lastname1, fname2 as firstname2 , mname2 as middlename2, lname2 as lastname2,fname3 as  firstname3,mname3 as  middlename3, lname3 as lastname3, publisherid=0, edition='', yearofedition='', isbn='', thesis_accessioning.noofcopy as noofcopies,bookaccessionmaster.bookprice,subtitle='' ,yearofPublication='', thesis_accessioning.dept,language_id, media_id, part='', series='',pages='',size='',specialprice='' , gname1 as adf1,gname2 as  adm1, gname3 as adl1, gname4 as adf2,gname5 as adm2, gname6  as adl2, gname7 as adf3, gname8 as adm3,gname9 as adl3,program_name,naration as abstract,bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, Vendor_id=0,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM bookaccessionmaster INNER JOIN thesis_accessioning ON bookaccessionmaster.srnoold = thesis_accessioning.srnoold inner join media_type on thesis_accessioning.media_type=media_type.media_id inner join program_master on program_master.program_id=thesis_accessioning.programe_id  where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }

                        else if (sr == "J")
                        {
                            AccessioningType = "J";
                            var davendor = new OleDbDataAdapter(("select vendor_id from existingbookkinfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN ('---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, volumeno='' ,category='' , bookaccessionmaster.booktitle as title, authortype='',firstname1='',middlename1='', lastname1='',firstname2='' , middlename2='',lastname2='',firstname3='',middlename3='',lastname3='', publisherid=0, edition='', yearofedition='', isbn='',noofcopies=0,bookprice='',subtitle='' ,yearofPublication='', department as dept,tran_language as language_id, Media_type as media_id, part='', series='',pages='',size='',specialprice='' ,adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, Vendor_id=0 FROM bookaccessionmaster,JournalMaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, volumeno='' ,category='' , bookaccessionmaster.booktitle as title, authortype='',firstname1='',middlename1='', lastname1='',firstname2='' , middlename2='',lastname2='',firstname3='',middlename3='',lastname3='', publisherid=0, edition='', yearofedition='', isbn='',noofcopies=0,bookprice='',subtitle='' ,yearofPublication='', department as dept,tran_language as language_id, Media_type as media_id, part='', series='',pages='',size='',specialprice='' ,adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, Vendor_id=0 FROM bookaccessionmaster,JournalMaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }
                        else if (sr == "d")
                        {
                            AccessioningType = "d";
                            var davendor = new OleDbDataAdapter(("select vendor_id from DirectCateloginfo, bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "' and vendor_id NOT IN (N'---Select---')"), acccon);
                            davendor.Fill(ds, "vendor");
                            if (ds.Tables["Vendor"].Rows.Count > 0)
                            {
                                selectqry = "SELECT bookaccessionmaster.form, DirectCateloginfo.volumeno, DirectCateloginfo.category, bookaccessionmaster.booktitle as title, DirectCateloginfo.authortype, DirectCateloginfo.firstname1, DirectCateloginfo.middlename1, DirectCateloginfo.lastname1, DirectCateloginfo.firstname2, DirectCateloginfo.middlename2, DirectCateloginfo.lastname2, DirectCateloginfo.firstname3, DirectCateloginfo.middlename3, DirectCateloginfo.lastname3, DirectCateloginfo.publisherid, DirectCateloginfo.edition, DirectCateloginfo.yearofedition, DirectCateloginfo.isbn, DirectCateloginfo.noofcopies,DirectCateloginfo.price,subtitle=DirectCateloginfo.subtitle,DirectCateloginfo.yearofPublication,bookaccessionmaster.deptCode  as dept,language_id,media_id,part as part,seriesname as series,no_of_pages as pages ,page_size as size,DirectCateloginfo.specialprice, adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, DirectCateloginfo.vendor_id, bookaccessionmaster.biilNo, bookaccessionmaster.billDate,bookaccessionmaster.loc_id ,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM DirectCateloginfo inner join bookaccessionmaster on DirectCateloginfo.DsrNo = bookaccessionmaster.Dsrno inner join media_type on DirectCateloginfo.mediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                            else
                            {
                                selectqry = "SELECT bookaccessionmaster.form, DirectCateloginfo.volumeno, DirectCateloginfo.category,  bookaccessionmaster.booktitle as title, DirectCateloginfo.authortype, DirectCateloginfo.firstname1, DirectCateloginfo.middlename1, DirectCateloginfo.lastname1, DirectCateloginfo.firstname2, DirectCateloginfo.middlename2, DirectCateloginfo.lastname2, DirectCateloginfo.firstname3, DirectCateloginfo.middlename3, DirectCateloginfo.lastname3, DirectCateloginfo.publisherid, existingbookkinfo.edition, DirectCateloginfo.yearofedition, DirectCateloginfo.isbn, DirectCateloginfo.noofcopies,DirectCateloginfo.price,subtitle=DirectCateloginfo.subtitle,DirectCateloginfo.yearofPublication,bookaccessionmaster.deptcode  as dept,language_id,media_id,part as part,seriesname as series,no_of_pages as pages ,page_size as size,DirectCateloginfo.specialprice, adf1='',adm1='',adl1='',adf2='',adm2='',adl2='',adf3='',adm3='',adl3='',program_name='',abstract='',bookaccessionmaster.Item_type,bookaccessionmaster.originalcurrency,bookaccessionmaster.originalprice, bookaccessionmaster.biilNo, bookaccessionmaster.billDate, DirectCateloginfo.Vendor_id ,bookaccessionmaster.loc_id,dbo.locdecode2(bookaccessionmaster.loc_id) location FROM DirectCateloginfo inner join bookaccessionmaster on DirectCateloginfo.DsrNo = bookaccessionmaster.DsrNo inner join media_type on DirectCateloginfo.mediatype=media_type.media_id where bookaccessionmaster.accessionnumber=N'" + Session["accno"].ToString() + "'";
                            }
                        }
                        var accda = new OleDbDataAdapter(selectqry, acccon);
                        var accds = new DataSet();
                        accda.Fill(accds, "Accession");   //IF MATCHING INFORMATION NOT FOUND
                        if (accds.Tables[0].Rows.Count == 0)
                        {


                            // clearfields();



                            txtbookno.Value = Session["BNumber"].ToString() == "" ? "" : Session["BNumber"].ToString();
                            txtclassno.Value = Session["CNumber"].ToString() == "" ? "" : Session["CNumber"].ToString();
                            return;
                        }
                        if (accds.Tables[0].Rows[0][0].ToString() == "s")
                        {
                            accds.Tables[0].Rows[0][0] = "Soft";
                        }
                        else if (accds.Tables[0].Rows[0][0].ToString() == "h")
                        {
                            accds.Tables[0].Rows[0][0] = "Hard";
                        }

                        cmbboundind.SelectedIndex = cmbboundind.Items.IndexOf(cmbboundind.Items.FindByText(accds.Tables[0].Rows[0][0].ToString()));
                        txtvolno.Value = accds.Tables[0].Rows[0].IsNull(accds.Tables[0].Columns[1].ColumnName) ? String.Empty : accds.Tables[0].Rows[0][1].ToString();
                        txtVolumeNo.Value = accds.Tables[0].Rows[0].IsNull(accds.Tables[0].Columns[1].ColumnName) ? String.Empty : accds.Tables[0].Rows[0][1].ToString();

                        //                        txtVolumeNo.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][1]), string.Empty, accds.Tables[0].Rows[0][1]);
                        txttitle.Value = accds.Tables[0].Rows[0].IsNull(accds.Tables[0].Columns[3].ColumnName) ? String.Empty : accds.Tables[0].Rows[0][3].ToString();
                        //                        txtbooktype.Value = accds.Tables[0].Rows[0].IsNull(accds.Tables[0].Columns[2].ColumnName) ? String.Empty : accds.Tables[0].Rows[0][2].ToString();

                        this.HdBookTitle.Value = txttitle.Value;
                        txtbookprice.Value = accds.Tables[0].Rows[0].IsNull(accds.Tables[0].Columns[19].ColumnName) ? String.Empty : accds.Tables[0].Rows[0][19].ToString();
                        if (this.hddeptcode.Value != "")
                        {
                            cmbdept.SelectedValue = this.hddeptcode.Value;
                        }
                        else
                        {
                            cmbdept.SelectedValue = accds.Tables[0].Rows[0]["dept"].ToString();
                        }
                        try
                        {

                            cmbMediaType.SelectedValue = accds.Tables[0].Rows[0]["mediatype"].ToString();
                            txtisbn.Value = accds.Tables[0].Rows[0]["isbn"].ToString();
                        }
                        catch { }
                        cmbLanguage.SelectedValue = accds.Tables[0].Rows[0]["language_id"].ToString();
                        //                       set item ItemType Index positon By jeetendra Prajapati as on 28 dec 009 
                        //                     This Field is separated with Accession No. not With Control no
                        cmbtype.SelectedIndex = cmbtype.Items.IndexOf(cmbtype.Items.FindByValue(accds.Tables[0].Rows[0]["Item_type"].ToString()));
                        //cmbvendor.SelectedIndex = accds.Tables(0).Rows(0).Item("vendor_id")
                        //'-------------jeetu
                        var vendcmd = new OleDbCommand("Select VendorName+', '+percity as VendorName from  VendorMaster join addresstable on  vendormaster.vendorcode=addresstable.addid and addrelation='vendor' and vendorid=N'" + accds.Tables[0].Rows[0]["vendor_id"].ToString() + "' order by vendorname", acccon);

                        txtCmbVendor.Text = vendcmd.ExecuteScalar().ToString();// accds.Tables(0).Rows(0).Item("vendor_id")
                                                                               //                                                                    '''''''''''''''''''''
                                                                               //                                                                  cmbvendor.SelectedIndex = cmbvendor.Items.IndexOf(cmbvendor.Items.FindByValue(accds.Tables(0).Rows(0).Item("vendor_id")))
                        txtDocNo.Value = accds.Tables[0].Rows[0].IsNull("biilNo") ? string.Empty : accds.Tables[0].Rows[0]["biilNo"].ToString();

                        if (accds.Tables[0].Rows[0].IsNull("billDate"))
                        {
                            txtDocDate.Text = string.Empty;
                        }
                        else
                        {
                            txtDocDate.Text = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(accds.Tables[0].Rows[0]["billDate"]));
                        }

                        //                        txtDocDate.Text = accds.Tables[0].Rows[0].IsNull("billDate") ? String.Empty : string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(accds.Tables[0].Rows[0]["billDate"]));
                        var litem = new ListItem();
                        litem.Value = accds.Tables[0].Rows[0]["mediatype"].ToString();
                        if (accds.Tables[0].Rows[0]["item_type"].ToString() == "Journals")
                        {
                            //                           if (cmbMediaType.Items.Contains(litem) == true)
                            //                         {
                            //                           cmbMediaType.SelectedValue = accds.Tables[0].Rows[0]["media_name"];
                            //                     }
                            //                   else
                            //                 {
                            //                   cmbMediaType.SelectedValue = cmbMediaType.Items.Count - 1;
                            //             }
                        }
                        else
                        {
                         //   cmbMediaType.Items.Clear();
                           // cmbMediaType.Items.Add(Resources.ValidationResources.iprint);
                        }

                        //                        txtGms.Value = accds.Tables[0].Rows[0].IsNull("media_name") ? String.Empty : accds.Tables[0].Rows[0]["media_name"].ToString();

                        txtPart.Value = accds.Tables[0].Rows[0].IsNull("part") ? String.Empty : accds.Tables[0].Rows[0]["part"].ToString();



                        txtseriesname.Value = accds.Tables[0].Rows[0]["series"].ToString();
                        txtpages.Value = accds.Tables[0].Rows[0]["pages"].ToString();
                        txtbooksize.Value = accds.Tables[0].Rows[0]["size"].ToString();
                        txtSpecialPrice.Value = accds.Tables[0].Rows[0]["specialprice"].ToString();

                        // If accds.Tables(0).Rows(0).Item("iTEM_TYPE") <> "Journals" Then
                        cmbbookcategory.SelectedValue = accds.Tables[0].Rows[0]["category"].ToString(); ;

                        cmbcurr.SelectedIndex = cmbcurr.Items.IndexOf(cmbcurr.Items.FindByText(accds.Tables[0].Rows[0]["OriginalCurrency"].ToString()));
                        //IIf(IsDBNull(tmpds.Tables("result").Rows(0).Item("OriginalCurrency")), String.Empty, tmpds.Tables("result").Rows(0).Item("OriginalCurrency"))
                        //txtForeignPrice.Value = accds.Tables[0].Rows[0]["originalprice"];
                        //Else
                        //cmbbookcategory.Value=
                        //End If

                        if (hdCtrlNo.Value == string.Empty)
                        {

                            if (accds.Tables[0].Rows[0][4].ToString() == "Author")
                            {
                                txtau1firstnm.Value = accds.Tables[0].Rows[0][5].ToString();
                                txtau1midnm.Value = accds.Tables[0].Rows[0][6].ToString();
                                txtau1surnm.Value = accds.Tables[0].Rows[0][7].ToString();
                                txtau2firstnm.Value = accds.Tables[0].Rows[0][8].ToString();
                                txtau2midnm.Value = accds.Tables[0].Rows[0][9].ToString();
                                txtau2surnm.Value = accds.Tables[0].Rows[0][10].ToString();
                                txtau3firstnm.Value = accds.Tables[0].Rows[0][11].ToString();
                                txtau3midnm.Value = accds.Tables[0].Rows[0][12].ToString();
                                txtau3surnm.Value = accds.Tables[0].Rows[0][13].ToString();
                                txtSubtitle.Value = accds.Tables[0].Rows[0]["subtitle"].ToString();
                            }
                            else if (accds.Tables[0].Rows[0][4].ToString() == "Editor")
                            {
                                //this.editor1Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][5]), string.Empty, accds.Tables[0].Rows[0][5]);
                                //this.editor1Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][6]), string.Empty, accds.Tables[0].Rows[0][6]);
                                //this.editor1Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][7]), string.Empty, accds.Tables[0].Rows[0][7]);
                                //this.editor2fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][8]), string.Empty, accds.Tables[0].Rows[0][8]);
                                //this.editor2Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][9]), string.Empty, accds.Tables[0].Rows[0][9]);
                                //this.editor2Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][10]), string.Empty, accds.Tables[0].Rows[0][10]);
                                //this.editor3Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][11]), string.Empty, accds.Tables[0].Rows[0][11]);
                                //this.editor3Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][12]), string.Empty, accds.Tables[0].Rows[0][12]);
                                //this.editor3Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][13]), string.Empty, accds.Tables[0].Rows[0][13]);
                            }
                            else if (accds.Tables[0].Rows[0][4].ToString() == "Illustrator")
                            {
                                //this.Illustrator1Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][5]), string.Empty, accds.Tables[0].Rows[0][5]);
                                //this.Illustrator1Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][6]), string.Empty, accds.Tables[0].Rows[0][6]);
                                //this.Illustrator1Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][7]), string.Empty, accds.Tables[0].Rows[0][7]);
                                //this.Illustrator2Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][8]), string.Empty, accds.Tables[0].Rows[0][8]);
                                //this.Illustrator2Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][9]), string.Empty, accds.Tables[0].Rows[0][9]);
                                //this.Illustrator2Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][10]), string.Empty, accds.Tables[0].Rows[0][10]);
                                //this.Illustrator3Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][11]), string.Empty, accds.Tables[0].Rows[0][11]);
                                //this.Illustrator3Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][12]), string.Empty, accds.Tables[0].Rows[0][12]);
                                //this.Illustrator3lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][13]), string.Empty, accds.Tables[0].Rows[0][13]);
                            }

                            else if (accds.Tables[0].Rows[0][4].ToString() == "Translator")
                            {
                                //    this.Translator1Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][5]), string.Empty, accds.Tables[0].Rows[0][5]);
                                //    this.Translator1Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][6]), string.Empty, accds.Tables[0].Rows[0][6]);
                                //    this.Translator1Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][7]), string.Empty, accds.Tables[0].Rows[0][7]);
                                //    this.Translator2Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][8]), string.Empty, accds.Tables[0].Rows[0][8]);
                                //    this.Translator2Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][9]), string.Empty, accds.Tables[0].Rows[0][9]);
                                //    this.Translator2Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][10]), string.Empty, accds.Tables[0].Rows[0][10]);
                                //    this.Translator3Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][11]), string.Empty, accds.Tables[0].Rows[0][11]);
                                //    this.Translator3Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][12]), string.Empty, accds.Tables[0].Rows[0][12]);
                                //    this.Translator3Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][13]), string.Empty, accds.Tables[0].Rows[0][13]);
                            }
                            else if (accds.Tables[0].Rows[0][4].ToString() == "Compiler")
                            {
                                //this.compiler1Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][5]), string.Empty, accds.Tables[0].Rows[0][5]);
                                //this.compiler1Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][6]), string.Empty, accds.Tables[0].Rows[0][6]);
                                //this.compiler1Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][7]), string.Empty, accds.Tables[0].Rows[0][7]);
                                //this.compiler2Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][8]), string.Empty, accds.Tables[0].Rows[0][8]);
                                //this.compiler2Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][9]), string.Empty, accds.Tables[0].Rows[0][9]);
                                //this.compiler2Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][10]), string.Empty, accds.Tables[0].Rows[0][10]);
                                //this.compiler3Fname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][11]), string.Empty, accds.Tables[0].Rows[0][11]);
                                //this.compiler3Mname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][12]), string.Empty, accds.Tables[0].Rows[0][12]);
                                //this.compiler3Lname.Value = IIf(IsDBNull(accds.Tables[0].Rows[0][13]), string.Empty, accds.Tables[0].Rows[0][13]);
                            }
                        }
                        txtaname1.Value = accds.Tables[0].Rows[0]["adf1"].ToString();
                        txtaname2.Value = accds.Tables[0].Rows[0]["adm1"].ToString();
                        txtaname3.Value = accds.Tables[0].Rows[0]["adl1"].ToString();

                        txtfname2.Value = accds.Tables[0].Rows[0]["adf2"].ToString();
                        txtmname2.Value = accds.Tables[0].Rows[0]["adm2"].ToString();
                        //                                     txtlname2.Value = IIf(IsDBNull(accds.Tables[0].Rows[0]["adl2"]), string.Empty, accds.Tables[0].Rows[0]["adl2"]);

                        //                                   txtfname3.Value = IIf(IsDBNull(accds.Tables[0].Rows[0]["adf3"]), string.Empty, accds.Tables[0].Rows[0]["adf3"]);
                        //                                 txtmname3.Value = IIf(IsDBNull(accds.Tables[0].Rows[0]["adm3"]), string.Empty, accds.Tables[0].Rows[0]["adm3"]);
                        ////                               txtaname9.Value = IIf(IsDBNull(accds.Tables[0].Rows[0]["adl3"]), string.Empty, accds.Tables[0].Rows[0]["adl3"]);
                        //                           txtnarration.Value = IIf(IsDBNull(accds.Tables[0].Rows[0]["abstract"]), string.Empty, accds.Tables[0].Rows[0]["abstract"]);
                        //                         txtProgram.Value = IIf(IsDBNull(accds.Tables[0].Rows[0]["program_name"]), string.Empty, accds.Tables[0].Rows[0]["program_name"]);

                        //                                               If Trim(accds.Tables(0).Rows(0).Item("program_name")) <> String.Empty Then
                        //                                             Label110.Text = Label110.Text & "(" & accds.Tables(0).Rows(0).Item("program_name") & ")"
                        //                                           Else
                        //                                         Label110.Text = "Author"
                        //                                       End If

                        string sqlstr = "Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'" + accds.Tables[0].Rows[0][14].ToString() + "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'";
                        var cmd = new OleDbCommand(sqlstr, acccon);
                        string tmpstr = cmd.ExecuteScalar().ToString();
                        this.hdPublisherId.Value = accds.Tables[0].Rows[0][14].ToString();
                        txtCmbPublisher.Text = tmpstr;

                        //                                               'cmbpubnm.SelectedIndex = cmbpubnm.Items.IndexOf(IIf(IsDBNull(accds.Tables(0).Rows(0).Item(14)), cmbpubnm.Items.Count - 1, cmbpubnm.Items.FindByValue(accds.Tables(0).Rows(0).Item(14))))
                        txtedition.Value = accds.Tables[0].Rows[0][15].ToString();
                        txteditionyear.Value = accds.Tables[0].Rows[0][16].ToString();

                        //                            if (!IsDBNull(accds.Tables[0].Rows[0][16]))
                        //                                             {
                        //                                               txteditionyear.Value = IIf(Val(accds.Tables[0].Rows[0][16]) == 0, string.Empty, accds.Tables[0].Rows[0][16]);
                        //                                         }
                        //                                       else
                        //                                     {
                        //                                       txteditionyear.Value = string.Empty;
                        //                                 }






                        //             // txtPubYear.Value = IIf(IsDBNull(accds.Tables(0).Rows(0).Item("yearofPublication")) Or accds.Tables(0).Rows(0).Item("yearofPublication") = 0, String.Empty, accds.Tables(0).Rows(0).Item("yearofPublication"))

                        if (!accds.Tables[0].Rows[0].IsNull("yearofPublication"))
                        {
                            txtPubYear.Value = accds.Tables[0].Rows[0]["yearofPublication"].ToString();
                        }
                        else
                        {
                            txtPubYear.Value = string.Empty;
                        }




                        txtisbn.Value = accds.Tables[0].Rows[0][17].ToString();
                        var ctrl = hdCtrlNo.Value;

                        //             string strCopy;
                        //             var cmdCopy = new OleDbCommand("select coalesce(max(copynumber),'0',max(copynumber)) from cataloguecardview where ctrl_no=" + Val(hdCtrlNo.Value));
                        //             cmdCopy.Connection = acccon;
                        //             strCopy = Conversions.ToString(cmdCopy.ExecuteScalar());
                        //             CopyNoStart = Val(strCopy);
                        //             txtCopyNo.Value = CopyNoStart + 1;  // start point of copy number is stoes
                        //             tot = IIf(IsDBNull(accds.Tables[0].Rows[0][18]), 0, accds.Tables[0].Rows[0][18]);   // No. of Copies

                        //             if (tot > 1)
                        //             {
                        //                 if (AccessioningType == "N")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster WHERE bookaccessionmaster.ctrl_no=0 and bookaccessionmaster.srno = ((SELECT distinct bookaccessionmaster.srno FROM bookaccessionmaster,modifiedordermaster WHERE bookaccessionmaster.srno=modifiedordermaster.srno and bookaccessionmaster.accessionnumber = N'", Session["accno"]), "')) AND accessionnumber <> N'"), Session["accno"]), "' ORDER BY a1,a2,suffix")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 else if (AccessioningType == "E")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster WHERE bookaccessionmaster.srNoOld = ((SELECT bookaccessionmaster.srNoOld FROM bookaccessionmaster where bookaccessionmaster.accessionnumber = N'", Session["accno"]), "')) AND accessionnumber <> N'"), Session["accno"]), "' ORDER BY a1,a2,suffix")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 else if (AccessioningType == "G")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster WHERE bookaccessionmaster.srNoOld = ((SELECT bookaccessionmaster.srNoOld FROM bookaccessionmaster where bookaccessionmaster.accessionnumber = N'", Session["accno"]), "')) AND accessionnumber <> N'"), Session["accno"]), "' ORDER BY a1,a2,suffix")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 else if (AccessioningType == "i")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster WHERE bookaccessionmaster.srNoOld = ((SELECT bookaccessionmaster.srNoOld FROM bookaccessionmaster where bookaccessionmaster.accessionnumber = N'", Session["accno"]), "')) AND accessionnumber <> N'"), Session["accno"]), "' ORDER BY a1,a2,suffix")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 else if (AccessioningType == "T")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster WHERE bookaccessionmaster.srNoOld = ((SELECT bookaccessionmaster.srNoOld FROM bookaccessionmaster where bookaccessionmaster.accessionnumber = N'", Session["accno"]), "')) AND accessionnumber <> N'"), Session["accno"]), "' ORDER BY a1,a2,suffix")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 else if (AccessioningType == "J")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster where  accessionnumber <> N'", Session["accno"]), "' ORDER BY a1,a2")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 else if (AccessioningType == "d")
                        //                 {
                        //                     accda = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("select accessionnumber, dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix,dbo.locdecode2(bookaccessionmaster.loc_id) location,loc_id FROM bookaccessionmaster WHERE bookaccessionmaster.DsrNo= ((SELECT bookaccessionmaster.DsrNo FROM bookaccessionmaster where bookaccessionmaster.accessionnumber = N'", Session["accno"]), "')) AND accessionnumber <> N'"), Session["accno"]), "' ORDER BY a1,a2,suffix")), acccon);
                        //                     AccessioningType = string.Empty;
                        //                 }
                        //                 // Dim i1 As Integer
                        //                 // For i1 = 0 To accds.Tables(0).Rows.Count - 1
                        //                 // Dim cmb As New DropDownList
                        //                 // cmb = CType(grdcopy.Items(i1).Cells(6).FindControl("cmbOriCurrency"), DropDownList)
                        //                 // libObj.populateDDL(cmb, "select currencycode,CurrencyName from ExchangeMaster order by CurrencyName", "CurrencyName", "Currencycode", "", acccon)
                        //                 // cmb.Items.Remove(cmb.Items(cmb.Items.Count - 1))
                        //                 // cmb.SelectedIndex = 0
                        //                 // Next

                        //                 accda.Fill(accds, "catalogdata");

                        //                 var mytab = new DataTable();
                        //                 if (accds.Tables["catalogdata"].Rows.Count > 0)
                        //                 {
                        //                     mytab = accds.Tables["catalogdata"];
                        //                     mytab.Columns.Add(new DataColumn("DocNo"));
                        //                     mytab.Columns.Add(new DataColumn("DocDate"));
                        //                     mytab.Columns.Add(new DataColumn("copyno"));
                        //                     mytab.Columns.Add(new DataColumn("year"));
                        //                     mytab.Columns.Add(new DataColumn("pubYear"));
                        //                     // mytab.Columns.Add(New DataColumn("OriginalCurrency"))
                        //                     mytab.Columns.Add(new DataColumn("OriginalPrice"));
                        //                     mytab.Columns.Add(new DataColumn("bookprice"));
                        //                     mytab.Columns.Add(new DataColumn("specialprice"));
                        //                     mytab.Columns.Add(new DataColumn("catalogdate"));
                        //                     mytab.Columns.Add(new DataColumn("cat_source"));
                        //                     mytab.Columns.Add(new DataColumn("Author"));
                        //                     // mytab.Columns.Add(New DataColumn("txtCpLoc"))
                        //                     // mytab.Columns.Add(New DataColumn("hdnCpLocId"))
                        //                     int i;
                        //                     DataRow rw;
                        //                     var loopTo1 = accds.Tables[1].Rows.Count - 1;
                        //                     for (i = 0; i <= loopTo1; i++)
                        //                     {
                        //                         rw = mytab.Rows[i];
                        //                         rw[6] = txtDocNo.Value;
                        //                         rw[7] = txtDocDate.Text;
                        //                         rw[8] = i + 2 + CopyNoStart;  // entries being displayed at the to 
                        //                         rw[9] = txteditionyear.Value;
                        //                         rw[10] = txtPubYear.Value;
                        //                         // rw(8) = ""
                        //                         rw[11] = txtForeignPrice.Value;
                        //                         rw[12] = txtbookprice.Value;
                        //                         rw[13] = txtSpecialPrice.Value;
                        //                         rw[14] = txtdate.Text;
                        //                         // ''''''''''''''''''jeetu
                        //                         // Dim vendcmd As New OleDbCommand("Select VendorName+', '+percity as VendorName from  VendorMaster join addresstable on  vendormaster.vendorcode=addresstable.addid and addrelation='vendor' and vendorid='" & accds.Tables(0).Rows(0).Item("vendor_id") & "' order by vendorname", acccon)

                        //                         // txtCmbVendor.Text = vendcmd.ExecuteScalar() 'accds.Tables(0).Rows(0).Item("vendor_id")

                        //                         // '''''''''''''''''''''''
                        //                         rw[13] = txtCmbVendor.Text;
                        //                         // rw(14) =
                        //                         mytab.AcceptChanges();
                        //                     }
                        //                     grdcopy.DataSource = mytab;
                        //                     grdcopy.DataBind();
                        //                     int i1;
                        //                     var cmb = new DropDownList();
                        //                     var hdtxtDocNoC = default(HiddenField);
                        //                     var loopTo2 = grdcopy.Items.Count - 1;
                        //                     for (i1 = 0; i1 <= loopTo2; i1++)
                        //                     {
                        //                         cmb = (DropDownList)grdcopy.Items(i1).Cells(6).FindControl("cmbOriCurrency");
                        //                         // libObj.populateDDL(cmb, "select distinct currencycode,CurrencyName from ExchangeMaster order by CurrencyName", "CurrencyName", "Currencycode", "", acccon)
                        //                         string str_curr = string.Empty;
                        //                         str_curr = "select distinct currencycode,CurrencyName from ExchangeMaster order by CurrencyName";
                        //                         var da_curr = new OleDbDataAdapter(str_curr, acccon);
                        //                         var ds_curr = new DataSet();
                        //                         da_curr.Fill(ds_curr);
                        //                         if (ds_curr.Tables[0].Rows.Count > 0)
                        //                         {
                        //                             cmb.DataSource = ds_curr;
                        //                             cmb.DataTextField = "CurrencyName";
                        //                             cmb.DataValueField = "currencycode";
                        //                             cmb.DataBind();
                        //                             // cmb.SelectedItem.Text = Me.cmbcurr.SelectedItem.Text
                        //                             cmb.SelectedIndex = cmb.Items.IndexOf(cmb.Items.FindByText(accds.Tables[0].Rows[0]["OriginalCurrency"])); // IIf(IsDBNull(tmpds.Tables("result").Rows(0).Item("OriginalCurrency")), String.Empty, tmpds.Tables("result").Rows(0).Item("OriginalCurrency"))
                        //                         }

                        //                         var cmb1 = new DropDownList();
                        //                         cmb1 = (DropDownList)grdcopy.Items(i1).FindControl("cmbCourse");
                        //                         LibObj.populateDDL(cmb1, "Select distinct program_id, Program_Master.program_name as programname from Program_Master order by program_name", "programname", "program_id", this.HComboSelect.Value, acccon);
                        //                         cmd.Parameters.Clear();
                        //                         cmd.CommandType = CommandType.Text;
                        //                         cmd.CommandText = "select program_id from bookaccessionmaster where accessionnumber='" + Trim(grdcopy.Items(i1).Cells(1).Text) + "'";
                        //                         if (IsDBNull(cmd.ExecuteScalar()))
                        //                         {
                        //                             cmb1.SelectedValue = 0;
                        //                         }
                        //                         else
                        //                         {
                        //                             cmb1.SelectedValue = cmd.ExecuteScalar();
                        //                             ((HiddenField)grdcopy.Items(i1).FindControl("hdcmbcourse")).Value = cmb1.SelectedValue;
                        //                             HiddenField hdcmbcors = (HiddenField)grdcopy.Items(i1).Cells(12).FindControl("hdcmbcourse");
                        //                             hdcmbcors.Value = cmb1.SelectedValue;
                        //                         }
                        //                         var cmbState = new DropDownList();
                        //                         cmbState = (DropDownList)grdcopy.Items(i1).FindControl("cmbStatus");
                        //                         // hdcmbStatus
                        //                         ((HiddenField)grdcopy.Items(i1).FindControl("hdcmbStatus")).Value = cmbState.SelectedValue;
                        //                         LibObj.populateDDL(cmbState, "select ItemStatusID,ItemStatus from Itemstatusmaster order by itemstatus", "ItemStatus", "ItemStatusID", this.HComboSelect.Value, acccon);
                        //                         cmd.Parameters.Clear();
                        //                         cmd.CommandType = CommandType.Text;
                        //                         cmd.CommandText = "select Status from bookaccessionmaster where accessionnumber='" + Trim(this.grdcopy.Items(i1).Cells(1).Text) + "'";
                        //                         if (IsDBNull(cmd.ExecuteScalar()))
                        //                         {
                        //                             cmbState.SelectedValue = 0;
                        //                         }
                        //                         else
                        //                         {
                        //                             cmbState.SelectedValue = cmd.ExecuteScalar();
                        //                             HiddenField hdcmbStatus = (HiddenField)grdcopy.Items(i1).FindControl("hdcmbStatus");
                        //                             hdcmbStatus.Value = cmbState.SelectedValue;
                        //                             // 
                        //                         } // 

                        //                         var CmbGrdDeptObj = new DropDownList();
                        //                         CmbGrdDeptObj = (DropDownList)this.grdcopy.Items(i1).FindControl("CmbGrdDept");
                        //                         LibObj.populateDDL(CmbGrdDeptObj, "select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "departmentname", "departmentcode", this.HComboSelect.Value, acccon);
                        //                         cmd.Parameters.Clear();
                        //                         cmd.CommandType = CommandType.Text;
                        //                         cmd.CommandText = "select DeptCode from bookaccessionmaster where accessionnumber='" + Trim(this.grdcopy.Items(i1).Cells(1).Text) + "'";
                        //                         if (IsDBNull(cmd.ExecuteScalar()))
                        //                         {
                        //                             CmbGrdDeptObj.SelectedValue = cmbdept.SelectedValue;
                        //                         }
                        //                         else
                        //                         {
                        //                             CmbGrdDeptObj.SelectedValue = cmd.ExecuteScalar();
                        //                             HiddenField hdCmbGrdDept = (HiddenField)grdcopy.Items(i1).FindControl("hdCmbGrdDept");
                        //                             hdCmbGrdDept.Value = CmbGrdDeptObj.SelectedValue;
                        //                             // 
                        //                         }
                        //                         // ----------Set item Type for every Book--------- By jeetendra Prajapati

                        //                         var CmbItemType = new DropDownList();
                        //                         CmbItemType = (DropDownList)grdcopy.Items(i1).FindControl("CmbItemType");
                        //                         LibObj.populateDDL(CmbItemType, "select Item_Type as ITVal,Item_Type as ITText from Item_Type where id <> 0 order by Item_Type", "ITText", "ITVal", HComboSelect.Value, bookcatalogcon);
                        //                         // CmbItemType.SelectedValue = ""
                        //                         bookcatalogcom.Parameters.Clear();
                        //                         bookcatalogcom.CommandType = CommandType.Text;
                        //                         bookcatalogcom.CommandText = "select Item_Type from bookaccessionmaster where accessionnumber='" + Trim(this.grdcopy.Items(i1).Cells(1).Text) + "'";
                        //                         if (IsDBNull(bookcatalogcom.ExecuteScalar()))
                        //                         {
                        //                             CmbItemType.SelectedValue = Resources.ValidationResources.ComboSelect;
                        //                         }
                        //                         else
                        //                         {
                        //                             // 
                        //                             CmbItemType.SelectedValue = bookcatalogcom.ExecuteScalar();
                        //                             HiddenField hdCmbItemType = (HiddenField)grdcopy.Items(i1).FindControl("hdCmbItemType");
                        //                             hdCmbItemType.Value = CmbItemType.SelectedValue;
                        //                         }

                        //                         // 'work in progress
                        //                         // '****************************************

                        //                         var CatItemType = new DropDownList();
                        //                         CatItemType = (DropDownList)grdcopy.Items(i1).FindControl("CatItemType");
                        //                         LibObj.populateDDL(CatItemType, "select Category_LoadingStatus as CVal,Category_LoadingStatus as CText from CategoryLoadingStatus where id <> 0 order by category_LoadingStatus", "CText", "CVal", HComboSelect.Value, bookcatalogcon);
                        //                         // Call LibObj.populateDDL(CatItemType, "select Item_Type as ITVal,Item_Type as ITText from Item_Type where id <> 0 order by Item_Type", "ITText", "ITVal", HComboSelect.Value, bookcatalogcon)

                        //                         // **************************************
                        //                         // 'CmbItemType.SelectedValue = ""
                        //                         bookcatalogcom.Parameters.Clear();
                        //                         bookcatalogcom.CommandType = CommandType.Text;
                        //                         bookcatalogcom.CommandText = "select ItemCategory from bookaccessionmaster where accessionnumber='" + Trim(this.grdcopy.Items(i1).Cells(1).Text) + "'";
                        //                         if (IsDBNull(bookcatalogcom.ExecuteScalar()))
                        //                         {
                        //                             CatItemType.SelectedValue = Resources.ValidationResources.ComboSelect;
                        //                         }
                        //                         else
                        //                         {
                        //                             CatItemType.SelectedValue = bookcatalogcom.ExecuteScalar();
                        //                             HiddenField hdCatItemType = (HiddenField)grdcopy.Items(i1).FindControl("hdCatItemType");
                        //                             hdCatItemType.Value = CatItemType.SelectedValue;
                        //                             // 
                        //                             // 
                        //                         }
                        //                         HtmlInputText shdtxtDocNoC = (HtmlInputText)grdcopy.Items(i1).FindControl("txtDocNoC");
                        //                         hdtxtDocNoC.Value = shdtxtDocNoC.Value;
                        //                         // '****************************************
                        //                     }
                        //                 }
                        //             }
                        //             else
                        //             {
                        ////                 var myt = new DataTable();
                        //                 grdcopy.DataSource = null;
                        //                 grdcopy.DataBind();
                        //             }
                        //             // grdcopy.Columns(12).Visible = False
                        //             // Now Considering the case if this form has been loaded for new entry 
                        //             if (Trim(hdCtrlNo.Value) != string.Empty)
                        //             {
                        //                 var tmpCon = new OleDbConnection(retConstr(Conversions.ToString(Session["LibWiseDBConn"])));
                        //                 tmpCon.Open();
                        //                 // for exported data from external source
                        //                 string qcat = "";
                        //                 if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(Session["marcnewid"], "", false)))
                        //                 {
                        //                     qcat = "SELECT booktype,volumenumber,initpages,pages,parts,leaves,boundind,publishercode,edition,isbn,subject1,subject2,subject3,Booksize,LCCN,Volumepages,biblioPages,bookindex,illustration,variouspaging,maps,ETalEditor,ETalCompiler,ETalIllus,ETalTrans,ETalAuthor,accmaterialhistory,MaterialDesignation,issn,Volume,dept,language_id,part,eBookURL,FixedData, Identifier FROM   MARCEXT_BookCatalog where MARCEXT_BookCatalog.ctrl_no='" + Session["marcnewid"].ToString() + "'";
                        //                 }
                        //                 else
                        //                 {
                        //                     qcat = "SELECT booktype,volumenumber,initpages,pages,parts,leaves,boundind,publishercode,edition,isbn,subject1,subject2,subject3,Booksize,LCCN,Volumepages,biblioPages,bookindex,illustration,variouspaging,maps,ETalEditor,ETalCompiler,ETalIllus,ETalTrans,ETalAuthor,accmaterialhistory,MaterialDesignation,issn,Volume,dept,language_id,part,eBookURL,FixedData, Identifier FROM   BookCatalog where BookCatalog.ctrl_no=" + hdCtrlNo.Value;
                        //                 }

                        //                 var tmpda = new OleDbDataAdapter(qcat, tmpCon);
                        //                 var tmpds = new DataSet();
                        //                 tmpda.Fill(tmpds, "result");
                        //                 if (tmpds.Tables[0].Rows.Count > 0)
                        //                 {
                        //                     cmbbookcategory.SelectedValue = tmpds.Tables[0].Rows[0]["booktype"].ToString();
                        //                     txtPart.Value = tmpds.Tables[0].Rows[0]["part"].ToString();
                        //                     this.txtvolno.Value = tmpds.Tables[0].Rows[0]["volumenumber"].ToString();

                        //                     cmbdept.SelectedValue = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["dept"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["dept"].ToString());
                        //                     // Me.txtvolno.Value = IIf(IsDBNull(tmpds.Tables(0).Rows(0).Item(3)) Or Val(tmpds.Tables(0).Rows(0).Item(3)) = 0, String.Empty, tmpds.Tables(0).Rows(0).Item(3))
                        //                     this.txtinitpages.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["initpages"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["initpages"].ToString());
                        //                     this.txtpages.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["pages"].ToString()) | Val(tmpds.Tables[0].Rows[0]["pages"].ToString()) == 0, string.Empty, tmpds.Tables[0].Rows[0]["pages"].ToString());
                        //                     this.txtparts.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["parts"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["parts"].ToString());
                        //                     this.txtleaves.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["leaves"].ToString()) | Val(tmpds.Tables[0].Rows[0]["leaves"].ToString()) == 0, string.Empty, tmpds.Tables[0].Rows[0]["leaves"].ToString());
                        //                     // If tmpds.Tables(0).Rows(0).Item("boundind") = "s" Then
                        //                     // tmpds.Tables(0).Rows(0).Item("boundind") = "Soft"
                        //                     // ElseIf tmpds.Tables(0).Rows(0).Item("boundind") = "h" Then
                        //                     // tmpds.Tables(0).Rows(0).Item("boundind") = "Hard"
                        //                     // End If
                        //                     cmbboundind.SelectedIndex = cmbboundind.Items.IndexOf(cmbboundind.Items.FindByText(tmpds.Tables[0].Rows[0]["boundind"].ToString()));
                        //                     // IIf(IsDBNull(tmpds.Tables(0).Rows(0).Item(8)), String.Empty, tmpds.Tables(0).Rows(0).Item(8))
                        //                     // 'Me.cmbpubnm.SelectedValue = (cmbpubnm.Items.IndexOf(cmbpubnm.Items.FindByValue(tmpds.Tables(0).Rows(0).Item("publishercode"))))
                        //                     string sqlstr1 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'", tmpds.Tables[0].Rows[0]["publishercode"]), "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'"));
                        //                     var cmd1 = new OleDbCommand(sqlstr1, tmpCon);
                        //                     string tmpstr1 = Conversions.ToString(cmd1.ExecuteScalar());
                        //                     this.hdPublisherId.Value = tmpds.Tables[0].Rows[0]["publishercode"].ToString();
                        //                     txtCmbPublisher.Text = tmpstr1;

                        //                     this.txtedition.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["edition"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["edition"].ToString());

                        //                     this.txtisbn.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["isbn"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["isbn"].ToString());
                        //                     this.txtsub11.Text = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["subject1"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["subject1"].ToString());
                        //                     this.txtsub2.Text = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["subject2"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["subject2"].ToString());
                        //                     this.txtsub3.Text = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["subject3"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["subject3"].ToString());
                        //                     this.txtbooksize.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["Booksize"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["Booksize"].ToString());
                        //                     this.txtlccn.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["LCCN"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["LCCN"].ToString());
                        //                     this.txtvolpages.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["Volumepages"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["Volumepages"].ToString());
                        //                     this.txtbiblpages.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["biblioPages"].ToString()), string.Empty, tmpds.Tables[0].Rows[0]["biblioPages"].ToString());
                        //                     this.cboBookIndex.SelectedIndex = cboBookIndex.Items.IndexOf(cboBookIndex.Items.FindByText(tmpds.Tables[0].Rows[0]["bookindex"].ToString()));
                        //                     this.cboIllistration.SelectedIndex = cboIllistration.Items.IndexOf(cboIllistration.Items.FindByText(tmpds.Tables[0].Rows[0]["illustration"].ToString()));
                        //                     this.cbovariouspaging.SelectedIndex = cbovariouspaging.Items.IndexOf(cbovariouspaging.Items.FindByText(tmpds.Tables[0].Rows[0]["variouspaging"].ToString()));
                        //                     this.txtmaps.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["maps"].ToString()) | tmpds.Tables[0].Rows[0]["maps"].ToString() == "0", string.Empty, tmpds.Tables[0].Rows[0]["maps"].ToString());
                        //                     this.cboEditorETAL.SelectedIndex = cboEditorETAL.Items.IndexOf(cboEditorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalEditor"].ToString()));
                        //                     this.cboCompilerETAL.SelectedIndex = cboCompilerETAL.Items.IndexOf(cboCompilerETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalCompiler"].ToString()));
                        //                     this.cboILLustratorETAL.SelectedIndex = cboILLustratorETAL.Items.IndexOf(cboILLustratorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalIllus"].ToString()));
                        //                     this.cboTranslatorETAL.SelectedIndex = cboTranslatorETAL.Items.IndexOf(cboTranslatorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalTrans"].ToString()));
                        //                     this.cboAuthorETAL.SelectedIndex = cboAuthorETAL.Items.IndexOf(cboAuthorETAL.Items.FindByText(tmpds.Tables[0].Rows[0]["ETalAuthor"].ToString()));
                        //                     this.txtmaterialinfo.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["accmaterialhistory"]), string.Empty, tmpds.Tables[0].Rows[0]["accmaterialhistory"].ToString());
                        //                     txtVolumeNo.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["Volume"]), string.Empty, tmpds.Tables[0].Rows[0]["Volume"].ToString());
                        //                     // txtauthors.Value = IIf(IsDBNull(accds.Tables(0).Rows(0).Item(6)), String.Empty, accds.Tables(0).Rows(0).Item(6))

                        //                     litem.Value = tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString();
                        //                     if (cmbMediaType.Items.Contains(litem) == true)
                        //                     {
                        //                         cmbMediaType.SelectedValue = tmpds.Tables[0].Rows[0]["MaterialDesignation"].ToString();
                        //                     }
                        //                     else
                        //                     {
                        //                         cmbMediaType.SelectedValue = cmbMediaType.Items.Count - 1;
                        //                     }


                        //                     // txtGms.Value = IIf(IsDBNull(tmpds.Tables(0).Rows(0).Item("MaterialDesignation")), String.Empty, tmpds.Tables(0).Rows(0).Item("MaterialDesignation"))
                        //                     txtIssnNo.Value = IIf(IsDBNull(tmpds.Tables[0].Rows[0]["issn"]), string.Empty, tmpds.Tables[0].Rows[0]["issn"]);
                        //                     // =================================
                        //                 }
                        //                 // =================================
                        //                 this.image1.Src = "imagehiddenform.aspx?cno=" + Trim(hdCtrlNo.Value.ToString);
                        //                 // =====================================
                        //                 string q1 = "";
                        //                 if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(Session["marcnewid"], "", false)))
                        //                 {
                        //                     q1 = "SELECT * from MARCEXT_bookauthor where ctrl_no='" + Session["marcnewid"].ToString() + "'";
                        //                 }
                        //                 else
                        //                 {
                        //                     q1 = "SELECT * from bookauthor where ctrl_no=" + Val(hdCtrlNo.Value);
                        //                 }
                        //                 var tmpda1 = new OleDbDataAdapter(q1, tmpCon);
                        //                 var tmpds1 = new DataSet();
                        //                 tmpda1.Fill(tmpds1, "Author");
                        //                 if (tmpds1.Tables["Author"].Rows.Count > 0)
                        //                 {
                        //                     this.txtau1firstnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][1]), string.Empty, tmpds1.Tables[0].Rows[0][1]);
                        //                     this.txtau1midnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][2]), string.Empty, tmpds1.Tables[0].Rows[0][2]);
                        //                     this.txtau1surnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][3]), string.Empty, tmpds1.Tables[0].Rows[0][3]);

                        //                     this.txtau2firstnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][4]), string.Empty, tmpds1.Tables[0].Rows[0][4]);
                        //                     this.txtau2midnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][5]), string.Empty, tmpds1.Tables[0].Rows[0][5]);
                        //                     this.txtau2surnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][6]), string.Empty, tmpds1.Tables[0].Rows[0][6]);

                        //                     this.txtau3firstnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][7]), string.Empty, tmpds1.Tables[0].Rows[0][7]);
                        //                     this.txtau3midnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][7]), string.Empty, tmpds1.Tables[0].Rows[0][7]);
                        //                     this.txtau3surnm.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0][8]), string.Empty, tmpds1.Tables[0].Rows[0][8]);
                        //                     txtUniformTitle.Value = IIf(IsDBNull(tmpds1.Tables["Author"].Rows[0]["UniFormTitle"]), string.Empty, tmpds1.Tables[0].Rows[0]["UniFormTitle"]);
                        //                 }
                        //                 tmpda1.Dispose();
                        //                 tmpds1.Dispose();

                        //                 string q2 = "";
                        //                 if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(Session["marcnewid"], "", false)))
                        //                 {
                        //                     q2 = "SELECT * from bookseries where ctrl_no='" + Session["marcnewid"].ToString() + "'";
                        //                 }
                        //                 else
                        //                 {
                        //                     q2 = "SELECT * from bookseries where ctrl_no=" + Val(hdCtrlNo.Value);
                        //                 }
                        //                 var tmpda2 = new OleDbDataAdapter(q2, tmpCon);
                        //                 var tmpds2 = new DataSet();
                        //                 tmpda2.Fill(tmpds2, "Series");
                        //                 if (tmpds2.Tables["Series"].Rows.Count > 0)
                        //                 {
                        //                     this.txtseriesname.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0][1]), string.Empty, tmpds2.Tables[0].Rows[0][1]);
                        //                     this.txtseriesno.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0][2]), string.Empty, tmpds2.Tables[0].Rows[0][2]);
                        //                     this.txtseriespart.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0][3]), string.Empty, tmpds2.Tables[0].Rows[0][3]);
                        //                     // txtSVolume.Value = IIf(IsDBNull(tmpds2.Tables(0).Rows(0).Item("Svolume")) Or tmpds2.Tables(0).Rows(0).Item("Svolume") = 0, String.Empty, tmpds2.Tables(0).Rows(0).Item("Svolume"))
                        //                     if (!IsDBNull(tmpds2.Tables[0].Rows[0]["Svolume"]))
                        //                     {
                        //                         txtSVolume.Value = IIf(Operators.ConditionalCompareObjectEqual(tmpds2.Tables[0].Rows[0]["Svolume"], 0, false), string.Empty, tmpds2.Tables[0].Rows[0]["Svolume"]);
                        //                     }
                        //                     else
                        //                     {
                        //                         txtSVolume.Value = string.Empty;
                        //                     }

                        //                     this.status.SelectedIndex = status.Items.IndexOf(status.Items.FindByText(tmpds2.Tables[0].Rows[0]["etal"]));
                        //                     af1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["af1"]), string.Empty, tmpds2.Tables[0].Rows[0]["af1"]);
                        //                     am1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["am1"]), string.Empty, tmpds2.Tables[0].Rows[0]["am1"]);
                        //                     al1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["al1"]), string.Empty, tmpds2.Tables[0].Rows[0]["al1"]);
                        //                     af2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["af2"]), string.Empty, tmpds2.Tables[0].Rows[0]["af2"]);
                        //                     am2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["am2"]), string.Empty, tmpds2.Tables[0].Rows[0]["am2"]);
                        //                     al2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["al2"]), string.Empty, tmpds2.Tables[0].Rows[0]["al2"]);
                        //                     af3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["af3"]), string.Empty, tmpds2.Tables[0].Rows[0]["af3"]);
                        //                     am3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["am3"]), string.Empty, tmpds2.Tables[0].Rows[0]["am3"]);
                        //                     al3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["al3"]), string.Empty, tmpds2.Tables[0].Rows[0]["al3"]);
                        //                     this.txtSecondSeriesTitle.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0]["SSeriesName"]), string.Empty, tmpds2.Tables[0].Rows[0]["SSeriesName"]);
                        //                     this.txtSecondSeriesNo.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0]["SseriesNo"]), string.Empty, tmpds2.Tables[0].Rows[0]["SseriesNo"]);
                        //                     this.txtSecondSeriesPart.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0]["SseriesPart"]), string.Empty, tmpds2.Tables[0].Rows[0]["SseriesPart"]);
                        //                     // txtsecSeriesVol.Value = IIf(IsDBNull(tmpds2.Tables(0).Rows(0).Item("SSvolume")) Or tmpds2.Tables(0).Rows(0).Item("SSvolume") = 0, String.Empty, tmpds2.Tables(0).Rows(0).Item("SSvolume"))
                        //                     if (!IsDBNull(tmpds2.Tables[0].Rows[0]["SSvolume"]))
                        //                     {
                        //                         txtsecSeriesVol.Value = IIf(Operators.ConditionalCompareObjectEqual(tmpds2.Tables[0].Rows[0]["SSvolume"], 0, false), string.Empty, tmpds2.Tables[0].Rows[0]["SSvolume"]);
                        //                     }
                        //                     else
                        //                     {
                        //                         txtsecSeriesVol.Value = string.Empty;
                        //                     }
                        //                     cmbSecetal.SelectedIndex = cmbSecetal.Items.IndexOf(cmbSecetal.Items.FindByText(tmpds2.Tables[0].Rows[0]["Setal"]));
                        //                     txtSecFirstName1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Saf1"]), string.Empty, tmpds2.Tables[0].Rows[0]["Saf1"]);
                        //                     txtSecMidName1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Sam1"]), string.Empty, tmpds2.Tables[0].Rows[0]["Sam1"]);
                        //                     txtSecLastName1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Sal1"]), string.Empty, tmpds2.Tables[0].Rows[0]["Sal1"]);
                        //                     txtSecFirstName2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Saf2"]), string.Empty, tmpds2.Tables[0].Rows[0]["Saf2"]);
                        //                     txtSecMidName2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Sam2"]), string.Empty, tmpds2.Tables[0].Rows[0]["Sam2"]);
                        //                     txtSecLastName2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Sal2"]), string.Empty, tmpds2.Tables[0].Rows[0]["Sal2"]);
                        //                     txtSecFirstName3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Saf3"]), string.Empty, tmpds2.Tables[0].Rows[0]["Saf3"]);
                        //                     txtSecMidName3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Sam3"]), string.Empty, tmpds2.Tables[0].Rows[0]["Sam3"]);
                        //                     txtSecLastName3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Sal3"]), string.Empty, tmpds2.Tables[0].Rows[0]["Sal3"]);

                        //                     txtPTitle.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["SeriesParallelTitle"]), string.Empty, tmpds2.Tables[0].Rows[0]["SeriesParallelTitle"]);
                        //                     txtSubPTitle.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["SubSeriesParallelTitle"]), string.Empty, tmpds2.Tables[0].Rows[0]["SubSeriesParallelTitle"]);
                        //                     txtSecondParallelTitle.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["SSeriesParallelTitle"]), string.Empty, tmpds2.Tables[0].Rows[0]["SSeriesParallelTitle"]);

                        //                     txtMainissn.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["ISSNMain"]), string.Empty, tmpds2.Tables[0].Rows[0]["ISSNMain"]);

                        //                     txtSubissn.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["ISSNSub"]), string.Empty, tmpds2.Tables[0].Rows[0]["ISSNsub"]);

                        //                     txtSecondissn.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["ISSNSecond"]), string.Empty, tmpds2.Tables[0].Rows[0]["ISSNSecond"]);


                        //                     this.txtSubseriesname.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0]["SubSeriesName"]), string.Empty, tmpds2.Tables[0].Rows[0]["SubSeriesName"]);
                        //                     this.txtSubseriesno.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0]["SubseriesNo"]), string.Empty, tmpds2.Tables[0].Rows[0]["SubseriesNo"]);
                        //                     this.txtSubseriespart.Value = IIf(IsDBNull(tmpds2.Tables["Series"].Rows[0]["SubSeriesPart"]), string.Empty, tmpds2.Tables[0].Rows[0]["SubSeriesPart"]);
                        //                     // txtSubSVolume.Value = IIf(IsDBNull(tmpds2.Tables(0).Rows(0).Item("SubSvolume")) Or tmpds2.Tables(0).Rows(0).Item("SubSvolume") = 0, String.Empty, tmpds2.Tables(0).Rows(0).Item("SubSvolume"))

                        //                     if (!IsDBNull(tmpds2.Tables[0].Rows[0]["SubSvolume"]))
                        //                     {
                        //                         txtSubSVolume.Value = IIf(Operators.ConditionalCompareObjectEqual(tmpds2.Tables[0].Rows[0]["SubSvolume"], 0, false), string.Empty, tmpds2.Tables[0].Rows[0]["SubSvolume"]);
                        //                     }
                        //                     else
                        //                     {
                        //                         txtSubSVolume.Value = string.Empty;
                        //                     }

                        //                     this.Substatus.SelectedIndex = status.Items.IndexOf(status.Items.FindByText(tmpds2.Tables[0].Rows[0]["Subetal"]));
                        //                     Subaf1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subaf1"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subaf1"]);
                        //                     Subam1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subam1"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subam1"]);
                        //                     Subal1.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subal1"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subal1"]);
                        //                     Subaf2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subaf2"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subaf2"]);
                        //                     Subam2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subam2"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subam2"]);
                        //                     Subal2.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subal2"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subal2"]);
                        //                     Subaf3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subaf3"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subaf3"]);
                        //                     Subam3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subam3"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subam3"]);
                        //                     Subal3.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["Subal3"]), string.Empty, tmpds2.Tables[0].Rows[0]["Subal3"]);
                        //                     txtSubPTitle.Value = IIf(IsDBNull(tmpds2.Tables[0].Rows[0]["SubSeriesParallelTitle"]), string.Empty, tmpds2.Tables[0].Rows[0]["SubSeriesParallelTitle"]);
                        //                 }
                        //                 tmpda2.Dispose();
                        //                 tmpds2.Dispose();

                        //                 string q3 = "";
                        //                 if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(Session["marcnewid"], "", false)))
                        //                 {
                        //                     q3 = "SELECT * from bookrelators where ctrl_no='" + Session["marcnewid"].ToString() + "'";
                        //                 }
                        //                 else
                        //                 {
                        //                     q3 = "SELECT * from bookrelators where ctrl_no=" + Val(hdCtrlNo.Value);
                        //                 }
                        //                 var tmpda3 = new OleDbDataAdapter(q3, tmpCon);
                        //                 var tmpds3 = new DataSet();
                        //                 tmpda3.Fill(tmpds3, "Relators");
                        //                 if (tmpds3.Tables["Relators"].Rows.Count > 0)
                        //                 {
                        //                     this.editor1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][1]), string.Empty, tmpds3.Tables[0].Rows[0][1]);
                        //                     this.editor1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][2]), string.Empty, tmpds3.Tables[0].Rows[0][2]);
                        //                     this.editor1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][3]), string.Empty, tmpds3.Tables[0].Rows[0][3]);

                        //                     this.editor2fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][4]), string.Empty, tmpds3.Tables[0].Rows[0][4]);
                        //                     this.editor2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][5]), string.Empty, tmpds3.Tables[0].Rows[0][5]);
                        //                     this.editor2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][6]), string.Empty, tmpds3.Tables[0].Rows[0][6]);

                        //                     this.editor3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][7]), string.Empty, tmpds3.Tables[0].Rows[0][7]);
                        //                     this.editor3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][8]), string.Empty, tmpds3.Tables[0].Rows[0][8]);
                        //                     this.editor3Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][9]), string.Empty, tmpds3.Tables[0].Rows[0][9]);

                        //                     this.compiler1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][10]), string.Empty, tmpds3.Tables[0].Rows[0][10]);
                        //                     this.compiler1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][11]), string.Empty, tmpds3.Tables[0].Rows[0][11]);
                        //                     this.compiler1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][12]), string.Empty, tmpds3.Tables[0].Rows[0][12]);

                        //                     this.compiler2Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][13]), string.Empty, tmpds3.Tables[0].Rows[0][13]);
                        //                     this.compiler2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][14]), string.Empty, tmpds3.Tables[0].Rows[0][14]);
                        //                     this.compiler2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][15]), string.Empty, tmpds3.Tables[0].Rows[0][15]);

                        //                     this.compiler3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][16]), string.Empty, tmpds3.Tables[0].Rows[0][16]);
                        //                     this.compiler3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][17]), string.Empty, tmpds3.Tables[0].Rows[0][17]);
                        //                     this.compiler3Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][18]), string.Empty, tmpds3.Tables[0].Rows[0][18]);

                        //                     this.Illustrator1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][19]), string.Empty, tmpds3.Tables[0].Rows[0][19]);
                        //                     this.Illustrator1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][20]), string.Empty, tmpds3.Tables[0].Rows[0][20]);
                        //                     this.Illustrator1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][21]), string.Empty, tmpds3.Tables[0].Rows[0][21]);

                        //                     this.Illustrator2Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][22]), string.Empty, tmpds3.Tables[0].Rows[0][22]);
                        //                     this.Illustrator2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][23]), string.Empty, tmpds3.Tables[0].Rows[0][23]);
                        //                     this.Illustrator2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][24]), string.Empty, tmpds3.Tables[0].Rows[0][24]);

                        //                     this.Illustrator3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][25]), string.Empty, tmpds3.Tables[0].Rows[0][25]);
                        //                     this.Illustrator3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][26]), string.Empty, tmpds3.Tables[0].Rows[0][26]);
                        //                     this.Illustrator3lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][27]), string.Empty, tmpds3.Tables[0].Rows[0][27]);

                        //                     this.Translator1Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][28]), string.Empty, tmpds3.Tables[0].Rows[0][28]);
                        //                     this.Translator1Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][29]), string.Empty, tmpds3.Tables[0].Rows[0][29]);
                        //                     this.Translator1Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][30]), string.Empty, tmpds3.Tables[0].Rows[0][30]);

                        //                     this.Translator2Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][31]), string.Empty, tmpds3.Tables[0].Rows[0][31]);
                        //                     this.Translator2Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][32]), string.Empty, tmpds3.Tables[0].Rows[0][32]);
                        //                     this.Translator2Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][33]), string.Empty, tmpds3.Tables[0].Rows[0][33]);

                        //                     this.Translator3Fname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][34]), string.Empty, tmpds3.Tables[0].Rows[0][34]);
                        //                     this.Translator3Mname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][35]), string.Empty, tmpds3.Tables[0].Rows[0][35]);
                        //                     this.Translator3Lname.Value = IIf(IsDBNull(tmpds3.Tables["Relators"].Rows[0][36]), string.Empty, tmpds3.Tables[0].Rows[0][36]);

                        //                 }
                        //                 tmpda3.Dispose();
                        //                 tmpds3.Dispose();

                        //                 string q4 = "";
                        //                 if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(Session["marcnewid"], "", false)))
                        //                 {
                        //                     q4 = "SELECT * from bookconference where ctrl_no='" + Session["marcnewid"].ToString() + "'";
                        //                 }
                        //                 else
                        //                 {
                        //                     q4 = "SELECT * from bookconference where ctrl_no=" + Val(hdCtrlNo.Value);
                        //                 }
                        //                 var tmpda4 = new OleDbDataAdapter(q4, tmpCon);
                        //                 var tmpds4 = new DataSet();
                        //                 tmpda4.Fill(tmpds4, "Conference");
                        //                 if (tmpds4.Tables["Conference"].Rows.Count > 0)
                        //                 {
                        //                     this.txtSubtitle.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][1]), string.Empty, tmpds4.Tables[0].Rows[0][1]);
                        //                     this.txtParallelTitle.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][2]), string.Empty, tmpds4.Tables[0].Rows[0][2]);
                        //                     this.txtConferenceName.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][3]), string.Empty, tmpds4.Tables[0].Rows[0][3]);
                        //                     this.txtConferenceYear.Value = IIf(Operators.ConditionalCompareObjectEqual(tmpds4.Tables["Conference"].Rows[0][4], string.Empty, false), string.Empty, tmpds4.Tables[0].Rows[0][4]);
                        //                     this.txtBN.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][5]), string.Empty, tmpds4.Tables[0].Rows[0][5]);
                        //                     this.txtcn.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][6]), string.Empty, tmpds4.Tables[0].Rows[0][6]);
                        //                     this.txtgn.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][7]), string.Empty, tmpds4.Tables[0].Rows[0][7]);
                        //                     this.txtvn.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][8]), string.Empty, tmpds4.Tables[0].Rows[0][8]);
                        //                     this.txtsn.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0][9]), string.Empty, tmpds4.Tables[0].Rows[0][9]);
                        //                     txtan.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["ANNotes"]), string.Empty, tmpds4.Tables[0].Rows[0]["ANNotes"]);


                        //                     txtaname1.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdFname1"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdFname1"]);
                        //                     txtaname2.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdMname1"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdMname1"]);
                        //                     txtaname3.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdLname1"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdLname1"]);

                        //                     txtfname2.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdFname2"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdFname2"]);
                        //                     txtmname2.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdMname2"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdMname2"]);
                        //                     txtlname2.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdLname2"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdLname2"]);

                        //                     txtfname3.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdFname3"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdFname3"]);
                        //                     txtmname3.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdMname3"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdMname3"]);
                        //                     txtaname9.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["AdLName3"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["AdLName3"]);
                        //                     txtnarration.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["abstract"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["abstract"]);
                        //                     txtProgram.Value = IIf(IsDBNull(tmpds4.Tables["Conference"].Rows[0]["program_name"]), string.Empty, tmpds4.Tables["Conference"].Rows[0]["program_name"]);
                        //                 }
                    }
                    //             accds.Dispose();
                    //             accda.Dispose();
                    //             acccon.Close();
                    //             acccon.Dispose();

                    //             bookcatalogcom.Parameters.Clear();
                    //             bookcatalogcom.CommandType = CommandType.Text;
                    //             bookcatalogcom.CommandText = "select program_id from bookaccessionmaster where accessionnumber='" + Trim(this.txtacc.Value) + "'";
                    //             if (IsDBNull(bookcatalogcom.ExecuteScalar()))
                    //             {
                    //                 cmbCourse1.SelectedValue = 0;
                    //             }
                    //             else
                    //             {
                    //                 cmbCourse1.SelectedValue = Val(bookcatalogcom.ExecuteScalar());
                    //             }

                    bookcatalogcom.Dispose();
                    bookcatalogcon.Close();
                    bookcatalogcon.Dispose();



                }
                //         showAttachments(Request.QueryString["ctrl"], bookcatalogcom);

                if (hdFirstLoad.Value == "")
            {

                // Session("catalogdetailaudit") = Nothing
                //var gclas = new GlobClassTr();
                //string qery;
                //gclas.TrOpen();
                //qery = "  select ISNULL(max(TransNo),0)+1 from AuditTriggerMaster  ";
                //string transno = gclas.ExScaler(qery).ToString;
                //qery = "  select ISNULL(max(id),0)+1 from AuditTriggerMaster  ";
                //string id = gclas.ExScaler(qery).ToString;
                //qery = "  select ISNULL(max(id),0)+1 from AuditTriggerchild  ";
                //string idchild = gclas.ExScaler(qery).ToString;
                //// 'hdTransNo.Value = gCla.ExScaler(qery).ToString
                //// '        qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + transno + ",'CatalogDetail', 'bookaccessionmaster','select','accessionnumber','" + txtacc.Value + "' ) "
                //qery = "  insert into AuditTriggerMaster (id,transno,AppName,TableName,Operation,ipaddress,userid) values (  " + id.ToString() + "," + transno.ToString() + ",'CatalogDetail', 'bookaccessionmaster','select','" + Request.UserHostAddress.ToString() + "','" + Session["user_id"].ToString() + "' ) ";
                //gclas.IUD(qery);
                //// qery = " insert into DataUpdateTrigger (transno,AppName,TableName,Operation,ColumnName,ValueBefore) values (  " + transno + ",'CatalogDetail', 'bookaccessionmaster','select','userid','" + Session("user_id").ToString() + "' ) "
                //qery = "  insert into AuditTriggerChild (id,transno,masterid,columnname,valuebefore ) values ( " + idchild.ToString() + "," + transno.ToString() + "," + id.ToString() + ",'accessionnumber','" + txtacc.Value + "')";
                //gclas.IUD(qery);
                //gclas.TrClose();
                hdFirstLoad.Value = txtacc.Text;
            }
            if (Session["back"].ToString() != Resources.ValidationResources.bSearch)
            {
                // Label6jk.Visible = False
                // cmbStatus.Visible = False
                Labelb.Visible = false;
                txtrelease.Visible = false;
                Button1.Visible = false;
            }
        }
            catch(Exception exc)
            {
                message.PageMesg(exc.Message, this, dbUtilities.MsgLevel.Failure);
        }

        msglabel.Visible = true;
            //            msglabel.Text = Err.Description;
        }
    public void PopulateCurrency()
    {
        var CurrencyCon = new OleDbConnection(retConstr(""));
        try
        {
            CurrencyCon.Open();
            var Currencyda = new OleDbDataAdapter("Select distinct currencycode,currencyname from exchangemaster order by currencyname", CurrencyCon);
            var Currencyds = new DataSet();
            Currencyda.Fill(Currencyds, "currency");
            if (Currencyds.Tables["currency"].Rows.Count > 0)
            {
                cmbcurr.DataSource = Currencyds;
                cmbcurr.DataTextField = "currencyname";
                cmbcurr.DataValueField = "currencycode";
                cmbcurr.DataBind();
                cmbcurr.Items.Add(this.HComboSelect.Value);
                cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
            }
            else
            {
                cmbcurr.Items.Clear();
                cmbcurr.Items.Add(this.HComboSelect.Value);
                cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
            }
            Currencyds.Dispose();
        }
        catch (Exception ex)
        {
            message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            //                this.msglabel.Text = ex.Message();
        }
        finally
        {
            if (CurrencyCon.State == ConnectionState.Open)
            {
                CurrencyCon.Close();
            }
            CurrencyCon.Dispose();
        }
    }
    protected void catentry(OleDbConnection catcon)
    {
        try
        {
            var Catda = new OleDbDataAdapter("Select count(ctrl_no) from bookaccessionmaster where ctrl_no not in (" + 0 + ")", catcon);
            var Catds = new DataSet();
            Catda.Fill(Catds, "rec");
            if (Catds.Tables[0].Rows.Count > 0)
            {
                this.lbltotentry.Text = Catds.Tables[0].Rows[0][0].ToString();
            }
            /// for Uncatalogued
            var Catda1 = new OleDbDataAdapter("Select count(ctrl_no) from bookaccessionmaster where ctrl_no  in (" + 0 + ")", catcon);
            var Catds1 = new DataSet();
            Catda1.Fill(Catds1, "rec");
            if (Catds.Tables[0].Rows.Count > 0)
            {
                this.lbltotentryun.Text = Catds1.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            //this.msglabel.Text = ex.Message();
        }
    }
    protected void cmbcurr_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbbookcategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbdept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CmbGrdDept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CmbItemType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CatItemType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbOriCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbStatus_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        panel1.Visible = true;
        LinkButton1.BorderStyle = BorderStyle.Double;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.None;
        panel2.Visible = false;
        panel3.Visible = false;
        panel4.Visible = false;
        Panel5.Visible = false;
        panel6.Visible = false;
        panel7.Visible = false;
        panel8.Visible = false;
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.Double;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.None;

        panel2.Visible = true;
        panel3.Visible = false;
        panel4.Visible = false;
        Panel5.Visible = false;
        panel6.Visible = false;
        panel7.Visible = false;
        panel8.Visible = false;
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.Double;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.None;
        panel2.Visible = false;
        panel3.Visible = true;
        panel4.Visible = false;
        Panel5.Visible = false;
        panel6.Visible = false;
        panel7.Visible = false;
        panel8.Visible = false;
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.Double;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.None;
        panel2.Visible = false;
        panel3.Visible = false;
        panel4.Visible = true;
        Panel5.Visible = false;
        panel6.Visible = false;
        panel7.Visible = false;
        panel8.Visible = false;
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.Double;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.None;
        panel2.Visible = false;
        panel3.Visible = false;
        panel4.Visible = false;
        Panel5.Visible = true;
        panel6.Visible = false;
        panel7.Visible = false;
        panel8.Visible = false;

    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.Double;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.None;
        panel2.Visible = false;
        panel3.Visible = false;
        panel4.Visible = false;
        Panel5.Visible = false;
        panel6.Visible = true;
        panel7.Visible = false;
        panel8.Visible = false;

    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.Double;
        LinkButton8.BorderStyle = BorderStyle.None;
        panel2.Visible = false;
        panel3.Visible = false;
        panel4.Visible = false;
        Panel5.Visible = false;
        panel6.Visible = false;
        panel7.Visible = true;
        panel8.Visible = false;
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LinkButton1.BorderStyle = BorderStyle.None;
        LinkButton2.BorderStyle = BorderStyle.None;
        LinkButton3.BorderStyle = BorderStyle.None;
        LinkButton4.BorderStyle = BorderStyle.None;
        LinkButton5.BorderStyle = BorderStyle.None;
        LinkButton6.BorderStyle = BorderStyle.None;
        LinkButton7.BorderStyle = BorderStyle.None;
        LinkButton8.BorderStyle = BorderStyle.Double;
        panel2.Visible = false;
        panel3.Visible = false;
        panel4.Visible = false;
        Panel5.Visible = false;
        panel6.Visible = false;
        panel7.Visible = false;
        panel8.Visible = true;
    }
    public void GeberateCtrlNo()
    {
        try
        {
            var ctrlCon = new OleDbConnection(retConstr(""));
            var ctrlCom = new OleDbCommand();
            ctrlCon.Open();
            ctrlCom.Connection = ctrlCon;
            ctrlCom.CommandType = CommandType.Text;
            ctrlCom.CommandText = "select prefix,currentposition,suffix from IdTable where objectName=N'catalog'";
            var dr = ctrlCom.ExecuteReader();
            dr.Read();
            hdCtrlNo.Value = (Convert.ToInt32(dr[1].ToString()) + 1).ToString();
            ctrlCom.Dispose();
            ctrlCon.Close();
            ctrlCon.Dispose();
        }
        catch (Exception ex)
        {
            msglabel.Visible = true;
            msglabel.Text = ex.Message;
        }
    }

    public void clearfields()
    {
        try
        {
                // ****************************************
                hdimgdata.Value = "";image1.Src = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=";
                txtLoc2.Text = "";
            txtlocation.Text = "";
            txtMainissn.Value = string.Empty;
            af1.Value = string.Empty;
            am1.Value = string.Empty;
            al1.Value = string.Empty;
            af2.Value = string.Empty;
            am2.Value = string.Empty;
            al2.Value = string.Empty;
            af3.Value = string.Empty;
            am3.Value = string.Empty;
            al3.Value = string.Empty;

            txtSubseriesname.Value = string.Empty;
            txtSubPTitle.Value = string.Empty;
            txtSubseriesno.Value = string.Empty;

            txtSubSVolume.Value = string.Empty;
            txtSubseriespart.Value = string.Empty;
            txtSubissn.Value = string.Empty;

            Subaf1.Value = string.Empty;
            Subam1.Value = string.Empty;
            Subal1.Value = string.Empty;
            Subaf2.Value = string.Empty;
            Subam2.Value = string.Empty;
            Subal2.Value = string.Empty;
            Subaf3.Value = string.Empty;
            Subam3.Value = string.Empty;
            Subal3.Value = string.Empty;
            txtSecondSeriesPart.Value = string.Empty;
            txtSecondSeriesTitle.Value = string.Empty;
            txtSecondParallelTitle.Value = string.Empty;
            txtSecondSeriesNo.Value = string.Empty;

            txtSecondissn.Value = string.Empty;
            txtSecFirstName1.Value = string.Empty;
            txtSecMidName1.Value = string.Empty;
            txtSecLastName1.Value = string.Empty;
            txtSecFirstName2.Value = string.Empty;
            txtSecMidName2.Value = string.Empty;
            txtSecLastName2.Value = string.Empty;
            txtSecFirstName3.Value = string.Empty;
            txtSecMidName3.Value = string.Empty;
            txtSecLastName3.Value = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Value = string.Empty;
            txtacc.Text = string.Empty;
            txtPubYear.Value = string.Empty;

            txtan.Value = string.Empty;
            txtIssnNo.Value = string.Empty;

            txtProgram.Value = string.Empty;
            txtaname1.Value = string.Empty;
            txtaname2.Value = string.Empty;
            txtaname3.Value = string.Empty;
            txtfname2.Value = string.Empty;
            txtmname2.Value = string.Empty;
            txtlname2.Value = string.Empty;
            txtfname3.Value = string.Empty;

            txtmname3.Value = string.Empty;
            txtaname9.Value = string.Empty;
            txtnarration.Value = string.Empty;

            // ========================================
            txtbookprice.Value = string.Empty;
            // txtclassno.Value = String.Empty
            // txtbookno.Value = String.Empty
            // txtdate.Text = String.Empty
            cmbboundind.SelectedIndex = cmbboundind.Items.Count - 1;
            txtvolno.Value = string.Empty;
            txtinitpages.Value = string.Empty;
            txtpages.Value = string.Empty;
            txtleaves.Value = string.Empty;
            txtparts.Value = string.Empty;
            // txtbooktype.Value = String.Empty
            txttitle.Value = string.Empty;
            txtUniformTitle.Value = string.Empty;
            // txtauthors.Value = String.Empty
            txtau1firstnm.Value = string.Empty;
            txtau1midnm.Value = string.Empty;
            txtau1surnm.Value = string.Empty;
            txtau2firstnm.Value = string.Empty;
            txtau2midnm.Value = string.Empty;
            txtau2surnm.Value = string.Empty;
            txtau3firstnm.Value = string.Empty;
            txtau3midnm.Value = string.Empty;
            txtau3surnm.Value = string.Empty;
            this.txtCmbPublisher.Text = "";
            // cmbpubnm.SelectedIndex = cmbpubnm.Items.Count - 1
            txtedition.Value = string.Empty;
            txteditionyear.Value = string.Empty;
            txtisbn.Value = string.Empty;
            txtSub11.Text = string.Empty;
            txtsub2.Text = string.Empty;
            txtsub3.Text = string.Empty;

            this.editor1Fname.Value = string.Empty;
            editor1Mname.Value = string.Empty;
            this.editor1Lname.Value = string.Empty;
            this.editor2fname.Value = string.Empty;
            this.editor2Lname.Value = string.Empty;
            this.editor2Mname.Value = string.Empty;
            this.editor3Fname.Value = string.Empty;
            this.editor3Lname.Value = string.Empty;
            this.editor3Mname.Value = string.Empty;
            this.compiler1Fname.Value = string.Empty;
            this.compiler1Lname.Value = string.Empty;
            this.compiler1Mname.Value = string.Empty;
            this.compiler2Fname.Value = string.Empty;
            this.compiler2Lname.Value = string.Empty;
            this.compiler2Mname.Value = string.Empty;
            this.compiler3Fname.Value = string.Empty;
            this.compiler3Lname.Value = string.Empty;
            this.compiler3Mname.Value = string.Empty;
            this.Translator1Fname.Value = string.Empty;
            this.Translator1Lname.Value = string.Empty;
            this.Translator1Mname.Value = string.Empty;
            this.Translator2Fname.Value = string.Empty;
            this.Translator2Lname.Value = string.Empty;
            this.Translator2Mname.Value = string.Empty;
            this.Translator3Fname.Value = string.Empty;
            this.Translator3Lname.Value = string.Empty;
            this.Translator3Mname.Value = string.Empty;
            this.Illustrator1Fname.Value = string.Empty;
            this.Illustrator1Lname.Value = string.Empty;
            this.Illustrator1Mname.Value = string.Empty;
            this.Illustrator2Fname.Value = string.Empty;
            this.Illustrator2Lname.Value = string.Empty;
            this.Illustrator2Mname.Value = string.Empty;
            this.Illustrator3Fname.Value = string.Empty;
            this.Illustrator3Fname.Value = string.Empty;
            this.Illustrator3lname.Value = string.Empty;
            this.Illustrator3Mname.Value = string.Empty;
            this.Illustrator3Mname.Value = string.Empty;

            this.txtvn.Value = string.Empty;
            this.txtgn.Value = string.Empty;
            this.txtsn.Value = string.Empty;
            this.txtBN.Value = string.Empty;
            this.txtcn.Value = string.Empty;

            this.cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
            this.cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
            this.cmbbookcategory.SelectedIndex = cmbbookcategory.Items.Count - 1;

            this.txtseriesname.Value = string.Empty;
            this.txtseriesno.Value = string.Empty;
            this.txtseriespart.Value = string.Empty;

            this.txtSubtitle.Value = string.Empty;
            this.txtPTitle.Value = string.Empty;
            this.txtConferenceName.Value = string.Empty;
            this.txtConferenceYear.Value = string.Empty;
            this.txtCopyNo.Value = string.Empty;

            this.txtbooksize.Value = string.Empty;
            this.txtvolpages.Value = string.Empty;
            this.txtlccn.Value = string.Empty;
            this.txtbiblpages.Value = string.Empty;
            this.txtmaps.Value = string.Empty;
            // Me.chkSearch.Checked = False
            // txtANo .Text = string.Empty;
            txtmaterialinfo.Value = string.Empty;
            var DT = new DataTable();
            grdcopy.DataSource = DT;
            grdcopy.DataBind();
            DT.Dispose();
            panel1.Visible = false;
            LinkButton1.BorderStyle = BorderStyle.None;
            LinkButton2.BorderStyle = BorderStyle.None;
            LinkButton3.BorderStyle = BorderStyle.None;
            LinkButton4.BorderStyle = BorderStyle.None;
            LinkButton5.BorderStyle = BorderStyle.None;
            LinkButton6.BorderStyle = BorderStyle.None;
            LinkButton7.BorderStyle = BorderStyle.Double;
            LinkButton8.BorderStyle = BorderStyle.None;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            Panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
            panel8.Visible = false;
            //return default;
        }
        catch
        {
        }

    }
    protected void cmdReset_ServerClick(object sender, System.EventArgs e)
    {
        clearfields();
    }

    protected void cmdReset1_1_Click(object sender, EventArgs e)
    {
        clearfields();
    }
    private void cmdsave_ServerClick(object sender, System.EventArgs e)
    {
        // Dim bookcatalogcon As New OleDbConnection(retConstr(Session("LibWiseDBConn")))
        if (cmbdept.SelectedItem.Text == "---Select---")
        {
            message.PageMesg("Select department", this, dbUtilities.MsgLevel.Warning);
            return;
        }
        if (cmbMediaType.SelectedItem.Text == "---Select---")
        {
            message.PageMesg("Select media", this, dbUtilities.MsgLevel.Warning);
            return;
        }
        if (cmbLanguage.SelectedItem.Text == "---Select---")
        {
            message.PageMesg("Select language", this, dbUtilities.MsgLevel.Warning);
            return;
        }
        if (cmbbookcategory.SelectedItem.Text == "---Select---")
        {
            message.PageMesg("Select book category", this, dbUtilities.MsgLevel.Warning);
            return;
        }

        if (cmbStatus.SelectedItem.Text == "---Select---")
        {
            message.PageMesg("Please first Asign the Status to the book!", this, dbUtilities.MsgLevel.Warning);
            return;
        }
        if (cmbtype.SelectedValue == "---Select---")
        {
            message.PageMesg("Select Item type", this, dbUtilities.MsgLevel.Warning);
            return;
        }
        if (txtCopyNo.Value.Trim() == "")
        {

            message.PageMesg("Copy no required.", this, dbUtilities.MsgLevel.Warning);
            return;
        }
        else
        {
            try
            {
                int cp = Convert.ToInt16((txtCopyNo.Value.Trim()));

            }
            catch
            {
                message.PageMesg("Valid Copy no required.", this, dbUtilities.MsgLevel.Warning);
                return;

            }

        }

        // Exit Sub
        using (var bookcatalogcon = new OleDbConnection(retConstr("")))
        {
            try
            {
                bookcatalogcon.Open();
            }
            catch (Exception ex)
            {
                msglabel.Visible = true;
                msglabel.Text = ex.Message;
                return;
            }
            string tcase;
            //                tcase = AppSettings.Get("tcase");

            // if (hdConfirm.Value != "Y")
            //{
            //    if Session["back"] != "catalog" Then
            //    // Dim chkds As New DataSet
            //    // chkds = LibObj.PopulateDataset("SELECT ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name from  dbo.CatalogueCardView where  classnumber=N'" & Trim(Me.txtclassno.Value).Replace("'", "''") & "' AND booknumber=N'" & Trim(Me.txtbookno.Value).Replace("'", "''") & "' and ctrl_no <> '" & hdCtrlNo.Value & "' order by copynumber", "dup", bookcatalogcon)
            //    // If chkds.Tables("dup").Rows.Count > 0 Then
            //    // hdForMesage.Value = "X"
            //    // 'grdcopy.DataSource = chkds.Tables("dup")
            //    // 'grdcopy.DataBind()
            //    // 'bookcatalogcon.Close()
            //    // 'bookcatalogcon.Dispose()
            //    // chkds.Dispose()
            //    // Exit Sub
            //    End If

            //}
            Session["ctrlnoforeresources"] = hdCtrlNo.Value;
            Session["itemvalue"] = cmbtype.SelectedValue;
            if (cmbtype.SelectedItem.Text == "Articles")
            {
                Session["Title"] = "E-Articles";
            }
            else if (cmbtype.SelectedItem.Text == "Project Reports")
            {
                Session["Title"] = "E-Project Reports";
            }
            else if (cmbtype.SelectedItem.Text == "Thesis")
            {
                Session["Title"] = "E-Thesis";
            }
            else if (cmbtype.SelectedItem.Text == "Journals")
            {
                Session["Title"] = "E-Journals";
            }
            else if (cmbtype.SelectedItem.Text == "Books" | cmbtype.SelectedItem.Text == "E-Books")
            {
                Session["Title"] = "E-Books";
            }
            else
            {
                Session["Title"] = "Add E-Resources";
            }
            var Titleds = new DataSet();
            int counter;
            var tempstr = default(string);
            // dim sqlAD as String=
            // Dim TitleAD As New OleDbDataAdapter("Select Accessionnumber,booktitle from BookAccessionmaster where booktitle='" & HdBookTitle.Value & "' and ctrl_no=0", bookcatalogcon)
            var TitleAD = new OleDbDataAdapter("Select Accessionnumber,booktitle from BookAccessionmaster where booktitle=? and ctrl_no=0", bookcatalogcon);
            TitleAD.SelectCommand.Parameters.AddWithValue("@booktitle", HdBookTitle.Value); // OleDbType.VarWChar, 200).Value = HdBookTitle.Value
            TitleAD.Fill(Titleds, "Tbl");
            if (Titleds.Tables["Tbl"].Rows.Count > 0)
            {
                var loopTo = Titleds.Tables["Tbl"].Rows.Count - 1;
                for (counter = 0; counter <= loopTo; counter++)
                {
                    if (txttitle.Value.ToUpper() != Titleds.Tables["Tbl"].Rows[counter]["booktitle"].ToString().ToUpper())
                    {
                        if (string.IsNullOrEmpty(tempstr))
                        {
                            tempstr = "'" + Titleds.Tables["Tbl"].Rows[counter]["Accessionnumber"].ToString() + "'";
                        }
                        else
                        {
                            tempstr = tempstr + "," + "'" + Titleds.Tables["Tbl"].Rows[counter]["Accessionnumber"].ToString() + "'";
                        }

                    }

                }

            }

            System.IFormatProvider x;
            // Dim tran As OleDb.OleDbTransaction
            // tran = bookcatalogcon.BeginTransaction
            var catalogcom1 = new OleDbCommand();
            // catalogcom.Transaction = tran
            catalogcom1.Connection = bookcatalogcon;

            // Block To Check The Duplicacy of copy Number

            bool blnIsFound = false;

            bool isNewCtrl = false;
            if ((Session["back"].ToString() == "catalog") && (hdCtrlNo.Value == string.Empty))  // 'If the form is openedfor new record entry without any matching criteria  then genearting new ctrlNo
            {
                GeberateCtrlNo();
                isNewCtrl = true;
            }
            else   // if opened for new item entry and but already catalogued entries found 
            {
                hdCtrlNo.Value = hdCtrlNo.Value;
            }

            Int16 iCntr = 0;
            Int16 CopyNo = 0;
            // Dim ClassCon As New OleDbConnection(retConstr(Session("LibWiseDBConn")))
            // ClassCon.Open()
            CopyNo = Convert.ToInt16(txtCopyNo.Value);

            if (Session["back"].ToString() == "catalog")
            {
                if (txtCopyNo.Value != string.Empty)
                {
                    if (LibObj.isDulicate("select Copynumber,ctrl_no from bookaccessionmaster where  Copynumber='" + txtCopyNo.Value.Replace("'", "''") + "' AND ctrl_no='" + hdCtrlNo.Value.Replace("'", "''") + "' ", bookcatalogcon) == true)
                    {
                        // hdCheck.Value = "found"
                        message.PageMesg(Resources.ValidationResources.DpCNoFPlzCkCNo, this, dbUtilities.MsgLevel.Warning);
                        // bookcatalogcon.Close()
                        // bookcatalogcon.Dispose()
                        hdCtrlNo.Value = string.Empty;
                        return;
                    }
                }
                else if (Convert.ToInt32(txtCopyNo.Value) == 0)
                {
                    message.PageMesg(Resources.ValidationResources.PlzCkCNoFndInV, this, dbUtilities.MsgLevel.Warning);
                    // hdCheck.Value = "Empty"
                    // bookcatalogcon.Close()
                    // bookcatalogcon.Dispose()
                    hdCtrlNo.Value = string.Empty;
                    return;
                }
            }


            var loopTo1 = grdcopy.Items.Count - 1;
            for (iCntr = 0; iCntr <= loopTo1; iCntr++)
            {
                var txtC = new HtmlInputText();
                txtC = (HtmlInputText)grdcopy.Items[iCntr].Cells[1].FindControl("txtCopyInfo");
                if (CopyNo == Convert.ToInt16(txtC.Value))
                {
                    blnIsFound = true;
                    // hdCheck.Value = "found"
                    message.PageMesg(Resources.ValidationResources.DpCNoFPlzCkCNo, this, dbUtilities.MsgLevel.Warning);
                    // bookcatalogcon.Close()
                    // bookcatalogcon.Dispose()
                    //   txtCopyNo.Focus();

                    if (Session["back"].ToString() == "catalog")
                    {
                        hdCtrlNo.Value = string.Empty;
                    }
                    return;
                }
            }

            if (blnIsFound == false)
            {
                var loopTo2 = grdcopy.Items.Count - 1;
                for (iCntr = 0; iCntr <= loopTo2; iCntr++)
                {
                    var txtC = new HtmlInputText();
                    CopyNo = Convert.ToInt16(((HtmlInputText)grdcopy.Items[iCntr].Cells[1].FindControl("txtCopyInfo")).Value);
                    Int16 i;
                    var loopTo3 = grdcopy.Items.Count - 1;
                    for (i = 1; i <= loopTo3; i++)
                    {
                        txtC = (HtmlInputText)grdcopy.Items[i].Cells[1].FindControl("txtCopyInfo");
                        if (Convert.ToInt32(txtC.Value) == 0)
                        {
                            // hdCheck.Value = "Empty"
                            if (Session["back"].ToString() == "catalog")
                            {
                                hdCtrlNo.Value = string.Empty;
                            }
                            message.PageMesg(Resources.ValidationResources.PlzCkCNoFndInV, this, dbUtilities.MsgLevel.Warning);
                            // bookcatalogcon.Close()
                            // bookcatalogcon.Dispose()
                            return;
                        }
                        if (CopyNo == Convert.ToInt16(txtC.Value) & iCntr != i)
                        {
                            blnIsFound = true;
                            // hdCheck.Value = "found"
                            // bookcatalogcon.Close()
                            // bookcatalogcon.Dispose()
                            if (Session["back"].ToString() == "catalog")
                            {
                                hdCtrlNo.Value = string.Empty;
                            }
                            message.PageMesg(Resources.ValidationResources.DpCNoFPlzCkCNo, this, dbUtilities.MsgLevel.Warning);
                            return;
                        }

                        if (Session["back"].ToString() == "catalog")  // to check copy number duplicacy only while saving new record
                        {
                            if (LibObj.isDulicate("select Copynumber,ctrl_no from bookaccessionmaster where  Copynumber='" + txtC.Value.Replace("'", "''") + "' AND ctrl_no='" + hdCtrlNo.Value.Replace("'", "''") + "' ", bookcatalogcon) == true)
                            {
                                // hdCheck.Value = "found"
                                hdCtrlNo.Value = string.Empty;
                                message.PageMesg(Resources.ValidationResources.DpCNoFPlzCkCNo, this, dbUtilities.MsgLevel.Warning);
                                // bookcatalogcon.Close()
                                // bookcatalogcon.Dispose()
                                return;
                            }
                        }
                    }
                }
            }

            // '''' for image convert in binary form
            int imglen;
            byte[] imgbin = new byte[] { 1, 0, 1, 1 };
                /*
                if (cmdsave.Value == Resources.ValidationResources.bSave)
                {
                    if (aa2 is not null)
                    {
                        // If aa1.Trim.Length > 0 And aa2.ContentLength > 0 Then
                        // Dim imgStream As Stream = aa2.InputStream()
                        // imglen = aa2.ContentLength
                        // Dim imgBinaryData(imglen) As Byte
                        // Dim n As Int32 = imgStream.Read(imgBinaryData, 0, imglen)
                        //imgbin = (byte[])Session["coverimg"];
                    }
                    // End If
                    else
                    {
                        var cmd = new OleDbCommand("select CoverPage from BookImage where ctrl_no='" + Trim(hdCtrlNo.Value) + "'", bookcatalogcon);
                        var dr = cmd.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows)
                        {
                            imgbin = (byte[])dr.GetValue(0);
                        }
                        dr.Close();
                        // Hidden3.Value = "30"
                    }
                }
                else if (aa2 is not null)
                {
                    if (aa1.Trim().Length > 0 & aa2.ContentLength > 0)
                    {
                        var imgStream = aa2.InputStream;
                        imglen = aa2.ContentLength;
                        var imgBinaryData = new byte[imglen + 1];
                        Int32 n = imgStream.Read(imgBinaryData, 0, imglen);
                        imgbin = imgBinaryData;
                    }
                }
                else
                {
                    // Dim constr As String
                    // Dim conn As New OleDb.OleDbConnection(retConstr(Session("LibWiseDBConn")))
                    // conn.Open()
                    var cmd = new OleDbCommand("select CoverPage from BookImage where ctrl_no='" + Trim(hdCtrlNo.Value) + "'", bookcatalogcon);
                    var dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        imgbin = (byte[])dr.GetValue(0);
                    }
                    dr.Close();
                }
                */
                // ''''''image store in  bookimage
                var cmd = new OleDbCommand("select CoverPage from BookImage where ctrl_no='" + hdCtrlNo.Value + "'", bookcatalogcon);
                DataTable dtImg = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                oda.Fill(dtImg);
                if (dtImg.Rows.Count > 0)
                {
                    cmd = new OleDbCommand("delete from BookImage where ctrl_no='" + hdCtrlNo.Value + "'", bookcatalogcon);
                    cmd.ExecuteNonQuery();
//                    imgbin = (byte[])dtImg.Rows[0]["CoverPage"];
                }
                if (hdimgdata.Value != "")
                {
                    var dpart=hdimgdata.Value.Split(',');
                    imgbin = (byte[]) Convert.FromBase64String(  dpart[1]);
                }
                if ((txtau1firstnm.Value.Trim() == string.Empty) || (editor1Fname.Value.Trim() == string.Empty) || (compiler1Fname.Value.Trim() == string.Empty) | !(Translator1Fname.Value == string.Empty) | !(Illustrator1Fname.Value == string.Empty) || (txtConferenceName.Value.Trim() == string.Empty))
            {
                catalogcom1.Parameters.Clear();
                if (hdPublisherId.Value == default)
                {
                    message.PageMesg(Resources.ValidationResources.IPubNotExist, this, dbUtilities.MsgLevel.Warning);
                    //   txtCmbPublisher.Focus();
                    if (Session["back"].ToString() == "catalog")
                    {
                        hdCtrlNo.Value = string.Empty;
                    }
                    return;
                }
                else
                {
                    string sqlstr = "Select publishermaster.publisherid  from  publishermaster,addresstable where  firstname+', '+percity=N'" + txtCmbPublisher.Text.Replace("'", "''") + "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'";

                    DataSet ds_pub = LibObj.PopulateDataset(sqlstr, "tbl", bookcatalogcon);
                    if (ds_pub.Tables[0].Rows.Count == 0)
                    {
                        message.PageMesg(Resources.ValidationResources.IPubNotExist, this, dbUtilities.MsgLevel.Warning);
                        //                            txtCmbPublisher.Focus();
                        if (Session["back"].ToString() == "catalog")
                        {
                            hdCtrlNo.Value = string.Empty;
                        }
                        return;
                    }
                    else
                    {
                        // asmpublisher.SelectedValue = ds_pub.Tables(0).Rows(0).Item(0)
                        hdPublisherId.Value = ds_pub.Tables[0].Rows[0][0].ToString();
                    }


                }
                // ************1111
                // ******************vendor

                if (txtCmbVendor.Text.ToLower() != string.Empty)
                {
                    string sqlstr = "Select VendorName+', '+percity as VendorName from  VendorMaster join addresstable on  vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor' and vendorName+', '+percity LIKE N'" + txtCmbVendor.Text.Replace("'", "''") + "'";
                    catalogcom1.CommandType = CommandType.Text;
                    catalogcom1.CommandText = sqlstr;
                    DataSet ds_vend = LibObj.PopulateDataset(sqlstr, "tbl", bookcatalogcon);
                    if (ds_vend.Tables[0].Rows.Count == 0)
                    {
                        if (Session["back"].ToString() == "catalog")
                        {
                            hdCtrlNo.Value = string.Empty;
                        }
                        message.PageMesg("Error in Vendor, select correct vendor", this, dbUtilities.MsgLevel.Warning);
                        // txtCmbVendor.Focus()
                        return;

                    }
                    if (txtCmbVendor.Text.ToUpper().Trim() != ds_vend.Tables[0].Rows[0][0].ToString().ToUpper())
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.VendorNotFound.ToString, Me)
                        if (Session["back"].ToString() == "catalog")
                        {
                            hdCtrlNo.Value = string.Empty;
                        }
                        message.PageMesg("Error in Vendor, select correct vendor", this, dbUtilities.MsgLevel.Warning);
                        // txtCmbVendor.Focus()
                        return;
                    }
                }
                // **********************

                catalogcom1.CommandType = CommandType.Text;
                // If asmpublisher.SelectedValue <> Nothing Then
                // catalogcom1.CommandText = "select firstname,percity,perstate,percountry,peraddress from publishermaster inner join addresstable on publishermaster.publisherid=addresstable.addid where addresstable.addrelation=N'publisher' and publishermaster.publisherid=N'" & asmpublisher.SelectedValue & "'"
                if (hdPublisherId.Value != default)
                {
                    catalogcom1.CommandText = "select firstname,percity,perstate,percountry,peraddress from publishermaster inner join addresstable on publishermaster.publisherid=addresstable.addid where addresstable.addrelation=N'publisher' and publishermaster.publisherid=N'" + hdPublisherId.Value + "'";
                }
                var tempds = new DataSet();
                var tempda = new OleDbDataAdapter();
                tempda.SelectCommand = catalogcom1;
                tempda.Fill(tempds, "publisher");

                string pname = default, paddress = default, pcity = default, pstate = default, pCountry = default;

                if (tempds.Tables["publisher"].Rows.Count > 0)
                {
                    pname = tempds.Tables["publisher"].Rows[0]["firstname"].ToString();
                    paddress = tempds.Tables["publisher"].Rows[0]["peraddress"].ToString();
                    pcity = tempds.Tables["publisher"].Rows[0]["percity"].ToString();
                    pstate = tempds.Tables["publisher"].Rows[0]["perstate"].ToString();
                    pCountry = tempds.Tables["publisher"].Rows[0]["percountry"].ToString();
                }
                tempda.Dispose();
                tempds.Dispose();

                // ****************************
                OleDbTransaction tran;
                tran = bookcatalogcon.BeginTransaction();
                string sUpdAccn;
                sUpdAccn = "  if exists( select * from FeaturesPer where FID=15 ) "; // setting booknumber individual accn
                sUpdAccn += " begin  /*DISABLE  TRIGGER trig_update_bookaccessionmaster ON bookaccessionmaster deleted*/; ";
                sUpdAccn += " update bookaccessionmaster set booknumber='" + txtbookno.Value.Trim() + "' ";
                sUpdAccn += " where accessionnumber='" + txtacc.Text.Trim() + "'; ";
                sUpdAccn += " /*ENABLE TRIGGER trig_update_bookaccessionmaster ON bookaccessionmaster */end ";




                var catalogcom = new OleDbCommand();
                catalogcom.Transaction = tran;
                catalogcom.Connection = bookcatalogcon;
                try
                {

                    var Accncom = new OleDbCommand();
                    Accncom.Transaction = tran;
                    Accncom.Connection = bookcatalogcon;
                    Accncom.CommandType = CommandType.Text;
                    Accncom.CommandText = sUpdAccn;
                    Accncom.ExecuteNonQuery();
                    catalogcom.CommandType = CommandType.Text;
                    //                       catalogcom.CommandText = "  select ISNULL(max(TransNo),0)+1 from AuditTriggerMaster  ";
                    //                     string transno = Conversions.ToString(catalogcom.ExecuteScalar());
                    string updTransNo = " transno=" + "0" + ",appname='CatalogDetail',ipaddress='" + Request.UserHostAddress + "',userid='" + LoggedUser.Logged().User_Id + "', ";
                    var cls = new GlobClassTr();
                    // cls.TrOpen()
                    // Dim qer As String = " DISABLE  TRIGGER trig_update_bookaccessionmaster ON bookaccessionmaster  "
                    // cls.IUD(qer)
                    // qer = " DISABLE  TRIGGER trig_update_bookauthor ON bookauthor "
                    // cls.IUD(qer)
                    // qer = " DISABLE  TRIGGER trig_update_bookcatalog ON bookcatalog   "
                    // cls.IUD(qer)
                    // qer = " DISABLE  TRIGGER trig_update_bookconference ON bookconference  "
                    // cls.IUD(qer)
                    // qer = " DISABLE  TRIGGER trig_update_catalogdata ON catalogdata   "
                    // cls.IUD(qer)
                    // qer += "  "
                    // qer += "  "
                    // cls.TrClose()


                    catalogcom.CommandType = CommandType.StoredProcedure;
                    catalogcom.CommandTimeout = 300;
                    catalogcom.CommandText = "insert_CatalogData_1";
                    catalogcom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
                    catalogcom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;
                    // Value is being stored in session variable so that it can be directly used in catalog detail entry form load event
                    catalogcom.Parameters.Add(new OleDbParameter("@classnumber_2", OleDbType.VarWChar));
                    catalogcom.Parameters["@classnumber_2"].Value = txtclassno.Value;
                    catalogcom.Parameters.Add(new OleDbParameter("@booknumber_3", OleDbType.VarWChar));
                    catalogcom.Parameters["@booknumber_3"].Value = txtbookno.Value;
                    catalogcom.Parameters.Add(new OleDbParameter("@TransNo", OleDbType.Integer));
                    catalogcom.Parameters["@transNo"].Value = 0; //later
                    catalogcom.ExecuteNonQuery();
                    catalogcom.Parameters.Clear();
                    // '''''''''''''''''''
                    // catalogcom.CommandType = CommandType.StoredProcedure
                    // catalogcom.CommandText = "insert_BookCatalog_1"
                    // catalogcom.Connection = bookcatalogcon
                    hdpublisheridNew.Value = this.hdPublisherId.Value;
                    // If hdPublisherId.Value = Nothing Or Me.HWhichFill.Value = "PublisherMaster" Then
                    // hdpublisheridNew.Value = Me.hdPublisherId.Value
                    // Else
                    // hdpublisheridNew.Value = Trim(asmpublisher.SelectedValue)
                    // End If








                    txtleaves.Value = "0";
                    var insCatg = ObjCatalog.InsertFunctionStr(Convert.ToInt32(hdCtrlNo.Value), Convert.ToDateTime(txtdate.Text), Convert.ToInt32(cmbbookcategory.SelectedValue),
                        txtvolno.Value,
                        txtinitpages.Value == "" ? "0" : txtinitpages.Value, Convert.ToInt32(txtpages.Value),
                        txtparts.Value, Convert.ToInt32(txtleaves.Value), cmbboundind.SelectedItem.Text, txttitle.Value,
                          Convert.ToInt32(hdpublisheridNew.Value),
                          txtedition.Value, txtisbn.Value,
                         txtSub11.Text, txtsub2.Text, txtsub3.Text, txtbooksize.Value,
                         txtlccn.Value, txtvolpages.Value, txtbiblpages.Value, cboBookIndex.SelectedItem.Text,
                         cboIllistration.SelectedItem.Text, cbovariouspaging.SelectedItem.Text, 0,
                         cboEditorETAL.SelectedItem.Text, cboCompilerETAL.SelectedItem.Text,
                         cboILLustratorETAL.SelectedItem.Text,
                         cboTranslatorETAL.SelectedItem.Text, cboAuthorETAL.SelectedItem.Text,
                         txtmaterialinfo.Value, cmbMediaType.SelectedItem.Text, txtIssnNo.Value, txtVolumeNo.Value,
                         Convert.ToInt32(cmbdept.SelectedValue), Convert.ToInt32(cmbLanguage.SelectedValue), this.txtPart.Value,
                         "", this.txtCmbVendor.Text.Replace("'", "''"), "", pname, pcity, pstate, pCountry,
                         paddress.Replace("'", "''"), cmbdept.SelectedItem.Text.Replace("'", "''"),
                         cmbtype.SelectedItem.Text, cmbLanguage.SelectedItem.Text,
                          cmbbookcategory.SelectedItem.Text, 0, txtControlNo.Text.Trim(),

                        catalogcom);
                    if (insCatg == "")
                    {
                    }
                    else
                    {
                        tran.Rollback();
                        bookcatalogcon.Close();
                        message.PageMesg(insCatg, this, dbUtilities.MsgLevel.Failure);
                        return;
                    }

                    catalogcom.Parameters.Clear();

                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;

                    catalogcom.Parameters.Clear();

                    catalogcom.CommandType = CommandType.StoredProcedure;

                    catalogcom.CommandText = "insert_BookAuthor_1";
                    catalogcom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));

                    catalogcom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;
                    // =====Ashish==
                    catalogcom.Parameters.Add(new OleDbParameter("@firstname1_2", OleDbType.VarWChar));
                    catalogcom.Parameters["@firstname1_2"].Value = txtau1firstnm.Value.Trim();


                    catalogcom.Parameters.Add(new OleDbParameter("@middlename1_3", OleDbType.VarWChar));
                    catalogcom.Parameters["@middlename1_3"].Value = txtau1midnm.Value.Trim();



                    catalogcom.Parameters.Add(new OleDbParameter("@lastname1_4", OleDbType.VarWChar));
                    catalogcom.Parameters["@lastname1_4"].Value = txtau1surnm.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@firstname2_5", OleDbType.VarWChar));
                    catalogcom.Parameters["@firstname2_5"].Value = txtau2firstnm.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@middlename2_6", OleDbType.VarWChar));
                    catalogcom.Parameters["@middlename2_6"].Value = txtau2midnm.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@lastname2_7", OleDbType.VarWChar));
                    catalogcom.Parameters["@lastname2_7"].Value = txtau2surnm.Value;



                    catalogcom.Parameters.Add(new OleDbParameter("@firstname3_8", OleDbType.VarWChar));
                    catalogcom.Parameters["@firstname3_8"].Value = txtau3firstnm.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@middlename3_9", OleDbType.VarWChar));
                    catalogcom.Parameters["@middlename3_9"].Value = txtau3midnm.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@lastname3_10", OleDbType.VarWChar));
                    catalogcom.Parameters["@lastname3_10"].Value = txtau3surnm.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@UniFormTitle_11", OleDbType.VarWChar));
                    catalogcom.Parameters["@UniFormTitle_11"].Value = txtUniformTitle.Value;
                    catalogcom.Parameters.Add(new OleDbParameter("@TransNo", OleDbType.VarWChar));
                    catalogcom.Parameters["@TransNo"].Value = 0;

                    catalogcom.ExecuteNonQuery();


                    catalogcom.Parameters.Clear();

                    catalogcom.CommandType = CommandType.StoredProcedure;
                    catalogcom.CommandText = "insert_BookSeries_1";
                    catalogcom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));

                    catalogcom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                    // catalogcom.Parameters("@accessionnumber_1").Value = IIf(Trim(cboACC.SelectedValue) = String.Empty, String.Empty, Trim(cboACC.SelectedValue))

                    catalogcom.Parameters.Add(new OleDbParameter("@SeriesName_2", OleDbType.VarWChar));
                    catalogcom.Parameters["@SeriesName_2"].Value = txtseriesname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@seriesNo_3", OleDbType.VarWChar));
                    catalogcom.Parameters["@seriesNo_3"].Value = txtseriesno.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@seriesPart_4", OleDbType.VarWChar));
                    catalogcom.Parameters["@seriesPart_4"].Value = txtseriespart.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@etal_5", OleDbType.VarWChar));
                    catalogcom.Parameters["@etal_5"].Value = status.SelectedItem.Text;

                    catalogcom.Parameters.Add(new OleDbParameter("@Svolume_6", OleDbType.Integer));
                    catalogcom.Parameters["@Svolume_6"].Value = txtSVolume.Value.Trim() == "" ? "0" : txtSVolume.Value.Trim();

                    catalogcom.Parameters.Add(new OleDbParameter("@af1_7", OleDbType.VarWChar));
                    catalogcom.Parameters["@af1_7"].Value = af1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@am1_8", OleDbType.VarWChar));
                    catalogcom.Parameters["@am1_8"].Value = am1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@al1_9", OleDbType.VarWChar));
                    catalogcom.Parameters["@al1_9"].Value = al1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@af2_10", OleDbType.VarWChar));
                    catalogcom.Parameters["@af2_10"].Value = af2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@am2_11", OleDbType.VarWChar));
                    catalogcom.Parameters["@am2_11"].Value = am2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@al2_12", OleDbType.VarWChar));
                    catalogcom.Parameters["@al2_12"].Value = al2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@af3_13", OleDbType.VarWChar));
                    catalogcom.Parameters["@af3_13"].Value = af3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@am3_14", OleDbType.VarWChar));
                    catalogcom.Parameters["@am3_14"].Value = am3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@al3_15", OleDbType.VarWChar));
                    catalogcom.Parameters["@al3_15"].Value = al3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SSeriesName_16", OleDbType.VarWChar));
                    catalogcom.Parameters["@SSeriesName_16"].Value = txtSecondSeriesTitle.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SseriesNo_17", OleDbType.VarWChar));
                    catalogcom.Parameters["@SseriesNo_17"].Value = txtSecondSeriesNo.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SseriesPart_18", OleDbType.VarWChar));
                    catalogcom.Parameters["@SseriesPart_18"].Value = txtSecondSeriesPart.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Setal_19", OleDbType.VarWChar));
                    catalogcom.Parameters["@Setal_19"].Value = cmbSecetal.SelectedItem.Text;


                    catalogcom.Parameters.Add(new OleDbParameter("@SSvolume_20", OleDbType.Integer));
                    catalogcom.Parameters["@SSvolume_20"].Value = txtsecSeriesVol.Value == "" ? "0" : txtsecSeriesVol.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Saf1_21", OleDbType.VarWChar));
                    catalogcom.Parameters["@Saf1_21"].Value = txtSecFirstName1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Sam1_22", OleDbType.VarWChar));
                    catalogcom.Parameters["@Sam1_22"].Value = txtSecMidName1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Sal1_23", OleDbType.VarWChar));
                    catalogcom.Parameters["@Sal1_23"].Value = txtSecLastName1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Saf2_24", OleDbType.VarWChar));
                    catalogcom.Parameters["@Saf2_24"].Value = txtSecFirstName2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Sam2_25", OleDbType.VarWChar));
                    catalogcom.Parameters["@Sam2_25"].Value = txtSecMidName2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Sal2_26", OleDbType.VarWChar));
                    catalogcom.Parameters["@Sal2_26"].Value = txtSecLastName2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Saf3_27", OleDbType.VarWChar));
                    catalogcom.Parameters["@Saf3_27"].Value = txtSecFirstName3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Sam3_28", OleDbType.VarWChar));
                    catalogcom.Parameters["@Sam3_28"].Value = txtSecMidName3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Sal3_29", OleDbType.VarWChar));
                    catalogcom.Parameters["@Sal3_29"].Value = txtSecLastName3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SeriesParallelTitle_30", OleDbType.VarWChar));
                    catalogcom.Parameters["@SeriesParallelTitle_30"].Value = txtPTitle.Value;


                    catalogcom.Parameters.Add(new OleDbParameter("@SSeriesParallelTitle_31", OleDbType.VarWChar));
                    catalogcom.Parameters["@SSeriesParallelTitle_31"].Value = txtSecondParallelTitle.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SubSeriesName_32", OleDbType.VarWChar));
                    catalogcom.Parameters["@SubSeriesName_32"].Value = txtSubseriesname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SubseriesNo_33", OleDbType.VarWChar));
                    catalogcom.Parameters["@SubseriesNo_33"].Value = txtSubseriesno.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SubseriesPart_34", OleDbType.VarWChar));
                    catalogcom.Parameters["@SubseriesPart_34"].Value = txtSubseriespart.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subetal_35", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subetal_35"].Value = Substatus.SelectedItem.Text;

                    catalogcom.Parameters.Add(new OleDbParameter("@SubSvolume_36", OleDbType.Integer));
                    catalogcom.Parameters["@SubSvolume_36"].Value = txtSubSVolume.Value == "" ? "0" : txtSubSVolume.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subaf1_37", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subaf1_37"].Value = Subaf1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subam1_38", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subam1_38"].Value = Subam1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subal1_39", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subal1_39"].Value = Subal1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subaf2_40", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subaf2_40"].Value = Subaf2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subam2_41", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subam2_41"].Value = Subam2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subal2_42", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subal2_42"].Value = Subal2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subaf3_43", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subaf3_43"].Value = Subaf3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subam3_44", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subam3_44"].Value = Subam3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subal3_45", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subal3_45"].Value = Subal3.Value;



                    catalogcom.Parameters.Add(new OleDbParameter("@SubSeriesParallelTitle_46", OleDbType.VarWChar));
                    catalogcom.Parameters["@SubSeriesParallelTitle_46"].Value = txtSubPTitle.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@ISSNMain_47", OleDbType.VarWChar));
                    catalogcom.Parameters["@ISSNMain_47"].Value = txtMainissn.Value;


                    catalogcom.Parameters.Add(new OleDbParameter("@ISSNSub_48", OleDbType.VarWChar));
                    catalogcom.Parameters["@ISSNSub_48"].Value = txtSubissn.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@ISSNSecond_49", OleDbType.VarWChar));
                    catalogcom.Parameters["@ISSNSecond_49"].Value = txtSecondissn.Value;


                    catalogcom.ExecuteNonQuery();

                    catalogcom.Parameters.Clear();  // jai

                    catalogcom.CommandType = CommandType.StoredProcedure;

                    catalogcom.CommandText = "insert_BookRelators_1";
                    catalogcom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));

                    catalogcom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorFname1_2", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorFname1_2"].Value = editor1Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorMname1_3", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorMname1_3"].Value = editor1Mname.Value;


                    catalogcom.Parameters.Add(new OleDbParameter("@editorLname1_4", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorLname1_4"].Value = editor1Lname.Value;


                    catalogcom.Parameters.Add(new OleDbParameter("@editorFname2_5", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorFname2_5"].Value = editor2fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorMname2_6", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorMname2_6"].Value = editor2Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorLname2_7", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorLname2_7"].Value = editor2Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorFname3_8", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorFname3_8"].Value = editor3Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorMname3_9", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorMname3_9"].Value = editor3Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@editorLname3_10", OleDbType.VarWChar));
                    catalogcom.Parameters["@editorLname3_10"].Value = editor3Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@CompilerFname1_11", OleDbType.VarWChar));
                    catalogcom.Parameters["@CompilerFname1_11"].Value = compiler1Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Compilermname1_12", OleDbType.VarWChar));
                    catalogcom.Parameters["@Compilermname1_12"].Value = compiler1Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Compilerlname1_13", OleDbType.VarWChar));
                    catalogcom.Parameters["@Compilerlname1_13"].Value = compiler1Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@CompilerFname2_14", OleDbType.VarWChar));
                    catalogcom.Parameters["@CompilerFname2_14"].Value = compiler2Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Compilermname2_15", OleDbType.VarWChar));
                    catalogcom.Parameters["@Compilermname2_15"].Value = compiler2Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Compilerlname2_16", OleDbType.VarWChar));
                    catalogcom.Parameters["@Compilerlname2_16"].Value = compiler2Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@CompilerFname3_17", OleDbType.VarWChar));
                    catalogcom.Parameters["@CompilerFname3_17"].Value = compiler3Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Compilermname3_18", OleDbType.VarWChar));
                    catalogcom.Parameters["@Compilermname3_18"].Value = compiler3Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Compilerlname3_19", OleDbType.VarWChar));
                    catalogcom.Parameters["@Compilerlname3_19"].Value = compiler3Lname.Value;

                    // illustrator information entry

                    catalogcom.Parameters.Add(new OleDbParameter("@illusFname1_20", OleDbType.VarWChar));
                    catalogcom.Parameters["@illusFname1_20"].Value = Illustrator1Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illusmname1_21", OleDbType.VarWChar));
                    catalogcom.Parameters["@illusmname1_21"].Value = Illustrator1Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illuslname1_22", OleDbType.VarWChar));
                    catalogcom.Parameters["@illuslname1_22"].Value = Illustrator1Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illusFname2_23", OleDbType.VarWChar));
                    catalogcom.Parameters["@illusFname2_23"].Value = Illustrator2Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illusmname2_24", OleDbType.VarWChar));
                    catalogcom.Parameters["@illusmname2_24"].Value = Illustrator2Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illuslname2_25", OleDbType.VarWChar));
                    catalogcom.Parameters["@illuslname2_25"].Value = Illustrator2Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illusFname3_26", OleDbType.VarWChar));
                    catalogcom.Parameters["@illusFname3_26"].Value = Illustrator3Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illusmname3_27", OleDbType.VarWChar));
                    catalogcom.Parameters["@illusmname3_27"].Value = Illustrator3Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@illuslname3_28", OleDbType.VarWChar));
                    catalogcom.Parameters["@illuslname3_28"].Value = Illustrator3lname.Value;

                    // translator information 

                    catalogcom.Parameters.Add(new OleDbParameter("@TranslatorFname1_29", OleDbType.VarWChar));
                    catalogcom.Parameters["@TranslatorFname1_29"].Value = Translator1Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Translatormname1_30", OleDbType.VarWChar));
                    catalogcom.Parameters["@Translatormname1_30"].Value = Translator1Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Translatorlname1_31", OleDbType.VarWChar));
                    catalogcom.Parameters["@Translatorlname1_31"].Value = Translator1Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@TranslatorFname2_32", OleDbType.VarWChar));
                    catalogcom.Parameters["@TranslatorFname2_32"].Value = Translator2Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Translatormname2_33", OleDbType.VarWChar));
                    catalogcom.Parameters["@Translatormname2_33"].Value = Translator2Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Translatorlname2_34", OleDbType.VarWChar));
                    catalogcom.Parameters["@Translatorlname2_34"].Value = Translator2Lname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@TranslatorFname3_35", OleDbType.VarWChar));
                    catalogcom.Parameters["@TranslatorFname3_35"].Value = Translator3Fname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Translatormname3_36", OleDbType.VarWChar));
                    catalogcom.Parameters["@Translatormname3_36"].Value = Translator3Mname.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Translatorlname3_37", OleDbType.VarWChar));
                    catalogcom.Parameters["@Translatorlname3_37"].Value = Translator3Lname.Value;

                    catalogcom.ExecuteNonQuery();
                    // conference information entry

                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.StoredProcedure;
                    catalogcom.CommandText = "insert_BookConference_1";


                    catalogcom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
                    catalogcom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Subtitle_2", OleDbType.VarWChar));
                    catalogcom.Parameters["@Subtitle_2"].Value = txtSubtitle.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Paralleltype_3", OleDbType.VarWChar));
                    catalogcom.Parameters["@Paralleltype_3"].Value = txtParallelTitle.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@ConfName_4", OleDbType.VarWChar));
                    catalogcom.Parameters["@ConfName_4"].Value = txtConferenceName.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@ConfYear_5", OleDbType.VarWChar));
                    catalogcom.Parameters["@ConfYear_5"].Value = txtConferenceYear.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@BNNote_6", OleDbType.VarWChar));
                    catalogcom.Parameters["@BNNote_6"].Value = txtBN.Value;
                    catalogcom.Parameters.Add(new OleDbParameter("@CNNote_7", OleDbType.VarWChar));

                    catalogcom.Parameters["@CNNote_7"].Value = txtcn.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@GNNotes_8", OleDbType.VarWChar));
                    catalogcom.Parameters["@GNNotes_8"].Value = txtgn.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@VNNotes_9", OleDbType.VarWChar));
                    catalogcom.Parameters["@VNNotes_9"].Value = txtvn.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@SNNotes_10", OleDbType.VarWChar));
                    catalogcom.Parameters["@SNNotes_10"].Value = txtsn.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@ANNotes_11", OleDbType.VarWChar));
                    catalogcom.Parameters["@ANNotes_11"].Value = txtan.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Course_12", OleDbType.VarWChar));
                    catalogcom.Parameters["@Course_12"].Value = txtProgram.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdFname1_13", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdFname1_13"].Value = txtaname1.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdMname1_14", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdMname1_14"].Value = txtaname2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdLname1_15", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdLname1_15"].Value = txtaname3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdFname2_16", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdFname2_16"].Value = txtfname2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdMname2_17", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdMname2_17"].Value = txtmname2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdLname2_18", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdLname2_18"].Value = txtlname2.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdFname3_19", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdFname3_19"].Value = txtfname3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdMname3_20", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdMname3_20"].Value = txtmname3.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@AdLName3_21", OleDbType.VarWChar));
                    catalogcom.Parameters["@AdLName3_21"].Value = txtaname9.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Abstract_22", OleDbType.VarWChar));
                    catalogcom.Parameters["@Abstract_22"].Value = txtnarration.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@Program_name_23", OleDbType.VarWChar));
                    catalogcom.Parameters["@Program_name_23"].Value = txtProgram.Value;


                    catalogcom.Parameters.Add(new OleDbParameter("@TransNo", OleDbType.VarWChar));
                    catalogcom.Parameters["@TransNo"].Value = 0;

                    catalogcom.ExecuteNonQuery();
                    catalogcom.Parameters.Clear();


                    // =============================================
                    catalogcom.CommandType = CommandType.StoredProcedure;

                    catalogcom.CommandText = "insert_BookImage_1";
                    catalogcom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));

                    catalogcom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                    catalogcom.Parameters.Add(new OleDbParameter("@CoverPage_2", OleDbType.Binary));
                    catalogcom.Parameters["@CoverPage_2"].Value = imgbin;

                    catalogcom.ExecuteNonQuery();
                    catalogcom.Parameters.Clear();
                    // ============================================= Praveen 10mar'08
                    // catalogcom.CommandType = CommandType.StoredProcedure

                    // catalogcom.CommandText = "insert_ItemsKeyword_1"
                    // catalogcom.Parameters.Add(New OleDbParameter("@ctrl_no_2", OleDbType.BigInt))
                    // catalogcom.Parameters("@ctrl_no_2").Value = Trim(hdCtrlNo.Value)

                    // catalogcom.Parameters.Add(New OleDbParameter("@keyword_3", OleDbType.VarWChar))
                    // catalogcom.Parameters("@keyword_3").Value = String.Empty
                    // catalogcom.Parameters.Add(New OleDbParameter("@itemtype_4", OleDbType.VarWChar))
                    // catalogcom.Parameters("@itemtype_4").Value = Me.cmbtype.SelectedValue
                    // catalogcom.ExecuteNonQuery()

                    // *******************************************
                    catalogcom.Parameters.Clear();

                    // ========================================================
                    catalogcom.CommandType = CommandType.Text;
                    // obj.update_accmaster(hdCtrlNo.Value, txtCopyNo.Value, txteditionyear.Value, txtacc.Value, txtPubYear.Value, txtbookprice.Value, txtSpecialPrice.Value)
                    // #######################
                    string StatusIssue = string.Empty;
                    string isbardate = string.Empty;
                    if (cmbStatus.SelectedIndex != cmbStatus.Items.Count - 1)
                    {
                        catalogcom.Parameters.Clear();
                        catalogcom.CommandText = "select isIsued FROM ITEMSTATUSMASTER Where Itemstatusid=" + cmbStatus.SelectedItem.Value;
                        StatusIssue = catalogcom.ExecuteScalar().ToString();
                    }
                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    // By Kaushal--All Changes commit based on Ctrl _NO 
                    // -----------------------------------------------------
                    string billdate; // =" & IIf(Trim(txtDocDate.Text) = String.Empty, "NULL",  Trim(txtDocDate.Text))  & "
                    if (txtDocDate.Text == string.Empty)
                    {
                        billdate = "NULL";
                    }
                    else
                    {
                        billdate = "'" + txtDocDate.Text + "'";
                    }
                    if (txtlocation.Text.Trim() == "")
                    {
                        txtLoc2.Text = "0";
                    }
                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    if ((cmbStatus.SelectedIndex == cmbStatus.Items.Count - 1) || (Session["back"].ToString() == "catalog"))
                    {
                        catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + "  ctrl_no=";
                        catalogcom.CommandText += hdCtrlNo.Value + " ,booktitle=N'" + txttitle.Value.Replace("'", "''") + "',    vendor_source=N'" + txtCmbVendor.Text.Trim().Replace("'", "''");
                        catalogcom.CommandText += "', Copynumber=" + txtCopyNo.Value.Trim() + " , editionyear='" + txteditionyear.Value + "'";
                        catalogcom.CommandText += ",pubYear='" + txtPubYear.Value + "', specialprice=" + (txtSpecialPrice.Value == "" ? "0" : txtSpecialPrice.Value);
                        catalogcom.CommandText += ",OriginalCurrency=N'" + this.cmbcurr.SelectedItem.Text + "',OriginalPrice='" + this.txtForeignPrice.Value + "', bookprice=" + txtbookprice.Value + " , catalogdate='" + txtdate.Text + "', biilNo=N'" + txtDocNo.Value + "',billdate=" + billdate + ", IssueStatus=N'" + StatusIssue + "',Status=N'" + cmbStatus.SelectedValue + "',ItemCategory='" + cmbbookcategory.Items[cmbbookcategory.SelectedIndex].Text + "',ItemCategoryCode='" + cmbbookcategory.SelectedValue + "', loc_id=" + (txtLoc2.Text == "" ? "0" : txtLoc2.Text) + "  where accessionnumber='" + txtacc.Text + "'";
                    }
                    else if (Session["back"].ToString() == "catalog")
                    {
                        catalogcom.CommandText = "update bookaccessionmaster set  " + updTransNo + " ctrl_no=" + hdCtrlNo.Value + ", booktitle=N'";
                        catalogcom.CommandText += txttitle.Value.Replace("'", "''") + "',vendor_source=N'" + txtCmbVendor.Text.Trim().Replace("'", "''");
                        catalogcom.CommandText += "',Copynumber=" + txtCopyNo.Value + " , editionyear='" + txteditionyear.Value + "'";
                        catalogcom.CommandText += ",pubYear='" + txtPubYear.Value + "', specialprice=" + (txtSpecialPrice.Value == "" ? "0" : txtSpecialPrice.Value) + ", bookprice=" + txtbookprice.Value;
                        catalogcom.CommandText += " , OriginalCurrency=N'" + this.cmbcurr.SelectedItem.Text + "',OriginalPrice='" + this.txtForeignPrice.Value + "',catalogdate='" + txtdate.Text;
                        catalogcom.CommandText += "', LoadingDate='" + txtdate.Text + "',ReleaseDate='" + (txtrelease.Value == string.Empty ? txtdate.Text : txtrelease.Value) + "', IssueStatus=N'" + StatusIssue;
                        catalogcom.CommandText += "',Status=N'" + cmbStatus.SelectedValue + "', biilNo=N'" + txtDocNo.Value + "',billdate=" + billdate + ",ItemCategory='" + cmbbookcategory.SelectedItem.Text + "',ItemCategoryCode='" + cmbbookcategory.SelectedValue + "', loc_id=" + (txtLoc2.Text == "" ? "0" : txtLoc2.Text) + "  where accessionnumber='" + txtacc.Text + "'";
                    }
                    else
                    {
                        catalogcom.CommandText = "update bookaccessionmaster set  " + updTransNo + " ctrl_no=" + hdCtrlNo.Value;
                        catalogcom.CommandText += ", booktitle=N'" + txttitle.Value.Trim().Replace("'", "''") + "',vendor_source=N'" + txtCmbVendor.Text.Trim().Replace("'", "''");
                        catalogcom.CommandText += "', Copynumber=" + txtCopyNo.Value + " , editionyear=" + txteditionyear.Value;
                        catalogcom.CommandText += ",pubYear='" + txtPubYear.Value + "', specialprice=" + (txtSpecialPrice.Value == "" ? "0" : txtSpecialPrice.Value) + ",OriginalCurrency=N'" + this.cmbcurr.SelectedItem.Text;
                        catalogcom.CommandText += "',OriginalPrice='" + this.txtForeignPrice.Value + "', bookprice=" + txtbookprice.Value + " , catalogdate='" + txtdate.Text + "', biilNo=N'" + txtDocNo.Value + "', billdate=" + billdate;
                        catalogcom.CommandText += ", IssueStatus=N'" + StatusIssue + "',Status=N'" + cmbStatus.SelectedValue + "',ItemCategory='" + cmbbookcategory.SelectedItem.Text + "',ItemCategoryCode='" + cmbbookcategory.SelectedValue + "', loc_id=" + (txtLoc2.Text == "" ? "0" : txtLoc2.Text.Trim());
                        catalogcom.CommandText += "   where accessionnumber='" + txtacc.Text + "' ";

                    }
                    // 

                    catalogcom.ExecuteNonQuery();
                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    catalogcom.CommandText = "if exists (select * from sys.triggers where name='trig_update_bookaccessionmaster') begin  DISABLE TRIGGER trig_update_bookaccessionmaster ON bookaccessionmaster  end ";
                    catalogcom.ExecuteNonQuery();


                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + "  Deptcode=" + this.cmbdept.SelectedValue + " ,Deptname='" + this.cmbdept.SelectedItem.Text.Replace("'", "''") + "' where accessionnumber='" + txtacc.Text + "'";
                    catalogcom.ExecuteNonQuery();

                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + " Program_id = " + (cmbCourse1.SelectedItem.Text == HComboSelect.Value ? "0" : cmbCourse1.SelectedValue) + ", Item_Type = '" + (cmbtype.SelectedItem.Text == HComboSelect.Value ? "0" : cmbtype.SelectedValue) + "' where accessionnumber='" + txtacc.Text + "'";
                    catalogcom.ExecuteNonQuery();
                    catalogcom.Parameters.Clear();
                    if (!string.IsNullOrEmpty(tempstr))
                    {
                        catalogcom.CommandText = "update BookAccessionmaster set " + updTransNo + " booktitle='" + this.txttitle.Value.Replace("'", "''") + "' where Accessionnumber in(" + tempstr + ")";
                        catalogcom.ExecuteNonQuery();
                        catalogcom.Parameters.Clear();
                    }
                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    //                        catalogcom.CommandText = " ENABLE TRIGGER trig_update_bookaccessionmaster ON bookaccessionmaster  ";
                    //                      catalogcom.ExecuteNonQuery();

                    // store audit


                    /*
                    // audit 
                    var audaccn = new BookAccn();
                    var audbookcatg = new BookCatalog();
                    var audcatdats = new CatalogData();
                    var audbookauth = new BookAuth();
                    var audbookconf = new BookConf();
                    audaccn.BookNumber = txtbookno.Value;
                    audaccn.accessionnumber = txtacc.Value;
                    audaccn.booktitle = txttitle.Value;
                    audbookcatg.title = txttitle.Value;
                    audaccn.catalogdate = txtdate.Text;
                    audaccn.Copynumber = txtCopyNo.Value;
                    audaccn.Item_type = cmbtype.SelectedValue;
                    audaccn.Status = cmbStatus.SelectedValue;
                    audaccn.Item_type = cmbtype.SelectedValue;
                    if (!string.IsNullOrEmpty(txtrelease.Value))
                    {
                        audaccn.ReleaseDate = txtrelease.Value;
                    }
                    audaccn.IpAddress = Request.UserHostAddress;
                    audaccn.indentnumber = txtDocNo.Value;
                    if (string.IsNullOrEmpty(txtDocDate.Text))
                    {
                        audaccn.billDate = txtDocDate.Text;
                    }
                    audaccn.bookprice = txtbookprice.Value;
                    audaccn.userid = Session["user_id"];
                    audaccn.ItemCategoryCode = cmbbookcategory.SelectedValue;
                    audaccn.Status = cmbStatus.SelectedValue;
                    audbookcatg.language_id = cmbLanguage.SelectedValue;
                    audbookcatg.part = txtPart.Value;
                    audbookcatg.volumenumber = txtvolno.Value;
                    audaccn.DeptCode = cmbdept.SelectedValue;
                    if (!string.IsNullOrEmpty(txtpages.Value))
                    {
                        audbookcatg.pages = txtpages.Value;
                    }
                    audbookcatg.parts = txtparts.Value;
                    audaccn.Loc_id = txtLoc2.Text;
                    audaccn.booktitle = txttitle.Value;
                    audbookcatg.title = txttitle.Value;
                    audbookcatg.publishercode = hdPublisherId.Value;
                    audbookcatg.edition = txtedition.Value;
                    if (!string.IsNullOrEmpty(txteditionyear.Value))
                    {
                        audaccn.editionyear = txteditionyear.Value;
                    }

                    audbookcatg.isbn = txtisbn.Value;
                    audbookcatg.subject1 = txtSub11.Text;
                    audbookcatg.subject2 = txtsub2.Text;
                    audbookcatg.subject3 = txtsub3.Text;
                    audaccn.OriginalPrice = txtbookprice.Value;
                    audaccn.bookprice = txtbookprice.Value;
                    audaccn.biilNo = txtDocNo.Value;
                    if (!string.IsNullOrEmpty(txtDocDate.Text))
                    {
                        audaccn.billDate = txtDocDate.Text;
                    }
                    audbookcatg.Volume = txtVolumeNo.Value;
                    audaccn.OriginalCurrency = cmbcurr.SelectedValue;
                    audaccn.vendor_source = txtCmbVendor.Text;
                    if (!string.IsNullOrEmpty(txtPubYear.Value))
                    {
                        audaccn.pubYear = txtPubYear.Value;
                    }
                    audbookcatg.materialdesignation = cmbMediaType.SelectedValue;
                    audbookcatg.issn = txtIssnNo.Value;

                    audbookauth.firstname1 = txtau1firstnm.Value;
                    audbookauth.middlename1 = txtau1midnm.Value;
                    audbookauth.lastname1 = txtau1surnm.Value;

                    audbookauth.firstname1 = txtau2firstnm.Value;
                    audbookauth.middlename1 = txtau2midnm.Value;
                    audbookauth.lastname1 = txtau2surnm.Value;
                    audbookauth.firstname1 = txtau3firstnm.Value;
                    audbookauth.middlename1 = txtau3midnm.Value;
                    audbookauth.lastname1 = txtau3surnm.Value;
                    audbookconf.Subtitle = txtSubtitle.Value;
                    audcatdats.classnumber = txtclassno.Value;
                    audcatdats.booknumber = txtbookno.Value;
                    var lsaccnaft = new List<BookAccn>();
                    lsaccnaft.Add(audaccn);
                    var lsaccnb4 = new List<BookAccn>();
                    var lsbkcatb4 = new List<BookCatalog>();
                    var lscatdatab4 = new List<CatalogData>();
                    var lsauthb4 = new List<BookAuth>();
                    // Dim audcl As Audit.UpdCatalog = Session("auditdata")
                    // lsaccnb4 = audcl.lsAccnb4


                    // lsbkcatb4 = audcl.lsCatloagb4
                    // lscatdatab4 = audcl.lscatagdatab4
                    // lsauthb4 = audcl.lsauthb4
                    var lsbookcatg = new List<BookCatalog>();
                    lsbookcatg.Add(audbookcatg);
                    var lscatdats = new List<CatalogData>();
                    lscatdats.Add(audcatdats);
                    var lsbookauth = new List<BookAuth>();
                    lsbookauth.Add(audbookauth);
                    var lsbookconf = new List<BookConf>();
                    lsbookconf.Add(audbookconf);
                    string audStat = "";
                    if (!string.IsNullOrEmpty(audStat))
                    {

                        messg.PageMesg("Before grid Data Saved, audit failed:" + audStat, this, DBUTIL.dbUtilities.MsgLevel.Warning);

                    }
                    */
                    int Icounter;
                    bool @bool = false;
                    var loopTo4 = grdcopy.Items.Count - 1;
                    /*
                    for (Icounter = 0; Icounter <= loopTo4; Icounter++)
                    {
                        catalogcom.Parameters.Clear();
                        catalogcom.CommandType = CommandType.Text;
                        var grdAccn = new BookAccn();
                        var grdAccnb4 = new BookAccn();
                        var txtC = new HtmlInputText();
                        string accNo = grdcopy.Items(Icounter).Cells(1).Text;
                        grdAccn.accessionnumber = accNo;
                        grdAccnb4.accessionnumber = accNo;
                        txtC = (HtmlInputText)grdcopy.Items(Icounter).Cells(1).FindControl("txtCopyInfo");
                        grdAccn.Copynumber = Convert.ToInt32(txtC.Value);
                        grdAccnb4.Copynumber = ((HiddenField)grdcopy.Items(Icounter).FindControl("hdcopyno")).Value;
                        var txtDocNo1 = new HtmlInputText();  // 
                        txtDocNo1 = (HtmlInputText)grdcopy.Items(Icounter).Cells(2).FindControl("txtDocNoC");
                        grdAccn.biilNo = txtDocNo1.Value;
                        grdAccnb4.biilNo = ((HiddenField)grdcopy.Items(Icounter).FindControl("hddocno")).Value;
                        var txtDocDate1 = new HtmlInputText();
                        txtDocDate1 = (HtmlInputText)grdcopy.Items(Icounter).Cells(3).FindControl("txtDocDateC");
                        if (Trim(txtDocDate1.Value) == string.Empty)
                        {
                            billdate = "NULL";
                        }
                        else
                        {
                            billdate = "'" + Trim(txtDocDate1.Value) + "'";
                            grdAccn.billDate = txtDocDate1.Value.Trim;
                        } // hddocdate
                        string s = ((HiddenField)grdcopy.Items(Icounter).FindControl("hddocdate")).Value;
                        if (!string.IsNullOrEmpty(s))
                        {
                            grdAccnb4.billDate = s;
                        }
                        var txtEYear = new HtmlInputText();
                        txtEYear = (HtmlInputText)grdcopy.Items(Icounter).Cells(4).FindControl("txtEdYear");
                        if (!string.IsNullOrEmpty(txtEYear.Value))
                        {
                            grdAccn.editionyear = txtEYear.Value;

                        }
                        string sedyr = ((HiddenField)grdcopy.Items(Icounter).FindControl("hdyear")).Value;
                        if (!string.IsNullOrEmpty(sedyr))
                        {
                            grdAccnb4.editionyear = Convert.ToInt32(sedyr);
                        }
                        var txtPYear = new HtmlInputText();
                        txtPYear = (HtmlInputText)grdcopy.Items(Icounter).Cells(5).FindControl("txtPubYear");
                        if (!string.IsNullOrEmpty(txtPYear.Value))
                        {
                            grdAccn.pubYear = txtPYear.Value;
                        }
                        string shdpubyear = ((HiddenField)grdcopy.Items(Icounter).FindControl("hdpubyear")).Value;
                        if (!string.IsNullOrEmpty(shdpubyear))
                        {
                            grdAccnb4.pubYear = Convert.ToInt32(shdpubyear);
                        }
                        var cmboricurr = new DropDownList();
                        cmboricurr = (DropDownList)grdcopy.Items(Icounter).Cells(6).FindControl("cmbOriCurrency");

                        var txtoricurr = new HtmlInputText();
                        txtoricurr = (HtmlInputText)grdcopy.Items(Icounter).Cells(7).FindControl("txtOriPriceC");

                        var txtPrice = new HtmlInputText();
                        txtPrice = (HtmlInputText)grdcopy.Items(Icounter).Cells(8).FindControl("txtPrice");
                        grdAccn.bookprice = txtPrice.Value;
                        string price = ((HiddenField)grdcopy.Items(Icounter).FindControl("hdorigprice")).Value;
                        if (!string.IsNullOrWhiteSpace(price))
                        {
                            grdAccnb4.bookprice = Convert.ToDecimal(price);
                        }
                        var txtSPrice = new HtmlInputText();
                        txtSPrice = (HtmlInputText)grdcopy.Items(Icounter).Cells(9).FindControl("txtsplPrice");
                        var txtvendor = new System.Web.UI.WebControls.TextBox();
                        txtvendor.Text = ((System.Web.UI.WebControls.TextBox)grdcopy.Items(Icounter).Cells(10).FindControl("txtCmbVendor")).Text;
                        string vends = ((HiddenField)grdcopy.Items(Icounter).FindControl("hdcatsource")).Value;
                        if (!string.IsNullOrWhiteSpace(vends))
                        {
                            grdAccnb4.vendor_source = vends;
                        }
                        grdAccn.vendor_source = txtvendor.Text;
                        try
                        {
                            grdAccn.catalogdate = Convert.ToDateTime((DateTime)grdcopy.Items(Icounter).Cells(9).Text);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            grdAccnb4.catalogdate = Convert.ToDateTime(((HiddenField)grdcopy.Items(Icounter).FindControl("hdcatalogdate")).Value);
                        }
                        catch (Exception ex)
                        {

                        }
                        int LocId;
                        // LocId = CType(grdcopy.Items(Icounter).FindControl("hdnCpLocId"), TextBox).Text
                        string txtCpLoc = ((TextBox)grdcopy.Items(Icounter).FindControl("txtCpLoc")).Text;   // txtCpLoc
                        if (string.IsNullOrEmpty(txtCpLoc.Trim()))
                        {
                            LocId = 0;
                        }
                        else
                        {
                            string[] sLoc = ((TextBox)grdcopy.Items(Icounter).FindControl("txtCpLoc")).Text.Split(",");
                            try
                            {
                                LocId = Conversions.ToInteger(sLoc[1]);
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException("Table does Not have valid Location");
                            }
                        }
                        grdAccnb4.Loc_id = 0;
                        string slocid = ((HiddenField)grdcopy.Items(Icounter).FindControl("hdloc_id")).Value;
                        if (!string.IsNullOrEmpty(slocid))
                        {
                            grdAccnb4.Loc_id = Convert.ToInt32(slocid);
                        }
                        grdAccn.Loc_id = LocId;
                        var cmb1 = new DropDownList();
                        cmb1 = (DropDownList)grdcopy.Items(Icounter).Cells(12).FindControl("cmbCourse");

                        var cmbstate = new DropDownList();
                        cmbstate = (DropDownList)grdcopy.Items(Icounter).Cells(12).FindControl("cmbStatus");
                        grdAccn.Status = cmbstate.SelectedValue;
                        HiddenField hdstat = (HiddenField)grdcopy.Items(Icounter).FindControl("hdcmbStatus"); // 
                        grdAccnb4.Status = hdstat.Value;
                        var cmbGrdDeptN = new DropDownList();
                        cmbGrdDeptN = (DropDownList)grdcopy.Items(Icounter).Cells(13).FindControl("CmbGrdDept");
                        grdAccn.DeptCode = cmbdept.SelectedValue;
                        HiddenField hddept = (HiddenField)grdcopy.Items(Icounter).FindControl("hdCmbGrdDept");
                        grdAccnb4.DeptCode = hddept.Value;
                        var CmbItemType = new DropDownList();
                        CmbItemType = (DropDownList)grdcopy.Items(Icounter).Cells(15).FindControl("CmbItemType");
                        grdAccn.Item_type = CmbItemType.SelectedValue;
                        HiddenField hditmtype = (HiddenField)grdcopy.Items(Icounter).FindControl("hdCmbItemType");
                        grdAccnb4.Item_type = hditmtype.Value;
                        var CatItemType = new DropDownList();
                        CatItemType = (DropDownList)grdcopy.Items(Icounter).Cells(16).FindControl("CatItemType");
                        // -------------------------------------------------------------------

                        var conobj = new OleDbConnection(retConstr(Conversions.ToString(Session["LibWiseDBConn"])));
                        conobj.Open();
                        var cmdobj = new OleDbCommand("select Id from CategoryLoadingStatus where Category_LoadingStatus='" + CatItemType.SelectedItem.Text + "'", conobj);
                        OleDbDataReader drdobj;
                        drdobj = cmdobj.ExecuteReader();
                        string ItemCategoryCodeVal = null;
                        while (drdobj.Read())
                        {
                            ItemCategoryCodeVal = drdobj["Id"].ToString();
                            grdAccn.ItemCategoryCode = CatItemType.SelectedValue;
                        }
                        HiddenField hdcatid = (HiddenField)grdcopy.Items(Icounter).FindControl("hdCatItemType");
                        grdAccnb4.ItemCategoryCode = hdcatid.Value;

                        // -------------------------------------------------------------------
                        if (cmbStatus.SelectedIndex == cmbStatus.Items.Count - 1 & Operators.ConditionalCompareObjectEqual(Session["back"], "catalog", false))
                        {
                            catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + " ctrl_no=" + hdCtrlNo.Value + ", Copynumber=" + Val(txtC.Value) + " , editionyear=" + Val(txtEYear.Value) + ", pubyear='" + txtPYear.Value + "' , specialprice=" + Val(txtSPrice.Value) + ",OriginalCurrency=N'" + cmboricurr.SelectedItem.Text + "',OriginalPrice='" + txtoricurr.Value + "', bookprice=" + Val(txtPrice.Value) + " ,vendor_source=N'" + Trim(txtvendor.Text) + "', catalogdate='" + grdcopy.Items(Icounter).Cells(11).Text + "' , biilNo=N'" + Trim(txtDocNo1.Value) + "', billdate=" + billdate + ", Program_id = " + IIf(cmb1.SelectedItem.Text == HComboSelect.Value, 0, cmb1.SelectedValue) + ",Status = " + IIf(cmbstate.SelectedItem.Text == HComboSelect.Value, "'NA'", cmbstate.SelectedValue) + ",DeptCode=" + cmbGrdDeptN.SelectedValue + ", DeptName='" + cmbGrdDeptN.SelectedItem.Text + "', Item_Type='" + IIf(CmbItemType.SelectedItem.Text == Resources.ValidationResources.ComboSelect, 0, CmbItemType.SelectedValue) + "', ItemCategory='" + CatItemType.SelectedItem.Text + "',ItemCategoryCode='" + ItemCategoryCodeVal + "',loc_id=" + LocId + " where accessionnumber =N'" + Trim(grdcopy.Items(Icounter).Cells(1).Text) + "'";
                        }
                        else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Session["back"], "catalog", false)))
                        {
                            if (@bool == false)
                            {
    //                            catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + " ctrl_no=" + hdCtrlNo.Value + ", Copynumber=" + Val(txtC.Value) + " , editionyear=" + Val(txtEYear.Value) + ", pubyear='" + txtPYear.Value + "' , specialprice=" + Val(txtSPrice.Value) + ",vendor_source=N'" + Trim(txtvendor.Text) + "', bookprice=" + Val(txtPrice.Value) + " ,OriginalCurrency=N'" + cmboricurr.SelectedItem.Text + "',OriginalPrice='" + txtoricurr.Value + "' , catalogdate='" + grdcopy.Items(Icounter).Cells(11).Text + "', LoadingDate='" + txtdate.Text + "',IssueStatus=N'" + StatusIssue + "',Status=N'" + cmbStatus.SelectedValue + "' ,ReleaseDate='" + IIf(Trim(this.txtrelease.Value) == string.Empty, txtdate.Text, txtrelease.Value) + "', biilNo=N'" + Trim(txtDocNo1.Value) + "', billdate=" + billdate + ", Program_id = " + IIf(cmb1.SelectedItem.Text == HComboSelect.Value, 0, cmb1.SelectedValue) + ", Item_Type='" + IIf(CmbItemType.SelectedItem.Text == Resources.ValidationResources.ComboSelect, 0, CmbItemType.SelectedValue) + "',ItemCategory='" + CatItemType.SelectedItem.Text + "',ItemCategoryCode='" + ItemCategoryCodeVal + "',loc_id=" + LocId + " where accessionnumber =N'" + Trim(grdcopy.Items(Icounter).Cells(1).Text) + "'";
                                @bool = true;
                            }
                            else
                            {
  //                              catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + " ctrl_no=" + hdCtrlNo.Value + ", Copynumber=" + Val(txtC.Value) + " , editionyear=" + Val(txtEYear.Value) + ", pubyear='" + txtPYear.Value + "' , specialprice=" + Val(txtSPrice.Value) + ",vendor_source=N'" + Trim(txtvendor.Text) + "', bookprice=" + Val(txtPrice.Value) + " ,OriginalCurrency=N'" + cmboricurr.SelectedItem.Text + "',OriginalPrice='" + txtoricurr.Value + "' , catalogdate='" + grdcopy.Items(Icounter).Cells(11).Text + "', LoadingDate='" + txtdate.Text + "',IssueStatus=N'" + StatusIssue + "',ReleaseDate='" + IIf(Trim(this.txtrelease.Value) == string.Empty, txtdate.Text, txtrelease.Value) + "', biilNo=N'" + Trim(txtDocNo1.Value) + "', billdate=" + billdate + ", Program_id = " + IIf(cmb1.SelectedItem.Text == HComboSelect.Value, 0, cmb1.SelectedValue) + " ,Status = " + IIf(cmbstate.SelectedItem.Text == HComboSelect.Value, "'NA'", cmbstate.SelectedValue) + ",DeptCode=" + cmbGrdDeptN.SelectedValue + " ,DeptName='" + cmbGrdDeptN.SelectedItem.Text + "', Item_Type='" + IIf(CmbItemType.SelectedItem.Text == Resources.ValidationResources.ComboSelect, 0, CmbItemType.SelectedValue) + "', ItemCategory='" + CatItemType.SelectedItem.Text + "',ItemCategoryCode='" + ItemCategoryCodeVal + "',loc_id=" + LocId + " where accessionnumber =N'" + Trim(grdcopy.Items(Icounter).Cells(1).Text) + "'";
                            }
                        }
                        else
                        {
//                                catalogcom.CommandText = "update bookaccessionmaster set " + updTransNo + " ctrl_no=" + hdCtrlNo.Value + ", Copynumber=" + Val(txtC.Value) + " , editionyear=" + Val(txtEYear.Value) + ", pubyear='" + txtPYear.Value + "' , specialprice=" + Val(txtSPrice.Value) + ",OriginalCurrency=N'" + cmboricurr.SelectedItem.Text + "',OriginalPrice='" + txtoricurr.Value + "', bookprice=" + Val(txtPrice.Value) + " ,vendor_source=N'" + Trim(txtvendor.Text) + "', catalogdate='" + grdcopy.Items(Icounter).Cells(11).Text + "' , biilNo=N'" + Trim(txtDocNo1.Value) + "', billdate=" + billdate + ", Program_id = " + IIf(cmb1.SelectedItem.Text == HComboSelect.Value, 0, cmb1.SelectedValue) + ",Status = " + IIf(cmbstate.SelectedItem.Text == HComboSelect.Value, "'NA'", cmbstate.SelectedValue) + " ,DeptCode=" + cmbGrdDeptN.SelectedValue + " , DeptName='" + cmbGrdDeptN.SelectedItem.Text + "', Item_Type='" + IIf(CmbItemType.SelectedItem.Text == Resources.ValidationResources.ComboSelect, 0, CmbItemType.SelectedValue) + "', ItemCategory='" + CatItemType.SelectedItem.Text + "',ItemCategoryCode='" + ItemCategoryCodeVal + "',loc_id=" + LocId + " where accessionnumber =N'" + Trim(grdcopy.Items(Icounter).Cells(1).Text) + "'";
                        }
                        catalogcom.ExecuteNonQuery();
                        catalogcom.Parameters.Clear();
                        try
                        {
//                              lsaccnaft.Add(grdAccn);
//                            lsaccnb4.Add(grdAccnb4);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    */
                    catalogcom.Parameters.Clear();
                    catalogcom.CommandType = CommandType.Text;
                    if (isNewCtrl == true)
                    {
                        catalogcom.CommandText = "update idtable set currentposition=" + hdCtrlNo.Value + " where objectname=N'catalog'";
                        catalogcom.ExecuteNonQuery();
                        isNewCtrl = false;
                    }
                    // **************
                    /*
                    try
                    {
                        var audaccnbefore = new BookAccn();
                        var audclas = new Audit.UpdCatalog();
                        audclas = Session["auditdata"];
                        // lsaccnb4.Insert(0, audclas.lsAccnb4(0))
                        // lsaccnaft.Insert(0, audaccn)
                        audclas.lsAccnaft = lsaccnaft;
                        audclas.lsAccnb4 = lsaccnb4;
                        audclas.lscatagdatab4 = lscatdatab4;
                        audclas.lsCatloagb4 = lsbkcatb4;
                        audclas.lsauthb4 = lsauthb4;
                        audclas.lsCatloagaft = lsbookcatg;
                        audclas.lscatagdataaft = lscatdats;
                        audclas.lsbookconfaft = lsbookconf;
                        audclas.lsauthaft = lsbookauth;
                        var audt = new AccnAudit();
                        audStat = audt.UpdateAudit(audclas, "CatalogDetail", Request.UserHostAddress, transno, Session["user_id"]);
                    }
                    catch (Exception ex)
                    {

                    }
                    */

                    tran.Commit();
                    catalogcom.Dispose();
                    cls.TrOpen();
                    // qer = " enable  TRIGGER trig_update_bookaccessionmaster ON bookaccessionmaster     "
                    // cls.IUD(qer)
                    // qer = " enable   TRIGGER trig_update_bookauthor ON bookauthor "
                    // cls.IUD(qer)
                    // qer = "  enable   TRIGGER trig_update_bookcatalog ON bookcatalog  "
                    // cls.IUD(qer)
                    // qer = " enable   TRIGGER trig_update_bookconference ON bookconference "
                    // cls.IUD(qer)
                    // qer = "enable   TRIGGER trig_update_catalogdata ON catalogdata   "
                    // cls.IUD(qer)
                    // qer += "  "
                    // qer += "  "

                    cls.TrClose();



                    var clasqr = new GlobClass();
                    string qery = "select count(*) from FeaturesPer where fid=19 ";  // record marc 21
                    int marcount;
                    marcount = Convert.ToInt32(clasqr.ExScaler(qery));
                    string resP = "";
                    // save marc data
                    /*
                    if (marcount > 0)
                    {
                        var marc = new MarcAddMod();
                        try
                        {
                            marc.MarcSave(txtacc.Value);
                            resP = resP + "; Marc 21 data also saved.";
                        }

                        catch (Exception ex)
                        {

                            resP = resP + ("; Marc 21 data Not saved:" + ex.Message);

                        }
                    }
                    */
                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        msglabel.Visible = true;
                        msglabel.Text = ex.Message;
                        if (Session["back"].ToString() == "catalog")
                        {
                            hdCtrlNo.Value = string.Empty;
                        }
                        message.PageMesg(Resources.ValidationResources.UToPcsCtl + ";" + ex.Message, this, dbUtilities.MsgLevel.Failure);
                    }
                    catch (Exception ex1)
                    {
                        msglabel.Visible = true;
                        msglabel.Text = ex1.Message;
                        message.PageMesg(Resources.ValidationResources.UToPcsCtl, this, dbUtilities.MsgLevel.Failure);
                        if (Session["back"].ToString() == "catalog")
                        {
                            hdCtrlNo.Value = string.Empty;
                        }
                        return;
                    }
                    msglabel.Visible = true;
                    msglabel.Text = ex.Message;
                    return;
                }
            }
            else
            {
                // hdForMesage.Value = "SR"
                message.PageMesg(Resources.ValidationResources.SOfRNotSp, this, dbUtilities.MsgLevel.Failure);
                if (Session["back"].ToString() == "catalog")
                {
                    hdCtrlNo.Value = string.Empty;
                }
                // bookcatalogcon.Close()
                // bookcatalogcon.Dispose()
                // catalogcom.Dispose()
                return;
            }

            var logged = LoggedUser.Logged();
            if (logged.IsAudit == "Y")
            {
                // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
                if (hdnInsUpd.Value == "")
                {
                    LibObj1.insertLoginFunc(logged.UserName, lblTitleCatalogue.Text, logged.Session, txtacc.Text, Resources.ValidationResources.Insert, retConstr(""));
                }
                else if (hdnInsUpd.Value.ToLower() == "u")
                {

                    LibObj1.insertLoginFunc(logged.UserName, lblTitleCatalogue.Text, logged.Session, txtacc.Text, Resources.ValidationResources.bUpdate, retConstr(""));
                }
            }

            // tran.Commit()
            // catalogcom.Dispose()


            //                catentry(bookcatalogcon);



            //                aa1 = string.Empty;
            //              aa2 = null;
            // To insert the marc Record
            // Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            // If myConfiguration.AppSettings.Settings.Item("IsMarc21").Value = "Y" Then
            // Call LibObj.PrepareMARC(Val(Trim(hdCtrlNo.Value)), bookcatalogcon)
            // End If
        }

        this.HWhichFill.Value = "";
        this.hdPublisherId.Value = "";
        // Catch ex As Exception
        // Try
        // tran.Rollback()
        // msglabel.Visible = True
        // msglabel.Text = ex.Message
        // If Session("back") = "catalog" Then
        // hdCtrlNo.Value = String.Empty
        // End If
        // LibObj.MsgBox1(Resources.ValidationResources.UToPcsCtl.ToString, Me)
        // Catch ex1 As Exception
        // msglabel.Visible = True
        // msglabel.Text = ex1.Message
        // LibObj.MsgBox1(Resources.ValidationResources.UToPcsCtl.ToString, Me)
        // If Session("back") = "catalog" Then
        // hdCtrlNo.Value = String.Empty
        // End If
        // Exit Sub
        // End Try
        // msglabel.Visible = True
        // msglabel.Text = ex.Message
        // Exit Sub
        // End Try
        /*
        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Session["back"], "catalog", false)))
        {
            hdacc.Value = txtacc.Value;
            Session["hdctr"] = Trim(hdCtrlNo.Value);
            // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "openwindow", "confirm1();", True)
            Response.Redirect("Catalogback.aspx?url=catalog" + "&title=" + Request.QueryString["title"]);
        }
        else
        {
            hdacc.Value = txtacc.Value;

            Session["hdctr"] = Trim(hdCtrlNo.Value);
            Response.Redirect("Catalogback.aspx?url=a" + "&title=" + Request.QueryString["title"]);
        }
        */
        Session["accno"] = string.Empty;
        txtacc.Text = string.Empty;
        Text1.Value = string.Empty;
        message.PageMesg("Catalog Saved successfully ", this);
    }

    protected void cmdSave7_2_Click(object sender, EventArgs e)
    {
        cmdsave_ServerClick(sender, e);
    }

    protected void cmdreset7_2_Click(object sender, EventArgs e)
    {
        clearfields();

    }

    protected void cmdBack7_2_Click(object sender, EventArgs e)
    {
        Response.Redirect("CataLogingStartScreen.aspx");
    }

    protected void cmdResetT_2_Click(object sender, EventArgs e)
    {
        clearfields();

    }

    protected void cmdReturnt_2_Click(object sender, EventArgs e)
    {
        cmdBack7_2_Click(sender, e);
    }

    protected void cmdReturn1_2_Click(object sender, EventArgs e)
    {
        cmdBack7_2_Click(sender, e);
    }

    protected void cmdReset4_2_Click(object sender, EventArgs e)
    {
        clearfields();
    }

    protected void cmdReturn4_2_Click(object sender, EventArgs e)
    {
        cmdBack7_2_Click(sender, e);
    }

    protected void cmdReset3_2_Click(object sender, EventArgs e)
    {
        clearfields();
    }

    protected void cmdReturn3_2_Click(object sender, EventArgs e)
    {
        cmdBack7_2_Click(sender, e);
    }

        protected void btnBackScateg_Click(object sender, EventArgs e)
        {
            Response.Redirect("Searchcatalogdetail_1.aspx");
        }
    }
}