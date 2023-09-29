using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Summary description for GetClasNoFServ
/// </summary>
/// 
namespace ServClasNo
{

public class GetClasNoFServ
{
    public GetClasNoFServ()
    {
        //
        // TODO: Add constructor logic here
        //
    }
        public class CallData
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string ClassNo { get; set; }
        }

        public List<CallData> GetSugg(string QerStr,ref string ErrS)
    {
            List<CallData> lsD = new List<CallData>();
            try
            {


                var m_strFilePath = "http://classify.oclc.org/classify2/Classify?" + QerStr;

        string xmlStr;
        using (var wc = new WebClient())
        {
            xmlStr = wc.DownloadString(m_strFilePath);
        }
        //        lb1.Text =   xmlStr;

        XDocument xDoc = XDocument.Parse(xmlStr);
        var parts = xDoc.Descendants("response").Attributes("code");
        var pa2 = xDoc.Element("response");

        var v10 = XElement.Parse(xmlStr);

        var w = xDoc.Descendants("works").Attributes("title");
      //  txVal.Text = xmlStr;

        bool Vald = false;
        if (xmlStr.Contains("response code=\"0"))
            Vald = true;
        if (xmlStr.Contains("response code=\"2"))
            Vald = true;
        if (xmlStr.Contains("response code=\"4"))
            Vald = true;
        ErrS = "";
        if (Vald == false)
        {
            //    labEr.Text = "Not found";
            ErrS = "Not found";
            return lsD;
        }
        var fo = xDoc.Descendants("works");//  .Descendants("work").Attributes("owi");

        string xmlS2 = WebUtility.HtmlEncode(xmlStr);
        XmlDocument Doc = new XmlDocument();
        //            Doc.Load(xmlS2);// doesnt work 

        //        //        XmlNodeList Lis=Doc.SelectNodes("works",)

        string owiF = xmlStr;
        bool owFnd = true;
        List<string> owiV = new List<string>();
        while (owFnd)
        {
            int iFnd = owiF.IndexOf(" owi=");
            if (iFnd < 0)
            {
                owFnd = false;
                continue;
            }
            iFnd = iFnd + 6;
            int owiT = owiF.IndexOf("schemes=");
            string owiVal = GetOwi(iFnd, owiF);
            owiV.Add(owiVal.Trim());
            owiF = owiF.Substring(iFnd);
        }

        for (int inX = 0; inX < owiV.Count; inX++)
        {
            string XmlServ = GetXmlServ(owiV[inX]);
            try
            {

                CallData g = GetData(XmlServ);
                lsD.Add(g);
            }
            catch { }

        }
                //        grdDat.DataSource = lsD;
                //      grdDat.DataBind();
                //    labEr.Text = "";
            }
            catch (Exception Exp)
            {
                ErrS = Exp.Message;
            }
            return lsD;
    }
    public CallData GetData(string Xmls)
    {
        CallData D = new CallData();
        int AuthPos1 = Xmls.IndexOf("work author=") + 12;
        int AuthPos2 = Xmls.IndexOf(" editions=");
        string A1 = Xmls.Substring(AuthPos1, AuthPos2 - AuthPos1);
        D.Author = A1.Trim();
        int TitlPos1 = Xmls.IndexOf(" title=") + 7;
        string TitlFrm = Xmls.Substring(TitlPos1);
        string Titl = TitlFrm.Substring(0, TitlFrm.IndexOf(">"));
        D.Title = Titl.Trim();
        int ClasPos1 = Xmls.IndexOf(" nsfa=") + 7;
        string ClasS = Xmls.Substring(ClasPos1);
        int ClasPos2 = ClasS.IndexOf(" sfa=");
        D.ClassNo = ClasS.Substring(0, ClasPos2 - 1);
        return D;
    }

    public string GetOwi(int inXStrt, string Xms)
    {
        string Ou = "";
        while (true)
        {
            string charf = Xms.Substring(inXStrt, 1);
            if ((charf != "\""))
                Ou = Ou + charf;
            else
            {
                break;
            }
            inXStrt++;
        }

        return Ou;
    }

    public string GetXmlServ(string owi)
    {
        string sREt = "";
        var m_strFilePath = "http://classify.oclc.org/classify2/Classify?owi=" + owi + "&summary=true";
        using (var wc = new WebClient())
        {
            sREt = wc.DownloadString(m_strFilePath);
        }

        return sREt;
    }


}

}
