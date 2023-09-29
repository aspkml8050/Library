using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;

namespace Library
{
    public partial class existingbookacc : BaseClass
    {
        insertLogin LibObj1 = new insertLogin();
        
        private dbUtilities message = new dbUtilities();
        libGeneralFunctions LibObj = new libGeneralFunctions();
        libGeneralFunctions libobj1 = new libGeneralFunctions();
        DBIStructure DBI = new DBIStructure();
        private static string tmpcondition;
        private bool isNewCtrl = false;
        private string strResult = "";
        private ClassFileCatelog ObjCatalog = new ClassFileCatelog();
        private ClassFileBookAccession ObjBookAccession = new ClassFileBookAccession();
    //    private LibraryRestriction LibRescObj = new LibraryRestriction();
        private static bool AllowIn = false;
        public string GetCallbackResult()
        {
            return strResult;
        }
        public void RaiseCallbackEvent(string eventArgument)
        {

//            this.SetFocus(cmbcurr);
            string s;
            var tmpcon = new OleDbConnection(retConstr(""));
            tmpcon.Open();
            if (eventArgument == HComboSelect.Value)
            {
                txtForeignPrice.Value = string.Empty;
                return;
            }
            else
            {
                s = libobj1 .populateCommandText("select currencyname from exchangemaster where currencycode=" + eventArgument, tmpcon);
            }
            txtForeignPrice.Value = string.Empty;
            var tmpcom = new OleDbCommand();
            System.DateTime tmpDate;
            float exRate = 0f;
            bool isFound = false;
            string p;
            var ds = new DataSet();
            var da = new OleDbDataAdapter("select CurrencyConversionfactor from librarysetupinformation", tmpcon);
            da.Fill(ds);
            p = ds.Tables[0].Rows[0][0].ToString();
            tmpDate = Convert.ToDateTime( txtAccDate.Text);
            var tempda = new OleDbDataAdapter("select " + (p == "Bank"? "bankrate": "gocrate") + ",EffectiveFrom from exchangemaster  where  currencycode=" + eventArgument + " order by EffectiveFrom desc", tmpcon);
            var tmpds = new DataSet();
            tempda.Fill(tmpds, "rates");
            int counter;
            var loopTo = tmpds.Tables[0].Rows.Count - 1;
            for (counter = 0; counter <= loopTo; counter++)
            {
                if (Convert.ToDateTime( tmpds.Tables[0].Rows[counter][1]) <= tmpDate)
                {
                    exRate = Convert.ToSingle(tmpds.Tables[0].Rows[counter][0]);
                    isFound = true;
                    break;
                }
            }
            if (eventArgument == HComboSelect.Value)
            {
                strResult =string. Format("{0:#.00}", exRate);
            }
            else
            {
                strResult = string.Format("{0:#.00}", exRate) + "|" + s;
            }
            if (isFound == false)
            {
                txtExchangeRate.Value = string.Empty;
                txtForeignPrice.Value = string.Empty;
                return;
            }
            tmpcon.Dispose();
            tmpcon.Close();
            tmpcon.Dispose();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string cbref = Page.ClientScript.GetCallbackEventReference(this, "arg", "clientback", "context");
                //string cbScr = string.Format("function UseCallBack(arg, context) {{ {0}; }} ", cbref);
                // Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "UseCallBack", cbScr, True)
                msglabel.Visible = false;
                // cmdReturn.CausesValidation = False
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
//                hdIsMarc21.Value = myConfiguration.AppSettings.Settings.Item("IsMarc21").Value;

                cmdreset.CausesValidation = false;
                this.cmddelete.CausesValidation = false;
                btnCategoryFilter.CausesValidation = false;
                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)
                if (!IsPostBack)
                {
                    this.cmddelete.Disabled = true;

                    PopulateCurrency();
                    PopulateCategory();
                    Populatemedia();
                    poplatelanguage();
                    this.Label36.Visible = false;
                    optaccno.Visible = false;
                    optpost.Visible = false;
                    optpre.Visible = false;
                    opttitle.Visible = false;
                    this.Label48.Visible = false;
                    this.txtrelease.Visible = false;
                    this.Button2.Visible = false;
                    Label42.Visible = false;
                    btnCategoryFilter.Visible = false;
                    Label44.Visible = false;
                    Label45.Visible = false;
                    txtCategory.Visible = false;
                    grddetail.Visible = false;
                    txtAccDate.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Today);

                 //   this.SetFocus(txtkeywords);
//                    lblTitle.Text = Request.QueryString["title"];

  //                  tmpcondition = Request.QueryString["condition"];
    //                this.HCondition.Value = Request.QueryString["condition"];
      //              hdCulture.Value = Request.Cookies["UserCulture"].Value;
        //            if (tmpcondition == "Y")
          //          {
                        // Me.cmdsave.Disabled = False
            //            btnS.Enabled = true;
              //      }
                //    else
                  //  {
                        // Me.cmdsave.Disabled = True
                    //    btnS.Enabled = false;
                    //}

                    string Curr = string.Empty;
                    Curr = libobj1 .getCurrency(retConstr(""));

                    Label35.Text = Resources.ValidationResources.LPricePCpy ;
                    Label15.Text = Resources.ValidationResources.LPricePCpy + "(" + Curr + ")";
                    Label3.Text = Resources.ValidationResources.LSPrice + "(" + Curr + ")";
                    // RequiredFieldValidator4.ErrorMessage = Resources.ValidationResources.EnterPrice.ToString & "(" & Curr & ")"
                    this.ChkCataloging.Checked = false;
                    this.txtClass.Value = string.Empty;
                    this.txtBook.Value = string.Empty;
                    this.txtClass.Visible = false;
                    this.txtBook.Visible = false;
                    this.Label40.Visible = false;
                    this.Label41.Visible = false;
                    this.Label47.Visible = false;
                    this.cmbStatus.Visible = false;
                    this.txtnoofcopies.Value = "1";
                    dept();
                    // Call vendor()

                    var prefixcon = new OleDbConnection(retConstr(""));
                    prefixcon.Open();
                    var prefixds = new DataSet();
                    // Praveen---add where clause
                    prefixds = libobj1.PopulateDataset("select prefixid,prefixname from prefixmaster where prefixid <> 0 order by prefixname", "PrefixTable", prefixcon);
                    if (prefixds.Tables["PrefixTable"].Rows.Count > 0)
                    {
                        cmbaccessionprefix.DataSource = prefixds;
                        cmbaccessionprefix.DataTextField = "prefixname";
                        cmbaccessionprefix.DataValueField = "prefixid";
                        cmbaccessionprefix.DataBind();
                        cmbaccessionprefix.Items.Add(this.HComboSelect.Value);
                        cmbaccessionprefix.SelectedIndex = cmbaccessionprefix.Items.Count - 1;

                        cmbaccrange.DataSource = prefixds;
                        cmbaccrange.DataTextField = "prefixname";
                        cmbaccrange.DataValueField = "prefixid";
                        cmbaccrange.DataBind();
                        cmbaccrange.Items.Add(this.HComboSelect.Value);
                        cmbaccrange.SelectedIndex = cmbaccrange.Items.Count - 1;
                    }
                    else
                    {
                        cmbaccessionprefix.Items.Clear();
                        cmbaccessionprefix.Items.Add(this.HComboSelect.Value);
                        cmbaccessionprefix.SelectedIndex = cmbaccessionprefix.Items.Count - 1;

                        cmbaccrange.Items.Clear();
                        cmbaccrange.Items.Add(this.HComboSelect.Value);
                        cmbaccrange.SelectedIndex = cmbaccrange.Items.Count - 1;
                    }
                    prefixds.Dispose();
                    string Err;
                    Err = AccPref(ref cmbaccessionprefix, prefixcon);
                    if (!string.IsNullOrEmpty(Err))
                    {
                        throw new ApplicationException(Err);
                    }
                    Err = AccPref(ref cmbaccrange, prefixcon);
                    if (!string.IsNullOrEmpty(Err))
                    {
                        throw new ApplicationException(Err);
                    }
                    // Call publisher()
                    Label12.Visible = true;
                    cmbaccessionprefix.Visible = true;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                    txtstartno.Visible = false;
                    txtaccession.Visible = false;
                    Label22.Visible = false;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                    chkApplyRange.Visible = false;

                    default_setting();

                }
                if (HCurrName.Value != "")
                {
                    libobj1.setResult(cmbcurr, Label35, "Price/Copy", this.HCurrName.Value, this.HComboSelect.Value);
                }
                else
                {
                    this.Label35.Text = Resources.ValidationResources.LPricePCpy;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }
        public void default_setting()
        {
            var prefixcon = new OleDbConnection(retConstr(""));
            string media, language, category, media1, language1, category1;
            var lib_ds = new DataSet();
            lib_ds = libobj1.PopulateDataset("select media,def_language,category,media_id,language_id,category_id from librarysetupinformation", "library", prefixcon);
            if (lib_ds.Tables["library"].Rows.Count > 0)
            {
                media = lib_ds.Tables["library"].Rows[0]["media"].ToString();
                language = lib_ds.Tables["library"].Rows[0]["def_language"].ToString();
                category = lib_ds.Tables["library"].Rows[0]["category"].ToString();
                media1 = lib_ds.Tables["library"].Rows[0]["media_id"].ToString();
                language1 = lib_ds.Tables["library"].Rows[0]["language_id"].ToString();
                category1 = lib_ds.Tables["library"].Rows[0]["category_id"].ToString();
                // Default value set 
                var lst1 = new ListItem();
                lst1.Text = category;
                lst1.Value = category1;
                if (cmbcategory.Items.Contains(lst1) == true)
                {
                    cmbcategory.SelectedIndex = cmbcategory.Items.IndexOf(cmbcategory.Items.FindByText(category));
                }
                else
                {
                    cmbcategory.SelectedIndex = cmbcategory.Items.Count - 1;
                }
                lst1.Text = media;
                lst1.Value = media1;
                if (cmbmediatype.Items.Contains(lst1) == true)
                {
                    cmbmediatype.SelectedIndex = cmbmediatype.Items.IndexOf(cmbmediatype.Items.FindByText(media));
                }
                else
                {
                    cmbmediatype.SelectedIndex = cmbmediatype.Items.Count - 1;
                }
                lst1.Text = language;
                lst1.Value = language1;
                if (cmbLanguage.Items.Contains(lst1) == true)
                {
                    cmbLanguage.SelectedIndex = cmbLanguage.Items.IndexOf(cmbLanguage.Items.FindByText(language));
                }
                else
                {
                    cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
                }
            }

            lib_ds = libobj1.PopulateDataset("Select PublisherId, firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'0' and publishermaster.publisherid=addresstable.addid and addrelation=N'publisher'", "lib", prefixcon);

            if (lib_ds.Tables["lib"].Rows.Count > 0)
            {
                txtCmbPublisher.Text = lib_ds.Tables["lib"].Rows[0]["firstname"].ToString();
                // asmpublisher.SelectedValue = lib_ds.Tables("lib").Rows(0).Item("PublisherId")
                hdPublisherId.Value = lib_ds.Tables["lib"].Rows[0]["PublisherId"].ToString();
            }

            lib_ds = libobj1.PopulateDataset("Select vendorid, vendorname+', '+percity as firstname  from  vendormaster,addresstable where vendorid=N'0' and vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor'", "lib1", prefixcon);
            if (lib_ds.Tables["lib1"].Rows.Count > 0)
            {
                this.txtCmbVendor.Text = lib_ds.Tables["lib1"].Rows[0]["firstname"].ToString();
                // Me.asmvendor.SelectedValue = lib_ds.Tables("lib1").Rows(0).Item("vendorid")
                HdVendorid.Value = lib_ds.Tables["lib1"].Rows[0]["vendorid"].ToString();

                HdVendorid.Value = lib_ds.Tables["lib1"].Rows[0]["vendorid"].ToString();
                // Hdvenid.Value = lib_ds.Tables("lib1").Rows(0).Item("vendorid")
            }

            // For Currency Setting By Bipin


            string cur = null;
            var cur1 = default(int);
            var exrate = default(decimal);
            lib_ds = libobj1.PopulateDataset("select CurrencyCode,currency,BankRate from librarysetupinformation,exchangemaster where  librarysetupinformation.currency=exchangemaster.CurrencyName", "Currency", prefixcon);
            if (lib_ds.Tables["currency"].Rows.Count > 0)
            {
                cur = lib_ds.Tables["currency"].Rows[0]["currency"].ToString();
                cur1 = Convert.ToInt32(lib_ds.Tables["currency"].Rows[0]["CurrencyCode"]);
                exrate = Convert.ToDecimal(lib_ds.Tables["currency"].Rows[0]["BankRate"]);

            }
            var lst2 = new ListItem();
            lst2.Text = cur;
            lst2.Value = cur1.ToString();
            if (this.cmbcurr.Items.Contains(lst2) == true)
            {
                this.cmbcurr.SelectedIndex = this.cmbcurr.Items.IndexOf(this.cmbcurr.Items.FindByText(cur));
            }
            else
            {
                this.cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
            }
            this.txtExchangeRate.Value = exrate.ToString();

            lib_ds.Dispose();
        }
        public void dept()
        {
            var Con = new OleDbConnection(retConstr(""));
            Con.Open();
            var da = new OleDbDataAdapter("select departmentcode,InstituteMaster.ShortName + '-' + departmentname as departmentname from institutemaster,departmentmaster where departmentmaster.institutecode=institutemaster.institutecode order by InstituteMaster.ShortName + '-' + departmentname", Con);
            var ds = new DataSet();
            da.Fill(ds, "dept");
            if (ds.Tables["dept"].Rows.Count > 0)
            {
                cmbdept.DataSource = ds;
                cmbdept.DataTextField = "departmentname";
                cmbdept.DataValueField = "departmentcode";
                cmbdept.DataBind();
                cmbdept.Items.Add(this.HComboSelect.Value);
                cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
            }
            else
            {
                cmbdept.Items.Clear();
                cmbdept.Items.Add(this.HComboSelect.Value);
                cmbdept.SelectedIndex = cmbdept.Items.Count - 1;
            }
            Con.Close();
            ds.Dispose();
            Con.Dispose();
        }
        public void PopulateCurrency()
        {
            var CurrencyCon = new OleDbConnection(retConstr(""));
            CurrencyCon.Open();
            var Currencyda = new OleDbDataAdapter("Select distinct currencycode,currencyname from exchangemaster order by currencyname", CurrencyCon);
            var Currencyds = new DataSet();
            Currencyda.Fill(Currencyds, "currency");
            Currencyda = new OleDbDataAdapter("select id,item_type from item_type ", CurrencyCon);
            Currencyda.Fill(Currencyds, "Itype");

            if (Currencyds.Tables["currency"].Rows.Count > 0)
            {
                cmbcurr.DataSource = Currencyds.Tables["currency"];
                cmbcurr.DataTextField = "currencyname";
                cmbcurr.DataValueField = "currencycode";
                cmbcurr.DataBind();
                cmbcurr.Items.Add(this.HComboSelect.Value);
                cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
            }
            else
            {
                cmbcurr.Items.Clear();
                cmbcurr.Items.Add(this.HComboSelect.Value);
                cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
            }
            if (Currencyds.Tables["itype"].Rows.Count > 0)
            {
                ddIType.DataSource = Currencyds.Tables["Itype"];
                ddIType.DataTextField = "Item_Type";
                ddIType.DataValueField = "id";
                ddIType.DataBind();

            }
            CurrencyCon.Close();
            Currencyds.Dispose();
            CurrencyCon.Dispose();
        }
        public void PopulateCategory()
        {
            var CategoryCon = new OleDbConnection(retConstr(""));
            CategoryCon.Open();
            var Categoryda = new OleDbDataAdapter("Select distinct id,Category_LoadingStatus from CategoryLoadingStatus where id <> 0 order by Category_LoadingStatus", CategoryCon);
            var Categoryds = new DataSet();
            Categoryda.Fill(Categoryds, "CategoryLoadingStatus");
            if (Categoryds.Tables["CategoryLoadingStatus"].Rows.Count > 0)
            {
                cmbcategory.DataSource = Categoryds;
                cmbcategory.DataTextField = "Category_LoadingStatus";
                cmbcategory.DataValueField = "id";
                cmbcategory.DataBind();
                cmbcategory.Items.Add(this.HComboSelect.Value);
                cmbcategory.SelectedIndex = cmbcategory.Items.Count - 1;
            }
            else
            {
                cmbcategory.Items.Clear();
                cmbcategory.Items.Add(this.HComboSelect.Value);
                cmbcategory.SelectedIndex = cmbcategory.Items.Count - 1;
            }
            Categoryds.Dispose();
            CategoryCon.Close();
            CategoryCon.Dispose();
        }
        public void Populatemedia()
        {
            var mediaCon = new OleDbConnection(retConstr(""));
            mediaCon.Open();
            var mediada = new OleDbDataAdapter("select distinct media_id,media_name from media_type order by media_name", mediaCon);
            var mediads = new DataSet();
            mediada.Fill(mediads, "media");
            if (mediads.Tables["media"].Rows.Count > 0)
            {
                cmbmediatype.DataSource = mediads;
                cmbmediatype.DataTextField = "media_name";
                cmbmediatype.DataValueField = "media_id";
                cmbmediatype.DataBind();
                cmbmediatype.Items.Add(this.HComboSelect.Value);
                cmbmediatype.SelectedIndex = cmbmediatype.Items.Count - 1;
            }
            else
            {
                cmbmediatype.Items.Clear();
                cmbmediatype.Items.Add(this.HComboSelect.Value);
                cmbmediatype.SelectedIndex = cmbmediatype.Items.Count - 1;
            }
            mediaCon.Close();
            mediaCon.Dispose();
            mediads.Dispose();
        }

        public void poplatelanguage()
        {
            var languageCon = new OleDbConnection(retConstr(""));
            languageCon.Open();
            var languageda = new OleDbDataAdapter("select distinct language_id,language_name from Translation_Language order by language_name", languageCon);
            var languageds = new DataSet();
            languageda.Fill(languageds, "language");
            if (languageds.Tables["language"].Rows.Count > 0)
            {
                cmbLanguage.DataSource = languageds;
                cmbLanguage.DataTextField = "language_name";
                cmbLanguage.DataValueField = "language_id";
                cmbLanguage.DataBind();
                cmbLanguage.Items.Add(HComboSelect.Value);
                cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
            }
            else
            {
                cmbLanguage.Items.Clear();
                cmbLanguage.Items.Add(HComboSelect.Value);
                cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
            }
            languageds.Dispose();
            languageCon.Close();
            languageCon.Dispose();
        }
        public string AccPref(ref DropDownList ddL, OleDbConnection Conn)
        {
            string Err;
            var gData = new FillDsTables();
            string Qry = "";
            var dtPF = new DataTable();
            if (Rbaccession1.Checked == true)
            {
                Qry = "select prefixid,prefixname from prefixmaster where prefixid<>0 and (suffixname='' or suffixname is null) and (prefixname<>'' or prefixname is not null) order by prefixname";
            }
            if (OptAccWOP.Checked == true) // NOT required!!!!
            {
                Qry = "select prefixid,prefixname from prefixmaster where prefixid=0";
            }
            if (RBPreSuff.Checked == true)
            {
                Qry = "select prefixid,prefixname+'-'+suffixname prefixname from prefixmaster where prefixname is not null and suffixname is not null and prefixname<>'' and suffixname<>''  order by prefixname+suffixname";
            }
            if (RBSuffix.Checked == true)
            {
                Qry = "select prefixid,suffixname prefixname from prefixmaster where prefixid<>0 and (suffixname<>'' or suffixname is not null) and (prefixname='' or prefixname is null)  order by suffixname";
            }
            if (string.IsNullOrEmpty(Qry))
            {
                Err = "Error in Pref/Suff selection.";
                throw new ApplicationException(Err);
            }
            Err = gData.FillDs(Qry, ref dtPF);
            ddL.DataSource = (object)null;
            ddL.DataBind();
            ddL.DataSource = dtPF;
            ddL.DataBind();
            ddL.Items.Add(this.HComboSelect.Value);
            if (ddL.Items.Count > 0)
            {

                ddL.SelectedIndex = ddL.Items.Count - 1;
            }

            return Err;
        }
        public void GeberateCtrlNo()
        {
            try
            {
                var ctrlCon = new OleDbConnection(retConstr(""));
                var ctrlCom = new OleDbCommand();
                ctrlCon.Open();
                ctrlCom.Connection = ctrlCon;
                ctrlCom.CommandType = CommandType.Text;
                ctrlCom.CommandText = "select prefix,currentposition,suffix from IdTable where objectName=N'catalog'";
                var dr = ctrlCom.ExecuteReader();
                dr.Read();
                hdCtrlNo.Value = (Convert.ToInt32(dr[1]) + 1).ToString();
                ctrlCom.Dispose();
                ctrlCon.Close();
                ctrlCon.Dispose();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmbaccessionprefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prefixcon = new OleDbConnection(retConstr(""));
            prefixcon.Open();
            var prefixds = new DataSet();
            try
            {
                if (cmbaccessionprefix.SelectedItem.Text == HComboSelect.Value)
                {
                    txtstartno.Value = string.Empty;
                    return;
                }
                // Hidden7.Value = "102"
                // Me.cmbprefix.Focus()
           //     SetFocus(this.cmbaccessionprefix);
                prefixds = libobj1.PopulateDataset("select currentposition from prefixmaster where prefixid=" + cmbaccessionprefix.SelectedItem.Value, "fill", prefixcon);
                txtstartno.Value = prefixds.Tables["fill"].Rows[0]["currentposition"].ToString();
            }

            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (prefixcon.State == ConnectionState.Open)
                {
                    prefixcon.Close();
                }
                prefixds.Dispose();
                prefixcon.Dispose();
            }
        }
        private void refreshfield()
        {
            // cmdsave.Value = Resources.ValidationResources.bSave.ToString
            btnS.Text = Resources.ValidationResources.bSave;
            hdval.Value = "";
            hdval1.Value = "";
            HdVendorid.Value = "";
            this.Label36.Visible = false;
            Rbaccession1.Checked = true;
            OptAccWOP.Checked = false;
            // Manualaccession.Checked = False
            Rbmanual1.Checked = false;

            Rbaccession1.Visible = true;
            OptAccWOP.Visible = true;
            // Manualaccession.Visible = True
            Rbmanual1.Visible = true;
            // Label22.Visible = False
            // txtaccession.Visible = False
            Label12.Visible = true;
            Label43.Visible = true;
            cmbaccessionprefix.Visible = true;

            txtaccession.Visible = false;
            Label22.Visible = false;
            lblaccrange.Visible = false;
            cmbaccrange.Visible = false;
            chkApplyRange.Visible = false;

            lnkContinue.Visible = false;
            lnkModify.Visible = false;
            optaccno.Visible = false;
            optpost.Visible = false;
            optpre.Visible = false;
            opttitle.Visible = false;
            btnCategoryFilter.Visible = false;
            Label44.Visible = false;
            Label45.Visible = false;
            txtCategory.Visible = false;
            grddetail.CurrentPageIndex = 0;
            var tab = new DataTable();
            grddetail.DataSource = tab;
            grddetail.DataBind();
            hdnGrdId.Value = grddetail.ClientID;

            grddetail.Visible = false;
            Label42.Visible = false;

            txtCmbPublisher.Text = "";
            txtCmbVendor.Text = string.Empty;
            txtCmbVendor.Text = "";
            hdFindPbl.Value = "";
            try
            {
            }
            // cmbvendor.ClearSelection()

            catch (Exception ex)
            {

            }

            // cmbvendor1.SelectedIndex() = cmbvendor1.Items.Count - 1
            txtSubbooktitle.Value = "";
            txttovlume.Value = "";
            txtspecialprice.Value = "";
            this.txtExchangeRate.Value = string.Empty;
            Label35.Text = " Price in Foreign curr.";
            this.txtaccid.Value = string.Empty;
            this.txtSTitle.Text = string.Empty;
            this.txtedition.Value = string.Empty;
            this.txteditionyear.Value = string.Empty;
            this.txtfname1.Value = string.Empty;
            this.txtfname2.Value = string.Empty;
            this.txtfname3.Value = string.Empty;
            this.txtisbn.Value = string.Empty;
            this.txtkeywords.Value = string.Empty;
            this.txtlname1.Value = string.Empty;
            this.txtlname2.Value = string.Empty;
            this.txtlname3.Value = string.Empty;
            this.txtmname1.Value = string.Empty;
            this.txtmname2.Value = string.Empty;
            txtPubYear.Value = string.Empty;
            this.txtmname3.Value = string.Empty;
            this.txtnoofcopies.Value = "1";
            this.txtprice.Value = string.Empty;
            this.txtseriesname.Value = string.Empty;
            this.txtstartno.Value = string.Empty;
            this.txtvolumeno.Value = string.Empty;
            cmbaccessionprefix.SelectedIndex = cmbaccessionprefix.Items.Count - 1;
            txtCategory.Value = "";
            optaccno.Checked = true;
            var tab1 = new DataTable();
            grddetail.DataSource = tab1;
            grddetail.DataBind();
            hdnGrdId.Value = grddetail.ClientID;

            txtaccession.Value = "";
            txtstartno.Visible = false;
            // Label41.Visible = True

            txtAccDate.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Today);// Format(System.DateTime.Today, hrDate.Value);
            txtForeignPrice.Value = string.Empty;
            cmbcurr.Items.Clear();
            cmbcurr.Items.Add(this.HComboSelect.Value);
            cmbcurr.SelectedIndex = cmbcurr.Items.Count - 1;
            // '*****Anil
            cmbmediatype.SelectedIndex = cmbmediatype.Items.Count - 1;
            cmbLanguage.SelectedIndex = cmbLanguage.Items.Count - 1;
            cmbcategory.SelectedIndex = cmbcategory.Items.Count - 1;

            Label35.Text = Resources.ValidationResources.LPricePCpy;
            // '**************
            // Call publisher()
            txtcopy.Value = "";
            PopulateCurrency();
            txtnoofcopies.Disabled = false;
            this.CheckBox1.Checked = false;
            this.optaccno.Checked = true;
            this.optpre.Checked = true;
            this.optpost.Checked = false;
            this.opttitle.Checked = false;
            this.OptAccWOP.Checked = false;
        }

        protected void cmdreset2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Label36.Visible = false;
                this.optpre.Enabled = true;
                this.optpost.Enabled = true;
                refreshfield();
                dept();
                string Curr = string.Empty;
                Curr = libobj1.getCurrency(retConstr(""));
                Label35.Text = Resources.ValidationResources.LPricePCpy;
                Label15.Text = Resources.ValidationResources.LPricePCpy + "(" + Curr + ")";
                Label3.Text = Resources.ValidationResources.LSPrice + "(" + Curr + ")";
                txtnoofcopies.Disabled = false;
                Rbaccession1.Checked = true;
                Rbmanual1.Checked = false;
                this.Txtpagesize.Value = string.Empty;
                // Me.SetFocus(txtkeywords)
                Hidden2.Value = "top";
                lblcopy.Visible = false;
                txtcopyno.Visible = false;
                txtcopyno.Value = "";
                chkcopy.Checked = false;
                this.txtClass.Value = string.Empty;
                this.txtBook.Value = string.Empty;
                this.txtClass.Visible = false;
                this.txtBook.Visible = false;
                this.Label40.Visible = false;
                this.Label41.Visible = false;
                this.Label47.Visible = false;
                this.cmbStatus.Visible = false;
                this.Label48.Visible = false;
                this.txtrelease.Visible = false;
                this.Button2.Visible = false;
                this.ChkCataloging.Checked = false;
                Label42.Visible = false;
                txtCategory.Value = "";
                this.lnkDelete.Visible = false;
                ChkCataloging.Visible = true;
                hdPublisherId.Value = "";
                // Call default_setting()
//                if (tmpcondition == "Y")
  //              {
                    // Me.cmdsave.Disabled = False
    //                btnS.Enabled = true;
      //          }

        //        else
          //      {
            //        btnS.Enabled = false;
                    // Me.cmdsave.Disabled = True
              //  }
                default_setting();
                this.chkAccDel.Visible = false;
                Label12.Text = Resources.ValidationResources.AccPfx;
                RBPreSuff.Checked = false;
                RBSuffix.Checked = false;
                var dbConnection = new OleDbConnection(retConstr(""));
                // Try
                // DDlPrefix_Suffix("prefixname", dbConnection)
                // Catch ex As Exception

                // Finally
                // dbConnection.Dispose()
                // End Try
                string Err;
                Err = AccPref(ref cmbaccessionprefix, dbConnection);
                if (!string.IsNullOrEmpty(Err))
                {
                    throw new ApplicationException(Err);
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void Rbaccession1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rbaccession1.Checked == true)
                {
                    var indentcon = new OleDbConnection(retConstr(""));
                   // this.SetFocus(cmbaccessionprefix);
                    Label12.Visible = true;
                    Label12.Text = Resources.ValidationResources.AccPfx;
                    // DDlPrefix_Suffix("prefixname", indentcon)
                    string Err;
                    Err = AccPref(ref cmbaccessionprefix, indentcon);
                    if (!string.IsNullOrEmpty(Err))
                    {
                        throw new ApplicationException(Err);
                    }
                    cmbaccessionprefix.Visible = true;
                    txtstartno.Visible = false;
                    Label43.Visible = true;
                    txtaccession.Visible = false;
                    Label22.Visible = false;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                    chkApplyRange.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void RBPreSuff_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBPreSuff.Checked == true)
                {
                    var indentcon = new OleDbConnection(retConstr(""));
           //         this.SetFocus(cmbaccessionprefix);
                    Label12.Visible = true;
                    Label12.Text = Resources.ValidationResources.LbPrefsuffix;
                    // DDlPrefix_Suffix("prefixname+'-'+suffixname", indentcon)
                    string Err;
                    Err = AccPref(ref cmbaccessionprefix, indentcon);
                    if (!string.IsNullOrEmpty(Err))
                    {
                        throw new ApplicationException(Err);
                    }
                    cmbaccessionprefix.Visible = true;
                    txtstartno.Visible = false;
                    Label43.Visible = true;
                    txtaccession.Visible = false;
                    Label22.Visible = false;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                    chkApplyRange.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void RBSuffix_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBSuffix.Checked == true)
                {
                    var indentcon = new OleDbConnection(retConstr(""));
                    //this.SetFocus(cmbaccessionprefix);
                    Label12.Visible = true;
                    Label12.Text = Resources.ValidationResources.Lbsuffix;
                    // DDlPrefix_Suffix("suffixname", indentcon)
                    string Err;
                    Err = AccPref(ref cmbaccessionprefix, indentcon);
                    if (!string.IsNullOrEmpty(Err))
                    {
                        throw new ApplicationException(Err);
                    }
                    cmbaccessionprefix.Visible = true;
                    txtstartno.Visible = false;
                    Label43.Visible = true;
                    txtaccession.Visible = false;
                    Label22.Visible = false;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                    chkApplyRange.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void Rbmanual1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rbmanual1.Checked == true)
                {

                    // Me.SetFocus(txtaccession)
                    Hidden17.Value = "manual";
                    txtaccession.Visible = true;
                    Label12.Visible = false;
                    cmbaccessionprefix.Visible = false;
                    txtstartno.Visible = false;
                    Label43.Visible = false;
                    Label22.Visible = true;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                    chkApplyRange.Visible = true;
                    this.chkApplyRange.Checked = false;
                    Label22.Text = Resources.ValidationResources.EntAccNoUseComma;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,  dbUtilities.MsgLevel.Failure);
            }
        }

        protected void btnCategoryFilter2_Click(object sender, EventArgs e)
        {
            string store;
            store = this.txtCategory.Value;
            var con = new OleDbConnection(retConstr(""));

            var bookds = new DataSet();
            string str;
            str = string.Empty;
            if (optpre.Checked == true & opttitle.Checked == true)
            {
                // str = "select accessionnumber as accno,case when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part<>' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Pt.' + existingbookkinfo.part +','+ 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part<>' ' and existingbookkinfo.edition='' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Pt.' + existingbookkinfo.part +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno=' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition='' and existingbookkinfo.language_id<>'' then  booktitle + '('+'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno=' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' else booktitle end as BookTitle,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2 from bookaccessionmaster,existingbookkinfo,translation_language where existingbookkinfo.srnoold=bookaccessionmaster.srnoold and translation_language.language_id=existingbookkinfo.language_id and bookaccessionmaster.ctrl_no='0' and bookaccessionmaster.booktitle like N'%" & Trim(txtCategory.Value) & "%' order by a1,a2"
                str = "select accessionnumber as accno,case when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part<>' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Pt.' + existingbookkinfo.part +','+ 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part<>' ' and existingbookkinfo.edition='' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Pt.' + existingbookkinfo.part +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno=' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition='' and existingbookkinfo.language_id<>'' then  booktitle + '('+'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno=' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' else booktitle end as BookTitle,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix from bookaccessionmaster,existingbookkinfo,translation_language where existingbookkinfo.srnoold=bookaccessionmaster.srnoold and translation_language.language_id=existingbookkinfo.language_id and bookaccessionmaster.ctrl_no='0' and bookaccessionmaster.booktitle like ? order by a1,a2,suffix";
            }
            else if (optpre.Checked == true & optaccno.Checked == true)
            {
                str = "select accessionnumber as accno,case when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part<>' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Pt.' + existingbookkinfo.part +','+ 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part<>' ' and existingbookkinfo.edition='' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Pt.' + existingbookkinfo.part +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno=' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition='' and existingbookkinfo.language_id<>'' then  booktitle + '('+'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno=' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' when existingbookkinfo.volumeno<>' ' and existingbookkinfo.part=' ' and existingbookkinfo.edition<>'' and existingbookkinfo.language_id<>'' then  booktitle + '(V.' + existingbookkinfo.volumeno + ',' + 'Ed.' + existingbookkinfo.edition +','+ 'Lang.' + translation_language.language_name + ')' else booktitle end as BookTitle,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix from bookaccessionmaster,existingbookkinfo,translation_language where existingbookkinfo.srnoold=bookaccessionmaster.srnoold and translation_language.language_id=existingbookkinfo.language_id and bookaccessionmaster.ctrl_no='0' and bookaccessionmaster.accessionnumber like ? order by a1,a2,suffix";
            }
            else if (optpost.Checked == true & optaccno.Checked == true)
            {
                str = "select accessionnumber as accno,case when CatalogueCardView.volume<>' ' and CatalogueCardView.part<>' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Pt.' + CatalogueCardView.part +','+ 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.part<>' ' and CatalogueCardView.edition='' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Pt.' + CatalogueCardView.part +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume=' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition='' and CatalogueCardView.language_id<>'' then  title + '('+'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume=' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(' + 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' else title end as BookTitle,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix from CatalogueCardView where CatalogueCardView.accessionnumber like ? order by a1,a2,suffix";
            }
            else if (optpost.Checked == true & opttitle.Checked == true)
            {
                str = "select accessionnumber as accno,case when CatalogueCardView.volume<>' ' and CatalogueCardView.part<>' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Pt.' + CatalogueCardView.part +','+ 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.part<>' ' and CatalogueCardView.edition='' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Pt.' + CatalogueCardView.part +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume=' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition='' and CatalogueCardView.language_id<>'' then  title + '('+'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume=' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(' + 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' when CatalogueCardView.volume<>' ' and CatalogueCardView.part=' ' and CatalogueCardView.edition<>'' and CatalogueCardView.language_id<>'' then  title + '(V.' + CatalogueCardView.volume + ',' + 'Ed.' + CatalogueCardView.edition +','+ 'Lang.' + CatalogueCardView.language_name + ')' else title end as BookTitle,dbo.pleft1(accessionnumber) as a1, cast (dbo.pright1(accessionnumber)  as numeric ) as a2,dbo.Suffix(accessionnumber) as suffix from CatalogueCardView where CatalogueCardView.Title like ? order by a1,a2,suffix";
            }
            con.Open();
            var bookda = new OleDbDataAdapter(str, con);
            bookda.SelectCommand.Parameters.AddWithValue("@booktitleAccNo", "%" + txtCategory.Value.Trim() + "%");
            bookda.Fill(bookds, "existingbookkinfo");
            con.Close();
            // bookds = libobj1.PopulateDataset(str, "existingbookkinfo", con)
            if (bookds.Tables["existingbookkinfo"].Rows.Count > 0)
            {
                int pg, count, tot_pg;
                pg = grddetail.PageSize;
                count = grddetail.CurrentPageIndex;
                tot_pg = bookds.Tables[0].Rows.Count;
//                if (count < Val[tot_pg] / Val[pg])
//                {
//  //                  grddetail.CurrentPageIndex = count;
//                }
//                else
//                {
////                    grddetail.CurrentPageIndex = Val[tot_pg] / Val[pg];
//                }
//                grddetail.CurrentPageIndex = 0;
                grddetail.DataSource = bookds;
                grddetail.DataBind();
                hdnGrdId.Value = grddetail.ClientID;
            }

            // Me.optpre.Enabled = False
            // Me.optpost.Enabled = False
            else
            {
                // Hidden2.Value = "record"
                // libobj1.MsgBox1(Resources.ValidationResources.rNotFound.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.rNotFound.ToString, Me)
                message.PageMesg(Resources.ValidationResources.rNotFound, this, dbUtilities.MsgLevel.Warning);
                grddetail.CurrentPageIndex = 0;
                var tab = new DataTable();
                grddetail.DataSource = tab;
                grddetail.DataBind();
                hdnGrdId.Value = grddetail.ClientID;

                this.optpre.Enabled = true;
                this.optpost.Enabled = true;
            }
            bookds.Dispose();
            bookda.Dispose();
         //   this.SetFocus(txtCategory);
        }

        protected void grddetail_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                // By kaushal :2-Mar-2012
                // -----------------------------------
                switch (((LinkButton)e.CommandSource).CommandName)
                {
                    case "Select":
                        {
                            var con = new OleDbConnection(retConstr(""));
                            con.Open();
                            var existds = new DataSet();
                            existds = libobj1.PopulateDataset("select existingbookkinfo.*,bookaccessionmaster.* from existingbookkinfo,bookaccessionmaster where bookaccessionmaster.srnoold=existingbookkinfo.srnoold and accessionnumber=N'" + grddetail.Items[e.Item.ItemIndex].Cells[1].Text + "'", "bookaccessionmaster", con);
                            // For Check It is Exist Or Not ?
                            if (existds.Tables[0].Rows.Count == 0)
                            {
                                // libobj1.MsgBox1(Resources.ValidationResources.directcataloguedmsg.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.directcataloguedmsg.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.directcataloguedmsg, this, dbUtilities.MsgLevel.Warning);
                                return;
                            }
                            if (existds.Tables[0].Rows.Count > 0)
                            {
                                this.txtAccDate.Text =  string.Format("{0:dd-MMM-yyyy}",Convert.ToDateTime (existds.Tables[0].Rows[0]["docDate"]));
                                cmbdept.SelectedValue = existds.Tables[0].Rows[0]["dept"].ToString();
                                txtkeywords.Value = existds.Tables[0].Rows[0]["keywords"].ToString();
                                txtSTitle.Text = existds.Tables[0].Rows[0]["booktitle"].ToString();
                                txtSubbooktitle.Value = existds.Tables[0].Rows[0]["subtitle"].ToString();
                                txtseriesname.Value = existds.Tables[0].Rows[0]["seriesname"].ToString();
                                txtfname1.Value = existds.Tables[0].Rows[0]["firstname1"].ToString();
                                txtfname2.Value = existds.Tables[0].Rows[0]["firstname2"].ToString();
                                txtfname3.Value = existds.Tables[0].Rows[0]["firstname3"].ToString();
                                txtlname1.Value = existds.Tables[0].Rows[0]["lastname1"].ToString();
                                txtlname2.Value = existds.Tables[0].Rows[0]["lastname2"].ToString();
                                txtlname3.Value = existds.Tables[0].Rows[0]["lastname3"].ToString() ;
                                txtmname1.Value = existds.Tables[0].Rows[0]["middlename1"].ToString();
                                txtmname2.Value = existds.Tables[0].Rows[0]["middlename2"].ToString();
                                txtmname3.Value = existds.Tables[0].Rows[0]["middlename3"].ToString();
                                txtedition.Value = existds.Tables[0].Rows[0]["edition"].ToString();
                                txteditionyear.Value = existds.Tables[0].Rows[0]["yearofedition"].ToString();
                                txtPubYear.Value = existds.Tables[0].Rows[0]["yearofPublication"].ToString();
                                txtvolumeno.Value = existds.Tables[0].Rows[0]["volumeno"].ToString();
                                txttovlume.Value = existds.Tables[0].Rows[0]["part"].ToString();
                                hdval1.Value = existds.Tables[0].Rows[0]["srnoold"].ToString();
                                txtisbn.Value = existds.Tables[0].Rows[0]["isbn"].ToString();
                                txtnoofcopies.Value = existds.Tables[0].Rows[0]["noofcopies"].ToString();
                                txtcopy.Value = existds.Tables[0].Rows[0]["no_of_pages"].ToString();
                                Txtpagesize.Value = "";// existds.Tables[0].Rows[0]["page_size"];
                                cmbLanguage.SelectedValue = existds.Tables[0].Rows[0]["Language_Id"].ToString();
                                txtprice.Value = existds.Tables[0].Rows[0]["price"].ToString();// IIf(Trim(existds.Tables[0].Rows[0]["price"]) == 0, string.Empty, existds.Tables[0].Rows[0]["price"]);

                                cmbdept.SelectedValue = existds.Tables[0].Rows[0]["dept"].ToString();
                                hdval.Value = existds.Tables[0].Rows[0]["srno"].ToString();

                                cmbpersontype.SelectedValue = existds.Tables[0].Rows[0]["authortype"].ToString();
                                // ''''''''''''''''''''''''''''''''''jeetendra''''''''''''''''''''
                                var pubds = new DataSet();
                                string sqlstr = "Select firstname+', '+percity as firstname  from  publishermaster,addresstable where publisherid=N'"+ existds.Tables[0].Rows[0]["publisherid"].ToString()+"' and publishermaster.publisherid=addresstable.addid and addresstable.addrelation=N'publisher'";
                                var cmd = new OleDbCommand(sqlstr, con);
                                string tmpstr = cmd.ExecuteScalar().ToString();
                                this.hdPublisherId.Value = existds.Tables[0].Rows[0]["publisherid"].ToString();
                                txtCmbPublisher.Text = tmpstr; // existds.Tables(0).Rows(0).Item("publisherid")
                                                               // asmpublisher.SelectedValue = existds.Tables(0).Rows(0).Item("publisherid")
                                cmbcurr.SelectedIndex = cmbcurr.Items.IndexOf(cmbcurr.Items.FindByText(existds.Tables[0].Rows[0]["FcurrencyCode"].ToString()));
                                // -------------------jeetendra---------------------------
                                string sqlstr1 = "Select vendorname+', '+percity as firstname  from  vendormaster,addresstable where vendorid=N'"+ existds.Tables[0].Rows[0]["vendor_id"].ToString()+ "' and vendormaster.vendorcode=addresstable.addid and addrelation=N'vendor'";
                                var cmd1 = new OleDbCommand(sqlstr1, con);
                                string tmpstr1 = cmd1.ExecuteScalar().ToString();
                                this.HdVendorid.Value = existds.Tables[0].Rows[0]["vendor_id"].ToString();
                                this.txtCmbVendor.Text = tmpstr1;
                                // asmvendor.SelectedValue = existds.Tables(0).Rows(0).Item("vendor_id")
                                HdVendorid.Value = existds.Tables[0].Rows[0]["vendor_id"].ToString();
                                // -------------------jeetendra---------------------------
                                // If Val(existds.Tables(0).Rows(0).Item("vendor_id")) = 0 Then
                                // cmbvendor1.SelectedIndex = cmbvendor1.Items.Count - 1
                                // Else
                                // cmbvendor1.SelectedValue = existds.Tables(0).Rows(0).Item("vendor_id")
                                // End If
                                txtForeignPrice.Value = existds.Tables[0].Rows[0]["Fprice"].ToString();//  IIf(Trim(existds.Tables[0].Rows[0]["Fprice"]) == 0, string.Empty, existds.Tables[0].Rows[0]["Fprice"]); // existds.Tables(0).Rows(0).Item("Fprice")
                                txtspecialprice.Value = existds.Tables[0].Rows[0]["specialprice"].ToString();
                                txtExchangeRate.Value = existds.Tables[0].Rows[0]["exchange_rate"].ToString();
                                cmbcategory.SelectedValue = existds.Tables[0].Rows[0]["category"].ToString();
                                cmbmediatype.SelectedValue = existds.Tables[0].Rows[0]["mediatype"].ToString();
                                // cmdsave.Value = Resources.ValidationResources.bUpdate.ToString
                                btnS.Text = Resources.ValidationResources.bUpdate;
                                if (existds.Tables[0].Rows[0]["ctrl_no"].ToString()=="0")
                                {
                                    lnkContinue.Visible = true;
                                    lnkModify.Visible = true;
                                    this.lnkDelete.Visible = true;
                                }
                                else
                                {
                                    Rbaccession1.Checked = true;
                                    OptAccWOP.Checked = false;
                                    // Manualaccession.Checked = False
                                    Rbmanual1.Checked = false;
                                    Rbaccession1.Visible = false;
                                    OptAccWOP.Visible = false;
                                    // Manualaccession.Visible = False
                                    Rbmanual1.Visible = false;
                                    Label22.Visible = false;
                                    txtaccession.Visible = false;
                                    lblaccrange.Visible = false;
                                    cmbaccrange.Visible = false;
                                    chkApplyRange.Visible = false;
                                    Label12.Visible = false;
                                    Label43.Visible = false;
                                    cmbaccessionprefix.Visible = false;
                                    lnkContinue.Visible = true;
                                    lnkModify.Visible = false;
                                    this.lnkDelete.Visible = false;
                                    ChkCataloging.Visible = false;
                                    // libobj1.MsgBox1(Resources.ValidationResources.EdtgNotPAfrCtlgPmd.ToString, Me)
                                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.EdtgNotPAfrCtlgPmd.ToString, Me)
                                    message.PageMesg(Resources.ValidationResources.EdtgNotPAfrCtlgPmd, this, dbUtilities.MsgLevel.Warning);
                                }
                            }
                            // If optpost.Checked = True Then
                            // lnkModify.Visible = False
                            // Me.lnkDelete.Visible = False
                            // libobj1.msgbox1(Resources.ValidationResources.EdtgNotPAfrCtlgPmd.ToString, Me)
                            // Else
                            // Rbaccession1.Enabled = True
                            // Manualaccession.Enabled = True
                            // Rbmanual1.Enabled = True
                            // hdvol.Value = Nothing
                            // End If
                            txtnoofcopies.Disabled = true;
                            // cmdsave.Disabled = True
                            btnS.Enabled = false;
                            optaccno.Visible = false;
                            optpost.Visible = false;
                            optpre.Visible = false;
                            opttitle.Visible = false;
                            CheckBox1.Checked = false;
                            btnCategoryFilter.Visible = false;
                            Label44.Visible = false;
                            Label45.Visible = false;
                            txtCategory.Visible = false;
                            grddetail.Visible = false;
                            Label42.Visible = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this,  dbUtilities.MsgLevel.Failure);
            }
            finally
            {
            }
        }

        protected void lnkContinue_Click(object sender, EventArgs e)
        {
            try
            {
                lnkContinue.Visible = false;
                lnkModify.Visible = false;
                this.lnkDelete.Visible = false;
                // cmdsave.Disabled = False
                btnS.Enabled = true;

                // cmdsave.Value = Resources.ValidationResources.bSave.ToString
                // cmdsave.Disabled = False
                btnS.Text = Resources.ValidationResources.bSave;
                btnS.Enabled = true;
                txtnoofcopies.Disabled = false;//== Resources.ValidationResources.bSave;

                Rbaccession1.Checked = true;
                this.RBPreSuff.Checked = false;
                this.RBSuffix.Checked = false;
                OptAccWOP.Checked = false;
                // Manualaccession.Checked = False
                Rbmanual1.Checked = false;
                Rbaccession1.Visible = true;
                OptAccWOP.Visible = true;
                // Manualaccession.Visible = True
                Rbmanual1.Visible = true;
                Label12.Visible = true;
                Label43.Visible = true;
                cmbaccessionprefix.Visible = true;
                txtaccession.Visible = false;
                Label22.Visible = false;
                lblaccrange.Visible = false;
                cmbaccrange.Visible = false;
                chkApplyRange.Visible = false;
                Label12.Visible = true;
                Label43.Visible = true;
                this.txtClass.Value = string.Empty;
                this.txtBook.Value = string.Empty;
                this.txtClass.Visible = false;
                this.txtBook.Visible = false;
                this.Label40.Visible = false;
                this.Label41.Visible = false;
                this.Label47.Visible = false;
                this.cmbStatus.Visible = false;
                this.Label48.Visible = false;
                this.txtrelease.Visible = false;
                this.Button2.Visible = false;
                this.ChkCataloging.Checked = false;
                this.ChkCataloging.Visible = true;
                Label42.Visible = false;
                cmbaccessionprefix.Visible = true;
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtCategory.Value = "";
            this.chkAccDel.Visible = false;
            this.Label36.Visible = false;
            try
            {
                if (CheckBox1.Checked == true)
                {
                    optaccno.Visible = true;
                    optpost.Visible = true;
                    optpre.Visible = true;
                    opttitle.Visible = true;
                    btnCategoryFilter.Visible = true;
                    Label44.Visible = true;
                    Label45.Visible = true;
                    txtCategory.Visible = true;
                    grddetail.Visible = true;
                    Label42.Visible = true;
                    var tab = new DataTable();
                    grddetail.DataSource = tab;
                    grddetail.DataBind();
                    hdnGrdId.Value = grddetail.ClientID;


                  //  this.SetFocus(txtCategory);
                }
                else
                {
                  //  this.SetFocus(CheckBox1);
                    optaccno.Visible = false;
                    optpost.Visible = false;
                    optpre.Visible = false;
                    opttitle.Visible = false;
                    btnCategoryFilter.Visible = false;
                    Label44.Visible = false;
                    Label45.Visible = false;
                    txtCategory.Visible = false;
                    var tab = new DataTable();
                    grddetail.DataSource = tab;
                    grddetail.DataBind();
                    hdnGrdId.Value = grddetail.ClientID;

                    grddetail.Visible = false;
                    Label42.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void opttitle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtCategory.Value = "";
                grddetail.CurrentPageIndex = 0;
                Label42.Text = Resources.ValidationResources.LBookTitle;

              //  this.SetFocus(txtCategory);
            }

            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void optaccno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtCategory.Value = "";
                grddetail.CurrentPageIndex = 0;
                Label42.Text = Resources.ValidationResources.AccessionNumber;
              //  this.SetFocus(txtCategory);
            }

            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void OptAccWOP_CheckedChanged(object sender, EventArgs e)
        {
            //this.SetFocus(OptAccWOP);
            try
            {
                this.cmbaccessionprefix.Visible = false;
                this.Label12.Visible = false;
                this.Label43.Visible = false;
                var con = new OleDbConnection(retConstr(""));
                con.Open();
                this.txtstartno.Value = libobj1.populateCommandText("select currentposition from prefixmaster where prefixid=0", con);
                con.Close();
                Label22.Visible = false;
                txtaccession.Visible = false;
                lblaccrange.Visible = false;
                cmbaccrange.Visible = false;
                chkApplyRange.Visible = false;
            }

            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcopy.Checked == true)
            {

                //this.SetFocus(chkcopy);
                lblcopy.Visible = true;

           //     this.SetFocus(txtcopyno);
                txtcopyno.Visible = true;

                lblcopy.Text = Resources.ValidationResources.MSGEnterCopyNumber;
            }
            else
            {

               // this.SetFocus(chkcopy);
                lblcopy.Visible = false;
                txtcopyno.Visible = false;
                txtcopyno.Value = "";
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var da = new OleDbDataAdapter("select distinct accessionnumber from bookaccessionmaster where srNoOld='" + hdval1.Value + "'", con);
            var ds = new DataSet();
            da.Fill(ds);
            this.chkAccDel.Visible = true;
            chkAccDel.Items.Clear();
            chkAccDel.DataSource = ds;
            chkAccDel.DataTextField = "accessionnumber";
            chkAccDel.DataValueField = "accessionnumber";
            chkAccDel.DataBind();
            da.Dispose();
            ds.Dispose();
            lnkContinue.Visible = false;
            lnkModify.Visible = false;
            this.lnkDelete.Visible = false;
            this.Label36.Visible = true;
            Rbaccession1.Checked = true;
            this.RBPreSuff.Checked = false;
            this.RBSuffix.Checked = false;
            OptAccWOP.Checked = false;
            // Manualaccession.Checked = False
            Rbmanual1.Checked = false;
            Rbaccession1.Visible = false;
            OptAccWOP.Visible = false;
            // Manualaccession.Visible = False
            this.txtClass.Value = string.Empty;
            this.txtBook.Value = string.Empty;
            this.txtClass.Visible = false;
            this.txtBook.Visible = false;
            this.Label40.Visible = false;
            this.Label41.Visible = false;
            this.Label47.Visible = false;
            this.cmbStatus.Visible = false;
            this.Label48.Visible = false;
            this.txtrelease.Visible = false;
            this.Button2.Visible = false;
            this.ChkCataloging.Checked = false;
            Label42.Visible = false;
            Rbmanual1.Visible = false;
            Label22.Visible = false;
            txtaccession.Visible = false;
            lblaccrange.Visible = false;
            cmbaccrange.Visible = false;
            chkApplyRange.Visible = false;
            Label12.Visible = false;
            Label43.Visible = false;
            cmbaccessionprefix.Visible = false;
            this.cmddelete.Disabled = false;
        }

        protected void cmddelete2_Click(object sender, EventArgs e)
        {
            var delcon = new OleDbConnection(retConstr(""));
            delcon.Open();
            var delcom = new OleDbCommand();
            delcom.CommandType = CommandType.Text;
            delcom.Connection = delcon;
            var arr = new ArrayList();
            string str = null;
            int j = 0;
            foreach (ListItem i in this.chkAccDel.Items)
            {
                if (i.Selected == true)
                {
                    arr.Add(i.Text);
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += "," + i.Value;
                    }
                    else
                    {
                        str = i.Value;
                    }
                    delcom.CommandText = "delete from bookaccessionmaster where accessionnumber =N'" + i.Value + "'";
                    delcom.CommandType = CommandType.Text;
                    delcom.ExecuteNonQuery();
                    delcom.Parameters.Clear();
                    delcom.CommandType = CommandType.Text;
                    delcom.CommandText = "update existingbookkinfo set noofcopies=noofcopies-1 where srNoOld='" + hdval1.Value + "'";
                    delcom.ExecuteNonQuery();
                    delcom.Parameters.Clear();
                    string str1;
                    str1 = "select * from bookaccessionmaster where srNoOld='" + hdval1.Value + "'";
                    delcom.CommandText = str1;
                    var delad = new OleDbDataAdapter();
                    delad.SelectCommand = delcom;
                    var delds = new DataSet();
                    delad.Fill(delds, "DelDS");
                    if (delds.Tables[0].Rows.Count == 0)
                    {
                        delcom.Parameters.Clear();
                        delcom.CommandText = "delete from existingbookkinfo where srNoOld ='" + hdval1.Value + "'";
                        delcom.CommandType = CommandType.Text;
                        delcom.ExecuteNonQuery();
                        if (LoggedUser.Logged().IsAudit=="Y")
                        {
                            if (cmddelete2.Text == Resources.ValidationResources.bDelete)
                            {
                                LibObj1 .insertLoginFunc(LoggedUser.Logged().UserName, lblTitle.Text, LoggedUser.Logged().Session, txtkeywords.Value.Trim(), Resources.ValidationResources.bDelete, retConstr(""));
                            }
                        }
                        delcom.Parameters.Clear();
                    }
                }
                else
                {
                    arr.Add("");
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                // libobj1.MsgBox1(Resources.ValidationResources.MSGAcessionNumberDeleted.ToString & str & "", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.MSGAcessionNumberDeleted.ToString, Me)
                message.PageMesg(Resources.ValidationResources.MSGAcessionNumberDeleted, this, dbUtilities.MsgLevel.Warning);
                this.chkAccDel.Visible = false;
                refreshfield();
                this.cmddelete.Disabled = true;
                // Me.cmdsave.Disabled = False

                btnS.Enabled = true;
            }
            else
            {
                // libobj1.MsgBox1(Resources.ValidationResources.SelFld.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelFld.ToString, Me)
                message.PageMesg(Resources.ValidationResources.SelFld, this, dbUtilities.MsgLevel.Warning);
                return;
            }
        }

        protected void ChkCataloging_CheckedChanged(object sender, EventArgs e)
        {
            var conn = new OleDbConnection(retConstr(""));
            conn.Open();
            if (ChkCataloging.Checked == true)
            {

               // this.SetFocus(txtClass);
                this.txtClass.Value = string.Empty;
                this.txtBook.Value = string.Empty;
                this.txtClass.Visible = true;
                this.txtBook.Visible = true;
                this.Label40.Visible = true;
                this.Label41.Visible = true;
                this.Label47.Visible = true;
                this.cmbStatus.Visible = true;
                this.Label48.Visible = true;
                this.txtrelease.Visible = true;
                this.Button2.Visible = true;
                libobj1.populateDDL(cmbStatus, "select ItemStatusID,ItemStatus from Itemstatusmaster order by itemstatus", "Itemstatus", "Itemstatusid", "", conn);
                cmbStatus.Items.Remove(cmbStatus.Items[cmbStatus.Items.Count - 1]);
                cmbStatus.SelectedIndex = -1;
                if (cmbStatus.SelectedIndex >= 0)
                {
                    string qry_str;
                    qry_str = "select isBardateApllicable FROM ITEMSTATUSMASTER Where Itemstatusid=" + this.cmbStatus.SelectedItem.Value;
                    var da = new OleDbDataAdapter(qry_str, conn);
                    var ds = new DataSet();
                    da.Fill(ds, "load");
                    string isbardate = string.Empty;
                    if (ds.Tables["load"].Rows.Count > 0)
                    {
                        isbardate =ds.Tables["load"].Rows[0]["isBardateApllicable"].ToString();
                    }
                    if (isbardate == "Y")
                    {
                        Label48.Visible = true;
                        txtrelease.Visible = true;
                        this.Button2.Visible = true;
                    }
                    else
                    {
                        Label48.Visible = false;
                        txtrelease.Visible = false;
                        Button2.Visible = false;
                    }
                }
                else
                {
                    Label48.Visible = false;
                    txtrelease.Visible = false;
                    Button2.Visible = false;
                }
            }
            else
            {

              //  this.SetFocus(ChkCataloging);
                this.txtClass.Value = string.Empty;
                this.txtBook.Value = string.Empty;
                this.txtClass.Visible = false;
                this.txtBook.Visible = false;
                this.Label40.Visible = false;
                this.Label41.Visible = false;
                this.Label47.Visible = false;
                this.cmbStatus.Visible = false;
                this.Label48.Visible = false;
                this.txtrelease.Visible = false;
                this.Button2.Visible = false;
            }
            conn.Close();
            conn.Dispose();
        }

        protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            var conn = new OleDbConnection(retConstr(""));
            conn.Open();
            string qry_str;
            qry_str = "select isBardateApllicable FROM ITEMSTATUSMASTER Where Itemstatusid=" + this.cmbStatus.SelectedItem.Value;
            var da = new OleDbDataAdapter(qry_str, conn);
            var ds = new DataSet();
            da.Fill(ds, "load");
            string isbardate = string.Empty;
            if (ds.Tables["load"].Rows.Count > 0)
            {
                isbardate =ds.Tables["load"].Rows[0]["isBardateApllicable"].ToString();
            }
            if (isbardate == "Y")
            {
                Label48.Visible = true;
                txtrelease.Visible = true;
                this.Button2.Visible = true;
            }
            else
            {
                Label48.Visible = false;
                txtrelease.Visible = false;
                this.Button2.Visible = false;
            }
        }

        protected void chkApplyRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chkApplyRange.Checked == true)
                {

                 //   this.SetFocus(chkApplyRange);
                    this.Label22.Text = Resources.ValidationResources.EntAccNoUseDash;
                    lblaccrange.Visible = true;
                    cmbaccrange.Visible = true;
                }
                else
                {

               //     this.SetFocus(chkApplyRange);
                    this.Label22.Text = Resources.ValidationResources.EntAccNoUseComma;
                    lblaccrange.Visible = false;
                    cmbaccrange.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
        public void AllowInsertion()
        {
            AllowIn=true;
           
            if (AllowIn == false)
            {
                object flag = false;
                string constr = System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString();
                constr = retConstr("");//  ConfigurationManager.ConnectionStrings(constr).ConnectionString;
                var da = new OleDbDataAdapter("select count(*) from bookaccessionmaster", constr);
                var ds = new DataSet();
                da.Fill(ds);
                //if (Convert.ToInt64(ds.Tables[0].Rows[0][0]) + Convert.ToInt32(txtnoofcopies.Value) < LibRescObj.BookRestriction())
                //{
                //    flag = true;
                //}
                //else
                //{
                //    flag = false;
                //}
                //AllowIn = Conversions.ToBoolean(flag);
            }
        }

        protected void btnS_Click(object sender, EventArgs e)
        {
            if (this.txtrelease.Visible == true & txtrelease.Text == string.Empty)
            {
                // libobj1.MsgBox1(Resources.ValidationResources.BarD, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.BarD.ToString, Me)
                message.PageMesg(Resources.ValidationResources.BarD, this, dbUtilities.MsgLevel.Warning);

              //  this.SetFocus(txtrelease);
                return;
            }

            var copynumber = default(int);
            string tcase="Y";
//            tcase = AppSettings.Get("tcase");
            var indentcon = new OleDbConnection(retConstr(""));
            var indentcom = new OleDbCommand();
            indentcom.Connection = indentcon;
            indentcon.Open();
            // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
            if (btnS.Text == Resources.ValidationResources.bSave)
            {
                if (cmbaccessionprefix.SelectedValue == HComboSelect.Value && Rbaccession1.Checked == true && Rbmanual1.Checked == false & this.OptAccWOP.Checked == false)
                {
                    // libobj1.MsgBox1(Resources.ValidationResources.SelAccPfx.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelAccPfx.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SelAccPfx, this, dbUtilities.MsgLevel.Warning);
                  //  this.SetFocus(cmbaccessionprefix);
                    return;
                }

                else if (cmbaccessionprefix.SelectedValue == HComboSelect.Value && this.RBPreSuff.Checked == true && this.OptAccWOP.Checked == false)
                {
                    // libobj1.MsgBox1(Resources.ValidationResources.SelAccPfxSfx.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelAccPfxSfx.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SelAccPfxSfx, this, dbUtilities.MsgLevel.Warning);
               //     this.SetFocus(cmbaccessionprefix);
                    return;
                }

                else if (cmbaccessionprefix.SelectedValue == HComboSelect.Value && this.RBSuffix.Checked == true && this.OptAccWOP.Checked == false)
                {
                    // libobj1.MsgBox1(Resources.ValidationResources.SelAccSfx.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.SelAccSfx.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.SelAccSfx, this, dbUtilities.MsgLevel.Warning);
                    //this.SetFocus(cmbaccessionprefix);
                    return;
                }
            }

            // ---------------By kaushal 20,10-14
            string A1 = hdPublisherId.Value;
            string A2 = txtCmbPublisher.Text;
            // If asmpublisher.SelectedValue = Nothing Or txtCmbPublisher.Text.Trim() = Nothing Then
            if (string.IsNullOrEmpty(A1))
            {
                if (hdPublisherId.Value == "")
                {
                    // libobj1.MsgBox1(Resources.ValidationResources.IPubNotExist.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.IPubNotExist.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.IPubNotExist, this, dbUtilities.MsgLevel.Warning);
                    // txtCmbPublisher.Focus()
                    // SetFocus(txtCmbPublisher)
                    hdPublisherId.Value = "";
                    return;

                }
            }
            else
            {
                // If hdPublisherId.Value = "" Then
                // hdPublisherId.Value = asmpublisher.SelectedValue
                // If Not asmpublisher.SelectedValue = Nothing And A1 = "" Then
                // hdPublisherId.Value = asmpublisher.SelectedValue
                // End If
                // End If
            }

            string V1 = HdVendorid.Value;
            // If asmvendor.SelectedValue = Nothing Or txtCmbVendor.Text.Trim() = Nothing Then
            if (string.IsNullOrEmpty(V1))
            {
                // libobj1.MsgBox1(Resources.ValidationResources.VendorNotFound.ToString, Me)
                if (HdVendorid.Value == "")
                {

                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.VendorNotFound.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.VendorNotFound, this, dbUtilities.MsgLevel.Warning);
                    // txtCmbVendor.Focus()
                    // SetFocus(txtCmbVendor)
                    HdVendorid.Value = "";
                    return;

                }
            }

            else
            {

            }
            if (HdVendorid.Value == "")
            {
                // HdVendorid.Value = asmvendor.SelectedValue
            }


            var SaveAccNumbercon = new OleDbConnection(retConstr(""));
            SaveAccNumbercon.Open();

            string tmpstr;
            tmpstr = libobj1.populateCommandText("select isnull(max(accessionid),0)+1 id from bookaccessionmaster", SaveAccNumbercon);
            txtaccid.Value = tmpstr;// == "0"? "1":  (Convert.ToInt32( tmpstr) + 1).ToString();

            OleDbTransaction tran;
            bool check;
            check = false;
            tran = SaveAccNumbercon.BeginTransaction();
            var accessioning = default(string);
            Session["startingnumberforacc"] = txtstartno.Value;

            var SaveAccCom = new OleDbCommand();
            SaveAccCom.Connection = SaveAccNumbercon;
            SaveAccCom.Transaction = tran;

            // volume calculation present or not

            int i;
            int noofcopies;
            string accno = string.Empty;
            i = 0;
            noofcopies = Convert.ToInt32(txtnoofcopies.Value);
            System.DateTime d;
            d = System.DateTime.Today;
            long mx;
            mx = 0L;
            int no, no1;
            int ddd;
            // .......................................
            tmpstr = libobj1.populateCommandText("select isnull(max(accessionid),0)+1 accnid from bookaccessionmaster", indentcon);
            txtaccid.Value = tmpstr;// IIf(Val(tmpstr) == 0, 1, Val(tmpstr) + 1);
            check = false;
            d = System.DateTime.Today;
            mx = 0L;
            try
            {
                // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
                if (btnS.Text == Resources.ValidationResources.bSave)
                {
                    SaveAccCom.CommandType = CommandType.Text;
                    // '''''''''''''''''''''''''SRNO----------------------------
                    SaveAccCom.CommandText = "select isnull(max(srno),0)+1 srno  from bookaccessionmaster";
                    ddd = Convert.ToInt32(SaveAccCom.ExecuteScalar());
                    ddd = ddd + 1;  // serial no. + 1
                                    // '''''''''''''''''''''''''SRNO----------------------------

                    SaveAccCom.Parameters.Clear();
                    SaveAccCom.CommandType = CommandType.Text;
                    // '''''''''''''''''''''''''SRNOOLD----------------------------
                    SaveAccCom.CommandText = "select isnull(max(srnoold),0)+1 srnold  from bookaccessionmaster";
                    no = Convert.ToInt32(SaveAccCom.ExecuteScalar());
                    no = no + 1;
                    no1 = no;
                    // '''''''''''''''''''''''''SRNOOLD----------------------------
                    SaveAccCom.Parameters.Clear();
                }
                else
                {
                    no = Convert.ToInt32( hSubmit1.Value);                 // SrnoOld of a selected accessionnumber from grid
                    no1 = Convert.ToInt32( hSubmit1.Value);
                    // ddd = hCurrentIndex2.Value          'SRNO of selected accessionnumber from grid
                    ddd = Convert.ToInt32( hdval.Value);
                }

                var accno_id = default(int);


                // If cmdsave.Value = Resources.ValidationResources.bUpdate.ToString Then
                if (btnS.Text == Resources.ValidationResources.bUpdate)
                {
                    SaveAccCom.CommandType = CommandType.Text;
                    SaveAccCom.CommandText = "select accessionid from bookaccessionmaster where srnoold = '" + no + "' order by accessionid";
                    var ad = new OleDbDataAdapter();
                    var ds = new DataSet();
                    ad.SelectCommand = SaveAccCom;
                    ad.Fill(ds, "FDF");
                    accno_id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);  // Accessionid of searched record
                    SaveAccCom.Parameters.Clear(); // 
                }
                // ...................by Kaushal B <Oct,18-14>
                int icnt, Ncopy;
                bool @bool = false;
                var str_man = default(string);
                System.Array array;
                int nofcopiesX= Convert.ToInt32(txtnoofcopies.Value);
                nofcopiesX = nofcopiesX * 1;
                
               var accNoX= txtaccession.Value ;
                string accstr = libobj1.GenerateAccFinal(retConstr(""), cmbaccessionprefix, Rbaccession1, RBSuffix, RBPreSuff, Rbmanual1, nofcopiesX, accNoX, chkApplyRange, cmbaccrange);
                nofcopiesX = nofcopiesX * 1;
                if (string.IsNullOrEmpty(accstr.Trim()))
                {
                    // libobj1.MsgBox1(" missing some parameters in the Generateaccno funtion , contact msspl technical team", Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, " missing some parameters in the Generateaccno funtion , contact msspl technical team", Me)
                    message.PageMesg(" missing some parameters in the Generateaccno funtion , contact msspl technical team", this, dbUtilities.MsgLevel.Warning);
                    tran.Rollback();
                    SaveAccNumbercon.Close();
                    return;
                }
                var arrayx = accstr.Split(',');
                var x=arrayx[0];
                if (arrayx[0].ToString().ToLower() == "k")
                {
                    // libobj1.MsgBox1(array(1).ToString(), Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, array(1).ToString(), Me)
                    message.PageMesg(arrayx[1], this, dbUtilities.MsgLevel.Warning);
                    tran.Rollback();
                    SaveAccNumbercon.Close();
                    return;
                }

                Ncopy = arrayx.Length;// + 1;
                int i_n = 0;
                while (i_n <= Ncopy - 1)
                {
                    var loopTo = Convert.ToInt32(txtnoofcopies.Value) - 1;
                    for (icnt = 0; icnt <= loopTo; icnt++)
                    {
                        // If Rbmanual1.Checked = True Then
                        if (arrayx.Length > 0)
                        {
                            str_man = arrayx[i_n];
                            if (noofcopies - Ncopy != 0)
                            {
                                // libobj1.MsgBox1(Resources.ValidationResources.AccNoCpyNotEql.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.AccNoCpyNotEql.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.AccNoCpyNotEql, this, dbUtilities.MsgLevel.Warning);
                                return;
                            }
                            SaveAccCom.CommandType = CommandType.Text;
                            SaveAccCom.CommandText = "select accessionnumber from bookaccessionmaster where accessionnumber=N'" + str_man + "' ";
                            var ad = new OleDbDataAdapter();
                            var ds = new DataSet();
                            ad.SelectCommand = SaveAccCom;
                            ad.Fill(ds);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                @bool = true;
                                Hdexistaccession.Value = str_man;
                                // Hidden2.Value = "99"E_newss
                                // libobj1.MsgBox1(Resources.ValidationResources.AccNo.ToString & Hdexistaccession.Value & " " & Resources.ValidationResources.AlrdyExst.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.AccNo.ToString & Hdexistaccession.Value & " " & Resources.ValidationResources.AlrdyExst.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.AccNo + Hdexistaccession.Value + " " + Resources.ValidationResources.AlrdyExst, this, dbUtilities.MsgLevel.Warning);
                                // tran.Rollback()
                                return;
                            }
                        }


                        SaveAccCom.Parameters.Clear();
                        // SaveAccCom.CommandType = CommandType.StoredProcedure
                        // SaveAccCom.CommandText = "insert_bookaccessionmaster_1"
                        // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
                        if (btnS.Text == Resources.ValidationResources.bSave)
                        {

                            HDAccesionIdNew.Value = (Convert.ToInt32(txtaccid.Value) + icnt).ToString();
                        }
                        else
                        {

                            HDAccesionIdNew.Value = (accno_id + icnt).ToString();

                        }
                        AllowInsertion();
                        if (AllowIn == true)
                        {
                            var Ssave = ObjBookAccession.InsertBookAccession(
                                str_man, "0", "0", cmbform.SelectedItem.Value, Convert.ToInt32( HDAccesionIdNew.Value), System.DateTime.Today, 
                                txtSTitle.Text.Trim(), ddd, "e", 
                                Convert.ToDecimal(txtprice.Value), no, "",   txtAccDate.Text, "Books", 0.0, cmbcurr.SelectedItem.Text,
                                LoggedUser.Logged().User_Id, txtCmbVendor.Text,Convert.ToInt32( cmbdept.SelectedValue), 0, cmbdept.SelectedItem.Text, SaveAccCom);
                            if (Ssave == false)
                            {
                                tran.Rollback();
                                SaveAccNumbercon.Close();
                                // libobj1.MsgBox("Operation not completed!", Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Operation not completed!", Me)
                                message.PageMesg("Operation not completed, Accessioning failed!", this, dbUtilities.MsgLevel.Warning);
                                return;
                            }
                        }
                        else
                        {
                            tran.Rollback();
                            SaveAccNumbercon.Close();
                            // libobj1.MsgBox("Book Accessionned Limit exceeded.", Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Book Accessionned Limit exceeded.", Me)
                            message.PageMesg("Book Accessionned Limit exceeded.", this, dbUtilities.MsgLevel.Warning);
                            return;
                        }

                        SaveAccCom.Parameters.Clear();
                        Hidden5.Value = str_man + "," + Hidden5.Value;
                        // End If

                        if (this.ChkCataloging.Checked == true)
                        {
                            // Generate Control No.
                            SaveAccCom.CommandType = CommandType.Text;
                            SaveAccCom.CommandText = "select prefix,currentposition,suffix from IdTable where objectName=N'catalog'";
                            var dr = SaveAccCom.ExecuteReader();
                            dr.Read();
                            hdCtrlNo.Value = (Convert.ToInt32(dr[0]) + 1).ToString();// Operators.ConcatenateObject(Operators.ConcatenateObject(dr[0], Operators.AddObject(dr[1], 1)), dr[2]);

                            dr.Close();

                            isNewCtrl = true;
                            // Data insert into Catalog Data Table
                            SaveAccCom.CommandType = CommandType.StoredProcedure;
                            SaveAccCom.CommandText = "insert_CatalogData_1";

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
                            SaveAccCom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@classnumber_2", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@classnumber_2"].Value = txtClass.Value.Trim();

                            SaveAccCom.Parameters.Add(new OleDbParameter("@booknumber_3", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@booknumber_3"].Value = txtBook.Value.Trim();

                            SaveAccCom.ExecuteNonQuery();
                            SaveAccCom.Parameters.Clear();

                            SaveAccCom.CommandType = CommandType.Text;
                            // If asmpublisher.SelectedValue = Nothing Then
                            SaveAccCom.CommandText = "Select firstname,peraddress,percity,perstate,percountry from AddressTable,PublisherMaster where  addid=N'" + hdPublisherId.Value + "' and Addid=Publisherid and addrelation=N'publisher'";
                            // Else
                            // SaveAccCom.CommandText = "Select firstname,peraddress,percity,perstate,percountry from AddressTable,PublisherMaster where  addid=N'" & Val(asmpublisher.SelectedValue) & "' and Addid=Publisherid and addrelation=N'publisher'"
                            // End If
                            var Aad = new OleDbDataAdapter();
                            var Ads = new DataSet();
                            Aad.SelectCommand = SaveAccCom;
                            Aad.Fill(Ads);
                            if (Ads.Tables[0].Rows.Count > 0)
                            {
                                this.hdFirst.Value = Ads.Tables[0].Rows[0]["firstname"].ToString();
                                this.hdAddress.Value = Ads.Tables[0].Rows[0]["peraddress"].ToString();
                                this.hdCity.Value = Ads.Tables[0].Rows[0]["percity"].ToString();
                                this.hdstate.Value = Ads.Tables[0].Rows[0]["perstate"].ToString();
                                this.hdCountry.Value = Ads.Tables[0].Rows[0]["percountry"].ToString();
                            }
                            // If asmpublisher.SelectedValue = Nothing Then
                            HDPubCode.Value = hdPublisherId.Value;  // Session("publisher")
                                                                         // Else
                                                                         // HDPubCode.Value = Val(asmpublisher.SelectedValue)  'Session("publisher")
                                                                         // End If
                          
                            if (ObjCatalog.InsertFunction(Convert.ToInt32(hdCtrlNo.Value), System.DateTime.Now, Convert.ToInt32( this.cmbcategory.SelectedValue), 
                                txtvolumeno.Value, string.Empty, txtcopy.Value.Trim()==""? 0:Convert.ToInt32( txtcopy.Value), string.Empty, 0,
                                string.Empty, txtSTitle.Text,Convert.ToInt32( HDPubCode.Value), txtedition.Value , txtisbn.Value.Trim(),
                                 string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
                                 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
                                 this.txtvolumeno.Value,Convert.ToInt32( cmbdept.SelectedValue),Convert.ToInt32( cmbLanguage.SelectedValue),"", txttovlume.Value, 
                                 "","", txtCmbPublisher.Text,hdCity.Value, hdstate.Value, this.hdCountry.Value, hdAddress.Value, cmbdept.SelectedItem.Text, string.Empty, 
                                 cmbLanguage.SelectedItem.Text, "Books", 0,"", SaveAccCom) == true)                           
                        {
                            }

                            else
                            {
                                tran.Rollback();
                                SaveAccNumbercon.Close();
                                // msglabel.Text = "Failed on catalog save."
                                // msglabel.ForeColor = System.Drawing.Color.Red
                                // msglabel.Visible = True
                                message.PageMesg("Failed on catalog save", this, dbUtilities.MsgLevel.Failure);
                                return;


                            }


                            SaveAccCom.Parameters.Clear();

                            SaveAccCom.CommandText = "insert_BookAuthor_1";
                            SaveAccCom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));

                            SaveAccCom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@firstname1_2", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@firstname1_2"].Value = libobj1.TitleCase(this.txtfname1.Value);

                            SaveAccCom.Parameters.Add(new OleDbParameter("@middlename1_3", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@middlename1_3"].Value = libobj1.TitleCase(this.txtmname1.Value);

                            SaveAccCom.Parameters.Add(new OleDbParameter("@lastname1_4", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@lastname1_4"].Value = libobj1.TitleCase(this.txtlname1.Value);

                            SaveAccCom.Parameters.Add(new OleDbParameter("@firstname2_5", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@firstname2_5"].Value = this.txtfname2.Value;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@middlename2_6", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@middlename2_6"].Value = txtmname2.Value;// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtmname2.Value.ToString)), Trim(this.txtmname2.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@lastname2_7", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@lastname2_7"].Value = this.txtlname2.Value;// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtlname2.Value.ToString)), Trim(this.txtlname2.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@firstname3_8", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@firstname3_8"].Value = this.txtfname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtfname3.Value.ToString)), Trim(this.txtfname3.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@middlename3_9", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@middlename3_9"].Value = this.txtmname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtmname3.Value.ToString)), Trim(this.txtmname3.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@lastname3_10", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@lastname3_10"].Value = txtlname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtlname3.Value.ToString)), Trim(this.txtlname3.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@UniFormTitle_11", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@UniFormTitle_11"].Value = string.Empty;

                            SaveAccCom.ExecuteNonQuery();
                            SaveAccCom.Parameters.Clear();

                            SaveAccCom.CommandType = CommandType.StoredProcedure;
                            SaveAccCom.CommandText = "insert_BookSeries_1";

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
                            SaveAccCom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SeriesName_2", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SeriesName_2"].Value = txtseriesname.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtseriesname.Value.ToString)), Trim(this.txtseriesname.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@seriesNo_3", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@seriesNo_3"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@seriesPart_4", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@seriesPart_4"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@etal_5", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@etal_5"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Svolume_6", OleDbType.Integer));
                            SaveAccCom.Parameters["@Svolume_6"].Value = 0;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@af1_7", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@af1_7"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@am1_8", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@am1_8"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@al1_9", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@al1_9"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@af2_10", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@af2_10"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@am2_11", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@am2_11"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@al2_12", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@al2_12"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@af3_13", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@af3_13"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@am3_14", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@am3_14"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@al3_15", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@al3_15"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SSeriesName_16", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SSeriesName_16"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SseriesNo_17", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SseriesNo_17"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SseriesPart_18", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SseriesPart_18"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Setal_19", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Setal_19"].Value = string.Empty;


                            SaveAccCom.Parameters.Add(new OleDbParameter("@SSvolume_20", OleDbType.Integer));
                            SaveAccCom.Parameters["@SSvolume_20"].Value = 0;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Saf1_21", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Saf1_21"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Sam1_22", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Sam1_22"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Sal1_23", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Sal1_23"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Saf2_24", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Saf2_24"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Sam2_25", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Sam2_25"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Sal2_26", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Sal2_26"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Saf3_27", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Saf3_27"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Sam3_28", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Sam3_28"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Sal3_29", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Sal3_29"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SeriesParallelTitle_30", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SeriesParallelTitle_30"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SSeriesParallelTitle_31", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SSeriesParallelTitle_31"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SubSeriesName_32", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SubSeriesName_32"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SubseriesNo_33", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SubseriesNo_33"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SubseriesPart_34", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SubseriesPart_34"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subetal_35", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subetal_35"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SubSvolume_36", OleDbType.Integer));
                            SaveAccCom.Parameters["@SubSvolume_36"].Value = 0;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subaf1_37", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subaf1_37"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subam1_38", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subam1_38"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subal1_39", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subal1_39"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subaf2_40", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subaf2_40"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subam2_41", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subam2_41"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subal2_42", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subal2_42"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subaf3_43", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subaf3_43"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subam3_44", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subam3_44"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subal3_45", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subal3_45"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SubSeriesParallelTitle_46", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SubSeriesParallelTitle_46"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ISSNMain_47", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@ISSNMain_47"].Value = string.Empty;


                            SaveAccCom.Parameters.Add(new OleDbParameter("@ISSNSub_48", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@ISSNSub_48"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ISSNSecond_49", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@ISSNSecond_49"].Value = string.Empty;


                            SaveAccCom.ExecuteNonQuery();
                            SaveAccCom.Parameters.Clear();


                            // For Book Relator

                            SaveAccCom.CommandType = CommandType.StoredProcedure;
                            SaveAccCom.CommandText = "insert_BookRelators_1";

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
                            SaveAccCom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                            if (this.cmbpersontype.SelectedItem.Value == "Editor")
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorFname1_2", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorFname1_2"].Value = txtfname1.Value;// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtfname1.Value.ToString)), Trim(txtfname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorMname1_3", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorMname1_3"].Value = txtmname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtmname1.Value.ToString)), Trim(txtmname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorLname1_4", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorLname1_4"].Value = txtlname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtlname1.Value.ToString)), Trim(txtlname1.Value.ToString));


                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorFname2_5", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorFname2_5"].Value = txtfname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname2.Value.ToString)), Trim(txtfname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorMname2_6", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorMname2_6"].Value = txtmname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname2.Value.ToString)), Trim(txtmname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorLname2_7", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorLname2_7"].Value = txtlname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname2.Value.ToString)), Trim(txtlname2.Value.ToString));


                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorFname3_8", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorFname3_8"].Value = txtfname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname3.Value.ToString)), Trim(txtfname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorMname3_9", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorMname3_9"].Value = txtmname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname3.Value.ToString)), Trim(txtmname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorLname3_10", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorLname3_10"].Value = txtlname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname3.Value.ToString)), Trim(txtlname3.Value.ToString));
                            }
                            else
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorFname1_2", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorFname1_2"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorMname1_3", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorMname1_3"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorLname1_4", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorLname1_4"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorFname2_5", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorFname2_5"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorMname2_6", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorMname2_6"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorLname2_7", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorLname2_7"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorFname3_8", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorFname3_8"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorMname3_9", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorMname3_9"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@editorLname3_10", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@editorLname3_10"].Value = string.Empty;
                            }

                            // compiler information 
                            if (this.cmbpersontype.SelectedItem.Value == "Compiler")
                            {

                                SaveAccCom.Parameters.Add(new OleDbParameter("@CompilerFname1_11", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@CompilerFname1_11"].Value = txtfname1.Value.Trim();//

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilermname1_12", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilermname1_12"].Value = txtmname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtmname1.Value.ToString)), Trim(txtmname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilerlname1_13", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilerlname1_13"].Value = txtlname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtlname1.Value.ToString)), Trim(txtlname1.Value.ToString));


                                SaveAccCom.Parameters.Add(new OleDbParameter("@CompilerFname2_14", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@CompilerFname2_14"].Value = txtfname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname2.Value.ToString)), Trim(txtfname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilermname2_15", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilermname2_15"].Value = txtmname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname2.Value.ToString)), Trim(txtmname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilerlname2_16", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilerlname2_16"].Value = txtlname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname2.Value.ToString)), Trim(txtlname2.Value.ToString));


                                SaveAccCom.Parameters.Add(new OleDbParameter("@CompilerFname3_17", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@CompilerFname3_17"].Value = txtfname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname3.Value.ToString)), Trim(txtfname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilermname3_18", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilermname3_18"].Value = txtmname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname3.Value.ToString)), Trim(txtmname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilerlname3_19", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilerlname3_19"].Value = txtlname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname3.Value.ToString)), Trim(txtlname3.Value.ToString));
                            }
                            else
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@CompilerFname1_11", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@CompilerFname1_11"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilermname1_12", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilermname1_12"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilerlname1_13", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilerlname1_13"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@CompilerFname2_14", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@CompilerFname2_14"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilermname2_15", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilermname2_15"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilerlname2_16", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilerlname2_16"].Value = string.Empty;


                                SaveAccCom.Parameters.Add(new OleDbParameter("@CompilerFname3_17", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@CompilerFname3_17"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilermname3_18", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilermname3_18"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Compilerlname3_19", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Compilerlname3_19"].Value = string.Empty;
                            }
                            // illustrator information entry

                            if (this.cmbpersontype.SelectedItem.Value == "Illustrator")
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusFname1_20", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusFname1_20"].Value = txtfname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtfname1.Value.ToString)), Trim(txtfname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusmname1_21", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusmname1_21"].Value = txtmname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtmname1.Value.ToString)), Trim(txtmname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illuslname1_22", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illuslname1_22"].Value = txtlname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtlname1.Value.ToString)), Trim(txtlname1.Value.ToString));


                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusFname2_23", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusFname2_23"].Value = txtfname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname2.Value.ToString)), Trim(txtfname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusmname2_24", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusmname2_24"].Value = txtmname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname2.Value.ToString)), Trim(txtmname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illuslname2_25", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illuslname2_25"].Value = txtlname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname2.Value.ToString)), Trim(txtlname2.Value.ToString));


                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusFname3_26", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusFname3_26"].Value = txtfname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname3.Value.ToString)), Trim(txtfname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusmname3_27", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusmname3_27"].Value = txtmname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname3.Value.ToString)), Trim(txtmname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illuslname3_28", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illuslname3_28"].Value = txtlname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname3.Value.ToString)), Trim(txtlname3.Value.ToString));
                            }
                            else
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusFname1_20", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusFname1_20"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusmname1_21", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusmname1_21"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illuslname1_22", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illuslname1_22"].Value = string.Empty;


                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusFname2_23", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusFname2_23"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusmname2_24", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusmname2_24"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illuslname2_25", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illuslname2_25"].Value = string.Empty;


                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusFname3_26", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusFname3_26"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illusmname3_27", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illusmname3_27"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@illuslname3_28", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@illuslname3_28"].Value = string.Empty;
                            }
                            // translator information 
                            if (this.cmbpersontype.SelectedItem.Value == "Translater")
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@TranslatorFname1_29", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@TranslatorFname1_29"].Value = txtfname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtfname1.Value.ToString)), Trim(txtfname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatormname1_30", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatormname1_30"].Value = txtmname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtmname1.Value.ToString)), Trim(txtmname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatorlname1_31", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatorlname1_31"].Value = txtlname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtlname1.Value.ToString)), Trim(txtlname1.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@TranslatorFname2_32", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@TranslatorFname2_32"].Value = txtfname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtfname2.Value.ToString)), Trim(txtfname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatormname2_33", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatormname2_33"].Value = txtmname2.Value.Trim();//  IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname2.Value.ToString)), Trim(txtmname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatorlname2_34", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatorlname2_34"].Value = txtlname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname2.Value.ToString)), Trim(txtlname2.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@TranslatorFname3_35", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@TranslatorFname3_35"].Value = txtfname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname3.Value.ToString)), Trim(txtfname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatormname3_36", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatormname3_36"].Value = txtmname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname3.Value.ToString)), Trim(txtmname3.Value.ToString));

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatorlname3_37", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatorlname3_37"].Value = txtlname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname3.Value.ToString)), Trim(txtlname3.Value.ToString));
                            }
                            else
                            {
                                SaveAccCom.Parameters.Add(new OleDbParameter("@TranslatorFname1_29", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@TranslatorFname1_29"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatormname1_30", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatormname1_30"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatorlname1_31", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatorlname1_31"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@TranslatorFname2_32", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@TranslatorFname2_32"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatormname2_33", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatormname2_33"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatorlname2_34", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatorlname2_34"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@TranslatorFname3_35", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@TranslatorFname3_35"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatormname3_36", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatormname3_36"].Value = string.Empty;

                                SaveAccCom.Parameters.Add(new OleDbParameter("@Translatorlname3_37", OleDbType.VarWChar));
                                SaveAccCom.Parameters["@Translatorlname3_37"].Value = string.Empty;
                            }
                            SaveAccCom.ExecuteNonQuery();
                            SaveAccCom.Parameters.Clear();

                            SaveAccCom.CommandType = CommandType.StoredProcedure;
                            SaveAccCom.CommandText = "insert_BookConference_1";


                            SaveAccCom.Parameters.Add(new OleDbParameter("@ctrl_no_1", OleDbType.BigInt));
                            SaveAccCom.Parameters["@ctrl_no_1"].Value = hdCtrlNo.Value;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Subtitle_2", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Subtitle_2"].Value = txtSubbooktitle.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(this.txtSubbooktitle.Value.ToString)), Trim(this.txtSubbooktitle.Value.ToString));

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Paralleltype_3", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Paralleltype_3"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ConfName_4", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@ConfName_4"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ConfYear_5", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@ConfYear_5"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@BNNote_6", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@BNNote_6"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@CNNote_7", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@CNNote_7"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@GNNotes_8", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@GNNotes_8"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@VNNotes_9", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@VNNotes_9"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@SNNotes_10", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@SNNotes_10"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@ANNotes_11", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@ANNotes_11"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Course_12", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Course_12"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdFname1_13", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdFname1_13"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdMname1_14", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdMname1_14"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdLname1_15", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdLname1_15"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdFname2_16", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdFname2_16"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdMname2_17", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdMname2_17"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdLname2_18", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdLname2_18"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdFname3_19", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdFname3_19"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdMname3_20", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdMname3_20"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@AdLName3_21", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@AdLName3_21"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Abstract_22", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Abstract_22"].Value = string.Empty;

                            SaveAccCom.Parameters.Add(new OleDbParameter("@Program_name_23", OleDbType.VarWChar));
                            SaveAccCom.Parameters["@Program_name_23"].Value = string.Empty;

                            SaveAccCom.ExecuteNonQuery();
                            SaveAccCom.Parameters.Clear();

                            // -Loading 
                            var Lds = new DataSet();
                            var Lda = new OleDbDataAdapter();
                            int statusId = 0;
                            string StatusIssue = string.Empty;
                            string isbardate = string.Empty;
                            string shortname = string.Empty;
                            DateTime bardate;
                            SaveAccCom.CommandType = CommandType.Text;
                            SaveAccCom.CommandText = "select isIsued,isBardateApllicable FROM ItemStatusMaster Where Itemstatusid=" + this.cmbStatus.SelectedItem.Value;
                            Lda.SelectCommand = SaveAccCom;
                            Lda.Fill(Lds, "load");
                            if (Lds.Tables["load"].Rows.Count > 0)
                            {
                                StatusIssue = Lds.Tables["load"].Rows[0]["isIsued"].ToString();
                                isbardate = Lds.Tables["load"].Rows[0]["isBardateApllicable"].ToString();
                            }

                            if (isbardate == "Y")
                            {
                                bardate =Convert.ToDateTime( txtrelease.Text);
                            }
                            else
                            {
                                bardate = System.DateTime.Today.Date;
                            }
                            copynumber = copynumber + 1;

                            if (this.ChkCataloging.Checked == true)
                            {
                                SaveAccCom.CommandType = CommandType.Text;
                                SaveAccCom.CommandText = "update bookaccessionmaster set  ctrl_no='" + hdCtrlNo.Value + "',catalogdate='" + System.DateTime.Now.Date + "',Loadingdate='" + System.DateTime.Now.Date + "',ReleaseDate='" + bardate + "',IssueStatus=N'" + StatusIssue + "',Status=N'" + this.cmbStatus.SelectedItem.Value + "',copynumber='" + copynumber + "',editionyear='" + this.txteditionyear.Value + "',pubYear='" + this.txtPubYear.Value + "' where accessionnumber= N'" + str_man + "' ";
                                SaveAccCom.ExecuteNonQuery();
                                SaveAccCom.Parameters.Clear();
                            }
                            // If isNewCtrl = True Then
                            // SaveAccCom.CommandText = "update idtable set currentposition=" & Val(hdCtrlNo.Value) & " where objectname='catalog'"
                            // SaveAccCom.ExecuteNonQuery()
                            // isNewCtrl = False
                            // End If

                        }
                        // If accessioning = String.Empty Then
                        // accessioning &= "'" & str_man & "'"
                        // Else
                        // accessioning &= ",'" & str_man & "'"
                        // End If
                        if (string.IsNullOrEmpty(accessioning))
                        {
                            accessioning += @"\'" + str_man + @"\'";
                        }
                        else
                        {
                            accessioning += @",\'" + str_man + @"\'";
                        }

                        // Hidden6.Value = "Title-" & "'" & txtSTitle.Text & "'" & vbCrLf & "Accession Number-" & vbCrLf
                        // Hidden6.Value = Hidden6.Value & accessioning
                        // Hidden6.Value = Hidden6.Value & vbCrLf
                        Hidden6.Value = "Title-" + @"\'" + txtSTitle.Text + @"\'\nAccession Number-\n";
                        Hidden6.Value = Hidden6.Value + accessioning;
                        Hidden6.Value = Hidden6.Value + @"\n";
                        i_n = i_n + 1;
                    }
                    icnt = icnt + 1;
                    no = no + 1;
                }
                if (isNewCtrl == true)
                {
                    SaveAccCom.CommandText = "update idtable set currentposition=" + hdCtrlNo.Value + " where objectname=N'catalog'";
                    SaveAccCom.ExecuteNonQuery();
                    isNewCtrl = false;
                }
                int curr_pos;

                if (  Rbmanual1.Checked == false)
                {
                    SaveAccCom.CommandType = CommandType.Text;
                    string strwhere = " prefixid=" + cmbaccessionprefix.SelectedValue;
                    if (OptAccWOP.Checked == true)
                    {
                        strwhere = " prefixid=0 ";
                    }
                    SaveAccCom.CommandText = "select currentposition from prefixmaster where " + strwhere;
                    curr_pos = Convert.ToInt32(SaveAccCom.ExecuteScalar());
                    SaveAccCom.Parameters.Clear();
                    SaveAccCom.CommandType = CommandType.Text;
                    SaveAccCom.CommandText = "update prefixmaster set currentposition=" + (curr_pos + i_n) + " ,status=N'U' where " + strwhere;
                    SaveAccCom.ExecuteNonQuery();
                    SaveAccCom.Parameters.Clear();
                }
                // End If
                // END------------------------------------------------------------------------------
                no = no1;
                SaveAccCom.Parameters.Clear();

                SaveAccCom.CommandType = CommandType.StoredProcedure;
                SaveAccCom.CommandText = "insert_existingbookkinfo_1";

                SaveAccCom.Parameters.Add(new OleDbParameter("@srNoOld_1", OleDbType.Numeric));
                SaveAccCom.Parameters["@srNoOld_1"].Value = no;

                SaveAccCom.Parameters.Add(new OleDbParameter("@mediatype_2", OleDbType.Numeric));
                SaveAccCom.Parameters["@mediatype_2"].Value = cmbmediatype.SelectedItem.Value;  // Session("media")

                SaveAccCom.Parameters.Add(new OleDbParameter("@title_3", OleDbType.VarWChar));
                SaveAccCom.Parameters["@title_3"].Value = txtSTitle.Text.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtSTitle.Text)), Trim(txtSTitle.Text)); // Session("title")

                SaveAccCom.Parameters.Add(new OleDbParameter("@authortype_4", OleDbType.VarWChar));
                SaveAccCom.Parameters["@authortype_4"].Value = cmbpersontype.SelectedItem.Text; // Session("authortype")

                SaveAccCom.Parameters.Add(new OleDbParameter("@firstname1_5", OleDbType.VarWChar));
                SaveAccCom.Parameters["@firstname1_5"].Value = txtfname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname1.Value)), Trim(txtfname1.Value)); // Session("firstname1")

                SaveAccCom.Parameters.Add(new OleDbParameter("@middlename1_6", OleDbType.VarWChar));
                SaveAccCom.Parameters["@middlename1_6"].Value = txtmname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname1.Value)), Trim(txtmname1.Value)); // Session("middlename1")

                SaveAccCom.Parameters.Add(new OleDbParameter("@lastname1_7", OleDbType.VarWChar));
                SaveAccCom.Parameters["@lastname1_7"].Value = txtlname1.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname1.Value)), Trim(txtlname1.Value)); // Session("lastname1")

                SaveAccCom.Parameters.Add(new OleDbParameter("@firstname2_8", OleDbType.VarWChar));
                SaveAccCom.Parameters["@firstname2_8"].Value = txtfname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname2.Value)), Trim(txtfname2.Value)); // Session("firstname2")

                SaveAccCom.Parameters.Add(new OleDbParameter("@middlename2_9", OleDbType.VarWChar));
                SaveAccCom.Parameters["@middlename2_9"].Value = txtmname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname2.Value)), Trim(txtmname2.Value)); // Session("middlename2")

                SaveAccCom.Parameters.Add(new OleDbParameter("@lastname2_10", OleDbType.VarWChar));
                SaveAccCom.Parameters["@lastname2_10"].Value = txtlname2.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname2.Value)), Trim(txtlname2.Value)); // Session("lastname2")

                SaveAccCom.Parameters.Add(new OleDbParameter("@firstname3_11", OleDbType.VarWChar));
                SaveAccCom.Parameters["@firstname3_11"].Value = txtfname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtfname3.Value)), Trim(txtfname3.Value)); // Session("firstname3")

                SaveAccCom.Parameters.Add(new OleDbParameter("@middlename3_12", OleDbType.VarWChar));
                SaveAccCom.Parameters["@middlename3_12"].Value = txtmname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtmname3.Value)), Trim(txtmname3.Value)); // ("middlename3")

                SaveAccCom.Parameters.Add(new OleDbParameter("@lastname3_13", OleDbType.VarWChar));
                SaveAccCom.Parameters["@lastname3_13"].Value = txtlname3.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtlname3.Value)), Trim(txtlname3.Value)); // Session("lastname3")

                SaveAccCom.Parameters.Add(new OleDbParameter("@edition_14", OleDbType.VarWChar));
                SaveAccCom.Parameters["@edition_14"].Value = txtedition.Value; // Session("edition")

                SaveAccCom.Parameters.Add(new OleDbParameter("@yearofedition_15", OleDbType.VarWChar));
                SaveAccCom.Parameters["@yearofedition_15"].Value = txteditionyear.Value; // Session("editionyear")

                SaveAccCom.Parameters.Add(new OleDbParameter("@volumeno_16", OleDbType.VarWChar));
                SaveAccCom.Parameters["@volumeno_16"].Value = txtvolumeno.Value; // session("volumeno")

                SaveAccCom.Parameters.Add(new OleDbParameter("@isbn_17", OleDbType.VarWChar));
                SaveAccCom.Parameters["@isbn_17"].Value = txtisbn.Value;   // Session("isbn")

                SaveAccCom.Parameters.Add(new OleDbParameter("@category_18", OleDbType.Integer));
                SaveAccCom.Parameters["@category_18"].Value = cmbcategory.SelectedItem.Value; // Session("category")

                SaveAccCom.Parameters.Add(new OleDbParameter("@noofcopies_19", OleDbType.Integer));
                SaveAccCom.Parameters["@noofcopies_19"].Value = txtnoofcopies.Value; // Session("noofcopiesforaccession")

                SaveAccCom.Parameters.Add(new OleDbParameter("@price_20", OleDbType.Decimal));
                SaveAccCom.Parameters["@price_20"].Value = txtprice.Value; // Session("price")

                SaveAccCom.Parameters.Add(new OleDbParameter("@publisherid_21", OleDbType.Integer));
                // If Not hdPublisherId.Value = "" Then
                SaveAccCom.Parameters["@publisherid_21"].Value = hdPublisherId.Value;  // Session("publisher")
                                                                                            // Else
                                                                                            // SaveAccCom.Parameters("@publisherid_21").Value = Val(asmpublisher.SelectedValue)  'Session("publisher")
                                                                                            // End If

                SaveAccCom.Parameters.Add(new OleDbParameter("@recordingdate_22", OleDbType.Date));
                SaveAccCom.Parameters["@recordingdate_22"].Value = System.DateTime.Today; // .Now.ToString("dd/MM/yyyy") ' Session("accdate")

                SaveAccCom.Parameters.Add(new OleDbParameter("@seriesname_23", OleDbType.VarWChar));
                SaveAccCom.Parameters["@seriesname_23"].Value = txtseriesname.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtseriesname.Value)), Trim(txtseriesname.Value)); // Session("series")

                SaveAccCom.Parameters.Add(new OleDbParameter("@form_24", OleDbType.VarWChar));
                SaveAccCom.Parameters["@form_24"].Value = cmbform.SelectedItem.Value; // Session("Form")

                SaveAccCom.Parameters.Add(new OleDbParameter("@keywords_25", OleDbType.VarWChar));
                SaveAccCom.Parameters["@keywords_25"].Value = txtkeywords.Value; // Session("keywords")

                SaveAccCom.Parameters.Add(new OleDbParameter("@docDate_26", OleDbType.Date));
                SaveAccCom.Parameters["@docDate_26"].Value = txtAccDate.Text;


                SaveAccCom.Parameters.Add(new OleDbParameter("@Fprice_27", OleDbType.Decimal));
                SaveAccCom.Parameters["@Fprice_27"].Value = txtForeignPrice.Value.Trim()==""?"0": txtForeignPrice.Value.Trim();

                SaveAccCom.Parameters.Add(new OleDbParameter("@FcurrencyCode_28", OleDbType.VarWChar));

                if (cmbcurr.SelectedItem.Text == HComboSelect.Value)
                {
                    SaveAccCom.Parameters["@FcurrencyCode_28"].Value = "-";
                }
                else
                {
                    SaveAccCom.Parameters["@FcurrencyCode_28"].Value = cmbcurr.SelectedItem.Text;
                }

                SaveAccCom.Parameters.Add(new OleDbParameter("@subtitle_29", OleDbType.VarWChar));
                SaveAccCom.Parameters["@subtitle_29"].Value = txtSubbooktitle.Value.Trim();// IIf(tcase == "Y", libobj1.TitleCase(Trim(txtSubbooktitle.Value)), Trim(txtSubbooktitle.Value));

                SaveAccCom.Parameters.Add(new OleDbParameter("@part_30", OleDbType.Numeric));
                SaveAccCom.Parameters["@part_30"].Value = txttovlume.Value.Trim()==""?"0": txttovlume.Value.Trim();

                SaveAccCom.Parameters.Add(new OleDbParameter("@specialprice_31", OleDbType.Decimal));
                SaveAccCom.Parameters["@specialprice_31"].Value = txtspecialprice.Value.Trim()==""?"0": txtspecialprice.Value;

                SaveAccCom.Parameters.Add(new OleDbParameter("@dept_32", OleDbType.Integer));
                SaveAccCom.Parameters["@dept_32"].Value = cmbdept.SelectedValue;

                SaveAccCom.Parameters.Add(new OleDbParameter("@yearofPublication_33", OleDbType.VarWChar));
                SaveAccCom.Parameters["@yearofPublication_33"].Value = txtPubYear.Value;

                SaveAccCom.Parameters.Add(new OleDbParameter("@Language_Id_34", OleDbType.Integer));
                SaveAccCom.Parameters["@Language_Id_34"].Value = cmbLanguage.SelectedItem.Value;

                SaveAccCom.Parameters.Add(new OleDbParameter("@exchange_rate_35", OleDbType.Decimal));
                SaveAccCom.Parameters["@exchange_rate_35"].Value = txtExchangeRate.Value;

                SaveAccCom.Parameters.Add(new OleDbParameter("@no_of_pages_36", OleDbType.Integer));
                SaveAccCom.Parameters["@no_of_pages_36"].Value = txtcopy.Value;  // Val(.Value)

                SaveAccCom.Parameters.Add(new OleDbParameter("@page_size_37", OleDbType.VarWChar));
                SaveAccCom.Parameters["@page_size_37"].Value = Txtpagesize.Value;

                SaveAccCom.Parameters.Add(new OleDbParameter("@vendorid_38", OleDbType.Integer));
                // Dim SSSV As String = IIf(Trim(asmvendor.SelectedValue) = Nothing, HdVendorid.Value, asmvendor.SelectedValue)
                // If Not HdVendorid.Value = "" Then
                SaveAccCom.Parameters["@vendorid_38"].Value = HdVendorid.Value;
                // Else
                // SaveAccCom.Parameters("@vendorid_38").Value = asmvendor.SelectedValue
                // End If
                // SaveAccCom.Parameters("@vendorid_38").Value = IIf(Trim(asmvendor.SelectedValue) = Nothing, HdVendorid.Value, asmvendor.SelectedValue)

                // SaveAccCom.Parameters.Add(New OleDbParameter("@vendor_id_38", OleDbType.VarWChar))
                // SaveAccCom.Parameters("@vendor_id_38").Value = cmbvendor1.SelectedItem.Value

                SaveAccCom.ExecuteNonQuery();
                SaveAccCom.Parameters.Clear();

                if (txtspecialprice.Value != string.Empty)
                {
                    SaveAccCom.CommandType = CommandType.Text;
                    SaveAccCom.CommandText = "update bookaccessionmaster set  specialprice=" + txtspecialprice.Value + " where srNoOld= " + no + " ";
                    SaveAccCom.ExecuteNonQuery();
                    SaveAccCom.Parameters.Clear();
                }
                tran.Commit();
                hdval1.Value = no1.ToString();
                hdval.Value = ddd.ToString();
                SaveAccCom.Dispose();
                // SaveAccNumbercon.Close()
                // SaveAccNumbercon.Dispose()

                // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
                // 'Hidden2.Value = "2"
                // '  msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                // ' Me.Hidden8.Value = "MicroSoft Internet Explorer"
                // 'System.Windows.Forms.MessageBox.Show(Me.Hidden6.Value, Me.Hidden8.Value, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
                // 'libobj1.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // 'libobj1.MsgBox1(Hidden6.Value, Me)
                // 'msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Hidden6.Value, Me)
                // 'Dim str = hdCtrlNo.Value
                // 'libobj1.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)
                // 'ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "OpenWindow", "msgrecordsaved(" & hdCtrlNo.Value & ");", True)
                // Else
                // 'libobj1.MsgBox1(Resources.ValidationResources.recsave.ToString, Me)

                // End If
                lnkContinue.Visible = true;
                lnkModify.Visible = true;

                // Me.cmdsave.Disabled = True
                btnS.Enabled = false;

                SaveAccNumbercon.Close();
                // msglabel.Visible = True
                 cmdreset2_Click(sender,e);
//                cmdreset2_ServerClick(sender, e);
                // msglabel.Text = "Saved."
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.recsave.ToString, Me)
                message.PageMesg(Resources.ValidationResources.recsave, this, dbUtilities.MsgLevel.Success);

                return;
            }
            catch (Exception ex)
            {
                try
                {
                    tran.Rollback();
                    // msglabel.Visible = True
                    // msglabel.Text = ex.Message
                    // libobj1.MsgBox1(Resources.ValidationResources.UPcsExstBAccs.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UPcsExstBAccs.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.UPcsExstBAccs +";"+ex.Message, this, dbUtilities.MsgLevel.Warning); ;
                }
                catch (Exception ex1)
                {
                    // msglabel.Visible = True
                    // msglabel.Text = ex1.Message
                    // libobj1.MsgBox1(Resources.ValidationResources.UPcsExstBAccs.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UPcsExstBAccs.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.UPcsExstBAccs, this, dbUtilities.MsgLevel.Failure);
                    return;
                }
            }
        }

        protected void btnfindPbl_Click(object sender, EventArgs e)
        {
            if (txfindPbl.Text.Trim() != "")
            {
                GlobClassTr gClas = new GlobClassTr();
                gClas.TrOpen();
                string Qer = "Select  top 200 firstname+', '+percity as publisher,publisherid from  publishermaster join addresstable on  publishermaster.publisherid=addresstable.addid and addrelation='publisher' and (firstname+', '+percity) LIKE N'%" + txfindPbl.Text.Trim().Replace("'","''") + "%' order by firstname";
                DataTable dtD = gClas.DataT(Qer);
                gClas.TrClose();
                grdPubl.DataSource = dtD;
                grdPubl.DataBind();
                hdFindPbl.Value = "y";
            }
        }

        protected void btnselit_Click(object sender, EventArgs e)
        {
            Button btnsel = (Button)sender;
            GridViewRow r = (GridViewRow) btnsel.NamingContainer;
            var h = (HiddenField)r.FindControl("hdpublid");
            hdPublisherId.Value = h.Value;
            Label l = (Label)r.FindControl("labpbl");
            txtCmbPublisher.Text = l.Text;
            hdFindPbl.Value = "";
        }
    }
}