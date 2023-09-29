using CrystalDecisions.CrystalReports.Engine;
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
    public partial class Program_Master : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            hdnGrdId.Value = grd_media.ClientID;

            try
            {
                // If Session("UserName") = String.Empty Then
                // Response.Redirect("default.aspx")
                // End If
                cmdreset.CausesValidation = false;
                cmddelete.CausesValidation = false;
                // cmdReturn.CausesValidation = False
                // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
                // con.Open()
                msglabel.Visible = false;
                if (!Page.IsPostBack)
                {

                    this.SetFocus(txtprogramname);
                    Session["NFormDW"] = null;
                    Chkimport.Visible = false;
                    hdTop.Value = Resources.ValidationResources.RBTop;
//                    lbltitle.Text = Request.QueryString["title"];
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
                        // lbltitle.Text = "Member Group"
  //                      lbltitle.Text = Resources.ValidationResources.LCrsDesig;
                        this.cmdsave.Enabled = true;
                        this.cmddelete.Disabled = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    // Course/Designation
                    this.cmddelete.Disabled = true;
                    msglabel.Visible = false;
                    // Dim serviceda As New OleDb.OleDbDataAdapter("select program_name,program_id,short_name from program_master order by program_name", con)
                    // ' ''Dim serviceda As New OleDb.OleDbDataAdapter("SELECT  dbo.Program_Master.program_id, dbo.Program_Master.program_name, dbo.Program_Master.short_name,dbo.departmentmaster.departmentname FROM dbo.Program_Master INNER JOIN  dbo.departmentmaster ON dbo.Program_Master.Deptcode = dbo.departmentmaster.departmentcode order by program_name,dbo.departmentmaster.departmentname", con)
                    // ' ''Dim serviceds As New DataSet
                    // ' ''serviceda.Fill(serviceds, "fill")
                    // ' ''grd_media.DataSource = serviceds
                    // ' ''grd_media.DataBind()
                    gridfill(con);
                    // -----dept combo fill ---------
                    this.RdCourse.Checked = true;
                    this.cmbdept.Visible = true;
                    this.Label11.Visible = true;
                    cmbdept.Items.Clear();
                    // dept()
                    LibObj.populateDDL(cmbdept, "select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "departmentname", "departmentcode", HComboSelect.Value, con);
                  //  LibObj.populateDDL(cmbdept, libobject.Query("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname"), "departmentname", "departmentcode", HComboSelect.Value, con)
                 // LibObj.populateDDL(cmbdept,"select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "departmentname", "departmentcode", con);

                }
            }
            // con.Close()
            // con.Dispose()
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public void gridfill(OleDbConnection con)
        {
            // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
            // con.Open()
            // Dim prg_da As New OleDb.OleDbDataAdapter("select program_name,program_id,short_name from program_master order by program_name", con)
            // Dim prg_da As New OleDb.OleDbDataAdapter("SELECT  dbo.Program_Master.program_id, dbo.Program_Master.program_name, dbo.Program_Master.short_name,dbo.departmentmaster.departmentname FROM dbo.Program_Master INNER JOIN  dbo.departmentmaster ON dbo.Program_Master.Deptcode = dbo.departmentmaster.departmentcode order by program_name,dbo.departmentmaster.departmentname", con)
            // Dim prg_da As New OleDb.OleDbDataAdapter("select program_id,program_name,short_name,case when deptcode =0 then ''when deptcode is null then '' else (select departmentname from departmentmaster where departmentcode=deptcode)end as department from  program_master order by program_name", con)
            var ds = new DataSet();
            if (RdCourse.Checked == true)
            {
                ds = LibObj.PopulateDataset("select program_id,program_name,short_name,case when deptcode =0 then ''when deptcode is null then '' else (select departmentname from departmentmaster where departmentcode=deptcode)end as department  from  program_master where program_master.deptcode<>'0' order by program_name", "A", con);
            }
            else
            {
                ds = LibObj.PopulateDataset("select program_id,program_name,short_name,case when deptcode =0 then ''when deptcode is null then '' else (select departmentname from departmentmaster where departmentcode=deptcode)end as department  from  program_master where program_master.deptcode='0' order by program_name", "A", con);

            }
           // grd_media.CurrentPageIndex = 0;
            grd_media.DataSource = ds;
            grd_media.DataBind();
            hdnGrdId.Value = grd_media.ClientID;

            ds.Dispose();
            // con.Close()
            // con.Dispose()
        }

        protected void grd_media_ItemCommand(object source, DataGridCommandEventArgs e)
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

                            this.cmddelete.Disabled = false;
                            // Dim con As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
                            // con.Open()
                            // Dim da As New OleDbDataAdapter("select program_name,program_id,short_name from program_master where program_id=" & grd_media.Items(e.Item.ItemIndex).Cells(2).Text, con)
                            // ------------------------------------------------------------
                            // --------show selected value of dept in combo frm the grd-------------------
                            // Dim da As New OleDbDataAdapter("select program_name,program_id,short_name,deptcode from program_master where program_id=" & grd_media.Items(e.Item.ItemIndex).Cells(2).Text, con)
                            // Dim da As New OleDbDataAdapter("select program_name,program_id,short_name,case when deptcode =0 then 0 when deptcode is null then 0 else (select departmentcode from departmentmaster where departmentcode=deptcode)end as department from  program_master where program_id=" & grd_media.Items(e.Item.ItemIndex).Cells(2).Text, con)
                            // '---------------------------------------------------------------------
                            // Dim ds As New DataSet
                            // da.Fill(ds, "DepartmentMaster")
                            ds =LibObj.PopulateDataset("select program_name,program_id,short_name,case when deptcode =0 then 0 when deptcode is null then 0 else (select departmentcode from departmentmaster where departmentcode=deptcode)end as department from  program_master where program_id=" + grd_media.Items[e.Item.ItemIndex].Cells[2].Text, "DepartmentMaster", con);
                            txtprogramname.Value = ds.Tables["DepartmentMaster"].Rows[0]["program_name"].ToString();
                            txtshortname.Value = ds.Tables["DepartmentMaster"].Rows[0]["short_name"].ToString();
                            Hd_name.Value = ds.Tables["DepartmentMaster"].Rows[0]["program_name"].ToString();
                            hd_short.Value = ds.Tables["DepartmentMaster"].Rows[0]["short_name"].ToString();
                            Hdaccession.Value = ds.Tables["DepartmentMaster"].Rows[0]["program_id"].ToString();
                            // (ds.Tables(0).Rows(0).Item("deptcode")) 
                            // ---------------
                            // If ((ds.Tables(0).Rows(0).Item("deptcode")) = 0) Then
                            if (ds.Tables["DepartmentMaster"].Rows[0]["department"].ToString()=="0")
                            {
                                this.cmbdept.Visible = false;
                                this.Label3.Visible = false;
                                this.Label11.Visible = false;
                                this.RdDesignation.Checked = true;
                                this.RdCourse.Checked = false;
                                this.cmbdept.SelectedIndex = this.cmbdept.Items.Count - 1;
                            }
                            else
                            {
                                this.Label3.Visible = true;
                                this.RdCourse.Checked = true;
                                this.RdDesignation.Checked = false;
                                // dept()
                                // libobject.populateDDL(cmbdept, libobject.Query("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname"), "departmentname", "departmentcode", HComboSelect.Value, con)
//                                libobject.populateLstBox1(cmbdept, libobject.Query("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname"), "departmentname", "departmentcode", con);
                                this.cmbdept.Visible = true;
                                this.Label11.Visible = true;
                                // cmbdept.SelectedIndex = cmbdept.Items.IndexOf(cmbdept.Items.FindByValue(ds.Tables(0).Rows(0).Item("deptcode")))
                                cmbdept.SelectedValue = cmbdept.Items.FindByValue(ds.Tables[0].Rows[0]["department"].ToString()).Value;
                            }
                            // cmbdept.SelectedIndex = cmbdept.Items.IndexOf(cmbdept.Items.FindByValue(ds.Tables(0).Rows(0).Item("deptcode")))
                            // -----------------------------------
                            ds.Dispose();
                            // da.Dispose()
                            con.Close();
                            con.Dispose();
                            this.SetFocus(txtprogramname);

                            cmdsave.Text = Resources.ValidationResources.bUpdate;
                            cmddelete2.Enabled = true;
                            Chkimport.Visible = true;
                            Chkimport.Checked = false;
                            if (RdCourse.Checked == true)
                            {
                                Chkimport.Text = "Import Course to Designation/Multi Disciplinary ";
                            }
                            else
                            {
                                Chkimport.Text = "Import Designation/Multi Disciplinary to Course";
                            }

                            break;
                        }
                }
                // libobject.SetFocus("txtprogramname", Me)
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // txtprogramname.Focus()
                // hdUnableMsg.Value = "d"
                // libobject.MsgBox1(Resources.ValidationResources.URetriveI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.URetriveI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.URetriveI, this,        dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                ds.Dispose();
                // da.Dispose()
                con.Close();
                con.Dispose();
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            try
            {

                this.SetFocus(txtprogramname);
                hdTop.Value = Resources.ValidationResources.RBTop;
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Disabled = false;
                    this.cmdsave.Enabled = true;
                }
                else if (tmpcondition == "N")
                {
                    this.cmddelete.Disabled = true;
                    this.cmdsave.Enabled = false;
                }
                else
                {
                }
                cmddelete2.Enabled = false;
                Hd_name.Value = "";
                hd_short.Value = "";
                // --------------------------------------------------------------
                this.RdCourse.Checked = true;
                this.cmbdept.Visible = true;
                this.Label11.Visible = true;
                // dept()
                // libobject.populateDDL(cmbdept, libobject.Query("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname"), "departmentname", "departmentcode", HComboSelect.Value, con)
                //libobject.populateLstBox1(cmbdept, libobject.Query("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname"), "departmentname", "departmentcode", con);
                LibObj.populateDDL(cmbdept, "select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", "departmentname", "departmentcode", HComboSelect.Value, con);
                this.RdDesignation.Checked = false;
                this.cmbdept.SelectedIndex = this.cmbdept.Items.Count - 1;
                // -------------------------------------------------------------
                if (tmpcondition == "N")
                {
                    cmdsave.Enabled = false;
                }
                else
                {
                    cmdsave.Enabled = true;
                }
                txtshortname.Disabled = false;
                clear_field();
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public void clear_field()
        {
            txtshortname.Disabled = false;
            this.cmddelete.Disabled = true;
            txtprogramname.Value = "";
            txtshortname.Value = "";
            // Me.cmbdept.SelectedIndex = Me.cmbdept.Items.Count - 1
            cmdsave.Text = Resources.ValidationResources.bSave;
            Chkimport.Visible = false;
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var md_ad = new OleDbDataAdapter();
            var md_ds = new DataSet();
            try
            {

                hdTop.Value = Resources.ValidationResources.RBTop;
                OleDbTransaction tran;
                // Dim con As New OleDb.OleDbConnection(retConStr(Session("LibWiseDBConn")))
                // con.Open()
                tran = con.BeginTransaction();
                var com = new OleDbCommand();
                com.Connection = con;
                com.Transaction = tran;
                int id;
                if (RdCourse.Checked == true)
                {
                    if (this.cmbdept.SelectedValue == Resources.ValidationResources.ComboSelect)
                    {
                        // libobject.MsgBox1(Resources.ValidationResources.IvDep.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.IvDep.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.IvDep, this, dbUtilities.MsgLevel.Warning);
                        this.SetFocus(cmbdept);
                        return;

                    }
                }

                if (cmdsave.Text == Resources.ValidationResources.bSave)
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "Select coalesce(max(program_id),0,max(program_id)) from program_master";
                    id = Convert.ToInt32(com.ExecuteScalar());
                    Hdaccession.Value = (id + 1).ToString();
                }
                com.Parameters.Clear();
                com.CommandType = CommandType.Text;
                com.CommandText = "Select program_id,program_name,short_name,Deptcode from  program_master";
                // Dim md_ad As New OleDb.OleDbDataAdapter
                // Dim md_ds As New DataSet
                md_ad.SelectCommand = com;
                md_ad.Fill(md_ds, "A");
                if (cmdsave.Text == Resources.ValidationResources.bSave)
                {
                    if (md_ds.Tables["A"].Rows.Count > 0)
                    {
                        if (LibObj.checkChildExistance("program_name", "program_master", "program_name=N'" + txtprogramname.Value.Trim().Replace("'", "''") + "'", retConstr("")) == true)
                        {
                            // Hdsave.Value = "9" '--Program name already Exist.
                            // libobject.MsgBox1(Resources.ValidationResources.CDAlExist.ToString, Me)
                            message.PageMesg("Course/Designation Already Exist", this, dbUtilities.MsgLevel.Warning);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Course/Designation Already Exist", Me)
                            return;
                        }
                        if (LibObj.checkChildExistance("short_name", "program_master", "short_name=N'" + txtshortname.Value.Trim().Replace("'", "''") + "'", retConstr("")) == true)
                        {
                            // Hdsave.Value = "19" '--Short name already Exist.
                            // libobject.MsgBox1(Resources.ValidationResources.MShort.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.MShort, this, dbUtilities.MsgLevel.Warning);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MShort.ToString, Me)
                            this.SetFocus(txtshortname);
                            return;
                        }
                    }
                }
                else
                {
                    if (Hd_name.Value != txtprogramname.Value)
                    {
                        if (LibObj.checkChildExistance("program_name", "program_master", "program_name=N'" + txtprogramname.Value.Trim().Replace("'", "''") + "'", retConstr("")) == true)
                        {
                            // Hdsave.Value = "9" '--Program name already Exist.
                            // libobject.MsgBox1(Resources.ValidationResources.CDAlExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.CDAlExist, this, dbUtilities.MsgLevel.Warning);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CDAlExist.ToString, Me)
                            return;
                        }
                    }
                    if (hd_short.Value != txtshortname.Value)
                    {
                        if (LibObj.checkChildExistance("short_name", "program_master", "short_name=N'" + txtshortname.Value.Trim().Replace("'", "''") + "'", retConstr(""   )) == true)
                        {
                            // Hdsave.Value = "19" '--Short name already Exist.
                            // libobject.MsgBox1(Resources.ValidationResources.MShort.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.MShort, this, dbUtilities.MsgLevel.Warning);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MShort.ToString, Me)
                            this.SetFocus(txtshortname);
                            return;
                        }
                    }
                    if (LibObj.checkChildExistance("programe_id", "thesis_accessioning", "programe_id='" + Hdaccession.Value + "'", retConstr("")) == true)
                    {
                        // Hdsave.Value = "short"
                        // libobject.MsgBox1(Resources.ValidationResources.ShortNameExistInChild.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.ShortNameExistInChild, this, dbUtilities.MsgLevel.Warning);
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ShortNameExistInChild.ToString, Me)
                        this.SetFocus(txtprogramname);
                        // txtshortname.Disabled = True
                        return;
                    }
                }
                try
                {
                    if (cmbdept.SelectedValue == "")
                    {
                        message.PageMesg("Deprtment required", this, dbUtilities.MsgLevel.Warning);
                        return;

                    }
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "insert_program_master_1";

                    com.Parameters.Add(new OleDbParameter("@program_id_1", OleDbType.Integer));
                    com.Parameters["@program_id_1"].Value = Hdaccession.Value == "0"? "0": Hdaccession.Value; // Session("Form")

                    com.Parameters.Add(new OleDbParameter("@program_name_2", OleDbType.VarWChar));
                    com.Parameters["@program_name_2"].Value = txtprogramname.Value;  // Session("Form")

                    com.Parameters.Add(new OleDbParameter("@short_name_3", OleDbType.VarWChar));
                    com.Parameters["@short_name_3"].Value = txtshortname.Value;

                    // Session("Form")

                    if (RdCourse.Checked == true)
                    {
                        com.Parameters.Add(new OleDbParameter("@deptcode_4", OleDbType.Integer));
                        com.Parameters["@deptcode_4"].Value = this.cmbdept.SelectedValue;
                    }
                    else
                    {
                        com.Parameters.Add(new OleDbParameter("@deptcode_4", OleDbType.Integer));
                        com.Parameters["@deptcode_4"].Value = 0;
                    }

                    com.Parameters.Add(new OleDbParameter("@userid_5", OleDbType.VarWChar));
                    com.Parameters["@userid_5"].Value = LoggedUser.Logged().User_Id;

                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                    // Hdsave.Value = "1"

                    // libobj.insertLoginFunc(Session("username"), lbltitle.Text, Session("session"), Trim(txtprogramname.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                    // BY kaushal For Import Rercord Between Course -Desgination
                    if (cmdsave.Text != Resources.ValidationResources.bSave)
                    {
                        if (RdCourse.Checked == true & Chkimport.Checked == true)
                        {
                            com.CommandType = CommandType.Text;
                            com.CommandText = "Update program_master  set Deptcode='0' where Program_Id= " + Hdaccession.Value;
                            com.ExecuteNonQuery();
                            com.Parameters.Clear();
                        }
                        else if (RdCourse.Checked == false & Chkimport.Checked == true)
                        {
                            // If (Me.cmbdept.SelectedItem.Text = "---Select---") Then
                            // libobject.MsgBox1("You have checked Import To Course,So Provide a department for Course.", Me)
                            // SetFocus("cmbdept")
                            // Exit Sub
                            // End If
                            com.CommandType = CommandType.Text;
                            com.CommandText = "Update program_master  set Deptcode=" + this.cmbdept.SelectedValue + " where Program_Id= " + Hdaccession.Value;
                            com.ExecuteNonQuery();
                            com.Parameters.Clear();
                        }
                    }


                    if (LoggedUser.Logged().IsAudit=="Y")
                    {
                        if (cmdsave.Text == Resources.ValidationResources.bSave)
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtprogramname.Value, "Submit", retConstr(""));
                        }
                        else
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, this.txtprogramname.Value, "Update", retConstr(""));
                        }
                    }


                    // If cmdsave.Text() = Resources.ValidationResources.bSave.ToString Then
                    // LibObj1.insertLoginFunc(Session("UserName"), lbltitle.Text, Session("session"), Trim(Me.txtprogramname.Value), "Insert", retConStr(Session("LibWiseDBConn")))
                    // Else
                    // LibObj1.insertLoginFunc(Session("UserName"), lbltitle.Text, Session("session"), Trim(Me.txtprogramname.Value), "Update", retConStr(Session("LibWiseDBConn")))
                    // End If

                    tran.Commit();
//                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ViewState["openCond"], null, false)))
  //                  {
    //                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('Hdaccession');", true);
      //              }
        //            else
          //          {
                        // Hdsave.Value = "1"
                        // libobject.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);
            //        }
                    com.Dispose();
                    // con.Close()
                    clear_field();
                    this.cmbdept.SelectedIndex = this.cmbdept.Items.Count - 1;
                    gridfill(con);
                    cmddelete2.Enabled=false;
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
                        message.PageMesg(Resources.ValidationResources.UTprocess, this, dbUtilities.MsgLevel.Failure);
                    }
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UTprocess.ToString, Me)
                    catch (Exception ex1)
                    {
                        // msglabel.Visible = True
                        // msglabel.Text = ex1.Message
                        // Hdsave.Value = "5"
                        // libobject.MsgBox1(Resources.ValidationResources.UTprocess.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.UTprocess, this, dbUtilities.MsgLevel.Failure);
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UTprocess.ToString, Me)
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
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.USaveI.ToString, Me)
                // hdUnableMsg.Value = "s"
                // libobject.MsgBox1(Resources.ValidationResources.USaveI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.USaveI, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                md_ad.Dispose();
                md_ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        protected void cmddelete2_Click(object sender, EventArgs e)
        {

            var delcon = new OleDbConnection(retConstr(""));
            delcon.Open();
            var ds = new DataSet();

            this.SetFocus(txtprogramname);
            try
            {
//                libobject.SetFocus("txtmedianame", this);
                // Me.SetFocus(txtmedianame)
                // Me.SetFocus(txtmedianame)
                // Dim delcon As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
                // delcon.Open()
                // Dim ds As New DataSet
                if (this.RdCourse.Checked == true)
                {
                    Hdaccession.Value = LibObj.populateCommandText("select program_id from Program_Master where program_name=N'" + txtprogramname.Value.Trim().Replace("'", "''") + "' and short_name=N'" + txtshortname.Value.Trim().Replace("'", "''") + "' and deptcode='" + this.cmbdept.SelectedValue + "'", delcon);
                }
                else
                {
                    Hdaccession.Value = LibObj.populateCommandText("select program_id from Program_Master where program_name=N'" + txtprogramname.Value.Trim().Replace("'", "''") + " ' and short_name=N'" + txtshortname.Value.Trim().Replace("'", "''") + " '", delcon);
                }
                hdTop.Value = Resources.ValidationResources.RBTop;
                if (txtprogramname.Value.Trim() == string.Empty)
                {
                    // Hidden3.Value = "2"
                    // libobject.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify, this, dbUtilities.MsgLevel.Warning);
                }
                else if (LibObj.checkChildExistancewc("program_id", "Program_Master", "Program_id='" + Hdaccession.Value + "'", delcon) == false)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                    //                    LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, this); // Currentl displayed record does not exist in database
                }
                else if (LibObj.checkChildExistancewc("programe_id", "thesis_accessioning", "programe_id='" + Hdaccession.Value + "'", delcon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                else if (LibObj.checkChildExistancewc("program_id", "CircUserManagement", "program_id='" + Hdaccession.Value + "'", delcon) == true)
                {
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
//                    libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, this); // current record check in data table
                }
                // ElseIf libobject.checkChildExistancewc("program_id", "staffmaster", "program_id='" & Trim(Hdaccession.Value) & "'", delcon) = True Then
                // libobject.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                else
                {
                    var delcom = new OleDbCommand("delete from program_master where program_id='" + Hdaccession.Value + "'", delcon);
                    delcom.CommandType = CommandType.Text;
                    delcom.ExecuteNonQuery();
                    gridfill(delcon);
//                    fillafterdelete(delcon);
                    // Hidden3.Value = "5"
                    // libobject.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDel, this, dbUtilities.MsgLevel.Success);
                    // Dim temp As String = String.Empty
                    delcom.Parameters.Clear();
                    if (LoggedUser.Logged().IsAudit=="Y")
                    {
                        LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lbltitle.Text, LoggedUser.Logged().Session, txtprogramname.Value, Resources.ValidationResources.bDelete, retConstr(""));
                    }
                    delcom.Dispose();
                }
                cmdreset_Click(sender, e);
                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                ds.Dispose();
                delcon.Close();
                delcon.Dispose();
            }
        }
    }
}