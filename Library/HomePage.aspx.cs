using LibData.Contract.Search;
using Library.App_Code.CSharp;
using Library.App_Code.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * 
  
          		 select 
			
      '<asp:Label ID="tx'+
	   c.name+ '" runat="server" Text='+char(39)+'<%# Eval("'+c.name+'")%>'+char(39)+'></asp:Label>'+char(13),
	   c.name +
' {get;set;} '  ,
       type_name(user_type_id) as data_type,
       c.max_length,
       c.precision
from sys.columns c
join sys.views v 
     on v.object_id = c.object_id and
	 v.name='vwebItems'
order by 
         column_id;
 
  
  

 * */

namespace Library
{
    public partial class HomePage : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                txPageSize.Text = txPageSize.Text==""? "250":txPageSize.Text;
                txNoofSet.Text = txNoofSet.Text==""? "1" :txNoofSet.Text;
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            try
            {
                var x = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected async void btnShowItems_Click(object sender, EventArgs e)
        {
            ApiDashboard dashcall=new ApiDashboard();
            AccnSearchCmd cmd= new AccnSearchCmd();
            cmd.Vendor = txVendor.Text.Trim();
            cmd.Title=txTitle.Text.Trim();
            cmd.Author=txAuthor.Text.Trim();
            cmd.PageSize = Convert.ToInt32( txPageSize.Text);
            cmd.PageNo=Convert.ToInt32(txNoofSet.Text);
            var rv = await dashcall.SearchItems(cmd);
            if (rv.isSuccess)
            {
                grdSearchItems.DataSource = rv.Data;
                grdSearchItems.DataBind();
            }

        }
    }
}