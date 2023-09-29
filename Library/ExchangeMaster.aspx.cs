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
using System.Text;

namespace Library
{
    public partial class ExchangeMaster : BaseClass, ICallbackEventHandler
    {
        private insertLogin LibObj1 = new insertLogin();
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

         string strResult = "";
        public string GetCallbackResult()
        {
            return strResult;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (eventArgument == "chkS")
            {
                var sb1 = new StringBuilder();
                var conn = new OleDbConnection(retConstr(""));
                conn.Open();
                string sqlStr, sqlStr1;
                var lstItem = new System.Web.UI.WebControls.ListItem();
                var da = new OleDbDataAdapter();
                OleDbCommand myCommand;
                OleDbCommand myCommand1;
                var ds = new DataSet();
                int RecordCount;
                if (txtCategory.Value != string.Empty)
                {
                    sqlStr = "select count(currencyname) from exchangemaster where currencyname LIKE N'%" + txtCategory.Value + "%'";
                }
                else
                {
                    sqlStr = "select count(currencyname) from exchangemaster";
                }
                myCommand = new OleDbCommand(sqlStr, conn);
                myCommand.CommandType = CommandType.Text;
                RecordCount = Convert.ToInt32(myCommand.ExecuteScalar());
                if (txtCategory.Value != string.Empty)
                {
                    sqlStr1 = "select distinct currencyname from exchangemaster where currencyname LIKE N'%" + txtCategory.Value + "%' order by currencyname";
                }
                else
                {
                    sqlStr1 = "select distinct currencyname from exchangemaster";
                }
                myCommand1 = new OleDbCommand(sqlStr1, conn);
                myCommand1.CommandType = CommandType.Text;
                da.SelectCommand = myCommand1;
                da.Fill(ds);
                if (RecordCount < 1)
                {
                    sb1.Append("");
                    sb1.Append("^");
                    sb1.Append(Resources.ValidationResources.EntrSrchCrt.ToString());
                    sb1.Append("|");
                }
                else
                {
                    int i = 0;
                    var loopTo = ds.Tables[0].Rows.Count - 1;
                    for (i = 0; i <= loopTo; i++)
                    {
                        sb1.Append(ds.Tables[0].Rows[i]["currencyname"]);
                        sb1.Append("^");
                        sb1.Append(ds.Tables[0].Rows[i]["currencyname"]);
                        sb1.Append("|");
                    }
                }
                da = null;
                conn.Close();
                conn.Dispose();
                strResult = sb1.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var cnadd = new OleDbConnection(retConstr(""));
            try
            {
                //string cbref = Page.ClientScript.GetCallbackEventReference(this, "arg", "clientback", "context");
                //string cbScr = string.Format("function UseCallBack(arg, context) {{ {0}; }} ", cbref);
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), "UseCallBack", cbScr, true);
                //msglabel.Visible = false;
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                cnadd.Open();
                if (!Page.IsPostBack)
                {
                    if (Convert.ToString(Session["UserName"]) == string.Empty)
                    {
                        Response.Redirect("default.aspx");
                    }
                    lblTitle.Text = Request.QueryString["title"];
                    ViewState["openCond"] = Request.QueryString["title"]; // to identify is request to open this page from tree or through shortcut if value is nothing means thro shortcut
                    tmpcondition = Request.QueryString["condition"];
                    Session["NFormDW"] = null;
                    if (tmpcondition == "Y")
                    {
                     //   this.cmdsave.Visible = false;
                       // this.cmddelete.Visible = false;
                    }
                    else if (tmpcondition == "N")
                    {
                        this.cmdsave.Visible = true;
                        this.cmddelete.Visible = true;
                    }
                    else
                    {
                        lblTitle.Text = Resources.ValidationResources.lblExrates;
                  //      this.cmdsave.Visible = false;
                  //      this.cmddelete.Visible = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    // '''''''''''''''shweta26-oct

                    var cmdadd = new OleDbCommand();
                    OleDbTransaction transact;
                    transact = cnadd.BeginTransaction();
                    cmdadd.Connection = cnadd;

                    cmdadd.Transaction = transact;


                    try
                    {
                        cmdadd.CommandType = CommandType.Text;
                        cmdadd.CommandTimeout = 180;
                        cmdadd.Connection = cnadd;
                        // cnadd.Open()
                        string query, conversion;
                        query = "Select CurrencyConversionFactor from librarysetupinformation ";
                        cmdadd.CommandText = query;
                        cmdadd.CommandType = CommandType.Text;
                        conversion = Convert.ToString(cmdadd.ExecuteScalar());
                        transact.Commit();
                        if (conversion == Resources.ValidationResources.LBnk)
                        {
                            this.Label10.Visible = true;
                        }

                        else
                        {
                            this.Label9.Visible = true;


                        }
                    }

                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        // If cnadd.State = ConnectionState.Open Then
                        // cnadd.Close()
                        // cnadd.Dispose()
                        // End If
                        cmdadd.Dispose();
                    }
                    // ''''''''''''''''''''''''''''''''''''''
                    hdTop.Value = "top";
                    // Call GenerateCurrencyCode()
                    //hdCulture.Value = Request.Cookies["UserCulture"].Value;
                    clearfields();
                    this.cmddelete.Visible = true;
                    this.cmdsave.Visible = true;
                    this.chkSearch.Checked = false;
                    this.lstAllCategory.Visible = false;
                    this.txtCategory.Visible = false;
                    this.txtCategory.Value = string.Empty;

                    this.btnCategoryFilter.Visible = false;
                    // Me.ddl1.Visible = False
                    this.lstAllCategory.Visible = false;
                    Label8.Visible = false;


                    Txtdate.Text =string.Format("{0:dd-MMM-yyyy}",System.DateTime.Today);
                    txtCategory.Attributes.Add("onkeyup", "txtCategory_onkeyup();");
                    // txtCategory.Attributes.Add("onkeydown", "txtCategory_onkeydown();")
                    // lstAllCategory.Attributes.Add("onclick", "return lstAllCategory_onclick();")
                    // Dim lstItem As System.Web.UI.WebControls.ListItem = New System.Web.UI.WebControls.ListItem

                    // cmdReturn.CausesValidation = False
                    cmddelete.CausesValidation = false;
                    Button1.CausesValidation = false;
                    btnCategoryFilter.CausesValidation = false;
                    cmdreset.CausesValidation = false;
                    cmdsave.Attributes.Add("ServerClick", "return cmdsave_Click();");
                    txtCategory.Attributes.Add("onkeydown", "txtCategory_onkeydown();");
                    this.lstAllCategory.Items.Clear();
                    GetData2(cnadd);

                }


            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (cnadd.State == ConnectionState.Open)
                {
                    cnadd.Close();
                }
                cnadd.Dispose();
            }
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                {
                    GenerateCurrencyCode();
                }

                if (Convert.ToInt32(txtbankrate.Value) > 10000)
                {
                    // Hidden1.Value = "89"
                    // LibObj.MsgBox1(Resources.ValidationResources.Bankrate.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.Bankrate.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.Bankrate.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtbankrate);
                    return; 
                }
                if (Convert.ToInt32(txtgocrate.Value) > 10000)
                {
                    // Hidden1.Value = "88"
                    // LibObj.MsgBox1(Resources.ValidationResources.GOCrate.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.GOCrate.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.GOCrate.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtgocrate);
                    // Me.txtgocrate.Focus()
                    return;
                }

                string name, shortname;
                var exchangemastercon = new OleDbConnection(retConstr(""));
                exchangemastercon.Open();
                var exchangemastercom = new OleDbCommand();

                if (cmdsave.Text == Resources.ValidationResources.bUpdate.ToString())
                {
                    var checkad1 = new OleDbDataAdapter("select currency from librarysetupinformation ", exchangemastercon);
                    var checkds1 = new DataSet();
                    checkad1.Fill(checkds1, "fill");
                    if (checkds1.Tables[0].Rows[0][0].ToString() == (txtcurrencyname.Value).Trim())
                    {
                        // Hidden1.Value = "90"
                        // LibObj.MsgBox1(Resources.ValidationResources.CurrencyNoModify.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CurrencyNoModify.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.CurrencyNoModify.ToString() , this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                    string sQer = "select * from exchangemaster where CurrencyCode<>" + txtcurrencycode.Value + "and CurrencyName='" + txtcurrencyname.Value.Trim() + "'";
                    var AdExi = new OleDbDataAdapter(sQer, exchangemastercon);
                    var dsExi = new DataSet();
                    AdExi.Fill(dsExi, "Ex");
                    if (dsExi.Tables[0].Rows.Count > 0)
                    {
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Currency is duplicate, not updated.", Me)
                        message.PageMesg("Currency is duplicate, not updated.", this, dbUtilities.MsgLevel.Warning);
                        return;

                    }
                }
                if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                {
                    var checkad = new OleDbDataAdapter("select ShortName,CurrencyName from exchangemaster where CurrencyName=N'" + txtcurrencyname.Value.Trim().Replace("'", "''") + "' or shortname=N'" + txtshortname.Value.Trim().Replace("'", "''") + "'", exchangemastercon);
                    var checkds = new DataSet();
                    checkad.Fill(checkds, "fill");
                    if (checkds.Tables[0].Rows.Count > 0)
                    {
                        shortname = Convert.ToString(checkds.Tables[0].Rows[0][0]);
                        name = Convert.ToString(checkds.Tables[0].Rows[0][1]);
                        if ((name).Trim() == (txtcurrencyname.Value).Trim().Replace("'", "''"))
                        {
                            // Hidden1.Value = "6"
                            // LibObj.MsgBox1(Resources.ValidationResources.CurrencyAlExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CurrencyAlExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.CurrencyAlExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                            this.SetFocus(txtcurrencyname);
                            return;
                        }
                        if (shortname.Trim() == (txtshortname.Value).Trim().Replace("'", "''"))
                        {
                            // Hidden1.Value = "short"
                            // LibObj.MsgBox1(Resources.ValidationResources.MShort.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MShort.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.MShort.ToString(), this, dbUtilities.MsgLevel.Warning);
                            this.SetFocus(txtshortname);
                            return;
                        }

                    }
                }
                var ad = new OleDbDataAdapter("select CurrencyConversionFactor from librarysetupinformation", exchangemastercon);
                var ds = new DataSet();
                ad.Fill(ds, "fill");
                string curr_str;
                curr_str = Convert.ToString(ds.Tables[0].Rows[0][0]);
                if ((curr_str == "GOC") && (txtgocrate.Value == "0"))
                {
                    // Hidden6.Value = "1"
                    // LibObj.MsgBox1(Resources.ValidationResources.EnterGOC.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.EnterGOC.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.EnterGOC, this, dbUtilities.MsgLevel.Warning);

                    this.SetFocus(txtgocrate);
                    return;
                }
                else if ((curr_str == "BANK") && (txtbankrate.Value) == "0")
                {
                    // Hidden6.Value = "11"'""""""""""""""""""""""""""""""""""""""""""""""
                    // LibObj.MsgBox1(Resources.ValidationResources.EnterBank.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.EnterBank.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.EnterBank.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtbankrate);
                    return;
                }

                exchangemastercom.Connection = exchangemastercon;
                exchangemastercom.CommandType = CommandType.StoredProcedure;

                exchangemastercom.CommandText = "insert_exchangemaster_1";
                exchangemastercom.Parameters.Add(new OleDbParameter("@CurrencyCode_1", OleDbType.Integer));
                exchangemastercom.Parameters["@CurrencyCode_1"].Value = (txtcurrencycode.Value).Trim();
                exchangemastercom.Parameters.Add(new OleDbParameter("@ShortName_2", OleDbType.VarWChar));
                exchangemastercom.Parameters["@ShortName_2"].Value = (txtshortname.Value).Trim();
                exchangemastercom.Parameters.Add(new OleDbParameter("@CurrencyName_3", OleDbType.VarWChar));
                exchangemastercom.Parameters["@CurrencyName_3"].Value = (txtcurrencyname.Value).Trim();
                exchangemastercom.Parameters.Add(new OleDbParameter("@GocRate_4", OleDbType.Decimal));
                exchangemastercom.Parameters["@GocRate_4"].Value = txtgocrate.Value;
                exchangemastercom.Parameters.Add(new OleDbParameter("@BankRate_5", OleDbType.Decimal));
                exchangemastercom.Parameters["@BankRate_5"].Value = txtbankrate.Value;
                exchangemastercom.Parameters.Add(new OleDbParameter("@EffectiveFrom_6", OleDbType.Date));
                exchangemastercom.Parameters["@EffectiveFrom_6"].Value = Txtdate.Text;
                exchangemastercom.Parameters.Add(new OleDbParameter("@userid_7", OleDbType.VarWChar));
                exchangemastercom.Parameters["@userid_7"].Value = Session["user_id"];

                exchangemastercom.ExecuteNonQuery();
                // If cmdReturn.Disabled = True Then
                // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "retOnSC('txtcurrencycode');", True)
                // End If
                if (ViewState["openCond"] == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtcurrencycode');", true);
                }
                this.lstAllCategory.Items.Clear();
                GetData2(exchangemastercon);

                exchangemastercom.Parameters.Clear();
                if (LoggedUser.Logged().IsAudit == "Y")
                {

                    if (cmdsave.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, (this.txtcurrencyname.Value).Trim(), Resources.ValidationResources.Insert.ToString(), retConstr(""));
                    }
                    else
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, (this.txtcurrencyname.Value).Trim(), Resources.ValidationResources.bUpdate, retConstr(""));
                    }

                }
                // *************Bi***********
                cmdsave.Text = Resources.ValidationResources.bSave.ToString();
                this.cmddelete.Visible = true;
                // ********************
                txtcurrencyname.Disabled = false;
                txtshortname.Disabled = false;
                // cmdadd.Disabled = False
                exchangemastercom.Dispose();
                exchangemastercon.Close();
                exchangemastercon.Dispose();
                hdTop.Value = "top";

                // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                this.SetFocus(txtcurrencyname);
                // Make The Fields Empty
                clearfields();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message

                // LibObj.MsgBox1(Resources.ValidationResources.UnsaveExcInfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnsaveExcInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnsaveExcInfo.ToString(), this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                this.chkSearch.Checked = false;
                this.lstAllCategory.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;
                this.btnCategoryFilter.Visible = false;
                this.lstAllCategory.Visible = false;
                Label8.Visible = false;
            }
        }

        public void GenerateCurrencyCode()
        {
            var exchangemastercon = new OleDbConnection(retConstr(""));
            exchangemastercon.Open();
            var exchangemastercom = new OleDbCommand();
            exchangemastercom.Connection = exchangemastercon;
            exchangemastercom.CommandType = CommandType.Text;
            exchangemastercom.CommandText = "select coalesce(max(currencycode),0,max(currencycode)) from exchangeMaster";
            string tmpstr;
            exchangemastercom.CommandText = "select isnull(max(currencycode),0)+1 from exchangeMaster";
            tmpstr = Convert.ToString(exchangemastercom.ExecuteScalar());
            txtcurrencycode.Value = tmpstr;
            exchangemastercom.Dispose();
            exchangemastercon.Close();
            exchangemastercon.Dispose();
        }

        public void clearfields()
        {
            try
            {
                txtcurrencyname.Disabled = false;
                txtshortname.Disabled = false;
                // cmdadd.Disabled = False
                txtshortname.Value = string.Empty;
                txtcurrencyname.Value = string.Empty;
                txtgocrate.Value = string.Empty;
                txtbankrate.Value = string.Empty;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            try
            {
                clearfields();
                this.SetFocus(this.txtcurrencyname);
                if (tmpcondition == "Y")
                {
                 //   this.cmdsave.Visible = false;
                 //   this.cmddelete.Visible = false;
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
                this.cmddelete.Visible = true;
                // cmdReturn.Disabled = False
                // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "simpleSearchVis('chksearch','Label8');", True)
                this.chkSearch.Checked = false;
                this.lstAllCategory.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;

                this.btnCategoryFilter.Visible = false;
                // Me.ddl1.Visible = False
                this.lstAllCategory.Visible = false;
                Label8.Visible = false;
                Txtdate.Text = string.Format("{0:dd-MMM-yyyy}", System.DateTime.Today); 
                cmdsave.Text = Resources.ValidationResources.bSave.ToString();
                // LibObj.SetFocus("txtcurrencyname", Me)
                hdTop.Value = "top";
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);

            }
        }

        public void GetData2(OleDbConnection Conn)
        {
            string sqlStr = null;
            if (txtCategory.Value.Trim() != string.Empty)
            {
                sqlStr = "select currencycode, currencyname from exchangemaster where currencyname LIKE N'%" + (txtCategory.Value).Trim().Replace("'", "''") + "%' order by currencyname";
            }
            else
            {
                sqlStr = "select currencycode, currencyname from exchangemaster";
            }
            LibObj.populateGetData2(lstAllCategory, sqlStr, "currencyname", "currencycode", Conn);
        }

        public void Button1_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                if (lstAllCategory.SelectedValue.Trim() != default)
                {
                    var tmpcon = new OleDbConnection(retConstr(""));
                    tmpcon.Open();
                    string str;
                    Session["check"] = 1;
                    str = "select * from exchangemaster where currencyname=N'" + lstAllCategory.SelectedItem.Value.Replace("'", "''") + "' and  EffectiveFrom =(select max(EffectiveFrom) from exchangemaster where currencyname=N'" + lstAllCategory.SelectedItem.Value.Replace("'", "''") + "')";
                    // 'str = str.Replace("$1", Update_cmb.SelectedValue.ToString())
                    var da = new OleDbDataAdapter(str, tmpcon);
                    var ds = new DataSet();
                    da.Fill(ds, "TmpIndent");
                    int i;
                    int icount;
                    icount = ds.Tables[0].Rows.Count;
                    if (icount > 0)
                    {
                        txtcurrencyname.Value = ds.Tables[0].Rows[0][2].ToString();
                        txtshortname.Value = ds.Tables[0].Rows[0][1].ToString();
                        txtgocrate.Value = ds.Tables[0].Rows[0][3].ToString();
                        txtbankrate.Value = ds.Tables[0].Rows[0][4].ToString();
                        Txtdate.Text = String.Format(ds.Tables[0].Rows[0][5].ToString(), hrDate.Value);
                        txtcurrencycode.Value = ds.Tables[0].Rows[0][0].ToString();
                        Hidden1.Value = "0";
                        cmdsave.Text = Resources.ValidationResources.bUpdate.ToString();
                        Hidden1.Value = "top";
                        this.cmddelete.Visible = false;
                        if (LibObj.checkChildExistancewc("currency", "librarysetupinformation", "currency=N'" + ds.Tables[0].Rows[0][2] + "'", tmpcon) == true)
                        {
                            cmddelete.Visible = true;
                            cmdsave.Visible = true;
                            // cmdReturn.Disabled = False
                            txtcurrencyname.Disabled = true;
                            txtshortname.Disabled = true;
                            // LibObj.MsgBox1(Resources.ValidationResources.CurrenNoModify.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CurrenNoModify.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.CurrenNoModify.ToString(), this, dbUtilities.MsgLevel.Warning);
                            cmdsave.Text = Resources.ValidationResources.bUpdate.ToString();

                            return;
                        }
                    }
                    else
                    {
                        // Hidden1.Value = "8"
                        // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rNotFound.ToString(), this, dbUtilities.MsgLevel.Warning);
                    }
                }

                else
                {
                    // Hidden1.Value = "11"
                    // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rNotFound.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                if (cmdsave.Text == Resources.ValidationResources.bUpdate.ToString())
                {
                    txtcurrencyname.Disabled = true;
                    cmdsave.Visible = false;
                    txtshortname.Disabled = true;
                    // cmdadd.Disabled = False
                }
                Hidden1.Value = "top";
                this.cmddelete.Visible = false;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // hdUnableMsg.Value = "d"
                // LibObj.MsgBox1(Resources.ValidationResources.UnRetriveExcInfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnRetriveExcInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnRetriveExcInfo.ToString(), this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "simpleSearchVis('chksearch','Label8');", true);
            }
        }


        protected void chkSearch_CheckedChanged(object sender, System.EventArgs e)
        {
            OleDbConnection Conn = null;
            try
            {
                Conn = new OleDbConnection(retConstr(""));
                if (this.chkSearch.Checked == true)
                {
                    this.chkSearch.Checked = true;
                    this.SetFocus(chkSearch);
                    this.lstAllCategory.Visible = true;
                    this.txtCategory.Visible = true;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter.Visible = true;
                    this.lstAllCategory.Visible = true;
                    this.lstAllCategory.Items.Clear();
                    Conn.Open();
                    GetData2(Conn);
                    Conn.Close();
                    Label8.Visible = true;
                }
                else
                {
                    this.chkSearch.Checked = false;
                    this.SetFocus(chkSearch);
                    this.lstAllCategory.Visible = false;
                    this.txtCategory.Visible = false;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter.Visible = false;
                    this.lstAllCategory.Visible = false;
                    Label8.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }

        }

        protected void cmddelete_Click(object sender, EventArgs e)
        {
            try
            {
                var delcon = new OleDbConnection(); // (retConStr(Session("LibWiseDBConn")))
                delcon.ConnectionString = retConstr("");
                delcon.Open();
                if ((txtcurrencyname.Value).Trim() == string.Empty)
                {
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (LibObj.checkChildExistancewc("CurrencyCode", "exchangemaster", "CurrencyCode='" + (txtcurrencycode.Value).Trim().Replace("'", "''") + "'", delcon) == false)
                {
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (LibObj.checkChildExistancewc("currencycode", "indentmaster", "CurrencyCode='" + (txtcurrencycode.Value).Trim().Replace("'", "''") + "'", delcon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);

                }
                else if (LibObj.checkChildExistancewc("exchangerate", "giftindentmaster", "exchangerate='" + (txtcurrencycode.Value).Trim().Replace("'", "''") + "'", delcon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else if (LibObj.checkChildExistancewc("exchangerate", "indentmaster", "exchangerate='" + (txtcurrencycode.Value).Trim().Replace("'", "''") + "'", delcon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    var delcom = new OleDbCommand("delete from exchangemaster where CurrencyName=N'" + txtcurrencyname.Value.Replace("'", "''") + "'and EffectiveFrom='" + (Txtdate.Text).Trim().Replace("'", "''") + "'", delcon);
                    delcom.CommandType = CommandType.Text;
                    delcom.ExecuteNonQuery();
                    message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);

                    // Dim temp As String = String.Empty
                    delcom.Parameters.Clear();
                    if (LoggedUser.Logged().IsAudit == "Y")
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, (this.txtcurrencyname.Value).Trim(), Resources.ValidationResources.bDelete.ToString(), retConstr(""));
                    }

                    delcom.Dispose();

                }

                delcon.Dispose();
                delcon.Close();
                cmdreset_Click(sender, e);
                this.SetFocus(txtcurrencyname);
                hdTop.Value = "top";
                this.chkSearch.Checked = false;
                this.lstAllCategory.Visible = false;
                this.txtCategory.Visible = false;
                this.txtCategory.Value = string.Empty;

                this.btnCategoryFilter.Visible = false;
                // Me.ddl1.Visible = False
                this.lstAllCategory.Visible = false;
                Label8.Visible = false;
            }

            // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "simpleSearchVis('chksearch','Label8');", True)
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void btnCategoryFilter_Click(object sender, EventArgs e)
        {
            OleDbConnection Conn = null;
            try
            {
                Conn = new OleDbConnection(retConstr(""));
                if ((txtCategory.Value).Trim() == string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.SearchCriteria.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SearchCriteria.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SearchCriteria.ToString(), this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtCategory);
                }
                else
                {
                    this.lstAllCategory.Items.Clear();
                    Conn.Open();
                    GetData2(Conn);
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        protected void lstAllCategory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            var gClas = new GlobClassTr();
            string lsitm = lstAllCategory.SelectedItem.Value;
            string sQer = "select CurrencyCode,CurrencyName,ShortName,GocRate,BankRate,REPLACE( CONVERT(varchar, EffectiveFrom,106),' ','-') effdt from exchangemaster where currencycode= " + lsitm;
            gClas.TrOpen();
            DataTable dt = gClas.DataT(sQer);
            gClas.TrClose();
            txtcurrencycode.Value = lsitm;
            txtcurrencyname.Value = dt.Rows[0][1].ToString();
            txtshortname.Value = dt.Rows[0][2].ToString();
            txtgocrate.Value = dt.Rows[0][3].ToString();
            txtbankrate.Value = dt.Rows[0][4].ToString();
            Txtdate.Text = dt.Rows[0][5].ToString();
            cmdsave.Text = Resources.ValidationResources.bUpdate;
            cmddelete.Visible = true;
            this.SetFocus(txtgocrate);
        }
    }
}