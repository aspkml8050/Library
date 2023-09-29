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
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using System.Web.Services.Description;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace Library
{
    public partial class standard_reply : BaseClass
    {

        private libGeneralFunctions libobject = new libGeneralFunctions();
        private insertLogin libobj = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Put user code to initialize the page here
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var Ds = new DataSet();
            try
            {
                hdnGrdId.Value = grd_reply.ClientID;
                cmddelete.Disabled = true;
                msglabel.Visible = false;
                
                if (!Page.IsPostBack)
                {
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                   
                    lbltitle.Text = Request.QueryString["title"];
                    ViewState["openCond"] = Request.QueryString["title"];
                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        this.cmdsave.Enabled = true;
                        this.cmddelete.Disabled = false;
                    }
                    else if (tmpcondition == "N")
                    {
                        this.cmdsave.Enabled = false;
                        this.cmddelete.Disabled = true;
                    }
                    else
                    {
                        lbltitle.Text = Resources.ValidationResources.Reply;
                        this.cmdsave.Enabled = true;
                        this.cmddelete.Disabled = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    cmdsave.Enabled = true;
                    msglabel.Visible = false;
                    var replyda = new OleDbDataAdapter("select * from standard_reply order by reply", con);
                    var replyds = new DataSet();
                    replyda.Fill(replyds, "fill");
                    grd_reply.DataSource = replyds;
                    grd_reply.DataBind();
                    hdnGrdId.Value = grd_reply.ClientID;
                    cmddelete.Disabled = true;
                    cmdreset.CausesValidation = false;
                    cmddelete.CausesValidation = false;
                    // cmdReturn.CausesValidation = False
                    cmddelete.Attributes.Add("ServerClick", "return DoConfirmation();");
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var Ds = new DataSet();
            try
            {
                hdTop.Value = Resources.ValidationResources.RBTop;
                OleDbTransaction tran;
                tran = con.BeginTransaction();
                var com = new OleDbCommand();
                com.Connection = con;
                com.Transaction = tran;
                int id;
                com.Parameters.Clear();
                if (cmdsave.Text == Resources.ValidationResources.bSave)
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select coalesce(max(cast(reply_id as int)),0,max(cast(reply_id as int))) from standard_reply";
                    id = (int)com.ExecuteScalar();
                    int asa = id == 0 ? 1 : id + 1;
                    Hdaccession.Value = asa.ToString();
                    Hdaccession.Value = id + 1.ToString();
                }
                com.Parameters.Clear();
                if (cmdsave.Text == Resources.ValidationResources.bUpdate)
                {

                }
                // If cmdsave.Text = "Save" Then
                com.CommandType = CommandType.Text;
                com.CommandText = "select reply from standard_reply order by reply";
                if (((hd_name.Value).ToUpper()) != ((Convert.ToString(txtreply.Value))))
                {
                    if (libobject.checkChildExistance("reply", "standard_reply", "reply=N'" + (txtreply.Value).Replace("'", "''") + "'", retConstr("")) == true)
                    {
                        Hdsave.Value = "9";

                        message.PageMesg(Resources.ValidationResources.StdRplyAlExit, this, dbUtilities.MsgLevel.Warning);

                        return;
                    }
                }
                this.cmdsave.Text = Resources.ValidationResources.bSave;

                com.Parameters.Clear();
                try
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "insert_standard_reply_1";

                    com.Parameters.Add(new OleDbParameter("@reply_id_1", OleDbType.Integer));

                    com.Parameters["@reply_id_1"].Value = Hdaccession.Value; // Session("Form")

                    com.Parameters.Add(new OleDbParameter("reply_2", OleDbType.VarWChar));
                    com.Parameters["reply_2"].Value = (txtreply.Value);  // Session("Form")

                    com.Parameters.Add(new OleDbParameter("userid_3", OleDbType.VarWChar));
                    com.Parameters["userid_3"].Value = LoggedUser.Logged().Session;

                    com.ExecuteNonQuery();
                    com.Parameters.Clear();

                    if (ViewState["openCond"] == null)
                    {
                        string returnScript = "";
                        returnScript += "<script language=javascript>";
                        returnScript += "window.returnValue='";
                        returnScript += txtreply.Value;
                        returnScript += "';window.close();";
                        returnScript += "<" + "/" + "script>";
                        Page.RegisterStartupScript("", returnScript);
                    }
                    // Hdsave.Value = "1"
                    // libobject.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);
                    
                    libobj.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, txtreply.Value.Trim(), "Insert", retConstr(""));
                    tran.Commit();
                    // clear_field()
                    com.Parameters.Clear();
                    // com.CommandType = CommandType.Text
                    // com.CommandText = "select * from Standard_Reply order by reply"
                    var mdda = new OleDbDataAdapter("select * from standard_reply order by reply", con);
                    // mdda.SelectCommand = com
                    var Ms = new DataSet();
                    mdda.Fill(Ms, "A");
                    grd_reply.DataSource = Ms;
                    grd_reply.DataBind();
                    hdnGrdId.Value = grd_reply.ClientID;
                    cmddelete.Disabled = true;
                    txtreply.Value = string.Empty;
                    //this.SetFocus(txtreply);
                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();

                        message.PageMesg(Resources.ValidationResources.UTprocess, this, dbUtilities.MsgLevel.Warning);
                    }

                    catch (Exception ex1)
                    {

                        message.PageMesg(Resources.ValidationResources.UTprocess, this, dbUtilities.MsgLevel.Warning);

                        return;
                    }

                }
            }
            catch (Exception exMain)
            {

                Hdsave.Value = "5";

                message.PageMesg(Resources.ValidationResources.UntoSaveSubjInfo, this, dbUtilities.MsgLevel.Failure);


                hdUnableMsg.Value = "s";
            }
            finally
            {
                con.Close();
                con.Dispose();

            }
        }

        protected void grd_reply_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {

            try
            {
                string searchqry;
                searchqry = "select * from standard_reply Order by reply";

                var staffmastercon = new OleDbConnection(retConstr(""));
                staffmastercon.Open();
                var staffmasterda = new OleDbDataAdapter(searchqry, staffmastercon);
                var staffmasterds = new DataSet();
                staffmasterda.Fill(staffmasterds, "st");
                var dt = staffmasterds.Tables["st"];
                var dv = new DataView(dt);
                grd_reply.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = grd_reply.Attributes["reply"];
                grd_reply.DataSource = dv;
                grd_reply.DataBind();
                hdnGrdId.Value = grd_reply.ClientID;
                staffmastercon.Close();
                staffmastercon.Dispose();
                return;
                staffmastercon.Close();
                staffmastercon.Dispose();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }

        }
        protected void grd_reply_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            DataSet Ds;
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            Ds = libobject.PopulateDataset("select * from standard_reply where reply_id=N'" + grd_reply.Items[e.Item.ItemIndex].Cells[1].Text + "'", "A", con);
                            txtreply.Value = Ds.Tables["A"].Rows[0][1].ToString();
                            hd_name.Value = Ds.Tables["A"].Rows[0][1].ToString();
                            Hdaccession.Value = Ds.Tables["A"].Rows[0][0].ToString();
                            cmdsave.Text = Resources.ValidationResources.bUpdate;
                            break;
                        }
                }
                cmdsave.Enabled = true;
                cmddelete.Disabled = false;
                
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                Hdsave.Value = "dn";
               
                message.PageMesg(Resources.ValidationResources.UntoRetriveSubIbfo, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            if (tmpcondition == "Y")
            {
                this.cmdsave.Enabled = true;
                this.cmddelete1.Visible = false;
            }
            else
            {
                this.cmdsave.Enabled = false;
                this.cmddelete1.Visible = true;
            }
            cmdsave.Enabled = true;
            var dt = new DataTable();
            this.grd_reply.DataSource = dt;
            cmddelete.Disabled = true;
            txtreply.Value = "";
            cmdsave.Text = Resources.ValidationResources.bSave;
            //this.SetFocus(txtreply);

            if (tmpcondition == "N")
            {
                cmdsave.Enabled = false;
            }
            else
            {
                cmdsave.Enabled = true;
            }
        }

        protected void cmddelete1_Click(object sender, EventArgs e)
        {
            var delcon = new OleDbConnection(retConstr(""));
            delcon.Open();
            var Ds = new DataSet();
            try
            {
                //this.SetFocus(txtreply);
                hdTop.Value = Resources.ValidationResources.RBTop;
                // Dim ds1 As New DataSet
                Hdaccession.Value = "";
                Ds = libobject.PopulateDataset("select reply_id from standard_reply where reply=N'" + (txtreply.Value).Replace("'", "''") + "'", "table", delcon);
                if (Ds.Tables["table"].Rows.Count > 0)
                {
                    Hdaccession.Value = Ds.Tables["table"].Rows[0][0].ToString();
                }
                if ((txtreply.Value) == string.Empty)
                {
                    // Hidden3.Value = "2"
                    // libobject.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify, this, dbUtilities.MsgLevel.Warning);

                    return;
                }
                else if (libobject.checkChildExistance("reply_id", "standard_reply", "reply_id=N'" + (Hdaccession.Value) + "'", retConstr("")) == false)
                {
                    // Hidden3.Value = "3" 'Currentl displayed record does not exist in database
                    // libobject.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelNotExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelNotExist, this, dbUtilities.MsgLevel.Warning);

                    return;
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);

                    return;
                }
                else
                {
                    var delcom = new OleDbCommand("delete from standard_reply where reply_id=N'" + (Hdaccession.Value) + "'", delcon);
                    delcom.CommandType = CommandType.Text;
                    delcom.ExecuteNonQuery();
                    delcom.Parameters.Clear();
                    delcom.CommandType = CommandType.Text;
                    FillAfterDelete();
                    // libobject.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDel, this, dbUtilities.MsgLevel.Success);

                    libobj.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.Hdaccession.Value.Trim(), "Delete", retConstr(""));
                }

                cmdreset_Click(sender, e);
                //this.SetFocus(txtreply);
                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                delcon.Close();
                delcon.Dispose();
            }
        }
        private void FillAfterDelete()
        {
            try
            {
                var con = new OleDbConnection(retConstr(""));
                con.Open();
                // POPULATE GRID AFTER DELETION
                string qryString;
                qryString = "select * from Standard_Reply order by reply";
                libobject.populateAfterDeletion(grd_reply, qryString, con);
                con.Close();
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