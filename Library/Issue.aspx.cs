using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Web.Services;
using Library.App_Code.MultipleFramworks;
using Library.App_Code.CSharp;

namespace Library
{
    public partial class Issue : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private DBIStructure DBI = new DBIStructure();
        private OleDbConnection con;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOtherExp.Text = "0.0";
                btnDelete.Enabled = false;
            }
            hdnGrdId.Value = GridView1.ClientID;
            //SSA.MakeAccessible(GridView1);
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalAmt.Text == "")
                {
                    total();
                }
                if (DropDownList1.SelectedItem.Text == "---Select---")
                {
                    // LibObj.MsgBox1("Select Issue Type", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Select Issue Type", Me)
                    message.PageMesg("Select Issue Type", this, dbUtilities.MsgLevel.Warning);

                    return;
                }
                var deptmastercon = new OleDbConnection(retConstr(""));
                deptmastercon.Open();
                //con.Open();
                if (btnsubmit.Text == "Submit")
                {
                    string str = "select Max(Id) from IssueSubscription";
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
                cmd.CommandText = "Insert_IssueSubscription";
                cmd.Parameters.Add(new OleDbParameter("@Id", OleDbType.Integer)).Value = HiddenField1.Value;
                cmd.Parameters.Add(new OleDbParameter("@IssueType", OleDbType.VarChar)).Value = DropDownList1.SelectedItem.Text;
                cmd.Parameters.Add(new OleDbParameter("@DocDate", OleDbType.Date)).Value = txtDocDate.Text;
                cmd.Parameters.Add(new OleDbParameter("@DocNumber", OleDbType.VarChar)).Value = txtDocNo.Text;
                cmd.Parameters.Add(new OleDbParameter("@ItemCode", OleDbType.VarChar)).Value = txtItemCode.Text;
                cmd.Parameters.Add(new OleDbParameter("@Quantity", OleDbType.VarChar)).Value = txtQty.Text;
                cmd.Parameters.Add(new OleDbParameter("@Rate", OleDbType.Decimal)).Value = txtRate.Text;
                cmd.Parameters.Add(new OleDbParameter("@Amount", OleDbType.Decimal)).Value = txtAmt.Text;
                cmd.Parameters.Add(new OleDbParameter("@OtherExp", OleDbType.Decimal)).Value = txtOtherExp.Text;
                cmd.Parameters.Add(new OleDbParameter("@TotalAmount", OleDbType.Decimal)).Value = txtTotalAmt.Text;
                cmd.ExecuteNonQuery();
                if (btnsubmit.Text == "Submit")
                {
                    // LibObj.MsgBox1("Record Saved Successfully!", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Record Saved Successfully!", Me)
                    message.PageMesg("Record Saved Successfully!", this, dbUtilities.MsgLevel.Success);
                }
                else if (btnsubmit.Text == "Update")
                {
                    // LibObj.MsgBox1("Record Updated Successfully!", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Record Updated Successfully!", Me)
                    message.PageMesg("Record Updated Successfully!", this, dbUtilities.MsgLevel.Success);
                }
                BindGrid();
                clear();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            var gclas = new GlobClassTr();
            //var myclass1 = new global::newclass1();
            string str = "select * from SubscriptionMaster where Item='" + txtItemCode.Text + "'";
            gclas.TrOpen();
            DataTable dt = gclas.DataT(str);
            gclas.TrClose();
            //ds.filladapter(str);
            if (dt.Rows.Count > 0)
            {
                txtRate.Text = dt.Rows[0]["Rate"].ToString();
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            txtAmt.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtRate.Text)).ToString();
            total();
        }

        protected void txtOtherExp_TextChanged(object sender, EventArgs e)
        {
            total();
        }
        public void total()
        {
            txtTotalAmt.Text = txtAmt.Text + txtOtherExp.Text;
        }
        public void clear()
        {
            DropDownList1.SelectedItem.Text = "---Select---";
            txtDocDate.Text = "";
            txtDocNo.Text = "";
            txtItemCode.Text = "";
            txtQty.Text = "";
            txtRate.Text = "";
            txtAmt.Text = "";
            txtOtherExp.Text = "";
            txtTotalAmt.Text = "";
            btnsubmit.Text = "Submit";
            btnDelete.Enabled = false;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gclas = new GlobClassTr();
            //var myclass1 = new global::newclass1();
            string str = "select * from IssueSubscription where DocNumber='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["DocNumber"] + "'";
            gclas.TrOpen();
            DataTable dt = gclas.DataT(str);
            gclas.TrClose();
            //myclass1.filladapter(str);
            if (dt.Rows.Count > 0)
            {
                HiddenField1.Value = dt.Rows[0]["Id"].ToString() ;
                DropDownList1.SelectedItem.Text = dt.Rows[0]["IssueType"].ToString();
                txtDocDate.Text = Convert.ToDateTime(dt.Rows[0]["DocDate"]).ToString("dd-MMM-yyyy");
                txtDocNo.Text = dt.Rows[0]["DocNumber"].ToString();
                txtItemCode.Text = dt.Rows[0]["ItemCode"].ToString();
                txtQty.Text = dt.Rows[0]["Quantity"].ToString();
                txtRate.Text = dt.Rows[0]["Rate"].ToString();
                txtAmt.Text = dt.Rows[0]["Amount"].ToString();
                txtOtherExp.Text = dt.Rows[0]["OtherExp"].ToString();
                txtTotalAmt.Text = dt.Rows[0]["TotalAmount"].ToString();
                btnsubmit.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
        public void BindGrid()
        {
            var gclas = new GlobClassTr();
            //var myclass1 = new global::newclass1();
            string str = "select * from IssueSubscription order by id desc ";
            gclas.TrOpen();
            DataTable dt = gclas.DataT(str);
            gclas.TrClose();
            //myclass1.filladapter(str);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = (object)null;
                GridView1.DataBind();
            }
            hdnGrdId.Value = GridView1.ClientID;
            //SSA.MakeAccessible(GridView1);
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var gclas = new GlobClassTr();
                //var myclass1 = new global::newclass1();
                string str = "delete from IssueSubscription where DocNumber='" + txtDocNo.Text + "'";
                gclas.TrOpen();
                DataTable dt = gclas.DataT(str);
                gclas.TrClose();
                //myclass1.filladapter(str);
                // LibObj.MsgBox1("Record Deleted Succesfully!", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Record Deleted Successfully!", Me)
                message.PageMesg("Record Deleted Successfully!", this, dbUtilities.MsgLevel.Success);
                BindGrid();
                clear();
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != "" & txtRate.Text != "")
            {
                txtAmt.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtRate.Text)).ToString();
                if (txtOtherExp.Text != "")
                {
                    txtTotalAmt.Text = txtAmt.Text + txtOtherExp.Text;
                }
            }
        }
    }
}