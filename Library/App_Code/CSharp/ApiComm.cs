using Azure.Core;
using CrystalDecisions.Shared;
using LibData.Contract;
using LibData.Model;
using Library.Models;
using Microsoft.SqlServer.Management.Smo;
using Model.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Library.App_Code.CSharp
{
    public class ApiComm
    {
        private string LibApiUrl = "";
        private HttpClient apiCall = null;
        public string url { get; set; }
        private string baseUrl;
        public ApiComm()
        {
            LibApiUrl = ConfigurationManager.AppSettings["libapilocal"];
        }
        public async Task<ReturnData<departmentmaster>> Sample()
        {
            var uri = new Uri("https://localhost:7296/api/Basic/departmentbyid?id=4");
            HttpClient client = new HttpClient();
            client.BaseAddress = uri;
            var resp = client.GetAsync(uri).Result;
            var data = await resp.Content.ReadAsStringAsync();
            var d = JsonConvert.DeserializeObject<ReturnData<departmentmaster>>(data);
            Console.WriteLine(resp);

            ReturnData<departmentmaster> x = new ReturnData<departmentmaster>();
            return d;
        }
        public async Task<ReturnData<LibrarysetupinformationMod>> getLibSetup()
        {
            var url = LibApiUrl + "LibrarySetup/getLibrarySetup";
            apiCall = new HttpClient();
            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var libset = JsonConvert.DeserializeObject<ReturnData<LibrarysetupinformationMod>>(d);
            return libset;
        }
        public async Task<ReturnData<List<departmentmaster>>> getAllDept()
        {
            var url = LibApiUrl + "Basic/DepartmentByName";
            apiCall = new HttpClient();

            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<ReturnData<List<departmentmaster>>>(d);
            return rd;
        }
        public async Task<ReturnData<List<departmentmaster>>> getDeptBySession()
        {
            var sess = LoggedUser.Logged().Session;
            var url = LibApiUrl + "Basic/GetDeptBySession?session=" + sess;
            apiCall = new HttpClient();

            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<ReturnData<List<departmentmaster>>>(d);
            return rd;
        }
        public async Task<ReturnData<List<Translation_Language>>> getLanguages()
        {
            var url = LibApiUrl + "Basic/GetTranslationLanguages";
            apiCall = new HttpClient();

            apiCall.BaseAddress = new Uri(url);

            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<ReturnData<List<Translation_Language>>>(d);
            return rd;
        }

        public async Task<ReturnData<List<UsertypepermissionsMod>>> GetUserTypeAll()
        {
            var url = LibApiUrl + "UserPermission/GetUserTypeAll";
            apiCall = new HttpClient();
            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<ReturnData<List<UsertypepermissionsMod>>>(d);
            return rd;
        }

        public async Task<ReturnData<List<ActionLPermMod>>> GetActionPermOnMember(ActionPermOnMemberCmd req)
        {
            var url = LibApiUrl + "UserPermission/GetActionPermOnMember";
            apiCall = new HttpClient();
            //            apiCall.BaseAddress = new Uri(url);
            var reqsend = new Dictionary<string, string>();
            PropertyInfo[] infos = req.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                reqsend.Add(info.Name, info.GetValue(req, null).ToString());
            }
            var datasend = new FormUrlEncodedContent(reqsend);
            var resp = await apiCall.PostAsync(url, datasend);
            var dret = await resp.Content.ReadAsStringAsync();
            var rd = JsonConvert.DeserializeObject<ReturnData<List<ActionLPermMod>>>(dret);
            return rd;
        }
        public async Task<ReturnData<ActionPermissionMod>> GetMenuItemPermission(UserMenuItemPerm req)
        {
            var url = LibApiUrl + "UserPermission/GetMenuItemPermission";
            apiCall = new HttpClient();
            var sendd = JsonConvert.SerializeObject(req);

            StringContent strCont = new StringContent(sendd, Encoding.UTF8,
            "application/json");

            var ret = new ReturnData<ActionPermissionMod>();

            var resp = await apiCall.PostAsync(url, strCont);
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ret.isSuccess = false;
                ret.Mesg = resp.ReasonPhrase;
                return ret;
            }
            var dret = await resp.Content.ReadAsStringAsync();
            var rd = JsonConvert.DeserializeObject<ReturnData<ActionPermissionMod>>(dret);
            return rd;
        }

        //Follow new calls methods as below 25Sep
        public async Task<ReturnData<CircUserManagementCmd>> GetCircUserById(string userid)
        {
            /*
            var url = LibApiUrl + "CircUser/GetCircUserById?UserId=" + userid;
            apiCall = new HttpClient();
            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<ReturnData<CircUserManagementCmd>>(d);
            return rd;
            */
            var getCal = new GetCall<ReturnData<CircUserManagementCmd>>();
            var url = "CircUser/GetCircUserById";
            NameValueCollection cols = new NameValueCollection();
            cols.Add("UserId", userid);
            var rv = await getCal.CallApi(url, cols);
            return rv;
        }
        public async Task<ReturnData<List<ExchangemasterMod>>> GetExchange()
        {

            var getCal = new GetCall<ReturnData<List<ExchangemasterMod>>>();
            var url = "Basic/GetExchange";
            NameValueCollection cols = new NameValueCollection();
            var rv = await getCal.CallApi(url, cols);
            return rv;
        }
        //get user
        public async Task<ReturnData<CircUserManagementCmd>> getCircUserById(string UserId)
        {
            var getCal = new GetCall<ReturnData<CircUserManagementCmd>>();
            var url = "Basic/getCircUserById";
            NameValueCollection cols = new NameValueCollection();
            cols.Add("UserId", UserId);
            var rv = await getCal.CallApi(url, cols);
            return rv;

        }
        //get call - no parameter
        public async Task<ReturnData<List<CategoryLoadingStatusMod>>> GetCategory()
        {
            var getCal = new GetCall<ReturnData<List<CategoryLoadingStatusMod>>>();
            var url = "Basic/GetCategory";
            NameValueCollection cols = new NameValueCollection();
            var rv = await getCal.CallApi(url, cols);
            return rv;
        }
        //get call with parameter(s)
        public async Task<ReturnData<string>> DeleteProgram(string id)
        {
            var putcall=new PutCall<ReturnData<string>>();
            var url = "Basic/DeleteProgramMaster";
            NameValueCollection cols = new NameValueCollection();
            cols.Add("Id", id);
            var rv = await putcall.CallApi(url, cols);
            return rv;
        }
        //post call
        public async Task<ReturnData<List<ProgramMasterMod>>> GetProgMaster(ProgramCmd req)
        {
            var post = new PostCall<ProgramCmd, List<ProgramMasterMod>>();
            var d = await post.CallApi(req, "Basic/GetProgramMasterCond");
            return d;
        }
        //post call
        public async Task<ReturnData<string>> InsUpdIndent(IndentmasterMod req)
        {
            var post = new PostCall<IndentmasterMod, string>();
            var d = await post.CallApi(req, "Indent/InsertUpdateIndent");
            return d;
        }
        //get call with parameter(s)
        public async Task<ReturnData<IndentmasterMod>> getIndentById(string id)
        {
            var putcall = new GetCall<ReturnData<IndentmasterMod>>();
            var url = "Indent/getIndentById";
            NameValueCollection cols = new NameValueCollection();
            cols.Add("indentno", id);
            var rv = await putcall.CallApi(url, cols);
            return rv;
        }
        public async Task<ReturnData<string>> DelIndentItem(DeleteIndentCmd req)
        {
            var post = new PostCall<DeleteIndentCmd, string>();
            var d = await post.CallApi(req, "Indent/DeleteIndentItem");
            return d;
        }
        public async Task<ReturnData<string>> UpdateIndentBudgetAddAmt(DeptSessionCmd req)
        {
            var post = new PostCall<DeptSessionCmd, string>();
            var d = await post.CallApi(req, "Indent/UpdateIndentBudgetAddAmt");
            return d;
        }
        public async Task<ReturnData<List<CircUserSmall>>> getCircUserByDeptCanReq(UserByCondCmd req)
        {
            var post = new PostCall<UserByCondCmd, List<CircUserSmall>>();
            var d = await post.CallApi(req, "Indent/getCircUserByDeptCanReq");
            return d;
        }
        public async Task<ReturnData<OrderMod>> getOrderByIndentId(string id)
        {
            var putcall = new GetCall<ReturnData<OrderMod>>();
            var url = "Indent/getOrderByIndentId";
            NameValueCollection cols = new NameValueCollection();
            cols.Add("indentno", id);
            var rv = await putcall.CallApi(url, cols);
            return rv;
        }
    }

    public class GetCall<T>
    {
        string LibApiUrl = "";
        public GetCall()
        {
            LibApiUrl = ConfigurationManager.AppSettings["libapilocal"];
        }
        public async Task<T> CallApi(string apiurl, NameValueCollection request)
        {
            var url = LibApiUrl + apiurl;// + request;
            for (int ixd = 0; ixd < request.Count; ixd++)
            {
                if (ixd == 0)
                    url += "?";
                url += request.Keys[ixd] + "=" + request[ixd];
                if (ixd < request.Count - 1)
                    url += "&";
            }
            var apiCall = new HttpClient();
            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<T>(d);
            return rd;
        }
    }


    public class PostCall<S, T>
    {
        string LibApiUrl = "";
        public PostCall()
        {
            LibApiUrl = ConfigurationManager.AppSettings["libapilocal"];
        }
        public async Task<ReturnData<T>> CallApi(S Request, string ControllerAction)
        {
            var url = LibApiUrl + ControllerAction;// "UserPermission/GetMenuItemPermission";
            var apiCall = new HttpClient();
            var sendd = JsonConvert.SerializeObject(Request);

            StringContent strCont = new StringContent(sendd, Encoding.UTF8,
            "application/json");

            var ret = new ReturnData<T>();

            var resp = await apiCall.PostAsync(url, strCont);
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ret.isSuccess = false;
                ret.Mesg = resp.ReasonPhrase;
                return ret;
            }
            var dret = await resp.Content.ReadAsStringAsync();
            var rd = JsonConvert.DeserializeObject<ReturnData<T>>(dret);
            rd.isSuccess = true;
            return rd;
        }

       
    }
    public class PutCall<T>
    {
        string LibApiUrl = "";
        public PutCall()
        {
            LibApiUrl = ConfigurationManager.AppSettings["libapilocal"];
        }
        public async Task<T> CallApi(string apiurl, NameValueCollection request)
        {
            var url = LibApiUrl + apiurl;// + request;
            for (int ixd = 0; ixd < request.Count; ixd++)
            {
                if (ixd == 0)
                    url += "?";
                url += request.Keys[ixd] + "=" + request[ixd];
                if (ixd < request.Count - 1)
                    url += "&";
            }
            var apiCall = new HttpClient();
            apiCall.BaseAddress = new Uri(url);
            var d = await apiCall.GetStringAsync(url);
            var rd = JsonConvert.DeserializeObject<T>(d);
            return rd;
        }
    }
}