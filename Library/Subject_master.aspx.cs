using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using Microsoft.VisualBasic;
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
    public partial class Subject_master : BaseClass
    {
        private insertLogin libobj = new insertLogin();
        private insertLogin LibObj1 = new insertLogin();
        private libGeneralFunctions libobject = new libGeneralFunctions();
          private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Put user code to initialize the page here

            var con = new OleDbConnection(retConstr(""));
            con.Open();
            hdnGrdId.Value = grd_media.ClientID;

            var subjectds = new DataSet();
            try
            {

                // If Session("UserName") = String.Empty Then
                // Response.Redirect("default.aspx")
                // End If
                cmddelete.Disabled = true;
                cmdreset.CausesValidation = false;
                cmddelete.CausesValidation = false;
                // cmdReturn.CausesValidation = False
                // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
                // con.Open()
                msglabel.Visible = false;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!Page.IsPostBack)
                {
                   // this.SetFocus(txtsubject);

                    // hdTop.Value = "top"
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
                        lbltitle.Text = Resources.ValidationResources.Lsubj;
                        this.cmdsave.Enabled = true;
                        this.cmddelete.Disabled = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";

                    }
                    cmdsave.Enabled = true;
                    msglabel.Visible = false;
                    // Dim subjectda As New OleDb.OleDbDataAdapter("select * from subject_master order by subject", con)
                    // Dim subjectds As New DataSet
                    // subjectda.Fill(subjectds, "fill")
                    subjectds = libobject.PopulateDataset("select * from subject_master order by subject", "fill", con);
                    grd_media.DataSource = subjectds;
                    grd_media.DataBind();
                    hdnGrdId.Value = grd_media.ClientID;
                    cmddelete.Disabled = true;
                    // cmddelete.Attributes.Add("onclick", "return DoConfirmation();")
                }

                cmddelete.Attributes.Add("ServerClick", "return DoConfirmation();");
                if (tmpcondition == "Y")
                {
                    //Control UControl = LoadControl("mainControl.ascx");
                    //UControl.ID = "MainControl1";
                }
                // Me.PanelTopCont.Controls.Add(UControl)
                else if (tmpcondition == "N")
                {
                    //Control UControl = LoadControl("mainControl.ascx");
                    //UControl.ID = "MainControl1";
                }
                // Me.PanelTopCont.Controls.Add(UControl)
                else
                {
                    // PanelTopCont.Visible = False
                }
                return;
            }
            // con.Close()
            // con.Dispose()
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                subjectds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        
        public void clear_field()
        {
            txtsubject.Value = "";
            cmdsave.Text = Resources.ValidationResources.bSave;
        }

        protected void cmddelete1_Click(object sender, EventArgs e)
        {
            var delcon = new OleDbConnection(retConstr(""));
            delcon.Open();
            try
            {
                //this.SetFocus(txtsubject);
                hdTop.Value = Resources.ValidationResources.RBTop;
                if ((txtsubject.Value) == string.Empty)
                {
                    message.PageMesg(Resources.ValidationResources.rDelSpecify, this, dbUtilities.MsgLevel.Warning);
                }
                else if (libobject.checkChildExistance("subject_id", "subject_master", "subject_id='" + (Hdaccession.Value).Replace("'", "''") + "'", retConstr("")) == false)
                {
                   
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (libobject.checkChildExistance("subject_id", "subject_master", "subject_id='" + (Hdaccession.Value).Replace("'", "''") + "'", retConstr("")) == true)
                {
                    
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this,dbUtilities.MsgLevel.Warning);
                }
                else if (libobject.checkChildExistance("subject1", "bookcatalog", "subject1=N'" + (Hdaccession.Value).Replace("'", "''") + "'", retConstr("")) == true)
                {
                    
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (libobject.checkChildExistance("subject2", "bookcatalog", "subject2=N'" + (Hdaccession.Value).Replace("'", "''") + "'", retConstr("")) == true)
                {
                   
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (libobject.checkChildExistance("subject3", "bookcatalog", "subject3=N'" + (Hdaccession.Value).Replace("'", "''") + "'", retConstr("")) == true)
                {
                   
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    // Dim delcon As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
                    // delcon.Open()
                    var delcom = new OleDbCommand("delete from subject_master where subject_id='" + (Hdaccession.Value).Replace("'", "''") + "'", delcon);
                    delcom.CommandType = CommandType.Text;
                    delcom.ExecuteNonQuery();
                    delcom.Dispose();

                    // Hidden3.Value = "5"
                    // libobject.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);

                    libobj.insertLoginFunc(LoggedUser.Logged().UserName, "subject_master", LoggedUser.Logged().Session, (this.Hdaccession.Value).Replace("'", "''"), "Delete", retConstr(""));
                    delcon.Close();
                    delcon.Dispose();
                }
                cmdreset1_Click(sender, e);
                //this.SetFocus(txtsubject);
                return;
            }
            catch (Exception ex)
            {
                
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                delcon.Close();
                delcon.Dispose();
            }
        }

        protected void cmdreset1_Click(object sender, EventArgs e)
        {
            if (tmpcondition == "Y")
            {
                this.cmdsave.Enabled = true;
                this.cmddelete.Disabled = false;
            }
            else
            {
                this.cmdsave.Enabled = false;
                this.cmddelete.Disabled = true;
            }
            this.grd_media.SelectedIndex = -1;
            cmdsave.Enabled = true;
            cmddelete1.Visible = true;
            hd_name.Value = " ";
            txtsubject.Value = "";
            cmdsave.Text = Resources.ValidationResources.bSave;
            
            hdTop.Value = Resources.ValidationResources.bSave;
            if (tmpcondition == "N")
            {
                cmdsave.Enabled = false;
            }
            else
            {
                cmdsave.Enabled = true;
            }
        }


        protected void grd_media_ItemCommand(object source,System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            // Dim con As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
                            // con.Open()
                            // Dim da As New OleDbDataAdapter("select * from subject_master where subject_id=" & grd_media.Items(e.Item.ItemIndex).Cells(1).Text, con)
                            // Dim ds As New DataSet
                            // da.Fill(ds, "DepartmentMaster")
                            ds = libobject.PopulateDataset("select * from subject_master where subject_id=" + grd_media.Items[e.Item.ItemIndex].Cells[1].Text, "DepartmentMaster", con);
                            txtsubject.Value = ds.Tables["DepartmentMaster"].Rows[0][1].ToString();
                            hd_name.Value = ds.Tables["DepartmentMaster"].Rows[0][1].ToString();
                            Hdaccession.Value = ds.Tables["DepartmentMaster"].Rows[0][0].ToString();
                            // ds.Dispose()
                            // da.Dispose()
                            // con.Close()
                            // con.Dispose()
                            cmdsave.Text = Resources.ValidationResources.bUpdate;
                            //this.SetFocus(cmdsave);
                            break;
                        }

                }
                cmdsave.Visible = true;
                cmddelete1.Visible = true;
                

                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // Hdsave.Value = "dn"
                // libobject.MsgBox1(Resources.ValidationResources.UntoRetriveSubIbfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UntoRetriveSubIbfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UntoRetriveSubIbfo.ToString(), this,dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        private void grd_media_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            try
            {

                this.SetFocus(grd_media);
                string searchqry;
                searchqry = "select * from subject_master order by subject";
                // Dim con As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
                // con.Open()
                // Dim da As New OleDbDataAdapter(searchqry, con)
                // Dim ds As New DataSet
                // da.Fill(ds, "fill")
                ds = libobject.PopulateDataset(searchqry, "fill", con);
                var dt = ds.Tables["fill"];
                var dv = new DataView(dt);
                grd_media.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = grd_media.Attributes["subject"];
                grd_media.DataSource = dv;
                grd_media.DataBind();
                hdnGrdId.Value = grd_media.ClientID;

                // con.Close()
                // con.Dispose()
                return;
            }
            // con.Close()
            // con.Dispose()
            catch (Exception ex)
            {
                
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var md_ds = new DataSet();
            try
            {
                //this.SetFocus(txtsubject);
                // hdTop.Value = "top"
                OleDbTransaction tran;
                // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
                // con.Open()
                tran = con.BeginTransaction();
                var com = new OleDbCommand();
                com.Connection = con;
                com.Transaction = tran;
                int id;
                if (cmdsave.Text == Resources.ValidationResources.bSave)
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select coalesce(max(subject_id),0,max(subject_id)) from subject_master";
                    id = Convert.ToInt32(com.ExecuteScalar());
                    Hdaccession.Value = Convert.ToString(id + 1);
                }
                com.Parameters.Clear();
                if (cmdsave.Text == Resources.ValidationResources.bUpdate)
                {
                    // If libobject.checkChildExistance("media_type", "material_accompany", "media_type='" & Hdaccession.Value & "'", retConStr(Session("LibWiseDBConn"))) = True Then
                    // txtprice.Disabled = True
                    // Hdsave.Value = "990"
                    // End If
                }
                com.CommandType = CommandType.Text;
                com.CommandText = "select subject from  subject_master order by subject";

                int cnt;
                var md_ad = new OleDbDataAdapter("select media_name,short_name from  media_type", con);
                // Dim md_ad As New OleDb.OleDbDataAdapter
                // Dim md_ds As New DataSet
                md_ad.SelectCommand = com;
                md_ad.Fill(md_ds);
                if (cmdsave.Text == Resources.ValidationResources.bSave)
                {
                    if (md_ds.Tables[0].Rows.Count > 0)
                    {
                        var loopTo = md_ds.Tables[0].Rows.Count - 1;
                        for (cnt = 0; cnt <= loopTo; cnt++)
                        {
                            if (libobject.checkChildExistance("subject", "subject_master", "subject=N'" + txtsubject.Value.Replace("'", "''") + "'", retConstr("")) == true)
                            {
                                // Hdsave.Value = "9"
                                // libobject.MsgBox1(Resources.ValidationResources.SubNameAlExist.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SubNameAlExist.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.SubNameAlExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                                // con.Close()
                                // con.Dispose()
                                return;
                            }
                        }
                    }
                }
                else if (hd_name.Value.ToLower() != txtsubject.Value.ToLower())
                {
                    if (libobject.checkChildExistance("subject", "subject_master", "subject=N'" + (txtsubject.Value).Replace("'", "''") + "'", retConstr("")) == true)
                    {
                        // Hdsave.Value = "9"
                        // libobject.MsgBox1(Resources.ValidationResources.SubNameAlExist.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SubNameAlExist.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SubNameAlExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                        // con.Close()
                        // con.Dispose()
                        return;
                    }
                    // Update Bookcatalog
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select subject from subject_master  where subject_id=" + Hdaccession.Value + "";
                    string str = com.ExecuteScalar().ToString();
                    com.Parameters.Clear();
                    com.CommandType = CommandType.Text;
                    com.CommandText = "update BookCatalog set subject1 =N'" + txtsubject.Value + "' where subject1 =N'" + str + "'";
                    com.ExecuteNonQuery();
                    com.Parameters.Clear();


                }
                md_ad.Dispose();
                md_ds.Dispose();
                com.Parameters.Clear();
                try
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "insert_subject_master_1";

                    com.Parameters.Add(new OleDbParameter("@subject_id_1", OleDbType.Integer));
                    com.Parameters["@subject_id_1"].Value = Hdaccession.Value;
                    /* IIf(Hdaccession.Value == 0, 0, Hdaccession.Value);*/ // Session("Form")

                    com.Parameters.Add(new OleDbParameter("subject_2", OleDbType.VarWChar));
                    com.Parameters["subject_2"].Value = (txtsubject.Value);  // Session("Form")

                    com.Parameters.Add(new OleDbParameter("userid_3", OleDbType.VarWChar));
                    com.Parameters["userid_3"].Value = LoggedUser.Logged().Session;

                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                    if (ViewState["openCond"] == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtsubject');", true);
                    }
                    message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                    // Dim temp As String = String.Empty
                    com.Parameters.Clear();
                    var logged = LoggedUser.Logged();
                    if (logged.IsAudit == "Y")
                    {
                        if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lbltitle.Text, logged.Session, (this.txtsubject.Value), Resources.ValidationResources.Insert, retConstr(""));
                        }
                        else
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lbltitle.Text, logged.Session, this.txtsubject.Value.Trim(), Resources.ValidationResources.bUpdate, retConstr(""));
                        }
                    }
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select * from subject_master order by subject";
                    var md_da = new OleDbDataAdapter();
                    md_da.SelectCommand = com;
                    var ds = new DataSet();
                    md_da.Fill(ds);
                    grd_media.DataSource = ds;
                    grd_media.DataBind();
                    hdnGrdId.Value = grd_media.ClientID;

                    tran.Commit();
                    com.Dispose();
                    // con.Close()
                    cmddelete.Disabled = true;
                    this.cmdsave.Text = Resources.ValidationResources.bSave;
                    // If cmdReturn.Disabled <> True Then//yyyyyyyyyyyyyyyyyyyyyyyyyy
                    txtsubject.Value = string.Empty;
                    // End If//yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy

                    //this.SetFocus(txtsubject);

                    md_da.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        // msglabel.Visible = True
                        // msglabel.Text = ex.Message
                        // Hdsave.Value = "5"
                        // libobject.MsgBox1(Resources.ValidationResources.UTprocess.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UTprocess.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UTprocess.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    catch (Exception ex1)
                    {
                        // msglabel.Visible = True
                        // msglabel.Text = ex1.Message
                        // Hdsave.Value = "5"
                        // libobject.MsgBox1(Resources.ValidationResources.UTprocess.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UTprocess.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UTprocess.ToString(), this, dbUtilities.MsgLevel.Warning);
                        // con.Close()
                        // con.Dispose()
                        return;
                    }

                }
            }
            catch (Exception exMain)
            {
                // msglabel.Visible = True
                // msglabel.Text = exMain.Message
                // Hdsave.Value = "5"
                // libobject.MsgBox1(Resources.ValidationResources.UTprocess.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UntoSaveSubjInfo.ToString, Me)
                // hdUnableMsg.Value = "s"
                // libobject.MsgBox1(Resources.ValidationResources.UntoSaveSubjInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UntoSaveSubjInfo.ToString(), this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }
    }
}