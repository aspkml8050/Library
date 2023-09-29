using Library.App_Code.CSharp;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["__EVENTARGUMENT"] == "logout")

            {
                Session["navmenudata"] = null;
                FormsAuthentication.SignOut();
                Response.Redirect("~/default.aspx");

            }
            else
            {
                getPgs();
            }
        }

        private void getPgs()
        {
            var logged = LoggedUser.Logged();
            if (logged == null)
                return;
            hdUser.Value = logged.UserName;
            GlobClassTr clas =new GlobClassTr();
            clas.TrOpen();
            string qer = "  select pgid,link,title,OnDate from LogPages("+logged.UserId+" ) ";
            DataTable dtusr = clas.DataT(qer);
            clas.TrClose();
            grdvispgs.DataSource= dtusr;
            grdvispgs.DataBind();
        }

protected void btno_Click(object sender, EventArgs e)
        {

        }

        protected void lnkpgs_Click(object sender, EventArgs e)
        {
            LinkButton lnk=(LinkButton)sender;
            GridViewRow r = (GridViewRow)lnk.NamingContainer;
            var hd =(HiddenField) r.FindControl("hdlink");
            var hdpgid = (HiddenField)r.FindControl("hdpgid");
            var ID = "Whatever the data is";
           Page. ClientScript.RegisterStartupScript(this.GetType(), "script", "CalledPagesLM('" + hd.Value +"|"+hdpgid.Value+ "');", true);
            //Response.Redirect(hd.Value);
        }
    }
}