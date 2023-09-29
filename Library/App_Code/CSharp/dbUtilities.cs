using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;


namespace Library.App_Code.CSharp
{

    public class dbUtilities
    {

        private static string getConStr
        {

            get
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                return constr;
            }


        }


        public static int GetIntScalarVal(string command)
        {
            int output = 0;
            OleDbConnection cn = new OleDbConnection(getConStr);
            OleDbCommand cmd = new OleDbCommand(command, cn);

            cmd.CommandType = CommandType.Text;

            cn.Open();
           

                output = Convert.ToInt32(cmd.ExecuteScalar());

           
           
            cn.Close();
            cmd = null;
            cn = null;

            return output;
        }


        public static int Execute(string command, params OleDbParameter[] parameters)
        {
            int intErr = 0;
            OleDbConnection cn = new OleDbConnection(getConStr);
            OleDbCommand cmd = new OleDbCommand(command, cn);

            cmd.CommandText = command;
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                foreach (OleDbParameter item in parameters)
                    cmd.Parameters.Add(item);

            cn.Open();
          
                cmd.ExecuteNonQuery();
            
           
            cn.Close();

            cmd = null;
            cn = null;

            return intErr;
        }

        public static DataTable readRecord(string query)
        {
          
            DataTable _datatable;
            using (OleDbDataAdapter da = new OleDbDataAdapter(query, getConStr))
            {

                _datatable = new DataTable();
                da.Fill(_datatable);

            }

            return _datatable;

        }
        public enum MsgLevel
        {
            Success=1,
            Warning=2,
            Failure=3,
            Close=4
        }
        public   void  PageMesg(string Mesg,Page pg, dbUtilities. MsgLevel msgLevel=MsgLevel.Success)
        {
            string scr = "<script type='text/javascript'>";
            Mesg = Mesg.Replace("'", @"\'");
            Mesg = Mesg.Replace("\r\n", "\\r\\n");
//            Mesg = "encodeURIComponent(" + Mesg + ")";
            //            scr += Mesg.Replace("\"", @"\""");
            //          scr += scr.Replace("(", @"\(");
            //        scr += scr.Replace(")", @"\)");

            string lvl = "1";
            switch (msgLevel)
            {
                case MsgLevel.Success:
                    lvl = "1";
                    break;
                case MsgLevel.Warning:
                    lvl = "2";
                    break;
                case MsgLevel.Failure:
                    lvl = "3";
                    break;
                case MsgLevel.Close:
                    lvl = "4";
                    break;
            }
            scr += "MasterpageMessage('" + Mesg + "',"+lvl+");";
            scr += "</script>";
            ScriptManager.RegisterStartupScript(pg.Page, pg.GetType(), "mesgkey", scr, false);

            return;
            MasterPage mp = pg.Master;
            UpdatePanel p = (UpdatePanel)mp.FindControl("upMyContracts");

            //   ScriptManager.RegisterStartupScript(CType(Page.Master.FindControl("upMyContracts"), UpdatePanel).Page, CType(Page.Master.FindControl("upMyContracts"), UpdatePanel).ID.GetType(), "getm", "<script type='text/javascript'>MasterpageMessage('test \'message');</script>", False)


            if (p != null)
                ScriptManager.RegisterStartupScript(p.Page, p.ID.GetType(), "mesgkey", scr, false);
            else
                pg.ClientScript.RegisterStartupScript(pg.GetType(), "mesg", scr);
        }
    }
}
