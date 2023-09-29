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
    public partial class rptvendor : BaseClass
    {
        insertLogin LibObj1 = new insertLogin();
        private dbUtilities message = new dbUtilities();
        libGeneralFunctions LibObj = new libGeneralFunctions();
        DBIStructure DBI = new DBIStructure();
        protected void Page_Load(object sender, EventArgs e)
        {
            var vendorCon = new OleDbConnection(retConstr(""));
            vendorCon.Open();
            var vendorDs = new DataSet();
            try
            {
                // cmdReturn.CausesValidation = False
                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)
                if (!Page.IsPostBack)
                {
                    this.SetFocus(txtcodeVen);
                //    lblTitle.Text = Request.QueryString["title"];
                  //  tmpcondition = Request.QueryString["condition"];
//                    vendorDs = LibObj.PopulateDataset("SELECT distinct code ,* from ViewRptVendor", "vendor", vendorCon);
                }
                msglabel.Visible = false;
             //   this.txtcodeVen.Attributes.Add("onkeyDown", "txtcode_onkeydown();");
               // this.txtcodeVen.Attributes.Add("onkeyDown", "txtname_onkeydown();");
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                vendorDs.Dispose();
                vendorCon.Close();
                vendorCon.Dispose();
            }
        }

        protected void btnSb_Click(object sender, EventArgs e)
        {
            var gCl = new GlobClassTr();
            string Qer = "select vendorcode,vendorname from vendormaster where vendorid= " + hdVid.Value;
            gCl.TrOpen();
            DataTable dt = gCl.DataT(Qer);
            gCl.TrClose();
            txtcodeVen.Text = dt.Rows[0][0].ToString();
            txtnameVen.Text = dt.Rows[0][1].ToString();
        }

        protected void cmdshow2_Click(object sender, EventArgs e)
        {
            var vendorrepcon = new OleDbConnection(retConstr(""));
            vendorrepcon.Open();
            var vendorrepds = new DataSet();
            try
            {
                this.SetFocus(txtcodeVen);
                string searchqry;
                searchqry = "SELECT distinct code,* from ViewRptVendor ";
                string filterqry;
                filterqry = "";
                if (this.txtcodeVen.Text != string.Empty)
                {
                    if (!string.IsNullOrEmpty(filterqry))
                    {
                        filterqry = filterqry;
                    }
                    filterqry = filterqry + " (ViewRptVendor.code=N'" + this.txtcodeVen.Text + "')";
                }
                if (txtnameVen.Text != string.Empty)
                {
                    if (!string.IsNullOrEmpty(filterqry))
                    {
                        filterqry = filterqry + " " + this.Dd1.SelectedItem.Value + "";
                    }

                    filterqry = filterqry + " (ViewRptVendor.code = N'" + txtcodeVen.Text.Trim() + "')";
                }
                string newsearchary;
                if (string.IsNullOrEmpty(filterqry))
                {
                    newsearchary = searchqry;
                }
                else
                {
                    newsearchary = searchqry + " " + "where" + filterqry;
                }
                Session["venqry"] = searchqry;
                vendorrepds = LibObj.PopulateDataset(newsearchary, "vendorrep", vendorrepcon);
                if (vendorrepds.Tables["vendorrep"].Rows.Count > 0)
                {
                    // 'jitendra
                    var myReportDocument = new ReportDocument();
                    myReportDocument.Load(Server.MapPath(@"Reports\rptvendor.rpt"));
                    myReportDocument.SetDataSource(vendorrepds.Tables["vendorrep"]);
                    // Field
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["gram1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["LibEmail1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["fax1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["phoneno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["libraryname1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 10.0f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["code1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["name1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["localadd1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["peradd1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["email1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["phone1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["web1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    // Formula
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.ReportObjects["addressformula1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 8.5f));
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)myReportDocument.ReportDefinition.ReportObjects["frmheading1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 12.0f));
                    // Textbox
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text8"]).Text = Resources.ValidationResources.rptGram;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text8"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]           ).Text = Resources.ValidationResources.rptMail   ;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text10"]).Text = Resources.ValidationResources.rptFax;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text10"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text11"]).Text = Resources.ValidationResources.PhNo;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text11"]  ).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text12"]  ).Text = lblTitle.Text;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text12"]  ).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 10.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text1"]   ).Text = Resources.ValidationResources.LVenC;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text2"]).Text = Resources.ValidationResources.LNam;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text2"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text3"]).Text = Resources.ValidationResources.LLocalAddr;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text3"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).Text = Resources.ValidationResources.LPerAddr;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).Text = Resources.ValidationResources.rptMail;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]   ).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).Text = Resources.ValidationResources.PhNo;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text7"]).Text = Resources.ValidationResources.LWebAddr;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)myReportDocument.ReportDefinition .Sections["Section2"].ReportObjects["Text7"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1, 9.0f));
                    // Crvendor.ReportSource = myReportDocument
                    // Crvendor.DataBind()
                    // Crvendor.RefreshReport()
                    ExportOptions exportOpts1 = myReportDocument.ExportOptions;
                    myReportDocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    myReportDocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    myReportDocument.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)myReportDocument.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\rptPublisherDetailsN.pdf");
                    myReportDocument.Export();
                    myReportDocument.Close();
                    myReportDocument.Dispose();
                    {
                        var withBlock = Response;
                        withBlock.ClearContent();
                        withBlock.ClearHeaders();
                        withBlock.ContentType = "application/pdf";
                        withBlock.AppendHeader("Content-Disposition", "attachment; filename=VenderReport.pdf");
                        withBlock.WriteFile(@"reportTemp\rptPublisherDetailsN.pdf");
                        withBlock.Flush();
                        withBlock.End();
                        withBlock.Close();
                    }
                    File.Delete(Server.MapPath(@"reportTemp\rptPublisherDetailsN.pdf"));
                }
                else
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.rNotFound, this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                //cmdReset.Disabled = false;
                cmdReset2.Enabled=true;
                return;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                var excp = ex.Message;//
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                        excp += ";" + ex.InnerException.Message;
                }
                                      //+ ";" + ex.InnerException ?? ex.InnerException.Message;
                // Me.msglabel.Text = ex.Message
                message.PageMesg(excp, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                vendorrepds.Dispose();
                vendorrepcon.Close();
                vendorrepcon.Dispose();
            }
        }

        protected void cmdReset2_Click(object sender, EventArgs e)
        {
            this.txtcodeVen.Text = string.Empty;
            this.txtnameVen.Text = string.Empty;
            this.SetFocus(txtcodeVen);
        }

    }
}