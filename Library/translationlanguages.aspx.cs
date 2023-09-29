using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Library
{
    public partial class translationlanguages : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private static string tmpcondition;
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();


        protected void Page_Load(object sender, EventArgs e)
        {
            var Con = new OleDbConnection(retConstr(""));
            Con.Open();
            var ds = new DataSet();

            try
            {
                hdnGrdId.Value = dgLanguage.ClientID;

                // cmdReturn.CausesValidation = False
                msglabel.Visible = false;
                cmdreset.CausesValidation = false;
                cmddelete.CausesValidation = false;
                cmdsave.Attributes.Add("ServerClick", "return cmdreset_Click();");
                cmddelete.Attributes.Add("ServerClick", "return cmddelete_Click();");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!Page.IsPostBack)
                {
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    // txtLanguageName.Focus()
                    this.SetFocus(txtLanguageName);
                    lbltitle.Text = Request.QueryString["title"];
                    ViewState["openCond"] = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                    Session["NFormDW"] = null;
                    if (tmpcondition == "Y")
                    {
                        this.cmdsave.Visible = false;
                        this.cmddelete.Visible = false;
                    }
                    else if (tmpcondition == "N")
                    {
                        this.cmdsave.Visible = true;
                        this.cmddelete.Visible = true;
                    }
                    else
                    {
                        lbltitle.Text = Resources.ValidationResources.Title_TransLng;
                        this.cmdsave.Visible = false;
                        this.cmddelete.Visible = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    // FontFamiliesForm()
                    // LibObj.SetFocus("txtLanguageName", Me)
                    this.cmdsave.Visible = true;
                    this.cmddelete.Visible = true;
                    // BindDataGrid()
                    // Dim Con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
                    // Con.Open()   
                    // Dim da As New OleDb.OleDbDataAdapter("select * from Translation_Language order by Language_Name", Con)
                    // Dim ds As New DataSet
                    ds = LibObj.PopulateDataset("select * from Translation_Language order by Language_Name", "Translation_Language", Con);
                    // da.Fill(ds, "Translation_Language")
                    dgLanguage.DataSource = ds.Tables["Translation_Language"].DefaultView;
                    dgLanguage.DataBind();
                    hdnGrdId.Value = dgLanguage.ClientID;

                    // ds.Dispose()
                    // da.Dispose()
                    // Con.Close()
                    // Con.Dispose()
                }
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
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ds.Dispose();
                Con.Close();
                Con.Dispose();
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (tmpcondition == "Y")
                {
                    this.cmdsave.Visible = false;
                    this.cmddelete.Visible = false;
                }
                else if (tmpcondition == "N")
                {
                    this.cmdsave.Visible = true;
                    this.cmddelete.Visible = true;
                }
                else
                {
                    this.cmdsave.Visible = false;
                    this.cmddelete.Visible = false;
                    // Me.cmdReturn.Disabled = True
                }
                this.dgLanguage.SelectedIndex = -1;
                this.cmdsave.Visible = true;
                this.cmddelete.Visible = true;
                txtShortName.Value = string.Empty;
                txtLanguageName.Value = string.Empty;
                txtLanguageName.Visible = true;
                txtLanguageName.Focus();
                this.SetFocus(txtLanguageName);
                cmdsave.Text = Resources.ValidationResources.bSave.ToString();
                hdTop.Value = Resources.ValidationResources.RBTop;

            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        public void GenerateTranslationLanguageID(OleDbConnection con)
        {
            // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
            // con.Open()
            var com = new OleDbCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = "select coalesce(max(Language_Id),0,max(Language_Id)) from Translation_Language";
            string tmpstr;
            tmpstr = Convert.ToString(com.ExecuteScalar());
            txtLanguageID.Value = tmpstr == "0" ? "1" : (Convert.ToInt32(tmpstr) + 1).ToString();
            //Val(IIf(Val(tmpstr) == 0, 1, Val(tmpstr) + 1));
            com.Dispose();
            // con.Close()
            // con.Dispose()
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            var con3 = new OleDbConnection(retConstr(""));
            con3.Open();
            var ds = new DataSet();
            try
            {
                if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                {
                    GenerateTranslationLanguageID(con3);
                }
                if (txtLanguageName.Value == string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.EnLang.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.EnLang.ToString, Me)
                    // txtLanguageName.Focus()
                    message.PageMesg(Resources.ValidationResources.EnLang.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtLanguageName);
                    return;
                }
                if (Regex.IsMatch(txtLanguageName.Value, "^[0-9 ]+$"))
                {
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Please Enter Only Alphabate.....!!!!!", Me)
                    message.PageMesg("Please Enter Only Alphabate.....!!!!!", this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtLanguageName);
                    return;
                }
                if (this.Hidden3.Value != txtLanguageName.Value)
                {
                    var com2 = new OleDbCommand();
                    com2.Connection = con3;
                    var md_ad = new OleDbDataAdapter();
                    // Dim md_ds As New DataSet
                    com2.CommandType = CommandType.Text;
                    com2.CommandText = "select Language_Name from Translation_Language order by Language_Name";
                    md_ad.SelectCommand = com2;
                    md_ad.Fill(ds, "A");
                    int cnt;
                    if (ds.Tables["A"].Rows.Count > 0)
                    {
                        var loopTo = ds.Tables["A"].Rows.Count - 1;
                        for (cnt = 0; cnt <= loopTo; cnt++)
                        {
                            if (LibObj.checkChildExistance("Language_Name", "Translation_Language", "Language_Name=N'" + txtLanguageName.Value.Replace("'", "''") + "'", retConstr("")) == true)
                            {
                                // LibObj.MsgBox1(Resources.ValidationResources.LangAlExist.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.LangAlExist.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.LangAlExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                                // ds.Dispose()
                                md_ad.Dispose();
                                com2.Dispose();
                                // con3.Close()
                                // con3.Dispose()
                                // Me.txtLanguageName.Focus()
                                this.SetFocus(txtLanguageName);
                                return;
                            }
                        }
                    }
                }
                var com = new OleDbCommand();
                string tmpr2;
                com.Connection = con3;
                com.CommandText = "select  Language_Name from Translation_Language where Language_Name=N'" + txtLanguageName.Value.Replace("'", "''") + "'";
                tmpr2 = Convert.ToString(com.ExecuteScalar());
                com.Parameters.Clear();
                var com1 = new OleDbCommand("insert_Translation_Language_1", con3);
                com1.CommandType = CommandType.StoredProcedure;
                com1.Parameters.Add(new OleDbParameter("@Language_Id_1", OleDbType.Integer));
                com1.Parameters["@Language_Id_1"].Value = (object)txtLanguageID.Value;
                com1.Parameters.Add(new OleDbParameter("@Language_Name_2", OleDbType.VarWChar));
                com1.Parameters["@Language_Name_2"].Value = (txtLanguageName.Value);
                com1.Parameters.Add(new OleDbParameter("@Font_Name_3", OleDbType.VarWChar));
                com1.Parameters["@Font_Name_3"].Value = txtShortName.Value;
                com1.Parameters.Add(new OleDbParameter("@userid_4", OleDbType.VarWChar));
                com1.Parameters["@userid_4"].Value = "Admin";

                com1.ExecuteNonQuery();


                // Dim temp As String = String.Empty
                com1.Parameters.Clear();
                if (LoggedUser.Logged().IsAudit == "Y")
                {
                    if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtLanguageName.Value, Resources.ValidationResources.bSave.ToString(), retConstr(""));
                    }
                    else
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtLanguageName.Value, Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                    }

                }

                if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                {
                    if (!(tmpr2 == string.Empty))
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.LangAlExist.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.LangAlExist.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.LangAlExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else
                    {
                        // com1.ExecuteNonQuery()
                        // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                    }
                }
                else
                {
                    // com1.ExecuteNonQuery()
                    // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                }
                // txtLanguageName.Focus()
                this.SetFocus(txtLanguageName);
                txtLanguageName.Value = string.Empty;
                this.txtLanguageName.Disabled = false;
                // Dim da1 As New OleDb.OleDbDataAdapter("Select * from Translation_Language order by Language_Name", con3)
                // Dim ds As New DataSet
                // da1.Fill(ds, "Translation_Language")
                ds = LibObj.PopulateDataset("Select * from Translation_Language order by Language_Name", "Translation_Language", con3);
                dgLanguage.DataSource = ds.Tables["Translation_Language"].DefaultView;
                dgLanguage.DataBind();
                hdnGrdId.Value = dgLanguage.ClientID;

                // ds.Dispose()
                // da1.Dispose()
                cmdsave.Text = Resources.ValidationResources.bSave;
                this.txtShortName.Value = string.Empty;
                if (ViewState["openCond"] == null)
                {
                    // Dim returnScript As String = ""
                    // returnScript &= "<script language='javascript' type='text/javascript'>"
                    // returnScript &= "javascript:retOnSC('txtLanguageID');"
                    // returnScript &= "<" & "/" & "script>"
                    // Page.RegisterStartupScript("", returnScript)
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtLanguageID');", true);
                    // Dim returnScript As String = ""
                    // returnScript &= "<script language=javascript>"
                    // returnScript &= "window.returnValue='"
                    // returnScript &= txtLanguageID.Value
                    // returnScript &= "';window.close();"
                    // returnScript &= "<" & "/" & "script>"
                    // Page.RegisterStartupScript("", returnScript)
                }
            }
            catch (Exception ex)
            {
                message.PageMesg(Resources.ValidationResources.UnsaveLangName.ToString(), this, dbUtilities.MsgLevel.Failure);

            }
            ds.Dispose();
            con3.Close();
            con3.Dispose();
        }

        protected void cmddelete_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            try
            {
                // LibObj.SetFocus("txtLanguageName", Me)
                this.SetFocus(txtLanguageName);
                var cmd = new OleDbCommand("Select Language_Id from Translation_Language where Language_Name=N'" + (this.txtLanguageName.Value).Replace("'", "''") + "'  and Font_Name=N'" + (this.txtShortName.Value).Replace("'", "''") + "'", con);
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtLanguageID.Value = dr.GetValue(0).ToString();
                }
                else
                {
                    txtLanguageID.Value = string.Empty;
                }
                if (txtLanguageName.Value == string.Empty)
                {
                    // Hidden2.Value = "2"

                    // LibObj.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
                    // con.Open()
                    // If LibObj.checkChildExistancewc("language_id", "bookcatalog", "language_id=" & txtLanguageID.Value, con) = True Then
                    // hdUnableMsg.Value = "child"
                    if (LibObj.checkChildExistancewc("Language_id", "Translation_Language", "Language_id='" + txtLanguageID.Value + "'", con) != true)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me) 'Currentl displayed record does not exist in database
                        message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else if (LibObj.checkChildExistancewc("Language_id", "bookcatalog", "Language_id='" + txtLanguageID.Value + "'", con) == true)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'Currentl displayed record does not exist in database
                        message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else if (LibObj.checkChildExistancewc("Language_id", "Giftindentmaster", "Language_id='" + txtLanguageID.Value + "'", con) == true)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) ''current record check in data table
                        message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else if (LibObj.checkChildExistancewc("Language_id", "indentmaster", "Language_id='" + txtLanguageID.Value + "'", con) == true)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                        message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else if (LibObj.checkChildExistancewc("tran_language", "journalmaster", "tran_language='" + txtLanguageID.Value + "'", con) == true)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                        message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                    else if (LibObj.checkChildExistancewc("Language_Id", "existingbookkinfo", "Language_Id='" + txtLanguageID.Value + "'", con) == true)
                    {
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                        // ElseIf LibObj.checkChildExistancewc("publisherid", "existingbookinfo", "publisherid='" & Val(txtLanguageID.Value) & "'", con) = True Then
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                        // ElseIf LibObj.checkChildExistancewc("dept", "existingbookinfo", "dept='" & Val(txtLanguageID.Value) & "'", con) = True Then
                        // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                        message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }


                    else
                    {
                        var com = new OleDbCommand("delete  from Translation_Language where Language_Id='" + txtLanguageID.Value + "'", con);
                        com.CommandType = CommandType.Text;
                        com.ExecuteNonQuery();
                        //.Replace("'", "''")
                        // Dim da2 As New OleDb.OleDbDataAdapter("select Language_Name,Language_Id,Font_Name from Translation_Language order by Language_Name", con)
                        // Dim ds2 As New DataSet
                        // da2.Fill(ds2)
                        ds = LibObj.PopulateDataset("select Language_Name,Language_Id,Font_Name from Translation_Language order by Language_Name", "A", con);
                        dgLanguage.DataSource = ds.Tables["A"].DefaultView;
                        dgLanguage.DataBind();
                        hdnGrdId.Value = dgLanguage.ClientID;

                        // con.Close()
                        // con.Dispose()
                        // ds.Dispose()
                        // da.Dispose()
                        // Hidden2.Value = "15"
                        // LibObj.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);

                        if (LoggedUser.Logged().IsAudit == "Y")
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, (this.txtLanguageName.Value), Resources.ValidationResources.bDelete.ToString(), retConstr(""));
                        }
                        com.Dispose();
                    }

                    txtLanguageName.Value = "";
                    txtShortName.Value = string.Empty;
                    cmdsave.Text = Resources.ValidationResources.bSave;
                    // LiObj1.insertLoginFunc(Session("UserName"), "Translation_Language", Session("session"), Trim(Me.txtLanguageID.Value), "Delete", retConStr(Session("LibWiseDBConn")))
                }
                // con.Dispose()
                // con.Close()

                ds = LibObj.PopulateDataset("select * from Translation_Language order by Language_Name", "Translation_Language", con);
                // da.Fill(ds, "Translation_Language")
                dgLanguage.DataSource = ds.Tables["Translation_Language"].DefaultView;
                dgLanguage.DataBind();
                hdnGrdId.Value = dgLanguage.ClientID;

                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        private void dgLanguage_PageIndexChanged1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            try
            {
                SetFocus(dgLanguage);
                string searchqry;
                searchqry = "select * from translation_language order by Language_Name";
                ds = LibObj.PopulateDataset(searchqry, "translation_language", con);
                var dt = ds.Tables["translation_language"];
                var dv = new DataView(dt);
                dgLanguage.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = dgLanguage.Attributes["Language_Name"];
                dgLanguage.DataSource = dv;
                dgLanguage.DataBind();
                hdnGrdId.Value = dgLanguage.ClientID;

                dt.Dispose();
                dv.Dispose();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                ds.Dispose();
                con.Dispose();
            }
        }

        protected void dgLanguage_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            var deptmasterds = new DataSet();
            try
            {
                SetFocus(dgLanguage);
                object strsort = dgLanguage.Attributes["Language_Name"];
                dgLanguage.Attributes["Language_Name"] = e.SortExpression;
                string searchqry;
                searchqry = "select * from translation_language order by Language_Name";
                deptmasterds = LibObj.PopulateDataset(searchqry, "language", deptmastercon);
                var dt = deptmasterds.Tables["language"];
                var dv = new DataView(dt);
                dv.Sort = dgLanguage.Attributes["Language_Name"];
                dgLanguage.DataSource = dv;
                dgLanguage.DataBind();
                hdnGrdId.Value = dgLanguage.ClientID;

                deptmastercon.Close();
                dt.Dispose();
                dv.Dispose();
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
                deptmasterds.Dispose();
                deptmastercon.Dispose();
            }
        }

        protected void dgLanguage_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
            var Con = new OleDbConnection(retConstr(""));
            Con.Open();
            var ds = new DataSet();
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            this.txtLanguageName.Disabled = false;
                        }
                        ds = LibObj.PopulateDataset("Select Language_Name,Language_Id, Font_Name  from Translation_Language where Language_Id=" + dgLanguage.Items[e.Item.ItemIndex].Cells[1].Text, "Translation_Language", Con);
                        txtLanguageName.Value = ds.Tables["Translation_Language"].Rows[0][0].ToString();
                        this.Hidden3.Value = ds.Tables["Translation_Language"].Rows[0][0].ToString();
                        this.txtLanguageID.Value = ds.Tables["Translation_Language"].Rows[0][1].ToString();
                        txtShortName.Value = ds.Tables["Translation_Language"].Rows[0][2].ToString();
                        if (LibObj.checkChildExistance("def_language", "librarysetupinformation", "def_language=N'" + ds.Tables["Translation_Language"].Rows[0][0].ToString().Trim().Replace("'", "''") + "'", retConstr("")) == true)
                        {
                            cmddelete.Visible = true;
                            txtLanguageName.Disabled = true;
                            cmdsave.Text = Resources.ValidationResources.bUpdate.ToString();
                            message.PageMesg(Resources.ValidationResources.LangNoModify.ToString(), this, dbUtilities.MsgLevel.Warning);
                            // txtShortName.Focus()
                            this.SetFocus(txtShortName);
                            // Con.Close()
                            // Con.Dispose()
                            return;
                        }
                        else
                        {
                            //this.cmddelete.Text = Convert.ToString(false);
                            cmdsave.Text = Convert.ToString(false);
                        }
                        cmddelete.Visible = true;
                        // ds.Dispose()
                        // da.Dispose()
                        // Con.Close()
                        // Con.Dispose()
                        cmdsave.Text = Resources.ValidationResources.bUpdate.ToString();
                        break;
                }


                // txtLanguageName.Focus()
                this.SetFocus(txtLanguageName);
                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // hdUnableMsg.Value = "d"
                // LibObj.MsgBox1(Resources.ValidationResources.UnRetrieveLang.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnRetrieveLang.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnRetrieveLang.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                ds.Dispose();
                Con.Close();
                Con.Dispose();
            }
        }
    }
}