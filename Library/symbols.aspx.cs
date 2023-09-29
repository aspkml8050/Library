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
using Microsoft.VisualBasic;

namespace Library
{
    public partial class symbols : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            var ClassCon = new OleDbConnection(retConstr(""));
            ClassCon.Open();
            hdnGrdId.Value = DataGrid1.ClientID;

            try{
                msglabel.Text = string.Empty;
                msglabel.Visible = false;
                if (!Page.IsPostBack)
                {
                    //LibObj.SetFocus("txtshortname", this);
                    lblt1.Text = Request.QueryString["title"];
                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        this.cmddelete.Visible = false;
                        this.cmdsave.Visible = true;
                    }
                    else
                    {
                        this.cmddelete.Visible = true;
                        this.cmdsave.Visible = true;
                    }
                    // cmdReturn.CausesValidation = False
                    txtdepartmentcode.Visible = false;
                    cmdreset.CausesValidation = false;
                    cmddelete.CausesValidation = false;
                    cmddelete.Visible = true;
                    
                    FillGrid();
                }
            }
            catch (Exception ex){
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ClassCon.Close();
                ClassCon.Dispose();
            }
        }

        public void GetDeptCode()
        {
            // Subroutine used to Generate New ID of Institute
            var DBConnection = new OleDbConnection(retConstr(""));
            string tmpstr;
            DBConnection.Open();
            tmpstr = LibObj.populateCommandText("select coalesce(max(SymbolTypeId),0,max(SymbolTypeId)) from Symbols", DBConnection);
            int z = (Int32.Parse(tmpstr) == 0) ? 1 : Int32.Parse(tmpstr) + 1;
            txtdepartmentcode.Value = z.ToString();
            DBConnection.Close();
            DBConnection.Dispose();
        }

        public void FillGrid()
        {
            // To Fill Datagrid with all available Institute in database
            var dt = new DataTable();
            OleDbConnection DBConnection = null;
            var ds = new DataSet();
            try
            {
                DBConnection = new OleDbConnection();
                DBConnection.ConnectionString = retConstr("");
                // DBConnection.Open()
                // ds = New DataSet
                ds = LibObj.PopulateDataset("select SymbolType,SymbolTypeId,Symbol from Symbols order by SymbolType", "status", DBConnection);
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
                hdnGrdId.Value = DataGrid1.ClientID;

                DBConnection.Close();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
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

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            var ClassCon = new OleDbConnection(retConstr(""));
            ClassCon.Open();
            try
            {
                if (ddsymbol.SelectedItem.Text == Resources.ValidationResources.ComboSelect)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.SelSymbType.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelSymbType.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SelSymbType.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(ddsymbol);
                    return;
                }
                if (txtshortname.Value == string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.Entersymbol.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.Entersymbol.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.Entersymbol.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtshortname);
                    return;
                }
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
                    if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        if (LibObj.isDulicate("select SymbolType,Symbol from Symbols where  SymbolType=N'" + ddsymbol.SelectedItem.Value + "' AND Symbol=N'" + txtshortname.Value + "' ", ClassCon) == true)
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);
                            this.SetFocus(txtshortname);
                            // ClassCon.Close()
                            // ClassCon.Dispose()
                            return;
                        }
                    }




                    if (Session["dptname"].ToString() != this.ddsymbol.SelectedItem.Value && Session["srtname"].ToString() == this.txtshortname.Value)
                    {
                        cmd.CommandText = "select Symbol  from Symbols where SymbolType=N'" + ddsymbol.SelectedItem.Text + "'";
                        tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                        // If Not Trim(tmpr1) = String.Empty Then
                        // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                        // Exit Sub
                        // Else
                        Hidden1.Value = "0";

                    } // *********************bipin*************************************************************
                      // End If
                    if (Session["dptname"].ToString() == this.ddsymbol.SelectedItem.Text && Session["srtname"].ToString() != this.txtshortname.Value)
                    {
                        cmd.CommandText = "select  SymbolType from Symbols where Symbol=N'" + txtshortname.Value + "'";
                        tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                        if (!(tmpr2 == string.Empty))
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);
                            this.SetFocus(txtshortname);
                            return;
                        }
                        else
                        {
                            Hidden2.Value = "0";
                        }
                    }
                    // '''''''''' shweta24-oct
                    if (Session["dptname"].ToString() != this.ddsymbol.SelectedItem.Text && Session["srtname"].ToString() == this.txtshortname.Value)
                    {
                        cmd.CommandText = "select  SymbolType from Symbols where Symbol=N'" + txtshortname.Value + "' and SymbolType=N'" + ddsymbol.SelectedItem.Value + "'";
                        tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                        if (!(tmpr2 == string.Empty))
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);

                            this.SetFocus(txtshortname);
                            return;
                        }
                        else
                        {
                            Hidden2.Value = "0";
                        }
                    }


                    // ''''''''''''''


                    if (Session["dptname"].ToString() != this.ddsymbol.SelectedItem.Text & Session["srtname"].ToString() != this.txtshortname.Value)
                    {
                        cmd.CommandText = "select Symbol  from Symbols where SymbolType=N'" + ddsymbol.SelectedItem.Value + "'";
                        tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                        // If Not Trim(tmpr1) = String.Empty Then
                        // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                        // Exit Sub
                        // Else
                        Hidden1.Value = "0";
                    }
                    // End If
                    if (cmd.CommandText == "select  SymbolType from Symbols where Symbol=N'" + txtshortname.Value + "'")
                    {
                        tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                        // Hidden2.Value = "0"
                        if (!(tmpr2 == string.Empty))
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);

                            this.SetFocus(txtshortname);
                            return;
                        }
                        else
                        {
                            Hidden2.Value = "0";
                        }
                    }
                    // %%%%%%%%%%%%%%%%%%%%%%%% bipin %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    if (cmdsave.Text == Resources.ValidationResources.bUpdate.ToString())
                    {

                        // ggggg

                        // If UCase(Session("dptname")) <> UCase(Me.ddsymbol.SelectedItem.Value) And UCase(Session("srtname")) = UCase(Me.txtshortname.Value) Then



                        if (Session["dptname"].ToString() == this.ddsymbol.SelectedItem.Value && Session["srtname"].ToString() != this.txtshortname.Value)
                        {
                            cmd.CommandText = "select Symbol  from Symbols where SymbolType=N'" + ddsymbol.SelectedItem.Text + "'";
                            tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                            if (tmpr1 == txtshortname.Value)
                            {
                                // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this,dbUtilities.MsgLevel.Warning);

                                this.SetFocus(txtshortname);
                                return;
                            }
                            else
                            {
                                Hidden1.Value = "0";

                            } // **********************************************************************************
                        }
                        if (Session["dptname"].ToString() == this.ddsymbol.SelectedItem.Text && Session["srtname"].ToString() != this.txtshortname.Value)
                        {
                            cmd.CommandText = "select  SymbolType from Symbols where Symbol=N'" + txtshortname.Value + "'";
                            tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                            if (!(tmpr2 == string.Empty))
                            {
                                // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);

                                this.SetFocus(txtshortname);
                                return;
                            }
                            else
                            {
                                Hidden2.Value = "0";
                            }
                        }
                        if (Session["dptname"].ToString() != this.ddsymbol.SelectedItem.Text && Session["srtname"].ToString() != this.txtshortname.Value)
                        {
                            cmd.CommandText = "select Symbol  from Symbols where SymbolType=N'" + ddsymbol.SelectedItem.Value + "' and symbol=N'" + txtshortname.Value + "' ";
                            tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                            if (tmpr1 == txtshortname.Value)
                            {
                                // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);

                                this.SetFocus(txtshortname);
                                return;
                            }
                            else
                            {
                                Hidden1.Value = "0";
                            }
                        }
                        if (cmd.CommandText == "select  SymbolType from Symbols where Symbol=N'" + txtshortname.Value + "'")
                        {
                            tmpr2 = Convert.ToString(cmd.ExecuteScalar());
                            // Hidden2.Value = "0"
                            if (!(tmpr2 == string.Empty))
                            {
                                // LibObj.MsgBox1(Resources.ValidationResources.SymExistSST.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SymExistSST.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.SymExistSST.ToString(), this, dbUtilities.MsgLevel.Warning);

                                this.SetFocus(txtshortname);
                                return;
                            }
                            else
                            {
                                Hidden2.Value = "0";
                            }
                        }




                    }
                    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    if (Hidden1.Value == "0" | Hidden2.Value == "0")
                    {
                        if (Session["dptname"] == null)
                        {
                            str_query = "select distinct SymbolType from Symbols where SymbolType= N'$1'";
                            str_query = str_query.Replace("$1", Convert.ToString(ddsymbol.SelectedItem.Text));
                            cmd.CommandText = str_query;

                            tmpr1 = Convert.ToString(cmd.ExecuteScalar());
                            if (!(tmpr1 == string.Empty))
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

                    if (dept_str == "1" | dept_str == "0" | dept_str == "2")  // Case if Operation Mode is insert  not Update
                    {
                        if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                        {
                            GetDeptCode();
                        }
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_Symbols_1";

                    cmd.Parameters.Add(new OleDbParameter("@SymbolTypeId_1 ", OleDbType.Integer));
                    cmd.Parameters["@SymbolTypeId_1 "].Value = (object)txtdepartmentcode.Value;


                    cmd.Parameters.Add(new OleDbParameter("@SymbolType_2", OleDbType.VarWChar));
                    cmd.Parameters["@SymbolType_2"].Value = ddsymbol.SelectedItem.Text;

                    cmd.Parameters.Add(new OleDbParameter("@Symbol_3", OleDbType.VarWChar));
                    cmd.Parameters["@Symbol_3"].Value = txtshortname.Value;

                    cmd.Parameters.Add(new OleDbParameter("@userid_4", OleDbType.VarWChar));
                    cmd.Parameters["@userid_4"].Value = Session["user_id"];


                    cmd.ExecuteNonQuery();


                    // Dim temp As String = String.Empty
                    cmd.Parameters.Clear();
                    if (LoggedUser.Logged().IsAudit == "Y")
                    {
                        if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session, this.txtshortname.Value, Resources.ValidationResources.Insert.ToString(), retConstr(""));
                        }
                        else
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session, this.txtshortname.Value, Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                        }
                    }


                    // If cmdsave.Value() = "Submit" Then
                    // LibObj1.insertLoginFunc(Session("UserName"), lblt1.Text, Session("session"), Trim(Me.txtshortname.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                    // Else
                    // LibObj1.insertLoginFunc(Session("UserName"), lblt1.Text, Session("session"), Trim(Me.txtshortname.Value), "Update", retConStr(Session("LibWiseDBConn")))
                    // End If
                    Session["dptname"] = "";
                    Session["srtname"] = "";
                    cmdsave.Text = Resources.ValidationResources.bSave;
                    // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);

                    tran.Commit();
                    if (tmpcondition == "Y")
                    {
                        this.cmddelete.Visible = false;
                        this.cmdsave.Visible = false;
                    }
                    else
                    {
                        this.cmddelete.Visible = true;
                        this.cmdsave.Visible = true;
                    }
                    this.cmdsave.Visible = true;
                    this.cmddelete.Visible = true;
                    txtshortname.Value = string.Empty;
                    this.SetFocus(txtshortname);
                    ddsymbol.SelectedIndex = 0;
                    cmd.Dispose();
                    // DBConnection.Close()
                    // DBConnection.Dispose()
                    FillGrid();
                }
                // DBConnection.Close()
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        // Me.msglabel.Visible = True
                        // Me.msglabel.Text = ex.Message
                        // LibObj.MsgBox1(Resources.ValidationResources.USaveI.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.USaveI.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.USaveI.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }

                    catch (Exception ex1)
                    {
                        // Me.msglabel.Visible = True
                        // Me.msglabel.Text = ex1.Message
                        // LibObj.MsgBox1(Resources.ValidationResources.USaveI.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.USaveI.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.USaveI.ToString(), this, dbUtilities.MsgLevel.Failure);
                    }
                }
                finally
                {
                    // If DBConnection.State = ConnectionState.Open Then
                    // DBConnection.Close()
                    // End If
                    tran.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ClassCon.Close();
                ClassCon.Dispose();
            }

        }

        public void clear_field()
        {
           
            txtshortname.Value = "";
            this.SetFocus(txtshortname);
            
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            try
            {
                hdTop.Value = Resources.ValidationResources.RBTop;
                this.SetFocus(ddsymbol);
                // LibObj.SetFocus("ddsymbol", Me)
                ddsymbol.SelectedIndex = 0;
                clear_field();
                Session["shortname"] = "";
                Session["dptname"] = "";
                cmdsave.Text = Resources.ValidationResources.bSave;
                this.DataGrid1.SelectedIndex = -1;
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Visible = false;
                    this.cmdsave.Visible = false;
                }
                else
                {
                    this.cmddelete.Visible = true;
                    this.cmdsave.Visible = true;
                }
                txtshortname.Disabled = false;
                cmddelete.Visible = true;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }

        }

        protected void DataGrid1_ItemCommand1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            var DBConnection = new OleDbConnection(retConstr(""));
            DBConnection.Open();
            var ds = new DataSet();
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            txtshortname.Disabled = false;
                            this.cmddelete.Visible = true;
                            // Dim DBConnection As OleDbConnection = New OleDbConnection
                            // DBConnection.ConnectionString = retConStr(Session("LibWiseDBConn"))
                            // DBConnection.Open()
                            // Dim ds As DataSet
                            // ds = New DataSet
                            ds = LibObj.PopulateDataset("Select SymbolType,Symbol,SymbolTypeId from Symbols where SymbolTypeId=" + DataGrid1.Items[e.Item.ItemIndex].Cells[0].Text, "Symbols", DBConnection);
                            // ddsymbol.SelectedValue = ds.Tables("Symbols").Rows(0).Item("SymbolType")
                            // ddsymbol.SelectedIndex = ds.Tables("Symbols").Rows(0).Item("SymbolTypeId")
                            ddsymbol.SelectedIndex = ddsymbol.Items.IndexOf(ddsymbol.Items.FindByText(ds.Tables["Symbols"].Rows[0]["SymbolType"].ToString()));
                            // Me.ddsymbol.SelectedIndex = (Me.ddsymbol.Items.IndexOf(Me.ddsymbol.Items.FindByText(ds.Tables("Symbols").Rows(0).Item(2))))

                            txtshortname.Value = ds.Tables["Symbols"].Rows[0]["Symbol"].ToString();
                            this.txtdepartmentcode.Value = ds.Tables["Symbols"].Rows[0]["SymbolTypeId"].ToString();

                            // ds.Dispose()
                            // DBConnection.Close()
                            // DBConnection.Dispose()
                            if (tmpcondition == "Y")
                            {
                                this.cmddelete.Visible = true;
                                this.cmdsave.Visible = true;
                                cmdsave.Text = Resources.ValidationResources.bUpdate;

                            }
                            else
                            {
                                this.cmddelete.Visible = true;
                                this.cmdsave.Visible = true;
                            }
                            // ''''''''''''''''''''''''''''''''''''''''''''''text'''''''''
                            Session["dptname"] = ddsymbol.SelectedItem.Value;
                            // '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            Session["srtname"] = txtshortname.Value;
                            break;
                        }
                        // DBConnection.Close()
                        // ds.Dispose()
                        // DBConnection.Dispose()
                }
                this.SetFocus(txtshortname);
            }
           

            catch (Exception ex)
            {
               
                message.PageMesg(Resources.ValidationResources.URetriveI.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                ds.Dispose();
                DBConnection.Close();
                DBConnection.Dispose();

            }
        }

        protected void DataGrid1_PageIndexChanged1(object source, DataGridPageChangedEventArgs e)
        {
            var DBConnection = new OleDbConnection(retConstr(""));
            DBConnection.Open();
            var ds = new DataSet();
            try
            {

                this.SetFocus(DataGrid1);
                string searchqry;
                searchqry = "SELECT *  FROM Symbols order by SymbolType";
                // Dim DBConnection As OleDbConnection
                // DBConnection = New OleDbConnection
                // DBConnection.ConnectionString = retConStr(Session("LibWiseDBConn"))
                // DBConnection.Open()
                // Dim ds As DataSet
                // ds = New DataSet
                ds = LibObj.PopulateDataset(searchqry, "Symbols", DBConnection);
                var dt = ds.Tables["Symbols"];
                var dv = new DataView(dt);
                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = DataGrid1.Attributes["SymbolType"];
                DataGrid1.DataSource = dv;
                DataGrid1.DataBind();
                hdnGrdId.Value = DataGrid1.ClientID;
            }

            // ds.Dispose()
            // DBConnection.Close()
            // DBConnection.Dispose()
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ds.Dispose();
                DBConnection.Close();
                DBConnection.Dispose();
            }


        }


        protected void cmddelete_Click(object sender, EventArgs e)
         {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            OleDbCommand cmd = null;
            OleDbTransaction tran = null;
            try
            {

                cmd = new OleDbCommand("Select SymbolTypeId from Symbols where SymbolTypeId='" + txtdepartmentcode.Value + "' and Symbol=N'" + this.txtshortname.Value + "' and SymbolType=N'" + this.ddsymbol.SelectedValue + "' ", deptmastercon);
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
                if (this.txtshortname.Value == string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Success);
                    this.SetFocus(txtshortname);
                }

                else if (LibObj.checkChildExistancewc("SymbolTypeId", "Symbols", "SymbolTypeId='" + txtdepartmentcode.Value + "'", deptmastercon) == false)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me) 'Currentl displayed record does not exist in database
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelNotExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtshortname);
                }

                // ElseIf LibObj.checkChildExistance("SymbolTypeId", "staffmaster", "SymbolTypeId='" & Val(txtdepartmentcode.Value) & "'", retConStr(Session("LibWiseDBConn"))) = True Then
                // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                // ElseIf LibObj.checkChildExistance("SymbolTypeId", "CircUserManagement", "SymbolTypeId='" & Val(txtdepartmentcode.Value) & "'", retConStr(Session("LibWiseDBConn"))) = True Then
                // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                else
                {
                    tran = deptmastercon.BeginTransaction();
                    var delcom = new OleDbCommand("delete from Symbols where SymbolTypeId='" + txtdepartmentcode.Value + "'", deptmastercon);
                    delcom.CommandType = CommandType.Text;
                    delcom.Transaction = tran;
                    try
                    {
                        delcom.ExecuteNonQuery();

                        // Dim temp As String = String.Empty
                        delcom.Parameters.Clear();
                        if (LoggedUser.Logged().IsAudit == "Y")
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblt1.Text, LoggedUser.Logged().Session, this.txtshortname.Value, Resources.ValidationResources.bDelete.ToString(), retConstr(""));
                        }


                        tran.Commit();
                        // delcom.Dispose()
                        // deptmastercon.Close()
                        FillAfterDelete(deptmastercon);
                        delcom.Dispose();
                        // deptmastercon.Close()
                        cmdreset_Click(sender, e);
                        //LibObj.SetFocus("ddsymbol", this);
                        hdTop.Value = Resources.ValidationResources.RBTop;
                        // LibObj.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();
                            // msglabel.Visible = True
                            // msglabel.Text = ex1.Message
                            // LibObj.MsgBox1(Resources.ValidationResources.UdelI.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UdelI.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.UdelI.ToString(), this, dbUtilities.MsgLevel.Warning);
                        }

                        catch (Exception ex2)
                        {
                            // msglabel.Visible = True
                            // msglabel.Text = ex2.Message
                            // LibObj.MsgBox1(Resources.ValidationResources.UdelI.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UdelI.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.UdelI.ToString(), this, dbUtilities.MsgLevel.Failure);

                        }
                    }
                }
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


        public void FillAfterDelete(OleDbConnection con)
        {
            try
            {
                // POPULATE GRID AFTER DELETION
                string qryString;
                qryString = "select SymbolType,Symbol,SymbolTypeId  from Symbols order by SymbolType ";
                LibObj.populateAfterDeletion(DataGrid1, qryString, con);
                hdnGrdId.Value = DataGrid1.ClientID;
            }

            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }
    }
    
}