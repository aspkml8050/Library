using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class CataLogingStartScreen : BaseClass
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            // --------------------------------------
            Session["SearchKey"] = "AccessionNumber"; // "CDE"
            Session["Filtersuggest"] = "Acc";
            // ------------------------------------
            if (Session["DDC"]!=null)
            {
                txtClassNumber.Text = Session["DDC"].ToString();
            }
            msglabel.Visible = false;
            if (!Page.IsPostBack)
            {
                txtClassNumber.Text = "";
                txtBookNumber.Attributes.Add("onkeydown", "txtBookNumber_onkeydown();");
                cmdreset.CausesValidation = false;
                var con = new OleDbConnection(retConstr(""));
                try
                {
                    con.Open();
                    txtCtrlNo.Value = string.Empty;
                    var lstItem = new System.Web.UI.WebControls.ListItem();
                    var DeptBassedCat = new DataSet();
                    DeptBassedCat = LibObj.PopulateDataset("select Dept_B_Cat From librarysetupinformation ", "librarysetup", con);
                    var ComboDs = new DataSet();
                    ComboDs = LibObj.PopulateDataset("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "dept", con);
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
                    cmbdept.Items.Add(this.HComboSelect.Value);
                    cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
                    if (DeptBassedCat.Tables["librarysetup"].Rows[0]["Dept_B_Cat"].ToString()== "Y")
                    {
                        cmbdept.Visible = true;
                        Label13.Visible = true;
                    }
                    else
                    {
                        cmbdept.Visible = false;
                        Label13.Visible = false;
                    }
                    // btnCategoryFilter.CausesValidation = False
                   // Session["lblt3"] = Request.QueryString["title"];
                 //   lblt1.Text = Session["lblt3"];

                    Session["tmpcondition"] = Request.QueryString["condition"];

                    // Me.SetFocus(txtcmbJTitle)
                    var dt = new DataTable();

                    grdcopy.DataSource = null;
                    grdcopy.DataBind();
                    grdImported.DataSource = null ;
                    grdImported.DataBind();
                    hdnGrdId.Value = grdcopy.ClientID;

                    dt.Dispose();
                }
                catch (Exception ex)
                {
                    // msglabel.Visible = True
                    // msglabel.Text = ex.Message
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Dispose();
                }
            }
            Session["accno"] = string.Empty;
        }
        protected void BtnNextGrid_ServerClick(object sender, System.EventArgs e)
        {
            /*
            int Counter, GridValue;
            if (txtCtrlNo.Value == string.Empty)
            {
                var cn = new OleDbConnection(retConstr(Conversions.ToString(Session["LibWiseDBConn"])));
                cn.Open();
                var chkds = new DataSet();
                var CheckDt = new DataTable();
                chkds = LibObj.PopulateDataset("SELECT ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname from  dbo.CatalogueCardView where  classnumber=N'" + Trim(txtClassNumber.Text).Replace("'", "''") + "' AND booknumber=N'" + Trim(txtBookNumber.Value).Replace("'", "''") + "' order by copyNumber", "dup", cn);
                cn.Close();
                cn.Dispose();

                CheckDt.Columns.Add(new DataColumn("ctrl_no"));
                CheckDt.Columns.Add(new DataColumn("accessionnumber"));
                CheckDt.Columns.Add(new DataColumn("volume"));
                CheckDt.Columns.Add(new DataColumn("title"));
                CheckDt.Columns.Add(new DataColumn("Author"));
                CheckDt.Columns.Add(new DataColumn("edition"));
                // CheckDt.Columns.Add(New DataColumn("editionyear"))
                // CheckDt.Columns.Add(New DataColumn("copyNumber"))
                CheckDt.Columns.Add(new DataColumn("Part"));
                CheckDt.Columns.Add(new DataColumn("Language_name"));
                CheckDt.Columns.Add(new DataColumn("departmentname"));
                CheckDt.Columns.Add(new DataColumn("classnumber"));
                CheckDt.Columns.Add(new DataColumn("booknumber"));

                DataRow DR;
                // DR = CheckDt.NewRow

                if (chkds.Tables["dup"].Rows.Count > 19)
                {

                    GridValue = Val(this.Hdcounter.Value) + 19;
                    if (chkds.Tables["dup"].Rows.Count > GridValue)
                    {
                        var loopTo = GridValue;
                        for (Counter = Val(this.Hdcounter.Value); Counter <= loopTo; Counter++)
                        {
                            // hdForMesage.Value = "X"
                            DR = CheckDt.NewRow();
                            DR["ctrl_no"] = IIf(Operators.ConditionalCompareObjectEqual(chkds.Tables["dup"].Rows[Counter]["ctrl_no"], 0, false), 0, chkds.Tables["dup"].Rows[Counter]["ctrl_no"]);
                            DR["accessionnumber"] = chkds.Tables["dup"].Rows[Counter]["accessionnumber"];
                            DR["volume"] = chkds.Tables["dup"].Rows[Counter]["volume"];
                            DR["title"] = chkds.Tables["dup"].Rows[Counter]["title"];
                            DR["Author"] = chkds.Tables["dup"].Rows[Counter]["Author"];
                            DR["edition"] = chkds.Tables["dup"].Rows[Counter]["edition"];
                            // DR("editionyear") = chkds.Tables("dup").Rows(Counter).Item("editionyear")
                            // DR("copyNumber") = chkds.Tables("dup").Rows(Counter).Item("copyNumber")
                            DR["Part"] = chkds.Tables["dup"].Rows[Counter]["Part"];
                            DR["Language_name"] = chkds.Tables["dup"].Rows[Counter]["Language_name"];
                            DR["departmentname"] = chkds.Tables["dup"].Rows[Counter]["departmentname"];
                            DR["classnumber"] = chkds.Tables["dup"].Rows[Counter]["classnumber"];
                            DR["booknumber"] = chkds.Tables["dup"].Rows[Counter]["booknumber"];

                            CheckDt.Rows.Add(DR);
                        }
                        grdcopy.DataSource = CheckDt;
                        grdcopy.DataBind();
                        hdnGrdId.Value = grdcopy.ClientID;

                        this.Hdcounter.Value = Counter;
                        chkds.Dispose();
                        CheckDt.Dispose();
                        // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "ConfirmbeforeSave();", True)
                        return;
                    }
                }

                else
                {
                    grdcopy.DataSource = chkds;
                    grdcopy.DataBind();
                    hdnGrdId.Value = grdcopy.ClientID;

                }
            }
            Session["accno"] = Trim(txtcmbJTitle.Text);
            Session["BNumber"] = Trim(txtBookNumber.Value.ToString);
            Session["CNumber"] = Trim(txtClassNumber.Text.ToString);
            Session["back"] = "catalog";
            */
        }

        protected void cmdsearch_Click(object sender, EventArgs e)
        {
            OleDbConnection CatalogCon = null;
            try
            {
                // txtcmbJTitle.Text =txtcmbJTitle.Text
                CatalogCon = new OleDbConnection(retConstr(""));
                txtClassNumber.Enabled = true;
                txtBookNumber.Disabled = false;
                chkDifferent.Visible = false;
                txtClassNumber.Text = string.Empty;
                txtBookNumber.Value = string.Empty;
                txtEdition.Value = string.Empty;
                txtLanguage.Value = string.Empty;
                txtPart.Value = string.Empty;
                hdItemType.Value = string.Empty;
                txtYear.Value = string.Empty;
                txtCtrlNo.Value = string.Empty;
                var dt = new DataTable();
                grdcopy.CurrentPageIndex = 0;
                grdcopy.DataSource = null;
                grdcopy.DataBind();
                hdnGrdId.Value = grdcopy.ClientID;

                grdImported.CurrentPageIndex = 0;
                grdImported.DataSource = null;
                grdImported.DataBind();
                dt.Dispose();
//                grdImported.SelectedIndex = -1;

                var catalogDs = new DataSet();
                var catalogDs1 = new DataSet();
                if (txtcmbJTitle.Text != string.Empty)  // if accessionnumber is selected 
                {
                    CatalogCon.Open();
                    // txtCategory.Value = asmJTitle.SelectedItem.Text
                    // Finding the accessioning path opted ( ie direct,or indent or...)
                    catalogDs = LibObj.PopulateDataset("select released from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "'", "Check", CatalogCon);
                    catalogDs1 = LibObj.PopulateDataset("select released from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "'", "Check1", CatalogCon);
                    if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToLower()== "y") // if case of normal Indent ie not a gift indent
                    {
                        catalogDs = LibObj.PopulateDataset("select volumeno,edition,Vpart as part,language_id,bookaccessionmaster.booktitle,DBO.get_full_name1(indentmaster.firstname1,indentmaster.middlename1,indentmaster.lastname1,indentmaster.firstname2,indentmaster.middlename2,indentmaster.lastname2,indentmaster.firstname3,indentmaster.middlename3,indentmaster.lastname3) as author, bookaccessionmaster.Item_type,departmentcode,bookaccessionmaster.DeptCode from indentmaster,bookaccessionmaster  where  indentmaster.indentid=bookaccessionmaster.indentnumber and bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }
                    else if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToUpper() == "G") // if case of Gift Indent
                    {
                        catalogDs = LibObj.PopulateDataset("select volumeno,edition,Vpart  as part,language_id,bookaccessionmaster.booktitle,DBO.get_full_name1(Giftindentmaster.firstname1,Giftindentmaster.middlename1,Giftindentmaster.lastname1,Giftindentmaster.firstname2,Giftindentmaster.middlename2,Giftindentmaster.lastname2,Giftindentmaster.firstname3,Giftindentmaster.middlename3,Giftindentmaster.lastname3) as author, bookaccessionmaster.Item_type ,bookaccessionmaster.DeptCode from giftindentmaster,bookaccessionmaster  where  giftindentmaster.giftindentid=bookaccessionmaster.indentnumber and bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }
                    else if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToLower() == "e") // if case of existing
                    {
                        catalogDs = LibObj.PopulateDataset("select volumeno,edition,part,language_id,existingbookkinfo.title as booktitle,DBO.get_full_name1(existingbookkinfo.firstname1,existingbookkinfo.middlename1,existingbookkinfo.lastname1,existingbookkinfo.firstname2,existingbookkinfo.middlename2,existingbookkinfo.lastname2,existingbookkinfo.firstname3,existingbookkinfo.middlename3,existingbookkinfo.lastname3) as author , bookaccessionmaster.Item_type,dept as departmentcode,bookaccessionmaster.DeptCode from existingbookkinfo,bookaccessionmaster  where  existingbookkinfo.srNoOld=bookaccessionmaster.srNoOld and bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }
                    else if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToLower() == "i") // if case of Direct invoice
                    {
                        catalogDs = LibObj.PopulateDataset("select volume as volumeno,edition,part,language_id,bookaccessionmaster.booktitle,DBO.get_full_name1(Direct_invoice_transaction.first_name,Direct_invoice_transaction.middle_name,Direct_invoice_transaction.last_name,'','','','','','') as author , bookaccessionmaster.Item_type,bookaccessionmaster.DeptCode from direct_invoice_transaction,bookaccessionmaster  where  direct_invoice_transaction.srNoOld=bookaccessionmaster.srNoOld and bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }
                    else if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToUpper() == "T") // if case Thesis
                    {
                        catalogDs = LibObj.PopulateDataset("select volumeno='',edition='',part='',language_id,bookaccessionmaster.booktitle,DBO.get_full_name1(thesis_accessioning.fname1,thesis_accessioning.mname1,thesis_accessioning.lname1,thesis_accessioning.fname2,thesis_accessioning.mname2,thesis_accessioning.lname2,thesis_accessioning.fname3,thesis_accessioning.mname3,thesis_accessioning .lname3)  as author , bookaccessionmaster.Item_type,bookaccessionmaster.DeptCode from thesis_accessioning,bookaccessionmaster  where  thesis_accessioning.srNoOld=bookaccessionmaster.srNoOld and bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }
                    else if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToUpper() == "J") // if case Thesis
                    {
                        catalogDs = LibObj.PopulateDataset("select volumeno='', edition='',part='',Tran_language as Language_id,bookaccessionmaster.booktitle,author='', bookaccessionmaster.Item_type,bookaccessionmaster.DeptCode from JournalMaster,bookaccessionmaster where bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }
                    else if (catalogDs.Tables["Check"].Rows[0]["released"].ToString().ToLower() == "d") // if case Thesis
                    {
                        catalogDs = LibObj.PopulateDataset("select dbo.DirectCateloginfo.volumeno, dbo.DirectCateloginfo.edition, dbo.DirectCateloginfo.part, dbo.DirectCateloginfo.Language_Id,dbo.DirectCateloginfo.title AS booktitle, dbo.GET_FULL_NAME1(dbo.DirectCateloginfo.firstname1, dbo.DirectCateloginfo.middlename1, dbo.DirectCateloginfo.lastname1, dbo.DirectCateloginfo.firstname2, dbo.DirectCateloginfo.middlename2, dbo.DirectCateloginfo.lastname2, dbo.DirectCateloginfo.firstname3, dbo.DirectCateloginfo.middlename3, dbo.DirectCateloginfo.lastname3) AS author,dbo.bookaccessionmaster.Item_type, dbo.DirectCateloginfo.dept AS departmentcode,bookaccessionmaster.DeptCode from DirectCateloginfo ,bookaccessionmaster  where  DirectCateloginfo .DsrNo=bookaccessionmaster.DsrNo and bookaccessionmaster.accessionnumber=N'" + txtcmbJTitle.Text + "'", "detail", CatalogCon);
                    }

                    if (catalogDs.Tables["detail"].Rows.Count > 0)
                    {
                        txtAuthor.Value = catalogDs.Tables["detail"].Rows[0]["author"].ToString();
                        txtTitle.Value = catalogDs.Tables["detail"].Rows[0]["booktitle"].ToString();
                        txtEdition.Value = catalogDs.Tables["detail"].Rows[0]["edition"].ToString();
                        txtVolume.Value = catalogDs.Tables["detail"].Rows[0]["volumeno"].ToString();
                        txtPart.Value = catalogDs.Tables["detail"].Rows[0]["part"].ToString() == "" || catalogDs.Tables["detail"].Rows[0]["part"].ToString() == "0"
                            ? string.Empty: catalogDs.Tables["detail"].Rows[0]["part"].ToString();
                        hdItemType.Value = catalogDs.Tables["detail"].Rows[0]["Item_type"].ToString();
                        hdLanguageId.Value = catalogDs.Tables["detail"].Rows[0]["language_id"].ToString();
                        this.cmbdept.SelectedValue = catalogDs.Tables["detail"].Rows[0]["DeptCode"].ToString();
                        catalogDs = LibObj.PopulateDataset("select Language_Name from  Translation_Language where language_id=" + hdLanguageId.Value, "lang", CatalogCon);
                        if (catalogDs.Tables["lang"].Rows.Count > 0)
                        {
                            txtLanguage.Value = catalogDs.Tables["lang"].Rows[0]["language_name"].ToString();
                        }
                    }
                    else
                    {

                        return;
                    }
                    // ###################################################
                    // IF DEPTMENT BASSED CATELOGINING IN ON
                    DataSet DeptBassCat = LibObj.PopulateDataset("Select Dept_B_Cat from librarysetupinformation", "DeptTbl", CatalogCon);
                    if (DeptBassCat.Tables["DeptTbl"].Rows[0]["Dept_B_Cat"].ToString().ToUpper() == "Y")
                    {
                        catalogDs = LibObj.PopulateDataset("SELECT classnumber,booknumber,ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname from  dbo.CatalogueCardView where  volume=N'" + txtVolume.Value + "' AND part=N'" + txtPart.Value + "' and edition=N'" + txtEdition.Value + "' and language_id=" + hdLanguageId.Value + " AND  title =(select booktitle from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "') and cast(dept as varchar)='" + this.cmbdept.SelectedValue + "' order by copynumber", "det", CatalogCon); // jitendra dwivedi
                    }
                    else
                    {
                        catalogDs = LibObj.PopulateDataset("SELECT classnumber,booknumber,ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname from  dbo.CatalogueCardView where  volume=N'" + txtVolume.Value + "' AND part=N'" + txtPart.Value + "' and edition=N'" + txtEdition.Value + "' and language_id=" + hdLanguageId.Value         + " AND  title =(select booktitle from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "')   order by copynumber", "det", CatalogCon);
                    } // jitendra dwivedi

                    // if Data found with Matcing criteria
                    if (catalogDs.Tables["det"].Rows.Count > 0)  // IF other catalogued Copies Found
                    {
                        grdcopy.DataSource = catalogDs.Tables["det"];
                        grdcopy.DataBind();
                        hdnGrdId.Value = grdcopy.ClientID;

                        // By Gaurav
                        // txtCtrlNo.Value = catalogDs.Tables("det").Rows(0).Item("ctrl_no")
                        // txtClassNumber.Text = catalogDs.Tables("det").Rows(0).Item("classnumber")
                        // txtBookNumber.Value = catalogDs.Tables("det").Rows(0).Item("booknumber")
                        // txtClassNumber.Disabled = True
                        // txtBookNumber.Disabled = True
                        // chkDifferent.Visible = True
                        // Me.SetFocus(chkDifferent)
                        // 14 november 2007 jai shukla
                        // Else 'if there is possiblity of data being found in imported data
                        // 'IF DEPTMENT BASSED CATELOGINING IN ON
                        // If DeptBassCat.Tables("DeptTbl").Rows(0).Item("Dept_B_Cat") = "Y" Then
                        // catalogDs = LibObj.PopulateDataset("Select classnumber,booknumber,bookcatalog.ctrl_no from  bookcatalog inner join catalogdata on bookcatalog.ctrl_no= catalogdata.ctrl_no where bookcatalog.title=(select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "') and  volume='" & Trim(txtVolume.Value) & "' AND part='" & Trim(txtPart.Value) & "' and edition='" & Trim(txtEdition.Value) & "' and language_name='" & Trim(txtLanguage.Value) & "' and bookcatalog.ctrl_no not in(select ctrl_no from bookaccessionmaster) and dept=" & Me.cmbdept.SelectedValue & "", "Import", CatalogCon)
                        // Else
                        // catalogDs = LibObj.PopulateDataset("Select classnumber,booknumber,bookcatalog.ctrl_no from  bookcatalog inner join catalogdata on bookcatalog.ctrl_no= catalogdata.ctrl_no where bookcatalog.title=(select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "') and  volume='" & Trim(txtVolume.Value) & "' AND part='" & Trim(txtPart.Value) & "' and edition='" & Trim(txtEdition.Value) & "' and language_name='" & Trim(txtLanguage.Value) & "' and bookcatalog.ctrl_no not in(select ctrl_no from bookaccessionmaster)", "Import", CatalogCon)
                        // End If
                        // If catalogDs.Tables("import").Rows.Count > 0 Then
                        // 'By Gaurav
                        // 'txtClassNumber.Text = catalogDs.Tables("import").Rows(0).Item("classnumber")
                        // 'txtBookNumber.Value = catalogDs.Tables("import").Rows(0).Item("booknumber")
                        // 'txtCtrlNo.Value = catalogDs.Tables("import").Rows(0).Item("ctrl_no")
                        // 'txtClassNumber.Disabled = True
                        // 'txtBookNumber.Disabled = True
                        // Dim Btitle As String = String.Empty
                        // catalogDs = LibObj.PopulateDataset("select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "'", "ti", CatalogCon)
                        // If catalogDs.Tables("ti").Rows.Count > 0 Then
                        // Btitle = catalogDs.Tables("ti").Rows(0).Item("booktitle")
                        // End If
                        // catalogDs = LibObj.PopulateDataset("select  distinct dbo.get_subtitle(title,subtitle,paralleltype) as title , firstname1 as author,volume,part,edition,language_name, bookcatalog.ctrl_no,classnumber,booknumber from bookcatalog inner join BookAuthor on bookcatalog.ctrl_no=bookauthor.ctrl_no inner join bookconference on bookcatalog.ctrl_no=bookconference.ctrl_no inner join catalogdata on catalogdata.ctrl_no=bookcatalog.ctrl_no  where bookauthor.ctrl_no not in ( select distinct ctrl_no from bookaccessionmaster) and bookcatalog.title ='" & Btitle & "'", "imp", CatalogCon)
                        // If catalogDs.Tables("imp").Rows.Count > 0 Then
                        // grdImported.DataSource = catalogDs.Tables("imp")
                        // grdImported.DataBind()
                        // Else
                        // Dim td As New DataTable
                        // grdImported.DataSource = td
                        // grdImported.DataBind()
                        // td.Dispose()
                        // End If
                        // End If
                    }
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (CatalogCon.State == ConnectionState.Open)
                {
                    CatalogCon.Close();
                }
                CatalogCon.Dispose();
            }
        }

        protected void cmdNext2_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            Session["BNo"] = txtBookNumber.Value;
            Session["CNo"] = txtClassNumber.Text;
            try
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    if (txtcmbJTitle.Text != string.Empty)
                    {
                        bool flg = false;
                        if ((txtClassNumber.Text.Trim()!=""))
                        {
                            var chkds = new DataSet();
                            con.Open();
                            chkds = LibObj.PopulateDataset("SELECT top 8 ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname,classnumber,booknumber from  dbo.CatalogueCardView where  classnumber=N'" + txtClassNumber.Text.Replace("'", "''") + "' AND booknumber=N'" + txtBookNumber.Value.Replace("'", "''") + "' order by copyNumber", "dup", con);
                            if (chkds.Tables[0].Rows.Count > 0)
                            {
                                flg = true;
                                grdcopy.DataSource = chkds;
                                grdcopy.DataBind();
                                hdnGrdId.Value = grdcopy.ClientID;
                            }
                        }

                        var catalogDs = new DataSet();
                        DataSet DeptBassCat = LibObj.PopulateDataset("Select Dept_B_Cat from librarysetupinformation", "DeptTbl", con);
                        if (DeptBassCat.Tables["DeptTbl"].Rows[0]["Dept_B_Cat"].ToString().ToUpper()== "Y")
                        {
                            if (this.cmbdept.SelectedItem.Text != this.HComboSelect.Value)
                            {
                                catalogDs = LibObj.PopulateDataset("SELECT classnumber,booknumber,ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname from  dbo.CatalogueCardView where  volume=N'" + txtVolume.Value + "' AND part=N'" + txtPart.Value + "' and edition=N'" + txtEdition.Value + "' and Author='" + this.txtAuthor.Value + "'  and language_id=" + hdLanguageId.Value + " AND  title =(select booktitle from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "') and dept=" + this.cmbdept.SelectedValue + "   order by copynumber", "det", con); // jitendra dwivedi
                                Session["Dept"] = this.cmbdept.SelectedValue;
                            }
                            else
                            {
                                catalogDs = LibObj.PopulateDataset("SELECT classnumber,booknumber,ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname from  dbo.CatalogueCardView where  volume=N'" + txtVolume.Value + "' AND part=N'" + txtPart.Value     + "' and edition=N'" + txtEdition.Value + "' and Author='" + this.txtAuthor.Value + "' and language_id=" + hdLanguageId.Value + " AND  title =(select booktitle from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "')   order by copynumber", "det", con);
                            } // jitendra dwivedi
                        }
                        else
                        {
                            catalogDs = LibObj.PopulateDataset("SELECT classnumber,booknumber,ctrl_no,accessionnumber,volume,dbo.get_subtitle(title,subtitle,paralleltype)  as title,dbo.GET_AUTHOR(auf1,aum1,aul1,auf2,aum2,aul2,auf3,aum3,aul3,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3)as Author,dbo.CatalogueCardView.edition,dbo.CatalogueCardView.editionyear,copyNumber,part,Language_name,departmentname from  dbo.CatalogueCardView where  volume=N'" + txtVolume.Value + "' AND part=N'" + txtPart.Value + "' and edition=N'" + txtEdition.Value + "' and Author='" + this.txtAuthor.Value + "'  and language_id=" + hdLanguageId.Value + " AND  title =(select booktitle from bookaccessionmaster where accessionnumber=N'" + txtcmbJTitle.Text + "')   order by copynumber", "det", con);
                        } // jitendra dwivedi
                          // if Data found with Matcing criteria
                        if (catalogDs.Tables["det"].Rows.Count > 0)  // IF other catalogued Copies Found
                        {
                            grdcopy.DataSource = catalogDs.Tables["det"];
                            grdcopy.DataBind();
                            hdnGrdId.Value = grdcopy.ClientID;
                            txtCtrlNo.Value = catalogDs.Tables["det"].Rows[0]["ctrl_no"].ToString();
                            txtClassNumber.Text = catalogDs.Tables["det"].Rows[0]["classnumber"].ToString();
                            txtBookNumber.Value = catalogDs.Tables["det"].Rows[0]["booknumber"].ToString();
                            txtClassNumber.Enabled = false;
                            txtBookNumber.Disabled = true;
                            chkDifferent.Visible = true;
                            this.SetFocus(chkDifferent);

                            // 14 november 2007 jai shukla
                            // Else 'if there is possiblity of data being found in imported data
                            // 'IF DEPTMENT BASSED CATELOGINING IN ON
                            // If DeptBassCat.Tables("DeptTbl").Rows(0).Item("Dept_B_Cat") = "Y" Then
                            // If Me.cmbdept.SelectedItem.Text <> Me.HComboSelect.Value Then
                            // catalogDs = LibObj.PopulateDataset("Select classnumber,booknumber,bookcatalog.ctrl_no from  bookcatalog inner join catalogdata on bookcatalog.ctrl_no= catalogdata.ctrl_no where bookcatalog.title=(select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "') and  volume='" & Trim(txtVolume.Value) & "' AND part='" & Trim(txtPart.Value) & "' and edition='" & Trim(txtEdition.Value) & "' and language_name='" & Trim(txtLanguage.Value) & "' and bookcatalog.ctrl_no not in(select ctrl_no from bookaccessionmaster) and dept=" & Me.cmbdept.SelectedValue & "", "Import", con)
                            // Session("Dept") = Me.cmbdept.SelectedValue
                            // Else
                            // catalogDs = LibObj.PopulateDataset("Select classnumber,booknumber,bookcatalog.ctrl_no from  bookcatalog inner join catalogdata on bookcatalog.ctrl_no= catalogdata.ctrl_no where bookcatalog.title=(select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "') and  volume='" & Trim(txtVolume.Value) & "' AND part='" & Trim(txtPart.Value) & "' and edition='" & Trim(txtEdition.Value) & "' and language_name='" & Trim(txtLanguage.Value) & "' and bookcatalog.ctrl_no not in(select ctrl_no from bookaccessionmaster)", "Import", con)
                            // End If
                            // Else
                            // catalogDs = LibObj.PopulateDataset("Select classnumber,booknumber,bookcatalog.ctrl_no from  bookcatalog inner join catalogdata on bookcatalog.ctrl_no= catalogdata.ctrl_no where bookcatalog.title=(select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "') and  volume='" & Trim(txtVolume.Value) & "' AND part='" & Trim(txtPart.Value) & "' and edition='" & Trim(txtEdition.Value) & "' and language_name='" & Trim(txtLanguage.Value) & "' and bookcatalog.ctrl_no not in(select ctrl_no from bookaccessionmaster)", "Import", con)
                            // End If
                            // If catalogDs.Tables("import").Rows.Count > 0 Then
                            // txtClassNumber.Text = catalogDs.Tables("import").Rows(0).Item("classnumber")
                            // txtBookNumber.Value = catalogDs.Tables("import").Rows(0).Item("booknumber")
                            // txtCtrlNo.Value = catalogDs.Tables("import").Rows(0).Item("ctrl_no")
                            // txtClassNumber.Disabled = True
                            // txtBookNumber.Disabled = True
                            // Dim Btitle As String = String.Empty
                            // catalogDs = LibObj.PopulateDataset("select booktitle from bookaccessionmaster where accessionnumber='" &txtcmbJTitle.Text & "'", "ti", con)
                            // If catalogDs.Tables("ti").Rows.Count > 0 Then
                            // Btitle = catalogDs.Tables("ti").Rows(0).Item("booktitle")
                            // End If
                            // catalogDs = LibObj.PopulateDataset("select  distinct dbo.get_subtitle(title,subtitle,paralleltype) as title , firstname1 as author,volume,part,edition,language_name, bookcatalog.ctrl_no,classnumber,booknumber from bookcatalog inner join BookAuthor on bookcatalog.ctrl_no=bookauthor.ctrl_no inner join bookconference on bookcatalog.ctrl_no=bookconference.ctrl_no inner join catalogdata on catalogdata.ctrl_no=bookcatalog.ctrl_no  where bookauthor.ctrl_no not in ( select distinct ctrl_no from bookaccessionmaster) and bookcatalog.title ='" & Btitle & "'", "imp", con)
                            // If catalogDs.Tables("imp").Rows.Count > 0 Then
                            // grdImported.DataSource = catalogDs.Tables("imp")
                            // grdImported.DataBind()
                            // Else
                            // Dim td As New DataTable
                            // grdImported.DataSource = td
                            // grdImported.DataBind()
                            // td.Dispose()
                            // End If
                            // End If
                        }
                        if (flg == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "ConfirmbeforeSave();", true);
                            return;
                            // hdConfirm.Value = String.Empty
                        }
                        // If hdConfirm.Value <> "Y" Then
                        // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "ConfirmbeforeSave();", True)
                        // Exit Sub
                        // hdConfirm.Value = String.Empty
                        // End If
                        Session["accno"] = txtcmbJTitle.Text.ToString();
                        Session["BNumber"] = txtBookNumber.Value.ToString();
                        Session["CNumber"] = txtClassNumber.Text.ToString();
                        Session["back"] = "catalog";
                        // If chkNewCtrl.Checked = True Then
                        // Response.Redirect("CatalogDetail.aspx?title=" & lblt1.Text & "&ctrl=" & txtCtrlNo.Value & "&nctrl=" & "9" & "&bt=" & hdItemType.Value)
                        // Else
                        Response.Redirect("CatalogDetail.aspx?title=" + lblt1.Text + "&ctrl=" + txtCtrlNo.Value + "&bt=" + hdItemType.Value);
                        // End If
//                        Response.Write("hello");
                    }
                    else
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.SelAccNo, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelAccNo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SelAccNo, this, dbUtilities.MsgLevel.Warning);
                        SetFocus("txtcmbJTitle");
                    }
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    if (txtcmbJTitle.Text != string.Empty)
                    {
                        var chkds = new DataSet();
                        con.Open();
                        chkds = LibObj.PopulateDataset("select  distinct MARCEXT_BookCatalog.title , firstname1 as author,volume,part,edition,language_name, MARCEXT_bookcatalog.ctrl_no,classnumber,booknumber from MARCEXT_bookcatalog inner join MARCEXT_BookAuthor on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookauthor.ctrl_no inner join MARCEXT_bookconference on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookconference.ctrl_no inner join MARCEXT_catalogdata on MARCEXT_catalogdata.ctrl_no=MARCEXT_bookcatalog.ctrl_no  where MARCEXT_bookcatalog.title like '%" + txtTitle.Value + "%'", "imp", con);
                        bool flg = false;
                        if (chkds.Tables[0].Rows.Count > 0)
                        {
                            flg = true;
                            grdImported.DataSource = chkds;
                            grdImported.DataBind();
                        }

                        var catalogDs = new DataSet();
                        DataSet DeptBassCat = LibObj.PopulateDataset("Select Dept_B_Cat from librarysetupinformation", "DeptTbl", con);
                        if (DeptBassCat.Tables["DeptTbl"].Rows[0]["Dept_B_Cat"].ToString().ToUpper() == "Y")
                        {
                            if (this.cmbdept.SelectedItem.Text != this.HComboSelect.Value)
                            {
                                // this query need s modification according to deptwise cataloging
                                catalogDs = LibObj.PopulateDataset("select  distinct MARCEXT_BookCatalog.title , firstname1 as author,volume,part,edition,language_name, MARCEXT_bookcatalog.ctrl_no,classnumber,booknumber from MARCEXT_bookcatalog inner join MARCEXT_BookAuthor on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookauthor.ctrl_no inner join MARCEXT_bookconference on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookconference.ctrl_no inner join MARCEXT_catalogdata on MARCEXT_catalogdata.ctrl_no=MARCEXT_bookcatalog.ctrl_no  where MARCEXT_bookcatalog.title like '%" + txtTitle.Value + "%'", "det", con);
                                Session["Dept"] = this.cmbdept.SelectedValue;
                            }
                            else
                            {
                                catalogDs = LibObj.PopulateDataset("select  distinct MARCEXT_BookCatalog.title , firstname1 as author,volume,part,edition,language_name, MARCEXT_bookcatalog.ctrl_no,classnumber,booknumber from MARCEXT_bookcatalog inner join MARCEXT_BookAuthor on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookauthor.ctrl_no inner join MARCEXT_bookconference on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookconference.ctrl_no inner join MARCEXT_catalogdata on MARCEXT_catalogdata.ctrl_no=MARCEXT_bookcatalog.ctrl_no  where MARCEXT_bookcatalog.title like '%" + txtTitle.Value + "%'", "det", con);
                            }
                        }
                        else
                        {
                            catalogDs = LibObj.PopulateDataset("select  distinct MARCEXT_BookCatalog.title , firstname1 as author,volume,part,edition,language_name, MARCEXT_bookcatalog.ctrl_no,classnumber,booknumber from MARCEXT_bookcatalog inner join MARCEXT_BookAuthor on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookauthor.ctrl_no inner join MARCEXT_bookconference on MARCEXT_bookcatalog.ctrl_no=MARCEXT_bookconference.ctrl_no inner join MARCEXT_catalogdata on MARCEXT_catalogdata.ctrl_no=MARCEXT_bookcatalog.ctrl_no  where MARCEXT_bookcatalog.title like '%" + txtTitle.Value + "%'", "det", con);
                        }
                        // if Data found with Matcing criteria
                        var ds = new DataSet();
                        ds = LibObj.PopulateDataset("select max(ctrl_no)+1 from bookcatalog", "getmax", con);

                        if (catalogDs.Tables["det"].Rows.Count > 0)  // IF other catalogued Copies Found
                        {
                            grdImported.DataSource = catalogDs.Tables["det"];
                            grdImported.DataBind();
                            txtCtrlNo.Value = ds.Tables[0].Rows[0][0].ToString(); // 
                            Session["marcnewid"] = catalogDs.Tables["det"].Rows[0]["ctrl_no"].ToString();
                            ds.Clear();
                            ds.Dispose();
                            txtClassNumber.Text = catalogDs.Tables["det"].Rows[0]["classnumber"].ToString();
                            txtBookNumber.Value = catalogDs.Tables["det"].Rows[0]["booknumber"].ToString();
                            txtClassNumber.Enabled = false;
                            txtBookNumber.Disabled = true;
                            chkDifferent.Visible = true;
                            this.SetFocus(chkDifferent);
                        }
                        if (flg == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "ConfirmbeforeSave();", true);
                            return;
                        }
                        Session["accno"] = txtcmbJTitle.Text.ToString();
                        Session["BNumber"] = txtBookNumber.Value.ToString();
                        Session["CNumber"] = txtClassNumber.Text.ToString();
                        Session["back"] = "catalog";
                        Response.Redirect("CatalogDetail.aspx?title=" + lblt1.Text + "&ctrl=" + txtCtrlNo.Value + "&bt=" + hdItemType.Value);
//                        Response.Write("hello");
                    }
                    else
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.SelAccNo, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelAccNo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SelAccNo, this, dbUtilities.MsgLevel.Warning);
                        SetFocus("txtcmbJTitle");
                    }
                }
                Session["DDC"] = "";
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
            }
        }
        protected void confirmBefSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                // hdConfirm.Value = String.Empty
                Session["accno"] = txtcmbJTitle.Text;
                Session["BNumber"] = Session["BNo"]; // Trim(txtBookNumber.Value.ToString)
                string a =     Session["CNo"].ToString();
                Session["CNumber"] = a; // Trim(txtClassNumber.Text.ToString)
                Session["back"] = "catalog";
                if (chkNewCtrl.Checked == true)
                {
                    Response.Redirect("CatalogDetail.aspx?title=" + lblt1.Text + "&ctrl=" + txtCtrlNo.Value + "&nctrl=" + "9" + "&bt=" + hdItemType.Value, true);
                }
                else
                {
                    Response.Redirect("CatalogDetail.aspx?title=" + lblt1.Text + "&ctrl=" + txtCtrlNo.Value + "&bt=" + hdItemType.Value, true);
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

            }
        }
        public void clearfields()
        {
            try
            {

            }
            catch
            {

            }

        }
    }

}