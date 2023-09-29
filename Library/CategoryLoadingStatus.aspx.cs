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
   public partial class CategoryLoadingStatus : BaseClass
   {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string aa1;
        private string tmpcondition;
        private static System.Web.HttpPostedFile aa2;
        private byte mempic;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();


        private DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnGrdId.Value = grdCLStatus.ClientID;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                msglabel.Visible = false;
                cmdreset.CausesValidation = false;
                cmdDelete.CausesValidation = false;
                //this.SetFocus(txtCLStatus);
                if (!IsPostBack)
                {
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    lblTitle.Text = Request.QueryString["title"];
                    // 
                    this.Hidden5.Value = Request.QueryString["condition"];
                    Session["NFormDW"] = null;
                    if (Hidden5.Value == "Y")
                    {
                        this.cmdDelete.Visible = false;
                        //this.cmdSave.Disabled = false;
                    }
                    else if (Hidden5.Value == "N")
                    {
                        this.cmdDelete.Visible = true;
                        this.cmdSave.Visible = true;
                    }
                    else
                    {
                        lblTitle.Text = Resources.ValidationResources.Litmcat;
                        //this.cmdSave.Disabled = false;
                        this.cmdDelete.Visible = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";

                    }
                    this.cmdDelete.Visible = true;
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                    CategoryLoadingStatusCon.Open();
                    var CategoryLoadingStatusda = new OleDbDataAdapter("select * from CategoryLoadingStatus  order by Category_LoadingStatus", CategoryLoadingStatusCon);
                    var CategoryLoadingStatusds = new DataSet();
                    CategoryLoadingStatusda.Fill(CategoryLoadingStatusds);
                    if (CategoryLoadingStatusds.Tables[0].Rows.Count > 0)
                    {
                        grdCLStatus.DataSource = CategoryLoadingStatusds.Tables[0].DefaultView;
                        grdCLStatus.DataBind();
                    }
                    else
                    {

                        grdCLStatus.DataSource = dt;
                        grdCLStatus.DataBind();
                    }
                    hdnGrdId.Value = grdCLStatus.ClientID;
                    CategoryLoadingStatusds.Dispose();
                    CategoryLoadingStatusda.Dispose();
                    CategoryLoadingStatusCon.Close();
                    CategoryLoadingStatusCon.Dispose();

                    
                }

            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }

        }
        
        protected void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmdSave.Text == Resources.ValidationResources.bSave)
                {
                    GenerateCLStatusID();
                }

                // *********Duplicate Check
                OleDbConnection CategoryLoadingStatusCon;
                CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                CategoryLoadingStatusCon.Open();
                var check_ds = new DataSet();
                if (cmdSave.Text == Resources.ValidationResources.bUpdate)
                {
                    if ((hd_name.Value) != (txtCLStatus.Value))

                    {
                        check_ds = LibObj.PopulateDataset("select * from CategoryLoadingStatus where Category_LoadingStatus=N'" + txtCLStatus.Value + "' ", "category", CategoryLoadingStatusCon);
                        cmdDelete.Visible = true;
                        if (check_ds.Tables[0].Rows.Count > 0)
                        {
                           
                            message.PageMesg(Resources.ValidationResources.CatAlExist, this, dbUtilities.MsgLevel.Warning);
                           Button.Disabled = true;
                            return;
                        }
                    }
                    else if ((hd_short.Value) != (txtAbbreviation.Value))
                    {
                        check_ds = LibObj.PopulateDataset("select * from CategoryLoadingStatus where Abbreviation=N'" + txtAbbreviation.Value + "' ", "category", CategoryLoadingStatusCon);
                        Button.Disabled = true;
                        if (check_ds.Tables[0].Rows.Count > 0)
                        {
                           
                            message.PageMesg(Resources.ValidationResources.AbbriAlExist, this, dbUtilities.MsgLevel.Warning);

                           cmdDelete.Visible = true;
                            return;
                        }
                    }
                   

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
                    if (cmdSave.Text == Resources.ValidationResources.bSave)
                    {
                        if (aa2 != null) 

                        {
                            if (aa1.Trim().Length > 0 & aa2.ContentLength > 0)
                            {
                                var imgStream = aa2.InputStream;
                                imglen = aa2.ContentLength;
                                var imgBinaryData = new byte[imglen + 1];
                                Int32 n = imgStream.Read(imgBinaryData, 0, imglen);
                                imgbin = imgBinaryData;
                            }
                        }
                    }
                    else if (aa2 != null)
                    {
                        if (aa1.Trim().Length > 0 & aa2.ContentLength > 0)
                        {
                            var imgStream = aa2.InputStream;
                            imglen = aa2.ContentLength;
                            var imgBinaryData = new byte[imglen + 1];
                            Int32 n = imgStream.Read(imgBinaryData, 0, imglen);
                            imgbin = imgBinaryData;
                        }
                    }
                    else
                    {
                        CategoryLoadingStatuscom.CommandType = CommandType.Text;
                        CategoryLoadingStatuscom.CommandText = "select cat_icon from CategoryLoadingStatus where Id='" + (hd_id.Value) + "'";
                        var dr = CategoryLoadingStatuscom.ExecuteReader();
                        dr.Read();
                        imgbin = (byte[])dr.GetValue(0);
                        dr.Close();
                    }
                    if (cmdSave.Text == Resources.ValidationResources.bSave)
                    {
                        string tmpr2;
                        CategoryLoadingStatuscom.CommandType = CommandType.Text;
                       
                        CategoryLoadingStatuscom.CommandText = "select Category_LoadingStatus from CategoryLoadingStatus where Category_LoadingStatus=N'" + (txtCLStatus.Value) + "'";
                        OleDbDataAdapter odb = new OleDbDataAdapter();
                        DataTable dtexis = new DataTable();
                        CategoryLoadingStatuscom.Parameters.Clear();
                        odb.Fill(dtexis);

                       
                        if (dtexis.Rows.Count>0)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.CatAlExist, this, dbUtilities.MsgLevel.Warning);
                          
                            return;
                        }
                        else
                        {
                            Hidden2.Value = "0";

                        }
                        string tmpr3;
                        CategoryLoadingStatuscom.CommandText = "select Abbreviation from CategoryLoadingStatus where  Abbreviation=N'" + (txtAbbreviation.Value) + "' ";
                        OleDbDataAdapter otdb = new OleDbDataAdapter();
                        DataTable dbexis = new DataTable();
                        odb.Fill(dbexis);
                        //tmpr3 = CategoryLoadingStatuscom.ExecuteScalar().ToString();
                        CategoryLoadingStatuscom.Parameters.Clear();
                        if (dbexis.Rows[0][0].ToString() == string.Empty)
                        {
                           
                            message.PageMesg(Resources.ValidationResources.AbbriAlExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                            this.txtAbbreviation.Value = string.Empty;

                          
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
                    CategoryLoadingStatuscom.CommandText = "insert_CategoryLoadingStatus_1";

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@id_1", OleDbType.Integer));
                    CategoryLoadingStatuscom.Parameters["@id_1"].Value = txtCLStatusCode.Value;

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@Category_LoadingStatus_2", OleDbType.VarWChar));
                    CategoryLoadingStatuscom.Parameters["@Category_LoadingStatus_2"].Value = (txtCLStatus.Value);

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@Abbreviation_3", OleDbType.VarWChar));
                    CategoryLoadingStatuscom.Parameters["@Abbreviation_3"].Value = (txtAbbreviation.Value);

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@cat_icon_4", OleDbType.Binary));
                    CategoryLoadingStatuscom.Parameters["@cat_icon_4"].Value = imgbin;

                    CategoryLoadingStatuscom.Parameters.Add(new OleDbParameter("@userid_5", OleDbType.VarWChar));
                    CategoryLoadingStatuscom.Parameters["@userid_5"].Value = LoggedUser.Logged().Session;

                    CategoryLoadingStatuscom.ExecuteNonQuery();

                    // ------------------------------------------------------------------------------------
                    CategoryLoadingStatuscom.Parameters.Clear();
                    CategoryLoadingStatuscom.CommandType = CommandType.Text;
                    CategoryLoadingStatuscom.CommandText = "Update bookaccessionmaster set itemcategoryCode = '" + txtCLStatusCode.Value + "',itemCategory = '" + txtCLStatus.Value + "' where (itemCategoryCode = '" + txtCLStatusCode.Value + "' or itemCategory = '" + hd_name.Value + "')";
                    CategoryLoadingStatuscom.ExecuteNonQuery();
                    // ------------------------------------------------------------------------------------

                    // ------------------------------------------------------------------------------------
                    CategoryLoadingStatuscom.Parameters.Clear();
                    CategoryLoadingStatuscom.CommandType = CommandType.Text;
                    CategoryLoadingStatuscom.CommandText = "Update BookCatalog set booktype = '" + txtCLStatusCode.Value + "',btype = '" + txtCLStatus.Value + "' where (booktype = '" + txtCLStatusCode.Value + "' or btype = '" + hd_name.Value + "')";
                    CategoryLoadingStatuscom.ExecuteNonQuery();
                    // ------------------------------------------------------------------------------------

                    // Dim temp As String = String.Empty
                    CategoryLoadingStatuscom.Parameters.Clear();
                    var logged = LoggedUser.Logged();
                    if (logged.IsAudit == "Y")
                        
                    {
                        if (cmdSave.Text == Resources.ValidationResources.bSave)
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, this.txtCLStatus.Value.Trim(), Resources.ValidationResources.Insert, retConstr(""));

                         }
                        else
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, this.txtCLStatus.Value.Trim(), Resources.ValidationResources.bUpdate, retConstr(""));
                        }
                    }

                    hd_id.Value = txtCLStatusCode.Value;
                    
                    message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    tran.Commit();
                    
                    cmdSave.Text = Resources.ValidationResources.bSave;
                    aa1 = string.Empty;
                    aa2 = null;
                    
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtCLStatusCode');", true);
                }

                // End If
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        
                        message.PageMesg(Resources.ValidationResources.UnsaveCLSInfo, this, dbUtilities.MsgLevel.Failure);
                    }
                    catch (Exception ex1)
                    {
                        
                        message.PageMesg(Resources.ValidationResources.UnsaveCLSInfo, this, dbUtilities.MsgLevel.Failure);

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
                var CategoryLoadingStatusda = new OleDbDataAdapter("Select * from CategoryLoadingStatus where id <> 0  order by Category_LoadingStatus", conn);
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
                
                message.PageMesg(Resources.ValidationResources.UnsaveCLSInfo, this, dbUtilities.MsgLevel.Failure);

            }

        }
        public void GenerateCLStatusID()
        {
            var CategoryLoadingStatusconn = new OleDbConnection(retConstr(""));
            CategoryLoadingStatusconn.Open();
            var CategoryLoadingStatuscom3 = new OleDbCommand();
            CategoryLoadingStatuscom3.Connection = CategoryLoadingStatusconn;
            CategoryLoadingStatuscom3.CommandType = CommandType.Text;
            CategoryLoadingStatuscom3.CommandText = "select coalesce(max(id),0,max(id)) from CategoryLoadingStatus";
            string tmpstr;
            tmpstr = Convert.ToString(CategoryLoadingStatuscom3.ExecuteScalar());
            //txtCLStatusCode.Value = Val(IIf(Val(tmpstr) == 0, 1, Val(tmpstr) + 1));
           
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
            image1.Src = null;
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hidden5.Value == "Y")
                {
                    this.Button.Disabled = false;
                    this.cmdSave.Visible = true;
                }
                else if (Hidden5.Value == "N")
                {
                    this.Button.Disabled = true;
                    this.cmdSave.Visible = true;
                }
                else
                {
                    this.Button.Disabled = false;
                    this.cmdSave.Visible = false;
                    // Me.cmdReturn.Disabled = True
                }
                this.Button.Disabled = true;
                hdTop.Value = Resources.ValidationResources.RBTop;
                image1.Src = null;
                clear_field();
                hd_id.Value = "";
                hd_name.Value = "";
                hd_short.Value = "";
                txtCLStatusCode.Value = "";
                //Call GenerateCLStatusID()
                cmdSave.Text = Resources.ValidationResources.bSave;
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

        private void grdCLStatus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            this.Button.Disabled = false;
                            string str = "cat";
                            var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                            CategoryLoadingStatusCon.Open();

                            var CategoryLoadingStatusda = new OleDbDataAdapter("Select * from CategoryLoadingStatus where id=" + grdCLStatus.Items[e.Item.ItemIndex].Cells[1].Text.ToString(), CategoryLoadingStatusCon);
                            var CategoryLoadingStatusds = new DataSet();
                            CategoryLoadingStatusda.Fill(CategoryLoadingStatusds, "CategoryLoadingStatus");

                            this.txtCLStatus.Value = CategoryLoadingStatusds.Tables[0].Rows[0][1].ToString();
                            this.txtCLStatusCode.Value = CategoryLoadingStatusds.Tables[0].Rows[0][0].ToString();
                            this.txtAbbreviation.Value = CategoryLoadingStatusds.Tables[0].Rows[0][2].ToString();
                            this.image1.Src = "imagehiddenform.aspx?cno=" + (grdCLStatus.Items[e.Item.ItemIndex].Cells[1].Text.ToString()) + "&id2=" + str;

                            hd_id.Value = txtCLStatusCode.Value;
                            hd_name.Value = txtCLStatus.Value;
                            hd_short.Value = txtAbbreviation.Value;
                            if  (LibObj.checkChildExistance("category", "librarysetupinformation", "category='"
                                + CategoryLoadingStatusds.Tables[0].Rows[0][1].ToString() + "'", retConstr(""))
                                == true)
                               
                                {
                                Button.Disabled = true;
                                cmdSave.Text = Resources.ValidationResources.bUpdate.ToString();
                                txtCLStatus.Disabled = true;
                               
                                message.PageMesg(Resources.ValidationResources.CatNoModify.ToString(), this, dbUtilities.MsgLevel.Warning);
                                // txtAbbreviation.Focus()

                                this.SetFocus(txtAbbreviation);
                                return;
                            }
                            else
                            {
                                txtCLStatus.Disabled = false;
                                Button.Disabled = false;
                                cmdSave.Visible = false;
                            }
                            CategoryLoadingStatusds.Dispose();
                            CategoryLoadingStatusda.Dispose();
                            CategoryLoadingStatusCon.Close();
                            CategoryLoadingStatusCon.Dispose();
                            cmdSave.Text = Resources.ValidationResources.bUpdate;
                            if (Hidden5.Value == "Y")
                            {
                                this.Button.Disabled = false;
                                this.cmdSave.Visible = false;
                            }
                            else if (Hidden5.Value == "N")
                            {
                                this.Button.Disabled = true;
                                this.cmdSave.Visible = true;
                            }
                            else
                            {
                                this.cmdSave.Visible = false;
                                this.Button.Disabled = false;
                                // Me.cmdReturn.Disabled = True
                            }
                            this.SetFocus(txtCLStatus);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
               
                message.PageMesg(Resources.ValidationResources.UnRetrieveCLSInfo.ToString(), this, dbUtilities.MsgLevel.Warning);
            }
        }

        private void grdCLStatus_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            try
            {
                string searchqry;
                searchqry = "SELECT *  FROM CategoryLoadingStatus where id <> 0 order by Category_LoadingStatus";
                var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                CategoryLoadingStatusCon.Open();
                var CategoryLoadingStatusda = new OleDbDataAdapter(searchqry, CategoryLoadingStatusCon);
                var CategoryLoadingStatusds = new DataSet();
                CategoryLoadingStatusda.Fill(CategoryLoadingStatusds);
                var dt = CategoryLoadingStatusds.Tables[0];
                var dv = new DataView(dt);
                grdCLStatus.CurrentPageIndex = e.NewPageIndex;
                dv.Sort = grdCLStatus.Attributes["Category_LoadingStatus"];
                grdCLStatus.DataSource = dv;
                grdCLStatus.DataBind();
                hdnGrdId.Value = grdCLStatus.ClientID;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
        }


        protected void cmddelete_Click(object sender, EventArgs e)
        {
            try
            {
                var delcon = new OleDbConnection(retConstr(""));
                delcon.Open();
                this.SetFocus(txtCLStatus);
                // LibObj.SetFocus("txtCLStatus", Me)
                var cmd = new OleDbCommand("Select id from CategoryLoadingStatus where Category_LoadingStatus=N'" + (this.txtCLStatus.Value).Replace("'", "''") + "'  and Abbreviation=N'" + (this.txtAbbreviation.Value).Replace("'", "''") + "'", delcon);
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCLStatusCode.Value = dr.GetValue(0).ToString();
                }
                else
                {
                    txtCLStatusCode.Value = string.Empty;
                }
                

                hdTop.Value = Resources.ValidationResources.RBTop;
                if ((txtCLStatus.Value) == string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.rDelSpecify.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("Category_LoadingStatus", "CategoryLoadingStatus", "id='" + txtCLStatusCode.Value.ToString() + "'", retConstr("")) == false)
                {
                   
                    message.PageMesg(Resources.ValidationResources.rDelNotExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("category", "existingbookkinfo", "category='" + this.txtCLStatusCode.Value + "'", retConstr("")) == true)
                {
                    // Hidden3.Value = "4"
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("category_id", "Direct_invoice_transaction", "category_id='" + this.txtCLStatusCode.Value.ToString() + "'", retConstr("")) == true)
                {
                    // Hidden3.Value = "4"
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("category_id", "thesis_accessioning", "category_id='" + this.txtCLStatusCode.Value.ToString() + "'", retConstr("")) == true)
                {
                    // Hidden3.Value = "4"
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("category", " indentmaster", "category='" + this.txtCLStatusCode.Value.ToString() + "'", retConstr("")) == true)
                {
                    // Hidden3.Value = "4"
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("itemCategoryCode", " bookaccessionmaster", "itemCategoryCode='" + (this.txtCLStatusCode.Value) + "'", retConstr("")) == true)
                {
                    // Hidden3.Value = "4"
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else if (LibObj.checkChildExistance("booktype", " BookCatalog", "booktype='" + this.txtCLStatusCode.Value.ToString() + "'", retConstr("")) == true)
                {
                    // Hidden3.Value = "4"
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist.ToString(), this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                else
                {

                    OleDbTransaction tran;
                    tran = delcon.BeginTransaction();
                    var delcom = new OleDbCommand(); // ("delete  from CD_SERVICE_MASTER where Service_id='" & Trim(txtCDServiceCode.Value) & "'", delcon)
                    delcom.Connection = delcon;
                    delcom.CommandType = CommandType.Text;
                    delcom.Transaction = tran;
                    try
                    {

                        delcom.CommandText = "delete  from CategoryLoadingStatus where id='" + this.txtCLStatusCode.Value.ToString() + "'";
                        delcom.ExecuteNonQuery();
                        // delcom.Dispose()



                        // Dim CDServiceda2 As New OleDbDataAdapter '("select Service_Name,Description,Service_id from CD_SERVICE_MASTER order by Service_Name", delcon)
                        // Dim CDServiceds2 As New DataSet
                        // delcom.CommandType = CommandType.Text
                        // delcom.CommandText = "select * from CategoryLoadingStatus  where id <>0 order by Category_LoadingStatus"
                        // CDServiceda2.SelectCommand = delcom
                        // CDServiceda2.Fill(CDServiceds2)
                        // grdCLStatus.DataSource = CDServiceds2.Tables(0).DefaultView
                        // grdCLStatus.DataBind()
                        // CDServiceds2.Dispose()
                        // CDServiceda2.Dispose()

                        // LibObj1.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtCDServiceCode.Value), "Delete", retConStr(Session("LibWiseDBConn")))
                        // Hidden3.Value = "5"
                        // LibObj.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);
                        var logged = LoggedUser.Logged();
                        if (logged.IsAudit == "Y")
                            
                        {
                            LibObj1.insertLoginFunc(logged.UserName, lblTitle.Text, logged.Session, this.txtCLStatus.Value, Resources.ValidationResources.Insert, retConstr(""));

                          }


                     
                        tran.Commit();
                        //FillAfterDelete(delcon);

                        this.SetFocus(txtCLStatus);
                    }

                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();
                           
                            message.PageMesg(Resources.ValidationResources.Udelrec.ToString(), this, dbUtilities.MsgLevel.Warning);
                        }
                        catch (Exception ex2)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.Udelrec.ToString(), this, dbUtilities.MsgLevel.Warning);
                        }
                    }
                    finally
                    {
                        delcon.Close();
                        delcon.Dispose();
                    }
                }
                //clear_field();
                this.cmdSave.Text = Resources.ValidationResources.bSave.ToString();
                this.Button.Disabled = true;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }
   }
   
 }
