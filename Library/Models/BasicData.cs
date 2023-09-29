
using AjaxControlToolkit;
using LibData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.Models
{
    public class BasicData
    {
        public SelectedData GetBasicData(ReqdData Reqd)
        {
            SelectedData data = new SelectedData();
            GlobClassTr clas=new GlobClassTr();
            clas.TrOpen();
            string sQer = "";
            if (Reqd.ItemType)
            {
                sQer = "select id,item_type from item_type order by id";
                var d = clas.DataT(sQer);
                var list= ExtConvert.ConvertTo<ItemType>(d);
                data.itemTypes = list;
            }
            if (Reqd.Categ)
            {
                sQer = "select id,category_loadingstatus from categoryloadingstatus order by id";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<CategoryLoadingStatus>(d);
                data.categories = list;
            }
            if (Reqd.ItemStatus)
            {
                sQer = "select itemstatusid,itemstatus from itemstatusmaster order by itemstatusid";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<ItemStatusMaster>(d);
                data.itemStatuses = list;
            }
            if (Reqd.Lang)
            {
                sQer = "select language_id,language_name from translation_language";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<Translation_Language>(d);
                data.Languages = list;
            }
            if (Reqd.Currency)
            {
                sQer = "select currencycode,currencyname from exchangemaster order by currencycode";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<exchangemaster>(d);
                data.Currencies = list;
            }
            if (Reqd.Media)
            {
                sQer = "select media_id,media_name from media_type ";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<media_type>(d);
                data.mediaTypes = list;
            }
            if (Reqd.Dept)
            {
                sQer = "select departmentcode,departmentname from departmentmaster";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<departmentmaster>(d);
                data.departments = list;
            }
            if (Reqd.Progs)
            {
                sQer = "select program_id,program_name,short_name,deptcode from  Program_Master";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<Program_Master>(d);
                data.programs = list;
            }
            if (Reqd.CastCateg)
            {
                sQer = "select cat_id,cat_name,shortname from CastCategories";
                var d = clas.DataT(sQer);
                var list = ExtConvert.ConvertTo<CastCategories>(d);
                data.castCategories = list;

            }

            clas.TrClose();
            return data;
        }

        public void GetDDLItemType(DropDownList ddl,List<ItemType> lsitem)
        {
            ddl.DataSource = lsitem;
            ddl.DataTextField = "Item_Type";
            ddl.DataValueField = "Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLItemStat(DropDownList ddl,List<ItemStatusMaster> lsitem)
        {
            ddl.DataSource = lsitem;
            ddl.DataTextField = "ItemStatus";
            ddl.DataValueField = "ItemStatusID";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLProg(DropDownList ddl, List<Program_Master> lsitem)
        {
            ddl.DataSource = lsitem;
            ddl.DataTextField = "program_name";
            ddl.DataValueField = "program_id";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLCateg(DropDownList ddl, List<CategoryLoadingStatus> lsitem)
        {
            
            ddl.DataSource = lsitem;
            ddl.DataTextField = "Category_LoadingStatus";
            ddl.DataValueField = "Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLLang(DropDownList ddl, List<Translation_Language> lsitem)
        {
            ddl.DataSource = lsitem;
            ddl.DataTextField = "Language_Name";
            ddl.DataValueField = "Language_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLExch(DropDownList ddl, List<exchangemaster> lsitem)
        {
            
            ddl.DataSource = lsitem;
            ddl.DataTextField = "CurrencyName";
            ddl.DataValueField = "CurrencyCode";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLDept(DropDownList ddl,List<departmentmaster> lsitem)
        {
            
            ddl.DataSource = lsitem;
            ddl.DataTextField = "departmentname";
            ddl.DataValueField = "departmentcode";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLMedia(DropDownList ddl,List<media_type> lsitem)
        {
            
            ddl.DataSource = lsitem;
            ddl.DataTextField = "media_name";
            ddl.DataValueField = "media_id";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }
        public void GetDDLForm(DropDownList ddl)
        {
            List<string> lsitem = new List<string>();
            lsitem.Add("Soft Bound");
            lsitem.Add("Hard Bound");
            ddl.DataSource = lsitem;
            ddl.DataTextField = "media_name";
            ddl.DataValueField = "media_id";
            ddl.DataBind();
            ddl.Items.Insert(0, "---Select---");
        }

    }
    public class SelectedData
    {
        public List<ItemType> itemTypes { get; set; }

        public List<CategoryLoadingStatus> categories { get; set; }
        public List<ItemStatusMaster> itemStatuses { get; set; }
        public List<Translation_Language> Languages { get; set; }
        public List<exchangemaster> Currencies { get; set; }
        public List<media_type> mediaTypes { get; set; }
        public List<departmentmaster> departments { get; set; }
        public List<Program_Master> programs { get; set; }
        public List<CastCategories> castCategories { get; set; }

    }
    public class ReqdData
    {
        public bool ItemType { get; set; } = false;
        public bool Categ { get; set; } = false;
        public bool ItemStatus { get; set; } = false;
        public bool Lang { get; set; } = false;
        public bool Currency { get; set; } = false;
        public bool Media { get; set; } = false;
        public bool Dept { get; set; } = false;
        public bool Progs { get; set; } = false;
        public bool CastCateg { get; set; } = false;
    }
    public class ItemType
    {
        public int Id { get; set; }
        public string Item_Type { get; set; }
        public string Abbreviation { get; set; }
        
        public string userid { get; set; }
    }
    public class CategoryLoadingStatus
    {
        public int Id { get; set; }
        public string Category_LoadingStatus { get; set; }
        public string Abbreviation { get; set; }
        public byte[] cat_icon { get; set; }
        public string userid { get; set; }
    }
    public class ItemStatusMaster
    {
        public long ItemStatusID { get; set; }
        public string ItemStatus { get; set; }
        public string ItemStatusShort { get; set; }
        public string isBardateApllicable { get; set; }
        public string isIsued { get; set; }
        public string userid { get; set; }

    }
    public class Translation_Language
    {
        public int Language_Id { get; set; }
        public string Language_Name { get; set; }
        public string Font_Name { get; set; }
        public string userid { get; set; }

    }
    public class exchangemaster
    {
        public int CurrencyCode { get; set; }
        public string ShortName { get; set; }
        public string CurrencyName { get; set; }
        public decimal GocRate { get; set; }
        public decimal BankRate { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public string userid { get; set; }
    }
    public class media_type
    {
        public int media_id { get; set; }
        public string media_name { get; set; }
        public string short_name { get; set; }
        public string userid { get; set; }
    }
 
    public class CastCategories
    {
        public int cat_id { get; set; }
        public string cat_name { get; set; }
        public string userid { get; set; }
        public string shortname { get; set; }
    }
    public class Program_Master
    {
        public int program_id { get; set; }
        public string program_name { get; set; }
        public string short_name { get; set; }
        public int deptcode { get; set; }
        public string userid { get; set; }
    }
    }