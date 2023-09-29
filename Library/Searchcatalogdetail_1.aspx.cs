using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
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
    public partial class Searchcatalogdetail_1 : BaseClass
    {
        libGeneralFunctions LibObj = new libGeneralFunctions();
        string tmpcondition;
        messageLibrary msgLibrary = new messageLibrary();
        dbUtilities message = new dbUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.cmdlogin.CausesValidation = false;
                // cmdReturn.CausesValidation = False
                this.msglabel.Visible = false;
                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)
                if (!IsPostBack)
                {
             //       LibObj.SetFocus("optDisp", this);

                    Session["lblt3"] = Request.QueryString["title"];
                    //this.lblTitle.Text = Conversions.ToString(Session["lblt3"]);
                    Session["tmpcondition"] = Request.QueryString["condition"];

                    var cn = new OleDbConnection(retConstr(""));
                    cn.Open();

                   // LibObj.populateDDL(this.cmbtype, "select id as ITVal,Item_Type as ITText from Item_Type where id <> 0 order by Item_Type", "ITText", "ITVal", Resources.ValidationResources.ComboSelect, cn);
                   // LibObj.populateDDL(this.cmbbookcategory, "select id,category_loadingStatus from CategoryLoadingStatus where id <>0 order By  category_loadingstatus", "category_loadingstatus", "id", Resources.ValidationResources.ComboSelect, cn);
                    cn.Close();
                    cn.Dispose();

                  //  this.txtaccno.Focus();
                    this.Label2.Visible = true;
                    this.txtSTitle.Visible = false;
                    this.Label1.Visible = false;
                    this.txtaccno.Visible = true;
                    this.optAccNo.Checked = true;
                    //optAccNo_CheckedChanged(sender, e);
                }
            }
            // txtaccno.Attributes.Add("onkeydown", "txtaccno_onkeydown();")
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdlogin2_Click(object sender, EventArgs e)
        {
            var cn = new OleDbConnection(retConstr(""));
            try
            {
                cn.Open();
                if (this.optDisp.Checked)
                {
                    if (this.optTitle.Checked)
                    {
                        if (string.IsNullOrEmpty(txtSTitle.Text))
                        {
                            // hdSearch.Value = "3"
                            // LibObj.MsgBox1(Resources.ValidationResources.SpTitle.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpTitle.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SpTitle, this, dbUtilities.MsgLevel.Warning);
                        }
                        else if (string.IsNullOrEmpty(txtSTitle.Text))
                        {
                            // hdSearch.Value = "I"
                            // LibObj.MsgBox1(Resources.ValidationResources.Bktitle.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.Bktitle.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.Bktitle, this, dbUtilities.MsgLevel.Warning);
                        }
                        else
                        {
                            var libobj = new libGeneralFunctions();
                            var dsAcc = new DataSet();
                            dsAcc = libobj.PopulateDataset("select  accessionnumber,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber) as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix, bookaccessionmaster.Item_type from bookaccessionmaster where ctrl_no=" + txtSTitle.Text + "  and CTRL_NO <>0 order by a1,a2,suffix", "ctrl1", cn);
                            string tmpFAcc = null;
                            if (dsAcc.Tables["ctrl1"].Rows.Count > 0)
                            {
                                tmpFAcc = dsAcc.Tables["ctrl1"].Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(tmpFAcc))
                            {
                                var ds = new DataSet();
                                ds = libobj.PopulateDataset("select a.ctrl_no, isnull( a.booknumber,'') booknumber,b.classnumber , a.Item_type from bookaccessionmaster a join CatalogData b on a.ctrl_no=b.ctrl_no  where accessionnumber=N'" + tmpFAcc + "'", "ctrl", cn);
                                int ctrl_no;
                                if (ds.Tables["ctrl"].Rows.Count > 0)
                                {
                                    ctrl_no = Convert.ToInt32(ds.Tables["ctrl"].Rows[0][0]);
                                    Session["accno"] = tmpFAcc;
                                    Session["back"] = "Search";
                                    Session["BNumber"] = ds.Tables["ctrl"].Rows[0]["booknumber"];
                                    Session["CNumber"] = ds.Tables["ctrl"].Rows[0]["classnumber"];
                                    ds.Dispose();
                                    Response.Redirect("CatalogDetail.aspx?title=" + this.lblTitle.Text + "&ctrl=" + ctrl_no + "&bt=" + this.hdItemType.Value);
                                }
                                else
                                {
                                    // hdSearch.Value = "2"
                                    // libobj.MsgBox1(Resources.ValidationResources.SpcfyVT.ToString, Me)
                                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpcfyVT.ToString, Me)
                                    message.PageMesg(Resources.ValidationResources.SpcfyVT, this, dbUtilities.MsgLevel.Warning);
                                    ds.Dispose();
                                }
                            }
                            else
                            {
                                // ctrl_no not found
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(  txtaccno.Text.Trim()))
                    {
                        // hdSearch.Value = "1"
                        // LibObj.MsgBox1(Resources.ValidationResources.InAccNo.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.InAccNo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.InAccNo, this, dbUtilities.MsgLevel.Warning);
                        cn.Close();
                        cn.Dispose();
                        return; // Accession number not specified
                    }
                    else
                    {
                        var libobj = new libGeneralFunctions();
                        var ds = new DataSet();
                        ds = libobj.PopulateDataset("select a.ctrl_no, isnull( a.booknumber,'') booknumber,b.classnumber from bookaccessionmaster a join CatalogData b on a.ctrl_no=b.ctrl_no where accessionnumber=N'" + txtaccno.Text.Trim() + "' and a.CTRL_NO <>0", "ctrl", cn);
                        int ctrl_no;
                        if (ds.Tables["ctrl"].Rows.Count > 0)
                        {
                            ctrl_no = Convert.ToInt32(ds.Tables["ctrl"].Rows[0][0]);
                            Session["accno"] = this.txtaccno.Text;
                            Session["back"] = "Search";
                            Session["BNumber"] = ds.Tables["ctrl"].Rows[0][1];
                            Session["CNumber"] = ds.Tables["ctrl"].Rows[0][2];
                            ds.Dispose();
                            Response.Redirect("CatalogDetail.aspx?title=" + this.lblTitle.Text + "&ctrl=" + ctrl_no + "&bt=" + this.hdItemType.Value);
                        }

                        else
                        {
                            // hdSearch.Value = "2"
                            // libobj.MsgBox1(Resources.ValidationResources.SpcfyVT.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpcfyVT.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SpcfyVT, this, dbUtilities.MsgLevel.Warning);
                            this.txtaccno.Focus();

                            ds.Dispose();
                        }
                    }
                }
                // on deletion cheched
                else if (this.optTitle.Checked)
                {
                    if (string.IsNullOrEmpty(txtSTitle.Text))
                    {
                        // hdSearch.Value = "3"
                        // LibObj.MsgBox1(Resources.ValidationResources.SpTitle.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpTitle.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SpTitle, this, dbUtilities.MsgLevel.Warning);
                    }

                    else if (string.IsNullOrEmpty(txtSTitle.Text))
                    {
                        // hdSearch.Value = "I"
                        // LibObj.MsgBox1(Resources.ValidationResources.SpcfyVT.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpcfyVT.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SpcfyVT, this, dbUtilities.MsgLevel.Warning);
                    }

                    else
                    {
                        var libobj = new libGeneralFunctions();
                        var dsAcc = new DataSet();
                        dsAcc = libobj.PopulateDataset("select  accessionnumber,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber) as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix, bookaccessionmaster.Item_type from bookaccessionmaster where ctrl_no=" + txtSTitle.Text.Trim() + " and CTRL_NO <>0 order by a1,a2,suffix", "ctrl1", cn);
                        string tmpFAcc = null;
                        if (dsAcc.Tables["ctrl1"].Rows.Count > 0)
                        {
                            tmpFAcc = Convert.ToString(dsAcc.Tables["ctrl1"].Rows[0][0]);
                        }
                        if (!string.IsNullOrEmpty(tmpFAcc))
                        {
                            var ds = new DataSet();
                            ds = libobj.PopulateDataset("select ctrl_no, bookaccessionmaster.Item_type from bookaccessionmaster where accessionnumber=N'" + tmpFAcc + "' and CTRL_NO <>0", "ctrl", cn);
                            int ctrl_no;
                            if (ds.Tables["ctrl"].Rows.Count > 0)
                            {
                                ctrl_no = Convert.ToInt32(ds.Tables["ctrl"].Rows[0][0]);
                                Session["accno"] = tmpFAcc;
                                Session["back"] = "Search";
                                ds.Dispose();
                                Response.Redirect("De_Cataloging.aspx?title=" + this.lblTitle.Text + "&ctrl=" + ctrl_no); // & "&bt=" & hdItemType.Value
                            }
                            else
                            {
                                // hdSearch.Value = "2"
                                // libobj.MsgBox1(Resources.ValidationResources.InAccNo.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.InAccNo.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.InAccNo, this, dbUtilities.MsgLevel.Warning);
                                ds.Dispose();
                            }
                        }
                        else
                        {
                            // ctrl_no not found
                        }
                    }
                }
                else if (string.IsNullOrEmpty(txtaccno.Text.Trim()))
                {
                    // hdSearch.Value = "1"
                    // LibObj.MsgBox1(Resources.ValidationResources.SpcfyAccN.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpcfyAccN.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SpcfyAccN, this, dbUtilities.MsgLevel.Warning);
                    cn.Close();
                    cn.Dispose();

                    return; // Accession number not specified
                }
                else
                {
                    var libobj = new libGeneralFunctions();
                    var ds = new DataSet();
                    ds = libobj.PopulateDataset("select ctrl_no from bookaccessionmaster where accessionnumber=N'" + txtaccno.Text.Trim() + "'  and CTRL_NO <>0", "ctrl", cn);
                    int ctrl_no;
                    if (ds.Tables["ctrl"].Rows.Count > 0)
                    {
                        ctrl_no = Convert.ToInt32(ds.Tables["ctrl"].Rows[0][0]);
                        Session["accno"] = this.txtaccno.Text;
                        Session["back"] = "Search";
                        ds.Dispose();
                        Response.Redirect("De_Cataloging.aspx?title=" + this.lblTitle.Text + "&ctrl=" + ctrl_no); // & "&bt=" & hdItemType.Value
                    }
                    else
                    {
                        // hdSearch.Value = "2"
                        // libobj.MsgBox1(Resources.ValidationResources.InAccNo.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.InAccNo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.InAccNo, this, dbUtilities.MsgLevel.Warning);
                        ds.Dispose();
                    }
                }
                cn.Dispose();
                cn.Close();

                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Dispose();
            }
        }
    }
}