using System;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using Library.Models;
using Model.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibData.Model;
using System.Data.OleDb;
using LibData.Contract;
using ItemIssueReturn2;
using System.Data;
using System.Xml.Linq;

namespace Library
{
    public partial class indent : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        // private Member objBLL = new Member();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();
        ApiComm sav = new ApiComm();
        protected async void Page_Load(object sender, EventArgs e)
        {
            var apiCall = new ApiComm();
            if (!IsPostBack)
            {
                try
                {
                    /***** apiCall   ****/
                    var libSetUp =await apiCall.getLibSetup();
                    List<departmentmaster> lsdept = new List<departmentmaster>();
                    hdOnlineP.Value = libSetUp. Data.OnlinePIndent;
                    if (libSetUp.Data.checkBudget.ToUpper() == "N")
                    {
                        var d = await apiCall.getAllDept();  //Data is return value(list for datatable) , check isstatus and message
                        //dept has shortname included
                        lsdept = d.Data;
                    }
                    else
                    {
                        var d = await apiCall.getDeptBySession();
                        lsdept = d.Data;

                    }

                    var lang = await apiCall.getLanguages();

                   // var exch = await apiCall.gete();

                }
                catch (Exception ex)
                {
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }

            }
        }
        private void checkPermission()
        {
            var strMID = "3001";
            //GetActionPerm
        }
        private void default_setting()
        {
            //get libset like abov
            //getpublisherbyid in indent
            //GetVendorByCode
            //GetExchangeFromSetup basic
        }
        public bool actionPermission(string sqlQuery)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var cmd = new OleDbCommand(sqlQuery, con);
            int i = 0;
            if(cmd.ExecuteScalar()== null) /*(Conversions.ToBoolean(Operators.OrObject(Operators.ConditionalCompareObjectEqual(cmd.ExecuteScalar(), null, false), Operators.ConditionalCompareObjectEqual(cmd.ExecuteScalar() ? "N" : false))))*/
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            if (i == 0)
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
                return false;
            }
            else
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
                return true;
            }
        }
        public async void populateRequesters(int depCode, OleDbConnection Conn)
        {
            try
            {
                UserByCondCmd deptreq = new UserByCondCmd();
                deptreq.CanRequest = true;
                deptreq.DeptCode = depCode;
            /*    var chkreq = await sav.getCircUserByDeptCanReq(deptreq);

                //OleDbDataReader genDr = objDAL.rtnDataReader("Select CircUserManagement.userid,firstname+' '+middlename+' '+lastname as firstname from CircUserManagement,circclassmaster where CircUserManagement.departmentcode=" + depCode + " and  CircUserManagement.classname=circclassmaster.classname and circclassmaster.canRequest=N'Y'  order by firstname+' '+middlename+' '+lastname ", Conn);
                
                if (chkreq.isSuccess)
                {
                    cmbreq.DataSource = chkreq.Data;
                    cmbreq.DataTextField = "name";
                    cmbreq.DataValueField = "userid";
                    cmbreq.DataBind();
                    cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                }
                else
                {
                    cmbreq.Items.Clear();
                }
                //cmbreq.Items.Add("HOD");

                //cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
              */
                return;
            }
            catch(Exception ex)
            {

                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected async void cmdsave1_Click(object sender, EventArgs e)
        {
            
            if (cmbdept.SelectedItem.Text == "---Select---")
            {
               
                message.PageMesg("Select department", this, dbUtilities.MsgLevel.Warning);
                return;
            }
            if (cmbLanguage.SelectedItem.Text == "---Select---")
            {
              
                message.PageMesg("Select Language", this, dbUtilities.MsgLevel.Warning);
                return;

            }
            if (cmbbookcategory.Value == HComboSelect.Value)
            {
                
                message.PageMesg("Select Book Category", this,dbUtilities.MsgLevel.Warning);
                return;

            }
            if (persontype.SelectedItem.Text == "---Select---")
            {
               
                message.PageMesg("Select Author type/Statement of responsibility", this, dbUtilities.MsgLevel.Warning);
                return;

            }
            if (mediatype.SelectedItem.Text == "---Select---")
            {
               
                message.PageMesg("Select Media type", this, dbUtilities.MsgLevel.Warning);
                return;

            }
            if (cmbcurr.SelectedItem.Text == "---Select---")
            {
                
                message.PageMesg("Select Currency", this, dbUtilities.MsgLevel.Warning);
                return;

            }
            if (txtnoofcopies.Value == "" | txtnoofcopies.Value == "0")
            {

                try
                {
                    int c = Convert.ToInt32(txtnoofcopies.Value);
                }
                catch (Exception ex)
                {
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Enter correct no of copies", Me)
                    message.PageMesg("Enter correct no of copies", this, dbUtilities.MsgLevel.Warning);
                    return;

                }
            }
            if (txtprice.Value == "")
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Enter correct Price", Me)
                message.PageMesg("Enter correct Price", this,   dbUtilities.MsgLevel.Warning);
                return;

            }
            try
            {
                double d = Convert .ToDouble(txtprice.Value);
            }
            catch (Exception ex)
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Enter correct Price", Me)
                message.PageMesg("Enter correct Price", this, dbUtilities.MsgLevel.Warning);
                return;

            }

            if ((hdOnlineP.Value) == "Y")
            {
                if (hdAskValidate.Value == "0")
                {
                    // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "Validate('IndentedBy');", True)
                    string returnScript = "";
                    returnScript += "<script language='javascript' type='text/javascript'>";
                    returnScript += "javascript:Validate('IndentedBy');";
                    returnScript += "<" + "/" + "script>";
                    Page.RegisterStartupScript("", returnScript);
                    return;
                }
                if (hdAskValidate.Value == "1")
                {
                    hdAskValidate.Value = "0";
                    if (actionPermission("SELECT actionLPerm.permission FROM userdetails INNER JOIN actionLPerm ON userdetails.usertype = actionLPerm.userTypeId WHERE userdetails.memberid = '" + (hdValidUserId.Value) + "' and actionLPerm.actionid='6' and actionLPerm.submenu_id='3001'") == false)
                    {

                        message.PageMesg(Resources.ValidationResources.NotAuth, this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                }
            }
            OleDbConnection indentcon = null;
            try
            {
                string tcase;
                //tcase = AppSettings.Get("tcase");
                //indentcon = objDAL.retConnection;
                indentcon.Open();
                
                var cmd1 = new OleDbCommand("select checkBudget  from librarysetupinformation", indentcon);
                string bugdet = Convert.ToString(cmd1.ExecuteScalar());
                if (bugdet == "Y")
                {
                    if (chkapproval.Checked == true)
                    {
                        var cdm = new OleDbCommand();
                        cdm.Connection = indentcon;
                        var r2 = new ProgramCmd();
                        r2.ProgramName = "b.";
                        r2.Shortname = "";
                        var resu = await sav.GetProgMaster(r2);

                        //cdm.CommandType = CommandType.Text;
                        //cdm.CommandText = "SELECT allocatedamount,approvalcommitedamt+nonapprovalcommitedamt AS amt,VendorPer FROM budgetmaster WHERE departmentcode=" + (cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'";
                        var dr = cdm.ExecuteReader();
                        decimal amt = 0.0m;
                        decimal amtPer = 0.0m;
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                amt = Convert.ToDecimal(dr["amt"].ToString());
                                amt = amt + Convert.ToDecimal(txtTotalAmount.Value);
                                if (!string.IsNullOrEmpty(dr["VendorPer"].ToString()))
                                {
                                    amtPer = amt * Convert.ToDecimal(dr["VendorPer"]) / 100m;
                                }
                                amt = amt + amtPer;
                                if (amt > Convert.ToDecimal(dr["allocatedamount"]))
                                {
                                    amt = amt - Convert.ToDecimal(dr["allocatedamount"]);
                                    amt = Convert.ToDecimal(txtTotalAmount.Value) - amt;
                                    
                                    message.PageMesg("You can commit only this Amount :" + "" + amt + "", this, dbUtilities.MsgLevel.Warning);
                                    return;
                                }
                            }


                        }
                    }
                }

                if (this.cmbdept.SelectedValue != HComboSelect.Value)
                {
                    populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), indentcon);
                    if (hReq.Value != "")
                    {
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }
                    else
                    {
                        hReq.Value = this.cmbreq.SelectedValue;
                    }
                }
               
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
               
                DateTime d1;
                DateTime d3;
                DateTime d4;
                d3 = Convert.ToDateTime(Session["sessionStartDate"]);
                d4 = Convert.ToDateTime(Session["sessionEndDate"]);
                d1 = Convert.ToDateTime(txtindentdate.Text);
                if (d1 < d3 | d1 > d4)
                {

                    if ((hdOnlineP.Value) == "Y")
                    {
                       
                        message.PageMesg(Resources.ValidationResources.IDate, this,
                            dbUtilities.MsgLevel.Warning);
                    }
                    else
                    {
                       
                        message.PageMesg(Resources.ValidationResources.IDate, this, dbUtilities.MsgLevel.Warning); 
                    }

                    this.SetFocus(txtindentdate);
                    return;
                }
                // ---Check Is title Duplicate?
                if (cmdsave.Value != "Update")
                {
                    if (LibObj.isDulicate("select title from indentmaster where  title =N'" + txttitle.Text + "'", indentcon) == true)
                    {
                       
                        message.PageMesg("Entered title is already exists,required some changes.", this,dbUtilities.MsgLevel.Warning);

                        return;
                    }
                }
                // ----------------------------------- 

                lnkContinue.Visible = true;
                lnkModify.Visible = true;
                //LibObj.SetFocus("lnkNew", this);
                bool flag = false;
               

                if (cmdsave.Value == Resources.ValidationResources.bSave.ToString() & !string.IsNullOrEmpty(txtIndentNumber.Value.Trim().ToString()))
                {
                   
                }
                else if (cmdsave.Value == Resources.ValidationResources.bSave.ToString() & string.IsNullOrEmpty(txtIndentNumber.Value.Trim().ToString()))
                {
                    flag = true;
                    
                }
               
                var tran = default(OleDbTransaction);
               
                if (Hdventag.Value != "C")
                {
                    if (LibObj.isDulicate("select indentid from indentmaster where vendorid <> N'" + HdVendorid.Value + "' and indentnumber=N'" + txtIndentNumber.Value + "' and indentid <>N'" + hdindentId.Value + "'", indentcon) == true)
                    {
                        if ((hdOnlineP.Value) == "Y")
                        {
                           
                            message.PageMesg(Resources.ValidationResources.ISameVen.ToString(), this,  dbUtilities.MsgLevel.Warning);
                        }
                        else
                        {
                            
                            message.PageMesg(Resources.ValidationResources.ISameVen.ToString(), this,  dbUtilities.MsgLevel.Warning);
                        }
                        indentcon.Close();
                        indentcon.Dispose();
                        return;
                    }
                }
                
                if (LibObj.isDulicate("select indentid from indentmaster where indentdate <>'" + d1.ToString("dd-MMM-yyyy") + "' and indentnumber=N'" + txtIndentNumber.Value + "' and indentid <>N'" + hdindentId.Value + "'", indentcon) == true)
                {
                    if ((hdOnlineP.Value) == "Y")
                    {
                        
                        message.PageMesg(Resources.ValidationResources.ISameDate.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else
                    {
                        
                        message.PageMesg(Resources.ValidationResources.ISameDate, this,  dbUtilities.MsgLevel.Warning);
                    }
                    indentcon.Close();
                    indentcon.Dispose();
                    return;
                }
                
                if (txtCmbPublisher.Text.Trim() == default)
                {
                   
                    message.PageMesg(Resources.ValidationResources.IPubNotExist, this,  dbUtilities.MsgLevel.Warning);

                   
                    hdPublisherId.Value = null;
                    hdPubId.Value = "";
                    return;
                }
                else
                {
                    
                }

                if (txtCmbVendor.Text.Trim() == default)
                {
                   
                    message.PageMesg(Resources.ValidationResources.VendorNotFound, this, dbUtilities.MsgLevel.Warning);
                   
                    HdVendorid.Value = null;
                    return;
                }
                else
                {
                   
                }
                //try
                //{


                    //    tran = indentcon.BeginTransaction();
                    //    string[] spParameters = new string[] { "@indentnumber_1", "@indentdate_2", "@mediatype_3", "@requestercode_4", "@departmentcode_5", "@title_6", "@authortype_7", "@firstname1_8", "@middlename1_9", "@lastname1_10", "@firstname2_11", "@middlename2_12", "@lastname2_13", "@firstname3_14", "@middlename3_15", "@lastname3_16", "@edition_17", "@yearofedition_18", "@volumeno_19", "@isbn_20", "@category_21", "@currencycode_22", "@go_bank_23", "@exchangerate_24", "@noofcopies_25", "@approval_26", "@price_27", "@totalamount_28", "@coursenumber_29", "@noofstudents_30", "@publisherid_31", "@vendorid_32", "@recordingdate_33", "@gifted_34", "@indenttype_35", "@indenttime_36", "@seriesname_37", "@order_check_code_38", "@yearofPublication_39", "@isSatnding_40", "@IndentId_41", "@Vpart_42", "@ItemNo_43", "@subtitle_44", "@Language_Id_45", "@userid_46" };
                    //    object[] spDataType = new object[] { OleDbType.VarWChar, OleDbType.Date, OleDbType.Integer, OleDbType.VarWChar, OleDbType.Integer, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.Integer, OleDbType.VarWChar, OleDbType.Decimal, OleDbType.Integer, OleDbType.VarWChar, OleDbType.Decimal, OleDbType.Decimal, OleDbType.VarWChar, OleDbType.Integer, OleDbType.Integer, OleDbType.Integer, OleDbType.Date, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.Date, OleDbType.VarWChar, OleDbType.Integer, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.Integer, OleDbType.VarWChar };
                    //    if (Hdventag.Value == "M")
                    //    {
                    //        string[] spPrmtValue = new string[] { Strings.Trim(txtIndentNumber.Value), d1, Val(mediatype.SelectedItem.Value), hReq.Value, Val(cmbdept.SelectedItem.Value), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txttitle.Text)), Strings.Trim(txttitle.Text)), persontype.SelectedItem.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname1.Value)), Strings.Trim(txtfname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname1.Value)), Strings.Trim(txtmname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname1.Value)), Strings.Trim(txtlname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname2.Value)), Strings.Trim(txtfname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname2.Value)), Strings.Trim(txtmname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname2.Value)), Strings.Trim(txtlname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname3.Value)), Strings.Trim(txtfname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname3.Value)), Strings.Trim(txtmname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname3.Value)), Strings.Trim(txtlname3.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtedition.Value)), string.Empty, Strings.Trim(txtedition.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtyrofedition.Value)), string.Empty, Strings.Trim(txtyrofedition.Value)), Strings.Trim(txtvolno.Value), Strings.Trim(txtisbn.Value), cmbbookcategory.Value, Val(cmbcurr.SelectedItem.Value), cmbgocorbank.SelectedItem.Text, Val(Strings.Trim(txtExchangeRate.Value)), Val(txtnoofcopies.Value), "n", Val(txtprice.Value), Val(txtTotalAmount.Value), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtcoursenm.Value)), string.Empty, Strings.Trim(txtcoursenm.Value)), Interaction.IIf(Val(txtnoofstud.Value) == 0, 0, Val(txtnoofstud.Value)), hdPubId.Value, HdVendorid.Value, DateTime.Now.ToShortDateString(), "0", Interaction.IIf(chkapproval.Checked == true, "Approval", "Non-Approval"), DateTime.Now.ToString("hh:mm:ss tt"), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSeries.Value)), Strings.Trim(txtSeries.Value)), 0, Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtPubYear.Value)), string.Empty, Strings.Trim(txtPubYear.Value)), Interaction.IIf(chkStanding.Checked == true, "y", "n"), hdindentId.Value, txtPart.Value, hditmeId.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSubtitle.Value)), Strings.Trim(txtSubtitle.Value)), Val(cmbLanguage.SelectedItem.Value), Session["user_id"] };
                    //        objDAL.spUnderTrans1(tran, "insert_indentmaster_1", spParameters, spPrmtValue, spDataType);
                    //    }
                    //    // Dim retVal As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_indentmaster_1", spParameters, spPrmtValue)
                    //    else if (Hdventag.Value == "C")
                    //    {
                    //        string[] spPrmtValue = new string[] { Strings.Trim(txtIndentNumber.Value), d1, Val(mediatype.SelectedItem.Value), hReq.Value, Val(cmbdept.SelectedItem.Value), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txttitle.Text)), Strings.Trim(txttitle.Text)), persontype.SelectedItem.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname1.Value)), Strings.Trim(txtfname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname1.Value)), Strings.Trim(txtmname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname1.Value)), Strings.Trim(txtlname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname2.Value)), Strings.Trim(txtfname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname2.Value)), Strings.Trim(txtmname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname2.Value)), Strings.Trim(txtlname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname3.Value)), Strings.Trim(txtfname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname3.Value)), Strings.Trim(txtmname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname3.Value)), Strings.Trim(txtlname3.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtedition.Value)), string.Empty, Strings.Trim(txtedition.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtyrofedition.Value)), string.Empty, Strings.Trim(txtyrofedition.Value)), Strings.Trim(txtvolno.Value), Strings.Trim(txtisbn.Value), cmbbookcategory.Value, Val(cmbcurr.SelectedItem.Value), cmbgocorbank.SelectedItem.Text, Val(Strings.Trim(txtExchangeRate.Value)), Val(txtnoofcopies.Value), "n", Val(txtprice.Value), Val(txtTotalAmount.Value), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtcoursenm.Value)), string.Empty, Strings.Trim(txtcoursenm.Value)), Interaction.IIf(Val(txtnoofstud.Value) == 0, 0, Val(txtnoofstud.Value)), hdPubId.Value, HdVendorid.Value, DateTime.Now.ToShortDateString(), "0", Interaction.IIf(chkapproval.Checked == true, "Approval", "Non-Approval"), DateTime.Now.ToString("hh:mm:ss tt"), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSeries.Value)), Strings.Trim(txtSeries.Value)), 0, Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtPubYear.Value)), string.Empty, Strings.Trim(txtPubYear.Value)), Interaction.IIf(chkStanding.Checked == true, "y", "n"), hdindentId.Value, txtPart.Value, hditmeId.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSubtitle.Value)), Strings.Trim(txtSubtitle.Value)), Val(cmbLanguage.SelectedItem.Value), Session["user_id"] };
                    //        objDAL.spUnderTrans1(tran, "insert_indentmaster_1", spParameters, spPrmtValue, spDataType);
                    //    }
                    //    // Dim retVal As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_indentmaster_1", spParameters, spPrmtValue)
                    //    else if (HdVendorid.Value == default)
                    //    {
                    //        string[] spPrmtValue = new string[] { Strings.Trim(txtIndentNumber.Value), d1, Val(mediatype.SelectedItem.Value), hReq.Value, Val(cmbdept.SelectedItem.Value), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txttitle.Text)), Strings.Trim(txttitle.Text)), persontype.SelectedItem.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname1.Value)), Strings.Trim(txtfname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname1.Value)), Strings.Trim(txtmname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname1.Value)), Strings.Trim(txtlname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname2.Value)), Strings.Trim(txtfname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname2.Value)), Strings.Trim(txtmname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname2.Value)), Strings.Trim(txtlname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname3.Value)), Strings.Trim(txtfname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname3.Value)), Strings.Trim(txtmname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname3.Value)), Strings.Trim(txtlname3.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtedition.Value)), string.Empty, Strings.Trim(txtedition.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtyrofedition.Value)), string.Empty, Strings.Trim(txtyrofedition.Value)), Strings.Trim(txtvolno.Value), Strings.Trim(txtisbn.Value), cmbbookcategory.Value, Val(cmbcurr.SelectedItem.Value), cmbgocorbank.SelectedItem.Text, Val(Strings.Trim(txtExchangeRate.Value)), Val(txtnoofcopies.Value), "n", Val(txtprice.Value), Val(txtTotalAmount.Value), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtcoursenm.Value)), string.Empty, Strings.Trim(txtcoursenm.Value)), Interaction.IIf(Val(txtnoofstud.Value) == 0, 0, Val(txtnoofstud.Value)), hdPubId.Value, Val(HdVendorid.Value), DateTime.Now.ToShortDateString(), "0", Interaction.IIf(chkapproval.Checked == true, "Approval", "Non-Approval"), DateTime.Now.ToString("hh:mm:ss tt"), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSeries.Value)), Strings.Trim(txtSeries.Value)), 0, Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtPubYear.Value)), string.Empty, Strings.Trim(txtPubYear.Value)), Interaction.IIf(chkStanding.Checked == true, "y", "n"), hdindentId.Value, txtPart.Value, hditmeId.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSubtitle.Value)), Strings.Trim(txtSubtitle.Value)), Val(cmbLanguage.SelectedItem.Value), Session["user_id"] };
                    //        objDAL.spUnderTrans1(tran, "insert_indentmaster_1", spParameters, spPrmtValue, spDataType);
                    //    }
                    //    // Dim retVal As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_indentmaster_1", spParameters, spPrmtValue)
                    //    else
                    //    {
                    //        string[] spPrmtValue = new string[] { Strings.Trim(txtIndentNumber.Value), d1, Val(mediatype.SelectedItem.Value), hReq.Value, Val(cmbdept.SelectedItem.Value), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txttitle.Text)), Strings.Trim(txttitle.Text)), persontype.SelectedItem.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname1.Value)), Strings.Trim(txtfname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname1.Value)), Strings.Trim(txtmname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname1.Value)), Strings.Trim(txtlname1.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname2.Value)), Strings.Trim(txtfname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname2.Value)), Strings.Trim(txtmname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname2.Value)), Strings.Trim(txtlname2.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtfname3.Value)), Strings.Trim(txtfname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtmname3.Value)), Strings.Trim(txtmname3.Value)), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtlname3.Value)), Strings.Trim(txtlname3.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtedition.Value)), string.Empty, Strings.Trim(txtedition.Value)), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtyrofedition.Value)), string.Empty, Strings.Trim(txtyrofedition.Value)), Strings.Trim(txtvolno.Value), Strings.Trim(txtisbn.Value), cmbbookcategory.Value, Val(cmbcurr.SelectedItem.Value), cmbgocorbank.SelectedItem.Text, Val(Strings.Trim(txtExchangeRate.Value)), Val(txtnoofcopies.Value), "n", Val(txtprice.Value), Val(txtTotalAmount.Value), Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtcoursenm.Value)), string.Empty, Strings.Trim(txtcoursenm.Value)), Interaction.IIf(Val(txtnoofstud.Value) == 0, 0, Val(txtnoofstud.Value)), hdPubId.Value, Val(HdVendorid.Value), DateTime.Now.ToShortDateString(), "0", Interaction.IIf(chkapproval.Checked == true, "Approval", "Non-Approval"), DateTime.Now.ToString("hh:mm:ss tt"), Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSeries.Value)), Strings.Trim(txtSeries.Value)), 0, Interaction.IIf(string.IsNullOrEmpty(Strings.Trim(txtPubYear.Value)), string.Empty, Strings.Trim(txtPubYear.Value)), Interaction.IIf(chkStanding.Checked == true, "y", "n"), hdindentId.Value, txtPart.Value, hditmeId.Value, Interaction.IIf(tcase == "Y", LibObj.TitleCase(Strings.Trim(txtSubtitle.Value)), Strings.Trim(txtSubtitle.Value)), Val(cmbLanguage.SelectedItem.Value), Session["user_id"] };
                    //        objDAL.spUnderTrans1(tran, "insert_indentmaster_1", spParameters, spPrmtValue, spDataType);
                    //        // Dim retVal As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_indentmaster_1", spParameters, spPrmtValue)
                    //    }


                    //    txtchangeval.Value = String.Trim(txtIndentNumber.Value);

                    //    if (hdIndentIdR.Value != default)
                    //    {
                    //        objDAL.queInsUpdUnderTrans(tran, "update opacindent set indentnumber=N'" + hdindentId.Value + "' where indentid=N'" + hdIndentIdR.Value + "'");

                    //    }
                    //    if (cmdsave.Value != "Update")
                    //    {
                    //        this.HWhichFill.Value = (object)null;
                    //        // Me.hdPublisherId.Value = Nothing
                    //    }
                    //    // **********************
                    //    if (cmdsave.Value == System.Resources.ValidationResources.bSave.ToString & flag == true)
                    //    {
                    //        int i = objDAL.queSingleColUnderTrans(tran, "select currentposition from departmentmaster where departmentcode='" + cmbdept.SelectedValue + "'");

                    //        objDAL.queInsUpdUnderTrans(tran, "update departmentmaster set currentposition=" + (i + 1) + " where departmentcode='" + cmbdept.SelectedValue + "'");
                    //    }
                    //    // Now inserting the data for audit trail purpose
                    //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Application["Audit"], "y", false)))
                    //    {
                    //        // indentcom.Parameters.Clear()
                    //        string[] spParameters1 = new string[] { "@UserID_1", "@OperationDate_2", "@OperationTime_3", "@IndentNo_4", "@IndentDate_5", "@ExchangeRate_6", "@NoofCopies_7", "@price_8", "@amount_9", "@operation_10", "@AffectedObjects_11" };
                    //        object[] spDataType1 = new object[] { OleDbType.VarWChar, OleDbType.Date, OleDbType.Date, OleDbType.VarWChar, OleDbType.Date, OleDbType.Decimal, OleDbType.Integer, OleDbType.Decimal, OleDbType.Decimal, OleDbType.VarWChar };

                    //        if (cmdsave.Value == System.Resources.ValidationResources.bSave.ToString)
                    //        {
                    //            string[] spPrmtValue1 = new string[] { Session["UserName"], DateTime.Now.ToShortDateString(), DateTime.Now.ToString("hh:mm:ss tt"), Strings.Trim(txtIndentNumber.Value), t, Strings.Trim(Val(txtExchangeRate.Value)), Strings.Trim(Val(txtnoofcopies.Value)), Strings.Trim(Val(txtprice.Value)), Strings.Trim(Val(txtTotalAmount.Value)), Session["user_id"], "Budget" };
                    //            objDAL.spUnderTrans2(tran, "insert_IndentAudit_1", spParameters1, spPrmtValue1, spDataType1, "q");
                    //        }
                    //        // Dim retVal1 As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_IndentAudit_1", spParameters1, spPrmtValue1)
                    //        // indentcom.Parameters("@operation_10").Value = "Insert"
                    //        else
                    //        {
                    //            string[] spPrmtValue1 = new string[] { Session["UserName"], DateTime.Now.ToShortDateString(), DateTime.Now.ToString("hh:mm:ss tt"), Strings.Trim(txtIndentNumber.Value), t, Strings.Trim(Val(txtExchangeRate.Value)), Strings.Trim(Val(txtnoofcopies.Value)), Strings.Trim(Val(txtprice.Value)), Strings.Trim(Val(txtTotalAmount.Value)), Session["user_id"], "Budget" };
                    //            objDAL.spUnderTrans2(tran, "insert_IndentAudit_1", spParameters1, spPrmtValue1, spDataType1, "");
                    //            // Dim retVal1 As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_IndentAudit_1", spParameters1, spPrmtValue1)
                    //            // indentcom.Parameters("@operation_10").Value = Resources.ValidationResources.bUpdate.ToString
                    //        }
                    //        // indentcom.Parameters.Add(New OleDbParameter("@AffectedObjects_11", OleDbType.VarWChar))
                    //        // indentcom.Parameters("@AffectedObjects_11").Value = "Budget"
                    //        // indentcom.ExecuteNonQuery()
                    //    }
                    //    // indentcom.Parameters.Clear()
                    //    // indentcom.CommandType = CommandType.Text

                    //    if (chkbdgt == "Y")   // Abhishek 27-sep-2007
                    //    {
                    //        if (chkapproval.Checked == true)
                    //        {
                    //            if (hdTemp.Value == "A" | cmdsave.Value() == System.Resources.ValidationResources.bSave.ToString)
                    //            {
                    //                // indentcom.CommandText = "update budgetmaster set approvalindentinhandamt=approvalindentinhandamt+" & Val(txtTotalAmount.Value) - Val(Session("IndentValue")) & " where departmentcode=" & Val(cmbdept.SelectedItem.Value) & " and curr_session=N'" & Session("session") & "'"
                    //                // indentcom.ExecuteNonQuery()
                    //                objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set approvalindentinhandamt=approvalindentinhandamt+" + (Val(txtTotalAmount.Value) - Conversion.Val(Session["IndentValue"])) + " where departmentcode=" + Val(cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'");
                    //            }
                    //            else if (hdTemp.Value == "N")
                    //            {
                    //                objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set nonapprovalindentinhandamt=nonapprovalindentinhandamt-" + Conversion.Val(Session["IndentValue"]) + " where departmentcode=" + Val(cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'");
                    //                objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set approvalindentinhandamt=approvalindentinhandamt+" + Val(txtTotalAmount.Value) + " where departmentcode=" + Val(cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'");
                    //                // indentcom.CommandText = "update budgetmaster set nonapprovalindentinhandamt=nonapprovalindentinhandamt-" & Val(Session("IndentValue")) & " where departmentcode=" & Val(cmbdept.SelectedItem.Value) & " and curr_session=N'" & Session("session") & "'"
                    //                // indentcom.ExecuteNonQuery()
                    //                // indentcom.CommandText = "update budgetmaster set approvalindentinhandamt=approvalindentinhandamt+" & Val(txtTotalAmount.Value) & " where departmentcode=" & Val(cmbdept.SelectedItem.Value) & " and curr_session=N'" & Session("session") & "'"
                    //                // indentcom.ExecuteNonQuery()
                    //            }
                    //        }
                    //        else if (hdTemp.Value == "N" | cmdsave.Value() == System.Resources.ValidationResources.bSave.ToString)
                    //        {
                    //            // indentcom.CommandText = "update budgetmaster set nonapprovalindentinhandamt=nonapprovalindentinhandamt+" & Val(txtTotalAmount.Value) - Val(Session("IndentValue")) & " where departmentcode=" & Val(cmbdept.SelectedItem.Value) & " and curr_session=N'" & Session("session") & "'"
                    //            // indentcom.ExecuteNonQuery()
                    //            objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set nonapprovalindentinhandamt=nonapprovalindentinhandamt+" + (Val(txtTotalAmount.Value) - Conversion.Val(Session["IndentValue"])) + " where departmentcode=" + Val(cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'");
                    //        }
                    //        else if (hdTemp.Value == "A")
                    //        {
                    //            objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set approvalindentinhandamt=approvalindentinhandamt-" + Conversion.Val(Session["IndentValue"]) + " where departmentcode=" + Val(cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'");
                    //            objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set nonapprovalindentinhandamt=nonapprovalindentinhandamt+" + Val(txtTotalAmount.Value) + " where departmentcode=" + Val(cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session["session"] + "'");
                    //            // indentcom.CommandText = "update budgetmaster set approvalindentinhandamt=approvalindentinhandamt-" & Val(Session("IndentValue")) & " where departmentcode=" & Val(cmbdept.SelectedItem.Value) & " and curr_session=N'" & Session("session") & "'"
                    //            // indentcom.ExecuteNonQuery()
                    //            // indentcom.CommandText = "update budgetmaster set nonapprovalindentinhandamt=nonapprovalindentinhandamt+" & Val(txtTotalAmount.Value) & " where departmentcode=" & Val(cmbdept.SelectedItem.Value) & " and curr_session=N'" & Session("session") & "'"
                    //            // indentcom.ExecuteNonQuery()
                    //        }
                    //    }
                    //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Application["Audit"], "Y", false)))
                    //    {
                    //        string[] spParameters1 = new string[] { "@loginname_1", "@logindate_2", "@logintime_3", "@tablename_4", "@useraction_5", "@id_6", "@sessionyr_7", "@financialValue_8" };
                    //        object[] spDataType1 = new object[] { OleDbType.VarWChar, OleDbType.Date, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.Double };
                    //        if (cmdsave.Value() == System.Resources.ValidationResources.bSave.ToString)
                    //        {
                    //            AuditIndentMasterReport(tran, "Insert"); // Procedure added by parvez(this line will be added)
                    //                                                     // LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(txtIndentNumber.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                    //                                                     // LibObj1.insertFinancialFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(txtIndentNumber.Value), "Insert", Trim(txtTotalAmount.Value), retConStr(Session("LibWiseDBConn")))

                    //            string[] spPrmtValue1 = new string[] { Session["UserName"], DateTime.Now.Date, DateTime.Now.ToString("T"), lblTitle.Text, "Insert", Strings.Trim(txtIndentNumber.Value), Session["session"], Strings.Trim(txtTotalAmount.Value) };
                    //            objDAL.spUnderTrans2(tran, "insert_LoginMaster_2", spParameters1, spPrmtValue1, spDataType1, "");
                    //        }
                    //        // Dim retVal As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_LoginMaster_2", spParameters1, spPrmtValue1)
                    //        else
                    //        {
                    //            AuditIndentMasterReport(tran, "Update"); // procedure added by parvez (this line will be added)
                    //                                                     // 'LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(txtIndentNumber.Value), "Update", retConStr(Session("LibWiseDBConn")))
                    //                                                     // LibObj1.insertFinancialFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(txtIndentNumber.Value), "Update", Trim(txtTotalAmount.Value), retConStr(Session("LibWiseDBConn")))
                    //            string[] spPrmtValue1 = new string[] { Session["UserName"], DateTime.Now.Date, DateTime.Now.ToString("T"), lblTitle.Text, "Update", Strings.Trim(txtIndentNumber.Value), Session["session"], Strings.Trim(txtTotalAmount.Value) };
                    //            objDAL.spUnderTrans2(tran, "insert_LoginMaster_2", spParameters1, spPrmtValue1, spDataType1, "");
                    //            // Dim retVal As Integer = objBLL.addUpdateTrans(indentcom, indentcon, "insert_LoginMaster_2", spParameters1, spPrmtValue1)
                    //            // AuditIndentMasterReport(delcom, "Delete", delcon) 'Procedure added by parvez(this line will be added)
                    //        }
                    //    }

                    //    if ((hdOnlineP.Value) == "Y" & cmdsave.Value == System.Resources.ValidationResources.bSave.ToString)
                    //    {
                    //        var cmd = new OleDbCommand("update indentmaster set OnlinePStatus=1 where indentnumber='" + Strings.Trim(txtIndentNumber.Value) + "'", indentcon, tran);
                    //        cmd.ExecuteNonQuery();
                    //        cmd.Dispose();
                    //    }
                    //    tran.Commit();
                    //    if ((hdOnlineP.Value) == "Y")
                    //    {
                    //        // cmdVerifiedBy.Disabled = False
                    //        var cmdxyz = new OleDbCommand("SELECT actionLPerm.permission FROM userdetails INNER JOIN actionLPerm ON userdetails.usertype = actionLPerm.userTypeId WHERE userdetails.memberid = '" + Strings.Trim(hdValidUserId.Value) + "' and actionLPerm.actionid='7' and actionLPerm.submenu_id='3001'", indentcon);
                    //        if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmdxyz.ExecuteScalar(), "Y", false)))
                    //        {
                    //            cmdVerifiedBy.Disabled = false;
                    //        }
                    //        else
                    //        {
                    //            cmdVerifiedBy.Disabled = true;
                    //        }
                    //        hdIndentStage.Value = "2";
                    //        indentMail("IndentedBy", indentcon);
                    //    }
                    //    hdTemp.Value = string.Empty;
                    //    if (tmpcondition == "Y")
                    //    {
                    //        this.cmddelete.Disabled = false;
                    //        this.cmdPrint1.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        this.cmddelete.Disabled = true;
                    //        this.cmdPrint1.Enabled = false;
                    //    }
                    //    if (cmdsave.Value == System.Resources.ValidationResources.bUpdate.ToString)
                    //    {
                    //        cmdsave.Value = System.Resources.ValidationResources.bUpdate.ToString;
                    //        cmdsave.Disabled = true;
                    //    }
                    //    else
                    //    {
                    //        cmdsave.Value = System.Resources.ValidationResources.bSave.ToString;
                    //        cmdsave.Disabled = true;
                    //    }
                    //    // indentcom.Dispose()
                    //    indentcon.Close();
                    //    indentcon.Dispose();
                    //    if ((hdOnlineP.Value) == "Y")
                    //    {   
                    //        if (askConfirm == "0")
                    //        {
                    //            // LibObj.MsgBox(Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //            message.PageMesg(System.Resources.ValidationResources.rSaveIndNo.ToString, this, DBUTIL.dbUtilities.MsgLevel.Success);
                    //        }
                    //        else
                    //        {
                    //            // LibObj.MsgBox(Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //            message.PageMesg(System.Resources.ValidationResources.rSaveIndNo.ToString, this, DBUTIL.dbUtilities.MsgLevel.Success);
                    //        }
                    //    }
                    //    else if (askConfirm == "0")
                    //    {
                    //        // LibObj.MsgBox(Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //        message.PageMesg(System.Resources.ValidationResources.rSaveIndNo.ToString, this, DBUTIL.dbUtilities.MsgLevel.Success);
                    //    }
                    //    else
                    //    {
                    //        // LibObj.MsgBox(Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rSaveIndNo.ToString + "" + txtchangeval.Value, Me)
                    //        message.PageMesg(System.Resources.ValidationResources.rSaveIndNo.ToString, this, DBUTIL.dbUtilities.MsgLevel.Success);
                    //    }
                    //    askConfirm = "0";
                    //    strlnkContinue = "0";

                    //    // txtindenttime.Value = Format$(Now, "hh:mm:ss")
                    //    txtCmbVendor.Enabled = false;
                    //    cmbdept.Enabled = false;
                    //    txtindentdate.Visible = false;
                    //    // Me.btnDate.Disabled = True

                    //    chkapproval.Disabled = true;
                    //    txtnoofcopies.Disabled = true;
                    //    txtprice.Disabled = true;
                    //    cmbcurr.Enabled = false;
                    //    this.hdBefSave.Value = (object)null;
                    //    this.hdIndentIdR.Value = (object)null;

                    //    hideTempLabels();
                    //}

                    //catch (Exception ex)
                    //{
                    //    try
                    //    {
                    //        tran.Rollback();
                    //        hdTemp.Value = string.Empty;
                    //        // msglabel.Visible = True
                    //        // msglabel.Text = ex.Message
                    //        // LibObj.MsgBox1(Resources.ValidationResources.ISUnable.ToString, Me)
                    //        // LibObj.MsgBox(Resources.ValidationResources.ISUnable.ToString, Me)
                    //        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ISUnable.ToString, Me)
                    //        message.PageMesg(System.Resources.ValidationResources.ISUnable.ToString, this, DBUTIL.dbUtilities.MsgLevel.Failure);
                    //    }
                    //    catch (Exception ex1)
                    //    {
                    //        // msglabel.Visible = True
                    //        // msglabel.Text = ex1.Message
                    //        // LibObj.MsgBox1(Resources.ValidationResources.ISUnable.ToString, Me)
                    //        // LibObj.MsgBox(Resources.ValidationResources.ISUnable.ToString, Me)
                    //        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.ISUnable.ToString, Me)
                    //        message.PageMesg(System.Resources.ValidationResources.ISUnable.ToString, this, DBUTIL.dbUtilities.MsgLevel.Failure);
                    //        hdTemp.Value = string.Empty;
                //    }
                //}
            }
            catch (Exception exMain)
            {
                // msglabel.Visible = True
                // msglabel.Text = exMain.Message
                hdTemp.Value = string.Empty;
                // LibObj.MsgBox1(Resources.ValidationResources.ISUnable.ToString, Me)
                message.PageMesg(Resources.ValidationResources.ISUnable.ToString(), this, dbUtilities.MsgLevel.Failure);

                
            }
            Session["IndentValue"] = 0;
        }

        protected async void cmddelete1_Click(object sender, EventArgs e)
        {
            
            OleDbConnection delcon = null;
            try
            {

                hdTop.Value = "top";
                hdTemp.Value = string.Empty;
                //delcon = objDAL.retConnection;
                //delcon.Open();
                var chkindent = await sav.getIndentById("Nath-2020-21-2");
                var chkindentno = await sav.getOrderByIndentId("3");
                if (string.IsNullOrEmpty(txtIndentNumber.Value))
                {

                    message.PageMesg(Resources.ValidationResources.rDelSpecify, this, dbUtilities.MsgLevel.Warning);
                }

                //objBLL.checkChildExistancewc("indentnumber", "indentmaster", "indentnumber=N'" + this.txtIndentNumber.Value + "'") == false

                else if (!chkindent.isSuccess)
                {

                    message.PageMesg(Resources.ValidationResources.rDelNotExist, this, dbUtilities.MsgLevel.Warning);
                }
                //objBLL.checkChildExistancewc("indentnumber", "ordermaster", "indentnumber=N'" + this.txtIndentNumber.Value + "'") == true
                else if (!chkindentno.isSuccess)
                {

                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                }
                else
                {

                    OleDbTransaction tran;
                    tran = delcon.BeginTransaction();

                    try
                    {
                        //objDAL.queInsUpdUnderTrans(tran, "delete from indentmaster where indentId=N'" + (this.hdindentId.Value) + "'");

                        //DeleteIndentCmd delint = new DeleteIndentCmd();
                        //delint.indentid = hdindentId.Value;
                        //var chkIndentId = await sav.DelIndentItem(delint);


                        //DeptSessionCmd upindentbudget = new DeptSessionCmd();
                        //upindentbudget.DeptCode = Convert.ToInt32(cmbdept.SelectedItem.Value);
                        //upindentbudget.Session = LoggedUser.Logged().Session;
                        //upindentbudget.TotAmt = Convert.ToInt32(txtTotalAmount.Value);



                        //if (chkapproval.Checked == true)
                        //{
                        //    //objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set        approvalindentinhandamt=approvalindentinhandamt-" + Val            (txtTotalAmount.Value) + " where departmentcode=" + Val           (cmbdept.SelectedItem.Value) + " and curr_session=N'" +            Session["session"] + "'");
                        //    upindentbudget.Appr = true;
                        //    var apprvindent = await sav.UpdateIndentBudgetAddAmt
                        //    (upindentbudget);
                        //}

                        //else
                        //{
                        //    upindentbudget.Appr = false;
                        //    var apprvindent = await sav.UpdateIndentBudgetAddAmt
                        //    (upindentbudget);



                        //    //objDAL.queInsUpdUnderTrans(tran, "update budgetmaster set        nonapprovalindentinhandamt=nonapprovalindentinhandamt-" + Val    (txtTotalAmount.Value) + " where departmentcode=" + Val             (cmbdept.SelectedItem.Value) + " and curr_session=N'" + Session     ["session"] + "'");

                        //}

                        //objDAL.queInsUpdUnderTrans(tran, "update indentmaster set itemno=itemno-1 where indentnumber=N'" + txtIndentNumber.Value + "' and itemno>N'" + Val(hditmeId.Value) + "'");
                        IndentmasterMod upitem = new IndentmasterMod();

                        upitem.indentnumber = hditmeId.Value;

                        var chkItemno = await sav.InsUpdIndent(upitem);

                        //if (LoggedUser.Logged().IsAudit == "Y")
                        //{

                        //    // '*******************take array to store stored procedures parameters adn value

                        //    string[] spParameters = new string[] { "@UserID_1", "@OperationDate_2", "@OperationTime_3", "@IndentNo_4", "@IndentDate_5", "@ExchangeRate_6", "@NoofCopies_7", "@price_8", "@amount_9", "@operation_10", "@AffectedObjects_11" };
                        //    string[] spPrmtValue = new string[] { LoggedUser.Logged().UserName, DateTime.Now.ToShortDateString(), DateTime.Now.ToString("hh:mm:ss tt"), (txtIndentNumber.Value), (txtindentdate.Text), (txtExchangeRate.Value), (txtnoofcopies.Value), (txtprice.Value), (txtTotalAmount.Value), "Delete", "Budget" };
                        //    object[] spDataType = new object[] { OleDbType.VarWChar, OleDbType.Date, OleDbType.Date, OleDbType.VarWChar, OleDbType.Date, OleDbType.Decimal, OleDbType.Integer, OleDbType.Decimal, OleDbType.Decimal, OleDbType.VarWChar };

                        //    //objDAL.spUnderTrans2(tran, "insert_IndentAudit_1", spParameters, spPrmtValue, spDataType, "");

                        //}

                        //if (LoggedUser.Logged().IsAudit == "Y")
                        //{
                        //    // '*******************take array to store stored procedures parameters adn value
                        //    string[] spParameters = new string[] { "@loginname_1", "@logindate_2", "@logintime_3", "@tablename_4", "@useraction_5", "@id_6", "@sessionyr_7", "@financialValue_8" };
                        //    string[] spPrmtValue = new string[] { LoggedUser.Logged().UserName, DateTime.Now.Date.ToString(), DateTime.Now.ToString("T"), lblTitle.Text, "Delete", (txtIndentNumber.Value), LoggedUser.Logged().Session, (txtTotalAmount.Value) };
                        //    object[] spDataType = new object[] { OleDbType.VarWChar, OleDbType.Date, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.VarWChar, OleDbType.Double };

                        //    //objDAL.spUnderTrans2(tran, "insert_LoginMaster_2", spParameters, spPrmtValue, spDataType, "");

                        //    /*AuditIndentMasterReport(tran, "Delete");*/ // Procedure added by parvez(this line will be added)
                        //}


                        message.PageMesg(Resources.ValidationResources.rDel, this, dbUtilities.MsgLevel.Success);

                        this.hReq.Value = this.HComboSelect.Value;


                        //tran.Commit();
                        this.cmdsave.Disabled = false;
                        this.cmddelete.Disabled = true;
                        this.cmdPrint1.Enabled = false;
                    }

                    catch (Exception ex1)
                    {
                        try
                        {
                            //tran.Rollback();

                            message.PageMesg(Resources.ValidationResources.IDUnable, this, dbUtilities.MsgLevel.Failure);
                        }
                        catch (Exception ex2)
                        {
                            message.PageMesg(Resources.ValidationResources.IDUnable, this, dbUtilities.MsgLevel.Failure);
                        }
                    }
                    
                                        // to clear fields
                    // Call Reset_button_Click(sender, e)
                    hdTop.Value = "top";
                    //refreshFileds(delcon);
                    cmdsave.Value = Resources.ValidationResources.bSave.ToString();
                    //default_setting(delcon);
                    delcon.Close();
                    delcon.Dispose();
                }
            }


            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // LibObj.MsgBox1(Resources.ValidationResources.IDUnable.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.IDUnable.ToString, Me)
                message.PageMesg(Resources.ValidationResources.IDUnable.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {

                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
                // 'shweta
                // Dim returnScript As String = ""
                // returnScript &= "<script language='javascript' type='text/javascript'>"
                // returnScript &= "javascript:SearchVis('chkSearch1','optIndent1','optAdvance1','lbloptIndent1','lbloptAdvance1');"
                // returnScript &= "<" & "/" & "script>"
                // Page.RegisterStartupScript("", returnScript)
                //if (delcon.State == ConnectionState.Open)
                //{
                //    delcon.Close();
                //}
            }
                Session["IndentValue"] = 0;
        }

        protected async void cmbdept_SelectedIndexChanged(object sender, EventArgs e)
        {
            var conn = new OleDbConnection(retConstr(""));
            try
            {
                var chkds = new DataSet(); // = objDAL.rtnDataSet("Select CircUserManagement.userid,firstname+' '+middlename+' '+lastname as firstname from CircUserManagement,circclassmaster where CircUserManagement.departmentcode=" & Me.cmbdept.SelectedValue & " and  CircUserManagement.classname=circclassmaster.classname and circclassmaster.canRequest=N'Y' order by firstname+' '+middlename+' '+lastname", conn)
                int i = 0;
                var ds = new DataSet();
                conn.Open();
                if (cmbdept.SelectedValue == HComboSelect.Value)
                {
                    cmbreq.Items.Clear();
                    cmbreq.Items.Add("HOD");
                    cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                }
                else
                {
                    //UserByCondCmd deptreq = new UserByCondCmd();
                    //deptreq.CanRequest = true;
                    //deptreq.DeptCode = depCode;
                    //var chkreq = await sav.getCircUserByDeptCanReq(deptreq);

                    var da = new OleDbDataAdapter("Select CircUserManagement.userid,firstname+' '+middlename+' '+lastname as firstname from CircUserManagement,circclassmaster where CircUserManagement.departmentcode=" + this.cmbdept.SelectedValue + " and  CircUserManagement.classname=circclassmaster.classname and circclassmaster.canRequest=N'Y' order by firstname+' '+middlename+' '+lastname", conn);
                    da.Fill(chkds);
                    if (chkds.Tables[0].Rows.Count > 0)
                    {
                        this.cmbreq.DataSource = chkds.Tables[0];
                        cmbreq.DataTextField = "firstname";
                        cmbreq.DataValueField = "userid";
                        cmbreq.DataBind();
                    }
                    else
                    {
                        cmbreq.Items.Clear();
                    }
                    cmbreq.Items.Add("HOD");
                    cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                    this.cmbreq.DataSource = chkds.Tables[0];
                }
                // SetFocus(btnDate)
                chkds.Dispose();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
        }
        public void GetData2(OleDbConnection Conn)
        {
            string Str = null;
            // Dim lstItem As System.Web.UI.WebControls.ListItem = New System.Web.UI.WebControls.ListItem
            if (optAdvance1.Checked == true)
            {
                if (txtCategory.Value != string.Empty)
                {
                    if (ddl1.SelectedValue == "Title")
                    {
                        Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster where  (title LIKE N'%" + txtCategory.Value + "%')) order by indentnumber +'/' + ItemNo";
                    }
                    else if (ddl1.SelectedValue == "Author")
                    {
                        Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster where  (firstname1+' '+middlename1+' '+lastname1+'/'+firstname2+' '+middlename2+' '+lastname2+'/'+firstname3+' '+middlename3+' '+lastname3 LIKE N'%" + txtCategory.Value + "%')) order by indentnumber +'/' + ItemNo"; // ((order_check_code='0' or issatnding=N'y') and
                    }
                    else if (ddl1.SelectedValue == "Publisher")
                    {
                        Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster,publishermaster where  (firstname LIKE N'%" + txtCategory.Value + "%') and (publishermaster.publisherid=indentmaster.publisherid)) order by indentnumber +'/' + ItemNo"; // ((order_check_code='0' or issatnding=N'y') and
                    }
                    else if (ddl1.SelectedValue == "Vendor")
                    {
                        Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster,vendormaster where  (vendorname LIKE N'%" + txtCategory.Value + "%') and (vendormaster.vendorid=indentmaster.vendorid)) order by indentnumber +'/' + ItemNo"; // ((order_check_code='0'or issatnding=N'y') and
                    }
                    else if (ddl1.SelectedValue == "Series")
                    {
                        Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster where  seriesname LIKE N'%" + txtCategory.Value + "%')  order by indentnumber +'/' + ItemNo"; // ((order_check_code='0' or issatnding=N'y')  and
                    }
                    else if (ddl1.SelectedValue == "ISBN")
                    {
                        Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster where  isbn LIKE N'" + txtCategory.Value + "%')  order by indentnumber +'/' + ItemNo"; // ((order_check_code='0' or issatnding=N'y') and
                    }
                }
                else
                {
                }
            }
            else if (optIndent1.Checked == true)
            {
                if (txtCategory.Value != string.Empty)
                {
                    Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster where  (indentnumber +'/' + ItemNo LIKE N'%" + txtCategory.Value + "%')) order by indentnumber +'/' + ItemNo"; // ((order_check_code='0' or issatnding=N'y') and
                }
                else
                {   
                Str = "select indentnumber +'/' + ItemNo as indentnumber,indentid from indentmaster  order by indentnumber +'/' + ItemNo";
                } // (order_check_code='0' or issatnding=N'y')
            }
            //objDAL.populateGetData2(lstAllCategory, Str, optAdvance1.Checked, Strings.Trim(txtCategory.Value), "indentnumber", "indentid", Conn);

        }


        protected void cmdIndentedBy_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "Validate('IndentedBy');", true);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdCheckedBy_ServerClick(object sender, EventArgs e)
        {
            try
            {
                // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "Validate('CheckedBy');", True)
                string returnScript = "";
                returnScript += "<script language='javascript' type='text/javascript'>";
                returnScript += "javascript:Validate('CheckedBy');";
                returnScript += "<" + "/" + "script>";
                Page.RegisterStartupScript("", returnScript);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdVerifiedBy_ServerClick(object sender, EventArgs e)
        {
            try
            {
                
                string returnScript = "";
                returnScript += "<script language='javascript' type='text/javascript'>";
                returnScript += "javascript:Validate('VerifiedBy');";
                returnScript += "<" + "/" + "script>";
                Page.RegisterStartupScript("", returnScript);
            }
            catch (Exception ex)
            {
              
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdPassedBy_ServerClick(object sender, EventArgs e)
        {
            try
            {
               
                string returnScript = "";
                returnScript += "<script language='javascript' type='text/javascript'>";
                returnScript += "javascript:Validate('PassedBy');";
                returnScript += "<" + "/" + "script>";
                Page.RegisterStartupScript("", returnScript);
            }
            catch (Exception ex)
            {
               
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void hdCmdSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbdept.SelectedItem.Text == "---Select---")
                {
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Select department", Me)
                    message.PageMesg("Select department", this,  dbUtilities.MsgLevel.Warning);
                    return;

                }

                var conn = new OleDbConnection(retConstr(""));
                var cmd = new OleDbCommand();
                var tran = default(OleDbTransaction);
                try
                {
                    var sqlStr = default(string);
                    var saveMsg = default(string);
                    conn.Open();
                    if (hdIndentStage.Value == "1")
                    {
                    }

                    else if (hdIndentStage.Value == "2")
                    {
                        if (actionPermission("SELECT actionLPerm.permission FROM userdetails INNER JOIN actionLPerm ON userdetails.usertype = actionLPerm.userTypeId WHERE userdetails.memberid = '" +  (hdValidUserId.Value) + "' and actionLPerm.actionid='7' and actionLPerm.submenu_id='3001'") == false)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.NotAuth, this,  dbUtilities.MsgLevel.Warning);
                            return;
                        }
                        sqlStr = "update indentmaster set OnlinePStatus=2 where indentnumber='" + (txtIndentNumber.Value) + "'";
                        saveMsg = "Indent has been Verified";
                    }
                    else if (hdIndentStage.Value == "3")
                    {
                        if (actionPermission("SELECT actionLPerm.permission FROM userdetails INNER JOIN actionLPerm ON userdetails.usertype = actionLPerm.userTypeId WHERE userdetails.memberid = '" +  (hdValidUserId.Value) + "' and actionLPerm.actionid='8' and actionLPerm.submenu_id='3001'") == false)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.NotAuth, this,  dbUtilities.MsgLevel.Warning);
                            return;
                        }
                        sqlStr = "update indentmaster set OnlinePStatus=3 where indentnumber='" + (txtIndentNumber.Value) + "'";
                        saveMsg = "Indent has been Checked";
                    }
                    else if (hdIndentStage.Value == "4")
                    { 
                        if (actionPermission("SELECT actionLPerm.permission FROM userdetails INNER JOIN actionLPerm ON userdetails.usertype = actionLPerm.userTypeId WHERE userdetails.memberid = '" +  (hdValidUserId.Value) + "' and actionLPerm.actionid='9' and actionLPerm.submenu_id='3001'") == false)
                        {
                           
                            message.PageMesg(Resources.ValidationResources.NotAuth, this,  dbUtilities.MsgLevel.Warning);
                            return;
                        }
                        sqlStr = "update indentmaster set OnlinePStatus=4 where indentnumber='" + (txtIndentNumber.Value) + "'";
                        saveMsg = "Indent has been Passed";
                    }
                    tran = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlStr;
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    LibObj.MsgBox(saveMsg, this);
                    if (hdIndentStage.Value == "1")
                    {
                    }

                    else if (hdIndentStage.Value == "2")
                    {
                        //indentMail("VerifiedBy", conn);
                    }
                    else if (hdIndentStage.Value == "3")
                    {
                        //indentMail("CheckedBy", conn);
                    }
                    else if (hdIndentStage.Value == "4")
                    {
                       // indentMail("PassedBy", conn);
                    }
                    hdIndentStage.Value = Convert.ToString(hdIndentStage.Value) + 1;
                   // checkPermission(conn);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    // msglabel.Visible = True
                    // msglabel.Text = ex.Message
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }
                finally
                {

                    tran.Dispose();
                    cmd.Dispose();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
               
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
        }

        public void indentMail(string process, OleDbConnection con)
        {
            try
            {
                var messagen = new dbUtilities();
                var fromEmail = default(string);
                var smtpAdd = default(string);
                string uid = null;
                string pwd = null;
                var studentsDS = new DataSet();
                var smtpDa = new OleDbDataAdapter("select email,smptp_IPadd,iuser ,ipwd from librarysetupinformation", con);
                smtpDa.Fill(studentsDS, "Settings");
                if (studentsDS.Tables["Settings"].Rows.Count > 0)
                {
                    fromEmail = Convert.ToString(studentsDS.Tables["Settings"].Rows[0]["email"]);
                    smtpAdd = Convert.ToString(studentsDS.Tables["Settings"].Rows[0]["smptp_IPadd"]);
                    uid = Convert.ToString(studentsDS.Tables["Settings"].Rows[0]["iuser"]);
                    pwd = Convert.ToString(studentsDS.Tables["Settings"].Rows[0]["ipwd"]);
                }

                string strMail = string.Empty;
                string message = string.Empty;
                string mailSubject = string.Empty;
                if (process == "IndentedBy")
                {
                    strMail = "select circusermanagement.email1, circusermanagement.email2, circusermanagement.userid from circusermanagement inner join userdetails on userdetails.memberid = circusermanagement.userid inner join actionlperm on userdetails.usertype = actionlperm.usertypeid where actionlperm.permission = 'Y' and actionlperm.submenu_id = '3001' and actionlperm.actionid = '7'";
                    message = "Dear Member," + "<br/>" + " Indent details are as follows -" + "<br/>" + "Indent Number" + " " + this.txtIndentNumber.Value + "" + "<br/>" + " Indent has been Prepared" + "<br/>" + " Remarks - " + (txtRemarks.Value) + "<br/>" + " Thanking you "; // & "<br/>" & " Librarian"
                    mailSubject = "Indent Prepared";
                }
                else if (process == "VerifiedBy")
                {
                    strMail = "select circusermanagement.email1, circusermanagement.email2, circusermanagement.userid from circusermanagement inner join userdetails on userdetails.memberid = circusermanagement.userid inner join actionlperm on userdetails.usertype = actionlperm.usertypeid where actionlperm.permission = 'Y' and actionlperm.submenu_id = '3001' and actionlperm.actionid = '8'";
                    message = "Dear Member," + "<br/>" + " Indent details are as follows -" + "<br/>" + "Indent Number" + " " + this.txtIndentNumber.Value + "" + "<br/>" + " Indent has been Verified" + "<br/>" + " Remarks - " + (txtRemarks.Value) + "<br/>" + " Thanking you "; // & "<br/>" & " Librarian"
                    mailSubject = "Indent Verified";
                }
                else if (process == "CheckedBy")
                {
                    strMail = "select circusermanagement.email1, circusermanagement.email2, circusermanagement.userid from circusermanagement inner join userdetails on userdetails.memberid = circusermanagement.userid inner join actionlperm on userdetails.usertype = actionlperm.usertypeid where actionlperm.permission = 'Y' and actionlperm.submenu_id = '3001' and actionlperm.actionid = '9'";
                    message = "Dear Member," + "<br/>" + " Indent details are as follows -" + "<br/>" + "Indent Number" + " " + this.txtIndentNumber.Value + "" + "<br/>" + " Indent has been Checked" + "<br/>" + " Remarks - " + (txtRemarks.Value) + "<br/>" + " Thanking you "; // & "<br/>" & " Librarian"
                    mailSubject = "Indent Checked";
                }
                else if (process == "PassedBy")
                {
                    strMail = "select distinct circusermanagement.email1, circusermanagement.email2, circusermanagement.userid from circusermanagement inner join userdetails on userdetails.memberid = circusermanagement.userid inner join actionlperm on userdetails.usertype = actionlperm.usertypeid where actionlperm.permission = 'Y' and actionlperm.submenu_id = '3001' and actionlperm.actionid in ('7', '8', '9')";
                    message = "Dear Member," + "<br/>" + " Indent details are as follows -" + "<br/>" + "Indent Number" + " " + this.txtIndentNumber.Value + "" + "<br/>" + " Indent has been Passed" + "<br/>" + " Remarks - " +(txtRemarks.Value) + "<br/>" + " Thanking you "; // & "<br/>" & " Librarian"
                    mailSubject = "Indent Passed";

                }

                var da1 = new OleDbDataAdapter(strMail, con);
                da1.Fill(studentsDS, "A");
                if (studentsDS.Tables["A"].Rows.Count == 0)
                {
                    // LibObj.MsgBox(Resources.ValidationResources.NoRecFndInSpeciGroup.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.NoRecFndInSpeciGroup.ToString, Me)
                    messagen.PageMesg(Resources.ValidationResources.NoRecFndInSpeciGroup, this,  dbUtilities.MsgLevel.Warning);

                    studentsDS.Dispose();
                    smtpDa.Dispose();
                    return;
                }
                int flagmsg = 0;
                int i = 0;
                //PrintorMail("Mail");
                string strAttachment = string.Empty;
                if (process == "PassedBy")
                {
                    strAttachment = Server.MapPath("GiftIndent.pdf");
                }
                var loopTo = studentsDS.Tables["A"].Rows.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    studentsDS.Dispose();
                    // PrintorMail(sender, e, "Mail")
                    string eMailId;
                    string eMailId1;
                    eMailId = Convert.ToString(studentsDS.Tables["A"].Rows[i][0]);
                    eMailId1 = Convert.ToString(studentsDS.Tables["A"].Rows[i][1]);
                    var sendDate = DateTime.Now;
                    //if (LibObj.SmtpServer(con) == true)
                    //{
                    //    // Libobj.SendMailLib(eMailId, fromEmail, message, "Autogenerate E-mail.", eMailId1, smtpAdd, ordercon)
                    //    if (LibObj.sendmailSmtp(eMailId, fromEmail, message, mailSubject, eMailId1, smtpAdd, uid, pwd, con, strAttachment) == true)
                    //    {
                    //        flagmsg = 1;
                    //    }
                    //    // LibObj.MsgBox(Resources.ValidationResources.msgSendMail.ToString, Me)
                    //    else if (LibObj.InsertDeleteSEND(eMailId, fromEmail, message, mailSubject, eMailId1, smtpAdd, "Y", sendDate, con) == true)
                    //    {
                    //        flagmsg = 2;
                    //        // LibObj.MsgBox1(Resources.ValidationResources.MailNotSentDueSerCF.ToString, Me)
                    //    }
                    //}
                    //else if (LibObj.InsertDeleteSEND(eMailId, fromEmail, message, mailSubject, eMailId1, smtpAdd, "Y", sendDate, con) == true)
                    //{
                    //    flagmsg = 2;
                    //    // LibObj.MsgBox1(Resources.ValidationResources.MailNotSentDueSerCF.ToString, Me)
                    //}
                    studentsDS.Dispose();
                    smtpDa.Dispose();
                    studentsDS.Dispose();
                    smtpDa.Dispose();
                }
                if (flagmsg == 1)
                {
                    // LibObj.MsgBox(Resources.ValidationResources.msgSendMail.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.msgSendMail.ToString, Me)
                    messagen.PageMesg(Resources.ValidationResources.msgSendMail, this, dbUtilities.MsgLevel.Warning);
                }
                else if (flagmsg == 2)
                {
                    // LibObj.MsgBox(Resources.ValidationResources.MailNotSentDueSerCF.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MailNotSentDueSerCF.ToString, Me)
                    messagen.PageMesg(Resources.ValidationResources.MailNotSentDueSerCF, this, dbUtilities.MsgLevel.Warning);
                }
                txtRemarks.Value = string.Empty;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void optAdvance1_CheckedChanged(object sender, EventArgs e)
        {
            var conn = new OleDbConnection(retConstr(""));
            conn.Open();
            try
            {
                txtCategory.Value = string.Empty;
                this.ddl1.Visible = true;
                this.ddl1.SelectedIndex = this.ddl1.SelectedIndex - 1;
                lstAllCategory.Items.Clear();
                GetData2(conn);
                this.SetFocus(txtCategory);
            }
            catch (Exception ex)
            {
                
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
        }

        protected void optIndent1_CheckedChanged(object sender, EventArgs e)
        {
            var conn = new OleDbConnection(retConstr(""));
            conn.Open();
            try
            {
                txtCategory.Value = string.Empty;
                lstAllCategory.Items.Clear();
                this.ddl1.Visible = false;
                GetData2(conn);
                //this.SetFocus(txtCategory);
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.hd_title.Value = txttitle.Text;
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "title_search();", true);
        }

        protected void cmdSerach_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            do
            {
                try
                {
                 
                    this.chkSearch.Checked = false;
                    optIndent1.Visible = false;
                    optAdvance1.Visible = false;
                    this.ddl1.Visible = false;
                    this.txtCategory.Visible = false;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter.Visible = false;
                    this.lstAllCategory.Visible = false;
                    this.lbloptIndent1.Visible = false;
                    this.lbloptAdvance1.Visible = false;
                   
                    //conn = objDAL.retConnection;
                    conn.Open();
                    
                    if (hdnIndId.Value != string.Empty)
                    {
                       
                        txttitle.Text = txttitle.Text;
                        string ctrlNo = string.Empty;
                        ctrlNo = hdnIndId.Value; 

                        //DataSet ds = objDAL.rtnDataSet("select indentmaster.*,institutemaster.shortname+ '-' + departmentmaster.departmentname as departmentname , CircUserManagement.firstname + ' ' + CircUserManagement.middlename + ' ' + CircUserManagement.lastname as staffname  from indentmaster,CircUserManagement,departmentmaster,institutemaster  where departmentmaster.institutecode=institutemaster.institutecode and indentmaster.departmentcode=departmentmaster.departmentcode and CircUserManagement.userid =indentmaster.requestercode  and indentmaster.indentid=N'" + ctrlNo + "'", conn);
                       
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    // 'txtIndentNumber.Value = ds.Tables(0).Rows(0).Item("indentnumber")
                        //    // lblIndentNo.Visible = True
                        //    // lblIndentNo.Text = ds.Tables(0).Rows(0).Item("indentnumber")
                        //    // ***
                        //    // ***
                        //    hdindentId.Value = ds.Tables[0].Rows[0]["indentid"];
                        //    // ***
                        //    // ***

                        //    // 'txtindentdate.Text = Format(ds.Tables(0).Rows(0).Item("indentdate"), Me.hrDate.Value)
                        //    // lblIndentDate.Visible = True
                        //    // lblIndentDate.Text = Format(ds.Tables(0).Rows(0).Item("indentdate"), Me.hrDate.Value)

                        //    // 'txtindenttime.Value = Format(ds.Tables(0).Rows(0).Item("indenttime"), "hh:mm:ss")
                        //    // lblIndentTime.Visible = True
                        //    // lblIndentTime.Text = Format(ds.Tables(0).Rows(0).Item("indenttime"), "hh:mm:ss tt")

                        //    txttitle.Text = ds.Tables[0].Rows[0]["title"];
                        //    txtSeries.Value = ds.Tables[0].Rows[0]["seriesname"];
                        //    txtfname1.Value = ds.Tables[0].Rows[0]["firstname1"];
                        //    txtfname2.Value = ds.Tables[0].Rows[0]["firstname2"];
                        //    txtfname3.Value = ds.Tables[0].Rows[0]["firstname3"];
                        //    txtlname1.Value = ds.Tables[0].Rows[0]["lastname1"];
                        //    txtlname2.Value = ds.Tables[0].Rows[0]["lastname2"];
                        //    txtlname3.Value = ds.Tables[0].Rows[0]["lastname3"];
                        //    txtmname1.Value = ds.Tables[0].Rows[0]["middlename1"];
                        //    txtmname2.Value = ds.Tables[0].Rows[0]["middlename2"];
                        //    txtmname3.Value = ds.Tables[0].Rows[0]["middlename3"];
                        //    txtedition.Value = ds.Tables[0].Rows[0]["edition"];
                        //    txtyrofedition.Value = ds.Tables[0].Rows[0]["yearofedition"];
                        //    txtPubYear.Value = ds.Tables[0].Rows[0]["yearofPublication"];
                        //    txtvolno.Value = ds.Tables[0].Rows[0]["volumeno"];
                        //    txtPart.Value = ds.Tables[0].Rows[0]["Vpart"];
                        //    hditmeId.Value = ds.Tables[0].Rows[0]["ItemNo"];
                        //    txtSubtitle.Value = ds.Tables[0].Rows[0]["subtitle"];
                        //    txtisbn.Value = ds.Tables[0].Rows[0]["isbn"];
                        //    txtnoofstud.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["noofstudents"], 0, false)), string.Empty, ds.Tables[0].Rows[0]["noofstudents"]);
                        //    txtnoofcopies.Value = ds.Tables[0].Rows[0]["noofcopies"];
                        //    txtcoursenm.Value = ds.Tables[0].Rows[0]["coursenumber"];
                        //    txtExchangeRate.Value = ds.Tables[0].Rows[0]["exchangerate"];
                        //    cmbLanguage.SelectedValue = ds.Tables[0].Rows[0]["Language_Id"];
                        //    txtTotalAmount.Value = ds.Tables[0].Rows[0]["totalamount"];
                        //    txtprice.Value = ds.Tables[0].Rows[0]["price"];
                        //    // cmbdept.SelectedValue = ds.Tables(0).Rows(0).Item("departmentcode")
                        //    // lblDept.Visible = True
                        //    // lblDept.Text = ds.Tables(0).Rows(0).Item("departmentname")

                        //    // populateRequesters(ds.Tables(0).Rows(0).Item("departmentcode"))
                        //    // cmbreq.SelectedValue = ds.Tables(0).Rows(0).Item("requestercode")

                        //    // lblRequester.Visible = True
                        //    // lblRequester.Text = ds.Tables(0).Rows(0).Item("staffname")


                        //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["indenttype"], "Approval", false)))
                        //    {
                        //        chkapproval.Checked = true;
                        //    }
                        //    else
                        //    {
                        //        chkapproval.Checked = false;
                        //    }

                        //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["isSatnding"], "y", false)))
                        //    {
                        //        chkStanding.Checked = true;
                        //    }
                        //    else
                        //    {
                        //        chkStanding.Checked = false;
                        //    }
                        //    persontype.SelectedValue = ds.Tables[0].Rows[0]["authortype"];
                        //    // ***************'************
                        //    OleDbDataReader genDr = objDAL.rtnDataReader(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'", ds.Tables[0].Rows[0]["publisherid"]), "' and publishermaster.publisherid=addresstable.addid  and addrelation=N'publisher';Select vendorname+', '+percity as firstname  from  vendormaster,addresstable where vendorid=N'"), ds.Tables[0].Rows[0]["vendorid"]), "' and vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor';"), conn);
                        //    genDr.Read();
                        //    // *******************************
                        //    // Dim sqlstr As String = "Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'" & ds.Tables(0).Rows(0).Item("publisherid") & "' and publishermaster.publisherid=addresstable.addid  and addrelation=N'publisher'"
                        //    // Dim cmd As New OleDbCommand(sqlstr, tmpcon)
                        //    string tmpstr = Conversions.ToString(genDr[0]); // cmd.ExecuteScalar
                        //                                                    // Me.hdPublisherId.Value = ds.Tables(0).Rows(0).Item("publisherid")
                        //    hdPubId.Value = ds.Tables[0].Rows[0]["publisherid"];
                        //    txtCmbPublisher.Text = tmpstr; // ds.Tables(0).Rows(0).Item("publisherid")


                        //    // asmpublisher.SelectedValue = ds.Tables(0).Rows(0).Item("publisherid")
                        //    // -------------------jeetendra---------------------------
                        //    // Dim sqlstr1 As String = "Select vendorname+', '+percity as firstname  from  vendormaster,addresstable where vendorid=N'" & ds.Tables(0).Rows(0).Item("vendorid") & "' and vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor'"
                        //    // Dim cmd1 As New OleDbCommand(sqlstr1, tmpcon)
                        //    genDr.NextResult();
                        //    genDr.Read();
                        //    string tmpstr1 = Conversions.ToString(genDr[0]); // cmd1.ExecuteScalar
                        //    genDr.Close();
                        //    this.HdVendorid.Value = ds.Tables[0].Rows[0]["vendorid"];
                        //    this.Hdvenid.Value = ds.Tables[0].Rows[0]["vendorid"];
                        //    this.txtCmbVendor.Text = tmpstr1;
                        //    // asmvendor.SelectedValue = ds.Tables(0).Rows(0).Item("vendorid")
                        //    // -------------------jeetendra---------------------------
                        //    // asmvendor.SelectedValue= ds.Tables(0).Rows(0).Item("vendorid")

                        //    cmbcurr.SelectedValue = ds.Tables[0].Rows[0]["currencycode"];

                        //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["go_bank"], "B", false)))
                        //    {
                        //        cmbgocorbank.SelectedItem.Text = "Bank";
                        //    }
                        //    else
                        //    {
                        //        cmbgocorbank.SelectedItem.Text = "Goc";
                        //    }
                        //    cmbbookcategory.Value = ds.Tables[0].Rows[0]["category"];
                        //    mediatype.SelectedValue = ds.Tables[0].Rows[0]["mediatype"];
                        //    // cmdsave.Value = Resources.ValidationResources.bUpdate.ToString
                        //    Session["IndentValue"] = Val(Strings.Trim(txtTotalAmount.Value));


                        //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["indenttype"], "Approval", false)))
                        //    {
                        //        hdTemp.Value = "A";
                        //    }
                        //    else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["indenttype"], "Non-Approval", false)))
                        //    {
                        //        hdTemp.Value = "N";
                        //    }
                        //    hdTop.Value = "top";
                        //}
                        //else
                        //{
                        //    // Hidden1.Value = "8"
                        //}
                    }
                    if (hdnIndId.Value == "")
                    {
                        short ddiV = Convert.ToSByte(txttitle.Text.IndexOf(""));
                        string accNo = txttitle.Text.Substring(1, ddiV - 1);

                        txttitle.Text = txttitle.Text.Substring(ddiV + 1);
                        var getData = new FillDsTables();
                        var dtBData = new DataTable();
                        string Err, Qry;
                        Qry = "select  booktitle,firstname1,middlename1,lastname1,firstname2,middlename2,lastname2,";
                        Qry += " firstname3,middlename3,lastname3,d.Subtitle,e.SeriesName,b.language_id,b.edition,";
                        Qry += "a.editionyear,a.pubYear,b.volume,b.part,b.isbn,a.ItemCategoryCode,b.MaterialDesignation,";
                        Qry += "a.OriginalCurrency,a.bookprice,b.publishercode,a.vendor_source,vendorid,h.firstname+', '+gg.percity publ";
                        Qry += " from bookaccessionmaster a,BookCatalog b,BookAuthor c,BookConference d,";
                        Qry += " BookSeries e,vendormaster f, addresstable g,publishermaster h,addresstable gg";
                        Qry += " where a.ctrl_no=b.ctrl_no and a.ctrl_no=c.ctrl_no";
                        Qry += " and a.ctrl_no=d.ctrl_no and a.ctrl_no=e.ctrl_no";
                        Qry += " and a.vendor_source=f.vendorname+', '+g.percity";
                        Qry += " and f.vendorcode=g.addid and g.addrelation='vendor'";
                        Qry += " and b.publishercode=h.publisherid and h.publisherid=gg.addid and gg.addrelation='publisher' ";
                        // Qry &= " and a.booktitle=N'" & txttitle.Text.Replace("'", "''") & "'"
                        Qry += " and a.accessionnumber='" + accNo + "'";
                        Err = getData.FillDs(Qry,ref  dtBData);
                        if (!string.IsNullOrEmpty(Err))
                        {
                            throw new ApplicationException(Err);
                        }
                        if (dtBData.Rows.Count == 0)
                        {
                            // msglabel.Text = "Record not found, MSSPL."
                            message.PageMesg("Record not found, MSSPL.", this, dbUtilities.MsgLevel.Failure);
                            // msglabel.Visible = True
                            break;
                        }
                        txtSubtitle.Value = dtBData.Rows[0]["Subtitle"].ToString();
                        txtfname1.Value = dtBData.Rows[0]["firstname1"].ToString();
                        //txtmname1.Value = dtBData.Rows[0]["middlename1"].ToString();
                        txtlname1.Value = dtBData.Rows[0]["lastname1"].ToString();
                        txtfname2.Value = dtBData.Rows[0]["firstname2"].ToString();
                        txtmname2.Value = dtBData.Rows[0]["middlename2"].ToString();
                        txtlname2.Value = dtBData.Rows[0]["lastname2"].ToString();
                        txtfname3.Value = dtBData.Rows[0]["firstname3"].ToString();
                        txtmname3.Value = dtBData.Rows[0]["middlename3"].ToString();
                        txtlname3.Value = dtBData.Rows[0]["lastname3"].ToString();
                        cmbLanguage.SelectedValue = dtBData.Rows[0]["language_id"].ToString();
                        txtedition.Value = dtBData.Rows[0]["edition"].ToString();
                        txtyrofedition.Value = dtBData.Rows[0]["editionyear"].ToString();
                        txtPubYear.Value = dtBData.Rows[0]["pubyear"].ToString();
                        txtvolno.Value = dtBData.Rows[0]["volume"].ToString();
                        txtPart.Value = dtBData.Rows[0]["part"].ToString();
                        txtisbn.Value = dtBData.Rows[0]["isbn"].ToString();
                        
                        txtprice.Value = dtBData.Rows[0]["bookprice"].ToString();
                        txtnoofcopies.Value = "";
                        hdPubId.Value = dtBData.Rows[0]["publishercode"].ToString(); // hdPublisherid
                        txtCmbPublisher.Text = dtBData.Rows[0]["publ"].ToString();
                        // asmpublisher.SelectedValue = dtBData.Rows(0)("publishercode")
                        HdVendorid.Value = dtBData.Rows[0]["vendorid"].ToString();
                        Hdvenid.Value = dtBData.Rows[0]["vendorid"].ToString();
                        txtCmbVendor.Text = dtBData.Rows[0]["vendor_source"].ToString();
                        // asmvendor.SelectedValue = dtBData.Rows(0)("vendorid")
                        // msglabel.Text = "Information - Please select correct Categ.,Currency etc."
                        message.PageMesg("Information - Please select correct Categ.,Currency etc.", this, dbUtilities.MsgLevel.Failure);
                        // msglabel.Visible = True
                    }
                }
                catch (Exception ex)
                {
                    
                    message.PageMesg(Resources.ValidationResources.IRUnable, this, dbUtilities.MsgLevel.Failure);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            while (false);
        }

        protected void btnFilEntries_ServerClick(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            try
            {
                //conn = objDAL.retConnection;
                //conn.Open();
                //DataSet ds = objDAL.rtnDataSet("Select * from opacindent where indentid =N'" + this.hdIndentIdR.Value + "'", conn);
                
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    txttitle.Text = ds.Tables[0].Rows[0]["title"];
                //    txtSeries.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["seriesname"], null, false)), string.Empty, ds.Tables[0].Rows[0]["seriesname"]);
                //    txtfname1.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["firstname1"], null, false)), string.Empty, ds.Tables[0].Rows[0]["firstname1"]);
                //    txtfname2.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["firstname2"], null, false)), string.Empty, ds.Tables[0].Rows[0]["firstname2"]);
                //    txtfname3.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["firstname3"], null, false)), string.Empty, ds.Tables[0].Rows[0]["firstname3"]);
                //    txtlname1.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["lastname1"], null, false)), string.Empty, ds.Tables[0].Rows[0]["lastname1"]);
                //    txtlname2.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["lastname2"], null, false)), string.Empty, ds.Tables[0].Rows[0]["lastname2"]);
                //    txtlname3.Value = ds.Tables[0].Rows[0]["lastname3"];
                //    txtmname1.Value = ds.Tables[0].Rows[0]["middlename1"];
                //    txtmname2.Value = ds.Tables[0].Rows[0]["middlename2"];
                //    txtmname3.Value = ds.Tables[0].Rows[0]["middlename3"];
                //    txtedition.Value = ds.Tables[0].Rows[0]["edition"];
                //    txtyrofedition.Value = ds.Tables[0].Rows[0]["yearofedition"];
                //    txtPubYear.Value = ds.Tables[0].Rows[0]["yearofPublication"];
                //    txtvolno.Value = ds.Tables[0].Rows[0]["volumeno"];
                //    txtPart.Value = ds.Tables[0].Rows[0]["Vpart"];
                //    // hditmeId.Value = ds.Tables(0).Rows(0).Item("ItemNo")
                //    txtSubtitle.Value = ds.Tables[0].Rows[0]["subtitle"];
                //    txtisbn.Value = ds.Tables[0].Rows[0]["isbn"];
                //    // txtnoofstud.Value = IIf(ds.Tables(0).Rows(0).Item("noofstudents") = 0, String.Empty, ds.Tables(0).Rows(0).Item("noofstudents"))
                //    // txtnoofcopies.Value = ds.Tables(0).Rows(0).Item("noofcopies")
                //    txtcoursenm.Value = ds.Tables[0].Rows[0]["coursenumber"];
                //    // txtExchangeRate.Value = ds.Tables(0).Rows(0).Item("exchangerate")
                //    cmbLanguage.SelectedValue = ds.Tables[0].Rows[0]["Language_Id"];
                //    // txtTotalAmount.Value = ds.Tables(0).Rows(0).Item("totalamount")
                //    // txtprice.Value = ds.Tables(0).Rows(0).Item("price")


                //    this.txtnoofcopies.Value = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["noofcopy"], 0, false)), string.Empty, ds.Tables[0].Rows[0]["noofcopy"]);


                //    ListItem lstItem;
                //    lstItem = cmbdept.Items.FindByValue(ds.Tables[0].Rows[0]["departmentcode"]);
                //    if (lstItem == null)
                //    {
                //        cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
                //    }
                //    else
                //    {
                //        cmbdept.SelectedValue = ds.Tables[0].Rows[0]["departmentcode"];
                //    }
                //    // ***************
                //    // Dim CommonDs As DataSet = objBLL.retDataSet("Select CircUserManagement.userid,firstname+' '+middlename+' '+lastname as firstname from CircUserManagement,circclassmaster where CircUserManagement.departmentcode=" & ds.Tables(0).Rows(0).Item("departmentcode") & " and  CircUserManagement.classname=circclassmaster.classname and circclassmaster.canRequest=N'Y'  order by firstname+' '+middlename+' '+lastname;")
                //    // *******************
                //    populateRequesters(Conversions.ToInteger(ds.Tables[0].Rows[0]["departmentcode"]), conn);
                //    // cmbreq.SelectedValue = ds.Tables(0).Rows(0).Item("requestercode")
                //    lstItem = cmbreq.Items.FindByValue(ds.Tables[0].Rows[0]["requestercode"]);

                //    if (lstItem == null)
                //    {
                //        cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                //    }
                //    else
                //    {
                //        cmbreq.SelectedValue = Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["requestercode"], "0", false)), "HOD", ds.Tables[0].Rows[0]["requestercode"]);
                //    }


                //    this.hReq.Value = ds.Tables[0].Rows[0]["requestercode"];
                //    // If ds.Tables(0).Rows(0).Item("indenttype") = "Approval" Then
                //    // chkapproval.Checked = True
                //    // Else
                //    // chkapproval.Checked = False
                //    // End If
                //    // If ds.Tables(0).Rows(0).Item("isSatnding") = "y" Then
                //    // chkStanding.Checked = True
                //    // Else
                //    // chkStanding.Checked = False
                //    // End If
                //    persontype.SelectedValue = ds.Tables[0].Rows[0]["authortype"];
                //    // ***************
                //    string sqlstr = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'", ds.Tables[0].Rows[0]["publisherid"]), "' and publishermaster.publisherid=addresstable.addid  and addrelation=N'publisher'"));
                //    // Dim cmd As New OleDbCommand(sqlstr, Con)
                //    string tmpstr = objDAL.rtnSingleVal(sqlstr, conn);
                //    this.hdPublisherId.Value = Interaction.IIf(ReferenceEquals(ds.Tables[0].Rows[0]["publisherid"], DBNull.Value), string.Empty, ds.Tables[0].Rows[0]["publisherid"]);
                //    txtCmbPublisher.Text = tmpstr; // ds.Tables(0).Rows(0).Item("publisherid")

                //    hdPubId.Value = Interaction.IIf(ReferenceEquals(ds.Tables[0].Rows[0]["publisherid"], DBNull.Value), string.Empty, ds.Tables[0].Rows[0]["publisherid"]);
                //    // cmbbookcategory.Value = ds.Tables(0).Rows(0).Item("category")
                //    mediatype.SelectedValue = ds.Tables[0].Rows[0]["mediatype"];
                //}
                
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
            }
           
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void lnkAsign_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            try
            {
                //conn = objDAL.retConnection;
                conn.Open();

                this.SetFocus(lnkAsign);
                //refreshFileds(conn);
                //default_setting(conn);
                if (this.cmbdept.SelectedValue != HComboSelect.Value)
                {
                    populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), conn);
                    if (this.hReq.Value != "")
                    {
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }
                    else
                    {
                        this.hReq.Value = this.cmbreq.SelectedValue;
                    }
                }
               
                //OleDbDataReader genDr = objDAL.rtnDataReader("Select  IndentId ,dbo.get_title(title,volumeno,Vpart,edition) as title, dbo.get_full_name1(firstname1,middlename1,lastname1,firstname2,middlename2,lastname2,firstname3,middlename3,lastname3) as authorname,publishermaster.firstname+', '+addresstable.percity as firstname FROM AddressTable INNER JOIN publishermaster ON AddressTable.addid = publishermaster.PublisherId RIGHT OUTER JOIN OPACINDENT ON publishermaster.PublisherId = OPACINDENT.publisherid where indentnumber =''", conn);
                
                //if (!genDr.HasRows)
                //{
                //    // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                //    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                //    message.PageMesg(System.Resources.ValidationResources.rNotFound.ToString, this, DBUTIL.dbUtilities.MsgLevel.Warning);

                //    genDr.Close();
                //    // Con.Dispose()
                //    // 'shweta
                //    this.chkSearch.Checked = false;
                //    optIndent1.Visible = false;
                //    optAdvance1.Visible = false;
                //    this.ddl1.Visible = false;
                //    this.txtCategory.Visible = false;
                //    this.txtCategory.Value = string.Empty;
                //    this.btnCategoryFilter.Visible = false;
                //    this.lstAllCategory.Visible = false;
                //    this.lbloptIndent1.Visible = false;
                //    this.lbloptAdvance1.Visible = false;
                //    // 'shweta
                //    // Dim returnScr As String = "" '
                //    // returnScr &= "<script language='javascript' type='text/javascript'>"
                //    // returnScr &= "javascript:SearchVis('chkSearch1','optIndent1','optAdvance1','lbloptIndent1','lbloptAdvance1');"
                //    // returnScr &= "<" & "/" & "script>"
                //    // Page.RegisterStartupScript("", returnScr)
                //    // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "callUVal1();", True)
                //    return;
                //}
                //genDr.Close();
                
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
                
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "callUVal2();", true);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void cmdIndentData_ServerClick(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            try
            {
                //conn = objDAL.retConnection;
                //if (this.cmbdept.SelectedValue != HComboSelect.Value)
                //{
                //    conn.Open();
                //    populateRequesters(this.cmbdept.SelectedValue, conn);
                //    this.cmbreq.SelectedValue = Interaction.IIf(this.hReq.Value == string.Empty, "HOD", hReq.Value);
                //}
               
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
               
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "verPreIndent('VerPreIndent','txttitle','txtSubtitle','txtfname1','txtmname1','txtlname1','txtSeries','txtisbn');", true);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void chkVerify_CheckedChanged(object sender, EventArgs e)
        {
            this.SetFocus(chkVerify);
            OleDbConnection conn = null;
            try
            {
                //conn = objDAL.retConnection;
                // 'shweta
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
                
                conn.Open();

                if (this.cmbdept.SelectedValue != HComboSelect.Value)
                {
                    //populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), conn);
                    //this.cmbreq.SelectedValue = Interaction(this.hReq.Value == string.Empty, "HOD", this.hReq.Value);
                }

                if (chkVerify.Checked == true)
                {
                    // Me.cmdCatalogData.Visible = True
                    this.cmdIndentData.Visible = true;
                }
                else
                {
                    // Me.cmdCatalogData.Visible = False
                    this.cmdIndentData.Visible = false;
                }
            }
            catch (Exception ex)
            {
                
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void btnFillPub_ServerClick(object sender, EventArgs e)
        {
            OleDbConnection Conn = null;
            
            try
            {
                
                //Conn = objDAL.retConnection;
                // 'shweta
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
               
                Conn.Open();
                if (this.HWhichFill.Value == "PublisherMaster")
                {
                    
                    this.hdPublisherId.Value = this.HNewForm.Value;  
                    string sqlstr = "Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'" + hdPubId.Value + "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'";
                  

                    //this.SetFocus(txtCmbPublisher);
                }
                // tmpcon.Dispose()
                // LibObj.populateDDL(cmbvennm, "Select vendorid,vendorname+', '+percity as vendorname,vendorcode from vendormaster join addresstable on vendormaster.vendorcode=addresstable.addid and AddressTable.addrelation='vendor' order by vendorname+' '+'-'+percity", "vendorname", "vendorid", HComboSelect.Value, tmpcon)
                // cmbvennm.SelectedValue = A
                else if (this.HWhichFill.Value == "UserManagement")
                {
                    if (cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(cmbdept.SelectedValue), Conn);
                        if (cmbreq.Items.Count > 1)
                        {
                            //string depCode = objDAL.rtnSingleVal("Select CircUserManagement.departmentcode from CircUserManagement,circclassmaster where CircUserManagement.userid=N'" + HNewForm.Value + "' and  CircUserManagement.classname=circclassmaster.classname and circclassmaster.canRequest=N'Y'", Conn);
                            //if (cmbdept.SelectedValue != deptCode)
                            //{
                            //    cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                            //}
                            //else
                            //{
                            //    cmbreq.SelectedValue = HNewForm.Value;
                            //    hReq.Value = HNewForm.Value;
                            //}
                        }
                    }
                    // ************
                    else
                    {
                        cmbreq.Items.Clear();
                        cmbreq.Items.Add("HOD");
                        cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                    }

                    this.SetFocus(cmbreq);
                }
                else if (this.HWhichFill.Value == "VendorMaster")
                {
                    
                    this.HdVendorid.Value = this.HNewForm.Value;
                    string sqlstr = "Select vendorname+', '+percity as firstname  from  vendormaster,addresstable where vendorid=N'" + HdVendorid.Value + "' and vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor'";
                    
                    //txtCmbVendor.Text = objDAL.rtnSingleVal(sqlstr, Conn); // tmpstr 'Me.HNewForm.Value

                    this.SetFocus(txtCmbVendor);
                   
                    
                    if (this.cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }

                    //this.SetFocus(txtCmbVendor);
                }
                else if (this.HWhichFill.Value == "DepartmentMaster")
                {
                    string Deptname = "";
                    //if (chkbdgt == "Y")
                    //{
                    //    Deptname = Convert.ToString("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from departmentmaster,institutemaster where  departmentmaster.institutecode=institutemaster.institutecode  and departmentcode in (select departmentcode from budgetmaster where curr_session=N'"+ LoggedUser.Logged(). Session)+ "') order by InstituteMaster.ShortName + '-' + departmentname";
                    //}
                    //else
                    //{
                    //    Deptname = "select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from departmentmaster,institutemaster where  departmentmaster.institutecode=institutemaster.institutecode  order by InstituteMaster.ShortName + '-' + departmentname";
                    //}
                    //GData.TrOpen();
                    //object datattabledepartment = GData.DataT(Deptname);
                    //if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(datattabledepartment.Rows.Count, 0, false)))
                    {
                        //cmbdept.DataSource = datattabledepartment;
                        cmbdept.DataTextField = "departmentname";
                        cmbdept.DataValueField = "departmentcode";
                        cmbdept.DataBind();
                    }
                    //GData.TrClose();
                    if (HNewForm.Value != "undefined")
                    {
                        cmbdept.SelectedValue = HNewForm.Value;
                        // ElseIf HNewForm.Value <> "" Then
                        // cmbdept.SelectedValue = HNewForm.Value
                    }
                    // cmbdept.Items.IndexOf(cmbdept.Items.FindByValue(Me.HNewForm.Value))
                    if (this.cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                        // Me.cmbreq.SelectedValue = Me.hReq.Value
                    }

                    this.SetFocus(cmbdept);
                }
                else if (this.HWhichFill.Value == "ExchangeMaster")
                {
                    //objDAL.populateDDLByQue(cmbcurr, "Select distinct currencycode,currencyname from exchangemaster order by currencyname", "currencyname", "currencycode", HComboSelect.Value, Conn);
                    if (this.HNewForm.Value != "")
                    {
                        cmbcurr.SelectedValue = this.HNewForm.Value;
                        //this.txtExchangeRate.Value = objDAL.rtnSingleVal(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select ", Interaction.IIf(cmbgocorbank.SelectedItem.Text == "Bank", "bankrate", "gocrate")), " from exchangemaster  where  currencycode=")) + HNewForm.Value, Conn);
                    }
                   
                    if (this.cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }

                    this.SetFocus(cmbcurr);
                }
                else if (this.HWhichFill.Value == "frm_mediatype")
                {
                    //objDAL.populateDDLByQue(mediatype, "Select media_id,media_name from media_type order by media_name", "media_name", "media_id", HComboSelect.Value, Conn);
                    mediatype.SelectedValue = this.HNewForm.Value;
                    if (this.cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }
                    this.SetFocus(mediatype);
                }

                else if (this.HWhichFill.Value == "TranslationLanguages")
                {
                    //objDAL.populateDDLByQue(cmbLanguage, "Select Language_Id,Language_name from Translation_Language order by Language_Name", "Language_name", "Language_Id", HComboSelect.Value, Conn);
                    cmbLanguage.SelectedValue = this.HNewForm.Value;
                    if (this.cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }
                    this.SetFocus(cmbLanguage);
                }

                else if (this.HWhichFill.Value == "CategoryLoadingStatus")
                {
                    //Fillcategory(Conn);
                    cmbbookcategory.SelectedIndex = this.cmbbookcategory.Items.IndexOf(cmbbookcategory.Items.FindByValue(HNewForm.Value));

                    this.SetFocus(cmbbookcategory);
                    if (this.cmbdept.SelectedValue != HComboSelect.Value)
                    {
                        populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                        this.cmbreq.SelectedValue = this.hReq.Value;
                    }
                }
            }

            // tmpcon.Close()
            // tmpcon.Dispose()

            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void cboLanguage_new_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection Conn = null;
            try
            {
                //Conn = objDAL.retConnection;
                Conn.Open();
                //this.TextBox1.Font.Name = objDAL.rtnSingleVal("Select font_name from Translation_Language where Language_Id=N'" + cboLanguage_new.SelectedValue + "' order by Language_Id", Conn); // languageds.Tables(0).Rows(0).Item(0).ToString()
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void chkLanguage_CheckedChanged(object sender, EventArgs e)
        {
            OleDbConnection Conn = null;
            try
            {
                //Conn = objDAL.retConnection;
                //if (chkLanguage.Checked == true)
                //{
                //    Conn.Open();
                //    //OleDbDataReader genDr = objDAL.rtnDataReader("Select Language_Id,Language_name from Translation_Language order by Language_Id", Conn);
                    
                //    this.cboLanguage_new.Visible = true;
                //    //if ()
                //    //    //(genDr.HasRows)
                //    //{
                //    //    //cboLanguage_new.DataSource = genDr;
                //    //    cboLanguage_new.DataTextField = "Language_name";
                //    //    cboLanguage_new.DataValueField = "Language_Id";
                //    //    cboLanguage_new.DataBind();
                //    //}
                //    else
                //    {
                //        cboLanguage_new.Items.Clear();
                //    }
                //    cboLanguage_new.Items.Add(HComboSelect.Value);
                //    cboLanguage_new.SelectedIndex = cmbLanguage.Items.Count - 1;
                    
                //    //genDr.Close();
                //}
                //else if (chkLanguage.Checked == false)
                //{
                //    this.cboLanguage_new.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void lnkModify_Click(object sender, EventArgs e)
        {
            if ((hdOnlineP.Value) == "Y")
            {
                if (hdIndentStage.Value == "3" | hdIndentStage.Value == "4" | hdIndentStage.Value == "5")
                {
                    LibObj.MsgBox1("Can not Proceed. Indent has been verified.", this);
                    return;
                }
            }
            OleDbConnection Conn = null;
            try
            {
                //Conn = objDAL.retConnection;
                string returnScript = "";
                Hdventag.Value = "M";
                // 'shweta
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
                
                lnkContinue.Visible = false;
                lnkModify.Visible = false;
                if (this.cmbdept.SelectedValue != HComboSelect.Value)
                {
                    Conn.Open();
                    populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                    Conn.Close();
                    this.cmbreq.SelectedValue = this.hReq.Value;
                }
                Session["IndentValue"] = (txtTotalAmount.Value);
                if (chkapproval.Checked == true)
                {
                    hdTemp.Value = "A";
                }
                else
                {
                    hdTemp.Value = "N";
                }
                if (tmpcondition == "Y")
                {
                    cmdsave.Disabled = false;
                }
                cmdsave.Value = Resources.ValidationResources.bUpdate.ToString();
                this.cmddelete.Disabled = true;
                this.cmdPrint1.Enabled = false;
                // asmvendor.SelectedValue = HdVendorid.Value  '??
                txtCmbVendor.Enabled = true;
                cmbdept.Enabled = false;
                txtindentdate.Visible = true;
                // Me.btnDate.Disabled = False

                chkapproval.Disabled = false;
                txtnoofcopies.Disabled = false;
                txtprice.Disabled = false;
                cmbcurr.Enabled = true;
                if ((hdOnlineP.Value) == "Y")
                {
                    cmdVerifiedBy.Disabled = true;
                    hdIndentStage.Value = "1";
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }

        }
        private static string strlnkContinue = "0";
        protected void lnkContinue_Click(object sender, EventArgs e)
        {
            if ((hdOnlineP.Value) == "Y")
            {
                if (hdIndentStage.Value == "3" | hdIndentStage.Value == "4" | hdIndentStage.Value == "5")
                {
                    // LibObj.MsgBox1("Can not Proceed. Indent has been verified.", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Can not Proceed. Indent has been verified.", Me)
                    message.PageMesg("Can not Proceed. Indent has been verified.", this,  dbUtilities.MsgLevel.Warning);

                    return;
                }
            }
            OleDbConnection Conn = null;
            strlnkContinue = "1";
            try
            {
                //Conn = objDAL.retConnection;
                string returnScript = "";
                Hdventag.Value = "C";
                // 'shweta
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;
                
                lnkContinue.Visible = false;
                lnkModify.Visible = false;
                if (this.cmbdept.SelectedValue != this.HComboSelect.Value)
                {
                    Conn.Open();
                    populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), Conn);
                    this.cmbreq.SelectedValue = this.hReq.Value;
                }
                if (tmpcondition == "Y")
                {
                    cmdsave.Disabled = false;
                }
                cmdsave.Value = Resources.ValidationResources.bSave.ToString();
                Session["IndentValue"] = 0;
                this.cmddelete.Disabled = true;
                this.cmdPrint1.Enabled = false;
                // asmvendor.SelectedValue = HdVendorid.Value
                txtCmbVendor.Enabled = false;
                chkapproval.Disabled = false;
                txtnoofcopies.Disabled = false;
                txtprice.Disabled = false;
                cmbcurr.Enabled = true;
                if ((hdOnlineP.Value) == "Y")
                {
                    cmdVerifiedBy.Disabled = true;
                    hdIndentStage.Value = "1";
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void btnCategoryFilter_ServerClick(object sender, EventArgs e)
        {
            OleDbConnection Conn = null;
            try
            {
                this.lstAllCategory.Items.Clear();
                //Conn = objDAL.retConnection;
                Conn.Open();
                GetData2(Conn);
                this.txtCategory.Value = string.Empty;
               
                if (optIndent1.Checked == true)
                {
                    this.ddl1.Visible = false;
                }
                else
                {
                    this.ddl1.Visible = true;

                }
                this.SetFocus(txtCategory);
                // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "setVis('optIndent1');", True)
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {
            OleDbConnection Conn = null;
            try
            {
                //Conn = objDAL.retConnection;
                //if (!string.IsNullOrEmpty((lstAllCategory.SelectedValue))
                //{
                   
                //    Conn.Open();


                //    //DataSet ds = objDAL.rtnDataSet("select * from indentmaster where indentid=N'" + lstAllCategory.SelectedItem.Value + "'", Conn);
                //    // ***************
                //    //if (ds.Tables[0].Rows.Count > 0)
                //    //{
                //    //    // 'order_check_code='0' or issatnding=N'y'

                //    //    txtIndentNumber.Value = ds.Tables[0].Rows[0]["indentnumber"].ToString();
                //    //    hdindentId.Value = ds.Tables[0].Rows[0]["indentid"].ToString();
                //    //    txtindentdate.Text = String.Format(ds.Tables[0].Rows[0]["indentdate"], this.hrDate.Value);
                //    //    txtindenttime.Value = String.Format(ds.Tables[0].Rows[0]["indenttime"], "hh:mm:ss tt");
                //    //    txttitle.Text = ds.Tables[0].Rows[0]["title"].ToString();
                //    //    txtSeries.Value = ds.Tables[0].Rows[0]["seriesname"].ToString();
                //    //    txtfname1.Value = ds.Tables[0].Rows[0]["firstname1"].ToString();
                //    //    txtfname2.Value = ds.Tables[0].Rows[0]["firstname2"].ToString();
                //    //    txtfname3.Value = ds.Tables[0].Rows[0]["firstname3"].ToString();
                //    //    txtlname1.Value = ds.Tables[0].Rows[0]["lastname1"].ToString();
                //    //    txtlname2.Value = ds.Tables[0].Rows[0]["lastname2"].ToString();
                //    //    txtlname3.Value = ds.Tables[0].Rows[0]["lastname3"].ToString();
                //    //    //txtmname1.Value = ds.Tables[0].Rows[0]["middlename1"].ToString();
                //    //    txtmname2.Value = ds.Tables[0].Rows[0]["middlename2"].ToString();
                //    //    txtmname3.Value = ds.Tables[0].Rows[0]["middlename3"].ToString();
                //    //    txtedition.Value = ds.Tables[0].Rows[0]["edition"].ToString();
                //    //    txtyrofedition.Value = ds.Tables[0].Rows[0]["yearofedition"].ToString();
                //    //    txtPubYear.Value = ds.Tables[0].Rows[0]["yearofPublication"].ToString();
                //    //    txtvolno.Value = ds.Tables[0].Rows[0]["volumeno"].ToString();
                //    //    txtPart.Value = ds.Tables[0].Rows[0]["Vpart"].ToString();
                //    //    hditmeId.Value = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                //    //    txtSubtitle.Value = ds.Tables[0].Rows[0]["subtitle"].ToString();
                //    //    txtisbn.Value = ds.Tables[0].Rows[0]["isbn"].ToString();
                //    //    //txtnoofstud.Value = Interaction.IIf(Convert.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["noofstudents"], 0, false)), string.Empty, ds.Tables[0].Rows[0]["noofstudents"]);
                //    //    txtnoofcopies.Value = ds.Tables[0].Rows[0]["noofcopies"].ToString();
                //    //    txtcoursenm.Value = ds.Tables[0].Rows[0]["coursenumber"].ToString();
                //    //    txtExchangeRate.Value = ds.Tables[0].Rows[0]["exchangerate"].ToString();
                //    //    cmbLanguage.SelectedValue = ds.Tables[0].Rows[0]["Language_Id"].ToString();
                //    //    txtTotalAmount.Value = ds.Tables[0].Rows[0]["totalamount"].ToString();
                //    //    txtprice.Value = ds.Tables[0].Rows[0]["price"].ToString();
                //    //    cmbdept.SelectedValue = ds.Tables[0].Rows[0]["departmentcode"].ToString();
                //    //    populateRequesters(Conversions.ToInteger(ds.Tables[0].Rows[0]["departmentcode"]), Conn);
                //    //    // 11111111
                //    //    this.hReq.Value = ds.Tables[0].Rows[0]["requestercode"].ToString();
                //    //    // 11111111
                //    //    int flag = 0;
                //    //    int i = 0;
                //    //    var loopTo = this.cmbreq.Items.Count - 1;
                //    //    for (i = 0; i <= loopTo; i++)
                //    //    {
                //    //        if (cmbreq.Items[i].Value == this.hReq.Value)
                //    //        {
                //    //            flag = 1;
                //    //            break;
                //    //        }
                //    //    }

                //    //    if (flag == 1)
                //    //    {
                //    //        cmbreq.SelectedValue = ds.Tables[0].Rows[0]["requestercode"].ToString();
                //    //    }
                //    //    else
                //    //    {
                //    //        cmbreq.SelectedIndex = cmbreq.Items.Count - 1;
                //    //    }


                //    //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["indenttype"], "Approval", false)))
                //    //    {
                //    //        chkapproval.Checked = true;
                //    //    }
                //    //    else
                //    //    {
                //    //        chkapproval.Checked = false;
                //    //    }
                //    //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["isSatnding"], "y", false)))
                //    //    {
                //    //        chkStanding.Checked = true;
                //    //    }
                //    //    else
                //    //    {
                //    //        chkStanding.Checked = false;
                //    //    }
                //    //    persontype.SelectedValue = ds.Tables[0].Rows[0]["authortype"].ToString();
                //    //    // ***************
                //    //    // ***************'************
                //    //   // OleDbDataReader genDr = objDAL.rtnDataReader(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'", ds.Tables[0].Rows[0]["publisherid"]), "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher';Select vendorname+', '+percity as firstname  from  vendormaster,addresstable where vendorid=N'"), ds.Tables[0].Rows[0]["vendorid"]), "' and vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor';"), Conn);
                //    //    //genDr.Read();
                //    //    // *******************************
                //    //    string sqlstr = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'", ds.Tables[0].Rows[0]["publisherid"]), "' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'"));
                     
                //    //    //DataSet dsP = objDAL.rtnDataSet(sqlstr, Conn);
                //    //   // string tmpstr = Conversions.ToString(genDr[0]);Me.hdPublisherId.Value = ds.Tables[0].Rows[0].Item["publisherid"];
                //    //    //txtCmbPublisher.Text = dsP.Tables[0].Rows[0][0];
                        
                //    //    hdPubId.Value = ds.Tables[0].Rows[0]["publisherid"].ToString();
                       
                //    //    genDr.NextResult();
                //    //    genDr.Read();
                //    //    string tmpstr1 = Conversions.ToString(genDr[0]); // cmd1.ExecuteScalar
                //    //    genDr.Close();
                //    //    this.HdVendorid.Value = ds.Tables[0].Rows[0]["vendorid"].ToString();
                //    //    this.Hdvenid.Value = ds.Tables[0].Rows[0]["vendorid"].ToString();
                //    //    this.txtCmbVendor.Text = tmpstr1;
                //    //    // asmvendor.SelectedValue = ds.Tables(0).Rows(0).Item("vendorid")
                //    //    // -------------------jeetendra---------------------------
                //    //    cmbcurr.SelectedValue = ds.Tables[0].Rows[0]["currencycode"].ToString();

                //    //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["go_bank"], "B", false)))
                //    //    {
                //    //        cmbgocorbank.SelectedItem.Text = "Bank";
                //    //    }
                //    //    else
                //    //    {
                //    //        cmbgocorbank.SelectedItem.Text = "Goc";
                //    //    }
                //    //    cmbbookcategory.Value = ds.Tables[0].Rows[0]["category"].ToString();
                //    //    mediatype.SelectedValue = ds.Tables[0].Rows[0]["mediatype"].ToString();
                //    //    cmdsave.Value = Resources.ValidationResources.bUpdate;
                //    //    cmdPrint1.Enabled = true;
                //    //    Session["IndentValue"] = (txtTotalAmount.Value);


                //    //    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["indenttype"], "Approval", false)))
                //    //    {
                //    //        hdTemp.Value = "A";
                //    //    }
                //    //    else if (Convert.ToBoolean(Operators.ConditionalCompareObjectEqual(ds.Tables[0].Rows[0]["indenttype"], "Non-Approval", false)))
                //    //    {
                //    //        hdTemp.Value = "N";
                //    //    }
                //    //    hdTop.Value = "top";

                //    //    // by kaushal ________________
                //    //    if (ds.Tables[0].Rows[0]["order_check_code"].ToString() != "0" | ds.Tables[0].Rows[0]["issatnding"].ToString() != "n")
                //    //    {
                //    //        LibObj.MsgBox1("Order of this Indent has been completed, So Can not be Update.", this);
                //    //        cmddelete.Disabled = true;
                //    //        cmdsave.Disabled = true;
                //    //        return;
                //    //    }
                //    //    else
                //    //    {
                //    //        cmddelete.Disabled = false;
                //    //        cmdsave.Disabled = false;
                //    //    }
                //    //    this.SetFocus(lnkModify);
                //    //    if (tmpcondition == "Y")
                //    //    {
                //    //        this.cmddelete.Disabled = false;
                //    //        this.cmdPrint1.Enabled = true;
                //    //        this.cmdsave.Disabled = false;
                //    //    }
                //    //    else
                //    //    {
                //    //        this.cmddelete.Disabled = true;
                //    //        this.cmdPrint1.Enabled = false;
                //    //        this.cmdsave.Disabled = true;
                //    //    }
                //    //    lnkContinue.Visible = true;
                //    //    lnkModify.Visible = true;
                //    //    cmdsave.Disabled = true;
                //    //    txtCmbVendor.Enabled = false;
                //    //    cmbdept.Enabled = false;
                //    //    txtindentdate.Visible = false;

                //    //    chkapproval.Disabled = true;
                //    //    txtnoofcopies.Disabled = true;
                //    //    txtprice.Disabled = true;
                //    //    cmbcurr.Enabled = false;
                //    //}
                //    // Me.btnDate.Visible = False

                //    //else
                //    //{
                //    //    // Hidden1.Value = "8"
                //    //}
                //}
               
                this.chkSearch.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;

                if (!string.IsNullOrEmpty((lstAllCategory.SelectedValue)))
                {
                    if ((hdOnlineP.Value) == "Y")
                    {
                        //var cmd = new OleDbCommand("select OnlinePStatus from indentmaster where indentnumber='" + (txtIndentNumber.Value) + "'", Conn);
                        //hdIndentStage.Value = Operators.AddObject(cmd.ExecuteScalar(), 1);
                        ////checkPermission(Conn);
                        //cmd.Dispose();
                    }
                }
            }
           
            catch (Exception ex)
            {
                
                message.PageMesg(Resources.ValidationResources.IRUnable, this,  dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void cmdPrint1_Click(object sender, EventArgs e)
        {
            if (txtIndentNumber.Value != "")
            {
                var con = new OleDbConnection(retConstr(""));
                con.Open();
                string cmdstr = "Update indentmaster set PrintStatus='Y' where indentnumber='" + txtIndentNumber.Value + "'";
                var cmd = new OleDbCommand(cmdstr, con);
                cmd.ExecuteNonQuery();
                con.Close();

               // PrintorMail("Print");

            }

        }

        protected void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var conn = new OleDbConnection(retConstr(""));
                conn.Open();
                if (this.cmbdept.SelectedValue != HComboSelect.Value)
                {
                    populateRequesters(Convert.ToInt32(this.cmbdept.SelectedValue), conn);
                    this.cmbreq.SelectedValue = this.hReq.Value;
                }
                if (this.chkSearch.Checked == true)
                {
                    this.optIndent1.Visible = true;
                    optAdvance1.Visible = true;
                    this.optIndent1.Checked = true;
                    this.SetFocus(txtCategory);
                    if (optIndent1.Checked == true)
                    {
                        this.txtCategory.Visible = true;
                        this.btnCategoryFilter.Visible = true;
                        this.lstAllCategory.Visible = true;
                        ddl1.Visible = false;
                        this.lbloptIndent1.Visible = true;
                        this.lbloptAdvance1.Visible = true;
                    }
                    else
                    {
                        this.ddl1.Visible = true;
                        this.txtCategory.Visible = true;
                        this.btnCategoryFilter.Visible = true;
                        this.lstAllCategory.Visible = true;
                    }
                }
                else
                {
                    optIndent1.Visible = false;
                    optAdvance1.Visible = false;
                    optAdvance1.Checked = false;
                    optIndent1.Checked = true;
                    this.ddl1.Visible = false;
                    this.txtCategory.Visible = false;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter.Visible = false;
                    this.lstAllCategory.Visible = false;
                    this.lbloptIndent1.Visible = false;
                    this.lbloptAdvance1.Visible = false;
                }
                this.lstAllCategory.Items.Clear();
                GetData2(conn);
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public void genrateindentID(OleDbConnection Conn)
        {
            //string tmpstr = objDAL.rtnSingleVal("select coalesce(max(convert(int,IndentId)),0,max(IndentId)) from indentmaster", Conn);
            //hdindentId.Value = Interaction.IIf(Conversion.Val(tmpstr) == 0d, 1, Conversion.Val(tmpstr) + 1d);
        }
        public void GenearteItemID(string indenNo, OleDbConnection Conn)
        {
            //string tmpstr = objDAL.rtnSingleVal("select coalesce(max(convert(int,ItemNo)),0,max(convert(int,ItemNo))) from indentmaster where indentnumber=N'" + indenNo + "'", Conn);
            //hditmeId.Value = Interaction.IIf(Conversion.Val(tmpstr) == 0d, 1, Conversion.Val(tmpstr) + 1d);
            
        }
        private string t = "Indent";
        public void genrateindent(OleDbConnection Conn)
        {
            try
            {
                // *************
                //OleDbDataReader GenDr = objDAL.rtnDataReader("select shortname from departmentmaster where departmentcode='" + cmbdept.SelectedValue + "';select CurrentPosition from departmentmaster where departmentcode='" + cmbdept.SelectedValue + "'", Conn);
                //GenDr.Read();
               // string tmpstr3 = Conversions.ToString(GenDr[0]);
                int maxno = 0;
                //GenDr.NextResult();
                //GenDr.Read();
                //maxno = Conversions.ToInteger(GenDr[0]);
                //GenDr.Close();
                
                //txtIndentNumber.Value = (tmpstr3 + "-"+ LoggedUser.Logged().Session)+ "-"+ maxno + 1;
                return;
            }
            catch(Exception ex)
            {

                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdreset1_Click(object sender, EventArgs e)
        {
            OleDbConnection indentcon = null;
            try
            {
                hdnIndId.Value = "";
                hdPublisherId.Value = "";
                HdVendorid.Value = "";
                Hdvenid.Value = "";
                hdPubId.Value = "";
                hdTop.Value = "top";
                //indentcon = objDAL.retConnection;
                indentcon.Open();
                //refreshFileds(indentcon);
                hdTemp.Value = string.Empty;
                txtCmbVendor.Text = string.Empty;
                txtindenttime.Value = DateTime.Now.ToString("hh:mm:ss tt");
                cmdsave.Value = Resources.ValidationResources.bSave.ToString();
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Disabled = false;
                    this.cmdPrint1.Enabled = true;
                    this.cmdsave.Disabled = false;
                }
                else
                {
                    this.cmddelete.Disabled = true;
                    this.cmdPrint1.Enabled = false;
                    this.cmdsave.Disabled = true;
                }
                this.lstAllCategory.Items.Clear();
                GetData2(indentcon);
                cmdPrint1.Enabled = false;
                cmddelete.Disabled = true;
                // 'shweta
                this.chkSearch.Checked = false;
                // optIndent1.Checked = False
                optAdvance1.Checked = false;
                optIndent1.Visible = false;
                optAdvance1.Visible = false;
                this.ddl1.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                this.lbloptIndent1.Visible = false;
                this.lbloptAdvance1.Visible = false;


                chkapproval.Disabled = false;
                txtnoofcopies.Disabled = false;
                txtprice.Disabled = false;
                cmbcurr.Enabled = true;
                persontype.SelectedIndex = 0;
                cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
                this.hReq.Value = this.HComboSelect.Value;
                //default_setting(indentcon);

                //hideTempLabels();

                if ((hdOnlineP.Value) == "Y")
                {
                    hdIndentStage.Value = "1";
                    //checkPermission(indentcon);
                }
                txtCmbPublisher.Text = "";
            }
            // asmpublisher.SelectedValue = ""
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (indentcon.State == ConnectionState.Open)
                {
                    indentcon.Close();
                }
            }

        }

        protected void cmbcurr_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            var conn = new OleDbConnection(retConstr(""));
            try
            {
                var chkds = new DataSet(); 
                int i = 0;
                
                var counter = default(int);
                
                DateTime ddate;
                ddate = Convert.ToDateTime(txtindentdate.Text);
                conn.Open();
                var da = new OleDbDataAdapter(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select ", Interaction.IIf(cmbgocorbank.SelectedItem.Text == "Bank", "bankrate", "gocrate")), ",EffectiveFrom from exchangemaster  where  currencycode=")) + this.cmbcurr.SelectedValue + " order by EffectiveFrom desc", conn);
                da.Fill(chkds);
                if (chkds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToDateTime(chkds.Tables[0].Rows[counter][1]) <= Convert.ToDateTime(ddate))
                    {
                        this.txtExchangeRate.Value = chkds.Tables[0].Rows[counter][0].ToString();
                    }
                }
                else
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rExRate.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rExRate.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rExRate.ToString(), this,dbUtilities.MsgLevel.Warning);
                    cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
                }
                //this.SetFocus(cmbcurr);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
            */
        }
        //public void PrintorMail(string strPrintorMail)
        //{
        //    try
        //    {
        //        Session["indentnumber"] = txtIndentNumber.Value;
        //        string str;
        //        str = "Select distinct * from NewIindentView_R where indentnumber=N'" + txtIndentNumber.Value + "'";
        //        var myConnection = new OleDbConnection(retConstr(Conversions.ToString(Session["LibWiseDBConn"])));
        //        myConnection.Open();
        //        var MyCommand = new OleDbCommand();
        //        MyCommand.Connection = myConnection;
        //        MyCommand.CommandText = str;
        //        MyCommand.CommandType = CommandType.Text;
        //        var MyDA = new OleDbDataAdapter();
        //        MyDA.SelectCommand = MyCommand;
        //        //var myDS = new NewIndentDataSet2();
        //        MyDA.Fill(myDS, "Cancel");
        //        if (myDS.Tables("Cancel").Rows.Count > 0)
        //        {

        //            var myReportDocument = new ReportDocument();
        //            myReportDocument.Load(Server.MapPath(@"Reports\NewIndentCrystalReport2.rpt"));
        //            myReportDocument.SetDataSource(myDS.Tables("Cancel"));
        //            // Field
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Field1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Field3"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 10.0f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field6"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field4"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field5"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field8"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field9"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field11"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field12"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field14"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field13"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["isbn1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field19"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["indenttype1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["CategoryLoadingStatus1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Field24"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));

        //            // Formula
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["IndentNo1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["dt11"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["author1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["address1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Field16"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Field18"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["nostudent1"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Field23"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 8.5f));
        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Field2"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 12.0f));

        //            // Textbox
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text18"]).Text = System.Resources.ValidationResources.LbDepartment.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text18"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text16"]).Text = System.Resources.ValidationResources.rpACQUISITIONUNIT.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text16"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 10.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text15"]).Text = System.Resources.ValidationResources.IForm.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text15"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 10.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text14"]).Text = System.Resources.ValidationResources.GrIndntN.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text14"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["GrIndntN"]).Text = System.Resources.ValidationResources.IDt.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["GrIndntN"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text17"]).Text = System.Resources.ValidationResources.rptDetailsofthePublication.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text17"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text11"]).Text = System.Resources.ValidationResources.LLname.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text11"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text10"]).Text = System.Resources.ValidationResources.LFname.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text10"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text9"]).Text = System.Resources.ValidationResources.LMname.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text9"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text8"]).Text = System.Resources.ValidationResources.LTitle.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text8"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text7"]).Text = System.Resources.ValidationResources.BkSeries.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text7"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text6"]).Text = System.Resources.ValidationResources.rptPlace.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text6"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text42"]).Text = System.Resources.ValidationResources.Title_publisher.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text42"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text5"]).Text = System.Resources.ValidationResources.LEdition.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text5"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text21"]).Text = System.Resources.ValidationResources.LPubY.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text21"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text3"]).Text = System.Resources.ValidationResources.rptVolNo.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text3"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text20"]).Text = System.Resources.ValidationResources.LEditionY.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text20"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text23"]).Text = System.Resources.ValidationResources.LPrc.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text23"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text45"]).Text = System.Resources.ValidationResources.rptIsbnIssn.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text45"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text19"]).Text = System.Resources.ValidationResources.rptNoofcopiesrecommendedinwords.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text19"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text28"]).Text = System.Resources.ValidationResources.LCouNo.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text28"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text29"]).Text = System.Resources.ValidationResources.LNoStudent.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text29"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text30"]).Text = System.Resources.ValidationResources.rptReceivedonapproval.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text30"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text31"]).Text = System.Resources.ValidationResources.rptAnyothrsource.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text31"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text32"]).Text = System.Resources.ValidationResources.LCat.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text32"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text33"]).Text = System.Resources.ValidationResources.rptRecommendedby.ToString();
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text33"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text41"]).Text = System.Resources.ValidationResources.rptHeadofDeptt.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text41"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text37"]).Text = System.Resources.ValidationResources.rptSignature.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text37"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text44"]).Text = System.Resources.ValidationResources.rptSignature.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text44"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text39"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text39"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text47"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text47"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text49"]).Text = System.Resources.ValidationResources.rptFORLIBRARYUSE.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text49"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text55"]).Text = System.Resources.ValidationResources.rptCheckedby.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text55"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text50"]).Text = System.Resources.ValidationResources.rptPublicCatalogue.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text50"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text51"]).Text = System.Resources.ValidationResources.LCalN.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text51"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text54"]).Text = System.Resources.ValidationResources.AccNo.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text54"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text52"]).Text = System.Resources.ValidationResources.rptInternalStatus.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text52"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text53"]).Text = System.Resources.ValidationResources.rptrOnOrder.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text53"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text56"]).Text = System.Resources.ValidationResources.rptAwaitingPayment.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text56"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text57"]).Text = System.Resources.ValidationResources.rptUnderProcessing.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text57"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text58"]).Text = System.Resources.ValidationResources.rptCheckedby.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text58"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text59"]).Text = System.Resources.ValidationResources.rptCrossCheckedby.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text59"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text63"]).Text = System.Resources.ValidationResources.rptApproximate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text63"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text65"]).Text = System.Resources.ValidationResources.Title_vendor.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text65"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text67"]).Text = System.Resources.ValidationResources.LOrderNo.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text67"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));


        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text73"]).Text = System.Resources.ValidationResources.rptBillNo.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text73"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text69"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text69"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text66"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text66"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text71"]).Text = System.Resources.ValidationResources.rptNumberofcopiesOrdered.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text71"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text75"]).Text = System.Resources.ValidationResources.HAccNo.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text75"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text79"]).Text = System.Resources.ValidationResources.rptAsstt.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text79"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text80"]).Text = System.Resources.ValidationResources.rptDyLibrarian.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text80"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text87"]).Text = System.Resources.ValidationResources.rptLibrarian.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text87"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text86"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text86"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text85"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text85"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));

        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text83"]).Text = System.Resources.ValidationResources.LDate.ToString;
        //            ((TextObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["Text83"]).ApplyFont(new Font(System.Resources.ValidationResources.TextBox1.ToString, 9.0f));
        //            myReportDocument.DataDefinition.FormulaFields["cusDateFrmt "].Text = System.Resources.ValidationResources.rptDateFrmt.ToString;

        //            ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["cusDateFrmt1"]).ObjectFormat.EnableSuppress = true;


        //            // CrystalReportViewer1.ReportSource = myReportDocument
        //            // CrystalReportViewer1.DataBind()
        //            // CrystalReportViewer1.RefreshReport()
        //            var exportOpts1 = myReportDocument.ExportOptions;
        //            myReportDocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //            myReportDocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //            myReportDocument.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
        //            ((DiskFileDestinationOptions)myReportDocument.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\GiftIndent.pdf");
        //            myReportDocument.Export();
        //            if (strPrintorMail == "Print")
        //            {
        //                myReportDocument.Close();
        //                myReportDocument.Dispose();
        //                {
        //                    var withBlock = Response;
        //                    withBlock.ClearContent();
        //                    withBlock.ClearHeaders();
        //                    withBlock.ContentType = "application/pdf";
        //                    withBlock.AppendHeader("Content-Disposition", "attachment; filename=IndentReport.pdf");
        //                    withBlock.WriteFile(@"reportTemp\GiftIndent.pdf");
        //                    withBlock.Flush();
        //                    withBlock.Close();
        //                }
        //                File.Delete(Server.MapPath(@"reportTemp\GiftIndent.pdf"));
        //            }
        //            else
        //            {
        //                myReportDocument.SaveAs(Server.MapPath(@"reportTemp\GiftIndent.pdf"));
        //                myReportDocument.Close();
        //                myReportDocument.Dispose();
        //                // CrystalReportViewer1.Visible = False
        //            }
        //            this.hdReport.Value = "0";
        //        }
        //        else
        //        {
        //            LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString(), this);
        //            msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString(), this);
        //            // message.PageMesg(Resources.ValidationResources.rNotFound.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Warning)
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msglabel.Visible = true;
        //        msglabel.Text = ex.Message;

        //    }
        //}
        //AuditIndentMasterReport(ByVal tran As OleDbTransaction, ByVal strAction As String)
        //spUnderTrans2



        //no validation yet on api yet
        //cmdsave_ServerClick(ByVal sender As System.Object, ByV

        //actionPermission("SELECT actionL....
        //  UserPermission/ GetActionPermOnMember  


        //select checkBudget.... libsetup avail above

        //dm.CommandText = "SELECT allocatedamount,approvalcommitedamt+nonapprovalcommitedamt AS amt,Ven.....
        // indent/ GetAllocateBudget


        //populateRequesters((ByVal depCode As Integer, ...
        //circuser/GetCircUserLimited

        // Sub genrateindentID(ByVal Conn As Ol
        //indent/GetMaxIndentId   get call

        //Sub GenearteItemID(ByVal indenNo As String,...
        //indent/GetMaxItemNo   get call


        //Public Sub genrateindent(By
        //basic/DepartmentById  pass code get call

        //Private Sub cmddelete_ServerClick(By
        //indent/DeleteIndentItem  and DeleteIndent 

        //Public Sub GetData2(ByVal  
        // take api and do it 




    }
}