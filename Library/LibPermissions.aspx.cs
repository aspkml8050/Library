using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class LibPermissions : BaseClass
    {
        libGeneralFunctions LibObj=new libGeneralFunctions();
        DBIStructure DBI = new DBIStructure();
        messageLibrary msgLibrary=new messageLibrary();
        dbUtilities message=new dbUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                msglabel.Visible = false;
                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)
                if (!IsPostBack)
                {
                    // By Aamir 03-02-2012
                    // Creating Entry for parent library in database
                    // ----------------------------------------------------
                    CreateParentLibrary();
                    CentralLibrary();

                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public int CreateParentLibrary()
        {
            string PID = null;
            PID = DBI.ExecuteScalar("select id from UnionLibSettings where ConnectionStringName='" + DBI.GetConnectionName() + "'", retConstr(""));

            if (string.IsNullOrEmpty(PID))
            {
                string DBName = null;
                string SName = null;

                string constr = DBI.GetConnectionString(DBI.GetConnectionName());
                DBName = constr.Substring(constr.IndexOf("Initial Catalog") + 15);
                DBName = DBName.Substring(DBName.IndexOf("=") + 1).Trim();
                DBName = DBName.Substring(0, DBName.IndexOf(";")).Trim();

                SName = constr.Substring(constr.IndexOf("Data Source") + 11);
                SName = SName.Substring(SName.IndexOf("=") + 1).Trim();
                SName = SName.Substring(0, SName.IndexOf(";")).Trim();

                var PParameters = new ParameterCollection();
                PParameters.Add("@LibraryName", DbType.String, "Central Library");
                PParameters.Add("@DatabaseName", DbType.String, DBName);
                PParameters.Add("@ServerName", DbType.String, SName);
                PParameters.Add("@ConnectionStringName", DbType.String, DBI.GetConnectionName());
                PParameters.Add("@ConnectionString", DbType.String, DBI.GetConnectionString(DBI.GetConnectionName()));
                PParameters.Add("@Status", DbType.String, "P");
                PParameters.Add("@PID", DbType.Int32, "-1");

                if (DBI.ExecuteProcedure("Insert_UnionLibSettings", PParameters, retConstr("")))
                {

                }
            }
            return default;
        }
        public void CentralLibrary()
        {
            var ds = new DataSet();
            ds = DBI.GetDataSet("select id,LibraryName,ConnectionStringName from UnionLibSettings where ConnectionStringName='" + DBI.GetConnectionName() + "'", retConstr(""));
            if (ds.Tables[0].Rows.Count > 0)
            {
                DdlLibraries.DataTextField = "LibraryName";
                DdlLibraries.DataValueField = "ConnectionStringName";
                DdlLibraries.DataSource = ds.Tables[0];
                DdlLibraries.DataBind();
                DdlLibraries.SelectedIndex = -1;
                DdlLibraries.Enabled = false;
                PopulateChildLibraries(Convert.ToInt32(ds.Tables[0].Rows[0]["id"]));
            }
        }
        public void PopulateChildLibraries(int cid)
        {
            var ds = new DataSet();
            ds = DBI.GetDataSet("Select * from UnionLibSettings where PID='" + cid + "'", retConstr(""));
            grdDetail.DataSource = ds.Tables[0];
            grdDetail.DataBind();

            int count1;
            var loopTo = grdDetail.Items.Count - 1;
            for (count1 = 0; count1 <= loopTo; count1++)
            {
                var chkbox = new System.Web.UI.HtmlControls.HtmlInputCheckBox(); // CheckBox
                chkbox = (System.Web.UI.HtmlControls.HtmlInputCheckBox)grdDetail.Items[count1].Cells[0].FindControl("Chkselect");

                var LibPerm = new Label();
                LibPerm = (Label)grdDetail .Items[count1].Cells[5].FindControl("LibPerm");
                if (LibPerm.Text == "Y")
                {
                    chkbox.Checked = true;
                }

            }
        }
    }
}