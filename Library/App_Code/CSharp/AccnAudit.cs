using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for AccnAudit
/// </summary>
/// 
namespace Audit
{

    public class AccnAudit
    {
        public AccnAudit()
        {

        }
        public BookAccn accndata { get; set; }
        public BookCatalog bookcatalog { get; set; }
        public BookAuth bookauth { get; set; }
        public CatalogData catalog { get; set; }
        public BookConf bookconf {get;set;}

        public UpdCatalog update { get; set; }
        public DataTable CompareLists(List<a1tst> lsorg,List<a1tst> lsmods)
        {
           return Compare2Lists(lsorg, lsmods);
        }

        public string InsertAudit(string[] Accn,string appname,string tablename,string ipaddr,int transno,string userid,string Operation="insert")
        {
            string rets = "";
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqn = new SqlConnection(constr);
            SqlTransaction sqtran;
            sqn.Open();
            sqtran = sqn.BeginTransaction();
            try
            {
                SqlCommand sqm = new SqlCommand();
                sqm.Connection = sqn;
                sqm.CommandType = System.Data.CommandType.Text;
                string qer = "select isnull(max(id),0)+1 from AuditTriggerMaster";
                sqm.CommandText = qer;
                sqm.Transaction = sqtran;
                Amaster masterd = new Amaster();
                masterd.id = Convert.ToInt32( sqm.ExecuteScalar());
                qer = "select isnull(max(id),0)+1 from AuditTriggerChild";
               // sqm = new SqlCommand();
                sqm.CommandText = qer;
                sqm.CommandType = System.Data.CommandType.Text;
                int childid = Convert.ToInt32(sqm.ExecuteScalar());

                if (transno == 0)
                {
                    //sqm = new SqlCommand();
                    qer = "select isnull(max(transno),0)+1 from AuditTriggerMaster";
                    sqm.CommandText = qer;
                    transno = Convert.ToInt32(sqm.ExecuteScalar());
                }
                masterd.transno = transno;
                masterd.appname = appname;
                masterd.tablename = tablename;
                masterd.ipaddress = ipaddr;
                masterd.userid = userid;
                masterd.operation = Operation;

              //  sqm = new SqlCommand();
                sqm.CommandText = "sp_AuditInsert";
                sqm.Transaction = sqtran;
                sqm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter p1 ;
                List<Amaster> ls1 = new List<Amaster>();
                ls1.Add(masterd);
                DataTable d1 = CreateDataTable<Amaster>(ls1);
                p1 = sqm.Parameters.AddWithValue("@AuditTriggerMaster", d1);
                p1.SqlDbType = System.Data.SqlDbType.Structured;
                p1.TypeName = "dbo.AuditTriggerMastertype";


                List<Achild> lschild = new List<Achild>();
                for (int indX = 0; indX < Accn.Length; indX++) 
                {
                    if (string.IsNullOrEmpty(Accn[indX]))
                        continue;
                    Achild pc1 = new Achild();
                    pc1.id = childid;
                    pc1.transno = transno;
                    pc1.masterid = masterd.id;
                    pc1.columname = "accessionnumber";
                    pc1.valuebefore = Accn[indX];
                    pc1.valueafter = "";
                    childid++;
                    lschild.Add(pc1);
                }
                DataTable d2 = CreateDataTable<Achild>(lschild);
                SqlParameter p2;
                p2 = sqm.Parameters.AddWithValue("@AuditTriggerChild", d2);
                p2.SqlDbType = System.Data.SqlDbType.Structured;
                p2.TypeName = "dbo.AuditTriggerChildtype";

                sqm.ExecuteNonQuery();
                sqtran.Commit();

                sqn.Close();
            }catch(Exception excp)
            {
                try
                {
                    sqtran.Rollback();
                }
                catch
                {

                }
                rets = excp.Message;
            }
            return rets;
        }
        public string UpdateAudit(UpdCatalog upd, string appname,  string ipaddr, int transno, string userid)
        {
            if (transno == 0)
            {
                return "transno is required.";
            }
            string rets = "";
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqn = new SqlConnection(constr);
            SqlTransaction sqtran;
            sqn.Open();
            sqtran = sqn.BeginTransaction();
            try
            {
                SqlCommand sqm = new SqlCommand();
                sqm.Connection = sqn;
                sqm.CommandType = System.Data.CommandType.Text;
                string qer = "select isnull(max(id),0)+1 from AuditTriggerMaster";
                sqm.CommandText = qer;
                sqm.Transaction = sqtran;
                int idmaster = Convert.ToInt32(sqm.ExecuteScalar());
                List<Amaster> ls1 = new List<Amaster>();

                //int mastId = masterd.id;
                qer = "select isnull(max(id),0)+1 from AuditTriggerChild";
                // sqm = new SqlCommand();
                sqm.CommandText = qer;
                sqm.CommandType = System.Data.CommandType.Text;
                int childid = Convert.ToInt32(sqm.ExecuteScalar());
                for (int indX = 0; indX < upd.lsAccnb4.Count; indX++)
                {
                    upd.lsAccnaft[indX].Id = idmaster;
                    upd.lsAccnb4[indX].Id = idmaster;
                    Amaster masterd = new Amaster();
                    masterd.id = idmaster++;
                    masterd.transno = transno;
                    masterd.appname = appname;
                    masterd.tablename = "bookaccessionmaster";
                    masterd.ipaddress = ipaddr;
                    masterd.userid = userid;
                    masterd.operation = "update";
                    ls1.Add(masterd);
                }

                Amaster masterbcatg = new Amaster();
                

                masterbcatg.id= idmaster;

                masterbcatg.transno = transno;
                masterbcatg.appname = appname;
                masterbcatg.tablename = "bookcatalog";
                masterbcatg.ipaddress = ipaddr;
                masterbcatg.userid = userid;
                masterbcatg.operation = "update";
                Amaster masterbauth = new Amaster();
                idmaster++;
                masterbauth.id = idmaster;
                masterbauth.transno = transno;
                masterbauth.appname = appname;
                masterbauth.tablename = "bookauthor";
                masterbauth.ipaddress = ipaddr;
                masterbauth.userid = userid;
                masterbauth.operation = "update";
                Amaster mastercatgdata = new Amaster();
                idmaster++;
                mastercatgdata.id = idmaster;
                mastercatgdata.transno = transno;
                mastercatgdata.appname = appname;
                mastercatgdata.tablename = "catalogdata";
                mastercatgdata.ipaddress = ipaddr;
                mastercatgdata.userid = userid;
                mastercatgdata.operation = "update";
                Amaster masterbookconf = new Amaster();
                idmaster++;
                masterbookconf.id = idmaster;
                masterbookconf.transno = transno;
                masterbookconf.appname = appname;
                masterbookconf.tablename = "bookconference";
                masterbookconf.ipaddress = ipaddr;
                masterbookconf.userid = userid;
                masterbookconf.operation = "update";

                sqm.CommandText = "sp_AuditInsert";
                sqm.Transaction = sqtran;
                sqm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter p1;
                
                ls1.Add(masterbcatg);
                ls1.Add(masterbauth);
                ls1.Add(mastercatgdata);
                ls1.Add(masterbookconf);
                DataTable d1 = CreateDataTable<Amaster>(ls1);
                p1 = sqm.Parameters.AddWithValue("@AuditTriggerMaster", d1);
                p1.SqlDbType = System.Data.SqlDbType.Structured;
                p1.TypeName = "dbo.AuditTriggerMastertype";
              //  sqm.ExecuteNonQuery();


                DataTable dtcompacn = Compare2Lists(upd.lsAccnb4, upd.lsAccnaft);
                DataTable dtcompcatg = Compare2Lists(upd.lsCatloagb4, upd.lsCatloagaft);
                DataTable dtcatdata = Compare2Lists(upd.lscatagdatab4, upd.lscatagdataaft);
                DataTable dtbookauth = Compare2Lists(upd.lsauthb4, upd.lsauthaft);
                DataTable dtbookconf = Compare2Lists(upd.lsbookconf4, upd.lsbookconfaft);

                List<Achild> lschildaccn = new List<Achild>();
                for (int indX = 0; indX < dtcompacn.Rows.Count; indX++)
                {
                    Achild pc1 = new Achild();
                    pc1.id = childid;
                    pc1.transno = transno;
                    pc1.masterid = Convert.ToInt32(dtcompacn.Rows[indX]["id"]);
                    pc1.columname = dtcompacn.Rows[indX]["columname"].ToString();
                    pc1.valuebefore =  dtcompacn.Rows[indX].IsNull("valuebefore")?"": dtcompacn.Rows[indX]["valuebefore"].ToString();
                    pc1.valueafter = dtcompacn.Rows[indX].IsNull("valueafter") ? "" : dtcompacn.Rows[indX]["valueafter"].ToString();
                    childid++;
                    lschildaccn.Add(pc1);
                }
                DataTable d2 = CreateDataTable<Achild>(lschildaccn);
                SqlParameter p2;
                //p2 = sqm.Parameters.AddWithValue("@AuditTriggerChild", d2);
                //p2.SqlDbType = System.Data.SqlDbType.Structured;
                //p2.TypeName = "dbo.AuditTriggerChildtype";
             //   sqm.ExecuteNonQuery();
                List<Achild> lscatg = new List<Achild>();
                for (int indX = 0; indX < dtcompcatg.Rows.Count; indX++)
                {
                    Achild pc1 = new Achild();
                    pc1.id = childid;
                    pc1.transno = transno;
                    pc1.masterid =  masterbcatg.id;
                    pc1.columname = dtcompcatg.Rows[indX]["columname"].ToString();
                    pc1.valuebefore = dtcompcatg.Rows[indX].IsNull("valuebefore") ? "" : dtcompcatg.Rows[indX]["valuebefore"].ToString();
                    pc1.valueafter = dtcompcatg.Rows[indX].IsNull("valueafter") ? "" : dtcompcatg.Rows[indX]["valueafter"].ToString();
                    childid++;
                    lscatg.Add(pc1);
                }
                DataTable d3 = CreateDataTable<Achild>(lscatg);
                for (int indX = 0; indX < d3.Rows.Count; indX++)
                {
                    d2.ImportRow(d3.Rows[indX]);
                }
                //SqlParameter p3;
                //p3 = sqm.Parameters.AddWithValue("@AuditTriggerChild", d3);
                //p3.SqlDbType = System.Data.SqlDbType.Structured;
                //p3.TypeName = "dbo.AuditTriggerChildtype";
             //   sqm.ExecuteNonQuery();

                List<Achild> lsauth = new List<Achild>();
                for (int indX = 0; indX < dtbookauth.Rows.Count; indX++)
                {
                    Achild pc1 = new Achild();
                    pc1.id = childid;
                    pc1.transno = transno;
                    pc1.masterid = masterbauth.id;
                    pc1.columname = dtbookauth.Rows[indX]["columname"].ToString();
                    pc1.valuebefore = dtbookauth.Rows[indX].IsNull("valuebefore") ? "" : dtbookauth.Rows[indX]["valuebefore"].ToString();
                    pc1.valueafter = dtbookauth.Rows[indX].IsNull("valueafter") ? "" : dtbookauth.Rows[indX]["valueafter"].ToString();
                    childid++;
                    lsauth.Add(pc1);
                }
                DataTable d5 = CreateDataTable<Achild>(lsauth);
                for (int indX = 0; indX < d5.Rows.Count; indX++)
                {
                    d2.ImportRow(d5.Rows[indX]);
                }

                SqlParameter p5;
                //p5 = sqm.Parameters.AddWithValue("@AuditTriggerChild", d5);
                //p5.SqlDbType = System.Data.SqlDbType.Structured;
                //p5.TypeName = "dbo.AuditTriggerChildtype";
           //     sqm.ExecuteNonQuery();


                List<Achild> lscatdat = new List<Achild>();
                for (int indX = 0; indX < dtcatdata.Rows.Count; indX++)
                {
                    Achild pc1 = new Achild();
                    pc1.id = childid;
                    pc1.transno = transno;
                    pc1.masterid =  mastercatgdata .id;
                    pc1.columname = dtcatdata.Rows[indX]["columname"].ToString();
                    pc1.valuebefore = dtcatdata.Rows[indX].IsNull("valuebefore") ? "" : dtcatdata.Rows[indX]["valuebefore"].ToString();
                    pc1.valueafter = dtcatdata.Rows[indX].IsNull("valueafter") ? "" : dtcatdata.Rows[indX]["valueafter"].ToString();
                    childid++;
                    lscatdat.Add(pc1);
                }
                DataTable d4 = CreateDataTable<Achild>(lscatdat);
                for (int indX = 0; indX < d4.Rows.Count; indX++)
                {
                    d2.ImportRow(d4.Rows[indX]);
                }

                //SqlParameter p4;
                //p4 = sqm.Parameters.AddWithValue("@AuditTriggerChild", d4);
                //p4.SqlDbType = System.Data.SqlDbType.Structured;
                //p4.TypeName = "dbo.AuditTriggerChildtype";
                //   sqm.ExecuteNonQuery();


                List<Achild> lsconf = new List<Achild>();
                for (int indX = 0; indX < dtbookconf.Rows.Count; indX++)
                {
                    Achild pc1 = new Achild();
                    pc1.id = childid;
                    pc1.transno = transno;
                    pc1.masterid = masterbookconf.id;
                    pc1.columname = dtbookconf.Rows[indX]["columname"].ToString();
                    pc1.valuebefore = dtbookconf.Rows[indX].IsNull("valuebefore") ? "" : dtbookconf.Rows[indX]["valuebefore"].ToString();
                    pc1.valueafter = dtbookconf.Rows[indX].IsNull("valueafter") ? "" : dtbookconf.Rows[indX]["valueafter"].ToString();
                    childid++;
                    lsconf.Add(pc1);
                }
                DataTable d6 = CreateDataTable<Achild>(lsconf);
                for (int indX = 0; indX < d6.Rows.Count; indX++)
                {
                    d2.ImportRow(d6.Rows[indX]);
                }

                SqlParameter p6;
                p6 = sqm.Parameters.AddWithValue("@AuditTriggerChild", d2);
                p6.SqlDbType = System.Data.SqlDbType.Structured;
                p6.TypeName = "dbo.AuditTriggerChildtype";
                sqm.ExecuteNonQuery();

                sqtran.Commit();

                sqn.Close();
            }
            catch (Exception excp)
            {
                try
                {
                    sqtran.Rollback();
                }
                catch
                {

                }
                rets = excp.Message;
            }
            return rets;
        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                    string nm = properties[i].Name;
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataTable Compare2Lists(List<a1tst> list1, List<a1tst> list2)
        {
            Type type = typeof(a1tst);
            var properties = type.GetProperties();
//            DataTable dataTable = new DataTable();
  //          dataTable.TableName = typeof(T).FullName;
    //        foreach (PropertyInfo info in properties)
      //      {
        //        dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
          //  }
            //dataTable.Columns.Add(new DataColumn("modified"));
           // DataTable dt2comp = dataTable.Clone();
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("rowid");
            dtRet.Columns.Add("columname");
            dtRet.Columns.Add("valuebefore");
            dtRet.Columns.Add("valueafter");
            int rowid = 0;
            foreach (a1tst entity in list1)
            {
                int inx = 0;
                object[] values = new object[properties.Length+1];
                a1tst entity2 = list2[inx];
                for (int i = 0; i < properties.Length; i++)
                {
                
                    object d1 = properties[i].GetValue(entity);
                    object d2 = properties[i].GetValue(entity2);
                    if (!d1.Equals(d2))
                    {
                        string nm = properties[i].Name;
                        DataRow r = dtRet.NewRow();
                        r[0] = rowid;
                        r[1] = nm;
                        r[2] = d1;
                        r[3] = d2;
                        dtRet.Rows.Add(r);
                    }
                    inx++;
                }
                rowid++;
//                dataTable.Rows.Add(values);
            }
            return dtRet;
        }
        public static DataTable Compare2Lists(List<BookAccn> list1, List<BookAccn> list2)
        {
            Type type = typeof(BookAccn);
            var properties = type.GetProperties();
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("rowid");
            dtRet.Columns.Add("columname");
            dtRet.Columns.Add("valuebefore");
            dtRet.Columns.Add("valueafter");
            dtRet.Columns.Add("id");
            int rowid = 0;

            DataTable d1 = CreateDataTable<BookAccn>(list1);
            DataTable d2 = CreateDataTable<BookAccn>(list2);
            DataRow rf = dtRet.NewRow();
            rf[0] = rowid++;
            rf[1] = "acessionnumber";
            rf[2] = d1.Rows[0]["accessionnumber"];
            rf[4] = d1.Rows[0]["id"]; 
            dtRet.Rows.Add(rf);
            for (int indX1 = 0; indX1 < d1.Rows.Count; indX1++)
            {
                for (int indX2 = 0; indX2 < d1.Columns.Count; indX2++)
                {
                    if (d1.Columns[indX2].ColumnName == d2.Columns[indX2].ColumnName)
                    {
                        object one = d1.Rows[indX1][indX2];
                        object two = d2.Rows[indX1][indX2];
                        if (!d1.Rows[indX1][indX2].Equals(d2.Rows[indX1][indX2]))
                        {
                            DataRow r = dtRet.NewRow();
                            r[0] = rowid;
                            r[1] = d1.Columns[indX2].ColumnName;
                            r[2] = d1.Rows[indX1][indX2];
                            r[3]= d2.Rows[indX1][indX2];
                            string p1= d1.Rows[indX1][indX2].ToString();
                            string p2 = d2.Rows[indX1][indX2].ToString();
                            r[4] = d1.Rows[indX1]["id"];
                            bool wrongdate = false;
                            if (d1.Columns[indX2].ColumnName.ToLower().Contains("date"))
                            {
                                if ((p1.Contains("1900")) || (p1.Contains("0001")))
                                {
                                    if ((p2.Contains("1900")) || (p2.Contains("0001")))
                                    {
                                        wrongdate = true;
                                    }

                                }
                            }
                            if (!wrongdate)
                              dtRet.Rows.Add(r);
                        }
                    }
                }
                rowid++;
            }

          /*
            foreach (BookAccn entity in list1)
            {
                int inx = 0;
                object[] values = new object[properties.Length + 1];
                BookAccn entity2 = list2[inx];
                for (int i = 0; i < properties.Length; i++)
                {
                    var vlu = properties[i].GetValue();
                   
                    object d1 = properties[i].GetValue(entity, properties[i]);
                    object d2 = properties[i].GetValue(entity2);
                    if (!d1.Equals(d2))
                    {
                        string nm = properties[i].Name;
                        DataRow r = dtRet.NewRow();
                        r[0] = rowid;
                        r[1] = nm;
                        r[2] = d1;
                        r[3] = d2;
                        dtRet.Rows.Add(r);
                    }
                    inx++;
                }
                rowid++;
            }
          */
            return dtRet;
        }

        public static DataTable Compare2Lists(List<BookCatalog> list1, List<BookCatalog> list2)
        {
            Type type = typeof(BookCatalog);
            var properties = type.GetProperties();
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("rowid");
            dtRet.Columns.Add("columname");
            dtRet.Columns.Add("valuebefore");
            dtRet.Columns.Add("valueafter");
            int rowid = 0;
            DataTable d1 = CreateDataTable<BookCatalog>(list1);
            DataTable d2 = CreateDataTable<BookCatalog>(list2);
           
            for (int indX1 = 0; indX1 < d1.Rows.Count; indX1++)
            {
                for (int indX2 = 0; indX2 < d1.Columns.Count; indX2++)
                {
                    if (d1.Columns[indX2].ColumnName == d2.Columns[indX2].ColumnName)
                    {
                        if (!d1.Rows[indX1][indX2].Equals(d2.Rows[indX1][indX2]))
                        {
                            DataRow r = dtRet.NewRow();
                            r[0] = rowid;
                            r[1] = d1.Columns[indX2].ColumnName;
                            r[2] = d1.Rows[indX1][indX2];
                            r[3] = d2.Rows[indX1][indX2];
                            dtRet.Rows.Add(r);
                        }
                    }
                }
                rowid++;
            }
       /*     foreach (BookCatalog entity in list1)
            {
                int inx = 0;
                object[] values = new object[properties.Length + 1];
                BookCatalog entity2 = list2[inx];
                for (int i = 0; i < properties.Length; i++)
                {

                    object d1 = properties[i].GetValue(entity);
                    object d2 = properties[i].GetValue(entity2);
                    if (!d1.Equals(d2))
                    {
                        string nm = properties[i].Name;
                        DataRow r = dtRet.NewRow();
                        r[0] = rowid;
                        r[1] = nm;
                        r[2] = d1;
                        r[3] = d2;
                        dtRet.Rows.Add(r);
                    }
                    inx++;
                }
                rowid++;
            }
            */
            return dtRet;
        }

        public static DataTable Compare2Lists(List<BookAuth> list1, List<BookAuth> list2)
        {
            Type type = typeof(BookAuth);
            var properties = type.GetProperties();
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("rowid");
            dtRet.Columns.Add("columname");
            dtRet.Columns.Add("valuebefore");
            dtRet.Columns.Add("valueafter");
            int rowid = 0;

            DataTable d1 = CreateDataTable<BookAuth>(list1);
            DataTable d2 = CreateDataTable<BookAuth>(list2);
            for (int indX1 = 0; indX1 < d1.Rows.Count; indX1++)
            {
                for (int indX2 = 0; indX2 < d1.Columns.Count; indX2++)
                {
                    if (d1.Columns[indX2].ColumnName == d2.Columns[indX2].ColumnName)
                    {
                        if (!d1.Rows[indX1][indX2].Equals(d2.Rows[indX1][indX2]))
                        {
                            DataRow r = dtRet.NewRow();
                            r[0] = rowid;
                            r[1] = d1.Columns[indX2].ColumnName;
                            r[2] = d1.Rows[indX1][indX2];
                            r[3] = d2.Rows[indX1][indX2];
                            dtRet.Rows.Add(r);
                        }
                    }
                }
                rowid++;
            }
            /*    foreach (BookAuth entity in list1)
            {
                int inx = 0;
                object[] values = new object[properties.Length + 1];
                BookAuth entity2 = list2[inx];
                for (int i = 0; i < properties.Length; i++)
                {

                    object d1 = properties[i].GetValue(entity);
                    object d2 = properties[i].GetValue(entity2);
                    if (!d1.Equals(d2))
                    {
                        string nm = properties[i].Name;
                        DataRow r = dtRet.NewRow();
                        r[0] = rowid;
                        r[1] = nm;
                        r[2] = d1;
                        r[3] = d2;
                        dtRet.Rows.Add(r);
                    }
                    inx++;
                }
                rowid++;
            }
            */
            return dtRet;
        }
        public static DataTable Compare2Lists(List<CatalogData> list1, List<CatalogData> list2)
        {
            Type type = typeof(CatalogData);
            var properties = type.GetProperties();
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("rowid");
            dtRet.Columns.Add("columname");
            dtRet.Columns.Add("valuebefore");
            dtRet.Columns.Add("valueafter");
            int rowid = 0;

            DataTable d1 = CreateDataTable<CatalogData>(list1);
            DataTable d2 = CreateDataTable<CatalogData>(list2);
            for (int indX1 = 0; indX1 < d1.Rows.Count; indX1++)
            {
                for (int indX2 = 0; indX2 < d1.Columns.Count; indX2++)
                {
                    if (d1.Columns[indX2].ColumnName == d2.Columns[indX2].ColumnName)
                    {
                        if (!d1.Rows[indX1][indX2].Equals(d2.Rows[indX1][indX2]))
                        {
                            DataRow r = dtRet.NewRow();
                            r[0] = rowid;
                            r[1] = d1.Columns[indX2].ColumnName;
                            r[2] = d1.Rows[indX1][indX2];
                            r[3] = d2.Rows[indX1][indX2];
                            dtRet.Rows.Add(r);
                        }
                    }
                }
                rowid++;
            }
         /*   foreach (CatalogData entity in list1)
            {
                int inx = 0;
                object[] values = new object[properties.Length + 1];
                CatalogData entity2 = list2[inx];
                for (int i = 0; i < properties.Length; i++)
                {

                    object d1 = properties[i].GetValue(entity);
                    object d2 = properties[i].GetValue(entity2);
                    if (!d1.Equals(d2))
                    {
                        string nm = properties[i].Name;
                        DataRow r = dtRet.NewRow();
                        r[0] = rowid;
                        r[1] = nm;
                        r[2] = d1;
                        r[3] = d2;
                        dtRet.Rows.Add(r);
                    }
                    inx++;
                }
                rowid++;
            }
            */
            return dtRet;
        }

        public static DataTable Compare2Lists(List<BookConf> list1, List<BookConf> list2)
        {
            Type type = typeof(BookConf);
            var properties = type.GetProperties();
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("rowid");
            dtRet.Columns.Add("columname");
            dtRet.Columns.Add("valuebefore");
            dtRet.Columns.Add("valueafter");
            int rowid = 0;

            DataTable d1 = CreateDataTable<BookConf>(list1);
            DataTable d2 = CreateDataTable<BookConf>(list2);
            for (int indX1 = 0; indX1 < d1.Rows.Count; indX1++)
            {
                for (int indX2 = 0; indX2 < d1.Columns.Count; indX2++)
                {
                    if (d1.Columns[indX2].ColumnName == d2.Columns[indX2].ColumnName)
                    {
                        if (!d1.Rows[indX1][indX2].Equals(d2.Rows[indX1][indX2]))
                        {
                            DataRow r = dtRet.NewRow();
                            r[0] = rowid;
                            r[1] = d1.Columns[indX2].ColumnName;
                            r[2] = d1.Rows[indX1][indX2];
                            r[3] = d2.Rows[indX1][indX2];
                            dtRet.Rows.Add(r);
                        }
                    }
                }
                rowid++;
            }
            /*

            foreach (BookConf entity in list1)
            {
                int inx = 0;
                object[] values = new object[properties.Length + 1];
                BookConf entity2 = list2[inx];
                for (int i = 0; i < properties.Length; i++)
                {

                    object d1 = properties[i].GetValue(entity);
                    object d2 = properties[i].GetValue(entity2);
                    if (!d1.Equals(d2))
                    {
                        string nm = properties[i].Name;
                        DataRow r = dtRet.NewRow();
                        r[0] = rowid;
                        r[1] = nm;
                        r[2] = d1;
                        r[3] = d2;
                        dtRet.Rows.Add(r);
                    }
                    inx++;
                }
                rowid++;
            }
            */
            return dtRet;
        }

    }

    public  class UpdCatalog
    {
        public  List< BookAccn> lsAccnb4 { get; set; }
        public List<BookAccn> lsAccnaft { get; set; }
        public   List<BookCatalog> lsCatloagb4 { get; set; }
        public List<BookCatalog> lsCatloagaft { get; set; }

        public List<BookAuth> lsauthb4 { get; set; }
        public List<BookAuth> lsauthaft { get; set; }

        public List<CatalogData> lscatagdatab4 { get; set; }
        public List<CatalogData> lscatagdataaft { get; set; }
        public List<BookConf> lsbookconf4 { get; set; }
        public List<BookConf> lsbookconfaft { get; set; }
    }

    public class BookAccn
    {
        public int Id { get; set; } //not in use
        public int MasterId { get; set; }
 public string accessionnumber { get; set; }
        public string ordernumber { get; set; }
        public string indentnumber { get; set; }
        public string form { get; set; }
        public double accessionid { get; set; }

      public DateTime  accessioneddate  { get; set; }

    public string booktitle { get; set; }



        public string released { get; set; }
    public decimal bookprice { get; set; }


public string Status { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string IssueStatus { get; set; }
        public DateTime LoadingDate { get; set; }

        public string CheckStatus { get; set; }
        public int ctrl_no { get; set; }

        public int editionyear { get; set; }

        public int Copynumber { get; set; }

        public decimal specialprice { get; set; }
        public int pubYear { get; set; }

        public string biilNo { get; set; }
        public DateTime billDate { get; set; }

        public DateTime catalogdate { get; set; }

        public  string Item_type { get; set; }
        public decimal  OriginalPrice { get; set; }
        public string OriginalCurrency { get; set; }
        public string userid { get; set; }
        public string vendor_source { get; set; }
        public int program_id { get; set; }
        public int DeptCode { get; set; }

 public   string DeptName { get; set; }
        public  int ItemCategoryCode { get; set; }
        public   string ItemCategory { get; set; }
        public int Loc_id { get; set; }
        public  string RfidId { get; set; }
        public  string BookNumber { get; set; }
        public int SetOFbooks { get; set; }
        public string SearchText { get; set; }
        public string IpAddress { get; set; }
        public int TransNo { get; set; }
        public string  AppName { get; set; }




    }

    public class BookCatalog
    {
       public DateTime catalogdate1 { get; set; }
        public int booktype { get; set; }
    public string volumenumber { get; set; }
public string initpages { get; set; }
public int pages { get; set; }
public string parts { get; set; }
        public string leaves{ get; set; }
        public string boundind { get; set; }
        public string title { get; set; }
        public int publishercode { get; set; }
        public string edition { get; set; }
        public string isbn { get; set; }
        public string subject1 { get; set; }
        public string subject2 { get; set; }

        public string subject3 { get; set; }
        public string Booksize { get; set; }
        public string issn { get; set; }
        public string Volume { get; set; }
        public string materialdesignation { get; set; }
        public int dept { get; set; }
        public int language_id { get; set; }
        public string part { get; set; }
        public string cat_Source { get; set; }
        public string firstname { get; set; }
        public string percity { get; set; }
        public string perstate { get; set; }
        public string percountry { get; set; }
        public string peraddress { get; set; }
        public string departmentname { get; set; }
        public string Btype { get; set; }
        public string language_name { get; set; }
        public string PublisherNo { get; set; }
        public string PubSource { get; set; }
        public string SysCtrlNo { get; set; }
        public string GeoArea { get; set; }
        public string latestTransDate { get; set; }
        public string ItemCategory { get; set; }
        public string OriginalCurrency { get; set; }
        public decimal OriginalPrice { get; set; }
        public string SearchText { get; set; }
        public int TransNo { get; set; }

    }

    public class BookAuth
    {
        public string firstname1 { get; set; }
        public string middlename1 { get; set; }
        public string lastname1 { get; set; }
        public string firstname2 { get; set; }
        public string middlename2 { get; set; }
        public string lastname2 { get; set; }
        public string firstname3 { get; set; }
        public string middlename3 { get; set; }
        public string lastname3 { get; set; }
    }
    public class BookConf
    {
        public string Subtitle { get; set; }
    }
    public class CatalogData
    {
        public string classnumber { get; set; }
        public string booknumber { get; set; }
        public string classnumber_l1 { get; set; }
        public string booknumber_l1 { get; set; }
        public string classnumber_l2 { get; set; }
        public string booknumber_l2 { get; set; }
        public int TransNo { get; set; }

    }
    public class Amaster
    {
        public int id { get; set; }
        public string appname { get; set; }
        public string tablename { get; set; }
        public string ipaddress { get; set; }
        public int transno { get; set; }
        public string userid { get; set; }
        public string operation { get; set; }
    }
    public class Achild
    {
        public int id { get; set; }
        public int transno { get; set; }
        public int masterid { get; set; }
        public string columname { get; set; }
        public string valuebefore { get; set; }
        public string valueafter { get; set; }

    }

    public class a1tst
    {
        public string mem1 { get; set; }
        public int it { get; set; }

        public DateTime dates { get; set; }
        public decimal amt { get; set; }
        public bool stat { get; set; }
    }
}
