using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Library.App_Code.MultipleFramworks;
using Library.App_Code.CSharp;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.Threading;

namespace Library
{
    public partial class ClassMaster : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private DataTable dt = new DataTable();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // cmdReturn.CausesValidation = False
                txtclassname.ReadOnly = false;
                msglabel.Visible = false;
                cmdsubmit.Attributes.Add("ServerClick", "return cmdsave_Click();");
                cmddelete.Attributes.Add("ServerClick", "DoConfirmation();");
                if (!Page.IsPostBack)
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    // txtclassname.Focus()
                    this.SetFocus(txtclassname);
                    hdTop.Value = Resources.ValidationResources.RBTop;
                    cmdreset.CausesValidation = false;
                    this.cmddelete.CausesValidation = false;
                    Session["NFormDW"] = null;
                    lblTitle.Text = Request.QueryString["title"];
                    ViewState["openCond"] = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        this.cmddelete.Visible = false;
                        this.cmdsubmit.Visible = false;
                    }
                    else if (tmpcondition == "N")
                    {
                        this.cmddelete.Visible = true;
                        this.cmdsubmit.Visible = true;
                    }
                    else
                    {
                        lblTitle.Text = Resources.ValidationResources.Title_membergroup;
                        this.cmdsubmit.Visible = false;
                        this.cmddelete.Visible = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    cmddelete.Visible = true;
                    lblcurr.Text = lblcurr.Text + "(" + LibObj.getCurrency(retConstr("")) + ")";
                    this.lblValueLimit.Text = lblValueLimit.Text + "(" + LibObj.getCurrency(retConstr("")) + ")";
                    FillGrid();
                    loadingStatus();
                    
                    grdDetail.Visible = false;
                }
            }
           
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
               
            }
        }

        public void FillGrid()
        {
            var classmastercon = new OleDbConnection(retConstr(""));
            classmastercon.Open();
            var classmasterds = new DataSet();
            //classmasterds = LibObj.PopulateDataset("select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,totalissueddays_jour,noofjournaltobeissued,fineperday_jour,reservedays_jour,Status,canrequest,ValueLimit,days_1phase,amt_1phase,days_2phase,amt_2phase,days_1phasej,amt_1phasej,days_2phasej,amt_2phasej from CircClassMaster order by classname", "DepartmentMaster", classmastercon);
            DataGrid1.DataSource = classmasterds.Tables[0].DefaultView;
            DataGrid1.DataBind();
            classmastercon.Close();
            classmastercon.Dispose();
            classmasterds.Dispose();
            hdnGrdId.Value = DataGrid1.ClientID;
        }

        public void loadingStatus()
        {
            var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
            CategoryLoadingStatusCon.Open();
            var CategoryLoadingStatusds = new DataSet();
            //CategoryLoadingStatusds = LibObj.PopulateDataset("select id,Category_LoadingStatus from CategoryLoadingStatus where id <> 0  order by Category_LoadingStatus", "CategoryLoadingStatus", CategoryLoadingStatusCon);
            if (CategoryLoadingStatusds.Tables["CategoryLoadingStatus"].Rows.Count > 0)
            {
                grdDetail.DataSource = CategoryLoadingStatusds.Tables["CategoryLoadingStatus"].DefaultView;
                grdDetail.DataBind();
            }
            else
            {
                grdDetail.DataSource = dt;
                grdDetail.DataBind();
            }
            CategoryLoadingStatusds.Dispose();
            CategoryLoadingStatusCon.Close();
            CategoryLoadingStatusCon.Dispose();
            hdnGrdId.Value = DataGrid1.ClientID;
        }

        protected void cmdsubmit_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt32(txtmaxissueday.Value) == 0 & txtmaxissueday.Value.Trim() != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.MsgIssuedDaysLimitb, this, dbUtilities.MsgLevel.Failure);
                   
                    this.SetFocus(txtmaxissueday);
                    return;
                }


                if (Convert.ToInt32(txtmaxissuebook.Value) == 0 & txtmaxissuebook.Value.Trim() != string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.MsgIssuedItemLimitb, this, dbUtilities.MsgLevel.Failure);
                    
                    this.SetFocus(txtmaxissuebook);
                    return;
                }


                if (Convert.ToInt32(txtDays1.Value) == 0 & txtDays1.Value.Trim() != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIDaysLimitb, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtDays1);
                    return;
                }


                if (Convert.ToInt32(txtfineperday1.Value) == 0 & txtfineperday1.Value.Trim() != string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIOverDueChargePerDayb, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtfineperday1);
                    return;
                }

                if (Convert.ToInt32(txtDays2.Value) == 0 & txtDays2.Value.Trim() != string.Empty)
                {
                    
                    //msgLibrary.showHtml_Message(messageLibrary.msgType.Warning, Resources.ValidationResources.MsgPhaseIIDaysLimitb, this);
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIOverDueChargePerDayb, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtDays2);
                    return;
                }

                if (Convert.ToInt32(txtfineperday2.Value) == 0 & txtfineperday2.Value.Trim() != string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.MsgPhaseIIOverDueChargePerDayb.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MsgPhaseIIOverDueChargePerDayb.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIIOverDueChargePerDayb, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtfineperday2);
                    return;
                }


                if (Convert.ToInt32(txtmaxissuedayJ.Value) == 0 & txtmaxissuedayJ.Value.Trim() != string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.MsgIssuedDaysLimitj, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtmaxissuedayJ);
                    return;
                }


                if (Convert.ToInt32(txtmaxissueJournal.Value) == 0 & txtmaxissueJournal.Value.Trim() != string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.MsgIssuedItemLimitj, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtmaxissueJournal);
                    return;
                }


                if (Convert.ToInt32(txtreservationJ.Value) == 0 & txtreservationJ.Value.Trim() != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.MsgReservationLimitj, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtreservationJ);
                    return;
                }

                if (Convert.ToInt32(txtfineperdayJ.Value) == 0 & txtfineperdayJ.Value.Trim() != string.Empty)
                {

                    message.PageMesg(Resources.ValidationResources.MsgOverDueChargesPerDayj, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtfineperdayJ);
                    return;
                }


                if (Convert.ToInt32(txtDays1J.Value) == 0 & txtDays1J.Value.Trim() != string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.MsgPhaseIDaysLimitj.ToString, Me)
                   // msgLibrary.showHtml_Message(messageLibrary.msgType.Warning, Resources.ValidationResources.MsgPhaseIDaysLimitj, this);
                    this.SetFocus(txtDays1J);
                    return;
                }

                if (Convert.ToInt32(txtfineperdayJ1.Value) == 0 & txtfineperdayJ1.Value.Trim() != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIOverDueChargePerDayj, this, dbUtilities.MsgLevel.Warning);

                    this.SetFocus(txtfineperdayJ1);
                    return;
                }


                if (Convert.ToInt32(txtDays2J.Value) == 0 & txtDays2J.Value.Trim() != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIIDaysLimitj, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(txtDays2J);
                    return;
                }


                if (Convert.ToInt32(txtfineperdayJ2.Value) == 0 & txtfineperdayJ2.Value.Trim() != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.MsgPhaseIIOverDueChargePerDayj, this, dbUtilities.MsgLevel.Warning);

                    this.SetFocus(txtfineperdayJ2);
                    return;
                }


                if (optgName.Checked == true)
                {
                    if (txtmaxissueday.Value == string.Empty)
                    {
                       
                        message.PageMesg(Resources.ValidationResources.BEnter + Label2.Text.Replace("&nbsp;", "") + " [" + Label24.Text + "]", this, dbUtilities.MsgLevel.Warning);

                        this.SetFocus(txtmaxissueday);
                        return;
                    }
                    if (this.txtmaxissuebook.Value == string.Empty)
                    {
                       
                        message.PageMesg(Resources.ValidationResources.BEnter + Label3.Text.Replace("&nbsp;", "") + " [" + Label24.Text + "]", this, dbUtilities.MsgLevel.Warning);

                        this.SetFocus(txtmaxissuebook);
                        return;
                    }
                    if (this.txtreservation.Value == string.Empty)
                    {
                      
                        message.PageMesg(Resources.ValidationResources.BEnter + Label6.Text.Replace("&nbsp;", "") + " [" + Label24.Text + "]", this, dbUtilities.MsgLevel.Warning);
                        this.SetFocus(txtreservation);
                        return;
                    }
                    if (this.txtfineperday.Value == string.Empty)
                    {
                        
                        message.PageMesg(Resources.ValidationResources.ReqEnterODuChargB, this, dbUtilities.MsgLevel.Warning);
                        // LibObj.MsgBox1(Resources.ValidationResources.BEnter.ToString & lblcurr.Text.Replace("&nbsp;", "") & " [" & Label24.Text & "]", Me)
                        this.SetFocus(txtfineperday);
                        return;
                    }
                }


                if (optICategory.Checked == true)
                {
                    bool tru;
                    tru = false;
                    int count;
                    var loopTo = grdDetail.Items.Count - 1;
                    for (count = 0; count <= loopTo; count++)
                    {
                        var chkbox = new CheckBox();
                        chkbox = (CheckBox)grdDetail.Items[count].Cells[0].FindControl("Chkselect");
                        if (chkbox.Checked == true)
                        {
                            tru = true;
                            break;
                        }
                    }

                    if (tru == false)
                    {
                        LibObj.MsgBox1(Resources.ValidationResources.selatlstcat, this);
                        return;
                    }
                    int count1;
                    var loopTo1 = grdDetail.Items.Count - 1;
                    for (count1 = 0; count1 <= loopTo1; count1++)
                    {
                        var chkbox = new CheckBox();
                        chkbox = (CheckBox)grdDetail.Items[count1].Cells[0].FindControl("Chkselect");
                        if (chkbox.Checked == true)
                        {
                            if (chkbox.Enabled == true)
                            {
                                var con = new OleDbConnection(retConstr(""));
                                con.Open();
                                var ds = new DataSet();
                                string str;
                                str = "select * from Tempclassmasterloadingstatus where classname=N'" + this.txtclassname.Text + "' and LoadingStatus='" + this.grdDetail.Items[count1].Cells[2].Text + "'";
                                var da = new OleDbDataAdapter(str, con);
                                da.Fill(ds);
                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    
                                    message.PageMesg(Resources.ValidationResources.spyPlcysltcat.ToString() + "-" + this.grdDetail.Items[count1].Cells[1].Text + "", this, dbUtilities.MsgLevel.Warning);

                                    return;
                                }
                                con.Close();
                            }
                        }
                    }

                    this.txtfineperday.Value = string.Empty;
                    this.txtmaxissuebook.Value = string.Empty;
                    this.txtmaxissueday.Value = string.Empty;
                    this.txtreservation.Value = string.Empty;
                    this.txtfineperdayJ.Value = string.Empty;
                    this.txtmaxissueJournal.Value = string.Empty;
                    this.txtmaxissuedayJ.Value = string.Empty;
                    this.txtreservationJ.Value = string.Empty;
                }
                if (txtDays1.Value != string.Empty & txtfineperday1.Value == string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.PhaseB1, this, dbUtilities.MsgLevel.Warning);
                    // txtfineperday1.Focus()
                    this.SetFocus(this.txtfineperday1);
                    return;
                }
                if (txtDays1J.Value != string.Empty & txtfineperdayJ1.Value == string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.PhaseJ1, this, dbUtilities.MsgLevel.Warning);
                    // txtfineperdayJ1.Focus()
                    this.SetFocus(this.txtfineperdayJ1);
                    return;
                }
                if (txtDays2.Value != string.Empty & txtfineperday2.Value == string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.PhaseB2, this, dbUtilities.MsgLevel.Warning);
                    
                    this.SetFocus(this.txtfineperday2);
                    return;
                }

              
                if (txtDays1.Value == string.Empty & txtfineperday1.Value != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.DayLimitB1, this, dbUtilities.MsgLevel.Warning);
                    
                    this.SetFocus(this.txtDays1);
                    return;
                }
                if (txtDays1J.Value == string.Empty & txtfineperdayJ1.Value != string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.DayLimitJ1, this, dbUtilities.MsgLevel.Warning);

                   
                    this.SetFocus(this.txtDays1J);
                    return;
                }
                if (txtDays2.Value == string.Empty & txtfineperday2.Value != string.Empty)
                {
                    
                    message.PageMesg(Resources.ValidationResources.DayLimitB2, this, dbUtilities.MsgLevel.Warning);

                    // txtDays2.Focus()
                    this.SetFocus(this.txtDays2);
                    return;
                }
                if (txtDays2J.Value == string.Empty & txtfineperdayJ2.Value != string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.DayLimitJ2, this, dbUtilities.MsgLevel.Warning);


                    // txtDays2J.Focus()
                    this.SetFocus(this.txtDays2J);
                    return;
                }
                // ''''''''''''''''''''''''''''''''''

                if (txtDays2J.Value != string.Empty & txtfineperdayJ2.Value == string.Empty)
                {
                   

                    message.PageMesg(Resources.ValidationResources.PhaseJ2, this, dbUtilities.MsgLevel.Warning);

                    // txtfineperdayJ2.Focus()
                    this.SetFocus(this.txtfineperdayJ2);
                    return;
                }
                if (txtDays2.Value != string.Empty & txtDays1.Value == string.Empty)
                {
                    // Hidden1.Value = "P"
                    // LibObj.MsgBox1(Resources.ValidationResources.PhaseDetailB1.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.PhaseDetailB1.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.PhaseDetailB1, this, dbUtilities.MsgLevel.Warning);

                    // txtDays1.Focus()
                    this.SetFocus(this.txtDays1);
                    return;
                }
                if (txtDays2J.Value != string.Empty & txtDays1J.Value == string.Empty)
                {
                   
                    message.PageMesg(Resources.ValidationResources.PhaseDetailJ1, this, dbUtilities.MsgLevel.Warning);

                    // txtDays1J.Focus()
                    this.SetFocus(this.txtDays1J);
                    return;
                }
                if (txtDays1.Value != string.Empty)
                {
                    if (Convert.ToInt32(txtDays1.Value) <= Convert.ToInt32(txtmaxissueday.Value))
                    {
                       
                        message.PageMesg(Resources.ValidationResources.DayLimGreaterB1, this, dbUtilities.MsgLevel.Warning);

                        // txtDays1.Focus()
                        this.SetFocus(this.txtDays1);
                        return;
                    }
                }
                if (txtDays1J.Value != string.Empty)
                {
                    if (Convert.ToInt32(txtDays1J.Value) <= Convert.ToInt32(txtmaxissuedayJ.Value))
                    {
                       
                        message.PageMesg(Resources.ValidationResources.DayLimGreaterJ1, this, dbUtilities.MsgLevel.Warning);

                        // txtDays1J.Focus()
                        this.SetFocus(this.txtDays1J);
                        return;
                    }
                }
                if (txtDays2.Value != string.Empty)
                {
                    if (Convert.ToInt32(txtDays2.Value) <= Convert.ToInt32(txtmaxissueday.Value) )
                    {
                        
                        message.PageMesg(Resources.ValidationResources.DayLimGreaterB2, this, dbUtilities.MsgLevel.Warning);

                        // txtDays2.Focus()
                        this.SetFocus(this.txtDays2);
                        return;
                    }
                    if (Convert.ToInt32(txtDays2.Value) <= Convert.ToInt32(txtDays1.Value))
                    {
                        
                        message.PageMesg(Resources.ValidationResources.DayLimGreaterB12, this, dbUtilities.MsgLevel.Warning);

                        // txtDays2.Focus()
                        this.SetFocus(this.txtDays2);
                        return;
                    }
                }
                if (txtDays2J.Value != string.Empty)
                {
                    if (Convert.ToInt32(txtDays2J.Value) <= Convert.ToInt32(txtmaxissuedayJ.Value))
                    {
                        
                        message.PageMesg(Resources.ValidationResources.DayLimGreaterJ2, this, dbUtilities.MsgLevel.Warning);

                        // txtDays2J.Focus()
                        this.SetFocus(this.txtDays2J);
                        return;
                    }
                    if (Convert.ToInt32(txtDays2J.Value) <= Convert.ToInt32(txtDays1J.Value))
                    {
                       
                        message.PageMesg(Resources.ValidationResources.DayLimGreaterJ12, this, dbUtilities.MsgLevel.Warning);
                        // txtDays2J.Focus()
                        this.SetFocus(this.txtDays2J);
                        return;
                    }
                }
                // txtclassname.Focus()
                this.SetFocus(this.txtclassname);
                OleDbTransaction tran;
                var classcon = new OleDbConnection(retConstr(""));
                classcon.Open();
                tran = classcon.BeginTransaction();
                var classcom = new OleDbCommand();
                classcom.Connection = classcon;
                classcom.Transaction = tran;
                try
                {
                    if (Session["ClassName"].ToString() != this.txtclassname.Text)
                    {
                        classcom.CommandType = CommandType.Text;
                        string tmpr;
                        classcom.CommandText = "select className from circclassmaster where classname=N'" + txtclassname.Text + "'";
                        tmpr = Convert.ToString(classcom.ExecuteScalar());
                        if (!(tmpr.Trim() == string.Empty))
                        {
                            // Hidden1.Value = "1"
                            // LibObj.MsgBox1(Resources.ValidationResources.SpecifiedMemExist.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SpecifiedMemExist.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.SpecifiedMemExist, this, dbUtilities.MsgLevel.Warning);
                            // txtclassname.Focus()
                            this.SetFocus(this.txtclassname);
                            return;
                        }
                        else
                        {
                            Hidden1.Value = "0";

                        }
                    }
                    if (Hidden1.Value != "1")
                    {
                        classcom.CommandType = CommandType.StoredProcedure;
                        classcom.CommandText = "insert_CircClassMaster_1";

                        classcom.Parameters.Add(new OleDbParameter("@classname_1", OleDbType.VarWChar));
                        classcom.Parameters["@classname_1"].Value = txtclassname.Text.Trim();

                        classcom.Parameters.Add(new OleDbParameter("@totalissueddays_2", OleDbType.Integer));
                        classcom.Parameters["@totalissueddays_2"].Value = txtmaxissueday.Value;

                        classcom.Parameters.Add(new OleDbParameter("@noofbookstobeissued_3", OleDbType.Integer));
                        classcom.Parameters["@noofbookstobeissued_3"].Value = txtmaxissuebook.Value;

                        classcom.Parameters.Add(new OleDbParameter("@finperday_4", OleDbType.Decimal));
                        classcom.Parameters["@finperday_4"].Value = txtfineperday.Value;

                        classcom.Parameters.Add(new OleDbParameter("@reservedays_5", OleDbType.Integer));
                        classcom.Parameters["@reservedays_5"].Value = txtreservation.Value;

                        classcom.Parameters.Add(new OleDbParameter("@totalissueddays_jour_6", OleDbType.Integer));
                        classcom.Parameters["@totalissueddays_jour_6"].Value = txtmaxissuedayJ.Value;

                        classcom.Parameters.Add(new OleDbParameter("@noofjournaltobeissued_7", OleDbType.Integer));
                        classcom.Parameters["@noofjournaltobeissued_7"].Value = txtmaxissueJournal.Value;

                        classcom.Parameters.Add(new OleDbParameter("@finperday_jour_8", OleDbType.Decimal));
                        classcom.Parameters["@finperday_jour_8"].Value = txtfineperdayJ.Value;

                        classcom.Parameters.Add(new OleDbParameter("@reservedays_jour_9", OleDbType.Integer));
                        classcom.Parameters["@reservedays_jour_9"].Value = txtreservationJ.Value;

                        string check;
                        if (this.ChkActive.Checked == true)
                        {
                            check = "Y";      // Deactivation
                        }
                        else
                        {
                            check = "N";
                        }      // Activation
                        classcom.Parameters.Add(new OleDbParameter("@Status_10", OleDbType.VarWChar));
                        classcom.Parameters["@Status_10"].Value = check;
                        if (this.chkRequester.Checked == true)
                        {
                            check = "Y";      // Deactivation
                        }
                        else
                        {
                            check = "N";
                        }      // Activation
                        classcom.Parameters.Add(new OleDbParameter("@canRequest_11", OleDbType.VarWChar));
                        classcom.Parameters["@canRequest_11"].Value = check;

                        classcom.Parameters.Add(new OleDbParameter("@MaxValue_12", OleDbType.Numeric));
                        classcom.Parameters["@MaxValue_12"].Value = this.txtMaxValue.Text;


                        classcom.Parameters.Add(new OleDbParameter("@days_1phase_13", OleDbType.Numeric));
                        classcom.Parameters["@days_1phase_13"].Value = this.txtDays1.Value;

                        classcom.Parameters.Add(new OleDbParameter("@amt_1phase_14", OleDbType.Numeric));
                        classcom.Parameters["@amt_1phase_14"].Value = this.txtfineperday1.Value;

                        classcom.Parameters.Add(new OleDbParameter("@days_2phase_15", OleDbType.Numeric));
                        classcom.Parameters["@days_2phase_15"].Value = this.txtDays2.Value;

                        classcom.Parameters.Add(new OleDbParameter("@amt_2phase_16", OleDbType.Numeric));
                        classcom.Parameters["@amt_2phase_16"].Value = this.txtfineperday2.Value;

                        classcom.Parameters.Add(new OleDbParameter("@days_1phasej_13", OleDbType.Numeric));
                        classcom.Parameters["@days_1phasej_13"].Value = this.txtDays1J.Value;

                        classcom.Parameters.Add(new OleDbParameter("@amt_1phasej_14", OleDbType.Numeric));
                        classcom.Parameters["@amt_1phasej_14"].Value = this.txtfineperdayJ1.Value;

                        classcom.Parameters.Add(new OleDbParameter("@days_2phasej_15", OleDbType.Numeric));
                        classcom.Parameters["@days_2phasej_15"].Value = this.txtDays2J.Value;

                        classcom.Parameters.Add(new OleDbParameter("@amt_2phasej_16", OleDbType.Numeric));
                        classcom.Parameters["@amt_2phasej_16"].Value = this.txtfineperdayJ2.Value;

                        classcom.Parameters.Add(new OleDbParameter("@shortname_21", OleDbType.VarWChar));
                        classcom.Parameters["@shortname_21"].Value = this.txtshortname.Text.Trim();

                        classcom.Parameters.Add(new OleDbParameter("@userid_22", OleDbType.VarWChar));
                        classcom.Parameters["@userid_22"].Value = Session["user_id"];

                        string policy = string.Empty;
                        if (this.optgName.Checked == true)
                        {
                            policy = "S";
                        }
                        else if (this.optICategory.Checked == true)
                        {
                            policy = "D";
                        }

                        classcom.Parameters.Add(new OleDbParameter("@policystatus_23", OleDbType.VarWChar));
                        classcom.Parameters["@Policystatus_23"].Value = policy;

                        if (RadioButtonList1.SelectedItem.Text == "")
                        {
                            classcom.Parameters.Add(new OleDbParameter("@MembershipType", OleDbType.VarChar));
                            classcom.Parameters["@MembershipType"].Value = "";
                        }
                        else
                        {
                            classcom.Parameters.Add(new OleDbParameter("@MembershipType", OleDbType.VarChar));
                            classcom.Parameters["@MembershipType"].Value = RadioButtonList1.SelectedItem.Text;
                        }

                        if (RadioButtonList2.SelectedItem.Text == "")
                        {
                            classcom.Parameters.Add(new OleDbParameter("@Security", OleDbType.VarChar));
                            classcom.Parameters["@Security"].Value = "";
                        }
                        else
                        {
                            classcom.Parameters.Add(new OleDbParameter("@Security", OleDbType.VarChar));
                            classcom.Parameters["@Security"].Value = RadioButtonList2.SelectedItem.Text;
                        }


                        classcom.ExecuteNonQuery();
                        classcom.Parameters.Clear();


                        int cnt1;
                        if (grdDetail.Items.Count > 0)
                        {

                            classcom.CommandType = CommandType.Text;
                            classcom.CommandText = "delete from classmasterloadingstatus where classname=N'" + txtclassname.Text + "'";
                            classcom.ExecuteNonQuery();
                            classcom.Parameters.Clear();
                            if (this.optgName.Checked == true)
                            {
                                var loopTo2 = grdDetail.Items.Count - 1;
                                for (cnt1 = 0; cnt1 <= loopTo2; cnt1++)
                                {
                                    var ctl = new CheckBox();
                                    ctl = (CheckBox)grdDetail.Items[cnt1].Cells[0].FindControl("Chkselect");
                                    classcom.CommandType = CommandType.StoredProcedure;
                                    classcom.CommandText = "insert_classmstloadstatus_1";
                                    if (ctl.Checked == true)
                                    {
                                        classcom.Parameters.Add(new OleDbParameter("@classname_1", OleDbType.VarWChar));
                                        classcom.Parameters["@classname_1"].Value = txtclassname.Text.Trim();

                                        classcom.Parameters.Add(new OleDbParameter("@totalissueddays_2", OleDbType.Integer));
                                        classcom.Parameters["@totalissueddays_2"].Value = txtmaxissueday.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@noofbookstobeissued_3", OleDbType.Integer));
                                        classcom.Parameters["@noofbookstobeissued_3"].Value = txtmaxissuebook.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@finperday_4", OleDbType.Decimal));
                                        classcom.Parameters["@finperday_4"].Value = txtfineperday.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@reservedays_5", OleDbType.Integer));
                                        classcom.Parameters["@reservedays_5"].Value = txtreservation.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@totalissueddays_jour_6", OleDbType.Integer));
                                        classcom.Parameters["@totalissueddays_jour_6"].Value = txtmaxissuedayJ.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@noofjournaltobeissued_7", OleDbType.Integer));
                                        classcom.Parameters["@noofjournaltobeissued_7"].Value = txtmaxissueJournal.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@fineperday_jour_8", OleDbType.Decimal));
                                        classcom.Parameters["@fineperday_jour_8"].Value = txtfineperdayJ.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@reservedays_jour_9", OleDbType.Integer));
                                        classcom.Parameters["@reservedays_jour_9"].Value = txtreservationJ.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@Status_10", OleDbType.VarWChar));
                                        classcom.Parameters["@Status_10"].Value = check;

                                        classcom.Parameters.Add(new OleDbParameter("@MaxValue_11", OleDbType.Numeric));
                                        classcom.Parameters["@MaxValue_11"].Value = this.txtMaxValue.Text;

                                        classcom.Parameters.Add(new OleDbParameter("@days_1phase_12", OleDbType.Numeric));
                                        classcom.Parameters["@days_1phase_12"].Value = this.txtDays1.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@amt_1phase_13", OleDbType.Numeric));
                                        classcom.Parameters["@amt_1phase_13"].Value = this.txtfineperday1.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@days_2phase_14", OleDbType.Numeric));
                                        classcom.Parameters["@days_2phase_14"].Value = this.txtDays2.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@amt_2phase_15", OleDbType.Numeric));
                                        classcom.Parameters["@amt_2phase_15"].Value = this.txtfineperday2.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@days_1phasej_16", OleDbType.Numeric));
                                        classcom.Parameters["@days_1phasej_16"].Value = this.txtDays1J.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@amt_1phasej_17", OleDbType.Numeric));
                                        classcom.Parameters["@amt_1phasej_17"].Value = this.txtfineperdayJ1.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@days_2phasej_18", OleDbType.Numeric));
                                        classcom.Parameters["@days_2phasej_18"].Value = this.txtDays2J.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@amt_2phasej_19", OleDbType.Numeric));
                                        classcom.Parameters["@amt_2phasej_19"].Value = this.txtfineperdayJ2.Value;

                                        classcom.Parameters.Add(new OleDbParameter("@shortname_20", OleDbType.VarWChar));
                                        classcom.Parameters["@shortname_20"].Value = this.txtshortname.Text.Trim();

                                        classcom.Parameters.Add(new OleDbParameter("@loadingstatus_21", OleDbType.VarWChar));
                                        classcom.Parameters["@loadingstatus_21"].Value = grdDetail.Items[cnt1].Cells[2].Text.Trim();

                                        classcom.ExecuteNonQuery();
                                        classcom.Parameters.Clear();
                                    }
                                }
                            }
                            else if (this.optICategory.Checked == true)
                            {
                                var classmasterda_1 = new OleDbDataAdapter();
                                var classmasterds_1 = new DataSet();
                                classcom.CommandType = CommandType.Text;
                                classcom.CommandText = "select * from Tempclassmasterloadingstatus where classname =N'" + txtclassname.Text.Trim() + "'";
                                classmasterda_1.SelectCommand = classcom;
                                classmasterda_1.Fill(classmasterds_1);
                                int icounter1;
                                var loopTo3 = classmasterds_1.Tables[0].Rows.Count - 1;
                                for (icounter1 = 0; icounter1 <= loopTo3; icounter1++)
                                {
                                    classcom.Parameters.Clear();
                                    classcom.CommandType = CommandType.StoredProcedure;
                                    classcom.CommandText = "insert_classmstloadstatus_1";

                                    classcom.Parameters.Add(new OleDbParameter("@classname_1", OleDbType.VarWChar));
                                    classcom.Parameters["@classname_1"].Value = classmasterds_1.Tables[0].Rows[icounter1]["classname"];

                                    classcom.Parameters.Add(new OleDbParameter("@totalissueddays_2", OleDbType.Integer));
                                    classcom.Parameters["@totalissueddays_2"].Value = classmasterds_1.Tables[0].Rows[icounter1]["totalissueddays"];

                                    classcom.Parameters.Add(new OleDbParameter("@noofbookstobeissued_3", OleDbType.Integer));
                                    classcom.Parameters["@noofbookstobeissued_3"].Value = classmasterds_1.Tables[0].Rows[icounter1]["noofbookstobeissued"];

                                    classcom.Parameters.Add(new OleDbParameter("@finperday_4", OleDbType.Decimal));
                                    classcom.Parameters["@finperday_4"].Value = classmasterds_1.Tables[0].Rows[icounter1]["finperday"];

                                    classcom.Parameters.Add(new OleDbParameter("@reservedays_5", OleDbType.Integer));
                                    classcom.Parameters["@reservedays_5"].Value = classmasterds_1.Tables[0].Rows[icounter1]["reservedays"];

                                    classcom.Parameters.Add(new OleDbParameter("@totalissueddays_jour_6", OleDbType.Integer));
                                    classcom.Parameters["@totalissueddays_jour_6"].Value = classmasterds_1.Tables[0].Rows[icounter1]["totalissueddays_jour"];

                                    classcom.Parameters.Add(new OleDbParameter("@noofjournaltobeissued_7", OleDbType.Integer));
                                    classcom.Parameters["@noofjournaltobeissued_7"].Value = classmasterds_1.Tables[0].Rows[icounter1]["noofjournaltobeissued"];

                                    classcom.Parameters.Add(new OleDbParameter("@fineperday_jour_8", OleDbType.Decimal));
                                    classcom.Parameters["@fineperday_jour_8"].Value = classmasterds_1.Tables[0].Rows[icounter1]["fineperday_jour"];

                                    classcom.Parameters.Add(new OleDbParameter("@reservedays_jour_9", OleDbType.Integer));
                                    classcom.Parameters["@reservedays_jour_9"].Value = classmasterds_1.Tables[0].Rows[icounter1]["reservedays_jour"];

                                    classcom.Parameters.Add(new OleDbParameter("@Status_10", OleDbType.VarWChar));
                                    classcom.Parameters["@Status_10"].Value = classmasterds_1.Tables[0].Rows[icounter1]["Status"];

                                    classcom.Parameters.Add(new OleDbParameter("@ValueLimit_11", OleDbType.Numeric));
                                    classcom.Parameters["@ValueLimit_11"].Value = classmasterds_1.Tables[0].Rows[icounter1]["ValueLimit"];

                                    classcom.Parameters.Add(new OleDbParameter("@days_1phase_12", OleDbType.Numeric));
                                    classcom.Parameters["@days_1phase_12"].Value = classmasterds_1.Tables[0].Rows[icounter1]["days_1phase"];

                                    classcom.Parameters.Add(new OleDbParameter("@amt_1phase_13", OleDbType.Numeric));
                                    classcom.Parameters["@amt_1phase_13"].Value = classmasterds_1.Tables[0].Rows[icounter1]["amt_1phase"];

                                    classcom.Parameters.Add(new OleDbParameter("@days_2phase_14", OleDbType.Numeric));
                                    classcom.Parameters["@days_2phase_14"].Value = classmasterds_1.Tables[0].Rows[icounter1]["days_2phase"];

                                    classcom.Parameters.Add(new OleDbParameter("@amt_2phase_15", OleDbType.Numeric));
                                    classcom.Parameters["@amt_2phase_15"].Value = classmasterds_1.Tables[0].Rows[icounter1]["amt_2phase"];

                                    classcom.Parameters.Add(new OleDbParameter("@days_1phasej_16", OleDbType.Numeric));
                                    classcom.Parameters["@days_1phasej_16"].Value = classmasterds_1.Tables[0].Rows[icounter1]["days_1phasej"];

                                    classcom.Parameters.Add(new OleDbParameter("@amt_1phasej_17", OleDbType.Numeric));
                                    classcom.Parameters["@amt_1phasej_17"].Value = classmasterds_1.Tables[0].Rows[icounter1]["amt_1phasej"];

                                    classcom.Parameters.Add(new OleDbParameter("@days_2phasej_18", OleDbType.Numeric));
                                    classcom.Parameters["@days_2phasej_18"].Value = classmasterds_1.Tables[0].Rows[icounter1]["days_2phasej"];

                                    classcom.Parameters.Add(new OleDbParameter("@amt_2phasej_19", OleDbType.Numeric));
                                    classcom.Parameters["@amt_2phasej_19"].Value = classmasterds_1.Tables[0].Rows[icounter1]["amt_2phasej"];

                                    classcom.Parameters.Add(new OleDbParameter("@shortname_20", OleDbType.VarWChar));
                                    classcom.Parameters["@shortname_20"].Value = classmasterds_1.Tables[0].Rows[icounter1]["shortname"];

                                    classcom.Parameters.Add(new OleDbParameter("@loadingstatus_21", OleDbType.VarWChar));
                                    classcom.Parameters["@loadingstatus_21"].Value = classmasterds_1.Tables[0].Rows[icounter1]["loadingstatus"];

                                    classcom.ExecuteNonQuery();
                                    classcom.Parameters.Clear();
                                }
                            }
                        }
                        if (Convert.ToBoolean(ViewState["openCond"] == null))
                        {
                            
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OpenWindow", "retOnSC('txtclassname');", true);
                           
                        }
                        
                        classcom.Parameters.Clear();
                        if (LoggedUser.Logged().IsAudit == "Y")
                        {
                            if (cmdsubmit.Text == Resources.ValidationResources.bSave.ToString())
                            {
                                LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, this.txtclassname.Text.Trim(),Resources.ValidationResources.Insert.ToString(), retConstr(""));
                            }
                            else
                            {
                                LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, this.txtclassname.Text.Trim(), Resources.ValidationResources.bUpdate.ToString(), retConstr(""));
                            }
                        }



                        
                        message.PageMesg(Resources.ValidationResources.recsave.ToString(), this, dbUtilities.MsgLevel.Success);
                        
                        refreshfields();

                        var classmasterda = new OleDbDataAdapter();
                        var classmasterds = new DataSet();
                        classcom.CommandType = CommandType.Text;

                        //classcom.CommandText = "select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,totalissueddays_jour,noofjournaltobeissued,fineperday_jour,reservedays_jour,status,canrequest,ValueLimit,days_1phase,amt_1phase,days_2phase,amt_2phase,days_1phasej,amt_1phasej,days_2phasej,amt_2phasej from CircClassMaster order by classname";
                        classmasterda.SelectCommand = classcom;
                        classmasterda.Fill(classmasterds);
                        DataGrid1.DataSource = classmasterds.Tables[0].DefaultView;
                        DataGrid1.DataBind();
                        var da = new OleDbDataAdapter();
                        var ds = new DataSet();
                        classcom.CommandType = CommandType.Text;
                        //classcom.CommandText = "select id,Category_LoadingStatus from CategoryLoadingStatus  where id <> 0 order by Category_LoadingStatus";
                        da.SelectCommand = classcom;
                        da.Fill(ds);
                        this.grdDetail.DataSource = ds;
                        this.grdDetail.DataBind();
                        tran.Commit();
                        classcon.Close();
                        classcom.Dispose();
                        classcon.Dispose();
                        classmasterds.Dispose();
                        classmasterda.Dispose();
                    }
                    Session["ClassName"] = "";
                    cmdsubmit.Text = Resources.ValidationResources.bSave.ToString();

                    if (tmpcondition == "Y")
                    {
                        this.cmddelete.Visible = false;
                        this.cmdsubmit.Visible = false;
                    }
                    else if (tmpcondition == "Y")
                    {
                        this.cmddelete.Visible = true;
                        this.cmdsubmit.Visible = true;
                    }
                    else
                    {
                        lblTitle.Text = Resources.ValidationResources.Title_membergroup;
                        this.cmdsubmit.Visible = false;
                        this.cmddelete.Visible = false;
                        // Me.cmdReturn.Disabled = True
                        Session["NFormDW"] = "dLogout";
                    }
                    txtEnable();
                    cmddelete.Visible = true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                        
                        message.PageMesg(Resources.ValidationResources.UnsaveMGInfo, this, dbUtilities.MsgLevel.Failure);
                    }
                    
                    catch (Exception ex1)
                    {
                        
                        message.PageMesg(Resources.ValidationResources.UnsaveMGInfo, this, dbUtilities.MsgLevel.Failure);
                       
                    }
                }
                finally
                {
                    if (classcon.State == ConnectionState.Open)
                    {
                        classcon.Close();
                    }
                    tran.Dispose();
                    classcom.Dispose();
                    classcon.Dispose();
                }
            }
            catch (Exception exmain)
            {
                
                message.PageMesg(Resources.ValidationResources.UnsaveMGInfo, this, dbUtilities.MsgLevel.Failure);
               
            }
        }

        public string populateCommandText(string sqlStr, OleDbConnection connection)
        {
            var CategoryLoadingStatuscom3 = new OleDbCommand();
            CategoryLoadingStatuscom3.Connection = connection;
            CategoryLoadingStatuscom3.CommandType = CommandType.Text;
            CategoryLoadingStatuscom3.CommandText = sqlStr;
            string tmpstr;
            tmpstr = Convert.ToString(CategoryLoadingStatuscom3.ExecuteScalar());
            return tmpstr;
        }

        public void GenerateMGLStatusID()
        {
            var CategoryLoadingStatusconn = new OleDbConnection(retConstr(""));
            CategoryLoadingStatusconn.Open();
            string tmpstr;
            //tmpstr = populateCommandText("select coalesce(max(id),0,max(id)) from classmasterLoadingStatus", CategoryLoadingStatusconn);
            //txtCLStatusCode.Value = Convert.ToInt32(tmpstr) == 0 ? "1" : tmpstr + 1;
            CategoryLoadingStatusconn.Close();
            CategoryLoadingStatusconn.Dispose();
        }
        public void refreshfields()
        {
            // --pranav---------
            this.txtDays1.Value = string.Empty;
            this.txtfineperday1.Value = string.Empty;
            this.txtDays2.Value = string.Empty;
            this.txtfineperday2.Value = string.Empty;
            // ---journal
            this.txtDays1J.Value = string.Empty;
            this.txtfineperdayJ1.Value = string.Empty;
            this.txtDays2J.Value = string.Empty;
            this.txtfineperdayJ2.Value = string.Empty;
            // ----------------------------
            if (Convert.ToBoolean(ViewState["openCond"] == null ))
            {
                txtclassname.Text = string.Empty;
            }
            txtmaxissuebook.Value = string.Empty;
            txtmaxissueday.Value = string.Empty;
            txtfineperday.Value = string.Empty;
            txtreservation.Value = string.Empty;
            this.txtfineperdayJ.Value = string.Empty;
            this.txtmaxissueJournal.Value = string.Empty;
            this.txtmaxissuedayJ.Value = string.Empty;
            this.txtreservationJ.Value = string.Empty;
            this.ChkActive.Checked = false;
            chkRequester.Checked = false;
            // Me.grdDetail.SelectedIndex = -1
            this.txtMaxValue.Text = string.Empty;
            this.txtshortname.Text = string.Empty;
            if (this.optICategory.Checked)
            {
                grdDetail.Columns[3].Visible = true;
            }
            else
            {

            }
            this.optgName.Checked = true;
            this.optICategory.Checked = false;

        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Reset();
            try
            {
                if (!(e.CommandName == "Page"))
                {
                    txtclassname.ReadOnly = true;
                }

                // txtclassname.Focus()
                this.SetFocus(this.txtclassname);
                // Hidden1.Value = "111"
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            var classmastercon = new OleDbConnection(retConstr(""));
                            var classmasterds = new DataSet();
                            //classmasterds = LibObj.PopulateDataset("select classname,totalissueddays,finperday,noofbookstobeissued,reservedays,totalissueddays_jour,noofjournaltobeissued,fineperday_jour,reservedays_jour,status,canRequest,CircClassMaster.ValueLimit  as ValueLimit,Days_1Phase,amt_1phase,Days_2Phase,amt_2phase,days_1phasej,amt_1phasej,days_2phasej,amt_2phasej,shortname,policystatus  from CircClassMaster where classname=N'" + DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text + "'", "ClassMaster", classmastercon);
                            if (classmasterds.Tables["ClassMaster"].Rows.Count == 0)
                            {
                                // HdMsg.Value = 1
                                // LibObj.MsgBox1(Resources.ValidationResources.RecNoLong.ToString, Me)
                                //msgLibrary.showHtml_Message(messageLibrary.msgType.Warning, Resources.ValidationResources.RecNoLong.ToString, this);
                                return;
                            }
                            this.txtshortname.Text = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["classmaster"].Rows[0]["shortname"]) ? string.Empty: classmasterds.Tables["classmaster"].Rows[0]["shortname"]);
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["Days_1Phase"]) == 0)
                            {
                                this.txtDays1.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["Days_1Phase"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["Days_1Phase"]);
                            }
                            else
                            {
                                this.txtDays1.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["amt_1phase"]) == 0)
                            {
                                this.txtfineperday1.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["amt_1phase"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["amt_1phase"]);
                            }
                            else
                            {
                                this.txtfineperday1.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["Days_2Phase"]) == 0)
                            {
                                this.txtDays2.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["Days_2Phase"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["Days_2Phase"]);
                            }
                            else
                            {
                                this.txtDays2.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["amt_2phase"]) == 0)
                            {
                                this.txtfineperday2.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["amt_2phase"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["amt_2phase"]);
                            }
                            else
                            {
                                this.txtfineperday2.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["Days_1Phasej"]) == 0)
                            {
                                this.txtDays1J.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["Days_1Phasej"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["Days_1Phasej"]);
                            }
                            else
                            {
                                this.txtDays1J.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["amt_1phasej"]) == 0)
                            {
                                this.txtfineperdayJ1.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["amt_1phasej"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["amt_1phasej"]);
                            }
                            else
                            {
                                this.txtfineperdayJ1.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["Days_2Phasej"]) == 0)
                            {
                                this.txtDays2J.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["Days_2Phasej"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["Days_2Phasej"]);
                            }
                            else
                            {
                                this.txtDays2J.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0]["amt_2phasej"]) == 0)
                            {
                                this.txtfineperdayJ2.Value = Convert.ToString(Convert.ToBoolean(classmasterds.Tables["ClassMaster"].Rows[0]["amt_2phasej"]) ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["amt_2phasej"]);
                            }
                            else
                            {
                                this.txtfineperdayJ2.Value = string.Empty;
                            }
                            txtclassname.Text = classmasterds.Tables["ClassMaster"].Rows[0][0].ToString();
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][1]) == 0)
                            {
                                txtmaxissueday.Value = classmasterds.Tables["ClassMaster"].Rows[0][1].ToString();
                            }
                            else
                            {
                                txtmaxissueday.Value = string.Empty;
                            }

                            if (Convert.ToString(classmasterds.Tables["ClassMaster"].Rows[0]["Policystatus"]) == "D")
                            {
                                this.optICategory.Checked = true;
                                this.optgName.Checked = false;
                                // grdDetail.Columns(3).Visible = True
                                grdDetail.Visible = true;
                                txtDisable();
                            }
                            else if (Convert.ToBoolean(Convert.ToString(classmasterds.Tables["ClassMaster"].Rows[0]["Policystatus"]) == "S"))
                            {
                                this.optICategory.Checked = false;
                                this.optgName.Checked = true;
                                // grdDetail.Columns(3).Visible = False
                                grdDetail.Visible = false;
                                txtEnable();
                            }
                            // Reset()

                            txtfineperday.Value = classmasterds.Tables["ClassMaster"].Rows[0][2].ToString();

                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][3]) == 0)
                            {
                                txtmaxissuebook.Value = classmasterds.Tables["ClassMaster"].Rows[0][3].ToString();
                            }
                            else
                            {
                                txtmaxissuebook.Value = string.Empty;
                            }
                            

                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][4]) == 0)
                            {
                                txtreservation.Value = "0";
                            }
                            else
                            {
                                txtreservation.Value = classmasterds.Tables["ClassMaster"].Rows[0][4].ToString();
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][5]) == 0)
                            {
                                txtmaxissuedayJ.Value = classmasterds.Tables["ClassMaster"].Rows[0][5].ToString();
                            }
                            else
                            {
                                txtmaxissuedayJ.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][7]) == 0)
                            {
                                txtfineperdayJ.Value = classmasterds.Tables["ClassMaster"].Rows[0][7].ToString();
                            }
                            else
                            {
                                txtfineperdayJ.Value = string.Empty;
                            }




                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][6]) == 0)
                            {
                                txtmaxissueJournal.Value = classmasterds.Tables["ClassMaster"].Rows[0][6].ToString();
                            }
                            else
                            {
                                txtmaxissueJournal.Value = string.Empty;
                            }
                            if (Convert.ToInt32(classmasterds.Tables["ClassMaster"].Rows[0][8]) == 0)
                            {
                                txtreservationJ.Value = classmasterds.Tables["ClassMaster"].Rows[0][8].ToString();
                            }
                            else
                            {
                                txtreservationJ.Value = string.Empty;
                            }
                            this.txtMaxValue.Text = Convert.ToString(Convert.ToUInt32(classmasterds.Tables["ClassMaster"].Rows[0]["ValueLimit"]) == 0 ? string.Empty : classmasterds.Tables["ClassMaster"].Rows[0]["ValueLimit"]);
                            string check;
                            check = Convert.ToString(classmasterds.Tables["ClassMaster"].Rows[0][9]);
                            if (check == "Y")
                            {
                                this.ChkActive.Checked = true;
                            }
                            else
                            {
                                this.ChkActive.Checked = false;
                            }

                            check = Convert.ToString(classmasterds.Tables["ClassMaster"].Rows[0]["canRequest"]);
                            if (check == "Y")
                            {
                                this.chkRequester.Checked = true;
                            }
                            else
                            {
                                this.chkRequester.Checked = false;
                            }


                            fillgrddetail(DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text, classmasterds.Tables["ClassMaster"].Rows[0]["Policystatus"].ToString(), classmastercon);

                            classmasterds.Dispose();
                            classmastercon.Close();
                            classmastercon.Dispose();
                            cmdsubmit.Text = Resources.ValidationResources.bUpdate.ToString();
                            if (tmpcondition == "Y")
                            {
                                this.cmddelete.Visible = false;
                                this.cmdsubmit.Visible = false;
                            }
                            else if (tmpcondition == "N")
                            {
                                this.cmddelete.Visible = true;
                                this.cmdsubmit.Visible = true;
                            }
                            else
                            {
                                lblTitle.Text = Resources.ValidationResources.Title_membergroup;
                                this.cmdsubmit.Visible = false;
                                this.cmddelete.Visible = false;
                                // Me.cmdReturn.Disabled = True
                                Session["NFormDW"] = "dLogout";
                            }

                            break;
                        }
                }

                Session["ClassName"] = this.txtclassname.Text;
            }
            catch (Exception ex)
            {
                
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                message.PageMesg(Resources.ValidationResources.UnRetriveMGInfo, this, dbUtilities.MsgLevel.Failure);
               
            }

        }

        public void fillgrddetail(string clsName, string status, OleDbConnection con)
        {

            //string sqlstr = "select loadingstatus from circclassmaster,classmasterloadingstatus where circclassmaster.classname= classmasterloadingstatus.classname and classmasterloadingstatus.classname=N'" + clsName + "'"; // and classmasterloadingstatus.status='Y'"
            int j;
            var ds = new DataSet();
            //ds = LibObj.PopulateDataset(sqlstr, "loadingstatus", con);
            if (ds.Tables["loadingstatus"].Rows.Count > 0)
            {
                int cnt1;
                if (grdDetail.Items.Count > 0)
                {
                    var loopTo = grdDetail.Items.Count - 1;
                    for (cnt1 = 0; cnt1 <= loopTo; cnt1++)
                    {
                        var loopTo1 = ds.Tables["loadingstatus"].Rows.Count - 1;
                        for (j = 0; j <= loopTo1; j++)
                        {
                            if (grdDetail.Items[cnt1].Cells[2].Text == ds.Tables["loadingstatus"].Rows[j][0].ToString())
                            {
                                if (status == "D")
                                {
                                    var ctl = new CheckBox();
                                    ctl = (CheckBox)grdDetail.Items[cnt1].Cells[0].FindControl("Chkselect");
                                    ctl.Checked = true;
                                    ctl.Enabled = false;
                                }
                                else
                                {
                                    var ctl = new CheckBox();
                                    ctl = (CheckBox)grdDetail.Items[cnt1].Cells[0].FindControl("Chkselect");
                                    ctl.Checked = true;
                                    ctl.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                //ds = LibObj.PopulateDataset("select id,Category_LoadingStatus from CategoryLoadingStatus  where id <> 0  order by Category_LoadingStatus", "Categoryloadingstatus", con);
                if (ds.Tables["Categoryloadingstatus"].Rows.Count > 0)
                {
                    grdDetail.DataSource = ds.Tables["Categoryloadingstatus"].DefaultView;
                    grdDetail.DataBind();
                }
                else
                {
                    grdDetail.DataSource = dt;
                    grdDetail.DataBind();
                }

            }
            ds.Dispose();

        }

        public DataView populategridpageIndex(string sqlStr, string tableName, OleDbConnection myConnection)
        {
            var Classmasterds = new DataSet();
            Classmasterds = LibObj.PopulateDataset(sqlStr, tableName, myConnection);
            var dt = Classmasterds.Tables[tableName];
            var dv = new DataView(dt);
            return dv;
            Classmasterds.Dispose();
        }

        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                var classMastercon = new OleDbConnection(retConstr(""));
                classMastercon.Open();
                string searchqry;
                // searchqry = "select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,totalissueddays_jour,noofjournaltobeissued,fineperday_jour,reservedays_jour,Status,canrequest from CircClassMaster order by classname"
                //searchqry = "select * from CircClassMaster order by classname";
                //DataGrid1.CurrentPageIndex = e.NewPageIndex;
                //DataGrid1.DataSource = populategridpageIndex(searchqry, "Classtmaster", classMastercon);
                DataGrid1.DataBind();
                classMastercon.Close();
                classMastercon.Dispose();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmddelete_Click(object sender, EventArgs e)
        {
            try
            {
                var delcon = new OleDbConnection(retConstr(""));
                delcon.Open();
               // var cmd = new OleDbCommand("Select status from CircClassMaster where shortname=N'" + this.txtshortname.Text.Replace("'", "''") + "'", delcon);
                OleDbDataReader dr;
                //dr = cmd.ExecuteReader();
                //if (dr.Read())
                //{
                //    txtCLStatusCode.Value = dr.GetValue(0).ToString();
                //}
                //else
                //{
                //    txtCLStatusCode.Value = string.Empty;
                //}
                if (this.txtclassname.Text.Trim() == string.Empty)
                {
                    // Hidden3.Value = "3"  'Select the record to be deleted
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelSpecify.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelSpecify, this, dbUtilities.MsgLevel.Warning);
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelSpecify.ToString, Me)
                    // Me.txtclassname.Focus()
                    this.SetFocus(this.txtclassname);
                }
                else if (LibObj.checkChildExistancewc("classname", "CircClassMaster", "status=N'" + this.txtCLStatusCode.Value.Trim() + "'", delcon) == false)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelNotExist.ToString, Me) 'Currentl displayed record does not exist in database
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelNotExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelNotExist, this, dbUtilities.MsgLevel.Warning);
                    delcon.Dispose();

                    delcon.Close();
                    return;
                }
                else if (LibObj.checkChildExistancewc("classname", "CircUserManagement", "classname=N'" + this.txtclassname.Text.Trim() + "'", delcon) == true)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'Child records found
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rDelChExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rDelChExist, this, dbUtilities.MsgLevel.Warning);
                    // Me.txtclassname.Focus()
                    this.SetFocus(this.txtclassname);
                    delcon.Dispose();
                    delcon.Close();
                    return;
                }
                // ElseIf LibObj.checkChildExistancewc("classname", "staffmaster", "classname='" & Trim(Me.txtclassname.Text) & "'", delcon) = True Then
                // LibObj.MsgBox1(Resources.ValidationResources.rDelChExist.ToString, Me) 'Child records found
                // delcon.Dispose()
                // delcon.Close()
                // Exit Sub
                else
                {
                    OleDbTransaction tran;

                    tran = delcon.BeginTransaction();
                    var delcom = new OleDbCommand();
                    string str, str1;
                    //str = "delete from circclassmaster where classname=N'" + this.txtclassname.Text.Trim() + "'"; // , delcon)
                    //str1 = "delete from classmasterloadingstatus where classname=N'" + this.txtclassname.Text.Trim() + "'";
                    delcom.CommandType = CommandType.Text;
                    delcom.Connection = delcon;
                    delcom.Transaction = tran;
                    // --------index error for grid ----------
                    int row_count, page_size, data_count;
                    row_count = DataGrid1.CurrentPageIndex;
                    page_size = DataGrid1.PageSize;
                    try
                    {
                        //delcom.CommandText = str;
                        delcom.ExecuteNonQuery();
                        delcom.Parameters.Clear();
                        //delcom.CommandText = str1;
                        delcom.ExecuteNonQuery();
                        delcom.Parameters.Clear();
                        var classmasterda = new OleDbDataAdapter();
                        delcom.CommandType = CommandType.Text;
                       // delcom.CommandText = "select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,totalissueddays_jour,noofjournaltobeissued,fineperday_jour,reservedays_jour,Status,canrequest,ValueLimit,days_1phase,amt_1phase,days_2phase,amt_2phase,days_1phasej,amt_1phasej,days_2phasej,amt_2phasej from CircClassMaster order by classname";
                        classmasterda.SelectCommand = delcom;
                        var classmasterds = new DataSet();
                        classmasterda.Fill(classmasterds, "DepartmentMaster");
                        if (classmasterds.Tables[0].Rows.Count > 0)
                        {
                            data_count = classmasterds.Tables[0].Rows.Count;
                            // -==============================================================
                            if (data_count % page_size == 0)
                            {

                                this.DataGrid1.CurrentPageIndex = 0;
                            }
                            else
                            {

                                this.DataGrid1.CurrentPageIndex = this.DataGrid1.CurrentPageIndex;

                            }
                            DataGrid1.DataSource = classmasterds.Tables[0].DefaultView;
                            DataGrid1.DataBind();
                        }

                        else
                        {
                            var tab = new DataTable();
                            DataGrid1.DataSource = tab;
                            DataGrid1.DataBind();
                        }
                        delcom.Parameters.Clear();
                        if (LoggedUser.Logged().IsAudit ==  "Y")
                        {
                            LibObj1.insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, this.txtclassname.Text.Trim(), Resources.ValidationResources.bDelete, retConstr(""));
                        }
                        tran.Commit();
                        delcon.Close();
                        delcon.Dispose();
                        delcom.Dispose();
                        classmasterds.Dispose();
                        classmasterda.Dispose();
                        cmdreset_Click(sender, e); // to clear the fields in form
                                                         // LibObj.MsgBox1(Resources.ValidationResources.rDel.ToString, Me)
                                                         // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.rDel.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.rDel, this, dbUtilities.MsgLevel.Warning);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            tran.Rollback();
                            // Me.msglabel.Visible = True
                            // msglabel.Text = ex.Message
                            message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                        }
                        catch (Exception ex1)
                        {
                            // Me.msglabel.Visible = True
                            // msglabel.Text = ex1.Message
                            message.PageMesg(ex1.Message, this, dbUtilities.MsgLevel.Failure);
                        }
                    }
                    refreshfields();
                }
                DataGrid1.SelectedIndex = -1;
                this.SetFocus(this.txtclassname);
                hdTop.Value = Resources.ValidationResources.RBTop;
            }
            catch (Exception exmain)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = exmain.Message
                message.PageMesg(exmain.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void grdDetail_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (((LinkButton)e.CommandSource).CommandName)
            {
                case "Policy":
                    {
                        // grdDetail.Focus()
                        this.SetFocus(this.grdDetail);
                        txtdesignationid.Value = grdDetail.Items[e.Item.ItemIndex].Cells[2].Text;
                        this.hcName.Value = this.txtclassname.Text.Trim();
                        if (hcName.Value == default)
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.ReqEnterMemGrN.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.ReqEnterMemGrN, this, dbUtilities.MsgLevel.Warning);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ReqEnterMemGrN.ToString, Me)
                            // txtclassname.Focus()
                            this.SetFocus(this.txtclassname);
                            hdTop.Value = Resources.ValidationResources.RBTop;
                            return;
                        }
                        this.hsName.Value = this.txtshortname.Text.Trim();
                        if (hsName.Value == default)
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.ReqEnterShortN.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.ReqEnterShortN, this, dbUtilities.MsgLevel.Warning);
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ReqEnterShortN.ToString, Me)
                            // txtshortname.Focus()
                            this.SetFocus(this.txtshortname);
                            hdTop.Value = Resources.ValidationResources.RBTop;
                            return;
                        }

                        if (this.chkRequester.Checked == true)
                        {
                            this.hAct.Value = "Y";      // Deactivation
                        }
                        else
                        {
                            hAct.Value = "N";
                        }      // Activation
                        this.hFill.Value = "0";

                        if (((CheckBox)grdDetail.Items[e.Item.ItemIndex].Cells[0].FindControl("Chkselect")).Checked == false)
                        {
                            // LibObj.MsgBox1(Resources.ValidationResources.Firstselcat.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.Firstselcat, this, dbUtilities.MsgLevel.Warning);
                        }
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.Firstselcat.ToString, Me)
                        else
                        {
                            // Dim returnScript As String = ""
                            // returnScript &= "<script language='javascript' type='text/javascript'>"
                            // returnScript &= "javascript:callUVal5('btnFillgrddetail');"
                            // returnScript &= "<" & "/" & "script>"
                            // Page.RegisterStartupScript("", returnScript)
                            txtdesignationid.Value = grdDetail.Items[e.Item.ItemIndex].Cells[2].Text;
                            hdnMemCate.Value = "Y";
                            // ClientScript.RegisterStartupScript(Me.Page, GetType(Page), "Catewise", "callUVal5();", True)
                            // ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "callUVal5();", True)
                        }

                        break;
                    }
            }
        }

        protected void optgName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                hdflag.Value = "0";
                chkissuestatus();
                if (hdflag.Value == "1")
                {
                    optgName.Checked = false;
                    optICategory.Checked = true;
                    return;
                }
                if (optgName.Checked == true)
                {
                    // Me.optgName.Focus()
                    this.SetFocus(this.optgName);
                    txtreservation.Value = string.Empty;
                    txtfineperday.Value = string.Empty;
                   
                    txtEnable();
                    loadingStatus();
                 
                    grdDetail.Visible = false;
                }
            }
            catch (Exception ex)
            {
                
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        protected void optICategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                hdflag.Value = "0";
                chkissuestatus();
                if (hdflag.Value == "1")
                {
                    optICategory.Checked = false;
                    optgName.Checked = true;
                    return;
                }
                if (optICategory.Checked == true)
                {
                    // Me.optICategory.Focus()
                    this.SetFocus(this.optICategory);
                   
                    txtDisable();
                    loadingStatus();
                    // grdDetail.Columns(3).Visible = True
                    grdDetail.Visible = true;
                }
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
            }
        }

        public void txtDisable()
        {
            this.txtfineperday.Visible = false;
            this.txtmaxissuebook.Visible = false;
            this.txtmaxissueday.Visible = false;
            this.txtreservation.Visible = false;
            this.txtfineperdayJ.Visible = false;
            this.txtmaxissueJournal.Visible = false;
            this.txtmaxissuedayJ.Visible = false;
            this.txtreservationJ.Visible = false;
            this.txtDays1.Visible = false;
            this.txtfineperday1.Visible = false;
            this.txtDays2.Visible = false;
            this.txtfineperday2.Visible = false;
            // ---journal
            this.txtDays1J.Visible = false;
            this.txtfineperdayJ1.Visible = false;
            this.txtDays2J.Visible = false;
            this.txtfineperdayJ2.Visible = false;
            this.Label2.Visible = false;
            this.Label3.Visible = false;
            this.Label6.Visible = false;
            this.Label7.Visible = false;
            this.Label4.Visible = false;
            this.Label8.Visible = false;
            this.Label24.Visible = false;
            this.Label25.Visible = false;
            this.lblcurr.Visible = false;
            this.lblCurrency.Visible = false;
            this.Label27.Visible = false;
            this.Label29.Visible = false;
            this.Label30.Visible = false;
            this.Label28.Visible = false;
            this.lblStatus.Visible = true;

        }
        public void txtEnable()
        {
            this.txtfineperday.Visible = true;
            this.txtmaxissuebook.Visible = true;
            this.txtmaxissueday.Visible = true;
            this.txtreservation.Visible = true;
            this.txtfineperdayJ.Visible = true;
            this.txtmaxissueJournal.Visible = true;
            this.txtmaxissuedayJ.Visible = true;
            this.txtreservationJ.Visible = true;
            this.txtDays1.Visible = true;
            this.txtfineperday1.Visible = true;
            this.txtDays2.Visible = true;
            this.txtfineperday2.Visible = true;
            // ---journal
            this.txtDays1J.Visible = true;
            this.txtfineperdayJ1.Visible = true;
            this.txtDays2J.Visible = true;
            this.txtfineperdayJ2.Visible = true;
            this.Label2.Visible = true;
            this.Label3.Visible = true;
            this.Label6.Visible = true;
            this.Label7.Visible = true;
            this.Label4.Visible = true;
            this.Label8.Visible = true;
            this.Label24.Visible = true;
            this.Label25.Visible = true;
            this.lblcurr.Visible = true;
            this.lblCurrency.Visible = true;
            this.Label27.Visible = true;
            this.Label29.Visible = true;
            this.Label30.Visible = true;
            this.Label28.Visible = true;
            this.lblStatus.Visible = false;
            // Me.Label31.Visible = True
            // Me.Label33.Visible = True
            // Me.Label34.Visible = True
            // Me.Label32.Visible = True

        }
       

        protected void btnFillgrddetail_Click(object sender, EventArgs e)
        {
            try
            {
                var classcon = new OleDbConnection(retConstr(""));
                classcon.Open();
                // loadingStatus()
                // fillgrddetail(Trim(Me.txtclassname.Text.Replace("'", "''")), classcon)
                classcon.Close();
                classcon.Dispose();
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
            }
        }

        public void chkissuestatus()
        {

            if (cmdsubmit.Text == Resources.ValidationResources.bUpdate.ToString())
            {
                var con = new OleDbConnection(retConstr(""));
                con.Open();
                if (LibObj.checkChildExistancewc("issuedbookstatus", "CircUserManagement", "issuedbookstatus > 0 AND classname=N'" + txtclassname.Text.Trim() + "'", con) == true)
                {
                    hdflag.Value = "1";
                    LibObj.MsgBox1(Resources.ValidationResources.pchange.ToString(), this); // Child records found
                    con.Dispose();
                    con.Close();
                    return;
                }
            }
        }

       
        protected void grdDetail_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // grdDetail.Focus()
            this.SetFocus(this.grdDetail);

        }

        public void Reset()
        {
            try
            {
                this.txtshortname.Text = string.Empty;
                this.txtclassname.Text = string.Empty;
                this.txtfineperday.Value = string.Empty;
                this.txtmaxissuebook.Value = string.Empty;
                this.txtmaxissueday.Value = string.Empty;
                this.txtreservation.Value = string.Empty;
                hdnMemCate.Value = "";
                this.txtfineperdayJ.Value = string.Empty;
                this.txtmaxissueJournal.Value = string.Empty;
                this.txtmaxissuedayJ.Value = string.Empty;
                this.txtMaxValue.Text = string.Empty;
                this.txtreservationJ.Value = string.Empty;
                // --pranav---------
                this.txtDays1.Value = string.Empty;
                this.txtfineperday1.Value = string.Empty;
                this.txtDays2.Value = string.Empty;
                this.txtfineperday2.Value = string.Empty;
                // ---journal
                this.txtDays1J.Value = string.Empty;
                this.txtfineperdayJ1.Value = string.Empty;
                this.txtDays2J.Value = string.Empty;
                this.txtfineperdayJ2.Value = string.Empty;
                // ----------------------------
                this.ChkActive.Checked = false;
                this.chkRequester.Checked = false;
                this.DataGrid1.SelectedIndex = -1;
                this.cmdsubmit.Text = Resources.ValidationResources.bSave.ToString();
                Session["ClassName"] = "";
                hdTop.Value = Resources.ValidationResources.RBTop;
                // txtclassname.Focus()
                this.SetFocus(this.txtclassname);
                var CategoryLoadingStatusCon = new OleDbConnection(retConstr(""));
                CategoryLoadingStatusCon.Open();
                var CategoryLoadingStatusds = new DataSet();
                //CategoryLoadingStatusds = LibObj.PopulateDataset("select id,Category_LoadingStatus from CategoryLoadingStatus  where id <> 0 order by Category_LoadingStatus", "CategoryLoadingStatusds", CategoryLoadingStatusCon);
                if (CategoryLoadingStatusds.Tables["CategoryLoadingStatusds"].Rows.Count > 0)
                {
                    grdDetail.DataSource = CategoryLoadingStatusds.Tables["CategoryLoadingStatusds"].DefaultView;
                    grdDetail.DataBind();
                }
                else
                {
                    grdDetail.DataSource = dt;
                    grdDetail.DataBind();
                }
                if (tmpcondition == "Y")
                {
                    this.cmddelete.Visible = false;
                    this.cmdsubmit.Visible = false;
                }
                else if (tmpcondition == "N")
                {
                    this.cmddelete.Visible = true;
                    this.cmdsubmit.Visible = true;
                }
                else
                {
                    lblTitle.Text = Resources.ValidationResources.MembGp;
                    this.cmdsubmit.Visible = false;
                    this.cmddelete.Visible = false;
                    // Me.cmdReturn.Disabled = True
                    Session["NFormDW"] = "dLogout";
                }
                FillGrid();
                cmddelete.Visible = true;
                CategoryLoadingStatusds.Dispose();
                CategoryLoadingStatusCon.Close();
                CategoryLoadingStatusCon.Dispose();
                this.optICategory.Checked = false;
                this.optgName.Checked = true;
                // Me.grdDetail.Columns(3).Visible = False
                grdDetail.Visible = false;
                txtEnable();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                // msglabel.Text = ex.Message
            }
        }


    }
}