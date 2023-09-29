using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;

namespace Library.App_Code.CSharp
{
    public class ClassFileCatelog
    {
        public bool InsertFunction( int ctrl_no, System.DateTime catalogdate, int BookType, string volumenumber, 
            string initpages, int pages, string parts, int leaves, string boundind, string title, int publishercode,
            string edition, string isbn, string subject1, string subject2, string subject3, string Booksize, 
            string LCCN, string Volumepages, string biblioPages, string bookindex, string illustration,
            string variouspaging, int maps, string ETalEditor, string ETalCompiler, string ETalIllus, string ETalTrans, 
            string ETalAuthor, string accmaterialhistory, string MaterialDesignation, string issn, string Volume, 
            int dept, int language_id, string part, string eBookURL, string cat_Source, string Identifier, 
            string firstname, string percity, string perstate, string percountry, string peraddress, string departmentname, 
            string Btype, string language_name, string ItemCategory, int transno,string controlNo ,
            OleDbCommand accmastercom)
        {
            bool ReturnFlag = true;
            accmastercom.CommandType = CommandType.StoredProcedure;
            accmastercom.CommandText = "insert_BookCatalog_1";

            accmastercom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
            accmastercom.Parameters["@ctrl_no_1"].Value = ctrl_no;
            accmastercom.Parameters.Add(new OleDbParameter("@catalogdate_2 ", OleDbType.Date));
            accmastercom.Parameters["@catalogdate_2 "].Value = catalogdate; // Date.Now.Date

            accmastercom.Parameters.Add(new OleDbParameter("@booktype_3", OleDbType.Integer));
            accmastercom.Parameters["@booktype_3"].Value = BookType;

            accmastercom.Parameters.Add(new OleDbParameter("@volumenumber_4", OleDbType.VarWChar));
            accmastercom.Parameters["@volumenumber_4"].Value = volumenumber; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@initpages_5", OleDbType.VarWChar));
            accmastercom.Parameters["@initpages_5"].Value = initpages; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@pages_6", OleDbType.Integer));
            accmastercom.Parameters["@pages_6"].Value = pages; // 0

            accmastercom.Parameters.Add(new OleDbParameter("@parts_7", OleDbType.VarWChar));
            accmastercom.Parameters["@parts_7"].Value = parts; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@leaves_8", OleDbType.VarWChar));
            accmastercom.Parameters["@leaves_8"].Value = leaves; // 0

            accmastercom.Parameters.Add(new OleDbParameter("@boundind_9", OleDbType.VarWChar));
            accmastercom.Parameters["@boundind_9"].Value = boundind; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@title_10", OleDbType.VarWChar));
            accmastercom.Parameters["@title_10"].Value = title; // lbltitl.Text

            accmastercom.Parameters.Add(new OleDbParameter("@publishercode_11", OleDbType.Integer));
            accmastercom.Parameters["@publishercode_11"].Value = publishercode; // Ads.Tables(0).Rows(0).Item("publisherid")

            accmastercom.Parameters.Add(new OleDbParameter("@edition_12", OleDbType.VarWChar));
            accmastercom.Parameters["@edition_12"].Value = edition; // IIf(Trim(Ads.Tables(0).Rows(0).Item("edition")) = String.Empty, String.Empty, Trim(Ads.Tables(0).Rows(0).Item("edition")))

            accmastercom.Parameters.Add(new OleDbParameter("@isbn_13", OleDbType.VarWChar));
            accmastercom.Parameters["@isbn_13"].Value = isbn; // IIf(Trim(Ads.Tables(0).Rows(0).Item("isbn")) = String.Empty, String.Empty, Trim(Ads.Tables(0).Rows(0).Item("isbn")))

            accmastercom.Parameters.Add(new OleDbParameter("@subject1_14", OleDbType.VarWChar));
            accmastercom.Parameters["@subject1_14"].Value = subject1; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@subject2_15", OleDbType.VarWChar));
            accmastercom.Parameters["@subject2_15"].Value = subject2; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@subject3_16", OleDbType.VarWChar));
            accmastercom.Parameters["@subject3_16"].Value = subject3; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@Booksize_17", OleDbType.VarWChar));
            accmastercom.Parameters["@Booksize_17"].Value = Booksize; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@LCCN_18", OleDbType.VarWChar));
            accmastercom.Parameters["@LCCN_18"].Value = string.Empty;

            accmastercom.Parameters.Add(new OleDbParameter("@Volumepages_19", OleDbType.VarWChar));
            accmastercom.Parameters["@Volumepages_19"].Value = Volumepages; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@biblioPages_20", OleDbType.VarWChar));
            accmastercom.Parameters["@biblioPages_20"].Value = biblioPages; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@bookindex_21", OleDbType.VarWChar));
            accmastercom.Parameters["@bookindex_21"].Value = bookindex; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@illustration_22", OleDbType.VarWChar));
            accmastercom.Parameters["@illustration_22"].Value = illustration; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@variouspaging_23", OleDbType.VarWChar));
            accmastercom.Parameters["@variouspaging_23"].Value = variouspaging; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@maps_24", OleDbType.Integer));
            accmastercom.Parameters["@maps_24"].Value = maps; // 0

            accmastercom.Parameters.Add(new OleDbParameter("@ETalEditor_25", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalEditor_25"].Value = ETalEditor; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@ETalCompiler_26", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalCompiler_26"].Value = ETalCompiler; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@ETalIllus_27", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalIllus_27"].Value = ETalIllus; // String.EmptyETalIllus

            accmastercom.Parameters.Add(new OleDbParameter("@ETalTrans_28", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalTrans_28"].Value = ETalTrans; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@ETalAuthor_29", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalAuthor_29"].Value = ETalAuthor; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@accmaterialhistory_31", OleDbType.VarWChar));
            accmastercom.Parameters["@accmaterialhistory_31"].Value = accmaterialhistory; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@MaterialDesignation_32", OleDbType.VarWChar));
            accmastercom.Parameters["@MaterialDesignation_32"].Value = MaterialDesignation; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@issn_33", OleDbType.VarWChar));
            accmastercom.Parameters["@issn_33"].Value = issn; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@Volume_34", OleDbType.VarWChar));
            accmastercom.Parameters["@Volume_34"].Value = Volume; // Ads.Tables(0).Rows(0).Item("volumeno")

            accmastercom.Parameters.Add(new OleDbParameter("@dept_35", OleDbType.Integer));
            accmastercom.Parameters["@dept_35"].Value = dept; // Ads.Tables(0).Rows(0).Item("departmentcode")

            accmastercom.Parameters.Add(new OleDbParameter("@language_id_36", OleDbType.Integer));
            accmastercom.Parameters["@language_id_36"].Value = language_id; // Ads.Tables(0).Rows(0).Item("Language_Id")

            accmastercom.Parameters.Add(new OleDbParameter("@part_37", OleDbType.VarWChar));
            accmastercom.Parameters["@part_37"].Value = part; // IIf(Val(Trim(Ads.Tables(0).Rows(0).Item("Vpart"))) = 0, String.Empty, Trim(Ads.Tables(0).Rows(0).Item("Vpart")))

            accmastercom.Parameters.Add(new OleDbParameter("@eBookURL_38", OleDbType.VarWChar));
            accmastercom.Parameters["@eBookURL_38"].Value = eBookURL; // "" ' IIf(Trim(Me.updURL.PostedFile.FileName) = String.Empty, String.Empty, Trim(updURL.PostedFile.FileName))

            accmastercom.Parameters.Add(new OleDbParameter("@cat_Source_39", OleDbType.VarWChar));
            accmastercom.Parameters["@cat_Source_39"].Value = cat_Source; // cmbvendor.SelectedItem.Text 'Ads.Tables(0).Rows(0).Item("vendorid")

            accmastercom.Parameters.Add(new OleDbParameter("@Identifier_40", OleDbType.VarWChar));
            accmastercom.Parameters["@Identifier_40"].Value = Identifier;

            accmastercom.Parameters.Add(new OleDbParameter("@firstname_41", OleDbType.VarWChar));
            accmastercom.Parameters["@firstname_41"].Value = firstname; // Trim(Me.hdFirst.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@percity_42", OleDbType.VarWChar));
            accmastercom.Parameters["@percity_42"].Value = percity; // Trim(Me.hdcity.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@perstate_43", OleDbType.VarWChar));
            accmastercom.Parameters["@perstate_43"].Value = perstate; // Trim(Me.hdState.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@percountry_44", OleDbType.VarWChar));
            accmastercom.Parameters["@percountry_44"].Value = percountry; // Trim(Me.hdCountry.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@peraddress_45", OleDbType.VarWChar));
            accmastercom.Parameters["@peraddress_45"].Value = peraddress; // Trim(Me.hdAddress.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@departmentname_46", OleDbType.VarWChar));
            accmastercom.Parameters["@departmentname_46"].Value = departmentname; // Me.hddepartment.Value

            accmastercom.Parameters.Add(new OleDbParameter("@Btype_47", OleDbType.VarWChar));
            accmastercom.Parameters["@Btype_47"].Value = Btype; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@language_name_48", OleDbType.VarWChar));
            accmastercom.Parameters["@language_name_48"].Value = language_name; // Me.hdLanguage.Value

            accmastercom.Parameters.Add(new OleDbParameter("@ItemCategory_49", OleDbType.VarWChar));
            accmastercom.Parameters["@ItemCategory_49"].Value = ItemCategory; // "Books"
            accmastercom.Parameters.Add(new OleDbParameter("@TransNo", OleDbType.Integer));
            accmastercom.Parameters["@Transno"].Value = transno; // 
            accmastercom.Parameters.Add(new OleDbParameter("@ControlNo", OleDbType.VarChar));
            accmastercom.Parameters["@ControlNo"].Value = controlNo; // 
            try
            {
                accmastercom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ReturnFlag = false;
                string p;
                p = ex.Message;
            }
            return ReturnFlag;
        }
        public string InsertFunctionStr(int ctrl_no, System.DateTime catalogdate, int BookType, string volumenumber,
          string initpages, int pages, string parts, int leaves, string boundind, string title, int publishercode,
          string edition, string isbn, string subject1, string subject2, string subject3, string Booksize,
          string LCCN, string Volumepages, string biblioPages, string bookindex, string illustration,
          string variouspaging, int maps, string ETalEditor, string ETalCompiler, string ETalIllus, string ETalTrans,
          string ETalAuthor, string accmaterialhistory, string MaterialDesignation, string issn, string Volume,
          int dept, int language_id, string part, string eBookURL, string cat_Source, string Identifier,
          string firstname, string percity, string perstate, string percountry, string peraddress, string departmentname,
          string Btype, string language_name, string ItemCategory, int transno, string controlNo,
          OleDbCommand accmastercom)
        {
            string ReturnFlag = "";
            accmastercom.CommandType = CommandType.StoredProcedure;
            accmastercom.CommandText = "insert_BookCatalog_1";

            accmastercom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
            accmastercom.Parameters["@ctrl_no_1"].Value = ctrl_no;
            accmastercom.Parameters.Add(new OleDbParameter("@catalogdate_2 ", OleDbType.Date));
            accmastercom.Parameters["@catalogdate_2 "].Value = catalogdate; // Date.Now.Date

            accmastercom.Parameters.Add(new OleDbParameter("@booktype_3", OleDbType.Integer));
            accmastercom.Parameters["@booktype_3"].Value = BookType;

            accmastercom.Parameters.Add(new OleDbParameter("@volumenumber_4", OleDbType.VarWChar));
            accmastercom.Parameters["@volumenumber_4"].Value = volumenumber; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@initpages_5", OleDbType.VarWChar));
            accmastercom.Parameters["@initpages_5"].Value = initpages; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@pages_6", OleDbType.Integer));
            accmastercom.Parameters["@pages_6"].Value = pages; // 0

            accmastercom.Parameters.Add(new OleDbParameter("@parts_7", OleDbType.VarWChar));
            accmastercom.Parameters["@parts_7"].Value = parts; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@leaves_8", OleDbType.VarWChar));
            accmastercom.Parameters["@leaves_8"].Value = leaves; // 0

            accmastercom.Parameters.Add(new OleDbParameter("@boundind_9", OleDbType.VarWChar));
            accmastercom.Parameters["@boundind_9"].Value = boundind; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@title_10", OleDbType.VarWChar));
            accmastercom.Parameters["@title_10"].Value = title; // lbltitl.Text

            accmastercom.Parameters.Add(new OleDbParameter("@publishercode_11", OleDbType.Integer));
            accmastercom.Parameters["@publishercode_11"].Value = publishercode; // Ads.Tables(0).Rows(0).Item("publisherid")

            accmastercom.Parameters.Add(new OleDbParameter("@edition_12", OleDbType.VarWChar));
            accmastercom.Parameters["@edition_12"].Value = edition; // IIf(Trim(Ads.Tables(0).Rows(0).Item("edition")) = String.Empty, String.Empty, Trim(Ads.Tables(0).Rows(0).Item("edition")))

            accmastercom.Parameters.Add(new OleDbParameter("@isbn_13", OleDbType.VarWChar));
            accmastercom.Parameters["@isbn_13"].Value = isbn; // IIf(Trim(Ads.Tables(0).Rows(0).Item("isbn")) = String.Empty, String.Empty, Trim(Ads.Tables(0).Rows(0).Item("isbn")))

            accmastercom.Parameters.Add(new OleDbParameter("@subject1_14", OleDbType.VarWChar));
            accmastercom.Parameters["@subject1_14"].Value = subject1; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@subject2_15", OleDbType.VarWChar));
            accmastercom.Parameters["@subject2_15"].Value = subject2; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@subject3_16", OleDbType.VarWChar));
            accmastercom.Parameters["@subject3_16"].Value = subject3; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@Booksize_17", OleDbType.VarWChar));
            accmastercom.Parameters["@Booksize_17"].Value = Booksize; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@LCCN_18", OleDbType.VarWChar));
            accmastercom.Parameters["@LCCN_18"].Value = string.Empty;

            accmastercom.Parameters.Add(new OleDbParameter("@Volumepages_19", OleDbType.VarWChar));
            accmastercom.Parameters["@Volumepages_19"].Value = Volumepages; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@biblioPages_20", OleDbType.VarWChar));
            accmastercom.Parameters["@biblioPages_20"].Value = biblioPages; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@bookindex_21", OleDbType.VarWChar));
            accmastercom.Parameters["@bookindex_21"].Value = bookindex; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@illustration_22", OleDbType.VarWChar));
            accmastercom.Parameters["@illustration_22"].Value = illustration; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@variouspaging_23", OleDbType.VarWChar));
            accmastercom.Parameters["@variouspaging_23"].Value = variouspaging; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@maps_24", OleDbType.Integer));
            accmastercom.Parameters["@maps_24"].Value = maps; // 0

            accmastercom.Parameters.Add(new OleDbParameter("@ETalEditor_25", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalEditor_25"].Value = ETalEditor; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@ETalCompiler_26", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalCompiler_26"].Value = ETalCompiler; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@ETalIllus_27", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalIllus_27"].Value = ETalIllus; // String.EmptyETalIllus

            accmastercom.Parameters.Add(new OleDbParameter("@ETalTrans_28", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalTrans_28"].Value = ETalTrans; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@ETalAuthor_29", OleDbType.VarWChar));
            accmastercom.Parameters["@ETalAuthor_29"].Value = ETalAuthor; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@accmaterialhistory_31", OleDbType.VarWChar));
            accmastercom.Parameters["@accmaterialhistory_31"].Value = accmaterialhistory; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@MaterialDesignation_32", OleDbType.VarWChar));
            accmastercom.Parameters["@MaterialDesignation_32"].Value = MaterialDesignation; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@issn_33", OleDbType.VarWChar));
            accmastercom.Parameters["@issn_33"].Value = issn; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@Volume_34", OleDbType.VarWChar));
            accmastercom.Parameters["@Volume_34"].Value = Volume; // Ads.Tables(0).Rows(0).Item("volumeno")

            accmastercom.Parameters.Add(new OleDbParameter("@dept_35", OleDbType.Integer));
            accmastercom.Parameters["@dept_35"].Value = dept; // Ads.Tables(0).Rows(0).Item("departmentcode")

            accmastercom.Parameters.Add(new OleDbParameter("@language_id_36", OleDbType.Integer));
            accmastercom.Parameters["@language_id_36"].Value = language_id; // Ads.Tables(0).Rows(0).Item("Language_Id")

            accmastercom.Parameters.Add(new OleDbParameter("@part_37", OleDbType.VarWChar));
            accmastercom.Parameters["@part_37"].Value = part; // IIf(Val(Trim(Ads.Tables(0).Rows(0).Item("Vpart"))) = 0, String.Empty, Trim(Ads.Tables(0).Rows(0).Item("Vpart")))

            accmastercom.Parameters.Add(new OleDbParameter("@eBookURL_38", OleDbType.VarWChar));
            accmastercom.Parameters["@eBookURL_38"].Value = eBookURL; // "" ' IIf(Trim(Me.updURL.PostedFile.FileName) = String.Empty, String.Empty, Trim(updURL.PostedFile.FileName))

            accmastercom.Parameters.Add(new OleDbParameter("@cat_Source_39", OleDbType.VarWChar));
            accmastercom.Parameters["@cat_Source_39"].Value = cat_Source; // cmbvendor.SelectedItem.Text 'Ads.Tables(0).Rows(0).Item("vendorid")

            accmastercom.Parameters.Add(new OleDbParameter("@Identifier_40", OleDbType.VarWChar));
            accmastercom.Parameters["@Identifier_40"].Value = Identifier;

            accmastercom.Parameters.Add(new OleDbParameter("@firstname_41", OleDbType.VarWChar));
            accmastercom.Parameters["@firstname_41"].Value = firstname; // Trim(Me.hdFirst.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@percity_42", OleDbType.VarWChar));
            accmastercom.Parameters["@percity_42"].Value = percity; // Trim(Me.hdcity.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@perstate_43", OleDbType.VarWChar));
            accmastercom.Parameters["@perstate_43"].Value = perstate; // Trim(Me.hdState.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@percountry_44", OleDbType.VarWChar));
            accmastercom.Parameters["@percountry_44"].Value = percountry; // Trim(Me.hdCountry.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@peraddress_45", OleDbType.VarWChar));
            accmastercom.Parameters["@peraddress_45"].Value = peraddress; // Trim(Me.hdAddress.Value)

            accmastercom.Parameters.Add(new OleDbParameter("@departmentname_46", OleDbType.VarWChar));
            accmastercom.Parameters["@departmentname_46"].Value = departmentname; // Me.hddepartment.Value

            accmastercom.Parameters.Add(new OleDbParameter("@Btype_47", OleDbType.VarWChar));
            accmastercom.Parameters["@Btype_47"].Value = Btype; // String.Empty

            accmastercom.Parameters.Add(new OleDbParameter("@language_name_48", OleDbType.VarWChar));
            accmastercom.Parameters["@language_name_48"].Value = language_name; // Me.hdLanguage.Value

            accmastercom.Parameters.Add(new OleDbParameter("@ItemCategory_49", OleDbType.VarWChar));
            accmastercom.Parameters["@ItemCategory_49"].Value = ItemCategory; // "Books"
            accmastercom.Parameters.Add(new OleDbParameter("@TransNo", OleDbType.Integer));
            accmastercom.Parameters["@Transno"].Value = transno; // 
            accmastercom.Parameters.Add(new OleDbParameter("@ControlNo", OleDbType.VarChar));
            accmastercom.Parameters["@ControlNo"].Value = controlNo; // 

            try
            {
                accmastercom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ReturnFlag = "Catalog ins/upd failed,"+ex.Message;
                string p;
                p = ex.Message;
            }
            return ReturnFlag;
        }

    }
}