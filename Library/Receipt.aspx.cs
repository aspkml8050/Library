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
    public partial class Receipt : BaseClass
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
                BindGrid();
            }
            hdnGrdId.Value = GridView1.ClientID;
            //SSA.MakeAccessible(GridView1);
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTotal.Text == "")
                {
                    total();
                }
                var deptmastercon = new OleDbConnection(retConstr(""));
                deptmastercon.Open();
                //con.Open();
                if (btnsubmit.Text == "Submit")
                {
                    string str = "select Max(Id) from ReceiptSubscription";
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
                cmd.CommandText = "Insert_ReceiptSubscription";
                cmd.Parameters.Add(new OleDbParameter("@Id", OleDbType.Integer)).Value = HiddenField1.Value;
                if (txtDate.Text == "")
                {
                    txtDate.Text = DateTime.Now.ToString();
                }
                cmd.Parameters.Add(new OleDbParameter("@TransactionDate", OleDbType.Date)).Value = txtDate.Text;
                cmd.Parameters.Add(new OleDbParameter("@ItemCode", OleDbType.VarChar)).Value = txtItemCode.Text;
                cmd.Parameters.Add(new OleDbParameter("@Quantity", OleDbType.VarChar)).Value = txtQty.Text;
                cmd.Parameters.Add(new OleDbParameter("@Rate", OleDbType.Decimal)).Value = txtRate.Text;
                if (btnsubmit.Text == "Submit")
                {
                    cmd.Parameters.Add(new OleDbParameter("@SupplierId", OleDbType.Integer)).Value = hdVid.Value;
                }
                else if (btnsubmit.Text == "Update")
                {
                    cmd.Parameters.Add(new OleDbParameter("@SupplierId", OleDbType.Integer)).Value = HiddenField2.Value;
                }

                cmd.Parameters.Add(new OleDbParameter("@Amount", OleDbType.Decimal)).Value = txtAmt.Text;
                cmd.Parameters.Add(new OleDbParameter("@OtherExp", OleDbType.Decimal)).Value = txtOtherExp.Text;
                cmd.Parameters.Add(new OleDbParameter("@TotalAmt", OleDbType.Decimal)).Value = txtTotal.Text;
                cmd.Parameters.Add(new OleDbParameter("@Remak", OleDbType.VarChar)).Value = txtRemark.Text;
                cmd.ExecuteNonQuery();
                BindGrid();
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
                
                Clear();
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            txtAmt.Text = "";
            txtDate.Text = "";
            txtItemCode.Text = "";
            txtQty.Text = "";
            txtRate.Text = "";
            txtCmbVendor.Text = "";
            txtAmt.Text = "";
            txtOtherExp.Text = "0.0";
            txtRemark.Text = "";
            txtTotal.Text = "";
            btnDelete.Enabled = false;
            btnsubmit.Text = "Submit";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmt.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtRate.Text)).ToString();

                total();
            }
            catch(Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }

        public void BindGrid()
        {
            var gClas = new GlobClassTr();
            //var myclass1 = new newclass1();
            string str = "SELECT     dbo.ReceiptSubscription.Id, dbo.ReceiptSubscription.TransactionDate, dbo.ReceiptSubscription.ItemCode, dbo.ReceiptSubscription.Quantity,dbo.ReceiptSubscription.Rate, dbo.ReceiptSubscription.SupplierId, dbo.ReceiptSubscription.Amount, dbo.ReceiptSubscription.OtherExp,   dbo.ReceiptSubscription.TotalAmt, dbo.ReceiptSubscription.Remak, dbo.vendormaster.vendorname AS SupplierName FROM         dbo.ReceiptSubscription INNER JOIN  dbo.vendormaster ON dbo.ReceiptSubscription.SupplierId = dbo.vendormaster.vendorid order by  dbo.ReceiptSubscription.Id desc";
            gClas.TrOpen();
            DataTable dt = gClas.DataT(str);
            gClas.TrClose();
            //myclass1.filladapter(str);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var gclas = new GlobClassTr();

               // var myclass1 = new newclass1();
                string str = "SELECT dbo.ReceiptSubscription.Id, dbo.ReceiptSubscription.TransactionDate, dbo.ReceiptSubscription.ItemCode, dbo.ReceiptSubscription.Quantity,                      dbo.ReceiptSubscription.Rate, dbo.ReceiptSubscription.SupplierId, dbo.ReceiptSubscription.Amount, dbo.ReceiptSubscription.OtherExp,   dbo.ReceiptSubscription.TotalAmt, dbo.ReceiptSubscription.Remak, dbo.vendormaster.vendorname AS SupplierName FROM         dbo.ReceiptSubscription INNER JOIN                       dbo.vendormaster ON dbo.ReceiptSubscription.SupplierId = dbo.vendormaster.vendorid where dbo.ReceiptSubscription.Id='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["Id"] + "'";
                gclas.TrOpen();
                DataTable dt = gclas.DataT(str);
                gclas.TrClose();
                //myclass1.filladapter(str);
                if (dt.Rows.Count > 0)
                {
                    HiddenField1.Value = dt.Rows[0]["Id"].ToString();
                    txtDate.Text = Convert.ToDateTime(dt.Rows[0]["TransactionDate"]).ToString("dd-MMM-yyyy");
                    txtItemCode.Text = dt.Rows[0]["ItemCode"].ToString();
                    txtQty.Text = dt.Rows[0]["Quantity"].ToString();
                    txtRate.Text = dt.Rows[0]["Rate"].ToString();
                    txtCmbVendor.Text = dt.Rows[0]["SupplierName"].ToString();
                    HiddenField2.Value = dt.Rows[0]["SupplierId"].ToString();
                    txtAmt.Text = dt.Rows[0]["Amount"].ToString();
                    txtOtherExp.Text = dt.Rows[0]["OtherExp"].ToString();
                    txtTotal.Text = dt.Rows[0]["TotalAmt"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remak"].ToString();
                    btnsubmit.Text = "Update";
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        protected void txtOtherExp_TextChanged(object sender, EventArgs e)
        {
            total();
        }
        public void total()
        {
            txtTotal.Text = txtAmt.Text + txtOtherExp.Text;
        }
    }
}