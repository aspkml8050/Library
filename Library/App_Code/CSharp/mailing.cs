using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for mailing
/// </summary>
/// 
namespace Library.App_Code.CSharp
{

    public class mailing
    {
        public mailing()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string _userID;
        private string _mailF;
        private string _mailT1;
        private string _mailT2;
        private string _smtpAdd;
        private string _uid;
        private string _pwd;
        private string _ismailAllowed;
        private string _isemAllow;

        public string UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
        public string Pwd
        {
            get
            {
                return _pwd;
            }
            set
            {
                _pwd = value;
            }
        }
        public string MailF
        {
            get
            {
                return _mailF;
            }
            set
            {
                _mailF = value;
            }
        }
        public string MailT1
        {
            get
            {
                return _mailT1;
            }
            set
            {
                _mailT1 = value;
            }
        }
        public string MailT2
        {
            get
            {
                return _mailT2;
            }
            set
            {
                _mailT2 = value;
            }
        }
        public string SmtpAdd
        {
            get
            {
                return _smtpAdd;
            }
            set
            {
                _smtpAdd = value;
            }
        }
        public string Uid
        {
            get
            {
                return _uid;
            }
            set
            {
                _uid = value;
            }
        }
        public string IsmailAllowed
        {
            get
            {
                return _ismailAllowed;
            }
            set
            {
                _ismailAllowed = value;
            }
        }
        public string IsemAllow
        {
            get
            {
                return _isemAllow;
            }
            set
            {
                _isemAllow = value;
            }
        }
    }

}
