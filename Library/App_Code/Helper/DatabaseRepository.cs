using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.IO.Ports;

namespace Library.Common.Helper.DL
{
    public class DatabaseRepository
    {
        public DataTable GetMenuDataDL(int UserTypeId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserTypeId", UserTypeId);
            param[1].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("select * from dbo.menuitems (@UserTypeId )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetDepartmentMaster(int DeptID, int InstID, int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DeptID", DeptID);
            param[1] = new SqlParameter("@InstID", InstID);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormID", FormID);
            param[4] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetDept](@DeptID, @InstID, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetPrefixMaster(int UserID, int FormID,  int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_Getprefixmaster](@UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetOrderMaster(int UserID, int FormID, int Type  , string listallcatogery)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@listallcatogery", listallcatogery);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_Getordermaster](@UserID, @FormID, @Type , @listallcatogery)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable Getlibrarysetup(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Getlibrarysetup](@UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GettempAccespointsMaxId(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GettempAccespointsMaxId](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIssuing_Auth_master(int id, int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormID", FormID);
            param[3] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetIssuing_Auth_master](@id, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable Issuing_Auth_masterInsert(int id, string AuthName, string shortname, string Bar_Council, int userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@AuthName", AuthName);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@Bar_Council", Bar_Council);
            param[4] = new SqlParameter("@userid", userid);
            param[5] = new SqlParameter("@FormID", FormID);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Issuing_Auth_master]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GetIDCardBack(int DescId, int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DescId", DescId);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormID", FormID);
            param[3] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_Getidcardbackdesc](@DescId, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable IdcardbackdescAddUpdate(string DescTitle, string DescInfo, int DescId, int userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@DescTitle", DescTitle);
            param[1] = new SqlParameter("@DescInfo", DescInfo);
            param[2] = new SqlParameter("@DescId", DescId);
            param[3] = new SqlParameter("@userid", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[proc_idcardbackdescAddUpdate]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable GetProgramMaster(int InstID, int DeptID, int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[1] = new SqlParameter("@InstID", InstID);
            param[0] = new SqlParameter("@DeptID", DeptID);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormID", FormID);
            param[4] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetProgramme](@InstID, @DeptID, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetCasteCategories(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[1] = new SqlParameter("@UserID", UserID);
            param[0] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetCastCat](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBindddl(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[1] = new SqlParameter("@UserID", UserID);
            param[0] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GetBindddl](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetStatusMaster(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[1] = new SqlParameter("@UserID", UserID);
            param[0] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[GetStatusMaster](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetDAFileInfo(int UserID, int FormID, int Type, string daid, string typeno)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@DAid", daid);
            param[4] = new SqlParameter("@typeno", typeno);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[GetDAFileInfo](@UserID , @FormID , @Type , @DAid, @typeno)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetFileSizeMaster(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[1] = new SqlParameter("@UserID", UserID);
            param[0] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[GetFileSizeMaster](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
       

        public DataTable GetDigitalinfo(int UserID, int FormID, int Type, int id)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[1] = new SqlParameter("@UserID", UserID);
            param[0] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@id", id);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM   [MTR].[GetDigitalinfo](@UserID , @FormID , @Type, @id )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetInstituteMaster(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetInstitute](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetCatogeryLoading(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);

            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetCategoryLoading](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;

        }

        public DataTable Getregistermaster(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);

            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GetRegisterMAster](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;

        }


        public DataTable GetBIndtransmaster(int UserID, int FormID, int Type, string cmbbinder)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);

            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@Cmbbinder", cmbbinder);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GETBINDTRANSMASTER](@UserID , @FormID , @Type,  @Cmbbinder)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;

        }

        public DataTable GetRegister_In(int UserID, int FormID, int Type, int view, int eaid)
        {
            DataTable dt = new DataTable();
          
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserID", UserID);

            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@view", view);
            param[4] = new SqlParameter("@eaid",eaid);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[GetRegister_IN](@UserID , @FormID , @Type,  @view, @eaid)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;

        }
        public DataTable SaveProgramMaster(int Progid, string Progname, string Shortname, int Deptid, int UserId, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ProgramID", Progid);
            param[1] = new SqlParameter("@ProgramName", Progname);
            param[2] = new SqlParameter("@Shortname", Shortname);
            param[3] = new SqlParameter("@DeptID", Deptid);
            param[4] = new SqlParameter("@UserID", UserId);
            param[5] = new SqlParameter("@FormID", FormID);
            param[6] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateProg]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }

        public DataTable SaveCasteCategories(int CatID, string CatName, string shortname, int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CatID", CatID);
            param[1] = new SqlParameter("@CatName", CatName);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateCasteCat]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }



        public DataTable UpsertInstitute(int InstituteCode, string InstituteName, string shortname, int userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@InstituteCode", InstituteCode);
            param[1] = new SqlParameter("@InstituteName", InstituteName);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@userid", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateInstitute]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable GetLoadingStatus(int ItemstatusID, int UserID, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[1] = new SqlParameter("@UserID", UserID);
            param[0] = new SqlParameter("@itemstatusid", ItemstatusID);
            param[2] = new SqlParameter("@FormID", FormID);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("Select * FROM [MTR].[Fn_GetLoadingStatus](@itemstatusid,@UserID , @FormID , @Type )", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }




        public DataTable UpdateInsertDepartmentMaster(int departmentcode, string departmentname, string shortname, int institutecode, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@departmentcode_1", departmentcode);
            param[1] = new SqlParameter("@departmentname_2", departmentname);
            param[2] = new SqlParameter("@shortname_3", shortname);
            param[3] = new SqlParameter("@institutecode_4", institutecode);
            param[4] = new SqlParameter("@userid_5", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_departmentmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable UpdateInsertPrefixMaster(int prefixid, string prefixname, int startno, int currenposition, string status, int category, string userid, string suffixname)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@prefixid_1", prefixid);
            param[1] = new SqlParameter("@prefixname_2 ", prefixname);
            param[2] = new SqlParameter("@startno_3", startno);
            param[3] = new SqlParameter("@currentposition_4", currenposition);
            param[4] = new SqlParameter("@status_5", status);
            param[5] = new SqlParameter("@Category_6", category);
            param[6] = new SqlParameter("@userid_7", userid);
            param[7] = new SqlParameter("@suffixname_8", suffixname);
          
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PrefixMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertCategoryStatus(int id, string Category_LoadingStatus, string Abbreviation, string cat_icon, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id_1", id);
            param[1] = new SqlParameter("@Category_LoadingStatus_2", Category_LoadingStatus);
            param[2] = new SqlParameter("@Abbreviation_3", Abbreviation);
            param[3] = new SqlParameter("@cat_icon_4", cat_icon);
            param[4] = new SqlParameter("@userid_5", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CategoryLoadingStatus_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateInsertLoadingStatus(int ItemStatusID, string ItemStatus, string ItemStatusShort, string isBardateApllicable, string isIsued, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ItemStatusID_1", ItemStatusID);
            param[1] = new SqlParameter("@ItemStatus_2", ItemStatus);
            param[2] = new SqlParameter("@ItemStatusShort_3", ItemStatusShort);
            param[3] = new SqlParameter("@isBardateApllicable_4", isBardateApllicable);
            param[4] = new SqlParameter("@isIsued_5", isIsued);
            param[5] = new SqlParameter("@userid_6", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ItemStatusMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }


        public DataTable UpdateInsertMediatype(int media_id, string media_name, string short_name, int userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@media_id_1", media_id);
            param[1] = new SqlParameter("@media_name_2", media_name);
            param[2] = new SqlParameter("@short_name_3", short_name);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            //sqlDataBase.ExceProc("[dbo].[insert_media_type_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateMediaType]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertPublisherMaster(string PublisherId, string PublisherCode, string firstname, string PublisherPhone1, string PublisherPhone2, string EmailID, string webaddress, string PublisherType, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PublisherId_1", PublisherId);
            param[1] = new SqlParameter("@PublisherCode_2", PublisherCode);
            param[2] = new SqlParameter("@firstname_3", firstname);
            param[3] = new SqlParameter("@PublisherPhone1_4", PublisherPhone1);
            param[4] = new SqlParameter("@PublisherPhone2_5", PublisherPhone2);
            param[5] = new SqlParameter("@EmailID_6", EmailID);
            param[6] = new SqlParameter("@webaddress_7", webaddress);
            param[7] = new SqlParameter("@PublisherType_8", PublisherType);
            param[8] = new SqlParameter("@userid_9", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_publishermaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }


        public DataTable Updateinsertissuesubscription(int Id, string IssueType, DateTime DocDate, String DocNumber, String ItemCode, String Quantity, float Rate, float Amount, float OtherExp, float TotalAmount)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@IssueType", IssueType);
            param[2] = new SqlParameter("@DocDate", DocDate);
            param[3] = new SqlParameter("@DocNumber", DocNumber);
            param[4] = new SqlParameter("@ItemCode", ItemCode);
            param[5] = new SqlParameter("@Quantity", Quantity);
            param[6] = new SqlParameter("@Rate", Rate);
            param[7] = new SqlParameter("@Amount", Amount);
            param[8] = new SqlParameter("@OtherExp", OtherExp);
            param[9] = new SqlParameter("@TotalAmount", TotalAmount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_IssueSubscription]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable Updateinsertreceiptsubscription(int Id, DateTime TransactionDate, string ItemCode, string Quantity, float Rate, int SupplierId, float Amount, float OtherExp, float TotalAmt, string Remak, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@TransactionDate", TransactionDate);
            param[2] = new SqlParameter("@ItemCode", ItemCode);
            param[3] = new SqlParameter("@Quantity", Quantity);
            param[4] = new SqlParameter("@Rate", Rate);
            param[5] = new SqlParameter("@SupplierId", SupplierId);
            param[6] = new SqlParameter("@Amount", Amount);
            param[7] = new SqlParameter("@OtherExp", OtherExp);
            param[8] = new SqlParameter("@TotalAmt", TotalAmt);
            param[9] = new SqlParameter("@Remak", Remak);
            param[10] = new SqlParameter("@FormID", FormID);
            param[11] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ReceiptSubscription]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        //akansha
        public DataTable UpdateinsertItemMaster(int id, string title, string sub_title, DateTime pub_day, string IssN_No, string Issue_No, string Volume, string Part_No, string Copy_No, string Lack_No, string Edition, string Edition_Year, int Language, string Publisher, string Vendor, int Currancy, decimal Price, string flg)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ID", id);
            param[1] = new SqlParameter("@Title", title);
            param[2] = new SqlParameter("@sub_Title", sub_title);
            param[3] = new SqlParameter("@Pub_Day", pub_day);
            param[4] = new SqlParameter("@IssN_No", IssN_No);
            param[5] = new SqlParameter("@Issue_No", Issue_No);
            param[6] = new SqlParameter("@Volume", Volume);
            param[7] = new SqlParameter("@Part_No", Part_No);
            param[8] = new SqlParameter("@Copy_No", Copy_No);
            param[9] = new SqlParameter("@Lack_No", Lack_No);
            param[10] = new SqlParameter("@Edition", Edition);
            param[11] = new SqlParameter("@Edition_Year", Edition_Year);
            param[12] = new SqlParameter("@Language", Language);
            param[13] = new SqlParameter("@Publisher", Publisher);
            param[14] = new SqlParameter("@Vendor", Vendor);
            param[15] = new SqlParameter("@Currency", Currancy);
            param[16] = new SqlParameter("@Price", Price);
            param[17] = new SqlParameter("@flg", flg);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Item_Master]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];



        }

        public DataTable Updateinsertsubscription(int id, DateTime date, string item, string Quantity, decimal rate, decimal amount, string period, DateTime ToDate, string subscription_code, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Id", id);
            param[1] = new SqlParameter("@Date", date);
            param[2] = new SqlParameter("@Item", item);
            param[3] = new SqlParameter("@Quantity", Quantity);
            param[4] = new SqlParameter("@Rate", rate);
            param[5] = new SqlParameter("@Amount", amount);
            param[6] = new SqlParameter("@Period", period);
            param[7] = new SqlParameter("@ToDate", ToDate);
            param[8] = new SqlParameter("@subscriber_code", subscription_code);
            param[9] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SubscriptionMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertservicemaster(int serviceid, string service, decimal price, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@service_id_1", serviceid);
            param[1] = new SqlParameter("@service_2", service);
            param[2] = new SqlParameter("@price_3", price);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Services_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertlibraryservices(int invoice_id, string invoice_no, DateTime invoice_date, string library, string member, decimal service_tax, decimal cess, DateTime duedate, decimal actual_amt, decimal total_amt, decimal postage, decimal tax, string userid, string pmt_type, decimal paidAmt, decimal balanceAmt, string DD_chkno, DateTime DD_chkdate, decimal DD_charge, string bank)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@invoice_Id_1", invoice_id);
            param[1] = new SqlParameter("@invoice_no_2", invoice_no);
            param[2] = new SqlParameter("@invoice_date_3", invoice_date);
            param[3] = new SqlParameter("@library_4", library);
            param[4] = new SqlParameter("@member_5", member);
            param[5] = new SqlParameter("@service_tax_6", service_tax);
            param[6] = new SqlParameter("@cess_7", cess);
            param[7] = new SqlParameter("@duedate_8", duedate);
            param[8] = new SqlParameter("@Actual_amt_9", actual_amt);
            param[9] = new SqlParameter("@total_amt_10", total_amt);
            param[10] = new SqlParameter("@postage_11", postage);
            param[11] = new SqlParameter("@tax_12", tax);
            param[12] = new SqlParameter("@userid_13", userid);
            param[13] = new SqlParameter("@Pmt_Type_14", pmt_type);
            param[14] = new SqlParameter("@PaidAmt_15", paidAmt);
            param[15] = new SqlParameter("@BallanceAmt_16", balanceAmt);
            param[16] = new SqlParameter("@DD_ChkNo_17", DD_chkno);
            param[17] = new SqlParameter("@DD_ChkDate_18", DD_chkdate);
            param[18] = new SqlParameter("@DD_charge_19", DD_charge);
            param[19] = new SqlParameter("@Bank_20", bank);


            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_library_servicesMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        // end 
        public DataTable GetUserInformaton(string UserCode, bool Exact, int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserCode", UserCode);
            param[1] = new SqlParameter("@Exact", Exact);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormID", FormID);
            param[4] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetUserInformaton](@UserCode, @Exact, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetLibFiles(string FileName, string Number, bool Exact, int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@FileName", FileName);
            param[1] = new SqlParameter("@Number", Number);
            param[2] = new SqlParameter("@Exact", Exact);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLibFiles](@FileName,@Number, @Exact, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetUserView(string UserID2, string name, string Departmenntname, string classname, string Joinyear, int Program_id, string subjects, string Opac_status, int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UserID2", UserID2);
            param[1] = new SqlParameter("@name", name);
            param[2] = new SqlParameter("@Departmenntname", Departmenntname);
            param[3] = new SqlParameter("@classname", classname);
            param[4] = new SqlParameter("@Joinyear", Joinyear);
            param[5] = new SqlParameter("@Program_id", Program_id);
            param[6] = new SqlParameter("@subjects", subjects);
            param[7] = new SqlParameter("@Opac_status", Opac_status);
            param[8] = new SqlParameter("@UserID", UserID);
            param[9] = new SqlParameter("@FormID", FormID);
            param[10] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetUserView](@UserID2, @name,@Departmenntname, @classname,@Joinyear,@Program_id,@subjects, @Opac_status,        @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable UpdateInsertSymbols(int SymbolTypeId, string SymbolType, string Symbol, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@SymbolTypeId_1", SymbolTypeId);
            param[1] = new SqlParameter("@SymbolType_2", SymbolType);
            param[2] = new SqlParameter("@Symbol_3", Symbol);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Symbols_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GenerateUserid(int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GenerateUserid]()", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetCircClass(int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCircClass](@UserID,@FormID,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        //
        public DataTable GetAcedemicSessionInformation(int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetAcedemicSessionInformation](@UserID,@FormID,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBinderinfo(int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Getbindgrid](@UserID,@FormID,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetcircHolidays(int FormID, int UserID, int Type,DateTime hdate)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@Hdate", hdate);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetcircHolidays](@UserID,@FormID,@Type,@Hdate)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetcircHolidaysMaxID(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetcircHolidaysMAxID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBinderMaxid(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetBindMaxID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetBinderinfoMaxid(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Getbinderinvoice](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable UpdateInsertSubjectMaster(int subject_id, string subject, int userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@subject_id_1", subject_id);
            param[1] = new SqlParameter("@subject_2", subject);
            param[2] = new SqlParameter("@userid_3", userid);
            param[3] = new SqlParameter("@FormID", FormID);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_subject_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertTransalation(int Language_Id, string Language_Name, string Font_Name, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Language_Id_1", Language_Id);
            param[0] = new SqlParameter("@Language_Name_2", Language_Name);
            param[0] = new SqlParameter("@Font_Name_3", Font_Name);
            param[0] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Translation_Language_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertVendorMaster(string vendorid, string vendorcode, string vendorname, string vendorwebaddress, string phone1, string phone2, string emailID, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@vendorid_1", vendorid);
            param[1] = new SqlParameter("@vendorcode_2", vendorcode);
            param[2] = new SqlParameter("@vendorname_3", vendorname);
            param[3] = new SqlParameter("@vendorwebaddress_4", vendorwebaddress);
            param[4] = new SqlParameter("@phone1_5", phone1);
            param[5] = new SqlParameter("@phone2_6", phone2);
            param[6] = new SqlParameter("@emailID_7", emailID);
            param[7] = new SqlParameter("@userid_8", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_vendormaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];



        }



        public DataTable UpdateInsertItemType(int Id, string Item_Type, string Abbreviation, Byte[] Item_icon, string userid, int FormID, int Type)

        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Id_1", Id);
            param[1] = new SqlParameter("@Item_Type_2", Item_Type);
            param[2] = new SqlParameter("@Abbreviation_3", Abbreviation);
            param[3] = new SqlParameter("@Item_icon_4", Item_icon);
            param[4] = new SqlParameter("@userid_5", userid);
            param[5] = new SqlParameter("@FormID", FormID);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Item_Type_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertcdserves(int service_id, string service_name, string description, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Service_id_1", service_id);
            param[1] = new SqlParameter("@Service_Name_2", service_name);
            param[2] = new SqlParameter("@Description_3", description);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_SERVICE_MASTER_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertcdprimarygroup(int levelid, int serviceid, string levelname, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@LEVEL1_ID_1", levelid);
            param[1] = new SqlParameter("@SERVICE_ID_2", serviceid);
            param[2] = new SqlParameter("@LEVEL_NAME_3", levelname);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_SERVICE_LEVEL1_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertcdservicesubgroup(int level2_id, int level1_id, string levelname, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@LEVEL2_ID_1", level2_id);
            param[1] = new SqlParameter("@LEVEL1_ID_2", level1_id);
            param[2] = new SqlParameter("@LEVEL_NAME_3", levelname);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_SERVICE_LEVEL2_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertcdmaster(int id, string cd_title, int level2_id, string description, DateTime add_date, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ID_1", id);
            param[1] = new SqlParameter("@Cd_Title_2", cd_title);
            param[2] = new SqlParameter("@LEVEL2_ID_7", level2_id);
            param[3] = new SqlParameter("@Description_8", description);
            param[4] = new SqlParameter("@add_date_9", add_date);
            param[5] = new SqlParameter("@userid_10", userid);
            param[6] = new SqlParameter("@FormID", FormID);
            param[7] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_MASTER_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }


        public DataTable UpsertDepartmentmMaster(int departmentcode, string departmentname, string shortname, int institutecode, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@DeptCode", departmentcode);
            param[1] = new SqlParameter("@DeptName", departmentname);
            param[2] = new SqlParameter("@Shortname", shortname);
            param[3] = new SqlParameter("@InstCode", institutecode);
            param[4] = new SqlParameter("@UserID", userid);
            param[5] = new SqlParameter("@FormID", FormID);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[SP.AddUpdateDepartment]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }
        // akansha
        public DataTable updateinsertefiles(string DAid, string AccessionNo, DateTime AccessionDate, string Title, string Synopsis, string Type, int DocGroup, string Type_No, string BuildingId, string FloorID, string AlmiraID, string RackId, string Category, int classv, string Receipt_flg, string Rec_daid, int size_id, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@DAid_1", DAid);
            param[1] = new SqlParameter("@AccessionNo_2", AccessionNo);
            param[2] = new SqlParameter("@AccessionDate_3", AccessionDate);
            param[3] = new SqlParameter("@Title_4", Title);
            param[4] = new SqlParameter("@Synopsis_5", Synopsis);
            param[5] = new SqlParameter("@Type_6", Type);
            param[6] = new SqlParameter("@DocGroup_7", DocGroup);
            param[7] = new SqlParameter("@Type_No", Type_No);
            param[8] = new SqlParameter("@BuildingId", BuildingId);
            param[9] = new SqlParameter("@FloorID", FloorID);
            param[10] = new SqlParameter("@AlmiraID", AlmiraID);
            param[11] = new SqlParameter("@RackId", RackId);
            param[12] = new SqlParameter("@Category", Category);
            param[13] = new SqlParameter("@class", classv);
            param[14] = new SqlParameter("@Receipt_flg", Receipt_flg);
            param[15] = new SqlParameter("@Rec_daid", Rec_daid);
            param[16] = new SqlParameter("@size_id", size_id);
            param[17] = new SqlParameter("@userid", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DigitalArchiveInfo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertfilereceipt(string DAid, string Title, DateTime Rec_date, string Challan_No, string Rec_year, DateTime Return_dt, string Register, string status)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DAid", DAid);
            param[1] = new SqlParameter("@Title", Title);
            param[2] = new SqlParameter("@Rec_date", Rec_date);
            param[3] = new SqlParameter("@Challan_No", Challan_No);
            param[4] = new SqlParameter("@Rec_year", Rec_year);
            param[5] = new SqlParameter("@Return_dt", Return_dt);
            param[6] = new SqlParameter("@Register", Register);
            param[7] = new SqlParameter("@status", status);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Receipt_Info]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }



        // Library parameters



        public DataTable updateinsertmaintainacdsession(string academicsession, DateTime startdate, DateTime enddate)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@academicsession_1", academicsession);
            param[1] = new SqlParameter("@startdate_2", startdate);
            param[2] = new SqlParameter("@enddate_3", enddate);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AcedemicSessionInfo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertmultilingualreportoptimizer(string id, string sentence, int inuse, string formtype)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id_1", id);
            param[1] = new SqlParameter("@sentence_2", sentence);
            param[2] = new SqlParameter("@inuse_3", inuse);
            param[3] = new SqlParameter("@formtype_4", formtype);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_MultilingualSentence_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertwindowservise(string Occure, DateTime StartDate, DateTime Enddate, DateTime FireDate, DateTime FireTime, string Day_weeks, string Months)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Occurs_1", Occure);
            param[1] = new SqlParameter("@StartDate_2", StartDate);
            param[2] = new SqlParameter("@EndDate_3", Enddate);
            param[3] = new SqlParameter("@FireDate_4", FireDate);
            param[4] = new SqlParameter("@FireTime_5", FireTime);
            param[5] = new SqlParameter("@Day_weeks_6", Day_weeks);
            param[6] = new SqlParameter("@Months_7", Months);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Window_Ser_2]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        // Security



        public DataTable updateinsertuserdetails(int usertype, string userid, string password, string memberid, string SaltVc, string status1, string ValidUpTo, string IPAddress)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@usertype_1", usertype);
            param[1] = new SqlParameter("@userid_2", userid);
            param[2] = new SqlParameter("@password_3", password);
            param[3] = new SqlParameter("@memberid_4", memberid);
            param[4] = new SqlParameter("@SaltVc_5", SaltVc);
            param[5] = new SqlParameter("@status1", status1);
            param[6] = new SqlParameter("@ValidUpTo", ValidUpTo);
            param[7] = new SqlParameter("@IPAddress", IPAddress);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_userdetails_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }
        // end 

        public DataTable UpdateInsertStockData(string TotalStock, string LostStock, string IssueStock, string BindStock, string ILLStock, string writeOffstock, string Missingstock, string userid, string docid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@TotalStock_1", TotalStock);
            param[1] = new SqlParameter("@LostStock_2", LostStock);
            param[2] = new SqlParameter("@IssueStock_3", IssueStock);
            param[3] = new SqlParameter("@BindStock_4", BindStock);
            param[4] = new SqlParameter("@ILLStock_5", ILLStock);
            param[5] = new SqlParameter("@writeOffstock_6", writeOffstock);
            param[6] = new SqlParameter("@missingstock_7", Missingstock);
            param[7] = new SqlParameter("@userid_8", userid);
            param[8] = new SqlParameter("@docid_9", docid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ItemStatusMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertNewsPaperMaster(int T_id, string Title_N)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@T_id", T_id);
            param[1] = new SqlParameter("@Title_N", Title_N);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaper_T]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateInsertEbookmaster(int id, string Cd_Title, string Startup_file, int ctrl_no, string Description, DateTime add_date, string itemCategory, string userid, string accessionnumber, string Pay_Mode, string Url)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@id_1", id);
            param[1] = new SqlParameter("@Cd_Title_2", Cd_Title);
            param[2] = new SqlParameter("@Startup_file_5", Startup_file);
            param[3] = new SqlParameter("@ctrl_no_6", ctrl_no);
            param[4] = new SqlParameter("@Description_7", Description);
            param[5] = new SqlParameter("@add_date_8", add_date);
            param[6] = new SqlParameter("@itemCategory_9", itemCategory);
            param[7] = new SqlParameter("@userid_10", userid);
            param[8] = new SqlParameter("@accessionnumber_11", accessionnumber);
            param[9] = new SqlParameter("@Pay_Mode_12", Pay_Mode);
            param[10] = new SqlParameter("@id_1", Url);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBOOK_MASTER_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertbookaccessionmaster(string accessionnumber, string ordernumber, string indentnumber, string form, int accessionid, DateTime accessioneddate, string booktitle, int srno, string released, float bookprice, int srNoOld, string biilNo, DateTime billDate, string Item_type, int OriginalPrice, string OriginalCurrency, string userid, string vendor_source, int DeptCode, int DSrno, string DeptName)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@accessionnumber_1", accessionnumber);
            param[1] = new SqlParameter("@ordernumber_2", ordernumber);
            param[2] = new SqlParameter("@indentnumber_3", indentnumber);
            param[3] = new SqlParameter("@form_4", form);
            param[4] = new SqlParameter("@accessionid_5", accessionnumber);
            param[5] = new SqlParameter("@accessioneddate_6", accessioneddate);
            param[6] = new SqlParameter("@booktitle_7", booktitle);
            param[7] = new SqlParameter("@srno_8", srno);
            param[8] = new SqlParameter("@released_9", released);
            param[9] = new SqlParameter("@bookprice_10", bookprice);
            param[10] = new SqlParameter("@srNoOld_11", srNoOld);
            param[11] = new SqlParameter("@biilNo_12", biilNo);
            param[12] = new SqlParameter("@billDate_13", billDate);
            param[13] = new SqlParameter("@Item_type_14", Item_type);
            param[14] = new SqlParameter("@OriginalPrice_15", OriginalPrice);
            param[15] = new SqlParameter("@OriginalCurrency_16", OriginalCurrency);
            param[16] = new SqlParameter("@vendor_source_18", vendor_source);
            param[17] = new SqlParameter("@DeptCode_19", DeptCode);
            param[18] = new SqlParameter("@DSrno_20", DSrno);
            param[19] = new SqlParameter("@DeptName_21", DeptName);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[bookaccessionmaster]   ", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }

        public DataTable UpsertCategoryLoadingStatus(int ItemID, string ItemName, string Abbrev, int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ItemID", ItemID);
            param[1] = new SqlParameter("@ItemName", ItemName);
            param[2] = new SqlParameter("@Abbrev", Abbrev);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateCatelogLoadingStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable GetCategoryLoading(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[Fn_GetCategoryLoading  ](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        /*        public DataTable GetCategoryLoadingStatus(int UserID, int FormID, int Type)
                {
                    DataTable dt = new DataTable();
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@UserID", UserID);
                    param[1] = new SqlParameter("@FormID", FormID);
                    param[2] = new SqlParameter("@Type", Type);
                    //param[0].SqlDbType = SqlDbType.Int;
                    EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
                    sqlDataBase.ExceFunc(" SELECT * FROM   [MTR].[  ](@UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
                    return dt;
                }
        wrong and dropped */

        public DataTable UpdateInsertCirclIssueBooks(string userid, string accno, DateTime issuedate, DateTime duedate, string status, string userid1, int IssueId)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@accno_2", accno);
            param[2] = new SqlParameter("@issuedate_3", issuedate);
            param[3] = new SqlParameter("@duedate_4", duedate);
            param[4] = new SqlParameter("@status_5", status);
            param[5] = new SqlParameter("@userid1_7", userid1);
            param[6] = new SqlParameter("@IssueId_8", IssueId);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[CircIssueTransaction]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable UpdateInsertsubject(int subject_id, string subject, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@subject_id_1", subject_id);
            param[1] = new SqlParameter("@subject_2", subject);
            param[2] = new SqlParameter("@userid_3", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_subject_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertTranslation(int Language_Id, string Language_Name, string Font_Name, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Language_Id_1", Language_Id);
            param[1] = new SqlParameter("@Language_Name_2", Language_Name);
            param[2] = new SqlParameter("@Font_Name_3", Font_Name);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Translation_Language_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertStandardReply(int reply_id, string reply, string user_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@reply_id_1", reply_id);
            param[1] = new SqlParameter("@reply_2", reply);
            param[2] = new SqlParameter("@user_id_3", user_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_standard_reply_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable GetAllFingerPrints(int FormID, int UserID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetAllFingerPrints]()", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateUserStatus(string UserId, string Status)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", UserId);
            param[1] = new SqlParameter("@Status", Status);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateUserStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateUserOpacStatus(string UserId, string OpacStatus)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", UserId);
            param[1] = new SqlParameter("@Status", OpacStatus);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateUserOpacStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable DeleteRfidLog(int AntNum)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@AntNum", AntNum);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteRfidLog]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable DeleteCircUser(string UserId)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", UserId);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteCircUser]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GetCircUserAddress(string UserID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@UserID", UserID);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetCircUserAddress](@UserID)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetLibrary(string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLibrary](@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetSearchEDocs(int UserID, int FormId, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetSearchEDocs](@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetFeaturePer(int ID, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@Id", ID);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetFeaturePer](@Id,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        //
        public DataTable CheckDupMember(string firstname, string middlename, string lastname, string Fathername, string mothername, string userid)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@firstname", firstname);
            param[1] = new SqlParameter("@middlename", middlename);
            param[2] = new SqlParameter("@lastname", lastname);
            param[3] = new SqlParameter("@Fathername", Fathername);
            param[4] = new SqlParameter("@mothername", mothername);
            param[5] = new SqlParameter("@userid", userid);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[CheckDupMember](@firstname,@middlename,@lastname,@Fathername,@mothername,@userid)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateMemberStatus(string UserId, string Status)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", UserId);
            param[1] = new SqlParameter("@Status", Status);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateMemberStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateMemberOpacStatus(string UserId, string OpacStatus)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", UserId);
            param[1] = new SqlParameter("@OpacStatus", OpacStatus);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateMemberOpacStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GetClassmasterLoadingStatus(string ClassName, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetClassmasterLoadingStatus](@ClassName,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable DeleteClassmasterLoadingStatus(string ClassName, string loadingstatus, string UserId, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@loadingstatus", loadingstatus);
            param[2] = new SqlParameter("@UserId", UserId);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteClassmasterLoadingStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable DeleteTempClassmasterLoadingStatus(string ClassName, string loadingstatus, string UserId, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@loadingstatus", loadingstatus);
            param[2] = new SqlParameter("@UserId", UserId);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteTempClassmasterLoadingStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GetTempClassmasterLoadingStatus(string ClassName, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetTempClassmasterLoadingStatus](@ClassName,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }


        public DataTable DeleteCircClass(string ClassName, string UserId, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@UserId", UserId);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteCircClass]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        // akansha

        public DataTable updateinsertstockentryform(string TotalStock, string LostStock, string IssueStock, string BindStock, string ILLStock, string writeOffstock, string Missingstock, string userid, string docid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@TotalStock_1", TotalStock);
            param[1] = new SqlParameter("@LostStock_2", LostStock);
            param[2] = new SqlParameter("@IssueStock_3", IssueStock);
            param[3] = new SqlParameter("@BindStock_4", BindStock);
            param[4] = new SqlParameter("@ILLStock_5", ILLStock);
            param[5] = new SqlParameter("@writeOffstock_6", writeOffstock);
            param[6] = new SqlParameter("@MissingStock_7", Missingstock);
            param[7] = new SqlParameter("@userid_8", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_StockData_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertstockmanagement(string taskid, string taskname, DateTime startdate, DateTime enddate, string status, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@taskid_1", taskid);
            param[1] = new SqlParameter("@taskname_2", taskname);
            param[2] = new SqlParameter("@startdate_3", startdate);
            param[3] = new SqlParameter("@enddate_4", enddate);
            param[4] = new SqlParameter("@status_5", status);
            param[5] = new SqlParameter("@userid_6", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_stock_mgmt_mst_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertstockmanagement(string TotalAcc, string LostAcc, string IssuedAcc, string BindAcc, string LibLAcc, string userid, string availtaskAcc, string WriteoffAcc)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@TotalAcc_2", TotalAcc);
            param[1] = new SqlParameter("@LostAcc_3", LostAcc);
            param[2] = new SqlParameter("@IssuedAcc_4", IssuedAcc);
            param[3] = new SqlParameter("@BindAcc_5", BindAcc);
            param[4] = new SqlParameter("@LibLAcc_6", LibLAcc);
            param[5] = new SqlParameter("@userid_7", userid);
            param[6] = new SqlParameter("@availtaskAcc_8", availtaskAcc);
            param[7] = new SqlParameter("@WriteoffAcc_9", WriteoffAcc);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_TaskReport_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        //
        public DataTable updateExhangemaster(int CurrencyCode, string ShortName, string CurrencyName, decimal GocRate, decimal BankRate, DateTime EffectiveFrom, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@CurrencyCode_1", CurrencyCode);
            param[1] = new SqlParameter("@ShortName_2", ShortName);
            param[2] = new SqlParameter("@CurrencyName_3", CurrencyName);
            param[3] = new SqlParameter("@GocRate_4", GocRate);
            param[4] = new SqlParameter("@BankRate_5", BankRate);
            param[5] = new SqlParameter("@EffectiveFrom_6", EffectiveFrom);
            param[6] = new SqlParameter("@userid_7", userid);
            param[7] = new SqlParameter("@FormID", FormID);
            param[8] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_exchangemaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertadmin(string SMSLink, string UserId, string Passwd)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SMSLink", SMSLink);
            param[1] = new SqlParameter("@UserId", UserId);
            param[2] = new SqlParameter("@Passwd", Passwd);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SMSSettings]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        //Acquisitioning



        public DataTable Updateinsertindent(string UserID, DateTime OperationDate, DateTime OperationTime, string IndentNo, DateTime IndentDate, decimal ExchangeRate, int NoofCopies, decimal price, decimal amount, string Opration, string AffectedObjects)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UserID_1", UserID);
            param[1] = new SqlParameter("@OperationDate_2", OperationDate);
            param[2] = new SqlParameter("@OperationTime_3", OperationTime);
            param[3] = new SqlParameter("@IndentNo_4", IndentNo);
            param[4] = new SqlParameter("@IndentDate_5", IndentDate);
            param[5] = new SqlParameter("@ExchangeRate_6", ExchangeRate);
            param[6] = new SqlParameter("@NoofCopies_7", NoofCopies);
            param[7] = new SqlParameter("@price_8", price);
            param[8] = new SqlParameter("@amount_9", amount);
            param[9] = new SqlParameter("@Operation_10", Opration);
            param[10] = new SqlParameter("@AffectedObjects_11", AffectedObjects);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_IndentAudit_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertgiftindent(string ordernumber, string indentnumber, DateTime orderdate, string letternumber, DateTime letterdate, byte cancelorder, int itemnumber, decimal orderamount, string vendorid, int identityofordernumber, int order_check_code, int departmentcode)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@ordernumber_1", ordernumber);
            param[1] = new SqlParameter("@indentnumber_2", indentnumber);
            param[2] = new SqlParameter("@orderdate_3", orderdate);
            param[3] = new SqlParameter("@letternumber_4", letternumber);
            param[4] = new SqlParameter("@letterdate_5", letterdate);
            param[5] = new SqlParameter("@cancelorder_6", cancelorder);
            param[6] = new SqlParameter("@itemnumber_7", itemnumber);
            param[7] = new SqlParameter("@orderamount_8", orderamount);
            param[8] = new SqlParameter("@vendorid_9", vendorid);
            param[9] = new SqlParameter("@identityofordernumber_10", identityofordernumber);
            param[10] = new SqlParameter("@order_check_code_11", order_check_code);
            param[11] = new SqlParameter("@departmentcode_12", departmentcode);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_giftordermaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertreindent(string indentnumber, DateTime indentdate, int mediatype, string requestercode, int departmentcode, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int currencycode, int go_bank, decimal exchangerate, int noofcopies, string approval, decimal price, decimal totalamount, string coursenumber, int noofstudents, string publisherid, string vendorid, DateTime recordingdate, string gifted, string indenttype, DateTime indenttime, string seriesname, int order_check_code, string yearofPublication, string isSatnding, string IndentId, string Vpart, string ItemNo, string subtitle, int Language_Id, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[46];
            param[0] = new SqlParameter("@indentnumber_1", indentnumber);
            param[1] = new SqlParameter("@indentdate_2", indentdate);
            param[2] = new SqlParameter("@mediatype_3", mediatype);
            param[3] = new SqlParameter("@requestercode_4", requestercode);
            param[4] = new SqlParameter("@departmentcode_5", departmentcode);
            param[5] = new SqlParameter("@title_6", title);
            param[6] = new SqlParameter("@authortype_7", authortype);
            param[7] = new SqlParameter("@firstname1_8", firstname1);
            param[8] = new SqlParameter("@middlename1_9", middlename2);
            param[9] = new SqlParameter("@lastname1_10", lastname2);
            param[10] = new SqlParameter("@firstname2_11", firstname2);
            param[11] = new SqlParameter("@middlename2_12", middlename2);
            param[12] = new SqlParameter("@lastname2_13", lastname2);
            param[13] = new SqlParameter("@firstname3_14", firstname3);
            param[14] = new SqlParameter("@middlename3_15", middlename3);
            param[15] = new SqlParameter("@lastname3_16", lastname3);
            param[16] = new SqlParameter("@edition_17", edition);
            param[17] = new SqlParameter("@yearofedition_18", yearofedition);
            param[18] = new SqlParameter("@volumeno_19", volumeno);
            param[19] = new SqlParameter("@isbn_20", isbn);
            param[20] = new SqlParameter("@category_21", category);
            param[21] = new SqlParameter("@currencycode_22", currencycode);
            param[22] = new SqlParameter("@go_bank_23", go_bank);
            param[23] = new SqlParameter("@exchangerate_24", exchangerate);
            param[24] = new SqlParameter("@noofcopies_25", noofcopies);
            param[25] = new SqlParameter("@approval_26", approval);
            param[26] = new SqlParameter("@price_27", price);
            param[27] = new SqlParameter("@totalamount_28", totalamount);
            param[28] = new SqlParameter("@coursenumber_29", coursenumber);
            param[29] = new SqlParameter("@noofstudents_30", noofstudents);
            param[30] = new SqlParameter("@publisherid_31", noofstudents);
            param[31] = new SqlParameter("@vendorid_32", noofstudents);
            param[32] = new SqlParameter("@recordingdate_33", noofstudents);
            param[33] = new SqlParameter("@gifted_34", noofstudents);
            param[34] = new SqlParameter("@indenttype_35", noofstudents);
            param[35] = new SqlParameter("@indenttime_36", noofstudents);
            param[36] = new SqlParameter("@seriesname_37", noofstudents);
            param[37] = new SqlParameter("@order_check_code_38", noofstudents);
            param[38] = new SqlParameter("@yearofPublication_39", noofstudents);
            param[39] = new SqlParameter("@isSatnding_40", noofstudents);
            param[40] = new SqlParameter("@IndentId_41", noofstudents);
            param[41] = new SqlParameter("@Vpart_42", noofstudents);
            param[42] = new SqlParameter("@ItemNo_43", noofstudents);
            param[43] = new SqlParameter("@subtitle_44", noofstudents);
            param[44] = new SqlParameter("@Language_Id_45", noofstudents);
            param[45] = new SqlParameter("userid_46", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_indentmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }


        // cerculation--> reservation


        public DataTable updateinsertbookreservation(string userid, int totalreservations)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@totalreservations_2", totalreservations);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circuserreservations_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }


        // master--> dynamic Id-card


        public DataTable updateinsertprintpagesize(int Id, string pageSizeName, decimal pageHeight, decimal pageWidth, int pRow, int pColumn)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@pageSizeName", pageSizeName);
            param[2] = new SqlParameter("@pageHeight", pageHeight);
            param[3] = new SqlParameter("@pageWidth", pageWidth);
            param[4] = new SqlParameter("@pRow", pRow);
            param[5] = new SqlParameter("@pColumn", pColumn);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_dynamic_PageSize]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertprintpagesize(int ctrlNo, int quenumber)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ctrlNo_1", ctrlNo);
            param[1] = new SqlParameter("@quenumber_2", quenumber);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circBookQue_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertdynamicidcard(int Id, string formatName, decimal height, decimal width, string roundedCorner, string variableBackSide, string frontDesign, string backDesign, string authorisedSignatory, string authorisedSignatory2)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@formatName", formatName);
            param[2] = new SqlParameter("@height", height);
            param[3] = new SqlParameter("@width", width);
            param[4] = new SqlParameter("@roundedCorner", roundedCorner);
            param[5] = new SqlParameter("@variableBackSide", variableBackSide);
            param[6] = new SqlParameter("@frontDesign", frontDesign);
            param[7] = new SqlParameter("@backDesign", backDesign);
            param[8] = new SqlParameter("@authorisedSignatory", authorisedSignatory);
            param[9] = new SqlParameter("@authorisedSignatory2", authorisedSignatory2);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_DynamicIDcard_Formats]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        // end 

        public DataTable UpdateInsertCircleissuemaster(string userid, int currentissuedbooks, string userid1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@currentissuedbooks_2", currentissuedbooks);
            param[2] = new SqlParameter("@userid1_3", userid1);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircIssueMaster_1]  ", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertCatalogCardPrint(string AccessionNumber, string ClassNumber, string BookNumber, string accesspoints, string Contents, string CardTitle, string Volume, string Copy, int id, string Tag)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccessionNumber_1", AccessionNumber);
            param[1] = new SqlParameter("@ClassNumber_2", ClassNumber);
            param[2] = new SqlParameter("@BookNumber_3", BookNumber);
            param[3] = new SqlParameter("@accesspoints_4", accesspoints);
            param[4] = new SqlParameter("@Contents_5", Contents);
            param[5] = new SqlParameter("@CardTitle_6", CardTitle);
            param[6] = new SqlParameter("@Volume_7", Volume);
            param[7] = new SqlParameter("@Copy_8", Copy);
            param[8] = new SqlParameter("@id_9", id);
            param[9] = new SqlParameter("@Tag_10", Tag);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircIssueMaster_1]  ", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertBuildingMaster(int BuildingId, string BuildingCode, string BuildingName, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@BuildingId_1", BuildingId);
            param[1] = new SqlParameter("@BuildingCode_2", BuildingCode);
            param[2] = new SqlParameter("@BuildingName_3", BuildingName);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BuildingMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertjournalinvoicemaster(string Invoice_id, string Invoice_No, DateTime Invoice_Date, string Order_Id, string Publisher_Code, string Currency, string Exchange_Rate, string PostageCharge, string Relation, string BillSerial_No, string Status, string Total_Amount, string ref_invoice_no, string Amount_Local, string pay_currency, string credit_amount, string credit_no, string Curr_code, string userid, string New_InvNo)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@Invoice_No_2", Invoice_No);
            param[2] = new SqlParameter("@Invoice_Date_3", Invoice_Date);
            param[3] = new SqlParameter("@Order_Id_4", Order_Id);
            param[4] = new SqlParameter("@Publisher_Code_5", Publisher_Code);
            param[5] = new SqlParameter("@Currency_6", Currency);
            param[6] = new SqlParameter("@Exchange_Rate_7", Exchange_Rate);
            param[7] = new SqlParameter("@PostageCharge_8", PostageCharge);
            param[8] = new SqlParameter("@Relation_9", Relation);
            param[9] = new SqlParameter("@BillSerial_No_10", BillSerial_No);
            param[10] = new SqlParameter("@Status_11", Status);
            param[11] = new SqlParameter("@Total_Amount_12", Total_Amount);
            param[12] = new SqlParameter("@ref_invoice_no_13", ref_invoice_no);
            param[13] = new SqlParameter("@Amount_Local_14", Amount_Local);
            param[14] = new SqlParameter("@pay_currency_15", pay_currency);
            param[15] = new SqlParameter("@credit_amount_16", credit_amount);
            param[16] = new SqlParameter("@credit_no_17", credit_no);
            param[17] = new SqlParameter("@Curr_code_18", Curr_code);
            param[18] = new SqlParameter("@userid_19", userid);
            param[19] = new SqlParameter("@New_InvNo", New_InvNo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourInvoice_masterN_1] ", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertjournalPaymentMaster(int paymentid, int draftnumber, DateTime draftdate, string bankname, float amount, string paymenttype, int vendorid, DateTime paymentdate, float draft_amt, string documentno, float draftrate, string Relation, string userid, string swift_code, string account_nm, string IBAN_no, string bank_Address, string purpose, string Letter_Members)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@paymentid_1", paymentid);
            param[1] = new SqlParameter("@draftnumber_2", draftnumber);
            param[2] = new SqlParameter("@draftdate_3", draftdate);
            param[3] = new SqlParameter("@bankname_4", bankname);
            param[4] = new SqlParameter("@amount_5", amount);
            param[5] = new SqlParameter("@paymenttype_6", paymenttype);
            param[6] = new SqlParameter("@vendorid_7", vendorid);
            param[7] = new SqlParameter("@paymentdate_8", paymentdate);
            param[8] = new SqlParameter("@draft_amt_9", draft_amt);
            param[9] = new SqlParameter("@documentno_10", documentno);
            param[10] = new SqlParameter("@draftrate_11", draftrate);
            param[11] = new SqlParameter("@Relation_12", Relation);
            param[12] = new SqlParameter("@userid_13", userid);
            param[13] = new SqlParameter("@swift_code", swift_code);
            param[14] = new SqlParameter("@IBAN_no", IBAN_no);
            param[15] = new SqlParameter("@bank_Address", bank_Address);
            param[16] = new SqlParameter("@purpose", purpose);
            param[17] = new SqlParameter("@Letter_Members", Letter_Members);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_paymentmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }
        public DataTable UpdateInsertjournalcreditnotepayment(int paymentid, int draftnumber, DateTime draftdate, string bankname, float amount, string paymenttype, int vendorid, DateTime paymentdate, float draft_amt, string documentno, float draftrate, string Relation, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@paymentid_1", paymentid);
            param[1] = new SqlParameter("@draftnumber_2", draftnumber);
            param[2] = new SqlParameter("@draftdate_3", draftdate);
            param[3] = new SqlParameter("@bankname_4", bankname);
            param[4] = new SqlParameter("@amount_5", amount);
            param[5] = new SqlParameter("@paymenttype_6", paymenttype);
            param[6] = new SqlParameter("@vendorid_7", vendorid);
            param[7] = new SqlParameter("@paymentdate_8", paymentdate);
            param[8] = new SqlParameter("@draft_amt_9", draft_amt);
            param[9] = new SqlParameter("@documentno_10", documentno);
            param[10] = new SqlParameter("@draftrate_11", draftrate);
            param[11] = new SqlParameter("@Relation_12", Relation);
            param[12] = new SqlParameter("@userid_13", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_paymentmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable UpdateInsertJournalMaster(string Member_Id, string Journal_No, string Volume, string Issue, string Part, float Fineamount, string Finecause, string isPaid, DateTime ReceiveDate, DateTime Publicationdate, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Member_Id_1", Member_Id);
            param[1] = new SqlParameter("@Journal_No_2", Journal_No);
            param[2] = new SqlParameter("@Issue_4", Volume);
            param[3] = new SqlParameter("@Part_5", Issue);
            param[4] = new SqlParameter("@Fineamount_6", Fineamount);
            param[5] = new SqlParameter("@Finecause_7", Finecause);
            param[6] = new SqlParameter("@isPaid_8", isPaid);
            param[7] = new SqlParameter("@ReceiveDate_9", ReceiveDate);
            param[8] = new SqlParameter("@publicationdate_10", Publicationdate);
            param[9] = new SqlParameter("@userid_11", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalReceive_Arrival_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }


        public DataTable UpdateInsertauditmaster(string UserID, string Process, DateTime OperationDate, DateTime OperationTime, string Operation, string Expenditure, float CommitMentApp, float CommitMentNApp, float IndentsApp, float IndentNapp, string DocumentNo)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UserID_1", UserID);
            param[1] = new SqlParameter("@Process_2", Process);
            param[2] = new SqlParameter("@OperationDate_3", OperationDate);
            param[3] = new SqlParameter("@OperationTime_4", OperationTime);
            param[4] = new SqlParameter("@Operation_5", Operation);
            param[5] = new SqlParameter("@Expenditure_6", Expenditure);
            param[6] = new SqlParameter("@CommitMentApp_7", CommitMentApp);
            param[7] = new SqlParameter("@CommitMentNApp_8", CommitMentNApp);
            param[8] = new SqlParameter("@IndentsApp_9", IndentNapp);
            param[9] = new SqlParameter("@IndentNapp_10", IndentNapp);
            param[10] = new SqlParameter("@DocumentNo_11", DocumentNo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAudit_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertNewpaperinvoiceMaster(string Invoice_id, string Invoice_No, DateTime Invoice_Date, string vendorid, float totalamount, string status, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@Invoice_No_2", Invoice_No);
            param[2] = new SqlParameter("@Invoice_Date_3", Invoice_Date);
            param[3] = new SqlParameter("@vendorid_4", vendorid);
            param[4] = new SqlParameter("@totalamount_5", totalamount);
            param[5] = new SqlParameter("@status_6", status);
            param[6] = new SqlParameter("@userid_7", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperInvoice]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertunioninsertsettings(string LibraryName, string DatabaseName, string ServerName, string ConnectionStringName, string ConnectionString, string Status, int PID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@LibraryName", LibraryName);
            param[1] = new SqlParameter("@DatabaseName", DatabaseName);
            param[2] = new SqlParameter("@ServerName", ServerName);
            param[3] = new SqlParameter("@ConnectionStringName", ConnectionStringName);
            param[4] = new SqlParameter("@ConnectionString", ConnectionString);
            param[5] = new SqlParameter("@Status", Status);
            param[6] = new SqlParameter("@PID", PID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_UnionLibSettings]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinserttempclsloadstatus(string classname, int totalissueddays, int noofbookstobeissued, float finperday, int reservedays, int totalissueddays_jour, int noofjournaltobeissued, float fineperday_jour, int reservedays_jour, string Status, int ValueLimit, int days_1phase, float amt_1phase, int days_2phase, float amt_2phase, int days_1phasej, float amt_1phasej, int days_2phasej, float amt_2phasej, string shortname, string loadingstatus)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@classname_1", classname);
            param[1] = new SqlParameter("@totalissueddays_2", totalissueddays);
            param[2] = new SqlParameter("@noofbookstobeissued_3", noofbookstobeissued);
            param[3] = new SqlParameter("@reservedays_5", reservedays);
            param[4] = new SqlParameter("@totalissueddays_jour_6", totalissueddays_jour);
            param[5] = new SqlParameter("@noofjournaltobeissued_7", noofjournaltobeissued);
            param[6] = new SqlParameter("@fineperday_jour_8", fineperday_jour);
            param[7] = new SqlParameter("@reservedays_jour_9", reservedays_jour);
            param[8] = new SqlParameter("@Status_10", Status);
            param[9] = new SqlParameter("@ValueLimit_11", ValueLimit);
            param[10] = new SqlParameter("@days_1phase_12", days_1phase);
            param[11] = new SqlParameter("@amt_1phase_13", amt_1phase);
            param[12] = new SqlParameter("@days_2phase_14", days_2phase);
            param[13] = new SqlParameter("@amt_2phase_15", amt_2phase);
            param[14] = new SqlParameter("@days_1phasej_16", days_2phasej);
            param[15] = new SqlParameter("@amt_1phasej_17", amt_1phasej);
            param[16] = new SqlParameter("@days_2phasej_18", days_1phasej);
            param[17] = new SqlParameter("@amt_2phasej_19", amt_2phasej);
            param[18] = new SqlParameter("@shortname_20", shortname);
            param[20] = new SqlParameter("@loadingstatus_21", loadingstatus);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Tempclsmstloadstatus_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }



        public DataTable GetIdTable(string ObjectName, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ObjectName", ObjectName);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIdTable](@ObjectName,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetVendor(string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetVendor](@ObjectName,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable Getbinderinfo(string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Getbinderinfomation](@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBindType1(int UserID, int FormId, int Type, string bindname)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@bindtypename", bindname);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetBindType1](@UserID,@FormId,@Type,@bindtypename)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetPublisherAddress(string Firstname, string Percity, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Firstname", Firstname);
            param[1] = new SqlParameter("@Percity", Percity);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetPublisherAddress](@Firstname,@Percity,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetVendorAddress(string Vendorname, string Percity, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Vendorname", Vendorname);
            param[1] = new SqlParameter("@Percity", Percity);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetVendorAddress](@Vendorname,@Percity,@UserID,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetMaxVendorid()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetMaxVendorid](   )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetMaxPublisherid()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetMaxPublisherid](   )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetPublisherAddressDetail(int Publisherid, string Publishercode, string Firstname, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Publisherid", Publisherid);
            param[1] = new SqlParameter("@Publishercode", Publishercode);
            param[2] = new SqlParameter("@Firstname", Firstname);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetPublisherAddressDetail](@Publisherid,@Publishercode,@Firstname  ,@UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable DeletePublisher(int Publisherid, string Publishercode, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Publisherid", Publisherid);
            param[1] = new SqlParameter("@Publishercode", Publishercode);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[DeletePublisher]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetVendorAddressDetail(int VendorId, string VendorCode, string VendorName, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@VendorId", VendorId);
            param[1] = new SqlParameter("@VendorCode", VendorCode);
            param[2] = new SqlParameter("@VendorName", VendorName);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetVendorAddressDetail](@VendorId,@VendorCode,@VendorName  ,@UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetEdocSuggesestion(int UserID, int FormID, int Type, string accno, string grpcat,string servicename, string grdservice, int serviceid)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@checkaccok", accno);
            param[4] = new SqlParameter("@groupcategory", grpcat);
            param[5] = new SqlParameter("@servicename", servicename);
            param[6] = new SqlParameter("@grdservice", grdservice);
            param[7] = new SqlParameter("@serviceid", serviceid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM [MTR].[GETEDOCUMENT_GROUP](@UserID,@FormId,@Type, @checkaccok,@groupcategory,@servicename,@grdservice,@serviceid)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable UpdateInsertUserTypeLevel3(int UserTypeId, int SubMenu_Id, string Permission)
        {
            DataTable dt = new DataTable();
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserTypeId_1", UserTypeId);
            param[1] = new SqlParameter("@SubMenu_Id_2", SubMenu_Id);
            param[2] = new SqlParameter("@Permission_3", Permission);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypeLavel3_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertImageGallerySubEvent(int Id, int PEventId, DateTime SubEventDateFrom, DateTime SubEventDateTo, string SubEventNote, string SubEventName, DateTime PhotographyDate, string PhotographerName, string Places)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@PEventId", PEventId);
            param[2] = new SqlParameter("@SubEventDateFrom", SubEventDateFrom);
            param[3] = new SqlParameter("@SubEventDateTo", SubEventDateTo);
            param[4] = new SqlParameter("@SubEventNote", SubEventNote);
            param[5] = new SqlParameter("@SubEventName", SubEventName);
            param[6] = new SqlParameter("@PhotographyDate", PhotographyDate);
            param[7] = new SqlParameter("@PhotographerName", PhotographerName);
            param[8] = new SqlParameter("@Places", Places);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ImageGallery_SubEvents]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertUserTypeLevel2(int UserTypeId, int MidMenu_Id, string Permission)
        {
            DataTable dt = new DataTable();
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserTypeId_1", UserTypeId);
            param[1] = new SqlParameter("@SubMenu_Id_2", MidMenu_Id);
            param[2] = new SqlParameter("@Permission_3", Permission);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypeLavel2_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertCircWriteoffEntry(string DocumentNo, DateTime writeoffdate, string accessionnumber, string Cause, string Note, string Status, decimal price, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DocumentNo_1", DocumentNo);
            param[1] = new SqlParameter("@writeoffdate_2", writeoffdate);
            param[2] = new SqlParameter("@accessionnumber_3", accessionnumber);
            param[3] = new SqlParameter("@Cause_4", Cause);
            param[4] = new SqlParameter("@Note_5", Note);
            param[5] = new SqlParameter("@Status_7", Status);
            param[6] = new SqlParameter("@price_71", price);
            param[7] = new SqlParameter("@userid_8", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circWriteoffentry_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertLostBookRecovery(string accessionnumber, DateTime recoverydate, string Paymode, int amount, string RecoveryMode, string Notes, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@DocumentNo_1", accessionnumber);
            param[1] = new SqlParameter("@writeoffdate_2", recoverydate);
            param[2] = new SqlParameter("@accessionnumber_3", Paymode);
            param[3] = new SqlParameter("@Cause_4", amount);
            param[4] = new SqlParameter("@Note_5", RecoveryMode);
            param[5] = new SqlParameter("@Status_7", Notes);
            param[6] = new SqlParameter("@price_71", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circLostBookRecovery_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertImageGalleryEvent(int Id, string EventName, string userid)
        {
            DataTable dt = new DataTable();
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserTypeId_1", Id);
            param[1] = new SqlParameter("@SubMenu_Id_2", EventName);
            param[2] = new SqlParameter("@Permission_3", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ImageGallery_Events]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertCircReceiveTransaction(string userid, string accno, DateTime receivingdate, decimal fineamount, string fineCause, string isPaid, DateTime Dueon, DateTime paidOn, string amtexp, string userid1, int issue_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@accno_2", accno);
            param[2] = new SqlParameter("@receivingdate_3", receivingdate);
            param[3] = new SqlParameter("@fineamount_4", fineamount);
            param[4] = new SqlParameter("@fineCause_5", fineCause);
            param[5] = new SqlParameter("@isPaid_6", isPaid);
            param[6] = new SqlParameter("@Dueon_7", Dueon);
            param[7] = new SqlParameter("@paidOn_8", paidOn);
            param[8] = new SqlParameter("@amtexp_9", amtexp);
            param[9] = new SqlParameter("@userid1_10", userid1);
            param[10] = new SqlParameter("@issue_id", issue_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circRecTransNDB_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertLostBookEntry(string accessionnumber, string userid, int Bookprice, DateTime PayDate, string payStatus, string receiptno, string entryCategory, string NewAccno, DateTime EntryDate, string Recovered, string userid1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@accessionnumber_1", accessionnumber);
            param[1] = new SqlParameter("@userid_2", userid);
            param[2] = new SqlParameter("@Bookprice_3", Bookprice);
            param[3] = new SqlParameter("@PayDate_4", PayDate);
            param[4] = new SqlParameter("@payStatus_5", payStatus);
            param[5] = new SqlParameter("@receiptno_6", receiptno);
            param[6] = new SqlParameter("@entryCategory_7", entryCategory);
            param[7] = new SqlParameter("@NewAccno_8", NewAccno);
            param[8] = new SqlParameter("@EntryDate_9", EntryDate);
            param[9] = new SqlParameter("@Recovered_10", Recovered);
            param[10] = new SqlParameter("@userid1_11", userid1);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circLostBookentry_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        //By HariOm

        public DataTable updateinsertaccessionindent(string indentnumber, int accessionedcopies)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@indentnumber_1", indentnumber);
            param[1] = new SqlParameter("@accessionedcopies_2", accessionedcopies);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AccessionedIndent_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinserttempaccesspoints(int id, string Title, string Tag)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id_2", id);
            param[1] = new SqlParameter("@Title_1", Title);
            param[2] = new SqlParameter("@Tag_3", Tag);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_tempAccespoints_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateccessionmaster(int ctrl_no, int Copy_no, int year_edition, string accessionnumber, string pubYear, float bookprice, int specialprice)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ctrl_no", ctrl_no);
            param[1] = new SqlParameter("@Copy_no", Copy_no);
            param[2] = new SqlParameter("@year_edition", year_edition);
            param[4] = new SqlParameter("@accessionnumber", accessionnumber);
            param[5] = new SqlParameter("@pubYear", pubYear);
            param[6] = new SqlParameter("@bookprice", bookprice);
            param[7] = new SqlParameter("specialprice", specialprice);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_new_accessionmaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinsertfloormaster(int FloorId, string FloorCode, string FloorName, string userid, string building_code)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@FloorId_1", FloorId);
            param[1] = new SqlParameter("@FloorCode_2", FloorCode);
            param[2] = new SqlParameter("@FloorName_3", FloorName);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@building_code", building_code);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_FloorMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertfeatureper(string Features, int FID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Features", Features);
            param[1] = new SqlParameter("@FID", FID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_FeaturePer]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinsertrackmaster(int RackId, string RackCode, string RackName, string userid, string almira_code)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RackId_1", RackId);
            param[1] = new SqlParameter("@RackCode_2", RackCode);
            param[2] = new SqlParameter("@RackName_3", RackName);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@almira_code", almira_code);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RackMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        //akansha

        public DataTable Updateinsertlibraryservices(int Guest_id, string name, string E_mail, string phone, string Address, string Affiliation)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Guest_id_1", Guest_id);
            param[1] = new SqlParameter("@name_2", name);
            param[2] = new SqlParameter("@E_mail_3", E_mail);
            param[3] = new SqlParameter("@phone_4", phone);
            param[4] = new SqlParameter("@Address_5", Address);
            param[5] = new SqlParameter("@Affiliation_6", Affiliation);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Lib_serv_GuestInfo]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }

        public DataTable Updateinsertdispatchinvoice(int PaymentID, string InvoiceID, decimal InvoiceAmount)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PaymentID_1", PaymentID);
            param[1] = new SqlParameter("@InvoiceID_2", InvoiceID);
            param[2] = new SqlParameter("@InvoiceAmount_3", InvoiceAmount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_jou_letteraccPmtchild_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }


        // Library parameter


        public DataTable updateinsertadmin(string BoardName, int BoardId, string DefaultCount, int usertypeid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserTypeId", usertypeid);
            param[1] = new SqlParameter("@BoardName", BoardName);
            param[2] = new SqlParameter("@BoardId", BoardId);
            param[3] = new SqlParameter("@DefaultCount", DefaultCount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_dashBoardSettings]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable DeleteEdocument(int serviceid, int UserID, int FormID, int Type)
        {
           
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Service_id", serviceid);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormID", FormID);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteEDocument]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertcataloguedetail(int Id, string Title, string Tag)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Title_1", Id);
            param[1] = new SqlParameter("@id_2", Title);
            param[2] = new SqlParameter("@Tag_3", Tag);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_tempAccespoints_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertsecuritymnu(string Message, DateTime SendDate, string Sendby, string SendTo, string Status, string PageName, DateTime ScheduleDate, string ScheduleGroup)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Message", Message);
            param[1] = new SqlParameter("@SendDate", SendDate);
            param[2] = new SqlParameter("@SendBy", Sendby);
            param[3] = new SqlParameter("@SendTo", SendTo);
            param[4] = new SqlParameter("@Status", Status);
            param[5] = new SqlParameter("@PageName", PageName);
            param[6] = new SqlParameter("@ScheduleDate", ScheduleDate);
            param[7] = new SqlParameter("@ScheduleGroup", ScheduleGroup);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ScheduleSMS]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertalmiramaster(int AlmiraId, string AlmiraCode, string AlmiraName, string userid, string floor_code)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@AlmiraId_1", AlmiraId);
            param[1] = new SqlParameter(" @AlmiraCode_2", AlmiraCode);
            param[2] = new SqlParameter("@AlmiraName_3", AlmiraName);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@floor_code", floor_code);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AlmiraMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertebookprimarygroup(int LEVEL1_ID, int SERVICE_ID, string LEVEL_NAME)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@LEVEL1_ID_1", LEVEL1_ID);
            param[1] = new SqlParameter(" @SERVICE_ID_2", SERVICE_ID);
            param[2] = new SqlParameter("@LEVEL_NAME_3", LEVEL_NAME);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBook_SERVICE_LEVEL1_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable Updateinsertlocationmasterredefined(int Id, string Location_Name, string Location_Path, string Inst_Id, string Location)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter(" @Location_Name", Location_Name);
            param[2] = new SqlParameter("@Location_Path", Location_Path);
            param[3] = new SqlParameter(" @Inst_Id", Inst_Id);
            param[4] = new SqlParameter("@Location", Location);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Mapped_Location]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertebookserves(int Service_id, string Service_Name, string Description)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Service_id_1", Service_id);
            param[1] = new SqlParameter(" @Service_Name_2", Service_Name);
            param[2] = new SqlParameter("@Description_3", Description);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBook_SERVICE_MASTER_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertebookreservation(int ctrlno, int reservations)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ctrlno_1", ctrlno);
            param[1] = new SqlParameter("@reservations_2", reservations);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circBookreservations_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertcomprativechart(string Order_No, string JournalPackage_No,int FormId,int Trans)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Order_No_1", Order_No);
            param[1] = new SqlParameter("@JournalPackage_No_2", JournalPackage_No);
           
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OrderChild_Journal_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertthesisaddkeyword(int id, string keywords, string keyword_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id_1", id);
            param[1] = new SqlParameter("@keywords_2", keywords);
            param[2] = new SqlParameter("@keyword_id_3", keyword_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_thesis_keyword_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertmemberdueslistgeneration(string listno, string memberid, string membername, string departmentname, decimal balance, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@listno_1", listno);
            param[1] = new SqlParameter("@memberid_2", memberid);
            param[2] = new SqlParameter("@membername_3", membername);
            param[3] = new SqlParameter("@departmentname_4", departmentname);
            param[4] = new SqlParameter("@balance_5", balance);
            param[5] = new SqlParameter("@userid_6", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Duelistchildmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertnewspaperpayment(string Invoice_id, int ActualNoofcopies, decimal ActualPricePerCopy, decimal ArrivalDate, string Newspaper_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@ActualNoofcopies_2", ActualNoofcopies);
            param[2] = new SqlParameter("@ActualPricePerCopy_3", ActualPricePerCopy);
            param[3] = new SqlParameter("@ArrivalDate_4", ArrivalDate);
            param[4] = new SqlParameter("@Newspaper_id_5", Newspaper_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperInvoice_Child]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertinvoicenew(string Invoice_id, string Journal_Id, decimal Discount, decimal Amount, decimal Balance, decimal Rs_E, string Jour_Status, decimal print_Amt, decimal online_Amt, decimal p_o_Amt)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@Journal_Id_2", Journal_Id);
            param[2] = new SqlParameter("@Discount_3", Discount);
            param[3] = new SqlParameter("@Amount_4", Amount);
            param[4] = new SqlParameter("@Balance_5", Balance);
            param[5] = new SqlParameter("@Rs_E_6", Rs_E);
            param[6] = new SqlParameter("@Jour_Status_7", Jour_Status);
            param[7] = new SqlParameter("@print_Amt_8", print_Amt);
            param[8] = new SqlParameter("@online_Amt_9", online_Amt);
            param[9] = new SqlParameter("@p_o_Amt_10", p_o_Amt);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourInvoice_ChildN_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertregisfingerprint(string userid, byte fingerprint)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@fingerprint_2", fingerprint);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RegisFingerPrint_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertpackagejourprice(string package_id, string journal_id, decimal amount, decimal con_amt, decimal pro_amt, decimal Print_Amt, decimal online_Amt, decimal p_o_Amt, decimal Pro_Credit_amt)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@package_id_1", package_id);
            param[1] = new SqlParameter("@journal_id_2", journal_id);
            param[2] = new SqlParameter("@amount_3", amount);
            param[3] = new SqlParameter("@con_amt_4", con_amt);
            param[4] = new SqlParameter("@pro_amt_5", pro_amt);
            param[5] = new SqlParameter("@Print_Amt_6", Print_Amt);
            param[6] = new SqlParameter("@online_Amt_7", online_Amt);
            param[7] = new SqlParameter("@p_o_Amt_8", p_o_Amt);
            param[8] = new SqlParameter("@Pro_Credit_amt_9", Pro_Credit_amt);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_package_jour_price_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertpackagejourpricesup(string package_id, string journal_id, decimal amount, decimal con_amt, decimal pro_amt/* decimal Print_Amt*//*, decimal online_Amt, decimal p_o_Amt, decimal Pro_Credit_amt*/)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@package_id_1", package_id);
            param[1] = new SqlParameter("@journal_id_2", journal_id);
            param[2] = new SqlParameter("@amount_3", amount);
            param[3] = new SqlParameter("@con_amt_4", con_amt);
            param[4] = new SqlParameter("@pro_amt_5", pro_amt);
            //param[5] = new SqlParameter("@Print_Amt_6", Print_Amt);
            //param[6] = new SqlParameter("@online_Amt_7", online_Amt);
            //param[7] = new SqlParameter("@p_o_Amt_8", p_o_Amt);
            //param[8] = new SqlParameter("@Pro_Credit_amt_9", Pro_Credit_amt);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_package_jour_price_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }






        public DataTable updateinsertscheduleemail(string Subject, string Body, DateTime mailGenerateDT, string MailTo, string Status, string EmailPage, DateTime ScheduleDate, string ScheduleGroup)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Subject", Subject);
            param[1] = new SqlParameter("@Body", Body);
            param[2] = new SqlParameter("@mailGenerateDT", mailGenerateDT);
            param[3] = new SqlParameter("@MailTo", MailTo);
            param[4] = new SqlParameter("@Status", Status);
            param[5] = new SqlParameter("@EmailPage", EmailPage);
            param[6] = new SqlParameter("@ScheduleDate", ScheduleDate);
            param[7] = new SqlParameter("@ScheduleGroup", ScheduleGroup);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ScheduleEmail]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertinstitutemaster(int InstituteCode, string InstituteName, string shortname, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@InstituteCode_1", InstituteCode);
            param[1] = new SqlParameter("@InstituteName_2", InstituteName);
            param[2] = new SqlParameter("@shortname_3", shortname);
            param[3] = new SqlParameter("@userid_4", userid);      
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_InstituteMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertcdkeywords(int ID, string Keyword_name, string key_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID_1", ID);
            param[1] = new SqlParameter("@Keyword_name_2", Keyword_name);
            param[2] = new SqlParameter("@key_id_3", key_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CD_Keywords_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertprogrammaster(int program_id, string program_name, string short_name, int deptcode, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@program_id_1", program_id);
            param[1] = new SqlParameter("@program_name_2", program_name);
            param[2] = new SqlParameter("@short_name_3", short_name);
            param[3] = new SqlParameter("@deptcode_4", deptcode);
            param[4] = new SqlParameter("@userid_5", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Program_Master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertroommaster(int RoomId, string RoomNo, string RoomName, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@RoomId_1", RoomId);
            param[1] = new SqlParameter("@RoomNo_2", RoomNo);
            param[2] = new SqlParameter("@RoomName_3", RoomName);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RoomMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertbindtypemaster(string bindtypename, decimal price, DateTime c_date, string userid, int Binder_id,int FormID, int Type )
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@bindtypename_1", bindtypename);
            param[1] = new SqlParameter("@price_2", price);
            param[2] = new SqlParameter("@c_date_3", c_date);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@Binder_id", Binder_id);
            param[5] = new SqlParameter("@FormID", FormID);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bindtypemaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertfinecalculation(string userid, decimal fine)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@fine_2", fine);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Fine_calculation_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjourwriteoffentry(string DocumentNo, DateTime writeoffdate, string accessionnumber, string Cause, string Note, string Status, string userid, int FormID)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DocumentNo_1", DocumentNo);
            param[1] = new SqlParameter("@writeoffdate_2", writeoffdate);
            param[2] = new SqlParameter("@accessionnumber_3", accessionnumber);
            param[3] = new SqlParameter("@Cause_4", Cause);
            param[4] = new SqlParameter("@Note_5", Note);
            param[5] = new SqlParameter("@Status_7", Status);
            param[6] = new SqlParameter("@userid_1", userid);
            param[7] = new SqlParameter("@FormID", FormID);
           
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Jour_Writeoffentry_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        // end 
        public DataTable UpdateinsertEBookService(int LEVEL2_ID, int LEVEL1_ID, string LEVEL_NAME)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@LEVEL2_ID_1", LEVEL2_ID);
            param[1] = new SqlParameter("@LEVEL1_ID_2", LEVEL1_ID);
            param[2] = new SqlParameter("@LEVEL_NAME_3", LEVEL_NAME);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBook_SERVICE_LEVEL2_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertUpdateCatalog(string firstname, string percity, string perstate, string percountry, string peraddress, string publishercode)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[1] = new SqlParameter("@firstname", firstname);
            param[0] = new SqlParameter("@percity", percity);
            param[2] = new SqlParameter("@perstate", perstate);
            param[3] = new SqlParameter("@percountry", percountry);
            param[4] = new SqlParameter("@peraddress", peraddress);
            param[5] = new SqlParameter("@publishercode", publishercode);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[update_catalog]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateInsertLocationObjectItem(int Id, int LocationObjectId, string LocationObjectItem, string Inst_Id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@LocationObjectId", LocationObjectId);
            param[2] = new SqlParameter("@LocationObjectItem", LocationObjectItem);
            param[3] = new SqlParameter("@Inst_Id", Inst_Id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_LocationObject_Items]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertMergeSplitReference(string Journal_No, string Journal_Id, string Reference, string Operation_Mode, string Journaltitle, string JournalReference, DateTime Effective_From)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];
            param[1] = new SqlParameter("@Journal_No_1", Journal_No);
            param[0] = new SqlParameter("@Journal_Id_2", Journal_Id);
            param[2] = new SqlParameter("@Reference_3", Reference);
            param[3] = new SqlParameter("@Operation_Mode_4", Operation_Mode);
            param[4] = new SqlParameter(" @Journaltitle_5", Journaltitle);
            param[5] = new SqlParameter("@JournalReference_6", JournalReference);
            param[6] = new SqlParameter("@Effective_From_7", Effective_From);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_Merge_SplitReference_1]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateCircBookStatus(string accno)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@accno_1", accno);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_CircBookStatus_1]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable DeleteDACINfo(string daid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DAId_1", daid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[DeleteDAFInfo]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateInsertOrderMasterAudit(string ordernumber, DateTime exparivaldateapproval, DateTime exparivaldatenonapproval, string indentnumber, DateTime orderdate, string letternumber, DateTime letterdate, int cancelorder, int itemnumber, int departmentcode, decimal orderamount, int vendorid, int identityofordernumber, int order_check_code, string UserId, DateTime OperationDate, DateTime OperationTime, string Operation, string AffectedObjects, string IpAddress)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@ordernumber_1", ordernumber);
            param[1] = new SqlParameter(" @exparivaldateapproval_2", exparivaldateapproval);
            param[2] = new SqlParameter(" @exparivaldatenonapproval_3", exparivaldatenonapproval);
            param[3] = new SqlParameter("@indentnumber_4", indentnumber);
            param[4] = new SqlParameter("@orderdate_5", orderdate);
            param[5] = new SqlParameter("@letternumber_6", letternumber);
            param[6] = new SqlParameter("@letterdate_7", letterdate);
            param[7] = new SqlParameter("@cancelorder_8", cancelorder);
            param[8] = new SqlParameter("@itemnumber_9", itemnumber);
            param[9] = new SqlParameter("@departmentcode_10", departmentcode);
            param[10] = new SqlParameter("@orderamount_11", orderamount);
            param[11] = new SqlParameter("@vendorid_12", vendorid);
            param[12] = new SqlParameter(" @identityofordernumber_13", identityofordernumber);
            param[13] = new SqlParameter("@order_check_code_14", order_check_code);
            param[14] = new SqlParameter("@UserId_15", UserId);
            param[15] = new SqlParameter("@OperationDate_16", OperationDate);
            param[16] = new SqlParameter("@OperationTime_17", OperationTime);
            param[17] = new SqlParameter("@Operation_18", Operation);
            param[18] = new SqlParameter("@AffectedObjects_19", AffectedObjects);
            param[19] = new SqlParameter("@IpAddress_20", IpAddress);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_ordermasterAudit_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateInsertAddressTable(string addid, string localaddress, string localcity, string localstate, string localpincode, string localcountry, string peraddress, string percity, string perstate, string percountry, string perpincode, string addrelation)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@addid_1", addid);
            param[1] = new SqlParameter("@localaddress_2", localaddress);
            param[2] = new SqlParameter("@localcity_3", localcity);
            param[3] = new SqlParameter("@localstate_4", localstate);
            param[4] = new SqlParameter("@localpincode_5", localpincode);
            param[5] = new SqlParameter("@localcountry_6", localcountry);
            param[6] = new SqlParameter("@peraddress_7", peraddress);
            param[7] = new SqlParameter("@percity_8", percity);
            param[8] = new SqlParameter("@perstate_9", perstate);
            param[9] = new SqlParameter("@percountry_10", percountry);
            param[10] = new SqlParameter("@perpincode_11", perpincode);
            param[11] = new SqlParameter("@addrelation_12", addrelation);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_AddressTable_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateinsertLoginMaster(string LoginName, DateTime LoginDate, string LoginTime, string TableName, string UserAction, string id, string sessionyr, decimal financialValue, string IpAddress)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@LoginName_1", LoginName);
            param[1] = new SqlParameter("@LoginDate_2", LoginDate);
            param[2] = new SqlParameter("@LoginTime_3", LoginTime);
            param[3] = new SqlParameter("@TableName_4", TableName);
            param[4] = new SqlParameter("@UserAction_5", UserAction);
            param[5] = new SqlParameter("@id_6", id);
            param[6] = new SqlParameter("@sessionyr_7", sessionyr);
            param[7] = new SqlParameter(" @financialValue_8", financialValue);
            param[8] = new SqlParameter("@IpAddress_9", IpAddress);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_LoginMaster_2]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertLocationobject(int Id, string LocationObject, string Abbreviation, string Inst_Id, string OrderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@LocationObject", LocationObject);
            param[2] = new SqlParameter("@Abbreviation", Abbreviation);
            param[3] = new SqlParameter("@Inst_Id", Inst_Id);
            param[4] = new SqlParameter("@OrderNo", OrderNo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Insert_LocationObject]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable UpdateInsertActionPerm(int UserTypeId, int actionId, string Permission, int submenu_id, int child)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserTypeId_1", UserTypeId);
            param[1] = new SqlParameter("@actionId_2", actionId);
            param[2] = new SqlParameter("@Permission_3", Permission);
            param[3] = new SqlParameter("@submenu_id_4", submenu_id);
            param[4] = new SqlParameter("@child_5", child);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_ActionLPerm_1]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateInsertEAttachmentShare(int Id, string daid, string sfileName, string Passwd, string Saltvc, int shareBy_UserId, int shareWith_UserId, string permission, string daysLimit, DateTime fromDate, DateTime toDate)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@Daid", daid);
            param[2] = new SqlParameter("@sFileName", sfileName);
            param[3] = new SqlParameter("@Passwd", Passwd);
            param[4] = new SqlParameter("@Saltvc", Saltvc);
            param[5] = new SqlParameter("@shareBy_UserId", shareBy_UserId);
            param[6] = new SqlParameter("@shareWith_UserId", shareWith_UserId);
            param[7] = new SqlParameter("@permission", permission);
            param[8] = new SqlParameter("@daysLimit", daysLimit);
            param[9] = new SqlParameter("@fromDate", fromDate);
            param[10] = new SqlParameter("@toDate", toDate);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_eAttachment_Share]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertItemKeyWord(int Ctrl_No, string Keyword, string ItemType, int S_No)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Ctrl_No_1", Ctrl_No);
            param[1] = new SqlParameter("@Keyword_2", Keyword);
            param[2] = new SqlParameter("@ItemType_3", ItemType);
            param[3] = new SqlParameter("@S_No_4", S_No);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ItemsKeyword_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertImageGalleryPhotos(int SubEventId, byte Photos, string TagMembers)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SubEventId", SubEventId);
            param[1] = new SqlParameter("@Photos", Photos);
            param[2] = new SqlParameter("@TagMembers", TagMembers);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Insert_ImageGallery_Photos]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;



        }
        public DataTable UpdateInsertMaterialAccompany(string accession_no, string media_accessionno, int media_type, string description, string fileurl, string userid)
        {
            DataTable dt = new DataTable();
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@accession_no_1", accession_no);
            param[1] = new SqlParameter("@media_accessionno_2", media_accessionno);
            param[2] = new SqlParameter("@media_type_3", media_type);
            param[3] = new SqlParameter("@description_4", description);
            param[4] = new SqlParameter("@fileurl_5", fileurl);
            param[5] = new SqlParameter("@userid_6", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_material_accompany_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateInsertJournalrequest(int Jrnl_Request_No, DateTime Jrnl_Date, int Department_Id, string Requester_code, string Jrnl_Title, string Media_Type, int Priority, string List_No, string Requester_Name, DateTime Request_Year, DateTime Request_year1, string userid, int NOC, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@Jrnl_Request_No_1", Jrnl_Request_No);
            param[1] = new SqlParameter("@Jrnl_Date_2", Jrnl_Date);
            param[2] = new SqlParameter("@Department_Id_3", Department_Id);
            param[3] = new SqlParameter("@Requester_code_4", Requester_code);
            param[4] = new SqlParameter("@Jrnl_Title_5", Jrnl_Title);
            param[5] = new SqlParameter("@Media_Type_6", Media_Type);
            param[6] = new SqlParameter("@Priority_7", Priority);
            param[7] = new SqlParameter("@List_No_8", List_No);
            param[8] = new SqlParameter("@Requester_Name_9", Requester_Name);
            param[9] = new SqlParameter("@Request_Year_10", Request_Year);
            param[10] = new SqlParameter("@Request_Year1_11", Request_year1);
            param[11] = new SqlParameter("@userid_12", userid);
            param[12] = new SqlParameter("@NOC", NOC);
            param[13] = new SqlParameter("@FormID", FormID);
            param[14] = new SqlParameter("@Type",Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Journal_Request_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertILLmaster(int ILLid, string authorisedperson, string ILLname, string address, string city, string state, string email, string phoneno, int maxdays, float odcharge, string userid, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@ILLid_1", ILLid);
            param[1] = new SqlParameter("@authorisedperson_2", authorisedperson);
            param[2] = new SqlParameter("@ILLname_3", ILLname);
            param[3] = new SqlParameter("@address_4", address);
            param[4] = new SqlParameter("@city_5", city);
            param[5] = new SqlParameter("@state_6", state);
            param[6] = new SqlParameter("@email_7", email);
            param[7] = new SqlParameter("@phoneno_8", phoneno);
            param[8] = new SqlParameter("@maxdays_9", maxdays);
            param[9] = new SqlParameter("@odcharge_10", odcharge);
            param[10] = new SqlParameter("@userid_11", userid);
            param[11] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ILLMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertpackagesubscription(string Package_Id, string Package_Code, string Subscription_No, string Title_Abbre, string Package_Title, string Agent, DateTime Start_Date, DateTime Expiry_Date, string Publisher, DateTime entry_Date, string Subscription_Status, string Process_Stage, string Journal_Status, DateTime SubscriptionStatus_Date, string Reason, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@Package_Id_1", Package_Id);
            param[1] = new SqlParameter("@Package_Code_2", Package_Code);
            param[2] = new SqlParameter("@Subscription_No_3", Subscription_No);
            param[3] = new SqlParameter("@Title_Abbre_4", Title_Abbre);
            param[4] = new SqlParameter("@Package_Title_5", Package_Title);
            param[5] = new SqlParameter("@Agent_6", Agent);
            param[6] = new SqlParameter("@Start_Date_7", Start_Date);
            param[7] = new SqlParameter("@Expiry_Date_8", Expiry_Date);
            param[8] = new SqlParameter("@Publisher_9", Publisher);
            param[9] = new SqlParameter("@Entry_Date_10", entry_Date);
            param[10] = new SqlParameter("@Subscription_Status_11", Subscription_Status);
            param[11] = new SqlParameter("@Process_Stage_12", Process_Stage);
            param[12] = new SqlParameter("@Journal_Status_13", Journal_Status);
            param[13] = new SqlParameter("@SubscriptionStatus_Date_14", SubscriptionStatus_Date);
            param[14] = new SqlParameter("@Reason_15", Reason);
            param[15] = new SqlParameter("@userid_16", userid);
            param[16] = new SqlParameter("@FormID", FormID);
            param[17] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_package_subscription_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertbindtransaction(string List_No, string Journal_No, string Volume, string Part, string From_Issue, string To_Issue, string Lack_No, string Status, string Journal_Year, string From_pubdate, string To_Pubdate, DateTime Fromdate, DateTime todate, string CopyNo_F, string CopyNo_T)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@List_No_1", List_No);
            param[1] = new SqlParameter("@Journal_No_2", Journal_No);
            param[2] = new SqlParameter("@Volume_3", Volume);
            param[3] = new SqlParameter("@From_Issue_5", Part);
            param[4] = new SqlParameter("@To_Issue_6", To_Issue);
            param[5] = new SqlParameter("@Lack_No_7", Lack_No);
            param[6] = new SqlParameter("@Status_8", Status);
            param[7] = new SqlParameter("@Journal_Year_9", Journal_Year);
            param[8] = new SqlParameter("@From_pubdate_10", From_pubdate);
            param[9] = new SqlParameter("@To_pubdate_11", To_Pubdate);
            param[10] = new SqlParameter("@Fromdate_12", Fromdate);
            param[11] = new SqlParameter("@todate_13", todate);
            param[12] = new SqlParameter("@CopyNo_F", CopyNo_F);
            param[13] = new SqlParameter("@CopyNo_T", CopyNo_T);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BindTransaction_Child_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertpackagemanagement(string packageid, string journalid, string gifted, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@packageid_1", packageid);
            param[1] = new SqlParameter("@journalid_2", journalid);
            param[2] = new SqlParameter("@gifted_3", gifted);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PackageManagement_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }


        public DataTable Updateinsertlibrarytransaction(int invoice_id, int services, int noofcopy, float rate, int id, float tot_amt)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@invoice_id_1", invoice_id);
            param[1] = new SqlParameter("@services_2", services);
            param[2] = new SqlParameter("@noofcopy_3", noofcopy);
            param[3] = new SqlParameter("@rate_4", rate);
            param[4] = new SqlParameter("@id_5", id);
            param[5] = new SqlParameter("@tot_amt_6", tot_amt);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_lib_serTrans_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateinsertPaymentTransaction(int PaymentID, float InvoiceID, float InvoiceAmount, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PaymentID_1", PaymentID);
            param[1] = new SqlParameter("@InvoiceID_2", InvoiceID);
            param[2] = new SqlParameter("@InvoiceAmount_3", InvoiceAmount);
            param[3] = new SqlParameter("@FormID",FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PaymentTransaction_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertbinderinformation(int binderid, string name, string address, string city, string state, string email, string phoneno, int maxissuedays, int overduecharges, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@binderid_1", binderid);
            param[1] = new SqlParameter("@name_2", name);
            param[2] = new SqlParameter("@address_3", address);
            param[3] = new SqlParameter("@city_4", city);
            param[4] = new SqlParameter("@state_5", state);
            param[5] = new SqlParameter("@email_6", email);
            param[6] = new SqlParameter("@phoneno_7", phoneno);
            param[7] = new SqlParameter("@overduecharges_9", overduecharges);
            param[8] = new SqlParameter("@userid_10", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_PaymentTransaction_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertwindowweek(string Weeks, string Days, int Number)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Weeks_1", Weeks);
            param[1] = new SqlParameter("@Days_2", Days);
            param[2] = new SqlParameter("@Number_3", Number);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Window_Week_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable Updateinsertonlinrjournal(int Journal_id, string FileName, byte[] Image_bin, string flag)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Journal_id", Journal_id);
            param[1] = new SqlParameter("@FileName", FileName);
            param[2] = new SqlParameter("@Image_bin", Image_bin);
            param[3] = new SqlParameter("@flg", flag);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_onlineJournal_attachments]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertbudgetmaster(int departmentcode, decimal allocatedamount, decimal expendedamount, decimal approvalcommitedamt, decimal nonapprovalcommitedamt, decimal approvalindentinhandamt, decimal nonapprovalindentinhandamt, Boolean status, string Curr_Session, string userid, int VenorId, decimal VendorPer)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@departmentcode_1", departmentcode);
            param[1] = new SqlParameter("@allocatedamount_2", allocatedamount);
            param[2] = new SqlParameter("@expendedamount_3", expendedamount);
            param[3] = new SqlParameter("@approvalcommitedamt_4", approvalcommitedamt);
            param[4] = new SqlParameter("@nonapprovalcommitedamt_5", nonapprovalcommitedamt);
            param[5] = new SqlParameter("@approvalindentinhandamt_6", approvalindentinhandamt);
            param[6] = new SqlParameter("@nonapprovalindentinhandamt_7", nonapprovalindentinhandamt);
            param[7] = new SqlParameter("@status_8", status);
            param[8] = new SqlParameter("@Curr_Session_9", Curr_Session);
            param[9] = new SqlParameter("@userid_10", userid);
            param[10] = new SqlParameter("@VenorId", VenorId);
            param[11] = new SqlParameter("@VendorPer", VendorPer);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_budgetmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable UpdateInsertbudgetadjustment(DateTime Date, int departmentcode, float Amount, string Curr_Session, string Operation, string userid, string FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Date_1", Date);
            param[1] = new SqlParameter("@departmentcode_2", departmentcode);
            param[2] = new SqlParameter("@Amount_3", Amount);
            param[3] = new SqlParameter("@Curr_Session_4", Curr_Session);
            param[4] = new SqlParameter("@Operation_5", Operation);
            param[5] = new SqlParameter("@userid_6", userid);
            param[6] = new SqlParameter("@FormID", FormID);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAdjustment_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable Getissueauthority(int id, int userid, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@UserID", userid);
            param[2] = new SqlParameter("@FormID", FormID);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetIssuingAuthority](@id , @UserID , @FormID , @Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;

        }
        //akansha
        public DataTable updateinsertjourrcarrival(string Journal_Id, string Doc_id, int Artical_No, string Artical_Title)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Journal_Id_1", Journal_Id);
            param[1] = new SqlParameter("@Doc_id_22", Doc_id);
            param[2] = new SqlParameter("@Artical_No_3", Artical_No);
            param[3] = new SqlParameter("@Artical_Title_4", Artical_Title);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Jour_RecArrival_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertstandardreply(int reply_id, string reply, string user_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@reply_id_1", reply_id);
            param[1] = new SqlParameter("@reply_2", reply);
            param[2] = new SqlParameter("@user_id_3", user_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_standard_reply_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertinvoicemaster(string invoicenumber, DateTime invoicedate, int invoiceid, decimal postage, decimal netamount, decimal discountamount, decimal discountpercentage, string vendorid, string billserialno, decimal handlingcharge, string payCurrency, decimal payAmount, string reportingtypeofinvoice, decimal total_amt, string userid, int FormID, int Type)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@invoicenumber_1", invoicenumber);
            param[1] = new SqlParameter("@invoicedate_2", invoicedate);
            param[2] = new SqlParameter("@invoiceid_3", invoiceid);
            param[3] = new SqlParameter("@postage_4", postage);
            param[4] = new SqlParameter("@netamount_5", netamount);
            param[5] = new SqlParameter("@discountamount_6", discountamount);
            param[6] = new SqlParameter("@discountpercentage_7", discountpercentage);
            param[7] = new SqlParameter("@vendorid_8", vendorid);
            param[8] = new SqlParameter("@billserialno_9", billserialno);
            param[9] = new SqlParameter("@handlingcharge_10", handlingcharge);
            param[10] = new SqlParameter("@payCurrency_11", payCurrency);
            param[11] = new SqlParameter("@payAmount_12", payAmount);
            param[12] = new SqlParameter("@typeofinvoice_13", reportingtypeofinvoice);
            param[13] = new SqlParameter("@total_amt_14", total_amt);
            param[14] = new SqlParameter("@user_id_15", userid);
            param[15] = new SqlParameter("@FormID", FormID);
            param[16] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_invoicemaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertebookkeywords(int ID, string keyword_name, int key_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID_1", ID);
            param[1] = new SqlParameter("@Keyword_name_2", keyword_name);
            param[2] = new SqlParameter("@Key_id_3", key_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EBOOK_Keywords_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertebookkeywords(int ID, string ApplicationName, string FileName, string Description)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ID_1", ID);
            param[1] = new SqlParameter("@ApplicationName", ApplicationName);
            param[2] = new SqlParameter("@FileName", FileName);
            param[3] = new SqlParameter("@Description", Description);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_ApplicationLinks]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertebookkeywords(int DescID, string DescTitle, string DescInfo)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DescID ", DescID);
            param[1] = new SqlParameter("@DescTitle", DescTitle);
            param[2] = new SqlParameter("@DescInfo", DescInfo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_idcardBackDesc]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertebookkeywords(string Journal_Id, string Doc_Id, string AMS, int Mediatype, string Description, string FileUrl, string postedfilename)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Journal_Id_1 ", Journal_Id);
            param[1] = new SqlParameter("@Doc_Id_2", Doc_Id);
            param[2] = new SqlParameter("@AMS_3", AMS);
            param[3] = new SqlParameter("@Mediatype_Id_4 ", Mediatype);
            param[4] = new SqlParameter("@Description_5", Description);
            param[5] = new SqlParameter("@FileUrl_6", FileUrl);
            param[6] = new SqlParameter("@postedfilename_6", postedfilename);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Jour_Accomaterial_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournaladdresstable(string Journal_Id, string Sus_ad_id, string rem_ad_id, string claim_ad_id, string sus_relation, string remi_relation, string claim_relation)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Journal_Id_1 ", Journal_Id);
            param[1] = new SqlParameter("@Sus_ad_id_2", Sus_ad_id);
            param[2] = new SqlParameter("@rem_ad_id_3", rem_ad_id);
            param[3] = new SqlParameter("@claim_ad_id_4 ", claim_ad_id);
            param[4] = new SqlParameter("@sus_relation_5 ", sus_relation);
            param[5] = new SqlParameter("@remi_relation_6", remi_relation);
            param[6] = new SqlParameter("@claim_relation_7", claim_relation);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_addresstable_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinsertExistJourPmt(string Journal_No, string Journal_Id, string Invoice_No, DateTime Invoice_Date, int Draft_No, DateTime Draft_date, string Journal_Amount, string currency)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Journal_No_1 ", Journal_No);
            param[1] = new SqlParameter("@Journal_Id_2", Journal_Id);
            param[2] = new SqlParameter("@Invoice_No_3", Invoice_No);
            param[3] = new SqlParameter("@Invoice_Date_4 ", Invoice_Date);
            param[4] = new SqlParameter("@Draft_No_5 ", Draft_No);
            param[5] = new SqlParameter("@Draft_date_6", Draft_date);
            param[6] = new SqlParameter("@Journal_Amount_7", Journal_Amount);
            param[7] = new SqlParameter("@currency_8", currency);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Exist_JourPmt_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }














        public DataTable updateinsertbindbillchild(int invoiceid, string bindtypename, int noofcopy, string amount)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@invoiceid_1 ", invoiceid);
            param[1] = new SqlParameter("@bindtypename_2", bindtypename);
            param[2] = new SqlParameter("@noofcopy_3", noofcopy);
            param[3] = new SqlParameter("@amount_4", amount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bindbillchild_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournalbudget(string departmentname, int code, decimal price)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@departmentname_1 ", departmentname);
            param[1] = new SqlParameter("@code_2", code);
            param[2] = new SqlParameter("@price_3", price);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_budget_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsert(int invoiceid, DateTime receivedate, string invoiceno, DateTime invoicedate, int binderid, decimal totalamount)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@invoiceid_1 ", invoiceid);
            param[1] = new SqlParameter("@receivedate_2", receivedate);
            param[2] = new SqlParameter("@invoiceno_3", invoiceno);
            param[3] = new SqlParameter("@invoicedate_4 ", invoicedate);
            param[4] = new SqlParameter("@binderid_5", binderid);
            param[5] = new SqlParameter("@totalamount_6", totalamount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bindbillmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertbouncemailrec(string eMailId, string fromEmail, string message, string Status, string eMailId1, string smtpAdd, string userid, string operation, string senddate)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@eMailID_1 ", eMailId);
            param[1] = new SqlParameter("@fromEmail_2", fromEmail);
            param[2] = new SqlParameter("@message_3", message);
            param[3] = new SqlParameter("@Status_4 ", Status);
            param[4] = new SqlParameter("@eMailId1_5", eMailId1);
            param[5] = new SqlParameter("@smtpAdd_6", smtpAdd);
            param[6] = new SqlParameter("@userid_7 ", userid);
            param[7] = new SqlParameter("@Flag_8", operation);
            param[8] = new SqlParameter("@senddate_9", senddate);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BouncemailRec_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertrevisionmanagement(int DAid, string Main_filenm, int Id, string title, string Prop_By, string Prop_Revno, string Prop_fileName, string Prop_Attachment, DateTime prop_dt, string Prop_toMembers, string comments)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@DAid ", DAid);
            param[1] = new SqlParameter("@Main_filenm", Main_filenm);
            param[2] = new SqlParameter("@Id", Id);
            param[3] = new SqlParameter("@title ", title);
            param[4] = new SqlParameter("@Prop_By", Prop_By);
            param[5] = new SqlParameter("@Prop_Revno", Prop_Revno);
            param[6] = new SqlParameter("@Prop_fileName ", Prop_fileName);
            param[7] = new SqlParameter("@Prop_Attachment", Prop_Attachment);
            param[8] = new SqlParameter("@prop_dt", prop_dt);
            param[9] = new SqlParameter("@Prop_toMembers", Prop_toMembers);
            param[10] = new SqlParameter("@comments", comments);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_RevisionManagement]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertinvoicetransaction(int invoiceid, string ordernumber, decimal totalorderamount, string indentnumber, decimal srno, decimal discount, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@invoiceid_1 ", invoiceid);
            param[1] = new SqlParameter("@ordernumber_2", ordernumber);
            param[2] = new SqlParameter("@totalorderamount_3", totalorderamount);
            param[3] = new SqlParameter("@indentnumber_4 ", indentnumber);
            param[4] = new SqlParameter("@srno_5", srno);
            param[5] = new SqlParameter("@discount_6", discount);
            param[6] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_invoicetransaction_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable updateinsertCircReceiveTransaction(string userid, string accno, DateTime receivingdate, decimal fineamount, string fineCause, string isPaid, DateTime Dueon, DateTime paidon, string amtexp, string userid1, int IssueId, int tran_id, int Id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@userid_1 ", userid);
            param[1] = new SqlParameter(" @accno_2 ", accno);
            param[2] = new SqlParameter("@receivingdate_3", receivingdate);
            param[3] = new SqlParameter("@fineamount_4", fineamount);
            param[4] = new SqlParameter("@fineCause_5", fineCause);
            param[5] = new SqlParameter("@isPaid_6", isPaid);
            param[0] = new SqlParameter("@DueDate_7 ", Dueon);
            param[6] = new SqlParameter(" @paidon_8 ", paidon);
            param[7] = new SqlParameter("@amtexp_9", amtexp);
            param[8] = new SqlParameter("@userid1_10", userid);
            param[9] = new SqlParameter("@IssueId_11", IssueId);
            param[10] = new SqlParameter("@tran_id", tran_id);
            param[11] = new SqlParameter("@Id", Id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[CircReceiveTransaction]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        //end 
        public DataTable insertepc(string AccNumber, string Location, string Rfid, string userName, DateTime LoginDate, string LoginTime, string sessionyr, string IpAddress, string UserAction)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@AccNumber", AccNumber);
            param[1] = new SqlParameter("@Location", Location);
            param[2] = new SqlParameter("@Rfid", Rfid);
            param[3] = new SqlParameter("@userName", userName);
            param[4] = new SqlParameter("@LoginDate", LoginDate);
            param[5] = new SqlParameter("@LoginTime", LoginTime);
            param[6] = new SqlParameter("@sessionyr", sessionyr);
            param[7] = new SqlParameter("@IpAddress", IpAddress);
            param[8] = new SqlParameter("@UserAction", UserAction);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Sp_insertEPC]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable insertserverrepository(int Id, string upFileName, string upFile, string downloadLink, int UserId)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@upfileName", upFileName);
            param[2] = new SqlParameter("@upFile", upFile);
            param[3] = new SqlParameter("@downloadLink", downloadLink);
            param[4] = new SqlParameter("@UserId", UserId);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_serverRepository]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertduelistmaster(string listno, DateTime listdate, string membergroup, string paidstatus, string program_name, string dept, string JoinYear, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@listno_1", listno);
            param[1] = new SqlParameter("@listdate_2", listdate);
            param[2] = new SqlParameter("@membergroup_3", membergroup);
            param[3] = new SqlParameter("@paidstatus_4", paidstatus);
            param[4] = new SqlParameter("@program_name_5", program_name);
            param[5] = new SqlParameter("@dept_6", dept);
            param[6] = new SqlParameter("@JoinYear_7", JoinYear);
            param[7] = new SqlParameter("@userid_8)", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Duelistmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinsertmemissctgwise(string Memberid, int Category_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Memberid_1", Memberid);
            param[1] = new SqlParameter("@Category_id_2", Category_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_MemIssCtgWise_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertordermasterjournal(string OrderNo, DateTime OrderDate, string PublisherCode, string Status, string Order_Status, string relation, string userid , int FormID , int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@OrderNo_1", OrderNo);
            param[1] = new SqlParameter("@OrderDate_2", OrderDate);
            param[2] = new SqlParameter("@PublisherCode_3", PublisherCode);
            param[3] = new SqlParameter("@Status_4", Status);
            param[4] = new SqlParameter("@Order_Status_5", Order_Status);
            param[5] = new SqlParameter("@relation_6", relation);
            param[6] = new SqlParameter("@userid_7", userid);
            param[7] = new SqlParameter("@FormID", FormID);
            param[8] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OrderMaster_Journal_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertddcbooknumber(int id, string MainClass, string DivisionTag, string SectiionTag, string Book_Title, string DDC_Number)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Id_1", id);
            param[1] = new SqlParameter("@MainClass_2", MainClass);
            param[2] = new SqlParameter("@DivisionTag_3", DivisionTag);
            param[3] = new SqlParameter("@SectiionTag_4", SectiionTag);
            param[4] = new SqlParameter("@Book_Title_5", Book_Title);
            param[5] = new SqlParameter("@DDC_Number_6", DDC_Number);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DDC_BookNumber_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertddcbooknumber(int ctrl_no, string classnumber, string booknumber, int TransNo)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@classnumber_2", classnumber);
            param[2] = new SqlParameter("@booknumber_3", booknumber);
            param[3] = new SqlParameter("@TransNo", TransNo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DDC_BookNumber_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertstockentrychild(string docno, string accessionnumber, string status, string missing_status)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@docno_1", docno);
            param[1] = new SqlParameter("@accessionnumber_2", accessionnumber);
            param[2] = new SqlParameter("@status_3", status);
            param[3] = new SqlParameter("@missing_status_4", missing_status);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Stock_Entry_Child_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertbookconference(int ctrl_no, string Subtitle, string Paralleltype, string ConfName, string ConfYear, string BNNote, string CNNote, string GNNotes, string VNNotes, string SNNotes, string ANNotes, string Course, string AdFname1, string AdMname1, string AdLname1, string AdFname2, string AdMname2, string AdLname2, string AdFname3, string AdMname3, string AdLName3, string Abstract, string program_name)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@Subtitle_2", Subtitle);
            param[2] = new SqlParameter("@Paralleltype_3", Paralleltype);
            param[3] = new SqlParameter("@ConfName_4", ConfName);
            param[4] = new SqlParameter("@ConfYear_5", ConfYear);
            param[5] = new SqlParameter("@BNNote_6", BNNote);
            param[6] = new SqlParameter("@CNNote_7", CNNote);
            param[7] = new SqlParameter("@GNNotes_8", GNNotes);
            param[8] = new SqlParameter("@VNNotes_9", VNNotes);
            param[9] = new SqlParameter("@SNNotes_10", SNNotes);
            param[10] = new SqlParameter("@ANNotes_11", ANNotes);
            param[11] = new SqlParameter("@Course_12", Course);
            param[12] = new SqlParameter("@AdFname1_13", AdFname1);
            param[13] = new SqlParameter("@AdMname1_14", AdMname1);
            param[14] = new SqlParameter("@AdLname1_15", AdLname1);
            param[15] = new SqlParameter("@AdFname2_16", AdFname2);
            param[16] = new SqlParameter("@AdMname2_17", AdMname2);
            param[17] = new SqlParameter("@AdLname2_18", AdLname2);
            param[18] = new SqlParameter("@AdFname3_19", AdFname3);
            param[19] = new SqlParameter("@AdMname3_20", AdMname3);
            param[20] = new SqlParameter("@AdLName3_21", AdLName3);
            param[21] = new SqlParameter("@Abstract_22", Abstract);
            param[22] = new SqlParameter("@program_name_23", program_name);
            //param[23] = new SqlParameter("@TransNo", TransNo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookConference_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertbookentryConference(int ctrl_no, string Subtitle, string Paralleltype, string ConfName, string ConfYear, string BNNote, string CNNote, string GNNotes, string VNNotes, string SNNotes, string ANNotes, string Course, string AdFname1, string AdMname1, string AdLname1, string AdFname2, string AdMname2, string AdLname2, string AdFname3, string AdMname3, string AdLName3, string Abstract, string program_name, int TransNo, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[25];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@Subtitle_2", Subtitle);
            param[2] = new SqlParameter("@Paralleltype_3", Paralleltype);
            param[3] = new SqlParameter("@ConfName_4", ConfName);
            param[4] = new SqlParameter("@ConfYear_5", ConfYear);
            param[5] = new SqlParameter("@BNNote_6", BNNote);
            param[6] = new SqlParameter("@CNNote_7", CNNote);
            param[7] = new SqlParameter("@GNNotes_8", GNNotes);
            param[8] = new SqlParameter("@VNNotes_9", VNNotes);
            param[9] = new SqlParameter("@SNNotes_10", SNNotes);
            param[10] = new SqlParameter("@ANNotes_11", ANNotes);
            param[11] = new SqlParameter("@Course_12", Course);
            param[12] = new SqlParameter("@AdFname1_13", AdFname1);
            param[13] = new SqlParameter("@AdMname1_14", AdMname1);
            param[14] = new SqlParameter("@AdLname1_15", AdLname1);
            param[15] = new SqlParameter("@AdFname2_16", AdFname2);
            param[16] = new SqlParameter("@AdMname2_17", AdMname2);
            param[17] = new SqlParameter("@AdLname2_18", AdLname2);
            param[18] = new SqlParameter("@AdFname3_19", AdFname3);
            param[19] = new SqlParameter("@AdMname3_20", AdMname3);
            param[20] = new SqlParameter("@AdLName3_21", AdLName3);
            param[21] = new SqlParameter("@Abstract_22", Abstract);
            param[22] = new SqlParameter("@program_name_23", program_name);
            param[23] = new SqlParameter("@TransNo", TransNo);
            param[24] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookConference_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinsertbookconferences(int ctrl_no, string Subtitle, string Paralleltype, string ConfName, string ConfYear, string BNNote, string CNNote, string GNNotes, string VNNotes, string SNNotes, string ANNotes, string Course, string AdFname1, string AdMname1, string AdLname1, string AdFname2, string AdMname2, string AdLname2, string AdFname3, string AdMname3, string AdLName3, string Abstract, string program_name)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@Subtitle_2", Subtitle);
            param[2] = new SqlParameter("@Paralleltype_3", Paralleltype);
            param[3] = new SqlParameter("@ConfName_4", ConfName);
            param[4] = new SqlParameter("@ConfYear_5", ConfYear);
            param[5] = new SqlParameter("@BNNote_6", BNNote);
            param[6] = new SqlParameter("@CNNote_7", CNNote);
            param[7] = new SqlParameter("@GNNotes_8", GNNotes);
            param[8] = new SqlParameter("@VNNotes_9", VNNotes);
            param[9] = new SqlParameter("@SNNotes_10", SNNotes);
            param[10] = new SqlParameter("@ANNotes_11", ANNotes);
            param[11] = new SqlParameter("@Course_12", Course);
            param[12] = new SqlParameter("@AdFname1_13", AdFname1);
            param[13] = new SqlParameter("@AdMname1_14", AdMname1);
            param[14] = new SqlParameter("@AdLname1_15", AdLname1);
            param[15] = new SqlParameter("@AdFname2_16", AdFname2);
            param[16] = new SqlParameter("@AdMname2_17", AdMname2);
            param[17] = new SqlParameter("@AdLname2_18", AdLname2);
            param[18] = new SqlParameter("@AdFname3_19", AdFname3);
            param[19] = new SqlParameter("@AdMname3_20", AdMname3);
            param[20] = new SqlParameter("@AdLName3_21", AdLName3);
            param[21] = new SqlParameter("@Abstract_22", Abstract);
            param[22] = new SqlParameter("@program_name_23", program_name);
            
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookConference_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }






















        public DataTable insertilltransactions(DateTime issuedate, int ILLid, string accno, DateTime exparrivaldate, string status, int check_status, string DocumentNo, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@issuedate_1", issuedate);
            param[1] = new SqlParameter("@ILLid_2", ILLid);
            param[2] = new SqlParameter("@accno_3", accno);
            param[3] = new SqlParameter("@exparrivaldate_4", exparrivaldate);
            param[4] = new SqlParameter("@status_5", status);
            param[5] = new SqlParameter("@check_status_6", check_status);
            param[6] = new SqlParameter("@DocumentNo_7", DocumentNo);
            param[7] = new SqlParameter("@userid_8", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ILLtransactions_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertusertypepermission(int usertypeid, string UserTypeName)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id_1", usertypeid);
            param[1] = new SqlParameter("@UserTypeName_2", UserTypeName);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypePermissions_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertbudgetallocjournal(int departmentcode, float allocated_amount, float expended_amount, float committed_amount, float balance, int status, string academic_session, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@departmentcode_1", departmentcode);
            param[1] = new SqlParameter("@allocated_amount_2", allocated_amount);
            param[2] = new SqlParameter("@expended_amount_3", expended_amount);
            param[3] = new SqlParameter("@committed_amount_4", committed_amount);
            param[4] = new SqlParameter("@balance_5", balance);
            param[5] = new SqlParameter("@status_6", status);
            param[6] = new SqlParameter("@academic_session_7", academic_session);
            param[7] = new SqlParameter("@userid_8", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAllocJournal_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable Updateinsertbarcodesheet(int Id, string SheetName, int BColumns, int BRows, float HPitch, float VPitch, float LHeight, float LWidth, float TopMargin, float BottomMargin, float LeftMargin, float RightMargin, float barcodeHeight, float OfficeId)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@SheetName", SheetName);
            param[2] = new SqlParameter("@BColumns", BColumns);
            param[3] = new SqlParameter("@BRows", BRows);
            param[4] = new SqlParameter("@HPitch", HPitch);
            param[5] = new SqlParameter("@VPitch", VPitch);
            param[6] = new SqlParameter("@LHeight", LHeight);
            param[7] = new SqlParameter("@LWidth", LWidth);
            param[8] = new SqlParameter("@TopMargin", TopMargin);
            param[9] = new SqlParameter("@BottomMargin", BottomMargin);
            param[10] = new SqlParameter("@LeftMargin", LeftMargin);
            param[11] = new SqlParameter("@RightMargin", RightMargin);
            param[12] = new SqlParameter("@barcodeHeight", barcodeHeight);
            param[13] = new SqlParameter("@OfficeId", OfficeId);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_barcode_Sheet]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable updateinsertpaymentmaster(int paymentid, int draftnumber, int currencycode, DateTime draftdate, string bankname, float amount, string paymenttype, string vendorid, DateTime paymentdate, float draft_amt, string documentno, string payment_type, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@paymentid_1", paymentid);
            param[1] = new SqlParameter("@draftnumber_2", draftnumber);
            param[2] = new SqlParameter("@currencycode_3", currencycode);
            param[3] = new SqlParameter("@draftdate_4", draftdate);
            param[4] = new SqlParameter("@bankname_5", bankname);
            param[5] = new SqlParameter("@amount_6", amount);
            param[6] = new SqlParameter("@paymenttype_7", paymenttype);
            param[7] = new SqlParameter("@vendorid_8", vendorid);
            param[8] = new SqlParameter("@paymentdate_9", paymentdate);
            param[9] = new SqlParameter("@draft_amt_10", draft_amt);
            param[10] = new SqlParameter("@documentno_11", documentno);
            param[11] = new SqlParameter("@payment_type_12", payment_type);
            param[12] = new SqlParameter("@userid_13", userid);
            param[13] = new SqlParameter("@FormID", FormID);
            param[14] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_paymentmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];


        }


        public DataTable Updateinsertaccountpaymentmaster(int paymentid, float amount, string paymenttype, string vendorid, DateTime paymentdate, string documentno, string payment_type, string userid, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@paymentid_1", paymentid);
            param[1] = new SqlParameter("@amount_2", amount);
            param[2] = new SqlParameter("@paymenttype_3", paymenttype);
            param[3] = new SqlParameter("@vendorid_4", vendorid);
            param[4] = new SqlParameter("@paymentdate_5", paymentdate);
            param[5] = new SqlParameter("@documentno_6", documentno);
            param[6] = new SqlParameter("@payment_type_7", payment_type);
            param[7] = new SqlParameter("@userid_8", userid);
            param[8] = new SqlParameter("@FormID", FormID);
            param[9] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_letter_acctopmtmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable Updateinsertrelative(string membercode, string relationship, string reladd, string relname, string flg)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Member_code", membercode);
            param[1] = new SqlParameter("@Relationship", relationship);
            param[2] = new SqlParameter("@Rel_name", relname);
            param[3] = new SqlParameter("@Rel_Add", reladd);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_letter_acctopmtmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }


        public DataTable Updateinsertdalmaster(string DAId, string AccessionNo, string fileName, string filepath, string filepasswd, string passwdkey, string type, string Type_No, string file_grp, byte[] File_BData, byte[] supportDoc_bdata, string supportDoc_name, string userId, string folderFile_virtualPath)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@DAId_1", DAId);
            param[1] = new SqlParameter("@AccessionNo_2", AccessionNo);
            param[2] = new SqlParameter("@fileName_3", fileName);
            param[3] = new SqlParameter("@filepath_4", filepath);
            param[4] = new SqlParameter("@filepasswd_5", filepasswd);
            param[5] = new SqlParameter("@passwdkey_6", passwdkey);
            param[6] = new SqlParameter("@type_7", type);
            param[7] = new SqlParameter("@Type_No", Type_No);
            param[8] = new SqlParameter("@file_grp", file_grp);
            param[9] = new SqlParameter("@File_BData", File_BData);
            param[10] = new SqlParameter("@supportDoc_bdata", supportDoc_bdata);
            param[11] = new SqlParameter("@supportDoc_name", supportDoc_name);
            param[12] = new SqlParameter("@userId", userId);
            param[13] = new SqlParameter("@folderFile_virtualPath", folderFile_virtualPath);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DAFileInfo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }





        public DataTable GetActionUserdetail(int SubMenu_id, string Usertype, string Userid2, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@SubMenu_id", SubMenu_id);
            param[1] = new SqlParameter("@Usertype", Usertype);
            param[2] = new SqlParameter("@Userid2", Userid2);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetActionUserdetail](@SubMenu_id,@Usertype,@Userid2  ,@UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetLibCurrency()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLibCurrency]( )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetActionUserPerm(int Memberid, int Actionid, int Submenu_id, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Memberid", Memberid);
            param[1] = new SqlParameter("@Actionid", Actionid);
            param[2] = new SqlParameter("@Submenu_id", Submenu_id);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetActionUserPerm](@Memberid,@Actionid,@Submenu_id  ,@UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBudget(string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetBudget](@UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndent(string indentnumber, string indentId, int departmentcode, string titleExact, string title, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@indentnumber", indentnumber);
            param[1] = new SqlParameter("@indentId", indentId);
            param[2] = new SqlParameter("@departmentcode", departmentcode);
            param[3] = new SqlParameter("@titleExact", titleExact);
            param[4] = new SqlParameter("@title", title);

            param[5] = new SqlParameter("@UserID", UserID);
            param[6] = new SqlParameter("@FormId", FormId);
            param[7] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndent](@indentnumber,@indentId,@departmentcode,@titleExact,@title, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GenerateIndentId()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GenerateIndentId]( )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        //
        public DataTable GenearteItemID(string indentnumber)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@indentnumber", indentnumber);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GenearteItemID](@indentnumber )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable DupIndentVendIndentId(int vendorid, string indentnumber, int indentid, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@vendorid", vendorid);
            param[1] = new SqlParameter("@indentnumber", indentnumber);
            param[2] = new SqlParameter("@indentid", indentid);
            param[3] = new SqlParameter("@UserId", UserId);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[DupIndentVendIndentId](@vendorid, @indentnumber,@indentid,@UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable DupIndentDateIndentId(DateTime indentdate, string indentnumber, int indentid, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@indentdate", indentdate);
            param[1] = new SqlParameter("@indentnumber", indentnumber);
            param[2] = new SqlParameter("@indentid", indentid);
            param[3] = new SqlParameter("@UserId", UserId);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[DupIndentDateIndentId](@indentdate, @indentnumber,@indentid,@UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateOpacIndent(string indentnumber, int indentid, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@indentnumber", indentnumber);
            param[1] = new SqlParameter("@indentid", indentid);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateOpacIndent]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable UpdateDepartmentCurrentPos(int departmentcode, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@departmentcode ", departmentcode);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateDepartmentCurrentPos]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable UpdateBudgetAddApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@departmentcode ", departmentcode);
            param[1] = new SqlParameter("@Curr_Session ", Curr_Session);
            param[2] = new SqlParameter("@IndentValue ", IndentValue);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddApprove]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable UpdateBudgetDecrNonApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@departmentcode ", departmentcode);
            param[1] = new SqlParameter("@Curr_Session ", Curr_Session);
            param[2] = new SqlParameter("@IndentValue ", IndentValue);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetDecrNonApprove]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable UpdateBudgetAddNonApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@departmentcode ", departmentcode);
            param[1] = new SqlParameter("@Curr_Session ", Curr_Session);
            param[2] = new SqlParameter("@IndentValue ", IndentValue);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddNonApprove]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable UpdateBudgetDecrApprove(int departmentcode, string Curr_Session, decimal IndentValue, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@departmentcode ", departmentcode);
            param[1] = new SqlParameter("@Curr_Session ", Curr_Session);
            param[2] = new SqlParameter("@IndentValue ", IndentValue);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetDecrApprove]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable UpdateIndentOnlinePStatus(string indentnumber, int OnlinePStatus, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@indentnumber ", indentnumber);
            param[1] = new SqlParameter("@OnlinePStatus ", OnlinePStatus);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateIndentOnlinePStatus]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }

        public DataTable GetActionLPermUser(string MemberId, int ActionId, int Submenu_id, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@MemberId", MemberId);
            param[1] = new SqlParameter("@ActionId", ActionId);
            param[2] = new SqlParameter("@Submenu_id", Submenu_id);
            param[3] = new SqlParameter("@UserId", UserId);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetActionLPermUser](@MemberId, @ActionId,@Submenu_id,@UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetUserCanRequest(string UserID2, string departmentcode, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserID2", UserID2);
            param[1] = new SqlParameter("@departmentcode", departmentcode);
            param[2] = new SqlParameter("@UserId", UserId);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[GetUserCanRequest](@UserID2, @departmentcode, @UserId,@FormId,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable DeleteIndent(string indentId, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@indentId ", indentId);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[DeleteIndent]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable IndentItemNoDecr(string indentNo, string ItemNo, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@indentNo ", indentNo);
            param[1] = new SqlParameter("@ItemNo ", ItemNo);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[IndentItemNoDecr]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable GetIndentVarious(string Title, string Author, string Firstname, string Vendorname, string SeriesName, string Isbn, string IndentItemNo, string All, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Title", Title);
            param[1] = new SqlParameter("@Author", Author);
            param[2] = new SqlParameter("@Firstname", Firstname);
            param[3] = new SqlParameter("@Vendorname", Vendorname);
            param[4] = new SqlParameter("@SeriesName", SeriesName);
            param[5] = new SqlParameter("@Isbn", Isbn);
            param[6] = new SqlParameter("@IndentItemNo", IndentItemNo);
            param[7] = new SqlParameter("@All", All);
            param[8] = new SqlParameter("@UserID", UserID);
            param[9] = new SqlParameter("@FormId", FormId);
            param[10] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentVarious](@Title,@Author ,@Firstname ,@Vendorname ,@SeriesName ,@Isbn ,@IndentItemNo ,@All , @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateIndentPrintStatus(string indentnumber, string PrintStatus, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@indentnumber ", indentnumber);
            param[1] = new SqlParameter("@PrintStatus ", PrintStatus);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceProc("[MTR].[UpdateIndentPrintStatus]", ref dsDel, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsDel.Tables[0];
        }
        public DataTable GetLanguage(int Language_Id, string Language_Name, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Language_Id", Language_Id);
            param[1] = new SqlParameter("@Language_Name", Language_Name);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetLanguage](@Language_Id,@Language_Name  , @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetdeptInst(string Session, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Session", Session);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetdeptInst](@Session  , @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetCurrencyExchange(string CurrencyCode, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CurrencyCode", CurrencyCode);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetCurrencyExchange](@CurrencyCode  , @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetMediaType(int media_id, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            media_id = media_id == 0 ? -1 : media_id;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@media_id", media_id);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetMediaType](@media_id  , @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetPublOpacEmptyIndent(string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetPublOpacEmptyIndent]( @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable SuggIndentTitle(string Title, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Title", Title);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[SuggIndentTitle](@Title, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentDetail(string indentnumber, string indentId, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@indentnumber", indentnumber);
            param[1] = new SqlParameter("@indentId", indentId);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDetail](@indentnumber,@indentId, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBookDetail(string Accno, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Accno", Accno);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetBookDetail](@Accno, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetOpacIndent(string Indentid, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Indentid", Indentid);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetOpacIndent](@Indentid, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetNewIindent(string indentnumber, string indentid, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@indentnumber", indentnumber);
            param[1] = new SqlParameter("@indentid", indentid);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            DataSet dsDel = new DataSet();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetNewIindent](@indentnumber,@indentid, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable Updateinsertstaffmaster(int departmentcode, string staffid, string firstname, string middlename, string lastname, string email, string phone1, string phone2, DateTime doj, string gender, string pfno, string email2, DateTime validupto, string remark, byte user_picture, string classname, string Fathername, DateTime Dob, string cat_id, string program_id, string Joinyear, string subjects, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@departmentcode_1", departmentcode);
            param[1] = new SqlParameter("@staffid_2", staffid);
            param[2] = new SqlParameter("@firstname_3", firstname);
            param[3] = new SqlParameter("@middlename_4", middlename);
            param[4] = new SqlParameter("@lastname_5", lastname);
            param[5] = new SqlParameter("@email_6", email);
            param[6] = new SqlParameter("@phone1_7", phone1);
            param[7] = new SqlParameter("@phone2_8", phone2);
            param[8] = new SqlParameter("@doj_10", doj);
            param[9] = new SqlParameter("@gender_11", gender);
            param[10] = new SqlParameter("@pfno_12", pfno);
            param[11] = new SqlParameter("@email2_13", email2);
            param[12] = new SqlParameter("@validupto_14", validupto);
            param[13] = new SqlParameter("@remark_15", remark);
            param[14] = new SqlParameter("@user_picture_16", user_picture);
            param[15] = new SqlParameter("@classname_17", classname);
            param[16] = new SqlParameter("@Fathername_18", Fathername);
            param[17] = new SqlParameter("@Dob_19", Dob);
            param[18] = new SqlParameter("@cat_id_20", cat_id);
            param[19] = new SqlParameter("@program_id_21", program_id);
            param[20] = new SqlParameter("@Joinyear_22", Joinyear);
            param[21] = new SqlParameter("@subjects_23", subjects);
            param[22] = new SqlParameter("@userid_24", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_staffmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }



        public DataTable Updateinsertbookaccessionmaster(string accessionnumber, string cpbooknumber, string ordernumber, string indentnumber, string form, string accessionid, string accessiondate, string booktitle, string srno, string released, string bookprice, string srnoOld, string status, string releaseddate, string issuestatus, string loadingdate, string checkstatus, string ctrlNo, string editionyear, string copynumber, string specialprice, string pubyear, string billNo, string billdate, string catalogdate, string itemtype, string originalprice, string originalcurrency, string vendorsource, string programid, string deptcode, string DSrno, string itemcategory, string Locid, string userid, string sess, string ipa, string title, string appname, string transno)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[40];
            param[0] = new SqlParameter("@accessionnumber", accessionnumber);
            param[1] = new SqlParameter("@cp_booknumber", cpbooknumber);
            param[2] = new SqlParameter("@ordernumber", ordernumber);
            param[3] = new SqlParameter("@indentnumber", indentnumber);
            param[4] = new SqlParameter("@form", form);
            param[5] = new SqlParameter("@accessionid", accessionid);
            param[6] = new SqlParameter("@accessioneddate", accessiondate);
            param[7] = new SqlParameter("@booktitle", booktitle);
            param[8] = new SqlParameter("@srno", srno);
            param[9] = new SqlParameter("@released", released);
            param[10] = new SqlParameter("@bookprice", bookprice);
            param[11] = new SqlParameter("@srNoOld", srnoOld);
            param[12] = new SqlParameter("@Status", status);
            param[13] = new SqlParameter("@ReleaseDate", releaseddate);
            param[14] = new SqlParameter("@IssueStatus", issuestatus);
            param[15] = new SqlParameter("@LoadingDate", loadingdate);
            param[16] = new SqlParameter("@CheckStatus", checkstatus);
            param[17] = new SqlParameter("@ctrl_no", ctrlNo);
            param[18] = new SqlParameter("@editionyear", editionyear);
            param[19] = new SqlParameter("@Copynumber", copynumber);
            param[20] = new SqlParameter("@Specialprice", specialprice);
            param[21] = new SqlParameter("@pubYear", pubyear);
            param[22] = new SqlParameter("@biilNo", billNo);
            param[23] = new SqlParameter("@billDate", billdate);
            param[24] = new SqlParameter("@catalogDate", catalogdate);
            param[25] = new SqlParameter("@Item_Type", itemtype);
            param[26] = new SqlParameter("@OriginalPrice", originalprice);
            param[27] = new SqlParameter("@OriginalCurrency", originalcurrency);
            param[28] = new SqlParameter("@vendor_source", vendorsource);
            param[29] = new SqlParameter("@program_id", programid);
            param[30] = new SqlParameter("@DeptCode", deptcode);
            param[31] = new SqlParameter("@DSrno", DSrno);
            param[32] = new SqlParameter("@ItemCategory", itemcategory);
            param[33] = new SqlParameter("@Loc_id", Locid);
            param[34] = new SqlParameter("@UserId", userid);
            param[35] = new SqlParameter("@Sess_102", sess);
            param[36] = new SqlParameter("@IPA_103", ipa);
            param[37] = new SqlParameter("@Title_104", title);
            param[38] = new SqlParameter("@AppName", appname);
            param[39] = new SqlParameter("@TransNo", transno);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bookaccessionmaster_new]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertnewfolder(string foldername, int id, string resourcetype, string flag, string indexcatalogue)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@FolderName", foldername);
            param[1] = new SqlParameter("@ID", id);
            param[2] = new SqlParameter("@ResourceType", resourcetype);
            param[3] = new SqlParameter("@flag", flag);
            param[4] = new SqlParameter("@IndexCatalog", indexcatalogue);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NEWFOLDER]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        //akansha
        public DataTable Updateinsertbookcatalog(int ctrl_no, DateTime catalogdate1, int booktype, string volumenumber, string initpages, int pages, string parts, string leaves, string boundind, string title, string publishercode, string edition, string isbn, string subject1, string subject2, string subject3, string Booksize, string LCCN, string Volumepages, string biblioPages, string bookindex, string illustration, string variouspaging, string maps, string ETalEditor, string ETalCompiler, string ETalIllus, string ETalTrans, string ETalAuthor, string accmaterialhistory, string MaterialDesignation, string issn, string Volume, int dept, int language_id, string part, string eBookURL, string cat_Source, string Identifier, string firstname, string percity, string perstate, string percountry, string peraddress, string departmentname, string Btype, string language_name, string ItemCategory, int TransNo)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[48];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter(" @catalogdate_2 ", catalogdate1);
            param[2] = new SqlParameter("@booktype_3", booktype);
            param[3] = new SqlParameter("@volumenumber_4", volumenumber);
            param[4] = new SqlParameter("@initpages_5", initpages);
            param[5] = new SqlParameter("@pages_6", pages);
            param[0] = new SqlParameter("@parts_7 ", parts);
            param[6] = new SqlParameter(" @leaves_8 ", leaves);
            param[7] = new SqlParameter("@boundind_9", boundind);
            param[8] = new SqlParameter("@title_10", title);
            param[9] = new SqlParameter("@publishercode_11", publishercode);
            param[10] = new SqlParameter("@edition_12", edition);
            param[11] = new SqlParameter("@isbn_13", isbn);
            param[12] = new SqlParameter("@subject1_14 ", subject1);
            param[13] = new SqlParameter(" @subject2_15 ", subject2);
            param[14] = new SqlParameter("@subject3_16", subject3);
            param[15] = new SqlParameter("@Booksize_17", Booksize);
            param[16] = new SqlParameter("@LCCN_18", LCCN);
            param[17] = new SqlParameter("@Volumepages_19", Volumepages);
            param[18] = new SqlParameter("@biblioPages_20 ", biblioPages);
            param[19] = new SqlParameter("@bookindex_21 ", bookindex);
            param[20] = new SqlParameter("@illustration_22", illustration);
            param[21] = new SqlParameter("@variouspaging_23", variouspaging);
            param[22] = new SqlParameter("@maps_24", maps);
            param[23] = new SqlParameter("@ETalEditor_25", ETalEditor);
            param[24] = new SqlParameter("@ETalCompiler_26", ETalCompiler);
            param[25] = new SqlParameter("@ETalIllus_27 ", ETalIllus);
            param[26] = new SqlParameter(" @ETalTrans_28 ", ETalTrans);
            param[27] = new SqlParameter("@ETalAuthor_29", ETalAuthor);
            param[28] = new SqlParameter("@accmaterialhistory_31", accmaterialhistory);
            param[29] = new SqlParameter("@MaterialDesignation_32", MaterialDesignation);
            param[30] = new SqlParameter("@issn_33", issn);
            param[31] = new SqlParameter("@Volume_34 ", Volume);
            param[32] = new SqlParameter(" @dept_35 ", dept);
            param[33] = new SqlParameter("@language_id_36", language_id);
            param[34] = new SqlParameter("@part_37", part);
            param[35] = new SqlParameter("@eBookURL_38", eBookURL);
            param[36] = new SqlParameter("@cat_Source_39", cat_Source);
            param[37] = new SqlParameter("@Identifier_40", Identifier);
            param[38] = new SqlParameter("@firstname_41 ", firstname);
            param[39] = new SqlParameter("@percity_42 ", percity);
            param[40] = new SqlParameter("@perstate_43", perstate);
            param[41] = new SqlParameter("@percountry_44", percountry);
            param[42] = new SqlParameter("@peraddress_45", peraddress);
            param[43] = new SqlParameter("@departmentname_46", departmentname);
            param[44] = new SqlParameter("@Btype_47 ", Btype);
            param[45] = new SqlParameter("@language_name_48 ", language_name);
            param[46] = new SqlParameter("@ItemCategory_49", ItemCategory);
            param[47] = new SqlParameter("@TransNo", TransNo);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookCatalog_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertbudgetadjustmentjournal(DateTime Date, int departmentcode, decimal Amount, string Curr_Session, string Operation, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Date_1", Date);
            param[1] = new SqlParameter("@departmentcode_2", departmentcode);
            param[2] = new SqlParameter("@Amount_3", Amount);
            param[3] = new SqlParameter("@Curr_Session_4", Curr_Session);
            param[4] = new SqlParameter("@Operation_5", Operation);
            param[5] = new SqlParameter("@userid_6", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BudgetAdjustmentJournal_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinserttranslationlanguage(int Language_Id, string Language_Name, string Font_Name, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Language_Id_1", Language_Id);
            param[1] = new SqlParameter("@Language_Name_2", Language_Name);
            param[2] = new SqlParameter("@Font_Name_3", Font_Name);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Translation_Language_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertcdmasterpassword(string id, string title, string fileName, string filepath, string filepassword, string passwdkey, string lev_id, int cd_mastId, string client_server, string FileSize, string FileBytes, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@id_1", id);
            param[1] = new SqlParameter("@title_2", title);
            param[2] = new SqlParameter("@fileName_3", fileName);
            param[3] = new SqlParameter("@filepath_4", filepath);
            param[4] = new SqlParameter("@filepassword_5", filepassword);
            param[5] = new SqlParameter("@passwdkey_6", passwdkey);
            param[6] = new SqlParameter("@lev_id_7", lev_id);
            param[7] = new SqlParameter("@Cd_MastId", cd_mastId);
            param[8] = new SqlParameter("@client_server_8", client_server);
            param[9] = new SqlParameter("@FileSize", FileSize);
            param[10] = new SqlParameter("@FileBytes", FileBytes);
            param[11] = new SqlParameter("@FormID", FormID);
            param[12] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_cd_master_password]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable updateinsertrevisioncomment(int Id, int Daid, int fileId, int revisionId, string UserId, string Comment)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Id_1", Id);
            param[1] = new SqlParameter("@Daid", Daid);
            param[2] = new SqlParameter("@fileId", fileId);
            param[3] = new SqlParameter("@revisionId", revisionId);
            param[4] = new SqlParameter("@UserId", UserId);
            param[5] = new SqlParameter("@comment", Comment);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_revision_Comment]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertCastCategories(int cat_id, string cat_name, string userid, string shortname)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@cat_id_1", cat_id);
            param[1] = new SqlParameter("@cat_name_2", cat_name);
            param[2] = new SqlParameter("@userid_3", userid);
            param[3] = new SqlParameter("@shortname_4", shortname);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CastCategories_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertloginmaster(string LoginName, DateTime LoginDate, string LoginTime, string TableName, string UserAction, string id, string sessionyr, string IpAddress)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@LoginName_1", LoginName);
            param[1] = new SqlParameter("@LoginDate_2", LoginDate);
            param[2] = new SqlParameter("@LoginTime_3", LoginTime);
            param[3] = new SqlParameter("@TableName_4", TableName);
            param[4] = new SqlParameter("@UserAction_5", UserAction);
            param[5] = new SqlParameter("@id_6", id);
            param[6] = new SqlParameter("@sessionyr_7", sessionyr);
            param[7] = new SqlParameter("@IpAddress_8", IpAddress);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_LoginMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable updateinsertmembershipfreedefinition(int id, string Member_code, string membership_type, DateTime From_dt, DateTime To_dt, string frequency, decimal fee_amount, string odc_type, decimal odc_amount, DateTime Wef_dt, string userid, string session_yr)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@Member_code", Member_code);
            param[2] = new SqlParameter("@membership_type", membership_type);
            param[3] = new SqlParameter("@From_dt", From_dt);
            param[4] = new SqlParameter("@To_dt", To_dt);
            param[5] = new SqlParameter("@frequency", frequency);
            param[6] = new SqlParameter("@fee_amount", fee_amount);
            param[7] = new SqlParameter("@odc_type", odc_type);
            param[8] = new SqlParameter("@odc_amount", odc_amount);
            param[9] = new SqlParameter("@Wef_dt", Wef_dt);
            param[10] = new SqlParameter("@userid", userid);
            param[11] = new SqlParameter("@session_yr", session_yr);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Membership_FeeDefinition]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournalrefunddetails(string Bill_serial_no, string CreditNote_no, decimal Amount, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Bill_serial_no_1", Bill_serial_no);
            param[1] = new SqlParameter("@CreditNote_no_2", CreditNote_no);
            param[2] = new SqlParameter("@Amount_3", Amount);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Journal_Refund_Detail_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertdistributedbudget(int departmentcode, decimal Amt_percentage, decimal amount, string bill_seriaL_NO, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@departmentcode_1", departmentcode);
            param[1] = new SqlParameter("@Amt_percentage_2", Amt_percentage);
            param[2] = new SqlParameter("@amount_3", amount);
            param[3] = new SqlParameter("@bill_seriaL_NO_4", bill_seriaL_NO);
            param[4] = new SqlParameter("@userid_5", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_distributed_budget_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertmembershipfreedefinitionchild(int Main_id, string Member_id, DateTime Expected_Date, DateTime Submission_date, DateTime curr_date, decimal AMount, decimal Fine_amt, string status, string paid_type, int row_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Main_id", Main_id);
            param[1] = new SqlParameter("@Member_id", Member_id);
            param[2] = new SqlParameter("@Expected_Date", Expected_Date);
            param[3] = new SqlParameter("@Submission_date", Submission_date);
            param[4] = new SqlParameter("@curr_date", curr_date);
            param[5] = new SqlParameter("@AMount", AMount);
            param[6] = new SqlParameter("@Fine_amt", Fine_amt);
            param[7] = new SqlParameter("@status", status);
            param[8] = new SqlParameter("@paid_type", paid_type);
            param[9] = new SqlParameter("@row_id", row_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Membership_FeeDefinition_child]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertmembershippaiduser(int Id, string MemberId, decimal Amount)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@MemberId", MemberId);
            param[2] = new SqlParameter("@Amount", Amount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_MembershipPaidUser]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertdirectinvoicemaster(int srno, DateTime curr_date, decimal postage, decimal net_amt, decimal disc_amt, decimal disc_percentage, string vedorid, decimal handling_charge, decimal total_amt, int rate_followed_by, decimal pay_amt, string invoice_no, string bill_no, DateTime bill_date)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@srno_1", srno);
            param[1] = new SqlParameter("@curr_date_2", curr_date);
            param[2] = new SqlParameter("@postage_3", postage);
            param[3] = new SqlParameter("@net_amt_4", net_amt);
            param[4] = new SqlParameter("@disc_amt_5", disc_amt);
            param[5] = new SqlParameter("@disc_percentage_6", disc_percentage);
            param[6] = new SqlParameter("@vedorid_7", vedorid);
            param[7] = new SqlParameter("@handling_charge_8", handling_charge);
            param[8] = new SqlParameter("@total_amt_9", total_amt);
            param[9] = new SqlParameter("@rate_followed_by_10", rate_followed_by);
            param[10] = new SqlParameter("@pay_amt_11", pay_amt);
            param[11] = new SqlParameter("@invoice_no_12", invoice_no);
            param[12] = new SqlParameter("@bill_no_13", bill_no);
            param[13] = new SqlParameter("@bill_date_14", bill_date);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_direct_invoice_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertJournalInvoice_Status(string Invoice_id, string Status, string Draft_No, DateTime Draft_Date, DateTime Entry_Date, string Publisher_Code, string Type, string remark, string userid, int FormID, int Type1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@Status_2", Status);
            param[2] = new SqlParameter("@Draft_No_3", Draft_No);
            param[3] = new SqlParameter("@Draft_Date_4", Draft_Date);
            param[4] = new SqlParameter("@Entry_Date_5", Entry_Date);
            param[5] = new SqlParameter("@Publisher_Code_6", Publisher_Code);
            param[6] = new SqlParameter("@Type_7", Type);
            param[7] = new SqlParameter("@remark_8", remark);
            param[8] = new SqlParameter("@userid_9", userid);
            param[9] = new SqlParameter("@FormID", FormID);
            param[10] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalInvoice_Status_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertReceive_details(string Rec_ID, string Member_ID, DateTime Curr_dt, DateTime Rec_dt, decimal Curr_Amt, decimal Rec_amt, decimal Curr_fine, decimal Rec_fine, int Main_id, int row_id, string userid, string session_yr)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@Rec_ID", Rec_ID);
            param[1] = new SqlParameter("@Member_ID", Member_ID);
            param[2] = new SqlParameter("@Curr_dt", Curr_dt);
            param[3] = new SqlParameter("@Rec_dt", Rec_dt);
            param[4] = new SqlParameter("@Curr_Amt", Curr_Amt);
            param[5] = new SqlParameter("@Rec_amt", Rec_amt);
            param[6] = new SqlParameter("@Curr_fine", Curr_fine);
            param[7] = new SqlParameter("@Rec_fine", Rec_fine);
            param[8] = new SqlParameter("@Main_id", Main_id);
            param[9] = new SqlParameter("@row_id", row_id);
            param[10] = new SqlParameter("@userid", userid);
            param[11] = new SqlParameter("@session_yr", session_yr);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Receive_details]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertqualification(string Member_code, string Ex_passesd, string Board, string RollN, string P_year, int Marks, int Outof, decimal per_age, string status, string Subject)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Member_code", Member_code);
            param[1] = new SqlParameter("@Ex_passesd", Ex_passesd);
            param[2] = new SqlParameter("@Board", Board);
            param[3] = new SqlParameter("@RollN", RollN);
            param[4] = new SqlParameter("@P_year", P_year);
            param[5] = new SqlParameter("@Marks", Marks);
            param[6] = new SqlParameter("@Outof", Outof);
            param[7] = new SqlParameter("@per_age", per_age);
            param[8] = new SqlParameter("@status", status);
            param[9] = new SqlParameter("@Subject", Subject);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Qualificarions_c]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable updateinsertjournal_PmtTrans(int PaymentID, string InvoiceID, decimal InvoiceAmount, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PaymentID_1", PaymentID);
            param[1] = new SqlParameter("@InvoiceID_2", InvoiceID);
            param[2] = new SqlParameter("@InvoiceAmount_3", InvoiceAmount);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_PmtTrans_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable updateinserthobbies(string Member_code, int id, string hobbies, string flg)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Member_code", Member_code);
            param[1] = new SqlParameter("@id", id);
            param[2] = new SqlParameter("@Hobbies", hobbies);
            param[3] = new SqlParameter("@flg", flg);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Hobbies]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertBookbindTranschild(string List_no, string Accession_no, string Status, string Bindtype)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@List_no_1", List_no);
            param[1] = new SqlParameter("@Accession_no_2", Accession_no);
            param[2] = new SqlParameter("@Status_3", Status);
            param[3] = new SqlParameter("@Bindtype_4", Bindtype);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookbindTrans_child_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertproposedSeconded(string Member_code, string PSType, string Act_Name, string Asso_Mem_No, string Council_Mem_No, string Setting_place_court, string file_name, string flag)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Member_code", Member_code);
            param[1] = new SqlParameter("@PSType", PSType);
            param[2] = new SqlParameter("@Act_Name", Act_Name);
            param[3] = new SqlParameter("@Asso_Mem_No", Asso_Mem_No);
            param[4] = new SqlParameter("@Council_Mem_No", Council_Mem_No);
            param[5] = new SqlParameter("@Setting_place_court", Setting_place_court);
            param[6] = new SqlParameter("@file_name", file_name);
            param[7] = new SqlParameter("@flg", flag);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_proposed_Seconded]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournalrefundentrymaster(int PubAgentCode, string DocumentNo, DateTime EntryDate, string Currency, string paymode, string DraftNo, DateTime DraftDate, string CreditNoteNo, DateTime CreditNoteDate, string PayableAt, decimal Amount, string Relation, decimal balance, string remark, string Status, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@PubAgentCode_1", PubAgentCode);
            param[1] = new SqlParameter("@DocumentNo_2", DocumentNo);
            param[2] = new SqlParameter("@EntryDate_3", EntryDate);
            param[3] = new SqlParameter("@Currency_4", Currency);
            param[4] = new SqlParameter("@paymode_5", paymode);
            param[5] = new SqlParameter("@DraftNo_6", DraftNo);
            param[6] = new SqlParameter("@DraftDate_7", DraftDate);
            param[7] = new SqlParameter("@CreditNoteNo_8", CreditNoteNo);
            param[8] = new SqlParameter("@CreditNoteDate_9", CreditNoteDate);
            param[9] = new SqlParameter("@PayableAt_10", PayableAt);
            param[10] = new SqlParameter("@Amount_11", Amount);
            param[11] = new SqlParameter("@Relation_12", Relation);
            param[12] = new SqlParameter("@balance_13", balance);
            param[13] = new SqlParameter("@remark_14", remark);
            param[14] = new SqlParameter("@Status_15", Status);
            param[15] = new SqlParameter("@userid_16", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourRefEntry_Master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertJourIssRet(string member_id, string tranid, string journal_no, DateTime issretdate, DateTime duedate, string issst, string accno, DateTime entdate, string userid, string ipa, string sess, string titl, string Remark, string Result)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@MemId", member_id);
            param[1] = new SqlParameter("@Jno", journal_no);
            param[2] = new SqlParameter("@IssRetDt", issretdate);
            param[3] = new SqlParameter("@DueDt", duedate);
            param[4] = new SqlParameter("@IssSt", issst);
            param[5] = new SqlParameter("@AccNo", accno);
            param[6] = new SqlParameter("@EntDt", entdate);
            param[7] = new SqlParameter("@userid", userid);
            param[8] = new SqlParameter("@ipa", ipa);
            param[9] = new SqlParameter("@sess", sess);
            param[10] = new SqlParameter("@titl", titl);
            param[11] = new SqlParameter("@Remark", Remark);
            param[12] = new SqlParameter("@Result", Result);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourIssRet]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        //end

        public DataTable UpdateInsertCircReceiveMaster(string userid, decimal totalfine, string userid1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@totalfine_2", totalfine);
            param[2] = new SqlParameter("@userid1_3", userid1);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[CircReceiveMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateLoginStatus(string userid, string Lstatus)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@Lstatus_2", Lstatus);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[LoginStatus]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertUpload(string id, string title, string file_url, string Group_Name, byte[] file_url1, string web_opacflg)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@title", title);
            param[2] = new SqlParameter("@file_url", file_url);
            param[3] = new SqlParameter("@group_Name", Group_Name);
            param[4] = new SqlParameter("@file_url1", file_url1);
            param[5] = new SqlParameter("@show_flg", web_opacflg);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_uploads_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateServerPath(int Id, string ServerPath)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Id_1", Id);
            param[1] = new SqlParameter("@ServerPath_2", ServerPath);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ServerPath_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertJournalIssue(string Member_Id, string Journal_No, string Volume, string Issue, string Part, DateTime IssueDate, DateTime DueDate, string Status, DateTime publication_date, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Member_Id_1", Member_Id);
            param[1] = new SqlParameter("@Journal_No_2", Journal_No);
            param[2] = new SqlParameter("@Volume_3", Volume);
            param[3] = new SqlParameter("@DocNumber", Issue);
            param[4] = new SqlParameter("@Part_5", Part);
            param[5] = new SqlParameter("@IssueDate_6", IssueDate);
            param[6] = new SqlParameter("@DueDate_7", DueDate);
            param[7] = new SqlParameter("@Status_8", Status);
            param[8] = new SqlParameter("@publication_date_9", publication_date);
            param[9] = new SqlParameter("@userid_10", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Journal_IssueNonA_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertBookImage(int ctrl_no, Byte[] CoverPage, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@CoverPage_2", CoverPage);
            param[2] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookImage_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertOverdueReceipt(string EntryId, DateTime EntryDate, string ReceiptNo, DateTime ReceiptDate, string userId, decimal Amount, string userid1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@EntryId_1", EntryId);
            param[1] = new SqlParameter("@EntryDate_2", EntryDate);
            param[2] = new SqlParameter("@ReceiptNo_3", ReceiptNo);
            param[3] = new SqlParameter("@ReceiptDate_4", ReceiptDate);
            param[4] = new SqlParameter("@userId_5", userId);
            param[5] = new SqlParameter("@Amount_6", Amount);
            param[6] = new SqlParameter("@userid1_7", userid1);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OverDueReceipt_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }
        public DataTable UpdateInsertJourRefEntryChild(string DocumentNo, int Journalid, decimal Jour_amount, string doc_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@media_id_1", DocumentNo);
            param[1] = new SqlParameter("@media_name_2", Journalid);
            param[2] = new SqlParameter("@short_name_3,", Jour_amount);
            param[3] = new SqlParameter("@userid_4", doc_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JourRefEntry_Child_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertPaperMarginSetting(int Id, string Name, int mLeft, int mRight, int mTop, int mBottom, string barcodeSlipHeight)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@Name", Name);
            param[2] = new SqlParameter("@mLeft", mLeft);
            param[3] = new SqlParameter("@mRight", mRight);
            param[4] = new SqlParameter("@mTop", mTop);
            param[5] = new SqlParameter("@mBottom", mBottom);
            param[6] = new SqlParameter("@barcodeSlipHeight", barcodeSlipHeight);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_paperMargin_Settings]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }
        public DataTable UpdateInsertClassLoadStatus(string classname, int totalissueddays, int noofbookstobeissued, decimal finperday, int reservedays, int totalissueddays_jour, int noofjournaltobeissued, decimal fineperday_jour, string reservedays_jour, string Status, int ValueLimit, int days_1phase, decimal amt_1phase, int days_2phase, decimal amt_2phase, int days_1phasej, decimal amt_1phasej, int days_2phasej, decimal amt_2phasej, string shortname, int loadingstatus)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@classname_1", classname);
            param[1] = new SqlParameter("@totalissueddays_2", totalissueddays);
            param[2] = new SqlParameter("@noofbookstobeissued_3", noofbookstobeissued);
            param[3] = new SqlParameter("@finperday_4", finperday);
            param[4] = new SqlParameter("@reservedays_5", reservedays);
            param[5] = new SqlParameter("@totalissueddays_jour_6", totalissueddays_jour);
            param[6] = new SqlParameter("@noofjournaltobeissued_7", noofjournaltobeissued);
            param[7] = new SqlParameter("@fineperday_jour_8", fineperday_jour);
            param[8] = new SqlParameter("@reservedays_jour_9", reservedays_jour);
            param[9] = new SqlParameter("@Status_10", Status);
            param[10] = new SqlParameter("@ValueLimit_11", ValueLimit);
            param[11] = new SqlParameter("@days_1phase_12", days_1phase);
            param[12] = new SqlParameter("@amt_1phase_13", amt_1phase);
            param[13] = new SqlParameter("@days_2phase_14", days_2phase);
            param[14] = new SqlParameter("@amt_2phase_15", amt_2phase);
            param[15] = new SqlParameter("@days_1phasej_16", days_1phasej);
            param[16] = new SqlParameter("@amt_1phasej_17", amt_1phasej);
            param[17] = new SqlParameter("@days_2phasej_18", days_2phasej);
            param[18] = new SqlParameter("@amt_2phasej_19", amt_2phasej);
            param[19] = new SqlParameter("@shortname_20", shortname);
            param[20] = new SqlParameter("@loadingstatus_21", loadingstatus);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_classmstloadstatus_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateImageInsert(string userid, byte memberPic, byte memberSign, char status)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@userid", userid);
            param[1] = new SqlParameter("@pic", memberPic);
            param[2] = new SqlParameter("@sign", memberSign);
            param[3] = new SqlParameter("@status", status);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[imageinsert]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertCircReservationtransaction(string userid, int ctrlNo, DateTime reservationdate, int queno, string title, int id, string userid1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@ctrlNo_2", ctrlNo);
            param[2] = new SqlParameter("@reservationdate_3", reservationdate);
            param[3] = new SqlParameter("@queno_4", queno);
            param[4] = new SqlParameter("@title_5", title);
            param[5] = new SqlParameter("@id_6", id);
            param[6] = new SqlParameter("@userid1_7", userid1);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circreservationTR_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }
        public DataTable UpdateInsertEDocGroupMASTER(int Service_id, string Service_Name, string AccessionNo, string userid, string GroupCategory)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Service_id_1", Service_id);
            param[1] = new SqlParameter("@Service_Name_2", Service_Name);
            param[2] = new SqlParameter("@AccessionNo_3", AccessionNo);
            param[3] = new SqlParameter("@userid_4", userid);
            param[4] = new SqlParameter("@GroupCategory", GroupCategory);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_EDoc_Group_MASTER_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }
        public DataTable UpdateInsertDirectArchMoreInfo(int Daid, string Author, string Volume, string IssueNo, DateTime PubDate, string Part, string Edition, string Publisher, int PageNo, int Noofpage, DateTime FromPubdate, DateTime ToPubDate, int SourceType)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@Daid_1", Daid);
            param[1] = new SqlParameter("@Author_2", Author);
            param[2] = new SqlParameter("@Volume_3", Volume);
            param[3] = new SqlParameter("@IssueNo_4", IssueNo);
            param[4] = new SqlParameter("@PubDate_5", PubDate);
            param[5] = new SqlParameter("@Part_6", Part);
            param[6] = new SqlParameter("@Edition_7", Edition);
            param[7] = new SqlParameter("@Publisher_8", Publisher);
            param[8] = new SqlParameter("@PageNo_9", PageNo);
            param[9] = new SqlParameter("@Noofpage_10", Noofpage);
            param[10] = new SqlParameter("@FPubDate_11", FromPubdate);
            param[11] = new SqlParameter("@TPubdate_12", ToPubDate);
            param[12] = new SqlParameter("@SourceType", SourceType);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_DirectArchMoreInfo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateInsertCancelIndent(string vendorid, string ordernumber, string indentnumber, int departmentcode, string userid, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@vendorid_1", vendorid);
            param[1] = new SqlParameter("@ordernumber_2", ordernumber);
            param[2] = new SqlParameter("@indentnumber_3", indentnumber);
            param[3] = new SqlParameter("@departmentcode_4", departmentcode);
            param[4] = new SqlParameter("@userid_5", userid);
            param[5] = new SqlParameter("@FormId", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CancelIndent_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertCircAccompanyingItemIssue(string memid, string accno, string media_accno, string status, string userid)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@memid_1", memid);
            param[1] = new SqlParameter("@accno_2", accno);
            param[2] = new SqlParameter("@media_accno_3", media_accno);
            param[3] = new SqlParameter("@status_4", status);
            param[4] = new SqlParameter("@userid_5", userid);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_CircAccompanyingItemIssue_1]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateInsertReminderMaster(string ordernumber, string indentnumber, string remindernumber, DateTime reminderdate, string letternumber, DateTime validitydate, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ordernumber_1", ordernumber);
            param[1] = new SqlParameter("@indentnumber_2", indentnumber);
            param[2] = new SqlParameter("@remindernumber_3", remindernumber);
            param[3] = new SqlParameter("@reminderdate_4", reminderdate);
            param[4] = new SqlParameter("@letternumber_5", letternumber);
            param[5] = new SqlParameter("@validitydate_6", validitydate);
            param[6] = new SqlParameter("@userid_7", userid);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circreservationTR_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }
        public DataTable UpdateInsertStockProcess(string taskid, string accssion_no, DateTime sysdate, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@taskid_1", taskid);
            param[1] = new SqlParameter("@accssion_no_2", accssion_no);
            param[2] = new SqlParameter("@sysdate_3,", sysdate);
            param[3] = new SqlParameter("@userid_4", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Stock_process_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        //Check for work
        public DataTable UpdateInsertCircularmsgpost(string cid, string circularnumber, string subject, string matter, string postedby, DateTime mesdate, string type, string userid, int msgtypeid, string tomemberid, int msgFrom, int verified, DateTime effectiveFrom, DateTime to_dt)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@cid_1", cid);
            param[1] = new SqlParameter("@circularnumber_2", circularnumber);
            param[2] = new SqlParameter("@subject_3", subject);
            param[3] = new SqlParameter("@matter_4", matter);
            param[4] = new SqlParameter("@postedby_5", postedby);
            param[5] = new SqlParameter("@mesdate_6", mesdate);
            param[6] = new SqlParameter("@type_7", type);
            param[7] = new SqlParameter("@userid_8", userid);
            param[8] = new SqlParameter("@msgtypeid", msgtypeid);
            param[9] = new SqlParameter("@tomemberid", tomemberid);
            param[10] = new SqlParameter("@msgFrom", msgFrom);
            param[11] = new SqlParameter("@verified", verified);
            param[12] = new SqlParameter("@effectiveFrom", effectiveFrom);
            param[13] = new SqlParameter("@to_dt", to_dt);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circularmsgpost_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertBinderInvoicetrans(int invoice_id, decimal pay_amt, int noofcopy, string list_no, string typeofbinding, int FormID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@invoice_id_1", invoice_id);
            param[1] = new SqlParameter("@pay_amt_2", pay_amt);
            param[2] = new SqlParameter("@noofcopy_3", noofcopy);
            param[3] = new SqlParameter("@list_no_4", list_no);
            param[4] = new SqlParameter("@typeofbinding_5", typeofbinding);
            param[5] = new SqlParameter("@FormID", FormID);            
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_binder_inv_trans_1]", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateInsertNonArrive(string Journal_no, string Volume, string Issue, string Part, string DateofPublication, string Doc_Id, string Letter_No)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Journal_no_1", Journal_no);
            param[1] = new SqlParameter("@Volume_2", Volume);
            param[2] = new SqlParameter("@Issue_3", Issue);
            param[3] = new SqlParameter("@Part_4", Part);
            param[4] = new SqlParameter("@DateofPublication_5", DateofPublication);
            param[5] = new SqlParameter("@Doc_Id_6", Doc_Id);
            param[6] = new SqlParameter("@Letter_No_7", Letter_No);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NonArrive_Jour_Child_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];

        }
        public DataTable UpdateInsertUserTypeLavel(int UserTypeId, int MenuId, string Premission)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserTypeId_1", UserTypeId);
            param[1] = new SqlParameter("@MidMenu_Id_2", MenuId);
            param[2] = new SqlParameter("@Permission_3", Premission);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_UserTypeLavel_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());

            return dsd.Tables[0];
        }
        public DataTable UpdateinsertBarcodePrint(int id, int AccessionNo, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id_1", id);
            param[1] = new SqlParameter("@AccessionNo_2", AccessionNo);
            param[2] = new SqlParameter("@userid_3", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BarcodePrint_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertSMSSettings(string Message, DateTime SendDate, string Sendby, string SendTo, string Status, string PageName)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Message", Message);
            param[1] = new SqlParameter("@SendDate", SendDate);
            param[2] = new SqlParameter("@Sendby", Sendby);
            param[3] = new SqlParameter("@SendTo", SendTo);
            param[4] = new SqlParameter("@Status", Status);
            param[5] = new SqlParameter("@PageName", PageName);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SMSMessage]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateDirectInvoiceMaster(int srno, DateTime curr_date, decimal postage, decimal net_amt, decimal disc_amt, decimal disc_percentage, string vedorid, decimal handling_charge, decimal total_amt, decimal rate_followed_by, decimal pay_amt, string invoice_no, string bill_no, DateTime bill_date, string userid)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@srno_1", srno);
            param[1] = new SqlParameter("@curr_date_2", curr_date);
            param[2] = new SqlParameter("@postage_3", postage);
            param[3] = new SqlParameter("@net_amt_4", net_amt);
            param[4] = new SqlParameter("@disc_amt_5", disc_amt);
            param[5] = new SqlParameter("@disc_percentage_6", disc_percentage);
            param[6] = new SqlParameter("@vedorid_7", vedorid);
            param[7] = new SqlParameter("@handling_charge_8", handling_charge);
            param[8] = new SqlParameter("@total_amt_9", total_amt);
            param[9] = new SqlParameter("@rate_followed_by_10", rate_followed_by);
            param[10] = new SqlParameter("@pay_amt_11", pay_amt);
            param[11] = new SqlParameter("@invoice_no_12", invoice_no);
            param[12] = new SqlParameter("@bill_no_13", bill_no);
            param[13] = new SqlParameter("@bill_date_14", bill_date);
            param[14] = new SqlParameter("@userid_15", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_classmstloadstatus_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateJournalKeyword(int S_No, string Journal_No, string Keyword, string Status, string ItemType)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@S_No_1", S_No);
            param[1] = new SqlParameter("@Journal_No_2", Journal_No);
            param[2] = new SqlParameter("@Keyword_3", Keyword);
            param[3] = new SqlParameter("@Status_4", Status);
            param[4] = new SqlParameter("@ItemType_5", ItemType);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_Journal_Keyword_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateInsertBinderInvoice(int invoice_id, int Binder_id, string invoice_no, int invoice_amt, DateTime invoice_date, string Bill_serial_no, string userid, string type, string CancelStatus)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@invoice_id_1", invoice_id);
            param[1] = new SqlParameter("@Binder_id_2", Binder_id);
            param[2] = new SqlParameter("@invoice_no_3", invoice_no);
            param[3] = new SqlParameter("@invoice_amt_4", invoice_amt);
            param[4] = new SqlParameter("@invoice_date_5", invoice_date);
            param[5] = new SqlParameter("@Bill_serial_no_6", Bill_serial_no);
            param[6] = new SqlParameter("@userid_7", userid);
            param[7] = new SqlParameter("@handling_charge_8", type);
            param[8] = new SqlParameter("@CancelStatus_9", CancelStatus);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_Journal_Keyword_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }

        public DataTable updateinsertsecuritymaster(int Id, DateTime Date, float Amount, DateTime ToDate, string MemberId)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id", Id);
            param[1] = new SqlParameter("@Date", Date);
            param[2] = new SqlParameter("@Amount", Amount);
            param[3] = new SqlParameter("@ToDate", ToDate);
            param[4] = new SqlParameter("@MemberId", MemberId);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_SecurityMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertjournalinvoicemaster(string Invoice_id, string Invoice_No, DateTime Invoice_Date, string Order_Id, string Publisher_Code, string Currency, float Exchange_Rate, float PostageCharge, string Relation, string BillSerial_No, string Status, float Total_Amount, string ref_invoice_no, float Amount_Local, string pay_currency, float credit_amount, string credit_no, int Curr_code, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@Invoice_No_2", Invoice_No);
            param[2] = new SqlParameter("@Invoice_Date_3", Invoice_Date);
            param[3] = new SqlParameter("@Order_Id_4", Order_Id);
            param[4] = new SqlParameter("@Publisher_Code_5", Publisher_Code);
            param[5] = new SqlParameter("@Currency_6", Currency);
            param[6] = new SqlParameter("@Exchange_Rate_7", Exchange_Rate);
            param[7] = new SqlParameter("@PostageCharge_8", PostageCharge);
            param[8] = new SqlParameter("@Relation_9", Relation);
            param[9] = new SqlParameter("@BillSerial_No_10", BillSerial_No);
            param[10] = new SqlParameter("@Status_11", Status);
            param[11] = new SqlParameter("@Total_Amount_12", Total_Amount);
            param[12] = new SqlParameter("@ref_invoice_no_13", ref_invoice_no);
            param[13] = new SqlParameter("@Amount_Local_14", Amount_Local);
            param[14] = new SqlParameter("@pay_currency_15", pay_currency);
            param[15] = new SqlParameter("@credit_amount_16", credit_amount);
            param[16] = new SqlParameter("@credit_no_17", credit_no);
            param[18] = new SqlParameter("@Curr_code_18", Curr_code);
            param[19] = new SqlParameter("@userid_19", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalInvoice_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }

        public DataTable updateinsertjouracctopmtmaster(int paymentid, float amount, string paymenttype, int vendorid, DateTime paymentdate, string documentno, string documentno1, string Status_S, string userid , int FormID, int Type)

        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@paymentid_1", paymentid);
            param[1] = new SqlParameter("@amount_2", amount);
            param[2] = new SqlParameter("@paymenttype_3", paymenttype);
            param[3] = new SqlParameter("@vendorid_4", vendorid);
            param[4] = new SqlParameter("@paymentdate_5", paymentdate);
            param[5] = new SqlParameter("@documentno_6", documentno);
            param[6] = new SqlParameter("@Relation_7", documentno1);
            param[7] = new SqlParameter("@Status_S_8", Status_S);
            param[8] = new SqlParameter("@userid_9", userid);
            param[9] = new SqlParameter("@FormID", FormID);
            param[10] = new SqlParameter("@Type", Type);         
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_jour_acctopmtmaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertjournalmaster(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, string dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[52];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@subscription_no_2", subscription_no);
            param[2] = new SqlParameter("@issn_3", issn);
            param[3] = new SqlParameter("@journal_title_4", journal_title);
            param[4] = new SqlParameter("@title_abbreviation_5", title_abbreviation);
            param[5] = new SqlParameter("@series_title_6", series_title);
            param[6] = new SqlParameter("@spine_title_7", spine_title);
            param[7] = new SqlParameter("@entry_date_8", entry_date);
            param[8] = new SqlParameter("@start_date_9", start_date);
            param[9] = new SqlParameter("@expiry_date_10", expiry_date);
            param[10] = new SqlParameter("@total_volume_11", total_volume);
            param[11] = new SqlParameter("@issue_per_volume_12", issue_per_volume);
            param[12] = new SqlParameter("@part_per_issue_13", part_per_issue);
            param[13] = new SqlParameter("@starting_volume_14", starting_volume);
            param[14] = new SqlParameter("@ending_volume_15", ending_volume);
            param[15] = new SqlParameter("@starting_issue_16", starting_issue);
            param[16] = new SqlParameter("@ending_issue_17", ending_issue);
            param[17] = new SqlParameter("@starting_part_18", starting_part);
            param[18] = new SqlParameter("@ending_part_19", ending_part);
            param[19] = new SqlParameter("@priority_20", priority);
            param[20] = new SqlParameter("@publisher_21", publisher);
            param[21] = new SqlParameter("@tran_language_22", tran_language);
            param[22] = new SqlParameter("@department_23", department);
            param[23] = new SqlParameter("@sponsor_24", sponsor);
            param[24] = new SqlParameter("@pigeon_25", pigeon);
            param[25] = new SqlParameter("@delivery_lag_26", delivery_lag);
            param[26] = new SqlParameter("@agent_27", agent);
            param[27] = new SqlParameter("@delivery_mode_28", delivery_mode);
            param[28] = new SqlParameter("@frequency_29", frequency);
            param[29] = new SqlParameter("@subscription_status_30", subscription_status);
            param[30] = new SqlParameter("@Process_Stage_31", Process_Stage);
            param[31] = new SqlParameter("@Journal_status_32", Journal_status);
            param[32] = new SqlParameter("@Indexes_33", Indexes);
            param[33] = new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date);
            param[34] = new SqlParameter("@Entry_Mode_35", Entry_Mode);
            param[35] = new SqlParameter("@Journal_Id_36", Journal_Id);
            param[36] = new SqlParameter("@packagepart_37", packagepart);
            param[37] = new SqlParameter("@Reason_38", Reason);
            param[38] = new SqlParameter("@New_Status_39", New_Status);
            param[39] = new SqlParameter("@New_Reference_40", New_Reference);
            param[40] = new SqlParameter("@Operation_mode_41", Operation_mode);
            param[41] = new SqlParameter("@List_No_42", List_No);
            param[42] = new SqlParameter("@FromYear_43", Fromyear);
            param[43] = new SqlParameter("@Toyear_44", Toyear);
            param[44] = new SqlParameter("@publicationDate_45", PublicationDate);
            param[45] = new SqlParameter("@dateofpublication_46", dateofpublication);
            param[46] = new SqlParameter("@Mode_Status_47", Mode_Status);
            param[47] = new SqlParameter("@Effective_From_48", Effective_From);
            param[48] = new SqlParameter("@Media_type_49", media_Type);
            param[49] = new SqlParameter("@Bind_Status_50", Bind_Status);
            param[50] = new SqlParameter("@Url_51", Url);
            //param[51] = new SqlParameter("@loc_id_51_2", Loc_id);
            param[51] = new SqlParameter("@userid_52", userid);
            //param[52] = new SqlParameter("@Type_53", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinsertjourmarge(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, string dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, string userid, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[53];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@subscription_no_2", subscription_no);
            param[2] = new SqlParameter("@issn_3", issn);
            param[3] = new SqlParameter("@journal_title_4", journal_title);
            param[4] = new SqlParameter("@title_abbreviation_5", title_abbreviation);
            param[5] = new SqlParameter("@series_title_6", series_title);
            param[6] = new SqlParameter("@spine_title_7", spine_title);
            param[7] = new SqlParameter("@entry_date_8", entry_date);
            param[8] = new SqlParameter("@start_date_9", start_date);
            param[9] = new SqlParameter("@expiry_date_10", expiry_date);
            param[10] = new SqlParameter("@total_volume_11", total_volume);
            param[11] = new SqlParameter("@issue_per_volume_12", issue_per_volume);
            param[12] = new SqlParameter("@part_per_issue_13", part_per_issue);
            param[13] = new SqlParameter("@starting_volume_14", starting_volume);
            param[14] = new SqlParameter("@ending_volume_15", ending_volume);
            param[15] = new SqlParameter("@starting_issue_16", starting_issue);
            param[16] = new SqlParameter("@ending_issue_17", ending_issue);
            param[17] = new SqlParameter("@starting_part_18", starting_part);
            param[18] = new SqlParameter("@ending_part_19", ending_part);
            param[19] = new SqlParameter("@priority_20", priority);
            param[20] = new SqlParameter("@publisher_21", publisher);
            param[21] = new SqlParameter("@tran_language_22", tran_language);
            param[22] = new SqlParameter("@department_23", department);
            param[23] = new SqlParameter("@sponsor_24", sponsor);
            param[24] = new SqlParameter("@pigeon_25", pigeon);
            param[25] = new SqlParameter("@delivery_lag_26", delivery_lag);
            param[26] = new SqlParameter("@agent_27", agent);
            param[27] = new SqlParameter("@delivery_mode_28", delivery_mode);
            param[28] = new SqlParameter("@frequency_29", frequency);
            param[29] = new SqlParameter("@subscription_status_30", subscription_status);
            param[30] = new SqlParameter("@Process_Stage_31", Process_Stage);
            param[31] = new SqlParameter("@Journal_status_32", Journal_status);
            param[32] = new SqlParameter("@Indexes_33", Indexes);
            param[33] = new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date);
            param[34] = new SqlParameter("@Entry_Mode_35", Entry_Mode);
            param[35] = new SqlParameter("@Journal_Id_36", Journal_Id);
            param[36] = new SqlParameter("@packagepart_37", packagepart);
            param[37] = new SqlParameter("@Reason_38", Reason);
            param[38] = new SqlParameter("@New_Status_39", New_Status);
            param[39] = new SqlParameter("@New_Reference_40", New_Reference);
            param[40] = new SqlParameter("@Operation_mode_41", Operation_mode);
            param[41] = new SqlParameter("@List_No_42", List_No);
            param[42] = new SqlParameter("@FromYear_43", Fromyear);
            param[43] = new SqlParameter("@Toyear_44", Toyear);
            param[44] = new SqlParameter("@publicationDate_45", PublicationDate);
            param[45] = new SqlParameter("@dateofpublication_46", dateofpublication);
            param[46] = new SqlParameter("@Mode_Status_47", Mode_Status);
            param[47] = new SqlParameter("@Effective_From_48", Effective_From);
            param[48] = new SqlParameter("@Media_type_49", media_Type);
            param[49] = new SqlParameter("@Bind_Status_50", Bind_Status);
            param[50] = new SqlParameter("@Url_51", Url);
            
            param[51] = new SqlParameter("@userid_52", userid);
            param[52] = new SqlParameter("@Type_53", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournaldetailmaster(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, DateTime dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, int locid, string userid, string Type, int FormID, int UType)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[56];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@subscription_no_2", subscription_no);
            param[2] = new SqlParameter("@issn_3", issn);
            param[3] = new SqlParameter("@journal_title_4", journal_title);
            param[4] = new SqlParameter("@title_abbreviation_5", title_abbreviation);
            param[5] = new SqlParameter("@series_title_6", series_title);
            param[6] = new SqlParameter("@spine_title_7", spine_title);
            param[7] = new SqlParameter("@entry_date_8", entry_date);
            param[8] = new SqlParameter("@start_date_9", start_date);
            param[9] = new SqlParameter("@expiry_date_10", expiry_date);
            param[10] = new SqlParameter("@total_volume_11", total_volume);
            param[11] = new SqlParameter("@issue_per_volume_12", issue_per_volume);
            param[12] = new SqlParameter("@part_per_issue_13", part_per_issue);
            param[13] = new SqlParameter("@starting_volume_14", starting_volume);
            param[14] = new SqlParameter("@ending_volume_15", ending_volume);
            param[15] = new SqlParameter("@starting_issue_16", starting_issue);
            param[16] = new SqlParameter("@ending_issue_17", ending_issue);
            param[17] = new SqlParameter("@starting_part_18", starting_part);
            param[18] = new SqlParameter("@ending_part_19", ending_part);
            param[19] = new SqlParameter("@priority_20", priority);
            param[20] = new SqlParameter("@publisher_21", publisher);
            param[21] = new SqlParameter("@tran_language_22", tran_language);
            param[22] = new SqlParameter("@department_23", department);
            param[23] = new SqlParameter("@sponsor_24", sponsor);
            param[24] = new SqlParameter("@pigeon_25", pigeon);
            param[25] = new SqlParameter("@delivery_lag_26", delivery_lag);
            param[26] = new SqlParameter("@agent_27", agent);
            param[27] = new SqlParameter("@delivery_mode_28", delivery_mode);
            param[28] = new SqlParameter("@frequency_29", frequency);
            param[29] = new SqlParameter("@subscription_status_30", subscription_status);
            param[30] = new SqlParameter("@Process_Stage_31", Process_Stage);
            param[31] = new SqlParameter("@Journal_status_32", Journal_status);
            param[32] = new SqlParameter("@Indexes_33", Indexes);
            param[33] = new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date);
            param[34] = new SqlParameter("@Entry_Mode_35", Entry_Mode);
            param[35] = new SqlParameter("@Journal_Id_36", Journal_Id);
            param[36] = new SqlParameter("@packagepart_37", packagepart);
            param[37] = new SqlParameter("@Reason_38", Reason);
            param[38] = new SqlParameter("@New_Status_39", New_Status);
            param[39] = new SqlParameter("@New_Reference_40", New_Reference);
            param[40] = new SqlParameter("@Operation_mode_41", Operation_mode);
            param[41] = new SqlParameter("@List_No_42", List_No);
            param[42] = new SqlParameter("@FromYear_43", Fromyear);
            param[43] = new SqlParameter("@Toyear_44", Toyear);
            param[44] = new SqlParameter("@publicationDate_45", PublicationDate);
            param[45] = new SqlParameter("@dateofpublication_46", dateofpublication);
            param[46] = new SqlParameter("@Mode_Status_47", Mode_Status);
            param[47] = new SqlParameter("@Effective_From_48", Effective_From);
            param[48] = new SqlParameter("@Media_type_49", media_Type);
            param[49] = new SqlParameter("@Bind_Status_50", Bind_Status);
            param[50] = new SqlParameter("@Url_51", Url);
            param[51] = new SqlParameter("@loc_id_51_2", locid);
            param[52] = new SqlParameter("@userid_52", userid);
            param[53] = new SqlParameter("@Type_53", Type);
            param[54] = new SqlParameter("@FormID_54", FormID);
            param[55] = new SqlParameter("@Utype_55", UType);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_insert_JournalMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertjournalsubscription(string journal_no, string subscription_no, string issn, string journal_title, string title_abbreviation, string series_title, string spine_title, DateTime entry_date, DateTime start_date, DateTime expiry_date, int total_volume, int issue_per_volume, int part_per_issue, int starting_volume, int ending_volume, int starting_issue, int ending_issue, int starting_part, int ending_part, string priority, string publisher, int tran_language, int department, string sponsor, string pigeon, int delivery_lag, string agent, string delivery_mode, string frequency, string subscription_status, string Process_Stage, string Journal_status, int Indexes, DateTime SubscriptionStatus_Date, string Entry_Mode, string Journal_Id, string packagepart, string Reason, string New_Status, string New_Reference, string Operation_mode, string List_No, DateTime Fromyear, DateTime Toyear, DateTime PublicationDate, string dateofpublication, string Mode_Status, DateTime Effective_From, string media_Type, string Bind_Status, string Url, double Loc_id, string userid, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[53];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@subscription_no_2", subscription_no);
            param[2] = new SqlParameter("@issn_3", issn);
            param[3] = new SqlParameter("@journal_title_4", journal_title);
            param[4] = new SqlParameter("@title_abbreviation_5", title_abbreviation);
            param[5] = new SqlParameter("@series_title_6", series_title);
            param[6] = new SqlParameter("@spine_title_7", spine_title);
            param[7] = new SqlParameter("@entry_date_8", entry_date);
            param[8] = new SqlParameter("@start_date_9", start_date);
            param[9] = new SqlParameter("@expiry_date_10", expiry_date);
            param[10] = new SqlParameter("@total_volume_11", total_volume);
            param[11] = new SqlParameter("@issue_per_volume_12", issue_per_volume);
            param[12] = new SqlParameter("@part_per_issue_13", part_per_issue);
            param[13] = new SqlParameter("@starting_volume_14", starting_volume);
            param[14] = new SqlParameter("@ending_volume_15", ending_volume);
            param[15] = new SqlParameter("@starting_issue_16", starting_issue);
            param[16] = new SqlParameter("@ending_issue_17", ending_issue);
            param[17] = new SqlParameter("@starting_part_18", starting_part);
            param[18] = new SqlParameter("@ending_part_19", ending_part);
            param[19] = new SqlParameter("@priority_20", priority);
            param[20] = new SqlParameter("@publisher_21", publisher);
            param[21] = new SqlParameter("@tran_language_22", tran_language);
            param[22] = new SqlParameter("@department_23", department);
            param[23] = new SqlParameter("@sponsor_24", sponsor);
            param[24] = new SqlParameter("@pigeon_25", pigeon);
            param[25] = new SqlParameter("@delivery_lag_26", delivery_lag);
            param[26] = new SqlParameter("@agent_27", agent);
            //param[27] = new SqlParameter("@delivery_mode_28", delivery_mode);
            param[27] = new SqlParameter("@frequency_29", frequency);
            param[28] = new SqlParameter("@subscription_status_30", subscription_status);
            param[29] = new SqlParameter("@Process_Stage_31", Process_Stage);
            param[30] = new SqlParameter("@Journal_status_32", Journal_status);
            param[31] = new SqlParameter("@Indexes_33", Indexes);
            param[32] = new SqlParameter("@SubscriptionStatus_Date_34", SubscriptionStatus_Date);
            param[33] = new SqlParameter("@Entry_Mode_35", Entry_Mode);
            param[34] = new SqlParameter("@Journal_Id_36", Journal_Id);
            param[35] = new SqlParameter("@packagepart_37", packagepart);
            param[36] = new SqlParameter("@Reason_38", Reason);
            param[37] = new SqlParameter("@New_Status_39", New_Status);
            param[38] = new SqlParameter("@New_Reference_40", New_Reference);
            param[39] = new SqlParameter("@Operation_mode_41", Operation_mode);
            param[40] = new SqlParameter("@List_No_42", List_No);
            param[41] = new SqlParameter("@FromYear_43", Fromyear);
            param[42] = new SqlParameter("@Toyear_44", Toyear);
            param[43] = new SqlParameter("@publicationDate_45", PublicationDate);
            param[44] = new SqlParameter("@dateofpublication_46", dateofpublication);
            param[45] = new SqlParameter("@Mode_Status_47", Mode_Status);
            param[46] = new SqlParameter("@Effective_From_48", Effective_From);
            param[47] = new SqlParameter("@Media_type_49", media_Type);
            param[48] = new SqlParameter("@Bind_Status_50", Bind_Status);
            param[49] = new SqlParameter("@Url_51", Url);
            param[50] = new SqlParameter("@loc_id_51_2", Loc_id);
            param[51] = new SqlParameter("@userid_52", userid);
            param[52] = new SqlParameter("@Type_53", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_JournalMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }




















        public DataTable insertpostmessage(string cid, string circularNumber, string uploadfile, string uploadpath, string postedby)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@cid_1", cid);
            param[1] = new SqlParameter("@circularNumber_2", circularNumber);
            param[2] = new SqlParameter("@uploadfile_3", uploadfile);
            param[3] = new SqlParameter("@uploadpath_4", uploadpath);
            param[4] = new SqlParameter("@postedby_5", postedby);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_postMessages_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertnewspapermaster(string Newspaper_id, string NewsPaperTitle, DateTime start_date, DateTime PublicationDate, string frequency, int noofcopies, string vendorid, string status, string userid, float price, float totalamount, int T_id, string Weakend_dys, float Weakend_Prc)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Newspaper_id_1", Newspaper_id);
            param[1] = new SqlParameter("@NewsPaperTitle_2", NewsPaperTitle);
            param[2] = new SqlParameter("@start_date_3", start_date);
            param[3] = new SqlParameter("@PublicationDate_4", PublicationDate);
            param[4] = new SqlParameter("@frequency_5", frequency);
            param[5] = new SqlParameter("@noofcopies_6", noofcopies);
            param[6] = new SqlParameter("@vendorid_7", vendorid);
            param[7] = new SqlParameter("@status_8", status);
            param[8] = new SqlParameter("@userid_9", userid);
            param[9] = new SqlParameter("@price_10", price);
            param[10] = new SqlParameter("@totalamount_11", totalamount);
            param[11] = new SqlParameter("@T_id", T_id);
            param[12] = new SqlParameter("@Weakend_dys_13", Weakend_dys);
            param[13] = new SqlParameter("@Weakend_Prc_14", Weakend_Prc);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable insertdirectcatelogininfo(int Dsrno, int mediatype, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int noofcopies, float price, string publisherid, DateTime recordingdate, string seriesname, string form, string keywords, DateTime docDate, float Fprice, string FcurrencyCode, string subtitle, string part, float specialprice, int dept, string yearofPublication, int Language_Id, float exchange_rate, int no_of_pages, string page_size, string vendor_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[38];
            param[0] = new SqlParameter("@Dsrno_1", Dsrno);
            param[1] = new SqlParameter("@mediatype_2", mediatype);
            param[2] = new SqlParameter("@title_3", title);
            param[3] = new SqlParameter("@authortype_4", authortype);
            param[4] = new SqlParameter("@firstname1_5", firstname1);
            param[5] = new SqlParameter("@middlename1_6", middlename1);
            param[6] = new SqlParameter("@lastname1_7", lastname1);
            param[7] = new SqlParameter("@firstname2_8", firstname2);
            param[8] = new SqlParameter("@middlename2_9", middlename2);
            param[9] = new SqlParameter("@lastname2_10", lastname2);
            param[10] = new SqlParameter("@firstname3_11", firstname3);
            param[11] = new SqlParameter("@middlename3_12", middlename3);
            param[12] = new SqlParameter("@lastname3_13", lastname3);
            param[13] = new SqlParameter("@edition_14", edition);
            param[14] = new SqlParameter("@yearofedition_15", yearofedition);
            param[15] = new SqlParameter("@volumeno_16", volumeno);
            param[16] = new SqlParameter("@isbn_17", isbn);
            param[17] = new SqlParameter("@category_18", category);
            param[18] = new SqlParameter("@noofcopies_19", noofcopies);
            param[19] = new SqlParameter("@price_20", price);
            param[20] = new SqlParameter("@publisherid_21", publisherid);
            param[21] = new SqlParameter("@recordingdate_22", recordingdate);
            param[22] = new SqlParameter("@seriesname_23", seriesname);
            param[23] = new SqlParameter("@form_24", form);
            param[24] = new SqlParameter("@keywords_25", keywords);
            param[25] = new SqlParameter("@docDate_26", docDate);
            param[26] = new SqlParameter("@Fprice_27", Fprice);
            param[27] = new SqlParameter("@FcurrencyCode_28", FcurrencyCode);
            param[28] = new SqlParameter("@subtitle_29", subtitle);
            param[29] = new SqlParameter("@part_30", part);
            param[30] = new SqlParameter("@specialprice_31", specialprice);
            param[31] = new SqlParameter("@dept_32", dept);
            param[32] = new SqlParameter("@yearofPublication_33", yearofPublication);
            param[33] = new SqlParameter("@Language_Id_34", Language_Id);
            param[34] = new SqlParameter(" @exchange_rate_35", exchange_rate);
            param[35] = new SqlParameter("@no_of_pages_36", no_of_pages);
            param[36] = new SqlParameter("@page_size_37", page_size);
            param[37] = new SqlParameter("@vendor_id_38", vendor_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DirectCateloginfo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable Updateinsertcircusermanagement(string userid, string usercode, string firstname, string middlename, string lastname, int departmentcode, DateTime validupto, string status, string remarks, int issuebookstatus, string email1, string email2, string gender, string doj, string phone1, string phone2, string memberpic, string membername, string classname, string fathername, string dob, string catid, string adhaarno, string programid, string joinyear, string subjects, string userid1, string yearsem, string section, string bloodgrp, string session, string affiliation, string membersign, string printingstatus, string imagestatus, string studentthumb, string isthumb, string thumbtemplate1, string thumbtemplate2, string studentthumb2, string isthumb2, string thumbtemplate3, string thumbtemplate4, string mothername, string panNo, string photoname, string signName, string latitude, string longitude, string searchtext)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[49];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@usercode_26", usercode);
            param[2] = new SqlParameter("@firstname_2", firstname);
            param[3] = new SqlParameter("@middlename_3", middlename);
            param[4] = new SqlParameter("@lastname_4", lastname);
            param[5] = new SqlParameter("@departmentcode_6", departmentcode);
            param[6] = new SqlParameter("@validupto_7", validupto);
            param[7] = new SqlParameter("@status_8", status);
            param[8] = new SqlParameter("@remarks_9", remarks);
            param[9] = new SqlParameter("@issuedbookstatus", issuebookstatus);
            param[10] = new SqlParameter("@email1_11", email1);
            param[11] = new SqlParameter("@email2_12", email2);
            param[12] = new SqlParameter("@gender_13", gender);
            param[13] = new SqlParameter("@doj_14", doj);
            param[14] = new SqlParameter("@phone1_15", phone1);
            param[15] = new SqlParameter("@phone2_16", phone2);
            param[16] = new SqlParameter("@memberpic_17", memberpic);
            param[17] = new SqlParameter("@classname_18", classname);
            param[18] = new SqlParameter("@Fathername_19", fathername);
            param[19] = new SqlParameter("@Dob_20", dob);
            param[20] = new SqlParameter("@cat_id_21", catid);
            param[21] = new SqlParameter("@AadharNo_211", adhaarno);
            param[22] = new SqlParameter("@program_id_22", programid);
            param[23] = new SqlParameter("@Joinyear_23", joinyear);
            param[24] = new SqlParameter("@subjects_24", subjects);
            param[25] = new SqlParameter("@userid1_25", userid);
            param[26] = new SqlParameter("@YearSem_26", yearsem);
            param[27] = new SqlParameter("@Section_27", section);
            param[28] = new SqlParameter("@BloodGrp_28", bloodgrp);
            param[29] = new SqlParameter("@Session_29", session);
            param[30] = new SqlParameter("@affiliation_30", affiliation);
            param[31] = new SqlParameter("@memberSign", membersign);
            param[32] = new SqlParameter("@printing_status", printingstatus);
            param[33] = new SqlParameter("@image_status", imagestatus);
            param[34] = new SqlParameter("@StudentThumb", studentthumb);
            param[35] = new SqlParameter("@IsThumb", isthumb);
            param[36] = new SqlParameter("@ThumbTemplate1", thumbtemplate1);
            param[37] = new SqlParameter("@ThumbTemplate2", thumbtemplate2);
            param[38] = new SqlParameter("@StudentThumb2", studentthumb2);
            param[39] = new SqlParameter("@IsThumb2", isthumb2);
            param[40] = new SqlParameter("@ThumbTemplate3", thumbtemplate3);
            param[41] = new SqlParameter("@ThumbTemplate4", thumbtemplate4);
            param[42] = new SqlParameter("@mothername", mothername);
            param[43] = new SqlParameter("@pan_no", panNo);
            param[44] = new SqlParameter("@photoname", photoname);
            param[45] = new SqlParameter("@Signname", signName);
            param[46] = new SqlParameter("@Latitude", latitude);
            param[47] = new SqlParameter("@Longitude", longitude);
            param[48] = new SqlParameter("@SearchText", searchtext);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircUserManagement_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        //    public DataTable Updateinsertlibrarysetupinformation(string instutitutename , string libraryname , string address , city , pincode , string state , 
        //    string phoneNo,
        //     string   fax,
        //       string Email,
        //    string gram , string currency , string shortname , string remindervalidityperiod , string currentacademicsession
        //, string currentconversionfactor , string databasesource , string smtpipad , string discount , string orderarrivalgroup       
        //       , string isbackdateallowed , string isauditallowed ,  string isemailallowed , string cutternolength , string maxrespercopy , string jurlReminderperiod , string ipaddress1 , string ipaddress2 
        //   , string proxyadd , string iuser , string ipwd , string categoryid , string mediaid , string languageid , string language , string media , string category , string dbblocation1 , string dbblocation2 , string dbblocation3 , string dbservername , string dbuserid , string dbpassword ,    
        //     string menu ,string departmentcode , string defcity , string defstate , string defcountry , string disnewarr , string  circpassword , string checkbudget , string collegepicture , string theme , string downpass , string staffissuerequirement , string bbsecurity , string bbrent , string emproord , string emcanord , 

        //   )

        //    {
        //        DataSet dsd = new DataSet();
        //        SqlParameter[] param = new SqlParameter[5];
        //        param[0] = new SqlParameter("@nstitutename_1", userid);
        //        param[0] = new SqlParameter("@libraryname_2", userid);
        //        param[0] = new SqlParameter("@address_3", userid);
        //        param[0] = new SqlParameter("@city_4", userid);
        //        param[0] = new SqlParameter("@pincode_5", userid);
        //        param[0] = new SqlParameter("@state_6", userid);
        //        param[0] = new SqlParameter("@phoneno_7", userid);
        //        param[0] = new SqlParameter("@fax_8", userid);
        //        param[0] = new SqlParameter("@email_9", userid);
        //        param[0] = new SqlParameter("@gram_10", userid);
        //        param[0] = new SqlParameter("@currency_11", userid);
        //        param[0] = new SqlParameter("@shortname_12", userid);
        //        param[0] = new SqlParameter("@reminderValidityPeriod_13", userid);
        //        param[0] = new SqlParameter("@CurrentAcademicSession_14", userid);
        //        param[0] = new SqlParameter("@CurrencyConversionFactor_15", userid);
        //        param[0] = new SqlParameter("@databasesource_16", userid);
        //        param[0] = new SqlParameter("@smptp_IPadd_17", userid);
        //        param[0] = new SqlParameter("@discount_18", userid);
        //        param[0] = new SqlParameter("@OrderArrivalGap_19", userid);
        //        param[0] = new SqlParameter("@isBackDateAllowed_20", userid);
        //        param[0] = new SqlParameter("@isAuditRequired_21", userid);
        //        param[0] = new SqlParameter("@isEmailAllowed_22", userid);
        //        param[0] = new SqlParameter("@CutterNoLength_23", userid);
        //        param[0] = new SqlParameter("@MaxResPerCopy_24", userid);
        //        param[0] = new SqlParameter("@Jurl_RemainderPeriod_25", userid);
        //        param[0] = new SqlParameter("@IPAddress_1_26", userid);
        //        param[0] = new SqlParameter("@IPAddress_2_27", userid);
        //        param[0] = new SqlParameter("@proxyAdd_28", userid);
        //        param[0] = new SqlParameter("@iUser_29", userid);
        //        param[0] = new SqlParameter("@iPwd_30", userid);
        //        param[0] = new SqlParameter("@category_id_31", userid);
        //        param[0] = new SqlParameter("@media_id_32", userid);
        //        param[0] = new SqlParameter("@language_id_33", userid);
        //        param[0] = new SqlParameter("@language_34", userid);
        //        param[0] = new SqlParameter("@media_35", userid);
        //        param[0] = new SqlParameter("@category_36", userid);
        //        param[0] = new SqlParameter("@DBBLocation1_37", userid);
        //        param[0] = new SqlParameter("@DBBLocation2_38", userid);
        //        param[0] = new SqlParameter("@DBBLocation3_39", userid);
        //        param[0] = new SqlParameter("@dbservername_40", userid);
        //        param[0] = new SqlParameter("@dbuserid_41", userid);
        //        param[0] = new SqlParameter("@dbpassword_42", userid);
        //        param[0] = new SqlParameter("@menu_43", userid);
        //        param[0] = new SqlParameter("@departmentcode_44", userid);
        //        param[0] = new SqlParameter("@def_city_45", userid);
        //        param[0] = new SqlParameter("@def_state_46", userid);
        //        param[0] = new SqlParameter("@def_country_47", userid);
        //        param[0] = new SqlParameter("@dis_newarr_48", userid);
        //        param[0] = new SqlParameter("@circpassword_49", userid);
        //        param[0] = new SqlParameter("@checkBudget_50", userid);
        //        param[0] = new SqlParameter("@college_picture", userid);
        //        param[0] = new SqlParameter("@theme_51", userid);
        //        param[0] = new SqlParameter("@downpass_52", userid);
        //        param[0] = new SqlParameter("@StopIssueRetirement_53", userid);
        //        param[0] = new SqlParameter("@BBSecurity_54", userid);
        //        param[0] = new SqlParameter("@BBRent_55", userid);
        //        param[0] = new SqlParameter("@emProOrd_57", userid);
        //        param[0] = new SqlParameter("@emCanOrd_80", userid);
        //        param[0] = new SqlParameter("@emOrdRemLet_58", userid);
        //        param[0] = new SqlParameter("@emOrdConLet_59", userid);
        //        param[0] = new SqlParameter("@emRemLetPriPro_60", userid);
        //        param[0] = new SqlParameter("@emTran_61", userid);
        //        param[0] = new SqlParameter("@emIsu_62", userid);
        //        param[0] = new SqlParameter("@emReIsu_63", userid);
        //        param[0] = new SqlParameter("@emRet_64", userid);
        //        param[0] = new SqlParameter("@emIsuTU_65", userid);
        //        param[0] = new SqlParameter("@emRetTU_66", userid);
        //        param[0] = new SqlParameter("@emIsuBakLog_67", userid);
        //        param[0] = new SqlParameter("@emRetBakLog_68", userid);
        //        param[0] = new SqlParameter("@emIsuSpe_69", userid);
        //        param[0] = new SqlParameter("@emIsuTecUnp_70", userid);
        //        param[0] = new SqlParameter("@emRetTecUnp_71", userid);
        //        param[0] = new SqlParameter("@emODCRecEnt_72", userid);
        //        param[0] = new SqlParameter("@emODCIntRecEnt_73", userid);
        //        param[0] = new SqlParameter("@emODCWavOff_74", userid);
        //        param[0] = new SqlParameter("@emODCDetMem_75", userid);
        //        param[0] = new SqlParameter("@emOrdJou_76", userid);
        //        param[0] = new SqlParameter("@emOrdPack_77", userid);
        //        param[0] = new SqlParameter("@emJouPay_78", userid);
        //        param[0] = new SqlParameter("@emJouClaim_79", userid);
        //        param[0] = new SqlParameter("@OnlineP_81", userid);
        //        param[0] = new SqlParameter("@OrgSName_82", userid);
        //        param[0] = new SqlParameter("@msgOPAC_83", userid);
        //        param[0] = new SqlParameter("@OnlinePIndent_84", userid);
        //        param[0] = new SqlParameter("@Dept_B_Cat_85", userid);
        //        param[0] = new SqlParameter("@Jornal_Bind_All_86", userid);
        //        param[0] = new SqlParameter("@Binding_Must_87", userid);
        //        param[0] = new SqlParameter("@DigitalDocPath_88", userid);
        //        param[0] = new SqlParameter("@WithoutOIP_89", userid);
        //        param[0] = new SqlParameter("@WebSite_90", userid);
        //        param[0] = new SqlParameter("@IsLMenuSlider_91", userid);
        //        param[0] = new SqlParameter("@DescImage", userid);
        //        param[0] = new SqlParameter("@PrimaryDescType", userid);
        //        param[0] = new SqlParameter("@smtp_Port_92", userid);
        //        param[0] = new SqlParameter("@BookReturnDate_Msg_93", userid);
        //        param[0] = new SqlParameter("@OverDue_Msg_94", userid);
        //        param[0] = new SqlParameter("@DBookBind_95", userid);
        //        param[0] = new SqlParameter("@msgPopUp_96", userid);
        //        param[0] = new SqlParameter("@SendMailAttempt_97", userid);
        //        param[0] = new SqlParameter("@Organization_pic_98", userid);
        //        param[0] = new SqlParameter("@reminder_email", userid);
        //        param[0] = new SqlParameter("@EnableBiometric", userid);
        //        param[0] = new SqlParameter("@EnableDualbiometric", userid);
        //        param[0] = new SqlParameter("@EnableAutoPassword", userid);
        //        param[0] = new SqlParameter("@indent_comb_prnt", userid);
        //        param[0] = new SqlParameter("@bookreturn_reminder", userid);
        //        param[0] = new SqlParameter("@usersdataband", userid);
        //        param[0] = new SqlParameter("@Enble_FregImm", userid);
        //        param[0] = new SqlParameter("@Multi_Issue", userid);
        //        param[0] = new SqlParameter("@addPublisher", userid);
        //        param[0] = new SqlParameter("@addResources", userid);
        //        param[0] = new SqlParameter("@jrnl_reminder_mobno", userid);
        //        param[0] = new SqlParameter("@CallNo", userid);
        //        param[0] = new SqlParameter("@CpyInform", userid);
        //        param[0] = new SqlParameter("@Reserve", userid);
        //        param[0] = new SqlParameter("@Content", userid);
        //        param[0] = new SqlParameter("@AddKey", userid);
        //        param[0] = new SqlParameter("@AddCart", userid);
        //        param[0] = new SqlParameter("@AuthorIfo", userid);
        //        param[0] = new SqlParameter("@BookInfo", userid);
        //        param[0] = new SqlParameter("@EArticlePath", userid);
        //        param[0] = new SqlParameter("@EProjectPath", userid);
        //        param[0] = new SqlParameter("@EThesisPath", userid);
        //        param[0] = new SqlParameter("@EJournalPath", userid);
        //        param[0] = new SqlParameter("@ant1", userid);
        //        param[0] = new SqlParameter("@ant2", userid);
        //        param[0] = new SqlParameter("@ant3", userid);
        //        param[0] = new SqlParameter("@ant4", userid);

        //    }

        public DataTable UpdateEditTableMult(string accessionnumber, string booktitle, int Ctrl_no, int accessionid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AccNo_1", accessionnumber);
            param[1] = new SqlParameter("@Title_2", booktitle);
            param[2] = new SqlParameter("@AccID_3", Ctrl_no);
            param[3] = new SqlParameter("@Crtl_no_4", accessionid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[UpDate_EditTableMult_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable UpdateEditTable(int Ctrl_no, string accessionnumber, string classnumber, string booknumber, string booktitle, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string subject1, string subject2, string subject3, int accessionid)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@Ctrl_no", Ctrl_no);
            param[1] = new SqlParameter("@AccNo_1", accessionnumber);
            param[2] = new SqlParameter("@ClassNo_2", classnumber);
            param[3] = new SqlParameter("@BookNo_3", booknumber);
            param[4] = new SqlParameter("@Title_4", booktitle);
            param[5] = new SqlParameter("@AF1_5", firstname1);
            param[6] = new SqlParameter("@AM2_6", middlename1);
            param[7] = new SqlParameter("@AL3_7", lastname1);
            param[8] = new SqlParameter("@AF2_8", firstname2);
            param[9] = new SqlParameter("@AM2_9", middlename2);
            param[10] = new SqlParameter("@AL2_10", lastname2);
            param[11] = new SqlParameter("@AF3_11", firstname3);
            param[12] = new SqlParameter("@AM3_12", middlename3);
            param[13] = new SqlParameter("@AL3_13", lastname3);
            param[14] = new SqlParameter("@Sub1_14", subject1);
            param[15] = new SqlParameter("@Sub2_15", subject2);
            param[16] = new SqlParameter("@Sub3_16", subject3);
            param[17] = new SqlParameter("@AccID_17", accessionid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[UpDate_EditTable_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }

        public DataTable DeleteEditTableMult(string Accessionnumber)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Accession_1", Accessionnumber);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Delete_EdittableMul_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable DeleteEditTable(int Ctrl_no)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Ctrl_no", Ctrl_no);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[Delete_Edittable_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable InsertMarcext(int ctrl_no)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Ctrl_no", ctrl_no);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[MARCEXT_impData]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable InsertInsTest(int c1, string c2)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@c1", c1);
            param[1] = new SqlParameter("@c2", c2);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insTest]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable DeleteAdduserSubject(string property)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@vchProperty", property);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[dt_adduserobject_vcs]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable Deletegetpropertiesbyid(int objectid, string property)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", objectid);
            param[1] = new SqlParameter("@property", property);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[dt_getpropertiesbyid_vcs]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateInsertCurrent_awairnes(string member_id, string item, string subject, string frequency, DateTime from_date, DateTime to_date, DateTime Sent_upto, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@member_id_1", member_id);
            param[1] = new SqlParameter("@item_2", item);
            param[2] = new SqlParameter("@subject_3", subject);
            param[3] = new SqlParameter("@frequency_4", frequency);
            param[4] = new SqlParameter("@from_date_5", from_date);
            param[5] = new SqlParameter("@to_date_6", to_date);
            param[6] = new SqlParameter("@Sent_upto_7", userid);
            param[7] = new SqlParameter("@userid_8", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Current_awairnes_ser_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertLibraryService(int invoice_Id, string invoice_no, DateTime invoice_date, string library, string member, decimal service_tax, decimal cess, DateTime duedate, decimal Actual_amt, decimal total_amt, decimal postage, decimal tax, string userid, string Pmt_Type, decimal PaidAmt, decimal BallanceAmt, string DD_ChkNo, DateTime DD_ChkDate, decimal DD_charge, string Bank)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@invoice_Id_1", invoice_Id);
            param[1] = new SqlParameter("@invoice_no_2", invoice_no);
            param[2] = new SqlParameter("@invoice_date_3", invoice_date);
            param[3] = new SqlParameter("@library_4", library);
            param[4] = new SqlParameter("@member_5", member);
            param[5] = new SqlParameter("@service_tax_6", service_tax);
            param[6] = new SqlParameter("@cess_7", cess);
            param[7] = new SqlParameter("@duedate_8", duedate);
            param[8] = new SqlParameter("@Actual_amt_9", Actual_amt);
            param[9] = new SqlParameter("@total_amt_10", total_amt);
            param[10] = new SqlParameter("@postage_11", postage);
            param[11] = new SqlParameter("@tax_12", tax);
            param[12] = new SqlParameter("@userid_13", userid);
            param[13] = new SqlParameter("@Pmt_Type_14", Pmt_Type);
            param[14] = new SqlParameter("@PaidAmt_15", PaidAmt);
            param[15] = new SqlParameter("@BallanceAmt_16", BallanceAmt);
            param[16] = new SqlParameter("@DD_ChkNo_17", DD_ChkNo);
            param[17] = new SqlParameter("@DD_ChkDate_18", DD_ChkDate);
            param[18] = new SqlParameter("@DD_charge_19", DD_charge);
            param[19] = new SqlParameter("@Bank_20", Bank);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_library_servicesMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateInsertStockEntryMaster(string docno, DateTime stockdate, string classnumber_from, string classnumber_to, int total_missing, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@docno_1", docno);
            param[1] = new SqlParameter("@stockdate_2", stockdate);
            param[2] = new SqlParameter("@classnumber_from_3", classnumber_from);
            param[3] = new SqlParameter("@classnumber_to_4", classnumber_to);
            param[4] = new SqlParameter("@total_missing_5", total_missing);
            param[5] = new SqlParameter("@userid_6", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Stock_Entry_Master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertOnlineJournal(string Journal_Title, string Url, DateTime From_date, DateTime To_Date, string Jurl_Id, int Dept, string userid, string Department, string pay_Mode)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Journal_Title_1", Journal_Title);
            param[1] = new SqlParameter("@Url_2", Url);
            param[2] = new SqlParameter("@From_date_3", From_date);
            param[3] = new SqlParameter("@To_Date_4", To_Date);
            param[4] = new SqlParameter("@Jurl_Id_5", Jurl_Id);
            param[5] = new SqlParameter("@Dept_6", Dept);
            param[6] = new SqlParameter("@userid_7", userid);
            param[7] = new SqlParameter("@Department_8", Department);
            param[8] = new SqlParameter("@pay_Mode_9", pay_Mode);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OnlineJournal_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable UpdateInsertJournalInvoiceChild(string Invoice_id, string Journal_Id, decimal Discount, decimal Amount, decimal Balance, decimal Rs_E, string Jour_Status, decimal print_Amt, decimal online_Amt, decimal p_o_Amt, string currency, decimal exchange_rate, string postage)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@Invoice_id_1", Invoice_id);
            param[1] = new SqlParameter("@Journal_Id_2", Journal_Id);
            param[2] = new SqlParameter("@Discount_3", Discount);
            param[3] = new SqlParameter("@Amount_4", Amount);
            param[4] = new SqlParameter("@Balance_5", Balance);
            param[5] = new SqlParameter("@Rs_E_6", Rs_E);
            param[6] = new SqlParameter("@Jour_Status_7", Jour_Status);
            param[7] = new SqlParameter("@print_Amt_8", print_Amt);
            param[8] = new SqlParameter("@online_Amt_9", online_Amt);
            param[9] = new SqlParameter("@p_o_Amt_10", p_o_Amt);
            param[10] = new SqlParameter("@currency", currency);
            param[11] = new SqlParameter("@exchange_rate", exchange_rate);
            param[12] = new SqlParameter("@postage", postage);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_JournalInvoice_Child_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable UpdateInsertCircClassMaster(string classname, int totalissueddays, int noofbookstobeissued, decimal finperday, int reservedays, int totalissueddays_jour, int noofjournaltobeissued, decimal fineperday_jour, int reservedays_jour, string Status, string canRequest, int ValueLimit, int days_1phase, decimal amt_1phase, int days_2phase, decimal amt_2phase, int days_1phasej, decimal amt_1phasej, int days_2phasej, decimal amt_2phasej, string shortname, string userid, string policystatus, string MembershipType, string Security)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[25];
            param[0] = new SqlParameter("@classname_1", classname);
            param[1] = new SqlParameter("@totalissueddays_2", totalissueddays);
            param[2] = new SqlParameter("@noofbookstobeissued_3", noofbookstobeissued);
            param[3] = new SqlParameter("@finperday_4", finperday);
            param[4] = new SqlParameter("@reservedays_5", reservedays);
            param[5] = new SqlParameter("@totalissueddays_jour_6", totalissueddays_jour);
            param[6] = new SqlParameter("@noofjournaltobeissued_7", noofjournaltobeissued);
            param[7] = new SqlParameter("@fineperday_jour_8", fineperday_jour);
            param[8] = new SqlParameter("@reservedays_jour_9", reservedays_jour);
            param[9] = new SqlParameter("@Status_10", Status);
            param[10] = new SqlParameter("@canRequest_11", canRequest);
            param[11] = new SqlParameter("@ValueLimit_12", ValueLimit);
            param[12] = new SqlParameter("@days_1phase_13", days_1phase);
            param[13] = new SqlParameter("@amt_1phase_14", amt_1phase);
            param[14] = new SqlParameter("@days_2phase_15", days_2phase);
            param[15] = new SqlParameter("@amt_2phase_16", amt_2phasej);
            param[16] = new SqlParameter("@days_1phasej_17", days_1phasej);
            param[17] = new SqlParameter("@amt_1phasej_18", amt_1phasej);
            param[18] = new SqlParameter("@days_2phasej_19", days_2phasej);
            param[19] = new SqlParameter("@amt_2phasej_20", amt_2phasej);
            param[20] = new SqlParameter("@shortname_21", shortname);
            param[21] = new SqlParameter("@userid_22", userid);
            param[22] = new SqlParameter("@loadingstatus_23", policystatus);
            param[23] = new SqlParameter("@loadingstatus_24", MembershipType);
            param[24] = new SqlParameter("@loadingstatus_25", Security);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[insert_CircClassMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable SpHelpDiagramDefnition(string name)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@diagramname", name);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_helpdiagramdefinition]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable Sprenamedigram(string name)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@diagramname", name);
            param[1] = new SqlParameter("@new_diagramname", name);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_renamediagram]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable Spalterdiagram(string name, int version, byte defintion)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@diagramname", name);
            param[1] = new SqlParameter("@version", version);
            param[2] = new SqlParameter("@definition", defintion);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_alterdiagram]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable Spdropdigram(string name)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@diagramname", name);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_dropdiagram]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        public DataTable Sphelpdigrams(string name)
        {
            DataTable dsd = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@diagramname", name);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("[dbo].[sp_helpdiagrams]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd;
        }
        //akansha






        public DataTable updateinsertjournalarrival(string journal_no, string exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, string publication_Dated, /*DateTime publicationDatee,*/ string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDate, int FormID  , int Copy_No)



        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@exp_date_2", exp_date);
            param[2] = new SqlParameter("@volume_3", volume);
            param[3] = new SqlParameter("@issues_4", issues);
            param[4] = new SqlParameter("@parts_5", parts);
            param[5] = new SqlParameter("@indexes_6", indexes);
            param[6] = new SqlParameter("@Status_7", Status);
            param[7] = new SqlParameter("@Remarks_8", Remarks);
            param[8] = new SqlParameter("@doc_id_9", doc_id);
            param[9] = new SqlParameter("@issue_type_10", issue_type);
            param[10] = new SqlParameter("@arr_date_11", arr_date);
            param[11] = new SqlParameter("@arr_year_12", arr_year);

            param[12] = new SqlParameter("@publication_Date_13", publicationDate);
            param[13] = new SqlParameter("@ISSNNO_14", ISSNNO);
            param[14] = new SqlParameter("@Media_Print_15", Media_Print);
            param[15] = new SqlParameter("@Media_Online_16", Media_Online);
            param[16] = new SqlParameter("@PublicationDate_17", publicationDate);
            param[18] = new SqlParameter("@FormID",FormID);
            param[17] = new SqlParameter("@Copy_No", Copy_No);
            
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertjourarrival(string journal_no, string exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, string publication_Dated, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDate,  int Copy_No)



        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@exp_date_2", exp_date);
            param[2] = new SqlParameter("@volume_3", volume);
            param[3] = new SqlParameter("@issues_4", issues);
            param[4] = new SqlParameter("@parts_5", parts);
            param[5] = new SqlParameter("@indexes_6", indexes);
            param[6] = new SqlParameter("@Status_7", Status);
            param[7] = new SqlParameter("@Remarks_8", Remarks);
            param[8] = new SqlParameter("@doc_id_9", doc_id);
            param[9] = new SqlParameter("@issue_type_10", issue_type);
            param[10] = new SqlParameter("@arr_date_11", arr_date);
            param[11] = new SqlParameter("@arr_year_12", arr_year);

            param[12] = new SqlParameter("@publication_Date_13",publication_Dated);
            param[13] = new SqlParameter("@ISSNNO_14", ISSNNO);
            param[14] = new SqlParameter("@Media_Print_15", Media_Print);
            param[15] = new SqlParameter("@Media_Online_16", Media_Online);
            param[16] = new SqlParameter("@PublicationDate_17", publicationDate);
           
            param[17] = new SqlParameter("@Copy_No", Copy_No);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }




        public DataTable updateinsertproformainvoice(string journal_no, string exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, DateTime publication_Date, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDate)


        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@exp_date_2", exp_date);
            param[2] = new SqlParameter("@volume_3", volume);
            param[3] = new SqlParameter("@issues_4", issues);
            param[4] = new SqlParameter("@parts_5", parts);
            param[5] = new SqlParameter("@indexes_6", indexes);
            param[6] = new SqlParameter("@Status_7", Status);
            param[7] = new SqlParameter("@Remarks_8", Remarks);
            param[8] = new SqlParameter("@doc_id_9", doc_id);
            param[9] = new SqlParameter("@issue_type_10", issue_type);
            param[10] = new SqlParameter("@arr_date_11", arr_date);
            param[11] = new SqlParameter("@arr_year_12", arr_year);
            param[12] = new SqlParameter("@publication_Date_13", publicationDate);
            param[13] = new SqlParameter("@ISSNNO_14", ISSNNO);
            param[14] = new SqlParameter("@Media_Print_15", Media_Print);
            param[15] = new SqlParameter("@Media_Online_16", Media_Online);
            param[16] = new SqlParameter("@PublicationDate_17", publicationDate);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }





        public DataTable updateinsertBindTransactionMst(string List_No, DateTime List_Date, string BindType, string Binder_id, DateTime Exp_Arrival_Date, string Type, int noofcopy, string userid, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@List_No_1", List_No);
            param[1] = new SqlParameter("@List_Date_2", List_Date);
            param[2] = new SqlParameter("@BindType_3", BindType);
            param[3] = new SqlParameter("@Binder_id_4", Binder_id);
            param[4] = new SqlParameter("@Exp_Arrival_Date_5", Exp_Arrival_Date);
            param[5] = new SqlParameter("@Type_6", Type);
            param[6] = new SqlParameter("@noofcopy_7", noofcopy);
            param[7] = new SqlParameter("@userid_8", userid);
            param[8] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BindTransaction_Mst_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertOPACINDENT(int mediatype, string requestercode, int departmentcode, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, string coursenumber, string publisherid, string seriesname, string yearofPublication, string IndentId, string Vpart, string subtitle, int Language_Id, string indentnumber, int noofcopy)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@mediatype_1", mediatype);
            param[1] = new SqlParameter("@requestercode_2", requestercode);
            param[2] = new SqlParameter("@departmentcode_3", departmentcode);
            param[3] = new SqlParameter("@title_4", title);
            param[4] = new SqlParameter("@authortype_5", authortype);
            param[5] = new SqlParameter("@firstname1_6", firstname1);
            param[6] = new SqlParameter("@middlename1_7", middlename1);
            param[7] = new SqlParameter("@lastname1_8", lastname1);
            param[8] = new SqlParameter("@firstname2_9", firstname2);
            param[9] = new SqlParameter("@middlename2_10", middlename2);
            param[10] = new SqlParameter("@lastname2_11", lastname3);
            param[11] = new SqlParameter("@firstname3_12", firstname3);
            param[12] = new SqlParameter("@middlename3_13", middlename3);
            param[13] = new SqlParameter("@lastname3_14", lastname3);
            param[14] = new SqlParameter("@edition_15", edition);
            param[15] = new SqlParameter("@yearofedition_16", yearofedition);
            param[16] = new SqlParameter("@volumeno_17", volumeno);
            param[17] = new SqlParameter("@isbn_18", isbn);
            param[18] = new SqlParameter("@coursenumber_19", coursenumber);
            param[19] = new SqlParameter("@publisherid_20", publisherid);
            param[20] = new SqlParameter("@seriesname_21", seriesname);
            param[21] = new SqlParameter("@yearofPublication_22", yearofPublication);
            param[22] = new SqlParameter("@IndentId_23", IndentId);
            param[23] = new SqlParameter("@Vpart_24", Vpart);
            param[24] = new SqlParameter("@subtitle_26", subtitle);
            param[25] = new SqlParameter("@Language_Id_27", Language_Id);
            param[26] = new SqlParameter("@indentnumber_28", indentnumber);
            param[27] = new SqlParameter("@noofcopy_29", noofcopy);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_OPACINDENT_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertordermaster(string ordernumber, DateTime exparivaldateapproval, DateTime exparivaldatenonapproval, string indentnumber, DateTime orderdate, string letternumber, DateTime letterdate, int cancelorder, int itemnumber, int departmentcode, decimal orderamount, string vendorid, int identityofordernumber, int order_check_code, string userid, string IpAddress)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@ordernumber_1", ordernumber);
            param[1] = new SqlParameter("@exparivaldateapproval_2", exparivaldateapproval);
            param[2] = new SqlParameter("@exparivaldatenonapproval_3", exparivaldatenonapproval);
            param[3] = new SqlParameter("@indentnumber_4", indentnumber);
            param[4] = new SqlParameter("@orderdate_5", orderdate);
            param[5] = new SqlParameter("@letternumber_6", letternumber);
            param[6] = new SqlParameter("@letterdate_7", letterdate);
            param[7] = new SqlParameter("@cancelorder_8", cancelorder);
            param[8] = new SqlParameter("@itemnumber_9", itemnumber);
            param[9] = new SqlParameter("@departmentcode_10", departmentcode);
            param[10] = new SqlParameter("@orderamount_11", orderamount);
            param[11] = new SqlParameter("@vendorid_12", vendorid);
            param[12] = new SqlParameter("@identityofordernumber_13", identityofordernumber);
            param[13] = new SqlParameter("@order_check_code_14", order_check_code);
            param[14] = new SqlParameter("@user_id", userid);
            param[15] = new SqlParameter("@IpAddress", IpAddress);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ordermaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertletteracctoPmtTrans(int PaymentID, string InvoiceID, decimal InvoiceAmount)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PaymentID_1", PaymentID);
            param[1] = new SqlParameter("@InvoiceID_2", InvoiceID);
            param[2] = new SqlParameter("@InvoiceAmount_3", InvoiceAmount);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_letter_acctoPmtTrans_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertinvoicemaster2(string invoicenumber, DateTime invoicedate, int invoiceid, decimal postage, decimal netamount, decimal discountamount, decimal discountpercentage, string vendorid, string billserialno, decimal handlingcharge, string payCurrency, decimal payAmount, string reportingtypeofinvoice, decimal total_amt, string userid, string IndentNumber, DateTime IndentDate, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@invoicenumber_1", invoicenumber);
            param[1] = new SqlParameter("@invoicedate_2", invoicedate);
            param[2] = new SqlParameter("@invoiceid_3", invoiceid);
            param[3] = new SqlParameter("@postage_4", postage);
            param[4] = new SqlParameter("@netamount_5", netamount);
            param[5] = new SqlParameter("@discountamount_6", discountamount);
            param[6] = new SqlParameter("@discountpercentage_7", discountpercentage);
            param[7] = new SqlParameter("@vendorid_8", vendorid);
            param[8] = new SqlParameter("@billserialno_9", billserialno);
            param[9] = new SqlParameter("@handlingcharge_10", handlingcharge);
            param[10] = new SqlParameter("@payCurrency_11", payCurrency);
            param[11] = new SqlParameter("@payAmount_12", payAmount);
            param[12] = new SqlParameter("@typeofinvoice_13", reportingtypeofinvoice);
            param[13] = new SqlParameter("@total_amt_14", total_amt);
            param[14] = new SqlParameter("@user_id_15", userid);
            param[15] = new SqlParameter("@indent_number_16", IndentNumber);
            param[16] = new SqlParameter("@indent_date_17", IndentDate);
            param[17] = new SqlParameter("@FormID",FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_invoicemaster_2]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournalaccessioning(string accession_no, string part_no, string journal_no, string volume, string fromIssue, string toIssue, string Lack_no, string accession_id, DateTime date_of_accessioning, string issue_status, string curr_year, string From_date, DateTime To_date, string userid, int Issue_No, int Copy_No)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@accession_no_1", accession_no);
            param[1] = new SqlParameter("@part_no_2", part_no);
            param[2] = new SqlParameter("@journal_no_3", journal_no);
            param[3] = new SqlParameter("@volume_4", volume);
            param[4] = new SqlParameter("@fromIssue_5", fromIssue);
            param[5] = new SqlParameter("@toIssue_6", toIssue);
            param[6] = new SqlParameter("@Lack_no_7", Lack_no);
            param[7] = new SqlParameter("@accession_id_8", accession_id);
            param[8] = new SqlParameter("@date_of_accessioning_9", date_of_accessioning);
            param[9] = new SqlParameter("@issue_status_10", issue_status);
            param[10] = new SqlParameter("@curr_year_11", curr_year);
            param[11] = new SqlParameter("@From_date_12", From_date);
            param[12] = new SqlParameter("@To_date_13", To_date);
            param[13] = new SqlParameter("@userid_14", userid);
            param[14] = new SqlParameter("@Issue_No", Issue_No);
            param[15] = new SqlParameter("@Copy_No", Copy_No);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_accessioning_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertNewsPaperTransaction(string Id, DateTime ArrivalDate, int ActualNoofcopies, decimal ActualPricePerCopy, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id_1", Id);
            param[1] = new SqlParameter("@ArrivalDate_2", ArrivalDate);
            param[2] = new SqlParameter("@ActualNoofcopies_3", ActualNoofcopies);
            param[3] = new SqlParameter("@ActualPricePerCopy_4", ActualPricePerCopy);
            param[4] = new SqlParameter("@userid_5", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperTransaction_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertModifiedOrderMaster(string ordernumber, string vendorid, string indentnumber, string title, int noofcopies, decimal OrderredPricePerCopy, decimal ActualPricePerCopy, string Status, decimal srno, DateTime dateofarrival, decimal price, decimal bankrate, decimal gocrate, decimal discount, string docno, int gift_arrival, string accessioned, string GRN, DateTime GRD, string proof_price, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@ordernumber_1", ordernumber);
            param[1] = new SqlParameter("@vendorid_2", vendorid);
            param[2] = new SqlParameter("@indentnumber_3", indentnumber);
            param[3] = new SqlParameter("@title_4", title);
            param[4] = new SqlParameter("@noofcopies_5", noofcopies);
            param[5] = new SqlParameter("@OrderredPricePerCopy_6", OrderredPricePerCopy);
            param[6] = new SqlParameter("@ActualPricePerCopy_7", ActualPricePerCopy);
            param[7] = new SqlParameter("@Status_8", Status);
            param[8] = new SqlParameter("@srno_9", srno);
            param[9] = new SqlParameter("@dateofarrival_10", dateofarrival);
            param[10] = new SqlParameter("@price_11", price);
            param[11] = new SqlParameter("@bankrate_12", bankrate);
            param[12] = new SqlParameter("@gocrate_13", gocrate);
            param[13] = new SqlParameter("@discount_14", discount);
            param[14] = new SqlParameter("@docno_15", docno);
            param[15] = new SqlParameter("@gift_arrival_16", gift_arrival);
            param[16] = new SqlParameter("@accessioned_17", accessioned);
            param[17] = new SqlParameter("@GRN_18", GRN);
            param[18] = new SqlParameter("@GRD_19", GRD);
            param[19] = new SqlParameter("@proof_price_20", proof_price);
            param[20] = new SqlParameter("@user_id", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ModifiedOrderMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertExistingJournalPayment(string Journal_No, string Journal_Id, string Invoice_No, DateTime Invoice_Date, int Draft_No, DateTime Draft_date, decimal Journal_Amount, string currency)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Journal_No_1", Journal_No);
            param[1] = new SqlParameter("@Journal_Id_2", Journal_Id);
            param[2] = new SqlParameter("@Invoice_No_3", Invoice_No);
            param[3] = new SqlParameter("@Invoice_Date_4", Invoice_Date);
            param[4] = new SqlParameter("@Draft_No_5", Draft_No);
            param[5] = new SqlParameter("@Draft_date_6", Draft_date);
            param[6] = new SqlParameter("@Journal_Amount_7", Journal_Amount);
            param[7] = new SqlParameter("@currency_8", currency);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_NewsPaperTransaction_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournalRecTrans(string userid, string accno, DateTime receivingdate, decimal fineamount, string fineCause, string isPaid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@userid_1", userid);
            param[1] = new SqlParameter("@accno_2", accno);
            param[2] = new SqlParameter("@receivingdate_3", receivingdate);
            param[3] = new SqlParameter("@fineamount_4", fineamount);
            param[4] = new SqlParameter("@fineCause_5", fineCause);
            param[5] = new SqlParameter("@isPaid_6", isPaid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_RecTrans_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertCircDesignationMaster(string Designationid, string Designation, string classname, string requester, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Designationid_1", Designationid);
            param[1] = new SqlParameter("@Designation_2", Designation);
            param[2] = new SqlParameter("@classname_3", classname);
            param[3] = new SqlParameter("@requester_4", requester);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircDesignationMaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertSpineLabelPrint(int id, string AccessionNo, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id_1", id);
            param[1] = new SqlParameter("@AccessionNo_2", AccessionNo);
            param[2] = new SqlParameter("@userid_3", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_SpineLabelPrint_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertConsortium(int Id, string ConsortiumName, string Cons_Url, byte[] Cons_Icon, string UserId)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id_1", Id);
            param[1] = new SqlParameter("@ConsortiumName_2", ConsortiumName);
            param[2] = new SqlParameter("@Cons_Url_3", Cons_Url);
            param[3] = new SqlParameter("@Cons_Icon_4", Cons_Icon);
            param[4] = new SqlParameter("@UserId_5", UserId);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Consortium_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertcircHolidays(int holidayid, DateTime h_date, string description, string scheduled, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@holidayid_1", holidayid);
            param[1] = new SqlParameter("@h_date_2", h_date);
            param[2] = new SqlParameter("@description_3", description);
            param[3] = new SqlParameter("@scheduled_4", scheduled);
            param[4] = new SqlParameter("@userid_5", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circHolidays_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertSkills(string Member_code, int id, string skills, string flg)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Member_code", Member_code);
            param[1] = new SqlParameter("@id", id);
            param[2] = new SqlParameter("@skills", skills);
            param[3] = new SqlParameter("@flg", flg);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Insert_Skills]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertbkpServiceSetting(string Occurs, int Recurs, DateTime Occurs_at, DateTime Startdate, DateTime Enddate, DateTime bkpDate, string status, string Status1, string Week, int Month, string databaseName, string Location)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@occurs", Occurs);
            param[1] = new SqlParameter("@recurs", Recurs);
            param[2] = new SqlParameter("@occurs_at", Occurs_at);
            param[3] = new SqlParameter("@startdate", Startdate);
            param[4] = new SqlParameter("@enddate", Enddate);
            param[5] = new SqlParameter("@bkpdate", bkpDate);
            param[6] = new SqlParameter("@status", status);
            param[7] = new SqlParameter("@Status1", Status1);
            param[8] = new SqlParameter("@Week", Week);
            param[9] = new SqlParameter("@month", Month);
            param[10] = new SqlParameter("@databaseName", databaseName);
            param[11] = new SqlParameter("@Location", Location);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Ins_bkpServiceSetting]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertwaivepaid(string refid, string userid, DateTime waivedate, decimal totaloverdue, decimal paidoverdue, string reason, string userid1)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@refid_1", refid);
            param[1] = new SqlParameter("@userid_2", userid);
            param[2] = new SqlParameter("@waivedate_3", waivedate);
            param[3] = new SqlParameter("@totaloverdue_4", totaloverdue);
            param[4] = new SqlParameter("@paidoverdue_5", paidoverdue);
            param[5] = new SqlParameter("@reason_6", reason);
            param[6] = new SqlParameter("@userid1_7", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_waivepaid_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertMARC_Data(string AccessionNumber, string tag_no, string tag_indicator, string tag_subField, string tag_value)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@AccessionNumber_1", AccessionNumber);
            param[1] = new SqlParameter("@tag_no_2", tag_no);
            param[2] = new SqlParameter("@tag_indicator_3", tag_indicator);
            param[3] = new SqlParameter("@tag_subField_4", tag_subField);
            param[4] = new SqlParameter("@tag_value_5", tag_value);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_MARC_Data_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertMembershipAcheivement(string Member_code, string MA_type, string Act_Name, string File_Name, byte Att_file, string flg)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Member_code", Member_code);
            param[1] = new SqlParameter("@MA_type", MA_type);
            param[2] = new SqlParameter("@Act_Name", Act_Name);
            param[3] = new SqlParameter("@File_Name", File_Name);
            param[4] = new SqlParameter("@Att_file", Att_file);
            param[5] = new SqlParameter("@flg", flg);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_waivepaid_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertexistingbookkinfo(int srNoOld, int mediatype, string title, string authortype, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int noofcopies, decimal price, string publisherid, DateTime recordingdate, string seriesname, string form, string keywords, DateTime docDate, decimal Fprice, string FcurrencyCode, string subtitle, string part, decimal specialprice, int dept, string yearofPublication, int Language_Id, decimal exchange_rate, int no_of_pages, string page_size, string vendor_id)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[38];
            param[0] = new SqlParameter("@srNoOld_1", srNoOld);
            param[1] = new SqlParameter("@mediatype_2", mediatype);
            param[2] = new SqlParameter("@title_3", title);
            param[3] = new SqlParameter("@authortype_4", authortype);
            param[4] = new SqlParameter("@firstname1_5", firstname1);
            param[5] = new SqlParameter("@middlename1_6", middlename1);
            param[6] = new SqlParameter("@lastname1_7", lastname1);
            param[7] = new SqlParameter("@firstname2_8", firstname2);
            param[8] = new SqlParameter("@middlename2_9", middlename2);
            param[9] = new SqlParameter("@lastname2_10", lastname2);
            param[10] = new SqlParameter("@firstname3_11", firstname3);
            param[11] = new SqlParameter("@middlename3_12", middlename3);
            param[12] = new SqlParameter("@lastname3_13", lastname3);
            param[13] = new SqlParameter("@edition_14", edition);
            param[14] = new SqlParameter("@yearofedition_15", yearofedition);
            param[15] = new SqlParameter("@volumeno_16", volumeno);
            param[16] = new SqlParameter("@isbn_17", isbn);
            param[17] = new SqlParameter("@category_18", category);
            param[18] = new SqlParameter("@noofcopies_19", noofcopies);
            param[19] = new SqlParameter("@price_20", price);
            param[20] = new SqlParameter("@publisherid_21", publisherid);
            param[21] = new SqlParameter("@recordingdate_22", recordingdate);
            param[22] = new SqlParameter("@seriesname_23", seriesname);
            param[23] = new SqlParameter("@form_24", form);
            param[24] = new SqlParameter("@keywords_25", keywords);
            param[25] = new SqlParameter("@docDate_26", docDate);
            param[26] = new SqlParameter("@Fprice_27", Fprice);
            param[27] = new SqlParameter("@FcurrencyCode_28", FcurrencyCode);
            param[28] = new SqlParameter("@subtitle_29", subtitle);
            param[29] = new SqlParameter("@part_30", part);
            param[30] = new SqlParameter("@specialprice_31", specialprice);
            param[31] = new SqlParameter("@dept_32", dept);
            param[32] = new SqlParameter("@yearofPublication_33", yearofPublication);
            param[33] = new SqlParameter("@Language_Id_34", Language_Id);
            param[34] = new SqlParameter("@exchange_rate_35", exchange_rate);
            param[35] = new SqlParameter("@no_of_pages_36", no_of_pages);
            param[36] = new SqlParameter("@page_size_37", page_size);
            param[37] = new SqlParameter("@vendor_id_38", vendor_id);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_existingbookkinfo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertBookSeries(int ctrl_no, string SeriesName, string seriesNo, string seriesPart, string etal, int Svolume, string af1, string am1, string al1, string af2, string am2, string al2, string af3, string am3, string al3, string SSeriesName, string SseriesNo, string SseriesPart, string Setal, int SSvolume, string Saf1, string Sam1, string Sal1, string Saf2, string Sam2, string Sal2, string Saf3, string Sam3, string Sal3, string SeriesParallelTitle, string SSeriesParallelTitle, string SubSeriesName, string SubseriesNo, string SubseriesPart, string Subetal, int SubSvolume, string Subaf1, string Subam1, string Subal1, string Subaf2, string Subam2, string Subal2, string Subaf3, string Subam3, string Subal3, string SubSeriesParallelTitle, string ISSNMain, string ISSNSub, string ISSNSecond)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[49];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@SeriesName_2", SeriesName);
            param[2] = new SqlParameter("@seriesNo_3", seriesNo);
            param[3] = new SqlParameter("@seriesPart_4", seriesPart);
            param[4] = new SqlParameter("@etal_5", etal);
            param[5] = new SqlParameter("@Svolume_6", Svolume);
            param[6] = new SqlParameter("@af1_7", af1);
            param[7] = new SqlParameter("@am1_8", am1);
            param[8] = new SqlParameter("@al1_9", al1);
            param[9] = new SqlParameter("@af2_10", af2);
            param[10] = new SqlParameter("@am2_11", am2);
            param[11] = new SqlParameter("@al2_12", al2);
            param[12] = new SqlParameter("@af3_13", af3);
            param[13] = new SqlParameter("@am3_14", am3);
            param[14] = new SqlParameter("@al3_15", al3);
            param[15] = new SqlParameter("@SSeriesName_16", SSeriesName);
            param[16] = new SqlParameter("@SseriesNo_17", SseriesNo);
            param[17] = new SqlParameter("@SseriesPart_18", SseriesPart);
            param[18] = new SqlParameter("@Setal_19", Setal);
            param[19] = new SqlParameter("@SSvolume_20", SSvolume);
            param[20] = new SqlParameter("@Saf1_21", Saf1);
            param[21] = new SqlParameter("@Sam1_22", Sam1);
            param[22] = new SqlParameter("@Sal1_23", Sal1);
            param[23] = new SqlParameter("@Saf2_24", Saf2);
            param[24] = new SqlParameter("@Sam2_25", Sam2);
            param[25] = new SqlParameter("@Sal2_26", Sal2);
            param[26] = new SqlParameter("@Saf3_27", Saf3);
            param[27] = new SqlParameter("@Sam3_28", Sam3);
            param[28] = new SqlParameter("@Sal3_29", Sal3);
            param[29] = new SqlParameter("@SeriesParallelTitle_30", SeriesParallelTitle);
            param[30] = new SqlParameter("@SSeriesParallelTitle_31", SSeriesParallelTitle);
            param[31] = new SqlParameter("@SubSeriesName_32", SubSeriesName);
            param[32] = new SqlParameter("@SubseriesNo_33", SubseriesNo);
            param[33] = new SqlParameter("@SubseriesPart_34", SubseriesPart);
            param[34] = new SqlParameter("@Subetal_35", Subetal);
            param[35] = new SqlParameter("@SubSvolume_36", SubSvolume);
            param[36] = new SqlParameter("@Subaf1_37", Subaf1);
            param[37] = new SqlParameter("@Subam1_38", Subam1);
            param[38] = new SqlParameter("@Subal1_39", Subal1);
            param[39] = new SqlParameter("@Subaf2_40", Subaf2);
            param[40] = new SqlParameter("@Subam2_41", Subam2);
            param[41] = new SqlParameter("@Subal2_42", Subal2);
            param[42] = new SqlParameter("@Subaf3_43", Subaf3);
            param[43] = new SqlParameter("@Subam3_44", Subam3);
            param[44] = new SqlParameter("@Subal3_45", Subal3);
            param[45] = new SqlParameter("@SubSeriesParallelTitle_46", SubSeriesParallelTitle);
            param[46] = new SqlParameter("@ISSNMain_47", ISSNMain);
            param[47] = new SqlParameter("@ISSNSub_48", ISSNSub);
            param[48] = new SqlParameter("@ISSNSecond_49", ISSNSecond);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookSeries_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        //end

        public DataTable GetGiftIndent(string GiftIndentId, string giftindentnumber, string title, string Author, string giftindentnumber2, bool bool_order_check_code0, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@GiftIndentId", GiftIndentId);
            param[1] = new SqlParameter("@giftindentnumber", giftindentnumber);
            param[2] = new SqlParameter("@title", title);
            param[3] = new SqlParameter("@Author", Author);
            param[4] = new SqlParameter("@giftindentnumber2", giftindentnumber2);
            param[4] = new SqlParameter("@bool_order_check_code0", bool_order_check_code0);
            param[5] = new SqlParameter("@UserId", UserId);
            param[6] = new SqlParameter("@FormId", FormId);
            param[7] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndent](@GiftIndentId, @giftindentnumber,@title,@Author,@giftindentnumber2,@bool_order_check_code0,@UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetGiftIndentPublisher(string GiftIndentId, string giftindentnumber, string Firstname, string giftindentnumberItemNo, bool bool_order_check_code0, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@GiftIndentId", GiftIndentId);
            param[1] = new SqlParameter("@giftindentnumber", giftindentnumber);
            param[2] = new SqlParameter("@Firstname", Firstname);
            param[3] = new SqlParameter("@giftindentnumberItemNo", giftindentnumberItemNo);
            param[4] = new SqlParameter("@bool_order_check_code0", bool_order_check_code0);
            param[5] = new SqlParameter("@UserId", UserId);
            param[6] = new SqlParameter("@FormId", FormId);
            param[7] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentPublisher](@GiftIndentId, @giftindentnumber,@Firstname,@giftindentnumberItemNo,@bool_order_check_code0,@UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetGiftIndentVendor(string GiftIndentId, string giftindentnumber, string Vendorname, bool bool_order_check_code0, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@GiftIndentId", GiftIndentId);
            param[1] = new SqlParameter("@giftindentnumber", giftindentnumber);
            param[2] = new SqlParameter("@Vendorname", Vendorname);
            param[3] = new SqlParameter("@bool_order_check_code0", bool_order_check_code0);
            param[4] = new SqlParameter("@UserId", UserId);
            param[5] = new SqlParameter("@FormId", FormId);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentVendor](@GiftIndentId, @giftindentnumber,@Vendorname,@bool_order_check_code0,@UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetGiftIndentDetail(string GiftIndentId, int deptcode, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@GiftIndentId", GiftIndentId);
            param[1] = new SqlParameter("@departmentcode", deptcode);
            param[2] = new SqlParameter("@UserId", UserId);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentDetail](@GiftIndentId,@departmentcode, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetGiftindentID()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftindentID]( )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetGiftindentItemNo()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftindentItemNo]( )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetGiftIndentViewR(string giftindentnumber, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@giftindentnumber", giftindentnumber);
            param[1] = new SqlParameter("@UserId", UserId);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentViewR](@giftindentnumber, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentApprRef(string indentnumber, string indentId, int departmentcode, string titleExact, string title, string Approval, bool EmptyRef, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@indentnumber", indentnumber);
            param[1] = new SqlParameter("@indentId", indentId);
            param[2] = new SqlParameter("@departmentcode", departmentcode);
            param[3] = new SqlParameter("@titleExact", titleExact);
            param[4] = new SqlParameter("@title", title);
            param[5] = new SqlParameter("@Approval", Approval);
            param[6] = new SqlParameter("@EmptyRef", EmptyRef);

            param[7] = new SqlParameter("@UserID", UserID);
            param[8] = new SqlParameter("@FormId", FormId);
            param[9] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentApprRef](@indentnumber,@indentId,@departmentcode,@titleExact,@title,@Approval,@EmptyRef, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetUserClass(string UserId2, bool CanRequest, int departmentcode, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@UserId2", UserId2);
            param[1] = new SqlParameter("@CanRequest", CanRequest);
            param[2] = new SqlParameter("@departmentcode", departmentcode);
            //
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetUserClass](@UserId2,@CanRequest,@departmentcode, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentMaxId()
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[0];
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentMaxId]( )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        //check OR condition in function
        public DataTable GetIndentDeptApprIsstand(string Approval, string Isstanding, string order_check_code, int departmentcode, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Approval", Approval);
            param[1] = new SqlParameter("@Isstanding", Isstanding);
            param[2] = new SqlParameter("@order_check_code", order_check_code);
            param[3] = new SqlParameter("@departmentcode", departmentcode);
            param[4] = new SqlParameter("@UserID", UserID);
            param[5] = new SqlParameter("@FormId", FormId);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDeptApprIsstand](@Approval,@Isstanding,@order_check_code,@departmentcode, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentDeptDate(int departmentcode, DateTime IndentDateFrom, DateTime IndentDateTo, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@departmentcode", departmentcode);
            param[1] = new SqlParameter("@IndentDateFrom", IndentDateFrom);
            param[2] = new SqlParameter("@IndentDateTo", IndentDateTo);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDeptDate](@departmentcode,@IndentDateFrom,@IndentDateTo, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetGiftIndentDeptDate(int departmentcode, string order_check_code, DateTime IndentDateFrom, DateTime IndentDateTo, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@departmentcode", departmentcode);
            param[1] = new SqlParameter("@order_check_code", order_check_code);
            param[2] = new SqlParameter("@IndentDateFrom", IndentDateFrom);
            param[3] = new SqlParameter("@IndentDateTo", IndentDateTo);
            param[4] = new SqlParameter("@UserID", UserID);
            param[5] = new SqlParameter("@FormId", FormId);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentDeptDate](@departmentcode,@order_check_code, @IndentDateFrom,@IndentDateTo, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetIndentAppr(string approval, string order_check_code, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@approval", approval);
            param[1] = new SqlParameter("@order_check_code", order_check_code);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentAppr](@approval,@order_check_code, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentDeptDateOrd(DateTime IndentDateFrom, DateTime IndentDateTo, string order_check_code, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@IndentDateFrom", IndentDateFrom);
            param[1] = new SqlParameter("@IndentDateTo", IndentDateTo);
            param[2] = new SqlParameter("@order_check_code", order_check_code);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormId", FormId);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDeptDateOrd](@IndentDateFrom,@IndentDateTo,@order_check_code, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetDeptInstIndent(string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetDeptInstIndent]( @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentDocno(string Docno, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Docno", Docno);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentDocno](@Docno, @UserID,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable UpdateIndentDocno(string indentid, string Docno, DateTime DocDate, string PrintStatus, string UserID, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@indentid", indentid);
            param[1] = new SqlParameter("@Docno", Docno);
            param[2] = new SqlParameter("@DocDate", DocDate);
            param[3] = new SqlParameter("@PrintStatus", PrintStatus);
            param[4] = new SqlParameter("@UserID", UserID);
            param[5] = new SqlParameter("@FormId", FormId);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[UpdateIndentDocno]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateIdTable(string objectname, int currentposition, string UserID, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@objectname", objectname);
            param[1] = new SqlParameter("@currentposition", currentposition);
            param[2] = new SqlParameter("@UserID", UserID);
            param[3] = new SqlParameter("@FormId", FormId);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[UpdateIdTable]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        //
        public DataTable GetGiftIndentViewRArr(DataTable giftindentnumbers, string UserId, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@giftindentnumbers", giftindentnumbers);
            param[0].TypeName = "dbo.arrstring";
            param[1] = new SqlParameter("@UserId", UserId);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetGiftIndentViewRArr](@giftindentnumbers, @UserId,@FormId,@Type )", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetIndentVarious2(string Title, DateTime IndentDateFrom, DateTime IndentDateTo, int departmentcode, string requestercode, string Indentnumber, string UserID, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@Title", Title);
            SqlParameter p2 = new SqlParameter("@IndentDateFrom", IndentDateFrom);
            SqlParameter p3 = new SqlParameter("@IndentDateTo", IndentDateTo);
            SqlParameter p4 = new SqlParameter("@departmentcode", departmentcode);
            SqlParameter p5 = new SqlParameter("@requestercode", requestercode);
            SqlParameter p6 = new SqlParameter("@Indentnumber", Indentnumber);

            SqlParameter p7 = new SqlParameter("@UserID", UserID);
            SqlParameter p8 = new SqlParameter("@FormId", FormId);
            SqlParameter p9 = new SqlParameter("@Type", Type);
            paras.Add(p1);
            paras.Add(p2);
            paras.Add(p3);
            paras.Add(p4);
            paras.Add(p5);
            paras.Add(p6);
            paras.Add(p7);
            paras.Add(p8);
            paras.Add(p9);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[GetIndentVarious2]", ref dsd, paras.ToArray(), EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GetOrder(bool NormalAdv, bool NormalOrder, bool AdvDept, bool OptAdvance, string Departmentname, string Vendorname, string Orderno, string CancelOrder, string Approval, string UserID, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@NormalAdv", NormalAdv);
            SqlParameter p2 = new SqlParameter("@NormalOrder", NormalOrder);
            SqlParameter p3 = new SqlParameter("@AdvDept", AdvDept);
            SqlParameter p4 = new SqlParameter("@OptAdvance", OptAdvance);
            SqlParameter p5 = new SqlParameter("@Departmentname", Departmentname);
            SqlParameter p6 = new SqlParameter("@Vendorname", Vendorname);
            SqlParameter p7 = new SqlParameter("@Orderno", Orderno);
            SqlParameter p8 = new SqlParameter("@CancelOrder", CancelOrder);
            SqlParameter p9 = new SqlParameter("@Approval", Approval);

            SqlParameter p10 = new SqlParameter("@UserID", UserID);
            SqlParameter p11 = new SqlParameter("@FormId", FormId);
            SqlParameter p12 = new SqlParameter("@Type", Type);
            paras.Add(p1);
            paras.Add(p2);
            paras.Add(p3);
            paras.Add(p4);
            paras.Add(p5);
            paras.Add(p6);
            paras.Add(p7);
            paras.Add(p8);
            paras.Add(p9);
            paras.Add(p10);
            paras.Add(p11);
            paras.Add(p12);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[GetOrder]", ref dsd, paras.ToArray(), EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable GetIndentVendorApprIsstand(bool OptNormal, string Approval, string Isstanding, string order_check_code, int vendorid, string UserID, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            DataTable dt = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@OptNormal", OptNormal);
            SqlParameter p2 = new SqlParameter("@Approval", Approval);
            SqlParameter p3 = new SqlParameter("@Isstanding", Isstanding);
            SqlParameter p4 = new SqlParameter("@order_check_code", order_check_code);
            SqlParameter p5 = new SqlParameter("@vendorid", vendorid);
            SqlParameter p6 = new SqlParameter("@UserID", UserID);
            SqlParameter p7 = new SqlParameter("@FormId", FormId);
            SqlParameter p8 = new SqlParameter("@Type", Type);
            paras.Add(p1);
            paras.Add(p2);
            paras.Add(p3);
            paras.Add(p4);
            paras.Add(p5);
            paras.Add(p6);
            paras.Add(p7);
            paras.Add(p8);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();

            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetIndentVendorApprIsstand](@OptNormal,@Approval,@Isstanding,@order_check_code,@vendorid, @UserId,@FormId,@Type )", ref dt, paras.ToArray(), EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetBudgetMaster(int departmentcode, string session, string UserID, string FormId, string Type)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@departmentcode", departmentcode);
            SqlParameter p2 = new SqlParameter("@session", session);
            SqlParameter p6 = new SqlParameter("@UserID", UserID);
            SqlParameter p7 = new SqlParameter("@FormId", FormId);
            SqlParameter p8 = new SqlParameter("@Type", Type);
            paras.Add(p1);
            paras.Add(p2);
            paras.Add(p6);
            paras.Add(p7);
            paras.Add(p8);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetBudgetMaster](@departmentcode,@session, @UserId,@FormId,@Type )", ref dt, paras.ToArray(), EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;

        }

        public DataTable UpdateIndentExchageRate(string Indentid, decimal ApplicableExchangeRate, decimal orderexchangerate, string UserID, string FormId, string Type)
        {
            DataSet dsd = new DataSet();
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@Indentid", Indentid);
            SqlParameter p2 = new SqlParameter("@ApplicableExchangeRate", ApplicableExchangeRate);
            SqlParameter p3 = new SqlParameter("@orderexchangerate", orderexchangerate);
            SqlParameter p10 = new SqlParameter("@UserID", UserID);
            SqlParameter p11 = new SqlParameter("@FormId", FormId);
            SqlParameter p12 = new SqlParameter("@Type", Type);
            paras.Add(p1);
            paras.Add(p2);
            paras.Add(p3);
            paras.Add(p10);
            paras.Add(p11);
            paras.Add(p12);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[mtr].[UpdateIndentExchageRate]", ref dsd, paras.ToArray(), EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable UpdateInsertClassMaster(int id, string Class, string shortname, string HowLongPreserve, string PermissionRequired, string TobeReturned, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Id", id);
            param[1] = new SqlParameter("@class", Class);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@HowLongPreserve", HowLongPreserve);
            param[4] = new SqlParameter("@PermissionRequired", PermissionRequired);
            param[5] = new SqlParameter("@TobeReturned", TobeReturned);
            param[6] = new SqlParameter("@FormID", FormID);
            param[7] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateClassMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }





        public DataTable GetCatalogueCardView(string classnumber, string booknumber, string volume, string part, string edition, string Author, int language_id, string booktitle, int dept)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@classnumber", classnumber);
            SqlParameter p2 = new SqlParameter("@booknumber", booknumber);
            SqlParameter p6 = new SqlParameter("@volume", volume);
            SqlParameter p7 = new SqlParameter("@part", part);
            SqlParameter p8 = new SqlParameter("@edition", edition);
            SqlParameter p9 = new SqlParameter("@Author", Author);
            SqlParameter p10 = new SqlParameter("@language_id", language_id);
            SqlParameter p11 = new SqlParameter("@title", booktitle);
            SqlParameter p12 = new SqlParameter("@dept", dept);
            paras.Add(p1);
            paras.Add(p2);
            paras.Add(p6);
            paras.Add(p7);
            paras.Add(p8);
            paras.Add(p9);
            paras.Add(p10);
            paras.Add(p11);
            paras.Add(p12);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCatalogueCardView](@classnumber,@booknumber, @volume,@part,@edition,@Author,@language_id,@title,@dept )", ref dt, paras.ToArray(), EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable UpdateInsertInvoice(int invoice_id, int Binder_id, string invoice_no, DateTime invoice_amt, DateTime invoice_date, string Bill_serial_no, string userid, string type, string CancelStatus, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@invoice_id_1", invoice_id);
            param[1] = new SqlParameter("@Binder_id_2", Binder_id);
            param[2] = new SqlParameter("@invoice_no_3", invoice_no);
            param[3] = new SqlParameter("@invoice_amt_4", invoice_amt);
            param[4] = new SqlParameter("@invoice_date_5", invoice_date);
            param[5] = new SqlParameter("@Bill_serial_no_6", Bill_serial_no);
            param[6] = new SqlParameter("@userid_7", userid);
            param[7] = new SqlParameter("@type_8", type);
            param[8] = new SqlParameter("@CancelStatus_9", CancelStatus);
            param[9] = new SqlParameter("@FormID", FormID);
            param[10] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_binder_invoice_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        public DataTable UpdateInsertDesignation(int id, string Designation, string shortname, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id", id);
            param[1] = new SqlParameter("@Designation", Designation);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@FormID", FormID);
            param[4] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_AddUpdateDesigMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        ///Harsh 20Feb2023
        public DataTable updateinsertstatusmaster(int id, string status, string avv, int UserID, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@status", status);
            param[2] = new SqlParameter("@avv", avv);
            param[3] = new SqlParameter("UserID", UserID);
            param[4] = new SqlParameter("FormID", FormID);
            param[5] = new SqlParameter("@Type", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_StatusMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertFileSizeMaster(int id, string SizeName, string shortname, int UserID, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@SizeName", SizeName);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@UserID", UserID);
            param[4] = new SqlParameter("@FormID", FormID);
            param[5] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_FileSizeMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertsectionmaster(int id, string Section, string shortname, string department, string UserID, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@Section", Section);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@department", department);
            param[4] = new SqlParameter("@UserID", UserID);
            param[5] = new SqlParameter("@FormID", FormID);
            param[6] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_sectionmaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable upsertFileSettingMaster(int id, string parameter, string avv, DateTime wef, string status, int UserID, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@parameter", parameter);
            param[2] = new SqlParameter("@avv", avv);

            param[3] = new SqlParameter("@wef", wef);
            param[4] = new SqlParameter("@status", status);

            param[5] = new SqlParameter("@UserID", UserID);
            param[6] = new SqlParameter("@FormID", FormID);
            param[7] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_FileSettingMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateInsertRegister(int id, string Register, string shortname, int section, int department, int designation, string MaintainedBy, int UserID, int FormID, int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Id", id);
            param[1] = new SqlParameter("@Register", Register);
            param[2] = new SqlParameter("@shortname", shortname);
            param[3] = new SqlParameter("@section", section);
            param[4] = new SqlParameter("@department", department);
            param[5] = new SqlParameter("@designation", designation);
            param[6] = new SqlParameter("@MaintainedBy", MaintainedBy);
            param[7] = new SqlParameter("@UserID", UserID);
            param[8] = new SqlParameter("@FormID", FormID);
            param[9] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[Sp_RegisterMaster]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertdocumentnumberstructure(string objectname, string prefix, string suffix, int currentposition, string description, string formname)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@objectname", objectname);
            param[1] = new SqlParameter("@prefix", prefix);
            param[2] = new SqlParameter("@suffix", suffix);
            param[3] = new SqlParameter("@currentposition", currentposition);
            param[4] = new SqlParameter("@description", description);
            param[5] = new SqlParameter("@formname", formname);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[DocumentNumberstructure]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertjournalaccessioning(string accession_no, string part_no, string journal_no, string volume, string fromIssue, string toIssue, string Lack_no, string accession_id, DateTime date_of_accessioning, string issue_status, string curr_year, string From_date, string To_date, string userid, int Issue_No, int Copy_No)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[16];

            param[0] = new SqlParameter("@accession_no_1", accession_no);
            param[1] = new SqlParameter("@part_no_2", part_no);
            param[2] = new SqlParameter("@journal_no_3", journal_no);
            param[3] = new SqlParameter("@volume_4", volume);
            param[4] = new SqlParameter("@fromIssue_5", fromIssue);
            param[5] = new SqlParameter("@toIssue_6", toIssue);
            param[6] = new SqlParameter("@Lack_no_7", Lack_no);
            param[7] = new SqlParameter("@accession_id_8", accession_id);
            param[8] = new SqlParameter("@date_of_accessioning_9", date_of_accessioning);
            param[9] = new SqlParameter("@issue_status_10", issue_status);
            param[10] = new SqlParameter("@curr_year_11", curr_year);
            param[11] = new SqlParameter("@From_date_12", From_date);
            param[12] = new SqlParameter("@To_date_13", To_date);
            param[13] = new SqlParameter("@userid_14", userid);
            param[14] = new SqlParameter("@Issue_No", Issue_No);
            param[15] = new SqlParameter("@Copy_No", Copy_No);


            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_accessioning_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];

        }
        //Akansha
        public DataTable UpdateinsertILLReceive(int s_no, DateTime receive_date, int ILLid, string accession_no, DateTime return_date, string isbn, string title, decimal book_price, decimal fine, string author, string edition, string description, string status, string volume, int return_status, string DocumentNo, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@s_no_1", s_no);
            param[1] = new SqlParameter("@receive_date_2", receive_date);
            param[2] = new SqlParameter("@ILLid_3", ILLid);
            param[3] = new SqlParameter("@accession_no_4", accession_no);
            param[4] = new SqlParameter("@return_date_5", return_date);
            param[5] = new SqlParameter("@isbn_6", isbn);
            param[6] = new SqlParameter("@title_7", title);
            param[7] = new SqlParameter("@book_price_8", book_price);
            param[8] = new SqlParameter("@fine_9", fine);
            param[9] = new SqlParameter("@author_10", author);
            param[10] = new SqlParameter("@edition_11", edition);
            param[11] = new SqlParameter("@description_12", description);
            param[12] = new SqlParameter("@status_13", status);
            param[13] = new SqlParameter("@volume_14", volume);
            param[14] = new SqlParameter("@return_status_15", return_status);
            param[15] = new SqlParameter("@DocumentNo_16", DocumentNo);
            param[16] = new SqlParameter("@userid_17", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_ILLReceive_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateinsertjournalCatalogue(int JOurnal_NO, string Journal_title, string Title_abbreviation, string series_title, string spine_title, string frequency, DateTime PublicationDate, string Publisher, string agent, string starting_Volume, string ending_volume, string starting_issue, string ending_issue, string starting_part, string ending_part, string Copy_No, string ctrl_No, string class_No, string Accession_no, string currency, decimal cuur_value, decimal price, decimal special_price, DateTime Catalog_date, string Lack_no, string userid, string Book_no, int loc_id, string flg)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@JOurnal_NO", JOurnal_NO);
            param[1] = new SqlParameter("@Journal_title", Journal_title);
            param[2] = new SqlParameter("@Title_abbreviation", Title_abbreviation);
            param[3] = new SqlParameter("@series_title", series_title);
            param[4] = new SqlParameter("@spine_title", spine_title);
            param[5] = new SqlParameter("@frequency", frequency);
            param[6] = new SqlParameter("@PublicationDate", PublicationDate);
            param[7] = new SqlParameter("@Publisher", Publisher);
            param[8] = new SqlParameter("@agent", agent);
            param[9] = new SqlParameter("@starting_Volume", starting_Volume);
            param[10] = new SqlParameter("@ending_volume", ending_volume);
            param[11] = new SqlParameter("@starting_issue", starting_issue);
            param[12] = new SqlParameter("@ending_issue", ending_issue);
            param[13] = new SqlParameter("@starting_part", starting_part);
            param[14] = new SqlParameter("@ending_part", ending_part);
            param[15] = new SqlParameter("@Copy_No", Copy_No);
            param[16] = new SqlParameter("@ctrl_No", ctrl_No);
            param[17] = new SqlParameter("@class_No", class_No);
            param[18] = new SqlParameter("@Accession_no", Accession_no);
            param[19] = new SqlParameter("@currency", currency);
            param[20] = new SqlParameter("@cuur_value", cuur_value);
            param[21] = new SqlParameter("@price", price);
            param[22] = new SqlParameter("@special_price", special_price);
            param[23] = new SqlParameter("@Catalog_date", Catalog_date);
            param[24] = new SqlParameter("@Lack_no", Lack_no);
            param[25] = new SqlParameter("@userid", userid);
            param[26] = new SqlParameter("@Book_no", Book_no);
            param[27] = new SqlParameter("@loc_id", loc_id);
            param[28] = new SqlParameter("@flg", flg);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_Catalogue]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertnewvol(string journal_no, int tot_vol, int issue_per_vol, int part_per_iss, int start_vol, int end_vol, int start_iss, int end_iss, int start_part, int end_part, string accno, int loc_id, DateTime Pub_dt, string Freq, string ExAccno, int Ctrl_no, string result)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@tot_vol_2", tot_vol);
            param[2] = new SqlParameter("@issue_per_vol_3", issue_per_vol);
            param[3] = new SqlParameter("@part_per_iss_4", part_per_iss);
            param[4] = new SqlParameter("@start_vol_5", start_vol);
            param[5] = new SqlParameter("@end_vol_6", end_vol);
            param[6] = new SqlParameter("@start_iss_7", start_iss);
            param[7] = new SqlParameter("@end_iss_8", end_iss);
            param[8] = new SqlParameter("@start_part_9", start_part);
            param[9] = new SqlParameter("@end_part_10", end_part);
            param[10] = new SqlParameter("@accno_11", accno);
            param[11] = new SqlParameter("@loc_id_12", loc_id);
            param[12] = new SqlParameter("@Pub_dt_13", Pub_dt);
            param[13] = new SqlParameter("@Freq_14", Freq);
            param[14] = new SqlParameter("@ExAccno_15", ExAccno);
            param[15] = new SqlParameter("@Ctrl_no_16", Ctrl_no);
            param[16] = new SqlParameter("@result_17", result);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_New_Vol]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];



        }

        public DataTable updateinsertTranData(string AccNos, string TranType, string TranIds, string TranDescS, string Res)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@AccNos", AccNos);
            param[1] = new SqlParameter("@TranType", TranType);
            param[2] = new SqlParameter("@TranIds", TranIds);
            param[3] = new SqlParameter("@TranDescS", TranDescS);
            param[4] = new SqlParameter("@Res", Res);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[TranData]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertJOverDue(string MemId, DateTime Entdt, string JTranId, string JCId, string PayAmtS, string WavAmtS, string userid, string RetStatus, string RetRem)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@MemId", MemId);
            param[1] = new SqlParameter("@EntDt", Entdt);
            param[2] = new SqlParameter("@JTranId", JTranId);
            param[3] = new SqlParameter("@JCId", JCId);
            param[4] = new SqlParameter("@PayAmtS", PayAmtS);
            param[5] = new SqlParameter("@WavAmtS", WavAmtS);
            param[6] = new SqlParameter("@userid", userid);
            param[7] = new SqlParameter("@RetStatus", RetStatus);
            param[8] = new SqlParameter("@RetRem", RetRem);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_JOverDue]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }



        //end

        public DataTable insertnonarrivaljourlist(string Letter_No, DateTime Letter_date, DateTime Reply_date, string reply_id, string remark, string status, string Doc_No, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Letter_No", Letter_No);
            param[1] = new SqlParameter("@Letter_date", Letter_date);
            param[2] = new SqlParameter("@Reply_date", Reply_date);
            param[3] = new SqlParameter("@reply_id", reply_id);
            param[4] = new SqlParameter("@remark", remark);
            param[5] = new SqlParameter("@status", status);
            param[6] = new SqlParameter("@Doc_No", Doc_No);
            param[7] = new SqlParameter("@userid", userid);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Nonarrive_Jour_Master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];



        }

        public DataTable DeleteMassCatalog(string accessionnumber, string AppName, string UserId, string IpAddress)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@accessionnumber", accessionnumber);
            param[1] = new SqlParameter("@AppName", AppName);
            param[2] = new SqlParameter("@UId", UserId);
            param[3] = new SqlParameter("@IpAddress", IpAddress);
          
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[sp_DeleteCatalog]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable UpdateinsertDirectCatalogExpress(string accessionno, string accessionnocp, string cpbook, string form, int accid, DateTime accdate,string booktitle, decimal bookprice, string status, string issuestatus, string checkstatus, int edityear,int copynumber,int pubyear,  string billno, DateTime billdate, string itemtype, string original_currency,
                                                           string userid, string vendorsource, int deptcode, int itemcatcode, string bookstitle, string volno, int part, string initpages, string parts,int publishcode,string edition, string isbn, string subject1, string subject2, string subject3, string bibilopages, string issn, string volume, 
                                                           int language, string Firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string classno, string bookno, int pages, string media_type, string searchtext, string volumenoV,
                                                           string InsUp, string firstname, string pubcity, int location, string result, int octrl, string audi, string session, string ipa, string title, string edf1,string edm1,string edL1,string edf2, string edm2, string edL2, string edf3, string edm3, string edL3, string compf1, string compm1, string compL1,
                                        
                                                          string compf2, string compm2, string compL2, string compf3, string compm3, string compL3, string ilf1, string ilm1, string ilL1, string ilf2, string ilm2,string ilL2, string ilf3, string ilm3, string ilL3, string tranf1, string tranm1, string tranL1 ,string tranf2,string tranm2, string tranL2,
                                                           string tranf3, string tranm3, string tranL3, int setofbooks, string appname, int transNo) 
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[101];
                    param[0] = new SqlParameter("@accessionnumber_1", accessionno);
                    param[1] = new SqlParameter("@accessionnumber_1_Cp", accessionnocp);
                   param[2] = new SqlParameter("@cp_booknumber", cpbook);
                  param[3] = new SqlParameter("@form_4", form);
                  param[4] = new SqlParameter("@accessionid_5", accid);
                   param[5] = new SqlParameter("@accessionid_6", accdate);
                   param[6] = new SqlParameter("@booktitle_7", booktitle);
                   param[7] = new SqlParameter("@@bookprice_8", bookprice);
                    param[8] = new SqlParameter("@status_9", status);
                    param[9] = new SqlParameter("@issuestatus_10", issuestatus);
                    param[10] = new SqlParameter("@checkstatus_11", checkstatus);
                    param[11] = new SqlParameter("@editionyear_12", edityear);
                    param[12] = new SqlParameter("@copynumber_13", copynumber);
                    param[13] = new SqlParameter("@pubyear_14", pubyear);
                    param[14] = new SqlParameter("@biilNo_15", billno);
                    param[15] = new SqlParameter("@billDate_16", billdate);
                    param[16] = new SqlParameter("@Item_type_17", itemtype);
                    param[17] = new SqlParameter("@OriginalCurrency_18", original_currency);
                    param[18] = new SqlParameter("@userid_19", userid);
                    param[19] = new SqlParameter("@vendor_source_20", vendorsource);
                   param[20] = new SqlParameter("@DeptCode_21", deptcode);
                    param[21] = new SqlParameter("@ItemCategoryCode_22", itemcatcode);
                   param[22] = new SqlParameter("@bookStitle_23", bookstitle);
                   param[23] = new SqlParameter("@volumenumber_24", volno);
                   param[24] = new SqlParameter("@Part_24_2", part);
                    param[25] = new SqlParameter("@initpages_25", initpages);
                    param[26] = new SqlParameter("@parts_26", parts);
                    param[27] = new SqlParameter("@publishercode_27", publishcode);
                   param[28] = new SqlParameter("@edition_28", edition);
                    param[29] = new SqlParameter("@isbn_29", isbn);
                    param[30] = new SqlParameter("@subject1_30", subject1);
                   param[31] = new SqlParameter("@subject2_31", subject2);
                   param[32] = new SqlParameter("@subject3_32", subject3);
                   param[33] = new SqlParameter("@bibliopages_33", bibilopages);
                    param[34] = new SqlParameter("@issn_34", issn);
                    param[35] = new SqlParameter("@volume_35", volume);
                    param[36] = new SqlParameter("@language_36", language);
                    param[37] = new SqlParameter("@firstname1_37", Firstname1);
                    param[38] = new SqlParameter("@middlename1_38", middlename1);
                    param[39] = new SqlParameter("@lastname1_39", lastname1);
                    param[40] = new SqlParameter("@firstname2_40", firstname2);
                    param[41] = new SqlParameter("@middlename2_41", middlename2);
                    param[42] = new SqlParameter("@lastname2_42", lastname2);
                    param[43] = new SqlParameter("@firstname3_43", firstname3);
                    param[44] = new SqlParameter("@middlename3_44", middlename3);
                    param[45] = new SqlParameter("@lastname3_45", lastname3);
                    param[46] = new SqlParameter("@classno_46", classno);
                    param[47] = new SqlParameter("@bookno_47", bookno);
                    param[48] = new SqlParameter("@pages_48", pages);
                   param[49] = new SqlParameter("@media_type_49", media_type);
                    param[50] = new SqlParameter("@searchtext_49_1", searchtext);
                    param[51] = new SqlParameter("@volumenumberV_50", volumenoV);
                    param[52] = new SqlParameter("@InsUpd48", InsUp);
                    param[53] = new SqlParameter("@firstname_49", firstname);
                    param[54] = new SqlParameter("@PubCity_50", pubcity);
                    param[55] = new SqlParameter("@Location_51", location);
                    param[56] = new SqlParameter("@Result55", result);
                    param[57] = new SqlParameter("@OCtrl_no", octrl);
                    param[58] = new SqlParameter("@Audi_101", audi);
                    param[59] = new SqlParameter("@Sess_102", session);
                    param[60] = new SqlParameter("@IPA_103", ipa);
                    param[61] = new SqlParameter("@Title_104", title);
                    param[62] = new SqlParameter("@EdF1", edf1);
                    param[63] = new SqlParameter("@EdM1", edm1);
                    param[64] = new SqlParameter("@EdL1", edL1);
                    param[65] = new SqlParameter("@EdF2", edf2);
                    param[66] = new SqlParameter("@EdM2", edm2);
                    param[67] = new SqlParameter("@EdL2", edL2);
                    param[68] = new SqlParameter("@EdF3", edf3);
                    param[69] = new SqlParameter("@EdM3", edm3);
                    param[70] = new SqlParameter("@EdL3", edL3);
                    param[71] = new SqlParameter("@CompF1", compf1);
                    param[72] = new SqlParameter("@CompM1", compm1);
                    param[73] = new SqlParameter("@CompL1", compL1);
                    param[74] = new SqlParameter("@CompF2", compf2);
                    param[75] = new SqlParameter("@CompM2", compm2);
                    param[76] = new SqlParameter("@CompL2", compL2);
                    param[77] = new SqlParameter("@CompF3", compf3);
                    param[78] = new SqlParameter("@CompM3", compm3);
                    param[79] = new SqlParameter("@CompL3", compL3);
                    param[80] = new SqlParameter("@IlF1", ilf1);
                    param[81] = new SqlParameter("@IlM1", ilm1);
                    param[82] = new SqlParameter("@IlL1", ilL1);
                    param[83] = new SqlParameter("@IlF2", ilf2);
                    param[84] = new SqlParameter("@IlM2", ilm2);
                    param[85] = new SqlParameter("@IlL2", ilL2);
                    param[86] = new SqlParameter("@IlF3", ilf3);
                    param[87] = new SqlParameter("@IlM3", ilm3);
                    param[88] = new SqlParameter("@IlL3", ilL3);
                    param[89] = new SqlParameter("@TranF1", tranf1);
                    param[90] = new SqlParameter("@TranM1", tranm1);
                    param[91] = new SqlParameter("@TranL1", tranL1);
                    param[92] = new SqlParameter("@TranF2", tranf2);
                    param[93] = new SqlParameter("@TranM2", tranm2);
                    param[94] = new SqlParameter("@TranL2", tranL2);
                    param[95] = new SqlParameter("@TranF3", tranf3);
                    param[96] = new SqlParameter("@TranM3", tranm3);
                    param[97] = new SqlParameter("@TranL3", tranL3);
                    param[98] = new SqlParameter("@SetOFbooks", setofbooks);
                    param[99] = new SqlParameter("@AppName", appname);
                    param[100] = new SqlParameter("@TransNo", transNo);
                   

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_DirectCatalog_Express]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertaccessionexpress(string AccessionNo, string Accessioncp, int copyno, int ctrlno, decimal bookprice,string booknocp,string  item_type, DateTime AccnCpDate,string billno, DateTime billdate, int deptid, int itemcategory, string vendor, int locid, string userid, string ipaddress, string appname, int transno)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@AccessionNo", AccessionNo);
            param[1] = new SqlParameter("@AccessionNoCp", Accessioncp);
            param[2] = new SqlParameter("@Copynnumber", copyno);
            param[3] = new SqlParameter("@Ctrl_no", ctrlno);
            param[4] = new SqlParameter("@BookPrice", bookprice);
            param[5] = new SqlParameter("@BookNumberCP", booknocp);
            param[6] = new SqlParameter("@Item_type", item_type);
         
            param[7] = new SqlParameter("@AccnCpDate", AccnCpDate);
            param[8] = new SqlParameter("@BiilNo", billno);
            param[9] = new SqlParameter("@BillDate", billdate);
            param[10] = new SqlParameter("@DeptId", deptid);
            param[11] = new SqlParameter("@ItemCategory", itemcategory);
            param[12] = new SqlParameter("@Vendor", vendor);
            param[13] = new SqlParameter("@LocId", locid);
            param[14] = new SqlParameter("@userid", userid);
            param[15] = new SqlParameter("@IpAddress", ipaddress);
            param[16] = new SqlParameter("@AppName", appname);
            param[17] = new SqlParameter("@TransNo", transno);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Accession_ExpressCP]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable UpdateBudgetApprove(int departmentcode, string session, decimal indent, int userid, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@departmentcode", departmentcode);
            param[1] = new SqlParameter("@Curr_Session", session);
            param[2] = new SqlParameter("@IndentValue", indent);
            param[3] = new SqlParameter("@UserId", userid);
            param[4] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddApprCommit]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable insertcataloguedBEM(int ctrl_no, string classnumber, string booknumber, int trans, int FormID)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ctrl_no", ctrl_no);
            param[1] = new SqlParameter("@classnumber", classnumber);
            param[2] = new SqlParameter("@booknumber", booknumber);
            param[3] = new SqlParameter("@TransNo",trans);
            param[4] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CatalogData_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertcataloguedBEM1(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string UniFormTitle, int FormID)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@ctrl_no", ctrl_no);
            param[1] = new SqlParameter("@firstname1", firstname1);
            param[2] = new SqlParameter("@middlename1", middlename1);
            param[3] = new SqlParameter("@lastname1", lastname1);
            param[4] = new SqlParameter("@firstname2", firstname2);
            param[5] = new SqlParameter("@middlename2", middlename2);
            param[6] = new SqlParameter("@lastname2", lastname2);
            param[7] = new SqlParameter("@firstname3", firstname3);
            param[8] = new SqlParameter("@middlename3", middlename3);
            param[9] = new SqlParameter("@lastname3", lastname3);
            param[10] = new SqlParameter("@UniFormTitle", UniFormTitle);
            param[11] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        //public DataTable insertcataloguedBookAuthor(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3)
        //{

        //    DataSet dsd = new DataSet();
        //    SqlParameter[] param = new SqlParameter[10];
        //    param[0] = new SqlParameter("@ctrl_no", ctrl_no);
        //    param[1] = new SqlParameter("@firstname1", firstname1);
        //    param[2] = new SqlParameter("@middlename1", middlename1);
        //    param[3] = new SqlParameter("@lastname1", lastname1);
        //    param[4] = new SqlParameter("@firstname2", firstname2);
        //    param[5] = new SqlParameter("@middlename2", middlename2);
        //    param[6] = new SqlParameter("@lastname2", lastname2);
        //    param[7] = new SqlParameter("@firstname3", firstname3);
        //    param[8] = new SqlParameter("@middlename3", middlename3);
        //    param[9] = new SqlParameter("@lastname3", lastname3);
           

        //    EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
        //    sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
        //    return dsd.Tables[0];
        //}

        public DataTable insertcatalogbookentry(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string UniFormTitle,int transNo, int FormID)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@ctrl_no", ctrl_no);
            param[1] = new SqlParameter("@firstname1", firstname1);
            param[2] = new SqlParameter("@middlename1", middlename1);
            param[3] = new SqlParameter("@lastname1", lastname1);
            param[4] = new SqlParameter("@firstname2", firstname2);
            param[5] = new SqlParameter("@middlename2", middlename2);
            param[6] = new SqlParameter("@lastname2", lastname2);
            param[7] = new SqlParameter("@firstname3", firstname3);
            param[8] = new SqlParameter("@middlename3", middlename3);
            param[9] = new SqlParameter("@lastname3", lastname3);
            param[10] = new SqlParameter("@UniFormTitle", UniFormTitle);
            param[11] = new SqlParameter("@transNo", transNo);
            param[12] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }













        //public DataTable DeleteMassCatalog(string accessionnumber, string AppName, string UserId, string IpAddress)
        //{
        //    DataSet dsd = new DataSet();
        //    SqlParameter[] param = new SqlParameter[4];
        //    param[0] = new SqlParameter("@accessionnumber", accessionnumber);
        //    param[1] = new SqlParameter("@AppName", AppName);
        //    param[2] = new SqlParameter("@UId", UserId);
        //    param[3] = new SqlParameter("@IpAddress", IpAddress);

        //    EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
        //    sqlDataBase.ExceProc("[dbo].[sp_DeleteCatalog]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
        //    return dsd.Tables[0];
        //}

        //public DataTable UpdateinsertDirectCatalogExpress(string accessionno, string accessionnocp, string cpbook, string form, int accid, DateTime accdate, string booktitle, decimal bookprice, string status, string issuestatus, string checkstatus, int edityear, int copynumber, int pubyear, string billno, DateTime billdate, string itemtype, string original_currency,
        //                                                   string userid, string vendorsource, int deptcode, int itemcatcode, string bookstitle, string volno, int part, string initpages, string parts, int publishcode, string edition, string isbn, string subject1, string subject2, string subject3, string bibilopages, string issn, string volume,
        //                                                   int language, string Firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3, string classno, string bookno, int pages, string media_type, string searchtext, string volumenoV,
        //                                                   string InsUp, string firstname, string pubcity, int location, string result, int octrl, string audi, string session, string ipa, string title, string edf1, string edm1, string edL1, string edf2, string edm2, string edL2, string edf3, string edm3, string edL3, string compf1, string compm1, string compL1,

        //                                                  string compf2, string compm2, string compL2, string compf3, string compm3, string compL3, string ilf1, string ilm1, string ilL1, string ilf2, string ilm2, string ilL2, string ilf3, string ilm3, string ilL3, string tranf1, string tranm1, string tranL1, string tranf2, string tranm2, string tranL2,
        //                                                   string tranf3, string tranm3, string tranL3, int setofbooks, string appname, int transNo)
        //{
        //    DataSet dsd = new DataSet();
        //    SqlParameter[] param = new SqlParameter[101];
        //    param[0] = new SqlParameter("@accessionnumber_1", accessionno);
        //    param[1] = new SqlParameter("@accessionnumber_1_Cp", accessionnocp);
        //    param[2] = new SqlParameter("@cp_booknumber", cpbook);
        //    param[3] = new SqlParameter("@form_4", form);
        //    param[4] = new SqlParameter("@accessionid_5", accid);
        //    param[5] = new SqlParameter("@accessionid_6", accdate);
        //    param[6] = new SqlParameter("@booktitle_7", booktitle);
        //    param[7] = new SqlParameter("@@bookprice_8", bookprice);
        //    param[8] = new SqlParameter("@status_9", status);
        //    param[9] = new SqlParameter("@issuestatus_10", issuestatus);
        //    param[10] = new SqlParameter("@checkstatus_11", checkstatus);
        //    param[11] = new SqlParameter("@editionyear_12", edityear);
        //    param[12] = new SqlParameter("@copynumber_13", copynumber);
        //    param[13] = new SqlParameter("@pubyear_14", pubyear);
        //    param[14] = new SqlParameter("@biilNo_15", billno);
        //    param[15] = new SqlParameter("@billDate_16", billdate);
        //    param[16] = new SqlParameter("@Item_type_17", itemtype);
        //    param[17] = new SqlParameter("@OriginalCurrency_18", original_currency);
        //    param[18] = new SqlParameter("@userid_19", userid);
        //    param[19] = new SqlParameter("@vendor_source_20", vendorsource);
        //    param[20] = new SqlParameter("@DeptCode_21", deptcode);
        //    param[21] = new SqlParameter("@ItemCategoryCode_22", itemcatcode);
        //    param[22] = new SqlParameter("@bookStitle_23", bookstitle);
        //    param[23] = new SqlParameter("@volumenumber_24", volno);
        //    param[24] = new SqlParameter("@Part_24_2", part);
        //    param[25] = new SqlParameter("@initpages_25", initpages);
        //    param[26] = new SqlParameter("@parts_26", parts);
        //    param[27] = new SqlParameter("@publishercode_27", publishcode);
        //    param[28] = new SqlParameter("@edition_28", edition);
        //    param[29] = new SqlParameter("@isbn_29", isbn);
        //    param[30] = new SqlParameter("@subject1_30", subject1);
        //    param[31] = new SqlParameter("@subject2_31", subject2);
        //    param[32] = new SqlParameter("@subject3_32", subject3);
        //    param[33] = new SqlParameter("@bibliopages_33", bibilopages);
        //    param[34] = new SqlParameter("@issn_34", issn);
        //    param[35] = new SqlParameter("@volume_35", volume);
        //    param[36] = new SqlParameter("@language_36", language);
        //    param[37] = new SqlParameter("@firstname1_37", Firstname1);
        //    param[38] = new SqlParameter("@middlename1_38", middlename1);
        //    param[39] = new SqlParameter("@lastname1_39", lastname1);
        //    param[40] = new SqlParameter("@firstname2_40", firstname2);
        //    param[41] = new SqlParameter("@middlename2_41", middlename2);
        //    param[42] = new SqlParameter("@lastname2_42", lastname2);
        //    param[43] = new SqlParameter("@firstname3_43", firstname3);
        //    param[44] = new SqlParameter("@middlename3_44", middlename3);
        //    param[45] = new SqlParameter("@lastname3_45", lastname3);
        //    param[46] = new SqlParameter("@classno_46", classno);
        //    param[47] = new SqlParameter("@bookno_47", bookno);
        //    param[48] = new SqlParameter("@pages_48", pages);
        //    param[49] = new SqlParameter("@media_type_49", media_type);
        //    param[50] = new SqlParameter("@searchtext_49_1", searchtext);
        //    param[51] = new SqlParameter("@volumenumberV_50", volumenoV);
        //    param[52] = new SqlParameter("@InsUpd48", InsUp);
        //    param[53] = new SqlParameter("@firstname_49", firstname);
        //    param[54] = new SqlParameter("@PubCity_50", pubcity);
        //    param[55] = new SqlParameter("@Location_51", location);
        //    param[56] = new SqlParameter("@Result55", result);
        //    param[57] = new SqlParameter("@OCtrl_no", octrl);
        //    param[58] = new SqlParameter("@Audi_101", audi);
        //    param[59] = new SqlParameter("@Sess_102", session);
        //    param[60] = new SqlParameter("@IPA_103", ipa);
        //    param[61] = new SqlParameter("@Title_104", title);
        //    param[62] = new SqlParameter("@EdF1", edf1);
        //    param[63] = new SqlParameter("@EdM1", edm1);
        //    param[64] = new SqlParameter("@EdL1", edL1);
        //    param[65] = new SqlParameter("@EdF2", edf2);
        //    param[66] = new SqlParameter("@EdM2", edm2);
        //    param[67] = new SqlParameter("@EdL2", edL2);
        //    param[68] = new SqlParameter("@EdF3", edf3);
        //    param[69] = new SqlParameter("@EdM3", edm3);
        //    param[70] = new SqlParameter("@EdL3", edL3);
        //    param[71] = new SqlParameter("@CompF1", compf1);
        //    param[72] = new SqlParameter("@CompM1", compm1);
        //    param[73] = new SqlParameter("@CompL1", compL1);
        //    param[74] = new SqlParameter("@CompF2", compf2);
        //    param[75] = new SqlParameter("@CompM2", compm2);
        //    param[76] = new SqlParameter("@CompL2", compL2);
        //    param[77] = new SqlParameter("@CompF3", compf3);
        //    param[78] = new SqlParameter("@CompM3", compm3);
        //    param[79] = new SqlParameter("@CompL3", compL3);
        //    param[80] = new SqlParameter("@IlF1", ilf1);
        //    param[81] = new SqlParameter("@IlM1", ilm1);
        //    param[82] = new SqlParameter("@IlL1", ilL1);
        //    param[83] = new SqlParameter("@IlF2", ilf2);
        //    param[84] = new SqlParameter("@IlM2", ilm2);
        //    param[85] = new SqlParameter("@IlL2", ilL2);
        //    param[86] = new SqlParameter("@IlF3", ilf3);
        //    param[87] = new SqlParameter("@IlM3", ilm3);
        //    param[88] = new SqlParameter("@IlL3", ilL3);
        //    param[89] = new SqlParameter("@TranF1", tranf1);
        //    param[90] = new SqlParameter("@TranM1", tranm1);
        //    param[91] = new SqlParameter("@TranL1", tranL1);
        //    param[92] = new SqlParameter("@TranF2", tranf2);
        //    param[93] = new SqlParameter("@TranM2", tranm2);
        //    param[94] = new SqlParameter("@TranL2", tranL2);
        //    param[95] = new SqlParameter("@TranF3", tranf3);
        //    param[96] = new SqlParameter("@TranM3", tranm3);
        //    param[97] = new SqlParameter("@TranL3", tranL3);
        //    param[98] = new SqlParameter("@SetOFbooks", setofbooks);
        //    param[99] = new SqlParameter("@AppName", appname);
        //    param[100] = new SqlParameter("@TransNo", transNo);


        //    EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
        //    sqlDataBase.ExceProc("[dbo].[insert_DirectCatalog_Express]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
        //    return dsd.Tables[0];
        //}

        //public DataTable updateinsertaccessionexpress(string AccessionNo, string Accessioncp, int copyno, int ctrlno, decimal bookprice, string booknocp, string item_type, DateTime AccnCpDate, string billno, DateTime billdate, int deptid, int itemcategory, string vendor, int locid, string userid, string ipaddress, string appname, int transno)
        //{
        //    DataSet dsd = new DataSet();
        //    SqlParameter[] param = new SqlParameter[18];
        //    param[0] = new SqlParameter("@AccessionNo", AccessionNo);
        //    param[1] = new SqlParameter("@AccessionNoCp", Accessioncp);
        //    param[2] = new SqlParameter("@Copynnumber", copyno);
        //    param[3] = new SqlParameter("@Ctrl_no", ctrlno);
        //    param[4] = new SqlParameter("@BookPrice", bookprice);
        //    param[5] = new SqlParameter("@BookNumberCP", booknocp);
        //    param[6] = new SqlParameter("@Item_type", item_type);

        //    param[7] = new SqlParameter("@AccnCpDate", AccnCpDate);
        //    param[8] = new SqlParameter("@BiilNo", billno);
        //    param[9] = new SqlParameter("@BillDate", billdate);
        //    param[10] = new SqlParameter("@DeptId", deptid);
        //    param[11] = new SqlParameter("@ItemCategory", itemcategory);
        //    param[12] = new SqlParameter("@Vendor", vendor);
        //    param[13] = new SqlParameter("@LocId", locid);
        //    param[14] = new SqlParameter("@userid", userid);
        //    param[15] = new SqlParameter("@IpAddress", ipaddress);
        //    param[16] = new SqlParameter("@AppName", appname);
        //    param[17] = new SqlParameter("@TransNo", transno);

        //    EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
        //    sqlDataBase.ExceProc("[dbo].[insert_Accession_ExpressCP]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
        //    return dsd.Tables[0];
        //}


        public DataTable insertcomparativechart(int txtorderno, string txtorderno0, DateTime txtorderdate, string txtcmbvendor, string txtCmbPublisher, string txtSTitle, string txtau1firstnm, decimal txtbookprice, decimal txtSpecialPrice, string Action)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@txtorderno", txtorderno);
            param[1] = new SqlParameter("@txtorderno0", txtorderno0);
            param[2] = new SqlParameter("@txtorderdate", txtorderdate);
            param[3] = new SqlParameter("@txtcmbvendor", txtcmbvendor);
            param[4] = new SqlParameter("@txtCmbPublisher", txtCmbPublisher);
            param[5] = new SqlParameter("@txtSTitle", txtSTitle);
            param[6] = new SqlParameter("@txtau1firstnm", txtau1firstnm);

            param[7] = new SqlParameter("@txtbookprice", txtbookprice);
            param[8] = new SqlParameter("@txtSpecialPrice", txtSpecialPrice);
            param[9] = new SqlParameter("@Action", Action);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[ComparitiveChart]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertjournalpaymenttransaction(int PaymentID, string InvoiceID, decimal InvoiceAmount, string userid)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PaymentID", PaymentID);
            param[1] = new SqlParameter("@InvoiceID", InvoiceID);
            param[2] = new SqlParameter("@InvoiceAmount", InvoiceAmount);
            param[3] = new SqlParameter("@userid", userid);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_paymenttransaction_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertjournalarrival1(string journal_no, DateTime exp_date, string volume, string issues, string parts, string indexes, string Status, string Remarks, string doc_id, string issue_type, DateTime arr_date, string arr_year, DateTime publication_Date, string ISSNNO, string Media_Print, string Media_Online, DateTime publicationDatee)

        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@journal_no_1", journal_no);
            param[1] = new SqlParameter("@exp_date_2", exp_date);
            param[2] = new SqlParameter("@volume_3", volume);
            param[3] = new SqlParameter("@issues_4", issues);
            param[4] = new SqlParameter("@parts_5", parts);
            param[5] = new SqlParameter("@indexes_6", indexes);
            param[6] = new SqlParameter("@Status_7", Status);
            param[7] = new SqlParameter("@Remarks_8", Remarks);
            param[8] = new SqlParameter("@doc_id_9", doc_id);
            param[9] = new SqlParameter("@issue_type_10", issue_type);
            param[10] = new SqlParameter("@arr_date_11", arr_date);
            param[11] = new SqlParameter("@arr_year_12", arr_year);
            param[12] = new SqlParameter("@publication_Date_13", publication_Date);
            param[13] = new SqlParameter("@ISSNNO_14", ISSNNO);
            param[14] = new SqlParameter("@Media_Print_15", Media_Print);
            param[15] = new SqlParameter("@Media_Online_16", Media_Online);
            param[16] = new SqlParameter("@PublicationDate_17", publicationDatee);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_journal_arrival_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        //public DataTable UpdateBudgetApprove(int departmentcode, string session, decimal indent, int userid, int FormID)
        //{
        //    DataSet dsd = new DataSet();
        //    SqlParameter[] param = new SqlParameter[5];
        //    param[0] = new SqlParameter("@departmentcode", departmentcode);
        //    param[1] = new SqlParameter("@Curr_Session", session);
        //    param[2] = new SqlParameter("@IndentValue", indent);
        //    param[3] = new SqlParameter("@UserId", userid);
        //    param[4] = new SqlParameter("@FormID", FormID);
        //    EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
        //    sqlDataBase.ExceProc("[MTR].[UpdateBudgetAddApprCommit]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
        //    return dsd.Tables[0];
        //}

        public DataTable insertlostitementry(string Receipt_No, string Member_id, DateTime Receipt_Date, Decimal Amount, string status, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Receipt_No", Receipt_No);
            param[1] = new SqlParameter("@Member_id", Member_id);
            param[2] = new SqlParameter("@Receipt_Date", Receipt_Date);
            param[3] = new SqlParameter("@Amount", Amount);
            param[4] = new SqlParameter("@status", status);
            param[5] = new SqlParameter("@userid", userid);
            param[3] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@userid", userid);
            param[3] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@userid", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_CircReceiptNo_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable insertlostitementry1(string userid, decimal totalfine, string userid1, int FormID, int Type)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@userid", userid);
            param[1] = new SqlParameter("@totalfine", totalfine);
            param[2] = new SqlParameter("@userid1", userid1);
            param[3] = new SqlParameter("@FormID",FormID);
            param[4] = new SqlParameter("@Type",Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_circreceivemaster_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
 
       

        public DataTable upsertaccwithinvindent(int srno, DateTime curr_date, Decimal postage, Decimal net_amt, Decimal disc_amt, Decimal disc_percentage, string vendorid, Decimal handling_charge, Decimal total_amt, int rate_followed_by, Decimal pay_amt, string invoice_no, string bill_no, DateTime bill_date, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@srno", srno);
            param[1] = new SqlParameter("@curr_date", curr_date);
            param[2] = new SqlParameter("@postage", postage);
            param[3] = new SqlParameter("@net_amt", net_amt);
            param[4] = new SqlParameter("@disc_amt", disc_amt);
            param[5] = new SqlParameter("@disc_percentage", disc_percentage);
            param[6] = new SqlParameter("@vendorid", vendorid);
            param[7] = new SqlParameter("@handling_charge", handling_charge);
            param[8] = new SqlParameter("@total_amt", total_amt);
            param[9] = new SqlParameter("@rate_followed_by", rate_followed_by);
            param[10] = new SqlParameter("@pay_amt", pay_amt);
            param[11] = new SqlParameter("@invoice_no", invoice_no);
            param[12] = new SqlParameter("@bill_no", bill_no);
            param[13] = new SqlParameter("@bill_date", bill_date);
            param[14] = new SqlParameter("@userid", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[update_Direct_invoice_master_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertBookRelators(int ctrl_no, string editorFname1, string editorMname1, string editorLname1, string editorFname2, string editorMname2, string editorLname2, string editorFname3, string editorMname3, string editorLname3, string CompilerFname1, string CompilerMname1, string CompilerLname1, string CompilerFname2, string CompilerMname2, string CompilerLname2, string CompilerFname3, string CompilerMname3, string CompilerLname3, string illusFname1, string illusMname1, string illusLname1, string illusFname2, string illusMname2, string illusrLname2, string illusFname3, string illusMname3, string illusLname3, string TranslatorFname1, string TranslatorMname11, string TranslatorLname1, string TranslatorFname2, string TranslatorMname2, string TranslatorLname2, string TranslatorFname3, string TranslatorMname3, string TranslatorLname3, int FormID)

        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[38];

            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@editorFname1_2", editorFname1);
            param[2] = new SqlParameter("@editorMname1_3", editorMname1);
            param[3] = new SqlParameter("@editorLname1_4", editorLname1);
            param[4] = new SqlParameter("@editorFname2_5", editorFname2);
            param[5] = new SqlParameter("@editorMname2_6", editorFname2);
            param[6] = new SqlParameter("@editorLname2_7", editorLname2);
            param[7] = new SqlParameter("@editorFname3_8", editorFname3);
            param[8] = new SqlParameter("@editorMname3_9", editorMname3);
            param[9] = new SqlParameter("@editorLname3_10", editorLname3);
            param[10] = new SqlParameter("@CompilerFname1_11", CompilerFname1);
            param[11] = new SqlParameter("@CompilerMname1_12", CompilerMname1);
            param[12] = new SqlParameter("@CompilerLname1_13", CompilerLname1);
            param[13] = new SqlParameter("@CompilerFname2_14", CompilerFname2);
            param[14] = new SqlParameter("@CompilerMname2_15", CompilerMname2);
            param[15] = new SqlParameter("@CompilerLname2_16", CompilerLname2);
            param[16] = new SqlParameter("@CompilerFname3_17", CompilerFname3);
            param[17] = new SqlParameter("@CompilerMname3_18", CompilerMname3);
            param[18] = new SqlParameter("@CompilerLname3_19", CompilerLname3);
            param[19] = new SqlParameter("@illusFname1_20", illusFname1);
            param[20] = new SqlParameter("@illusMname1_21", illusMname1);
            param[21] = new SqlParameter("@illusLname1_22", illusLname1);
            param[22] = new SqlParameter("@illusFname2_23", illusFname2);
            param[23] = new SqlParameter("@illusMname2_24", illusMname2);
            param[24] = new SqlParameter("@illusrLname2_25", illusrLname2);
            param[25] = new SqlParameter("@illusFname3_26", illusFname3);
            param[26] = new SqlParameter("@illusMname3_27", illusMname3);
            param[27] = new SqlParameter("@illusLname3_28", illusLname3);
            param[28] = new SqlParameter("@TranslatorFname1_29", TranslatorFname1);
            param[29] = new SqlParameter("@TranslatorMname11_30", TranslatorMname11);
            param[30] = new SqlParameter("@TranslatorLname1_31", TranslatorLname1);
            param[31] = new SqlParameter("@TranslatorFname2_32", TranslatorFname2);
            param[32] = new SqlParameter("@TranslatorMname2_33", TranslatorMname2);
            param[33] = new SqlParameter("@TranslatorLname2_34", TranslatorLname2);
            param[34] = new SqlParameter("@TranslatorFname3_35", TranslatorFname3);
            param[35] = new SqlParameter("@TranslatorMname3_36", TranslatorMname3);
            param[36] = new SqlParameter("@TranslatorLname3_37", TranslatorLname3);
            param[37] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        public DataTable updateinserteditorRelators(int ctrl_no, string editorFname1, string editorMname1, string editorLname1, string editorFname2, string editorMname2, string editorLname2, string editorFname3, string editorMname3, string editorLname3,int FormID)

        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ctrl_no_1", ctrl_no);
            param[1] = new SqlParameter("@editorFname1_2", editorFname1);
            param[2] = new SqlParameter("@editorMname1_3", editorMname1);
            param[3] = new SqlParameter("@editorLname1_4", editorLname1);
            param[4] = new SqlParameter("@editorFname2_5", editorFname2);
            param[5] = new SqlParameter("@editorMname2_6", editorMname2);
            param[6] = new SqlParameter("@editorLname2_7", editorLname2);
            param[7] = new SqlParameter("@editorFname3_8", editorFname3);
            param[8] = new SqlParameter("@editorMname3_9", editorMname3);
            param[9] = new SqlParameter("@editorLname3_10", editorLname3);
            param[10] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertcompilerRelators(string CompilerFname1,  string CompilerMname1, string CompilerLname1, string CompilerFname2, string CompilerMname2, string CompilerLname2, string CompilerFname3, string CompilerMname3, string CompilerLname3, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CompilerFname1_11", CompilerFname1);
            param[1] = new SqlParameter("@CompilerMname1_12", CompilerMname1);
            param[2] = new SqlParameter("@CompilerLname1_13", CompilerLname1);
            param[3] = new SqlParameter("@CompilerFname2_14", CompilerFname2);
            param[4] = new SqlParameter("@CompilerMname2_15", CompilerMname2);
            param[5] = new SqlParameter("@CompilerLname2_16", CompilerLname2);
            param[6] = new SqlParameter("@CompilerFname3_17", CompilerFname3);
            param[7] = new SqlParameter("@CompilerMname3_18", CompilerMname3);
            param[8] = new SqlParameter("@CompilerLname3_19", CompilerLname3);
            param[9] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable updateinsertTranslatorRelators(string TranslatorFname1, string TranslatorMname11, string TranslatorLname1, string TranslatorFname2, string TranslatorMname2, string TranslatorLname2, string TranslatorFname3, string TranslatorMname3, string TranslatorLname3, int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@TranslatorFname1_29", TranslatorFname1);
            param[1] = new SqlParameter("@TranslatorMname11_30", TranslatorMname11);
            param[2] = new SqlParameter("@TranslatorLname1_31", TranslatorLname1);
            param[3] = new SqlParameter("@TranslatorFname2_32", TranslatorFname2);
            param[4] = new SqlParameter("@TranslatorMname2_33", TranslatorMname2);
            param[5] = new SqlParameter("@TranslatorLname2_34", TranslatorLname2);
            param[6] = new SqlParameter("@TranslatorFname3_35", TranslatorFname3);
            param[7] = new SqlParameter("@TranslatorMname3_36", TranslatorMname3);
            param[8] = new SqlParameter("@TranslatorLname3_37", TranslatorLname3);
            param[9] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertillusRelators(string illusFname1, string illusMname1, string illusLname1, string illusFname2, string illusMname2, string illusrLname2, string illusFname3, string illusMname3, string illusLname3,int FormID)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@illusFname1_20", illusFname1);
            param[1] = new SqlParameter("@illusMname1_21", illusMname1);
            param[2] = new SqlParameter("@illusLname1_22", illusLname1);
            param[3] = new SqlParameter("@illusFname2_23", illusFname2);
            param[4] = new SqlParameter("@illusMname2_24", illusMname2);
            param[5] = new SqlParameter("@illusrLname2_25", illusrLname2);
            param[6] = new SqlParameter("@illusFname3_26", illusFname3);
            param[7] = new SqlParameter("@illusMname3_27", illusMname3);
            param[8] = new SqlParameter("@illusLname3_28", illusLname3);
            param[9] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookRelators_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
 
        public DataTable DeletepostMessage(string cid, int FormID)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cid_1", cid);
            param[1] = new SqlParameter("@FormID", FormID);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[delete_postMessages_1] ", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertDirinvtrans(int srno, int dept,decimal discount,string title, int language_id, string volume, string part, string edition, string author_type, string first_name, string middle_name, string  last_name, string publisher_id,int noofcopy,decimal price_copy, int curr_code,decimal exchange_rate, string isbn,int category_id,int media_type,int SrNoOld,int sr_id,decimal tot_amt, string Indentnumber, string IndentId, string Status )
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@srno_1", srno);
            param[1] = new SqlParameter("@dept_2", dept);
            param[2] = new SqlParameter("@discount_3", discount);
            param[3] = new SqlParameter("@title_4", title);
            param[4] = new SqlParameter("@language_id_5", language_id);
            param[5] = new SqlParameter("@volume_6", volume);
            param[6] = new SqlParameter("@part_7", part);
            param[7] = new SqlParameter("@edition_8", edition);
            param[8] = new SqlParameter("@author_type_9", author_type);
            param[9] = new SqlParameter("@first_name_10", first_name);
            param[10] = new SqlParameter("@middle_name_11", middle_name);
            param[11] = new SqlParameter("@last_name_12", last_name);
            param[12] = new SqlParameter("@publisher_id_13",publisher_id );
            param[13] = new SqlParameter("@noofcopy_14", noofcopy);
            param[14] = new SqlParameter("@price_copy_15", price_copy);
            param[15] = new SqlParameter("@curr_code_16", curr_code);
            param[16] = new SqlParameter("@exchange_rate_17", exchange_rate);
            param[17] = new SqlParameter("@isbn_18", isbn);
            param[18] = new SqlParameter("@category_id_19", category_id);
            param[19] = new SqlParameter("@media_type_20", media_type);
            param[20] = new SqlParameter("@SrNoOld_21", SrNoOld);
            param[21] = new SqlParameter("@sr_id_22", sr_id);
            param[22] = new SqlParameter("@tot_amt_23", tot_amt);
            param[23] = new SqlParameter("@IndentNumber_24", Indentnumber);
            param[24] = new SqlParameter("@IndentId_25", IndentId);
            param[25] = new SqlParameter("@status_26", Status);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Dir_inv_trans_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

 

     
        public DataTable upsertaccwithinvindent1(int srno, int dept, decimal discount, string title, int language_id, string volume, string part, string edition, string author_type, string first_name, string middle_name, string last_name, string publisher_id, int noofcopy, decimal price_copy, int curr_code, decimal exchange_rate, string isbn, int category_id, int media_type, int SrNoold, int Sr_id, decimal tot_amt, string IndentNumber, string IndentId, string Status)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@srno", srno);
            param[1] = new SqlParameter("@dept", dept);
            param[2] = new SqlParameter("@discount", discount);
            param[3] = new SqlParameter("@title", title);
            param[4] = new SqlParameter("@language_id", language_id);
            param[5] = new SqlParameter("@volume", volume);
            param[6] = new SqlParameter("@part", part);
            param[7] = new SqlParameter("@edition", edition);
            param[8] = new SqlParameter("@author_type", author_type);
            param[9] = new SqlParameter("@first_name", first_name);
            param[10] = new SqlParameter("@middle_name", middle_name);
            param[11] = new SqlParameter("@last_name", last_name);
            param[12] = new SqlParameter("@last_name", last_name);
            param[13] = new SqlParameter("@noofcopy", noofcopy);
            param[14] = new SqlParameter("@price_copy", price_copy);
            param[15] = new SqlParameter("@curr_code", curr_code);
            param[16] = new SqlParameter("@exchange_rate", exchange_rate);
            param[17] = new SqlParameter("@isbn", isbn);
            param[18] = new SqlParameter("@category_id", category_id);
            param[19] = new SqlParameter("@media_type", media_type);
            param[20] = new SqlParameter("@SrNoold", SrNoold);
            param[21] = new SqlParameter("@Sr_id", Sr_id);
            param[22] = new SqlParameter("@tot_amt", tot_amt);
            param[23] = new SqlParameter("@IndentNumber", IndentNumber);
            param[24] = new SqlParameter("@IndentId", IndentId);
            param[25] = new SqlParameter("@Status", Status);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_Dir_inv_trans_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        
        public DataTable insertcataloguedBookAuthor(int ctrl_no, string firstname1, string middlename1, string lastname1, string firstname2, string middlename2, string lastname2, string firstname3, string middlename3, string lastname3)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@ctrl_no", ctrl_no);
            param[1] = new SqlParameter("@firstname1", firstname1);
            param[2] = new SqlParameter("@middlename1", middlename1);
            param[3] = new SqlParameter("@lastname1", lastname1);
            param[4] = new SqlParameter("@firstname2", firstname2);
            param[5] = new SqlParameter("@middlename2", middlename2);
            param[6] = new SqlParameter("@lastname2", lastname2);
            param[7] = new SqlParameter("@firstname3", firstname3);
            param[8] = new SqlParameter("@middlename3", middlename3);
            param[9] = new SqlParameter("@lastname3", lastname3);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BookAuthor_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsert_catalogCardPrint1(string AccessionNumber, string ClassNumber, string BookNumber, string accesspoints, string Contents, string CardTitle, string Volume, string Copy,int id, string Tag)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccessionNumber_1", AccessionNumber);
            param[1] = new SqlParameter(" @ClassNumber_2", ClassNumber);
            param[2] = new SqlParameter("@BookNumber_3", BookNumber);
            param[3] = new SqlParameter(" @accesspoints_4", accesspoints);
            param[4] = new SqlParameter("@Contents_5", Contents);
            param[5] = new SqlParameter("@CardTitle_6", CardTitle);
            param[6] = new SqlParameter("@Volume_7", Volume);
            param[7] = new SqlParameter("@Copy_8", Copy);
            param[8] = new SqlParameter("@id_9", id);
            param[9] = new SqlParameter("@Tag_10", Tag);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_catalogCardPrint_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }
        public DataTable Updateinsertbookaccessionmaster(string accessionnumber, string ordernumber, string indentnumber, string form,int accessionid, DateTime accessioneddate, string booktitle, int srno, string released,decimal bookprice, int srNoOld, string biilNo,DateTime billDate, string Item_type,int OriginalPrice, string OriginalCurrency, string userid, string vendor_source,int DeptCode,int DSrno, string DeptName, string ItemCategory,int ItemCategoryCode,string BookNumber, string AppName , string ipaddress,int TransNo)
        {

            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@accessionnumber_1", accessionnumber);
            param[1] = new SqlParameter("@ordernumber_2", ordernumber);
            param[2] = new SqlParameter("@indentnumber_3", indentnumber);
            param[3] = new SqlParameter(" @form_4", form);
            param[4] = new SqlParameter("@accessionid_5", accessionid);
            param[5] = new SqlParameter("@accessioneddate_6", accessioneddate);
            param[6] = new SqlParameter("@booktitle_7", booktitle);
            param[7] = new SqlParameter("@srno_8", srno);
            param[8] = new SqlParameter("@released_9", released);
            param[9] = new SqlParameter("@bookprice_10", bookprice);
            param[10] = new SqlParameter("@srNoOld_11", srNoOld);
            param[11] = new SqlParameter("@biilNo_12", biilNo);
            param[12] = new SqlParameter("@billDate_13", billDate);
            param[13] = new SqlParameter("@Item_type_14", Item_type);
            param[14] = new SqlParameter("@OriginalPrice_15", OriginalPrice);
            param[15] = new SqlParameter("@OriginalCurrency_16", OriginalCurrency);
            param[16] = new SqlParameter("@userid_17", userid);
            param[17] = new SqlParameter("@vendor_source_18", vendor_source);
            param[18] = new SqlParameter("@DeptCode_19", DeptCode);
            param[19] = new SqlParameter("@DSrno_20", DSrno);
            param[20] = new SqlParameter("@DeptName_21", DeptName);
            param[21] = new SqlParameter("@ItemCategory_22", ItemCategory);
           
            param[22] = new SqlParameter("@ItemCategoryCode_23", ItemCategoryCode);
            param[23] = new SqlParameter("@BookNumber", BookNumber);
            param[24] = new SqlParameter("@AppName", AppName);
            param[25] = new SqlParameter("@ItemCategoryCode_23", ItemCategoryCode);
            param[26] = new SqlParameter("@ipaddress", ipaddress);
            param[27] = new SqlParameter("@TransNo", TransNo);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_bookaccessionmaster_2]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable updateinsertbinderinformation1(int binderid, string name,string address, string city, string state, string email,string phoneno, int maxissuedays , int overduecharges, string userid)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@binderid_1", binderid);
            param[1] = new SqlParameter("@name_2", name);
            param[2] = new SqlParameter("@address_3", address);
            param[3] = new SqlParameter("@city_4", city);
            param[4] = new SqlParameter("@state_5", state);
            param[5] = new SqlParameter("@email_6", email);
            param[6] = new SqlParameter("@phoneno_7", phoneno);
            param[7] = new SqlParameter("@maxissuedays_8", maxissuedays);
            param[8] = new SqlParameter("@overduecharges_9", overduecharges);
            param[9] = new SqlParameter("@userid_10", userid);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[insert_BinderInformation_1]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }

        public DataTable GetItemType(int ItemID, int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ItemID", ItemID);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@FormID", FormID);
            param[3] = new SqlParameter("@Type", Type);
            //param[0].SqlDbType = SqlDbType.Int;
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetItemType](@ItemID, @UserID, @FormID, @Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

      
        public DataTable GetOrderNo(int UserID, int FormID, int Type , string cmbCategory)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@cmbCategory", cmbCategory);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetOrderNo](@UserID, @FormID, @Type, @cmbCategory)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        // akansha fn
        public DataTable GetCDService(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);          
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCDService](@UserID,@FormID,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetCDServiceMAxID(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);
           
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_GetCDServiceMAxID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GETEdocMaxID(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetEDOCMAXID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable Fn_CD_Service_Level1(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_CD_Service_Level1](@UserID,@FormID,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable Fn_CD_SERVICE_LEVEL1MAxID(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[Fn_CD_SERVICE_LEVEL1MAxID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable Fn_CD_Service_Level2(int UserID, int FormID, int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_CD_Service_Level2](@UserID,@FormID,@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetCDMasterMAxID(int Type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetCDMasterMAxID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        
            public DataTable GetConsortiumMAxID(int Type)
            {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc(" SELECT * FROM  [MTR].[GetConsortiumMAxID](@Type)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
            }

        public DataTable GetConsortium(int UserID, int FormID, int Type, string txtCategory)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@txtCategory", txtCategory);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetConsortium](@UserID,@FormID,@Type,@txtCategory)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetConsortiumName(int UserID, int FormID, int Type, string txtConsortium,string hd_name, string txtUrl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@txtConsortium", txtConsortium);
            param[4] = new SqlParameter("@hd_name", hd_name);
            param[5] = new SqlParameter("@txtUrl", txtUrl);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetConsortiumName](@UserID,@FormID,@Type,@txtConsortium,@hd_name,@txtUrl)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetOnlineJournal(int UserID, int FormID, int Type, string Category )
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@Category", Category);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetOnlineJournal](@UserID,@FormID,@Type,@Category)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetonlineJournal_attachments(int UserID, int FormID, int Type, string Category)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@Category", Category);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetonlineJournal_attachments](@UserID,@FormID,@Type,@Category)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable Getcircularmessageposting(int UserID, int FormID, int Type, int id)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@id", id);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_Getcircularmessageposting](@UserID,@FormID,@Type,@id)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetCircularVIEW(int UserID, int FormID, int Type, DateTime Dated, string filterqry)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@Date", Dated);
            param[4] = new SqlParameter("@filterqry", filterqry);
            
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetCircularVIEW](@UserID,@FormID,@Type,@Date,@filterqry)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }
        public DataTable GetCircularMeaageVIEW(int UserID, int FormID, int Type,int ddlmtype,DateTime Dated, string filterqry)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@FormID", FormID);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@ddlmtype", ddlmtype);
            param[4] = new SqlParameter("@Date", Dated);
            param[5] = new SqlParameter("@filterqry", filterqry);

            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceFunc("SELECT * FROM  [MTR].[Fn_GetCircularMeaageVIEW](@UserID,@FormID,@Type,@ddlmtype,@Date,@filterqry)", ref dt, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dt;
        }

        public DataTable GetDashboardData(int userid,int Type)
        {
            DataSet dsd = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userid", userid);
            param[1] = new SqlParameter("@Type", Type);
            EduERPAPI.DB.SqlDataBase sqlDataBase = new EduERPAPI.DB.SqlDataBase();
            sqlDataBase.ExceProc("[dbo].[Usp_DashBoard]", ref dsd, param, EduERPAPI.DB.SqlDataBase.GetConnectionString());
            return dsd.Tables[0];
        }


        //end
    }
}
            