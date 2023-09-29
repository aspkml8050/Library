using Library.App_Code.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class Item_type : BaseClass
    {
        insertLogin LibObj1=new insertLogin();
        private dbUtilities message = new dbUtilities();
        libGeneralFunctions LibObj = new libGeneralFunctions();
        DBIStructure DBI = new DBIStructure();

        protected void Page_Load(object sender, EventArgs e)
        {
            hdnGrdId.Value = grdCLStatus.ClientID;
            msglabel.Visible = false;
            var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
            CategoryLoadingStatusCon.Open();
            if (!IsPostBack)
            {
                // LibObj.save_itemtype(CategoryLoadingStatusCon, Session("user_id").ToString())
                hdTop.Value = Resources.ValidationResources.RBTop;
                lblTitle.Text = Request.QueryString["title"];
                this.Hidden5.Value = Request.QueryString["condition"];
                Session["NFormDW"] = null;
                if (Hidden5.Value == "Y")
                {
                    this.cmddelete2.Enabled=false;
                    this.cmdsave2.Enabled = true;
                }
                else if (Hidden5.Value == "N")
                {
                    this.cmddelete2.Enabled = false;
                    this.cmdsave2.Enabled = false;
                }
                else
                {
                    lblTitle.Text = Resources.ValidationResources.Litmcat;
                    this.cmdsave2.Enabled=true;
                    this.cmddelete2.Enabled = true;
                    // Me.cmdReturn.Disabled = True
                    Session["NFormDW"] = "dLogout";

                }
                this.cmddelete2.Enabled = false;
                hdTop.Value = Resources.ValidationResources.RBTop;

                var CategoryLoadingStatusda = new OleDbDataAdapter("select * from Item_Type  where id <> 0 order by Item_Type", CategoryLoadingStatusCon);
                var CategoryLoadingStatusds = new DataSet();
                CategoryLoadingStatusda.Fill(CategoryLoadingStatusds);
                if (CategoryLoadingStatusds.Tables[0].Rows.Count > 0)
                {
                    grdCLStatus.DataSource = CategoryLoadingStatusds.Tables[0].DefaultView;
                    grdCLStatus.DataBind();
                }
                else
                {

                    grdCLStatus.DataSource = null;
                    grdCLStatus.DataBind();
                }
                hdnGrdId.Value = grdCLStatus.ClientID;

                CategoryLoadingStatusds.Dispose();
                CategoryLoadingStatusda.Dispose();
                CategoryLoadingStatusCon.Close();
                CategoryLoadingStatusCon.Dispose();
            }

        }
        public void GenerateCLStatusID()
        {
            var CategoryLoadingStatusconn = new OleDbConnection(retConstr(""));
            CategoryLoadingStatusconn.Open();
            var CategoryLoadingStatuscom3 = new OleDbCommand();
            CategoryLoadingStatuscom3.Connection = CategoryLoadingStatusconn;
            CategoryLoadingStatuscom3.CommandType = CommandType.Text;
            CategoryLoadingStatuscom3.CommandText = "select coalesce(max(id),0,max(id)) from Item_Type";
            string tmpstr;
            tmpstr = CategoryLoadingStatuscom3.ExecuteScalar().ToString();
            txtCLStatusCode.Value = tmpstr == "0"? "1": (Convert.ToInt32(tmpstr)+1).ToString();
            hd_id.Value = txtCLStatusCode.Value;
            CategoryLoadingStatuscom3.Dispose();
            CategoryLoadingStatusconn.Close();
            CategoryLoadingStatusconn.Dispose();
        }
        public void clear_field()
        {
            txtCLStatus.Value = "";
            txtAbbreviation.Value = "";
            this.txtCLStatus.Disabled = false;
            image1.Src = "~//images/AddImage.jpg";
        }
        private void cmdsave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {

                if (cmdsave2.Text == Resources.ValidationResources.bSave)
                {
                    GenerateCLStatusID();
                }

                // himanshu 04 nov 2010
                // If cmdsave.Value.ToUpper.ToString = "UPDATE" Then
                // If checkbeforedelete(txtCLStatus.Value.ToString) > 0 Then
                // MsgBox("Value cannot be deleted. Other tables contain this value.", MsgBoxStyle.Critical, "Messege")
                // Exit Sub
                // End If
                // End If
                // *********Duplicate Check
                OleDbConnection CategoryLoadingStatusCon;
                CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                CategoryLoadingStatusCon.Open();
                var check_ds = new DataSet();
                if (cmdsave2.Text == Resources.ValidationResources.bUpdate)
                {
                    if (hd_name.Value.ToUpper() != txtCLStatus.Value.ToUpper())
                    {
                        check_ds = LibObj.PopulateDataset("select * from Item_Type where Item_Type=N'" + txtCLStatus.Value.ToLower() + "' ", "category", CategoryLoadingStatusCon);
                        cmddelete2.Enabled = false;
                        if (check_ds.Tables[0].Rows.Count > 0)
                        {
                            // Hidden2.Value = "1"
                            // LibObj.MsgBox1(Resources.ValidationResources.CatAlExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CatAlExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.CatAlExist, this, dbUtilities.MsgLevel.Warning);
                            // Me.txtCLStatus.Focus()
                            this.SetFocus(txtCLStatus);

                            cmddelete2.Enabled=false;
                            return;
                        }
                    }
                    else if (hd_short.Value.ToUpper() != txtAbbreviation.Value.ToUpper())
                    {
                        check_ds = LibObj.PopulateDataset("select * from Item_Type where Abbreviation=N'" + txtAbbreviation.Value.ToLower() + "' ", "category", CategoryLoadingStatusCon);
                        cmddelete2.Enabled = false;
                        if (check_ds.Tables[0].Rows.Count > 0)
                        {
                            // hdabbreviation.Value = "1"
                            // LibObj.MsgBox1(Resources.ValidationResources.AbbriAlExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.AbbriAlExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.AbbriAlExist, this, dbUtilities.MsgLevel.Warning);
                            this.SetFocus(txtAbbreviation);

                            cmddelete2.Enabled = false;
                            return;
                        }
                    }
                    // End If

                }
                OleDbTransaction tran;
                tran = CategoryLoadingStatusCon.BeginTransaction();
                var CategoryLoadingStatuscom = new OleDbCommand();
                CategoryLoadingStatuscom.Connection = CategoryLoadingStatusCon;
                CategoryLoadingStatuscom.Transaction = tran;
                try
                {
                    int imglen;
                    byte[] imgbin = new byte[] { 1, 1, 0, 0 };
                    if (this.cmdsave2.Text == Resources.ValidationResources.bSave)
                    {
                        if (hdMemImg.Value!="")
                        {
                            imgbin = Convert.FromBase64String(hdMemImg.Value);
                            // If aa1.Trim.Length > 0 And aa2.ContentLength > 0 Then
                            // Dim imgStream As Stream = aa2.InputStream()
                            // imglen = aa2.ContentLength
                            // Dim imgBinaryData(imglen) As Byte
                            // Dim n As Int32 = imgStream.Read(imgBinaryData, 0, imglen)
                            // imgbin = imgBinaryData
                            // End If
                        }
                    }
                    else if (hdMemImg.Value != "")
                    {
                        imgbin = Convert.FromBase64String(hdMemImg.Value);
                    }
                    // If aa1.Trim.Length > 0 And aa2.ContentLength > 0 Then
                    // Dim imgStream As Stream = aa2.InputStream()
                    // imglen = aa2.ContentLength
                    // Dim imgBinaryData(imglen) As Byte
                    // Dim n As Int32 = imgStream.Read(imgBinaryData, 0, imglen)
                    // imgbin = imgBinaryData
                    // End If
                    else
                    {
                        CategoryLoadingStatuscom.CommandType = CommandType.Text;
                        CategoryLoadingStatuscom.CommandText = "select Item_icon from Item_Type where Id='" + hd_id.Value + "'";
                        var dr = CategoryLoadingStatuscom.ExecuteReader();
                        dr.Read();
                        imgbin = (byte[])dr.GetValue(0);
                        dr.Close();
                    }
                    if (cmdsave2.Text == Resources.ValidationResources.bSave)
                    {
                        string tmpr2;
                        CategoryLoadingStatuscom.CommandType = CommandType.Text;
                        CategoryLoadingStatuscom.CommandText = "select count(*) from Item_Type where Item_Type=N'" + txtCLStatus.Value.ToUpper() + "'";
                        tmpr2 = CategoryLoadingStatuscom.ExecuteScalar().ToString();
                        CategoryLoadingStatuscom.Parameters.Clear();
                        if (tmpr2 != "0")
                        {
                            // Hidden2.Value = "1"
                            // LibObj.MsgBox1(Resources.ValidationResources.CatAlExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CatAlExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.CatAlExist, this, dbUtilities.MsgLevel.Warning);
                            // Me.txtCLStatus.Focus()
                            this.SetFocus(txtCLStatus);
                            return;
                        }
                        else
                        {
                            Hidden2.Value = "0";

                        }
                        string tmpr3;
                        CategoryLoadingStatuscom.CommandText = "select count(*) from Item_Type where  Abbreviation=N'" + txtAbbreviation.Value + "' ";
                        tmpr3 = CategoryLoadingStatuscom.ExecuteScalar().ToString();
                        CategoryLoadingStatuscom.Parameters.Clear();
                        if (tmpr3 != "0")
                        {
                            // hdabbreviation.Value = "1"
                            // LibObj.MsgBox1(Resources.ValidationResources.AbbriAlExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.AbbriAlExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.AbbriAlExist, this, dbUtilities.MsgLevel.Warning);
                            this.txtAbbreviation.Value = string.Empty;

                            this.SetFocus(txtAbbreviation);
                            return;
                        }
                        else
                        {
                            hdabbreviation.Value = "0";
                        }
                    }
                    // ********End of Duplicate Check
                    // ==================
                    CategoryLoadingStatuscom.CommandType = CommandType.StoredProcedure;
                    CategoryLoadingStatuscom.CommandText = "insert_Item_Type_1";

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@id_1", OleDbType.Integer));
                    CategoryLoadingStatuscom.Parameters["@id_1"].Value = (object)txtCLStatusCode.Value;

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@Item_Type_2", OleDbType.VarWChar));
                    CategoryLoadingStatuscom.Parameters["@Item_Type_2"].Value = txtCLStatus.Value.Trim();

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@Abbreviation_3", OleDbType.VarWChar));
                    CategoryLoadingStatuscom.Parameters["@Abbreviation_3"].Value = txtAbbreviation.Value.Trim();

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@Item_icon_4", OleDbType.Binary));
                    CategoryLoadingStatuscom.Parameters["@Item_icon_4"].Value = imgbin;

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@userid_5", OleDbType.VarWChar));
                    var logged = LoggedUser.Logged();
                    CategoryLoadingStatuscom.Parameters["@userid_5"].Value = logged.UserId;
                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@FormID", OleDbType.VarWChar));
                    CategoryLoadingStatuscom.Parameters["@FormID"].Value = 10;
                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@Type", OleDbType.SmallInt));
                    CategoryLoadingStatuscom.Parameters["@Type"].Value = 1;

                    CategoryLoadingStatuscom.ExecuteNonQuery();

                    // Dim temp As String = String.Empty
                    CategoryLoadingStatuscom.Parameters.Clear();

                    if (logged.IsAudit=="Y")
                    {
                        if (cmdsave2.Text == Resources.ValidationResources.bSave)
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lblTitle.Text, logged.Session, txtCLStatus.Value, Resources.ValidationResources.Insert, retConstr(""));
                        }
                        else
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lblTitle.Text, logged.Session, txtCLStatus.Value, Resources.ValidationResources.bUpdate, retConstr(""));
                        }
                    }



                    // If cmdsave.Value() = "Submit" Then
                    // LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtCLStatus.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                    // Else
                    // LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtCLStatus.Value), "Update", retConStr(Session("LibWiseDBConn")))
                    // End If
                    // jitendra-----------


                    hd_id.Value = txtCLStatusCode.Value;
                    // Hidden3.Value = "1"
                    // LibObj.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    tran.Commit();
                    this.SetFocus(txtCLStatus);
                    // txtCLStatus.Focus()
                    cmdsave2.Text = Resources.ValidationResources.bSave;
//                    aa1 = string.Empty;
  //                  aa2 = null;
                    // If cmdReturn.Disabled = True Then
                    // Dim returnScript As String = ""
                    // returnScript &= "<script language='javascript' type='text/javascript'>"
                    // returnScript &= "javascript:retOnSC('txtCLStatusCode');"
                    // returnScript &= "<" & "/" & "script>"
                    // Page.RegisterStartupScript("", returnScript)
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtCLStatusCode');", true);
                }

                // End If
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        // msglabel.Visible = True
                        // msglabel.Text = ex.Message
                        // hdUnableMsg.Value = "s"
                        // LibObj.MsgBox1(Resources.ValidationResources.UnsaveCLSInfo.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UnsaveCLSInfo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UnsaveCLSInfo, this, dbUtilities.MsgLevel.Warning);
                    }
                    catch (Exception ex1)
                    {
                        // msglabel.Visible = True
                        // msglabel.Text = ex1.Message
                        // hdUnableMsg.Value = "s"
                        // LibObj.MsgBox1(Resources.ValidationResources.UnsaveCLSInfo.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnsaveCLSInfo.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UnsaveCLSInfo, this, dbUtilities.MsgLevel.Warning);
                    }
                }
                finally
                {
                    CategoryLoadingStatusCon.Close();
                    CategoryLoadingStatusCon.Dispose();
                }

                // Me.txtCLStatus.Value = String.Empty

                // Grid Code Refilling
                OleDbConnection conn;
                conn = new OleDbConnection(retConstr(""));
                conn.Open();
                var CategoryLoadingStatusda = new OleDbDataAdapter("Select * from Item_Type where id <> 0  order by Item_Type", conn);
                var CategoryLoadingStatusds = new DataSet();
                CategoryLoadingStatusda.Fill(CategoryLoadingStatusds);
                grdCLStatus.DataSource = CategoryLoadingStatusds.Tables[0].DefaultView;
                grdCLStatus.DataBind();
                hdnGrdId.Value = grdCLStatus.ClientID;

                CategoryLoadingStatusds.Dispose();
                CategoryLoadingStatusda.Dispose();
                conn.Close();
                conn.Dispose();
                // End Of Grid Code
                clear_field();
            }
            // Call GenerateCLStatusID()
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // hdUnableMsg.Value = "s"
                // LibObj.MsgBox1(Resources.ValidationResources.UnsaveCLSInfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnsaveCLSInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnsaveCLSInfo, this, dbUtilities.MsgLevel.Failure);
            }
        }
        private void cmdreset_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                if (Hidden5.Value == "Y")
                {
                    this.cmddelete2.Enabled = true;
                    this.cmdsave2.Enabled = true;
                }
                else if (Hidden5.Value == "N")
                {
                    this.cmddelete2.Enabled=false;
                    this.cmdsave2.Enabled = false;
                }
                else
                {
                    this.cmddelete2.Enabled = true;
                    this.cmdsave2.Enabled = true;
                    // Me.cmdReturn.Disabled = True
                }
                this.cmddelete2.Enabled = false;
                hdTop.Value = Resources.ValidationResources.RBTop;
                image1.Src = "~//images/AddImage.jpg";
                clear_field();
                hd_id.Value = "";
                hd_name.Value = "";
                hd_short.Value = "";
                txtCLStatusCode.Value = "";
                // Call GenerateCLStatusID()
                cmdsave2.Text = Resources.ValidationResources.bSave;
                this.SetFocus(txtCLStatus);

                txtAbbreviation.Disabled = false;
                txtCLStatus.Disabled = false;
                this.grdCLStatus.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public int checkbeforedelete(string itemtype)
        {

            try
            {

                var chkcon = new OleDbConnection(retConstr(""));
                chkcon.Open();
                var chkds = new DataSet();
                // Dim chkda As OleDbDataAdapter
                OleDbCommand chkcmd;
                chkcmd = new OleDbCommand("select count(*) from bookcatalog where ItemCategory='" + itemtype + "'", chkcon);
                int count = 0;
                count = Convert.ToInt32(chkcmd.ExecuteScalar());
                chkcon.Close();
                return count;
            }

            catch (Exception ex)
            {
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            return -1;

        }


        protected void grdCLStatus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {

                    case "Select":
                        {
                            this.cmddelete2.Enabled = true;
                            string str = "item";
                            var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                            CategoryLoadingStatusCon.Open();

                            var CategoryLoadingStatusda = new OleDbDataAdapter("Select * from Item_Type where id=" + grdCLStatus.Items[e.Item.ItemIndex].Cells[1].Text, CategoryLoadingStatusCon);
                            var CategoryLoadingStatusds = new DataSet();
                            CategoryLoadingStatusda.Fill(CategoryLoadingStatusds, "CategoryLoadingStatus");

                            this.txtCLStatus.Value = CategoryLoadingStatusds.Tables[0].Rows[0][1].ToString();
                            txtCLStatus.Disabled = false;
                          //  if (checkbeforedelete(txtCLStatus.Value) > 0)
                            //{
                              //  txtCLStatus.Disabled = true;

//                            }


                            this.txtCLStatusCode.Value = CategoryLoadingStatusds.Tables[0].Rows[0][0].ToString();
                            this.txtAbbreviation.Value = CategoryLoadingStatusds.Tables[0].Rows[0][2].ToString();
                            this.image1.Src = "imagehiddenform.aspx?cno=" + grdCLStatus.Items[e.Item.ItemIndex].Cells[1].Text + "&id2=" + str;

                            hd_id.Value = txtCLStatusCode.Value;
                            hd_name.Value = txtCLStatus.Value;
                            hd_short.Value = txtAbbreviation.Value;
                            // +++++++++++++Set Any Item_Type as Default In Library setupinformation table 
                            // +++++++++++++++++++By: Jeetendra Prajapati as on 04 Dec 009

                            // If LibObj.checkChildExistance("category", "librarysetupinformation", "category='" & CategoryLoadingStatusds.Tables(0).Rows(0).Item(1) & "'", retConStr(Session("LibWiseDBConn"))) = True Then
                            // cmddelete.Disabled = True
                            // cmdsave.Value = Resources.ValidationResources.bUpdate.ToString
                            // txtCLStatus.Disabled = True
                            // 'Hidden2.Value = "No"
                            // LibObj.MsgBox1(Resources.ValidationResources.CatNoModify.ToString, Me)
                            // 'txtAbbreviation.Focus()

                            // Me.SetFocus(txtAbbreviation)
                            // Exit Sub
                            // Else
                            // txtCLStatus.Disabled = False
                            // cmddelete.Disabled = False
                            // cmdsave.Disabled = False
                            // End If
                            CategoryLoadingStatusds.Dispose();
                            CategoryLoadingStatusda.Dispose();
                            CategoryLoadingStatusCon.Close();
                            CategoryLoadingStatusCon.Dispose();
                            cmdsave2.Text = Resources.ValidationResources.bUpdate;
                            if (Hidden5.Value == "Y")
                            {
                                this.cmddelete2.Enabled = true;

                                this.cmdsave2.Enabled = true;
                            }
                            else if (Hidden5.Value == "N")
                            {
                                this.cmddelete2.Enabled = false;
                                this.cmdsave2.Enabled = false;
                            }
                            else
                            {
                                this.cmdsave2.Enabled = true;
                                this.cmddelete2.Enabled = true;// .Disabled = false;
                                // Me.cmdReturn.Disabled = True
                            }
                            this.SetFocus(txtCLStatus);
                            break;
                        }
                        // LibObj.SetFocus("txtCLStatus", Me)
                }
            }

            // Session("dptname") = txtdepartmentname.Value
            // Session("srtname") = txtshortname.Value
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                // hdUnableMsg.Value = "d"
                // LibObj.MsgBox1(Resources.ValidationResources.UnRetrieveCLSInfo.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UnRetrieveCLSInfo.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UnRetrieveCLSInfo, this, dbUtilities.MsgLevel.Failure);
            }
            try
            {
               // if (checkbeforedelete(txtCLStatus.Value) < 0)
                //{
                 //   cmdsave2.Enabled = false;

//                }
            }

            catch (Exception ex)
            {

            }
        }

        protected void cmdsave2_Click(object sender, EventArgs e)
        {
            cmdsave_ServerClick(sender,e);
        }

        protected void cmdreset2_Click(object sender, EventArgs e)
        {
            cmdreset_ServerClick(sender,e);
        }

        protected void cmddelete2_Click(object sender, EventArgs e)
        {
           
        }
    }
}