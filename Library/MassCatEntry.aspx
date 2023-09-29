<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="MassCatEntry.aspx.cs" Inherits="Library.MassCatEntry" %>

<%@ Register TagPrefix="CatS" TagName="Catalogs" Src="~/UCCatalogShow.ascx" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="cdHead" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="FormScripts/CatalogEntry.js"></script>
    <style>
        span {
            /*font-weight:normal!important;*/
            /*font-size:13px!important*/
        }

        .LayWidCont {
            width: 100% !important
        }

        .dvMLay2 {
            margin-top: 12px !important;
        }

        .vScrollBarRight {
            height: 200px;
            position: absolute;
            z-index: 99999;
            overflow-y: auto;
            border: solid 1px #333;
            font-size: small;
        }
        /*#ctl00_MainContent_CatgEdit_grdData,table,tr,th
       {
           
           height:30px!important
       }*/
    </style>
    <style type="text/css">
        .Bc {
            background-color: #b1fb17;
        }

        .TAccno {
            margin-top: 0px !important;
            width: 500px !important;
            max-height: 240px;
            font-size: 13px;
            overflow: auto;
            padding: 1px;
            margin: 0;
            border: 2px solid green;
            z-index: 1000;
        }

        .TSubj {
            width: 250px !important;
            font-size: 13px;
            max-height: 200px;
            overflow: auto;
            padding: 1px;
            margin-top: 0px;
            border: 2px solid green;
            z-index: 1000;
        }

        .Tscroll35 {
            width: 900px !important;
            max-height: 200px;
            overflow: auto;
            padding: 1px;
            padding-left: 3px;
            background-color: white;
            margin-top: 0;
            font-size: 11px;
            border: 2px solid green;
            z-index: 1000;
        }

        .TPubl {
            width: 700px !important;
            font-size: 13px;
            max-height: 200px;
            scrollbar-3dlight-color: gray;
            overflow-x: scroll;
            overflow-y: scroll;
            text-wrap: none;
            padding: 1px;
            border: 2px solid green;
            z-index: 1000;
        }

        .TVend {
            width: 500px !important;
            font-size: 13px;
            max-height: 200px;
            scrollbar-3dlight-color: gray;
            overflow-x: scroll;
            overflow-y: scroll;
            text-wrap: none;
            padding: 1px;
            border: 2px solid green;
            z-index: 1000;
        }

        .Tscroll355 {
            cursor: pointer;
            padding: 3px 0px 3px 0px;
            border-bottom: 1px solid grey;
        }

        .SDim {
            background-color: lightblue;
            opacity: 0.5;
            filter: alpha(opacity=50);
        }

        .TCopyNo {
            width: 180px !important;
            font-size: 13px;
            overflow: auto;
            margin-top: 0px;
            padding: 1px;
            max-height: 200px;
            border: 1px solid green;
        }

        .TLocation {
            width: 300px !important;
            font-size: 13px;
            overflow: auto;
            margin: 0px;
            padding: 1px;
            max-height: 250px;
            border: 1px solid green;
            z-index: 1000;
        }

        .MVols {
            width: 700px;
            height: 500px;
            border: 3px solid green;
            background-color: white;
            position: absolute;
            /*display:none;*/
        }

        .lnkB {
            background: inherit !important;
            font-weight: normal !important;
            color: black !important;
            text-transform: inherit !important;
        }

            .lnkB:hover {
                text-decoration: underline !important;
            }

        .ModBack {
            opacity: 0.9;
        }

        #ctl00_MainContent_txtDt, #ctl00_MainContent_btnSearchLoc, #ctl00_MainContent_btnsearch {
            font-size: 13px !important;
            height: auto !important
        }

        .rightcast {
            width: 47%;
            height: auto;
            font-size: 13px;
            padding-left: 2px;
            float: right;
        }

        .leftcast {
            width: 53%;
            height: auto;
            font-size: 13px;
            padding-left: 2px;
            float: left;
        }

        @media screen and (max-width:90%) {

            .rightcast {
                width: 53%;
                height: auto;
                font-size: 13px;
                float: right;
            }

            .leftcast {
                width: 53%;
                height: auto;
                font-size: 13px;
                float: left;
            }
        }

        @media screen and (max-width:1000px) {

            .leftcast {
                width: 100%;
                height: auto;
                font-size: 13px;
                float: left;
            }

            .rightcast {
                width: 100%;
                height: auto;
                font-size: 13px;
                float: left;
            }
        }
    </style>
    <script type="text/javascript">
        var ddt;
        $(document).ready(function () {
            ddt = document.getElementById("<%=hdnStime.ClientID%>").value;
                    // ddt = new Date();
                    setInterval("Ttime()", 1000);
                    $('#dvGetCN').draggable();
                    $('#dvz3950').draggable();

                });
        function Ttime() {
            var Ctime = new Date();
            var ddt2 = Ctime - ddt;
            //    document.getElementById('spMsg').innerHTML = ddt + " - " + Ctime.getHours() + ":"+ Ctime.getMinutes() + ":" + Ctime.getSeconds() ;
            //  document.getElementById('spMsg').innerHTML = ddt.getElapsed();
            //            document.getElementById('spMsg').innerHTML = ddt;
        }
                // $(document).ready = GetInitialData();

                // function GetInitialData() {
                //     $.ajax({
                //         type: "POST",
                //         url: "MassCatEntry.aspx/GetInitialData",
                //         data: '{}',
                //         contentType: "application/json; charset=utf-8",
                //         dataType: "json",

                //         success: function (data) {
                //             alert(data.d);
                //                 $("#txt_TestSuggoftite").autocomplete({
                //                     source: data.d,
                //                     focus: function (event,ui) {
                //                         var TPrfeix = ui.item.value;
                //                         $.ajax({
                //                             type: "POST",
                //                             url: "MassCatEntry.aspx/GetDataAccToTitle",
                //                             data: '{"TPrfeix":' + JSON.stringify(TPrfeix) + '}',
                //                             contentType: "application/json; charset=utf-8",
                //                             dataType: "json",

                //                             success: function (data) {
                //                                 ShowIt1(data.d);

                //                             }
                //                             ,
                //                             failure: function (response) {

                //                                 alert("Failure : " + response.d);
                //                             },
                //                             error:
                //                            function (xhr, ajaxOptions, thrownError) {
                //                                alert(xhr.status);
                //                                alert(xhr.responseText);
                //                                alert(thrownError);

                //                            }
                //                         });
                //                     },
                //                     select: function () {
                //                         HideIt();
                //                         TSelect2();
                //                     },
                //                     change: function () { HideIt(); },
                //                     mindLength: 0
                //                 }).focus(function(){
                //                     if (this.value == '') {
                //                         $(this).autocomplete("search");
                //                     }
                //                 });



                //         }
                //,
                //         failure: function (response) {

                //             alert("Failure : " + response.d);
                //         },
                //         error:
                //        function (xhr, ajaxOptions, thrownError) {
                //            alert(xhr.status);
                //            alert(xhr.responseText);
                //            alert(thrownError);

                //        }
                //     });
                // }










//        function ShowIt1(Details) {
//            //            alert(Details);
//            document.getElementById("hdnBookId").value = Details;
//            var fieldS = Details.split("|");
//            document.getElementById("ParaHdnOrigin").innerText = fieldS[1];
//        //    alert(Details);
//            if (fieldS[1] + 0 == 0) { //Origin
//                document.getElementById("spMsg").innerHTML = Details;
////                alert(Details);
//  //             alert(fieldS.length);
//                document.getElementById("PopUpSugg").style.visibility = 'hidden';
//                document.getElementById("ParaPopSrc2").innerText = 'Local DataBase';
//                document.getElementById("PopUpLocal").style.visibility = 'visible';
//                document.getElementById("ParaPopSrc2").textContent = 'Local DataBase';
//                document.getElementById('ParaPopAccno').innerText = fieldS[0] + " ";
//                document.getElementById('ParaPopAccno').textContent = fieldS[0] + " ";
//                document.getElementById('ParaPopTitle2').innerText = fieldS[2] + " ";
//                document.getElementById('ParaPopTitle2').textContent = fieldS[2] + " ";

//                document.getElementById('ParaPopAuthor2').innerText = fieldS[3] + " ";
//                document.getElementById('ParaPopAuthor2').textContent = fieldS[3] + " ";
//                 document.getElementById('ParaPopPages2').innerText = fieldS[4] + " ";
//                document.getElementById('ParaPopPages2').textContent = fieldS[4] + " ";
//    //            alert(fieldS[4]);

//                document.getElementById('ParaPopVol2').innerText = fieldS[5] + " ";
//                document.getElementById('ParaPopVol2').textContent = fieldS[5] + " ";
//                document.getElementById('ParaPopPart2').innerText = fieldS[6] + " ";
//                document.getElementById('ParaPopPart2').textContent = fieldS[6] + " ";
//                document.getElementById('ParaPopEdition2').innerText = fieldS[7] + " ";
//                document.getElementById('ParaPopEdition2').textContent = fieldS[7] + " ";
//                document.getElementById('ParaPopClassNo2').innerText = fieldS[8] + " ";
//                document.getElementById('ParaPopClassNo2').textContent = fieldS[8] + " ";
//                document.getElementById('ParaPopBookNo2').innerText = fieldS[9] + " ";
//                document.getElementById('ParaPopBookNo2').textContent = fieldS[9] + " ";
//                document.getElementById('ParaPopIsbn2').innerText = fieldS[10] + " ";
//                document.getElementById('ParaPopIsbn2').textContent = fieldS[10] + " ";
//                document.getElementById('ParaPopIssn2').innerText = fieldS[11] + " ";
//                document.getElementById('ParaPopIssn2').textContent = fieldS[11] + " ";
//                document.getElementById('ParaPopLang2').innerText = fieldS[12];
//                document.getElementById('ParaPopLang2').textContent = fieldS[12];

//                document.getElementById('ParaPopSubjects2').innerText = fieldS[13] + " ";
//                document.getElementById('ParaPopSubjects2').textContent = fieldS[13] + " ";
//                document.getElementById('ParaPopPrice2').innerText = fieldS[14] + " ";
//                document.getElementById('ParaPopPrice2').textContent = fieldS[14] + " ";
//                document.getElementById('ParaPopCur2').innerText = fieldS[15];
//                document.getElementById('ParaPopCur2').textContent = fieldS[15];
//                document.getElementById('ParaPopPubl2').innerText = fieldS[16] + ", " + fieldS[17];
//                document.getElementById('ParaPopPubl2').textContent = fieldS[16] + ", " + fieldS[17];

//                document.getElementById('ParaPopEdYr2').innerText = fieldS[18] + " ";
//                document.getElementById('ParaPopEdYr2').textContent = fieldS[18] + " ";
//                document.getElementById('ParaPopPubYear2').innerText = fieldS[19] + " ";
//                document.getElementById('ParaPopPubYear2').textContent = fieldS[19] + " ";
//      //          alert(fieldS[20]);
//                document.getElementById('ParaPopCP').innerText = fieldS[20] + " ";
//                   document.getElementById('ParaPopCP').textContent = fieldS[20] + " ";
//                 document.getElementById('ParaPopDept2').innerText =  fieldS[22] +" ";
//                 document.getElementById('ParaPopDept2').textContent =  fieldS[22] + " ";

//            }
//            else { //ssOrigin.ToString + ";" + ssAccno + ";" + ssBookTitle + ";" + ssAuthor + ";" + ssCopynumber + ";" + ssPg + ";" + ssVolume + ";" + ssParts + ";" + ssEdition + ";" + ssClassNo + ";" + ssBookNo + ";" + ssIsbn + ";" + ssIssn + ";" + ssLang + ";" + ssSubjects + ";" + ssPrice + ";" + ssCurrency + ";" + ssPubl + ";" + ssPubCity + ";" + ssEditionYr + ";" + ssPubYear
//                document.getElementById("spMsg").innerHTML = Details;
//                document.getElementById("PopUpLocal").style.visibility = 'hidden';
//                document.getElementById("PopUpSugg").style.backgroundColor = '#ffff75';
//                document.getElementById("ParaPopSrc").innerText = 'MSSPL DataBase';
//                document.getElementById("ParaPopSrc").textContent = 'MSSPL DataBase';
//                document.getElementById("PopUpSugg").style.visibility = 'visible';
//                document.getElementById('ParaPopTitle').innerText = fieldS[2] + " ";
//                document.getElementById('ParaPopTitle').textContent = fieldS[2] + " ";
//                document.getElementById('ParaPopAuthor').innerText = fieldS[3] + " ";
//                document.getElementById('ParaPopAuthor').textContent = fieldS[3] + " ";
//                document.getElementById('ParaPopPg').innerText = fieldS[4] + " ";
//                document.getElementById('ParaPopPg').textContent = fieldS[4] + " ";
//                document.getElementById('ParaPopVol').innerText = fieldS[5] + " ";
//                document.getElementById('ParaPopVol').textContent = fieldS[5] + " ";
//                document.getElementById('ParaPopParts').innerText = fieldS[6] + " ";
//                document.getElementById('ParaPopParts').textContent = fieldS[6] + " ";
//                document.getElementById('ParaPopEdition').innerText = fieldS[7] + " ";
//                document.getElementById('ParaPopEdition').textContent = fieldS[7] + " ";

//                document.getElementById('ParaPopClassNo').innerText = fieldS[8] + " ";
//                document.getElementById('ParaPopClassNo').textContent = fieldS[8] + " ";
//                document.getElementById('ParaPopBookNo').innerText = fieldS[9];
//                document.getElementById('ParaPopBookNo').textContent = fieldS[9];
//                document.getElementById('ParaPopIsbn').innerText = fieldS[10] + " ";
//                document.getElementById('ParaPopIsbn').textContent = fieldS[10] + " ";
//                document.getElementById('ParaPopIssn').innerText = fieldS[11] + " ";
//                document.getElementById('ParaPopIssn').textContent = fieldS[11] + " ";
//                document.getElementById('ParaPopLang').innerText = fieldS[12];
//                document.getElementById('ParaPopLang').textContent = fieldS[12];
//                document.getElementById('ParaPopSubj').innerText = fieldS[13] + " ";
//                document.getElementById('ParaPopSubj').textContent = fieldS[13] + " ";
//                document.getElementById('ParaPopPrice').innerText = fieldS[14] + " ";
//                document.getElementById('ParaPopPrice').textContent = fieldS[14] + " ";

//                document.getElementById('ParaPopCur').innerText = fieldS[15];
//                document.getElementById('ParaPopCur').textContent = fieldS[15];
//                document.getElementById('ParaPopPubl').innerText = fieldS[16] + ", " + fieldS[17];
//                document.getElementById('ParaPopPubl').textContent = fieldS[16] + ", " + fieldS[17];
//                document.getElementById('ParaPopEdYr').innerText = fieldS[18] + " ";
//                document.getElementById('ParaPopEdYr').textContent = fieldS[18] + " ";
//                document.getElementById('ParaPopPubYear').innerText = fieldS[19] + " ";
//                document.getElementById('ParaPopPubYear').textContent = fieldS[19] + " ";
////                document.getElementById('ParaPopCP').innerText = fieldS[20] + " ";
// //               document.getElementById('ParaPopCP').textContent = fieldS[20] + " ";
//            }
//        }




    </script>
    <script type="text/javascript">
        //    window.onload = function () {
        // alert('loaded');
        //  }
        //window.onload = ShowAccWarn();
        //var HWarn = 0;
        //function ShowAccWarn() {
        //    if (HWarn == 0) {
        //        setTimeout("HideWarn()", 15000);
        //        HWarn = 1;
        //    }
        //}
        //function HideWarn() {
        //    document.getElementById('AccNoWarning').style.display = 'none';
        //}
        //function GetTitle(vVal) {
        //    try {

        //        var ddT = vVal.value;
        //        var aa;
        //        PageMethods.Try2(ddT, OnSucceeded, Onfailed);
        //        function OnSucceeded(vval) {
        //            alert(vval);
        //        }
        //        function Onfailed(vval) {
        //            alert(vval + ":XX");
        //        }
        //    }catch (err) {
        //        alert(err.message);
        //    }

        //}
        function NumericOnly(fld) {
            //alert(fld.id);
            if (isFinite(fld.value) == true) {
                return true;
            } else {
                fld.value = "";
                alert("Enter Numeric value only.");
                // fld.focus();
                return false;
            }
        }
        function TAccnoSel(source, eventArgs) {
            var Arrg = eventArgs.get_value();
            var ErrAccNo = Arrg.split("|");

            document.getElementById("<%=txtAccNo.ClientID%>").value = ErrAccNo[0];
        document.getElementById("<%=hdnAccNo.ClientID%>").value = ErrAccNo[0];
//        document.getElementById("<%=hdnCtrl_no.ClientID%>").value =  ErrAccNo[1];
            //        __doPostBack('__Page', 'AccNoSelected');
            $("[id$=btnAccNoSel]").click();
        }
        function TSelect(source, eventArgs) {
            document.getElementById("<%=hdnBookId.ClientID%>").value = eventArgs.get_value();
        //        alert(document.getElementById("<%=hdnBookId.ClientID%>").value);
        //        __doPostBack('__Page', 'BookCollectionSelect');
        document.getElementById("<%=btnTitleSel.ClientID%>").click();
        }
        function TSelect2() {
            __doPostBack('__Page', 'BookCollectionSelect');  //will cause trouble on master page as full post back occurs
        }
        function TLoading(sender, e) {
            sender._element.className = "TLoading";
        }
        function TLoaded(sender, e) {
            sender._element.className = "";
        }

        function Onfailed(vval) {
            alert(vval);
        }
        function OnSucceeded(vval) {
            var fldNo;

            try {
                //        alert("Errors: " + vval);
                var fieldS = vval.split("|");
                //          document.getElementById("hdnBookId").value = vval;
                //          alert(fieldS[1]);
                if (fieldS[1] + 0 == 0) {
                    document.getElementById("PopUpSugg").style.visibility = 'hidden';
                    document.getElementById("ParaPopSrc2").innerText = 'Local DataBase';
                    document.getElementById("PopUpLocal").style.visibility = 'visible';
                    document.getElementById("ParaPopSrc2").textContent = 'Local DataBase';
                    //            alert(101);
                    fldNo = 0;
                    document.getElementById('ParaPopAccno').innerText = fieldS[0] + " ";
                    document.getElementById('ParaPopAccno').textContent = fieldS[0] + " ";
                    //            alert(fieldS[0]);
                    //          alert(fieldS[2]);
                    fldNo = 2;
                    document.getElementById('ParaPopTitle2').innerText = fieldS[2] + " ";
                    document.getElementById('ParaPopTitle2').textContent = fieldS[2] + " ";
                    fldNo = 3;
                    document.getElementById('ParaPopAuthor2').innerText = fieldS[3] + " ";
                    document.getElementById('ParaPopAuthor2').textContent = fieldS[3] + " ";
                    fldNo = 21;
                    document.getElementById('ParaPopCP').innerText = fieldS[20] + " ";
                    document.getElementById('ParaPopCP').textContent = fieldS[20] + " ";
                    fldNo = 4;
                    document.getElementById('ParaPopPages2').innerText = fieldS[4] + " ";

                    document.getElementById('ParaPopPages2').textContent = fieldS[4] + " ";
                    fldNo = 5;
                    document.getElementById('ParaPopVol2').innerText = fieldS[5] + " ";
                    document.getElementById('ParaPopVol2').textContent = fieldS[5] + " ";
                    fldNo = 6;
                    document.getElementById('ParaPopPart2').innerText = fieldS[6] + " ";
                    document.getElementById('ParaPopPart2').textContent = fieldS[6] + " ";
                    fldNo = 7;
                    document.getElementById('ParaPopEdition2').innerText = fieldS[7] + " ";
                    document.getElementById('ParaPopEdition2').textContent = fieldS[7] + " ";
                    fldNo = 10;
                    document.getElementById('ParaPopIsbn2').innerText = fieldS[10] + " ";
                    document.getElementById('ParaPopIsbn2').textContent = fieldS[10] + " ";
                    fldNo = 11;

                    document.getElementById('ParaPopIssn2').innerText = fieldS[11] + " ";
                    document.getElementById('ParaPopIssn2').textContent = fieldS[11] + " ";
                    fldNo = 12;

                    document.getElementById('ParaPopLang2').innerText = fieldS[12] + " ";
                    document.getElementById('ParaPopLang2').textContent = fieldS[12] + " ";
                    fldNo = 13;

                    document.getElementById('ParaPopSubjects2').innerText = fieldS[13] + " ";
                    document.getElementById('ParaPopSubjects2').textContent = fieldS[13] + " ";
                    fldNo = 18;
                    document.getElementById('ParaPopEdYr2').innerText = fieldS[18] + " ";
                    document.getElementById('ParaPopEdYr2').textContent = fieldS[18] + " ";
                    fldNo = 19;

                    document.getElementById('ParaPopPubYear2').innerText = fieldS[19] + " ";
                    document.getElementById('ParaPopPubYear2').textContent = fieldS[19] + " ";
                    fldNo = 8;

                    document.getElementById('ParaPopClassNo2').innerText = fieldS[8] + " ";
                    document.getElementById('ParaPopClassNo2').textContent = fieldS[8] + " ";
                    fldNo = 9;

                    document.getElementById('ParaPopBookNo2').innerText = fieldS[9] + " ";
                    document.getElementById('ParaPopBookNo2').textContent = fieldS[9] + " ";
                    fldNo = 14;

                    document.getElementById('ParaPopPrice2').innerText = fieldS[14] + "(" + fieldS[15] + ")";
                    document.getElementById('ParaPopPrice2').textContent = fieldS[14] + "(" + fieldS[15] + ")";
                    fldNo = 15;

                    //           document.getElementById('ParaPopCur2').innerText = fieldS[15];
                    //           document.getElementById('ParaPopCur2').textContent = fieldS[15];
                    fldNo = 16;

                    //            document.getElementById('ParaPopPubl2').innerText = fieldS[16] + ", " + fieldS[17];


                } else {
                    //ssOrigin.ToString + ";" + ssAccno + ";" + ssBookTitle + ";" + ssAuthor + ";" + ssCopynumber + ";" + ssPg + ";" + ssVolume + ";" + ssParts + ";" + ssEdition + ";" + ssClassNo + ";" + ssBookNo + ";" + ssIsbn + ";" + ssIssn + ";" + ssLang + ";" + ssSubjects + ";" + ssPrice + ";" + ssCurrency + ";" + ssPubl + ";" + ssPubCity + ";" + ssEditionYr + ";" + ssPubYear
                    document.getElementById("PopUpLocal").style.visibility = 'hidden';
                    document.getElementById("PopUpSugg").style.backgroundColor = '#ffff75';
                    document.getElementById("ParaPopSrc").innerText = 'MSSPL DataBase';
                    document.getElementById("ParaPopSrc").textContent = 'MSSPL DataBase';
                    document.getElementById("PopUpSugg").style.visibility = 'visible';
                    fldNo = 0;
                    document.getElementById('ParaPopAccno').innerText = fieldS[0] + " ";
                    document.getElementById('ParaPopAccno').textContent = fieldS[0] + " ";
                    //            alert(fieldS[0]);
                    //          alert(fieldS[2]);
                    fldNo = 2;
                    document.getElementById('ParaPopTitle').innerText = fieldS[2] + " ";
                    document.getElementById('ParaPopTitle').textContent = fieldS[2] + " ";
                    fldNo = 3;
                    document.getElementById('ParaPopAuthor').innerText = fieldS[3] + " ";
                    document.getElementById('ParaPopAuthor').textContent = fieldS[3] + " ";
                    fldNo = 21;
                    //            document.getElementById('ParaPopCP').innerText = fieldS[21] + " ";
                    //           document.getElementById('ParaPopCP').textContent = fieldS[21] + " ";
                    fldNo = 4;
                    document.getElementById('ParaPopPages').innerText = fieldS[4] + " ";
                    document.getElementById('ParaPopPages').textContent = fieldS[4] + " ";
                    fldNo = 5;
                    document.getElementById('ParaPopVol').innerText = fieldS[5] + " ";
                    document.getElementById('ParaPopVol').textContent = fieldS[5] + " ";
                    fldNo = 6;
                    document.getElementById('ParaPopParts').innerText = fieldS[6] + " ";
                    document.getElementById('ParaPopParts').textContent = fieldS[6] + " ";
                    fldNo = 7;
                    //            alert(document.getElementById('ParaPopEdition').innerText);
                    document.getElementById('ParaPopEdition').innerText = fieldS[7] + " ";
                    document.getElementById('ParaPopEdition').textContent = fieldS[7] + " ";
                    fldNo = 10;
                    document.getElementById('ParaPopIsbn').innerText = fieldS[10] + " ";
                    document.getElementById('ParaPopIsbn').textContent = fieldS[10] + " ";
                    fldNo = 11;
                    document.getElementById('ParaPopIssn').innerText = fieldS[11] + " ";
                    document.getElementById('ParaPopIssn').textContent = fieldS[11] + " ";
                    fldNo = 12;

                    document.getElementById('ParaPopLang').innerText = fieldS[12];
                    document.getElementById('ParaPopLang').textContent = fieldS[12];
                    fldNo = 13;

                    document.getElementById('ParaPopSubjects').innerText = fieldS[13] + " ";
                    document.getElementById('ParaPopSubjects').textContent = fieldS[13] + " ";
                    fldNo = 18;

                    document.getElementById('ParaPopEdYr').innerText = fieldS[18] + " ";
                    document.getElementById('ParaPopEdYr').textContent = fieldS[18] + " ";
                    fldNo = 19;

                    document.getElementById('ParaPopPubYear').innerText = fieldS[19] + " ";
                    document.getElementById('ParaPopPubYear').textContent = fieldS[19] + " ";
                    fldNo = 8;

                    document.getElementById('ParaPopClassNo').innerText = fieldS[8] + " ";
                    document.getElementById('ParaPopClassNo').textContent = fieldS[8] + " ";
                    fldNo = 9;

                    document.getElementById('ParaPopBookNo').innerText = fieldS[9] + " ";
                    document.getElementById('ParaPopBookNo').textContent = fieldS[9] + " ";
                    fldNo = 14;

                    document.getElementById('ParaPopPrice').innerText = fieldS[14] + " ";
                    document.getElementById('ParaPopPrice').textContent = fieldS[14] + " ";
                    fldNo = 15;

                    document.getElementById('ParaPopCur').innerText = fieldS[15];
                    document.getElementById('ParaPopCur').textContent = fieldS[15];
                    fldNo = 16;
                    document.getElementById('ParaPopPubl').innerText = fieldS[16] + ", " + fieldS[17];
                }
            } catch (err) {
                alert(err + ":" + fldNo + ":" + fieldS[fldNo] + ":" + fieldS[0]);
                alert(vval);
            }

            return;

        }
        function ShowIt(source, eventArgs) {
            try {

                var Details = eventArgs.get_value();
                var fieldS = Details.split("|");
                //        alert(Details);
                document.getElementById("ParaHdnOrigin").innerText = fieldS[0];
                PageMethods.getBooksHOV(fieldS[1], fieldS[2], OnSucceeded, Onfailed);
                //       alert("2");
            } catch (err) {
                alert(err);
            }
            //     alert("Unlaw");

            return false;
            //original stopped below
            //if (fieldS[0] + 0 == 0) { //Origin
            //    document.getElementById("PopUpSugg").style.visibility = 'hidden';
            //    document.getElementById("ParaPopSrc2").innerText = 'Local DataBase';
            //    document.getElementById("PopUpLocal").style.visibility = 'visible';
            //    document.getElementById("ParaPopSrc2").textContent = 'Local DataBase';

            //    document.getElementById('ParaPopAccno').innerText = fieldS[1] + " ";
            //    document.getElementById('ParaPopAccno').textContent = fieldS[1] + " ";
            //    document.getElementById('ParaPopTitle2').innerText = fieldS[2] + " ";
            //    document.getElementById('ParaPopTitle2').textContent = fieldS[2] + " ";

            //    document.getElementById('ParaPopAuthor2').innerText = fieldS[3] + " ";
            //    document.getElementById('ParaPopAuthor2').textContent = fieldS[3] + " ";
            //    document.getElementById('ParaPopCP').innerText = fieldS[4] + " ";
            //    document.getElementById('ParaPopCP').textContent = fieldS[4] + " ";
            //    document.getElementById('ParaPopPages2').innerText = fieldS[5] + " ";
            //    document.getElementById('ParaPopPages2').textContent = fieldS[5] + " ";
            //    document.getElementById('ParaPopEdition2').innerText = fieldS[8] + " ";
            //    document.getElementById('ParaPopEdition2').textContent = fieldS[8] + " ";
            //    document.getElementById('ParaPopIsbn2').innerText = fieldS[11] + " ";
            //    document.getElementById('ParaPopIsbn2').textContent = fieldS[11] + " ";
            //    document.getElementById('ParaPopIssn2').innerText = fieldS[12] + " ";
            //    document.getElementById('ParaPopIssn2').textContent = fieldS[12] + " ";
            //    document.getElementById('ParaPopLang2').innerText = fieldS[13];
            //    document.getElementById('ParaPopLang2').textContent = fieldS[13];

            //    document.getElementById('ParaPopSubjects2').innerText = fieldS[14] + " ";
            //    document.getElementById('ParaPopSubjects2').textContent = fieldS[14] + " ";
            //    document.getElementById('ParaPopEdYr2').innerText = fieldS[19] + " ";
            //    document.getElementById('ParaPopEdYr2').textContent = fieldS[19] + " ";
            //    document.getElementById('ParaPopPubYear2').innerText = fieldS[20] + " ";
            //    document.getElementById('ParaPopPubYear2').textContent = fieldS[20] + " ";
            //    document.getElementById('ParaPopClassNo2').innerText = fieldS[9] + " ";
            //    document.getElementById('ParaPopClassNo2').textContent = fieldS[9] + " ";
            //    document.getElementById('ParaPopBookNo2').innerText = fieldS[10] + " ";
            //    document.getElementById('ParaPopBookNo2').textContent = fieldS[10] + " ";

            //}
            //else { //ssOrigin.ToString + ";" + ssAccno + ";" + ssBookTitle + ";" + ssAuthor + ";" + ssCopynumber + ";" + ssPg + ";" + ssVolume + ";" + ssParts + ";" + ssEdition + ";" + ssClassNo + ";" + ssBookNo + ";" + ssIsbn + ";" + ssIssn + ";" + ssLang + ";" + ssSubjects + ";" + ssPrice + ";" + ssCurrency + ";" + ssPubl + ";" + ssPubCity + ";" + ssEditionYr + ";" + ssPubYear
            //    document.getElementById("PopUpLocal").style.visibility = 'hidden';
            //    document.getElementById("PopUpSugg").style.backgroundColor = '#ffff75';
            //    document.getElementById("ParaPopSrc").innerText = 'MSSPL DataBase';
            //    document.getElementById("ParaPopSrc").textContent = 'MSSPL DataBase';
            //    document.getElementById("PopUpSugg").style.visibility = 'visible';
            //    document.getElementById('ParaPopTitle').innerText = fieldS[2] + " XXXXXXXXXXXXX";
            //    document.getElementById('ParaPopTitle').textContent = fieldS[2] + " ";
            //    document.getElementById('ParaPopAuthor').innerText = fieldS[3] + " ";
            //    document.getElementById('ParaPopAuthor').textContent = fieldS[3] + " ";
            //    document.getElementById('ParaPopPg').innerText = fieldS[5] + " ";
            //    document.getElementById('ParaPopPg').textContent = fieldS[5] + " ";
            //    document.getElementById('ParaPopVol').innerText = fieldS[6] + " ";
            //    document.getElementById('ParaPopVol').textContent = fieldS[6] + " ";
            //    document.getElementById('ParaPopParts').innerText = fieldS[7] + " ";
            //    document.getElementById('ParaPopParts').textContent = fieldS[7] + " ";
            //    document.getElementById('ParaPopEdition').innerText = fieldS[8] + " ";
            //    document.getElementById('ParaPopEdition').textContent = fieldS[8] + " ";

            //    document.getElementById('ParaPopClassNo').innerText = fieldS[9] + " ";
            //    document.getElementById('ParaPopClassNo').textContent = fieldS[9] + " ";
            //    document.getElementById('ParaPopBookNo').innerText = fieldS[10];
            //    document.getElementById('ParaPopBookNo').textContent = fieldS[10];
            //    document.getElementById('ParaPopIsbn').innerText = fieldS[11] + " ";
            //    document.getElementById('ParaPopIsbn').textContent = fieldS[11] + " ";
            //    document.getElementById('ParaPopIssn').innerText = fieldS[12] + " ";
            //    document.getElementById('ParaPopIssn').textContent = fieldS[12] + " ";
            //    document.getElementById('ParaPopLang').innerText = fieldS[13];
            //    document.getElementById('ParaPopLang').textContent = fieldS[13];
            //    document.getElementById('ParaPopSubj').innerText = fieldS[14] + " ";
            //    document.getElementById('ParaPopSubj').textContent = fieldS[14] + " ";
            //    document.getElementById('ParaPopPrice').innerText = fieldS[15] + " ";
            //    document.getElementById('ParaPopPrice').textContent = fieldS[15] + " ";

            //    document.getElementById('ParaPopCur').innerText = fieldS[16];
            //    document.getElementById('ParaPopCur').textContent = fieldS[16];
            //    document.getElementById('ParaPopPubl').innerText = fieldS[17] + ", " + fieldS[18];
            //    document.getElementById('ParaPopPubl').textContent = fieldS[17] + ", " + fieldS[18];
            //    document.getElementById('ParaPopEdYr').innerText = fieldS[19] + " ";
            //    document.getElementById('ParaPopEdYr').textContent = fieldS[19] + " ";
            //    document.getElementById('ParaPopPubYear').innerText = fieldS[20] + " ";
            //    document.getElementById('ParaPopPubYear').textContent = fieldS[20] + " ";
            //}
        }
        function HideIt(source, eventArgs) {
            document.getElementById("PopUpSugg").style.visibility = 'hidden';
        }

        function PublMast() {
            window.open("PublisherMaster.aspx", "Publisher", "Location=yes");
        }
        function TPublSel(source, eventArgs) {
            document.getElementById("<%=hdnPublId.ClientID%>").value = eventArgs.get_value();
        }
        function TVendSel(source, eventArgs) {
            document.getElementById("<%=hdnVendid.ClientID%>").value = eventArgs.get_value();
            //  alert(document.getElementById("hdnVendId").value);
        }
        function TLocSel(source, eventArgs) {
            var lloc = eventArgs.get_value()
            var lloc2 = lloc.split(",");
            //            alert(lloc2[1]);
            document.getElementById("<%=txtLoc2.ClientID%>").value = lloc2[1];
            //            alert(lloc);
            //           alert(document.getElementById("txtLoc2").value);

        }
        function TLocSelCP(source, eventArgs) {
            var lloc = eventArgs.get_value()
            var lloc2 = lloc.split(",");
            //            alert(lloc2[1]);
            document.getElementById("<%=hdLocidCP.ClientID%>").value = lloc2[1];
            //            alert(lloc);
            //           alert(document.getElementById("txtLoc2").value);

        }


        function popMulCopy() {
            let acno = document.getElementById("<%=txtAccNo.ClientID%>").value;
        //        console.log(acno);
        let voln = document.getElementById("<%=txtVolume.ClientID%>") == null ? "" : document.getElementById("<%=txtVolume.ClientID%>").value;
        //      console.log('vol:' + voln);
        let prc = document.getElementById("<%=txtPrice.ClientID%>") == null ? 0 : document.getElementById("<%=txtPrice.ClientID%>").value;
    //    console.log(document.getElementById("<%=txtPrice.ClientID%>").value);
        let ddlCat = document.getElementById("<%=ddlCat.ClientID%>");
        let catid = ddlCat.options[ddlCat.selectedIndex] == null ? "" : ddlCat.options[ddlCat.selectedIndex].value;
        //  console.log(ddlCat.options[ddlCat.selectedIndex].text);
        let ctrln = document.getElementById("<%=hdnCtrl_no.ClientID%>") == null ? "" : document.getElementById("<%=hdnCtrl_no.ClientID%>").value;
        //    console.log(document.getElementById("<%=hdnCtrl_no.ClientID%>").value);
//        var ddlCat = document.getElementById("<%=ddlCat.ClientID%>");
            //Note here: following line ctrl_no and accno are due and to be tested in called form
            var strReturn;
            //            alert(document.getElementById('hdnCtrl_no').value);
            //strReturn = window.showModalDialog("AddCopy.aspx" + '?volno=' + document.getElementById('txtvolume').value + "&price=" + document.getElementById('txtprice').value + "&cat=" + ddlCat.options[ddlCat.selectedIndex].value + "&CtrlNo=" + document.getElementById('hdnCtrl_no').value + "&AccNo=" + document.getElementById('txtAccNo').value, "Editor", "status:no;dialogWidth:600px;dialogHeight:600px;dialogHide:true;help:no;scroll:no;");
            if (navigator.appName == "Microsoft Internet Explorer") {
                strReturn = window.showModalDialog("AddCopy.aspx" + '?volno=' + voln + "&price=" + prc + "&cat=" + catid + "&CtrlNo=" + ctrln + "&AccNo=" + acno + "&appname=masscatentry", "Editor", "status:no;dialogWidth:600px;dialogHeight:600px;dialogHide:true;help:no;scroll:no;");
            } else {
                //                                alert(ddlCat.options[ddlCat.selectedIndex].text);

                window.open("AddCopy.aspx" + '?volno=' + voln + "&price=" + prc + "&cat=" + catid + "&CtrlNo=" + ctrln + "&AccNo=" + acno + "&appname=masscatentry", "Editor", "status=no,width=900,height=500,hide=true,help=no,scroll=no");//,dialogWidth=600,dialogHeight=600,dialogHide=true,help=no,scroll=no
            }


            //            document.getElementById('btnShowCount').click();
        }

        function GetAN() {
            return "650";
        }
        function opeIF() {
            var acNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
        var fR = document.getElementById('<%=ifVols.ClientID%>');
            fR.src = "MultVols.aspx?accNo=" + acNo;
            fR.style.visibility = "visible";
        }

        function MSSSelect(arg, eve) {
            document.getElementById('<%=btnPBG.ClientID%>').click();
        }
        function OpenIF(oc) {
            if (oc == 'y') {
                document.getElementById('dvGetCN').style.display = "block";
                document.getElementById('<%=labEr.ClientID%>').innerText = "";
            } else {
                document.getElementById('dvGetCN').style.display = "none";
            }
        }
        function finIt() {
            document.getElementById('<%=labEr.ClientID%>').innerText = "Searching...";
        document.getElementById('<%=btnFin.ClientID%>').click();
            return false;
        }
        function finIt2() {
            document.getElementById('<%=labEr.ClientID%>').innerText = "Searching...";
        document.getElementById('<%=btnFinISB.ClientID%>').click();
            return false;
        }
        function fillClasSer(txTi) {
            let ti = $(txTi).val();
            document.getElementById('<%=txFramTitle.ClientID%>').value = ti;
        let aut = document.getElementById('<%=txtAuthL1.ClientID%>').value;
        document.getElementById('<%=txtAuth.ClientID%>').value = aut;
        }

        function filPar(clas) {
            document.getElementById('<%=txtClassNo.ClientID%>').value = $(clas).text();
            return true;
        }

        function CallPub2() {
            let hdid = $('[id$=hdnPublId]').attr('id');
            let txt = $('[id$=txtPubl]').attr('id');
            window.open("PublisherMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Publisher Master", "height=700px,width=800px");

        }
        //

        //
        function callVend() {
            let hdid = $('[id$=hdnVendid]').attr('id');
            let txt = $('[id$=txtVend]').attr('id');
            window.open("VendorMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Vendor Master", "height=700px,width=800px");

        }

        function showattach(lab) {
            $(lab).next().css('display', 'block');
        }
        function closit(elem) {
            $(elem).parent().css('display', 'none');
        }
    </script>


</asp:Content>
<asp:Content ID="cdMain" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:HiddenField ID="hdnInsUpd" runat="server" Value="I" />
    <input id="hdnScrW" runat="server" type="hidden" value="" />
    <input id="hdnCat" runat="server" type="hidden" value="" />
    <input id="hdnItype" runat="server" type="hidden" value="" />
    <input id="hdnIStat" runat="server" type="hidden" value="" />
    <input id="hdnCurr" runat="server" type="hidden" value="" />
    <input id="hdnLang" runat="server" type="hidden" value="" />
    <input id="hdnDept" runat="server" type="hidden" value="" />
    <input id="hdnMedia" runat="server" type="hidden" value="" />
    <input id="hdnStime" type="hidden" runat="server" value="" />
    <input id="hdnAccNo" runat="server" type="hidden" value="" />

    <div style="width: 100%; display: none" class="title">
        <div style="width: 100%; float: left">

            <asp:Label ID="lblHead" runat="server" Style="text-align: center; font-weight: bold; text-decoration: underline; font-size: 1.3em;" Text=""></asp:Label>
        </div>
        <div style="float: right; vertical-align: top">
            <a id="lnkHelp" href="#" onclick="ShowHelp('Help/DirectCatEntryExpr.htm')">
                <img src="help.jpg" alt="H" height="15" /></a>
        </div>
    </div>
    <asp:UpdatePanel ID="UPCnt" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <asp:Button ID="btnAccNoSel" runat="server" Style="display: none;" OnClick="btnAccNoSel_Click" />
            <asp:Button ID="btnTitleSel" runat="server" Style="display: none;" OnClick="btnTitleSel_Click" />
            <asp:HiddenField ID="hdz39path" runat="server" />
            <asp:HiddenField ID="hdBookNumAccn" runat="server" />
            <%--                            This field is not emptied - stores whether booknumber to be stored in accessionmaster and so on--%>

            <div class="container tableborderst">
                further work multiple copies, volume to do, z3950
                <table class="no-more-tables table-condensed GenTable1" style="width: 100%; margin-left: auto; margin-right: auto; border: 0">
                    <tr>
                        <td>
                            <div class="leftcast">
                                <table style="width: 100%; border: 0">
                                    <tr>
                                        <td><span style="color: green;">Total Catalogued:</span></td>
                                        <td>
                                            <asp:Label ID="lblTotC" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <span style="color: green;">Total Accessioned:</span></td>
                                        <td>
                                            <asp:Label ID="lblTotA" runat="server" Text=""></asp:Label>

                                        </td>
                                        <td>

                                            <span>
                                                <asp:Label ID="lblTod" runat="server" Text=""></asp:Label></span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="rightcast">
                                <table style="width: 100%; border: 0">
                                    <tr>
                                        <td>
                                            <span style="color: #6d3f26; vertical-align: top">Catalog Date: </span></td>
                                        <td>
                                            <asp:TextBox ID="txtDt" runat="server" Text="" Height="22px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <button type="button" id="btnsearch" runat="server" data-target="#demo">Search</button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearchLoc" runat="server" Text="Refresh Location" ToolTip="Click on Button to Refresh Location for Suggestion" OnClick="btnSearchLoc_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="RFidProcess" runat="server" visible="false" style="width: 100%;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblantena" runat="server" Text="Select Antenna"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlantenna" runat="server">
                                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnscan2" runat="server" Text="Scan" OnClick="btnscan_Click" />
                                                <asp:Button ID="BtnRfidReset" runat="server" Text="RFID Reset" class="btnstyle" OnClick="BtnRfidReset_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>





                    </tr>
                </table>


            </div>

    <%--<ajax:CalendarExtender ID="ajCal" runat="server"  TargetControlID="txtDt" Format="dd-MMM-yyyy"></ajax:CalendarExtender>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnDel" />
        </Triggers>
        <ContentTemplate>
            <%-- TransNo is used in logging for audit  --%>
            <asp:HiddenField ID="hdTransNo" runat="server" />
            <asp:HiddenField ID="hdIpAddress" runat="server" />

            <input type="hidden" id="inpShoPB" runat="server" />
            <script type="text/javascript">

                function recPrev() {

                }

                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(keepSerOP);
                function keepSerOP(s, e) {

                    var m = $('[id$=inpShoPB]').val();
                    if (m == 'y') {
                        $('[id$=inpShoPB]').val('');
                        $('#demo').collapse('show');
                    }
                    $('#dvGetCN').draggable();
                    //fillClasSer(this)
                    $('[id$=txt_TestSuggoftite]').on('blur', function () {
                        fillClasSer($(this));
                    });
                    var hdClasSr = $('[id$=hdClasSer]');
                    if (hdClasSr.val() == 'y') {
                        hdClasSr.val('');
                        $('#dvGetCN').css('display', 'block');
                    }
                    $('[id$=txtISBN]').on('blur', function () {
                        $('[id$=txISBNClas]').val($(this).val());
                    });

                }
            </script>
            <div class="container tableborderst">
                
   <div id="demo" class="collapse">
       <span>MSSPL Suggestions</span>
       <table style="margin-top: 5px; width: 80%; margin-left: auto; margin-right: auto; border: 0px;">
           <tr>
               <td>Title and Author
               </td>
               <td>
                   <asp:TextBox ID="txtTAut" runat="server" Width="300"></asp:TextBox>
               </td>
               <td>
                   <asp:Button ID="btnSerch" runat="server" Text="Find" OnClick="btnSerch_Click" />
               </td>
           </tr>
       </table>
       <div style="width: auto; max-height: 250px; overflow: auto;">
           <asp:GridView ID="grdSugBks" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#336699">
               <Columns>
                   <asp:TemplateField>
                       <HeaderTemplate>
                           Title
                       </HeaderTemplate>
                       <ItemTemplate>
                           <asp:HiddenField ID="hdnBId" runat="server" Value='<%# Eval("bookid") %>' />
                           <asp:LinkButton ID="lnkTitl" Width="300" OnClick="lnkTitl_Click" runat="server" Text='<%# Eval("title") %>'></asp:LinkButton>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <HeaderTemplate>
                           Author
                       </HeaderTemplate>
                       <ItemTemplate>
                           <asp:Label ID="lblAu" runat="server" Width="200" Text='<%# Eval("author") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <HeaderTemplate>
                           Publisher
                       </HeaderTemplate>
                       <ItemTemplate>
                           <asp:Label ID="lblPu" runat="server" Width="250" Text='<%# Eval("publisher") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <HeaderTemplate>
                           Publ.City
                       </HeaderTemplate>
                       <ItemTemplate>
                           <asp:Label ID="lblPuCit" runat="server" Width="120" Text='<%# Eval("pubcity") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="classno" HeaderText="Class No" />
                   <asp:BoundField DataField="bookno" HeaderText="Book No" />
                   <asp:BoundField DataField="isbn" HeaderText="ISBN" />
                   <asp:BoundField DataField="subjects" HeaderText="Subject" />


               </Columns>
           </asp:GridView>
       </div>


   </div>
                <asp:Panel ID="PnlShowData" runat="server" Style="background-color: white; margin-left: auto; width: 70%; margin-right: auto; display: none; max-height: 750px;" CssClass="MVols">

                    <style>
                        .TVendpop {
                            width: 300px !important;
                            font-size: 13px;
                            max-height: 200px;
                            scrollbar-3dlight-color: gray;
                            overflow-x: scroll;
                            overflow-y: scroll;
                            text-wrap: none;
                            padding: 1px;
                            border: 2px solid green;
                            z-index: 300000 !important;
                        }

                        .TLocationloc {
                            width: 300px !important;
                            font-size: 13px;
                            max-height: 200px;
                            scrollbar-3dlight-color: gray;
                            overflow-x: scroll;
                            overflow-y: scroll;
                            text-wrap: none;
                            padding: 1px;
                            border: 2px solid green;
                            z-index: 300000 !important;
                        }
                    </style>
                    <table style="width: 100%;" class="no-more-tables table-condensed">
                        <tr>
                            <td>
                                <h3 id="HeaderTitle">Copies of Book Accessionnumber                </h3>

                            </td>
                            <td style="text-align: right">

                                <img id="ImgClose" runat="server" width="35" height="35" style="text-align: right;" src="~/Images/del.png" alt="Close" />
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; width: 100%; max-height: 400px;">
                        <asp:GridView ID="GrdCopyAcc" Width="700px" DataKeyNames="accessionnumber" OnSelectedIndexChanged="GrdCopyAcc_SelectedIndexChanged" AllowPaging="false" EnableTheming="false" runat="server">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkCopyDelete" runat="server" />
                                        <asp:HiddenField ID="HdnAccNo" Value='<%# Eval("accessionnumber") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accession Number">
                                    <ItemTemplate>
                                        <asp:Label ID="LblGrdAccno" runat="server" Font-Bold="true" Width="80px" Text='<%#Eval("accessionnumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdTitle" runat="server" Width="300px" Text='<%#Eval("booktitle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Price" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrdPrice" Width="100px" runat="server" Text='<%# Eval("bookprice") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Copy Number">
                                    <ItemTemplate>
                                        <asp:Label ID="LblgrdCopynumber" runat="server" Width="80px" Text='<%#Eval("Copynumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrdVendor" Width="300px" runat="server" Text='<%# Eval("vendor_source") %>'></asp:TextBox>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publisher Year" ItemStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrdPubyear" Width="100px" runat="server" Text='<%# Eval("pubYear") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No." ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrdbiilNo" Width="100px" runat="server" Text='<%# Eval("biilNo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrdbilldate" Width="150px" runat="server" Text='<%# Eval("billdate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrdLocation" Width="300px" runat="server" Text='<%# Eval("Location") %>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; padding-top: -10px;">
                                <asp:Button ID="BtnDeleteCopy" CssClass="btnstyle" runat="server" OnClick="BtnDeleteCopy_Click" Text="Delete" />
                                <asp:Button ID="BtnInsertAllCopy" CssClass="btnstyle" runat="server" OnClick="BtnInsertAllCopy_Click" Text="Update All Copy" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Panel ID="pnlVols" runat="server" Style="background-color: white; margin-left: auto; width: 70%; margin-right: auto; display: none; max-height: 600px;" CssClass="MVols">
                    <table style="width: 100%;" class="no-more-tables table-condensed">
                        <tr>
                            <td>
                                <h4 id="Header" style="border: none; margin-bottom: 15px" class="hMulV">Multiple Volume Entry                </h4>

                            </td>
                            <td style="background-color: white; text-align: right">
                                <img id="imClo" runat="server" width="25" height="25" style="text-align: right;" src="~/Images/close.gif" alt="Close" />
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; width: 100%">
                        <iframe id="ifVols" style="height: 80%; width: 100%;" runat="server"></iframe>
                    </div>
                    <asp:Button ID="btnDum1" runat="server" CssClass="btnstyle" Text="OK" />

                </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table class="no-more-tables table-condensed GenTable1" style="background-color: #f0f0f9; margin-top: 5px; border-collapse: collapse; padding-right: 2px;" border="1">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Acc No:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAccNo" runat="server" AutoPostBack="false" Text="" AutoCompleteType="None" ToolTip="To Add Copy of current book, enter new accession number and press 'Add Copy'"></asp:TextBox>
                                        <%-- <ajax:AutoCompleteExtender ID="ExtAccNo"  runat="server" TargetControlID="txtAccNo"
          MinimumPrefixLength="2"
          CompletionInterval="100"
          CompletionSetCount="50"
          FirstRowSelected="true" 
          CompletionListCssClass="TAccno"
          OnClientItemSelected="TAccnoSel"
          ServicePath="MssplSugg.asmx"
          EnableCaching="true" 
          ServiceMethod="GetAccNo" >
     </ajax:AutoCompleteExtender>--%>
                                        <div id="divT"></div>



                                    </td>


                                    <td>
                                        <i class="fa fa-clipboard" style="font-size: 20px; color: initial;" title="Data of relevant fields are stored in LocalStorage of Browser and is available on refresh, By enabling this icon copy data from localstarage to field, see circle on Dropdown for this." onclick="copyFromStore(this);"></i>
                                    </td>

                            </table>
                        </td>
                        <td>
                            <table class="no-more-tables table-condensed GenTable1" style="background-color: #f0f0f9; margin-top: 5px; border-collapse: collapse; padding-right: 2px;" border="1">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lblbookcopy" Font-Size="Large" Width="40%" runat="server" OnClick="lblbookcopy_Click" Text="Total No. of Copies:-"></asp:LinkButton>

                                        <asp:LinkButton ID="LnkBookCopy" Font-Size="Large" runat="server" Text="0" OnClick="LnkBookCopy_Click"></asp:LinkButton>
                                        <ajax:ModalPopupExtender ID="MPShowCopy" runat="server" TargetControlID="LnkBookCopy"
                                            PopupControlID="PnlShowData" CancelControlID="ImgClose"
                                            PopupDragHandleControlID="Header" Y="100">
                                        </ajax:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
    <table class="no-more-tables table-condensed GenTable1" style="width: 100%; background-color: #f0f0f9; margin-left: auto; margin-right: auto; margin-top: 5px; border-collapse: collapse; padding-right: 2px;" border="1">
        <tr>
            <td style="border-style: inset;">
                <table class="table-condensed GenTable1" style="width: 100%; border: 0">
                    <tr>
                        <td>
                            <span style="font-size: 0.9em; font-weight: normal;">CopyAccNo.:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccNoCopy" runat="server" Enabled="false" Width="150" ToolTip="Enter New Accession Number and press Add Copy."></asp:TextBox>
                          

                        </td>

                        <td>Price</td>
                        <td>
                            <asp:TextBox ID="txCpPrice" runat="server" Width="90"></asp:TextBox>
                        </td>
                        <td>Cp Book No
                        </td>
                        <td>
                            <asp:TextBox ID="txCpBookno" runat="server" Width="150" ToolTip="Enter Book No of Copy other than Catalog Book Number of Call Number."></asp:TextBox>*
                        </td>
                    </tr>
                    <tr>
                        <td>Item Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlItemTypeCP" runat="server" Width="120"></asp:DropDownList>
                        </td>

                        <td>
                            <span style="font-size: 0.9em; font-weight: normal;">CopyNo:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCopyNo" runat="server" Width="50" onblur="return NumericOnly(this);" Enabled="false" Text=""></asp:TextBox>
                           
                        </td>
                        <td>
                            <span style="font-size: 0.9em; font-weight: normal;">Dt.:</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtacccopydt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Bill No
                        </td>
                        <td>
                            <asp:TextBox ID="txBillNoCP" runat="server"></asp:TextBox>
                        </td>
                        <td>Bill Date
                        </td>
                        <td>
                            <asp:TextBox ID="txBillDtCP" runat="server"></asp:TextBox>
                        </td>
                        <td>Dept
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeptCP" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Item Category
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategCP" runat="server"></asp:DropDownList>
                        </td>
                        <td>Vendor
 
                        </td>
                        <td>
                            <asp:TextBox ID="txVendorCP" runat="server"></asp:TextBox>
                         

                        </td>
                        <td>Location
                        </td>
                        <td>
                            <asp:HiddenField ID="hdLocidCP" runat="server" />
                            <asp:TextBox ID="txLocationCP" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
                     
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right">
                            <%--<ajax:CalendarExtender ID="CalendarExtendercpydt" runat="server"  TargetControlID="txtacccopydt" Format="dd-MMM-yyyy"></ajax:CalendarExtender>--%>
                            <asp:Button ID="btnAddCopy" runat="server" CssClass="btnstyle" Text="Add Copy" Enabled="false" OnClick="btnAddCopy_Click" ToolTip="Enter New Accession Number, and press this button to add copy of the book."></asp:Button>
                            <input id="hdnCtrl_no" runat="server" type="hidden" value="0" />
                            <div style="display: none; position: relative;" id="dvattachments" runat="server">
                                <asp:Label ID="lablHasFiles" onmouseover="showattach(this)" runat="server">Attachments*</asp:Label>
                                <div id="dvattachlists" runat="server" style="position: absolute; z-index: 10; min-width: 350px; display: none; padding: 4px 4px; border: 1px solid grey; background-color: white;">
                                    <span style="font-weight: bold; cursor: pointer;" onclick="closit(this)">X</span>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>





            </td>

            <td>
                <table style="width: 100%; border: 0" class="table-condensed GenTable1">
                    <tr>
                        <td>
                            <span title="appsetting z39path should be set by admin to access SRU Server Book data">Access Z39.50*</span>
                        </td>
                        <td>
                            <asp:Button ID="btnZ39" runat="server" CssClass="btn btn-primary" OnClientClick="return showZ39dv();" Text="Get Z39 SRU data"></asp:Button>
                            <div id="dvz3950" style="width: 900px; background-color: rgba(255,255,255,0.8); z-index: 100; position: absolute; top: 80px; max-height: 650px; overflow: auto; right: 100px; min-height: 350px; border: 1px solid grey; border-radius: 6px; padding: 6px 6px; display: none;">
                                <div style="display: flex; align-items: center; justify-content: center; align-content: space-around; background-color: #efe1e1; width: 100%; margin: 0;">
                                    <div>
                                        Z39.50 SRU Sever Data
                                    </div>
                                    <div>
                                        <input type="text" style="width: 250px; height: 20px;" placeholder="Enter title to find" id="txz39" />
                                    </div>
                                    <div>
                                        <button type="button" onclick="return getSruData();">Search</button>
                                    </div>
                                    <div>
                                        <span id="spz39cp" style="display: none;">Click on Cell to copy</span>
                                    </div>
                                    <div>
                                        <span style="font-size: 10px; padding: 0px 6px;">move block</span>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <i class="fa fa-close" onclick="closZ39();" style="font-size: 28px; color: red;"></i>
                                    </div>
                                </div>

                                <div style="max-height: 700px; overflow: auto;">


                                    <i id="spz39wt" style="display: none; font-size: 24px"></i>
                                    <table id="tblZ39" style="background-color: transparent; width: 100%; font-size: 12px; cursor: pointer">
                                    </table>
                                </div>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><span style="margin-left: 2px;">Vol Acc No:</span></td>
                        <td>
                            <asp:TextBox ID="txtVAcc" runat="server" Width="120" Enabled="false" ToolTip="Current Accessioned Book will be created with New Accession No and New Volume "></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><span style="margin-left: 2px;">Vol. No:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVolume2" Width="120   " runat="server" Enabled="false" ToolTip="Current Accessioned Book will be created with New Accession No and New Volume "></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAddV" runat="server" CssClass="btnstyle" Style="border: 2px solid gray;" Text="Add Volume" Enabled="false" ToolTip="Current Accessioned Book will be created with New Accession No and New Volume by this Button." />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                <table class="table-condensed no-more-tables GenTable1">
                    <tr>
                        <td><%--MSSPL Suggestions --%>
                            <asp:RadioButtonList ID="rblDBOption" runat="server" OnSelectedIndexChanged="rblDBOption_SelectedIndexChanged" AutoPostBack="true" Visible="false" Font-Size="9" RepeatDirection="Horizontal" RepeatLayout="Flow" ToolTip="Retrieve Title suggestions from the database options shown in radio buttons.">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="color: green;"><%--(Suggestion Format: Title|Author|Edition|Vol.|Part|Lang.|AccNo)--%>		</td>
                        <td></td>
                        <td style="text-align: right">
                            <asp:Button ID="btnCopyMore" OnClientClick="popMulCopy();" runat="server" CssClass="btnstyle" Style="border: 2px solid gray; margin-top: 5px; width: auto" Enabled="false" Text="Multiple Copies" />
                            <asp:Button ID="btnMulVols" CssClass="btnstyle" runat="server" OnClientClick="opeIF();" Style="margin-top: 5px; margin-right: 5px; width: auto" Text="Add Multiple Vols" />
                            <ajax:ModalPopupExtender ID="MPVols" runat="server" TargetControlID="btnMulVols"
                                PopupControlID="pnlVols" OkControlID="btnDum1" CancelControlID="imClo"
                                PopupDragHandleControlID="Header" Y="100">
                            </ajax:ModalPopupExtender>


                        </td>
                    </tr>
                </table>
                <table class="no-more-tables table-condensed GenTable1">
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label2" runat="server" Text="Title:"></asp:Label>

                        </td>
                        <td>
                            <input type="hidden" id="hdnBookId" runat="server" value="" />
                            <input type="hidden" id="hdnOrigin" runat="server" value="" />
                            <asp:TextBox ID="txtMssPlTitl" placeholder="MSSPL Suggestion - do like others sugg" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPBG" runat="server" Style="display: none" OnClick="btnPBG_Click" />
                            <%--         CompletionListHighlightedItemCssClass
                            --%>
                            <asp:TextBox ID="txt_TestSuggoftite" runat="server" spellcheck="true" TextMode="SingleLine"></asp:TextBox>
                            <%--<ajax:AutoCompleteExtender ID="ExtTitle" runat="server" TargetControlID="txt_TestSuggoftite"
       MinimumPrefixLength="0"
       CompletionInterval   ="50"
       CompletionSetCount="50"
         FirstRowSelected="true"
          CompletionListCssClass="TAccno"
           CompletionListElementID="dvTi"
       OnClientItemSelected="TSelect"
          ServicePath=""
           EnableCaching="true"
           SkinID="None"
       UseContextKey="false"
       ServiceMethod="GetBooks" >
       </ajax:AutoCompleteExtender>
      <div id="dvTi">

      </div>--%>
                            <%--       CompletionListCssClass="Tscroll35"
       EnableCaching="true" 
    OnClientItemOver="ShowIt"
       OnClientHidden   ="HideIt"
       OnClientPopulating   ="TLoading"
       
    <input type="text" id="txt_TestSuggoftiteOLD" size="0" style="width:345px;" runat="server" name="txt_TestSuggoftite"  />--%>
                            <%--  <input id="TitlOne" runat="server" style="width:100px;" type="text" onkeyup="GetTitle(this);" />--%>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Sub Title:"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtSTitle" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="billno" runat="server" Text=" Bill No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBillNo" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Billdate" runat="server" Text="Bill Date:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBillDt" runat="server" Text=""></asp:TextBox>
                            <%--<ajax:CalendarExtender ID="CalBillDt" runat="server" TargetControlID="txtBillDt" Format="dd-MMM-yyyy"></ajax:CalendarExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="classno" runat="server" Text="Class No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClassNo" runat="server" Text=""></asp:TextBox>
                            <a href="javascript:void(0)" id="AncClasNo" runat="server" onclick="OpenIF('y');" title="Get Class Number suggestions from Server" style="display: none">CN</a>
                            <div id="dvGetCN" draggable="true" style="display: none; width: 850px; position: absolute; top: 10%; height: 400px; background-color: rgba(255,255,255,0.7); border: 2px solid grey; border-radius: 4px;">
                                <a href="javascript:void(0)" id="AncClasClos" onclick="OpenIF('n');">Close</a>
                                &nbsp;&nbsp; <i class="fa fa-arrows"></i>
                                <br />
                                <%--                                   <iframe id="ifCN" runat="server" src="ExtGetClanNo.aspx"  width="800" height="390"></iframe>--%>
                                <div>
                                    <asp:Label ID="labEr" runat="server" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:HiddenField ID="hdClasSer" runat="server" />
                                    Title:
           <asp:TextBox ID="txFramTitle" runat="server" Width="300" ToolTip="Main page click on Title fills this field" placeholder="title"></asp:TextBox>*
           Author Last Name:
                                    <asp:TextBox ID="txtAuth" runat="server" Width="150" ToolTip="Main page click on Author Lastname fills this field" placeholder="Last Name"></asp:TextBox>*
        <asp:Button ID="btnSb" runat="server" Text="Find By Title/Auth" OnClientClick="return finIt();" />
                                    <asp:Button ID="btnFin" runat="server" Style="display: none" Text="Find" OnClick="btnFin_Click" />
                                    <asp:Button ID="btnResClas" runat="server" Text="Reset" OnClick="btnResClas_Click" />
                                </div>
                                <div>
                                    ISBN:
            <asp:TextBox ID="txISBNClas" runat="server" Width="120" placeholder="Search by ISBN"></asp:TextBox>
                                    <asp:Button ID="btnSb2" runat="server" Text="Find By ISBN" OnClientClick="return finIt2();" />
                                    <asp:Button ID="btnFinISB" runat="server" Style="display: none" OnClick="btnFinISB_Click" />
                                </div>
                                <asp:Label ID="lb1" runat="server"></asp:Label>
                                <asp:TextBox ID="txVal" runat="server" Width="300" Height="40" placeholder="No entry-test only" TextMode="MultiLine"></asp:TextBox>
                                <div style="width: 850px; max-height: 250px; overflow: auto;">
                                    <asp:GridView ID="grdDat" AutoGenerateColumns="false" runat="server" HeaderStyle-BackColor="#336699">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Title
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblT" Width="350" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Author
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAuth" Width="350" runat="server" Text='<%# Eval("author") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Class No
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkClas" OnClientClick="return filPar(this);" runat="server" Text='<%# Eval("classno") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>



                            </div>

                        </td>
                        <td>
                            <asp:Label ID="bookno" runat="server" Text="Book No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBookNo" runat="server" Text=""></asp:TextBox>
                            <asp:Label ID="labBookN" ToolTip="Based on feature setting this value can be stored with individual Accession copy " runat="server" Text="Note*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="volume" runat="server" Text="Volume:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVolume" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Part" runat="server" Text="Part:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPart" runat="server" onblur="return NumericOnly(this);" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Edition" runat="server" Text="Edition:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEdition" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="EditionYear" runat="server" Text="Edition Year:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditionYear" runat="server" onblur="return NumericOnly(this);" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="pubyear" runat="server" Text="Pub Year:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPubYear" runat="server" onblur="return NumericOnly(this);" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Category" runat="server" Text="Category:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCat" runat="server" Height="30" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Item Type:"></asp:Label>

                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIType" runat="server" OnSelectedIndexChanged="ddlIType_SelectedIndexChanged" AutoPostBack="false" Height="30"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Item Status:"></asp:Label>

                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIStat" runat="server" Height="30" OnSelectedIndexChanged="ddlIStat_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Currency:"></asp:Label>

                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCurr" runat="server" OnSelectedIndexChanged="ddlCurr_SelectedIndexChanged" Height="30" AutoPostBack="false"></asp:DropDownList>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label6" runat="server" Text="Price:"></asp:Label>

                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtPrice" runat="server" onblur="return NumericOnly(this);" Width="40%" Text=""></asp:TextBox>
                            <asp:Label ID="lblsetVol" runat="server" Text="Set of Vol"></asp:Label>
                            <asp:TextBox ID="TxtSetOffVol" runat="server" placeholder="Set Of Volumes" Width="30%" onblur="return NumericOnly(this);" ToolTip="Set Of Volumes" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Language:"></asp:Label>

                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLang" runat="server" Height="30" AutoPostBack="false"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text=" Department:"></asp:Label>

                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDept" Height="30" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkPubl" runat="server" Text="Publisher:" CssClass="lnkB" ToolTip="Enter Publisher in Master with Details." OnClientClick="CallPub2();"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPubl" runat="server" Text=""></asp:TextBox>
                            <input id="hdnPublId" type="hidden" runat="server" value="" />
                            <asp:Panel ID="pnlPubl" runat="server" Style="width: 100%; height: 200px; border: 2px solid gray; display: none; background-color: white; text-align: center;">
                                Publisher "<asp:Label ID="lblPubl" runat="server" Text=""></asp:Label>" is NOT available in Local Database,<br />
                                If pressed "Yes", "Publisher Name" and "City" will be entered in Master.<br />
                                <br />
                                <asp:LinkButton ID="lnkY" runat="server" OnClick="lnkY_Click" Text="<u>Y</u>es" AccessKey="y"></asp:LinkButton>
                                <asp:LinkButton ID="lnkN" runat="server" OnClick="lnkN_Click" Text="<u>N</u>o" AccessKey="n"></asp:LinkButton>
                            </asp:Panel>
                            <asp:Button ID="Dummy" Style="visibility: hidden;" runat="server" />
                            <ajax:ModalPopupExtender ID="MPEPubl" runat="server" TargetControlID="Dummy" Y="200"
                                BackgroundCssClass="SDim" PopupControlID="pnlPubl">
                            </ajax:ModalPopupExtender>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkVend" runat="server" CssClass="lnkB" Text="Vendor:" OnClientClick="callVend();"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVend" runat="server" Text=""></asp:TextBox>
                            <input id="hdnVendid" type="hidden" runat="server" value="" />
                        </td>
                    </tr>
                    <%--</tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="isbn" runat="server" Text="ISBN:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtISBN" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="issn" runat="server" Text="ISSN:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtISSN" runat="server" Text=""></asp:TextBox>
                        </td>
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalpage" runat="server" Text="Total Page:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPages" runat="server" onblur="return NumericOnly(this);" Text=""></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="media" runat="server" Text="Media:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMedia" runat="server" Height="30" OnSelectedIndexChanged="ddlMedia_SelectedIndexChanged" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblForm" runat="server" Text="Form:"></asp:Label>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlForm" runat="server" Height="30" AutoPostBack="false">
                                    <asp:ListItem Selected="True" Text="Soft Bound" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Hard Bound" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="location" runat="server" Text="Location:"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtLocation" runat="server" Text="" ></asp:TextBox>
                                <input id="hdnLoc" runat="server" type="hidden" value="-1" />
                                <asp:TextBox ID="txtLoc2" runat="server" Style="visibility: hidden;" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label62" runat="server" Text="Search Text:"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txSearchText" runat="server"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="no-more-tables GenTable1" style="width: 100%; margin-left: auto; margin-right: auto; margin-top: 0px; border: 1px solid gray; padding: 2px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text=" Subject1"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSub1" runat="server" ToolTip="do like other fields" Text="" Style="margin-top: 3px"></asp:TextBox>
                                       <%-- <ajax:AutoCompleteExtender ID="ExtSub1" runat="server" TargetControlID="txtSub1"
                                            MinimumPrefixLength="0"
                                            CompletionInterval="50"
                                            CompletionSetCount="50"
                                            FirstRowSelected="true"
                                            CompletionListCssClass="TSubj"
                                            ServicePath=""
                                            EnableCaching="true" SkinID="None"
                                            ServiceMethod="GetSubj" UseContextKey="True">
                                        </ajax:AutoCompleteExtender>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Subject2"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSub2" runat="server" ToolTip="auto jquery do it" Text="" Style="margin-top: 3px"></asp:TextBox>
                                       <%-- <ajax:AutoCompleteExtender ID="ExtSub2" runat="server" TargetControlID="txtSub2"
                                            MinimumPrefixLength="0"
                                            CompletionInterval="50"
                                            CompletionSetCount="50"
                                            FirstRowSelected="true"
                                            CompletionListCssClass="TSubj"
                                            ServicePath=""
                                            EnableCaching="true" SkinID="None"
                                            ServiceMethod="GetSubj" UseContextKey="True">
                                        </ajax:AutoCompleteExtender>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Subject3"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSub3" runat="server" ToolTip="do auto compl. " Text="" Style="margin-top: 3px"></asp:TextBox>
<%--                                        <ajax:AutoCompleteExtender ID="ExtSub3" runat="server" TargetControlID="txtSub3"
                                            MinimumPrefixLength="0"
                                            CompletionInterval="50"
                                            CompletionSetCount="50"
                                            FirstRowSelected="true"
                                            CompletionListCssClass="TSubj"
                                            ServicePath=""
                                            EnableCaching="true" SkinID="None"
                                            ServiceMethod="GetSubj" UseContextKey="True">
                                        </ajax:AutoCompleteExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="Author"></asp:Label>
                                        <asp:Button ID="btnOAuth" CssClass="btnstyle" runat="server" Text="Other Than Authors Also" /><asp:Label ID="Label23" runat="server" Text="Show/Hide"></asp:Label>
                                        <ajax:CollapsiblePanelExtender ID="ColAuth" runat="server"
                                            CollapseControlID="btnOAuth" ExpandControlID="btnOAuth"
                                            Collapsed="true" ExpandDirection="Vertical"
                                            TargetControlID="AuthPlus"></ajax:CollapsiblePanelExtender>
                                        <asp:Panel ID="AuthPlus" runat="server" Width="100%" ScrollBars="Vertical">
                                            <table style="width: 98%; margin-left: 1%; margin-top: 5px; background-color: #f0f0f9; border: 0px" class="no-more-tables table-condensed GenTable1">
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label24" runat="server" Text="Editor F Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdF1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label25" runat="server" Text="Editor M Name1"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdM1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label26" runat="server" Text="Editor L Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdL1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label27" runat="server" Text="Editor F Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdF2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label28" runat="server" Text="Editor M Name2"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdM2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label29" runat="server" Text="Editor L Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdL2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label30" runat="server" Text="Editor F Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdF3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label31" runat="server" Text="Editor M Name3"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdM3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label32" runat="server" Text="Editor L Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdL3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label33" runat="server" Text="Comp. F Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompF1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label34" runat="server" Text="Comp. M Name1"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompM1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label35" runat="server" Text="Comp. L Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompL1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label36" runat="server" Text="Comp. F Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompF2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label37" runat="server" Text="Comp. M Name2"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompM2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label38" runat="server" Text="Comp. L Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompL2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label39" runat="server" Text="Comp. F Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompF3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label40" runat="server" Text="Comp. M Name3"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompM3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label41" runat="server" Text="Comp. L Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompL3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label42" runat="server" Text="Illus. F Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlF1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label43" runat="server" Text="Illus. M Name1"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlM1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label44" runat="server" Text="Illus. L Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlL1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label45" runat="server" Text="Illus. F Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlF2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label46" runat="server" Text="Illus. M Name2"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlM2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label47" runat="server" Text="Illus. L Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlL2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label48" runat="server" Text="Illus. F Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlF3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label49" runat="server" Text="Illus. M Name3"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlM3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label50" runat="server" Text="Illus. L Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIlL3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label51" runat="server" Text="Tran. F Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranF1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label52" runat="server" Text="Tran. M Name1"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranM1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label53" runat="server" Text="Tran. L Name1"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranL1" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label54" runat="server" Text="Tran. F Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranF2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label55" runat="server" Text="Tran. M Name2"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranM2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label56" runat="server" Text="Tran. L Name2"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranL2" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><span>
                                                        <asp:Label ID="Label57" runat="server" Text="Tran. F Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranF3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label58" runat="server" Text="Tran. M Name3"></asp:Label></span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranM3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                    <td><span>
                                                        <asp:Label ID="Label59" runat="server" Text="Tran. L Name3"></asp:Label></span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTranL3" runat="server" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>

                                            </table>
                                            <br />
                                        </asp:Panel>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Firstname:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthF1" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="Middlename:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthM1" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Lastname:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthL1" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Label ID="Label60" runat="server" Font-Bold="true" Text="Author 2"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Firstname:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthF2" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Middlename:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthM2" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Lastname:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthL2" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Label ID="Label61" runat="server" Text="Author 3" Font-Bold="true"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="Firstname:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthF3" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="Middlename:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthM3" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Lastname:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthL3" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        <div style="position: fixed; left: 20px; bottom: 60px;">
            <asp:Button ID="btnSave" runat="server" AccessKey="S" CssClass="btnstyle" Text="Save" OnClick="btnSave_Click" /><br />
            <asp:Button ID="btnReset" runat="server" AccessKey="R" CssClass="btnstyle" Text="Reset" OnClick="btnReset_Click" /><br />
            <asp:Button ID="btnDel" runat="server" AccessKey="d" CssClass="btnstyle" Enabled="false" OnClick="btnDel_Click" Text="Delete" />
            <br />
            <asp:Button ID="BtnOk" Visible="false" runat="server" CssClass="btnstyle" Text="Correct" OnClick="BtnOk_Click" /><br />
            <asp:Button ID="BtnIncorrect" Visible="false" runat="server" CssClass="btnstyle" Text="Incorrect" OnClick="BtnIncorrect_Click" />
            <br />
            <asp:Label ID="labCorrect" runat="server" Font-Bold="true" Visible="false"></asp:Label>
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:CheckBox ID="ChkShowSearch" runat="server" AutoPostBack="true" Text="Books Search" OnCheckedChanged="ChkShowSearch_CheckedChanged" />

    <asp:Panel ID="pnlSearch" Visible="false" runat="server">

        <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container tableborderst">
                    <CatS:Catalogs ID="FindBooks" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetdatePicker();
            //evDataCookies();
            inputCAdditems();
            $('[id$=txtAccNo]').on('keyup', function () {
                AccNoJSugg($(this));
            });
            txt_TestSuggoftite_keyup();
            $('[id$=txtPubl]').on('keyup', function () {
                pblSugg($(this));
            });

            $('[id$=txtVend]').on('keyup', function () {
                VendJSugg($(this));
            });
            $('[id$=txtGrdVendor]').on('keyup', function () {
                VendGrdJSugg($(this));
            });
            
            $('[id$=txtGrdLocation]').on('keyup', function () {
                VendGrdLocationJSugg($(this));
            });
            // 
            $('[id$=txLocationCP]').on('keyup', function () {
              GetLocation2JSugg($(this));
            });
            //  
            $('[id$=txtAccNoCopy]').on('keyup', function () {
                AccNoCPJSugg($(this));
            });
            $('[id$=txtCopyNo]').on('keyup', function () {
                AccNoCPNoJSugg($(this));
            });
            $('[id$=txVendorCP]').on('keyup', function () {
                VendCPJSugg($(this));
            });
            
            $('[id$=txtLocation]').on('keyup', function () {
                VendLocation2JSugg($(this));
            });
            $('[id$=txt_TestSuggoftite]').on('keyup', function () {
                SuggOfTitleJSugg($(this));
            });

        });

        function pblSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txtPubl]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetPublJsugg',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;
                    console.log(i.item);
                    $('[id$=hdnPublId]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        //  
        function VendJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txtVend]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetVendorJSugg',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                    $('[id$=hdnVendid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });

        }
        function VendCPJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txVendorCP]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetVendorJSugg',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                   // $('[id$=hdnVendid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });

        }
        function VendGrdJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txtGrdVendor]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetVendorJSugg',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                    //   $('[id$=hdnVendid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });

        }
        function VendGrdJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txtGrdVendor]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetVendorJSugg',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                    //   $('[id$=hdnVendid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });

        }
        function VendGrdLocationJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txtGrdLocation]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/txtGrdLocation',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                    //   $('[id$=hdnVendid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });

        }
        function VendLocation2JSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txtLocation]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetLocation2Jq',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                    $('[id$=hdnLoc]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });

        }
        function AccNoJSugg(ele) {
            let nombre = $(ele).val();

            $('[id$=txtAccNo]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetAccNoJq',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;
                    var ErrAccNo = vdata.split("|");

                    document.getElementById("<%=txtAccNo.ClientID%>").value = ErrAccNo[0];
                     document.getElementById("<%=hdnAccNo.ClientID%>").value = ErrAccNo[0];
                     //        document.getElementById("<%=hdnCtrl_no.ClientID%>").value =  ErrAccNo[1];
                     //        __doPostBack('__Page', 'AccNoSelected');
                     $("[id$=btnAccNoSel]").click();
                 },
                 minLength: 1
             }).focus(function () {
                 $(this).autocomplete("search");
             });
        }
        function AccNoCPJSugg(ele) {
            let nombre = $(ele).val();

            $('[id$=txtAccNoCopy]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetAccNoJq',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;
                  
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function AccNoCPNoJSugg(ele) {
            let nombre = $(ele).val();

            $('[id$=txtCopyNo]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetCopyNoJq',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;

                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }

        function GetLocation2JSugg(ele) {
            let nombre = $(ele).val();

            $('[id$=txLocationCP]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetLocationJq',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;
                    document.getElementById("<%=hdLocidCP.ClientID%>").value = vdata;

                 },
                 minLength: 1
             }).focus(function () {
                 $(this).autocomplete("search");
             });
        }

        function UCAccnSent(accn) {  //called from user control
            document.getElementById("<%=hdnAccNo.ClientID%>").value = accn;
            $("[id$=btnAccNoSel]").click();

        }
        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetdatePicker();
                    // evDataCookies();
                    inputCAdditems();
                    $('[id$=txtAccNo]').on('keyup', function () {
                        AccNoJSugg($(this));
                    });
                    txt_TestSuggoftite_keyup();
                    $('[id$=txtPubl]').on('keyup', function () {
                        pblSugg($(this));
                    });
                    $('[id$=txtVend]').on('keyup', function () {
                        VendJSugg($(this));
                    });
                    $('[id$=txtGrdVendor]').on('keyup', function () {
                        VendGrdJSugg($(this));
                    });
                    $('[id$=txtGrdLocation]').on('keyup', function () {
                        VendGrdLocationJSugg($(this));
                    });
                    $('[id$=txLocationCP]').on('keyup', function () {
                        GetLocation2JSugg($(this));
                    });
                    $('[id$=txtAccNoCopy]').on('keyup', function () {
                        AccNoCPJSugg($(this));
                    });
                    $('[id$=txtCopyNo]').on('keyup', function () {
                        AccNoCPNoJSugg($(this));
                    });
                    $('[id$=txVendorCP]').on('keyup', function () {
                        VendCPJSugg($(this));
                    });
                    $('[id$=txtLocation]').on('keyup', function () {
                        VendLocation2JSugg($(this));
                    });
                    $('[id$=txt_TestSuggoftite]').on('keyup', function () {
                        SuggOfTitleJSugg($(this));
                    });

                    
                }
            });
        };
        function SetdatePicker() {
            $("[id$=txtBillDt],[id$=txtacccopydt],[id$=txtDocDate],[id$=txtDt],[id$=txBillDtCP],[id$=txtGrdbilldate]").datepicker({
                onSelect: function (date) {
                   // evDataCookies($(this));
                },
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DfATE FORMAT.
            });

        }
        function inputCAdditems() {
        }
        function SuggOfTitleJSugg(ele) {
            let nombre = $(ele).val();

            $('[id$=txt_TestSuggoftite]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetTitleJq',
                        data: JSON.stringify({ "prefixText": nombre }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,// item.split('-')[0],
                                    val: item.value// item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    let vdata = i.item.val;
                    document.getElementById("<%=txtAccNo.ClientID%>").value = vdata;
                    document.getElementById("<%=hdnAccNo.ClientID%>").value = vdata;
                    //        document.getElementById("<%=hdnCtrl_no.ClientID%>").value =  ErrAccNo[1];
                    //        __doPostBack('__Page', 'AccNoSelected');
                    $('[id$=txtAccNo]').val(vdata);
                    $("[id$=btnAccNoSel]").click();

                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function txt_TestSuggoftite_keyup() {  //*****Not working yet*/

            return "";
            var Prefix = document.getElementById("<%=txt_TestSuggoftite.ClientID%>").value;
            $.ajax({
                type: "POST",
                url: "MassCatEntry.aspx/GetDataForTitle",
                data: '{"Prefix":' + JSON.stringify(Prefix) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (data) {
                    //         alert(data.d);
                    $("[id$=txt_TestSuggoftite]").autocomplete({
                        source: data.d,
                        focus: function (event, ui) {
                            var TPrfeix = ui.item.value;
                            $.ajax({
                                type: "POST",
                                url: "MassCatEntry.aspx/GetDataAccToTitle",
                                data: '{"TPrfeix":' + JSON.stringify(TPrfeix) + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",

                                success: function (data) {
                                    ShowIt1(data.d);

                                }
                                ,
                                failure: function (response) {

                                    alert("Failure : " + response.d);
                                },
                                error:
                                    function (xhr, ajaxOptions, thrownError) {
                                        alert(xhr.status);
                                        alert(xhr.responseText);
                                        alert(thrownError);

                                    }
                            });
                        },

                        create: function () {

                        },
                        select: function () {
                          //  HideIt();
                            TSelect2();
                        },
                        change: function () { HideIt(); },
                        mindLength: 0
                    });

                }
                ,
                failure: function (response) {

                    alert("Failure : " + response.d);
                },
                error:
                    function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(xhr.responseText);
                        alert(thrownError);

                    }
            });
        }

    </script>
</asp:Content>

