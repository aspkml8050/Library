using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Library.App_Code.CSharp
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
    public interface IDataBase
    {
        IDbConnection CreateConnection(string dbConnectionString);

        IDbCommand CreateCommand();

        IDbConnection CreateOpenConnection(string dbConnectionString);

        IDbCommand CreateCommand(string commandText, IDbConnection connection);

        IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);

        IDataParameter CreateParameter(string parameterName, object parameterValue);

        IDataAdapter CreateAdatapter(SqlCommand cmd);
    }
    public class SqlDataBase : IDataBase
    {
        public static string UserdbConnectionString { get; private set; }

        public static string ERPdbConnectionString { get; private set; }

        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public IDataAdapter CreateAdatapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }

        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            SqlCommand sqlCommand = (SqlCommand)CreateCommand();
            sqlCommand.CommandText = commandText;
            sqlCommand.Connection = (SqlConnection)connection;
            sqlCommand.CommandType = CommandType.Text;
            return sqlCommand;
        }

        public IDbConnection CreateConnection(string dbConnectionString)
        {
            return new SqlConnection(dbConnectionString);
        }

        public IDbConnection CreateOpenConnection(string dbConnectionString)
        {
            SqlConnection sqlConnection = (SqlConnection)CreateConnection(dbConnectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        public IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            SqlCommand sqlCommand = (SqlCommand)CreateCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.Connection = (SqlConnection)connection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return sqlCommand;
        }

        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection, SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = (SqlCommand)CreateCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.Connection = (SqlConnection)connection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            if (commandParameters != null)
            {
                foreach (SqlParameter sqlParameter in commandParameters)
                {
                    if (sqlParameter.Direction == ParameterDirection.InputOutput && sqlParameter.Value == null)
                    {
                        sqlParameter.Value = DBNull.Value;
                    }

                    sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            return sqlCommand;
        }

        public IDbCommand CreateStoredFuncCommand(string procName, IDbConnection connection, SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = (SqlCommand)CreateCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.Connection = (SqlConnection)connection;
            sqlCommand.CommandType = CommandType.Text;
            if (commandParameters != null)
            {
                foreach (SqlParameter sqlParameter in commandParameters)
                {
                    if (sqlParameter.Direction == ParameterDirection.InputOutput && sqlParameter.Value == null)
                    {
                        sqlParameter.Value = DBNull.Value;
                    }

                    sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            return sqlCommand;
        }

        public void ExceProc(string ProcName, ref DataSet objDataset, SqlParameter[] CommandParameters, string dbConnectionString)
        {
            SqlCommand cmd = (SqlCommand)CreateStoredProcCommand(ProcName, (SqlConnection)CreateOpenConnection(dbConnectionString), CommandParameters);
            SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)CreateAdatapter(cmd);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            objDataset = dataSet;
        }

        public void ExceProc(string ProcName, out DataTable objDataset, SqlParameter[] commandParameters, string dbConnectionString)
        {
            SqlCommand cmd = (SqlCommand)CreateStoredProcCommand(ProcName, (SqlConnection)CreateOpenConnection(dbConnectionString), commandParameters);
            SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)CreateAdatapter(cmd);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            objDataset = dataSet.Tables[0];
        }

        public void ExceFunc(string FuncString, ref DataTable objDataset, SqlParameter[] CommandParameters, string dbConnectionString)
        {
            SqlCommand cmd = (SqlCommand)CreateStoredFuncCommand(FuncString, (SqlConnection)CreateOpenConnection(dbConnectionString), CommandParameters);
            SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)CreateAdatapter(cmd);
            sqlDataAdapter.Fill(objDataset);
        }

        public static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter sqlParameter in commandParameters)
            {
                if (sqlParameter.Direction == ParameterDirection.InputOutput && sqlParameter.Value == null)
                {
                    sqlParameter.Value = DBNull.Value;
                }

                command.Parameters.Add(sqlParameter);
            }
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        }
    }
    public class DataBaseRepository
    {
        public DataTable GetMenuDataDL(int UserTypeId)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[2]
            {
                new SqlParameter("@UserTypeId", UserTypeId),
                null
            };
            array[1].SqlDbType = SqlDbType.Int;
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("select * from dbo.menuitems (@UserTypeId )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetDepartmentMaster(int DeptID, int InstID, int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@DeptID", DeptID),
                new SqlParameter("@InstID", InstID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetDept](@DeptID, @InstID, @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetPrefixMaster(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_Getprefixmaster](@UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetOrderMaster(int UserID, int FormID, int Type, string listallcatogery)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@listallcatogery", listallcatogery)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_Getordermaster](@UserID, @FormID, @Type , @listallcatogery)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Getlibrarysetup(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Getlibrarysetup](@UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GettempAccespointsMaxId(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GettempAccespointsMaxId](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIssuing_Auth_master(int id, int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetIssuing_Auth_master](@id, @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Issuing_Auth_masterInsert(int id, string AuthName, string shortname, string Bar_Council, int userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@AuthName", AuthName),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@Bar_Council", Bar_Council),
                new SqlParameter("@userid", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Issuing_Auth_master]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetIDCardBack(int DescId, int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@DescId", DescId),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_Getidcardbackdesc](@DescId, @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable IdcardbackdescAddUpdate(string DescTitle, string DescInfo, int DescId, int userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@DescTitle", DescTitle),
                new SqlParameter("@DescInfo", DescInfo),
                new SqlParameter("@DescId", DescId),
                new SqlParameter("@userid", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[proc_idcardbackdescAddUpdate]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetProgramMaster(int InstID, int DeptID, int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[5];
            array[1] = new SqlParameter("@InstID", InstID);
            array[0] = new SqlParameter("@DeptID", DeptID);
            array[2] = new SqlParameter("@UserID", UserID);
            array[3] = new SqlParameter("@FormID", FormID);
            array[4] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetProgramme](@InstID, @DeptID, @UserID, @FormID, @Type)", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCasteCategories(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[3];
            array[1] = new SqlParameter("@UserID", UserID);
            array[0] = new SqlParameter("@FormID", FormID);
            array[2] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetCastCat](@UserID , @FormID , @Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBindddl(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[3];
            array[1] = new SqlParameter("@UserID", UserID);
            array[0] = new SqlParameter("@FormID", FormID);
            array[2] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GetBindddl](@UserID , @FormID , @Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Fn_GetStatusMaster(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[3];
            array[1] = new SqlParameter("@UserID", UserID);
            array[0] = new SqlParameter("@FormID", FormID);
            array[2] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[Fn_GetStatusMaster](@UserID , @FormID , @Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetDAFileInfo(int UserID, int FormID, int Type, string daid, string typeno)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@DAid", daid),
                new SqlParameter("@typeno", typeno)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[GetDAFileInfo](@UserID , @FormID , @Type , @DAid, @typeno)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Fn_GetFileSizeMaster(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[3];
            array[1] = new SqlParameter("@UserID", UserID);
            array[0] = new SqlParameter("@FormID", FormID);
            array[2] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[Fn_GetFileSizeMaster](@UserID , @FormID , @Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetDigitalinfo(int UserID, int FormID, int Type, int id)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@id", id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[GetDigitalinfo](@UserID , @FormID , @Type, @id )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetInstituteMaster(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetInstitute](@UserID , @FormID , @Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCatogeryLoading(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetCategoryLoading](@UserID , @FormID , @Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Getregistermaster(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GetRegisterMAster](@UserID , @FormID , @Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBIndtransmaster(int UserID, int FormID, int Type, string cmbbinder)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Cmbbinder", cmbbinder)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GETBINDTRANSMASTER](@UserID , @FormID , @Type,  @Cmbbinder)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetRegister_In(int UserID, int FormID, int Type, int view, int eaid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@view", view),
                new SqlParameter("@eaid", eaid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GetRegister_IN](@UserID , @FormID , @Type,  @view, @eaid)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable SaveProgramMaster(int Progid, string Progname, string Shortname, int Deptid, int UserId, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@ProgramID", Progid),
                new SqlParameter("@ProgramName", Progname),
                new SqlParameter("@Shortname", Shortname),
                new SqlParameter("@DeptID", Deptid),
                new SqlParameter("@UserID", UserId),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateProg]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable SaveCasteCategories(int CatID, string CatName, string shortname, int UserID, int FormID, int Type)
        {
            DataTable dataTable = new DataTable();
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@CatID", CatID),
                new SqlParameter("@CatName", CatName),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateCasteCat]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpsertInstitute(int InstituteCode, string InstituteName, string shortname, int userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@InstituteCode", InstituteCode),
                new SqlParameter("@InstituteName", InstituteName),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@userid", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateInstitute]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetLoadingStatus(int ItemstatusID, int UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[4];
            array[1] = new SqlParameter("@UserID", UserID);
            array[0] = new SqlParameter("@itemstatusid", ItemstatusID);
            array[2] = new SqlParameter("@FormID", FormID);
            array[3] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("Select * FROM [MTR].[Fn_GetLoadingStatus](@itemstatusid,@UserID , @FormID , @Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertDepartmentMaster(int departmentcode, string departmentname, string shortname, int institutecode, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@departmentcode_1", departmentcode),
                new SqlParameter("@departmentname_2", departmentname),
                new SqlParameter("@shortname_3", shortname),
                new SqlParameter("@institutecode_4", institutecode),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_departmentmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertPrefixMaster(int prefixid, string prefixname, int startno, int currenposition, string status, int category, string userid, string suffixname)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@prefixid_1", prefixid),
                new SqlParameter("@prefixname_2 ", prefixname),
                new SqlParameter("@startno_3", startno),
                new SqlParameter("@currentposition_4", currenposition),
                new SqlParameter("@status_5", status),
                new SqlParameter("@Category_6", category),
                new SqlParameter("@userid_7", userid),
                new SqlParameter("@suffixname_8", suffixname)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PrefixMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCategoryStatus(int id, string Category_LoadingStatus, string Abbreviation, string cat_icon, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id_1", id),
                new SqlParameter("@Category_LoadingStatus_2", Category_LoadingStatus),
                new SqlParameter("@Abbreviation_3", Abbreviation),
                new SqlParameter("@cat_icon_4", cat_icon),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CategoryLoadingStatus_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertLoadingStatus(int ItemStatusID, string ItemStatus, string ItemStatusShort, string isBardateApllicable, string isIsued, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@ItemStatusID_1", ItemStatusID),
                new SqlParameter("@ItemStatus_2", ItemStatus),
                new SqlParameter("@ItemStatusShort_3", ItemStatusShort),
                new SqlParameter("@isBardateApllicable_4", isBardateApllicable),
                new SqlParameter("@isIsued_5", isIsued),
                new SqlParameter("@userid_6", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ItemStatusMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertMediatype(int media_id, string media_name, string short_name, int userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@media_id_1", media_id),
                new SqlParameter("@media_name_2", media_name),
                new SqlParameter("@short_name_3", short_name),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateMediaType]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertPublisherMaster(string PublisherId, string PublisherCode, string firstname, string PublisherPhone1, string PublisherPhone2, string EmailID, string webaddress, string PublisherType, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@PublisherId_1", PublisherId),
                new SqlParameter("@PublisherCode_2", PublisherCode),
                new SqlParameter("@firstname_3", firstname),
                new SqlParameter("@PublisherPhone1_4", PublisherPhone1),
                new SqlParameter("@PublisherPhone2_5", PublisherPhone2),
                new SqlParameter("@EmailID_6", EmailID),
                new SqlParameter("@webaddress_7", webaddress),
                new SqlParameter("@PublisherType_8", PublisherType),
                new SqlParameter("@userid_9", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_publishermaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertissuesubscription(int Id, string IssueType, DateTime DocDate, string DocNumber, string ItemCode, string Quantity, float Rate, float Amount, float OtherExp, float TotalAmount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@IssueType", IssueType),
                new SqlParameter("@DocDate", DocDate),
                new SqlParameter("@DocNumber", DocNumber),
                new SqlParameter("@ItemCode", ItemCode),
                new SqlParameter("@Quantity", Quantity),
                new SqlParameter("@Rate", Rate),
                new SqlParameter("@Amount", Amount),
                new SqlParameter("@OtherExp", OtherExp),
                new SqlParameter("@TotalAmount", TotalAmount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_IssueSubscription]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertreceiptsubscription(int Id, DateTime TransactionDate, string ItemCode, string Quantity, float Rate, int SupplierId, float Amount, float OtherExp, float TotalAmt, string Remak, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@TransactionDate", TransactionDate),
                new SqlParameter("@ItemCode", ItemCode),
                new SqlParameter("@Quantity", Quantity),
                new SqlParameter("@Rate", Rate),
                new SqlParameter("@SupplierId", SupplierId),
                new SqlParameter("@Amount", Amount),
                new SqlParameter("@OtherExp", OtherExp),
                new SqlParameter("@TotalAmt", TotalAmt),
                new SqlParameter("@Remak", Remak),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ReceiptSubscription]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertItemMaster(int id, string title, string sub_title, DateTime pub_day, string IssN_No, string Issue_No, string Volume, string Part_No, string Copy_No, string Lack_No, string Edition, string Edition_Year, int Language, string Publisher, string Vendor, int Currancy, decimal Price, string flg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@ID", id),
                new SqlParameter("@Title", title),
                new SqlParameter("@sub_Title", sub_title),
                new SqlParameter("@Pub_Day", pub_day),
                new SqlParameter("@IssN_No", IssN_No),
                new SqlParameter("@Issue_No", Issue_No),
                new SqlParameter("@Volume", Volume),
                new SqlParameter("@Part_No", Part_No),
                new SqlParameter("@Copy_No", Copy_No),
                new SqlParameter("@Lack_No", Lack_No),
                new SqlParameter("@Edition", Edition),
                new SqlParameter("@Edition_Year", Edition_Year),
                new SqlParameter("@Language", Language),
                new SqlParameter("@Publisher", Publisher),
                new SqlParameter("@Vendor", Vendor),
                new SqlParameter("@Currency", Currancy),
                new SqlParameter("@Price", Price),
                new SqlParameter("@flg", flg)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Item_Master]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertsubscription(int id, DateTime date, string item, string Quantity, decimal rate, decimal amount, string period, DateTime ToDate, string subscription_code, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Date", date),
                new SqlParameter("@Item", item),
                new SqlParameter("@Quantity", Quantity),
                new SqlParameter("@Rate", rate),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Period", period),
                new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@subscriber_code", subscription_code),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SubscriptionMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertservicemaster(int serviceid, string service, decimal price, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@service_id_1", serviceid),
                new SqlParameter("@service_2", service),
                new SqlParameter("@price_3", price),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Services_master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertlibraryservices(int invoice_id, string invoice_no, DateTime invoice_date, string library, string member, decimal service_tax, decimal cess, DateTime duedate, decimal actual_amt, decimal total_amt, decimal postage, decimal tax, string userid, string pmt_type, decimal paidAmt, decimal balanceAmt, string DD_chkno, DateTime DD_chkdate, decimal DD_charge, string bank)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[20]
            {
                new SqlParameter("@invoice_Id_1", invoice_id),
                new SqlParameter("@invoice_no_2", invoice_no),
                new SqlParameter("@invoice_date_3", invoice_date),
                new SqlParameter("@library_4", library),
                new SqlParameter("@member_5", member),
                new SqlParameter("@service_tax_6", service_tax),
                new SqlParameter("@cess_7", cess),
                new SqlParameter("@duedate_8", duedate),
                new SqlParameter("@Actual_amt_9", actual_amt),
                new SqlParameter("@total_amt_10", total_amt),
                new SqlParameter("@postage_11", postage),
                new SqlParameter("@tax_12", tax),
                new SqlParameter("@userid_13", userid),
                new SqlParameter("@Pmt_Type_14", pmt_type),
                new SqlParameter("@PaidAmt_15", paidAmt),
                new SqlParameter("@BallanceAmt_16", balanceAmt),
                new SqlParameter("@DD_ChkNo_17", DD_chkno),
                new SqlParameter("@DD_ChkDate_18", DD_chkdate),
                new SqlParameter("@DD_charge_19", DD_charge),
                new SqlParameter("@Bank_20", bank)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_library_servicesMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetUserInformaton(string UserCode, bool Exact, int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@UserCode", UserCode),
                new SqlParameter("@Exact", Exact),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetUserInformaton](@UserCode, @Exact, @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetLibFiles(string FileName, string Number, bool Exact, int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@FileName", FileName),
                new SqlParameter("@Number", Number),
                new SqlParameter("@Exact", Exact),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLibFiles](@FileName,@Number, @Exact, @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetUserView(string UserID2, string name, string Departmenntname, string classname, string Joinyear, int Program_id, string subjects, string Opac_status, int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@UserID2", UserID2),
                new SqlParameter("@name", name),
                new SqlParameter("@Departmenntname", Departmenntname),
                new SqlParameter("@classname", classname),
                new SqlParameter("@Joinyear", Joinyear),
                new SqlParameter("@Program_id", Program_id),
                new SqlParameter("@subjects", subjects),
                new SqlParameter("@Opac_status", Opac_status),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetUserView](@UserID2, @name,@Departmenntname, @classname,@Joinyear,@Program_id,@subjects, @Opac_status,        @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertSymbols(int SymbolTypeId, string SymbolType, string Symbol, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@SymbolTypeId_1", SymbolTypeId),
                new SqlParameter("@SymbolType_2", SymbolType),
                new SqlParameter("@Symbol_3", Symbol),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Symbols_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GenerateUserid(int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GenerateUserid]()", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCircClass(int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCircClass](@UserID,@FormID,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetAcedemicSessionInformation(int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetAcedemicSessionInformation](@UserID,@FormID,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBinderinfo(int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Getbindgrid](@UserID,@FormID,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetcircHolidays(int FormID, int UserID, int Type, DateTime hdate)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Hdate", hdate)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetcircHolidays](@UserID,@FormID,@Type,@Hdate)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetcircHolidaysMaxID(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetcircHolidaysMAxID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBinderMaxid(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetBinderMaxID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBinderinfoMaxid(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Getbinderinvoice](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertSubjectMaster(int subject_id, string subject, int userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@subject_id_1", subject_id),
                new SqlParameter("@subject_2", subject),
                new SqlParameter("@userid_3", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_subject_master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertTransalation(int Language_Id, string Language_Name, string Font_Name, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[4];
            array[0] = new SqlParameter("@Language_Id_1", Language_Id);
            array[0] = new SqlParameter("@Language_Name_2", Language_Name);
            array[0] = new SqlParameter("@Font_Name_3", Font_Name);
            array[0] = new SqlParameter("@userid_4", userid);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Translation_Language_1]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertVendorMaster(string vendorid, string vendorcode, string vendorname, string vendorwebaddress, string phone1, string phone2, string emailID, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@vendorid_1", vendorid),
                new SqlParameter("@vendorcode_2", vendorcode),
                new SqlParameter("@vendorname_3", vendorname),
                new SqlParameter("@vendorwebaddress_4", vendorwebaddress),
                new SqlParameter("@phone1_5", phone1),
                new SqlParameter("@phone2_6", phone2),
                new SqlParameter("@emailID_7", emailID),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_vendormaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertItemType(int Id, string Item_Type, string Abbreviation, byte[] Item_icon, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Id_1", Id),
                new SqlParameter("@Item_Type_2", Item_Type),
                new SqlParameter("@Abbreviation_3", Abbreviation),
                new SqlParameter("@Item_icon_4", Item_icon),
                new SqlParameter("@userid_5", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Item_Type_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertcdserves(int service_id, string service_name, string description, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Service_id_1", service_id),
                new SqlParameter("@Service_Name_2", service_name),
                new SqlParameter("@Description_3", description),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_SERVICE_MASTER_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertcdprimarygroup(int levelid, int serviceid, string levelname, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@LEVEL1_ID_1", levelid),
                new SqlParameter("@SERVICE_ID_2", serviceid),
                new SqlParameter("@LEVEL_NAME_3", levelname),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_SERVICE_LEVEL1_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertcdservicesubgroup(int level2_id, int level1_id, string levelname, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@LEVEL2_ID_1", level2_id),
                new SqlParameter("@LEVEL1_ID_2", level1_id),
                new SqlParameter("@LEVEL_NAME_3", levelname),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_SERVICE_LEVEL2_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertcdmaster(int id, string cd_title, int level2_id, string description, DateTime add_date, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@ID_1", id),
                new SqlParameter("@Cd_Title_2", cd_title),
                new SqlParameter("@LEVEL2_ID_7", level2_id),
                new SqlParameter("@Description_8", description),
                new SqlParameter("@add_date_9", add_date),
                new SqlParameter("@userid_10", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_MASTER_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpsertDepartmentmMaster(int departmentcode, string departmentname, string shortname, int institutecode, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@DeptCode", departmentcode),
                new SqlParameter("@DeptName", departmentname),
                new SqlParameter("@Shortname", shortname),
                new SqlParameter("@InstCode", institutecode),
                new SqlParameter("@UserID", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[SP.AddUpdateDepartment]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertefiles(string DAid, string AccessionNo, DateTime AccessionDate, string Title, string Synopsis, string Type, int DocGroup, string Type_No, string BuildingId, string FloorID, string AlmiraID, string RackId, string Category, int classv, string Receipt_flg, string Rec_daid, int size_id, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@DAid_1", DAid),
                new SqlParameter("@AccessionNo_2", AccessionNo),
                new SqlParameter("@AccessionDate_3", AccessionDate),
                new SqlParameter("@Title_4", Title),
                new SqlParameter("@Synopsis_5", Synopsis),
                new SqlParameter("@Type_6", Type),
                new SqlParameter("@DocGroup_7", DocGroup),
                new SqlParameter("@Type_No", Type_No),
                new SqlParameter("@BuildingId", BuildingId),
                new SqlParameter("@FloorID", FloorID),
                new SqlParameter("@AlmiraID", AlmiraID),
                new SqlParameter("@RackId", RackId),
                new SqlParameter("@Category", Category),
                new SqlParameter("@class", classv),
                new SqlParameter("@Receipt_flg", Receipt_flg),
                new SqlParameter("@Rec_daid", Rec_daid),
                new SqlParameter("@size_id", size_id),
                new SqlParameter("@userid", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DigitalArchiveInfo_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertfilereceipt(string DAid, string Title, DateTime Rec_date, string Challan_No, string Rec_year, DateTime Return_dt, string Register, string status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@DAid", DAid),
                new SqlParameter("@Title", Title),
                new SqlParameter("@Rec_date", Rec_date),
                new SqlParameter("@Challan_No", Challan_No),
                new SqlParameter("@Rec_year", Rec_year),
                new SqlParameter("@Return_dt", Return_dt),
                new SqlParameter("@Register", Register),
                new SqlParameter("@status", status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Receipt_Info]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertmaintainacdsession(string academicsession, DateTime startdate, DateTime enddate)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@academicsession_1", academicsession),
                new SqlParameter("@startdate_2", startdate),
                new SqlParameter("@enddate_3", enddate)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AcedemicSessionInfo_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertmultilingualreportoptimizer(string id, string sentence, int inuse, string formtype)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@id_1", id),
                new SqlParameter("@sentence_2", sentence),
                new SqlParameter("@inuse_3", inuse),
                new SqlParameter("@formtype_4", formtype)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_MultilingualSentence_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertwindowservise(string Occure, DateTime StartDate, DateTime Enddate, DateTime FireDate, DateTime FireTime, string Day_weeks, string Months)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Occurs_1", Occure),
                new SqlParameter("@StartDate_2", StartDate),
                new SqlParameter("@EndDate_3", Enddate),
                new SqlParameter("@FireDate_4", FireDate),
                new SqlParameter("@FireTime_5", FireTime),
                new SqlParameter("@Day_weeks_6", Day_weeks),
                new SqlParameter("@Months_7", Months)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Window_Ser_2]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertuserdetails(int usertype, string userid, string password, string memberid, string SaltVc, string status1, string ValidUpTo, string IPAddress)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@usertype_1", usertype),
                new SqlParameter("@userid_2", userid),
                new SqlParameter("@password_3", password),
                new SqlParameter("@memberid_4", memberid),
                new SqlParameter("@SaltVc_5", SaltVc),
                new SqlParameter("@status1", status1),
                new SqlParameter("@ValidUpTo", ValidUpTo),
                new SqlParameter("@IPAddress", IPAddress)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_userdetails_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertStockData(string TotalStock, string LostStock, string IssueStock, string BindStock, string ILLStock, string writeOffstock, string Missingstock, string userid, string docid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@TotalStock_1", TotalStock),
                new SqlParameter("@LostStock_2", LostStock),
                new SqlParameter("@IssueStock_3", IssueStock),
                new SqlParameter("@BindStock_4", BindStock),
                new SqlParameter("@ILLStock_5", ILLStock),
                new SqlParameter("@writeOffstock_6", writeOffstock),
                new SqlParameter("@missingstock_7", Missingstock),
                new SqlParameter("@userid_8", userid),
                new SqlParameter("@docid_9", docid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ItemStatusMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertNewsPaperMaster(int T_id, string Title_N)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@T_id", T_id),
                new SqlParameter("@Title_N", Title_N)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaper_T]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertEbookmaster(int id, string Cd_Title, string Startup_file, int ctrl_no, string Description, DateTime add_date, string itemCategory, string userid, string accessionnumber, string Pay_Mode, string Url)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@id_1", id),
                new SqlParameter("@Cd_Title_2", Cd_Title),
                new SqlParameter("@Startup_file_5", Startup_file),
                new SqlParameter("@ctrl_no_6", ctrl_no),
                new SqlParameter("@Description_7", Description),
                new SqlParameter("@add_date_8", add_date),
                new SqlParameter("@itemCategory_9", itemCategory),
                new SqlParameter("@userid_10", userid),
                new SqlParameter("@accessionnumber_11", accessionnumber),
                new SqlParameter("@Pay_Mode_12", Pay_Mode),
                new SqlParameter("@id_1", Url)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBOOK_MASTER_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertbookaccessionmaster(string accessionnumber, string ordernumber, string indentnumber, string form, int accessionid, DateTime accessioneddate, string booktitle, int srno, string released, float bookprice, int srNoOld, string biilNo, DateTime billDate, string Item_type, int OriginalPrice, string OriginalCurrency, string userid, string vendor_source, int DeptCode, int DSrno, string DeptName)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[20]
            {
                new SqlParameter("@accessionnumber_1", accessionnumber),
                new SqlParameter("@ordernumber_2", ordernumber),
                new SqlParameter("@indentnumber_3", indentnumber),
                new SqlParameter("@form_4", form),
                new SqlParameter("@accessionid_5", accessionnumber),
                new SqlParameter("@accessioneddate_6", accessioneddate),
                new SqlParameter("@booktitle_7", booktitle),
                new SqlParameter("@srno_8", srno),
                new SqlParameter("@released_9", released),
                new SqlParameter("@bookprice_10", bookprice),
                new SqlParameter("@srNoOld_11", srNoOld),
                new SqlParameter("@biilNo_12", biilNo),
                new SqlParameter("@billDate_13", billDate),
                new SqlParameter("@Item_type_14", Item_type),
                new SqlParameter("@OriginalPrice_15", OriginalPrice),
                new SqlParameter("@OriginalCurrency_16", OriginalCurrency),
                new SqlParameter("@vendor_source_18", vendor_source),
                new SqlParameter("@DeptCode_19", DeptCode),
                new SqlParameter("@DSrno_20", DSrno),
                new SqlParameter("@DeptName_21", DeptName)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[bookaccessionmaster]   ", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpsertCategoryLoadingStatus(int ItemID, string ItemName, string Abbrev, int UserID, int FormID, int Type)
        {
            DataTable dataTable = new DataTable();
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@ItemID", ItemID),
                new SqlParameter("@ItemName", ItemName),
                new SqlParameter("@Abbrev", Abbrev),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateCatelogLoadingStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetCategoryLoading(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetCategoryLoading  ](@UserID , @FormID , @Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertCirclIssueBooks(string userid, string accno, DateTime issuedate, DateTime duedate, string status, string userid1, int IssueId)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@accno_2", accno),
                new SqlParameter("@issuedate_3", issuedate),
                new SqlParameter("@duedate_4", duedate),
                new SqlParameter("@status_5", status),
                new SqlParameter("@userid1_7", userid1),
                new SqlParameter("@IssueId_8", IssueId)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[CircIssueTransaction]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertsubject(int subject_id, string subject, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@subject_id_1", subject_id),
                new SqlParameter("@subject_2", subject),
                new SqlParameter("@userid_3", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_subject_master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertTranslation(int Language_Id, string Language_Name, string Font_Name, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Language_Id_1", Language_Id),
                new SqlParameter("@Language_Name_2", Language_Name),
                new SqlParameter("@Font_Name_3", Font_Name),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Translation_Language_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertStandardReply(int reply_id, string reply, string user_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@reply_id_1", reply_id),
                new SqlParameter("@reply_2", reply),
                new SqlParameter("@user_id_3", user_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_standard_reply_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetAllFingerPrints(int FormID, int UserID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetAllFingerPrints]()", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateUserStatus(string UserId, string Status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@Status", Status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateUserStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateUserOpacStatus(string UserId, string OpacStatus)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@Status", OpacStatus)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateUserOpacStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable DeleteRfidLog(int AntNum)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@AntNum", AntNum)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteRfidLog]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable DeleteCircUser(string UserId)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@UserId", UserId)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteCircUser]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetCircUserAddress(string UserID)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@UserID", UserID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetCircUserAddress](@UserID)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetLibrary(string UserID, int FormId, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLibrary](@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetSearchEDocs(int UserID, int FormId, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetSearchEDocs](@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetFeaturePer(int ID, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Id", ID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetFeaturePer](@Id,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable CheckDupMember(string firstname, string middlename, string lastname, string Fathername, string mothername, string userid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@firstname", firstname),
                new SqlParameter("@middlename", middlename),
                new SqlParameter("@lastname", lastname),
                new SqlParameter("@Fathername", Fathername),
                new SqlParameter("@mothername", mothername),
                new SqlParameter("@userid", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[CheckDupMember](@firstname,@middlename,@lastname,@Fathername,@mothername,@userid)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateMemberStatus(string UserId, string Status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@Status", Status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateMemberStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateMemberOpacStatus(string UserId, string OpacStatus)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@OpacStatus", OpacStatus)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateMemberOpacStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetClassmasterLoadingStatus(string ClassName, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ClassName", ClassName),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetClassmasterLoadingStatus](@ClassName,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteClassmasterLoadingStatus(string ClassName, string loadingstatus, string UserId, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@ClassName", ClassName),
                new SqlParameter("@loadingstatus", loadingstatus),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteClassmasterLoadingStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable DeleteTempClassmasterLoadingStatus(string ClassName, string loadingstatus, string UserId, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@ClassName", ClassName),
                new SqlParameter("@loadingstatus", loadingstatus),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteTempClassmasterLoadingStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetTempClassmasterLoadingStatus(string ClassName, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ClassName", ClassName),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetTempClassmasterLoadingStatus](@ClassName,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteCircClass(string ClassName, string UserId, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ClassName", ClassName),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteCircClass]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertstockentryform(string TotalStock, string LostStock, string IssueStock, string BindStock, string ILLStock, string writeOffstock, string Missingstock, string userid, string docid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@TotalStock_1", TotalStock),
                new SqlParameter("@LostStock_2", LostStock),
                new SqlParameter("@IssueStock_3", IssueStock),
                new SqlParameter("@BindStock_4", BindStock),
                new SqlParameter("@ILLStock_5", ILLStock),
                new SqlParameter("@writeOffstock_6", writeOffstock),
                new SqlParameter("@MissingStock_7", Missingstock),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_StockData_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertstockmanagement(string taskid, string taskname, DateTime startdate, DateTime enddate, string status, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@taskid_1", taskid),
                new SqlParameter("@taskname_2", taskname),
                new SqlParameter("@startdate_3", startdate),
                new SqlParameter("@enddate_4", enddate),
                new SqlParameter("@status_5", status),
                new SqlParameter("@userid_6", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_stock_mgmt_mst_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertstockmanagement(string TotalAcc, string LostAcc, string IssuedAcc, string BindAcc, string LibLAcc, string userid, string availtaskAcc, string WriteoffAcc)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@TotalAcc_2", TotalAcc),
                new SqlParameter("@LostAcc_3", LostAcc),
                new SqlParameter("@IssuedAcc_4", IssuedAcc),
                new SqlParameter("@BindAcc_5", BindAcc),
                new SqlParameter("@LibLAcc_6", LibLAcc),
                new SqlParameter("@userid_7", userid),
                new SqlParameter("@availtaskAcc_8", availtaskAcc),
                new SqlParameter("@WriteoffAcc_9", WriteoffAcc)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_TaskReport_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateExhangemaster(int CurrencyCode, string ShortName, string CurrencyName, decimal GocRate, decimal BankRate, DateTime EffectiveFrom, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@CurrencyCode_1", CurrencyCode),
                new SqlParameter("@ShortName_2", ShortName),
                new SqlParameter("@CurrencyName_3", CurrencyName),
                new SqlParameter("@GocRate_4", GocRate),
                new SqlParameter("@BankRate_5", BankRate),
                new SqlParameter("@EffectiveFrom_6", EffectiveFrom),
                new SqlParameter("@userid_7", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_exchangemaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertadmin(string SMSLink, string UserId, string Passwd)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@SMSLink", SMSLink),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@Passwd", Passwd)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SMSSettings]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertindent(string UserID, DateTime OperationDate, DateTime OperationTime, string IndentNo, DateTime IndentDate, decimal ExchangeRate, int NoofCopies, decimal price, decimal amount, string Opration, string AffectedObjects)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@UserID_1", UserID),
                new SqlParameter("@OperationDate_2", OperationDate),
                new SqlParameter("@OperationTime_3", OperationTime),
                new SqlParameter("@IndentNo_4", IndentNo),
                new SqlParameter("@IndentDate_5", IndentDate),
                new SqlParameter("@ExchangeRate_6", ExchangeRate),
                new SqlParameter("@NoofCopies_7", NoofCopies),
                new SqlParameter("@price_8", price),
                new SqlParameter("@amount_9", amount),
                new SqlParameter("@Operation_10", Opration),
                new SqlParameter("@AffectedObjects_11", AffectedObjects)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_IndentAudit_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertgiftindent(string ordernumber, string indentnumber, DateTime orderdate, string letternumber, DateTime letterdate, byte cancelorder, int itemnumber, decimal orderamount, string vendorid, int identityofordernumber, int order_check_code, int departmentcode)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@ordernumber_1", ordernumber),
                new SqlParameter("@indentnumber_2", indentnumber),
                new SqlParameter("@orderdate_3", orderdate),
                new SqlParameter("@letternumber_4", letternumber),
                new SqlParameter("@letterdate_5", letterdate),
                new SqlParameter("@cancelorder_6", cancelorder),
                new SqlParameter("@itemnumber_7", itemnumber),
                new SqlParameter("@orderamount_8", orderamount),
                new SqlParameter("@vendorid_9", vendorid),
                new SqlParameter("@identityofordernumber_10", identityofordernumber),
                new SqlParameter("@order_check_code_11", order_check_code),
                new SqlParameter("@departmentcode_12", departmentcode)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_giftordermaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertreindent(string indentnumber, DateTime indentdate, int mediatype, string requestercode, int departmentcode, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int currencycode, int go_bank, decimal exchangerate, int noofcopies, string approval, decimal price, decimal totalamount, string coursenumber, int noofstudents, string publisherid, string vendorid, DateTime recordingdate, string gifted, string indenttype, DateTime indenttime, string seriesname, int order_check_code, string yearofPublication, string isSatnding, string IndentId, string Vpart, string ItemNo, string subtitle, int Language_Id, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[46]
            {
                new SqlParameter("@indentnumber_1", indentnumber),
                new SqlParameter("@indentdate_2", indentdate),
                new SqlParameter("@mediatype_3", mediatype),
                new SqlParameter("@requestercode_4", requestercode),
                new SqlParameter("@departmentcode_5", departmentcode),
                new SqlParameter("@title_6", title),
                new SqlParameter("@authortype_7", authortype),
                new SqlParameter("@firstname1_8", firstname1),
                new SqlParameter("@middlename1_9", middlename2),
                new SqlParameter("@lastname1_10", lastname2),
                new SqlParameter("@firstname2_11", firstname2),
                new SqlParameter("@middlename2_12", middlename2),
                new SqlParameter("@lastname2_13", lastname2),
                new SqlParameter("@firstname3_14", firstname3),
                new SqlParameter("@middlename3_15", middlename3),
                new SqlParameter("@lastname3_16", lastname3),
                new SqlParameter("@edition_17", edition),
                new SqlParameter("@yearofedition_18", yearofedition),
                new SqlParameter("@volumeno_19", volumeno),
                new SqlParameter("@isbn_20", isbn),
                new SqlParameter("@category_21", category),
                new SqlParameter("@currencycode_22", currencycode),
                new SqlParameter("@go_bank_23", go_bank),
                new SqlParameter("@exchangerate_24", exchangerate),
                new SqlParameter("@noofcopies_25", noofcopies),
                new SqlParameter("@approval_26", approval),
                new SqlParameter("@price_27", price),
                new SqlParameter("@totalamount_28", totalamount),
                new SqlParameter("@coursenumber_29", coursenumber),
                new SqlParameter("@noofstudents_30", noofstudents),
                new SqlParameter("@publisherid_31", noofstudents),
                new SqlParameter("@vendorid_32", noofstudents),
                new SqlParameter("@recordingdate_33", noofstudents),
                new SqlParameter("@gifted_34", noofstudents),
                new SqlParameter("@indenttype_35", noofstudents),
                new SqlParameter("@indenttime_36", noofstudents),
                new SqlParameter("@seriesname_37", noofstudents),
                new SqlParameter("@order_check_code_38", noofstudents),
                new SqlParameter("@yearofPublication_39", noofstudents),
                new SqlParameter("@isSatnding_40", noofstudents),
                new SqlParameter("@IndentId_41", noofstudents),
                new SqlParameter("@Vpart_42", noofstudents),
                new SqlParameter("@ItemNo_43", noofstudents),
                new SqlParameter("@subtitle_44", noofstudents),
                new SqlParameter("@Language_Id_45", noofstudents),
                new SqlParameter("userid_46", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_indentmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbookreservation(string userid, int totalreservations)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@totalreservations_2", totalreservations)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circuserreservations_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertprintpagesize(int Id, string pageSizeName, decimal pageHeight, decimal pageWidth, int pRow, int pColumn)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@pageSizeName", pageSizeName),
                new SqlParameter("@pageHeight", pageHeight),
                new SqlParameter("@pageWidth", pageWidth),
                new SqlParameter("@pRow", pRow),
                new SqlParameter("@pColumn", pColumn)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_dynamic_PageSize]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertprintpagesize(int ctrlNo, int quenumber)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@ctrlNo_1", ctrlNo),
                new SqlParameter("@quenumber_2", quenumber)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circBookQue_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertdynamicidcard(int Id, string formatName, decimal height, decimal width, string roundedCorner, string variableBackSide, string frontDesign, string backDesign, string authorisedSignatory, string authorisedSignatory2)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@formatName", formatName),
                new SqlParameter("@height", height),
                new SqlParameter("@width", width),
                new SqlParameter("@roundedCorner", roundedCorner),
                new SqlParameter("@variableBackSide", variableBackSide),
                new SqlParameter("@frontDesign", frontDesign),
                new SqlParameter("@backDesign", backDesign),
                new SqlParameter("@authorisedSignatory", authorisedSignatory),
                new SqlParameter("@authorisedSignatory2", authorisedSignatory2)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_DynamicIDcard_Formats]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircleissuemaster(string userid, int currentissuedbooks, string userid1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@currentissuedbooks_2", currentissuedbooks),
                new SqlParameter("@userid1_3", userid1)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircIssueMaster_1]  ", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCatalogCardPrint(string AccessionNumber, string ClassNumber, string BookNumber, string accesspoints, string Contents, string CardTitle, string Volume, string Copy, int id, string Tag)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@AccessionNumber_1", AccessionNumber),
                new SqlParameter("@ClassNumber_2", ClassNumber),
                new SqlParameter("@BookNumber_3", BookNumber),
                new SqlParameter("@accesspoints_4", accesspoints),
                new SqlParameter("@Contents_5", Contents),
                new SqlParameter("@CardTitle_6", CardTitle),
                new SqlParameter("@Volume_7", Volume),
                new SqlParameter("@Copy_8", Copy),
                new SqlParameter("@id_9", id),
                new SqlParameter("@Tag_10", Tag)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircIssueMaster_1]  ", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertBuildingMaster(int BuildingId, string BuildingCode, string BuildingName, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@BuildingId_1", BuildingId),
                new SqlParameter("@BuildingCode_2", BuildingCode),
                new SqlParameter("@BuildingName_3", BuildingName),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BuildingMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertjournalinvoicemaster(string Invoice_id, string Invoice_No, DateTime Invoice_Date, string Order_Id, string Publisher_Code, string Currency, string Exchange_Rate, string PostageCharge, string Relation, string BillSerial_No, string Status, string Total_Amount, string ref_invoice_no, string Amount_Local, string pay_currency, string credit_amount, string credit_no, string Curr_code, string userid, string New_InvNo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[20]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@Invoice_No_2", Invoice_No),
                new SqlParameter("@Invoice_Date_3", Invoice_Date),
                new SqlParameter("@Order_Id_4", Order_Id),
                new SqlParameter("@Publisher_Code_5", Publisher_Code),
                new SqlParameter("@Currency_6", Currency),
                new SqlParameter("@Exchange_Rate_7", Exchange_Rate),
                new SqlParameter("@PostageCharge_8", PostageCharge),
                new SqlParameter("@Relation_9", Relation),
                new SqlParameter("@BillSerial_No_10", BillSerial_No),
                new SqlParameter("@Status_11", Status),
                new SqlParameter("@Total_Amount_12", Total_Amount),
                new SqlParameter("@ref_invoice_no_13", ref_invoice_no),
                new SqlParameter("@Amount_Local_14", Amount_Local),
                new SqlParameter("@pay_currency_15", pay_currency),
                new SqlParameter("@credit_amount_16", credit_amount),
                new SqlParameter("@credit_no_17", credit_no),
                new SqlParameter("@Curr_code_18", Curr_code),
                new SqlParameter("@userid_19", userid),
                new SqlParameter("@New_InvNo", New_InvNo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourInvoice_masterN_1] ", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertjournalPaymentMaster(int paymentid, int draftnumber, DateTime draftdate, string bankname, float amount, string paymenttype, int vendorid, DateTime paymentdate, float draft_amt, string documentno, float draftrate, string Relation, string userid, string swift_code, string account_nm, string IBAN_no, string bank_Address, string purpose, string Letter_Members)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@paymentid_1", paymentid),
                new SqlParameter("@draftnumber_2", draftnumber),
                new SqlParameter("@draftdate_3", draftdate),
                new SqlParameter("@bankname_4", bankname),
                new SqlParameter("@amount_5", amount),
                new SqlParameter("@paymenttype_6", paymenttype),
                new SqlParameter("@vendorid_7", vendorid),
                new SqlParameter("@paymentdate_8", paymentdate),
                new SqlParameter("@draft_amt_9", draft_amt),
                new SqlParameter("@documentno_10", documentno),
                new SqlParameter("@draftrate_11", draftrate),
                new SqlParameter("@Relation_12", Relation),
                new SqlParameter("@userid_13", userid),
                new SqlParameter("@swift_code", swift_code),
                new SqlParameter("@IBAN_no", IBAN_no),
                new SqlParameter("@bank_Address", bank_Address),
                new SqlParameter("@purpose", purpose),
                new SqlParameter("@Letter_Members", Letter_Members)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_paymentmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertjournalcreditnotepayment(int paymentid, int draftnumber, DateTime draftdate, string bankname, float amount, string paymenttype, int vendorid, DateTime paymentdate, float draft_amt, string documentno, float draftrate, string Relation, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[13]
            {
                new SqlParameter("@paymentid_1", paymentid),
                new SqlParameter("@draftnumber_2", draftnumber),
                new SqlParameter("@draftdate_3", draftdate),
                new SqlParameter("@bankname_4", bankname),
                new SqlParameter("@amount_5", amount),
                new SqlParameter("@paymenttype_6", paymenttype),
                new SqlParameter("@vendorid_7", vendorid),
                new SqlParameter("@paymentdate_8", paymentdate),
                new SqlParameter("@draft_amt_9", draft_amt),
                new SqlParameter("@documentno_10", documentno),
                new SqlParameter("@draftrate_11", draftrate),
                new SqlParameter("@Relation_12", Relation),
                new SqlParameter("@userid_13", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_paymentmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertJournalMaster(string Member_Id, string Journal_No, string Volume, string Issue, string Part, float Fineamount, string Finecause, string isPaid, DateTime ReceiveDate, DateTime Publicationdate, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Member_Id_1", Member_Id),
                new SqlParameter("@Journal_No_2", Journal_No),
                new SqlParameter("@Issue_4", Volume),
                new SqlParameter("@Part_5", Issue),
                new SqlParameter("@Fineamount_6", Fineamount),
                new SqlParameter("@Finecause_7", Finecause),
                new SqlParameter("@isPaid_8", isPaid),
                new SqlParameter("@ReceiveDate_9", ReceiveDate),
                new SqlParameter("@publicationdate_10", Publicationdate),
                new SqlParameter("@userid_11", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalReceive_Arrival_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertauditmaster(string UserID, string Process, DateTime OperationDate, DateTime OperationTime, string Operation, string Expenditure, float CommitMentApp, float CommitMentNApp, float IndentsApp, float IndentNapp, string DocumentNo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@UserID_1", UserID),
                new SqlParameter("@Process_2", Process),
                new SqlParameter("@OperationDate_3", OperationDate),
                new SqlParameter("@OperationTime_4", OperationTime),
                new SqlParameter("@Operation_5", Operation),
                new SqlParameter("@Expenditure_6", Expenditure),
                new SqlParameter("@CommitMentApp_7", CommitMentApp),
                new SqlParameter("@CommitMentNApp_8", CommitMentNApp),
                new SqlParameter("@IndentsApp_9", IndentNapp),
                new SqlParameter("@IndentNapp_10", IndentNapp),
                new SqlParameter("@DocumentNo_11", DocumentNo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAudit_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertNewpaperinvoiceMaster(string Invoice_id, string Invoice_No, DateTime Invoice_Date, string vendorid, float totalamount, string status, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@Invoice_No_2", Invoice_No),
                new SqlParameter("@Invoice_Date_3", Invoice_Date),
                new SqlParameter("@vendorid_4", vendorid),
                new SqlParameter("@totalamount_5", totalamount),
                new SqlParameter("@status_6", status),
                new SqlParameter("@userid_7", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperInvoice]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertunioninsertsettings(string LibraryName, string DatabaseName, string ServerName, string ConnectionStringName, string ConnectionString, string Status, int PID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@LibraryName", LibraryName),
                new SqlParameter("@DatabaseName", DatabaseName),
                new SqlParameter("@ServerName", ServerName),
                new SqlParameter("@ConnectionStringName", ConnectionStringName),
                new SqlParameter("@ConnectionString", ConnectionString),
                new SqlParameter("@Status", Status),
                new SqlParameter("@PID", PID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_UnionLibSettings]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinserttempclsloadstatus(string classname, int totalissueddays, int noofbookstobeissued, float finperday, int reservedays, int totalissueddays_jour, int noofjournaltobeissued, float fineperday_jour, int reservedays_jour, string Status, int ValueLimit, int days_1phase, float amt_1phase, int days_2phase, float amt_2phase, int days_1phasej, float amt_1phasej, int days_2phasej, float amt_2phasej, string shortname, string loadingstatus)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[21]
            {
                new SqlParameter("@classname_1", classname),
                new SqlParameter("@totalissueddays_2", totalissueddays),
                new SqlParameter("@noofbookstobeissued_3", noofbookstobeissued),
                new SqlParameter("@reservedays_5", reservedays),
                new SqlParameter("@totalissueddays_jour_6", totalissueddays_jour),
                new SqlParameter("@noofjournaltobeissued_7", noofjournaltobeissued),
                new SqlParameter("@fineperday_jour_8", fineperday_jour),
                new SqlParameter("@reservedays_jour_9", reservedays_jour),
                new SqlParameter("@Status_10", Status),
                new SqlParameter("@ValueLimit_11", ValueLimit),
                new SqlParameter("@days_1phase_12", days_1phase),
                new SqlParameter("@amt_1phase_13", amt_1phase),
                new SqlParameter("@days_2phase_14", days_2phase),
                new SqlParameter("@amt_2phase_15", amt_2phase),
                new SqlParameter("@days_1phasej_16", days_2phasej),
                new SqlParameter("@amt_1phasej_17", amt_1phasej),
                new SqlParameter("@days_2phasej_18", days_1phasej),
                new SqlParameter("@amt_2phasej_19", amt_2phasej),
                new SqlParameter("@shortname_20", shortname),
                null,
                new SqlParameter("@loadingstatus_21", loadingstatus)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Tempclsmstloadstatus_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetIdTable(string ObjectName, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ObjectName", ObjectName),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIdTable](@ObjectName,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetVendor(string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetVendor](@ObjectName,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Getbinderinfo(string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Getbinderinfomation](@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBindType1(int UserID, int FormId, int Type, int binderid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type),
                new SqlParameter("@binderid", binderid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetBindType1](@UserID,@FormId,@Type,@binderid)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetPublisherAddress(string Firstname, string Percity, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Firstname", Firstname),
                new SqlParameter("@Percity", Percity),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetPublisherAddress](@Firstname,@Percity,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetVendorAddress(string Vendorname, string Percity, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Vendorname", Vendorname),
                new SqlParameter("@Percity", Percity),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetVendorAddress](@Vendorname,@Percity,@UserID,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetMaxVendorid()
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[0];
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetMaxVendorid](   )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetMaxPublisherid()
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[0];
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetMaxPublisherid](   )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetPublisherAddressDetail(int Publisherid, string Publishercode, string Firstname, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Publisherid", Publisherid),
                new SqlParameter("@Publishercode", Publishercode),
                new SqlParameter("@Firstname", Firstname),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetPublisherAddressDetail](@Publisherid,@Publishercode,@Firstname  ,@UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeletePublisher(int Publisherid, string Publishercode, string UserID, string FormId, string Type)
        {
            DataTable result = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Publisherid", Publisherid),
                new SqlParameter("@Publishercode", Publishercode),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[DeletePublisher]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return result;
        }

        public DataTable GetVendorAddressDetail(int VendorId, string VendorCode, string VendorName, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@VendorId", VendorId),
                new SqlParameter("@VendorCode", VendorCode),
                new SqlParameter("@VendorName", VendorName),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetVendorAddressDetail](@VendorId,@VendorCode,@VendorName  ,@UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetEdocSuggesestion(int UserID, int FormID, int Type, string accno, string grpcat, string servicename, string grdservice, int serviceid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@checkaccok", accno),
                new SqlParameter("@groupcategory", grpcat),
                new SqlParameter("@servicename", servicename),
                new SqlParameter("@grdservice", grdservice),
                new SqlParameter("@serviceid", serviceid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM [MTR].[GETEDOCUMENT_GROUP](@UserID,@FormId,@Type, @checkaccok,@groupcategory,@servicename,@grdservice,@serviceid)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertUserTypeLevel3(int UserTypeId, int SubMenu_Id, string Permission)
        {
            DataTable dataTable = new DataTable();
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserTypeId_1", UserTypeId),
                new SqlParameter("@SubMenu_Id_2", SubMenu_Id),
                new SqlParameter("@Permission_3", Permission)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypeLavel3_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertImageGallerySubEvent(int Id, int PEventId, DateTime SubEventDateFrom, DateTime SubEventDateTo, string SubEventNote, string SubEventName, DateTime PhotographyDate, string PhotographerName, string Places)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@PEventId", PEventId),
                new SqlParameter("@SubEventDateFrom", SubEventDateFrom),
                new SqlParameter("@SubEventDateTo", SubEventDateTo),
                new SqlParameter("@SubEventNote", SubEventNote),
                new SqlParameter("@SubEventName", SubEventName),
                new SqlParameter("@PhotographyDate", PhotographyDate),
                new SqlParameter("@PhotographerName", PhotographerName),
                new SqlParameter("@Places", Places)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ImageGallery_SubEvents]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertUserTypeLevel2(int UserTypeId, int MidMenu_Id, string Permission)
        {
            DataTable dataTable = new DataTable();
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserTypeId_1", UserTypeId),
                new SqlParameter("@SubMenu_Id_2", MidMenu_Id),
                new SqlParameter("@Permission_3", Permission)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypeLavel2_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircWriteoffEntry(string DocumentNo, DateTime writeoffdate, string accessionnumber, string Cause, string Note, string Status, decimal price, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@DocumentNo_1", DocumentNo),
                new SqlParameter("@writeoffdate_2", writeoffdate),
                new SqlParameter("@accessionnumber_3", accessionnumber),
                new SqlParameter("@Cause_4", Cause),
                new SqlParameter("@Note_5", Note),
                new SqlParameter("@Status_7", Status),
                new SqlParameter("@price_71", price),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circWriteoffentry_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertLostBookRecovery(string accessionnumber, DateTime recoverydate, string Paymode, int amount, string RecoveryMode, string Notes, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@DocumentNo_1", accessionnumber),
                new SqlParameter("@writeoffdate_2", recoverydate),
                new SqlParameter("@accessionnumber_3", Paymode),
                new SqlParameter("@Cause_4", amount),
                new SqlParameter("@Note_5", RecoveryMode),
                new SqlParameter("@Status_7", Notes),
                new SqlParameter("@price_71", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circLostBookRecovery_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertImageGalleryEvent(int Id, string EventName, string userid)
        {
            DataTable dataTable = new DataTable();
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserTypeId_1", Id),
                new SqlParameter("@SubMenu_Id_2", EventName),
                new SqlParameter("@Permission_3", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ImageGallery_Events]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircReceiveTransaction(string userid, string accno, DateTime receivingdate, decimal fineamount, string fineCause, string isPaid, DateTime Dueon, DateTime paidOn, string amtexp, string userid1, int issue_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@accno_2", accno),
                new SqlParameter("@receivingdate_3", receivingdate),
                new SqlParameter("@fineamount_4", fineamount),
                new SqlParameter("@fineCause_5", fineCause),
                new SqlParameter("@isPaid_6", isPaid),
                new SqlParameter("@Dueon_7", Dueon),
                new SqlParameter("@paidOn_8", paidOn),
                new SqlParameter("@amtexp_9", amtexp),
                new SqlParameter("@userid1_10", userid1),
                new SqlParameter("@issue_id", issue_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circRecTransNDB_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertLostBookEntry(string accessionnumber, string userid, int Bookprice, DateTime PayDate, string payStatus, string receiptno, string entryCategory, string NewAccno, DateTime EntryDate, string Recovered, string userid1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@accessionnumber_1", accessionnumber),
                new SqlParameter("@userid_2", userid),
                new SqlParameter("@Bookprice_3", Bookprice),
                new SqlParameter("@PayDate_4", PayDate),
                new SqlParameter("@payStatus_5", payStatus),
                new SqlParameter("@receiptno_6", receiptno),
                new SqlParameter("@entryCategory_7", entryCategory),
                new SqlParameter("@NewAccno_8", NewAccno),
                new SqlParameter("@EntryDate_9", EntryDate),
                new SqlParameter("@Recovered_10", Recovered),
                new SqlParameter("@userid1_11", userid1)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circLostBookentry_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertaccessionindent(string indentnumber, int accessionedcopies)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@indentnumber_1", indentnumber),
                new SqlParameter("@accessionedcopies_2", accessionedcopies)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AccessionedIndent_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinserttempaccesspoints(int id, string Title, string Tag)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@id_2", id),
                new SqlParameter("@Title_1", Title),
                new SqlParameter("@Tag_3", Tag)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_tempAccespoints_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateccessionmaster(int ctrl_no, int Copy_no, int year_edition, string accessionnumber, string pubYear, float bookprice, int specialprice)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[7];
            array[0] = new SqlParameter("@ctrl_no", ctrl_no);
            array[1] = new SqlParameter("@Copy_no", Copy_no);
            array[2] = new SqlParameter("@year_edition", year_edition);
            array[4] = new SqlParameter("@accessionnumber", accessionnumber);
            array[5] = new SqlParameter("@pubYear", pubYear);
            array[6] = new SqlParameter("@bookprice", bookprice);
            array[7] = new SqlParameter("specialprice", specialprice);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_new_accessionmaster]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertfloormaster(int FloorId, string FloorCode, string FloorName, string userid, string building_code)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@FloorId_1", FloorId),
                new SqlParameter("@FloorCode_2", FloorCode),
                new SqlParameter("@FloorName_3", FloorName),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@building_code", building_code)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_FloorMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertfeatureper(string Features, int FID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@Features", Features),
                new SqlParameter("@FID", FID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_FeaturePer]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertrackmaster(int RackId, string RackCode, string RackName, string userid, string almira_code)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@RackId_1", RackId),
                new SqlParameter("@RackCode_2", RackCode),
                new SqlParameter("@RackName_3", RackName),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@almira_code", almira_code)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RackMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertlibraryservices(int Guest_id, string name, string E_mail, string phone, string Address, string Affiliation)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Guest_id_1", Guest_id),
                new SqlParameter("@name_2", name),
                new SqlParameter("@E_mail_3", E_mail),
                new SqlParameter("@phone_4", phone),
                new SqlParameter("@Address_5", Address),
                new SqlParameter("@Affiliation_6", Affiliation)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Lib_serv_GuestInfo]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertdispatchinvoice(int PaymentID, string InvoiceID, decimal InvoiceAmount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@PaymentID_1", PaymentID),
                new SqlParameter("@InvoiceID_2", InvoiceID),
                new SqlParameter("@InvoiceAmount_3", InvoiceAmount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_jou_letteraccPmtchild_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertadmin(string BoardName, int BoardId, string DefaultCount, int usertypeid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserTypeId", usertypeid),
                new SqlParameter("@BoardName", BoardName),
                new SqlParameter("@BoardId", BoardId),
                new SqlParameter("@DefaultCount", DefaultCount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_dashBoardSettings]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable DeleteEdocument(int serviceid, int UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Service_id", serviceid),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteEDocument]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertcataloguedetail(int Id, string Title, string Tag)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@Title_1", Id),
                new SqlParameter("@id_2", Title),
                new SqlParameter("@Tag_3", Tag)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_tempAccespoints_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertsecuritymnu(string Message, DateTime SendDate, string Sendby, string SendTo, string Status, string PageName, DateTime ScheduleDate, string ScheduleGroup)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Message", Message),
                new SqlParameter("@SendDate", SendDate),
                new SqlParameter("@SendBy", Sendby),
                new SqlParameter("@SendTo", SendTo),
                new SqlParameter("@Status", Status),
                new SqlParameter("@PageName", PageName),
                new SqlParameter("@ScheduleDate", ScheduleDate),
                new SqlParameter("@ScheduleGroup", ScheduleGroup)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ScheduleSMS]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertalmiramaster(int AlmiraId, string AlmiraCode, string AlmiraName, string userid, string floor_code)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@AlmiraId_1", AlmiraId),
                new SqlParameter(" @AlmiraCode_2", AlmiraCode),
                new SqlParameter("@AlmiraName_3", AlmiraName),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@floor_code", floor_code)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AlmiraMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertebookprimarygroup(int LEVEL1_ID, int SERVICE_ID, string LEVEL_NAME)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@LEVEL1_ID_1", LEVEL1_ID),
                new SqlParameter(" @SERVICE_ID_2", SERVICE_ID),
                new SqlParameter("@LEVEL_NAME_3", LEVEL_NAME)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBook_SERVICE_LEVEL1_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertlocationmasterredefined(int Id, string Location_Name, string Location_Path, string Inst_Id, string Location)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter(" @Location_Name", Location_Name),
                new SqlParameter("@Location_Path", Location_Path),
                new SqlParameter(" @Inst_Id", Inst_Id),
                new SqlParameter("@Location", Location)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Mapped_Location]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertebookserves(int Service_id, string Service_Name, string Description)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@Service_id_1", Service_id),
                new SqlParameter(" @Service_Name_2", Service_Name),
                new SqlParameter("@Description_3", Description)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBook_SERVICE_MASTER_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertebookreservation(int ctrlno, int reservations)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@ctrlno_1", ctrlno),
                new SqlParameter("@reservations_2", reservations)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circBookreservations_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertcomprativechart(string Order_No, string JournalPackage_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@Order_No_1", Order_No),
                new SqlParameter("@JournalPackage_No_2", JournalPackage_No)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OrderChild_Journal_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertthesisaddkeyword(int id, string keywords, string keyword_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@id_1", id),
                new SqlParameter("@keywords_2", keywords),
                new SqlParameter("@keyword_id_3", keyword_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_thesis_keyword_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertmemberdueslistgeneration(string listno, string memberid, string membername, string departmentname, decimal balance, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@listno_1", listno),
                new SqlParameter("@memberid_2", memberid),
                new SqlParameter("@membername_3", membername),
                new SqlParameter("@departmentname_4", departmentname),
                new SqlParameter("@balance_5", balance),
                new SqlParameter("@userid_6", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Duelistchildmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertnewspaperpayment(string Invoice_id, int ActualNoofcopies, decimal ActualPricePerCopy, decimal ArrivalDate, string Newspaper_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@ActualNoofcopies_2", ActualNoofcopies),
                new SqlParameter("@ActualPricePerCopy_3", ActualPricePerCopy),
                new SqlParameter("@ArrivalDate_4", ArrivalDate),
                new SqlParameter("@Newspaper_id_5", Newspaper_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperInvoice_Child]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertinvoicenew(string Invoice_id, string Journal_Id, decimal Discount, decimal Amount, decimal Balance, decimal Rs_E, string Jour_Status, decimal print_Amt, decimal online_Amt, decimal p_o_Amt)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@Journal_Id_2", Journal_Id),
                new SqlParameter("@Discount_3", Discount),
                new SqlParameter("@Amount_4", Amount),
                new SqlParameter("@Balance_5", Balance),
                new SqlParameter("@Rs_E_6", Rs_E),
                new SqlParameter("@Jour_Status_7", Jour_Status),
                new SqlParameter("@print_Amt_8", print_Amt),
                new SqlParameter("@online_Amt_9", online_Amt),
                new SqlParameter("@p_o_Amt_10", p_o_Amt)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourInvoice_ChildN_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertregisfingerprint(string userid, byte fingerprint)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@fingerprint_2", fingerprint)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RegisFingerPrint_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertpackagejourprice(string package_id, string journal_id, decimal amount, decimal con_amt, decimal pro_amt, decimal Print_Amt, decimal online_Amt, decimal p_o_Amt, decimal Pro_Credit_amt)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@package_id_1", package_id),
                new SqlParameter("@journal_id_2", journal_id),
                new SqlParameter("@amount_3", amount),
                new SqlParameter("@con_amt_4", con_amt),
                new SqlParameter("@pro_amt_5", pro_amt),
                new SqlParameter("@Print_Amt_6", Print_Amt),
                new SqlParameter("@online_Amt_7", online_Amt),
                new SqlParameter("@p_o_Amt_8", p_o_Amt),
                new SqlParameter("@Pro_Credit_amt_9", Pro_Credit_amt)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_package_jour_price_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertpackagejourpricesup(string package_id, string journal_id, decimal amount, decimal con_amt, decimal pro_amt)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@package_id_1", package_id),
                new SqlParameter("@journal_id_2", journal_id),
                new SqlParameter("@amount_3", amount),
                new SqlParameter("@con_amt_4", con_amt),
                new SqlParameter("@pro_amt_5", pro_amt)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_package_jour_price_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertscheduleemail(string Subject, string Body, DateTime mailGenerateDT, string MailTo, string Status, string EmailPage, DateTime ScheduleDate, string ScheduleGroup)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Subject", Subject),
                new SqlParameter("@Body", Body),
                new SqlParameter("@mailGenerateDT", mailGenerateDT),
                new SqlParameter("@MailTo", MailTo),
                new SqlParameter("@Status", Status),
                new SqlParameter("@EmailPage", EmailPage),
                new SqlParameter("@ScheduleDate", ScheduleDate),
                new SqlParameter("@ScheduleGroup", ScheduleGroup)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ScheduleEmail]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertinstitutemaster(int InstituteCode, string InstituteName, string shortname, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@InstituteCode_1", InstituteCode),
                new SqlParameter("@InstituteName_2", InstituteName),
                new SqlParameter("@shortname_3", shortname),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_InstituteMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertcdkeywords(int ID, string Keyword_name, string key_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@ID_1", ID),
                new SqlParameter("@Keyword_name_2", Keyword_name),
                new SqlParameter("@key_id_3", key_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_Keywords_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertprogrammaster(int program_id, string program_name, string short_name, int deptcode, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@program_id_1", program_id),
                new SqlParameter("@program_name_2", program_name),
                new SqlParameter("@short_name_3", short_name),
                new SqlParameter("@deptcode_4", deptcode),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Program_Master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertroommaster(int RoomId, string RoomNo, string RoomName, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@RoomId_1", RoomId),
                new SqlParameter("@RoomNo_2", RoomNo),
                new SqlParameter("@RoomName_3", RoomName),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RoomMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbindtypemaster(string bindtypename, decimal price, DateTime c_date, string userid, int Binder_id, int hd_type1, DateTime hd_date1, string hd_name1, decimal hd_price1, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@bindtypename_1", bindtypename),
                new SqlParameter("@price_2", price),
                new SqlParameter("@c_date_3", c_date),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@Binder_id", Binder_id),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@hd_type", hd_type1),
                new SqlParameter("@hd_date", hd_date1),
                new SqlParameter("@hd_name", hd_name1),
                new SqlParameter("@hd_price", hd_price1)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bindtypemaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertfinecalculation(string userid, decimal fine)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@fine_2", fine)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Fine_calculation_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjourwriteoffentry(string DocumentNo, DateTime writeoffdate, string accessionnumber, string Cause, string Note, string Status, string userid, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@DocumentNo_1", DocumentNo),
                new SqlParameter("@writeoffdate_2", writeoffdate),
                new SqlParameter("@accessionnumber_3", accessionnumber),
                new SqlParameter("@Cause_4", Cause),
                new SqlParameter("@Note_5", Note),
                new SqlParameter("@Status_7", Status),
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Jour_Writeoffentry_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertEBookService(int LEVEL2_ID, int LEVEL1_ID, string LEVEL_NAME)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@LEVEL2_ID_1", LEVEL2_ID),
                new SqlParameter("@LEVEL1_ID_2", LEVEL1_ID),
                new SqlParameter("@LEVEL_NAME_3", LEVEL_NAME)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBook_SERVICE_LEVEL2_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertUpdateCatalog(string firstname, string percity, string perstate, string percountry, string peraddress, string publishercode)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[6];
            array[1] = new SqlParameter("@firstname", firstname);
            array[0] = new SqlParameter("@percity", percity);
            array[2] = new SqlParameter("@perstate", perstate);
            array[3] = new SqlParameter("@percountry", percountry);
            array[4] = new SqlParameter("@peraddress", peraddress);
            array[5] = new SqlParameter("@publishercode", publishercode);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[update_catalog]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertLocationObjectItem(int Id, int LocationObjectId, string LocationObjectItem, string Inst_Id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@LocationObjectId", LocationObjectId),
                new SqlParameter("@LocationObjectItem", LocationObjectItem),
                new SqlParameter("@Inst_Id", Inst_Id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_LocationObject_Items]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertMergeSplitReference(string Journal_No, string Journal_Id, string Reference, string Operation_Mode, string Journaltitle, string JournalReference, DateTime Effective_From)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[7];
            array[1] = new SqlParameter("@Journal_No_1", Journal_No);
            array[0] = new SqlParameter("@Journal_Id_2", Journal_Id);
            array[2] = new SqlParameter("@Reference_3", Reference);
            array[3] = new SqlParameter("@Operation_Mode_4", Operation_Mode);
            array[4] = new SqlParameter(" @Journaltitle_5", Journaltitle);
            array[5] = new SqlParameter("@JournalReference_6", JournalReference);
            array[6] = new SqlParameter("@Effective_From_7", Effective_From);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_Merge_SplitReference_1]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateCircBookStatus(string accno)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@accno_1", accno)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_CircBookStatus_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteDACINfo(string daid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@DAId_1", daid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteDAFInfo]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertOrderMasterAudit(string ordernumber, DateTime exparivaldateapproval, DateTime exparivaldatenonapproval, string indentnumber, DateTime orderdate, string letternumber, DateTime letterdate, int cancelorder, int itemnumber, int departmentcode, decimal orderamount, int vendorid, int identityofordernumber, int order_check_code, string UserId, DateTime OperationDate, DateTime OperationTime, string Operation, string AffectedObjects, string IpAddress)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[20]
            {
                new SqlParameter("@ordernumber_1", ordernumber),
                new SqlParameter(" @exparivaldateapproval_2", exparivaldateapproval),
                new SqlParameter(" @exparivaldatenonapproval_3", exparivaldatenonapproval),
                new SqlParameter("@indentnumber_4", indentnumber),
                new SqlParameter("@orderdate_5", orderdate),
                new SqlParameter("@letternumber_6", letternumber),
                new SqlParameter("@letterdate_7", letterdate),
                new SqlParameter("@cancelorder_8", cancelorder),
                new SqlParameter("@itemnumber_9", itemnumber),
                new SqlParameter("@departmentcode_10", departmentcode),
                new SqlParameter("@orderamount_11", orderamount),
                new SqlParameter("@vendorid_12", vendorid),
                new SqlParameter(" @identityofordernumber_13", identityofordernumber),
                new SqlParameter("@order_check_code_14", order_check_code),
                new SqlParameter("@UserId_15", UserId),
                new SqlParameter("@OperationDate_16", OperationDate),
                new SqlParameter("@OperationTime_17", OperationTime),
                new SqlParameter("@Operation_18", Operation),
                new SqlParameter("@AffectedObjects_19", AffectedObjects),
                new SqlParameter("@IpAddress_20", IpAddress)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_ordermasterAudit_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertAddressTable(string addid, string localaddress, string localcity, string localstate, string localpincode, string localcountry, string peraddress, string percity, string perstate, string percountry, string perpincode, string addrelation)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@addid_1", addid),
                new SqlParameter("@localaddress_2", localaddress),
                new SqlParameter("@localcity_3", localcity),
                new SqlParameter("@localstate_4", localstate),
                new SqlParameter("@localpincode_5", localpincode),
                new SqlParameter("@localcountry_6", localcountry),
                new SqlParameter("@peraddress_7", peraddress),
                new SqlParameter("@percity_8", percity),
                new SqlParameter("@perstate_9", perstate),
                new SqlParameter("@percountry_10", percountry),
                new SqlParameter("@perpincode_11", perpincode),
                new SqlParameter("@addrelation_12", addrelation)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AddressTable_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertLoginMaster(string LoginName, DateTime LoginDate, string LoginTime, string TableName, string UserAction, string id, string sessionyr, decimal financialValue, string IpAddress)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@LoginName_1", LoginName),
                new SqlParameter("@LoginDate_2", LoginDate),
                new SqlParameter("@LoginTime_3", LoginTime),
                new SqlParameter("@TableName_4", TableName),
                new SqlParameter("@UserAction_5", UserAction),
                new SqlParameter("@id_6", id),
                new SqlParameter("@sessionyr_7", sessionyr),
                new SqlParameter(" @financialValue_8", financialValue),
                new SqlParameter("@IpAddress_9", IpAddress)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_LoginMaster_2]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertLocationobject(int Id, string LocationObject, string Abbreviation, string Inst_Id, string OrderNo)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@LocationObject", LocationObject),
                new SqlParameter("@Abbreviation", Abbreviation),
                new SqlParameter("@Inst_Id", Inst_Id),
                new SqlParameter("@OrderNo", OrderNo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Insert_LocationObject]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertActionPerm(int UserTypeId, int actionId, string Permission, int submenu_id, int child)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@UserTypeId_1", UserTypeId),
                new SqlParameter("@actionId_2", actionId),
                new SqlParameter("@Permission_3", Permission),
                new SqlParameter("@submenu_id_4", submenu_id),
                new SqlParameter("@child_5", child)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_ActionLPerm_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertEAttachmentShare(int Id, string daid, string sfileName, string Passwd, string Saltvc, int shareBy_UserId, int shareWith_UserId, string permission, string daysLimit, DateTime fromDate, DateTime toDate)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@Daid", daid),
                new SqlParameter("@sFileName", sfileName),
                new SqlParameter("@Passwd", Passwd),
                new SqlParameter("@Saltvc", Saltvc),
                new SqlParameter("@shareBy_UserId", shareBy_UserId),
                new SqlParameter("@shareWith_UserId", shareWith_UserId),
                new SqlParameter("@permission", permission),
                new SqlParameter("@daysLimit", daysLimit),
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_eAttachment_Share]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertItemKeyWord(int Ctrl_No, string Keyword, string ItemType, int S_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Ctrl_No_1", Ctrl_No),
                new SqlParameter("@Keyword_2", Keyword),
                new SqlParameter("@ItemType_3", ItemType),
                new SqlParameter("@S_No_4", S_No)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ItemsKeyword_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertImageGalleryPhotos(int SubEventId, byte Photos, string TagMembers)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@SubEventId", SubEventId),
                new SqlParameter("@Photos", Photos),
                new SqlParameter("@TagMembers", TagMembers)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Insert_ImageGallery_Photos]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertMaterialAccompany(string accession_no, string media_accessionno, string media_type, string description, string fileurl, string userid)
        {
            DataTable dataTable = new DataTable();
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@accession_no_1", accession_no),
                new SqlParameter("@media_accessionno_2", media_accessionno),
                new SqlParameter("@media_type_3", media_type),
                new SqlParameter("@description_4", description),
                new SqlParameter("@fileurl_5", fileurl),
                new SqlParameter("@userid_6", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_material_accompany_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertJournalrequest(int Jrnl_Request_No, DateTime Jrnl_Date, int Department_Id, string Requester_code, string Jrnl_Title, string Media_Type, int Priority, string List_No, string Requester_Name, DateTime Request_Year, DateTime Request_year1, string userid, int NOC, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[15]
            {
                new SqlParameter("@Jrnl_Request_No_1", Jrnl_Request_No),
                new SqlParameter("@Jrnl_Date_2", Jrnl_Date),
                new SqlParameter("@Department_Id_3", Department_Id),
                new SqlParameter("@Requester_code_4", Requester_code),
                new SqlParameter("@Jrnl_Title_5", Jrnl_Title),
                new SqlParameter("@Media_Type_6", Media_Type),
                new SqlParameter("@Priority_7", Priority),
                new SqlParameter("@List_No_8", List_No),
                new SqlParameter("@Requester_Name_9", Requester_Name),
                new SqlParameter("@Request_Year_10", Request_Year),
                new SqlParameter("@Request_Year1_11", Request_year1),
                new SqlParameter("@userid_12", userid),
                new SqlParameter("@NOC", NOC),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Journal_Request_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertILLmaster(int ILLid, string authorisedperson, string ILLname, string address, string city, string state, string email, string phoneno, int maxdays, float odcharge, string userid, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@ILLid_1", ILLid),
                new SqlParameter("@authorisedperson_2", authorisedperson),
                new SqlParameter("@ILLname_3", ILLname),
                new SqlParameter("@address_4", address),
                new SqlParameter("@city_5", city),
                new SqlParameter("@state_6", state),
                new SqlParameter("@email_7", email),
                new SqlParameter("@phoneno_8", phoneno),
                new SqlParameter("@maxdays_9", maxdays),
                new SqlParameter("@odcharge_10", odcharge),
                new SqlParameter("@userid_11", userid),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ILLMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertpackagesubscription(string Package_Id, string Package_Code, string Subscription_No, string Title_Abbre, string Package_Title, string Agent, DateTime Start_Date, DateTime Expiry_Date, string Publisher, DateTime entry_Date, string Subscription_Status, string Process_Stage, string Journal_Status, DateTime SubscriptionStatus_Date, string Reason, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@Package_Id_1", Package_Id),
                new SqlParameter("@Package_Code_2", Package_Code),
                new SqlParameter("@Subscription_No_3", Subscription_No),
                new SqlParameter("@Title_Abbre_4", Title_Abbre),
                new SqlParameter("@Package_Title_5", Package_Title),
                new SqlParameter("@Agent_6", Agent),
                new SqlParameter("@Start_Date_7", Start_Date),
                new SqlParameter("@Expiry_Date_8", Expiry_Date),
                new SqlParameter("@Publisher_9", Publisher),
                new SqlParameter("@Entry_Date_10", entry_Date),
                new SqlParameter("@Subscription_Status_11", Subscription_Status),
                new SqlParameter("@Process_Stage_12", Process_Stage),
                new SqlParameter("@Journal_Status_13", Journal_Status),
                new SqlParameter("@SubscriptionStatus_Date_14", SubscriptionStatus_Date),
                new SqlParameter("@Reason_15", Reason),
                new SqlParameter("@userid_16", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_package_subscription_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertbindtransaction(string List_No, string Journal_No, string Volume, string Part, string From_Issue, string To_Issue, string Lack_No, string Status, string Journal_Year, string From_pubdate, string To_Pubdate, DateTime Fromdate, DateTime todate, string CopyNo_F, string CopyNo_T)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[14]
            {
                new SqlParameter("@List_No_1", List_No),
                new SqlParameter("@Journal_No_2", Journal_No),
                new SqlParameter("@Volume_3", Volume),
                new SqlParameter("@From_Issue_5", Part),
                new SqlParameter("@To_Issue_6", To_Issue),
                new SqlParameter("@Lack_No_7", Lack_No),
                new SqlParameter("@Status_8", Status),
                new SqlParameter("@Journal_Year_9", Journal_Year),
                new SqlParameter("@From_pubdate_10", From_pubdate),
                new SqlParameter("@To_pubdate_11", To_Pubdate),
                new SqlParameter("@Fromdate_12", Fromdate),
                new SqlParameter("@todate_13", todate),
                new SqlParameter("@CopyNo_F", CopyNo_F),
                new SqlParameter("@CopyNo_T", CopyNo_T)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BindTransaction_Child_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertpackagemanagement(string packageid, string journalid, string gifted, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@packageid_1", packageid),
                new SqlParameter("@journalid_2", journalid),
                new SqlParameter("@gifted_3", gifted),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PackageManagement_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertlibrarytransaction(int invoice_id, int services, int noofcopy, decimal rate, int id, decimal tot_amt)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@invoice_id_1", invoice_id),
                new SqlParameter("@services_2", services),
                new SqlParameter("@noofcopy_3", noofcopy),
                new SqlParameter("@rate_4", rate),
                new SqlParameter("@id_5", id),
                new SqlParameter("@tot_amt_6", tot_amt)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_lib_serTrans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertPaymentTransaction(int PaymentID, float InvoiceID, float InvoiceAmount, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@PaymentID_1", PaymentID),
                new SqlParameter("@InvoiceID_2", InvoiceID),
                new SqlParameter("@InvoiceAmount_3", InvoiceAmount),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PaymentTransaction_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertbinderinformation(int binderid, string name, string address, string city, string state, string email, string phoneno, int maxissuedays, int overduecharges, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@binderid_1", binderid),
                new SqlParameter("@name_2", name),
                new SqlParameter("@address_3", address),
                new SqlParameter("@city_4", city),
                new SqlParameter("@state_5", state),
                new SqlParameter("@email_6", email),
                new SqlParameter("@phoneno_7", phoneno),
                new SqlParameter("@overduecharges_9", overduecharges),
                new SqlParameter("@userid_10", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PaymentTransaction_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertwindowweek(string Weeks, string Days, int Number)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@Weeks_1", Weeks),
                new SqlParameter("@Days_2", Days),
                new SqlParameter("@Number_3", Number)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Window_Week_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertonlinrjournal(int Journal_id, string FileName, byte[] Image_bin, string flag)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Journal_id", Journal_id),
                new SqlParameter("@FileName", FileName),
                new SqlParameter("@Image_bin", Image_bin),
                new SqlParameter("@flg", flag)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_onlineJournal_attachments]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertbudgetmaster(int departmentcode, decimal allocatedamount, decimal expendedamount, decimal approvalcommitedamt, decimal nonapprovalcommitedamt, decimal approvalindentinhandamt, decimal nonapprovalindentinhandamt, bool status, string Curr_Session, string userid, int VenorId, decimal VendorPer)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@departmentcode_1", departmentcode),
                new SqlParameter("@allocatedamount_2", allocatedamount),
                new SqlParameter("@expendedamount_3", expendedamount),
                new SqlParameter("@approvalcommitedamt_4", approvalcommitedamt),
                new SqlParameter("@nonapprovalcommitedamt_5", nonapprovalcommitedamt),
                new SqlParameter("@approvalindentinhandamt_6", approvalindentinhandamt),
                new SqlParameter("@nonapprovalindentinhandamt_7", nonapprovalindentinhandamt),
                new SqlParameter("@status_8", status),
                new SqlParameter("@Curr_Session_9", Curr_Session),
                new SqlParameter("@userid_10", userid),
                new SqlParameter("@VenorId", VenorId),
                new SqlParameter("@VendorPer", VendorPer)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_budgetmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertbudgetadjustment(DateTime Date, int departmentcode, float Amount, string Curr_Session, string Operation, string userid, string FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Date_1", Date),
                new SqlParameter("@departmentcode_2", departmentcode),
                new SqlParameter("@Amount_3", Amount),
                new SqlParameter("@Curr_Session_4", Curr_Session),
                new SqlParameter("@Operation_5", Operation),
                new SqlParameter("@userid_6", userid),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAdjustment_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Getissueauthority(int id, int userid, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@UserID", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetIssuingAuthority](@id , @UserID , @FormID , @Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable updateinsertjourrcarrival(string Journal_Id, string Doc_id, int Artical_No, string Artical_Title)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Journal_Id_1", Journal_Id),
                new SqlParameter("@Doc_id_22", Doc_id),
                new SqlParameter("@Artical_No_3", Artical_No),
                new SqlParameter("@Artical_Title_4", Artical_Title)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Jour_RecArrival_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertstandardreply(int reply_id, string reply, string user_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@reply_id_1", reply_id),
                new SqlParameter("@reply_2", reply),
                new SqlParameter("@user_id_3", user_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_standard_reply_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertinvoicemaster(string invoicenumber, DateTime invoicedate, int invoiceid, decimal postage, decimal netamount, decimal discountamount, decimal discountpercentage, string vendorid, string billserialno, decimal handlingcharge, string payCurrency, decimal payAmount, string reportingtypeofinvoice, decimal total_amt, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[17]
            {
                new SqlParameter("@invoicenumber_1", invoicenumber),
                new SqlParameter("@invoicedate_2", invoicedate),
                new SqlParameter("@invoiceid_3", invoiceid),
                new SqlParameter("@postage_4", postage),
                new SqlParameter("@netamount_5", netamount),
                new SqlParameter("@discountamount_6", discountamount),
                new SqlParameter("@discountpercentage_7", discountpercentage),
                new SqlParameter("@vendorid_8", vendorid),
                new SqlParameter("@billserialno_9", billserialno),
                new SqlParameter("@handlingcharge_10", handlingcharge),
                new SqlParameter("@payCurrency_11", payCurrency),
                new SqlParameter("@payAmount_12", payAmount),
                new SqlParameter("@typeofinvoice_13", reportingtypeofinvoice),
                new SqlParameter("@total_amt_14", total_amt),
                new SqlParameter("@user_id_15", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_invoicemaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertebookkeywords(int ID, string keyword_name, int key_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@ID_1", ID),
                new SqlParameter("@Keyword_name_2", keyword_name),
                new SqlParameter("@Key_id_3", key_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBOOK_Keywords_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertebookkeywords(int ID, string ApplicationName, string FileName, string Description)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ID_1", ID),
                new SqlParameter("@ApplicationName", ApplicationName),
                new SqlParameter("@FileName", FileName),
                new SqlParameter("@Description", Description)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ApplicationLinks]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertebookkeywords(int DescID, string DescTitle, string DescInfo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@DescID ", DescID),
                new SqlParameter("@DescTitle", DescTitle),
                new SqlParameter("@DescInfo", DescInfo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_idcardBackDesc]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertebookkeywords(string Journal_Id, string Doc_Id, string AMS, int Mediatype, string Description, string FileUrl, string postedfilename)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Journal_Id_1 ", Journal_Id),
                new SqlParameter("@Doc_Id_2", Doc_Id),
                new SqlParameter("@AMS_3", AMS),
                new SqlParameter("@Mediatype_Id_4 ", Mediatype),
                new SqlParameter("@Description_5", Description),
                new SqlParameter("@FileUrl_6", FileUrl),
                new SqlParameter("@postedfilename_6", postedfilename)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Jour_Accomaterial_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournaladdresstable(string Journal_Id, string Sus_ad_id, string rem_ad_id, string claim_ad_id, string sus_relation, string remi_relation, string claim_relation)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Journal_Id_1 ", Journal_Id),
                new SqlParameter("@Sus_ad_id_2", Sus_ad_id),
                new SqlParameter("@rem_ad_id_3", rem_ad_id),
                new SqlParameter("@claim_ad_id_4 ", claim_ad_id),
                new SqlParameter("@sus_relation_5 ", sus_relation),
                new SqlParameter("@remi_relation_6", remi_relation),
                new SqlParameter("@claim_relation_7", claim_relation)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_addresstable_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertExistJourPmt(string Journal_No, string Journal_Id, string Invoice_No, DateTime Invoice_Date, int Draft_No, DateTime Draft_date, string Journal_Amount, string currency)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Journal_No_1 ", Journal_No),
                new SqlParameter("@Journal_Id_2", Journal_Id),
                new SqlParameter("@Invoice_No_3", Invoice_No),
                new SqlParameter("@Invoice_Date_4 ", Invoice_Date),
                new SqlParameter("@Draft_No_5 ", Draft_No),
                new SqlParameter("@Draft_date_6", Draft_date),
                new SqlParameter("@Journal_Amount_7", Journal_Amount),
                new SqlParameter("@currency_8", currency)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Exist_JourPmt_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbindbillchild(int invoiceid, string bindtypename, int noofcopy, string amount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@invoiceid_1 ", invoiceid),
                new SqlParameter("@bindtypename_2", bindtypename),
                new SqlParameter("@noofcopy_3", noofcopy),
                new SqlParameter("@amount_4", amount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bindbillchild_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalbudget(string departmentname, int code, decimal price)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@departmentname_1 ", departmentname),
                new SqlParameter("@code_2", code),
                new SqlParameter("@price_3", price)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_budget_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsert(int invoiceid, DateTime receivedate, string invoiceno, DateTime invoicedate, int binderid, decimal totalamount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@invoiceid_1 ", invoiceid),
                new SqlParameter("@receivedate_2", receivedate),
                new SqlParameter("@invoiceno_3", invoiceno),
                new SqlParameter("@invoicedate_4 ", invoicedate),
                new SqlParameter("@binderid_5", binderid),
                new SqlParameter("@totalamount_6", totalamount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bindbillmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbouncemailrec(string eMailId, string fromEmail, string message, string Status, string eMailId1, string smtpAdd, string userid, string operation, string senddate)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@eMailID_1 ", eMailId),
                new SqlParameter("@fromEmail_2", fromEmail),
                new SqlParameter("@message_3", message),
                new SqlParameter("@Status_4 ", Status),
                new SqlParameter("@eMailId1_5", eMailId1),
                new SqlParameter("@smtpAdd_6", smtpAdd),
                new SqlParameter("@userid_7 ", userid),
                new SqlParameter("@Flag_8", operation),
                new SqlParameter("@senddate_9", senddate)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BouncemailRec_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertrevisionmanagement(int DAid, string Main_filenm, int Id, string title, string Prop_By, string Prop_Revno, string Prop_fileName, string Prop_Attachment, DateTime prop_dt, string Prop_toMembers, string comments)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@DAid ", DAid),
                new SqlParameter("@Main_filenm", Main_filenm),
                new SqlParameter("@Id", Id),
                new SqlParameter("@title ", title),
                new SqlParameter("@Prop_By", Prop_By),
                new SqlParameter("@Prop_Revno", Prop_Revno),
                new SqlParameter("@Prop_fileName ", Prop_fileName),
                new SqlParameter("@Prop_Attachment", Prop_Attachment),
                new SqlParameter("@prop_dt", prop_dt),
                new SqlParameter("@Prop_toMembers", Prop_toMembers),
                new SqlParameter("@comments", comments)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RevisionManagement]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertinvoicetransaction(int invoiceid, string ordernumber, decimal totalorderamount, string indentnumber, decimal srno, decimal discount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@invoiceid_1 ", invoiceid),
                new SqlParameter("@ordernumber_2", ordernumber),
                new SqlParameter("@totalorderamount_3", totalorderamount),
                new SqlParameter("@indentnumber_4 ", indentnumber),
                new SqlParameter("@srno_5", srno),
                new SqlParameter("@discount_6", discount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_invoicetransaction_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return null;
        }

        public DataTable updateinsertCircReceiveTransaction(string userid, string accno, DateTime receivingdate, decimal fineamount, string fineCause, string isPaid, DateTime Dueon, DateTime paidon, string amtexp, string userid1, int IssueId, int tran_id, int Id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[12];
            array[0] = new SqlParameter("@userid_1 ", userid);
            array[1] = new SqlParameter(" @accno_2 ", accno);
            array[2] = new SqlParameter("@receivingdate_3", receivingdate);
            array[3] = new SqlParameter("@fineamount_4", fineamount);
            array[4] = new SqlParameter("@fineCause_5", fineCause);
            array[5] = new SqlParameter("@isPaid_6", isPaid);
            array[0] = new SqlParameter("@DueDate_7 ", Dueon);
            array[6] = new SqlParameter(" @paidon_8 ", paidon);
            array[7] = new SqlParameter("@amtexp_9", amtexp);
            array[8] = new SqlParameter("@userid1_10", userid);
            array[9] = new SqlParameter("@IssueId_11", IssueId);
            array[10] = new SqlParameter("@tran_id", tran_id);
            array[11] = new SqlParameter("@Id", Id);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[CircReceiveTransaction]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertepc(string AccNumber, string Location, string Rfid, string userName, DateTime LoginDate, string LoginTime, string sessionyr, string IpAddress, string UserAction)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@AccNumber", AccNumber),
                new SqlParameter("@Location", Location),
                new SqlParameter("@Rfid", Rfid),
                new SqlParameter("@userName", userName),
                new SqlParameter("@LoginDate", LoginDate),
                new SqlParameter("@LoginTime", LoginTime),
                new SqlParameter("@sessionyr", sessionyr),
                new SqlParameter("@IpAddress", IpAddress),
                new SqlParameter("@UserAction", UserAction)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Sp_insertEPC]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertserverrepository(int Id, string upFileName, string upFile, string downloadLink, int UserId)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@upfileName", upFileName),
                new SqlParameter("@upFile", upFile),
                new SqlParameter("@downloadLink", downloadLink),
                new SqlParameter("@UserId", UserId)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_serverRepository]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertduelistmaster(string listno, DateTime listdate, string membergroup, string paidstatus, string program_name, string dept, string JoinYear, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@listno_1", listno),
                new SqlParameter("@listdate_2", listdate),
                new SqlParameter("@membergroup_3", membergroup),
                new SqlParameter("@paidstatus_4", paidstatus),
                new SqlParameter("@program_name_5", program_name),
                new SqlParameter("@dept_6", dept),
                new SqlParameter("@JoinYear_7", JoinYear),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Duelistmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertmemissctgwise(string Memberid, int Category_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@Memberid_1", Memberid),
                new SqlParameter("@Category_id_2", Category_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_MemIssCtgWise_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertordermasterjournal(string OrderNo, DateTime OrderDate, string PublisherCode, string Status, string Order_Status, string relation, string Userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@OrderNo_1", OrderNo),
                new SqlParameter("@OrderDate_2", OrderDate),
                new SqlParameter("@PublisherCode_3", PublisherCode),
                new SqlParameter("@Status_4", Status),
                new SqlParameter("@Order_Status_5", Order_Status),
                new SqlParameter("@relation_6", relation),
                new SqlParameter("@userid_7", Userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OrderMaster_Journal_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertddcbooknumber(int id, string MainClass, string DivisionTag, string SectiionTag, string Book_Title, string DDC_Number)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Id_1", id),
                new SqlParameter("@MainClass_2", MainClass),
                new SqlParameter("@DivisionTag_3", DivisionTag),
                new SqlParameter("@SectiionTag_4", SectiionTag),
                new SqlParameter("@Book_Title_5", Book_Title),
                new SqlParameter("@DDC_Number_6", DDC_Number)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DDC_BookNumber_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertddcbooknumber(int ctrl_no, string classnumber, string booknumber, int TransNo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@classnumber_2", classnumber),
                new SqlParameter("@booknumber_3", booknumber),
                new SqlParameter("@TransNo", TransNo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DDC_BookNumber_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertstockentrychild(string docno, string accessionnumber, string status, string missing_status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@docno_1", docno),
                new SqlParameter("@accessionnumber_2", accessionnumber),
                new SqlParameter("@status_3", status),
                new SqlParameter("@missing_status_4", missing_status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Stock_Entry_Child_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbookconference(int ctrl_no, string Subtitle, string Paralleltype, string ConfName, string ConfYear, string BNNote, string CNNote, string GNNotes, string VNNotes, string SNNotes, string ANNotes, string Course, string AdFname1, string AdMname1, string AdLname1, string AdFname2, string AdMname2, string AdLname2, string AdFname3, string AdMname3, string AdLName3, string Abstract, string program_name)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[23]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@Subtitle_2", Subtitle),
                new SqlParameter("@Paralleltype_3", Paralleltype),
                new SqlParameter("@ConfName_4", ConfName),
                new SqlParameter("@ConfYear_5", ConfYear),
                new SqlParameter("@BNNote_6", BNNote),
                new SqlParameter("@CNNote_7", CNNote),
                new SqlParameter("@GNNotes_8", GNNotes),
                new SqlParameter("@VNNotes_9", VNNotes),
                new SqlParameter("@SNNotes_10", SNNotes),
                new SqlParameter("@ANNotes_11", ANNotes),
                new SqlParameter("@Course_12", Course),
                new SqlParameter("@AdFname1_13", AdFname1),
                new SqlParameter("@AdMname1_14", AdMname1),
                new SqlParameter("@AdLname1_15", AdLname1),
                new SqlParameter("@AdFname2_16", AdFname2),
                new SqlParameter("@AdMname2_17", AdMname2),
                new SqlParameter("@AdLname2_18", AdLname2),
                new SqlParameter("@AdFname3_19", AdFname3),
                new SqlParameter("@AdMname3_20", AdMname3),
                new SqlParameter("@AdLName3_21", AdLName3),
                new SqlParameter("@Abstract_22", Abstract),
                new SqlParameter("@program_name_23", program_name)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookConference_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbookentryConference(int ctrl_no, string Subtitle, string Paralleltype, string ConfName, string ConfYear, string BNNote, string CNNote, string GNNotes, string VNNotes, string SNNotes, string ANNotes, string Course, string AdFname1, string AdMname1, string AdLname1, string AdFname2, string AdMname2, string AdLname2, string AdFname3, string AdMname3, string AdLName3, string Abstract, string program_name, int TransNo, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[25]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@Subtitle_2", Subtitle),
                new SqlParameter("@Paralleltype_3", Paralleltype),
                new SqlParameter("@ConfName_4", ConfName),
                new SqlParameter("@ConfYear_5", ConfYear),
                new SqlParameter("@BNNote_6", BNNote),
                new SqlParameter("@CNNote_7", CNNote),
                new SqlParameter("@GNNotes_8", GNNotes),
                new SqlParameter("@VNNotes_9", VNNotes),
                new SqlParameter("@SNNotes_10", SNNotes),
                new SqlParameter("@ANNotes_11", ANNotes),
                new SqlParameter("@Course_12", Course),
                new SqlParameter("@AdFname1_13", AdFname1),
                new SqlParameter("@AdMname1_14", AdMname1),
                new SqlParameter("@AdLname1_15", AdLname1),
                new SqlParameter("@AdFname2_16", AdFname2),
                new SqlParameter("@AdMname2_17", AdMname2),
                new SqlParameter("@AdLname2_18", AdLname2),
                new SqlParameter("@AdFname3_19", AdFname3),
                new SqlParameter("@AdMname3_20", AdMname3),
                new SqlParameter("@AdLName3_21", AdLName3),
                new SqlParameter("@Abstract_22", Abstract),
                new SqlParameter("@program_name_23", program_name),
                new SqlParameter("@TransNo", TransNo),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookConference_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbookconferences(int ctrl_no, string Subtitle, string Paralleltype, string ConfName, string ConfYear, string BNNote, string CNNote, string GNNotes, string VNNotes, string SNNotes, string ANNotes, string Course, string AdFname1, string AdMname1, string AdLname1, string AdFname2, string AdMname2, string AdLname2, string AdFname3, string AdMname3, string AdLName3, string Abstract, string program_name)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[23]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@Subtitle_2", Subtitle),
                new SqlParameter("@Paralleltype_3", Paralleltype),
                new SqlParameter("@ConfName_4", ConfName),
                new SqlParameter("@ConfYear_5", ConfYear),
                new SqlParameter("@BNNote_6", BNNote),
                new SqlParameter("@CNNote_7", CNNote),
                new SqlParameter("@GNNotes_8", GNNotes),
                new SqlParameter("@VNNotes_9", VNNotes),
                new SqlParameter("@SNNotes_10", SNNotes),
                new SqlParameter("@ANNotes_11", ANNotes),
                new SqlParameter("@Course_12", Course),
                new SqlParameter("@AdFname1_13", AdFname1),
                new SqlParameter("@AdMname1_14", AdMname1),
                new SqlParameter("@AdLname1_15", AdLname1),
                new SqlParameter("@AdFname2_16", AdFname2),
                new SqlParameter("@AdMname2_17", AdMname2),
                new SqlParameter("@AdLname2_18", AdLname2),
                new SqlParameter("@AdFname3_19", AdFname3),
                new SqlParameter("@AdMname3_20", AdMname3),
                new SqlParameter("@AdLName3_21", AdLName3),
                new SqlParameter("@Abstract_22", Abstract),
                new SqlParameter("@program_name_23", program_name)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookConference_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertilltransactions(DateTime issuedate, int ILLid, string accno, DateTime exparrivaldate, string status, int check_status, string DocumentNo, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@issuedate_1", issuedate),
                new SqlParameter("@ILLid_2", ILLid),
                new SqlParameter("@accno_3", accno),
                new SqlParameter("@exparrivaldate_4", exparrivaldate),
                new SqlParameter("@status_5", status),
                new SqlParameter("@check_status_6", check_status),
                new SqlParameter("@DocumentNo_7", DocumentNo),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ILLtransactions_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertusertypepermission(int usertypeid, string UserTypeName)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@id_1", usertypeid),
                new SqlParameter("@UserTypeName_2", UserTypeName)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypePermissions_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbudgetallocjournal(int departmentcode, float allocated_amount, float expended_amount, float committed_amount, float balance, int status, string academic_session, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@departmentcode_1", departmentcode),
                new SqlParameter("@allocated_amount_2", allocated_amount),
                new SqlParameter("@expended_amount_3", expended_amount),
                new SqlParameter("@committed_amount_4", committed_amount),
                new SqlParameter("@balance_5", balance),
                new SqlParameter("@status_6", status),
                new SqlParameter("@academic_session_7", academic_session),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAllocJournal_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertbarcodesheet(int Id, string SheetName, int BColumns, int BRows, float HPitch, float VPitch, float LHeight, float LWidth, float TopMargin, float BottomMargin, float LeftMargin, float RightMargin, float barcodeHeight, float OfficeId)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[14]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@SheetName", SheetName),
                new SqlParameter("@BColumns", BColumns),
                new SqlParameter("@BRows", BRows),
                new SqlParameter("@HPitch", HPitch),
                new SqlParameter("@VPitch", VPitch),
                new SqlParameter("@LHeight", LHeight),
                new SqlParameter("@LWidth", LWidth),
                new SqlParameter("@TopMargin", TopMargin),
                new SqlParameter("@BottomMargin", BottomMargin),
                new SqlParameter("@LeftMargin", LeftMargin),
                new SqlParameter("@RightMargin", RightMargin),
                new SqlParameter("@barcodeHeight", barcodeHeight),
                new SqlParameter("@OfficeId", OfficeId)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_barcode_Sheet]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertpaymentmaster(int paymentid, int draftnumber, int currencycode, DateTime draftdate, string bankname, float amount, string paymenttype, string vendorid, DateTime paymentdate, float draft_amt, string documentno, string payment_type, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[15]
            {
                new SqlParameter("@paymentid_1", paymentid),
                new SqlParameter("@draftnumber_2", draftnumber),
                new SqlParameter("@currencycode_3", currencycode),
                new SqlParameter("@draftdate_4", draftdate),
                new SqlParameter("@bankname_5", bankname),
                new SqlParameter("@amount_6", amount),
                new SqlParameter("@paymenttype_7", paymenttype),
                new SqlParameter("@vendorid_8", vendorid),
                new SqlParameter("@paymentdate_9", paymentdate),
                new SqlParameter("@draft_amt_10", draft_amt),
                new SqlParameter("@documentno_11", documentno),
                new SqlParameter("@payment_type_12", payment_type),
                new SqlParameter("@userid_13", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_paymentmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertaccountpaymentmaster(int paymentid, float amount, string paymenttype, string vendorid, DateTime paymentdate, string documentno, string payment_type, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@paymentid_1", paymentid),
                new SqlParameter("@amount_2", amount),
                new SqlParameter("@paymenttype_3", paymenttype),
                new SqlParameter("@vendorid_4", vendorid),
                new SqlParameter("@paymentdate_5", paymentdate),
                new SqlParameter("@documentno_6", documentno),
                new SqlParameter("@payment_type_7", payment_type),
                new SqlParameter("@userid_8", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_letter_acctopmtmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertrelative(string membercode, string relationship, string reladd, string relname, string flg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Member_code", membercode),
                new SqlParameter("@Relationship", relationship),
                new SqlParameter("@Rel_name", relname),
                new SqlParameter("@Rel_Add", reladd)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_letter_acctopmtmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertdalmaster(string DAId, string AccessionNo, string fileName, string filepath, string filepasswd, string passwdkey, string type, string Type_No, string file_grp, byte[] File_BData, byte[] supportDoc_bdata, string supportDoc_name, string userId, string folderFile_virtualPath)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[14]
            {
                new SqlParameter("@DAId_1", DAId),
                new SqlParameter("@AccessionNo_2", AccessionNo),
                new SqlParameter("@fileName_3", fileName),
                new SqlParameter("@filepath_4", filepath),
                new SqlParameter("@filepasswd_5", filepasswd),
                new SqlParameter("@passwdkey_6", passwdkey),
                new SqlParameter("@type_7", type),
                new SqlParameter("@Type_No", Type_No),
                new SqlParameter("@file_grp", file_grp),
                new SqlParameter("@File_BData", File_BData),
                new SqlParameter("@supportDoc_bdata", supportDoc_bdata),
                new SqlParameter("@supportDoc_name", supportDoc_name),
                new SqlParameter("@userId", userId),
                new SqlParameter("@folderFile_virtualPath", folderFile_virtualPath)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DAFileInfo_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetActionUserdetail(int SubMenu_id, string Usertype, string Userid2, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@SubMenu_id", SubMenu_id),
                new SqlParameter("@Usertype", Usertype),
                new SqlParameter("@Userid2", Userid2),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetActionUserdetail](@SubMenu_id,@Usertype,@Userid2  ,@UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetLibCurrency()
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[0];
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLibCurrency]( )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetActionUserPerm(int Memberid, int Actionid, int Submenu_id, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Memberid", Memberid),
                new SqlParameter("@Actionid", Actionid),
                new SqlParameter("@Submenu_id", Submenu_id),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetActionUserPerm](@Memberid,@Actionid,@Submenu_id  ,@UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBudget(string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetBudget](@UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndent(string indentnumber, string indentId, int departmentcode, string titleExact, string title, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentId", indentId),
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@titleExact", titleExact),
                new SqlParameter("@title", title),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndent](@indentnumber,@indentId,@departmentcode,@titleExact,@title, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GenerateIndentId()
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[0];
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GenerateIndentId]( )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GenearteItemID(string indentnumber)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@indentnumber", indentnumber)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GenearteItemID](@indentnumber )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DupIndentVendIndentId(int vendorid, string indentnumber, int indentid, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@vendorid", vendorid),
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentid", indentid),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[DupIndentVendIndentId](@vendorid, @indentnumber,@indentid,@UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DupIndentDateIndentId(DateTime indentdate, string indentnumber, int indentid, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@indentdate", indentdate),
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentid", indentid),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[DupIndentDateIndentId](@indentdate, @indentnumber,@indentid,@UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateOpacIndent(string indentnumber, int indentid, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentid", indentid),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateOpacIndent]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateDepartmentCurrentPos(int departmentcode, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@departmentcode ", departmentcode),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateDepartmentCurrentPos]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateBudgetAddApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@departmentcode ", departmentcode),
                new SqlParameter("@Curr_Session ", Curr_Session),
                new SqlParameter("@IndentValue ", IndentValue),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddApprove]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateBudgetDecrNonApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@departmentcode ", departmentcode),
                new SqlParameter("@Curr_Session ", Curr_Session),
                new SqlParameter("@IndentValue ", IndentValue),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetDecrNonApprove]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateBudgetAddNonApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@departmentcode ", departmentcode),
                new SqlParameter("@Curr_Session ", Curr_Session),
                new SqlParameter("@IndentValue ", IndentValue),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddNonApprove]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateBudgetDecrApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@departmentcode ", departmentcode),
                new SqlParameter("@Curr_Session ", Curr_Session),
                new SqlParameter("@IndentValue ", IndentValue),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetDecrApprove]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateIndentOnlinePStatus(string indentnumber, int OnlinePStatus, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@indentnumber ", indentnumber),
                new SqlParameter("@OnlinePStatus ", OnlinePStatus),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateIndentOnlinePStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetActionLPermUser(string MemberId, int ActionId, int Submenu_id, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@MemberId", MemberId),
                new SqlParameter("@ActionId", ActionId),
                new SqlParameter("@Submenu_id", Submenu_id),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetActionLPermUser](@MemberId, @ActionId,@Submenu_id,@UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetUserCanRequest(string UserID2, string departmentcode, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@UserID2", UserID2),
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[GetUserCanRequest](@UserID2, @departmentcode, @UserId,@FormId,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteIndent(string indentId, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@indentId ", indentId),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[DeleteIndent]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable IndentItemNoDecr(string indentNo, string ItemNo, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@indentNo ", indentNo),
                new SqlParameter("@ItemNo ", ItemNo),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[IndentItemNoDecr]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetIndentVarious(string Title, string Author, string Firstname, string Vendorname, string SeriesName, string Isbn, string IndentItemNo, string All, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@Title", Title),
                new SqlParameter("@Author", Author),
                new SqlParameter("@Firstname", Firstname),
                new SqlParameter("@Vendorname", Vendorname),
                new SqlParameter("@SeriesName", SeriesName),
                new SqlParameter("@Isbn", Isbn),
                new SqlParameter("@IndentItemNo", IndentItemNo),
                new SqlParameter("@All", All),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentVarious](@Title,@Author ,@Firstname ,@Vendorname ,@SeriesName ,@Isbn ,@IndentItemNo ,@All , @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateIndentPrintStatus(string indentnumber, string PrintStatus, string UserID, string FormId, string Type)
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@indentnumber ", indentnumber),
                new SqlParameter("@PrintStatus ", PrintStatus),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet objDataset = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateIndentPrintStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetLanguage(int Language_Id, string Language_Name, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Language_Id", Language_Id),
                new SqlParameter("@Language_Name", Language_Name),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLanguage](@Language_Id,@Language_Name  , @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetdeptInst(string Session, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Session", Session),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetdeptInst](@Session  , @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCurrencyExchange(string CurrencyCode, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@CurrencyCode", CurrencyCode),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetCurrencyExchange](@CurrencyCode  , @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetMediaType(int media_id, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            media_id = ((media_id == 0) ? (-1) : media_id);
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@media_id", media_id),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetMediaType](@media_id  , @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetPublOpacEmptyIndent(string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetPublOpacEmptyIndent]( @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable SuggIndentTitle(string Title, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Title", Title),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[SuggIndentTitle](@Title, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentDetail(string indentnumber, string indentId, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentId", indentId),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDetail](@indentnumber,@indentId, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBookDetail(string Accno, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Accno", Accno),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetBookDetail](@Accno, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetOpacIndent(string Indentid, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Indentid", Indentid),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetOpacIndent](@Indentid, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetNewIindent(string indentnumber, string indentid, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentid", indentid),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            DataSet dataSet = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetNewIindent](@indentnumber,@indentid, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Updateinsertstaffmaster(int departmentcode, string staffid, string firstname, string middlename, string lastname, string email, string phone1, string phone2, DateTime doj, string gender, string pfno, string email2, DateTime validupto, string remark, byte user_picture, string classname, string Fathername, DateTime Dob, string cat_id, string program_id, string Joinyear, string subjects, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[23]
            {
                new SqlParameter("@departmentcode_1", departmentcode),
                new SqlParameter("@staffid_2", staffid),
                new SqlParameter("@firstname_3", firstname),
                new SqlParameter("@middlename_4", middlename),
                new SqlParameter("@lastname_5", lastname),
                new SqlParameter("@email_6", email),
                new SqlParameter("@phone1_7", phone1),
                new SqlParameter("@phone2_8", phone2),
                new SqlParameter("@doj_10", doj),
                new SqlParameter("@gender_11", gender),
                new SqlParameter("@pfno_12", pfno),
                new SqlParameter("@email2_13", email2),
                new SqlParameter("@validupto_14", validupto),
                new SqlParameter("@remark_15", remark),
                new SqlParameter("@user_picture_16", user_picture),
                new SqlParameter("@classname_17", classname),
                new SqlParameter("@Fathername_18", Fathername),
                new SqlParameter("@Dob_19", Dob),
                new SqlParameter("@cat_id_20", cat_id),
                new SqlParameter("@program_id_21", program_id),
                new SqlParameter("@Joinyear_22", Joinyear),
                new SqlParameter("@subjects_23", subjects),
                new SqlParameter("@userid_24", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_staffmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertbookaccessionmaster(string accessionnumber, string cpbooknumber, string ordernumber, string indentnumber, string form, string accessionid, string accessiondate, string booktitle, string srno, string released, string bookprice, string srnoOld, string status, string releaseddate, string issuestatus, string loadingdate, string checkstatus, string ctrlNo, string editionyear, string copynumber, string specialprice, string pubyear, string billNo, string billdate, string catalogdate, string itemtype, string originalprice, string originalcurrency, string vendorsource, string programid, string deptcode, string DSrno, string itemcategory, string Locid, string userid, string sess, string ipa, string title, string appname, string transno)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[40]
            {
                new SqlParameter("@accessionnumber", accessionnumber),
                new SqlParameter("@cp_booknumber", cpbooknumber),
                new SqlParameter("@ordernumber", ordernumber),
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@form", form),
                new SqlParameter("@accessionid", accessionid),
                new SqlParameter("@accessioneddate", accessiondate),
                new SqlParameter("@booktitle", booktitle),
                new SqlParameter("@srno", srno),
                new SqlParameter("@released", released),
                new SqlParameter("@bookprice", bookprice),
                new SqlParameter("@srNoOld", srnoOld),
                new SqlParameter("@Status", status),
                new SqlParameter("@ReleaseDate", releaseddate),
                new SqlParameter("@IssueStatus", issuestatus),
                new SqlParameter("@LoadingDate", loadingdate),
                new SqlParameter("@CheckStatus", checkstatus),
                new SqlParameter("@ctrl_no", ctrlNo),
                new SqlParameter("@editionyear", editionyear),
                new SqlParameter("@Copynumber", copynumber),
                new SqlParameter("@Specialprice", specialprice),
                new SqlParameter("@pubYear", pubyear),
                new SqlParameter("@biilNo", billNo),
                new SqlParameter("@billDate", billdate),
                new SqlParameter("@catalogDate", catalogdate),
                new SqlParameter("@Item_Type", itemtype),
                new SqlParameter("@OriginalPrice", originalprice),
                new SqlParameter("@OriginalCurrency", originalcurrency),
                new SqlParameter("@vendor_source", vendorsource),
                new SqlParameter("@program_id", programid),
                new SqlParameter("@DeptCode", deptcode),
                new SqlParameter("@DSrno", DSrno),
                new SqlParameter("@ItemCategory", itemcategory),
                new SqlParameter("@Loc_id", Locid),
                new SqlParameter("@UserId", userid),
                new SqlParameter("@Sess_102", sess),
                new SqlParameter("@IPA_103", ipa),
                new SqlParameter("@Title_104", title),
                new SqlParameter("@AppName", appname),
                new SqlParameter("@TransNo", transno)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bookaccessionmaster_new]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertnewfolder(string foldername, int id, string resourcetype, string flag, string indexcatalogue)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@FolderName", foldername),
                new SqlParameter("@ID", id),
                new SqlParameter("@ResourceType", resourcetype),
                new SqlParameter("@flag", flag),
                new SqlParameter("@IndexCatalog", indexcatalogue)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NEWFOLDER]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertbookcatalog(int ctrl_no, DateTime catalogdate1, int booktype, string volumenumber, string initpages, int pages, string parts, string leaves, string boundind, string title, string publishercode, string edition, string isbn, string subject1, string subject2, string subject3, string Booksize, string LCCN, string Volumepages, string biblioPages, string bookindex, string illustration, string variouspaging, string maps, string ETalEditor, string ETalCompiler, string ETalIllus, string ETalTrans, string ETalAuthor, string accmaterialhistory, string MaterialDesignation, string issn, string Volume, int dept, int language_id, string part, string eBookURL, string cat_Source, string Identifier, string firstname, string percity, string perstate, string percountry, string peraddress, string departmentname, string Btype, string language_name, string ItemCategory, int TransNo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[48];
            array[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            array[1] = new SqlParameter(" @catalogdate_2 ", catalogdate1);
            array[2] = new SqlParameter("@booktype_3", booktype);
            array[3] = new SqlParameter("@volumenumber_4", volumenumber);
            array[4] = new SqlParameter("@initpages_5", initpages);
            array[5] = new SqlParameter("@pages_6", pages);
            array[0] = new SqlParameter("@parts_7 ", parts);
            array[6] = new SqlParameter(" @leaves_8 ", leaves);
            array[7] = new SqlParameter("@boundind_9", boundind);
            array[8] = new SqlParameter("@title_10", title);
            array[9] = new SqlParameter("@publishercode_11", publishercode);
            array[10] = new SqlParameter("@edition_12", edition);
            array[11] = new SqlParameter("@isbn_13", isbn);
            array[12] = new SqlParameter("@subject1_14 ", subject1);
            array[13] = new SqlParameter(" @subject2_15 ", subject2);
            array[14] = new SqlParameter("@subject3_16", subject3);
            array[15] = new SqlParameter("@Booksize_17", Booksize);
            array[16] = new SqlParameter("@LCCN_18", LCCN);
            array[17] = new SqlParameter("@Volumepages_19", Volumepages);
            array[18] = new SqlParameter("@biblioPages_20 ", biblioPages);
            array[19] = new SqlParameter("@bookindex_21 ", bookindex);
            array[20] = new SqlParameter("@illustration_22", illustration);
            array[21] = new SqlParameter("@variouspaging_23", variouspaging);
            array[22] = new SqlParameter("@maps_24", maps);
            array[23] = new SqlParameter("@ETalEditor_25", ETalEditor);
            array[24] = new SqlParameter("@ETalCompiler_26", ETalCompiler);
            array[25] = new SqlParameter("@ETalIllus_27 ", ETalIllus);
            array[26] = new SqlParameter(" @ETalTrans_28 ", ETalTrans);
            array[27] = new SqlParameter("@ETalAuthor_29", ETalAuthor);
            array[28] = new SqlParameter("@accmaterialhistory_31", accmaterialhistory);
            array[29] = new SqlParameter("@MaterialDesignation_32", MaterialDesignation);
            array[30] = new SqlParameter("@issn_33", issn);
            array[31] = new SqlParameter("@Volume_34 ", Volume);
            array[32] = new SqlParameter(" @dept_35 ", dept);
            array[33] = new SqlParameter("@language_id_36", language_id);
            array[34] = new SqlParameter("@part_37", part);
            array[35] = new SqlParameter("@eBookURL_38", eBookURL);
            array[36] = new SqlParameter("@cat_Source_39", cat_Source);
            array[37] = new SqlParameter("@Identifier_40", Identifier);
            array[38] = new SqlParameter("@firstname_41 ", firstname);
            array[39] = new SqlParameter("@percity_42 ", percity);
            array[40] = new SqlParameter("@perstate_43", perstate);
            array[41] = new SqlParameter("@percountry_44", percountry);
            array[42] = new SqlParameter("@peraddress_45", peraddress);
            array[43] = new SqlParameter("@departmentname_46", departmentname);
            array[44] = new SqlParameter("@Btype_47 ", Btype);
            array[45] = new SqlParameter("@language_name_48 ", language_name);
            array[46] = new SqlParameter("@ItemCategory_49", ItemCategory);
            array[47] = new SqlParameter("@TransNo", TransNo);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookCatalog_1]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbudgetadjustmentjournal(DateTime Date, int departmentcode, decimal Amount, string Curr_Session, string Operation, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Date_1", Date),
                new SqlParameter("@departmentcode_2", departmentcode),
                new SqlParameter("@Amount_3", Amount),
                new SqlParameter("@Curr_Session_4", Curr_Session),
                new SqlParameter("@Operation_5", Operation),
                new SqlParameter("@userid_6", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAdjustmentJournal_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinserttranslationlanguage(int Language_Id, string Language_Name, string Font_Name, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Language_Id_1", Language_Id),
                new SqlParameter("@Language_Name_2", Language_Name),
                new SqlParameter("@Font_Name_3", Font_Name),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Translation_Language_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertcdmasterpassword(string id, string title, string fileName, string filepath, string filepassword, string passwdkey, string lev_id, int cd_mastId, string client_server, string FileSize, string FileBytes, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[13]
            {
                new SqlParameter("@id_1", id),
                new SqlParameter("@title_2", title),
                new SqlParameter("@fileName_3", fileName),
                new SqlParameter("@filepath_4", filepath),
                new SqlParameter("@filepassword_5", filepassword),
                new SqlParameter("@passwdkey_6", passwdkey),
                new SqlParameter("@lev_id_7", lev_id),
                new SqlParameter("@Cd_MastId", cd_mastId),
                new SqlParameter("@client_server_8", client_server),
                new SqlParameter("@FileSize", FileSize),
                new SqlParameter("@FileBytes", FileBytes),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_cd_master_password]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertrevisioncomment(int Id, int Daid, int fileId, int revisionId, string UserId, string Comment)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Id_1", Id),
                new SqlParameter("@Daid", Daid),
                new SqlParameter("@fileId", fileId),
                new SqlParameter("@revisionId", revisionId),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@comment", Comment)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_revision_Comment]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertCastCategories(int cat_id, string cat_name, string userid, string shortname)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@cat_id_1", cat_id),
                new SqlParameter("@cat_name_2", cat_name),
                new SqlParameter("@userid_3", userid),
                new SqlParameter("@shortname_4", shortname)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CastCategories_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertloginmaster(string LoginName, DateTime LoginDate, string LoginTime, string TableName, string UserAction, string id, string sessionyr, string IpAddress)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@LoginName_1", LoginName),
                new SqlParameter("@LoginDate_2", LoginDate),
                new SqlParameter("@LoginTime_3", LoginTime),
                new SqlParameter("@TableName_4", TableName),
                new SqlParameter("@UserAction_5", UserAction),
                new SqlParameter("@id_6", id),
                new SqlParameter("@sessionyr_7", sessionyr),
                new SqlParameter("@IpAddress_8", IpAddress)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_LoginMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertmembershipfreedefinition(int id, string Member_code, string membership_type, DateTime From_dt, DateTime To_dt, string frequency, decimal fee_amount, string odc_type, decimal odc_amount, DateTime Wef_dt, string userid, string session_yr)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@Member_code", Member_code),
                new SqlParameter("@membership_type", membership_type),
                new SqlParameter("@From_dt", From_dt),
                new SqlParameter("@To_dt", To_dt),
                new SqlParameter("@frequency", frequency),
                new SqlParameter("@fee_amount", fee_amount),
                new SqlParameter("@odc_type", odc_type),
                new SqlParameter("@odc_amount", odc_amount),
                new SqlParameter("@Wef_dt", Wef_dt),
                new SqlParameter("@userid", userid),
                new SqlParameter("@session_yr", session_yr)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Membership_FeeDefinition]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalrefunddetails(string Bill_serial_no, string CreditNote_no, decimal Amount, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Bill_serial_no_1", Bill_serial_no),
                new SqlParameter("@CreditNote_no_2", CreditNote_no),
                new SqlParameter("@Amount_3", Amount),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Journal_Refund_Detail_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertdistributedbudget(int departmentcode, decimal Amt_percentage, decimal amount, string bill_seriaL_NO, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@departmentcode_1", departmentcode),
                new SqlParameter("@Amt_percentage_2", Amt_percentage),
                new SqlParameter("@amount_3", amount),
                new SqlParameter("@bill_seriaL_NO_4", bill_seriaL_NO),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_distributed_budget_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertmembershipfreedefinitionchild(int Main_id, string Member_id, DateTime Expected_Date, DateTime Submission_date, DateTime curr_date, decimal AMount, decimal Fine_amt, string status, string paid_type, int row_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Main_id", Main_id),
                new SqlParameter("@Member_id", Member_id),
                new SqlParameter("@Expected_Date", Expected_Date),
                new SqlParameter("@Submission_date", Submission_date),
                new SqlParameter("@curr_date", curr_date),
                new SqlParameter("@AMount", AMount),
                new SqlParameter("@Fine_amt", Fine_amt),
                new SqlParameter("@status", status),
                new SqlParameter("@paid_type", paid_type),
                new SqlParameter("@row_id", row_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Membership_FeeDefinition_child]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertmembershippaiduser(int Id, string MemberId, decimal Amount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@MemberId", MemberId),
                new SqlParameter("@Amount", Amount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_MembershipPaidUser]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertdirectinvoicemaster(int srno, DateTime curr_date, decimal postage, decimal net_amt, decimal disc_amt, decimal disc_percentage, string vedorid, decimal handling_charge, decimal total_amt, int rate_followed_by, decimal pay_amt, string invoice_no, string bill_no, DateTime bill_date)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[14]
            {
                new SqlParameter("@srno_1", srno),
                new SqlParameter("@curr_date_2", curr_date),
                new SqlParameter("@postage_3", postage),
                new SqlParameter("@net_amt_4", net_amt),
                new SqlParameter("@disc_amt_5", disc_amt),
                new SqlParameter("@disc_percentage_6", disc_percentage),
                new SqlParameter("@vedorid_7", vedorid),
                new SqlParameter("@handling_charge_8", handling_charge),
                new SqlParameter("@total_amt_9", total_amt),
                new SqlParameter("@rate_followed_by_10", rate_followed_by),
                new SqlParameter("@pay_amt_11", pay_amt),
                new SqlParameter("@invoice_no_12", invoice_no),
                new SqlParameter("@bill_no_13", bill_no),
                new SqlParameter("@bill_date_14", bill_date)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_direct_invoice_master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertJournalInvoice_Status(string Invoice_id, string Status, string Draft_No, DateTime Draft_Date, DateTime Entry_Date, string Publisher_Code, string Type, string remark, string userid, int FormID, int Type1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@Status_2", Status),
                new SqlParameter("@Draft_No_3", Draft_No),
                new SqlParameter("@Draft_Date_4", Draft_Date),
                new SqlParameter("@Entry_Date_5", Entry_Date),
                new SqlParameter("@Publisher_Code_6", Publisher_Code),
                new SqlParameter("@Type_7", Type),
                new SqlParameter("@remark_8", remark),
                new SqlParameter("@userid_9", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalInvoice_Status_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertReceive_details(string Rec_ID, string Member_ID, DateTime Curr_dt, DateTime Rec_dt, decimal Curr_Amt, decimal Rec_amt, decimal Curr_fine, decimal Rec_fine, int Main_id, int row_id, string userid, string session_yr)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@Rec_ID", Rec_ID),
                new SqlParameter("@Member_ID", Member_ID),
                new SqlParameter("@Curr_dt", Curr_dt),
                new SqlParameter("@Rec_dt", Rec_dt),
                new SqlParameter("@Curr_Amt", Curr_Amt),
                new SqlParameter("@Rec_amt", Rec_amt),
                new SqlParameter("@Curr_fine", Curr_fine),
                new SqlParameter("@Rec_fine", Rec_fine),
                new SqlParameter("@Main_id", Main_id),
                new SqlParameter("@row_id", row_id),
                new SqlParameter("@userid", userid),
                new SqlParameter("@session_yr", session_yr)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Receive_details]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertqualification(string Member_code, string Ex_passesd, string Board, string RollN, string P_year, int Marks, int Outof, decimal per_age, string status, string Subject)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Member_code", Member_code),
                new SqlParameter("@Ex_passesd", Ex_passesd),
                new SqlParameter("@Board", Board),
                new SqlParameter("@RollN", RollN),
                new SqlParameter("@P_year", P_year),
                new SqlParameter("@Marks", Marks),
                new SqlParameter("@Outof", Outof),
                new SqlParameter("@per_age", per_age),
                new SqlParameter("@status", status),
                new SqlParameter("@Subject", Subject)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Qualificarions_c]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournal_PmtTrans(int PaymentID, string InvoiceID, decimal InvoiceAmount, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@PaymentID_1", PaymentID),
                new SqlParameter("@InvoiceID_2", InvoiceID),
                new SqlParameter("@InvoiceAmount_3", InvoiceAmount),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_PmtTrans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinserthobbies(string Member_code, int id, string hobbies, string flg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Member_code", Member_code),
                new SqlParameter("@id", id),
                new SqlParameter("@Hobbies", hobbies),
                new SqlParameter("@flg", flg)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Hobbies]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertBookbindTranschild(string List_no, string Accession_no, string Status, string Bindtype)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@List_no_1", List_no),
                new SqlParameter("@Accession_no_2", Accession_no),
                new SqlParameter("@Status_3", Status),
                new SqlParameter("@Bindtype_4", Bindtype)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookbindTrans_child_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertproposedSeconded(string Member_code, string PSType, string Act_Name, string Asso_Mem_No, string Council_Mem_No, string Setting_place_court, string file_name, string flag)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Member_code", Member_code),
                new SqlParameter("@PSType", PSType),
                new SqlParameter("@Act_Name", Act_Name),
                new SqlParameter("@Asso_Mem_No", Asso_Mem_No),
                new SqlParameter("@Council_Mem_No", Council_Mem_No),
                new SqlParameter("@Setting_place_court", Setting_place_court),
                new SqlParameter("@file_name", file_name),
                new SqlParameter("@flg", flag)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_proposed_Seconded]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalrefundentrymaster(int PubAgentCode, string DocumentNo, DateTime EntryDate, string Currency, string paymode, string DraftNo, DateTime DraftDate, string CreditNoteNo, DateTime CreditNoteDate, string PayableAt, decimal Amount, string Relation, decimal balance, string remark, string Status, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[16]
            {
                new SqlParameter("@PubAgentCode_1", PubAgentCode),
                new SqlParameter("@DocumentNo_2", DocumentNo),
                new SqlParameter("@EntryDate_3", EntryDate),
                new SqlParameter("@Currency_4", Currency),
                new SqlParameter("@paymode_5", paymode),
                new SqlParameter("@DraftNo_6", DraftNo),
                new SqlParameter("@DraftDate_7", DraftDate),
                new SqlParameter("@CreditNoteNo_8", CreditNoteNo),
                new SqlParameter("@CreditNoteDate_9", CreditNoteDate),
                new SqlParameter("@PayableAt_10", PayableAt),
                new SqlParameter("@Amount_11", Amount),
                new SqlParameter("@Relation_12", Relation),
                new SqlParameter("@balance_13", balance),
                new SqlParameter("@remark_14", remark),
                new SqlParameter("@Status_15", Status),
                new SqlParameter("@userid_16", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourRefEntry_Master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertJourIssRet(string member_id, string tranid, string journal_no, DateTime issretdate, DateTime duedate, string issst, string accno, DateTime entdate, string userid, string ipa, string sess, string titl, string Remark, string Result)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[13]
            {
                new SqlParameter("@MemId", member_id),
                new SqlParameter("@Jno", journal_no),
                new SqlParameter("@IssRetDt", issretdate),
                new SqlParameter("@DueDt", duedate),
                new SqlParameter("@IssSt", issst),
                new SqlParameter("@AccNo", accno),
                new SqlParameter("@EntDt", entdate),
                new SqlParameter("@userid", userid),
                new SqlParameter("@ipa", ipa),
                new SqlParameter("@sess", sess),
                new SqlParameter("@titl", titl),
                new SqlParameter("@Remark", Remark),
                new SqlParameter("@Result", Result)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourIssRet]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircReceiveMaster(string userid, decimal totalfine, string userid1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@totalfine_2", totalfine),
                new SqlParameter("@userid1_3", userid1)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[CircReceiveMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateLoginStatus(string userid, string Lstatus)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@Lstatus_2", Lstatus)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[LoginStatus]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertUpload(string id, string title, string file_url, string Group_Name, byte[] file_url1, string web_opacflg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@title", title),
                new SqlParameter("@file_url", file_url),
                new SqlParameter("@group_Name", Group_Name),
                new SqlParameter("@file_url1", file_url1),
                new SqlParameter("@show_flg", web_opacflg)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_uploads_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateServerPath(int Id, string ServerPath)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@Id_1", Id),
                new SqlParameter("@ServerPath_2", ServerPath)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ServerPath_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertJournalIssue(string Member_Id, string Journal_No, string Volume, string Issue, string Part, DateTime IssueDate, DateTime DueDate, string Status, DateTime publication_date, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Member_Id_1", Member_Id),
                new SqlParameter("@Journal_No_2", Journal_No),
                new SqlParameter("@Volume_3", Volume),
                new SqlParameter("@DocNumber", Issue),
                new SqlParameter("@Part_5", Part),
                new SqlParameter("@IssueDate_6", IssueDate),
                new SqlParameter("@DueDate_7", DueDate),
                new SqlParameter("@Status_8", Status),
                new SqlParameter("@publication_date_9", publication_date),
                new SqlParameter("@userid_10", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Journal_IssueNonA_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertBookImage(int ctrl_no, byte[] CoverPage, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@CoverPage_2", CoverPage),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookImage_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertOverdueReceipt(string EntryId, DateTime EntryDate, string ReceiptNo, DateTime ReceiptDate, string userId, decimal Amount, string userid1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@EntryId_1", EntryId),
                new SqlParameter("@EntryDate_2", EntryDate),
                new SqlParameter("@ReceiptNo_3", ReceiptNo),
                new SqlParameter("@ReceiptDate_4", ReceiptDate),
                new SqlParameter("@userId_5", userId),
                new SqlParameter("@Amount_6", Amount),
                new SqlParameter("@userid1_7", userid1)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OverDueReceipt_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertJourRefEntryChild(string DocumentNo, int Journalid, decimal Jour_amount, string doc_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@media_id_1", DocumentNo),
                new SqlParameter("@media_name_2", Journalid),
                new SqlParameter("@short_name_3,", Jour_amount),
                new SqlParameter("@userid_4", doc_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourRefEntry_Child_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertPaperMarginSetting(int Id, string Name, int mLeft, int mRight, int mTop, int mBottom, string barcodeSlipHeight)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@Name", Name),
                new SqlParameter("@mLeft", mLeft),
                new SqlParameter("@mRight", mRight),
                new SqlParameter("@mTop", mTop),
                new SqlParameter("@mBottom", mBottom),
                new SqlParameter("@barcodeSlipHeight", barcodeSlipHeight)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_paperMargin_Settings]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertClassLoadStatus(string classname, int totalissueddays, int noofbookstobeissued, decimal finperday, int reservedays, int totalissueddays_jour, int noofjournaltobeissued, decimal fineperday_jour, string reservedays_jour, string Status, int ValueLimit, int days_1phase, decimal amt_1phase, int days_2phase, decimal amt_2phase, int days_1phasej, decimal amt_1phasej, int days_2phasej, decimal amt_2phasej, string shortname, int loadingstatus)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[21]
            {
                new SqlParameter("@classname_1", classname),
                new SqlParameter("@totalissueddays_2", totalissueddays),
                new SqlParameter("@noofbookstobeissued_3", noofbookstobeissued),
                new SqlParameter("@finperday_4", finperday),
                new SqlParameter("@reservedays_5", reservedays),
                new SqlParameter("@totalissueddays_jour_6", totalissueddays_jour),
                new SqlParameter("@noofjournaltobeissued_7", noofjournaltobeissued),
                new SqlParameter("@fineperday_jour_8", fineperday_jour),
                new SqlParameter("@reservedays_jour_9", reservedays_jour),
                new SqlParameter("@Status_10", Status),
                new SqlParameter("@ValueLimit_11", ValueLimit),
                new SqlParameter("@days_1phase_12", days_1phase),
                new SqlParameter("@amt_1phase_13", amt_1phase),
                new SqlParameter("@days_2phase_14", days_2phase),
                new SqlParameter("@amt_2phase_15", amt_2phase),
                new SqlParameter("@days_1phasej_16", days_1phasej),
                new SqlParameter("@amt_1phasej_17", amt_1phasej),
                new SqlParameter("@days_2phasej_18", days_2phasej),
                new SqlParameter("@amt_2phasej_19", amt_2phasej),
                new SqlParameter("@shortname_20", shortname),
                new SqlParameter("@loadingstatus_21", loadingstatus)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_classmstloadstatus_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateImageInsert(string userid, byte memberPic, byte memberSign, char status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@userid", userid),
                new SqlParameter("@pic", memberPic),
                new SqlParameter("@sign", memberSign),
                new SqlParameter("@status", status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[imageinsert]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircReservationtransaction(string userid, int ctrlNo, DateTime reservationdate, int queno, string title, int id, string userid1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@ctrlNo_2", ctrlNo),
                new SqlParameter("@reservationdate_3", reservationdate),
                new SqlParameter("@queno_4", queno),
                new SqlParameter("@title_5", title),
                new SqlParameter("@id_6", id),
                new SqlParameter("@userid1_7", userid1)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circreservationTR_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertEDocGroupMASTER(int Service_id, string Service_Name, string AccessionNo, string userid, string GroupCategory)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Service_id_1", Service_id),
                new SqlParameter("@Service_Name_2", Service_Name),
                new SqlParameter("@AccessionNo_3", AccessionNo),
                new SqlParameter("@userid_4", userid),
                new SqlParameter("@GroupCategory", GroupCategory)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EDoc_Group_MASTER_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertDirectArchMoreInfo(int Daid, string Author, string Volume, string IssueNo, DateTime PubDate, string Part, string Edition, string Publisher, int PageNo, int Noofpage, DateTime FromPubdate, DateTime ToPubDate, int SourceType)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[13]
            {
                new SqlParameter("@Daid_1", Daid),
                new SqlParameter("@Author_2", Author),
                new SqlParameter("@Volume_3", Volume),
                new SqlParameter("@IssueNo_4", IssueNo),
                new SqlParameter("@PubDate_5", PubDate),
                new SqlParameter("@Part_6", Part),
                new SqlParameter("@Edition_7", Edition),
                new SqlParameter("@Publisher_8", Publisher),
                new SqlParameter("@PageNo_9", PageNo),
                new SqlParameter("@Noofpage_10", Noofpage),
                new SqlParameter("@FPubDate_11", FromPubdate),
                new SqlParameter("@TPubdate_12", ToPubDate),
                new SqlParameter("@SourceType", SourceType)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_DirectArchMoreInfo_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertCancelIndent(string vendorid, string ordernumber, string indentnumber, int departmentcode, string userid, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@vendorid_1", vendorid),
                new SqlParameter("@ordernumber_2", ordernumber),
                new SqlParameter("@indentnumber_3", indentnumber),
                new SqlParameter("@departmentcode_4", departmentcode),
                new SqlParameter("@userid_5", userid),
                new SqlParameter("@FormId", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CancelIndent_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircAccompanyingItemIssue(string memid, string accno, string media_accno, string status, string userid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@memid_1", memid),
                new SqlParameter("@accno_2", accno),
                new SqlParameter("@media_accno_3", media_accno),
                new SqlParameter("@status_4", status),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_CircAccompanyingItemIssue_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertReminderMaster(string ordernumber, string indentnumber, string remindernumber, DateTime reminderdate, string letternumber, DateTime validitydate, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@ordernumber_1", ordernumber),
                new SqlParameter("@indentnumber_2", indentnumber),
                new SqlParameter("@remindernumber_3", remindernumber),
                new SqlParameter("@reminderdate_4", reminderdate),
                new SqlParameter("@letternumber_5", letternumber),
                new SqlParameter("@validitydate_6", validitydate),
                new SqlParameter("@userid_7", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circreservationTR_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertStockProcess(string taskid, string accssion_no, DateTime sysdate, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@taskid_1", taskid),
                new SqlParameter("@accssion_no_2", accssion_no),
                new SqlParameter("@sysdate_3,", sysdate),
                new SqlParameter("@userid_4", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Stock_process_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertCircularmsgpost(string cid, string circularnumber, string subject, string matter, string postedby, DateTime mesdate, string type, string userid, int msgtypeid, string tomemberid, int msgFrom, int verified, DateTime effectiveFrom, DateTime to_dt)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[14]
            {
                new SqlParameter("@cid_1", cid),
                new SqlParameter("@circularnumber_2", circularnumber),
                new SqlParameter("@subject_3", subject),
                new SqlParameter("@matter_4", matter),
                new SqlParameter("@postedby_5", postedby),
                new SqlParameter("@mesdate_6", mesdate),
                new SqlParameter("@type_7", type),
                new SqlParameter("@userid_8", userid),
                new SqlParameter("@msgtypeid", msgtypeid),
                new SqlParameter("@tomemberid", tomemberid),
                new SqlParameter("@msgFrom", msgFrom),
                new SqlParameter("@verified", verified),
                new SqlParameter("@effectiveFrom", effectiveFrom),
                new SqlParameter("@to_dt", to_dt)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circularmsgpost_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertBinderInvoicetrans(int invoice_id, decimal pay_amt, int noofcopy, string list_no, string typeofbinding, int FormID)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@invoice_id_1", invoice_id),
                new SqlParameter("@pay_amt_2", pay_amt),
                new SqlParameter("@noofcopy_3", noofcopy),
                new SqlParameter("@list_no_4", list_no),
                new SqlParameter("@typeofbinding_5", typeofbinding),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_binder_inv_trans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertNonArrive(string Journal_no, string Volume, string Issue, string Part, string DateofPublication, string Doc_Id, string Letter_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Journal_no_1", Journal_no),
                new SqlParameter("@Volume_2", Volume),
                new SqlParameter("@Issue_3", Issue),
                new SqlParameter("@Part_4", Part),
                new SqlParameter("@DateofPublication_5", DateofPublication),
                new SqlParameter("@Doc_Id_6", Doc_Id),
                new SqlParameter("@Letter_No_7", Letter_No)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NonArrive_Jour_Child_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertUserTypeLavel(int UserTypeId, int MenuId, string Premission)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserTypeId_1", UserTypeId),
                new SqlParameter("@MidMenu_Id_2", MenuId),
                new SqlParameter("@Permission_3", Premission)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypeLavel_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertBarcodePrint(int id, int AccessionNo, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@id_1", id),
                new SqlParameter("@AccessionNo_2", AccessionNo),
                new SqlParameter("@userid_3", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BarcodePrint_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertSMSSettings(string Message, DateTime SendDate, string Sendby, string SendTo, string Status, string PageName)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Message", Message),
                new SqlParameter("@SendDate", SendDate),
                new SqlParameter("@Sendby", Sendby),
                new SqlParameter("@SendTo", SendTo),
                new SqlParameter("@Status", Status),
                new SqlParameter("@PageName", PageName)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SMSMessage]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateDirectInvoiceMaster(int srno, DateTime curr_date, decimal postage, decimal net_amt, decimal disc_amt, decimal disc_percentage, string vedorid, decimal handling_charge, decimal total_amt, decimal rate_followed_by, decimal pay_amt, string invoice_no, string bill_no, DateTime bill_date, string userid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[15]
            {
                new SqlParameter("@srno_1", srno),
                new SqlParameter("@curr_date_2", curr_date),
                new SqlParameter("@postage_3", postage),
                new SqlParameter("@net_amt_4", net_amt),
                new SqlParameter("@disc_amt_5", disc_amt),
                new SqlParameter("@disc_percentage_6", disc_percentage),
                new SqlParameter("@vedorid_7", vedorid),
                new SqlParameter("@handling_charge_8", handling_charge),
                new SqlParameter("@total_amt_9", total_amt),
                new SqlParameter("@rate_followed_by_10", rate_followed_by),
                new SqlParameter("@pay_amt_11", pay_amt),
                new SqlParameter("@invoice_no_12", invoice_no),
                new SqlParameter("@bill_no_13", bill_no),
                new SqlParameter("@bill_date_14", bill_date),
                new SqlParameter("@userid_15", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_classmstloadstatus_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateJournalKeyword(int S_No, string Journal_No, string Keyword, string Status, string ItemType)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@S_No_1", S_No),
                new SqlParameter("@Journal_No_2", Journal_No),
                new SqlParameter("@Keyword_3", Keyword),
                new SqlParameter("@Status_4", Status),
                new SqlParameter("@ItemType_5", ItemType)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_Journal_Keyword_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertBinderInvoice(int invoice_id, int Binder_id, string invoice_no, int invoice_amt, DateTime invoice_date, string Bill_serial_no, string userid, string type, string CancelStatus)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@invoice_id_1", invoice_id),
                new SqlParameter("@Binder_id_2", Binder_id),
                new SqlParameter("@invoice_no_3", invoice_no),
                new SqlParameter("@invoice_amt_4", invoice_amt),
                new SqlParameter("@invoice_date_5", invoice_date),
                new SqlParameter("@Bill_serial_no_6", Bill_serial_no),
                new SqlParameter("@userid_7", userid),
                new SqlParameter("@handling_charge_8", type),
                new SqlParameter("@CancelStatus_9", CancelStatus)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_Journal_Keyword_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable updateinsertsecuritymaster(int Id, DateTime Date, float Amount, DateTime ToDate, string MemberId)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@Date", Date),
                new SqlParameter("@Amount", Amount),
                new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@MemberId", MemberId)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SecurityMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalinvoicemaster(string Invoice_id, string Invoice_No, DateTime Invoice_Date, string Order_Id, string Publisher_Code, string Currency, float Exchange_Rate, float PostageCharge, string Relation, string BillSerial_No, string Status, float Total_Amount, string ref_invoice_no, float Amount_Local, string pay_currency, float credit_amount, string credit_no, int Curr_code, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[20]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@Invoice_No_2", Invoice_No),
                new SqlParameter("@Invoice_Date_3", Invoice_Date),
                new SqlParameter("@Order_Id_4", Order_Id),
                new SqlParameter("@Publisher_Code_5", Publisher_Code),
                new SqlParameter("@Currency_6", Currency),
                new SqlParameter("@Exchange_Rate_7", Exchange_Rate),
                new SqlParameter("@PostageCharge_8", PostageCharge),
                new SqlParameter("@Relation_9", Relation),
                new SqlParameter("@BillSerial_No_10", BillSerial_No),
                new SqlParameter("@Status_11", Status),
                new SqlParameter("@Total_Amount_12", Total_Amount),
                new SqlParameter("@ref_invoice_no_13", ref_invoice_no),
                new SqlParameter("@Amount_Local_14", Amount_Local),
                new SqlParameter("@pay_currency_15", pay_currency),
                new SqlParameter("@credit_amount_16", credit_amount),
                new SqlParameter("@credit_no_17", credit_no),
                null,
                new SqlParameter("@Curr_code_18", Curr_code),
                new SqlParameter("@userid_19", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalInvoice_master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjouracctopmtmaster(int paymentid, float amount, string paymenttype, int vendorid, DateTime paymentdate, string documentno, string documentno1, string Status_S, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@paymentid_1", paymentid),
                new SqlParameter("@amount_2", amount),
                new SqlParameter("@paymenttype_3", paymenttype),
                new SqlParameter("@vendorid_4", vendorid),
                new SqlParameter("@paymentdate_5", paymentdate),
                new SqlParameter("@documentno_6", documentno),
                new SqlParameter("@Relation_7", documentno1),
                new SqlParameter("@Status_S_8", Status_S),
                new SqlParameter("@userid_9", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_jour_acctopmtmaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalmaster(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, string dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[52]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@subscription_no_2", subscription_no),
                new SqlParameter("@issn_3", issn),
                new SqlParameter("@journal_title_4", journal_title),
                new SqlParameter("@title_abbreviation_5", title_abbreviation),
                new SqlParameter("@series_title_6", series_title),
                new SqlParameter("@spine_title_7", spine_title),
                new SqlParameter("@entry_date_8", entry_date),
                new SqlParameter("@start_date_9", start_date),
                new SqlParameter("@expiry_date_10", expiry_date),
                new SqlParameter("@total_volume_11", total_volume),
                new SqlParameter("@issue_per_volume_12", issue_per_volume),
                new SqlParameter("@part_per_issue_13", part_per_issue),
                new SqlParameter("@starting_volume_14", starting_volume),
                new SqlParameter("@ending_volume_15", ending_volume),
                new SqlParameter("@starting_issue_16", starting_issue),
                new SqlParameter("@ending_issue_17", ending_issue),
                new SqlParameter("@starting_part_18", starting_part),
                new SqlParameter("@ending_part_19", ending_part),
                new SqlParameter("@priority_20", priority),
                new SqlParameter("@publisher_21", publisher),
                new SqlParameter("@tran_language_22", tran_language),
                new SqlParameter("@department_23", department),
                new SqlParameter("@sponsor_24", sponsor),
                new SqlParameter("@pigeon_25", pigeon),
                new SqlParameter("@delivery_lag_26", delivery_lag),
                new SqlParameter("@agent_27", agent),
                new SqlParameter("@delivery_mode_28", delivery_mode),
                new SqlParameter("@frequency_29", frequency),
                new SqlParameter("@subscription_status_30", subscription_status),
                new SqlParameter("@Process_Stage_31", Process_Stage),
                new SqlParameter("@Journal_status_32", Journal_status),
                new SqlParameter("@Indexes_33", Indexes),
                new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date),
                new SqlParameter("@Entry_Mode_35", Entry_Mode),
                new SqlParameter("@Journal_Id_36", Journal_Id),
                new SqlParameter("@packagepart_37", packagepart),
                new SqlParameter("@Reason_38", Reason),
                new SqlParameter("@New_Status_39", New_Status),
                new SqlParameter("@New_Reference_40", New_Reference),
                new SqlParameter("@Operation_mode_41", Operation_mode),
                new SqlParameter("@List_No_42", List_No),
                new SqlParameter("@FromYear_43", Fromyear),
                new SqlParameter("@Toyear_44", Toyear),
                new SqlParameter("@publicationDate_45", PublicationDate),
                new SqlParameter("@dateofpublication_46", dateofpublication),
                new SqlParameter("@Mode_Status_47", Mode_Status),
                new SqlParameter("@Effective_From_48", Effective_From),
                new SqlParameter("@Media_type_49", media_Type),
                new SqlParameter("@Bind_Status_50", Bind_Status),
                new SqlParameter("@Url_51", Url),
                new SqlParameter("@userid_52", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjourmarge(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, string dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, string userid, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[53]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@subscription_no_2", subscription_no),
                new SqlParameter("@issn_3", issn),
                new SqlParameter("@journal_title_4", journal_title),
                new SqlParameter("@title_abbreviation_5", title_abbreviation),
                new SqlParameter("@series_title_6", series_title),
                new SqlParameter("@spine_title_7", spine_title),
                new SqlParameter("@entry_date_8", entry_date),
                new SqlParameter("@start_date_9", start_date),
                new SqlParameter("@expiry_date_10", expiry_date),
                new SqlParameter("@total_volume_11", total_volume),
                new SqlParameter("@issue_per_volume_12", issue_per_volume),
                new SqlParameter("@part_per_issue_13", part_per_issue),
                new SqlParameter("@starting_volume_14", starting_volume),
                new SqlParameter("@ending_volume_15", ending_volume),
                new SqlParameter("@starting_issue_16", starting_issue),
                new SqlParameter("@ending_issue_17", ending_issue),
                new SqlParameter("@starting_part_18", starting_part),
                new SqlParameter("@ending_part_19", ending_part),
                new SqlParameter("@priority_20", priority),
                new SqlParameter("@publisher_21", publisher),
                new SqlParameter("@tran_language_22", tran_language),
                new SqlParameter("@department_23", department),
                new SqlParameter("@sponsor_24", sponsor),
                new SqlParameter("@pigeon_25", pigeon),
                new SqlParameter("@delivery_lag_26", delivery_lag),
                new SqlParameter("@agent_27", agent),
                new SqlParameter("@delivery_mode_28", delivery_mode),
                new SqlParameter("@frequency_29", frequency),
                new SqlParameter("@subscription_status_30", subscription_status),
                new SqlParameter("@Process_Stage_31", Process_Stage),
                new SqlParameter("@Journal_status_32", Journal_status),
                new SqlParameter("@Indexes_33", Indexes),
                new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date),
                new SqlParameter("@Entry_Mode_35", Entry_Mode),
                new SqlParameter("@Journal_Id_36", Journal_Id),
                new SqlParameter("@packagepart_37", packagepart),
                new SqlParameter("@Reason_38", Reason),
                new SqlParameter("@New_Status_39", New_Status),
                new SqlParameter("@New_Reference_40", New_Reference),
                new SqlParameter("@Operation_mode_41", Operation_mode),
                new SqlParameter("@List_No_42", List_No),
                new SqlParameter("@FromYear_43", Fromyear),
                new SqlParameter("@Toyear_44", Toyear),
                new SqlParameter("@publicationDate_45", PublicationDate),
                new SqlParameter("@dateofpublication_46", dateofpublication),
                new SqlParameter("@Mode_Status_47", Mode_Status),
                new SqlParameter("@Effective_From_48", Effective_From),
                new SqlParameter("@Media_type_49", media_Type),
                new SqlParameter("@Bind_Status_50", Bind_Status),
                new SqlParameter("@Url_51", Url),
                new SqlParameter("@userid_52", userid),
                new SqlParameter("@Type_53", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournaldetailmaster(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, DateTime dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, int locid, string userid, string Type, int FormID, int UType)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[56]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@subscription_no_2", subscription_no),
                new SqlParameter("@issn_3", issn),
                new SqlParameter("@journal_title_4", journal_title),
                new SqlParameter("@title_abbreviation_5", title_abbreviation),
                new SqlParameter("@series_title_6", series_title),
                new SqlParameter("@spine_title_7", spine_title),
                new SqlParameter("@entry_date_8", entry_date),
                new SqlParameter("@start_date_9", start_date),
                new SqlParameter("@expiry_date_10", expiry_date),
                new SqlParameter("@total_volume_11", total_volume),
                new SqlParameter("@issue_per_volume_12", issue_per_volume),
                new SqlParameter("@part_per_issue_13", part_per_issue),
                new SqlParameter("@starting_volume_14", starting_volume),
                new SqlParameter("@ending_volume_15", ending_volume),
                new SqlParameter("@starting_issue_16", starting_issue),
                new SqlParameter("@ending_issue_17", ending_issue),
                new SqlParameter("@starting_part_18", starting_part),
                new SqlParameter("@ending_part_19", ending_part),
                new SqlParameter("@priority_20", priority),
                new SqlParameter("@publisher_21", publisher),
                new SqlParameter("@tran_language_22", tran_language),
                new SqlParameter("@department_23", department),
                new SqlParameter("@sponsor_24", sponsor),
                new SqlParameter("@pigeon_25", pigeon),
                new SqlParameter("@delivery_lag_26", delivery_lag),
                new SqlParameter("@agent_27", agent),
                new SqlParameter("@delivery_mode_28", delivery_mode),
                new SqlParameter("@frequency_29", frequency),
                new SqlParameter("@subscription_status_30", subscription_status),
                new SqlParameter("@Process_Stage_31", Process_Stage),
                new SqlParameter("@Journal_status_32", Journal_status),
                new SqlParameter("@Indexes_33", Indexes),
                new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date),
                new SqlParameter("@Entry_Mode_35", Entry_Mode),
                new SqlParameter("@Journal_Id_36", Journal_Id),
                new SqlParameter("@packagepart_37", packagepart),
                new SqlParameter("@Reason_38", Reason),
                new SqlParameter("@New_Status_39", New_Status),
                new SqlParameter("@New_Reference_40", New_Reference),
                new SqlParameter("@Operation_mode_41", Operation_mode),
                new SqlParameter("@List_No_42", List_No),
                new SqlParameter("@FromYear_43", Fromyear),
                new SqlParameter("@Toyear_44", Toyear),
                new SqlParameter("@publicationDate_45", PublicationDate),
                new SqlParameter("@dateofpublication_46", dateofpublication),
                new SqlParameter("@Mode_Status_47", Mode_Status),
                new SqlParameter("@Effective_From_48", Effective_From),
                new SqlParameter("@Media_type_49", media_Type),
                new SqlParameter("@Bind_Status_50", Bind_Status),
                new SqlParameter("@Url_51", Url),
                new SqlParameter("@loc_id_51_2", locid),
                new SqlParameter("@userid_52", userid),
                new SqlParameter("@Type_53", Type),
                new SqlParameter("@FormID_54", FormID),
                new SqlParameter("@Utype_55", UType)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_insert_JournalMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalsubscription(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, string dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, double Loc_id, string userid, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[53]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@subscription_no_2", subscription_no),
                new SqlParameter("@issn_3", issn),
                new SqlParameter("@journal_title_4", journal_title),
                new SqlParameter("@title_abbreviation_5", title_abbreviation),
                new SqlParameter("@series_title_6", series_title),
                new SqlParameter("@spine_title_7", spine_title),
                new SqlParameter("@entry_date_8", entry_date),
                new SqlParameter("@start_date_9", start_date),
                new SqlParameter("@expiry_date_10", expiry_date),
                new SqlParameter("@total_volume_11", total_volume),
                new SqlParameter("@issue_per_volume_12", issue_per_volume),
                new SqlParameter("@part_per_issue_13", part_per_issue),
                new SqlParameter("@starting_volume_14", starting_volume),
                new SqlParameter("@ending_volume_15", ending_volume),
                new SqlParameter("@starting_issue_16", starting_issue),
                new SqlParameter("@ending_issue_17", ending_issue),
                new SqlParameter("@starting_part_18", starting_part),
                new SqlParameter("@ending_part_19", ending_part),
                new SqlParameter("@priority_20", priority),
                new SqlParameter("@publisher_21", publisher),
                new SqlParameter("@tran_language_22", tran_language),
                new SqlParameter("@department_23", department),
                new SqlParameter("@sponsor_24", sponsor),
                new SqlParameter("@pigeon_25", pigeon),
                new SqlParameter("@delivery_lag_26", delivery_lag),
                new SqlParameter("@agent_27", agent),
                new SqlParameter("@frequency_29", frequency),
                new SqlParameter("@subscription_status_30", subscription_status),
                new SqlParameter("@Process_Stage_31", Process_Stage),
                new SqlParameter("@Journal_status_32", Journal_status),
                new SqlParameter("@Indexes_33", Indexes),
                new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date),
                new SqlParameter("@Entry_Mode_35", Entry_Mode),
                new SqlParameter("@Journal_Id_36", Journal_Id),
                new SqlParameter("@packagepart_37", packagepart),
                new SqlParameter("@Reason_38", Reason),
                new SqlParameter("@New_Status_39", New_Status),
                new SqlParameter("@New_Reference_40", New_Reference),
                new SqlParameter("@Operation_mode_41", Operation_mode),
                new SqlParameter("@List_No_42", List_No),
                new SqlParameter("@FromYear_43", Fromyear),
                new SqlParameter("@Toyear_44", Toyear),
                new SqlParameter("@publicationDate_45", PublicationDate),
                new SqlParameter("@dateofpublication_46", dateofpublication),
                new SqlParameter("@Mode_Status_47", Mode_Status),
                new SqlParameter("@Effective_From_48", Effective_From),
                new SqlParameter("@Media_type_49", media_Type),
                new SqlParameter("@Bind_Status_50", Bind_Status),
                new SqlParameter("@Url_51", Url),
                new SqlParameter("@loc_id_51_2", Loc_id),
                new SqlParameter("@userid_52", userid),
                new SqlParameter("@Type_53", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertpostmessage(string cid, string circularNumber, string uploadfile, string uploadpath, string postedby)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@cid_1", cid),
                new SqlParameter("@circularNumber_2", circularNumber),
                new SqlParameter("@uploadfile_3", uploadfile),
                new SqlParameter("@uploadpath_4", uploadpath),
                new SqlParameter("@postedby_5", postedby)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_postMessages_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertnewspapermaster(string Newspaper_id, string NewsPaperTitle, DateTime start_date, DateTime PublicationDate, string frequency, int noofcopies, string vendorid, string status, string userid, float price, float totalamount, int T_id, string Weakend_dys, float Weakend_Prc)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[14]
            {
                new SqlParameter("@Newspaper_id_1", Newspaper_id),
                new SqlParameter("@NewsPaperTitle_2", NewsPaperTitle),
                new SqlParameter("@start_date_3", start_date),
                new SqlParameter("@PublicationDate_4", PublicationDate),
                new SqlParameter("@frequency_5", frequency),
                new SqlParameter("@noofcopies_6", noofcopies),
                new SqlParameter("@vendorid_7", vendorid),
                new SqlParameter("@status_8", status),
                new SqlParameter("@userid_9", userid),
                new SqlParameter("@price_10", price),
                new SqlParameter("@totalamount_11", totalamount),
                new SqlParameter("@T_id", T_id),
                new SqlParameter("@Weakend_dys_13", Weakend_dys),
                new SqlParameter("@Weakend_Prc_14", Weakend_Prc)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertdirectcatelogininfo(int Dsrno, int mediatype, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int noofcopies, float price, string publisherid, DateTime recordingdate, string seriesname, string form, string keywords, DateTime docDate, float Fprice, string FcurrencyCode, string subtitle, string part, float specialprice, int dept, string yearofPublication, int Language_Id, float exchange_rate, int no_of_pages, string page_size, string vendor_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[38]
            {
                new SqlParameter("@Dsrno_1", Dsrno),
                new SqlParameter("@mediatype_2", mediatype),
                new SqlParameter("@title_3", title),
                new SqlParameter("@authortype_4", authortype),
                new SqlParameter("@firstname1_5", firstname1),
                new SqlParameter("@middlename1_6", middlename1),
                new SqlParameter("@lastname1_7", lastname1),
                new SqlParameter("@firstname2_8", firstname2),
                new SqlParameter("@middlename2_9", middlename2),
                new SqlParameter("@lastname2_10", lastname2),
                new SqlParameter("@firstname3_11", firstname3),
                new SqlParameter("@middlename3_12", middlename3),
                new SqlParameter("@lastname3_13", lastname3),
                new SqlParameter("@edition_14", edition),
                new SqlParameter("@yearofedition_15", yearofedition),
                new SqlParameter("@volumeno_16", volumeno),
                new SqlParameter("@isbn_17", isbn),
                new SqlParameter("@category_18", category),
                new SqlParameter("@noofcopies_19", noofcopies),
                new SqlParameter("@price_20", price),
                new SqlParameter("@publisherid_21", publisherid),
                new SqlParameter("@recordingdate_22", recordingdate),
                new SqlParameter("@seriesname_23", seriesname),
                new SqlParameter("@form_24", form),
                new SqlParameter("@keywords_25", keywords),
                new SqlParameter("@docDate_26", docDate),
                new SqlParameter("@Fprice_27", Fprice),
                new SqlParameter("@FcurrencyCode_28", FcurrencyCode),
                new SqlParameter("@subtitle_29", subtitle),
                new SqlParameter("@part_30", part),
                new SqlParameter("@specialprice_31", specialprice),
                new SqlParameter("@dept_32", dept),
                new SqlParameter("@yearofPublication_33", yearofPublication),
                new SqlParameter("@Language_Id_34", Language_Id),
                new SqlParameter(" @exchange_rate_35", exchange_rate),
                new SqlParameter("@no_of_pages_36", no_of_pages),
                new SqlParameter("@page_size_37", page_size),
                new SqlParameter("@vendor_id_38", vendor_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DirectCateloginfo_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertcircusermanagement(string userid, string usercode, string firstname, string middlename, string lastname, int departmentcode, DateTime validupto, string status, string remarks, int issuebookstatus, string email1, string email2, string gender, string doj, string phone1, string phone2, string memberpic, string membername, string classname, string fathername, string dob, string catid, string adhaarno, string programid, string joinyear, string subjects, string userid1, string yearsem, string section, string bloodgrp, string session, string affiliation, string membersign, string printingstatus, string imagestatus, string studentthumb, string isthumb, string thumbtemplate1, string thumbtemplate2, string studentthumb2, string isthumb2, string thumbtemplate3, string thumbtemplate4, string mothername, string panNo, string photoname, string signName, string latitude, string longitude, string searchtext)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[49]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@usercode_26", usercode),
                new SqlParameter("@firstname_2", firstname),
                new SqlParameter("@middlename_3", middlename),
                new SqlParameter("@lastname_4", lastname),
                new SqlParameter("@departmentcode_6", departmentcode),
                new SqlParameter("@validupto_7", validupto),
                new SqlParameter("@status_8", status),
                new SqlParameter("@remarks_9", remarks),
                new SqlParameter("@issuedbookstatus", issuebookstatus),
                new SqlParameter("@email1_11", email1),
                new SqlParameter("@email2_12", email2),
                new SqlParameter("@gender_13", gender),
                new SqlParameter("@doj_14", doj),
                new SqlParameter("@phone1_15", phone1),
                new SqlParameter("@phone2_16", phone2),
                new SqlParameter("@memberpic_17", memberpic),
                new SqlParameter("@classname_18", classname),
                new SqlParameter("@Fathername_19", fathername),
                new SqlParameter("@Dob_20", dob),
                new SqlParameter("@cat_id_21", catid),
                new SqlParameter("@AadharNo_211", adhaarno),
                new SqlParameter("@program_id_22", programid),
                new SqlParameter("@Joinyear_23", joinyear),
                new SqlParameter("@subjects_24", subjects),
                new SqlParameter("@userid1_25", userid),
                new SqlParameter("@YearSem_26", yearsem),
                new SqlParameter("@Section_27", section),
                new SqlParameter("@BloodGrp_28", bloodgrp),
                new SqlParameter("@Session_29", session),
                new SqlParameter("@affiliation_30", affiliation),
                new SqlParameter("@memberSign", membersign),
                new SqlParameter("@printing_status", printingstatus),
                new SqlParameter("@image_status", imagestatus),
                new SqlParameter("@StudentThumb", studentthumb),
                new SqlParameter("@IsThumb", isthumb),
                new SqlParameter("@ThumbTemplate1", thumbtemplate1),
                new SqlParameter("@ThumbTemplate2", thumbtemplate2),
                new SqlParameter("@StudentThumb2", studentthumb2),
                new SqlParameter("@IsThumb2", isthumb2),
                new SqlParameter("@ThumbTemplate3", thumbtemplate3),
                new SqlParameter("@ThumbTemplate4", thumbtemplate4),
                new SqlParameter("@mothername", mothername),
                new SqlParameter("@pan_no", panNo),
                new SqlParameter("@photoname", photoname),
                new SqlParameter("@Signname", signName),
                new SqlParameter("@Latitude", latitude),
                new SqlParameter("@Longitude", longitude),
                new SqlParameter("@SearchText", searchtext)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircUserManagement_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateEditTableMult(string accessionnumber, string booktitle, int Ctrl_no, int accessionid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@AccNo_1", accessionnumber),
                new SqlParameter("@Title_2", booktitle),
                new SqlParameter("@AccID_3", Ctrl_no),
                new SqlParameter("@Crtl_no_4", accessionid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[UpDate_EditTableMult_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateEditTable(int Ctrl_no, string accessionnumber, string classnumber, string booknumber, string booktitle, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string subject1, string subject2, string subject3, int accessionid)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@Ctrl_no", Ctrl_no),
                new SqlParameter("@AccNo_1", accessionnumber),
                new SqlParameter("@ClassNo_2", classnumber),
                new SqlParameter("@BookNo_3", booknumber),
                new SqlParameter("@Title_4", booktitle),
                new SqlParameter("@AF1_5", firstname1),
                new SqlParameter("@AM2_6", middlename1),
                new SqlParameter("@AL3_7", lastname1),
                new SqlParameter("@AF2_8", firstname2),
                new SqlParameter("@AM2_9", middlename2),
                new SqlParameter("@AL2_10", lastname2),
                new SqlParameter("@AF3_11", firstname3),
                new SqlParameter("@AM3_12", middlename3),
                new SqlParameter("@AL3_13", lastname3),
                new SqlParameter("@Sub1_14", subject1),
                new SqlParameter("@Sub2_15", subject2),
                new SqlParameter("@Sub3_16", subject3),
                new SqlParameter("@AccID_17", accessionid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[UpDate_EditTable_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteEditTableMult(string Accessionnumber)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Accession_1", Accessionnumber)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Delete_EdittableMul_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteEditTable(int Ctrl_no)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Ctrl_no", Ctrl_no)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Delete_Edittable_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable InsertMarcext(int ctrl_no)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Ctrl_no", ctrl_no)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[MARCEXT_impData]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable InsertInsTest(int c1, string c2)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@c1", c1),
                new SqlParameter("@c2", c2)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insTest]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable DeleteAdduserSubject(string property)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@vchProperty", property)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[dt_adduserobject_vcs]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Deletegetpropertiesbyid(int objectid, string property)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@id", objectid),
                new SqlParameter("@property", property)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[dt_getpropertiesbyid_vcs]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertCurrent_awairnes(string member_id, string item, string subject, string frequency, DateTime from_date, DateTime to_date, DateTime Sent_upto, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@member_id_1", member_id),
                new SqlParameter("@item_2", item),
                new SqlParameter("@subject_3", subject),
                new SqlParameter("@frequency_4", frequency),
                new SqlParameter("@from_date_5", from_date),
                new SqlParameter("@to_date_6", to_date),
                new SqlParameter("@Sent_upto_7", userid),
                new SqlParameter("@userid_8", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Current_awairnes_ser_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertLibraryService(int invoice_Id, string invoice_no, DateTime invoice_date, string library, string member, decimal service_tax, decimal cess, DateTime duedate, decimal Actual_amt, decimal total_amt, decimal postage, decimal tax, string userid, string Pmt_Type, decimal PaidAmt, decimal BallanceAmt, string DD_ChkNo, DateTime DD_ChkDate, decimal DD_charge, string Bank)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[20]
            {
                new SqlParameter("@invoice_Id_1", invoice_Id),
                new SqlParameter("@invoice_no_2", invoice_no),
                new SqlParameter("@invoice_date_3", invoice_date),
                new SqlParameter("@library_4", library),
                new SqlParameter("@member_5", member),
                new SqlParameter("@service_tax_6", service_tax),
                new SqlParameter("@cess_7", cess),
                new SqlParameter("@duedate_8", duedate),
                new SqlParameter("@Actual_amt_9", Actual_amt),
                new SqlParameter("@total_amt_10", total_amt),
                new SqlParameter("@postage_11", postage),
                new SqlParameter("@tax_12", tax),
                new SqlParameter("@userid_13", userid),
                new SqlParameter("@Pmt_Type_14", Pmt_Type),
                new SqlParameter("@PaidAmt_15", PaidAmt),
                new SqlParameter("@BallanceAmt_16", BallanceAmt),
                new SqlParameter("@DD_ChkNo_17", DD_ChkNo),
                new SqlParameter("@DD_ChkDate_18", DD_ChkDate),
                new SqlParameter("@DD_charge_19", DD_charge),
                new SqlParameter("@Bank_20", Bank)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_library_servicesMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertStockEntryMaster(string docno, DateTime stockdate, string classnumber_from, string classnumber_to, int total_missing, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@docno_1", docno),
                new SqlParameter("@stockdate_2", stockdate),
                new SqlParameter("@classnumber_from_3", classnumber_from),
                new SqlParameter("@classnumber_to_4", classnumber_to),
                new SqlParameter("@total_missing_5", total_missing),
                new SqlParameter("@userid_6", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Stock_Entry_Master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertOnlineJournal(string Journal_Title, string Url, DateTime From_date, DateTime To_Date, string Jurl_Id, int Dept, string userid, string Department, string pay_Mode)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@Journal_Title_1", Journal_Title),
                new SqlParameter("@Url_2", Url),
                new SqlParameter("@From_date_3", From_date),
                new SqlParameter("@To_Date_4", To_Date),
                new SqlParameter("@Jurl_Id_5", Jurl_Id),
                new SqlParameter("@Dept_6", Dept),
                new SqlParameter("@userid_7", userid),
                new SqlParameter("@Department_8", Department),
                new SqlParameter("@pay_Mode_9", pay_Mode)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OnlineJournal_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertJournalInvoiceChild(string Invoice_id, string Journal_Id, decimal Discount, decimal Amount, decimal Balance, decimal Rs_E, string Jour_Status, decimal print_Amt, decimal online_Amt, decimal p_o_Amt, string currency, decimal exchange_rate, string postage)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[13]
            {
                new SqlParameter("@Invoice_id_1", Invoice_id),
                new SqlParameter("@Journal_Id_2", Journal_Id),
                new SqlParameter("@Discount_3", Discount),
                new SqlParameter("@Amount_4", Amount),
                new SqlParameter("@Balance_5", Balance),
                new SqlParameter("@Rs_E_6", Rs_E),
                new SqlParameter("@Jour_Status_7", Jour_Status),
                new SqlParameter("@print_Amt_8", print_Amt),
                new SqlParameter("@online_Amt_9", online_Amt),
                new SqlParameter("@p_o_Amt_10", p_o_Amt),
                new SqlParameter("@currency", currency),
                new SqlParameter("@exchange_rate", exchange_rate),
                new SqlParameter("@postage", postage)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_JournalInvoice_Child_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertCircClassMaster(string classname, int totalissueddays, int noofbookstobeissued, decimal finperday, int reservedays, int totalissueddays_jour, int noofjournaltobeissued, decimal fineperday_jour, int reservedays_jour, string Status, string canRequest, int ValueLimit, int days_1phase, decimal amt_1phase, int days_2phase, decimal amt_2phase, int days_1phasej, decimal amt_1phasej, int days_2phasej, decimal amt_2phasej, string shortname, string userid, string policystatus, string MembershipType, string Security)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[25]
            {
                new SqlParameter("@classname_1", classname),
                new SqlParameter("@totalissueddays_2", totalissueddays),
                new SqlParameter("@noofbookstobeissued_3", noofbookstobeissued),
                new SqlParameter("@finperday_4", finperday),
                new SqlParameter("@reservedays_5", reservedays),
                new SqlParameter("@totalissueddays_jour_6", totalissueddays_jour),
                new SqlParameter("@noofjournaltobeissued_7", noofjournaltobeissued),
                new SqlParameter("@fineperday_jour_8", fineperday_jour),
                new SqlParameter("@reservedays_jour_9", reservedays_jour),
                new SqlParameter("@Status_10", Status),
                new SqlParameter("@canRequest_11", canRequest),
                new SqlParameter("@ValueLimit_12", ValueLimit),
                new SqlParameter("@days_1phase_13", days_1phase),
                new SqlParameter("@amt_1phase_14", amt_1phase),
                new SqlParameter("@days_2phase_15", days_2phase),
                new SqlParameter("@amt_2phase_16", amt_2phasej),
                new SqlParameter("@days_1phasej_17", days_1phasej),
                new SqlParameter("@amt_1phasej_18", amt_1phasej),
                new SqlParameter("@days_2phasej_19", days_2phasej),
                new SqlParameter("@amt_2phasej_20", amt_2phasej),
                new SqlParameter("@shortname_21", shortname),
                new SqlParameter("@userid_22", userid),
                new SqlParameter("@loadingstatus_23", policystatus),
                new SqlParameter("@loadingstatus_24", MembershipType),
                new SqlParameter("@loadingstatus_25", Security)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_CircClassMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable SpHelpDiagramDefnition(string name)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@diagramname", name)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_helpdiagramdefinition]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Sprenamedigram(string name)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@diagramname", name),
                new SqlParameter("@new_diagramname", name)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_renamediagram]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Spalterdiagram(string name, int version, byte defintion)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@diagramname", name),
                new SqlParameter("@version", version),
                new SqlParameter("@definition", defintion)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_alterdiagram]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Spdropdigram(string name)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@diagramname", name)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_dropdiagram]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Sphelpdigrams(string name)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@diagramname", name)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_helpdiagrams]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable updateinsertjournalarrival(string journal_no, string exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, string publication_Dated, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDate, int FormID, int Copy_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[19];
            array[0] = new SqlParameter("@journal_no_1", journal_no);
            array[1] = new SqlParameter("@exp_date_2", exp_date);
            array[2] = new SqlParameter("@volume_3", volume);
            array[3] = new SqlParameter("@issues_4", issues);
            array[4] = new SqlParameter("@parts_5", parts);
            array[5] = new SqlParameter("@indexes_6", indexes);
            array[6] = new SqlParameter("@Status_7", Status);
            array[7] = new SqlParameter("@Remarks_8", Remarks);
            array[8] = new SqlParameter("@doc_id_9", doc_id);
            array[9] = new SqlParameter("@issue_type_10", issue_type);
            array[10] = new SqlParameter("@arr_date_11", arr_date);
            array[11] = new SqlParameter("@arr_year_12", arr_year);
            array[12] = new SqlParameter("@publication_Date_13", publicationDate);
            array[13] = new SqlParameter("@ISSNNO_14", ISSNNO);
            array[14] = new SqlParameter("@Media_Print_15", Media_Print);
            array[15] = new SqlParameter("@Media_Online_16", Media_Online);
            array[16] = new SqlParameter("@PublicationDate_17", publicationDate);
            array[18] = new SqlParameter("@FormID", FormID);
            array[17] = new SqlParameter("@Copy_No", Copy_No);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjourarrival(string journal_no, string exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, string publication_Dated, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDate, int Copy_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@exp_date_2", exp_date),
                new SqlParameter("@volume_3", volume),
                new SqlParameter("@issues_4", issues),
                new SqlParameter("@parts_5", parts),
                new SqlParameter("@indexes_6", indexes),
                new SqlParameter("@Status_7", Status),
                new SqlParameter("@Remarks_8", Remarks),
                new SqlParameter("@doc_id_9", doc_id),
                new SqlParameter("@issue_type_10", issue_type),
                new SqlParameter("@arr_date_11", arr_date),
                new SqlParameter("@arr_year_12", arr_year),
                new SqlParameter("@publication_Date_13", publication_Dated),
                new SqlParameter("@ISSNNO_14", ISSNNO),
                new SqlParameter("@Media_Print_15", Media_Print),
                new SqlParameter("@Media_Online_16", Media_Online),
                new SqlParameter("@PublicationDate_17", publicationDate),
                new SqlParameter("@Copy_No", Copy_No)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertproformainvoice(string journal_no, string exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, DateTime publication_Date, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDate)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[17]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@exp_date_2", exp_date),
                new SqlParameter("@volume_3", volume),
                new SqlParameter("@issues_4", issues),
                new SqlParameter("@parts_5", parts),
                new SqlParameter("@indexes_6", indexes),
                new SqlParameter("@Status_7", Status),
                new SqlParameter("@Remarks_8", Remarks),
                new SqlParameter("@doc_id_9", doc_id),
                new SqlParameter("@issue_type_10", issue_type),
                new SqlParameter("@arr_date_11", arr_date),
                new SqlParameter("@arr_year_12", arr_year),
                new SqlParameter("@publication_Date_13", publicationDate),
                new SqlParameter("@ISSNNO_14", ISSNNO),
                new SqlParameter("@Media_Print_15", Media_Print),
                new SqlParameter("@Media_Online_16", Media_Online),
                new SqlParameter("@PublicationDate_17", publicationDate)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertBindTransactionMst(string List_No, DateTime List_Date, string BindType, string Binder_id, DateTime Exp_Arrival_Date, string Type, int noofcopy, string userid, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@List_No_1", List_No),
                new SqlParameter("@List_Date_2", List_Date),
                new SqlParameter("@BindType_3", BindType),
                new SqlParameter("@Binder_id_4", Binder_id),
                new SqlParameter("@Exp_Arrival_Date_5", Exp_Arrival_Date),
                new SqlParameter("@Type_6", Type),
                new SqlParameter("@noofcopy_7", noofcopy),
                new SqlParameter("@userid_8", userid),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BindTransaction_Mst_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertOPACINDENT(int mediatype, string requestercode, int departmentcode, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, string coursenumber, string publisherid, string seriesname, string yearofPublication, string IndentId, string Vpart, string subtitle, int Language_Id, string indentnumber, int noofcopy)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[28]
            {
                new SqlParameter("@mediatype_1", mediatype),
                new SqlParameter("@requestercode_2", requestercode),
                new SqlParameter("@departmentcode_3", departmentcode),
                new SqlParameter("@title_4", title),
                new SqlParameter("@authortype_5", authortype),
                new SqlParameter("@firstname1_6", firstname1),
                new SqlParameter("@middlename1_7", middlename1),
                new SqlParameter("@lastname1_8", lastname1),
                new SqlParameter("@firstname2_9", firstname2),
                new SqlParameter("@middlename2_10", middlename2),
                new SqlParameter("@lastname2_11", lastname3),
                new SqlParameter("@firstname3_12", firstname3),
                new SqlParameter("@middlename3_13", middlename3),
                new SqlParameter("@lastname3_14", lastname3),
                new SqlParameter("@edition_15", edition),
                new SqlParameter("@yearofedition_16", yearofedition),
                new SqlParameter("@volumeno_17", volumeno),
                new SqlParameter("@isbn_18", isbn),
                new SqlParameter("@coursenumber_19", coursenumber),
                new SqlParameter("@publisherid_20", publisherid),
                new SqlParameter("@seriesname_21", seriesname),
                new SqlParameter("@yearofPublication_22", yearofPublication),
                new SqlParameter("@IndentId_23", IndentId),
                new SqlParameter("@Vpart_24", Vpart),
                new SqlParameter("@subtitle_26", subtitle),
                new SqlParameter("@Language_Id_27", Language_Id),
                new SqlParameter("@indentnumber_28", indentnumber),
                new SqlParameter("@noofcopy_29", noofcopy)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OPACINDENT_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertordermaster(string ordernumber, DateTime exparivaldateapproval, DateTime exparivaldatenonapproval, string indentnumber, DateTime orderdate, string letternumber, DateTime letterdate, int cancelorder, int itemnumber, int departmentcode, decimal orderamount, string vendorid, int identityofordernumber, int order_check_code, string userid, string IpAddress)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[16]
            {
                new SqlParameter("@ordernumber_1", ordernumber),
                new SqlParameter("@exparivaldateapproval_2", exparivaldateapproval),
                new SqlParameter("@exparivaldatenonapproval_3", exparivaldatenonapproval),
                new SqlParameter("@indentnumber_4", indentnumber),
                new SqlParameter("@orderdate_5", orderdate),
                new SqlParameter("@letternumber_6", letternumber),
                new SqlParameter("@letterdate_7", letterdate),
                new SqlParameter("@cancelorder_8", cancelorder),
                new SqlParameter("@itemnumber_9", itemnumber),
                new SqlParameter("@departmentcode_10", departmentcode),
                new SqlParameter("@orderamount_11", orderamount),
                new SqlParameter("@vendorid_12", vendorid),
                new SqlParameter("@identityofordernumber_13", identityofordernumber),
                new SqlParameter("@order_check_code_14", order_check_code),
                new SqlParameter("@user_id", userid),
                new SqlParameter("@IpAddress", IpAddress)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ordermaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertletteracctoPmtTrans(int PaymentID, string InvoiceID, decimal InvoiceAmount)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@PaymentID_1", PaymentID),
                new SqlParameter("@InvoiceID_2", InvoiceID),
                new SqlParameter("@InvoiceAmount_3", InvoiceAmount)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_letter_acctoPmtTrans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertinvoicemaster2(string invoicenumber, DateTime invoicedate, int invoiceid, decimal postage, decimal netamount, decimal discountamount, decimal discountpercentage, string vendorid, string billserialno, decimal handlingcharge, string payCurrency, decimal payAmount, string reportingtypeofinvoice, decimal total_amt, string userid, string IndentNumber, DateTime IndentDate)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[17]
            {
                new SqlParameter("@invoicenumber_1", invoicenumber),
                new SqlParameter("@invoicedate_2", invoicedate),
                new SqlParameter("@invoiceid_3", invoiceid),
                new SqlParameter("@postage_4", postage),
                new SqlParameter("@netamount_5", netamount),
                new SqlParameter("@discountamount_6", discountamount),
                new SqlParameter("@discountpercentage_7", discountpercentage),
                new SqlParameter("@vendorid_8", vendorid),
                new SqlParameter("@billserialno_9", billserialno),
                new SqlParameter("@handlingcharge_10", handlingcharge),
                new SqlParameter("@payCurrency_11", payCurrency),
                new SqlParameter("@payAmount_12", payAmount),
                new SqlParameter("@typeofinvoice_13", reportingtypeofinvoice),
                new SqlParameter("@total_amt_14", total_amt),
                new SqlParameter("@user_id_15", userid),
                new SqlParameter("@indent_number_16", IndentNumber),
                new SqlParameter("@indent_date_17", IndentDate)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_invoicemaster_2]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalaccessioning(string accession_no, string part_no, string journal_no, string volume, string fromIssue, string toIssue, string Lack_no, string accession_id, DateTime date_of_accessioning, string issue_status, string curr_year, string From_date, DateTime To_date, string userid, int Issue_No, int Copy_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[16]
            {
                new SqlParameter("@accession_no_1", accession_no),
                new SqlParameter("@part_no_2", part_no),
                new SqlParameter("@journal_no_3", journal_no),
                new SqlParameter("@volume_4", volume),
                new SqlParameter("@fromIssue_5", fromIssue),
                new SqlParameter("@toIssue_6", toIssue),
                new SqlParameter("@Lack_no_7", Lack_no),
                new SqlParameter("@accession_id_8", accession_id),
                new SqlParameter("@date_of_accessioning_9", date_of_accessioning),
                new SqlParameter("@issue_status_10", issue_status),
                new SqlParameter("@curr_year_11", curr_year),
                new SqlParameter("@From_date_12", From_date),
                new SqlParameter("@To_date_13", To_date),
                new SqlParameter("@userid_14", userid),
                new SqlParameter("@Issue_No", Issue_No),
                new SqlParameter("@Copy_No", Copy_No)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_accessioning_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertNewsPaperTransaction(string Id, DateTime ArrivalDate, int ActualNoofcopies, decimal ActualPricePerCopy, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id_1", Id),
                new SqlParameter("@ArrivalDate_2", ArrivalDate),
                new SqlParameter("@ActualNoofcopies_3", ActualNoofcopies),
                new SqlParameter("@ActualPricePerCopy_4", ActualPricePerCopy),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperTransaction_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertModifiedOrderMaster(string ordernumber, string vendorid, string indentnumber, string title, int noofcopies, decimal OrderredPricePerCopy, decimal ActualPricePerCopy, string Status, decimal srno, DateTime dateofarrival, decimal price, decimal bankrate, decimal gocrate, decimal discount, string docno, int gift_arrival, string accessioned, string GRN, DateTime GRD, string proof_price, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[21]
            {
                new SqlParameter("@ordernumber_1", ordernumber),
                new SqlParameter("@vendorid_2", vendorid),
                new SqlParameter("@indentnumber_3", indentnumber),
                new SqlParameter("@title_4", title),
                new SqlParameter("@noofcopies_5", noofcopies),
                new SqlParameter("@OrderredPricePerCopy_6", OrderredPricePerCopy),
                new SqlParameter("@ActualPricePerCopy_7", ActualPricePerCopy),
                new SqlParameter("@Status_8", Status),
                new SqlParameter("@srno_9", srno),
                new SqlParameter("@dateofarrival_10", dateofarrival),
                new SqlParameter("@price_11", price),
                new SqlParameter("@bankrate_12", bankrate),
                new SqlParameter("@gocrate_13", gocrate),
                new SqlParameter("@discount_14", discount),
                new SqlParameter("@docno_15", docno),
                new SqlParameter("@gift_arrival_16", gift_arrival),
                new SqlParameter("@accessioned_17", accessioned),
                new SqlParameter("@GRN_18", GRN),
                new SqlParameter("@GRD_19", GRD),
                new SqlParameter("@proof_price_20", proof_price),
                new SqlParameter("@user_id", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ModifiedOrderMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertExistingJournalPayment(string Journal_No, string Journal_Id, string Invoice_No, DateTime Invoice_Date, int Draft_No, DateTime Draft_date, decimal Journal_Amount, string currency)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Journal_No_1", Journal_No),
                new SqlParameter("@Journal_Id_2", Journal_Id),
                new SqlParameter("@Invoice_No_3", Invoice_No),
                new SqlParameter("@Invoice_Date_4", Invoice_Date),
                new SqlParameter("@Draft_No_5", Draft_No),
                new SqlParameter("@Draft_date_6", Draft_date),
                new SqlParameter("@Journal_Amount_7", Journal_Amount),
                new SqlParameter("@currency_8", currency)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperTransaction_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalRecTrans(string userid, string accno, DateTime receivingdate, decimal fineamount, string fineCause, string isPaid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@userid_1", userid),
                new SqlParameter("@accno_2", accno),
                new SqlParameter("@receivingdate_3", receivingdate),
                new SqlParameter("@fineamount_4", fineamount),
                new SqlParameter("@fineCause_5", fineCause),
                new SqlParameter("@isPaid_6", isPaid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_RecTrans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertCircDesignationMaster(string Designationid, string Designation, string classname, string requester, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Designationid_1", Designationid),
                new SqlParameter("@Designation_2", Designation),
                new SqlParameter("@classname_3", classname),
                new SqlParameter("@requester_4", requester),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircDesignationMaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertSpineLabelPrint(int id, string AccessionNo, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@id_1", id),
                new SqlParameter("@AccessionNo_2", AccessionNo),
                new SqlParameter("@userid_3", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_SpineLabelPrint_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertConsortium(int Id, string ConsortiumName, string Cons_Url, byte[] Cons_Icon, string UserId)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id_1", Id),
                new SqlParameter("@ConsortiumName_2", ConsortiumName),
                new SqlParameter("@Cons_Url_3", Cons_Url),
                new SqlParameter("@Cons_Icon_4", Cons_Icon),
                new SqlParameter("@UserId_5", UserId)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Consortium_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertcircHolidays(int holidayid, DateTime h_date, string description, string scheduled, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@holidayid_1", holidayid),
                new SqlParameter("@h_date_2", h_date),
                new SqlParameter("@description_3", description),
                new SqlParameter("@scheduled_4", scheduled),
                new SqlParameter("@userid_5", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circHolidays_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertSkills(string Member_code, int id, string skills, string flg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Member_code", Member_code),
                new SqlParameter("@id", id),
                new SqlParameter("@skills", skills),
                new SqlParameter("@flg", flg)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Skills]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbkpServiceSetting(string Occurs, int Recurs, DateTime Occurs_at, DateTime Startdate, DateTime Enddate, DateTime bkpDate, string status, string Status1, string Week, int Month, string databaseName, string Location)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@occurs", Occurs),
                new SqlParameter("@recurs", Recurs),
                new SqlParameter("@occurs_at", Occurs_at),
                new SqlParameter("@startdate", Startdate),
                new SqlParameter("@enddate", Enddate),
                new SqlParameter("@bkpdate", bkpDate),
                new SqlParameter("@status", status),
                new SqlParameter("@Status1", Status1),
                new SqlParameter("@Week", Week),
                new SqlParameter("@month", Month),
                new SqlParameter("@databaseName", databaseName),
                new SqlParameter("@Location", Location)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Ins_bkpServiceSetting]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertwaivepaid(string refid, string userid, DateTime waivedate, decimal totaloverdue, decimal paidoverdue, string reason, string userid1)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@refid_1", refid),
                new SqlParameter("@userid_2", userid),
                new SqlParameter("@waivedate_3", waivedate),
                new SqlParameter("@totaloverdue_4", totaloverdue),
                new SqlParameter("@paidoverdue_5", paidoverdue),
                new SqlParameter("@reason_6", reason),
                new SqlParameter("@userid1_7", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_waivepaid_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertMARC_Data(string AccessionNumber, string tag_no, string tag_indicator, string tag_subField, string tag_value)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@AccessionNumber_1", AccessionNumber),
                new SqlParameter("@tag_no_2", tag_no),
                new SqlParameter("@tag_indicator_3", tag_indicator),
                new SqlParameter("@tag_subField_4", tag_subField),
                new SqlParameter("@tag_value_5", tag_value)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_MARC_Data_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertMembershipAcheivement(string Member_code, string MA_type, string Act_Name, string File_Name, byte Att_file, string flg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@Member_code", Member_code),
                new SqlParameter("@MA_type", MA_type),
                new SqlParameter("@Act_Name", Act_Name),
                new SqlParameter("@File_Name", File_Name),
                new SqlParameter("@Att_file", Att_file),
                new SqlParameter("@flg", flg)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_waivepaid_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertexistingbookkinfo(int srNoOld, int mediatype, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int noofcopies, decimal price, string publisherid, DateTime recordingdate, string seriesname, string form, string keywords, DateTime docDate, decimal Fprice, string FcurrencyCode, string subtitle, string part, decimal specialprice, int dept, string yearofPublication, int Language_Id, decimal exchange_rate, int no_of_pages, string page_size, string vendor_id)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[38]
            {
                new SqlParameter("@srNoOld_1", srNoOld),
                new SqlParameter("@mediatype_2", mediatype),
                new SqlParameter("@title_3", title),
                new SqlParameter("@authortype_4", authortype),
                new SqlParameter("@firstname1_5", firstname1),
                new SqlParameter("@middlename1_6", middlename1),
                new SqlParameter("@lastname1_7", lastname1),
                new SqlParameter("@firstname2_8", firstname2),
                new SqlParameter("@middlename2_9", middlename2),
                new SqlParameter("@lastname2_10", lastname2),
                new SqlParameter("@firstname3_11", firstname3),
                new SqlParameter("@middlename3_12", middlename3),
                new SqlParameter("@lastname3_13", lastname3),
                new SqlParameter("@edition_14", edition),
                new SqlParameter("@yearofedition_15", yearofedition),
                new SqlParameter("@volumeno_16", volumeno),
                new SqlParameter("@isbn_17", isbn),
                new SqlParameter("@category_18", category),
                new SqlParameter("@noofcopies_19", noofcopies),
                new SqlParameter("@price_20", price),
                new SqlParameter("@publisherid_21", publisherid),
                new SqlParameter("@recordingdate_22", recordingdate),
                new SqlParameter("@seriesname_23", seriesname),
                new SqlParameter("@form_24", form),
                new SqlParameter("@keywords_25", keywords),
                new SqlParameter("@docDate_26", docDate),
                new SqlParameter("@Fprice_27", Fprice),
                new SqlParameter("@FcurrencyCode_28", FcurrencyCode),
                new SqlParameter("@subtitle_29", subtitle),
                new SqlParameter("@part_30", part),
                new SqlParameter("@specialprice_31", specialprice),
                new SqlParameter("@dept_32", dept),
                new SqlParameter("@yearofPublication_33", yearofPublication),
                new SqlParameter("@Language_Id_34", Language_Id),
                new SqlParameter("@exchange_rate_35", exchange_rate),
                new SqlParameter("@no_of_pages_36", no_of_pages),
                new SqlParameter("@page_size_37", page_size),
                new SqlParameter("@vendor_id_38", vendor_id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_existingbookkinfo_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertBookSeries(int ctrl_no, string SeriesName, string seriesNo, string seriesPart, string etal, int Svolume, string af1, string am1, string al1, string af2, string am2, string al2, string af3, string am3, string al3, string SSeriesName, string SseriesNo, string SseriesPart, string Setal, int SSvolume, string Saf1, string Sam1, string Sal1, string Saf2, string Sam2, string Sal2, string Saf3, string Sam3, string Sal3, string SeriesParallelTitle, string SSeriesParallelTitle, string SubSeriesName, string SubseriesNo, string SubseriesPart, string Subetal, int SubSvolume, string Subaf1, string Subam1, string Subal1, string Subaf2, string Subam2, string Subal2, string Subaf3, string Subam3, string Subal3, string SubSeriesParallelTitle, string ISSNMain, string ISSNSub, string ISSNSecond)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[49]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@SeriesName_2", SeriesName),
                new SqlParameter("@seriesNo_3", seriesNo),
                new SqlParameter("@seriesPart_4", seriesPart),
                new SqlParameter("@etal_5", etal),
                new SqlParameter("@Svolume_6", Svolume),
                new SqlParameter("@af1_7", af1),
                new SqlParameter("@am1_8", am1),
                new SqlParameter("@al1_9", al1),
                new SqlParameter("@af2_10", af2),
                new SqlParameter("@am2_11", am2),
                new SqlParameter("@al2_12", al2),
                new SqlParameter("@af3_13", af3),
                new SqlParameter("@am3_14", am3),
                new SqlParameter("@al3_15", al3),
                new SqlParameter("@SSeriesName_16", SSeriesName),
                new SqlParameter("@SseriesNo_17", SseriesNo),
                new SqlParameter("@SseriesPart_18", SseriesPart),
                new SqlParameter("@Setal_19", Setal),
                new SqlParameter("@SSvolume_20", SSvolume),
                new SqlParameter("@Saf1_21", Saf1),
                new SqlParameter("@Sam1_22", Sam1),
                new SqlParameter("@Sal1_23", Sal1),
                new SqlParameter("@Saf2_24", Saf2),
                new SqlParameter("@Sam2_25", Sam2),
                new SqlParameter("@Sal2_26", Sal2),
                new SqlParameter("@Saf3_27", Saf3),
                new SqlParameter("@Sam3_28", Sam3),
                new SqlParameter("@Sal3_29", Sal3),
                new SqlParameter("@SeriesParallelTitle_30", SeriesParallelTitle),
                new SqlParameter("@SSeriesParallelTitle_31", SSeriesParallelTitle),
                new SqlParameter("@SubSeriesName_32", SubSeriesName),
                new SqlParameter("@SubseriesNo_33", SubseriesNo),
                new SqlParameter("@SubseriesPart_34", SubseriesPart),
                new SqlParameter("@Subetal_35", Subetal),
                new SqlParameter("@SubSvolume_36", SubSvolume),
                new SqlParameter("@Subaf1_37", Subaf1),
                new SqlParameter("@Subam1_38", Subam1),
                new SqlParameter("@Subal1_39", Subal1),
                new SqlParameter("@Subaf2_40", Subaf2),
                new SqlParameter("@Subam2_41", Subam2),
                new SqlParameter("@Subal2_42", Subal2),
                new SqlParameter("@Subaf3_43", Subaf3),
                new SqlParameter("@Subam3_44", Subam3),
                new SqlParameter("@Subal3_45", Subal3),
                new SqlParameter("@SubSeriesParallelTitle_46", SubSeriesParallelTitle),
                new SqlParameter("@ISSNMain_47", ISSNMain),
                new SqlParameter("@ISSNSub_48", ISSNSub),
                new SqlParameter("@ISSNSecond_49", ISSNSecond)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookSeries_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetGiftIndent(string GiftIndentId, string giftindentnumber, string title, string Author, string giftindentnumber2, bool bool_order_check_code0, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@GiftIndentId", GiftIndentId),
                new SqlParameter("@giftindentnumber", giftindentnumber),
                new SqlParameter("@title", title),
                new SqlParameter("@Author", Author),
                new SqlParameter("@giftindentnumber2", giftindentnumber2),
                new SqlParameter("@bool_order_check_code0", bool_order_check_code0),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndent](@GiftIndentId, @giftindentnumber,@title,@Author,@giftindentnumber2,@bool_order_check_code0,@UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftIndentPublisher(string GiftIndentId, string giftindentnumber, string Firstname, string giftindentnumberItemNo, bool bool_order_check_code0, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[7];
            array[0] = new SqlParameter("@GiftIndentId", GiftIndentId);
            array[1] = new SqlParameter("@giftindentnumber", giftindentnumber);
            array[2] = new SqlParameter("@Firstname", Firstname);
            array[3] = new SqlParameter("@giftindentnumberItemNo", giftindentnumberItemNo);
            array[4] = new SqlParameter("@bool_order_check_code0", bool_order_check_code0);
            array[5] = new SqlParameter("@UserId", UserId);
            array[6] = new SqlParameter("@FormId", FormId);
            array[7] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentPublisher](@GiftIndentId, @giftindentnumber,@Firstname,@giftindentnumberItemNo,@bool_order_check_code0,@UserId,@FormId,@Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftIndentVendor(string GiftIndentId, string giftindentnumber, string Vendorname, bool bool_order_check_code0, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@GiftIndentId", GiftIndentId),
                new SqlParameter("@giftindentnumber", giftindentnumber),
                new SqlParameter("@Vendorname", Vendorname),
                new SqlParameter("@bool_order_check_code0", bool_order_check_code0),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentVendor](@GiftIndentId, @giftindentnumber,@Vendorname,@bool_order_check_code0,@UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftIndentDetail(string GiftIndentId, int deptcode, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@GiftIndentId", GiftIndentId),
                new SqlParameter("@departmentcode", deptcode),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM    (@GiftIndentId,@departmentcode, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftindentID()
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[0];
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftindentID]()", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftindentItemNo(string giftindentnumber)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@giftindentnumber", giftindentnumber)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftindentItemNo](@giftindentnumber)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftIndentViewR(string giftindentnumber, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@giftindentnumber", giftindentnumber),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentViewR](@giftindentnumber, @UserId,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentApprRef(string indentnumber, string indentId, int departmentcode, string titleExact, string title, string Approval, bool EmptyRef, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@indentnumber", indentnumber),
                new SqlParameter("@indentId", indentId),
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@titleExact", titleExact),
                new SqlParameter("@title", title),
                new SqlParameter("@Approval", Approval),
                new SqlParameter("@EmptyRef", EmptyRef),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentApprRef](@indentnumber,@indentId,@departmentcode,@titleExact,@title,@Approval,@EmptyRef, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetUserClass(string UserId2, bool CanRequest, int departmentcode, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@UserId2", UserId2),
                new SqlParameter("@CanRequest", CanRequest),
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetUserClass](@UserId2,@CanRequest,@departmentcode, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentMaxId()
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[0];
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentMaxId]( )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentDeptApprIsstand(string Approval, string Isstanding, string order_check_code, int departmentcode, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@Approval", Approval),
                new SqlParameter("@Isstanding", Isstanding),
                new SqlParameter("@order_check_code", order_check_code),
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDeptApprIsstand](@Approval,@Isstanding,@order_check_code,@departmentcode, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentDeptDate(int departmentcode, DateTime IndentDateFrom, DateTime IndentDateTo, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@IndentDateFrom", IndentDateFrom),
                new SqlParameter("@IndentDateTo", IndentDateTo),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDeptDate](@departmentcode,@IndentDateFrom,@IndentDateTo, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetGiftIndentDeptDate(int departmentcode, string order_check_code, DateTime IndentDateFrom, DateTime IndentDateTo, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@order_check_code", order_check_code),
                new SqlParameter("@IndentDateFrom", IndentDateFrom),
                new SqlParameter("@IndentDateTo", IndentDateTo),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentDeptDate](@departmentcode,@order_check_code, @IndentDateFrom,@IndentDateTo, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentAppr(string approval, string order_check_code, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@approval", approval),
                new SqlParameter("@order_check_code", order_check_code),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentAppr](@approval,@order_check_code, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentDeptDateOrd(DateTime IndentDateFrom, DateTime IndentDateTo, string order_check_code, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@IndentDateFrom", IndentDateFrom),
                new SqlParameter("@IndentDateTo", IndentDateTo),
                new SqlParameter("@order_check_code", order_check_code),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDeptDateOrd](@IndentDateFrom,@IndentDateTo,@order_check_code, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetDeptInstIndent(string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetDeptInstIndent]( @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentDocno(string Docno, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@Docno", Docno),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDocno](@Docno, @UserID,@FormId,@Type )", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateIndentDocno(string indentid, string Docno, DateTime DocDate, string PrintStatus, string UserID, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@indentid", indentid),
                new SqlParameter("@Docno", Docno),
                new SqlParameter("@DocDate", DocDate),
                new SqlParameter("@PrintStatus", PrintStatus),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[UpdateIndentDocno]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateIdTable(string objectname, int currentposition, string UserID, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@objectname", objectname),
                new SqlParameter("@currentposition", currentposition),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormId", FormId),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[UpdateIdTable]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetGiftIndentViewRArr(DataTable giftindentnumbers, string UserId, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] array = new SqlParameter[4]
            {
                new SqlParameter("@giftindentnumbers", giftindentnumbers),
                null,
                null,
                null
            };
            array[0].TypeName = "dbo.arrstring";
            array[1] = new SqlParameter("@UserId", UserId);
            array[2] = new SqlParameter("@FormId", FormId);
            array[3] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentViewRArr](@giftindentnumbers, @UserId,@FormId,@Type )", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetIndentVarious2(string Title, DateTime IndentDateFrom, DateTime IndentDateTo, int departmentcode, string requestercode, string Indentnumber, string UserID, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@Title", Title);
            SqlParameter item2 = new SqlParameter("@IndentDateFrom", IndentDateFrom);
            SqlParameter item3 = new SqlParameter("@IndentDateTo", IndentDateTo);
            SqlParameter item4 = new SqlParameter("@departmentcode", departmentcode);
            SqlParameter item5 = new SqlParameter("@requestercode", requestercode);
            SqlParameter item6 = new SqlParameter("@Indentnumber", Indentnumber);
            SqlParameter item7 = new SqlParameter("@UserID", UserID);
            SqlParameter item8 = new SqlParameter("@FormId", FormId);
            SqlParameter item9 = new SqlParameter("@Type", Type);
            list.Add(item);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);
            list.Add(item6);
            list.Add(item7);
            list.Add(item8);
            list.Add(item9);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[GetIndentVarious2]", ref objDataset, list.ToArray(), SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetOrder(bool NormalAdv, bool NormalOrder, bool AdvDept, bool OptAdvance, string Departmentname, string Vendorname, string Orderno, string CancelOrder, string Approval, string UserID, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@NormalAdv", NormalAdv);
            SqlParameter item2 = new SqlParameter("@NormalOrder", NormalOrder);
            SqlParameter item3 = new SqlParameter("@AdvDept", AdvDept);
            SqlParameter item4 = new SqlParameter("@OptAdvance", OptAdvance);
            SqlParameter item5 = new SqlParameter("@Departmentname", Departmentname);
            SqlParameter item6 = new SqlParameter("@Vendorname", Vendorname);
            SqlParameter item7 = new SqlParameter("@Orderno", Orderno);
            SqlParameter item8 = new SqlParameter("@CancelOrder", CancelOrder);
            SqlParameter item9 = new SqlParameter("@Approval", Approval);
            SqlParameter item10 = new SqlParameter("@UserID", UserID);
            SqlParameter item11 = new SqlParameter("@FormId", FormId);
            SqlParameter item12 = new SqlParameter("@Type", Type);
            list.Add(item);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);
            list.Add(item6);
            list.Add(item7);
            list.Add(item8);
            list.Add(item9);
            list.Add(item10);
            list.Add(item11);
            list.Add(item12);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[GetOrder]", ref objDataset, list.ToArray(), SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetIndentVendorApprIsstand(bool OptNormal, string Approval, string Isstanding, string order_check_code, int vendorid, string UserID, string FormId, string Type)
        {
            DataSet dataSet = new DataSet();
            DataTable objDataset = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@OptNormal", OptNormal);
            SqlParameter item2 = new SqlParameter("@Approval", Approval);
            SqlParameter item3 = new SqlParameter("@Isstanding", Isstanding);
            SqlParameter item4 = new SqlParameter("@order_check_code", order_check_code);
            SqlParameter item5 = new SqlParameter("@vendorid", vendorid);
            SqlParameter item6 = new SqlParameter("@UserID", UserID);
            SqlParameter item7 = new SqlParameter("@FormId", FormId);
            SqlParameter item8 = new SqlParameter("@Type", Type);
            list.Add(item);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);
            list.Add(item6);
            list.Add(item7);
            list.Add(item8);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentVendorApprIsstand](@OptNormal,@Approval,@Isstanding,@order_check_code,@vendorid, @UserId,@FormId,@Type )", ref objDataset, list.ToArray(), SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetBudgetMaster(int departmentcode, string session, string UserID, string FormId, string Type)
        {
            DataTable objDataset = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@departmentcode", departmentcode);
            SqlParameter item2 = new SqlParameter("@session", session);
            SqlParameter item3 = new SqlParameter("@UserID", UserID);
            SqlParameter item4 = new SqlParameter("@FormId", FormId);
            SqlParameter item5 = new SqlParameter("@Type", Type);
            list.Add(item);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetBudgetMaster](@departmentcode,@session, @UserId,@FormId,@Type )", ref objDataset, list.ToArray(), SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateIndentExchageRate(string Indentid, decimal ApplicableExchangeRate, decimal orderexchangerate, string UserID, string FormId, string Type)
        {
            DataSet objDataset = new DataSet();
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@Indentid", Indentid);
            SqlParameter item2 = new SqlParameter("@ApplicableExchangeRate", ApplicableExchangeRate);
            SqlParameter item3 = new SqlParameter("@orderexchangerate", orderexchangerate);
            SqlParameter item4 = new SqlParameter("@UserID", UserID);
            SqlParameter item5 = new SqlParameter("@FormId", FormId);
            SqlParameter item6 = new SqlParameter("@Type", Type);
            list.Add(item);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);
            list.Add(item6);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[UpdateIndentExchageRate]", ref objDataset, list.ToArray(), SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertClassMaster(int id, string Class, string shortname, string HowLongPreserve, string PermissionRequired, string TobeReturned, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@class", Class),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@HowLongPreserve", HowLongPreserve),
                new SqlParameter("@PermissionRequired", PermissionRequired),
                new SqlParameter("@TobeReturned", TobeReturned),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateClassMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetCatalogueCardView(string classnumber, string booknumber, string volume, string part, string edition, string Author, int language_id, string booktitle, int dept)
        {
            DataTable objDataset = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@classnumber", classnumber);
            SqlParameter item2 = new SqlParameter("@booknumber", booknumber);
            SqlParameter item3 = new SqlParameter("@volume", volume);
            SqlParameter item4 = new SqlParameter("@part", part);
            SqlParameter item5 = new SqlParameter("@edition", edition);
            SqlParameter item6 = new SqlParameter("@Author", Author);
            SqlParameter item7 = new SqlParameter("@language_id", language_id);
            SqlParameter item8 = new SqlParameter("@title", booktitle);
            SqlParameter item9 = new SqlParameter("@dept", dept);
            list.Add(item);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);
            list.Add(item6);
            list.Add(item7);
            list.Add(item8);
            list.Add(item9);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCatalogueCardView](@classnumber,@booknumber, @volume,@part,@edition,@Author,@language_id,@title,@dept )", ref objDataset, list.ToArray(), SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable UpdateInsertInvoice(int invoice_id, int Binder_id, string invoice_no, DateTime invoice_amt, DateTime invoice_date, string Bill_serial_no, string userid, string type, string CancelStatus, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[9];
            array[0] = new SqlParameter("@invoice_id_1", invoice_id);
            array[1] = new SqlParameter("@Binder_id_2", Binder_id);
            array[2] = new SqlParameter("@invoice_no_3", invoice_no);
            array[3] = new SqlParameter("@invoice_amt_4", invoice_amt);
            array[4] = new SqlParameter("@invoice_date_5", invoice_date);
            array[5] = new SqlParameter("@Bill_serial_no_6", Bill_serial_no);
            array[6] = new SqlParameter("@userid_7", userid);
            array[7] = new SqlParameter("@type_8", type);
            array[8] = new SqlParameter("@CancelStatus_9", CancelStatus);
            array[9] = new SqlParameter("@FormID", FormID);
            array[10] = new SqlParameter("@Type", Type);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_binder_invoice_master_1]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertDesignation(int id, string Designation, string shortname, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Designation", Designation),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateDesigMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertstatusmaster(int id, string status, string avv, int UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@status", status),
                new SqlParameter("@avv", avv),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_StatusMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertFileSizeMaster(int id, string SizeName, string shortname, int UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@SizeName", SizeName),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_FileSizeMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertsectionmaster(int id, string Section, string shortname, string department, string UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@Section", Section),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@department", department),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_sectionmaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable upsertFileSettingMaster(int id, string parameter, string avv, DateTime wef, string status, int UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@parameter", parameter),
                new SqlParameter("@avv", avv),
                new SqlParameter("@wef", wef),
                new SqlParameter("@status", status),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_FileSettingMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateInsertRegister(int id, string Register, string shortname, int section, int department, int designation, string MaintainedBy, int UserID, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Register", Register),
                new SqlParameter("@shortname", shortname),
                new SqlParameter("@section", section),
                new SqlParameter("@department", department),
                new SqlParameter("@designation", designation),
                new SqlParameter("@MaintainedBy", MaintainedBy),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_RegisterMaster]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertdocumentnumberstructure(string objectname, string prefix, string suffix, int currentposition, string formname)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@objectname", objectname),
                new SqlParameter("@prefix", prefix),
                new SqlParameter("@suffix", suffix),
                new SqlParameter("@currentposition", currentposition),
                new SqlParameter("@formname", formname)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[DocumentNumberstructure]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertjournalaccessioning(string accession_no, string part_no, string journal_no, string volume, string fromIssue, string toIssue, string Lack_no, string accession_id, DateTime date_of_accessioning, string issue_status, string curr_year, string From_date, string To_date, string userid, int Issue_No, int Copy_No)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[16]
            {
                new SqlParameter("@accession_no_1", accession_no),
                new SqlParameter("@part_no_2", part_no),
                new SqlParameter("@journal_no_3", journal_no),
                new SqlParameter("@volume_4", volume),
                new SqlParameter("@fromIssue_5", fromIssue),
                new SqlParameter("@toIssue_6", toIssue),
                new SqlParameter("@Lack_no_7", Lack_no),
                new SqlParameter("@accession_id_8", accession_id),
                new SqlParameter("@date_of_accessioning_9", date_of_accessioning),
                new SqlParameter("@issue_status_10", issue_status),
                new SqlParameter("@curr_year_11", curr_year),
                new SqlParameter("@From_date_12", From_date),
                new SqlParameter("@To_date_13", To_date),
                new SqlParameter("@userid_14", userid),
                new SqlParameter("@Issue_No", Issue_No),
                new SqlParameter("@Copy_No", Copy_No)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_accessioning_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertILLReceive(int s_no, DateTime receive_date, int ILLid, string accession_no, DateTime return_date, string isbn, string title, decimal book_price, decimal fine, string author, string edition, string description, string status, string volume, int return_status, string DocumentNo, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[17]
            {
                new SqlParameter("@s_no_1", s_no),
                new SqlParameter("@receive_date_2", receive_date),
                new SqlParameter("@ILLid_3", ILLid),
                new SqlParameter("@accession_no_4", accession_no),
                new SqlParameter("@return_date_5", return_date),
                new SqlParameter("@isbn_6", isbn),
                new SqlParameter("@title_7", title),
                new SqlParameter("@book_price_8", book_price),
                new SqlParameter("@fine_9", fine),
                new SqlParameter("@author_10", author),
                new SqlParameter("@edition_11", edition),
                new SqlParameter("@description_12", description),
                new SqlParameter("@status_13", status),
                new SqlParameter("@volume_14", volume),
                new SqlParameter("@return_status_15", return_status),
                new SqlParameter("@DocumentNo_16", DocumentNo),
                new SqlParameter("@userid_17", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ILLReceive_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertjournalCatalogue(int JOurnal_NO, string Journal_title, string Title_abbreviation, string series_title, string spine_title, string frequency, DateTime PublicationDate, string Publisher, string agent, string starting_Volume, string ending_volume, string starting_issue, string ending_issue, string starting_part, string ending_part, string Copy_No, string ctrl_No, string class_No, string Accession_no, string currency, decimal cuur_value, decimal price, decimal special_price, DateTime Catalog_date, string Lack_no, string userid, string Book_no, int loc_id, string flg)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[29]
            {
                new SqlParameter("@JOurnal_NO", JOurnal_NO),
                new SqlParameter("@Journal_title", Journal_title),
                new SqlParameter("@Title_abbreviation", Title_abbreviation),
                new SqlParameter("@series_title", series_title),
                new SqlParameter("@spine_title", spine_title),
                new SqlParameter("@frequency", frequency),
                new SqlParameter("@PublicationDate", PublicationDate),
                new SqlParameter("@Publisher", Publisher),
                new SqlParameter("@agent", agent),
                new SqlParameter("@starting_Volume", starting_Volume),
                new SqlParameter("@ending_volume", ending_volume),
                new SqlParameter("@starting_issue", starting_issue),
                new SqlParameter("@ending_issue", ending_issue),
                new SqlParameter("@starting_part", starting_part),
                new SqlParameter("@ending_part", ending_part),
                new SqlParameter("@Copy_No", Copy_No),
                new SqlParameter("@ctrl_No", ctrl_No),
                new SqlParameter("@class_No", class_No),
                new SqlParameter("@Accession_no", Accession_no),
                new SqlParameter("@currency", currency),
                new SqlParameter("@cuur_value", cuur_value),
                new SqlParameter("@price", price),
                new SqlParameter("@special_price", special_price),
                new SqlParameter("@Catalog_date", Catalog_date),
                new SqlParameter("@Lack_no", Lack_no),
                new SqlParameter("@userid", userid),
                new SqlParameter("@Book_no", Book_no),
                new SqlParameter("@loc_id", loc_id),
                new SqlParameter("@flg", flg)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_Catalogue]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertnewvol(string journal_no, int tot_vol, int issue_per_vol, int part_per_iss, int start_vol, int end_vol, int start_iss, int end_iss, int start_part, int end_part, string accno, int loc_id, DateTime Pub_dt, string Freq, string ExAccno, int Ctrl_no, string result)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[17]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@tot_vol_2", tot_vol),
                new SqlParameter("@issue_per_vol_3", issue_per_vol),
                new SqlParameter("@part_per_iss_4", part_per_iss),
                new SqlParameter("@start_vol_5", start_vol),
                new SqlParameter("@end_vol_6", end_vol),
                new SqlParameter("@start_iss_7", start_iss),
                new SqlParameter("@end_iss_8", end_iss),
                new SqlParameter("@start_part_9", start_part),
                new SqlParameter("@end_part_10", end_part),
                new SqlParameter("@accno_11", accno),
                new SqlParameter("@loc_id_12", loc_id),
                new SqlParameter("@Pub_dt_13", Pub_dt),
                new SqlParameter("@Freq_14", Freq),
                new SqlParameter("@ExAccno_15", ExAccno),
                new SqlParameter("@Ctrl_no_16", Ctrl_no),
                new SqlParameter("@result_17", result)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_New_Vol]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertTranData(string AccNos, string TranType, string TranIds, string TranDescS, string Res)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@AccNos", AccNos),
                new SqlParameter("@TranType", TranType),
                new SqlParameter("@TranIds", TranIds),
                new SqlParameter("@TranDescS", TranDescS),
                new SqlParameter("@Res", Res)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[TranData]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertJOverDue(string MemId, DateTime Entdt, string JTranId, string JCId, string PayAmtS, string WavAmtS, string userid, string RetStatus, string RetRem)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[9]
            {
                new SqlParameter("@MemId", MemId),
                new SqlParameter("@EntDt", Entdt),
                new SqlParameter("@JTranId", JTranId),
                new SqlParameter("@JCId", JCId),
                new SqlParameter("@PayAmtS", PayAmtS),
                new SqlParameter("@WavAmtS", WavAmtS),
                new SqlParameter("@userid", userid),
                new SqlParameter("@RetStatus", RetStatus),
                new SqlParameter("@RetRem", RetRem)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_JOverDue]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertnonarrivaljourlist(string Letter_No, DateTime Letter_date, DateTime Reply_date, string reply_id, string remark, string status, string Doc_No, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[8]
            {
                new SqlParameter("@Letter_No", Letter_No),
                new SqlParameter("@Letter_date", Letter_date),
                new SqlParameter("@Reply_date", Reply_date),
                new SqlParameter("@reply_id", reply_id),
                new SqlParameter("@remark", remark),
                new SqlParameter("@status", status),
                new SqlParameter("@Doc_No", Doc_No),
                new SqlParameter("@userid", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Nonarrive_Jour_Master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable DeleteMassCatalog(string accessionnumber, string AppName, string UserId, string IpAddress)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@accessionnumber", accessionnumber),
                new SqlParameter("@AppName", AppName),
                new SqlParameter("@UId", UserId),
                new SqlParameter("@IpAddress", IpAddress)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[sp_DeleteCatalog]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateinsertDirectCatalogExpress(string accessionno, string accessionnocp, string cpbook, string form, int accid, DateTime accdate, string booktitle, decimal bookprice, string status, string issuestatus, string checkstatus, int edityear, int copynumber, int pubyear, string billno, DateTime billdate, string itemtype, string original_currency, string userid, string vendorsource, int deptcode, int itemcatcode, string bookstitle, string volno, int part, string initpages, string parts, int publishcode, string edition, string isbn, string subject1, string subject2, string subject3, string bibilopages, string issn, string volume, int language, string Firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string classno, string bookno, int pages, string media_type, string searchtext, string volumenoV, string InsUp, string firstname, string pubcity, int location, string result, int octrl, string audi, string session, string ipa, string title, string edf1, string edm1, string edL1, string edf2, string edm2, string edL2, string edf3, string edm3, string edL3, string compf1, string compm1, string compL1, string compf2, string compm2, string compL2, string compf3, string compm3, string compL3, string ilf1, string ilm1, string ilL1, string ilf2, string ilm2, string ilL2, string ilf3, string ilm3, string ilL3, string tranf1, string tranm1, string tranL1, string tranf2, string tranm2, string tranL2, string tranf3, string tranm3, string tranL3, int setofbooks, string appname, int transNo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[101]
            {
                new SqlParameter("@accessionnumber_1", accessionno),
                new SqlParameter("@accessionnumber_1_Cp", accessionnocp),
                new SqlParameter("@cp_booknumber", cpbook),
                new SqlParameter("@form_4", form),
                new SqlParameter("@accessionid_5", accid),
                new SqlParameter("@accessionid_6", accdate),
                new SqlParameter("@booktitle_7", booktitle),
                new SqlParameter("@@bookprice_8", bookprice),
                new SqlParameter("@status_9", status),
                new SqlParameter("@issuestatus_10", issuestatus),
                new SqlParameter("@checkstatus_11", checkstatus),
                new SqlParameter("@editionyear_12", edityear),
                new SqlParameter("@copynumber_13", copynumber),
                new SqlParameter("@pubyear_14", pubyear),
                new SqlParameter("@biilNo_15", billno),
                new SqlParameter("@billDate_16", billdate),
                new SqlParameter("@Item_type_17", itemtype),
                new SqlParameter("@OriginalCurrency_18", original_currency),
                new SqlParameter("@userid_19", userid),
                new SqlParameter("@vendor_source_20", vendorsource),
                new SqlParameter("@DeptCode_21", deptcode),
                new SqlParameter("@ItemCategoryCode_22", itemcatcode),
                new SqlParameter("@bookStitle_23", bookstitle),
                new SqlParameter("@volumenumber_24", volno),
                new SqlParameter("@Part_24_2", part),
                new SqlParameter("@initpages_25", initpages),
                new SqlParameter("@parts_26", parts),
                new SqlParameter("@publishercode_27", publishcode),
                new SqlParameter("@edition_28", edition),
                new SqlParameter("@isbn_29", isbn),
                new SqlParameter("@subject1_30", subject1),
                new SqlParameter("@subject2_31", subject2),
                new SqlParameter("@subject3_32", subject3),
                new SqlParameter("@bibliopages_33", bibilopages),
                new SqlParameter("@issn_34", issn),
                new SqlParameter("@volume_35", volume),
                new SqlParameter("@language_36", language),
                new SqlParameter("@firstname1_37", Firstname1),
                new SqlParameter("@middlename1_38", middlename1),
                new SqlParameter("@lastname1_39", lastname1),
                new SqlParameter("@firstname2_40", firstname2),
                new SqlParameter("@middlename2_41", middlename2),
                new SqlParameter("@lastname2_42", lastname2),
                new SqlParameter("@firstname3_43", firstname3),
                new SqlParameter("@middlename3_44", middlename3),
                new SqlParameter("@lastname3_45", lastname3),
                new SqlParameter("@classno_46", classno),
                new SqlParameter("@bookno_47", bookno),
                new SqlParameter("@pages_48", pages),
                new SqlParameter("@media_type_49", media_type),
                new SqlParameter("@searchtext_49_1", searchtext),
                new SqlParameter("@volumenumberV_50", volumenoV),
                new SqlParameter("@InsUpd48", InsUp),
                new SqlParameter("@firstname_49", firstname),
                new SqlParameter("@PubCity_50", pubcity),
                new SqlParameter("@Location_51", location),
                new SqlParameter("@Result55", result),
                new SqlParameter("@OCtrl_no", octrl),
                new SqlParameter("@Audi_101", audi),
                new SqlParameter("@Sess_102", session),
                new SqlParameter("@IPA_103", ipa),
                new SqlParameter("@Title_104", title),
                new SqlParameter("@EdF1", edf1),
                new SqlParameter("@EdM1", edm1),
                new SqlParameter("@EdL1", edL1),
                new SqlParameter("@EdF2", edf2),
                new SqlParameter("@EdM2", edm2),
                new SqlParameter("@EdL2", edL2),
                new SqlParameter("@EdF3", edf3),
                new SqlParameter("@EdM3", edm3),
                new SqlParameter("@EdL3", edL3),
                new SqlParameter("@CompF1", compf1),
                new SqlParameter("@CompM1", compm1),
                new SqlParameter("@CompL1", compL1),
                new SqlParameter("@CompF2", compf2),
                new SqlParameter("@CompM2", compm2),
                new SqlParameter("@CompL2", compL2),
                new SqlParameter("@CompF3", compf3),
                new SqlParameter("@CompM3", compm3),
                new SqlParameter("@CompL3", compL3),
                new SqlParameter("@IlF1", ilf1),
                new SqlParameter("@IlM1", ilm1),
                new SqlParameter("@IlL1", ilL1),
                new SqlParameter("@IlF2", ilf2),
                new SqlParameter("@IlM2", ilm2),
                new SqlParameter("@IlL2", ilL2),
                new SqlParameter("@IlF3", ilf3),
                new SqlParameter("@IlM3", ilm3),
                new SqlParameter("@IlL3", ilL3),
                new SqlParameter("@TranF1", tranf1),
                new SqlParameter("@TranM1", tranm1),
                new SqlParameter("@TranL1", tranL1),
                new SqlParameter("@TranF2", tranf2),
                new SqlParameter("@TranM2", tranm2),
                new SqlParameter("@TranL2", tranL2),
                new SqlParameter("@TranF3", tranf3),
                new SqlParameter("@TranM3", tranm3),
                new SqlParameter("@TranL3", tranL3),
                new SqlParameter("@SetOFbooks", setofbooks),
                new SqlParameter("@AppName", appname),
                new SqlParameter("@TransNo", transNo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DirectCatalog_Express]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertaccessionexpress(string AccessionNo, string Accessioncp, int copyno, int ctrlno, decimal bookprice, string booknocp, string item_type, DateTime AccnCpDate, string billno, DateTime billdate, int deptid, int itemcategory, string vendor, int locid, string userid, string ipaddress, string appname, int transno)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[18]
            {
                new SqlParameter("@AccessionNo", AccessionNo),
                new SqlParameter("@AccessionNoCp", Accessioncp),
                new SqlParameter("@Copynnumber", copyno),
                new SqlParameter("@Ctrl_no", ctrlno),
                new SqlParameter("@BookPrice", bookprice),
                new SqlParameter("@BookNumberCP", booknocp),
                new SqlParameter("@Item_type", item_type),
                new SqlParameter("@AccnCpDate", AccnCpDate),
                new SqlParameter("@BiilNo", billno),
                new SqlParameter("@BillDate", billdate),
                new SqlParameter("@DeptId", deptid),
                new SqlParameter("@ItemCategory", itemcategory),
                new SqlParameter("@Vendor", vendor),
                new SqlParameter("@LocId", locid),
                new SqlParameter("@userid", userid),
                new SqlParameter("@IpAddress", ipaddress),
                new SqlParameter("@AppName", appname),
                new SqlParameter("@TransNo", transno)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Accession_ExpressCP]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable UpdateBudgetApprove(int departmentcode, string session, decimal indent, int userid, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@departmentcode", departmentcode),
                new SqlParameter("@Curr_Session", session),
                new SqlParameter("@IndentValue", indent),
                new SqlParameter("@UserId", userid),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddApprCommit]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertcataloguedBEM(int ctrl_no, string classnumber, string booknumber, int trans, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@ctrl_no", ctrl_no),
                new SqlParameter("@classnumber", classnumber),
                new SqlParameter("@booknumber", booknumber),
                new SqlParameter("@TransNo", trans),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CatalogData_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertcataloguedBEM1(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string UniFormTitle, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[12]
            {
                new SqlParameter("@ctrl_no", ctrl_no),
                new SqlParameter("@firstname1", firstname1),
                new SqlParameter("@middlename1", middlename1),
                new SqlParameter("@lastname1", lastname1),
                new SqlParameter("@firstname2", firstname2),
                new SqlParameter("@middlename2", middlename2),
                new SqlParameter("@lastname2", lastname2),
                new SqlParameter("@firstname3", firstname3),
                new SqlParameter("@middlename3", middlename3),
                new SqlParameter("@lastname3", lastname3),
                new SqlParameter("@UniFormTitle", UniFormTitle),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertcatalogbookentry(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string UniFormTitle, int transNo, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[13]
            {
                new SqlParameter("@ctrl_no", ctrl_no),
                new SqlParameter("@firstname1", firstname1),
                new SqlParameter("@middlename1", middlename1),
                new SqlParameter("@lastname1", lastname1),
                new SqlParameter("@firstname2", firstname2),
                new SqlParameter("@middlename2", middlename2),
                new SqlParameter("@lastname2", lastname2),
                new SqlParameter("@firstname3", firstname3),
                new SqlParameter("@middlename3", middlename3),
                new SqlParameter("@lastname3", lastname3),
                new SqlParameter("@UniFormTitle", UniFormTitle),
                new SqlParameter("@transNo", transNo),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertcomparativechart(int txtorderno, string txtorderno0, DateTime txtorderdate, string txtcmbvendor, string txtCmbPublisher, string txtSTitle, string txtau1firstnm, decimal txtbookprice, decimal txtSpecialPrice, string Action)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@txtorderno", txtorderno),
                new SqlParameter("@txtorderno0", txtorderno0),
                new SqlParameter("@txtorderdate", txtorderdate),
                new SqlParameter("@txtcmbvendor", txtcmbvendor),
                new SqlParameter("@txtCmbPublisher", txtCmbPublisher),
                new SqlParameter("@txtSTitle", txtSTitle),
                new SqlParameter("@txtau1firstnm", txtau1firstnm),
                new SqlParameter("@txtbookprice", txtbookprice),
                new SqlParameter("@txtSpecialPrice", txtSpecialPrice),
                new SqlParameter("@Action", Action)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[ComparitiveChart]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalpaymenttransaction(int PaymentID, string InvoiceID, decimal InvoiceAmount, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@PaymentID", PaymentID),
                new SqlParameter("@InvoiceID", InvoiceID),
                new SqlParameter("@InvoiceAmount", InvoiceAmount),
                new SqlParameter("@userid", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_paymenttransaction_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertjournalarrival1(string journal_no, DateTime exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, DateTime publication_Date, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDatee)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[17]
            {
                new SqlParameter("@journal_no_1", journal_no),
                new SqlParameter("@exp_date_2", exp_date),
                new SqlParameter("@volume_3", volume),
                new SqlParameter("@issues_4", issues),
                new SqlParameter("@parts_5", parts),
                new SqlParameter("@indexes_6", indexes),
                new SqlParameter("@Status_7", Status),
                new SqlParameter("@Remarks_8", Remarks),
                new SqlParameter("@doc_id_9", doc_id),
                new SqlParameter("@issue_type_10", issue_type),
                new SqlParameter("@arr_date_11", arr_date),
                new SqlParameter("@arr_year_12", arr_year),
                new SqlParameter("@publication_Date_13", publication_Date),
                new SqlParameter("@ISSNNO_14", ISSNNO),
                new SqlParameter("@Media_Print_15", Media_Print),
                new SqlParameter("@Media_Online_16", Media_Online),
                new SqlParameter("@PublicationDate_17", publicationDatee)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertlostitementry(string Receipt_No, string Member_id, DateTime Receipt_Date, decimal Amount, string status, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] array = new SqlParameter[6];
            array[0] = new SqlParameter("@Receipt_No", Receipt_No);
            array[1] = new SqlParameter("@Member_id", Member_id);
            array[2] = new SqlParameter("@Receipt_Date", Receipt_Date);
            array[3] = new SqlParameter("@Amount", Amount);
            array[4] = new SqlParameter("@status", status);
            array[5] = new SqlParameter("@userid", userid);
            array[3] = new SqlParameter("@status", status);
            array[3] = new SqlParameter("@userid", userid);
            array[3] = new SqlParameter("@status", status);
            array[3] = new SqlParameter("@userid", userid);
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircReceiptNo_1]", ref objDataset, array, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertlostitementry1(string userid, decimal totalfine, string userid1, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@userid", userid),
                new SqlParameter("@totalfine", totalfine),
                new SqlParameter("@userid1", userid1),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circreceivemaster_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable upsertaccwithinvindent(int srno, DateTime curr_date, decimal postage, decimal net_amt, decimal disc_amt, decimal disc_percentage, string vendorid, decimal handling_charge, decimal total_amt, int rate_followed_by, decimal pay_amt, string invoice_no, string bill_no, DateTime bill_date, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[15]
            {
                new SqlParameter("@srno", srno),
                new SqlParameter("@curr_date", curr_date),
                new SqlParameter("@postage", postage),
                new SqlParameter("@net_amt", net_amt),
                new SqlParameter("@disc_amt", disc_amt),
                new SqlParameter("@disc_percentage", disc_percentage),
                new SqlParameter("@vendorid", vendorid),
                new SqlParameter("@handling_charge", handling_charge),
                new SqlParameter("@total_amt", total_amt),
                new SqlParameter("@rate_followed_by", rate_followed_by),
                new SqlParameter("@pay_amt", pay_amt),
                new SqlParameter("@invoice_no", invoice_no),
                new SqlParameter("@bill_no", bill_no),
                new SqlParameter("@bill_date", bill_date),
                new SqlParameter("@userid", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_Direct_invoice_master_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertBookRelators(int ctrl_no, string editorFname1, string editorMname1, string editorLname1, string editorFname2, string editorMname2, string editorLname2, string editorFname3, string editorMname3, string editorLname3, string CompilerFname1, string CompilerMname1, string CompilerLname1, string CompilerFname2, string CompilerMname2, string CompilerLname2, string CompilerFname3, string CompilerMname3, string CompilerLname3, string illusFname1, string illusMname1, string illusLname1, string illusFname2, string illusMname2, string illusrLname2, string illusFname3, string illusMname3, string illusLname3, string TranslatorFname1, string TranslatorMname11, string TranslatorLname1, string TranslatorFname2, string TranslatorMname2, string TranslatorLname2, string TranslatorFname3, string TranslatorMname3, string TranslatorLname3, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[38]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@editorFname1_2", editorFname1),
                new SqlParameter("@editorMname1_3", editorMname1),
                new SqlParameter("@editorLname1_4", editorLname1),
                new SqlParameter("@editorFname2_5", editorFname2),
                new SqlParameter("@editorMname2_6", editorFname2),
                new SqlParameter("@editorLname2_7", editorLname2),
                new SqlParameter("@editorFname3_8", editorFname3),
                new SqlParameter("@editorMname3_9", editorMname3),
                new SqlParameter("@editorLname3_10", editorLname3),
                new SqlParameter("@CompilerFname1_11", CompilerFname1),
                new SqlParameter("@CompilerMname1_12", CompilerMname1),
                new SqlParameter("@CompilerLname1_13", CompilerLname1),
                new SqlParameter("@CompilerFname2_14", CompilerFname2),
                new SqlParameter("@CompilerMname2_15", CompilerMname2),
                new SqlParameter("@CompilerLname2_16", CompilerLname2),
                new SqlParameter("@CompilerFname3_17", CompilerFname3),
                new SqlParameter("@CompilerMname3_18", CompilerMname3),
                new SqlParameter("@CompilerLname3_19", CompilerLname3),
                new SqlParameter("@illusFname1_20", illusFname1),
                new SqlParameter("@illusMname1_21", illusMname1),
                new SqlParameter("@illusLname1_22", illusLname1),
                new SqlParameter("@illusFname2_23", illusFname2),
                new SqlParameter("@illusMname2_24", illusMname2),
                new SqlParameter("@illusrLname2_25", illusrLname2),
                new SqlParameter("@illusFname3_26", illusFname3),
                new SqlParameter("@illusMname3_27", illusMname3),
                new SqlParameter("@illusLname3_28", illusLname3),
                new SqlParameter("@TranslatorFname1_29", TranslatorFname1),
                new SqlParameter("@TranslatorMname11_30", TranslatorMname11),
                new SqlParameter("@TranslatorLname1_31", TranslatorLname1),
                new SqlParameter("@TranslatorFname2_32", TranslatorFname2),
                new SqlParameter("@TranslatorMname2_33", TranslatorMname2),
                new SqlParameter("@TranslatorLname2_34", TranslatorLname2),
                new SqlParameter("@TranslatorFname3_35", TranslatorFname3),
                new SqlParameter("@TranslatorMname3_36", TranslatorMname3),
                new SqlParameter("@TranslatorLname3_37", TranslatorLname3),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinserteditorRelators(int ctrl_no, string editorFname1, string editorMname1, string editorLname1, string editorFname2, string editorMname2, string editorLname2, string editorFname3, string editorMname3, string editorLname3, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[11]
            {
                new SqlParameter("@ctrl_no_1", ctrl_no),
                new SqlParameter("@editorFname1_2", editorFname1),
                new SqlParameter("@editorMname1_3", editorMname1),
                new SqlParameter("@editorLname1_4", editorLname1),
                new SqlParameter("@editorFname2_5", editorFname2),
                new SqlParameter("@editorMname2_6", editorMname2),
                new SqlParameter("@editorLname2_7", editorLname2),
                new SqlParameter("@editorFname3_8", editorFname3),
                new SqlParameter("@editorMname3_9", editorMname3),
                new SqlParameter("@editorLname3_10", editorLname3),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertcompilerRelators(string CompilerFname1, string CompilerMname1, string CompilerLname1, string CompilerFname2, string CompilerMname2, string CompilerLname2, string CompilerFname3, string CompilerMname3, string CompilerLname3, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@CompilerFname1_11", CompilerFname1),
                new SqlParameter("@CompilerMname1_12", CompilerMname1),
                new SqlParameter("@CompilerLname1_13", CompilerLname1),
                new SqlParameter("@CompilerFname2_14", CompilerFname2),
                new SqlParameter("@CompilerMname2_15", CompilerMname2),
                new SqlParameter("@CompilerLname2_16", CompilerLname2),
                new SqlParameter("@CompilerFname3_17", CompilerFname3),
                new SqlParameter("@CompilerMname3_18", CompilerMname3),
                new SqlParameter("@CompilerLname3_19", CompilerLname3),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertTranslatorRelators(string TranslatorFname1, string TranslatorMname11, string TranslatorLname1, string TranslatorFname2, string TranslatorMname2, string TranslatorLname2, string TranslatorFname3, string TranslatorMname3, string TranslatorLname3, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@TranslatorFname1_29", TranslatorFname1),
                new SqlParameter("@TranslatorMname11_30", TranslatorMname11),
                new SqlParameter("@TranslatorLname1_31", TranslatorLname1),
                new SqlParameter("@TranslatorFname2_32", TranslatorFname2),
                new SqlParameter("@TranslatorMname2_33", TranslatorMname2),
                new SqlParameter("@TranslatorLname2_34", TranslatorLname2),
                new SqlParameter("@TranslatorFname3_35", TranslatorFname3),
                new SqlParameter("@TranslatorMname3_36", TranslatorMname3),
                new SqlParameter("@TranslatorLname3_37", TranslatorLname3),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertillusRelators(string illusFname1, string illusMname1, string illusLname1, string illusFname2, string illusMname2, string illusrLname2, string illusFname3, string illusMname3, string illusLname3, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@illusFname1_20", illusFname1),
                new SqlParameter("@illusMname1_21", illusMname1),
                new SqlParameter("@illusLname1_22", illusLname1),
                new SqlParameter("@illusFname2_23", illusFname2),
                new SqlParameter("@illusMname2_24", illusMname2),
                new SqlParameter("@illusrLname2_25", illusrLname2),
                new SqlParameter("@illusFname3_26", illusFname3),
                new SqlParameter("@illusMname3_27", illusMname3),
                new SqlParameter("@illusLname3_28", illusLname3),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable DeletepostMessage(string cid, int FormID)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[2]
            {
                new SqlParameter("@cid_1", cid),
                new SqlParameter("@FormID", FormID)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[delete_postMessages_1] ", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertDirinvtrans(int srno, int dept, decimal discount, string title, int language_id, string volume, string part, string edition, string author_type, string first_name, string middle_name, string last_name, string publisher_id, int noofcopy, decimal price_copy, int curr_code, decimal exchange_rate, string isbn, int category_id, int media_type, int SrNoOld, int sr_id, decimal tot_amt, string Indentnumber, string IndentId, string Status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[26]
            {
                new SqlParameter("@srno_1", srno),
                new SqlParameter("@dept_2", dept),
                new SqlParameter("@discount_3", discount),
                new SqlParameter("@title_4", title),
                new SqlParameter("@language_id_5", language_id),
                new SqlParameter("@volume_6", volume),
                new SqlParameter("@part_7", part),
                new SqlParameter("@edition_8", edition),
                new SqlParameter("@author_type_9", author_type),
                new SqlParameter("@first_name_10", first_name),
                new SqlParameter("@middle_name_11", middle_name),
                new SqlParameter("@last_name_12", last_name),
                new SqlParameter("@publisher_id_13", publisher_id),
                new SqlParameter("@noofcopy_14", noofcopy),
                new SqlParameter("@price_copy_15", price_copy),
                new SqlParameter("@curr_code_16", curr_code),
                new SqlParameter("@exchange_rate_17", exchange_rate),
                new SqlParameter("@isbn_18", isbn),
                new SqlParameter("@category_id_19", category_id),
                new SqlParameter("@media_type_20", media_type),
                new SqlParameter("@SrNoOld_21", SrNoOld),
                new SqlParameter("@sr_id_22", sr_id),
                new SqlParameter("@tot_amt_23", tot_amt),
                new SqlParameter("@IndentNumber_24", Indentnumber),
                new SqlParameter("@IndentId_25", IndentId),
                new SqlParameter("@status_26", Status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Dir_inv_trans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable upsertaccwithinvindent1(int srno, int dept, decimal discount, string title, int language_id, string volume, string part, string edition, string author_type, string first_name, string middle_name, string last_name, string publisher_id, int noofcopy, decimal price_copy, int curr_code, decimal exchange_rate, string isbn, int category_id, int media_type, int SrNoold, int Sr_id, decimal tot_amt, string IndentNumber, string IndentId, string Status)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[26]
            {
                new SqlParameter("@srno", srno),
                new SqlParameter("@dept", dept),
                new SqlParameter("@discount", discount),
                new SqlParameter("@title", title),
                new SqlParameter("@language_id", language_id),
                new SqlParameter("@volume", volume),
                new SqlParameter("@part", part),
                new SqlParameter("@edition", edition),
                new SqlParameter("@author_type", author_type),
                new SqlParameter("@first_name", first_name),
                new SqlParameter("@middle_name", middle_name),
                new SqlParameter("@last_name", last_name),
                new SqlParameter("@last_name", last_name),
                new SqlParameter("@noofcopy", noofcopy),
                new SqlParameter("@price_copy", price_copy),
                new SqlParameter("@curr_code", curr_code),
                new SqlParameter("@exchange_rate", exchange_rate),
                new SqlParameter("@isbn", isbn),
                new SqlParameter("@category_id", category_id),
                new SqlParameter("@media_type", media_type),
                new SqlParameter("@SrNoold", SrNoold),
                new SqlParameter("@Sr_id", Sr_id),
                new SqlParameter("@tot_amt", tot_amt),
                new SqlParameter("@IndentNumber", IndentNumber),
                new SqlParameter("@IndentId", IndentId),
                new SqlParameter("@Status", Status)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Dir_inv_trans_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable insertcataloguedBookAuthor(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@ctrl_no", ctrl_no),
                new SqlParameter("@firstname1", firstname1),
                new SqlParameter("@middlename1", middlename1),
                new SqlParameter("@lastname1", lastname1),
                new SqlParameter("@firstname2", firstname2),
                new SqlParameter("@middlename2", middlename2),
                new SqlParameter("@lastname2", lastname2),
                new SqlParameter("@firstname3", firstname3),
                new SqlParameter("@middlename3", middlename3),
                new SqlParameter("@lastname3", lastname3)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsert_catalogCardPrint1(string AccessionNumber, string ClassNumber, string BookNumber, string accesspoints, string Contents, string CardTitle, string Volume, string Copy, int id, string Tag)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@AccessionNumber_1", AccessionNumber),
                new SqlParameter("@ClassNumber_2", ClassNumber),
                new SqlParameter("@BookNumber_3", BookNumber),
                new SqlParameter("@accesspoints_4", accesspoints),
                new SqlParameter("@Contents_5", Contents),
                new SqlParameter("@CardTitle_6", CardTitle),
                new SqlParameter("@Volume_7", Volume),
                new SqlParameter("@Copy_8", Copy),
                new SqlParameter("@id_9", id),
                new SqlParameter("@Tag_10", Tag)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_catalogCardPrint_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable Updateinsertbookaccessionmaster(string accessionnumber, string ordernumber, string indentnumber, string form, int accessionid, DateTime accessioneddate, string booktitle, int srno, string released, decimal bookprice, int srNoOld, string biilNo, DateTime billDate, string Item_type, int OriginalPrice, string OriginalCurrency, string userid, string vendor_source, int DeptCode, int DSrno, string DeptName, string ItemCategory, int ItemCategoryCode, string BookNumber, string AppName, string ipaddress, int TransNo)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[27]
            {
                new SqlParameter("@accessionnumber_1", accessionnumber),
                new SqlParameter("@ordernumber_2", ordernumber),
                new SqlParameter("@indentnumber_3", indentnumber),
                new SqlParameter("@form_4", form),
                new SqlParameter("@accessionid_5", accessionid),
                new SqlParameter("@accessioneddate_6", accessioneddate),
                new SqlParameter("@booktitle_7", booktitle),
                new SqlParameter("@srno_8", srno),
                new SqlParameter("@released_9", released),
                new SqlParameter("@bookprice_10", bookprice),
                new SqlParameter("@srNoOld_11", srNoOld),
                new SqlParameter("@biilNo_12", biilNo),
                new SqlParameter("@billDate_13", billDate),
                new SqlParameter("@Item_type_14", Item_type),
                new SqlParameter("@OriginalPrice_15", OriginalPrice),
                new SqlParameter("@OriginalCurrency_16", OriginalCurrency),
                new SqlParameter("@userid_17", userid),
                new SqlParameter("@vendor_source_18", vendor_source),
                new SqlParameter("@DeptCode_19", DeptCode),
                new SqlParameter("@DSrno_20", DSrno),
                new SqlParameter("@DeptName_21", DeptName),
                new SqlParameter("@ItemCategory_22", ItemCategory),
                new SqlParameter("@ItemCategoryCode_23", ItemCategoryCode),
                new SqlParameter("@BookNumber", BookNumber),
                new SqlParameter("@AppName", AppName),
                new SqlParameter("@ipaddress", ipaddress),
                new SqlParameter("@TransNo", TransNo)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bookaccessionmaster_2]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable updateinsertbinderinformation1(int binderid, string name, string address, string city, string state, string email, string phoneno, int maxissuedays, int overduecharges, string userid)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[10]
            {
                new SqlParameter("@binderid_1", binderid),
                new SqlParameter("@name_2", name),
                new SqlParameter("@address_3", address),
                new SqlParameter("@city_4", city),
                new SqlParameter("@state_5", state),
                new SqlParameter("@email_6", email),
                new SqlParameter("@phoneno_7", phoneno),
                new SqlParameter("@maxissuedays_8", maxissuedays),
                new SqlParameter("@overduecharges_9", overduecharges),
                new SqlParameter("@userid_10", userid)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BinderInformation_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

        public DataTable GetItemType(int ItemID, int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@ItemID", ItemID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetItemType](@ItemID, @UserID, @FormID, @Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetOrderNo(int UserID, int FormID, int Type, string cmbCategory)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@cmbCategory", cmbCategory)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetOrderNo](@UserID, @FormID, @Type, @cmbCategory)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCDService(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCDService](@UserID,@FormID,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCDServiceMAxID(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCDServiceMAxID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GETEdocMaxID(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetEDOCMAXID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Fn_CD_Service_Level1(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_CD_Service_Level1](@UserID,@FormID,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Fn_CD_SERVICE_LEVEL1MAxID(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_CD_SERVICE_LEVEL1MAxID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Fn_CD_Service_Level2(int UserID, int FormID, int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[3]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_CD_Service_Level2](@UserID,@FormID,@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCDMasterMAxID(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetCDMasterMAxID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetConsortiumMAxID(int Type)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[1]
            {
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetConsortiumMAxID](@Type)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetConsortium(int UserID, int FormID, int Type, string txtCategory)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@txtCategory", txtCategory)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetConsortium](@UserID,@FormID,@Type,@txtCategory)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetConsortiumName(int UserID, int FormID, int Type, string txtConsortium, string hd_name, string txtUrl)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@txtConsortium", txtConsortium),
                new SqlParameter("@hd_name", hd_name),
                new SqlParameter("@txtUrl", txtUrl)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetConsortiumName](@UserID,@FormID,@Type,@txtConsortium,@hd_name,@txtUrl)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetOnlineJournal(int UserID, int FormID, int Type, string Category)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Category", Category)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetOnlineJournal](@UserID,@FormID,@Type,@Category)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetonlineJournal_attachments(int UserID, int FormID, int Type, string Category)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Category", Category)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetonlineJournal_attachments](@UserID,@FormID,@Type,@Category)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable Getcircularmessageposting(int UserID, int FormID, int Type, int id)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[4]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@id", id)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_Getcircularmessageposting](@UserID,@FormID,@Type,@id)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCircularVIEW(int UserID, int FormID, int Type, DateTime Dated, string filterqry)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[5]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Date", Dated),
                new SqlParameter("@filterqry", filterqry)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetCircularVIEW](@UserID,@FormID,@Type,@Date,@filterqry)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable GetCircularMeaageVIEW(int UserID, int FormID, int Type, int ddlmtype, DateTime Dated, string filterqry)
        {
            DataTable objDataset = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[6]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@ddlmtype", ddlmtype),
                new SqlParameter("@Date", Dated),
                new SqlParameter("@filterqry", filterqry)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetCircularMeaageVIEW](@UserID,@FormID,@Type,@ddlmtype,@Date,@filterqry)", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset;
        }

        public DataTable updateinsertorderJour(string OrderNo, DateTime OrderDate, string PublisherCode, string Status, string userid, int FormID, int Type)
        {
            DataSet objDataset = new DataSet();
            SqlParameter[] commandParameters = new SqlParameter[7]
            {
                new SqlParameter("@OrderNo_1", OrderNo),
                new SqlParameter("@OrderDate_2", OrderDate),
                new SqlParameter("@PublisherCode_3", PublisherCode),
                new SqlParameter("@Status_4", Status),
                new SqlParameter("@userid_7", userid),
                new SqlParameter("@FormID", FormID),
                new SqlParameter("@Type", Type)
            };
            SqlDataBase sqlDataBase = new SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OrderMaster_Journal_1]", ref objDataset, commandParameters, SqlDataBase.GetConnectionString());
            return objDataset.Tables[0];
        }

    }
}