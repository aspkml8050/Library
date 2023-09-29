using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Library.App_Code.CSharp
{
    /****  this is required when installig library   ***/

    /******
    create versions based on changes
    1.1

    ******/
    public class RunSqlScript
    {
        private string conStr;
        public RunSqlScript()
        {
            conStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        }
        /******prepare script at the start, test ok, it should be in transaction not yet, complete it  ****/
        public string Start()
        {
            //set @curversion='1.1' is also set in 1st sql script
            string version = "1.1";
            string rets = "";
            try
            {
                FileInfo fi= new FileInfo(HttpContext.Current.Server.MapPath("~/app_data/TestQuery/10version.sql"));
                string script1=fi.OpenText().ReadToEnd();
            Microsoft.Data.SqlClient.    SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(conStr);
                conn.Open();
                Server server = new Server(new ServerConnection(conn));

                var r= server.ConnectionContext.ExecuteReader(script1);
               
                    while (r.Read())
                    {
                        var d1 = r[0].ToString();
                        var d2= Convert.ToDateTime( r[1]);
                        rets=d1.ToString();
                    }
                    r.Close();
//                if (rets != version)
  //              {
                    fi = new FileInfo(HttpContext.Current.Server.MapPath("~/app_data/TestQuery/11vWebItem.sql"));
                    string script2 = fi.OpenText().ReadToEnd();
                    server.ConnectionContext.ExecuteNonQuery(script2);
    //            }

                conn.Close();

            }catch (Exception ex)
            {
                rets= ex.Message;
            }
            return rets;
        }
    }
}