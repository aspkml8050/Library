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
using System.Data.SqlClient;

namespace Library
{
    public partial class frmHolidayManagement : BaseClass
    {
        private libGeneralFunctions libobj = new libGeneralFunctions();
        
        private insertLogin LibObj1 = new insertLogin();
        private insertLogin libinsert = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnGrdId.Value = DataGrid1.ClientID;

                var holycon = new OleDbConnection(retConstr(""));
                holycon.Open();
                var holyds = new DataSet();
                var dsSess = new DataSet();
                var com = new OleDbCommand();
                try
                {
                    this.msglabel.Visible = false;
                    // cmdReturn.CausesValidation = False
                    txtholidayid.Visible = false;
                    // -------------------------------------
                    cmddelete1.CausesValidation = false;
                    // ''cmddelete.Disabled = True
                    // -------------------------
                    // Put user code to initialize the page here
                    cmdsave.Attributes.Add("ServerClick", "return cmdsave_ServerClick();");
                    this.cmdreset1.CausesValidation = false;

                    // Dim UControl As Control = LoadControl("mainControl.ascx")
                    // UControl.ID = "MainControl1"
                    // Me.PanelTopCont.Controls.Add(UControl)

                    if (!Page.IsPostBack)
                    {
                        SetFocus(this.txtHolidayDate);
                        Label7.Text = Request.QueryString["title"];
                        tmpcondition = Request.QueryString["condition"];
                        //hdCulture.Value = Request.Cookies["UserCulture"].Value;
                        if (tmpcondition == "Y")
                        {
                            this.cmdsave.Disabled = false;
                        }
                        else
                        {
                            this.cmdsave.Disabled = true;
                        }
                        cmddelete1.Visible = true;
                        // Dim holycom As New OleDb.OleDbCommand
                        // holycom.Connection = holycon
                        // Grid Code
                        com.Connection = holycon;
                        string SessQry = "select AcademicSession,AcademicSession AcademicSession2 from AcedemicSessionInformation order by AcademicSession";
                        var Ada = new OleDbDataAdapter(SessQry, holycon);
                        // Dim Ada As New OleDb.OleDbDataAdapter
                        Ada.Fill(dsSess);
                        if (ddlSess != null)
                        {
                            ddlSess.DataSource = dsSess.Tables[0];
                            ddlSess.DataValueField = "AcademicSession";
                            ddlSess.DataTextField = "AcademicSession2";
                            ddlSess.DataBind();

                        }
                        fillgrid(holycon, holyds);
                        var SweekDays = new DataTable();
                        SweekDays.Columns.Add("DNo");
                        SweekDays.Columns["DNo"].DataType = typeof(int);
                        SweekDays.Columns.Add("Day");
                        SweekDays.Columns["Day"].DataType = typeof(string);
                        SweekDays.AcceptChanges();
                        var Sdr = SweekDays.NewRow();
                        Sdr[0] = 0;
                        Sdr[1] = "Sunday";
                        SweekDays.Rows.Add(Sdr);
                        Sdr = SweekDays.NewRow();
                        Sdr[0] = 1;
                        Sdr[1] = "Monday";
                        SweekDays.Rows.Add(Sdr);
                        Sdr = SweekDays.NewRow();
                        Sdr[0] = 2;
                        Sdr[1] = "Tuesday";
                        SweekDays.Rows.Add(Sdr);
                        Sdr = SweekDays.NewRow();
                        Sdr[0] = 3;
                        Sdr[1] = "Wednesday";
                        SweekDays.Rows.Add(Sdr);
                        Sdr = SweekDays.NewRow();
                        Sdr[0] = 4;
                        Sdr[1] = "Thursday";
                        SweekDays.Rows.Add(Sdr);
                        Sdr = SweekDays.NewRow();
                        Sdr[0] = 5;
                        Sdr[1] = "Friday";
                        SweekDays.Rows.Add(Sdr);
                        Sdr = SweekDays.NewRow();
                        Sdr[0] = 6;
                        Sdr[1] = "Saturday";
                        SweekDays.Rows.Add(Sdr);

                        if (ddlWeekds != null)
                        {
                            ddlWeekds.DataSource = SweekDays;
                            ddlWeekds.DataValueField = "DNo";
                            ddlWeekds.DataTextField = "Day";
                            ddlWeekds.DataBind();
                        }
                      
                    }
                }
                catch (Exception ex)
                {
                    msglabel.Visible = true;
                    msglabel.Text = ex.Message;
                }
                finally
                {
                    holyds.Dispose();
                    if (holycon.State == ConnectionState.Open)
                    {
                        holycon.Close();
                    }
                    holycon.Dispose();
                }
            }
            catch (Exception ex1)
            {
                msglabel.Visible = true;
                msglabel.Text = ex1.Message;
            }

        }
        public void fillgrid(OleDbConnection con, DataSet ds)
        {
            // Dim da As New OleDb.OleDbDataAdapter("SELECT h_date, description FROM circHolidays order by h_date desc", con)
            string Qr = "SELECT distinct circHolidays.h_date, circHolidays.description FROM  circHolidays, librarysetupinformation inner join AcedemicSessionInformation on librarysetupinformation.CurrentAcademicSession = AcedemicSessionInformation.AcademicSession where circHolidays.h_date between AcedemicSessionInformation.StartDate and AcedemicSessionInformation.EndDate order by circHolidays.h_date desc";
            var da = new OleDbDataAdapter(Qr, con);
            da.Fill(ds, "circHolidays");
            DataGrid1.DataSource = ds.Tables["circHolidays"].DefaultView;
            DataGrid1.DataBind();
            hdnGrdId.Value = DataGrid1.ClientID;

            da.Dispose();
        }

        protected void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            string sqr;

            if (chkShow.Checked)
            {
                sqr = "select Splholidayid,datefrom,dateupto,Description,Filename,Document,'' strdocument from circsplholidays order by datefrom desc";
                var gcls = new GlobClassTr();
                var dt = new DataTable();
                gcls.TrOpen();
                dt = gcls.DataT(sqr);
                gcls.TrClose();
                for (int inX = 0, loopTo = dt.Rows.Count - 1; inX <= loopTo; inX++)
                {
                    if (!dt.Rows[inX].IsNull("document"))
                    {
                        dt.Rows[inX]["strdocument"] = Convert.ToByte(dt.Rows[inX]["document"]);
                    }
                }
                grdSplHs.DataSource = dt;
                grdSplHs.DataBind();
            }
            else
            {
                grdSplHs.DataSource = (object)null;
                grdSplHs.DataBind();
            }
        }
        protected void btnGSave_Click(object sender, EventArgs e)
        {
            // Dim con As New OleDb.OleDbConnection(retConstr(Session("LibWiseDBConn")))
            string ConString = retConstr("");
            string[] ConString2 = ConString.Split(';');
            var con = new SqlConnection("Integrated Security=No;" + ConString2[2] + ";" + ConString2[3] + ";" + ConString2[4] + ";" + ConString2[5] + ";" + ConString2[6]);

            try
            {
                con.Open();
                var Com = new SqlCommand();
                Com.Connection = con;
                Com.CommandType = CommandType.StoredProcedure;
                Com.CommandText = "holidayg";
                var dt2 = new DataTable();
                if (Session["Sdt"] != null)
                {
                    dt2 = (DataTable)Session["Sdt"];
                }
                else
                {
                    con.Close();
                   
                    message.PageMesg("Data Not Available", this, dbUtilities.MsgLevel.Failure);
                    return;
                }

                // dt2.Columns.Remove("dt2")
                var ParaM = Com.Parameters.AddWithValue("@Dates", dt2);
                // ParaM.Value
                ParaM.SqlDbType = SqlDbType.Structured;

                string dd;
                dd = cmbdepartmentcode.SelectedItem.ToString();

                string gg;
                gg = Session["user_id"].ToString();
                var ParaM2 = Com.Parameters.AddWithValue("@Sched", dd);
                var ParaM3 = Com.Parameters.AddWithValue("@uid", gg);
                // ParaM.OleDbType = OleDbType.
                // Dim pp As SqlParameter = Com.Parameters.AddWithValue("@X", Session("X"))
                Com.ExecuteNonQuery();
                Session.Remove("Sdt");
                libobj.MsgBox1("Done", this);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                // libobj.MsgBox1(ex.Message, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, ex.Message, Me)
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Warning);
                return;
            }
        }
        protected void btnDNPdf_Click(object sender, EventArgs e)
        {
            // data:image/pdf;base64,

            // contenttype = "application/pdf";
            string fn = txSplPdf.Text;


            if (hdSplHoliDs.Value == "")
            {
                return;
            }

            if (string.IsNullOrEmpty(fn))
            {
                return;
            }

            byte[] bi = Convert.FromBase64String(hdSplHoliDs.Value);

            var gClas = new GlobClassTr();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fn);
            Response.BinaryWrite(bi);
            Response.Flush();
            Response.End();




        }

        protected void btnSplSav_Click(object sender, EventArgs e)
        {
            var gClas = new GlobClassTr();
            string sQer;
            if (txSDf.Text == "" | txSDu.Text == "" | txSDesc.Text == "")
            {
                
                message.PageMesg("Start date,upto date and description are required", this, dbUtilities.MsgLevel.Warning);
                return;
            }

            System.DateTime df = Convert.ToDateTime(txSDf.Text);
            System.DateTime du = Convert.ToDateTime(txSDu.Text);
            if (df > du)
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, "Date error", Me)
                message.PageMesg("Date error", this, dbUtilities.MsgLevel.Failure);
                return;
            }
            try
            {

                gClas.TrOpen();

                var dttot = new DataTable();
                var lsdates = new List<System.DateTime>();
                if (hdSplHId.Value == "")
                {
                    sQer = "select * from circSplHolidays "; // where datefrom >='" + txSDf.Text + "' and "
                                                             // sQer += " dateupto<='" + txSDu.Text + "' "
                    dttot = gClas.DataT(sQer);
                }
                else
                {
                    sQer = "select * from circSplHolidays where Splholidayid <>" + hdSplHId.Value;

                    dttot = gClas.DataT(sQer);
                }
                // collect all existing dates then check new inputs - no overlapping allowed
                for (int inX1 = 0, loopTo = dttot.Rows.Count - 1; inX1 <= loopTo; inX1++)
                {
                    string id = Convert.ToString(dttot.Rows[inX1]["Splholidayid"]);
                    string sdf1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dttot.Rows[inX1]["datefrom"]));
                    string sdu1 = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dttot.Rows[inX1]["dateupto"]));

                    System.DateTime df1 = Convert.ToDateTime(dttot.Rows[inX1]["datefrom"]);
                    System.DateTime du1 = Convert.ToDateTime(dttot.Rows[inX1]["dateupto"]);
                    while (df1 <= du1)
                    {
                        lsdates.Add(df1);
                        df1 = df1.AddDays(1d);
                    }
                }
                // While df <= du
                // lsdates.Add(df)
                // df = df.AddDays(1)
                // End While



                df = Convert.ToDateTime(txSDf.Text);
                while (df <= du)
                {
                    for (int inX = 0, loopTo1 = lsdates.Count - 1; inX <= loopTo1; inX++)
                    {
                        if (df == lsdates[inX])
                        {
                            gClas.TrClose();
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, "Overlapping dates found", Me)
                            message.PageMesg("Overlapping dates found", this, dbUtilities.MsgLevel.Failure);
                            return;

                        }
                    }
                    df = df.AddDays(1d);
                }
                df = Convert.ToDateTime(txSDf.Text);

                sQer = "delete from circHolidays where h_date>='" + txSDf.Text + "' and h_date<='" + txSDu.Text + "' ";
                gClas.IUD(sQer);
                if (hdSplHId.Value == "")
                {
                    sQer = "select isnull(max(Splholidayid),0)+1 from circsplholidays";
                    int id = Convert.ToInt32(gClas.ExScaler(sQer));
                    if (hdSplHoliDs.Value != "")
                    {
                        var p = new SqlParameter("pdfd", SqlDbType.VarBinary);
                        byte[] bid = Convert.FromBase64String(hdSplHoliDs.Value);
                        p.Value = bid;
                        sQer = "insert into circsplholidays ( Splholidayid,datefrom,dateupto,Description,Filename,Document ) values (";
                        sQer += id.ToString() + ",'" + txSDf.Text + "','" + txSDu.Text + "',N'" + txSDesc.Text.Trim().Replace("'", "''") + "',";
                        sQer += "'" + txSplPdf.Text + "',@pdfd )";
                        var lis = new List<SqlParameter>();
                        lis.Add(p);
                        gClas.IUD(sQer, lis.ToArray());
                    }
                    else
                    {
                        sQer = "insert into circsplholidays ( Splholidayid,datefrom,dateupto,Description,Filename,Document ) values (";
                        sQer += id.ToString() + ",'" + txSDf.Text + "','" + txSDu.Text + "',N'" + txSDesc.Text.Trim().Replace("'", "''") + "',";
                        sQer += "'" + txSplPdf.Text + "',null )";
                        gClas.IUD(sQer);
                    }
                }
                if (hdSplHId.Value != "")
                {
                    if (hdSplHoliDs.Value != "")
                    {
                        var p = new SqlParameter("pdfd", SqlDbType.VarBinary);
                        byte[] bid = Convert.FromBase64String(hdSplHoliDs.Value);
                        p.Value = bid;
                        sQer = "update circsplholidays set datefrom='" + txSDf.Text + "' ,dateupto='" + txSDu.Text + "',Description=N'" + txSDesc.Text.Trim().Replace("'", "''") + "'";
                        sQer += "  ,Filename='" + txSplPdf.Text + "',Document=@pdfd ";
                        sQer += " where Splholidayid=" + hdSplHId.Value;

                        var lis = new List<SqlParameter>();
                        lis.Add(p);
                        gClas.IUD(sQer, lis.ToArray());
                    }
                    else
                    {
                        sQer = "update circsplholidays set datefrom='" + txSDf.Text + "' ,dateupto='" + txSDu.Text + "',Description=N'" + txSDesc.Text.Trim().Replace("'", "''") + "'";
                        sQer += "  ,Filename='" + txSplPdf.Text + "',Document=null ";
                        sQer += " where Splholidayid=" + hdSplHId.Value;
                        gClas.IUD(sQer);
                    }

                }
                sQer = "select isnull(max(holidayid),0)+1 from circHolidays ";
                int idch = Convert.ToInt32(gClas.ExScaler(sQer));
                while (df <= du)
                {
                    string sf = string.Format("{0:dd-MMM-yyyy}", df);

                    sQer = "insert into circHolidays (holidayid,h_date,description,scheduled,userid  ) values (";
                    sQer += idch.ToString() + ",'" + sf + "','special','No','Admin' ) ";
                    gClas.IUD(sQer);
                    df = df.AddDays(1d);
                    idch += 1;
                }


                gClas.TrClose();
                cmdreset1_Click(sender, e);
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Success, please reload page", Me)
                message.PageMesg("Success, please reload page", this, dbUtilities.MsgLevel.Success);
            }
            catch (Exception ex)
            {
                gClas.TrRollBack();
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, ex.Message, Me)
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdsave1_Click(object sender, EventArgs e)
        {
            var holycon = new OleDbConnection(retConstr(""));
            holycon.Open();
            var holycmd = new OleDbCommand();
            holycmd.Connection = holycon;
            var holyds = new DataSet();
            try
            {
                SetFocus(txtHolidayDate);
                if (cmdsave1.Text == Resources.ValidationResources.bSave)
                {
                   
                    System.DateTime hDate;
                    hDate = Convert.ToDateTime(txtHolidayDate.Text);
                    holycmd.Parameters.Clear();
                    holycmd.CommandType = CommandType.Text;
                    holycmd.CommandText = "select description from circHolidays where h_date='" + hDate.ToString(hrDate.Value) + "'";
                    string tmpr;
                    tmpr = Convert.ToString(holycmd.ExecuteScalar());
                    if (!((tmpr) == string.Empty))
                    {
                        
                        message.PageMesg(Resources.ValidationResources.HoliDayExist, this,dbUtilities.MsgLevel.Warning);
                       
                        return;
                    }
                }
                if (!(Hidden2.Value == "1"))
                {
                   
                    if (cmdsave.Value == Resources.ValidationResources.bSave)
                    {
                       
                        holycmd.Parameters.Clear();
                        holycmd.CommandType = CommandType.Text;
                        holycmd.CommandText = "select coalesce(max(holidayid),0,max(holidayid)) from circholidays";
                        string tmpstr;
                        tmpstr = Convert.ToString(holycmd.ExecuteScalar());
                        var z = Convert.ToInt32(tmpstr);
                        txtholidayid.Value = z == 0 ? "1" : (tmpstr) + 1;
                    }
                   
                    holycmd.Parameters.Clear();
                    holycmd.CommandType = CommandType.StoredProcedure;
                    holycmd.CommandText = "insert_circHolidays_1";
                    System.DateTime hDate1;
                    hDate1 = Convert.ToDateTime(txtHolidayDate.Text);
                    holycmd.Parameters.Add(new OleDbParameter("@holidayid_1", OleDbType.Integer));
                    holycmd.Parameters["@holidayid_1"].Value = (txtholidayid.Value);
                    holycmd.Parameters.Add(new OleDbParameter("@h_date_2", OleDbType.Date));
                    holycmd.Parameters["@h_date_2"].Value = hDate1; // .ToString("MM/dd/yyyy")
                                                                    // 'staffmastercom.Parameters("@doj_10").Value = IIf(Trim(txtdoj.Value) = String.Empty, DBNull.Value, Trim(txtdoj.Value))
                    holycmd.Parameters.Add(new OleDbParameter("@description_3", OleDbType.VarWChar));
                    holycmd.Parameters["@description_3"].Value = (txtDescription.Value);
                    holycmd.Parameters.Add(new OleDbParameter("@scheduled_4", OleDbType.VarWChar));
                    holycmd.Parameters["@scheduled_4"].Value = (cmbdepartmentcode.SelectedItem.Value);
                    holycmd.Parameters.Add(new OleDbParameter("@userid_5", OleDbType.VarWChar));
                    holycmd.Parameters["@userid_5"].Value = Session["user_id"];
                    holycmd.ExecuteNonQuery();
                   
                    message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);
                   
                    holycmd.Parameters.Clear();

                    if (LoggedUser.Logged().IsAudit == "Y")
                    {
                        if (cmdsave.Value == Resources.ValidationResources.bSave.ToString())
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, Label7.Text, LoggedUser.Logged().Session, this.txtHolidayDate.Text.Trim(), Resources.ValidationResources.Insert,retConstr(""));
                        }
                        else
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, Label7.Text, LoggedUser.Logged().Session, this.txtHolidayDate.Text.Trim(), Resources.ValidationResources.bUpdate,retConstr(""));
                        }
                    }


                    
                    cmdsave1.Text = Resources.ValidationResources.bSave;
                    clear_fields();
                }
            }
            catch (Exception ex)
            {
                msglabel.Visible = true;
                msglabel.Text = ex.Message;
            }
            finally
            {
                holyds.Dispose();
                holycmd.Dispose();
                if (holycon.State == ConnectionState.Open)
                {
                    holycon.Close();
                }
                holycon.Dispose();
            }
        }
        private void clear_fields()
        {
            this.DataGrid1.SelectedIndex = -1;
            txtHolidayDate.Text = string.Empty;
            txtDescription.Value = string.Empty;
            cmbdepartmentcode.SelectedIndex = 0;
        }
        protected void lnkDf_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grd = (GridViewRow)lnk.NamingContainer;
            HiddenField hid = (HiddenField)grd.FindControl("hdid");
            HiddenField hdoc = (HiddenField)grd.FindControl("hdstrdoc");
            txSDf.Text = lnk.Text;
            hdSplHId.Value = hid.Value;
            hdSplHoliDs.Value = hdoc.Value;
            Label labdu = (Label)grd.FindControl("labdu");
            txSDu.Text = labdu.Text;
            Label labdesc = (Label)grd.FindControl("labdesc");
            txSDesc.Text = labdesc.Text;
            Label labpdf = (Label)grd.FindControl("labpdf");
            txSplPdf.Text = labpdf.Text;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            var gcls = new GlobClassTr();
            try
            {
                gcls.TrOpen();
                string qer;
                Button b = (Button)sender;
                GridViewRow grd = (GridViewRow)b.NamingContainer;
                HiddenField hid = (HiddenField)grd.FindControl("hdid");
                LinkButton labdf = (LinkButton)grd.FindControl("lnkDf");
                Label labdu = (Label)grd.FindControl("labdu");
                qer = "delete from circHolidays where h_date>='" + labdf.Text + "' and h_date<='" + labdu.Text + "' ";
                gcls.IUD(qer);
                qer = "delete from circsplholidays where Splholidayid=" + hid.Value;
                gcls.IUD(qer);


                gcls.TrClose();
                cmdreset1_Click(sender, e);
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Success, please reload page", Me)
                message.PageMesg("Success, please reload page", this, dbUtilities.MsgLevel.Success);
            }
            catch (Exception ex)
            {
                gcls.TrRollBack();
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, ex.Message, Me)
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void btnGene_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            try
            {

                var ds2 = new DataSet();
                string Qry2 = "select StartDate,Enddate from AcedemicSessionInformation where AcademicSession='" + ddlSess.SelectedItem.ToString() + "'";
                var da2 = new OleDbDataAdapter(Qry2, con);
                da2.Fill(ds2, "Acad");
                string Qry3 = Convert.ToString("select h_date,description from circholidays where h_date between '" + ds2.Tables[0].Rows[0][0] + "' and '" + ds2.Tables[0].Rows[0][1] + "' order by h_date");
                var da3 = new OleDbDataAdapter(Qry3, con);
                da3.Fill(ds2, "Holydays");

                System.DateTime Sdt = Convert.ToDateTime(ds2.Tables["Acad"].Rows[0][0]);
                ds2.Tables.Add("Hday");
                ds2.Tables["Hday"].Columns.Add("h_date");
                ds2.Tables["Hday"].Columns["h_date"].DataType = typeof(DateTime);
                ds2.Tables["Hday"].Columns.Add("Dt2"); // unused
                ds2.Tables["Hday"].Columns["Dt2"].DataType = typeof(DateTime);
                ds2.Tables["Hday"].Columns.Add("Description");
                ds2.Tables["Hday"].Columns["Description"].DataType = typeof(string);
                DataRow dr2; // = ds2.Tables("Hday").NewRow
                Int16 fnd = 0;
                while (Sdt < Convert.ToDateTime(ds2.Tables[0].Rows[0][1]))
                {
                    // libobj.MsgBox1(Sdt.DayOfWeek, Me)
                    if (Sdt.DayOfWeek.ToString() == ddlWeekds.SelectedValue)
                    {
                        for (Int16 loopto = 0; loopto <= ds2.Tables["holydays"].Rows.Count - 1;  loopto++)
                        {
                            if (Sdt == Convert.ToDateTime(ds2.Tables["holydays"].Rows[loopto][0]))
                            {
                                fnd = 1;
                                break;
                            }
                        }
                        if (fnd == 0)
                        {
                            dr2 = ds2.Tables["holydays"].NewRow();
                            dr2[0] = Sdt;
                            dr2[1] = ddlWeekds.SelectedItem.ToString();
                            ds2.Tables["holydays"].Rows.Add(dr2);
                        }
                        fnd = 0;
                    }

                    Sdt = Sdt.AddDays(1d);
                }
                Qry2 = Qry2;
                con.Close();
                var dt2 = ds2.Tables["holydays"].Clone();
                for (int ixd1 = 0, loopTo1 = ds2.Tables["holydays"].Rows.Count - 1; ixd1 <= loopTo1; ixd1++)
                {
                    bool aded = false;
                    for (int ixd2 = 0, loopTo2 = dt2.Rows.Count - 1; ixd2 <= loopTo2; ixd2++)
                    {
                        if (Convert.ToBoolean((dt2.Rows[ixd2][0], ds2.Tables["holydays"].Rows[ixd1][0], false)))
                        {
                            aded = true;
                            break;
                        }
                    }
                    if (!aded)
                    {
                        dt2.ImportRow(ds2.Tables["holydays"].Rows[ixd1]);
                    }
                }
                DataGrid1.DataSource = dt2; // ds2.Tables("holydays")
                DataGrid1.DataBind();
                hdnGrdId.Value = DataGrid1.ClientID;

                // DataGrid1.Columns("h_date").SortExpression = Nothing
                btnGSave.Enabled = true;
                Session["Sdt"] = ds2.Tables["holydays"];
            }
            catch (Exception ex)
            {
                con.Close();
                // libobj.MsgBox1(ex.Message.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, ex.Message.ToString, Me)
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
                return;

            }

        }
        protected void ddlSess_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var holycon = new OleDbConnection(retConstr(""));
            try
            {

                holycon.Open();
                var holyds = new DataSet();
                var com = new OleDbCommand();
                com.Connection = holycon;
                string SessQry = "select distinct a.h_date,a.description from circHolidays a,AcedemicSessionInformation b where h_date between b.StartDate and b.Enddate and AcademicSession='" + ddlSess.SelectedItem.ToString() + "' order by h_date";

                var Ada = new OleDbDataAdapter(SessQry, holycon);
                // Dim Ada As New OleDb.OleDbDataAdapter
                Ada.Fill(holyds);
                holycon.Close();
                DataGrid1.DataSource = holyds.Tables[0];
                DataGrid1.DataBind();
                hdnGrdId.Value = DataGrid1.ClientID;
            }

            catch (Exception ex)
            {
                holycon.Close();
                // libobj.MsgBox(ex.Message, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, ex.Message.ToString, Me)
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }



        }

        protected void cmdreset1_Click(object sender, EventArgs e)
        {
            var holycon = new OleDbConnection(retConstr(""));
            holycon.Open();
            var holyds = new DataSet();
            try
            {
                if (tmpcondition == "Y")
                {
                    this.cmdsave.Disabled = false;
                }
                else
                {
                    this.cmdsave.Disabled = true;
                }
                clear_fields();
                cmdsave1.Text = Resources.ValidationResources.bSave;
                cmddelete1.Visible = true;
                btnGSave.Enabled = false;
                fillgrid(holycon, holyds);

                

                var holyda = new OleDbDataAdapter("SELECT h_date, description FROM circHolidays order by h_date desc", holycon);
                holyda.Fill(holyds, "circHolidays");
                DataGrid1.DataSource = holyds.Tables[0].DefaultView;
                DataGrid1.DataBind();
                hdnGrdId.Value = DataGrid1.ClientID;

                holyds.Dispose();

                SetFocus(txtHolidayDate);
                grdSplHs.DataSource = (object)null;
                grdSplHs.DataBind();
                hdSplHoliDs.Value = "";
                hdSplHId.Value = "";
                txSDesc.Text = "";
                txSDf.Text = "";
                txSDu.Text = "";
                txSplPdf.Text = "";
                chkShow.Checked = false;
            }

            catch (Exception ex)
            {
                msglabel.Visible = true;
                msglabel.Text = ex.Message;
            }
            finally
            {
                holyds.Dispose();
                if (holycon.State == ConnectionState.Open)
                {
                    holycon.Close();
                }
                holycon.Dispose();
            }
        }

        protected void cmddelete1_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            // Dim holyds As New DataSet
            OleDbCommand cmd = null;
            OleDbTransaction tran = null;
            try
            {
                // Bipin 09-08-07
                cmd = new OleDbCommand("Select holidayid from circHolidays where description=N'" + (this.txtDescription.Value) + "' and h_date='" + (this.txtHolidayDate.Text).Trim() + "'", con);
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtholydayid.Value = dr.GetValue(0).ToString();
                }
                else
                {
                    txtholydayid.Value = string.Empty;
                }
                // Bipin 9-08-07
                if ((this.txtDescription.Value) == string.Empty)
                {
                    // libobj.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify, this,dbUtilities.MsgLevel.Warning);
                    SetFocus(this.txtHolidayDate);
                }
                else if (libobj.checkChildExistancewc("h_date", "circHolidays", "holidayid='" + (txtholydayid.Value) + "'", con) == false)
                {
                  
                    message.PageMesg(Resources.ValidationResources.rDelNotExist, this, dbUtilities.MsgLevel.Warning);
                    SetFocus(this.txtHolidayDate);
                }
                else if (libobj.checkChildExistancewc("issuedate", "CircIssueTransaction", "duedate='" + (txtHolidayDate.Text) + "'", con) == true)
                {
                    // libobj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'current record check in data table
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                    SetFocus(this.txtHolidayDate);
                }
                else if (libobj.checkChildExistancewc("issuedate", "circIssueTransactionNDB", "issuedate='" + (txtHolidayDate.Text) + "'", con) == true)
                {
                   
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                    SetFocus(this.txtHolidayDate);
                }
                else
                {
                    tran = con.BeginTransaction();
                    cmd.Parameters.Clear();
                    cmd = new OleDbCommand("delete from circHolidays where holidayid='" + (txtholydayid.Value).Trim() + "'", con);
                    // Dim delcom As New OleDbCommand("delete from circHolidays where holidayid='" & Trim(txtholidaycode.Value) & "'", con)
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        // fillgrid(con, holyds)
                        // Dim temp As String = String.Empty
                        // fillafterdelete(con)
                        cmd.Parameters.Clear();
                        if (LoggedUser.Logged().IsAudit == "Y")
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, Label7.Text, LoggedUser.Logged().Session,this.txtHolidayDate.Text.Trim(), Resources.ValidationResources.bDelete,retConstr(""));
                        }


                        tran.Commit();
                        
                        message.PageMesg(Resources.ValidationResources.rDel, this, dbUtilities.MsgLevel.Success);
                         //Call cmdreset1_Click(sender, e)
                        //SetFocus(txtHolidayDate);
                        hdTop.Value = Resources.ValidationResources.RBTop;
                        Hidden3.Value = "5";
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();
                            msglabel.Visible = true;
                            msglabel.Text = ex1.Message;
                            Hidden1.Value = 22.ToString();
                        }
                        catch (Exception ex2)
                        {
                            msglabel.Visible = true;
                            msglabel.Text = ex2.Message;
                            Hidden1.Value = 22.ToString();
                        }
                    }
                }
                cmdreset1_Click(sender, e);
            }
            

            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                this.msglabel.Text = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                cmd.Dispose();
                con.Dispose();
            }
            clear_fields();
            // ---------------------------------
        }
        private void fillafterdelete(OleDbConnection con)
        {
            string sel;
            // sel = "SELECT h_date, description FROM circHolidays where year(h_date)=" & Now.Year
            sel = "SELECT distinct circHolidays.h_date, circHolidays.description FROM  circHolidays, librarysetupinformation inner join AcedemicSessionInformation on librarysetupinformation.CurrentAcademicSession = AcedemicSessionInformation.AcademicSession where circHolidays.h_date between AcedemicSessionInformation.StartDate and AcedemicSessionInformation.EndDate";
            libobj.populateAfterDeletion(DataGrid1, sel, con);
            hdnGrdId.Value = DataGrid1.ClientID;

        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                var holycon = new OleDbConnection(retConstr(""));
                holycon.Open();
                var holyds = new DataSet();
                try
                {
                    switch (((LinkButton)e.CommandSource).CommandName)
                    {
                        case "Select":
                            {
                                // Dim holyda As New OleDb.OleDbDataAdapter("Select * from circHolidays where h_date='" & DataGrid1.Items(e.Item.ItemIndex()).Cells(1).Text() & "'", holycon)
                                // holyda.Fill(holyds, "circHolidays")
                                holyds = libobj.PopulateDataset("Select * from circHolidays where h_date='" + DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text + "'", "circHolidays", holycon);
                                txtholidayid.Value = holyds.Tables["circHolidays"].Rows[0][0].ToString() == string.Empty ? "" : holyds.Tables["circHolidays"].Rows[0][0].ToString();
                                DateTime t1;
                                t1 = Convert.ToDateTime(holyds.Tables["circHolidays"].Rows[0][1].ToString() == string.Empty ? "" : holyds.Tables["circHolidays"].Rows[0][1].ToString());
                                txtHolidayDate.Text = t1.ToString(hrDate.Value);
                                txtDescription.Value = holyds.Tables["circHolidays"].Rows[0][2].ToString() == string.Empty ? "" : holyds.Tables["circHolidays"].Rows[0][2].ToString();
                                cmbdepartmentcode.SelectedIndex = cmbdepartmentcode.Items.IndexOf(cmbdepartmentcode.Items.FindByText(holyds.Tables["circHolidays"].Rows[0]["scheduled"].ToString()));
                                cmdsave1.Text = Resources.ValidationResources.bUpdate;
                                cmddelete.Visible = false;

                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    msglabel.Visible = true;
                    msglabel.Text = ex.Message;
                }
                finally
                {
                    holyds.Dispose();
                    if (holycon.State == ConnectionState.Open)
                    {
                        holycon.Close();
                    }
                    holycon.Dispose();
                }
            }
            catch (Exception ex1)
            {
                msglabel.Visible = true;
                msglabel.Text = ex1.Message;
            }
        }
       


    }
}