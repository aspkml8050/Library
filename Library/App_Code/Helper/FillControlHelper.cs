using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Library.Common.Helper.DL
{
    public class FillControl 
    {
        public void FillDropDownList(DropDownList ctrlID, DataTable dataTable, string TextField, string ValueField)
        {
            if (dataTable.Rows.Count > 0)
            {
                ctrlID.DataSource = dataTable;
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
                ctrlID.Items.Insert(0, "---Select---");
            }
            else
            {
                ctrlID.Items.Clear();
                ctrlID.Items.Insert(0, "---Select---");
            }
        }
        public void FillListBox(ListBox ctrlID, DataTable dataTable, string TextField, string ValueField)
        {
            if (dataTable.Rows.Count > 0)
            {
                ctrlID.DataSource = dataTable;
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
            }
            else
            {
                ctrlID.Items.Clear();
            }
        }
    }

}
