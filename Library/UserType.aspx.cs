using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class UserType : BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();
        GlobClassTr gclas = new GlobClassTr();
        ApiComm apiCall = new ApiComm();
        protected  async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              await  showd();
            }
        }
        private async Task showd()
        {
            /*
            gclas.TrOpen();
            string qer = "select usertypeid,UserTypeName from UserTypePermissions order by UserTypeName";
            DataTable dtut = gclas.DataT(qer);
            gclas.TrClose();
            */
            var d = await apiCall.GetUserTypeAll();
            dlUtype.DataSource = d.Data;
            dlUtype.DataBind();
        }
        protected void lnktype_Click(object sender, EventArgs e)
        {

        }

        protected void lnktypeDl_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            DataListItem dli = (DataListItem)btn.NamingContainer;
            HiddenField h =(HiddenField) dli.FindControl("hdid");
            txutype.Text = btn.Text;
            hdid.Value=h.Value;
            btnDel.Enabled = true;
            btnSave.Text = "Update";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txutype.Text.Trim() == "")
            {
                message.PageMesg("Enter User Type", this, dbUtilities.MsgLevel.Warning);
                return;
            }
            GlobClassTr gclas = new GlobClassTr();
            try
            {
                gclas.TrOpen();
                if (hdid.Value == "")
                {
                    string qer = "select isnull(max( usertypeid),0)+1 from UserTypePermissions  ";
                    var id = gclas.ExScaler(qer);
                    qer = "select count(*) from  UserTypePermissions where UserTypeName='"+txutype.Text.Trim()+"'";
                    var exi = Convert.ToInt32( gclas.ExScaler(qer));
                    if (exi > 0)
                    {
                        throw new ApplicationException("User type already exists.");
                    }
                    qer = "insert into UserTypePermissions ( usertypeid,UserTypeName) values ("+id+", '" + txutype.Text.Trim() + "')";
                    gclas.IUD(qer);

                }
                else
                {
                   string qer = "select count(*) from  UserTypePermissions where usertypeid<>"+hdid.Value+" and UserTypeName='" + txutype.Text.Trim() + "'";
                    var exi = Convert.ToInt32(gclas.ExScaler(qer));
                    if (exi > 0)
                    {
                        throw new ApplicationException("User type already exists.");
                    }
                    qer= " update UserTypePermissions set UserTypeName='"+txutype.Text+ "' where usertypeid="+ hdid.Value;
                    gclas.IUD(qer);
                }
                gclas.TrClose();
                btnRes_Click(sender, e);
                showd();
                message.PageMesg("User type saved.", this);
            }
            catch (Exception exp)
            {
                gclas.TrRollBack();
                message.PageMesg(exp.Message, this, dbUtilities.MsgLevel.Failure);

            }
        }

        protected void btnRes_Click(object sender, EventArgs e)
        {
            txutype.Text = "";
            hdid.Value = "";
            btnSave.Text = "Save";
            btnDel.Enabled= false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string qer = "select count(*) from menu_perm where usertypeid= " + hdid.Value;
            GlobClassTr gclas = new GlobClassTr();
            try
            {
                gclas.TrOpen();
                var cn = Convert.ToInt32( gclas.ExScaler(qer));
                if (cn > 1 )
                {
                    throw new ApplicationException("User type in use cannot be deleted");
                }
                qer= " delete from UserTypePermissions where usertypeid= " + hdid.Value;
                gclas.IUD(qer);
                gclas.TrClose();
               btnRes_Click(sender, e);
                showd();
                message.PageMesg("User type deleted.", this);
            }catch (Exception exp)
            {

                gclas.TrRollBack();
                message.PageMesg(exp.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
    }
}