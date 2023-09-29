using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class Subscription : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private DBIStructure DBI = new DBIStructure();
        private OleDbConnection con;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtCmbMemid.Text = Convert.ToString(Session["subscriberID"]);
               
                if (txtCmbMemid.Text != "")
                {
                    var deptmastercon = new OleDbConnection(retConstr(""));
                    deptmastercon.Open();
                    FillGrid(con);
                    var deptmasterds = new DataSet();
                    deptmasterds = LibObj.PopulateDataset("Select * from SubscriptionMaster where subscriber_code=" + txtCmbMemid.Text.Trim(), "DepartmentMaster", deptmastercon);
                    if (deptmasterds.Tables[0].Rows.Count > 0)
                    {
                        fill_data(deptmasterds);
                    }

                }
                Session["subscriberID"] = "";
            }
            hdnGrdId.Value = DataGrid1.ClientID;
        }

        public void FillGrid(OleDbConnection FillCon)
        {
            var departmentmasterds = new DataSet();
            string str;
            str = "select * from SubscriptionMaster order by date desc";
            LibObj.populateAfterDeletion(DataGrid1, str, FillCon);
            hdnGrdId.Value = DataGrid1.ClientID;

            departmentmasterds.Dispose();
        }

         private void fill_data(DataSet deptmasterds)
        {
            HiddenField1.Value = deptmasterds.Tables[0].Rows[0]["ID"].ToString();
            txtDate.Text = Convert.ToDateTime(deptmasterds.Tables[0].Rows[0]["date"]).ToString("dd-MMM-yyy");
            txtItem.Text = deptmasterds.Tables[0].Rows[0]["Item"].ToString();
            txtQty.Text = deptmasterds.Tables[0].Rows[0]["Quantity"].ToString();
            txtRate.Text = deptmasterds.Tables[0].Rows[0]["Rate"].ToString();
            txtAmt.Text = deptmasterds.Tables[0].Rows[0]["Amount"].ToString();
            txtPeriod.Text = deptmasterds.Tables[0].Rows[0]["Amount"].ToString();
            txtDate1.Text = Convert.ToDateTime(deptmasterds.Tables[0].Rows[0]["date"]).ToString("dd-MMM-yyy");
            btnSubmit.Text = "Update";
        }

        public void clear()
        {
            txtAmt.Text = "";
            txtDate.Text = "";
            txtItem.Text = "";
            txtPeriod.Text = "";
            txtQty.Text = "";
            txtRate.Text = "";
            txtCmbMemid.Text = "";
            btnSubmit.Text = "Submit";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var deptmastercon = new OleDbConnection(retConstr(""));
                deptmastercon.Open();
                if (txtCmbMemid.Text.Trim() == "")
                {
                    // LibObj.MsgBox1("Enter/Select Member Id as Subscriber Code!", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Enter/Select Member Id as Subscriber Code!", Me)
                    message.PageMesg("Enter/Select Member Id as Subscriber Code!", this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                if (btnSubmit.Text != "Update")
                {
                    
                    string str = "select Max(Id) from SubscriptionMaster";
                    var ad = new OleDbDataAdapter(str, deptmastercon);
                    var ds = new DataSet();
                    ad.Fill(ds);
                    if (ReferenceEquals(ds.Tables[0].Rows[0][0], DBNull.Value))
                    {
                        HiddenField1.Value = "1";
                    }
                    else
                    {
                        HiddenField1.Value = ds.Tables[0].Rows[0][0].ToString() + 1;
                    }

                }
               
                var cmd = new OleDbCommand();
                cmd.Connection = deptmastercon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Insert_SubscriptionMaster";
                cmd.Parameters.Add(new OleDbParameter("@Id", OleDbType.Integer)).Value = HiddenField1.Value;
                if (txtDate.Text == "")
                {
                    txtDate.Text = DateTime.Now.ToString();
                }
                cmd.Parameters.Add(new OleDbParameter("@Date", OleDbType.Date)).Value = Convert.ToDateTime(txtDate.Text);
                cmd.Parameters.Add(new OleDbParameter("@Item", OleDbType.VarChar)).Value = txtItem.Text;
                cmd.Parameters.Add(new OleDbParameter("@Quantity", OleDbType.VarChar)).Value = txtQty.Text;
                if (txtRate.Text == "")
                {
                    txtRate.Text = "0.0";
                }
                cmd.Parameters.Add(new OleDbParameter("@Rate", OleDbType.Decimal)).Value = txtRate.Text;
                if (txtAmt.Text == "")
                {
                    txtAmt.Text = "0.0";
                }
                cmd.Parameters.Add(new OleDbParameter("@Amount", OleDbType.Decimal)).Value = txtAmt.Text;
                cmd.Parameters.Add(new OleDbParameter("@Period", OleDbType.VarChar)).Value = txtPeriod.Text;
                if (txtDate1.Text == "")
                {
                    txtDate1.Text = DateTime.Now.ToString();
                }
                cmd.Parameters.Add(new OleDbParameter("@ToDate", OleDbType.Date)).Value = Convert.ToDateTime(txtDate1.Text);
                cmd.Parameters.Add(new OleDbParameter("@subscriber_code", OleDbType.VarChar)).Value = txtCmbMemid.Text.Trim();
                cmd.ExecuteNonQuery();
                if (btnSubmit.Text == "Submit")
                {
                   
                    message.PageMesg("Record Saved Successfully!", this, dbUtilities.MsgLevel.Success);
                }
                else
                {
                    
                    message.PageMesg("Record Updated Successfully!", this, dbUtilities.MsgLevel.Success);
                }
                clear();
                
                FillGrid(deptmastercon);
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void DataGrid1_ItemCommand1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            var deptmastercon = new OleDbConnection(retConstr(""));
            deptmastercon.Open();
            var deptmasterds = new DataSet();
            // Dim tran1 As OleDb.OleDbTransaction
            try
            {
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            HiddenField1.Value = DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text.Trim();
                            deptmasterds = LibObj.PopulateDataset("Select * from SubscriptionMaster where id=" + Eval(DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text), "DepartmentMaster", deptmastercon);
                            txtCmbMemid.Text = deptmasterds.Tables[0].Rows[0]["subscriber_code"].ToString();
                            txtDate.Text = Convert.ToDateTime(deptmasterds.Tables[0].Rows[0]["date"]).ToString("dd-MMM-yyy");
                            txtItem.Text = deptmasterds.Tables[0].Rows[0]["Item"].ToString();
                            txtQty.Text = deptmasterds.Tables[0].Rows[0]["Quantity"].ToString();
                            txtRate.Text = deptmasterds.Tables[0].Rows[0]["Rate"].ToString();
                            txtAmt.Text = deptmasterds.Tables[0].Rows[0]["Amount"].ToString();
                            txtPeriod.Text = deptmasterds.Tables[0].Rows[0]["Amount"].ToString();
                            txtDate1.Text = Convert.ToDateTime(deptmasterds.Tables[0].Rows[0]["date"]).ToString("dd-MMM-yyy");
                            btnSubmit.Text = "Update";
                            break;
                        }
                    case "Del":
                        {
                            var cmd = new OleDbCommand();
                            cmd.Connection = deptmastercon;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "Delete from SubscriptionMaster where id=" + Eval(DataGrid1.Items[e.Item.ItemIndex].Cells[1].Text.Trim());
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            // LibObj.MsgBox1("Deleted Successfully !", Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Record Deleted Successfully!", Me)
                            message.PageMesg("Record Deleted Successfully!", this, dbUtilities.MsgLevel.Success);
                            FillGrid(deptmastercon);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                // LibObj.MsgBox1(Resources.ValidationResources.UntoretriveDeptInfo.ToString, Me)
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (deptmastercon.State == ConnectionState.Open)
                {
                    deptmastercon.Close();
                }
                deptmasterds.Dispose();
                deptmastercon.Dispose();
            }
        }

        protected void txtCmbMemid_TextChanged(object sender, EventArgs e)
        {
            if (txtCmbMemid.Text != "")
            {
                var deptmastercon = new OleDbConnection(retConstr(""));
                deptmastercon.Open();
                var deptmasterds = new DataSet();
                deptmasterds = LibObj.PopulateDataset("Select * from SubscriptionMaster where subscriber_code='" + txtCmbMemid.Text.Trim() + "'", "DepartmentMaster", deptmastercon);
                if (deptmasterds.Tables[0].Rows.Count > 0)
                {
                    fill_data(deptmasterds);
                }

            }
        }
    }
}