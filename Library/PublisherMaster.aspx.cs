using Library.App_Code.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Web.Services;
using Library.App_Code.MultipleFramworks;
using static System.Net.WebRequestMethods;
using System.IO;
using static Library.App_Code.CSharp.dbUtilities;
using System.Net.NetworkInformation;
using System.Web.Services.Description;

namespace Library
{
    public partial class PublisherMaster : BaseClass
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
                hdnGrdId.Value = grdsearch.ClientID;
                //                Sconstr = DBI.GetConnectionString(Conversions.ToString(Session["LibWiseDBConn"]));
                //              Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!Page.IsPostBack)
                {

                    Session["NFormDW"] = null;
                    //     hdTop.Value = "top";
                    chkvendor.Visible = true;
                    //   lblTitle.Text = Request.QueryString["title"];
                    //                ViewState["openCond"] = Request.QueryString["title"];
                    tmpcondition = Request.QueryString["condition"];
                    var mytab = new DataTable();
                    //   cmdreset.CausesValidation = false;
                    //
                    //cmdsave.Attributes.Add("ServerClick", "return cmdsave_ServerClick();");
                    //cmdreset.Attributes.Add("click", "return cmdreset_ServerClick();");
                    grdsearch.DataSource = null;
                    grdsearch.DataBind();
                    hdnGrdId.Value = grdsearch.ClientID;

                    if (tmpcondition == "N")
                    {
                        this.cmdsave.Disabled = true;
                        this.cmddelete.Disabled = true;
                    }
                    else if (tmpcondition == "Y")
                    {
                        this.cmdsave.Disabled = false;
                        this.cmddelete.Disabled = false;
                    }
                    else
                    {
                        // this.txtfirstname.Value = Request.QueryString["txt"];
                        //lblTitle.Text = Resources.ValidationResources.rptpublisher.ToString;
                        this.cmdsave.Disabled = false;
                        this.cmddelete.Disabled = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";

                    }
                    // txtpublisherid.Visible = False
                    this.cmddelete.Disabled = true;
                    default_setting();
                }
                hdnGrdId.Value = grdsearch.ClientID;
            }

            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public void default_setting()
        {
            // '''''''''''shweta
            var admincon = new OleDbConnection(retConstr(""));
            admincon.Open();
            var adminad = new OleDbDataAdapter("select def_country from librarysetupinformation", admincon);
            var adminds = new DataSet();
            adminad.Fill(adminds);
            if (adminds.Tables[0].Rows.Count > 0)
            {
                txtpcountry.Value = adminds.Tables[0].Rows[0]["def_country"].ToString();
            }
            admincon.Close();
        }
        private void clearfields()
        {
            this.ddlPublisherType.SelectedIndex = 0;
            chkSame.Checked = false;
            txtfirstname.Value = string.Empty;
            txtphone1.Value = string.Empty;
            txtphone2.Value = string.Empty;
            txtemail.Value = string.Empty;
            txtwebadd.Value = string.Empty;
            txtlstate.Value = string.Empty;
            txtladd.Value = string.Empty;
            txtlcity.Text = string.Empty;
            txtlpincode.Value = string.Empty;
            txtlcountry.Value = string.Empty;
            txtpaddress.Value = string.Empty;
            txtpcity.Value = string.Empty;
            txtpstate.Value = string.Empty;
            txtpcountry.Value = string.Empty;
            txtppincode.Value = string.Empty;
            txtpubcode.Value = string.Empty;
            txtpubcode1.Value = string.Empty;
            txtpubname.Value = string.Empty;
            txtvendor_C.Value = "";
            //            txtvendorcode.Value = "";
            chkvendor.Checked = false;
            chkvendor.Visible = true;
            default_setting();
        }
        private void cleargrid()
        {
            var mytab = new DataTable();
            //            grdsearch.CurrentPageIndex = 0;
            grdsearch.DataSource = null;
            grdsearch.DataBind();
            hdnGrdId.Value = grdsearch.ClientID;

        }

        private void cmdreset_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                clearfields();
                cleargrid();
                // default_setting()
                if (tmpcondition == "Y")
                {
                    this.cmdsave.Disabled = false;
                    this.cmddelete.Disabled = false;
                }
                else if (tmpcondition == "N")
                {
                    this.cmdsave.Disabled = true;
                    this.cmddelete.Disabled = true;
                }
                else
                {
                    this.cmdsave.Disabled = false;
                    this.cmddelete.Disabled = false;
                    // Me.cmdReturn.Disabled = True
                }
                cmdsave.Value = Resources.ValidationResources.bSave;
                this.cmddelete.Disabled = true;


                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        private void cmdsearch_ServerClick(object sender, System.EventArgs e)
        {
            var publishermastercon = new OleDbConnection(retConstr(""));
            publishermastercon.Open();
            var publishermasterds = new DataSet();
            try
            {
                var mytab = new DataTable();
                grdsearch.CurrentPageIndex = 0;
                grdsearch.DataSource = null;
                grdsearch.DataBind();
                hdnGrdId.Value = grdsearch.ClientID;

                if (txtpubcode1.Value == string.Empty && txtpubname.Value == string.Empty)
                {
                    // Hidden1.Value = "90"
                    // LibObj.MsgBox1(Resources.ValidationResources.SpRecSearch.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpRecSearch.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SpRecSearch, this, dbUtilities.MsgLevel.Warning);

                    // txtpubcode1.Focus()
                    this.SetFocus(txtpubcode1);
                    return;
                }
                if (txtpubcode1.Value.Trim() != string.Empty && txtpubname.Value.Trim() == string.Empty)
                {
                    publishermasterds = LibObj.PopulateDataset("SELECT DISTINCT publishermaster.PublisherId, publishermaster.PublisherCode, publishermaster.firstname, publishermaster.PublisherPhone1,publishermaster.PublisherPhone2, publishermaster.EmailID, publishermaster.webaddress, AddressTable.localaddress,AddressTable.localcity, AddressTable.localstate, AddressTable.localpincode, AddressTable.localcountry,AddressTable.peraddress, AddressTable.percity, AddressTable.perstate, AddressTable.percountry, AddressTable.perpincode,publishermaster.publishertype FROM AddressTable INNER JOIN publishermaster ON AddressTable.addid = publishermaster.PublisherId WHERE (AddressTable.addrelation = N'publisher') AND (publishermaster.PublisherCode = N'" + txtpubcode1.Value.Trim().Replace("'", "''") + "')", "publisherMaster", publishermastercon);
                    if (publishermasterds.Tables["publisherMaster"].Rows.Count > 0)
                    {
                        txtpublisherid.Value = publishermasterds.Tables[0].Rows[0][0].ToString();
                        txtpubcode.Value = publishermasterds.Tables[0].Rows[0][1].ToString();
                        txtfirstname.Value = publishermasterds.Tables[0].Rows[0][2].ToString();
                        txtphone1.Value = publishermasterds.Tables[0].Rows[0][3].ToString();
                        txtphone2.Value = publishermasterds.Tables[0].Rows[0][4].ToString();
                        txtemail.Value = publishermasterds.Tables[0].Rows[0][5].ToString();
                        txtwebadd.Value = publishermasterds.Tables[0].Rows[0][6].ToString();
                        txtladd.Value = publishermasterds.Tables[0].Rows[0][7].ToString();
                        txtlcity.Text = publishermasterds.Tables[0].Rows[0][8].ToString();
                        txtlstate.Value = publishermasterds.Tables[0].Rows[0][9].ToString();
                        txtlpincode.Value = publishermasterds.Tables[0].Rows[0][10].ToString();
                        txtlcountry.Value = publishermasterds.Tables[0].Rows[0][11].ToString();
                        txtpaddress.Value = publishermasterds.Tables[0].Rows[0][12].ToString();
                        txtpcity.Value = publishermasterds.Tables[0].Rows[0][13].ToString();
                        txtpstate.Value = publishermasterds.Tables[0].Rows[0][14].ToString();
                        txtpcountry.Value = publishermasterds.Tables[0].Rows[0][15].ToString();
                        txtppincode.Value = publishermasterds.Tables[0].Rows[0][16].ToString();
                        ddlPublisherType.SelectedIndex = ddlPublisherType.Items.IndexOf(ddlPublisherType.Items.FindByText(publishermasterds.Tables[0].Rows[0][17].ToString()));
                        string str_V = string.Empty;
                        str_V = "select Vendorcode from vendorMaster,AddressTable where VendorName=N'" + this.txtfirstname.Value.Replace("'", "''") + "' and percity=N'" + txtpcity.Value.Replace("'", "''") + "' and vendorMaster.Vendorcode=AddressTable.Addid";
                        var Da_v = new OleDbDataAdapter(str_V, publishermastercon);
                        var Ds_v = new DataSet();
                        //  Da_v.Fill(Ds_v);
                        cmdsave2.Enabled = true;
                        cmddelete.Disabled = false;
                        if (Ds_v.Tables[0].Rows.Count == 0)
                        {
                            this.chkvendor.Visible = true;
                        }
                        else
                        {
                            chkvendor.Visible = false;
                        }
                        cmdsave.Value = Resources.ValidationResources.bUpdate;
                        grdsearch.DataSource = null;
                        grdsearch.DataBind();
                        hdnGrdId.Value = grdsearch.ClientID;

                        this.cmddelete.Disabled = false;
                        // txtpubcode.Focus()
                        this.SetFocus(txtpubcode);
                    }
                    else
                    {
                        // Hidden3.Value = "11"  'message record not found
                        // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rNotFound, this, dbUtilities.MsgLevel.Warning);

                        // txtpubcode1.Focus()
                        this.SetFocus(txtpubcode1);
                        txtpubcode1.Value = string.Empty;
                        return;
                    }
                }
                else
                {
                    string searchqry;
                    if (txtpubcode1.Value == string.Empty)
                    {
                        if (txtpubname.Value == string.Empty)
                        {
                            // Hidden3.Value = "11"  'message record not found
                            // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.rNotFound, this, dbUtilities.MsgLevel.Warning);

                            // txtpubcode1.Focus()
                            this.SetFocus(txtpubcode1);
                            txtpubcode1.Value = string.Empty;
                            return;
                        }
                        else
                        {
                            searchqry = "SELECT PublisherCode, firstname AS publishername FROM publishermaster WHERE (firstname LIKE N'%" + txtpubname.Value.Trim().Replace("'", "''") + "%')";
                            //hd_searchQuery.Value = searchqry;
                            // grdsearch.Focus()
                            this.SetFocus(grdsearch);
                        }
                    }
                    else if (txtpubname.Value == string.Empty)
                    {
                        searchqry = "SELECT PublisherCode, firstname AS publishername FROM publishermaster WHERE (PublisherCode = N'" + txtpubcode1.Value.Trim().Replace("'", "''") + "')";
                        //                        hd_searchQuery.Value = searchqry;
                    }
                    else
                    {
                        searchqry = "SELECT PublisherCode, firstname AS publishername FROM publishermaster WHERE (PublisherCode = N'" + txtpubcode1.Value.Trim().Replace("'", "''") + "') and (firstname LIKE N'%" + txtpubname.Value.Trim().Replace("'", "''") + "%') ";
                        //                        hd_searchQuery.Value = searchqry;
                    }
                    publishermasterds = LibObj.PopulateDataset(searchqry, "publisherMaster", publishermastercon);
                    if (publishermasterds.Tables["publisherMaster"].Rows.Count <= 0)
                    {
                        // Hidden3.Value = "11" 'message record not found
                        // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rNotFound, this, dbUtilities.MsgLevel.Warning);

                        // txtpubname.Focus()
                        this.SetFocus(txtpubname);
                        grdsearch.DataSource = publishermasterds.Tables[0].DefaultView;
                        grdsearch.DataBind();
                        hdnGrdId.Value = grdsearch.ClientID;

                        txtpubname.Value = string.Empty;
                    }
                    else
                    {
                        grdsearch.DataSource = publishermasterds.Tables[0].DefaultView;
                        grdsearch.DataBind();
                        hdnGrdId.Value = grdsearch.ClientID;

                        txtpubname.Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (publishermastercon.State == ConnectionState.Open)
                {
                    publishermastercon.Close();
                }
                publishermasterds.Dispose();
                publishermastercon.Dispose();
            }
        }
        public void GeberatePublisherCode()
        {
            try
            {
                var publishermastercon = new OleDbConnection(retConstr(""));
                var publishermastercom = new OleDbCommand();
                publishermastercon.Open();
                publishermastercom.Connection = publishermastercon;
                publishermastercom.CommandType = CommandType.Text;
                publishermastercom.CommandText = "select prefix,currentposition,suffix from IdTable where objectName=N'" + "Publisher" + "'";
                var da1 = new OleDbDataAdapter();
                var ds1 = new DataSet();
                da1.SelectCommand = publishermastercom;
                da1.Fill(ds1);
                txtpubcode.Value = ds1.Tables[0].Rows[0][0].ToString() + (Convert.ToInt32(ds1.Tables[0].Rows[0][1]) + 1).ToString() + ds1.Tables[0].Rows[0][2].ToString();
                int cur_pos = Convert.ToInt32(ds1.Tables[0].Rows[0][1]) + 1;
                publishermastercom.CommandType = CommandType.Text;
                publishermastercom.CommandText = "select publishercode from publishermaster"; // where vendorcode='" & vendcode.Value & "'"
                var da = new OleDbDataAdapter();
                var ds = new DataSet();
                da.SelectCommand = publishermastercom;
                //                da.Fill(ds);
                int i = 0;
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    var loopTo = ds.Tables[0].Rows.Count - 1;
                //    for (i = 0; i <= loopTo; i++)
                //    {
                //        publishermastercom.CommandType = CommandType.Text;
                //        publishermastercom.CommandText = "select publishercode from publishermaster  where publishercode=N'" + txtpubcode.Value + "'";
                //        string str = null;
                //        str = publishermastercom.ExecuteScalar().ToString();
                //        if (!string.IsNullOrEmpty(str))
                //        {
                //            cur_pos = cur_pos + 1;
                //            txtpubcode.Value = ds1.Tables[0].Rows[0][0], cur_pos), ds1.Tables[0].Rows[0][2]);
                //        }
                //        else
                //        {
                //            break;
                //        }
                //    }
                //}
                ds.Dispose();
                ds1.Dispose();
                publishermastercom.Dispose();
                publishermastercon.Close();
                publishermastercon.Dispose();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        protected void cmdsave2_Click(object sender, EventArgs e)
        {
            try
            {
                string tcase;
                //   tcase = AppSettings.Get("tcase");
                var publishermastercon = new OleDbConnection(retConstr(""));
                publishermastercon.Open();
                var publishermastercom = new OleDbCommand();
                publishermastercom.Connection = publishermastercon;
                // LibObj.SetFocus("txtpubcode", Me)
                this.SetFocus(txtpubcode);
                string vendor;
                vendor = string.Empty;
                if (cmdsave2.Text != Resources.ValidationResources.bUpdate)
                {
                    var pubds = new DataSet();
                    pubds = LibObj.PopulateDataset("select AddressTable.peraddress,Addresstable.percity,publishermaster.firstname from AddressTable,publishermaster where publishermaster.publisherid=AddressTable.addid  and percity=N'" + txtpcity.Value.Trim() + "' and firstname=N'" + txtfirstname.Value.Trim() + "' and addrelation=N'Publisher'", "publisher", publishermastercon); // and AddressTable.peraddress='" & Trim(txtpaddress.Value.Replace(Chr(Asc(13)), "")) & "'
                    if (pubds.Tables[0].Rows.Count > 0)
                    {
                        // Me.Hidden4.Value = "Exist"
                        // LibObj.MsgBox1(Resources.ValidationResources.SpPubExist.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpPubExist.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SpPubExist, this, dbUtilities.MsgLevel.Warning);
                        this.SetFocus(this.txtpcity);

                        return;
                    }
                }
                /*
                if (chkvendor.Checked == true)
                {
                    var vends = new DataSet();
                    vends = LibObj.PopulateDataset("select AddressTable.peraddress,Addresstable.percity,vendormaster.vendorname from AddressTable,vendormaster where vendormaster.vendorcode=AddressTable.addid  and percity=N'" + Trim(txtpcity.Value).Replace("'", "''") + "' and vendorname=N'" + Trim(txtfirstname.Value).Replace("'", "''") + "' and addrelation=N'vendor'", "publisher", publishermastercon);
                    if (vends.Tables[0].Rows.Count > 0)
                    {
                        // Me.Hidden4.Value = "V_Exist"
                        // LibObj.MsgBox1(Resources.ValidationResources.SameVenExist.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SameVenExist.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.SameVenExist.ToString, this, DBUTIL.dbUtilities.MsgLevel.Warning);

                        vendor = "n";
                    }
                    else
                    {
                        if (Trim(txtvendor_C.Value) == string.Empty)
                        {
                            GenearteVendorID();
                            publishermastercom.CommandType = CommandType.Text;
                            publishermastercom.CommandText = "select coalesce(max(cast(vendorid as int)),0,max(cast(vendorid as int))) from vendorMaster";
                            string tmpstr;
                            tmpstr = Conversions.ToString(publishermastercom.ExecuteScalar());
                            txtvendorcode.Value = IIf(Val(tmpstr) == 0, 1, Val(tmpstr) + 1);
                            publishermastercom.Parameters.Clear();
                        }
                        vendor = "Y";
                    }
                }
                */
                // End If
                // --------*************
                bool flag = false;
                if (cmdsave2.Text == Resources.ValidationResources.bSave)
                {
                    if (txtpubcode.Value.Trim() == string.Empty)
                    {
                        flag = true;
                        GeberatePublisherCode();
                        publishermastercom.CommandType = CommandType.Text;
                        publishermastercom.CommandText = "select coalesce(max(cast(Publisherid as int)),0,max(cast(Publisherid as int))) from PublisherMaster";
                        string tmpstr;
                        tmpstr = publishermastercom.ExecuteScalar().ToString();
                        txtpublisherid.Value = tmpstr == "0" ? "1" : (Convert.ToInt32(tmpstr) + 1).ToString();
                        publishermastercom.Parameters.Clear();
                    }
                    else
                    {
                        string tmpCode;
                        // flag = True
                        tmpCode = LibObj.populateCommandText("select Publisherid from Publishermaster where Publishercode=N'" + txtpubcode.Value.Trim() + "'", publishermastercon);
                        if (!string.IsNullOrEmpty(tmpCode))
                        {
                            // Me.Hidden4.Value = "7"
                            // LibObj.MsgBox1(Resources.ValidationResources.SpPubCodeExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpPubCodeExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SpPubCodeExist, this, dbUtilities.MsgLevel.Warning);

                            this.SetFocus(txtpubcode);

                            return;
                        }
                        //                        this.pubcode.Value = this.txtpubcode.Value;

                        publishermastercom.CommandType = CommandType.Text;
                        publishermastercom.CommandText = "select coalesce(max(cast(Publisherid as int)),0,max(cast(Publisherid as int))) from PublisherMaster";
                        string tmpstr;
                        tmpstr = publishermastercom.ExecuteScalar().ToString();
                        txtpublisherid.Value = tmpstr == "0" ? "1" : (Convert.ToInt32(tmpstr) + 1).ToString();
                        publishermastercom.Parameters.Clear();
                    }
                }
                else if (txtpubcode.Value.Trim() == string.Empty)
                {
                    GeberatePublisherCode();
                }
                else
                {
                    string tmpCode;
                    tmpCode = LibObj.populateCommandText("select Publisherid from Publishermaster where Publishercode=N'" + txtpubcode.Value.Trim() + "'", publishermastercon);
                    if (!string.IsNullOrEmpty(tmpCode))
                    {
                        if (tmpCode != txtpublisherid.Value.Trim())
                        {
                            // Me.Hidden4.Value = "7"
                            // LibObj.MsgBox1(Resources.ValidationResources.SpPubCodeExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpPubCodeExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SpPubCodeExist, this, dbUtilities.MsgLevel.Warning);

                            return;
                        }
                    }

                    //                    this.pubcode.Value = this.txtpubcode.Value;
                }
                // ------------***********
                string tmpr1;
                tmpr1 = LibObj.populateCommandText("select Count(*) pcount from publishermaster where PublisherCode=N'" + txtpubcode.Value.Trim() + "'", publishermastercon);
                if (tmpr1.Trim() != "0")
                {
                    if (cmdsave2.Text == "Submit")
                    {
                        // Hidden1.Value = "1"
                        // LibObj.MsgBox1(Resources.ValidationResources.PubCodeExist.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.PubCodeExist.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.PubCodeExist, this, dbUtilities.MsgLevel.Warning);

                        return;
                    }
                }
                OleDbTransaction tran;
                tran = publishermastercon.BeginTransaction();
                publishermastercom.Connection = publishermastercon;
                publishermastercom.Transaction = tran;
                var logged = LoggedUser.Logged();
                try
                {
                    publishermastercom.CommandType = CommandType.StoredProcedure;
                    publishermastercom.CommandText = "insert_publishermaster_1";
                    publishermastercom.Parameters.Add(new OleDbParameter("@PublisherId_1", OleDbType.VarWChar));
                    publishermastercom.Parameters["@PublisherId_1"].Value = txtpublisherid.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@PublisherCode_2", OleDbType.VarWChar));

                    publishermastercom.Parameters["@PublisherCode_2"].Value = txtpubcode.Value.Trim();
                    //                    hdSaveMsg.Value = pubcode.Value;

                    //                  txtpubcode.Value = hdSaveMsg.Value;
                    publishermastercom.Parameters.Add(new OleDbParameter("@firstname_3", OleDbType.VarWChar));
                    publishermastercom.Parameters["@firstname_3"].Value = txtfirstname.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@PublisherPhone1_4", OleDbType.VarWChar));
                    publishermastercom.Parameters["@PublisherPhone1_4"].Value = txtphone1.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@PublisherPhone2_5", OleDbType.VarWChar));
                    publishermastercom.Parameters["@PublisherPhone2_5"].Value = txtphone2.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@EmailID_6", OleDbType.VarWChar));
                    publishermastercom.Parameters["@EmailID_6"].Value = txtemail.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@webaddress_7", OleDbType.VarWChar));
                    publishermastercom.Parameters["@webaddress_7"].Value = txtwebadd.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@PublisherType_8", OleDbType.VarWChar));
                    publishermastercom.Parameters["@PublisherType_8"].Value = ddlPublisherType.SelectedItem.Value;


                    publishermastercom.Parameters.Add(new OleDbParameter("@userid_9", OleDbType.VarWChar));
                    publishermastercom.Parameters["@userid_9"].Value = logged.UserId;





                    // If Not Trim(tmpr1) = String.Empty Then
                    // If cmdsave.Value() = "Submit" Then
                    // 'Hidden1.Value = "1"
                    // LibObj.MsgBox1(Resources.ValidationResources.PubCodeExist.ToString, Me)
                    // End If
                    // End If
                    publishermastercom.ExecuteNonQuery();
                    // Hidden2.Value = "1"
                    // LibObj.MsgBox1(Resources.ValidationResources.RecSaveWithPubCode.ToString & " " & hdSaveMsg.Value, Me)

                    publishermastercom.Parameters.Clear();
                    publishermastercom.CommandText = "insert_AddressTable_1";
                    publishermastercom.Parameters.Add(new OleDbParameter("@addid_1", OleDbType.VarWChar));
                    publishermastercom.Parameters["@addid_1"].Value = txtpublisherid.Value;
                    publishermastercom.Parameters.Add(new OleDbParameter("@localaddress_2", OleDbType.VarWChar));
                    publishermastercom.Parameters["@localaddress_2"].Value = txtladd.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@localcity_3", OleDbType.VarWChar));
                    publishermastercom.Parameters["@localcity_3"].Value = txtlcity.Text.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@localstate_4 ", OleDbType.VarWChar));
                    publishermastercom.Parameters["@localstate_4 "].Value = txtlstate.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@localpincode_5", OleDbType.VarWChar));
                    publishermastercom.Parameters["@localpincode_5"].Value = txtlpincode.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@localcountry_6", OleDbType.VarWChar));
                    publishermastercom.Parameters["@localcountry_6"].Value = txtlcountry.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@peraddress_7", OleDbType.VarWChar));
                    // publishermastercom.Parameters("@peraddress_7").Value = Trim(txtpaddress.Value.Replace(Chr(Asc(13)), ""))
                    publishermastercom.Parameters["@peraddress_7"].Value = txtpaddress.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@percity_8", OleDbType.VarWChar));
                    publishermastercom.Parameters["@percity_8"].Value = txtpcity.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@perstate_9", OleDbType.VarWChar));
                    publishermastercom.Parameters["@perstate_9"].Value = txtpstate.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@percountry_10", OleDbType.VarWChar));
                    publishermastercom.Parameters["@percountry_10"].Value = txtpcountry.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@perpincode_11", OleDbType.VarWChar));
                    publishermastercom.Parameters["@perpincode_11"].Value = txtppincode.Value.Trim();
                    publishermastercom.Parameters.Add(new OleDbParameter("@addrelation_12", OleDbType.VarWChar));
                    publishermastercom.Parameters["@addrelation_12"].Value = "publisher";

                    publishermastercom.ExecuteNonQuery();


                    // Dim temp As String = String.Empty
                    publishermastercom.Parameters.Clear();
                    if (logged.IsAudit == "Y")
                    {
                        if (cmdsave2.Text == Resources.ValidationResources.bSave)
                        {
                            LibObj1.insertLoginFunc(logged.UserName, "Publisher", logged.Session, txtpubcode.Value, Resources.ValidationResources.Insert, retConstr(""));
                        }
                        else
                        {
                            LibObj1.insertLoginFunc(logged.UserName, "Publisher", logged.Session, txtpubcode.Value, Resources.ValidationResources.bUpdate, retConstr(""));
                        }
                    }



                    // If cmdsave.Value() = "Submit" Then
                    // LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtpubcode.Value).Replace("'", "''"), "Insert", retConStr(Session("LibWiseDBConn")))
                    // Else
                    // LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtpubcode.Value).Replace("'", "''"), "Update", retConStr(Session("LibWiseDBConn")))
                    // End If
                    if (flag == true)
                    {
                        int i;
                        publishermastercom.CommandType = CommandType.Text;
                        publishermastercom.CommandText = "select currentposition from idtable where objectName=N'" + "Publisher" + "'";
                        i = Convert.ToInt32(publishermastercom.ExecuteScalar());
                        publishermastercom.Parameters.Clear();
                        publishermastercom.CommandType = CommandType.Text;
                        publishermastercom.CommandText = "update idtable set currentposition=" + (i + 1) + " where objectname=N'" + "Publisher" + "'";
                        publishermastercom.ExecuteNonQuery();
                    }
                    publishermastercom.Parameters.Clear();             // '''''''Publisher Entry Save as Vendor
                    /*
                        if (vendor == "Y")
                        {


                            publishermastercom.CommandType = CommandType.StoredProcedure;
                            publishermastercom.CommandText = "insert_vendormaster_1";
                            publishermastercom.Parameters.Add(new OleDbParameter("@vendorid_1", OleDbType.VarWChar));
                            publishermastercom.Parameters["@vendorid_1"].Value = Trim(txtvendorcode.Value);
                            publishermastercom.Parameters.Add(new OleDbParameter("@vendorcode_2", OleDbType.VarWChar));
                            // ---------------***************
                            // If flag = True Then
                            publishermastercom.Parameters["@vendorcode_2"].Value = Trim(txtvendor_C.Value);
                            hdSaveMsg.Value = pubcode.Value;

                            txtpubcode.Value = hdSaveMsg.Value;
                            publishermastercom.Parameters.Add(new OleDbParameter("@vendorname_3", OleDbType.VarWChar));
                            publishermastercom.Parameters["@vendorname_3"].Value = Trim(txtfirstname.Value);
                            publishermastercom.Parameters.Add(new OleDbParameter("@vendorwebaddress_4", OleDbType.VarWChar));
                            publishermastercom.Parameters["@vendorwebaddress_4"].Value = Trim(txtwebadd.Value);
                            publishermastercom.Parameters.Add(new OleDbParameter("@phone1_5", OleDbType.VarWChar));
                            publishermastercom.Parameters["@phone1_5"].Value = Trim(txtphone1.Value);
                            publishermastercom.Parameters.Add(new OleDbParameter("@phone2_6", OleDbType.VarWChar));
                            publishermastercom.Parameters["@phone2_6"].Value = Trim(txtphone2.Value);
                            publishermastercom.Parameters.Add(new OleDbParameter("@emailID_7", OleDbType.VarWChar));
                            publishermastercom.Parameters["@emailID_7"].Value = Trim(txtemail.Value);
                            publishermastercom.Parameters.Add(new OleDbParameter("@userid_8", OleDbType.VarWChar));
                            publishermastercom.Parameters["@userid_8"].Value = Session["user_id"];

                            publishermastercom.ExecuteNonQuery();
                            publishermastercom.Parameters.Clear();

                            publishermastercom.CommandType = CommandType.StoredProcedure;
                            publishermastercom.CommandText = "insert_AddressTable_1";
                            publishermastercom.Parameters.Add(new OleDbParameter("@addid_1", OleDbType.VarWChar));
                            publishermastercom.Parameters["@addid_1"].Value = txtvendor_C.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@localaddress_2", OleDbType.VarWChar));
                            publishermastercom.Parameters["@localaddress_2"].Value = txtladd.Value.Trim().Replace("'","''");
                            publishermastercom.Parameters.Add(new OleDbParameter("@localcity_3", OleDbType.VarWChar));
                            publishermastercom.Parameters["@localcity_3"].Value = txtlcity.Text.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@localstate_4 ", OleDbType.VarWChar));
                            publishermastercom.Parameters["@localstate_4 "].Value = txtlstate.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@localpincode_5", OleDbType.VarWChar));
                            publishermastercom.Parameters["@localpincode_5"].Value = txtlpincode.Value;
                            publishermastercom.Parameters.Add(new OleDbParameter("@localcountry_6", OleDbType.VarWChar));
                            publishermastercom.Parameters["@localcountry_6"].Value = txtlcountry.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@peraddress_7", OleDbType.VarWChar));
                            publishermastercom.Parameters["@peraddress_7"].Value = txtpaddress.Value.Trim().Replace("'", "''");
                            publishermastercom.Parameters.Add(new OleDbParameter("@percity_8", OleDbType.VarWChar));
                            publishermastercom.Parameters["@percity_8"].Value = txtpcity.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@perstate_9", OleDbType.VarWChar));
                            publishermastercom.Parameters["@perstate_9"].Value = txtpstate.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@percountry_10", OleDbType.VarWChar));
                            publishermastercom.Parameters["@percountry_10"].Value = txtpcountry.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@perpincode_11", OleDbType.VarWChar));
                            publishermastercom.Parameters["@perpincode_11"].Value = txtppincode.Value.Trim();
                            publishermastercom.Parameters.Add(new OleDbParameter("@addrelation_12", OleDbType.VarWChar));
                            publishermastercom.Parameters["@addrelation_12"].Value = "vendor";

                            publishermastercom.ExecuteNonQuery();
                            // publishermastercom.Parameters.Clear()
                            // publishermastercom.CommandType = CommandType.Text
                            // publishermastercom.CommandText = "update bookcatalog set firstname = '" & IIf(tcase = "Y", LibObj.TitleCase(Trim(txtfirstname.Value)), Trim(txtfirstname.Value)) & "',percity = '" & Trim(txtpcity.Value).Replace("'", "''") & "',perstate = '" & Trim(txtpstate.Value).Replace("'", "''") & "', percountry = '" & Trim(txtpcountry.Value).Replace("'", "''") & "' where publishercode='" & Trim(pubcode.Value).Replace("'", "''") & "'"
                            // publishermastercom.ExecuteNonQuery()

                            publishermastercom.Parameters.Clear();
                            publishermastercom.CommandType = CommandType.Text;
                            publishermastercom.CommandText = "update idtable set currentposition=currentposition + 1 where objectName=N'Vendor'";
                            publishermastercom.ExecuteNonQuery();

                        }
                        */
                    // BY Kaushal 23-Jan-2012
                    // --------------------------------
                    if (cmdsave2.Text == Resources.ValidationResources.bUpdate)
                    {

                        publishermastercom.Parameters.Clear();
                        publishermastercom.CommandType = CommandType.StoredProcedure;
                        publishermastercom.CommandText = "update_catalog";
                        publishermastercom.Parameters.Add(new OleDbParameter("@firstname", OleDbType.VarWChar));
                        publishermastercom.Parameters["@firstname"].Value = (txtfirstname.Value);
                        publishermastercom.Parameters.Add(new OleDbParameter("@percity", OleDbType.VarWChar));
                        publishermastercom.Parameters["@percity"].Value = (txtpcity.Value).Replace("'", "''");
                        publishermastercom.Parameters.Add(new OleDbParameter("@perstate", OleDbType.VarWChar));
                        publishermastercom.Parameters["@perstate"].Value = (txtpstate.Value).Replace("'", "''");
                        publishermastercom.Parameters.Add(new OleDbParameter("@percountry", OleDbType.VarWChar));
                        publishermastercom.Parameters["@percountry"].Value = (txtpcountry.Value).Replace("'", "''");
                        publishermastercom.Parameters.Add(new OleDbParameter("@peraddress", OleDbType.VarWChar));
                        publishermastercom.Parameters["@peraddress"].Value = txtpaddress.Value.Trim().Replace("'", "''");
                        publishermastercom.Parameters.Add(new OleDbParameter("@publishercode", OleDbType.VarWChar));
                        publishermastercom.Parameters["@publishercode"].Value = txtpublisherid.Value;


                        // ------------------------------------------------
                        // publishermastercom.CommandText = "update bookcatalog set firstname = N'" &  & "',percity = N'" & Trim(txtpcity.Value).Replace("'", "''") & "',perstate = N'" & Trim(txtpstate.Value).Replace("'", "''") & "', percountry = N'" & Trim(txtpcountry.Value).Replace("'", "''") & "',peraddress = N'" & Trim(txtpaddress.Value.Replace(Chr(Asc(13)), "")) & "'where publishercode=N'" & Val(Trim(txtpublisherid.Value).Replace("'", "''")) & "'"
                        publishermastercom.ExecuteNonQuery();
                    }


                    tran.Commit();
                    //                        hdTop.Value = "top";
                    cmdsave2.Text = Resources.ValidationResources.bSave;
                    this.cmddelete.Disabled = true;

                    // ****************
                    publishermastercom.Dispose();
                    // ********9Jan2007---Praveen
                    //                      if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ViewState["openCond"], null, false)))
                    //                    {
                    //                      string QS2 = Request.QueryString["fldName"];            // ViewState("fldName")
                    //                    string QS2V = Request.QueryString["fldId"];            // ViewState("fldName")
                    //                  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "SetCaller('" + QS2 + "','" + QS2V + "');", true);
                    //            }
                    //          else
                    //        {
                    clearfields();
                    //      }
                    // ************
                    // LibObj.MsgBox1("Operation compeleted !", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Operation compeleted !", Me)
                    message.PageMesg("Operation compeleted !", this, dbUtilities.MsgLevel.Success);
                    if (grdsearch.Items.Count > 0)
                    {
                        //                            FillAfterDelete(publishermastercon);
                    }
                    this.SetFocus(this.txtfirstname);
                    publishermastercon.Close();
                    publishermastercon.Dispose();

                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        // msglabel.Visible = True
                        // msglabel.Text = ex.Message
                        // hdUnableMsg.Value = "s"
                        // LibObj.MsgBox1(Resources.ValidationResources.UnsavePubInfo.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UnsavePubInfo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UnsavePubInfo, this, dbUtilities.MsgLevel.Warning);
                    }

                    catch (Exception ex1)
                    {
                        // msglabel.Visible = True
                        // msglabel.Text = ex1.Message
                        // hdUnableMsg.Value = "s"
                        // LibObj.MsgBox1(Resources.ValidationResources.UnsavePubInfo.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnsavePubInfo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UnsavePubInfo, this, dbUtilities.MsgLevel.Failure);

                    }

                }
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }

        protected void btnreset2_Click(object sender, EventArgs e)
        {
            cmdreset_ServerClick(sender, e);
        }

        protected void btnsearch2_Click(object sender, EventArgs e)
        {
            cmdsearch_ServerClick(sender, e);
        }
        private void grdsearch_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
            var publishermastercon = new OleDbConnection(retConstr(""));
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName ?? "")
                {
                    case "Select":
                        {
                            this.chkvendor.Checked = false;
                            txtpubname.Value = "";
                            publishermastercon.Open();
                            var publishermasterds = new DataSet();
                            publishermasterds = LibObj.PopulateDataset("SELECT DISTINCT publishermaster.PublisherId, publishermaster.PublisherCode, publishermaster.firstname, publishermaster.PublisherPhone1,publishermaster.PublisherPhone2, publishermaster.EmailID, publishermaster.webaddress, AddressTable.localaddress,AddressTable.localcity, AddressTable.localstate, AddressTable.localpincode, AddressTable.localcountry,AddressTable.peraddress, AddressTable.percity, AddressTable.perstate, AddressTable.percountry, AddressTable.perpincode,publishermaster.publishertype FROM AddressTable INNER JOIN publishermaster ON AddressTable.addid = publishermaster.PublisherId WHERE (AddressTable.addrelation = N'publisher') AND (publishermaster.PublisherCode = N'" + grdsearch.Items[e.Item.ItemIndex].Cells[1].Text + "')", "publisherMaster", publishermastercon);
                            txtpublisherid.Value = publishermasterds.Tables["publisherMaster"].Rows[0][0].ToString();
                            txtpubcode.Value = publishermasterds.Tables["publisherMaster"].Rows[0][1].ToString();
                            txtfirstname.Value = publishermasterds.Tables["publisherMaster"].Rows[0][2].ToString();
                            txtphone1.Value = publishermasterds.Tables["publisherMaster"].Rows[0][3].ToString();
                            txtphone2.Value = publishermasterds.Tables["publisherMaster"].Rows[0][4].ToString();
                            txtemail.Value = publishermasterds.Tables["publisherMaster"].Rows[0][5].ToString();
                            txtwebadd.Value = publishermasterds.Tables["publisherMaster"].Rows[0][6].ToString();
                            txtladd.Value = publishermasterds.Tables["publisherMaster"].Rows[0][7].ToString();
                            txtlcity.Text = publishermasterds.Tables["publisherMaster"].Rows[0][8].ToString();
                            txtlstate.Value = publishermasterds.Tables["publisherMaster"].Rows[0][9].ToString();
                            txtlpincode.Value = publishermasterds.Tables["publisherMaster"].Rows[0][10].ToString();
                            txtlcountry.Value = publishermasterds.Tables["publisherMaster"].Rows[0][11].ToString();
                            txtpaddress.Value = publishermasterds.Tables["publisherMaster"].Rows[0][12].ToString();
                            txtpcity.Value =            publishermasterds.Tables["publisherMaster"].Rows[0][13].ToString();
                            txtpstate.Value = publishermasterds.Tables["publisherMaster"].Rows[0][14].ToString();
                            txtpcountry.Value = publishermasterds.Tables["publisherMaster"].Rows[0][15].ToString();
                            txtppincode.Value =     publishermasterds.Tables["publisherMaster"].Rows[0][16].ToString();
                            ddlPublisherType.SelectedIndex = ddlPublisherType.Items.IndexOf(ddlPublisherType.Items.FindByText(publishermasterds.Tables["publisherMaster"].Rows[0][17].ToString()));
                            string str_V = string.Empty;
//                            str_V = "select Vendorcode from vendorMaster,AddressTable where VendorName=N'" + this.txtfirstname.Value.Replace("'", "''") + "' and percity=N'" + txtpcity.Value.Replace("'", "''") + "' and vendorMaster.Vendorcode=AddressTable.Addid";
  //                          var Da_v = new OleDbDataAdapter(str_V, publishermastercon);
    //                        var Ds_v = new DataSet();
      //                      Da_v.Fill(Ds_v);
        //                    if (Ds_v.Tables[0].Rows.Count == 0)
          //                  {
            //                    this.chkvendor.Visible = true;
              //              }
                //            else
                  //          {
                    //            chkvendor.Visible = false;
                      //      }
                            publishermasterds.Dispose();
                            publishermastercon.Close();
                            cmdsave2.Text = Resources.ValidationResources.bUpdate;
                            // txtpubcode.Focus()
                        //    this.SetFocus(txtpubcode);
                            break;
                        }
                }
                // LibObj.SetFocus("txtpubcode", Me)
                //            msglabel.Visible = false;
                this.cmddelete.Disabled = false;
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // hdUnableMsg.Value = "d"
                // LibObj.MsgBox1(Resources.ValidationResources.UnRetrivePubInfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnRetrivePubInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnRetrivePubInfo, this, dbUtilities.MsgLevel.Failure);
                // txtpubcode.Focus()
                //            this.SetFocus(txtpubcode);
            }
            finally
            {
                if (publishermastercon.State == ConnectionState.Open)
                {
                    publishermastercon.Close();
                }
                publishermastercon.Dispose();
            }
        }

        protected void grdsearch_ItemCommand(object source, DataGridCommandEventArgs e)
        {
             grdsearch_ItemCommand1(source, e);
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
          
        }
    }
 
}