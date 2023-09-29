using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;

namespace Library.App_Code.CSharp
{
    public class ClassFileBookAccession
    {
//        private LibraryRestriction LibRescObj = new LibraryRestriction();
        private static int TotalCount;

        public bool InsertBookAccession(string accessionnumber, string ordernumber, string indentnumber, string form, int accessionid, System.DateTime accessioneddate, string booktitle, int srno, string relStatus, decimal bookprice, int srNoOld, string biilNo, string billDate, string Item_type, double OriginalPrice, string OriginalCurrency, string userid, string vendor_source, int DeptCode, int DSrno, string DeptName, OleDbCommand accmastercom)
        {
            if (CountAccession(accmastercom))
            {
                // By kaushal 
                // ---------------------------------------------
                bool RetFlag = true;

                accmastercom.CommandType = CommandType.StoredProcedure;
                accmastercom.CommandText = "insert_bookaccessionmaster_1";
                accmastercom.Parameters.Add(new OleDbParameter("@accessionnumber_1", OleDbType.VarWChar));
                accmastercom.Parameters["@accessionnumber_1"].Value =accessionnumber.ToUpper(); // accno

                accmastercom.Parameters.Add(new OleDbParameter("@ordernumber_2", OleDbType.VarWChar));
                accmastercom.Parameters["@ordernumber_2"].Value = ordernumber; // lstorders.SelectedItem.Value

                accmastercom.Parameters.Add(new OleDbParameter("@indentnumber_3", OleDbType.VarWChar));
                accmastercom.Parameters["@indentnumber_3"].Value = indentnumber; // DataGrid1.Items(i).Cells(10).Text

                accmastercom.Parameters.Add(new OleDbParameter("@form_4", OleDbType.VarWChar));
                accmastercom.Parameters["@form_4"].Value = form; // String.Empty 'datagrid1.Items(i).Cells(  .SelectedItem.Text 'Session("Form")

                accmastercom.Parameters.Add(new OleDbParameter("@accessionid_5", OleDbType.Numeric));
                accmastercom.Parameters["@accessionid_5"].Value = accessionid; // Val(txtaccid.Value) + access

                accmastercom.Parameters.Add(new OleDbParameter("@accessioneddate_6", OleDbType.Date));
                accmastercom.Parameters["@accessioneddate_6"].Value = accessioneddate; // docarrivaldate.Value  '.Now.ToString("dd/MM/yyyy") ' d.ToString("dd/MM/yyyy")

                accmastercom.Parameters.Add(new OleDbParameter("@booktitle_7", OleDbType.VarWChar));
                accmastercom.Parameters["@booktitle_7"].Value = booktitle; // lbltitl.Text 'DataGrid1.Items(i).Cells(1).Text   'Trim(txtbooktitle.Value) ' Session("title")

                accmastercom.Parameters.Add(new OleDbParameter("@srno_8", OleDbType.Numeric));

                accmastercom.Parameters["@srno_8"].Value = srno; // dt.Rows(i).Item(0) 'Val(tmpstr1) ' + i


                accmastercom.Parameters.Add(new OleDbParameter("@released_9", OleDbType.VarWChar));
                accmastercom.Parameters["@released_9"].Value = relStatus; // sr

                accmastercom.Parameters.Add(new OleDbParameter("@bookprice_10", OleDbType.Decimal));
                accmastercom.Parameters["@bookprice_10"].Value = bookprice; // Val(lblActualPrice.Text) * Val(ex_rate)  'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)

                accmastercom.Parameters.Add(new OleDbParameter("@srNoOld_11", OleDbType.Numeric));
                accmastercom.Parameters["@srNoOld_11"].Value = srNoOld; // Val(tmpstr) + i  'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)


                accmastercom.Parameters.Add(new OleDbParameter("@biilNo_12", OleDbType.VarWChar));
                accmastercom.Parameters["@biilNo_12"].Value = biilNo; // Trim(docno.Value) 'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)

                accmastercom.Parameters.Add(new OleDbParameter(" @billDate_13", OleDbType.Date));
                // accmastercom.Parameters(" @billDate_13").Value = IIf(billDate = String.Empty, DBNull.Value, billDate) 'docarrivaldate.Value    'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)
                accmastercom.Parameters[" @billDate_13"].Value = string.IsNullOrEmpty(billDate)? null: billDate; // docarrivaldate.Value    'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)

                accmastercom.Parameters.Add(new OleDbParameter("@Item_type_14", OleDbType.VarWChar));
                accmastercom.Parameters["@Item_type_14"].Value = Item_type; // "Books"


                accmastercom.Parameters.Add(new OleDbParameter("@OriginalPrice_15", OleDbType.Double));
                accmastercom.Parameters["@OriginalPrice_15"].Value = OriginalPrice; // Val(lblActualPrice.Text)


                accmastercom.Parameters.Add(new OleDbParameter("@OriginalCurrency_16", OleDbType.VarWChar));
                accmastercom.Parameters["@OriginalCurrency_16"].Value = OriginalCurrency; // DataGrid1.Items(i).Cells(8).Text

                accmastercom.Parameters.Add(new OleDbParameter("@userid_17", OleDbType.VarWChar));
                accmastercom.Parameters["@userid_17"].Value = userid; // Session("user_id")

                accmastercom.Parameters.Add(new OleDbParameter("@vendor_source_18", OleDbType.VarWChar));
                accmastercom.Parameters["@vendor_source_18"].Value = vendor_source; // cmbvendor.SelectedItem.Text

                accmastercom.Parameters.Add(new OleDbParameter("@DeptCode_19", OleDbType.Integer));
                accmastercom.Parameters["@DeptCode_19"].Value = DeptCode; // dtbew.Rows(i).Item("departmentcode")

                accmastercom.Parameters.Add(new OleDbParameter("@DSrno_20", OleDbType.Numeric));
                accmastercom.Parameters["@DSrno_20"].Value = DSrno;

                accmastercom.Parameters.Add(new OleDbParameter("@DeptName_21", OleDbType.VarWChar));
                accmastercom.Parameters["@DeptName_21"].Value = DeptName;

                try
                {
                    accmastercom.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    RetFlag = false;
                }
                return RetFlag;
            }
            else
            {
                // MsgBox("You are Only allowed to Catalogue " & LibRescObj.BookRestriction() & " books ! Please Contact MSSPL!")
                return false;
            }
        }
        // by kaushal K
        public string InsertBookAccession(string accessionnumber, string ordernumber, string indentnumber, string form, int accessionid, System.DateTime accessioneddate, string booktitle, int srno, string relStatus, decimal bookprice, int srNoOld, string biilNo, string billDate, string Item_type, double OriginalPrice, string OriginalCurrency, string userid, string vendor_source, int DeptCode, int DSrno, string DeptName, string ItemCategory, int ItemCategoryCode, OleDbCommand accmastercom, string BookNumber)
        {
            if (CountAccession(accmastercom))
            {

                string RetFlag = "";

                accmastercom.CommandType = CommandType.StoredProcedure;
                accmastercom.CommandText = "insert_bookaccessionmaster_2";
                accmastercom.CommandTimeout = 300;
                accmastercom.Parameters.Add(new OleDbParameter("@accessionnumber_1", OleDbType.VarWChar));
                accmastercom.Parameters["@accessionnumber_1"].Value = accessionnumber; // accno

                accmastercom.Parameters.Add(new OleDbParameter("@ordernumber_2", OleDbType.VarWChar));
                accmastercom.Parameters["@ordernumber_2"].Value = ordernumber; // lstorders.SelectedItem.Value

                accmastercom.Parameters.Add(new OleDbParameter("@indentnumber_3", OleDbType.VarWChar));
                accmastercom.Parameters["@indentnumber_3"].Value = indentnumber; // DataGrid1.Items(i).Cells(10).Text

                accmastercom.Parameters.Add(new OleDbParameter("@form_4", OleDbType.VarWChar));
                accmastercom.Parameters["@form_4"].Value = form; // String.Empty 'datagrid1.Items(i).Cells(  .SelectedItem.Text 'Session("Form")

                accmastercom.Parameters.Add(new OleDbParameter("@accessionid_5", OleDbType.Numeric));
                accmastercom.Parameters["@accessionid_5"].Value = accessionid; // Val(txtaccid.Value) + access

                accmastercom.Parameters.Add(new OleDbParameter("@accessioneddate_6", OleDbType.Date));
                accmastercom.Parameters["@accessioneddate_6"].Value = accessioneddate; // docarrivaldate.Value  '.Now.ToString("dd/MM/yyyy") ' d.ToString("dd/MM/yyyy")

                accmastercom.Parameters.Add(new OleDbParameter("@booktitle_7", OleDbType.VarWChar));
                accmastercom.Parameters["@booktitle_7"].Value = booktitle; // lbltitl.Text 'DataGrid1.Items(i).Cells(1).Text   'Trim(txtbooktitle.Value) ' Session("title")

                accmastercom.Parameters.Add(new OleDbParameter("@srno_8", OleDbType.Numeric));

                accmastercom.Parameters["@srno_8"].Value = srno; // dt.Rows(i).Item(0) 'Val(tmpstr1) ' + i


                accmastercom.Parameters.Add(new OleDbParameter("@relStatus_9", OleDbType.VarWChar));
                accmastercom.Parameters["@relStatus_9"].Value = relStatus; // sr

                accmastercom.Parameters.Add(new OleDbParameter("@bookprice_10", OleDbType.Decimal));
                accmastercom.Parameters["@bookprice_10"].Value = bookprice; // Val(lblActualPrice.Text) * Val(ex_rate)  'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)

                accmastercom.Parameters.Add(new OleDbParameter("@srNoOld_11", OleDbType.Numeric));
                accmastercom.Parameters["@srNoOld_11"].Value = srNoOld; // Val(tmpstr) + i  'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)


                accmastercom.Parameters.Add(new OleDbParameter("@biilNo_12", OleDbType.VarWChar));
                accmastercom.Parameters["@biilNo_12"].Value = biilNo; // Trim(docno.Value) 'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)

                accmastercom.Parameters.Add(new OleDbParameter(" @billDate_13", OleDbType.Date));
                // accmastercom.Parameters(" @billDate_13").Value = IIf(billDate = String.Empty, DBNull.Value, billDate) 'docarrivaldate.Value    'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)
                accmastercom.Parameters[" @billDate_13"].Value = string.IsNullOrEmpty(billDate) ? null : billDate; // docarrivaldate.Value    'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)

                accmastercom.Parameters.Add(new OleDbParameter("@Item_type_14", OleDbType.VarWChar));
                accmastercom.Parameters["@Item_type_14"].Value = Item_type; // "Books"


                accmastercom.Parameters.Add(new OleDbParameter("@OriginalPrice_15", OleDbType.Double));
                accmastercom.Parameters["@OriginalPrice_15"].Value = OriginalPrice; // Val(lblActualPrice.Text)


                accmastercom.Parameters.Add(new OleDbParameter("@OriginalCurrency_16", OleDbType.VarWChar));
                accmastercom.Parameters["@OriginalCurrency_16"].Value = OriginalCurrency; // DataGrid1.Items(i).Cells(8).Text

                accmastercom.Parameters.Add(new OleDbParameter("@userid_17", OleDbType.VarWChar));
                accmastercom.Parameters["@userid_17"].Value = userid; // Session("user_id")

                accmastercom.Parameters.Add(new OleDbParameter("@vendor_source_18", OleDbType.VarWChar));
                accmastercom.Parameters["@vendor_source_18"].Value = vendor_source; // cmbvendor.SelectedItem.Text

                accmastercom.Parameters.Add(new OleDbParameter("@DeptCode_19", OleDbType.Integer));
                accmastercom.Parameters["@DeptCode_19"].Value = DeptCode; // dtbew.Rows(i).Item("departmentcode")

                accmastercom.Parameters.Add(new OleDbParameter("@DSrno_20", OleDbType.Numeric));
                accmastercom.Parameters["@DSrno_20"].Value = DSrno;

                accmastercom.Parameters.Add(new OleDbParameter("@DeptName_21", OleDbType.VarWChar));
                accmastercom.Parameters["@DeptName_21"].Value = DeptName;

                accmastercom.Parameters.Add(new OleDbParameter("@ItemCategory_22", OleDbType.VarWChar));
                accmastercom.Parameters["@ItemCategory_22"].Value = ItemCategory;

                accmastercom.Parameters.Add(new OleDbParameter("@ItemCategoryCode_23", OleDbType.Integer));
                accmastercom.Parameters["@ItemCategoryCode_23"].Value = ItemCategoryCode;

                accmastercom.Parameters.Add(new OleDbParameter("@BookNumber", OleDbType.VarWChar));
                accmastercom.Parameters["@BookNumber"].Value = BookNumber;   // Trim(docno.Value) 'Val(DataGrid1.Items(i).Cells(7).Text)   'Val(txtprice.Value)



                try
                {
                    accmastercom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    RetFlag = ex.Message;


                }

                return RetFlag;
            }
            else
            {
                // MsgBox("You are Only allowed to Catalogue " & LibRescObj.BookRestriction() & " books! Please Contact MSSPL!")
                // Return "You are Only allowed to Catalogue " & LibRescObj.BookRestriction() & " books! Please Contact MSSPL!"
                return "false";
            }
        }



        // **************************************By Jeetendra Prajapati as on 31 October, 009***********************

        public bool InsertExistingBookInfo(int srNoOld, int mediatype, string title, string authortype, string authfirstname1, string authmiddlename1, string authlastname1, string authfirstname2, string authmiddlename2, string authlastname2, string authfirstname3, string authmiddlename3, string authlastname3, string edition, string yearofedition, string volumeno, string isbn, int category, int noofcopies, decimal price, int publisherid, System.DateTime recordingdate, string seriesname, string form, string keywords, System.DateTime DocDate, decimal Fprice, string currencyCode, string subtitle, int Part, decimal specialprice, int departmentcode, string yearofPublication, int Language_Id, decimal exchange_rate, string bk_no_of_pages, string page_size, int vendor_id, OleDbCommand existbkcom)
        {
            if (CAccno())
            {
                bool RetFlag = true;

                existbkcom.Parameters.Clear();
                existbkcom.CommandType = CommandType.StoredProcedure;
                existbkcom.CommandText = "insert_existingbookkinfo_1";

                existbkcom.Parameters.Add(new OleDbParameter("@srNoOld_1", OleDbType.Numeric));
                existbkcom.Parameters["@srNoOld_1"].Value = srNoOld;

                existbkcom.Parameters.Add(new OleDbParameter("@mediatype_2", OleDbType.Numeric));
                existbkcom.Parameters["@mediatype_2"].Value = mediatype;

                existbkcom.Parameters.Add(new OleDbParameter("@title_3", OleDbType.VarWChar));
                existbkcom.Parameters["@title_3"].Value = title;

                existbkcom.Parameters.Add(new OleDbParameter("@authortype_4", OleDbType.VarWChar));
                existbkcom.Parameters["@authortype_4"].Value = authortype; // Default is 'Author'

                existbkcom.Parameters.Add(new OleDbParameter("@firstname1_5", OleDbType.VarWChar));
                existbkcom.Parameters["@firstname1_5"].Value = authfirstname1;

                existbkcom.Parameters.Add(new OleDbParameter("@middlename1_6", OleDbType.VarWChar));
                existbkcom.Parameters["@middlename1_6"].Value = authmiddlename1;

                existbkcom.Parameters.Add(new OleDbParameter("@lastname1_7", OleDbType.VarWChar));
                existbkcom.Parameters["@lastname1_7"].Value = authlastname1;

                existbkcom.Parameters.Add(new OleDbParameter("@firstname2_8", OleDbType.VarWChar));
                existbkcom.Parameters["@firstname2_8"].Value = authfirstname2;

                existbkcom.Parameters.Add(new OleDbParameter("@middlename2_9", OleDbType.VarWChar));
                existbkcom.Parameters["@middlename2_9"].Value = authmiddlename2;

                existbkcom.Parameters.Add(new OleDbParameter("@lastname2_10", OleDbType.VarWChar));
                existbkcom.Parameters["@lastname2_10"].Value = authlastname2;

                existbkcom.Parameters.Add(new OleDbParameter("@firstname3_11", OleDbType.VarWChar));
                existbkcom.Parameters["@firstname3_11"].Value = authfirstname3;

                existbkcom.Parameters.Add(new OleDbParameter("@middlename3_12", OleDbType.VarWChar));
                existbkcom.Parameters["@middlename3_12"].Value = authmiddlename3;

                existbkcom.Parameters.Add(new OleDbParameter("@lastname3_13", OleDbType.VarWChar));
                existbkcom.Parameters["@lastname3_13"].Value = authlastname3;

                existbkcom.Parameters.Add(new OleDbParameter("@edition_14", OleDbType.VarWChar));
                existbkcom.Parameters["@edition_14"].Value = edition;

                existbkcom.Parameters.Add(new OleDbParameter("@yearofedition_15", OleDbType.VarWChar));
                existbkcom.Parameters["@yearofedition_15"].Value = yearofedition;

                existbkcom.Parameters.Add(new OleDbParameter("@volumeno_16", OleDbType.VarWChar));
                existbkcom.Parameters["@volumeno_16"].Value = volumeno;

                existbkcom.Parameters.Add(new OleDbParameter("@isbn_17", OleDbType.VarWChar));
                existbkcom.Parameters["@isbn_17"].Value = isbn;

                existbkcom.Parameters.Add(new OleDbParameter("@category_18", OleDbType.Integer));
                existbkcom.Parameters["@category_18"].Value = category;

                existbkcom.Parameters.Add(new OleDbParameter("@noofcopies_19", OleDbType.Integer));
                existbkcom.Parameters["@noofcopies_19"].Value = noofcopies;

                existbkcom.Parameters.Add(new OleDbParameter("@price_20", OleDbType.Decimal));
                existbkcom.Parameters["@price_20"].Value = price;

                existbkcom.Parameters.Add(new OleDbParameter("@publisherid_21", OleDbType.Integer));
                existbkcom.Parameters["@publisherid_21"].Value = publisherid;


                existbkcom.Parameters.Add(new OleDbParameter("@recordingdate_22", OleDbType.Date));
                existbkcom.Parameters["@recordingdate_22"].Value = recordingdate;

                existbkcom.Parameters.Add(new OleDbParameter("@seriesname_23", OleDbType.VarWChar));
                existbkcom.Parameters["@seriesname_23"].Value = seriesname;

                existbkcom.Parameters.Add(new OleDbParameter("@form_24", OleDbType.VarWChar));
                existbkcom.Parameters["@form_24"].Value = form;

                existbkcom.Parameters.Add(new OleDbParameter("@keywords_25", OleDbType.VarWChar));
                existbkcom.Parameters["@keywords_25"].Value = keywords;

                existbkcom.Parameters.Add(new OleDbParameter("@docDate_26", OleDbType.Date));
                existbkcom.Parameters["@docDate_26"].Value = DocDate;


                existbkcom.Parameters.Add(new OleDbParameter("@Fprice_27", OleDbType.Decimal));
                existbkcom.Parameters["@Fprice_27"].Value = Fprice;

                existbkcom.Parameters.Add(new OleDbParameter("@FcurrencyCode_28", OleDbType.VarWChar));
                existbkcom.Parameters["@FcurrencyCode_28"].Value = currencyCode;


                existbkcom.Parameters.Add(new OleDbParameter("@subtitle_29", OleDbType.VarWChar));
                existbkcom.Parameters["@subtitle_29"].Value = subtitle;

                existbkcom.Parameters.Add(new OleDbParameter("@part_30", OleDbType.Numeric));
                existbkcom.Parameters["@part_30"].Value = Part;

                existbkcom.Parameters.Add(new OleDbParameter("@specialprice_31", OleDbType.Decimal));
                existbkcom.Parameters["@specialprice_31"].Value = specialprice;

                existbkcom.Parameters.Add(new OleDbParameter("@dept_32", OleDbType.Integer));
                existbkcom.Parameters["@dept_32"].Value = departmentcode;

                existbkcom.Parameters.Add(new OleDbParameter("@yearofPublication_33", OleDbType.VarWChar));
                existbkcom.Parameters["@yearofPublication_33"].Value = yearofPublication;

                existbkcom.Parameters.Add(new OleDbParameter("@Language_Id_34", OleDbType.Integer));
                existbkcom.Parameters["@Language_Id_34"].Value = Language_Id;

                existbkcom.Parameters.Add(new OleDbParameter("@exchange_rate_35", OleDbType.Decimal));
                existbkcom.Parameters["@exchange_rate_35"].Value = exchange_rate;

                existbkcom.Parameters.Add(new OleDbParameter("@no_of_pages_36", OleDbType.Integer));
                existbkcom.Parameters["@no_of_pages_36"].Value = bk_no_of_pages;

                existbkcom.Parameters.Add(new OleDbParameter("@page_size_37", OleDbType.VarWChar));
                existbkcom.Parameters["@page_size_37"].Value = page_size;

                existbkcom.Parameters.Add(new OleDbParameter("@vendorid_38", OleDbType.Integer));
                existbkcom.Parameters["@vendorid_38"].Value = vendor_id;

                existbkcom.ExecuteNonQuery();
                try
                {
                    existbkcom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    RetFlag = false;
                }

                return RetFlag;
            }
            else
            {
                // MsgBox("You are Only allowed to Catalogue " & LibRescObj.BookRestriction() & " books! Please Contact MSSPL!")
                return false;
            }
        }
        // *********************************************************************************************************

        public bool CountAccession(OleDbCommand cmd)
        {
            // by kausahl @14.10.14
            // Dim tr As OleDbTransaction
            bool flag = false;
            // Dim constr As String = ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session("LibWiseDBConn")).ToString()
            // Dim con As New OleDbConnection(constr)
            // con.Open()
            // tr = con.BeginTransaction()
            // Dim cmd As New OleDbCommand(, con)
            // cmd.Transaction = tr
            cmd.CommandText = "select count(accessionnumber) from bookaccessionmaster";
            TotalCount = Convert.ToInt32(cmd.ExecuteScalar());

            //do it later sep
            //            if (TotalCount < LibRescObj.BookRestriction())
            //          {
            //            flag = true;
            //      }
            //    else
            //  {
            //    flag = false;
            //}
            flag = true; //see above
            cmd.Parameters.Clear();

            return flag;
        }

        public bool CAccno()
        {
            return true;
            /*
            object flag = false;
            string constr = System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString();
            constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            var da = new OleDbDataAdapter("select count(*) from bookaccessionmaster", constr);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            if (Convert.ToInt64(ds.Tables[0].Rows[0][0]) < LibRescObj.BookRestriction())
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return Convert.ToBoolean(flag);
            */
        }
    }
}