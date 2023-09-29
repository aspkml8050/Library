using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class RptBudget : BaseClass
    {
        libGeneralFunctions libobj = new libGeneralFunctions();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            try
            {
                msglabel.Visible = false;
                if (!Page.IsPostBack)
                {

                    // Me.chkSelectAll.Focus()
                    this.SetFocus(chkSelectAll);
                    msglabel.Text = Request.QueryString["title"];
                    tmpcondition = Request.QueryString["condition"];
                    string str;
                    str = "select departmentmaster.departmentcode as id ,InstituteMaster.ShortName + '-' + departmentname as departmentname from departmentmaster,institutemaster where departmentmaster.institutecode=institutemaster.institutecode order by departmentname";
                    ds = libobj.PopulateDataset(str, "dept", con);
                    dgbudget.DataSource = ds;
                    dgbudget.DataBind();
                }
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            } 
            
            finally
            {
                ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        private void SearchDepartments()
        {

            using (var con = new OleDbConnection(retConstr("")))
            {
                using (var cmd = new OleDbCommand())
                {
                    string sql = "select departmentmaster.departmentcode as id ,InstituteMaster.ShortName + '-' + departmentname as departmentname from departmentmaster,institutemaster where departmentmaster.institutecode=institutemaster.institutecode";
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        sql += " and departmentname LIKE '%" + txtSearch.Text.Trim() + "%'";
                        // cmd.Parameters.AddWithValue("@Department", txtSearch.Text.Trim())
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (var sda = new OleDbDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        sda.Fill(dt);
                        dgbudget.DataSource = dt;
                        dgbudget.DataBind();
                    }
                }
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked == true)
                {
                    // Me.chkSelectAll.Focus()
                    this.SetFocus(chkSelectAll);
                    int i;
                    if (dgbudget.Items.Count > 0)
                    {
                        var loopTo = dgbudget.Items.Count - 1;
                        for (i = 0; i <= loopTo; i++)
                        {
                            var chk1 = new CheckBox();
                            chk1 = (CheckBox)dgbudget.Items[i].Cells[0].FindControl("Chkselect");
                            chk1.Checked = true;
                        }
                    }
                }
                else
                {
                    // chkSelectAll.Focus()
                    //this.SetFocus(chkSelectAll);
                    int i;
                    if (dgbudget.Items.Count > 0)
                    {
                        var loopTo1 = dgbudget.Items.Count - 1;
                        for (i = 0; i <= loopTo1; i++)
                        {
                            var chk1 = new CheckBox();
                            chk1 = (CheckBox)dgbudget.Items[i].Cells[0].FindControl("Chkselect");
                            chk1.Checked = false;
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

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            try
            {
                var isSelected = default(bool);
                int i;
                string tmpvar = string.Empty;
                var loopTo = dgbudget.Items.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    var chk1 = new CheckBox();
                    chk1 = (CheckBox)dgbudget.Items[i].Cells[0].FindControl("ChkSelect");
                    if (chk1.Checked == true)
                    {
                        isSelected = true;
                        if (string.IsNullOrEmpty(tmpvar))
                        {
                            tmpvar += "" + dgbudget.Items[i].Cells[0].Text + "";
                        }
                        else
                        {
                            tmpvar += "," + dgbudget.Items[i].Cells[0].Text + "";
                        }
                    }
                    else
                    {
                    }
                }
                if (isSelected == false)
                {
                    
                    message.PageMesg(Resources.ValidationResources.IvDep.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    string str;
                    str = "select CategoryLoadingStatus.Category_LoadingStatus AS ItemCategory ,* from BudgetView_R left join CategoryLoadingStatus on CategoryLoadingStatus.Id = BudgetView_R.ItemCategoryId where BudgetView_R.departmentcode IN(" + tmpvar + ")  order by deptname";
                    
                    ds = libobj.PopulateDataset(str, "BudgetView", con);
                    if (ds.Tables["BudgetView"].Rows.Count == 0)
                    {
                        
                        message.PageMesg(Resources.ValidationResources.NoRecFndFPnt.ToString(), this, dbUtilities.MsgLevel.Warning);
                       
                        Response.Redirect(Request.Url.ToString());
                        return;
                    }
                    // mycommand = libobj.populateCommandText(str, con)
                    var MyCommand = new OleDbCommand();
                    // mycommand = libobj.populateCommandText(str, con)
                    MyCommand.Connection = con;
                    MyCommand.CommandText = str;
                    MyCommand.CommandType = CommandType.Text;
                    var MyDA = new OleDbDataAdapter();
                    MyDA.SelectCommand = MyCommand;
                    var myDS = new DataSet();
                    MyDA.Fill(myDS, "ccp");
                    // myDS = libobj.PopulateDataset(str, "ccp", con)
                    int P = 0;
                    P = myDS.Tables["ccp"].Rows.Count;
                    var O_rpt = new ReportDocument(); 

                    O_rpt.Load(Server.MapPath(@"Reports\BudgetCrystalReport.rpt"));
                    O_rpt.SetDataSource(myDS.Tables["ccp"]);
                    // Field
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["gram1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["email1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["fax1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["phoneno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["libraryname1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 10.0f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["deptname1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["allocatedamount1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["approvalcommitedamt1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["nonapprovalcommitedamt1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["approvalindentinhandamt1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["nonapprovalindentinhandamt1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section3"].ReportObjects["expendedamount1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["CurrSession1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["PrintDate1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.Sections["Section4"].ReportObjects["currency1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    // Formula 
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["addressformula1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    // CType(O_rpt.ReportDefinition.ReportObjects("TotalSpentAmount1"), CrystalDecisions.CrystalReports.Engine.FieldObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 8.5F))
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["TotalNetBalance1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["InstituteCity1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 12.0f));
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["asum1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    // CType(O_rpt.ReportDefinition.ReportObjects("totexp1"), CrystalDecisions.CrystalReports.Engine.FieldObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 8.5F))
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["balance1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["commapprov1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["inhandapprov1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)O_rpt.ReportDefinition.ReportObjects["expendituretotal1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    // CType(O_rpt.ReportDefinition.ReportObjects("totexp2"), CrystalDecisions.CrystalReports.Engine.FieldObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 8.5F))
                    // CType(O_rpt.ReportDefinition.ReportObjects("balance2"), CrystalDecisions.CrystalReports.Engine.FieldObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 8.5F))
                    // Textbox
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text1"]).Text = Resources.ValidationResources.rptGram.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text2"]).Text = Resources.ValidationResources.rptMail.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text2"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text3"]).Text = Resources.ValidationResources.rptFax.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text3"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text4"]).Text = Resources.ValidationResources.PhNo.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text4"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text15"]).Text = Resources.ValidationResources.Title_BudRep.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text15"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 10.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text19"]).Text = Resources.ValidationResources.BdgtSt.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text19"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text20"]).Text = Resources.ValidationResources.LAson.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text20"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).Text = Resources.ValidationResources.LbDepartment.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text5"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text9"]).Text = Resources.ValidationResources.GrAllo.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text9"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text7"]).Text = Resources.ValidationResources.IChkApp.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text7"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text8"]).Text = Resources.ValidationResources.GrNonApp.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text8"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text12"]).Text = Resources.ValidationResources.IChkApp.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text12"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text11"]).Text = Resources.ValidationResources.GrNonApp.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text13"]).Text = Resources.ValidationResources.GrExpenditure.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text13"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    // CType(O_rpt.ReportDefinition.Sections("Section2").ReportObjects("Text18"), CrystalDecisions.CrystalReports.Engine.TextObject).Text = Resources.ValidationResources.LToExpend.ToString
                    // CType(O_rpt.ReportDefinition.Sections("Section2").ReportObjects("Text18"), CrystalDecisions.CrystalReports.Engine.TextObject).ApplyFont(New Font(Resources.ValidationResources.TextBox1.ToString, 9.0F))

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text14"]).Text = Resources.ValidationResources.GrBal.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text14"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text17"]).Text = Resources.ValidationResources.LTotal.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text17"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text16"]).Text = Resources.ValidationResources.LAllAmountIn.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text16"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).Text = Resources.ValidationResources.GrCommitAmt.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text6"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.0f));

                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text10"]).Text = Resources.ValidationResources.LIndentsInHand.ToString();
                    ((TextObject)O_rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text10"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString() , 9.0f));


                    // CrystalReportViewer1.ReportSource = O_rpt
                    // CrystalReportViewer1.DataBind()
                    // CrystalReportViewer1.RefreshReport()
                    var exportOpts1 = O_rpt.ExportOptions;
                    O_rpt.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    O_rpt.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    O_rpt.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)O_rpt.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\rptPublisherDetailsN.pdf");
                    O_rpt.Export();
                    O_rpt.Close();
                    O_rpt.Dispose();
                    // MyDA.Dispose()
                    {
                        var withBlock = Response;
                        withBlock.ClearContent();
                        withBlock.ClearHeaders();
                        withBlock.ContentType = "application/pdf";
                        withBlock.AppendHeader("Content-Disposition", "attachment; filename=BudgetReport.pdf");
                        withBlock.WriteFile(@"reportTemp\rptPublisherDetailsN.pdf");
                        withBlock.Flush();
                        withBlock.End();
                        withBlock.Close();
                    }
                    File.Delete(Server.MapPath(@"reportTemp\rptPublisherDetailsN.pdf"));
                    myDS.Dispose();
                    MyDA.Dispose();
                }
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            int countck;

            chkSelectAll.Checked = false;
            this.SetFocus(chkSelectAll);
            if (this.dgbudget.Items.Count > 0)
            {
                var loopTo = this.dgbudget.Items.Count - 1;
                for (countck = 0; countck <= loopTo; countck++)
                {
                    var chk1 = new CheckBox();
                    chk1 = (CheckBox)dgbudget.Items[countck].Cells[0].FindControl("Chkselect");
                    chk1.Checked = false;
                }
            }
        }

        protected void grdrep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();

            var ds1 = new DataSet();
            string tmpvar = string.Empty;
            bool isSelected;
            int i;

            var loopTo = dgbudget.Items.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                var chk1 = new CheckBox();
                chk1 = (CheckBox)dgbudget.Items[i].Cells[0].FindControl("ChkSelect");
                if (chk1.Checked == true)
                {
                    isSelected = true;
                    if (string.IsNullOrEmpty(tmpvar))
                    {
                        tmpvar += "" + dgbudget.Items[i].Cells[0].Text + "";
                    }
                    else
                    {
                        tmpvar += "," + dgbudget.Items[i].Cells[0].Text + "";
                    }
                }
                else
                {
                }
            }
            string tot = "SELECT SUM(allocatedamount) AS AllocatedAmount, sum(nonapprovalindentinhandamt +approvalindentinhandamt) As IndentInHand ,sum(nonapprovalindentinhandamt +approvalindentinhandamt) As CommittedAmmount, sum(balance) As Balance ,sum(expendedamount) As Expenditure FROM BudgetView_R where BudgetView_R.departmentcode IN(" + tmpvar + ")";

            // con.Open()
            ds1 = libobj.PopulateDataset(tot, "BudgetView_R", con);
            if (ds1.Tables[0].Rows.Count == 0)
            {
                // Hidden1.Value = "Show"
                // libobj.MsgBox1(Resources.ValidationResources.NoRecFndFPnt.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.NoRecFndFPnt.ToString, Me)
                message.PageMesg(Resources.ValidationResources.NoRecFndFPnt.ToString(), this, dbUtilities.MsgLevel.Warning);
                return;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {

                Label lblall = (Label)e.Row.FindControl("alloc");
                // lblall.Text = "dddf"
                Label lblInd = (Label)e.Row.FindControl("IndentIn");
                Label lblExp = (Label)e.Row.FindControl("Expen");
                Label lblComm = (Label)e.Row.FindControl("Commit");
                Label lblball = (Label)e.Row.FindControl("ball");
                var loopTo1 = ds1.Tables[0].Rows.Count - 1;
                for (int i1 = 0; i1 <= loopTo1; i1++)
                {
                    lblall.Text = ds1.Tables[0].Rows[i1]["AllocatedAmount"].ToString();
                    lblInd.Text = ds1.Tables[0].Rows[i1]["IndentInHand"].ToString();
                    lblExp.Text = ds1.Tables[0].Rows[i1]["Expenditure"].ToString();
                    lblComm.Text = ds1.Tables[0].Rows[i1]["CommittedAmmount"].ToString();
                    lblball.Text = ds1.Tables[0].Rows[i1]["Balance"].ToString();
                }

            }

        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchDepartments();
        }

        protected void nreco_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var ds = new DataSet();
            var ds1 = new DataSet();
            try
            {
                var isSelected = default(bool);
                int i;
                string tmpvar = string.Empty;

                var loopTo = dgbudget.Items.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    var chk1 = new CheckBox();
                    chk1 = (CheckBox)dgbudget.Items[i].Cells[0].FindControl("ChkSelect");
                    if (chk1.Checked == true)
                    {
                        isSelected = true;
                        if (string.IsNullOrEmpty(tmpvar))
                        {
                            tmpvar += "" + dgbudget.Items[i].Cells[0].Text + "";
                        }
                        else
                        {
                            tmpvar += "," + dgbudget.Items[i].Cells[0].Text + "";
                        }
                    }
                    else
                    {
                    }
                }
                if (isSelected == false)
                {
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.IvDep.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.IvDep.ToString(), this, dbUtilities.MsgLevel.Warning);
                }
                else
                {
                    string str;
                    str = "select * from BudgetView_R where BudgetView_R.departmentcode IN(" + tmpvar + ")  order by deptname";
                    ds = libobj.PopulateDataset(str, "BudgetView", con);
                    if (ds.Tables["BudgetView"].Rows.Count == 0)
                    {
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.NoRecFndFPnt.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.NoRecFndFPnt.ToString(), this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                    var dt = new DataTable();
                    var MyCommand = new OleDbCommand();
                    MyCommand.Connection = con;
                    MyCommand.CommandText = str;
                    MyCommand.CommandType = CommandType.Text;
                    var MyDA = new OleDbDataAdapter();
                    MyDA.SelectCommand = MyCommand;
                    var myDS = new DataSet();
                    MyDA.Fill(dt);
                    int P = 0;
                    ins.Text = dt.Rows[0]["institutename"].ToString();
                    grm.Text = dt.Rows[0]["gram"].ToString();
                    sess.Text = dt.Rows[0]["Curr_Session"].ToString();
                    curdt.Text = String.Format("dd-MMM-yyyy", System.DateTime.Now );
                    cit.Text = dt.Rows[0]["city"].ToString();
                    pin.Text = dt.Rows[0]["pincode"].ToString();
                    addr.Text = dt.Rows[0]["address"].ToString();
                    stat.Text = dt.Rows[0]["state"].ToString();
                    libranm.Text = dt.Rows[0]["libraryname"].ToString();
                    eml.Text = dt.Rows[0]["email"].ToString();
                    phn.Text = dt.Rows[0]["phoneno"].ToString();
                    fx.Text = dt.Rows[0]["fax"].ToString();



                    grdrep.DataSource = dt;
                    grdrep.DataBind();
                    // For l As Integer = 0 To dt.Columns.Count - 1
                    // grdrep.HeaderRow.Cells(l).Attributes.Add("bgcolor", "#336699")
                    // 'grdrep.HeaderRow.Cells[l].BackColor = System.Drawing.Color.FromArgb(3, 76, 113)
                    // Next

                    Context.Response.AddHeader("content-disposition", "attachment;filename=BudjetRep.pdf");
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
                ds.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}