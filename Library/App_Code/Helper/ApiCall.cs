using Model.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.App_Code.Helper
{
    public class ApiGetCall<T>
    {
        string LibApiUrl = "";
        public ApiGetCall()
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


    public class ApiPostCall<S, T>
    {
        string LibApiUrl = "";
        public ApiPostCall()
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

}