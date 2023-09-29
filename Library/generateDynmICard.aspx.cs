using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using Library.App_Code.MultipleFramworks;
using Microsoft.SqlServer.Management.XEvent;
using Library.App_Code.CSharp;

namespace Library
{
    public partial class generateDynmICard : BaseClass
    {
        private static DBIStructure DBI = new DBIStructure();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitle.Text = Request.QueryString["title"];
            }
        }

        protected static object[] btnSearch_Click(string memId, string session, string memGrp, string dept, string courseDesg)
        {
            var items = new System.Collections.Generic.List<dynamicICard_DataFields>();

            string where = string.Empty;
            if (!string.IsNullOrEmpty(memId.Trim()))
            {
                where += " and (userId = '" + memId.Trim() + "' or userCode = '" + memId.Trim() + "') ";
            }
            if (!string.IsNullOrEmpty(session.Trim()) & session.Trim() != "0")
            {
                where += " and session = '" + session.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(memGrp.Trim()) & memGrp.Trim() != "0")
            {
                where += " and memberGroup = '" + memGrp.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(dept.Trim()) & dept.Trim() != "0")
            {
                where += " and departmentCode = '" + dept.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(courseDesg.Trim()) & courseDesg.Trim() != "0")
            {
                where += " and program_id = '" + courseDesg.Trim() + "' ";
            }

            DataTable dt = DBI.GetDataTable("Select * from dynamicIDCard_Fields_View where 1=1 " + where, DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
            {
                var record = new dynamicICard_DataFields();
                record.usercode_P = Convert.ToString(dt.Rows[i]["usercode"]);
                record.memberName_P = Convert.ToString(dt.Rows[i]["fullName"]);
                record.fatherName_P = Convert.ToString(dt.Rows[i]["fatheName"]);
                record.photoUrl_P = Convert.ToString(dt.Rows[i]["photoUrl"]);
                items.Add(record);
            }
            return items.ToArray();
        }

        public static string btnGenerate_Click(string memberList, string pSize, string cardFormat)
        {
            string url = string.Empty;
            System.Web.HttpContext.Current.Session["memberList"] = memberList;
            DataTable dt = DBI.GetDataTable("Select * from dynamic_PageSize where Id = '" + pSize + "'", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                url = Convert.ToString("printDynmIDCard.aspx?idcf=" + cardFormat.Trim() + "&h="+ dt.Rows[0]["pageHeight"]+ "&w="+ dt.Rows[0]["pageWidth"]+ "&r="+ dt.Rows[0]["prow"]+ "&c="+ dt.Rows[0]["pcolumn"]);
            }
            else
            {
                url = "printDynmIDCard.aspx";
            }
            return url;
        }

        public static string getPageSize()
        {
            string str = "<option value='0'>---Select---</option>";
            DataTable dt = DBI.GetDataTable("Select * from dynamic_PageSize", DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                str = Convert.ToString(str + "<option value='"+ dt.Rows[i]["Id"]+ "'>"+ dt.Rows[i]["pageSizeName"]+ "</option>");
            return str;
        }

        public static string getIDCardFormat()
        {
            string str = "<option value='0'>---Select---</option>";
            DataTable dt = DBI.GetDataTable("Select * from DynamicIDcard_Formats", DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                str = Convert.ToString(str + "<option value='"+ dt.Rows[i]["Id"]+ "'>"+ dt.Rows[i]["formatName"]+ "</option>");
            return str;
        }

        public static string getAcedemicSession()
        {
            string str = "<option value='0'>---Select---</option>";
            DataTable dt = DBI.GetDataTable("Select * from AcedemicSessionInformation", DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                str = Convert.ToString(str + "<option value='"+ dt.Rows[i]["AcademicSession"]+ "'>"+ dt.Rows[i]["AcademicSession"]+ "</option>");
            return str;
        }

        public static string getMemberGroup()
        {
            string str = "<option value='0'>---Select---</option>";
            DataTable dt = DBI.GetDataTable("Select distinct classname from circclassmaster", DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                str = Convert.ToString(str + "<option value='"+ dt.Rows[i]["classname"]+ "'>"+ dt.Rows[i]["classname"]+ "</option>");
            return str;
        }

        public static string getDepartments()
        {
            string str = "<option value='0'>---Select---</option>";
            DataTable dt = DBI.GetDataTable("Select * from departmentmaster", DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                str = Convert.ToString(str + "<option value='"+ dt.Rows[i]["departmentcode"]+ "'>"+ dt.Rows[i]["departmentname"]+ "</option>");
            return str;
        }

        public static string getPrograms()
        {
            string str = "<option value='0'>---Select---</option>";
            DataTable dt = DBI.GetDataTable("Select * from Program_Master", DBI.GetConnectionString(DBI.GetConnectionName()));
            for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                str = Convert.ToString(str + "<option value='"+ dt.Rows[i]["program_id"]+ "'>"+ dt.Rows[i]["program_name"]+ "</option>");
            return str;
        }
        //protected void btnReset_Click(object sender, EventArgs e)
        //{

        //}
    }
}

public class dynamicICard_DataFields
{
    private string usercode;
    private string memberName;
    private string fatherName;
    private string gender;
    private string dob;
    private string year;
    private string session;
    private string department;
    private string memberGroup;
    private string course_Designation;
    private string photoUrl;

    public string photoUrl_P
    {
        get
        {
            return photoUrl;
        }
        set
        {
            photoUrl = value;
        }
    }

    public string usercode_P
    {
        get
        {
            return usercode;
        }
        set
        {
            usercode = value;
        }
    }

    public string memberName_P
    {
        get
        {
            return memberName;
        }
        set
        {
            memberName = value;
        }
    }

    public string fatherName_P
    {
        get
        {
            return fatherName;
        }
        set
        {
            fatherName = value;
        }
    }

    public string gender_P
    {
        get
        {
            return gender;
        }
        set
        {
            gender = value;
        }
    }

    public string dob_P
    {
        get
        {
            return dob;
        }
        set
        {
            dob = value;
        }
    }

    public string year_P
    {
        get
        {
            return year;
        }
        set
        {
            year = value;
        }
    }

    public string session_P
    {
        get
        {
            return session;
        }
        set
        {
            session = value;
        }
    }

    public string department_P
    {
        get
        {
            return department;
        }
        set
        {
            department = value;
        }
    }

    public string memberGroup_P
    {
        get
        {
            return memberGroup;
        }
        set
        {
            memberGroup = value;
        }
    }

    public string course_Designation_P
    {
        get
        {
            return course_Designation;
        }
        set
        {
            course_Designation = value;
        }
    }
}