using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using ItemIssueReturn2;
using Model.Shared;

namespace MSItem
{
    public class PasEncrypo
    {
        public String HashPassword(String password, String salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public String GetRandomSalt(Int32 size = 12)  //unUsed
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        public Boolean ValidatePassword(String enteredPassword, String storedHash, String storedSalt)
        {
            // Consider this function as an internal function where parameters like
            // storedHash and storedSalt are read from the database and then passed.
            var hash = HashPassword(enteredPassword, storedSalt);
            return String.Equals(storedHash, hash);
        }
    }

        //if ObjectPKeys array passed , then object(table or view will be checked for permissions)
        //in case of running transaction pass sqlcomm so no new connection is opened

         // if cook UserNm is not null, then allow all
        //check the if for objnm int? i.e. null pkey then assumed allowed, for the object overall for said permission
    }
    public class GlobClass
    {
    //make sure a sqlconnection string with this name exists.

    public string ConnS(string ConStr = "")
        {

            //get all
            foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                //use c.Name
            }
            //make sure a sqlconnection string with this name exists.
            if (ConStr == "")
                ConStr = "SqlConn";
            string a = ConfigurationManager.ConnectionStrings[ConStr].ConnectionString;
            return a;
        }
        SqlConnection sqCon = new SqlConnection();
     public   SqlDataReader sqRd;// = new SqlDataReader();
        SqlCommand sqCom;//= new SqlCommand();
        public void ClosRdr()
        {
            if ((sqRd!=null) && (!sqRd.IsClosed))
            {
                sqRd.Close();
            }
            if (sqCom != null)
            {
                sqCom.Dispose();
                sqCom = null;

            }

            if (sqCon.State == ConnectionState.Open)
            {
                sqCon.Close();
            }
        }
        public string GetRdrAct( string Qery,string ActNm="", string ConStr = "")
        {
            try
            {
                
                if (sqCon.State != ConnectionState.Open)
                {
                    sqCon = new SqlConnection(ConnS(ConStr));
                    sqCon.Open();

                }
                if (sqCom==null)
                {
                    try
                    {
                    }
                    catch { }
                    //                    string sLocQr = "select count(*) from roleperm where rid=1 and mid and perm ";
                    sqCom = new SqlCommand(Qery);
                    sqCom.Connection = sqCon;
                    sqRd = sqCom.ExecuteReader();
                }
                if (!sqRd.HasRows)
                {
                    sqRd.Close();
                    return "End";
                }
          bool Sta=           sqRd.Read();
                if (Sta==false)
                {
                
                    sqRd.Close();
                    sqCom.Dispose();
                    sqCon.Close();
                    return "End";
                }
                try
                {
                    string av = sqRd[0].ToString();
                }
                catch (Exception ex)
                {
                    sqCom.Dispose();
                    sqCon.Close();
                    if (!sqRd.IsClosed)
                        sqRd.Close();
                    if (ex.Message.ToLower().Contains("no data"))
                        return "No Data";
                }
                return "";
            }
            catch (Exception ex)
            {
                return "Error:"+ex.Message;
            }
        }
        
        public string IUD(string Qer, List<SqlParameter> Parms=null,string ActNm="",string ConStr="")
        {
            try
            {
                if (sqCon.State != ConnectionState.Open)
                {
                    sqCon = new SqlConnection(ConnS(ConStr));
                    sqCon.Open();

                }
                if ((sqRd!=null) && (!sqRd.IsClosed))
                    sqRd.Close();
                    sqCom = new SqlCommand(Qer);
                    sqCom.Connection = sqCon;
                    if (Parms != null)
                    {
                        foreach (SqlParameter Pa in Parms)
                        {
                            sqCom.Parameters.Add(Pa);
                        }

                    }
                    sqCom.ExecuteNonQuery();
                
                sqCon.Close();
                return "";
            }
            catch (Exception ex)
            {
                sqCon.Close();
                return ex.Message;
            }
        }
        public string ExProc(string StorPOrFunc,List<SqlParameter> Parms, string ActNm = "", string ConStr = "")
        {
            try
            {
                if (sqCon.State != ConnectionState.Open)
                    sqCon.Open();
                sqCom = new SqlCommand(StorPOrFunc);
                sqCom.Connection = sqCon;
                sqCom.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter Pa in Parms)
                    sqCom.Parameters.Add(Pa);
                sqCom.ExecuteNonQuery();
                sqCom.Dispose();
                sqCon.Close();
                return "";
            }
            catch (Exception ex)
            {
                if (sqCon.State == ConnectionState.Open)
                    sqCon.Close();
                return ex.Message;
            }
        }
    public object ExScaler(string Qery)
        {
            try
            {
                if (sqCon.State != ConnectionState.Open)
                {
                    sqCon = new SqlConnection(ConnS());
                    sqCon.Open();

                }
                sqCom = new SqlCommand(Qery);
                sqCom.Connection = sqCon;
                sqCom.CommandType = CommandType.Text;
                return sqCom.ExecuteScalar();

            }
            catch (Exception ex)
            {
                if (sqCon.State == ConnectionState.Open)
                    sqCon.Close();
                return ex.Message;

            }
        }
        public string Capit(string sString)
        {
            Capit Capi = new Capit();
            string a1 = Capi.InitCap(sString);
            return a1;
        }
    }
    public class Capit
    {
        public string InitCap(string Str)
        {
            if ((Str.Trim() == ""))
            {
                return Str.Trim();
            }
            Str = Str.Trim();
            //      RegexOptions options = RegexOptions.None;
            // Regex regex = new Regex(@"[ ]{2,}", options);     
            // tempo = regex.Replace(tempo, @" ");
            RegexOptions regOpt = RegexOptions.None;
            Regex reg = new Regex("[ ]{2,}", regOpt);
            Str = reg.Replace(Str, " ");
            string[] spC =           { " ","[", "{",
            ",",
            ".",
            "=",
            "-",
            "%",
            "@",
            "!",
            "$",
            "?",
            "<",
            ">",
            "+",
            "(",
            ")",
            "&",
            "~",
            ":",
            ";",
            "_",
            "*",
            "#",
            "%",
            "?",
            "/"};
            string OStr = Str.Substring(0, 1).ToUpper();
            for (int ord = 1; (ord <= (Str.Length - 1)); ord++)
            {
                string fCar = Str.Substring((ord - 1), 1);
                string fF = Array.Find(spC, ele => ele.Contains(fCar));
                //                string fouND = Array.Find(spC, Function, x[x.ToString.Contains(fCar)]);
                if ((fF == null))
                {
                    Str.Substring(ord, 1).ToUpper();
                }
                else
                {
                    Str.Substring(ord, 1).ToLower();
                }
            }
            return OStr;
        }

    }
    public class GlobClassTr
    {
    public string ConnS(string ConStr = "")
    {

        //get all
        foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
        {
            //use c.Name
        }
        //make sure a sqlconnection string with this name exists.
        if (ConStr == "")
            ConStr = "SqlConn";
        string a = ConfigurationManager.ConnectionStrings[ConStr].ConnectionString;
        return a;
    }

    private SqlConnection ConnT;//= new SqlConnection( ConnStr. ConnS);
        //  GlobClass locCons = new GlobClass();
    public string ConS {

        get
        {
            return ConnS();
        }
    } 
        private SqlCommand Comm;

  //      private SqlDataAdapter Ada;
        public SqlDataReader sqRd;// = new SqlDataReader();

        private SqlTransaction Tran;
        private DataTable dtPerm = new DataTable();
   public    byte[] GetBytes(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }
    public void TrOpen(string ConStr="",string WebConfConNam="")
        {
        //if (ConnT.State == ConnectionState.Open)
        //{
        if (ConStr != "")
        {
    //            string a = ConfigurationManager.ConnectionStrings[ConStr].ConnectionString;
                ConnT = new SqlConnection(ConStr);
                ConnT.Open();
                Tran = ConnT.BeginTransaction(IsolationLevel.ReadUncommitted);
            return;
        }
        else if(WebConfConNam != ""){
                        string a = ConfigurationManager.ConnectionStrings[WebConfConNam].ConnectionString;
            ConnT = new SqlConnection(a);
            ConnT.Open();
            Tran = ConnT.BeginTransaction(IsolationLevel.ReadUncommitted);
            return;

        }

        try
            {
              if (ConnT!=null)
                  ConnT.Close();
                  
            }
            catch { }
            //}
            ConnT = new SqlConnection(ConS);
            ConnT.Open();
            Tran = ConnT.BeginTransaction(IsolationLevel.ReadUncommitted);
        }
      public void ClosRdr()
    {
        if ((sqRd != null) && (!sqRd.IsClosed))
            sqRd.Close();

    }
    public void TrClose()
        {
        if (Tran != null)
        {
            if ((sqRd != null) && (!sqRd.IsClosed))
                sqRd.Close();
            Tran.Commit();
            ConnT.Close();
        }
        }
        public void TrRollBack()
        {
            if ((ConnT.State == ConnectionState.Open))
            {
            if ((sqRd!=null) &&  (!sqRd.IsClosed))
                sqRd.Close();
            Tran.Rollback();
                ConnT.Close();
            }
        }

    public DataTable DataT(string sQery)
    {
        Comm = new SqlCommand(sQery);
        Comm.Transaction = Tran;
        Comm.Connection = ConnT;
        Comm.CommandTimeout = 1500;
        SqlDataAdapter sAd = new SqlDataAdapter(Comm);
      
        DataTable dtR = new DataTable();
        sAd.Fill(dtR);
        return dtR;
    }
    public DataSet DataSetT(string sQery)
    {
        Comm = new SqlCommand(sQery);
        Comm.Transaction = Tran;
        Comm.Connection = ConnT;
        Comm.CommandTimeout = 1500;
        SqlDataAdapter sAd = new SqlDataAdapter(Comm);

        DataSet dtR = new DataSet();
        sAd.Fill(dtR);
        return dtR;
    }
    public void IUD(/*string App, int Usid,*/ string Qry,SqlParameter[] Parms=null, string ActNm = "",string ConStr = "")
        {
            Comm = new SqlCommand(Qry);
            if (Parms != null)
            {
                foreach (SqlParameter Pa in Parms)
                {
                    Comm.Parameters.Add(Pa);
                }

            }

            Comm.Transaction = Tran;
            Comm.Connection = ConnT;
        Comm.CommandTimeout = 1500;

        Comm.ExecuteNonQuery();
        
    }
        public Object ExScaler(/*string App, int Usid,*/ string Qry)
        {
            Comm = new SqlCommand(Qry);
            Comm.Transaction = Tran;
            Comm.Connection = ConnT;
        Comm.CommandTimeout = 1500;

        return Comm.ExecuteScalar();
        }
    public ReturnData<string> ExProcReturn(string StorPOrFunc, List<SqlParameter> Parms)
    {
        if (ConnT.State != ConnectionState.Open)
            throw new ApplicationException("Connection is Not opened.");
        Comm = new SqlCommand(StorPOrFunc);
        Comm.Connection = ConnT;
        Comm.Transaction = Tran;
        Comm.CommandTimeout = 1500;
        Comm.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter Pa in Parms)
            Comm.Parameters.Add(Pa);
        var r= Comm.ExecuteReader();
        DataTable dtR = new DataTable();
        ReturnData<string> rd = new ReturnData<string>();
            r.Read();
        rd.isSuccess=r.GetBoolean(0);
        rd.Mesg = r.GetString(1);
        r.Close();
        return rd;
        //             Comm.Dispose();

    }
    public SqlCommand getSqlCom(string Q = "", CommandType cType = CommandType.Text)
        {
            if (Q != "")
                Comm = new SqlCommand(Q);
            else
                Comm = new SqlCommand();
            Comm.CommandType = cType;
            Comm.Transaction = Tran;
            Comm.Connection = ConnT;
            return Comm;
        }
        public void ExProc(string StorPOrFunc, List<SqlParameter> Parms, string ActNm = "",  string ConStr = "")
        {
                if (ConnT.State != ConnectionState.Open)
                    throw new ApplicationException( "Connection is Not opened.");
                Comm = new SqlCommand(StorPOrFunc);
                Comm.Connection = ConnT;
                Comm.Transaction=Tran;
        Comm.CommandTimeout = 1500;

        Comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter Pa in Parms)
                    Comm.Parameters.Add(Pa);
                Comm.ExecuteNonQuery();
        //             Comm.Dispose();
        
        }
    public DataSet ExProcDS(string StorPOrFunc, List<SqlParameter> Parms, string ActNm = "", string ConStr = "")
    {
        DataSet dsRet= new DataSet();
        if (ConnT.State != ConnectionState.Open)
            throw new ApplicationException("Connection is Not opened.");
        Comm = new SqlCommand(StorPOrFunc);
        Comm.Connection = ConnT;
        Comm.Transaction = Tran;
        Comm.CommandTimeout = 1500;

        Comm.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter Pa in Parms)
            Comm.Parameters.Add(Pa);
        var adapter = new SqlDataAdapter(Comm);
        adapter.Fill(dsRet);
        //Comm.ExecuteNonQuery();
        return dsRet;
        //             Comm.Dispose();

    }
    public void ExProcshow(string StorPOrFunc, List<SqlParameter> Parms, string ActNm = "", string ConStr = "")
    {
        DataTable dt = new DataTable();
        if (ConnT.State != ConnectionState.Open)
            throw new ApplicationException("Connection is Not opened.");
        Comm = new SqlCommand(StorPOrFunc);
        Comm.Connection = ConnT;
        Comm.Transaction = Tran;
        Comm.CommandTimeout = 1500;

        Comm.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter Pa in Parms)
            Comm.Parameters.Add(Pa);
        SqlDataAdapter sAd = new SqlDataAdapter(Comm);
        DataTable dtR = new DataTable();
        sAd.Fill(dtR);
        return ;
       
        //Comm.ExecuteNonQuery();
        //             Comm.Dispose();
    }
    
    public string getRdrAct(string Qery, string ActNm = "", string ConStr = "")
        {
            try
            {

                if (ConnT.State != ConnectionState.Open)
                {
                    return "Connection is Not opened.";

                }
                if (Comm == null)
                {
                    Comm = new SqlCommand(Qery);
                    Comm.Connection = ConnT;
                    Comm.Transaction = Tran;
                    sqRd = Comm.ExecuteReader();
                }
                if (( (sqRd==null) ||   (sqRd.IsClosed)) && (Comm!=null))
            {
                Comm = new SqlCommand(Qery);
                Comm.Connection = ConnT;
                Comm.Transaction = Tran;
                sqRd = Comm.ExecuteReader();

            }
            if (!sqRd.HasRows)
                {
                    sqRd.Close();
                    return "End";
                }
                bool Sta = sqRd.Read();
                if (Sta == false)
                {
                    sqRd.Close();
                    return "End";
                }
                try
                {
                    string av = sqRd[0].ToString();
                }
                catch (Exception ex)
                {
                    Comm.Dispose();
                    ConnT.Close();
                    if (!sqRd.IsClosed)
                        sqRd.Close();
                    if (ex.Message.ToLower().Contains("no data"))
                        return "No Data";
                }
                return "";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }

        }
    


}

