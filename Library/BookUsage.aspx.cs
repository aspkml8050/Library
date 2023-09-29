using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.App_Code.CSharp;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class BookUsage : BaseClass
    {
        OleDbCommand cmd = null;
        OleDbDataAdapter ad = null;
        OleDbConnection con = new OleDbConnection();
//        DataLayer.SqlDataAccess ObjSDA = new DataLayer.SqlDataAccess();
        libGeneralFunctions LibObj = new libGeneralFunctions();
        messageLibrary msg = new messageLibrary();
                dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdPrint_Click(object sender, EventArgs e)
        {
            con.ConnectionString = retConstr("");
            con.Open();

            // OleDbCommand cmd = new OleDbCommand();
            //cmd.Connection = con;

            //cmd.CommandText = "SELECT * FROM librarysetupinformation ";
            DataSet dss = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM librarysetupinformation", con);
            da.Fill(dss, "ins_nm");

            DataSet ds= new DataSet();
            try
            {
                if (rbldetsum.SelectedIndex == 0)
                {

                    if (txtFrom.Text != "")
                    {
                        string query = "select accno,booktitle,convert(varchar,Issuedt,106) as Issuedt,Author,NoOfTrans from BookUsageV where Issuedt >='" + txtFrom.Text + "' AND Issuedt <='" + txtTo.Text + "'";
                        ad = new OleDbDataAdapter(query, con);
                        ad.Fill(ds, "Book_Usage");
                        if (ds.Tables["Book_Usage"].Rows.Count <= 0)
                        {
                            //LibObj.MsgBox1("No Record Found !", this);
                            //msg.showHtml_Message(Library.messageLibrary.msgType.Warning, "No Record Found !", this);
                            message.PageMesg("No Record Found !", this, dbUtilities.MsgLevel.Warning);
                            return;
                        }





                        ReportDocument RptDoc1 = new ReportDocument();
                        RptDoc1.Load(Server.MapPath("Reports\\BookUsage_report.rpt"));
                        RptDoc1.SetDataSource(ds);
                        if (dss.Tables["ins_nm"].Rows.Count > 0)
                        {
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtgrm"]).Text = dss.Tables["ins_nm"].Rows[0][9].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtfax"]).Text = dss.Tables["ins_nm"].Rows[0][7].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtphone"]).Text = dss.Tables["ins_nm"].Rows[0][6].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtinst"]).Text = dss.Tables["ins_nm"].Rows[0][0].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtlib"]).Text = dss.Tables["ins_nm"].Rows[0][1].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtadd"]).Text = dss.Tables["ins_nm"].Rows[0][2].ToString();

                        }
                        //this.CrystRptSchlr_MultiOpt.ReportSource = RptDoc1;
                        //CrystRptSchlr_MultiOpt.DataBind();
                        //CrystRptSchlr_MultiOpt.RefreshReport();
                        Session["Rptdoc"] = RptDoc1;
                        ExportOptions myexportoptions_default = RptDoc1.ExportOptions;
                        RptDoc1.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        RptDoc1.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        RptDoc1.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                        ((DiskFileDestinationOptions)RptDoc1.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath("reportTemp\\BookUsage_report13.pdf");
                        RptDoc1.Export();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + "BookUsage_report13.pdf"));

                        Response.WriteFile(Server.MapPath("~/reportTemp/BookUsage_report13.pdf"));

                        Response.Flush();
                        Response.End();

                    }
                }
                else if (rbldetsum.SelectedIndex == 1)
                {

                    if (txtFrom.Text != "")
                    {
                        string query = "select accno,booktitle,Author,sum(NoOfTrans) NoOfTrans from BookUsageV where Issuedt >='" + txtFrom.Text + "' AND Issuedt <='" + txtTo.Text + "' group by accno,booktitle,Author order by accno";
                        var com = new OleDbCommand(query, con);
                        com.CommandTimeout = 120;
                        ad = new OleDbDataAdapter(com);

                        ad.Fill(ds, "Book_Usage");
                        if (ds.Tables["Book_Usage"].Rows.Count <= 0)
                        {
                            // LibObj.MsgBox1("No Record Found !", this);
                            //msg.showHtml_Message(Library.messageLibrary.msgType.Warning, "No Record Found !", this);
                            message.PageMesg("No Record Found !", this, dbUtilities.MsgLevel.Warning);
                            return;
                        }
                        ReportDocument RptDoc1 = new ReportDocument();
                        RptDoc1.Load(Server.MapPath("Reports\\BookUsage_summary.rpt"));
                        RptDoc1.SetDataSource(ds);
                        if (dss.Tables["ins_nm"].Rows.Count > 0)
                        {
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtgrm"]).Text = dss.Tables["ins_nm"].Rows[0][9].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtfax"]).Text = dss.Tables["ins_nm"].Rows[0][7].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtphone"]).Text = dss.Tables["ins_nm"].Rows[0][6].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtinst"]).Text = dss.Tables["ins_nm"].Rows[0][0].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtlib"]).Text = dss.Tables["ins_nm"].Rows[0][1].ToString();
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)RptDoc1.ReportDefinition.Sections["Section1"].ReportObjects["txtadd"]).Text = dss.Tables["ins_nm"].Rows[0][2].ToString();

                        }
                        //this.CrystRptSchlr_MultiOpt.ReportSource = RptDoc1;
                        //CrystRptSchlr_MultiOpt.DataBind();
                        //CrystRptSchlr_MultiOpt.RefreshReport();
                        Session["Rptdoc"] = RptDoc1;
                        ExportOptions myexportoptions_default = RptDoc1.ExportOptions;
                        RptDoc1.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        RptDoc1.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        RptDoc1.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                        ((DiskFileDestinationOptions)RptDoc1.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath("reportTemp\\BookUsage_summary12.pdf");
                        RptDoc1.Export();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + "BookUsage_summary12.pdf"));

                        Response.WriteFile(Server.MapPath("~/reportTemp/BookUsage_summary12.pdf"));

                        Response.Flush();
                        Response.End();
                    }


                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                message.PageMesg(ex.Message, this,      dbUtilities.MsgLevel.Warning);

            }
        }
        private void reset()
        {
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;
        }
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}