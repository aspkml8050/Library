<%@ Page Language="C#" Async="true"  MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="ItemDisplayEdit.aspx.cs" Inherits="Library.ItemDisplayEdit" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<%@ Register TagPrefix="CatE" TagName="Catalog" Src="~/UCCatalogShow.ascx" %>

<asp:Content ID="hd" runat="server" ContentPlaceHolderID="head">
    <style>
        select {
            height: 36px;
            border-color: lightgray;
            border-radius: 3px;
        }

        selErr {
            border-color: red;
        }
    </style>
    <script type="text/javascript">
        function UCAccnSent(accn) {  //called from user control
            document.getElementById("<%=txaccessionnumber.ClientID%>").value = accn;
            $("[id$=btnPostBack]").click();

        }
        //
        function UCCtrlSent(ctrlno) {  //called from user control
            document.getElementById("<%=hdctrl_no.ClientID%>").value = ctrlno;
            $("[id$=btnPostBack]").click();

        }

        //On Page Load.
        $(function () {
            //       ForDataTable();
            SetdatePicker();
            $('[id$=txVendor]').on('keyup', function () {
                VendJSugg($(this));
            });
            $('[id$=txLocation]').on('keyup', function () {
                LocationJSugg($(this));
            });
            $('[id$=txpublisher]').on('keyup', function () {
                PublJSugg($(this));
            });
            $('[id$=txsubject1]').on('keyup', function () {
                GetSubj1JSugg($(this));
            });
            $('[id$=txsubject2]').on('keyup', function () {
                GetSubj2JSugg($(this));
            });
            $('[id$=txsubject3]').on('keyup', function () {
                GetSubj3JSugg($(this));
            });
        });
        function valiDate() {

        }

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    //                  ForDataTable();
                    SetdatePicker();
                    valiDate();
                    $('[id$=txVendor]').on('keyup', function () {
                        VendJSugg($(this));
                    });
                    $('[id$=txLocation]').on('keyup', function () {
                        LocationJSugg($(this));
                    });
                    $('[id$=txpublisher]').on('keyup', function () {
                        PublJSugg($(this));
                    });
                    $('[id$=txsubject1]').on('keyup', function () {
                        GetSubj1JSugg($(this));
                    });
                    $('[id$=txsubject2]').on('keyup', function () {
                        GetSubj2JSugg($(this));
                    });
                    $('[id$=txsubject3]').on('keyup', function () {
                        GetSubj3JSugg($(this));
                    });
                }
            });
        };

        function ForDataTable() {
            try {
                //              var grdId = $("[id$=hdnGrdId]").val();
                //            $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                //          $('#' + grdId + ' tr:first td').contents().unwrap().wrap('<th></th>');

            }
            catch (err) {
            }
        }
        function SetdatePicker() {
            $("[id$=txcatalogdate],[id$=txbillDate],[id$=txaccessiondate],[id$=txreleasedate],[id$=txLoadingDate],[id$=txcatalogdate1]").datepicker({
                changeMonth: true,//this option for allowing user to select month ,[id$=txtrelease],[id$=txtDocDate]
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy',  // CHANGE DATE FORMAT.
                enableOnReadonly: false
            });
        }

        function changeTab(anc) {
            alert(1);
            $('.nav-link').removeClass('active');
            $(anc).addClass('.active');
            $('.tab-pane').removeClass('active');
            $('.tab-pane').addClass('fade');
            alert(2);
            let hr = $(anc).attr('href');
            alert(3);
            alert(hr);
            return false;
        }
        function show2() {
            $('[href="#menu2"]').tab('show');
        }
        function VendJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txVendor]').autocomplete({
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
                    $('#MainContent_hdVendorid').val(i.item.val);
                    //   $('[id$=hdnVendid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function LocationJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txLocation]').autocomplete({
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
                    $('[id$=hdlocationid]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function PublJSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txpublisher]').autocomplete({
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
                    $('[id$=hdpublishercode]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function GetSubj1JSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txsubject1]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetSubjectJSugg',
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
                    // $('[id$=hdpublishercode]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function GetSubj2JSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txsubject2]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetSubjectJSugg',
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
                    // $('[id$=hdpublishercode]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
        function GetSubj3JSugg(ele) {
            let nombre = $(ele).val();
            $('[id$=txsubject3]').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'MssplSugg.asmx/GetSubjectJSugg',
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
                    // $('[id$=hdpublishercode]').val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }
    </script>
</asp:Content>
<asp:Content ID="main" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upds">
        <ContentTemplate>
            <div class="container mt-1">
                <!-- Nav tabs -->
                <asp:TextBox ID="txApiAccn" runat="server"  ></asp:TextBox>
                <asp:Button ID="btnApiFind" runat="server" CssClass="btn btn-secondory " Text="Find" OnClick="btnApiFind_Click" />
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item tabmss">
                        <a class="nav-link active" data-bs-toggle="tab" href="#Accn">Accession</a>
                    </li>
                    <li class="nav-item tabmss">
                        <a class="nav-link" data-bs-toggle="tab" href="#menu1">Statement of Resp</a>
                    </li>
                    <li class="nav-item tabmss">
                        <a class="nav-link" data-bs-toggle="tab" href="#menu2">Catalog</a>
                    </li>
                    <li class="nav-item tabmss">
                        <a class="nav-link" data-bs-toggle="tab" href="#menuseries">Series</a>
                    </li>
                    <li class="nav-item tabmss">
                        <a class="nav-link" data-bs-toggle="tab" href="#menunotes">Notes</a>
                    </li>
                    <li class="nav-item tabmss">
                        <a class="nav-link" data-bs-toggle="tab" href="#menucopies">Accn Copies</a>
                    </li>
                </ul>

                <!-- Tab panes -->
                <div class="tab-content">
                    <div id="Accn" class="container tab-pane active">

                        <asp:HiddenField ID="hdaccessionid" runat="server" />
                        <asp:HiddenField ID="hdsrno" runat="server" />
                        <asp:HiddenField ID="hdreleased" runat="server" />
                        <asp:HiddenField ID="hdsrNoOld" runat="server" />
                        <asp:HiddenField ID="hdctrl_no" runat="server" />
                        <asp:HiddenField ID="hdDSrno" runat="server" />
                        <asp:HiddenField ID="hdissuestatus" runat="server" />
                        <asp:HiddenField ID="hdcheckstatus" runat="server" />
                        <asp:Button ID="btnPostBack" runat="server" Style="display: none" OnClick="btnPostBack_Click" />
                        <div id="dvAccn" class="container mt-1 border rounded">
                            <div class="row  p-1">
                                <div class="col-md-2  ">
                                    <label for="ddlItemtype" class="label">Item Type</label>
                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:DropDownList ID="ddlItemtype" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-2  m-0">
                                    <label for="ddlItemStat" class="label">Item Status</label>
                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:DropDownList ID="ddlItemStat" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txcatalogdate" class="label">Catalog Date*</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txcatalogdate" ReadOnly="true" runat="server" ToolTip="Enter/Change this value in Catalog Tab" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="ddlMedia" class="label">Media</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:DropDownList ID="ddlMedia" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>

                                </div>
                           <%-- </div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txindentnumber" class="label">Indent /Order No</label>
                                </div>
                                <div class="col-md-4  m-0">
                                    <div class="row">
                                      <asp:TextBox ID="txindentnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txordernumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txaccessiondate" class="label">Accn Date</label>
                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txaccessiondate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                           <%-- </div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txreleasedate" class="label">Release Date</label>
                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txreleasedate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="ddlprogram_id" class="label">Program/Course</label>
                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:DropDownList ID="ddlprogram_id" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txbiilNo" class="label">Doc No</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txbiilNo" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txbillDate" class="label">Doc. Date</label>
                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txbillDate" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txaccessionnumber" class="label">Accn No</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txaccessionnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txCopynumber" class="label">Copy No</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txCopynumber" runat="server" MaxLength="2" CssClass="form-control"></asp:TextBox>
                                </div>
                           <%-- </div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="ddlForm" class="label">Form</label>
                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:DropDownList ID="ddlForm" Width="100%" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Soft Bound" Value="Soft Bound"></asp:ListItem>
                                        <asp:ListItem Text="Hard Bound" Value="Hard Bound"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txbooktitle" class="label">Title</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txbooktitle" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txbookprice" class="label">Price</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txbookprice" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txLoadingDate" class="label">Loading Date</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txLoadingDate" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="labCheckStatus" class="label">Check Status</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:Label ID="labCheckStatus" Width="50" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txeditionyear" class="label">Edition Year</label>
                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txeditionyear" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txspecialprice" class="label">Special Price</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txspecialprice" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txpubYear" class="label">Pub. Year</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txpubYear" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="ddlOriginalCurrency" class="label">Original Currency</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:DropDownList ID="ddlOriginalCurrency" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>

                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txOriginalPrice" class="label">Original Price</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txOriginalPrice" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txVendor" class="label">Vendor</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txVendor" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdVendorid" runat="server" />
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="ddlDeptCode" class="label">Department</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:DropDownList ID="ddlDeptCode" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>

                                </div>
                           <%-- </div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="ddlItemCategoryCode" class="label">Category</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:DropDownList ID="ddlItemCategoryCode" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>

                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txLocation" class="label">Location</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdlocationid" runat="server" />

                                </div>
                            <%--</div>
                            <div class="row p-1">--%>
                                <div class="col-md-2  m-0">
                                    <label for="txRfidid" class="label">RFID</label>

                                </div>
                                <div class="col-md-4  m-0">
                                    <asp:TextBox ID="txRfidid" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2 m-0">
                                    <label for="txBookNumber" class="label">Book Number(Accn)</label>

                                </div>
                                <div class="col-md-4 m-0">
                                    <asp:TextBox ID="txBookNumber" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                        </div>



                    </div>
                    <div id="menu1" class="container tab-pane fade">
                        <b>Author</b>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txfirstname1" class="label">First Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txfirstname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txmiddlename1" class="label">Middle Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txmiddlename1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txlastname1" class="label">Last Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txlastname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txfirstname2" class="label">First Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txfirstname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txmiddlename2" class="label">Middle Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txmiddlename2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txlastname2" class="label">Last Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txlastname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txfirstname3" class="label">First Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txfirstname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txmiddlename3" class="label">Middle Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txmiddlename3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txlastname3" class="label">Last Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txlastname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>

                            </div>
                        </div>
                        <hr />
                        <b>Editor</b>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txeditorFname1" class="label">First Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorFname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txeditorMname1" class="label">Middle Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorMname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txeditorLname1" class="label">Last Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorLname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txeditorFname2" class="label">First Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorFname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txeditorMname2" class="label">Middle Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorMname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txeditorLname2" class="label">Last Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorLname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txeditorFname3" class="label">First Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorFname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txeditorMname3" class="label">Middle Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorMname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txeditorLname3" class="label">Last Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeditorLname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>

                            </div>
                        </div>
                        <hr />
                        <b>Compiler</b>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txCompilerFname1" class="label">First Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerFname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txCompilerMname1" class="label">Middle Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerMname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txCompilerLname1" class="label">Last Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerLname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txCompilerFname2" class="label">First Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerFname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txCompilerMname2" class="label">Middle Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerMname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txCompilerLname2" class="label">Last Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerLname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txCompilerFname3" class="label">First Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerFname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txCompilerMname3" class="label">Middle Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerMname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txCompilerLname3" class="label">Last Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCompilerLname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <b>Illustrator</b>
                        <div class="contaner">

                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txillusFname1" class="label">First Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusFname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txillusMname1" class="label">Middle Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusMname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txillusLname1" class="label">Last Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusLname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txillusFname2" class="label">First Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusFname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txillusMname2" class="label">Middle Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusMname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txillusrLname2" class="label">Last Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusrLname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txillusFname3" class="label">First Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusFname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txillusMname3" class="label">Middle Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusMname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txillusLname3" class="label">Last Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txillusLname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                        </div>
                        <hr />
                        <b>Translator</b>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txTranslatorFname1" class="label">First Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorFname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txTranslatorMname11" class="label">Middle Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorMname11" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txTranslatorLname1" class="label">Last Name 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorLname1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txTranslatorFname2" class="label">First Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorFname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txTranslatorMname2" class="label">Middle Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorMname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txTranslatorLname2" class="label">Last Name 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorLname2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txTranslatorFname3" class="label">First Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorFname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txTranslatorMname3" class="label">Middle Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorMname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txTranslatorLname3" class="label">Last Name 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txTranslatorLname3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>

                            </div>
                        </div>
                        <hr />
                        <b>Corporate/Personal</b>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txCorporateName" class="label">Corporate Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCorporateName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txDateAssociated" class="label">Date Associated</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txDateAssociated" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txPersonalName" class="label">Personal Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txPersonalName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="menu2" class="container tab-pane fade">
                        <div class="container">

                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txcatalogdate1" class="label">Catalog Date</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txcatalogdate1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlbooktype" class="label">Book Type</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlbooktype" runat="server" CssClass="form-select" Width="100%"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txvolumenumber" class="label">Vol. No</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txvolumenumber" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txinitpages" class="label">Init Pgs</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txinitpages" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txpages" class="label">Pages</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txpages" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txparts" class="label">Parts</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txparts" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txleaves" class="label">Leaves</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txleaves" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlBound" class="label">Form</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlBound" Width="100%" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Soft Bound" Value="Soft Bound"></asp:ListItem>
                                        <asp:ListItem Text="Hard Bound" Value="Hard Bound"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txtitle" class="label">Title</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txpublishercode" class="label">Publisher</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:HiddenField ID="hdpublishercode" runat="server" />
                                    <asp:TextBox ID="txpublisher" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txclassnumber" class="label">Class Number</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txclassnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txbooknumber2" class="label">Book Number</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txbooknumber2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txedition" class="label">Edition</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txedition" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txisbn" class="label">ISBN</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txisbn" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txsubject1" class="label">Subject 1</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txsubject1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txsubject2" class="label">Subject 2</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txsubject2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txsubject3" class="label">Subject 3</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txsubject3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txBooksize" class="label">Book Size</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txBooksize" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txLCCN" class="label">LCCN</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txLCCN" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txVolumepages" class="label">Vol Pgs</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txVolumepages" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txbiblioPages" class="label">Biblio Pgs</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txbiblioPages" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlbookindex" class="label">Book Index</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlbookindex" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="ddlillustration" class="label">Illustration</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlillustration" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlvariouspaging" class="label">Various Pages</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlvariouspaging" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txmaps" class="label">Maps</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txmaps" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txETalEditor" class="label">Editor</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlETalEditor" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txETalCompiler" class="label">Compiler</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlETalCompiler" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="txETalIllus" class="label">Illustrator</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlETalIllus" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="ddlETalTrans" class="label">Translator</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlETalTrans" runat="server" CssClass="form-select">
                                        <asp:ListItem Selected="True" Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <label for="txGeoArea" class="label">Geo Area</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txGeoArea" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-2">
                                    <label for="txPhyExtent" class="label">Physical Extent</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txPhyExtent" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txPhyOther" class="label">Physical Other</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txPhyOther" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txpubDate" class="label">Publication Date</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txpubDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txBookCost" class="label">Book Cost</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txBookCost" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-2">
                                    <label for="txlatestTransDate" class="label">Latest Trans Date</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txlatestTransDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txItemCategory" class="label">Item Category</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txItemCategory" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txOriginalCurrency" class="label">Original Currency</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddl2OriginalCurrency" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="txOriginalPrice" class="label">Original Price</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="tx2OriginalPrice" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txaccmaterialhistory" class="label">Accompany Material</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txaccmaterialhistory" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txMaterialDesignation" class="label">Material Designation</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txMaterialDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txissn" class="label">ISSN</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txissn" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txVolume" class="label">Volume</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txVolume" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txdept" class="label">Department</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddldept" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="txlanguage_id" class="label">Language</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddllanguage_id" Width="100%" runat="server" CssClass="form-select"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txpart" class="label">Part</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txpart" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txeBookURL" class="label">Book URL</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txeBookURL" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txFixedData" class="label">Fixed Data</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txFixedData" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txcat_Source" class="label">Catalog Source</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txcat_Source" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txIdentifier" class="label">Identifier</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txIdentifier" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txfirstname" class="label">Publisher</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txfirstname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txpercity" class="label">City</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txpercity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txperstate" class="label">State</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txperstate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txpercountry" class="label">Country</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txpercountry" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txperaddress" class="label">Address</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txperaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txdepartmentname" class="label">Department Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txdepartmentname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txControl008" class="label">Control No</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txControl008" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txlanguage_name" class="label">Name of Language</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txlanguage_name" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txPublisherNo" class="label">Publisher No</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txPublisherNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txPubSource" class="label">Publisher Source</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txPubSource" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>

                            </div>
                        </div>
                    </div>
                    <div id="menuseries" class="container tab-pane fade">
                        <b title="look libnew17 around MarcTagStruct(490) - to add later ">Series*</b>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txSeriesName" class="label">Series Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txSeriesName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txseriesNo" class="label">Series No</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txseriesNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div id="menunotes" class="container tab-pane fade">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txSubtitle" class="label">Sub Title</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txSubtitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txParalleltype" class="label">Parallel Type</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txParalleltype" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txConfName" class="label">Conference Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txConfName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txConfYear" class="label">Conference Year</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txConfYear" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txBNNote" class="label">Book Info</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txBNNote" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txCNNote" class="label">Content</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txCNNote" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                           
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txAbstract" class="label">Abstract</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txAbstract" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txProgram_name" class="label">Programme Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txProgram_name" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="txConfPlace" class="label">Conference Place</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txConfPlace" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="menucopies" class="container tab-pane fade">
                        <b>Copies</b>
                        <div class="container">
                         <asp:GridView ID="grdCopies" runat="server" CssClass="table" AutoGenerateColumns="false">
                             <Columns>
                                 <asp:TemplateField HeaderText="Accn">
                                     <ItemTemplate>
<asp:HiddenField ID="hdaccessionnumber" runat="server" Value='<%# Eval("accessionnumber") %>'></asp:HiddenField>
<asp:HiddenField ID="hdordernumber" runat="server" Value='<%# Eval("ordernumber") %>'></asp:HiddenField>
<asp:HiddenField ID="hdindentnumber" runat="server" Value='<%# Eval("indentnumber") %>'></asp:HiddenField>
<asp:HiddenField ID="hdform" runat="server" Value='<%# Eval("form") %>'></asp:HiddenField>
<asp:HiddenField ID="hdaccessionid" runat="server" Value='<%# Eval("accessionid") %>'></asp:HiddenField>
<asp:HiddenField ID="hdaccessioneddate" runat="server" Value='<%# Eval("accessioneddate") %>'></asp:HiddenField>
<asp:HiddenField ID="hdbooktitle" runat="server" Value='<%# Eval("booktitle") %>'></asp:HiddenField>
<asp:HiddenField ID="hdsrno" runat="server" Value='<%# Eval("srno") %>'></asp:HiddenField>
<asp:HiddenField ID="hdreleased" runat="server" Value='<%# Eval("released") %>'></asp:HiddenField>
<asp:HiddenField ID="hdbookprice" runat="server" Value='<%# Eval("bookprice") %>'></asp:HiddenField>
<asp:HiddenField ID="hdsrNoOld" runat="server" Value='<%# Eval("srNoOld") %>'></asp:HiddenField>
<asp:HiddenField ID="hdStatus" runat="server" Value='<%# Eval("Status") %>'></asp:HiddenField>
<asp:HiddenField ID="hdReleaseDate" runat="server" Value='<%# Eval("ReleaseDate") %>'></asp:HiddenField>
<asp:HiddenField ID="hdIssueStatus" runat="server" Value='<%# Eval("IssueStatus") %>'></asp:HiddenField>
<asp:HiddenField ID="hdLoadingDate" runat="server" Value='<%# Eval("LoadingDate") %>'></asp:HiddenField>
<asp:HiddenField ID="hdCheckStatus" runat="server" Value='<%# Eval("CheckStatus") %>'></asp:HiddenField>
<asp:HiddenField ID="hdctrl_no" runat="server" Value='<%# Eval("ctrl_no") %>'></asp:HiddenField>
<asp:HiddenField ID="hdeditionyear" runat="server" Value='<%# Eval("editionyear") %>'></asp:HiddenField>
<asp:HiddenField ID="hdCopynumber" runat="server" Value='<%# Eval("Copynumber") %>'></asp:HiddenField>
<asp:HiddenField ID="hdspecialprice" runat="server" Value='<%# Eval("specialprice") %>'></asp:HiddenField>
<asp:HiddenField ID="hdpubYear" runat="server" Value='<%# Eval("pubYear") %>'></asp:HiddenField>
<asp:HiddenField ID="hdbiilNo" runat="server" Value='<%# Eval("biilNo") %>'></asp:HiddenField>
<asp:HiddenField ID="hdbillDate" runat="server" Value='<%# Eval("billDate") %>'></asp:HiddenField>
<asp:HiddenField ID="hdcatalogdate" runat="server" Value='<%# Eval("catalogdate") %>'></asp:HiddenField>
<asp:HiddenField ID="hdItem_type" runat="server" Value='<%# Eval("Item_type") %>'></asp:HiddenField>
<asp:HiddenField ID="hdOriginalPrice" runat="server" Value='<%# Eval("OriginalPrice") %>'></asp:HiddenField>
<asp:HiddenField ID="hdOriginalCurrency" runat="server" Value='<%# Eval("OriginalCurrency") %>'></asp:HiddenField>
<asp:HiddenField ID="hduserid" runat="server" Value='<%# Eval("userid") %>'></asp:HiddenField>
<asp:HiddenField ID="hdvendor_source" runat="server" Value='<%# Eval("vendor_source") %>'></asp:HiddenField>
<asp:HiddenField ID="hdprogram_id" runat="server" Value='<%# Eval("program_id") %>'></asp:HiddenField>
<asp:HiddenField ID="hdDeptCode" runat="server" Value='<%# Eval("DeptCode") %>'></asp:HiddenField>
<asp:HiddenField ID="hdDSrno" runat="server" Value='<%# Eval("DSrno") %>'></asp:HiddenField>
<asp:HiddenField ID="hdDeptName" runat="server" Value='<%# Eval("DeptName") %>'></asp:HiddenField>
<asp:HiddenField ID="hdItemCategoryCode" runat="server" Value='<%# Eval("ItemCategoryCode") %>'></asp:HiddenField>
<asp:HiddenField ID="hdItemCategory" runat="server" Value='<%# Eval("ItemCategory") %>'></asp:HiddenField>
<asp:HiddenField ID="hdLoc_id" runat="server" Value='<%# Eval("Loc_id") %>'></asp:HiddenField>
<asp:HiddenField ID="hdRfidId" runat="server" Value='<%# Eval("RfidId") %>'></asp:HiddenField>
<asp:HiddenField ID="hdBookNumber" runat="server" Value='<%# Eval("BookNumber") %>'></asp:HiddenField>
<asp:HiddenField ID="hdSetOFbooks" runat="server" Value='<%# Eval("SetOFbooks") %>'></asp:HiddenField>
<asp:HiddenField ID="hdSearchText" runat="server" Value='<%# Eval("SearchText") %>'></asp:HiddenField>
<asp:HiddenField ID="hdIpAddress" runat="server" Value='<%# Eval("IpAddress") %>'></asp:HiddenField>
<asp:HiddenField ID="hdTransNo" runat="server" Value='<%# Eval("TransNo") %>'></asp:HiddenField>
<asp:HiddenField ID="hdAppName" runat="server" Value='<%# Eval("AppName") %>'></asp:HiddenField>
<asp:HiddenField ID="hdVendorId" runat="server" Value='<%# Eval("VendorId") %>'></asp:HiddenField>

                                         <asp:LinkButton ID="labaccessionnumber" OnClick="labaccessionnumber_Click" runat="server" Text='<%# Eval("accessionnumber") %>'></asp:LinkButton>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Copy">
                                     <ItemTemplate>
<asp:Label ID="labCopynumber" runat="server" Text='<%# Eval("Copynumber") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill No">
                                     <ItemTemplate>
                                         <asp:Label ID="labbiilNo" runat="server" Text='<%# Eval("biilNo") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill Date">
                                     <ItemTemplate>
                                         <asp:Label ID="labbillDate" runat="server" Text='<%#  Eval("billDate") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Vendor">
                                     <ItemTemplate>
                                         <asp:Label ID="labvendor_source" runat="server" Text='<%# Eval("vendor_source") %>'></asp:Label>

                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Location">
                                     <ItemTemplate>
                                         <asp:Label ID="labLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>

                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Book No">
                                     <ItemTemplate>
                                         <asp:Label ID="labBookNumber" runat="server" Text='<%# Eval("BookNumber") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Price">
                                     <ItemTemplate>
<asp:Label ID="labbookprice" runat="server" Text='<%# Eval("bookprice") %>'></asp:Label>                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                         </asp:GridView>

                        </div>
                    </div>
                </div>
                
            </div>
            <button type="button" onclick="show2()">show 2</button>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnReset_Click" />
            <asp:UpdatePanel ID="upSearch" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <script type="text/javascript">
                        //On UpdatePanel Refresh.
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        if (prm != null) {
                            prm.add_endRequest(function (sender, e) {
                                if (sender._postBackSettings.panelsToUpdate != null) {
                                    $('#collapseExample').addClass('show');
                                }
                            });
                        };

                    </script>
                    <p>
                        <button class="btn btn-sm btn-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#collapseExample" aria-expanded="false" aria-controls="collapseExample">
                            Search
                        </button>
                        <asp:HiddenField ID="hdcollapseExample" runat="server" />
                    </p>
                    <div class="container collapse" id="collapseExample">
                        <div class="card card-body">
                            <CatE:Catalog ID="CatgEdit" runat="server" OnClientmyCustomEvent="alert(150);" />

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
