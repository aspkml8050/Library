using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.App_Code.MultipleFramworks;
using Library.App_Code.CSharp;
namespace Library
{
    public partial class IssueReturnSubscriptionRpt : BaseClass
    {

        private messageLibrary msgLibrary = new messageLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string FilterQry = string.Empty;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                if (txtFromDate.Text != "")
                {
                    FilterQry = "TransactionDate>='" + txtFromDate.Text + "'";
                }
                if (txtToDate.Text != "")
                {
                    if (!string.IsNullOrEmpty(FilterQry))
                    {
                        FilterQry = FilterQry + " and TransactionDate<='" + txtToDate.Text + "'";
                    }
                    else
                    {
                        FilterQry = "TransactionDate<='" + txtToDate.Text + "'";
                    }
                }
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                if (txtFromDate.Text != "")
                {
                    FilterQry = "DocDate>='" + txtFromDate.Text + "'";
                }
                if (txtToDate.Text != "")
                {
                    if (!string.IsNullOrEmpty(FilterQry))
                    {
                        FilterQry = FilterQry + " and DocDate<='" + txtToDate.Text + "'";
                    }
                    else
                    {
                        FilterQry = "DocDate<='" + txtToDate.Text + "'";
                    }
                }
            }
            if (txtItemCode.Text != "")
            {
                if (!string.IsNullOrEmpty(FilterQry))
                {
                    FilterQry = FilterQry + " and ItemCode='" + txtItemCode.Text + "'";
                }
                else
                {
                    FilterQry = "ItemCode='" + txtItemCode.Text + "'";
                }
            }
            string str = string.Empty;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                if (!string.IsNullOrEmpty(FilterQry))
                {
                    str = "SELECT     dbo.ReceiptSubscription.Id, dbo.ReceiptSubscription.TransactionDate, dbo.ReceiptSubscription.ItemCode, dbo.ReceiptSubscription.Quantity,dbo.ReceiptSubscription.Rate, dbo.ReceiptSubscription.SupplierId, dbo.ReceiptSubscription.Amount, dbo.ReceiptSubscription.OtherExp,   dbo.ReceiptSubscription.TotalAmt, dbo.ReceiptSubscription.Remak, dbo.vendormaster.vendorname AS SupplierName FROM         dbo.ReceiptSubscription INNER JOIN  dbo.vendormaster ON dbo.ReceiptSubscription.SupplierId = dbo.vendormaster.vendorid where " + FilterQry;
                }
                else
                {
                    str = "SELECT     dbo.ReceiptSubscription.Id, dbo.ReceiptSubscription.TransactionDate, dbo.ReceiptSubscription.ItemCode, dbo.ReceiptSubscription.Quantity,dbo.ReceiptSubscription.Rate, dbo.ReceiptSubscription.SupplierId, dbo.ReceiptSubscription.Amount, dbo.ReceiptSubscription.OtherExp,   dbo.ReceiptSubscription.TotalAmt, dbo.ReceiptSubscription.Remak, dbo.vendormaster.vendorname AS SupplierName FROM         dbo.ReceiptSubscription INNER JOIN  dbo.vendormaster ON dbo.ReceiptSubscription.SupplierId = dbo.vendormaster.vendorid";
                }
            }
            else if (!string.IsNullOrEmpty(FilterQry))
            {
                str = "Select * from IssueSubscription where " + FilterQry;
            }
            else
            {
                str = "Select * from IssueSubscription";
            }
            var con = new OleDbConnection(retConstr(""));
            var ad = new OleDbDataAdapter(str, con);
            var ds = new DataSet();
            ad.Fill(ds);
            var RptDoc = new ReportDocument();
            if (RadioButtonList1.SelectedIndex == 0)
            {
                RptDoc.Load(Server.MapPath(@"Reports\\ReceiptSubscription.rpt"));
            }
            else
            {
                RptDoc.Load(Server.MapPath(@"Reports\\IssueSubscription.rpt"));
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                RptDoc.SetDataSource(ds.Tables[0]);
                // CrystalReportViewer1.ReportSource = RptDoc
                // CrystalReportViewer1.DataBind()
                // CrystalReportViewer1.RefreshReport()
                var exportOpts1 = RptDoc.ExportOptions;
                RptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                RptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                RptDoc.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                ((DiskFileDestinationOptions)RptDoc.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\IcardPrintN.pdf");
                RptDoc.Export();
                RptDoc.Close();
                RptDoc.Dispose();
                {
                    var withBlock = Response;
                    withBlock.ClearContent();
                    withBlock.ClearHeaders();
                    withBlock.ContentType = "application/pdf";
                    withBlock.AppendHeader("Content-Disposition", "attachment; filename=IcardPrintN.pdf");
                    withBlock.WriteFile(@"reportTemp\IcardPrintN.pdf");
                    withBlock.Flush();
                    withBlock.Close();
                }
                File.Delete(Server.MapPath(@"reportTemp\IcardPrintN.pdf"));
                clear();
            }
        }

        public void clear()
        {

            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtItemCode.Text = "";

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}