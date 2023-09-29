using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Library.DataBase
{
    public class SqlScript
    {

        //run script within asp.net
        public void Script()
        {
            SqlConnection con1 = new SqlConnection("YOUR CONNECTIONSTRING");
            string sqlConnectionString = "Data Source=(local);                 Initial Catalog = AdventureWorks; Integrated Security = True";
            FileInfo file = new FileInfo("C:\\myscript.sql");
            string script = file.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(sqlConnectionString));
            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }
}