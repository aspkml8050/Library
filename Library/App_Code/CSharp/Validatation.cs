using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Library.Validation
{
    public class Validatation
    {
        public (List<string>, List<bookaccessionmaster>) ValidateAccn(List<bookaccessionmaster> accn)
        {
            List<string> flds = new List<string>();
            var modal = accn;
            int ixd = 0;

            BasicData basd = new BasicData();
            ReqdData rq = new ReqdData();
            rq.ItemType = true;
            var itmt = basd.GetBasicData(rq);

            var dupcp = modal.GroupBy(g => new { g.accessionnumber, g.Copynumber })

                .Select(call => new
                {
                    accn = call.Key.accessionnumber,
                    copynumber = call.Key.Copynumber,
                    count = call.Count()
                }).Where(d => d.count > 1).ToList();
            if (dupcp.Count > 0)
            {
                flds.Add("Invalid Accession Number");
                return (flds, modal);
            };
            GlobClassTr clas = new GlobClassTr();
            clas.TrOpen();
            foreach (var m in modal)
            {
                m.ordernumber = string.IsNullOrEmpty(m.ordernumber) ? "0" : m.ordernumber;
                m.indentnumber = string.IsNullOrEmpty(m.indentnumber) ? "0" : m.indentnumber;
                m.editionyear = m.editionyear == null ? (short)0 : m.editionyear;
                m.biilNo = string.IsNullOrEmpty(m.biilNo) ? "0" : m.biilNo;
                m.billDate = m.billDate == null ? Convert.ToDateTime("1900-1-1") : m.billDate;
                if (m.Copynumber == 0)
                    flds.Add("Copynumber is empty, accn: " + m.accessionnumber);
                if ((string.IsNullOrEmpty(m.form)) || (m.form == "---Select---"))
                    flds.Add("Form is empty, accn: " + m.accessionnumber);
                if ((string.IsNullOrEmpty(m.accessionnumber)) || (m.accessionnumber == "---Select---"))
                    flds.Add("Accession Number is empty, accn: " + m.accessionnumber);
                if (string.IsNullOrEmpty(m.booktitle))
                    flds.Add("Title is empty, accn: " + m.accessionnumber);

                if ((string.IsNullOrEmpty(m.Item_type)) || (m.Item_type == "---Select---"))
                    flds.Add("Item Type is empty, accn: " + m.accessionnumber);
                if (m.pubYear == null)
                    flds.Add("Publication Year, is empty accn: " + m.accessionnumber);
                if (m.bookprice == null)
                    flds.Add("Book price is empty accn: " + m.accessionnumber);
                if (m.VendorId == null)
                {
                    flds.Add("Vendor is empty accn: " + m.accessionnumber);
                }
                else
                {
                    string sqr = " select a.vendorname+', '+b.percity from vendormaster a join AddressTable b on a.vendorcode=b.addid and addrelation='vendor' and a.vendorid=  " + m.VendorId;
                    m.vendor_source = clas.ExScaler(sqr).ToString();
                }
                if (m.srno == null)
                    m.srno = 0;
                if (m.srNoOld == null)
                    m.srNoOld = 0;
                if (m.DSrno == null)
                    m.DSrno = 0;
                if (m.DeptCode == null)
                {
                    flds.Add("Dept Accn is empty accn: " + m.accessionnumber);
                }
                else
                {
                    string sqr = " select departmentname from departmentmaster where departmentcode=  " + m.DeptCode;
                    m.DeptName = clas.ExScaler(sqr).ToString();
                }
                if (m.ItemCategoryCode == null)
                {
                    flds.Add("Item Category Accn is empty accn: " + m.accessionnumber);
                }
                else
                {
                    string sqr = " select Category_LoadingStatus from CategoryLoadingStatus where id=  " + m.ItemCategoryCode;
                    m.ItemCategory = clas.ExScaler(sqr).ToString();
                }

                ixd++;
            }
            clas.TrClose();
            return (flds, modal);

        }

        public (List<string>, BookCatalogLib) ValidateBCateg(BookCatalogLib categ)
        {
            List<string> flds = new List<string>();
            var m = categ;
            BasicData basd = new BasicData();
            ReqdData rq = new ReqdData();
            rq.ItemType = true;
            var itmt = basd.GetBasicData(rq);
            GlobClassTr clas = new GlobClassTr();
            clas.TrOpen();
            if (m.catalogdate1 == null)
                flds.Add("Catalog Date empty");
            if (m.booktype == null)
                flds.Add("Catelog Book type empty");
            m.volumenumber = m.volumenumber == null ? "" : m.volumenumber.Trim();
            m.initpages = m.initpages == null ? "0" : m.initpages.Trim();
            if (m.pages == null)
                flds.Add("No of pages empty");
            m.parts = m.parts == null ? "" : m.parts;
            m.leaves = m.leaves == null ? "" : m.leaves.Trim();
            if (m.boundind == null)
                flds.Add("Bound empty");
            if (string.IsNullOrEmpty(m.title))
                flds.Add("Title is empty");
            if (m.publishercode == null)
                flds.Add("Publisher is Not selected");
            m.edition = m.edition == null ? "" : m.edition.Trim();
            m.isbn = m.isbn == null ? "" : m.isbn.Trim();
            m.subject1 = m.subject1 == null ? "" : m.subject1.Trim();
            m.subject2 = m.subject2 == null ? "" : m.subject2.Trim();
            m.subject3 = m.subject3 == null ? "" : m.subject3.Trim();
            m.Booksize = m.Booksize == null ? "" : m.Booksize.Trim();
            m.LCCN = m.LCCN == null ? "" : m.LCCN.Trim();
            m.volumenumber = m.volumenumber == null ? "" : m.volumenumber.Trim();
            m.biblioPages = m.biblioPages == null ? "0" : m.biblioPages.Trim();
            m.maps = 0;
            m.accmaterialhistory = m.accmaterialhistory == null ? "0" : m.accmaterialhistory.Trim();
            m.MaterialDesignation = m.MaterialDesignation == null ? "0" : m.MaterialDesignation.Trim();
            m.issn = m.issn == null ? "" : m.issn.Trim();
            m.Volume = m.Volume == null ? "" : m.Volume.Trim();
            if (m.dept == null)
                flds.Add("Catelog Dept Not selected");
            if (m.language_id == null)
                flds.Add("Catelog Language Not selected");
            m.parts = m.parts == null ? "" : m.parts.Trim();
            m.eBookURL = m.eBookURL == null ? "" : m.eBookURL.Trim();
            m.FixedData = m.FixedData == null ? "" : m.FixedData.Trim();
            m.cat_Source = m.cat_Source == null ? "" : m.cat_Source.Trim();
            m.Identifier = "MSSPL";
            m.firstname = m.firstname == null ? "" : m.firstname.Trim();
            m.percity = m.percity == null ? "" : m.percity.Trim();
            m.perstate = m.perstate == null ? "" : m.perstate.Trim();
            m.percountry = m.percountry == null ? "" : m.percountry.Trim();
            m.peraddress = m.peraddress == null ? "" : m.peraddress.Trim();
            m.departmentname = m.departmentname == null ? "" : m.departmentname.Trim();
            m.Btype = m.Btype == null ? "" : m.Btype.Trim();
            m.language_name = m.language_name == null ? "" : m.language_name.Trim();
            m.PublisherNo = m.PublisherNo == null ? "" : m.PublisherNo.Trim();
            m.PubSource = m.PubSource == null ? "" : m.PubSource.Trim();
            m.NLMCN = "";
            m.GeoArea = m.GeoArea == null ? "" : m.GeoArea.Trim();
            m.PhyExtent = m.PhyExtent == null ? "" : m.PhyExtent.Trim();
            m.pubDate = m.pubDate == null ? "" : m.pubDate.Trim();
            m.latestTransDate = m.latestTransDate == null ? "" : m.latestTransDate.Trim();
            m.ItemCategory = m.ItemCategory == null ? "" : m.ItemCategory.Trim();

            clas.TrClose();
            return (flds, m);

        }
        public (List<string>, BookAuthor) ValidateAuthor(BookAuthor auth)
        {
            List<string> flds = new List<string>();
            var m = auth;
            if (string.IsNullOrEmpty(m.firstname1))
                flds.Add("FirstName is empty");
            m.middlename1=m.middlename1== null ? "" : m.middlename1.Trim();
            m.lastname1 = m.lastname1 == null ? "" : m.lastname1.Trim();
            m.firstname2 = m.firstname2 == null ? "":m.firstname2.Trim();
            m.middlename2 = m.middlename2 == null ? "" : m.middlename2.Trim();
            m.lastname2 = m.lastname2 == null ? "" : m.lastname2.Trim();
            m.firstname3 = m.firstname3 == null ? "" : m.firstname3.Trim();
            m.middlename3 = m.middlename3 == null ? "" : m.middlename3.Trim();
            m.lastname3=m.lastname3==null ? "" : m.lastname3.Trim();
            return (flds, m);
        }
        public (List<string>, BookConference) ValidateConf(BookConference conf)
        {
            List<string> flds = new List<string>();
            var m = conf;
            conf.Subtitle= m.Subtitle==null?"":m.Subtitle.Trim();
            conf.ConfName = m.ConfName == null ? "" : m.ConfName.Trim();
            conf.ConfYear = m.ConfYear == null ? "" : m.ConfYear.Trim();
            conf.ConfPlace = m.ConfPlace == null ? "" : m.ConfPlace.Trim();
            return (flds, m);
        }
        public (List<string>, CatalogDataLib) ValidateConf(CatalogDataLib conf)
        {
            List<string> flds = new List<string>();
            var m = conf;
            conf.classnumber = m.classnumber == null ?"": m.classnumber.Trim();
            conf.booknumber = m.booknumber == null ? "" : m.booknumber.Trim();
            return (flds, m);
        }

    }
}