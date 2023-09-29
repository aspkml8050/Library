using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using Library.Models;
namespace Library.App_Code.CSharp
{
    public static class LoggedUser
    {
        public static Models.LoggedData Logged()
        {
            var authCookie = HttpContext.Current. Request.Cookies[FormsAuthentication.FormsCookieName];
            if ( authCookie==null)
            {
                return null;

            }
            Models.LoggedData ud=new Models.LoggedData();
            try
            {
                var x = FormsAuthentication.Decrypt(authCookie.Value);
                var x1=x.UserData.Split('|');
                foreach(var x2 in x1)
                {
                    if (x2.StartsWith("UserId"))
                        ud.UserId = x2.Split(':')[1];
                    if (x2.StartsWith("UserType"))
                        ud.UserType = x2.Split(':')[1];
                    if (x2.StartsWith("User_Id"))
                        ud.User_Id = x2.Split(':')[1];
                    if (x2.StartsWith("UserName"))
                        ud.UserName = x2.Split(':')[1];
                    if (x2.StartsWith("Session"))
                        ud.Session = x2.Split(':')[1];
                    if (x2.StartsWith("IsAudit"))
                        ud.IsAudit = x2.Split(':')[1];
                    if (x2.StartsWith("sessionStartDate"))
                        ud.sessionStartDate =Convert.ToDateTime( x2.Split(':')[1]);
                    if (x2.StartsWith("sessionEndDate"))
                        ud.sessionEndDate = Convert.ToDateTime(x2.Split(':')[1]);
                    if (x2.StartsWith("ipaddress"))
                        ud.ipaddrss = x2.Substring(x2.IndexOf(":") + 2);
                    if (x2.StartsWith("LoginTime"))
                    {
                        var time = x2.Substring(x2.IndexOf(":") + 1);
                        ud.LoginTime = DateTime.ParseExact(time, "dd-MMM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Logged Cookies not found");
            }
            return ud;  
        }
    }
}