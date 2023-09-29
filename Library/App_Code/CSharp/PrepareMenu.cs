using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for PrepareMenu
/// </summary>
/// 
namespace Mdatas
{


    public class PrepareMenu
    {
        public PrepareMenu()
        {
        }
       public string UserTypeId { get; set; }
        public string MenuHtml { get
            {
                return prepMnu(getMdata(this.UserTypeId));
            } 
        }
        private List<mndata> getMdata(string usertypeid)
        {
            DataTable dtD = new DataTable();
            if (HttpContext.Current.Session["navmenudata"] != null)
            {
                dtD = (DataTable)HttpContext.Current.Session["navmenudata"];

            }
            else
            {
                GlobClassTr clas = new GlobClassTr();
                clas.TrOpen();
                string qer = "select a.Menu_id,a.Menu_name,a.ParentId,trim(isnull(a.Href,'')) href,a.OrdNo,isnull( a.helpref,'') helpref,isnull(a.status,'0') stat from Popup_new a   join menu_perm b on b.menuid=a.Menu_id and b.usertypeid= '" + usertypeid + "'";
                dtD = clas.DataT(qer);
                clas.TrClose();
                HttpContext.Current.Session["navmenudata"] = dtD;

            }
            List<mndata> ldata = new List<mndata>();

            for (int indX = 0; indX < dtD.Rows.Count; indX++)
            {
                mndata m = new mndata();
                m.menu_id = Convert.ToInt32(dtD.Rows[indX]["menu_id"]);
                m.menu_name = dtD.Rows[indX]["menu_name"].ToString();
                m.parentid = dtD.Rows[indX].IsNull("parentid") ? "" : dtD.Rows[indX]["parentid"].ToString();
                m.href = dtD.Rows[indX]["href"].ToString();
                m.ordno = Convert.ToInt32(dtD.Rows[indX]["ordno"]);
                m.helpref = dtD.Rows[indX]["helpref"].ToString();
                m.status = dtD.Rows[indX]["stat"].ToString();
                ldata.Add(m);
            }

            return ldata;

        }
        private string prepMnu(List<mndata> lsd)
        {
            string pare = "<ul>";
            var lsd2 = lsd.Where(b => b.parentid.Equals("")).ToList().OrderBy(a => a.ordno).ToList();
            for (int indX1 = 0; indX1 < lsd2.Count; indX1++)
            {
                string href = lsd2[indX1].href;
                int pid = lsd2[indX1].menu_id;
                string title = lsd2[indX1].menu_name;
                string parentid = lsd2[indX1].parentid.ToString();
                if (string.IsNullOrEmpty(href))
                {
                    pare += "<li><a href=\"#\"  aria-expanded=\"false\"  data-id='" + pid.ToString() + "' data-parentid='" + parentid + "' >" + lsd2[indX1].menu_name.Replace("'", "''") + "</a>";
                }
                else
                {
                    pare += "<li><a data-id='"+pid.ToString()+"' data-parentid='"+parentid+"' onclick='StorPg(this)' href='" + href + "?title=" + title+"&condition=y'>" + lsd2[indX1].menu_name.Replace("'", "''") + "</a>";

                }
                retchild(lsd, pid, ref pare);
                pare += "</li>";
            }
            pare += "</ul>";
            return pare;
        }
        private void retchild(List<mndata> olsD, int pareint, ref string rdata)
        {
            var lsd2 = olsD.Where(a => a.parentid.Equals(pareint.ToString())).OrderBy(b => b.ordno).ToList();
            if (lsd2.Count > 0)
            {
                rdata += "<ul>";
                for (int indX = 0; indX < lsd2.Count; indX++)
                {
                    string href = lsd2[indX].href;
                    string title = lsd2[indX].menu_name;
                    int id = lsd2[indX].menu_id;
                    string parentid = lsd2[indX].parentid;
                    if (string.IsNullOrEmpty(href))
                    {
                        rdata += "<li><a href=\"#\" data-id='" + id.ToString() + "' data-parentid='" + parentid + "'  >" + title + "</a>";
                        retchild(olsD, id, ref rdata);
                        rdata += "</li>";
                    }
                    else
                    {
                        rdata += "<li><a data-id='"+id.ToString()+"' data-parentid='"+parentid+ "'  onclick='StorPg(this)'  href='" + href + "?title=" + title + "&condition=Y'>" + title + "</a></li>";
                    }
                }

                rdata += "</ul>";
            }


            //        rdata += "</ul>";
            //  return rdata;
        }
        private class mndata
        {
            public int menu_id { get; set; }
            public string menu_name { get; set; }
            public string parentid { get; set; }
            public string href { get; set; }
            public int ordno { get; set; }
            public string helpref { get; set; }
            public string status { get; set; }



        }
    }

    public class PrepareMenu2 //for new library 
    {
        public PrepareMenu2()
        {
        }
        public string UserTypeId { get; set; }
        public string MenuHtml
        {
            get
            {
                return prepMnu(getMdata(this.UserTypeId));
            }
        }
        private List<mndata> getMdata(string usertypeid)
        {
            DataTable dtD = new DataTable();
            if (HttpContext.Current.Session["navmenudata"] != null)
            {
                dtD = (DataTable)HttpContext.Current.Session["navmenudata"];

            }
            else
            {
                GlobClassTr clas = new GlobClassTr();
                clas.TrOpen();
                string qer = "select a.Menu_id,a.Menu_name,a.ParentId,trim(isnull(a.Href,'')) href,a.OrdNo,isnull( a.helpref,'') helpref,isnull(a.status,'0') stat from Popup_new a   join menu_perm b on b.menuid=a.Menu_id and b.usertypeid= '" + usertypeid + "'";
                dtD = clas.DataT(qer);
                clas.TrClose();
                HttpContext.Current.Session["navmenudata"] = dtD;

            }
            List<mndata> ldata = new List<mndata>();

            for (int indX = 0; indX < dtD.Rows.Count; indX++)
            {
                mndata m = new mndata();
                m.menu_id = Convert.ToInt32(dtD.Rows[indX]["menu_id"]);
                m.menu_name = dtD.Rows[indX]["menu_name"].ToString();
                m.parentid = dtD.Rows[indX].IsNull("parentid") ? "" : dtD.Rows[indX]["parentid"].ToString();
                m.href = dtD.Rows[indX]["href"].ToString();
                m.ordno = Convert.ToInt32(dtD.Rows[indX]["ordno"]);
                m.helpref = dtD.Rows[indX]["helpref"].ToString();
                m.status = dtD.Rows[indX]["stat"].ToString();
                ldata.Add(m);
            }

            return ldata;

        }
        private string prepMnu(List<mndata> lsd)
        {
            string pare = "<ul id=\"menu\">";
            var lsd2 = lsd.Where(b => b.parentid.Equals("")).ToList().OrderBy(a => a.ordno).ToList();
            for (int indX1 = 0; indX1 < lsd2.Count; indX1++)
            {
                string href = lsd2[indX1].href;
                int pid = lsd2[indX1].menu_id;
                string title = lsd2[indX1].menu_name;
                string parentid = lsd2[indX1].parentid.ToString();
                if (string.IsNullOrEmpty(href))
                {
                    pare += "<li><a href=\"#\" class=\"has-arrow\"  aria-expanded=\"false\"   data-id='" + pid.ToString() + "' data-parentid='" + parentid + "' >" + lsd2[indX1].menu_name.Replace("'", "''") + "</a>";
                }
                else
                {
                    pare += "<li><a data-id='" + pid.ToString() + "' data-parentid='" + parentid + "' onclick='StorPg(this)' href='" + href + "?title=" + title + "&condition=y'>" + lsd2[indX1].menu_name.Replace("'", "''") + "</a>";

                }
                retchild(lsd, pid, ref pare);
                pare += "</li>";
            }
            pare += "</ul>";
            return pare;
        }
        private void retchild(List<mndata> olsD, int pareint, ref string rdata)
        {
            var lsd2 = olsD.Where(a => a.parentid.Equals(pareint.ToString())).OrderBy(b => b.ordno).ToList();
            if (lsd2.Count > 0)
            {
                rdata += "<ul >";
                for (int indX = 0; indX < lsd2.Count; indX++)
                {
                    string href = lsd2[indX].href;
                    string title = lsd2[indX].menu_name;
                    int id = lsd2[indX].menu_id;
                    string parentid = lsd2[indX].parentid;
                    if (string.IsNullOrEmpty(href))
                    {
                        rdata += "<li><a href=\"#\" class=\"has-arrow\"  aria-expanded=\"false\"  data-id='" + id.ToString() + "' data-parentid='" + parentid + "'  >" + title + "</a>";
                        retchild(olsD, id, ref rdata);
                        rdata += "</li>";
                    }
                    else
                    {
                        rdata += "<li><a data-id='" + id.ToString() + "' data-parentid='" + parentid + "'  onclick='StorPg(this)'  href='" + href + "?title=" + title + "&condition=Y'>" + title + "</a></li>";
                    }
                }

                rdata += "</ul>";
            }


            //        rdata += "</ul>";
            //  return rdata;
        }
        private class mndata
        {
            public int menu_id { get; set; }
            public string menu_name { get; set; }
            public string parentid { get; set; }
            public string href { get; set; }
            public int ordno { get; set; }
            public string helpref { get; set; }
            public string status { get; set; }



        }
    }
}