using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Data.OleDb;
using System.Web.Services;
using System.Collections.Generic;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class upload : BaseClass
    {

        libGeneralFunctions Libobj = new libGeneralFunctions();
        static string tmpcondition;
        System.Web.HttpPostedFile ImageFile;
        messageLibrary msg = new messageLibrary();
        //SendEmailSMSData SSA = new SendEmailSMSData();

        String ImageFileName;
        dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                OleDbConnection upldcon = new OleDbConnection(retConstr(""));
                upldcon.Open();
                btnupld.CausesValidation = true;
                this.SetFocus(Txtgroup);

                lbltitle.Text = Request.QueryString["title"];
                tmpcondition = Request.QueryString["condition"];
                this.Grddownldrec(upldcon);
                if (tmpcondition == "Y")
                {
                    this.btndelete.Visible = false;
                    this.btnupld.Visible = false;
                }
                else if (tmpcondition == "N")
                {
                    this.btndelete.Visible = true;
                    this.btnupld.Visible = true;
                }
                else
                {
                    this.btnupld.Visible = false;
                    // this.btndelete.Disabled = false;
                    //this.cmdReturn.Disabled = true;
                }
                //this.cmdReturn.CausesValidation = false;
                this.btnupld.Visible = true;
                this.btndelete.Visible = true;
                GlobClassTr gClas = new GlobClassTr();
                string sQer = "select * from upload_group ";
                gClas.TrOpen();
                DataTable dtG = gClas.DataT(sQer);
                gClas.TrClose();
                grdGrp.AllowPaging = false;
                grdGrp.DataSource = dtG;
                grdGrp.DataBind();
                btnupld.Visible = true;
                //hdnGrdId.Value = grdGrp.ClientID;
                //SSA.MakeAccessible(grdGrp);
            }
            LblMessage.Visible = false;
        }

        protected void btnupld_Click(object sender, EventArgs e)
        {
            OleDbConnection upldcon = new OleDbConnection(retConstr(""));
            OleDbCommand upldcommand = new OleDbCommand();
            try
            {
                upldcon.Open();
                upldcommand.Connection = upldcon;
                if (btnupld.Text.Equals(Resources.ValidationResources.FileUpld))
                {
                 
                    upldcommand.CommandType = CommandType.Text;
                    upldcommand.CommandText = "select title from uploads where title=N'" + TxtTitle.Value + "'";
                    if (Convert.ToString(upldcommand.ExecuteScalar()) != string.Empty)
                    {
                       
                        message.PageMesg(Resources.ValidationResources.Exsttitle.ToString(), this, dbUtilities.MsgLevel.Warning);

                        return;
                    }
                    upldcommand.Parameters.Clear();
                    upldcommand.CommandType = CommandType.Text;
                    upldcommand.CommandText = "select file_url from uploads where file_url=N'" + this.FileUpld.FileName.ToString() + "'";
                    if (Convert.ToString(upldcommand.ExecuteScalar()) != string.Empty)
                    {
                      
                        message.PageMesg(Resources.ValidationResources.UpldFileexst.ToString(), this, dbUtilities.MsgLevel.Warning);

                        this.SetFocus(FileUpld);
                        return;
                    }
                    upldcommand.Parameters.Clear();
                    //********************************************
                    string path = Server.MapPath("Downloads");
                    //IPAddress IPAddr;

                    string hostnm = Dns.GetHostName();
                    string IPAddr = Dns.Resolve(hostnm).AddressList[0].ToString();
                    FileUpld.PostedFile.SaveAs(path + "\\" + this.FileUpld.FileName.ToString());

                    upldcommand.CommandType = CommandType.Text;
                    upldcommand.CommandText = "select coalesce(max(cast(id as int)),0,max(cast(id as int))) from uploads";
                    int id = Convert.ToInt32(upldcommand.ExecuteScalar());
                    HDtitleid.Value = Convert.ToString((id == 0 ? 1 : id + 1));
                }
                if (btnupld.Text.Equals(Resources.ValidationResources.bUpdate))
                {
                    if (this.TxtTitle.Value.Trim() == string.Empty)
                    {
                        
                        message.PageMesg(Resources.ValidationResources.IvTitle.ToString(), this, dbUtilities.MsgLevel.Warning);

                        this.SetFocus(TxtTitle);
                        return;
                    }

                    if (this.FileUpld.FileName.Trim() == string.Empty)
                    {
                        
                        message.PageMesg(Resources.ValidationResources.BrowseFileToUpload.ToString(), this, dbUtilities.MsgLevel.Warning);

                        this.SetFocus(FileUpld);
                        return;
                    }

                    string path = Server.MapPath("Downloads");
                    //IPAddress IPAddr;

                    string hostnm = Dns.GetHostName();
                    string IPAddr = Dns.Resolve(hostnm).AddressList[0].ToString();

                    upldcommand.Parameters.Clear();
                    upldcommand.CommandType = CommandType.Text;
                    upldcommand.CommandText = "select FILE_URL from uploads where ID=N'" + HDtitleid.Value + "'";
                    string filename = upldcommand.ExecuteScalar().ToString();
                    File.Delete(path + "\\" + filename);
                    FileUpld.PostedFile.SaveAs(path + "\\" + this.FileUpld.FileName.ToString());
                    this.btnupld.Text = Resources.ValidationResources.FileUpld.ToString();//

                }
                //image-------------


                if (FileUpld.HasFile)
                {
                    Int32 temp;
                    String Img;
                    temp = FileUpld.PostedFile.FileName.LastIndexOf(".");
                    Img = FileUpld.PostedFile.FileName.Substring(temp + 1);
                    //if ((Img.ToLower == "gif") || (Img.ToLower = "jpeg") || (Img.ToLower  ="jpg") || (Img.ToLower  = "bmp") || (Img.ToLower = "dib")) 
                    //{
                    String imagepath = FileUpld.PostedFile.FileName;
                    image1.Src = imagepath;
                    image1.Alt = "Not Shown";
                    //}
                    //  Else
                    //{
                    // Libobj.("Select Valid Path", this);


                    //}
                    ImageFileName = FileUpld.PostedFile.FileName;
                    ImageFile = FileUpld.PostedFile;
                }
                else
                {
                    //    // objgeneral.NewMsgBox("Select The Image", Me)
                    image1.Src = "";


                }


                //----------------end

                Int32 imglen;
                Byte[] tempD = { 1, 0, 1, 0 };

                //Now Go image
                //If (!ImageFile is) 
                if ((ImageFileName.Length > 0) && (ImageFile.ContentLength > 0))
                {
                    System.IO.Stream imgStream = ImageFile.InputStream;
                    imglen = ImageFile.ContentLength;
                    Byte[] imgBinaryData = new byte[imglen];
                    Int32 n = imgStream.Read(imgBinaryData, 0, imglen);
                    tempD = imgBinaryData;
                }
                
                upldcommand.CommandType = CommandType.StoredProcedure;
                upldcommand.CommandText = "Insert_uploads_1";

                upldcommand.Parameters.Add("@id", OleDbType.Char).Value = HDtitleid.Value.Trim();

                upldcommand.Parameters.Add("@title", OleDbType.VarWChar).Value = TxtTitle.Value.Trim();

                upldcommand.Parameters.Add("@file_url", OleDbType.VarWChar).Value = this.FileUpld.FileName.ToString();
                upldcommand.Parameters.Add(" @group_Name", OleDbType.VarWChar).Value = this.Txtgroup.Value;
                //if (this.FileUpld.FileName.ToString()!=string.Empty)
                //{
                //    tempD =byte.Parse ((this.FileUpld.PostedFile.FileName).ToString ());
                //}
                upldcommand.Parameters.Add(" @file_url1", OleDbType.Binary).Value = tempD;

                upldcommand.Parameters.Add(" @show_flg", OleDbType.VarWChar).Value = this.DropDownList1.SelectedItem.Value;


                upldcommand.ExecuteNonQuery();
                this.Grddownldrec(upldcon);
                
                message.PageMesg(Resources.ValidationResources.RecUploadSuccess.ToString(), this, dbUtilities.MsgLevel.Success);

                //------Save Uplaod Group BY Kaushal
                OleDbConnection upldgrp = new OleDbConnection(retConstr(""));
                upldgrp.Open();
                OleDbCommand cmdgrp = new OleDbCommand();
                cmdgrp.Connection = upldgrp;

                OleDbDataAdapter dk = new OleDbDataAdapter("Select distinct group_name from  Upload_Group", upldgrp);
                DataSet sk = new DataSet();
                dk.Fill(sk);
                Boolean tr = true;
                for (int i = 0; sk.Tables[0].Rows.Count > i; i++)
                {
                    if (Txtgroup.Value.ToString() == sk.Tables[0].Rows[i][0].ToString())
                    {
                        tr = false;
                        break;

                    }

                }


                if (tr == true)
                {


                    cmdgrp.Parameters.Clear();
                    cmdgrp.Connection = upldgrp;
                    cmdgrp.CommandType = CommandType.Text;
                    cmdgrp.CommandText = "Select Coalesce(max(id),0) from  Upload_Group";
                    string id = cmdgrp.ExecuteScalar().ToString();
                    cmdgrp.Parameters.Clear();
                    cmdgrp.Connection = upldgrp;
                    cmdgrp.CommandType = CommandType.Text;
                    cmdgrp.CommandText = "Insert Into Upload_Group (id ,Group_name) values(" + (int.Parse(id) + 1) + ",'" + Txtgroup.Value + "')";
                    cmdgrp.ExecuteNonQuery();
                }
                //ENd
                btnreset_Click(sender, e);
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                
                message.PageMesg(Resources.ValidationResources.RecnotUpload.ToString(), this, dbUtilities.MsgLevel.Warning);

            }

            finally
            {
                upldcommand.Parameters.Clear();
                upldcommand.Dispose();
                if (upldcon.State == ConnectionState.Open)
                {
                    upldcon.Close();
                }
                upldcon.Dispose();
            }
        }

        protected void Grddownldrec(OleDbConnection con)
        {
            DataSet grdDs = new DataSet();
            grdDs = Libobj.PopulateDataset("select id,title,file_url,Group_Name from uploads order by id", "uploads", con);
            this.Grddownld.DataSource = grdDs;
            this.Grddownld.DataBind();
            Grddownld.AllowPaging = false;
            hdnGrdId.Value = Grddownld.ClientID;
            //SSA.MakeAccessible(Grddownld);

        }

        protected void Grddownld_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OleDbConnection upldcon = new OleDbConnection(retConstr(""));
            try
            {
                upldcon.Open();
                Grddownld.PageIndex = e.NewPageIndex;
                this.Grddownldrec(upldcon);
            }
            catch (Exception ex)
            {
                //LblMessage.Visible = true;
                //LblMessage.Text = ex.Message;
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
            finally
            {
                if (upldcon.State == ConnectionState.Open)
                {
                    upldcon.Close();
                }
                upldcon.Dispose();
            }
        }

        protected void Grddownld_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            OleDbConnection upldcon = new OleDbConnection(retConstr(""));
            DataSet upldds = new DataSet();
            try
            {
                if (e.CommandName == "show")
                {
                    upldcon.Open();
                    int i = Convert.ToInt32(e.CommandArgument);
                    this.HDtitleid.Value = Convert.ToInt32(e.CommandArgument).ToString();//((LinkButton)(Grddownld.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].FindControl("Lnktitle"))).Text;
                    if (Int32.Parse(this.HDtitleid.Value) <= 5)
                        btnupld.Text = Resources.ValidationResources.bUpdate;
                    btndelete.Visible = true;
                    OleDbDataAdapter upldad = new OleDbDataAdapter("select title,file_url,Group_Name,web_opacflg from uploads where id=N'" + HDtitleid.Value + "'", upldcon);
                    upldad.Fill(upldds);
                    this.TxtTitle.Value = upldds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    this.Txtgroup.Value = upldds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                    if (upldds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != string.Empty)
                    {
                        this.DropDownList1.SelectedValue = upldds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    }
                    else
                    {
                        this.DropDownList1.SelectedValue = "N";
                    }
                    btndelete.Visible = true;
                    btnupld.CausesValidation = false;
                    lblmnd2.Visible = false;
                    //this.SetFocus(TxtTitle);
                    upldad.Dispose();
                }
            }
            catch (Exception ex)
            {
                upldds.Dispose();
                // LblMessage.Text = ex.Message;
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);

            }
            finally
            {
                upldds.Dispose();
                if (upldcon.State == ConnectionState.Open)
                {
                    upldcon.Close();
                }
                upldcon.Dispose();
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            OleDbConnection upldcon = new OleDbConnection(retConstr(""));
            OleDbCommand upldcommand = new OleDbCommand();
            try
            {
                if (Convert.ToInt32(HDtitleid.Value) >= 0)
                {
                    upldcon.Open();
                    upldcommand.Connection = upldcon;
                    upldcommand.CommandType = CommandType.Text;
                    upldcommand.CommandText = "select FILE_URL from uploads where ID=N'" + HDtitleid.Value + "'";
                    string filename = upldcommand.ExecuteScalar().ToString();
                    string path = Server.MapPath("Downloads");
                    File.Delete(path + "\\" + filename);
                    upldcommand.Parameters.Clear();
                    upldcommand.Dispose();
                    OleDbCommand upldcmd = new OleDbCommand("delete from uploads where id=N'" + HDtitleid.Value + "'", upldcon);
                    upldcmd.ExecuteNonQuery();
                    upldcmd.Dispose();
                    this.Grddownldrec(upldcon);
                    //btnupld.Disabled = true;
                    btnreset_Click(sender, e);
                    // Libobj.MsgBox(Resources.ValidationResources.rDel.ToString(), this);
                    // msg.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString(), this);
                    message.PageMesg(Resources.ValidationResources.rDel.ToString(), this, dbUtilities.MsgLevel.Success);

                }
                else
                {
                    //Libobj.MsgBox1(Resources.ValidationResources.Fildelnotpermiss.ToString(), this);
                    //  msg.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.Fildelnotpermiss.ToString(), this);
                    message.PageMesg(Resources.ValidationResources.Fildelnotpermiss.ToString(), this, dbUtilities.MsgLevel.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                //Libobj.MsgBox(Resources.ValidationResources.Udelrec.ToString(), this);
                //msg.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.Udelrec.ToString(), this);
                //LblMessage.Text = ex.Message; 
                message.PageMesg(Resources.ValidationResources.Udelrec.ToString(), this, dbUtilities.MsgLevel.Failure);

            }
            finally
            {
                upldcommand.Parameters.Clear();
                upldcommand.Dispose();
                if (upldcon.State == ConnectionState.Open)
                {
                    upldcon.Close();
                }
                upldcon.Dispose();
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            LblMessage.Text = string.Empty;
            this.TxtTitle.Value = string.Empty;
            this.Txtgroup.Value = string.Empty;
            this.SetFocus(TxtTitle);
            lblmnd2.Visible = true;
            btnupld.Visible = true;
            btnupld.CausesValidation = true;
            btndelete.Visible = true;
            btnupld.Text = (Resources.ValidationResources.bDisplay);
        }

        protected void Grddownld_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.Grddownld.SelectedIndex;
            string title = this.Grddownld.SelectedRow.Cells[0].Text;
        }

        private void WriteToFile(string strPath, ref byte[] Buffer)
        {
            // Create a file
            FileStream newFile = new FileStream(strPath, FileMode.Create);

            // Write data to the file
            newFile.Write(Buffer, 0, Buffer.Length);

            // Close file
            newFile.Close();
        }

        protected void Grddownld_Sorting(object sender, GridViewSortEventArgs e)
        {
            OleDbConnection upldcon = new OleDbConnection(retConstr(""));
            DataSet deptmasterds = new DataSet();
            try
            {
                SetFocus(Grddownld);
                object strsort = Grddownld.Attributes["title"];
                Grddownld.Attributes["title"] = e.SortExpression;
                string searchqry;
                searchqry = "select id,title,file_url from uploads order by title";
                upldcon.Open();
                deptmasterds = Libobj.PopulateDataset(searchqry, "uploads", upldcon);
                DataTable dt = deptmasterds.Tables["uploads"];
                DataView dv = new DataView(dt);
                dv.Sort = Grddownld.Attributes["title"];
                Grddownld.DataSource = dv;
                Grddownld.DataBind();
                hdnGrdId.Value = grdGrp.ClientID;
                //SSA.MakeAccessible(grdGrp);

                upldcon.Close();
                dt.Dispose();
                dv.Dispose();

            }
            catch (Exception ex)
            {
                //this.LblMessage .Visible = true;
                //this.LblMessage.Text = ex.Message;
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);

            }
            finally
            {
                if (upldcon.State == ConnectionState.Open)
                {
                    upldcon.Close();
                }
                deptmasterds.Dispose();
                upldcon.Dispose();
            }

        }

        protected void lnkGrp_Click(object sender, EventArgs e)
        {
            LinkButton l = (LinkButton)sender;
            Txtgroup.Value = l.Text;

        }

    }
}