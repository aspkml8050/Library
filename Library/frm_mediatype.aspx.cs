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
    public partial class frm_mediatype : BaseClass
    {
        private insertLogin libobj = new insertLogin();
        private libGeneralFunctions libobject = new libGeneralFunctions();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnGrdId.Value = grd_media.ClientID;
                msglabel.Visible = false;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.SetFocus(txtmedianame);
                if (!Page.IsPostBack)
                {
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    lbltitle.Text = Request.QueryString["title"];
                    tmpcondition = Request.QueryString["condition"];
                    Session["NFormDW"] = null;
                    if (tmpcondition == "Y")
                    {
                        this.cmdsave.Enabled = true;
                        this.cmddelete.Visible = false;
                    }
                    else if (tmpcondition == "N")
                    {
                        this.cmdsave.Enabled = false;
                        this.cmddelete.Visible = true;
                    }
                    else
                    {
                        lbltitle.Text = Resources.ValidationResources.lblMediaT;
                        this.cmdsave.Enabled = true;
                        this.cmddelete.Visible = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    msglabel.Visible = false;
                    this.cmddelete.Visible = true;
                    var con = new OleDbConnection(retConstr(""));
                    con.Open();
                    var mediada = new OleDbDataAdapter("select media_name,short_name,media_id from media_type order by media_name", con);
                    var mediads = new DataSet();
                    mediada.Fill(mediads, "fill");
                    grd_media.DataSource = mediads;
                    grd_media.DataBind();
                    hdnGrdId.Value = grd_media.ClientID;
                    con.Close();
                    con.Dispose();
                    mediads.Dispose();
                    if (tmpcondition == "Y")
                    {
                        //Control UControl = LoadControl("mainControl.ascx");
                        //UControl.ID = "mainControl";
                    }
                    // Me.PanelTopCont.Controls.Add(UControl)
                    else if (tmpcondition == "N")
                    {
                        //Control UControl = LoadControl("mainControl.ascx");
                        //UControl.ID = "mainControl";
                    }
                    // Me.PanelTopCont.Controls.Add(UControl)
                    else
                    {
                        // PanelTopCont.Visible = False
                    }
                    cmdreset.CausesValidation = false;
                    cmddelete.CausesValidation = false;
                    // cmdReturn.CausesValidation = False
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = Err.Description
                message.PageMesg(msglabel.Text, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            try
            {
                // libobject.SetFocus("txtmedianame", Me)
                // hdTop.Value = "top"
                OleDbTransaction tran;
                var con = new OleDbConnection(retConstr(""));
                con.Open();
                tran = con.BeginTransaction();
                var com = new OleDbCommand();
                com.Connection = con;
                com.Transaction = tran;
                int id;
                // com.Parameters.Clear()
                if (cmdsave.Text == Resources.ValidationResources.bSave)
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select coalesce(max(media_id),0,max(media_id)) from media_type";
                    id = Convert.ToInt32(com.ExecuteScalar());
                    Hdaccession.Value = Convert.ToString(id + 1);
                }
                com.Parameters.Clear();

             

                com.CommandType = CommandType.Text;
                com.CommandText = "select media_id,media_name,short_name from  media_type";

                int cnt;
               
                var md_ad = new OleDbDataAdapter();
                var md_ds = new DataSet();
                md_ad.SelectCommand = com;
                md_ad.Fill(md_ds);
                if (cmdsave.Text == Resources.ValidationResources.bUpdate)
                {
                    if (md_ds.Tables[0].Rows.Count > 0)
                    {
                        
                        if (txtmedianame.Value != Hd_name.Value)
                        {
                            if (libobject.checkChildExistance("media_name", "media_type", "media_name=N'" + txtmedianame.Value.Replace("'", "''") + "'", retConstr("")) == true)
                            {
                               
                                message.PageMesg(Resources.ValidationResources.MediaExist.ToString(), this, dbUtilities.MsgLevel.Warning);


                                this.SetFocus(txtmedianame);
                                return;
                            }
                        }
                        if (txtshortname.Value != hd_short.Value)
                        {
                            if (libobject.checkChildExistance("short_name", "media_type", "short_name=N'" + txtshortname.Value.Replace("'", "''") + "'", retConstr("")) == true)
                            {
                               
                                message.PageMesg(Resources.ValidationResources.MShort.ToString(), this, dbUtilities.MsgLevel.Success);


                                this.SetFocus(txtshortname);
                                return;
                            }
                            if (libobject.checkChildExistance("media_type", "material_accompany", "media_type='" + Hdaccession.Value.Replace("'", "''") + "'", retConstr("")) == true)
                            {
                                txtshortname.Disabled = true;
                               
                                message.PageMesg(Resources.ValidationResources.ShortNameExistInChild.ToString(), this, dbUtilities.MsgLevel.Warning);

                            }
                        }

                        // Next
                    }
                    // ************
                    this.cmddelete.Visible = true;
                }
                // *******************
                else
                {
                   
                    if (Hd_name.Value != txtmedianame.Value)
                    {
                        if (libobject.checkChildExistance("media_name", "media_type", "media_name=N'" + txtmedianame.Value.Replace("'", "''") + "'", retConstr("")) == true)
                        {
                           
                            message.PageMesg(Resources.ValidationResources.MediaExist.ToString(), this, dbUtilities.MsgLevel.Warning);


                            this.SetFocus(txtmedianame);
                            return;
                        }
                    }
                    if (hd_short.Value != txtshortname.Value)
                    {
                        if (libobject.checkChildExistance("short_name", "media_type", "short_name=N'" + txtshortname.Value.Replace("'", "''") + "'", retConstr("")) == true)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.MShort.ToString(), this, dbUtilities.MsgLevel.Warning);

                            this.SetFocus(txtshortname);
                            return;
                        }
                    }
                }

                
                md_ad.Dispose();
                md_ds.Dispose();

                // End If

                com.Parameters.Clear();
                try
                {

                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "insert_media_type_1";

                    com.Parameters.Add(new OleDbParameter("@media_id_1", OleDbType.Integer));
                    com.Parameters["@media_id_1"].Value =  Hdaccession.Value; // Session("Form")
                    //IIf(Hdaccession.Value == 0, 0, Hdaccession.Value);
                    com.Parameters.Add(new OleDbParameter("@media_name_2", OleDbType.VarWChar));
                    com.Parameters["@media_name_2"].Value = txtmedianame.Value;  // Session("Form")

                    com.Parameters.Add(new OleDbParameter("@short_name_3", OleDbType.VarWChar));
                    com.Parameters["@short_name_3"].Value = txtshortname.Value;   // Session("Form")


                    com.Parameters.Add(new OleDbParameter("@userid_4", OleDbType.VarWChar));
                    com.Parameters["@userid_4"].Value = Session["user_id"];



                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                    // Hdsave.Value = "1"
                    // libobject.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);

                    this.SetFocus(txtmedianame);
                    hdTop.Value = Resources.ValidationResources.RBTop;


                    if (LoggedUser.Logged().IsAudit == "Y")
                    {
                        if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                        {
                            libobj.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtmedianame.Value, Resources.ValidationResources.bSave, retConstr(""));
                        }
                        else
                        {
                            libobj.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtmedianame.Value, Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                        }
                    }


                   
                    tran.Commit();
                    com.Dispose();
                    con.Close();

                    gridfill();
                   

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('Hdaccession');", true);
                    
                    clear_field();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                       
                        message.PageMesg(Resources.ValidationResources.UnProcessMT.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    catch (Exception ex1)
                    {
                        // msglabel.Visible = True
                        // msglabel.Text = ex1.Message
                        // Hdsave.Value = "5"
                        // libobject.MsgBox1(Resources.ValidationResources.UnProcessMT.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnProcessMT.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UnProcessMT.ToString(), this, dbUtilities.MsgLevel.Failure);
                        return;
                    }
                }
            }
            catch (Exception exMain)
            {
                // msglabel.Visible = True
                // msglabel.Text = exMain.Message
                // Hdsave.Value = "5"
                // libobject.MsgBox1(Resources.ValidationResources.UnProcessMT.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnsaveMTInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnsaveMTInfo.ToString(), this, dbUtilities.MsgLevel.Failure);

                // hdUnableMsg.Value = "s"
                // libobject.MsgBox1(Resources.ValidationResources.UnsaveMTInfo.ToString, Me)
            }
        }

        public void clear_field()
        {
            txtshortname.Disabled = false;
            txtmedianame.Value = "";
            txtshortname.Value = "";
            this.txtmedianame.Disabled = false;
            cmdsave.Text = Resources.ValidationResources.bSave;
        }

        public void gridfill()
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var md_da = new OleDbDataAdapter("select media_name,short_name,media_id from media_type order by media_name", con);
            var md_ds = new DataSet();
            md_da.Fill(md_ds);
            grd_media.DataSource = md_ds;
            grd_media.DataBind();
            hdnGrdId.Value = grd_media.ClientID;

        }

        protected void grd_media_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {

                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            this.cmddelete.Visible = true;
                            var con = new OleDbConnection(retConstr(""));
                            con.Open();
                            var t = "select media_name,short_name,media_id from media_type where media_id = " + grd_media.Items[e.Item.ItemIndex].Cells[3].Text;

                            var da = new OleDbDataAdapter( t , con);
                            var ds = new DataSet();
                            //da.Fill(ds, "DepartmentMaster");
                            var dt = new DataTable();
                            da.Fill(dt);

                            txtmedianame.Value = dt.Rows[0][0].ToString();
                            txtshortname.Value = dt.Rows[0][1].ToString();
                            Hd_name.Value = dt.Rows[0][0].ToString();
                            hd_short.Value = dt.Rows[0][1].ToString();
                            Hdaccession.Value = dt.Rows[0][2].ToString();
                            if (libobject.checkChildExistance("media", "librarysetupinformation", "media=N'" + dt.Rows[0][0].ToString().Replace("'", "''") + "'", retConstr("")) == true)
                            {
                                cmddelete.Visible = true;
                                txtmedianame.Disabled = true;
                                cmdsave.Text = Resources.ValidationResources.bUpdate;
                                // Hdsave.Value = "No"
                                // libobject.MsgBox1(Resources.ValidationResources.MTNotDel.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MTNotDel.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.MTNotDel.ToString(), this, dbUtilities.MsgLevel.Warning);
                                return;
                            }
                            else
                            {
                                txtmedianame.Disabled = false;
                                cmddelete.Visible = true;
                                cmdsave.Enabled = true;
                            }
                            ds.Dispose();
                            da.Dispose();
                            con.Close();
                            con.Dispose();
                            cmdsave.Text = Resources.ValidationResources.bUpdate;
                            break;
                        }
                }
                //libobject.SetFocus("txtmedianame", this);
                return;
            }
            catch (Exception ex)
            {
                
                message.PageMesg(Resources.ValidationResources.UnRetriveMTInfo.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            if (tmpcondition == "Y")
            {
                this.cmdsave.Enabled = true;
                this.cmddelete.Visible = false;
            }
            else if (tmpcondition == "N")
            {
                this.cmdsave.Enabled = false;
                this.cmddelete.Visible = true;
            }
            else
            {
                this.cmdsave.Enabled = true;
                this.cmddelete.Visible = false;
                // Me.cmdReturn.Disabled = True
            }
            this.cmddelete.Visible = true;
            Hd_name.Value = "";
            hd_short.Value = "";
            //libobject.SetFocus("txtmedianame", this);
            hdTop.Value = Resources.ValidationResources.RBTop;
            if (tmpcondition == "N")
            {
                cmdsave.Enabled = false;
            }
            else
            {
                cmdsave.Enabled = true;
            }
            clear_field();
            txtshortname.Disabled = false;
            txtmedianame.Disabled = false;
            cmdsave.Enabled = true;
        }

        protected void grd_media_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            try
            {
                string searchqry;
                searchqry = "select media_name,short_name,media_id from media_type order by media_name";
                var con = new OleDbConnection(retConstr(""));
                con.Open();
                var da = new OleDbDataAdapter(searchqry, con);
                var ds = new DataSet();
                da.Fill(ds, "fill");
                var dt = ds.Tables[0];
                var dv = new DataView(dt);
                grd_media.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = grd_media.Attributes["media_name"];
                grd_media.DataSource = dv;
                grd_media.DataBind();
                cmddelete.Visible = true;
                hdnGrdId.Value = grd_media.ClientID;
                
                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void cmddelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                // libobject.SetFocus("txtmedianame", Me)
                // hdTop.Value = "top"
                var delcon = new OleDbConnection(retConstr(""));
                delcon.Open();
                var cmd = new OleDbCommand("Select media_id from media_type where media_name=N'" + txtmedianame.Value.Trim().Replace("'", "''") + "'  and short_name=N'" + txtshortname.Value.Trim().Replace("'", "''") + "'", delcon);
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Hdaccession.Value = dr.GetValue(0).ToString();
                }
                else
                {
                    Hdaccession.Value = string.Empty;
                }

                // Dim ds As New DataSet
                // ds = libobject.PopulateDataset("select media_id from media_type where media_id='" & Hdaccession.Value & "'", "table", delcon)
                // If ds.Tables(0).Rows.Count > 0 Then
                // Hdaccession.Value = ds.Tables(0).Rows(0).Item(0)
                // End If
                if (txtmedianame.Value.Trim() == string.Empty)
                {
                    // Hidden3.Value = "2"
                    // libobject.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Success);
                }

                else if (libobject.checkChildExistancewc("media_id", "media_type", "media_id='" + Hdaccession.Value.Trim() + "'", delcon) == false)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me) 'Currentl displayed record does not exist in database
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("media_type", "material_accompany", "media_type='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("mediatype", "indentmaster", "mediatype='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("mediatype", "existingbookkinfo", "mediatype='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("media_type", "Direct_invoice_transaction", "media_type='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("media_type", "thesis_accessioning", "media_type='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("giftmediatype", "Giftindentmaster", "giftmediatype='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else if (libobject.checkChildExistancewc("mediatype", "indentmaster", "mediatype='" + Hdaccession.Value.Trim() + "'", delcon) == true)
                {
                    // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }

                else
                {
                    var delcom = new OleDbCommand("delete from media_type where media_id='" + Hdaccession.Value.Trim() + "'", delcon);
                    delcom.CommandType = CommandType.Text;
                    delcom.ExecuteNonQuery();

                    gridfill();
                    fillafterdelete(delcon);

                    // Hidden3.Value = "5"
                    // libobject.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);


                    if (LoggedUser.Logged().IsAudit == "Y")
                    {
                        libobj.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtmedianame.Value.Trim(), Resources.ValidationResources.bDelete.ToString(), retConstr(""));
                    }

                    delcom.Dispose();
                }

                cmdreset_Click(sender, e);

                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = Err.Description
                message.PageMesg(ex.Message , this, dbUtilities.MsgLevel.Warning);
            }
        }
        public void fillafterdelete(OleDbConnection con)
        {
            string sel;
            sel = "select media_name,short_name,media_id from media_type order by media_name";
            //libobject.populateAfterDeletion(grd_media, sel, con);

        }
    }
}