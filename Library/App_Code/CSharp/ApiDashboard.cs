using LibData.Contract.Search;
using LibData.Model;
using Model.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Library.App_Code.Helper;

namespace Library.App_Code.CSharp
{
    public class ApiDashboard
    {
        private string LibApiUrl = "";
        private HttpClient apiCall = null;
        public string url { get; set; }
        private string baseUrl;
        public ApiDashboard()
        {
            LibApiUrl = ConfigurationManager.AppSettings["libapilocal"];
        }
        public async Task<ReturnData<List<vwebItems>>> SearchItems(AccnSearchCmd req)
        {
            var post = new ApiPostCall<AccnSearchCmd, List<vwebItems>>();
            var d = await post.CallApi(req, "Search/SearchItems");
            return d;
        }
    }

}