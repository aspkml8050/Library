using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Newtonsoft.Json;

using MoreLinq;
using System.Web.Script.Serialization;
using Model.Shared;
using Library.Models;

namespace Library
{
	public partial class DepartmentMaster : BaseClass
	{
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        protected async void Page_Load(object sender, EventArgs e)
		{
            try
            {
                hdnGrdId.Value = DataGrid1.ClientID;
                //   Page.Form.Attributes.Add("enctype", "multipart/form-data");
                // msglabel.Visible = False
                // Response.Cache.SetCacheability(HttpCacheability.NoCache)
                if (!Page.IsPostBack)
                {
                    var con = new OleDbConnection(retConstr(""));
                    con.Open();
                    var cmdk = new OleDbCommand();
                    cmdk.CommandType = CommandType.Text;
                    cmdk.Connection = con;
                    string cod = string.Empty;
                    // By Kaushal:23-Nov-11 Check Institute Creation
                    // ------------------------------------------------------
                    cmdk.CommandText = "Select Count(*) as IsInst from InstituteMaster ";
                    string IsInts = cmdk.ExecuteScalar().ToString();
                    cmdk.ExecuteNonQuery();
                    if (IsInts == "0")
                    {
                        LibObj.MsgBox1("First You should be Create Institute From Institute Master In Master!", this);
                        return;
                    }
                    // Me.ScriptManager1.SetFocus(Me.txtdepartmentname)
                    Session["NFormDW"] = null;
                    lblt1.Text = Request.QueryString["title"];
                    ViewState["openCond"] = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                    this.hdopenB.Value = Request.QueryString["openB"];
                    // Dim con As New OleDbConnection(retConStr(Session("LibWiseDBConn")))

                    LibObj.populateDDL(cmbInstName, "select ShortName,institutename,institutecode from institutemaster order by ShortName", "ShortName", "institutecode", HComboSelect.Value, con);
                    if (tmpcondition == "Y")
                    {
                        this.cmddelete.Disabled = false;
                        this.cmdsave.Disabled = false;
                    }
                    else if (tmpcondition == "N")
                    {
                        this.cmddelete.Disabled = true;
                        this.cmdsave.Disabled = true;
                    }
                    else
                    {
                        lblt1.Text = Resources.ValidationResources.Title_department;
                        this.cmdsave.Disabled = false;
                        this.cmddelete.Disabled = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    // cmdReturn.CausesValidation = False
                    // txtdepartmentcode.Visible = False
                    cmdreset.CausesValidation = false;
                    cmddelete.CausesValidation = false;
                    cmddelete.Disabled = true;
                    cmddelete2.Enabled = false;
                    
                    //  FillGrid(con);
                   await showGrid();
                    con.Close();
                    hdTop.Value = "top";
                }

            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
            }
        }
        public void clear_field()
        {
            txtdepartmentcode.Value = "";
            txtdepartmentname.Value = "";
            txtshortname.Text = "";
            hdnGrdId.Value = DataGrid1.ClientID;
        }

        private async Task showGrid()
        {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            var deptmasterds = new DataSet();
            try
            {
                // ScriptManager1.SetFocus(DataGrid1)
                string searchqry;
                searchqry = "select departmentname,departmentmaster.shortname,departmentcode,InstituteMaster.ShortName as institutename from departmentmaster,InstituteMaster where departmentmaster.Institutecode=InstituteMaster.InstituteCode order by departmentname";
                deptmasterds = LibObj.PopulateDataset(searchqry, "departmentmaster", deptmastercon);
                var dt = deptmasterds.Tables["departmentmaster"];
                var dv = new DataView(dt);
                //                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                // dv.Sort = DataGrid1.Attributes("departmentname");

                var url = LibApiUrl + "Basic/departmentbyname";
                ApiUrl(url);
                var d=await apiCall.GetStringAsync(url);
                var acd= JsonConvert.DeserializeObject<ReturnData<List<Department>>>(d);
                DataGrid1.DataSource = acd.Data;
                DataGrid1.DataBind();
                hdnGrdId.Value = DataGrid1.ClientID;
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
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                deptmasterds.Dispose();
                deptmastercon.Dispose();
            }
        }
         protected  void   cmdreset_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                hdTop.Value = "top";
                clear_field();
                this.txtshortname.ReadOnly = false;
                Session["srtname"] = "";
                Session["dptname"] = "";
                cmdsave.Value = Resources.ValidationResources.bSave;
                this.DataGrid1.SelectedIndex = -1;
                this.cmbInstName.SelectedIndex = this.cmbInstName.Items.Count - 1;
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Disabled = false;
                    this.cmdsave.Disabled = false;
                }
                else if (tmpcondition == "N")
                {
                    this.cmddelete.Disabled = true;
                    this.cmdsave.Disabled = true;
                }
                else
                {

                }
                cmddelete.Disabled = true;
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
            }
            hdnGrdId.Value = DataGrid1.ClientID;
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            var deptmasterds = new DataSet();
            OleDbTransaction tran1;
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            this.txtshortname.ReadOnly = false;
                            deptmasterds = LibObj.PopulateDataset("Select departmentname,departmentmaster.shortname,departmentcode,InstituteMaster.ShortName as institutename from departmentmaster,InstituteMaster where departmentmaster.Institutecode=InstituteMaster.InstituteCode and departmentcode=" + DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text, "DepartmentMaster", deptmastercon);
                            txtdepartmentname.Value = deptmasterds.Tables["DepartmentMaster"].Rows[0][0].ToString();
                            txtshortname.Text = deptmasterds.Tables["DepartmentMaster"].Rows[0][1].ToString();
                            this.txtdepartmentcode.Value = deptmasterds.Tables["DepartmentMaster"].Rows[0][2].ToString();
                            this.cmbInstName.SelectedIndex = this.cmbInstName.Items.IndexOf(this.cmbInstName.Items.FindByText(deptmasterds.Tables["DepartmentMaster"].Rows[0][3].ToString()));
                            cmdsave.Value = Resources.ValidationResources.bUpdate;
                            this.txtshortname.ReadOnly = false;
                            tran1 = deptmastercon.BeginTransaction();
                            OleDbCommand deptmastercom1;
                            deptmastercom1 = new OleDbCommand();
                            deptmastercom1.Connection = deptmastercon;
                            deptmastercom1.Transaction = tran1;
                            deptmastercom1.CommandType = CommandType.Text;
                            string tmpr1;
                            string str_query;
                            str_query = "select count(*) from indentmaster where departmentcode in (select departmentcode from departmentmaster where  departmentname = N'$1')";
                            str_query = str_query.Replace("$1", Convert.ToString(deptmasterds.Tables["DepartmentMaster"].Rows[0][0]));
                            deptmastercom1.CommandText = str_query;
                            tmpr1 = deptmastercom1.ExecuteScalar().ToString();
                            // deptmasterds.Dispose()
                            if (tmpr1.Trim() != "0")
                            {
                                this.txtshortname.ReadOnly = true;
                            }
                            else
                            {
                                Hidden1.Value = "0";
                            }

                            if (tmpcondition == "Y")
                            {
                                this.cmddelete.Disabled = false;
                                this.cmdsave.Disabled = false;
                            }
                            else if (tmpcondition == "N")
                            {
                                this.cmddelete.Disabled = true;
                                this.cmdsave.Disabled = true;
                            }
                            else
                            {
                                this.cmdsave.Disabled = false;
                                this.cmddelete.Disabled = false;
                                // Me.cmdReturn.Disabled = True
                            }
                            cmddelete2.Enabled = true;
                            cmdsave2.Text = Resources.ValidationResources.bUpdate;
                            Session["dptname"] = txtdepartmentname.Value;
                            Session["srtname"] = txtshortname.Text;
                            Session["Inst"] = DataGrid1.Items[e.Item.ItemIndex].Cells[3].Text;
                            deptmastercon.Close();
                            break;
                        }
                        // ScriptManager1.SetFocus(txtdepartmentname)
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // LibObj.MsgBox1(Resources.ValidationResources.UntoretriveDeptInfo.ToString, Me)

                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UntoretriveDeptInfo.ToString, Me)
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                deptmasterds.Dispose();
                deptmastercon.Dispose();
            }
        }

        protected void cmdsave2_Click(object sender, EventArgs e)
        {
            if (( txtshortname.Text.Trim()=="") || (txtdepartmentname.Value.Trim() == ""))
            {
                message.PageMesg("Enter fields.",this,dbUtilities.MsgLevel.Warning);
                return;
            }
            if (cmbInstName.SelectedValue == "---Select---")
            {
                message.PageMesg("Select Inst.", this, dbUtilities.MsgLevel.Warning);
                return;

            }
            var deptmastercon = new OleDbConnection(retConstr(""));
            var ds7 = new DataSet();
            string strq, chkbdgt;
            var tran = default(OleDbTransaction);
            try
            {
                deptmastercon.Open();

                var cmdk = new OleDbCommand();
                cmdk.Connection = deptmastercon;
                cmdk.CommandType = CommandType.Text;
                cmdk.CommandText = "Select Count(*) as IsInst from InstituteMaster ";
                string IsInts = cmdk.ExecuteScalar().ToString();
                cmdk.ExecuteNonQuery();
                if (IsInts == "0")
                {
                    // LibObj.MsgBox1("First You should be Create Institute From Institute Master In Master!", Me)
                    message.PageMesg("First You should be Create Institute From Institute Master In Master!", this, dbUtilities.MsgLevel.Success);
                    return;
                }
                if (this.cmdsave.Value == Resources.ValidationResources.bUpdate)
                {
                    string sqlStr7;
                    sqlStr7 = "select departmentname,departmentcode from departmentmaster where departmentcode<>N'"+txtdepartmentcode.Value.Trim()+"' and departmentname=N'" + this.txtdepartmentname.Value + "' and institutecode=" + this.cmbInstName.SelectedValue;
                    ds7 = LibObj.PopulateDataset(sqlStr7, "deptt", deptmastercon);
                    if (ds7.Tables["deptt"].Rows.Count > 0)
                    {
                            message.PageMesg("Department already exists", this, dbUtilities.MsgLevel.Warning);
                            return;
                    }
                    string sqlStr6;
                    sqlStr6 = "select shortname,departmentcode from departmentmaster where  departmentcode<>'"+txtdepartmentcode.Value.Trim()+"' and  shortname=N'" + this.txtshortname.Text + "'";
                    ds7 = LibObj.PopulateDataset(sqlStr6, "deptt1", deptmastercon);
                    if (ds7.Tables["deptt1"].Rows.Count > 0)
                    {
                        message.PageMesg("Short name already exists", this, dbUtilities.MsgLevel.Warning);
                        // LibObj.MsgBox1(Resources.ValidationResources.DeptNameExUnderInsti.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.DeptNameExUnderInsti.ToString, Me)
                        // ScriptManager1.SetFocus(txtdepartmentname)
                        return;
                    }
                }
                if (cmdsave2.Text == Resources.ValidationResources.bSave)
                {
                    GetDeptCode(deptmastercon);
                }
                strq = "select checkBudget from librarysetupinformation";
                ds7 = LibObj.PopulateDataset(strq, "LibSInf", deptmastercon);
                chkbdgt = Convert.ToString(ds7.Tables["LibSInf"].Rows[0][0]);
                tran = deptmastercon.BeginTransaction();
                OleDbCommand deptmastercom1;
                deptmastercom1 = new OleDbCommand();
                deptmastercom1.Connection = deptmastercon;
                deptmastercom1.Transaction = tran;
                deptmastercom1.CommandType = CommandType.Text;
                string tmpr1;
                string str_query;
                str_query = "select count(*) from indentmaster where departmentcode in (select departmentcode from departmentmaster where  departmentname = N'$1')";
                str_query = str_query.Replace("$1", Convert.ToString(Session["dptname"]));
                deptmastercom1.CommandText = str_query;
                tmpr1 = Convert.ToString(deptmastercom1.ExecuteScalar());

                if (tmpr1 != "0")
                {
                    this.txtshortname.ReadOnly = true;
                }
                else
                {
                    Hidden1.Value = "0";
                }
                this.txtshortname.ReadOnly = false;

                if (cmdsave2.Text == Resources.ValidationResources.bSave)
                {
                    string sqlStr8;
                    string t1;
                    sqlStr8 = "select count(*) from departmentmaster where institutecode='" + this.cmbInstName.SelectedValue + "' and departmentname=N'" + this.txtdepartmentname.Value.Trim() + "'";
                    deptmastercom1.CommandText = sqlStr8;
                    t1 = Convert.ToString(deptmastercom1.ExecuteScalar());
                    if (t1 != "0")
                    {
                        message.PageMesg("Department already exists", this, dbUtilities.MsgLevel.Warning);
                        // specified department already under institute
                        // LibObj.MsgBox1(Resources.ValidationResources.DeptNameExUnderInsti.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.DeptNameExUnderInsti.ToString, Me)
                        // ScriptManager1.SetFocus(txtdepartmentname)
                        return;
                    }
                    else
                    {
                        Hidden5.Value = "y1";
                    }

                    string sqlStr9;
                    string t2;
                    sqlStr9 = "select Count(*) from departmentmaster where shortname=N'" + this.txtshortname.Text.Trim() + "'";
                    deptmastercom1.CommandText = sqlStr9;
                    t2 = Convert.ToString(deptmastercom1.ExecuteScalar());
                    if (t2 != "0")
                    {
                        message.PageMesg("Short name already exists", this, dbUtilities.MsgLevel.Warning);
                        // specified short already under institute
                        // LibObj.MsgBox1(Resources.ValidationResources.ShortNameExUnderInsti.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ShortNameExUnderInsti.ToString, Me)
                        // ScriptManager1.SetFocus(Me.txtshortname)
                        return;
                    }
                    else
                    {
                        Hidden6.Value = "y2";
                    }
                }
                deptmastercom1.CommandType = CommandType.StoredProcedure;
                deptmastercom1.CommandText = "insert_departmentmaster_1";

                deptmastercom1.Parameters.Add(new OleDbParameter("@departmentcode_1", OleDbType.Integer));
                deptmastercom1.Parameters["@departmentcode_1"].Value = (object)txtdepartmentcode.Value;

                deptmastercom1.Parameters.Add(new OleDbParameter("@departmentname_2", OleDbType.VarWChar));
                deptmastercom1.Parameters["@departmentname_2"].Value = txtdepartmentname.Value.Trim();

                deptmastercom1.Parameters.Add(new OleDbParameter("@shortname_3", OleDbType.VarWChar));
                deptmastercom1.Parameters["@shortname_3"].Value = txtshortname.Text.Trim();

                deptmastercom1.Parameters.Add(new OleDbParameter("@institutecode_4", OleDbType.VarWChar));
                deptmastercom1.Parameters["@institutecode_4"].Value = this.cmbInstName.SelectedValue;

                deptmastercom1.Parameters.Add(new OleDbParameter("@userid_5", OleDbType.VarWChar));
                deptmastercom1.Parameters["@userid_5"].Value = LoggedUser.Logged().User_Id;


                deptmastercom1.ExecuteNonQuery();
                deptmastercom1.Parameters.Clear();
                if (chkbdgt == "N")  // Abhishek 27-sep-2007
                {
                    deptmastercom1.CommandType = CommandType.StoredProcedure;
                    // deptmastercom1.Connection = budgetcon
                    deptmastercom1.CommandText = "insert_budgetmaster_1";

                    deptmastercom1.Parameters.Add(new OleDbParameter("@departmentcode_1", OleDbType.Integer));
                    deptmastercom1.Parameters["@departmentcode_1"].Value = txtdepartmentcode.Value;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@allocatedamount_2", OleDbType.Decimal));
                    deptmastercom1.Parameters["@allocatedamount_2"].Value = 0;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@expendedamount_3", OleDbType.Decimal));
                    deptmastercom1.Parameters["@expendedamount_3"].Value = 0;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@approvalcommitedamt_4", OleDbType.Decimal));
                    deptmastercom1.Parameters["@approvalcommitedamt_4"].Value = 0;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@nonapprovalcommitedamt_5", OleDbType.Decimal));
                    deptmastercom1.Parameters["@nonapprovalcommitedamt_5"].Value = 0;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@approvalindentinhandamt_6", OleDbType.Decimal));
                    deptmastercom1.Parameters["@approvalindentinhandamt_6"].Value = 0;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@nonapprovalindentinhandamt_7", OleDbType.Decimal));
                    deptmastercom1.Parameters["@nonapprovalindentinhandamt_7"].Value = 0;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@status_8", OleDbType.Integer));
                    deptmastercom1.Parameters["@status_8"].Value = 1;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@Curr_Session_9", OleDbType.VarWChar));
                    deptmastercom1.Parameters["@Curr_Session_9"].Value = LoggedUser.Logged().Session;

                    deptmastercom1.Parameters.Add(new OleDbParameter("@userid_10", OleDbType.VarWChar));
                    deptmastercom1.Parameters["@userid_10"].Value = LoggedUser.Logged().User_Id;

                    deptmastercom1.ExecuteNonQuery();
                }
                deptmastercom1.Parameters.Clear();

                if (LoggedUser.Logged().IsAudit=="Y")
                {
                    if (cmdsave2.Text == Resources.ValidationResources.bSave)
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session, txtdepartmentname.Value.Trim(), Resources.ValidationResources.Insert, retConstr(""));
                    }
                    else
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session,  txtdepartmentname.Value.Trim(), Resources.ValidationResources.bUpdate, retConstr(""));
                    }
                }


                // If cmdsave.Value() = Resources.ValidationResources.bSave.ToString Then
                // LibObj1.insertLoginFunc(Session("UserName"), lblt1.Text, Session("session"), Trim(Me.txtdepartmentname.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                // Else
                // LibObj1.insertLoginFunc(Session("UserName"), lblt1.Text, Session("session"), Trim(Me.txtdepartmentname.Value), "Update", retConStr(Session("LibWiseDBConn")))
                // End If
                Session["dptname"] = "";
                cmdsave2.Text = Resources.ValidationResources.bSave;
                // If cmdReturn.Disabled <> True Then
                // End If
                tran.Commit();
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Disabled = false;
                    this.cmdsave.Disabled = false;
                }
                else if (tmpcondition == "N")
                {
                    this.cmddelete.Disabled = true;
                    this.cmdsave.Disabled = true;
                }
                else
                {
                    this.cmdsave.Disabled = false;
                    this.cmddelete.Disabled = false;
                    // Me.cmdReturn.Disabled = True
                }
                this.cmddelete2.Enabled = false;
                txtdepartmentname.Value = string.Empty;
                txtshortname.Text = string.Empty;
                this.cmbInstName.SelectedIndex = this.cmbInstName.Items.Count - 1;
                deptmastercom1.Dispose();
//                FillGrid(deptmastercon);
                showGrid();
                message.PageMesg("Data Saved.", this);
                // ScriptManager1.SetFocus(txtdepartmentname)
                // deptmastercon.Close()
                //if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ViewState["openCond"], null, false)))
                //{
                //    if (hdopenB.Value != "Indent")
                //    {
                //        // Dim returnScript As String = ""
                //        // returnScript &= "<script language='javascript' type='text/javascript'>"
                //        // returnScript &= "javascript:retOnSC('txtdepartmentcode');"
                //        // returnScript &= "<" & "/" & "script>"
                //        // Page.RegisterStartupScript("", returnScript)
                //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtdepartmentcode');", true);
                //    }
                //    else
                //    {

                //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtdepartmentcode');", true);
                //        // Dim returnScript As String = ""
                //        // returnScript &= "<script language='javascript' type='text/javascript'>"
                //        // returnScript &= "javascript:retOnSCdep('txtdepartmentcode');"
                //        // returnScript &= "<" & "/" & "script>"
                //        // Page.RegisterStartupScript("", returnScript)
                //    }
                //    // Dim returnScript As String = ""
                //    // returnScript &= "<script language=javascript>"
                //    // returnScript &= "window.returnValue='"
                //    // returnScript &= txtdepartmentcode.Value
                //    // returnScript &= "';window.close();"
                //    // returnScript &= "<" & "/" & "script>"
                //    // Page.RegisterStartupScript("", returnScript)
                //}
                //this.txtshortname.ReadOnly = false;
            }
            catch (Exception ex)
            {
                try
                {

                    tran.Rollback();
                    // Me.msglabel.Visible = True
                    // Me.msglabel.Text = ex.Message
                    message.PageMesg(Resources.ValidationResources.UntosaveDeptInfo, this, dbUtilities.MsgLevel.Failure);
                }
                // LibObj.MsgBox1(Resources.ValidationResources.UntosaveDeptInfo.ToString, Me)
                // ' msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UntosaveDeptInfo.ToString, Me)
                catch (Exception ex1)
                {
                    // Me.msglabel.Visible = True
                    // Me.msglabel.Text = ex1.Message
                    message.PageMesg(Resources.ValidationResources.UntosaveDeptInfo, this, dbUtilities.MsgLevel.Success);
                    // LibObj.MsgBox1(Resources.ValidationResources.UntosaveDeptInfo.ToString, Me)
                    // 'msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UntosaveDeptInfo.ToString, Me)
                }
            }
            finally
            {
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                ds7.Dispose();
                deptmastercon.Dispose();
            }
        }
        public void GetDeptCode(OleDbConnection deptmastercon_id)
        {
            string tmpstr = string.Empty;
            tmpstr = LibObj.populateCommandText("select coalesce(max(departmentcode),0,max(departmentcode)) from departmentmaster", deptmastercon_id);
            txtdepartmentcode.Value = tmpstr == "0"? "1": (Convert.ToInt32(tmpstr)+1).ToString();
        }
        protected void cmdreset2_Click(object sender, EventArgs e)
        {
            try
            {
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                hdTop.Value = "top";
                clear_field();
                this.txtshortname.ReadOnly = false;
                Session["srtname"] = "";
                Session["dptname"] = "";
                cmdsave2.Text = Resources.ValidationResources.bSave;
                this.DataGrid1.SelectedIndex = -1;
                this.cmbInstName.SelectedIndex = this.cmbInstName.Items.Count - 1;
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Disabled = false;
                    this.cmdsave.Disabled = false;
                    this.cmddelete2.Enabled = true;
                    this.cmdsave2.Enabled=true;
                }
                else if (tmpcondition == "N")
                {
                    this.cmddelete.Disabled = true;
                    this.cmdsave.Disabled = true;
                    this.cmddelete2.Enabled = false;
                    this.cmdsave2.Enabled = true;
                }
                else
                {

                }
                cmddelete2.Enabled = false;
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
            }
            hdnGrdId.Value = DataGrid1.ClientID;
        }

        protected void cmddelete2_Click(object sender, EventArgs e)
        {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            OleDbTransaction tran = null;
            // abhi 9 aug night start
            try
            {
                var cmd = new OleDbCommand("Select departmentcode from Departmentmaster where departmentname=N'" + txtdepartmentname.Value.Replace("'", "''") + "'  and shortname=N'" + txtshortname.Text.Replace("'", "''") + "' and institutecode='" + this.cmbInstName.SelectedValue + "'", deptmastercon);
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtdepartmentcode.Value = dr.GetValue(0).ToString();
                }
                else
                {
                    txtdepartmentcode.Value = string.Empty;
                }

                if (txtdepartmentname.Value == string.Empty)
                {
                    return;
                }
                // LibObj.MsgBox1(Resources.ValidationResources.SpecifyRecDel.ToString, Me)
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("departmentcode", "departmentMaster", "departmentcode='" + txtdepartmentcode.Value + "'", deptmastercon) == false)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, this); // Currentl displayed record does not exist in database
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                // ElseIf LibObj.checkChildExistancewc("departmentcode", "staffmaster", "departmentcode='" & Trim(txtdepartmentcode.Value) & "'", deptmastercon) = True Then ''''(select departmentcode from departmentmaster where departmentname='" & Trim(txtdepartmentname.Value) & "'", retConStr(Session("LibWiseDBConn"))) = True Then'", retConStr(Session("LibWiseDBConn"))) = True Then
                // LibObj.MsgBox(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                else if (LibObj.checkChildExistancewc("departmentcode", "indentmaster", "departmentcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("departmentcode", "CircUserManagement", "departmentcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("deptcode", "Program_Master", "deptcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("departmentcode", "budgetmaster", "departmentcode='" + txtdepartmentcode.Value + "' and (allocatedamount+expendedamount+approvalcommitedamt+nonapprovalcommitedamt+approvalindentinhandamt+nonapprovalindentinhandamt)>0.00 ", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("departmentcode", "BudgetAllocationJournal", "departmentcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("departmentcode", "Giftindentmaster", "departmentcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("departmentcode", "CircUserManagement", "departmentcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("department", "JournalMaster", "department='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("dept", "existingbookkinfo", "dept='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
//                    LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else if (LibObj.checkChildExistancewc("deptcode", "bookaccessionmaster", "deptcode='" + txtdepartmentcode.Value + "'", deptmastercon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                }
                // ScriptManager1.SetFocus(Me.txtdepartmentname)
                else
                {
                    // up to here
                    tran = deptmastercon.BeginTransaction();
                    var delcom = new OleDbCommand("delete from departmentmaster where departmentcode='" + txtdepartmentcode.Value.Trim() + "'", deptmastercon);
                    delcom.CommandType = CommandType.Text;
                    delcom.Transaction = tran;
                    try
                    {
                        delcom.ExecuteNonQuery();
                        delcom.Parameters.Clear();
                        if (LoggedUser.Logged().IsAudit=="Y")
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session,txtdepartmentname.Value.Trim(), Resources.ValidationResources.bDelete, retConstr(""));
                        }
                        tran.Commit();
                        delcom.Dispose();
//                        FillGrid(deptmastercon);
                        showGrid();
                        deptmastercon.Close();
                        cmdreset_ServerClick(sender, e);

                        hdTop.Value = "top";
                        // LibObj.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDel    , this, dbUtilities.MsgLevel.Success);
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();
                            // msglabel.Visible = True
                            // msglabel.Text = ex1.Message
                            // LibObj.MsgBox1(Resources.ValidationResources.UntoDelDeptInfo.ToString, Me)
                            message.PageMesg(ex1.Message, this, dbUtilities.MsgLevel.Failure);
                        }
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UntoDelDeptInfo.ToString, Me)
                        // ScriptManager1.SetFocus(Me.txtdepartmentname)
                        catch (Exception ex2)
                        {
                            // msglabel.Visible = True
                            // msglabel.Text = ex2.Message
                            // LibObj.MsgBox1(Resources.ValidationResources.UntoDelDeptInfo.ToString, Me)
                            message.PageMesg(ex2.Message, this, dbUtilities.MsgLevel.Failure);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UntoDelDeptInfo.ToString, Me)
                            // ScriptManager1.SetFocus(txtdepartmentname)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                deptmastercon.Dispose();
            }

        }

         protected  void btnt_Click(object sender, EventArgs e)
        {

        }

        protected void btn2_Click(object sender, EventArgs e)
        {

        }
    }
}