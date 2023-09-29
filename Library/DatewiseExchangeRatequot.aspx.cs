using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class DatewiseExchangeRatequot : BaseClass
    {
        private static string tmpcondition;
        libGeneralFunctions LibObj = new libGeneralFunctions();
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                msglabel.Visible = false;
                var Con = new OleDbConnection(retConstr(""));
                var currds = new DataSet();
                try
                {
                   
                    if (!Page.IsPostBack)
                    {
                        Con.Open();
                        // Me.CrystalReportViewer1.Visible = False
                        lblTitle.Text = Request.QueryString["title"];

                      
                        txttodate.Text =string. Format("{0:dd-MMM-yyyy}",   System.DateTime.Today);
                        txtfromdate.Text = string.Format("{0:dd-MMM-yyyy}", System.DateTime.Today.AddDays(-7));


                        currds = LibObj.PopulateDataset("Select distinct currencycode,currencyname from exchangemaster", "currency", Con);
                        if (currds.Tables[0].Rows.Count > 0)
                        {
                            grdCurrency.DataSource = currds;
                            grdCurrency.DataBind();
                        }

                        else
                        {
                            var dt = new DataTable();
                            grdCurrency.DataSource = dt;
                            grdCurrency.DataBind();
                            dt.Dispose();
                        }
                        //hdCulture.Value = Request.Cookies["UserCulture"].Value;
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
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                    Con.Dispose();
                    currds.Dispose();
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked == true)
                {
                    // chkSelectAll.Focus()
                    this.SetFocus(chkSelectAll);
                    int cnt1;
                    if (grdCurrency.Items.Count > 0)
                    {
                        var loopTo = grdCurrency.Items.Count - 1;
                        for (cnt1 = 0; cnt1 <= loopTo; cnt1++)
                        {
                            var ctl = new CheckBox();
                            ctl = (CheckBox)grdCurrency.Items[cnt1].Cells[0].FindControl("Chkselect");
                            ctl.Checked = true;
                        }
                    }
                }
                else
                {
                    // chkSelectAll.Focus()
                    this.SetFocus(chkSelectAll);
                    int cnt1;
                    if (grdCurrency.Items.Count > 0)
                    {
                        var loopTo1 = grdCurrency.Items.Count - 1;
                        for (cnt1 = 0; cnt1 <= loopTo1; cnt1++)
                        {
                            var ctl = new CheckBox();
                            ctl = (CheckBox)grdCurrency.Items[cnt1].Cells[0].FindControl("Chkselect");
                            ctl.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdSearch1_Click(object sender, EventArgs e)
        {
            var myConnection = new OleDbConnection(retConstr(""));
            myConnection.Open();
            try
            {
                labMesg.Text = "";
                // Me.txtfromdate.Focus()
                //this.SetFocus(txtfromdate);
                string strCurrency = string.Empty;
                bool isSelected = false;
                if (grdCurrency.Items.Count <= 0)
                {

                    
                    message.PageMesg(Resources.ValidationResources.CurrNotFnd, this, dbUtilities.MsgLevel.Warning);
                    // Response.Redirect(Request.Url.ToString())

                    return;
                }
                else
                {

                    int iCounter;
                    var loopTo = grdCurrency.Items.Count - 1;
                    for (iCounter = 0; iCounter <= loopTo; iCounter++)
                    {
                        var ctl = new CheckBox();
                        ctl = (CheckBox)grdCurrency.Items[iCounter].Cells[0].FindControl("Chkselect");
                        if (ctl.Checked == true)
                        {
                            isSelected = true;
                            if (string.IsNullOrEmpty(strCurrency))
                            {
                                strCurrency += "'" + grdCurrency.Items[iCounter].Cells[1].Text + "'";
                            }
                            else
                            {
                                strCurrency += ",'" + grdCurrency.Items[iCounter].Cells[1].Text + "'";
                            }
                        }
                    }
                }
                string str = string.Empty;
                if (isSelected == false)
                {
                   
                    message.PageMesg(Resources.ValidationResources.GrSel, this, dbUtilities.MsgLevel.Warning);
                }
                // Response.Redirect(Request.Url.ToString())

                else
                {
                    if (txtfromdate.Text != "" & txttodate.Text != "")
                    {
                        str = "Select * from rpexchangemaster where EffectiveFrom >='" + (txtfromdate.Text) + "' and EffectiveFrom<='" + (txttodate.Text) + "' AND CurrencyCode IN(" + strCurrency + ")";
                    }

                    if (txtfromdate.Text == "" & txttodate.Text != "")
                    {
                        str = "Select * from rpexchangemaster where EffectiveFrom<='" + (txttodate.Text) + "' AND CurrencyCode IN(" + strCurrency + ")";
                    }

                    if (txtfromdate.Text != "" & txttodate.Text == "")
                    {
                        str = "Select * from rpexchangemaster where EffectiveFrom >='" + (txtfromdate.Text) + "' and EffectiveFrom<='" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "' AND CurrencyCode IN(" + strCurrency + ")";
                    }

                    if (lblTitle.Text.Substring(4) == "DATE")
                    {
                        var MyCommand = new OleDbCommand();
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = str;
                        MyCommand.CommandType = CommandType.Text;
                        var MyDA = new OleDbDataAdapter();
                        MyDA.SelectCommand = MyCommand;
                        var myDS = new DataSet();
                        MyDA.Fill(myDS, "ccp");
                        if (myDS.Tables["ccp"].Rows.Count > 0)
                        {
                            var O_rpt = new ReportDocument(); // ReportDatewiseExchange
                            O_rpt.Load(Server.MapPath(@"Reports\ReportDatewiseExchange.rpt"));
                            O_rpt.SetDataSource(myDS.Tables["ccp"]);
                            // Field()
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["gram1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["email1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["fax1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["phoneno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["libraryname1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 10.0f));
                            
                            ((FieldObject)O_rpt.ReportDefinition.Sections["GroupHeaderSection2"].ReportObjects["GroupNameCurrencyName1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["BankRate1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["GocRate1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt.ReportDefinition.ReportObjects["dt11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            // Formula
                            ((FieldObject)O_rpt.ReportDefinition.ReportObjects["header1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 12.0f));
                            ((FieldObject)O_rpt.ReportDefinition.ReportObjects["addressformula1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            // Textbox

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text1"]).Text = Resources.ValidationResources.rptGram.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text2"]).Text = Resources.ValidationResources.rptMail.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text2"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text7"]).Text = Resources.ValidationResources.rptFax.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text7"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text8"]).Text = Resources.ValidationResources.PhNo.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text8"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]).Text = this.lblTitle.Text;
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 10.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).Text = Resources.ValidationResources.LDate.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text3"]).Text = Resources.ValidationResources.LCurrName.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text3"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).Text = Resources.ValidationResources.LGRate.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).Text = Resources.ValidationResources.LBRate.ToString();
                            ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));
                            O_rpt.DataDefinition.FormulaFields["cusDateFrmt"].Text = Resources.ValidationResources.rptDateFrmt.ToString();

                            ((FieldObject)O_rpt.ReportDefinition.ReportObjects["cusDateFrmt1"]).ObjectFormat.EnableSuppress = true;


                           
                            var exportOpts1 = O_rpt.ExportOptions;
                            O_rpt.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            O_rpt.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            O_rpt.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                            ((DiskFileDestinationOptions)O_rpt.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\rptPublisherDetailsN.pdf");
                            O_rpt.Export();
                            O_rpt.Close();
                            O_rpt.Dispose();
                            {
                                var withBlock = Response;
                                withBlock.ClearContent();
                                withBlock.ClearHeaders();
                                withBlock.ContentType = "application/pdf";
                                withBlock.AppendHeader("Content-Disposition", "attachment; filename=Date wise Exchange Rate.pdf");
                                withBlock.WriteFile(@"reportTemp\rptPublisherDetailsN.pdf");
                                withBlock.Flush();
                                withBlock.End();
                                withBlock.Close();
                            }
                            File.Delete(Server.MapPath(@"reportTemp\rptPublisherDetailsN.pdf"));
                        }

                        else
                        {
                           
                            message.PageMesg(Resources.ValidationResources.RecNFBwn, this,              dbUtilities.MsgLevel.Warning);

                            // Response.Redirect(Request.Url.ToString())

                            return;
                        }
                    }
                    else
                    {

                        var MyCommand = new OleDbCommand();
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = str;
                        MyCommand.CommandType = CommandType.Text;
                        var MyDA = new OleDbDataAdapter();
                        MyDA.SelectCommand = MyCommand;
                        var myDS = new DataSet();
                        MyDA.Fill(myDS, "ccp");
                        if (myDS.Tables["ccp"].Rows.Count > 0)
                        {
                            // You have to use the same name as that of your Dataset that you created during design time
                            var O_rpt1 = new ReportDocument(); // ReportCurrencywiseRate
                            O_rpt1.Load(Server.MapPath(@"Reports\ReportCurrencywiseRate.rpt"));
                            O_rpt1.SetDataSource(myDS.Tables["ccp"]);
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["gram1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["email1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["fax1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["phoneno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["libraryname1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 10.0f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["GroupNameCurrencyName1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            // CType(O_rpt1.ReportDefinition.Sections("GroupHeaderSection2").ReportObjects("GroupNameEffectiveFromdaily1"), CrystalDecisions.CrystalReports.Engine.FieldObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 8.5F))
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["BankRate1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["GocRate1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            ((FieldObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["institutename1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 12.0f));
                            ((FieldObject)O_rpt1.ReportDefinition.ReportObjects["dt11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            // Formula
                            // CType(O_rpt1.ReportDefinition.ReportObjects("headerformula1"), CrystalDecisions.CrystalReports.Engine.FieldObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 12.0F))
                            ((FieldObject)O_rpt1.ReportDefinition.ReportObjects["addressformula1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                            // Textbox
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text7"]).Text = Resources.ValidationResources.rptGram.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text7"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text8"]).Text = Resources.ValidationResources.rptMail.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text8"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]).Text = Resources.ValidationResources.rptFax.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text10"]).Text = Resources.ValidationResources.PhNo.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text10"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text3"]).Text = Resources.ValidationResources.LCurrName.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section1"].ReportObjects["Text3"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).Text = Resources.ValidationResources.LDate.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).Text = Resources.ValidationResources.LBRate.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).Text = Resources.ValidationResources.LGRate.ToString();
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text12"]).Text = lblTitle.Text;
                            ((TextObject)O_rpt1.ReportDefinition.Sections["Section2"].ReportObjects["Text12"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 10.0f));
                            O_rpt1.DataDefinition.FormulaFields["cusDateFrmt "].Text = Resources.ValidationResources.rptDateFrmt.ToString();

                            ((FieldObject)O_rpt1.ReportDefinition.ReportObjects["cusDateFrmt1"]).ObjectFormat.EnableSuppress = true;


                            var exportOpts1 = O_rpt1.ExportOptions;
                            O_rpt1.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            O_rpt1.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            O_rpt1.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                            ((DiskFileDestinationOptions)O_rpt1.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\Currency wise exchange Rate.pdf");
                            O_rpt1.Export();
                            O_rpt1.Close();
                            O_rpt1.Dispose();
                            {
                                var withBlock1 = Response;
                                withBlock1.ClearContent();
                                withBlock1.ClearHeaders();
                                withBlock1.ContentType = "application/pdf";
                                withBlock1.AppendHeader("Content-Disposition", "attachment; filename=Currency wise exchange Rate.pdf");
                                withBlock1.WriteFile(@"reportTemp\Currency wise exchange Rate.pdf");
                                withBlock1.Flush();
                                withBlock1.End();
                                withBlock1.Close();
                            }
                            File.Delete(Server.MapPath(@"reportTemp\Currency wise exchange Rate.pdf"));
                        }

                        else
                        {
                            // Response.Write(Resources.ValidationResources.RecNFBwn.ToString & " " & txtfromdate.Text & " " & Resources.ValidationResources.LAnd.ToString & " " & txttodate.Text)
                            // LibObj.MsgBox1(Resources.ValidationResources.RecNFBwn.ToString & " " & txtfromdate.Text & " " & Resources.ValidationResources.LAnd.ToString & " " & txttodate.Text, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.RecNFBwn.ToString & " " & txtfromdate.Text & " " & Resources.ValidationResources.LAnd.ToString & " " & txttodate.Text, Me)
                            message.PageMesg(Resources.ValidationResources.RecNFBwn, this, dbUtilities.MsgLevel.Warning);

                            // Response.Redirect(Request.Url.ToString())
                            return;
                        }
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
                myConnection.Dispose();
            }
        }

        protected void cmdreset1_Click(object sender, EventArgs e)
        {
            var Con = new OleDbConnection(retConstr(""));
            Con.Open();
            var currds = new DataSet();
            try
            {
                // Me.txtfromdate.Focus()
                //this.SetFocus(txtfromdate); 

                txttodate.Text = string.Format("{0:dd-MMM-yyyy}", System.DateTime.Today);
                txtfromdate.Text = string.Format("{0:dd-MMM-yyyy}",System.DateTime.Today.AddDays(-7));
                this.chkSelectAll.Checked = false;
                currds = LibObj.PopulateDataset("Select distinct currencycode,currencyname from exchangemaster", "currency", Con);



                if (currds.Tables[0].Rows.Count > 0)
                {
                    grdCurrency.DataSource = currds;
                    grdCurrency.DataBind();
                }

                else
                {
                    var dt = new DataTable();
                    grdCurrency.DataSource = dt;
                    grdCurrency.DataBind();
                    dt.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
                Con.Dispose();
                currds.Dispose();
            }
        }

        protected void nreco1_Click(object sender, EventArgs e)
        {
            var Con = new OleDbConnection(retConstr(""));
            Con.Open();
            try
            {
                this.SetFocus(txtfromdate);
                string strCurrency = string.Empty;
                bool isSelected = false;
                if (grdCurrency.Items.Count <= 0)
                {

                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.CurrNotFnd.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.CurrNotFnd, this, dbUtilities.MsgLevel.Warning);

                    return;
                }
                else
                {

                    int iCounter;
                    var loopTo = grdCurrency.Items.Count - 1;
                    for (iCounter = 0; iCounter <= loopTo; iCounter++)
                    {
                        var ctl = new CheckBox();
                        ctl = (CheckBox)grdCurrency.Items[iCounter].Cells[0].FindControl("Chkselect");
                        if (ctl.Checked == true)
                        {
                            isSelected = true;
                            if (string.IsNullOrEmpty(strCurrency))
                            {
                                strCurrency += "'" + grdCurrency.Items[iCounter].Cells[1].Text + "'";
                            }
                            else
                            {
                                strCurrency += ",'" + grdCurrency.Items[iCounter].Cells[1].Text + "'";
                            }
                        }
                    }
                }
                string str = string.Empty;
                if (isSelected == false)
                {
                    
                    message.PageMesg(Resources.ValidationResources.GrSel, this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    if (txtfromdate.Text != "" & txttodate.Text != "")
                    {
                        str = "Select * from rpexchangemaster where EffectiveFrom >='" + (txtfromdate.Text) + "' and EffectiveFrom<='" + (txttodate.Text) + "' AND CurrencyCode IN(" + strCurrency + ")";
                    }

                    if (txtfromdate.Text == "" & txttodate.Text != "")
                    {
                        str = "Select * from rpexchangemaster where EffectiveFrom<='" + (txttodate.Text) + "' AND CurrencyCode IN(" + strCurrency + ")";
                    }

                    if (txtfromdate.Text != "" & txttodate.Text == "")
                    {
                        str = "Select * from rpexchangemaster where EffectiveFrom >='" + (txtfromdate.Text) + "' and EffectiveFrom<='" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "' AND CurrencyCode IN(" + strCurrency + ")";
                    }


                    var currds = new DataSet();
                    var dt = new DataTable();
                    var MyCommand = new OleDbCommand();
                    MyCommand.Connection = Con;
                    MyCommand.CommandText = str;
                    MyCommand.CommandType = CommandType.Text;
                    var MyDA = new OleDbDataAdapter();
                    MyDA.SelectCommand = MyCommand;
                    // Dim myDS As New ExchangeRateDS
                    MyDA.Fill(dt);
                    var dtt = new DataTable();
                    if (dt.Rows.Count <= 0)
                    {
                       
                        message.PageMesg(Resources.ValidationResources.NRcdFound, this, dbUtilities.MsgLevel.Warning);
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {


                            dtt.Columns.Add("S.No");
                            dtt.Columns.Add("Currency Name");
                            dtt.Columns.Add("Date");
                            dtt.Columns.Add("Bank Rate");
                            dtt.Columns.Add("GOC Rate");
                            for (int k = 0, loopTo1 = dt.Rows.Count - 1; k <= loopTo1; k++)
                            {
                                var row = dtt.NewRow();
                                row["S.No"] = k + 1;
                                row["Currency Name"] = dt.Rows[k]["CurrencyName"];
                                row["Date"] = dt.Rows[k]["EffectiveFrom"];
                                row["Bank Rate"] = dt.Rows[k]["BankRate"].ToString();
                                row["GOC Rate"] = dt.Rows[k]["GocRate"];
                                dtt.Rows.Add(row);
                            }
                            int r;
                            var loopTo2 = dtt.Rows.Count - 1;
                            for (r = 0; r <= loopTo2; r++)
                            {
                                var loopTo3 = dtt.Columns.Count - 1;
                                for (int c = 0; c <= loopTo3; c++)
                                {
                                    dtt.Rows[r][c] = dtt.Rows[r][c].ToString().Replace("''''", "`");
                                    dtt.Rows[r][c] = dtt.Rows[r][c].ToString().Replace("'", "`");
                                    
                                }

                            }

                            for (int l = 0, loopTo4 = grdrep.Rows.Count - 1; l <= loopTo4; l++)
                            {
                                for (int il = 0, loopTo5 = grdrep.Rows[l].Cells.Count - 1; il <= loopTo5; il++)
                                {
                                    // For n As Integer = 0 To dtt.Columns.Count - 1
                                    for (int m = 0, loopTo6 = dtt.Rows.Count - 1; m <= loopTo6; m++)
                                        // Try


                                        grdrep.Rows[m].Cells[il].Style["padding"] = "5px";

                                }
                            }

                        }

                        ins.Text = dt.Rows[0]["institutename"].ToString();
                        grm.Text = dt.Rows[0]["gram"].ToString();

                        curdt.Text = String.Format("ddMMMyyyy",System.DateTime.Now);
                        cit.Text = dt.Rows[0]["city"].ToString();
                        // pin.Text = ds.Tables(0).Rows(0)("pincode").ToString()
                        addr.Text = dt.Rows[0]["address"].ToString();
                        stat.Text = dt.Rows[0]["state"].ToString();
                        libranm.Text = dt.Rows[0]["libraryname"].ToString();
                        phn.Text = dt.Rows[0]["phoneno"].ToString();
                        fx.Text = dt.Rows[0]["fax"].ToString();

                        // For i = 0 To ds.Tables(0).Rows.Count - 1


                        // Next
                        grdrep.DataSource = dtt;
                        grdrep.DataBind();
                        //AddHeaders();
                        dt.Dispose();

                        foreach (GridViewRow row in grdrep.Rows)

                            row.Style.Add("page-break-inside", "avoid");
                        
                        if (lblTitle.Text.Substring(4) == "DATE")
                        {
                            lblvp.Text = "Date Wise Reports";
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "pdfClick();", true);
                        }
                        else
                        {
                            lblvp.Text = "Currency Wise Reports";
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "pdfClick();", true);
                        }
                    }

                    
                    message.PageMesg(Resources.ValidationResources.RecNFBwn, this, dbUtilities.MsgLevel.Warning);

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
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
                Con.Dispose();
            }
        }
    }
}