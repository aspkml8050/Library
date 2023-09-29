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
    public partial class PrefixMaster : BaseClass
    {
        
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private static string tmpcondition;
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        private void PageLoad(object sender, EventArgs e)
        {
            this.hdnGrdId.Value = this.grdPS.ClientID;
            //SSA.MakeAccessible(this.grdPS);
            var tmpcon = new OleDbConnection(retConstr(""));
            tmpcon.Open();
            var tmpmasterds = new DataSet();
            txtprefixname.Value = "XXASDAD3213";
            try
            {
                string tmpstr;
                // Me.cmdReturn.CausesValidation = False
                this.txtprefixid.Visible = false;
                cmdreset1.CausesValidation = false;
                cmddelete1.CausesValidation = false;
                cmdSave1.Attributes.Add("ServerClick", "return cmdsave1_Click();");
                //this.msglabel.Visible = false;


                if (!Page.IsPostBack)
                {
                    var cmd1 = new OleDbCommand();
                    cmd1.Connection = tmpcon;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from categoryloadingstatus where id=0";
                    var dr = cmd1.ExecuteReader();
                    byte[] img = new[] { (byte)1, (byte)0, (byte)1, (byte)0 };

                    if (!dr.HasRows)
                    {
                        var cmd2 = new OleDbCommand();
                        cmd2.Connection = tmpcon;
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "insert into categoryloadingstatus  (id,Category_LoadingStatus,Abbreviation,cat_icon,userid) values(0,'NA','N','" + img.ToString() + "' ,'Admin')";
                        cmd2.ExecuteNonQuery();
                    }
                    this.SetFocus(this.txtprefixname);
                    hdTop.Value = "top";
                    this.lblTitle.Text = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];


                    if (tmpcondition == "Y")
                    {
                        cmdSave1.Visible = false;
                        cmddelete1.Visible = false;
                    }
                    else
                    {
                        cmdSave1.Visible = true;
                            cmddelete1.Visible = true;
                    }
                    fillgrid();
                    // Call GeneratePrefixID()
                    //tmpstr = LibObj.populateCommandText("select isnull(max(prefixid),0)+1 prefixid  from prefixmaster", tmpcon);
                    //this.txtprefixid.Value = tmpstr;
                    //tmpcon.Open();
                    //tmpmasterds = LibObj.PopulateDataset("select prefixid,prefixname as prefixname,suffixname,startno,currentposition,case when categoryloadingstatus.category_loadingstatus='None' then '' else categoryloadingstatus.category_loadingstatus end  as Category  from prefixmaster,categoryloadingstatus where prefixmaster.category=categoryloadingstatus.id and prefixid <> 0  order by prefixname", "tmpMaster", tmpcon);
                    //Session["tab"] = tmpmasterds.Tables["tmpMaster"];  // not inuse
                    ////DataGrid1.DataSource = tmpmasterds.Tables["tmpMaster"];
                    //Session["tab"] = (DataTable)Session["tab"];
                    ////DataGrid1.DataBind();
                    //grdPS.AllowPaging = false;
                    //grdPS.DataSource = tmpmasterds.Tables[0];
                    //grdPS.DataBind();
                    //hdnGrdId.Value = grdPS.ClientID;
                    //SSA.MakeAccessible(this.grdPS);

                    // populateCategory()
                    //LibObj.populateDDL(cmbCategory, "select id,category_loadingstatus from categoryloadingstatus", "category_loadingstatus", "id", Resources.ValidationResources.ComboSelect, tmpcon);
                    //cmddelete1.Visible = true;
                }
                return;
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);


            }
            finally
            {
                if (tmpcon.State == ConnectionState.Open)
                {
                    tmpcon.Close();
                }
                tmpmasterds.Dispose();
                tmpcon.Dispose();
            } 

        }

        public void fillgrid()
        {
            string tmpstr;
            var tmpcon = new OleDbConnection(retConstr(""));
            tmpcon.Open();
            var tmpmasterds = new DataSet();

            tmpstr = LibObj.populateCommandText("select isnull(max(prefixid),0)+1 prefixid  from prefixmaster", tmpcon);
            this.txtprefixid.Value = tmpstr;
            
            tmpmasterds = LibObj.PopulateDataset("select prefixid,prefixname as prefixname,suffixname,startno,currentposition,case when categoryloadingstatus.category_loadingstatus='None' then '' else categoryloadingstatus.category_loadingstatus end  as Category  from prefixmaster,categoryloadingstatus where prefixmaster.category=categoryloadingstatus.id and prefixid <> 0  order by prefixname", "tmpMaster", tmpcon);
            Session["tab"] = tmpmasterds.Tables["tmpMaster"];  // not inuse
                                                               //DataGrid1.DataSource = tmpmasterds.Tables["tmpMaster"];
            Session["tab"] = (DataTable)Session["tab"];
            //DataGrid1.DataBind();
            grdPS.AllowPaging = false;
            grdPS.DataSource = tmpmasterds;
            grdPS.DataBind();
            hdnGrdId.Value = grdPS.ClientID;
            txtprefixname.Value = tmpmasterds.Tables[0].Rows.Count.ToString();
            cmddelete1.Visible = true;
            
        }


        

        public void cmdSave1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtprefixname.Value.Trim()) && string.IsNullOrEmpty(this.txtsuffix.Value.Trim()))
            {
                // LibObj.MsgBox1("Enter Prefix Or Suffix", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Enter Prefix Or Suffix", Me)
                message.PageMesg("Enter Prefix Or Suffix", this, dbUtilities.MsgLevel.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtstartno.Value))
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Enter Start No", Me)
                message.PageMesg("Enter Start No", this, dbUtilities.MsgLevel.Warning);
                return;

            }
            var deptmastercon1 = new OleDbConnection(retConstr(""));
            deptmastercon1.Open();
            var ds = new DataSet();
            var ds1 = new DataSet();
            try
            {
                string tmpstr;
                SetFocus(txtprefixname);
                if ((cmdSave1.Text ?? "") == (Resources.ValidationResources.bUpdate ?? ""))
                {

                }
                if (!string.IsNullOrEmpty(txtprefixid.Value)) // update
                {
                    string sQr;
                    if (!string.IsNullOrEmpty(txtprefixname.Value) & !string.IsNullOrEmpty(this.txtsuffix.Value))
                    {
                        sQr = "select COUNT(*) from PrefixMaster where prefixname='" + txtprefixname.Value.Trim() + "' and suffixname='" + this.txtsuffix.Value.Trim() + "' and prefixid<>" + this.txtprefixid.Value;
                    }
                    else if (!string.IsNullOrEmpty(txtprefixname.Value))
                    {
                        sQr = "select COUNT(*) from PrefixMaster where prefixname='" + txtprefixname.Value.Trim() + "'  and prefixid<>" + this.txtprefixid.Value;
                    }
                    else
                    {
                        sQr = "select COUNT(*) from PrefixMaster where  suffixname='" + this.txtsuffix.Value.Trim() + "' and prefixid<>" + txtprefixid.Value;

                    }
                
                    var Exi = LibObj.PopulateDataset(sQr, "k", deptmastercon1);
                    if (Exi.Tables[0].Rows[0][0].ToString()!= "0")
                    {
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Duplicate found, Not updated", Me)
                        message.PageMesg("Duplicate found, Not updated", this, dbUtilities.MsgLevel.Warning);
                        this.SetFocus(txtprefixname);
                        return;

                    }

                }

                bool flg; // invPrefixN
                flg = LibObj.ValidateAccPrefix(txtprefixname.Value);
                if (flg == false)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.invPrefixN.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.invPrefixN.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.invPrefixN.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtprefixname);
                    return;
                }
                flg = LibObj.ValidateAccPrefix(txtsuffix.Value);
                if (flg == false)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.invSuffixN.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.invSuffixN.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.invSuffixN.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtsuffix);
                    return;
                }
                if (string.IsNullOrEmpty(txtprefixid.Value))
                {
                    string sQr;
                    if (!string.IsNullOrEmpty(txtprefixname.Value) & !string.IsNullOrEmpty(this.txtsuffix.Value))
                    {
                        sQr = "select COUNT(*) from PrefixMaster where prefixname='" + txtprefixname.Value.Trim() + "' and suffixname='" + this.txtsuffix.Value.Trim() + "' ";
                    }
                    else if (!string.IsNullOrEmpty(txtprefixname.Value))
                    {
                        sQr = "select COUNT(*) from PrefixMaster where prefixname='" + txtprefixname.Value.Trim() + "'  ";
                    }
                    else
                    {
                        sQr = "select COUNT(*) from PrefixMaster where  suffixname='" + txtsuffix.Value.Trim() + "' ";
                    }
                    var Exi = LibObj.PopulateDataset(sQr, "k", deptmastercon1);
                    if (Exi.Tables[0].Rows[0][0].ToString() != "0")
                    {
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Duplicate found, Not Saved", Me)
                        message.PageMesg("Duplicate found, Not Saved", this, dbUtilities.MsgLevel.Warning);
                        SetFocus(txtprefixname);
                        return;

                    }
                    if ((cmdSave1.Text ?? "") == (Resources.ValidationResources.bSave ?? ""))
                    {
                        // Call GeneratePrefixID()
                        tmpstr = LibObj.populateCommandText("select isnull(max(prefixid),0)+1, prefixid from prefixmaster", deptmastercon1);
                        txtprefixid.Value = tmpstr;
                    }
                    else
                    {


                    }
                    string com_str;
                    com_str = txtprefixname.Value + this.txtstartno.Value;
                    if (Session["prefixid"].ToString() != "0")
                    {
                        int id;
                        id = Convert.ToInt32(Session["prefixid"]);
                        string status;
                        status = LibObj.populateCommandText("Select status from prefixmaster where prefixid='" + id + "'", deptmastercon1);
                        if (status == "U")
                        {
                            // Hidden3.Value = "7"
                            // LibObj.MsgBox1(Resources.ValidationResources.NoEdit.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.NoEdit.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.NoEdit.ToString(), this, dbUtilities.MsgLevel.Warning);
                            return;
                        }
                        else
                        {
                            string tmpr1;
                            tmpr1 = LibObj.populateCommandText("select prefixid from prefixmaster where prefixname=N'" + this.txtprefixname.Value + "' and suffixname= N'" + txtsuffix.Value.Replace("'", "''") + "'", deptmastercon1);

                            if (!string.IsNullOrEmpty(tmpr1) & !(((id.ToString()) ?? "") == (tmpr1 ?? "")))
                            {
                                // Hidden2.Value = "1"
                                // LibObj.MsgBox1(Resources.ValidationResources.PrifixExist.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.PrifixExist.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.PrifixExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                                SetFocus(txtprefixname);
                                return;
                            }
                            else
                            {
                                var prefixmastercom = new OleDbCommand();
                                prefixmastercom.Connection = deptmastercon1;
                                prefixmastercom.CommandType = CommandType.StoredProcedure;
                                prefixmastercom.CommandText = "insert_prefixmaster_1";
                                prefixmastercom.Parameters.Add(new OleDbParameter("@prefixid_1", OleDbType.Integer));
                                prefixmastercom.Parameters["@prefixid_1"].Value = txtprefixid.Value;
                                prefixmastercom.Parameters.Add(new OleDbParameter("@prefixname_2", OleDbType.VarWChar));
                                prefixmastercom.Parameters["@prefixname_2"].Value = txtprefixname.Value;
                                prefixmastercom.Parameters.Add(new OleDbParameter("@startno_3", OleDbType.Decimal));
                                prefixmastercom.Parameters["@startno_3"].Value = (object)txtstartno.Value;
                                prefixmastercom.Parameters.Add(new OleDbParameter("@currentposition_4", OleDbType.Decimal));
                                prefixmastercom.Parameters["@currentposition_4"].Value = (object)txtstartno.Value;
                                prefixmastercom.Parameters.Add(new OleDbParameter("@status_5", OleDbType.Char));
                                prefixmastercom.Parameters["@status_5"].Value = "N";
                                prefixmastercom.Parameters.Add(new OleDbParameter("@Category_6", OleDbType.Integer));
                                prefixmastercom.Parameters["@Category_6"].Value = "0";    // Me.cmbCategory.SelectedItem.Value

                                prefixmastercom.Parameters.Add(new OleDbParameter("@userid_7", OleDbType.VarWChar));
                                prefixmastercom.Parameters["@userid_7"].Value = Session["user_id"];

                                prefixmastercom.Parameters.Add(new OleDbParameter("@suffixname_8", OleDbType.VarWChar));
                                prefixmastercom.Parameters["@suffixname_8"].Value = txtsuffix.Value;

                                prefixmastercom.ExecuteNonQuery();
                                // prefixmastercom.CommandText = "insert_prefixmaster_1 " & CInt(txtprefixid.Value) & ",'" & Trim(txtprefixname.Value) & "'," & Val(txtstartno.Value) & "," & Val(txtstartno.Value)
                                // Hidden1.Value = "1"
                                // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.recsave.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Warning);
                            }
                        }
                    }
                    else
                    {
                        string tmpr1;
                        tmpr1 = LibObj.populateCommandText("select prefixid from prefixmaster where prefixname=N'" + txtprefixname.Value + "' and suffixname= N'" + txtsuffix.Value.Replace("'", "''") + "'", deptmastercon1);
                        if (tmpr1 == "0")
                        {
                            tmpr1 = "";
                        }
                        if (!string.IsNullOrEmpty(tmpr1))
                        {
                            if ((cmdSave1.Text ?? "") == (Resources.ValidationResources.bSave ?? ""))               
                            {
                                // Hidden2.Value = "1"
                                // LibObj.MsgBox1(Resources.ValidationResources.PrifixExist.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.PrifixExist.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.PrifixExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                                SetFocus(this.txtprefixname);
                                return;
                            }
                        }
                        var prefixmastercom = new OleDbCommand();
                        prefixmastercom.Connection = deptmastercon1;
                        prefixmastercom.CommandType = CommandType.StoredProcedure;
                        prefixmastercom.CommandText = "insert_prefixmaster_1";
                        prefixmastercom.Parameters.Add(new OleDbParameter("@prefixid_1", OleDbType.Integer));
                        prefixmastercom.Parameters["@prefixid_1"].Value = txtprefixid.Value;
                        prefixmastercom.Parameters.Add(new OleDbParameter("@prefixname_2", OleDbType.VarWChar));
                        prefixmastercom.Parameters["@prefixname_2"].Value = txtprefixname.Value;
                        prefixmastercom.Parameters.Add(new OleDbParameter("@startno_3", OleDbType.Decimal));
                        prefixmastercom.Parameters["@startno_3"].Value = (object)txtstartno.Value;
                        prefixmastercom.Parameters.Add(new OleDbParameter("@currentposition_4", OleDbType.Decimal));
                        prefixmastercom.Parameters["@currentposition_4"].Value = (object)txtstartno.Value;
                        prefixmastercom.Parameters.Add(new OleDbParameter("@status_5", OleDbType.Char));
                        prefixmastercom.Parameters["@status_5"].Value = "N";
                        prefixmastercom.Parameters.Add(new OleDbParameter("@Category_6", OleDbType.VarWChar));
                        prefixmastercom.Parameters["@Category_6"].Value = "0"; // Me.cmbCategory.SelectedItem.Value

                        prefixmastercom.Parameters.Add(new OleDbParameter("@userid_7", OleDbType.VarWChar));
                        prefixmastercom.Parameters["@userid_7"].Value = Session["user_id"];

                        prefixmastercom.Parameters.Add(new OleDbParameter("@suffixname_8", OleDbType.VarWChar));
                        prefixmastercom.Parameters["@suffixname_8"].Value = txtsuffix.Value;

                        if (!(Hidden2.Value == "1"))
                        {
                            prefixmastercom.ExecuteNonQuery();
                            // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);

                            this.SetFocus(this.txtprefixname);
                            prefixmastercom.Dispose();
                            // clearfields(deptmastercon1)
                            cmdSave1.Text = Resources.ValidationResources.bSave;
                        }
                    }
                    var prefixmastercomm = new OleDbCommand();
                    prefixmastercomm.Connection = deptmastercon1;
                    // Dim temp As String = String.Empty
                    prefixmastercomm.Parameters.Clear();
                    if (Application["Audit"].ToString() != "0")
                    {
                        // '  If cmdSave.Value() = Resources.ValidationResources.bSave Then                    LibObj1.insertLoginFunc1(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtprefixname.Value), Resources.ValidationResources.Insert.ToString, deptmastercon1)
                        // Else
                        // LibObj1.insertLoginFunc1(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtprefixname.Value), Resources.ValidationResources.bUpdate.ToString, deptmastercon1)
                        // End If
                    }


                    prefixmastercomm.Dispose();
                    clearfields(deptmastercon1);

                    ds = LibObj.PopulateDataset("select prefixid, prefixname,suffixname,startno,currentposition,case when categoryloadingstatus.category_loadingstatus='None' then '' else categoryloadingstatus.category_loadingstatus end  as Category  from prefixmaster,categoryloadingstatus where prefixmaster.category=categoryloadingstatus.id and prefixid <> 0 order by prefixname", "tmpMaster", deptmastercon1);
                    Session["tab"] = ds.Tables["tmpMaster"];
                    //this.DataGrid1.DataSource = ds.Tables["tmpMaster"];
                    Session["tab"] = (DataTable)Session["tab"];
                    //this.DataGrid1.DataBind();

                    Session["prefixid"] = 0;

                    grdPS.DataSource = ds;
                    grdPS.DataBind();
                    hdnGrdId.Value = grdPS.ClientID;
                    //SSA.MakeAccessible(this.grdPS);

                    return;

                }
            }
            catch (Exception ex)
            {
                message.PageMesg(Resources.ValidationResources.UnsavePrifix.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (deptmastercon1.State == ConnectionState.Open)
                {
                    deptmastercon1.Close();
                }
                // ds.Dispose()
                deptmastercon1.Dispose();
            }
        }

        

        private void clearfields(OleDbConnection tmpcon)
        {
            // Try

            this.cmdSave1.Text = Resources.ValidationResources.bSave;
            this.txtprefixname.Value = string.Empty;
            this.txtsuffix.Value = string.Empty;
            this.txtstartno.Value = string.Empty;
            // populateCategory()
            this.txtprefixid.Value = "";
            LibObj.populateDDL(this.cmbCategory, "select id,category_loadingstatus from categoryloadingstatus", "category_loadingstatus", "id", Resources.ValidationResources.ComboSelect, tmpcon);
            this.cmdSave1.Visible = true;
            this.cmddelete1.Visible = true;
            // Catch ex As Exception
            // msglabel.Visible = True
            // msglabel.Text = ex.Message
            // End Try
        }

        protected void cmdreset1_Click(object sender, EventArgs e)
        {
            
                this.SetFocus(this.txtprefixname);
                var prefixmastercon = new OleDbConnection(retConstr(""));
                prefixmastercon.Open();
                try
                {
                    this.cmdSave1.Visible = false;
                    if (tmpcondition == "Y")
                    {
                        this.cmdSave1.Visible = false;
                        this.cmddelete1.Visible = false;
                    }
                    else
                    {
                    //cmdSave1.Visible = true;
                        this.cmddelete1.Visible = true;
                    }
                    clearfields(prefixmastercon);
                    Session["prefixid"] = 0;
                    this.SetFocus(this.txtprefixname);
                    this.hdTop.Value = "top";
                fillgrid();
                }
                catch (Exception ex)
                {
                    // msglabel.Visible = True
                    // msglabel.Text = ex.Message
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }
                finally
                {
                    prefixmastercon.Close();
                    prefixmastercon.Dispose();
                }

            
        }

       
       

        protected void cmddelete1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtprefixid.Value))
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this,dbUtilities.MsgLevel.Warning);
            }
            var delcon = new OleDbConnection(retConstr(""));
            delcon.Open();
            OleDbTransaction tran = null;
            try
            {
                this.SetFocus(this.txtprefixname);
                if (string.IsNullOrEmpty(this.txtprefixname.Value) & string.IsNullOrEmpty(this.txtsuffix.Value))
                {
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (LibObj.checkChildExistance("prefixid", "prefixmaster", "prefixid='" + this.txtprefixid.Value + "'", retConstr("")) == false)
                {
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    tran = delcon.BeginTransaction();
                    var delcom = new OleDbCommand("delete from prefixmaster where prefixid='" + this.txtprefixid.Value + "'", delcon, tran);

                    delcom.Transaction = tran;
                    delcom.CommandType = CommandType.Text;
                    try
                    {
                        delcom.ExecuteNonQuery();
                        delcom.Parameters.Clear();
                        var logged = LoggedUser.Logged();
                        if (logged.IsAudit == "Y")
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lblTitle.Text, logged.Session,(this.txtprefixname.Value),Resources.ValidationResources.bDelete, retConstr(""));
                        }

                        tran.Commit();
                        delcom.Dispose();

                        cmdreset1_Click(sender, e);
                        this.hdTop.Value = "top";
                        // delcon.Close()
                        Response.Redirect(Request.Url.ToString(), true);
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();

                            message.PageMesg(Resources.ValidationResources.UntoDelDeptInfo.ToString(), this, dbUtilities.MsgLevel.Warning);

                            this.SetFocus(this.txtprefixname);
                        }
                        catch (Exception ex2)
                        {
                            message.PageMesg(Resources.ValidationResources.UntoDelDeptInfo.ToString(), this, dbUtilities.MsgLevel.Failure);

                            this.SetFocus(this.txtprefixname);
                        }
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                delcon.Close();
                delcon.Dispose();
            }
        }

        protected void lnkSel_Click(object sender, EventArgs e)
        {
            LinkButton l = (LinkButton)sender;
            GridViewRow g = (GridViewRow)l.NamingContainer;
            HiddenField id = (HiddenField)g.FindControl("hdPid");
            this.txtprefixid.Value = id.Value;
            this.txtprefixname.Value = g.Cells[1].Text.Replace("&nbsp;", "");
            this.txtsuffix.Value = g.Cells[2].Text.Replace("&nbsp;", "");
            this.txtstartno.Value = g.Cells[3].Text;
            this.cmdSave1.Text = "Update";
            this.cmddelete1.Visible = true;
            this.cmdSave1.Visible = true;
        }
    }
}