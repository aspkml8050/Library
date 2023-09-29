using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class BudgetAdjustmentJournal : BaseClass, ICallbackEventHandler
    {
        private insertLogin LibObj1 = new insertLogin();
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        private string strResult = "";
        public string GetCallbackResult()
        {
            return strResult;
        }
        public void RaiseCallbackEvent(string eventArgument)
        {
            // '''''''''''''''''''''''shweta
            string s1, s2;
            try
            {
                var budgetcon1 = new OleDbConnection(retConstr(""));
                budgetcon1.Open();
                if (eventArgument == HComboSelect.Value)
                {
                    txtallocatedamount.Value = string.Empty;
                    txtbalance.Value = string.Empty;
                    return;
                }
                var budgetds1 = new DataSet();
                var budgetda1 = new OleDbDataAdapter("select sum(BudgetAllocationJournal.allocated_amount) as allocatedamount,sum(BudgetAllocationJournal.allocated_amount - BudgetAllocationJournal.expended_amount) AS balence from BudgetAllocationJournal where departmentcode=" + eventArgument + " and academic_session=N'" + Session["session"] + "'", budgetcon1);
                budgetda1.Fill(budgetds1, "BudgetDS");
                if (budgetds1.Tables[0].Rows.Count > 0)
                {

                    s1 = Convert.ToString(budgetds1.Tables[0].Rows[0]["allocatedamount"]);
                    s2 = Convert.ToString(budgetds1.Tables[0].Rows[0]["balence"]);
                    strResult = s1 + "|" + s2;
                    return;
                }
                else
                {
                    strResult = "T";
                    return;
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        
        {
            try
            {
                // ''''''''''''''''''''''''shweta
                string cbref = Page.ClientScript.GetCallbackEventReference(this, "arg", "clientback", "context");
                string cbScr = string.Format("function UseCallBack(arg, context) {{ {0}; }} ", cbref);
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "UseCallBack", cbScr, true);

                // ''''''''''''''''''''''''shweta
                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)

                // cmdReturn.CausesValidation = False
                cmdreset.CausesValidation = false;

                msglabel.Text = string.Empty;
                msglabel.Visible = false;

                cmdsave.Attributes.Add("ServerClick", "return cmdsave_Click();");
                if (!IsPostBack)
                {
                    // LibObj.SetFocus("txtdate", Me)
                    //this.SetFocus(txtdate);
                    hdTop.Value = "top";
                    lblTitle.Text = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        this.cmdsave.Visible = false;
                    }
                    else
                    {
                        this.cmdsave.Visible = true;
                    }
                    Label3.Text = Label3.Text + " " + "-" + "(" + Session["session"] + ")";
                    FillDeptCombo();
                    optalc.Checked = true;
                    optdealc.Checked = false;
                    lbldeptsearch.Visible = true;
                    cmddeptsearch.Visible = true;
                    chkSearch.Checked = false;
                    lbldeptsearch.Visible = false;
                    cmddeptsearch.Visible = false;
                    txtdate.Text = String.Format("{0:yyyy-MM-dd}",System.DateTime.Today);
                     Gridrefresh();
                    DataGrid1.Visible = false;
                    this.cmdsave.Visible = true;
                    
                    //hdCulture.Value = Request.Cookies["UserCulture"].Value;
                    //lblCurrency.Text = LibObj.getCurrency("");
                    Label8.Text = LibObj.getCurrency(retConstr(""));
                    Label9.Text = LibObj.getCurrency(retConstr(""));
                    var budgetcon = new OleDbConnection(retConstr(""));
                    budgetcon.Open();
                    budgetcon.Close();
                    budgetcon.Dispose();
                }
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }


        public void FillDeptCombo()
        {
            var deptcon = new OleDbConnection(retConstr(""));
            deptcon.Open();
            var deptDs = new DataSet();
            var deptDa = new OleDbDataAdapter("select InstituteMaster.ShortName + '-' + departmentname as departmentname,BudgetAllocationJournal.departmentcode from BudgetAllocationJournal,departmentmaster,institutemaster where BudgetAllocationJournal.departmentcode=departmentmaster.departmentcode and departmentmaster.institutecode=institutemaster.institutecode and  academic_session=N'"+ Session["session"]+ "' order by InstituteMaster.ShortName + '-' + departmentname", deptcon);

            deptDa.Fill(deptDs, "deptDs");
            cmbdept.DataSource = deptDs;
            cmbdept.DataTextField = "departmentname";
            cmbdept.DataValueField = "departmentcode";
            cmbdept.DataBind();
            cmbdept.Items.Add(HComboSelect.Value);
            cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
            deptDa.Dispose();
            deptDs.Dispose();
            deptcon.Close();
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbdept.SelectedItem.Text == "---Select---")
                {
                    message.PageMesg("Select Department", this, dbUtilities.MsgLevel.Warning);
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Select Department", Me)
                    return;

                }
                // LibObj.SetFocus("txtdate", Me)
                // Me.SetFocus(txtdate)
                // hdTop.Value = "top"
                if (Convert.ToDecimal(txtamount.Value) <= 0)
                {
                    // LibObj.MsgBox1("Amount Should not be less or equal to zero.!", Me)
                    message.PageMesg("Amount Should not be less or equal to zero.!", this, dbUtilities.MsgLevel.Warning);
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Amount Should not be less or equal to zero.!", Me)
                    return;
                }
                if (optdealc.Checked == true)
                {
                    if (Convert.ToDecimal(txtamount.Value) > Convert.ToDecimal(txtbalance.Value))
                    {
                        // Hidden7.Value = "Balance"
                        // LibObj.MsgBox1(Resources.ValidationResources.BalGrtAddAmt.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.BalGrtAddAmt.ToString(), this, dbUtilities.MsgLevel.Warning);
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.BalGrtAddAmt.ToString, Me)
                        return;
                    }
                }
                var budgetcon = new OleDbConnection(retConstr(""));
                var budgetcom = new OleDbCommand();
                budgetcon.Open();
                budgetcom.CommandType = CommandType.StoredProcedure;
                budgetcom.Connection = budgetcon;
                // budgetcom.CommandText = "insert_budgetmaster_1 " & CInt(cmbdept.SelectedValue) & "," & Val(txtallocatedamt.Value) & ",0,0,0,0,0," & CInt(IIf(status.SelectedItem.Text = "Allowed", 1, 0))
                budgetcom.CommandText = "insert_BudgetAdjustmentJournal_1";
                budgetcom.Parameters.Add(new OleDbParameter("@Date_1", OleDbType.Date));
                budgetcom.Parameters["@Date_1"].Value = this.txtdate.Text;

                budgetcom.Parameters.Add(new OleDbParameter("@departmentcode_2", OleDbType.Integer));
                budgetcom.Parameters["@departmentcode_2"].Value = cmbdept.SelectedValue;
                // ''''shweta
                string f = ".";
                if (txtamount.Value == f)
                {
                    // hdUnableMsg.Value = "a"
                    // LibObj.MsgBox1(Resources.ValidationResources.CurrAmtInval.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CurrAmtInval.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.CurrAmtInval.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                budgetcom.Parameters.Add(new OleDbParameter("@Amount_3", OleDbType.Decimal));
                budgetcom.Parameters["@Amount_3"].Value = Convert.ToDecimal(txtamount.Value);

                budgetcom.Parameters.Add(new OleDbParameter("@Curr_Session_4", OleDbType.VarWChar));
                budgetcom.Parameters["@Curr_Session_4"].Value = Session["session"]; // CInt(IIf(status.SelectedItem.Text = "Allowed", 1, 0))

                budgetcom.Parameters.Add(new OleDbParameter("@Operation_5", OleDbType.VarWChar));
                if (optalc.Checked == true)
                {
                    budgetcom.Parameters["@Operation_5"].Value = "Allocate";
                }
                else if (optdealc.Checked == true)
                {
                    budgetcom.Parameters["@Operation_5"].Value = "Deallocate";
                }

                budgetcom.Parameters.Add(new OleDbParameter("@userid_6", OleDbType.VarWChar));
                budgetcom.Parameters["@userid_6"].Value = Session["user_id"];

                budgetcom.ExecuteNonQuery();
                budgetcom.Parameters.Clear();

                budgetcom.Parameters.Clear();
                budgetcom.CommandType = CommandType.Text;
                string str;
                decimal allocatedamount;
                decimal deallocamount;
                string tmpstr;
                str = "select BudgetAllocationJournal.allocated_amount as allocamount from BudgetAllocationJournal where departmentcode='" + cmbdept.SelectedValue + "' and academic_session=N'" + Session["session"] + "'";
                budgetcom.CommandText = str;
                tmpstr = Convert.ToString(budgetcom.ExecuteScalar());
                if (optalc.Checked == true)
                {
                    allocatedamount = Convert.ToDecimal(tmpstr + txtamount.Value);
                    budgetcom.Parameters.Clear();
                    budgetcom.CommandType = CommandType.Text;
                    budgetcom.CommandText = "update BudgetAllocationJournal set BudgetAllocationJournal.allocated_amount=" + allocatedamount + " where departmentcode='" + cmbdept.SelectedValue + "' and academic_session=N'" + Session["session"] + "'";
                    budgetcom.ExecuteNonQuery();
                }
                else if (optdealc.Checked == true)
                {
                    deallocamount = Convert.ToDecimal(tmpstr) - Convert.ToDecimal(txtamount.Value);
                    budgetcom.Parameters.Clear();
                    budgetcom.CommandType = CommandType.Text;
                    budgetcom.CommandText = "update BudgetAllocationJournal set BudgetAllocationJournal.allocated_amount=" + deallocamount + " where departmentcode='" + cmbdept.SelectedValue + "' and academic_session=N'" + Session["session"] + "'";
                    budgetcom.ExecuteNonQuery();
                }
                
                budgetcom.Parameters.Clear();
                if (LoggedUser.Logged().IsAudit =="Y")
                {
                    if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, "BudgetAdjustmentJournal", LoggedUser.Logged().Session, cmbdept.SelectedItem.Text, Resources.ValidationResources.Insert.ToString(), retConstr(""));
                    }
                    else
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, "BudgetAdjustmentJournal", LoggedUser.Logged().Session, cmbdept.SelectedItem.Text, Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                    }
                }

                budgetcom.Dispose();
                // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
                // LibObj1.insertLoginFunc(Session("UserName"), "BudgetAdjustmentJournal", Session("session"), cmbdept.SelectedItem.Text, "Insert", retConStr(Session("LibWiseDBConn")))
                // Else
                // LibObj1.insertLoginFunc(Session("UserName"), "BudgetAdjustmentJournal", Session("session"), cmbdept.SelectedItem.Text, "Update", retConStr(Session("LibWiseDBConn")))
                // End If
                FillDeptCombo();
                budgetcon.Dispose();
                budgetcon.Close();
                cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
                txtamount.Value = string.Empty;
                txtallocatedamount.Value = string.Empty;
                txtbalance.Value = string.Empty;
                // Hidden1.Value = 1
                // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                cmdsave.Text = Resources.ValidationResources.bSave.ToString();
                cmddeptsearch_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(Resources.ValidationResources.UnsaveBudInfo.ToString(), this, dbUtilities.MsgLevel.Failure);
                // LibObj.MsgBox1(Resources.ValidationResources.UnsaveBudInfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnsaveBudInfo.ToString, Me)
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            this.cmbdept.Enabled = true;
            optalc.Checked = true;
            optdealc.Checked = false;
            chkSearch.Checked = false;
            lbldeptsearch.Visible = false;
            cmddeptsearch.Visible = false;
            txtamount.Value = string.Empty;
            txtallocatedamount.Value = string.Empty;
            txtbalance.Value = string.Empty;
            txtdate.Text = String.Format("{0:yyyy-MM-dd}", System.DateTime.Today);
            cmdsave.Text = Resources.ValidationResources.bSave.ToString();
            // Hidden1.Value = "top"
            FillDeptCombo();
            Gridrefresh();
            DataGrid1.Visible = false;
            // LibObj.SetFocus("txtdate", Me)
            SetFocus(this.txtdate);
            hdTop.Value = "top";
            if (tmpcondition == "Y")
            {
                this.cmdsave.Visible = false;
            }
            else
            {
                this.cmdsave.Visible = true;
            }
        }

        public void FillDeptSearch()
        {
            var deptcon = new OleDbConnection(retConstr(""));
            deptcon.Open();
            var deptDs = new DataSet();
            var deptDa = new OleDbDataAdapter("select distinct InstituteMaster.ShortName + '-' + departmentname as departmentname,BudgetAdjustmentJournal.departmentcode from BudgetAdjustmentJournal,departmentmaster,institutemaster where BudgetAdjustmentJournal.departmentcode=departmentmaster.departmentcode and departmentmaster.institutecode=institutemaster.institutecode and  curr_session=N'"+ LoggedUser.Logged().Session+ "' order by InstituteMaster.ShortName + '-' + departmentname ", deptcon);
            deptDa.Fill(deptDs, "deptDs");
            cmddeptsearch.DataSource = deptDs;
            cmddeptsearch.DataTextField = "departmentname";
            cmddeptsearch.DataValueField = "departmentcode";
            cmddeptsearch.DataBind();
            cmddeptsearch.Items.Add(HComboSelect.Value);
            cmddeptsearch.SelectedIndex = cmddeptsearch.Items.Count - 1;
            deptDa.Dispose();
            deptDs.Dispose();
            deptcon.Close();
        }

        protected void chkSearch_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkSearch.Checked == true)
            {
                // chkSearch.Focus()
                SetFocus(this.chkSearch);
                lbldeptsearch.Visible = true;
                cmddeptsearch.Visible = true;
                FillDeptSearch();
            }
            else
            {
                // chkSearch.Focus()
                SetFocus(this.chkSearch);
                lbldeptsearch.Visible = false;
                cmddeptsearch.Visible = false;
                Gridrefresh();
                DataGrid1.Visible = false;
            }

        }
        
        protected void cmddeptsearch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                // cmddeptsearch.Focus()
                SetFocus(this.cmddeptsearch);
                DataGrid1.Visible = true;
                var budgetcon1 = new OleDbConnection(retConstr(""));
                budgetcon1.Open();

                this.DataGrid1.Columns[2].HeaderText = "Amount(" + LibObj.getCurrency(retConstr("")) + ")";
                var budgetds1 = new DataSet();
                var budgetda1 = new OleDbDataAdapter("select Date,Operation,Amount from BudgetAdjustmentJournal where departmentcode=" + cmddeptsearch.SelectedValue + " and curr_session=N'" + LoggedUser.Logged().Session + "' order by Date desc", budgetcon1);
                budgetda1.Fill(budgetds1, "BudgetDS");
                if (budgetds1.Tables[0].Rows.Count > 0)
                {
                    DataGrid1.DataSource = budgetds1;
                    DataGrid1.DataBind();
                    hdnGrdId.Value = DataGrid1.ClientID;
                    budgetds1.Dispose();
                    budgetds1.Dispose();
                    budgetcon1.Close();
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public void Gridrefresh()
        {
            var grd = new DataTable();
            DataGrid1.DataSource = null;
            DataGrid1.DataBind();
            hdnGrdId.Value = DataGrid1.ClientID;
            grd = null;
        }
    }
}