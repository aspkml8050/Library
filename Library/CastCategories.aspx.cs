using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using Library.Models;
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
    public partial class CastCategories : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            
                        try
                        {
                var logged = LoggedUser.Logged();
                            msglabel.Text = string.Empty;
                            msglabel.Visible = false;
                            hdnGrdId.Value = DataGrid1.ClientID;


                            if (!Page.IsPostBack)
                            {
                                this.SetFocus(txtStatus);

                    //   lblt1.Text = Request.QueryString("title");

                    tmpcondition = "Y";// Request.QueryString("condition");
                                if (tmpcondition == "Y")
                                {
                                    //this.cmddelete.Disabled = false;
                                    cmdsaved.Enabled = true;
                                    btnDelete.Enabled = false;
                                }
//                                else
  //                              {
    //                                this.cmddelete.Disabled = true;
      //                              this.cmdsaved.Enabled = false;
        //                        }
                                // cmdReturn.CausesValidation = False
                                txtdepartmentcode.Visible = false;
                    cmdsaved.Enabled = true;
                    FillGrid();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Me.msglabel.Visible = True
                            // Me.msglabel.Text = ex.Message
                            message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                        }
                        
        }
        public void GetDeptCode()
        {
            // Subroutine used to Generate New ID of Institute
            var DBConnection = new OleDbConnection(retConstr(""));
            string tmpstr;
            DBConnection.Open();
            tmpstr = LibObj.populateCommandText("select isnull(max(cat_id),0)+1 from castcategories", DBConnection);
            //  txtdepartmentcode.Value = tmpstr;
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
                ds = LibObj.PopulateDataset("select cat_name,shortname,cat_id from castcategories order by cat_name", "status", DBConnection);
                if (ds.Tables["status"].Rows.Count > 0)
                {
                         DataGrid1.DataSource = ds.Tables["status"].DefaultView;
                       DataGrid1.DataBind();
                }
                else
                {
                     DataGrid1.DataSource = null;
                     DataGrid1.DataBind();
                }
                            hdnGrdId.Value = DataGrid1.ClientID;
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
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
        protected void cmdsave_ServerClick(object sender, EventArgs e)
        {
          
        }

        public void clear_field()
        {
                  txtdepartmentcode.Value = "";
                  txtStatus.Value = "";
                  txtshortname.Value = "";
          
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            OleDbConnection DBConnection = null;
            OleDbTransaction tran = null;
            OleDbCommand cmd = null;
            try
            {
                var logged = LoggedUser.Logged();
                if ((logged==null) || (string.IsNullOrEmpty( logged.UserId)))
                {
                    Response.Redirect("~/Default.aspx");
                }

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
                cmd.CommandText = "select isnull(shortname,'') shortname  from castcategories where cat_name=N'" + txtStatus.Value + "'";
               var r= LibObj.checkChildExistance("shortname", "castcategories", "cat_name='" + txtStatus.Value.Trim()+"'", retConstr(""));
                //tmpr1 = cmd.ExecuteScalar().ToString();
                if (r)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.Cstatus.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.Cstatus, this, dbUtilities.MsgLevel.Failure);
                    //this.SetFocus(txtStatus);

                    return;
                }
                else
                {
                    Hidden1.Value = "0";
                }
                cmd.CommandText = "select  cat_name from castcategories where shortname=N'" + txtshortname.Value + "'";
//                tmpr2 = cmd.ExecuteScalar().ToString();
                var r2 = LibObj.checkChildExistance("cat_name", "castcategories", "shortname='" + txtStatus.Value.Trim()+"'", retConstr(""));
                if (r2)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.MShort.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MShort.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.MShort, this, dbUtilities.MsgLevel.Failure);
                    this.SetFocus(txtshortname);
                    return;
                }
                else
                {
                    Hidden2.Value = "0";
                }
                cmd.CommandText = "select Shortname  from castcategories where cat_name=N'" + txtStatus.Value + "'";
//                tmpr1 = cmd.ExecuteScalar().ToString();
                var r3 = LibObj.checkChildExistance("Shortname", "castcategories", "cat_name='" + txtStatus.Value.Trim() + "'", retConstr(""));
                if (r3)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.CCNameAlrExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CCNameAlrExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.CCNameAlrExist, this, dbUtilities.MsgLevel.Failure);
                    this.SetFocus(txtStatus);
                    return;
                }
                else
                {
                    Hidden1.Value = "0";
                }


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_castcategories_1";

                cmd.Parameters.Add(new OleDbParameter("@cat_id_1", OleDbType.Integer));
                cmd.Parameters["@cat_id_1"].Value = txtdepartmentcode.Value;

                cmd.Parameters.Add(new OleDbParameter("@cat_name_2", OleDbType.VarWChar));
                cmd.Parameters["@cat_name_2"].Value = txtStatus.Value.Trim();

                cmd.Parameters.Add(new OleDbParameter("@userid_3", OleDbType.VarWChar));
                cmd.Parameters["@userid_3"].Value = logged.UserId;// Session["user_id"];

                cmd.Parameters.Add(new OleDbParameter("@shortname_4", OleDbType.VarWChar));
                cmd.Parameters["@shortname_4"].Value = txtshortname.Value.Trim();

                cmd.ExecuteNonQuery();

                // Dim temp As String = String.Empty
                cmd.Parameters.Clear();


                // If cmdsave.Value() = "Submit" Then
                // LibObj1.insertLoginFunc(Session("UserName"), lblt1.Text, Session("session"), Trim(Me.txtStatus.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                // Else
                // LibObj1.insertLoginFunc(Session("UserName"), lblt1.Text, Session("session"), Trim(Me.txtStatus.Value), "Update", retConStr(Session("LibWiseDBConn")))
                // End If
                Session["dptname"] = "";
                Session["srtname"] = "";
                cmdsaved.Text = Resources.ValidationResources.bSave;
                // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);

                tran.Commit();
                btnDelete.Enabled = false;
                txtStatus.Value = string.Empty;
                txtshortname.Value = string.Empty;
                this.SetFocus(txtStatus);
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
                    // Me.msglabel.Visible = True
                    // Me.msglabel.Text = ex.Message
                    message.PageMesg(Resources.ValidationResources.USaveI, this, dbUtilities.MsgLevel.Warning);
                }
                // LibObj.MsgBox1(Resources.ValidationResources.USaveI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.USaveI.ToString, Me)

                catch (Exception ex1)
                {
                    // Me.msglabel.Visible = True
                    // Me.msglabel.Text = ex1.Message
                    // LibObj.MsgBox1(Resources.ValidationResources.USaveI.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.USaveI.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.USaveI, this, dbUtilities.MsgLevel.Failure);

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
        private void DataGrid1_ItemCommand1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
           
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                hdTop.Value = Resources.ValidationResources.RBTop;
                this.SetFocus(txtStatus);
                clear_field();
                Session["shortname"] = "";
                Session["dptname"] = "";
                cmdsaved.Text = Resources.ValidationResources.bSave;
                this.DataGrid1.SelectedIndex = -1;
                if (tmpcondition == "Y")
                {
                    btnDelete.Enabled = true;
                    cmdsaved.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    cmdsaved.Enabled = false;
                }
                cmdsaved.Enabled = true;
                txtshortname.Disabled = false;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            txtshortname.Disabled = false;
                            btnDelete.Enabled = true;
                            var DBConnection = new OleDbConnection();
                            DBConnection.ConnectionString = retConstr("");
                            DBConnection.Open();
                            DataSet ds;
                            ds = new DataSet();
                            ds = LibObj.PopulateDataset("Select cat_name,shortname,cat_id from castcategories where cat_id=" + e.Item.Cells[1].Text, "castcategories", DBConnection);
                            txtStatus.Value = ds.Tables["castcategories"].Rows[0]["cat_name"].ToString();
                            txtshortname.Value = ds.Tables["castcategories"].Rows[0]["shortname"].ToString();
                            this.txtdepartmentcode.Value = ds.Tables["castcategories"].Rows[0]["cat_id"].ToString();
                            ds.Dispose();
                            DBConnection.Close();
                            DBConnection.Dispose();
                            cmdsaved.Text = Resources.ValidationResources.bUpdate;
                            if (tmpcondition == "Y")
                            {
                                btnDelete.Enabled = true;
                                cmdsaved.Enabled = true;
                            }
                            else
                            {
                                // Me.cmddelete.Disabled = True
                                // Me.cmdsave.Disabled = True
                            }
                            Session["dptname"] = txtStatus.Value;
                            Session["srtname"] = txtshortname.Value;
                            DBConnection.Close();
                            ds.Dispose();
                            DBConnection.Dispose();
                            break;
                        }
                }
                // LibObj.SetFocus("txtStatus", Me)
                this.SetFocus(txtStatus);
            }

            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // LibObj.MsgBox1(Resources.ValidationResources.URetriveI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.URetriveI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.URetriveI, this, dbUtilities.MsgLevel.Failure);
            }
        }
    }
}