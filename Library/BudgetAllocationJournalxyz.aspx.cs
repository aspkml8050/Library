using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using Library.App_Code.MultipleFramworks;
using Library.App_Code.CSharp;

namespace Library
{
    public partial class BudgetAllocationJournalxyz : BaseClass
    {
        private static string tmpcondition;
        private DataTable dt = new DataTable();
        private insertLogin LibObj1 = new insertLogin();
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private System.Drawing.Color FR;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();
        private string strResult = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            msglabel.Visible = false;
            var budgetcon = new OleDbConnection(retConstr(""));
            budgetcon.Open();
            try
            {
                hdnGrdId.Value = grddetail.ClientID;

                CalculateValues(budgetcon);
                if (!IsPostBack)
                {
                    LibObj.populateDDL(itemC, "select * from CategoryLoadingStatus", "Category_LoadingStatus", "Id", Resources.ValidationResources.ComboSelect, budgetcon);
                    // cmdReturn.CausesValidation = False
                    cmdreset.CausesValidation = false;
                    // Me.cmbdepartment.Focus()
                    this.SetFocus(cmbdepartment);
                    lbltitle.Text = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        this.cmdsave.Visible = false;
                        this.cmddelete.Visible = false;
                        this.BtnPrint.Enabled = true;
                    }
                    else
                    {
                        this.cmdsave.Visible = true;
                        this.cmddelete.Visible = true;
                        this.BtnPrint.Enabled = false;
                    }
                    FillDeptCombo(budgetcon);
                    Label1.Text = Label1.Text + " " + "-" + "(" + Session["session"] + ")";
                    // :::::::::::::
                    //string result;
                    //result = LibObj.getCurrency1(budgetcon);
                    //lblCurrency.Text = lblCurrency.Text + " in " + result;
                    // ::::
                    txtallocatedamount.Text = string.Empty;
                    cmdsave.Visible = true;
                    this.cmddelete.Visible = true;
                    this.BtnPrint.Enabled = false;
                    
                    var budgetallocationds = new DataSet();
                    // budgetallocationda.Fill(budgetallocationds, "BudgetAllocationJournal")
                    budgetallocationds = LibObj.PopulateDataset("SELECT CategoryLoadingStatus.Category_LoadingStatus,BudgetAllocationJournal.departmentcode,BudgetAllocationJournal.allocated_amount,BudgetAllocationJournal.expended_amount,BudgetAllocationJournal.committed_amount,BudgetAllocationJournal.balance,InstituteMaster.ShortName + '-' + departmentmaster.departmentname as departmentname,case BudgetAllocationJournal.status when 1 then 'Allowed' else 'Not Allowed' end as status FROM institutemaster,BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode left outer join CategoryLoadingStatus on CategoryLoadingStatus.Id=BudgetAllocationJournal.Itemcategoryid  where departmentmaster.institutecode=institutemaster.institutecode and academic_session=N'" + Session["session"]+ "' ORDER BY InstituteMaster.ShortName + '-' + departmentmaster.departmentname", "BudgetAllocationJournal", budgetcon);
                    var com = new OleDbCommand();
                    com.Connection = budgetcon;
                    com.CommandType = CommandType.Text;
                    com.CommandText = Convert.ToString("SELECT sum(BudgetAllocationJournal.allocated_amount) AS allocated_amount, sum(BudgetAllocationJournal.expended_amount) AS expended_amount, sum(BudgetAllocationJournal.committed_amount) AS committed_amount, sum(BudgetAllocationJournal.balance) AS balance FROM BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode where academic_session=N'"+ Session["session"]+ "'");
                    OleDbDataReader r1;
                    r1 = com.ExecuteReader();
                    r1.Read();
                    DataRow dr;

                    try
                    {
                        dt = budgetallocationds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
                    }

                    dr = dt.NewRow();
                    dr["departmentName"] = "TOTAL";
                    dr["departmentcode"] = 1000000;
                    dr["allocated_amount"] = r1[0];
                    dr["expended_amount"] = r1[1];
                    dr["committed_amount"] = r1[2];
                    dr["balance"] = r1[3];
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    grddetail.DataSource = dt;
                    grddetail.DataBind();
                    cmdsave.Visible = true;
                    hdnGrdId.Value = grddetail.ClientID;



                    r1.Close();
                    budgetallocationds.Dispose();
                    // budgetallocationda.Dispose()
                    // budgetallocationda.Dispose()
                    // budgetcon.Close()
                }
            }
            // Exit Sub
            // fail:
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (budgetcon.State == ConnectionState.Open)
                {
                    budgetcon.Close();
                }
                budgetcon.Dispose();
            }

        }

        protected void cmdreset_Click(object sender, System.EventArgs e)
        {

            var budgetcon = new OleDbConnection(retConstr(""));
            budgetcon.Open();
            try
            {
                if (tmpcondition == "Y")
                {
                    this.cmdsave.Visible = false;
                    this.cmddelete.Visible = false;
                    this.BtnPrint.Enabled = true;
                }
                else
                {
                    this.cmdsave.Visible = true;
                    this.cmddelete.Visible = true;
                    this.BtnPrint.Enabled = false;
                }
                cmbstatus.SelectedIndex = 0;
                itemC.SelectedIndex = itemC.Items.Count - 1;
                cmdsave.Text = Resources.ValidationResources.bSave.ToString();
                txtallocatedamount.Text = string.Empty;
                this.cmddelete.Visible = true;
                this.BtnPrint.Enabled = false;
                FillDeptCombo(budgetcon);
                this.cmbdepartment.Enabled = true;
                // Me.cmbdepartment.Focus()
                SetFocus(cmbdepartment);
                grddetail.SelectedIndex = -1;
                hidDeptID.Value = "";
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (budgetcon.State == ConnectionState.Open)
                {
                    budgetcon.Close();
                }
                budgetcon.Dispose();
            }
        }


        protected void grddetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            // On Error GoTo fail
            var budgetmastercon = new OleDbConnection(retConstr(""));
            budgetmastercon.Open();
            var budgetds = new DataSet();
            try
            {
                // grddetail.Focus()
                SetFocus(this.grddetail);
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            if (Convert.ToSingle(this.grddetail.Items[e.Item.ItemIndex].Cells[1].Text) != 1000000)
                            {

                                string search_query;
                                search_query = "SELECT departmentcode FROM BudgetAllocationJournal WHERE departmentcode=" + grddetail.Items[e.Item.ItemIndex].Cells[1].Text + " and (expended_amount<>'0' or committed_amount<>'0') and academic_session=N'" + LoggedUser.Logged().Session + "'";

                               
                                budgetds = LibObj.PopulateDataset(search_query, "BudgetAllocationJournal", budgetmastercon);
                                if (budgetds.Tables["BudgetAllocationJournal"].Rows.Count > 0)
                                {
                                   
                                    message.PageMesg(Resources.ValidationResources.RecordNoUp.ToString(), this, dbUtilities.MsgLevel.Warning);
                                    // cmbdepartment.Focus()
                                    SetFocus(cmbdepartment);
                                    return;
                                }
                                cmbdepartment.Enabled = false;
                                this.cmddelete.Visible = false;
                                this.BtnPrint.Enabled = true;
                                budgetds = LibObj.PopulateDataset("Select ItemCategoryId,departmentcode,allocated_amount,status from BudgetAllocationJournal where BudgetAllocationJournal.departmentcode=" + grddetail.Items[e.Item.ItemIndex].Cells[1].Text + " and academic_session=N'" + LoggedUser.Logged().Session + "'", "budgetAllocationJournal", budgetmastercon);

                                var deptds = new DataSet();
                                deptds = LibObj.PopulateDataset("select InstituteMaster.ShortName + '-' + departmentname as departmentname,departmentcode from departmentmaster,institutemaster where departmentmaster.institutecode=institutemaster.institutecode and departmentcode = '"+ budgetds.Tables["budgetAllocationJournal"].Rows[0][1]+ "'", "dept", budgetmastercon);
                                if (budgetds.Tables["budgetAllocationJournal"].Rows.Count > 0)
                                {
                                    // cmbdepartment.SelectedIndex = cmbdepartment.Items.IndexOf(cmbdepartment.Items.FindByValue(budgetds.Tables("budgetAllocationJournal").Rows(0).Item(0)))
                                    cmbdepartment.SelectedItem.Text = deptds.Tables["dept"].Rows[0][0].ToString();
                                    if (Convert.ToBoolean(budgetds.Tables["budgetAllocationJournal"].Rows[0][0]))
                                    {
                                        itemC.SelectedIndex = Convert.ToBoolean(budgetds.Tables["budgetAllocationJournal"].Rows[0][0]) ? itemC.Items.Count - 1 : Convert.ToInt32(budgetds.Tables["budgetAllocationJournal"].Rows[0][0]);
                                    }

                                    else if (string.IsNullOrEmpty(budgetds.Tables["budgetAllocationJournal"].Rows[0][0].ToString()))
                                    {
                                        itemC.SelectedIndex = budgetds.Tables["budgetAllocationJournal"].Rows[0][0].ToString() == "" ? itemC.Items.Count - 1 : Convert.ToInt32(budgetds.Tables["budgetAllocationJournal"].Rows[0][0]);
                                    }
                                    else
                                    {
                                        itemC.SelectedIndex = Convert.ToInt32(budgetds.Tables["budgetAllocationJournal"].Rows[0][0]);
                                    }

                                    txtallocatedamount.Text = budgetds.Tables["budgetAllocationJournal"].Rows[0][2].ToString();
                                    cmbstatus.SelectedIndex = Convert.ToInt32(budgetds.Tables["budgetAllocationJournal"].Rows[0][3]);
                                    hidDeptID.Value = budgetds.Tables["budgetAllocationJournal"].Rows[0][1].ToString();
                                    
                                    cmdsave.Text = Resources.ValidationResources.bUpdate.ToString();
                                    // Me.txtallocatedamount.Focus()go 
                                    SetFocus(txtallocatedamount);
                                }
                            }

                            break;
                        }
                }
            }
            // Exit Sub
            // fail:
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (budgetmastercon.State == ConnectionState.Open)
                {
                    budgetmastercon.Close();
                }
                budgetmastercon.Dispose();
                budgetds.Dispose();
            }
        }

        protected void cmdsave_Click(object sender, System.EventArgs e)
        {
            // On Error GoTo Fail

            if (cmbdepartment.SelectedItem.Text == "---Select---")
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Select Department", Me)
                message.PageMesg("Select Department", this, dbUtilities.MsgLevel.Warning);
                // cmbdepartment.Focus()
                SetFocus(cmbdepartment);
                return;

            }
            var budgetcon = new OleDbConnection(retConstr(""));
            var budgetcom = new OleDbCommand();
            budgetcon.Open();
            var budgetds = new DataSet();
            try
            {
                string search_query;
                search_query = "SELECT departmentcode FROM BudgetAllocationJournal WHERE departmentcode=" + this.cmbdepartment.SelectedItem.Value + " and (expended_amount<>'0' or committed_amount<>'0') and academic_session=N'" + Session["session"] + "'";
                // Dim budgetds As New DataSet
                // Dim budgetda As New OleDbDataAdapter(search_query, budgetcon)
                // budgetda.Fill(budgetds, "BudgetAllocationJournal")
                budgetds = LibObj.PopulateDataset(search_query, "BudgetAllocationJournal", budgetcon);
                if (budgetds.Tables["BudgetAllocationJournal"].Rows.Count > 0)
                {
                    // Hidden3.Value = "Show1"
                    // LibObj.MsgBox1(Resources.ValidationResources.RecordNoUp.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.RecordNoUp.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.RecordNoUp.ToString(), this, dbUtilities.MsgLevel.Warning);
                    // cmbdepartment.Focus()
                    SetFocus(cmbdepartment);
                    return;
                }
                budgetcom.CommandType = CommandType.StoredProcedure;
                budgetcom.Connection = budgetcon;
                budgetcom.CommandText = "insert_BudgetAllocJournal_1";
                var departmentcodeds = new DataSet();
                departmentcodeds = LibObj.PopulateDataset("select departmentcode from (select InstituteMaster.ShortName + '-' + departmentname as departmentname,departmentcode from departmentmaster,institutemaster where departmentmaster.institutecode=institutemaster.institutecode) as x where x.departmentname = '" + cmbdepartment.SelectedItem.Text + "'", "tbl1", budgetcon);
                budgetcom.Parameters.Add(new OleDbParameter("@departmentcode_1", OleDbType.Integer));
                budgetcom.Parameters["@departmentcode_1"].Value = Convert.ToInt32(departmentcodeds.Tables["tbl1"].Rows[0][0]);
                if (this.txtallocatedamount.Text == string.Empty)
                {
                    this.txtallocatedamount.Text = "0.0";
                }
                budgetcom.Parameters.Add(new OleDbParameter("@allocated_amount_2", OleDbType.Decimal));
                budgetcom.Parameters["@allocated_amount_2"].Value = Convert.ToDecimal(txtallocatedamount.Text);

                budgetcom.Parameters.Add(new OleDbParameter("@expended_amount_3", OleDbType.Decimal));
                budgetcom.Parameters["@expended_amount_3"].Value = 0;

                budgetcom.Parameters.Add(new OleDbParameter("@committed_amount_4", OleDbType.Decimal));
                budgetcom.Parameters["@committed_amount_4"].Value = 0;

                budgetcom.Parameters.Add(new OleDbParameter("@balance_5", OleDbType.Decimal));
                budgetcom.Parameters["@balance_5"].Value = 0;

                budgetcom.Parameters.Add(new OleDbParameter("@status_6", OleDbType.SmallInt));
                budgetcom.Parameters["@status_6"].Value = cmbstatus.SelectedItem.Text == "Allowed" ? 1 : 0;
                budgetcom.Parameters.Add(new OleDbParameter("@ItemCategory", OleDbType.Integer));
                budgetcom.Parameters["@ItemCategory"].Value = itemC.SelectedIndex == itemC.Items.Count - 1 ? default : itemC.SelectedIndex;

                budgetcom.Parameters.Add(new OleDbParameter("@academic_session_7", OleDbType.VarWChar));
                budgetcom.Parameters["@academic_session_7"].Value = LoggedUser.Logged().Session;

                budgetcom.Parameters.Add(new OleDbParameter("@userid_8", OleDbType.VarWChar));
                budgetcom.Parameters["@userid_8"].Value = LoggedUser.Logged().User_Id;

                budgetcom.ExecuteNonQuery();
                budgetcom.Parameters.Clear();
                // budgetcom.Dispose()


                // Dim temp As String = String.Empty
                budgetcom.Parameters.Clear();
                if (LoggedUser.Logged().IsAudit == "Y")
                {
                    if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, "BudgetAllocationJournal", LoggedUser.Logged().Session, cmbdepartment.SelectedItem.Text, Resources.ValidationResources.Insert.ToString(), retConstr(""));
                    }
                    else
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, "BudgetAllocationJournal", LoggedUser.Logged().Session, cmbdepartment.SelectedItem.Text, Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                        cmbdepartment.Enabled = true;
                    }
                }



               
                CalculateValues(budgetcon);
                budgetds = LibObj.PopulateDataset("SELECT CategoryLoadingStatus.Category_LoadingStatus,BudgetAllocationJournal.departmentcode,BudgetAllocationJournal.allocated_amount,BudgetAllocationJournal.expended_amount,BudgetAllocationJournal.committed_amount,BudgetAllocationJournal.balance,InstituteMaster.ShortName + '-' + departmentmaster.departmentname as departmentname,case BudgetAllocationJournal.status when 1 then 'Allowed' else 'Not Allowed' end as status FROM institutemaster,BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode left outer join CategoryLoadingStatus on CategoryLoadingStatus.Id=BudgetAllocationJournal.Itemcategoryid where departmentmaster.institutecode=institutemaster.institutecode and academic_session=N'"+ Session["session"] + "' ORDER BY InstituteMaster.ShortName + '-' + departmentmaster.departmentname ", "BudgetAllocationJournal", budgetcon);
                // Dim com As New OleDb.OleDbCommand
                budgetcom.Connection = budgetcon;
                budgetcom.CommandType = CommandType.Text;
                budgetcom.CommandText = Convert.ToString("SELECT sum(BudgetAllocationJournal.allocated_amount) AS allocated_amount, sum(BudgetAllocationJournal.expended_amount) AS expended_amount, sum(BudgetAllocationJournal.committed_amount) AS committed_amount, sum(BudgetAllocationJournal.balance) AS balance FROM BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode where  academic_session=N'" + Session["session"] + "'");
                OleDbDataReader r1;
                r1 = budgetcom.ExecuteReader();
                r1.Read();
                DataRow dr;
                dt = budgetds.Tables["BudgetAllocationJournal"];
                dr = dt.NewRow();
                dr["departmentName"] = "TOTAL";
                dr["departmentcode"] = 1000000;
                dr["allocated_amount"] = r1[0];
                dr["expended_amount"] = r1[1];
                dr["committed_amount"] = r1[2];
                dr["balance"] = r1[3];
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                grddetail.DataSource = dt;
                grddetail.DataBind();
                hdnGrdId.Value = grddetail.ClientID;

                // budgetmasterds.Dispose()
                // budgetmasterda.Dispose()

                cmbdepartment.SelectedIndex = cmbdepartment.Items.Count - 1;
                txtallocatedamount.Text = "0.00";
                // Hidden1.Value = 1
                // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                // cmbdepartment.Focus()
                SetFocus(cmbdepartment);
                // *************Bi**************
                cmdsave.Text = Resources.ValidationResources.bSave.ToString();
                this.cmddelete.Visible = true;
                this.BtnPrint.Enabled = false;
                // ******************************
                cmbstatus.SelectedIndex = 0;
            }
            
            catch (Exception ex)
            {
                
                message.PageMesg(Resources.ValidationResources.UnsaveBudInfo.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (budgetcon.State == ConnectionState.Open)
                {
                    budgetcon.Close();
                }
                budgetcon.Dispose();
                budgetcom.Dispose();
                budgetds.Dispose();
            }

        }

        public void FillDeptCombo(OleDbConnection deptcon)
        {

            LibObj.populateDDL(cmbdepartment, "SELECT InstituteMaster.ShortName + '-' + departmentname as departmentname,departmentcode FROM institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode ORDER BY InstituteMaster.ShortName + '-' + departmentname", "departmentname", "departmentcode", HComboSelect.Value, deptcon);
        }

        public object CalculateValues(OleDbConnection budgetcon)
        {
            // On Error GoTo Fail
            msglabel.Visible = false;
            cmdreset.CausesValidation = false;
            // FillDeptCombo()
            // Dim budgetcon As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
            // budgetcon.Open()
            // Dim budgetda As New OleDbDataAdapter("SELECT departmentcode as dcode,allocated_amount as a_amount,expended_amount as e_amount,committed_amount as c_amount FROM BudgetAllocationJournal where  academic_session='" & Session("session") & "'", budgetcon)
            var budgetds = new DataSet();
            // budgetda.Fill(budgetds, "Budget")
            budgetds = LibObj.PopulateDataset("SELECT departmentcode as dcode,allocated_amount as a_amount,expended_amount as e_amount,committed_amount as c_amount FROM BudgetAllocationJournal where  academic_session=N'"+ Session["session"]+ "'", "Budget", budgetcon);

            int DepartmentCode;
            decimal AllocatedAmount;
            decimal ExpendedAmount;
            decimal CommittedAmount;
            decimal Balance;
            int count = budgetds.Tables["Budget"].Rows.Count;
            if (budgetds.Tables["Budget"].Rows.Count > 0)
            {
                string update_query;
                int i;
                var loopTo = count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    var budgetcom = new OleDbCommand();
                    DepartmentCode = Convert.ToInt32(budgetds.Tables["Budget"].Rows[i]["dcode"]);
                    AllocatedAmount = Convert.ToDecimal(budgetds.Tables["Budget"].Rows[i]["a_amount"]);
                    ExpendedAmount = Convert.ToDecimal(budgetds.Tables["Budget"].Rows[i]["e_amount"]);
                    CommittedAmount = Convert.ToDecimal(budgetds.Tables["Budget"].Rows[i]["c_amount"]);
                    Balance = AllocatedAmount - ExpendedAmount;
                    update_query = Convert.ToString("UPDATE BudgetAllocationJournal SET balance=" + Balance + ",committed_amount=" + CommittedAmount + " WHERE departmentcode=" + DepartmentCode + " and academic_session=N'"+ Session["session"]+ "'");
                    budgetcom.Connection = budgetcon;
                    budgetcom.CommandText = update_query;
                    budgetcom.ExecuteNonQuery();
                    budgetcom.Dispose();
                }
            }
            // budgetcon.Dispose()
            else
            {
                return default;
            }
            return default;
            // Fail:
            // msglabel.Visible = True
            // msglabel.Text = Err.Description
            //message.PageMesg(Err.Description, this, DBUTIL.dbUtilities.MsgLevel.Failure);
        }

        // Private Sub cmdReturn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReturn.ServerClick
        // Response.Redirect("frmMainControl.aspx?url=BudgetAllocationJournal.aspx?title=" & lbltitle.Text & "?condition=" & tmpcondition)
        // End Sub

        protected void cmddelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                var delcon = new OleDbConnection(); // (retConStr(Session("LibWiseDBConn")))
                delcon.ConnectionString = retConstr("");
                delcon.Open();
                OleDbCommand cmd;
                // Dim budgetds As New DataSet
                try
                {
                    // Bipin 9-08-07


                    cmd = new OleDbCommand("Select departmentcode from BudgetAllocationJournal where allocated_amount='" + this.txtallocatedamount.Text.Trim() + "'", delcon); // and status='" & Val(Trim(Me.cmbstatus.SelectedValue)) & "'
                    OleDbDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        this.txtdepartmentcode.Value = dr.GetValue(0).ToString();
                    }
                    else
                    {
                        txtdepartmentcode.Value = string.Empty;
                    }



                    if (cmbdepartment.SelectedValue.Trim() == "---Select---")
                    {
                        // Hidden3.Value = "2"
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else if (LibObj.checkChildExistancewc("departmentcode", "BudgetAllocationJournal", "departmentcode='" + this.txtdepartmentcode.Value.Trim().Replace("'", "''") + "'", delcon) == false)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelNotExist.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else
                    {
                        var delcom = new OleDbCommand("delete from BudgetAllocationJournal where departmentcode='" + cmbdepartment.SelectedValue + "' and academic_session=N'" + Session["session"] + "'", delcon);
                        delcom.CommandType = CommandType.Text;
                        delcom.ExecuteNonQuery();
                        // delcom.Dispose()
                        // Hidden3.Value = "5"
                        // LibObj.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);
                        // Dim temp As String = String.Empty
                        delcom.Parameters.Clear();
                        if (LoggedUser.Logged().IsAudit == "Y")
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, "BudgetAllocationJournal", LoggedUser.Logged().Session, cmbdepartment.SelectedItem.Text, Resources.ValidationResources.bDelete.ToString(), retConstr(""));
                        }

                        delcom.Dispose();
                    }
                    cmdreset_Click(sender, e);
                    // LibObj.SetFocus("cmbdepartment", Me)
                    // Me.cmbdepartment.Focus()
                    this.SetFocus(cmbdepartment);
                    FillGrid();
                    // hdTop.Value = "top"
                    return;
                }
                catch (Exception ex)
                {
                    // Me.msglabel.Visible = True
                    // Me.msglabel.Text = ex.Message
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public void FillGrid()
        {
            var delcon1 = new OleDbConnection();
            delcon1.ConnectionString = retConstr("");
            delcon1.Open();
            var budgetallocationds = new DataSet();
            try
            {


               
                budgetallocationds = LibObj.PopulateDataset("SELECT CategoryLoadingStatus.Category_LoadingStatus, BudgetAllocationJournal.departmentcode,BudgetAllocationJournal.allocated_amount,BudgetAllocationJournal.expended_amount,BudgetAllocationJournal.committed_amount,BudgetAllocationJournal.balance,InstituteMaster.ShortName + '-' + departmentmaster.departmentname as departmentname,case BudgetAllocationJournal.status when 1 then 'Allowed' else 'Not Allowed' end as status FROM institutemaster,BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode left outer join CategoryLoadingStatus on CategoryLoadingStatus.Id=BudgetAllocationJournal.Itemcategoryid where departmentmaster.institutecode=institutemaster.institutecode and academic_session=N'" + Session["session"]+ "' order by InstituteMaster.ShortName + '-' + departmentmaster.departmentname", "BudgetAllocationJournal", delcon1);
                var com = new OleDbCommand();
                com.Connection = delcon1;
                com.CommandType = CommandType.Text;
                com.CommandText = Convert.ToString("SELECT sum(BudgetAllocationJournal.allocated_amount) AS allocated_amount, sum(BudgetAllocationJournal.expended_amount) AS expended_amount, sum(BudgetAllocationJournal.committed_amount) AS committed_amount, sum(BudgetAllocationJournal.balance) AS balance FROM BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode where  academic_session=N'"+ Session["session"]+ "'");
                OleDbDataReader r1;
                r1 = com.ExecuteReader();
                r1.Read();
                DataRow dr;
                dt = budgetallocationds.Tables[0];
                dr = dt.NewRow();
                dr["departmentName"] = "TOTAL";
                dr["departmentcode"] = 1000000;
                dr["allocated_amount"] = r1[0];
                dr["expended_amount"] = r1[1];
                dr["committed_amount"] = r1[2];
                dr["balance"] = r1[3];
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                grddetail.DataSource = dt;
                grddetail.DataBind();
                hdnGrdId.Value = grddetail.ClientID;

                com.Dispose();

                // budgetallocationda.Dispose()
                // budgetallocationda.Dispose()
                delcon1.Close();
            }
            catch (Exception ex)
            {
                if (delcon1.State == ConnectionState.Open)
                {
                    delcon1.Close();
                }
                delcon1.Dispose();
                budgetallocationds.Dispose();
            }
        }
        protected void grddetail_PageIndexChanged1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            var com = new OleDbCommand();
            var deptmasterds = new DataSet();
            try
            {
                SetFocus(grddetail);
                string searchqry;
                searchqry = Convert.ToString("SELECT CategoryLoadingStatus.Category_LoadingStatus ,BudgetAllocationJournal.departmentcode,BudgetAllocationJournal.allocated_amount,BudgetAllocationJournal.expended_amount,BudgetAllocationJournal.committed_amount,BudgetAllocationJournal.balance,InstituteMaster.ShortName + '-' + departmentmaster.departmentname as departmentname,case BudgetAllocationJournal.status when '1' then 'Allowed' else 'Not Allowed' end as status FROM institutemaster,BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode left outer join CategoryLoadingStatus on CategoryLoadingStatus.Id=BudgetAllocationJournal.Itemcategoryid where departmentmaster.institutecode=institutemaster.institutecode and academic_session=N'"+ Session["session"]+ "' ORDER BY InstituteMaster.ShortName + '-' + departmentmaster.departmentname");
                deptmasterds = LibObj.PopulateDataset(searchqry, "BudgetAllocationJournal", deptmastercon);
                com.Connection = deptmastercon;
                com.CommandType = CommandType.Text;
                com.CommandText = Convert.ToString("SELECT sum(BudgetAllocationJournal.allocated_amount) AS allocated_amount, sum(BudgetAllocationJournal.expended_amount) AS expended_amount, sum(BudgetAllocationJournal.committed_amount) AS committed_amount, sum(BudgetAllocationJournal.balance) AS balance FROM BudgetAllocationJournal INNER JOIN departmentmaster ON BudgetAllocationJournal.departmentcode = departmentmaster.departmentcode where academic_session=N'"+ Session["session"]+ "'");
                OleDbDataReader r1;
                r1 = com.ExecuteReader();
                r1.Read();
                DataRow dr;
                var dt = deptmasterds.Tables["BudgetAllocationJournal"];
                dr = dt.NewRow();
                dr["departmentName"] = "TOTAL";
                dr["departmentcode"] = 1000000;
                dr["allocated_amount"] = r1[0];
                dr["expended_amount"] = r1[1];
                dr["committed_amount"] = r1[2];
                dr["balance"] = r1[3];
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                var dv = new DataView(dt);
                grddetail.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = grddetail.Attributes["departmentcode"];
                grddetail.DataSource = dv;
                grddetail.DataBind();
                hdnGrdId.Value = grddetail.ClientID;

                dt.Dispose();
                dv.Dispose();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                com.Dispose();
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                deptmasterds.Dispose();
                deptmastercon.Dispose();
            }
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var deptmastercon = new OleDbConnection(retConstr(""));
                deptmastercon.Open();
                var MyDA = new OleDbDataAdapter();
                var myDS = new DataSet();
                var com1 = new OleDbCommand();
                com1.Connection = deptmastercon;
                com1.CommandType = CommandType.Text;
                com1.CommandText = "select institutename,libraryname,city,state,phoneno,fax,email from librarysetupinformation";
                MyDA.SelectCommand = com1;
                MyDA.Fill(myDS, "ccp1");

                var com = new OleDbCommand();




                com.Connection = deptmastercon;
                com.CommandType = CommandType.Text;
                com.CommandText = "select CategoryLoadingStatus.Category_LoadingStatus,BudgetAllocationJournal.allocated_amount,BudgetAllocationJournal.balance,BudgetAllocationJournal.committed_amount,BudgetAllocationJournal.expended_amount,departmentmaster.departmentname  from BudgetAllocationJournal,departmentmaster,CategoryLoadingStatus where departmentmaster.departmentcode=BudgetAllocationJournal.departmentcode and   CategoryLoadingStatus.Id = BudgetAllocationJournal.ItemCategoryId and BudgetAllocationJournal.departmentcode='" + hidDeptID.Value + "'";


                MyDA.SelectCommand = com;

                MyDA.Fill(myDS, "ccp");
                if (myDS.Tables["ccp"].Rows.Count > 0)
                {
                    var O_rpt = new ReportDocument();
                    O_rpt.Load(Server.MapPath(@"Reports\BudgetAllocationJournal.rpt"));
                    O_rpt.SetDataSource(myDS);
                    // CrystalReportViewer1.ReportSource = O_rpt
                    // CrystalReportViewer1.DataBind()
                    // CrystalReportViewer1.RefreshReport()
                    var exportOpts1 = O_rpt.ExportOptions;
                    O_rpt.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    O_rpt.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    O_rpt.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)O_rpt.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\BudgetAllocationJournal.pdf");
                    O_rpt.Export();
                    O_rpt.Close();
                    O_rpt.Dispose();

                    {
                        var withBlock = Response;
                        withBlock.ClearContent();
                        withBlock.ClearHeaders();
                        withBlock.ContentType = "application/pdf";
                        withBlock.AppendHeader("Content-Disposition", "attachment; filename=BudgetAllocationJournal.pdf");
                        withBlock.WriteFile(@"reportTemp\BudgetAllocationJournal.pdf");
                        withBlock.Flush();
                        withBlock.Close();
                    }
                    File.Delete(Server.MapPath(@"reportTemp\BudgetAllocationJournal.pdf"));
                }
                deptmastercon.Close();
                deptmastercon.Dispose();
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
    }
}