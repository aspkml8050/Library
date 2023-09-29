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



namespace Library
{
    public partial class LoadingStatus : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        public void GetDeptCode()
        {
            // Subroutine used to Generate New ID of Institute
            var DBConnection = new OleDbConnection(retConstr(""));
            string tmpstr;
            DBConnection.Open();
            tmpstr = LibObj.populateCommandText("select coalesce(max(ItemStatusID),0,max(ItemStatusID)) from ItemStatusMaster", DBConnection);
           
            txtdepartmentcode.Value = tmpstr == "0" ? "1" : (Convert.ToInt32(tmpstr) + 1).ToString();
           // txtdepartmentcode.Value = tmpstr;
            DBConnection.Close();
            DBConnection.Dispose();
        }

        public void FillGrid()
        {
            // To Fill Datagrid with all available Institute in database
            var dt = new DataTable();
            OleDbConnection DBConnection = null;
            DataSet ds = null;
            try
            {
                DBConnection = new OleDbConnection();
                DBConnection.ConnectionString = retConstr("");
                DBConnection.Open();
                ds = new DataSet();
                ds = LibObj.PopulateDataset("select ItemStatus,ItemStatusShort,ItemStatusID,(case when isBardateApllicable='Y' then 'Y' else 'N' end) as isBardateApllicable,(case when isIsued='Y' then 'Y' else 'N' end) as isIsued from ItemStatusMaster order by ItemStatus", "status", DBConnection);
                if (ds.Tables["status"].Rows.Count > 0)
                {
                    DataGrid1.DataSource = ds.Tables["status"].DefaultView;
                    DataGrid1.DataBind();
                }
                else
                {
                    DataGrid1.DataSource = dt;
                    DataGrid1.DataBind();
                }
                //hdnGrdId.Value = DataGrid1.ClientID;
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
                DBConnection.Dispose();
                ds.Dispose();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                msglabel.Text = string.Empty;
                msglabel.Visible = false;
                if (!Page.IsPostBack)
                {
                    //Page.SetFocus(txtStatus);
                    // Me.ScriptManager1.SetFocus(txtStatus)
                    lblt1.Text = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                }
                if (tmpcondition == "Y")
                {
                    this.cmddelete1.Visible = false;
                    this.cmdsave2.Visible = true;
                }
                else
                {
                    this.cmddelete1.Visible = true;
                    this.cmdsave2.Visible = true;
                }
                txtdepartmentcode.Visible = false;
                cmdreset1.CausesValidation = false;
                cmddelete1.CausesValidation = false;
                cmddelete1.Visible = true;
                FillGrid();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void cmdsave2_Click(object sender, EventArgs e)
        
        {
            OleDbConnection DBConnection = null;
                    OleDbTransaction tran = null;
                    OleDbCommand cmd = null;
            try
            {
                DBConnection = new OleDbConnection();
                DBConnection.ConnectionString = retConstr("");
                DBConnection.Open();
                tran = DBConnection.BeginTransaction();
                cmd = new OleDbCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                string tmpr1 = string.Empty;
                string tmpr2 = string.Empty;
                string str_query = string.Empty;
                string dept_str = string.Empty;
                if((Session["dptname"] == null) || (Session["dptname"].ToString() == ""))
                {
                    cmd.CommandText = "select ItemStatusShort  from ItemStatusMaster where ItemStatus=N'" + txtStatus.Value + "'";
                    tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                    //GetDeptCode();
                    if (!((tmpr1) == string.Empty))
                    {
                      
                        message.PageMesg(Resources.ValidationResources.MStatus, this, dbUtilities.MsgLevel.Warning);
                        Page.SetFocus(txtStatus);
                        // Me.ScriptManager1.SetFocus(Me.txtStatus)
                        return;
                    }
                    else
                    {
                        Hidden1.Value = "0";

                    }

                }
                else
                if (Session["dptname"].ToString() != this.txtStatus.Value && Session["srtname"].ToString()
                        == this.txtshortname.Value)
                {
                    cmd.CommandText = "select ItemStatusShort  from ItemStatusMaster where ItemStatus=N'" + txtStatus.Value + "'";
                    tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                    if (!((tmpr1) == string.Empty))
                    {
                        message.PageMesg(Resources.ValidationResources.MStatus.ToString(), this, dbUtilities.MsgLevel.Warning);
                        Page.SetFocus(txtStatus);
                        // Me.ScriptManager1.SetFocus(Me.txtStatus)
                        return;
                    }
                    else
                    {
                        Hidden1.Value = "0";

                    }

                }
                else if ((Session["dptname"].ToString() == this.txtStatus.Value && Session["srtname"].ToString()
                    != this.txtshortname.Value))
                {
                    cmd.CommandText = "select  ItemStatus from ItemStatusMaster where ItemStatusShort=N'" + txtshortname.Value + "'";
                    tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                    if (!((tmpr2) == string.Empty))
                    {
                        message.PageMesg(Resources.ValidationResources.MShort.ToString(), this, dbUtilities.MsgLevel.Warning);
                        Page.SetFocus(txtStatus);
                        // Me.ScriptManager1.SetFocus(txtshortname)
                        return;
                    }
                    else
                    {
                        Hidden2.Value = "0";
                    }
                }
                else if (Session["dptname"].ToString() != this.txtStatus.Value && Session["srtname"].ToString() != (this.txtshortname.Value))
                {
                    cmd.CommandText = "select ItemStatusShort  from ItemStatusMaster where ItemStatus=N'" + txtStatus.Value + "'";
                    tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                    if (!((tmpr1) == string.Empty))
                    {
                        message.PageMesg(Resources.ValidationResources.MStatus.ToString(), this, dbUtilities.MsgLevel.Warning);

                        Page.SetFocus(txtStatus);
                        // Me.ScriptManager1.SetFocus(txtStatus)
                        return;
                    }
                    else
                    {
                        Hidden1.Value = "0";
                    }
                    cmd.CommandText = "select  ItemStatus from ItemStatusMaster where ItemStatusShort=N'" + txtshortname.Value + "'";
                    tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                    if (!((tmpr2) == string.Empty))
                    {
                        message.PageMesg(Resources.ValidationResources.MShort.ToString(), this, dbUtilities.MsgLevel.Warning);

                    //    Page.SetFocus(txtStatus);
                        // Me.ScriptManager1.SetFocus(txtshortname)


                        return;
                    }
                    else
                    {
                        Hidden2.Value = "0";
                    }
                }
                if (Hidden1.Value == "0" || Hidden2.Value == "0")
                {
                    if (Session["dptname"].ToString() == "0")
                    {
                        str_query = "select distinct ItemStatus from ItemStatusMaster where ItemStatus = N'$1'";
                        str_query = str_query.Replace("$1", Convert.ToString(txtStatus.Value));
                        cmd.CommandText = str_query;
                        tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                        if (!((tmpr1) == string.Empty))
                        {
                            dept_str = "1";
                        }
                        else
                        {
                            dept_str = "0";
                        }
                    }
                    else
                    {
                        dept_str = "2";
                    }
                }
                if (dept_str == "0" || dept_str == "2")  // Case if Operation Mode is insert  not Update
                {
                    if (cmdsave2.Text == Resources.ValidationResources.bSave)
                    {
                    GetDeptCode();
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_ItemStatusMaster_1";

                cmd.Parameters.Add(new OleDbParameter("@ItemStatusID_1", OleDbType.Integer));
                cmd.Parameters["@ItemStatusID_1"].Value = txtdepartmentcode.Value;

                cmd.Parameters.Add(new OleDbParameter("@ItemStatus_2", OleDbType.VarWChar));
                cmd.Parameters["@ItemStatus_2"].Value = (txtStatus.Value);

                cmd.Parameters.Add(new OleDbParameter("@ItemStatusShort_3", OleDbType.VarWChar));
                cmd.Parameters["@ItemStatusShort_3"].Value = (txtshortname.Value);

                if (chkboth.Items[0].Selected == true)
                {
                    cmd.Parameters.Add(new OleDbParameter("@isBardateApllicable_4", OleDbType.VarWChar));
                    cmd.Parameters["@isBardateApllicable_4"].Value = "Y";
                }
                else
                {
                    cmd.Parameters.Add(new OleDbParameter("@isBardateApllicable_4", OleDbType.VarWChar));
                    cmd.Parameters["@isBardateApllicable_4"].Value = "N";
                }

                if (chkboth.Items[1].Selected == true)
                {
                    cmd.Parameters.Add(new OleDbParameter("@isIsued_5", OleDbType.VarWChar));
                    cmd.Parameters["@isIsued_5"].Value = "Y";
                }
                else
                {
                    cmd.Parameters.Add(new OleDbParameter("@isIsued_5", OleDbType.VarWChar));
                    cmd.Parameters["@isIsued_5"].Value = "N";

                }

                cmd.Parameters.Add(new OleDbParameter("@userid_6", OleDbType.VarWChar));
                cmd.Parameters["@userid_6"].Value = LoggedUser.Logged().Session;

                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;

                if (chkboth.Items[1].Selected == true)
                {
                    cmd.CommandText = "UPDATE bookaccessionmaster set IssueStatus=N'Y' where status=N'" + this.txtdepartmentcode.Value + "'";
                }
                else
                {
                    cmd.CommandText = "UPDATE bookaccessionmaster set IssueStatus=N'N' where status=N'" + this.txtdepartmentcode.Value + "'";
                }
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                var logged = LoggedUser.Logged();
                if (logged.IsAudit=="Y")
                {
                    if (cmdsave2.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session, this.txtStatus.Value.Trim(), Resources.ValidationResources.Insert, retConstr(""));
                    }
                    else
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session,this.txtStatus.Value.Trim(), Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                    }
                }
                Session["dptname"] = "";
                Session["srtname"] = "";
                cmdsave2.Text = Resources.ValidationResources.bSave;
                // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);

                tran.Commit();
                if (tmpcondition == "Y")
                {
                    this.cmddelete1.Visible = false;
                    this.cmdsave2.Visible = false;
                }
                else
                {
                    this.cmddelete1.Visible = true;
                    this.cmdsave2.Visible = true;
                }
                chkboth.Items[0].Selected = false;
                chkboth.Items[1].Selected = false;
                this.cmddelete1.Visible = true;
                txtStatus.Value = string.Empty;
                txtshortname.Value = string.Empty;
                //Page.SetFocus(txtStatus);
                // Me.ScriptManager1.SetFocus(Me.txtStatus)
                cmd.Dispose();
                DBConnection.Close();
                DBConnection.Dispose();
                FillGrid();
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    tran.Rollback();
                    
                    message.PageMesg(Resources.ValidationResources.USaveI.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                catch (Exception ex1)
                {
                    message.PageMesg(Resources.ValidationResources.USaveI.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
                tran.Dispose();
            }
        }
        public void clear_field()
        {
            txtdepartmentcode.Value = "";
            txtStatus.Value = "";
            txtshortname.Value = "";
        }

        protected void cmdreset1_Click(object sender, EventArgs e)
        {
            try
            {
                hdTop.Value = Resources.ValidationResources.RBTop;
                // Me.ScriptManager1.SetFocus(Me.txtStatus)
                clear_field();
                Session["srtname"] = "";
                Session["dptname"] = "";
                cmdsave2.Text = Resources.ValidationResources.bSave.ToString();
                this.DataGrid1.SelectedIndex = -1;
                if (tmpcondition == "Y")
                {
                    this.cmddelete1.Visible = false;
                   this.cmdsave2.Visible = false;
                }
                else
                {
                    this.cmddelete1.Visible = true;
                    this.cmdsave2.Visible = true;
                }
                txtshortname.Disabled = false;
                cmddelete1.Visible = true;

                chkboth.Items[0].Selected = false;
                chkboth.Items[1].Selected = false;
                FillGrid();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }

        }

        protected void cmddelete1_Click(object sender, EventArgs e)
        {
            OleDbConnection deptmastercon = null;
            OleDbCommand cmd = null;
            OleDbTransaction tran = null;
            try
            {
                deptmastercon = new OleDbConnection();
                deptmastercon.ConnectionString = retConstr("");
                deptmastercon.Open();
                cmd = new OleDbCommand("Select ItemStatusID from ItemStatusMaster where ItemStatus=N'" + (txtStatus.Value) + "' and ItemStatusShort=N'" + (this.txtshortname.Value) + "' ", deptmastercon);
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
                if (txtStatus.Value == string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this,dbUtilities.MsgLevel.Warning);
                    //Page.SetFocus(txtStatus);
                }
                // Me.ScriptManager1.SetFocus(txtStatus)
                else if (LibObj.checkChildExistancewc("ItemStatusID", "ItemStatusMaster", "ItemStatusID='" + (txtdepartmentcode.Value) + "'", deptmastercon) == false)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me) 'Currentl displayed record does not exist in database
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelNotExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this,dbUtilities.MsgLevel.Warning);
                    //Page.SetFocus(txtStatus);
                }
                // Me.ScriptManager1.SetFocus(txtStatus)
                else if (LibObj.checkChildExistance("Status", "bookaccessionmaster", "Status=N'" + txtdepartmentcode.Value + "'", retConstr("")) == true)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                   // Page.SetFocus(txtStatus);
                }
                // Me.ScriptManager1.SetFocus(txtStatus)
                else
                {
                    tran = deptmastercon.BeginTransaction();
                    cmd.Parameters.Clear();
                    cmd = new OleDbCommand("delete from ItemStatusMaster where ItemStatusID='" + (txtdepartmentcode.Value) + "'", deptmastercon);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.ExecuteNonQuery();


                        // Dim temp As String = String.Empty
                        var logged = LoggedUser.Logged();
                        if (logged.IsAudit == "Y")         
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lblt1.Text, logged.Session, (this.txtStatus.Value), Resources.ValidationResources.bDelete, retConstr(""));
                        }


                        tran.Commit();
                        cmd.Dispose();
                        deptmastercon.Close();
                        
                        FillAfterDelete(deptmastercon);
                        cmdreset1_Click(sender, e);
                       //LibObj.SetFocus("txtStatus", this);
                        hdTop.Value = Resources.ValidationResources.RBTop;
                        
                        message.PageMesg(Resources.ValidationResources.rDel.ToString(), this,dbUtilities.MsgLevel.Success);

                       // Page.SetFocus(txtStatus);
                    }
                    // Me.ScriptManager1.SetFocus(txtStatus)
                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();
                            
                            message.PageMesg(Resources.ValidationResources.UdelI.ToString(), this, dbUtilities.MsgLevel.Warning);
                        }
                        catch (Exception ex2)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.UdelI.ToString(), this, dbUtilities.MsgLevel.Warning);
                        }
                    }
                }
                FillAfterDelete(deptmastercon);
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                cmd.Dispose();
                deptmastercon.Dispose();
            }
        }
        protected void DataGrid1_ItemCommand1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            txtshortname.Disabled = false;
                            this.cmddelete1.Visible = false;
                            var DBConnection = new OleDbConnection();
                            DBConnection.ConnectionString = retConstr("");
                            DBConnection.Open();
                            DataSet ds;
                            ds = new DataSet();
                            ds = LibObj.PopulateDataset("Select ItemStatus,ItemStatusShort,ItemStatusID,isBardateApllicable,isIsued from ItemStatusMaster where ItemStatusID=" + DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text +"", "ItemStatusMaster", DBConnection);

                            txtStatus.Value = ds.Tables["ItemStatusMaster"].Rows[0]["ItemStatus"].ToString();
                            txtshortname.Value = ds.Tables["ItemStatusMaster"].Rows[0]["ItemStatusShort"].ToString();
                            this.txtdepartmentcode.Value = ds.Tables["ItemStatusMaster"].Rows[0]["ItemStatusID"].ToString();
                            if (ds.Tables["ItemStatusMaster"].Rows[0]["isBardateApllicable"].ToString()== "Y")
                            {
                                chkboth.Items[0].Selected = true;
                            }
                            else
                            {
                                chkboth.Items[0].Selected = false;
                            }
                            if (ds.Tables["ItemStatusMaster"].Rows[0]["isIsued"].ToString()== "Y")
                            {
                                chkboth.Items[1].Selected = true;
                            }
                            else
                            {
                                chkboth.Items[1].Selected = false;
                            }
                            ds.Dispose();
                            DBConnection.Close();
                            DBConnection.Dispose();
                            cmdsave2.Text = Resources.ValidationResources.bUpdate;
                            if (tmpcondition == "Y")
                            {
                                this.cmddelete1.Visible = false;
                                this.cmdsave2.Visible = false;
                            }
                            else
                            {
                                this.cmddelete1.Visible = true;
                                this.cmdsave2.Visible = true;
                            }
                            Session["dptname"] = txtStatus.Value;
                            Session["srtname"] = txtshortname.Value;
                            DBConnection.Close();
                            ds.Dispose();
                            DBConnection.Dispose();
                            break;
                        }
                }
                //LibObj.SetFocus("txtStatus", this);
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // LibObj.MsgBox1(Resources.ValidationResources.URetriveI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.URetriveI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.URetriveI.ToString(), this,dbUtilities.MsgLevel.Warning);
            }
        }
        private void DataGrid1_PageIndexChanged1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            try
            {

                // Me.ScriptManager1.SetFocus(DataGrid1)
                string searchqry;
                searchqry = "SELECT *  FROM ItemStatusMaster order by ItemStatus";
                OleDbConnection DBConnection;
                DBConnection = new OleDbConnection();
                DBConnection.ConnectionString = retConstr("");
                DBConnection.Open();
                DataSet ds;
                ds = new DataSet();
                ds = LibObj.PopulateDataset(searchqry, "ItemStatusMaster", DBConnection);
                var dt = ds.Tables["ItemStatusMaster"];
                var dv = new DataView(dt);
                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = DataGrid1.Attributes["ItemStatus"];
                DataGrid1.DataSource = dv;
                DataGrid1.DataBind();
                //hdnGrdId.Value = DataGrid1.ClientID;
                ds.Dispose();
                DBConnection.Close();
                DBConnection.Dispose();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
        }
        public void FillAfterDelete(OleDbConnection con)
        {
            try
            {
                // POPULATE GRID AFTER DELETION
                string qryString;
                qryString = "select ItemStatus,ItemStatusShort,ItemStatusID,(case when isBardateApllicable='Y' then 'Yes' else 'No' end) as isBardateApllicable,(case when isIsued='Y' then 'Yes' else 'No' end) as isIsued from ItemStatusMaster order by ItemStatus";
                LibObj.populateAfterDeletion(DataGrid1, qryString, con);
                //hdnGrdId.Value = DataGrid1.ClientID;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
        }

    }
}
        





        



    
