<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCatalogShow.ascx.cs" Inherits="Library.UCCatalogShow" %>
<style>
    .confirmPanel {
    background-color: #0C4B6A ;padding:10px ;box-shadow:0px 0px 8px 0px #a1a1a1 ;border:solid 1px #cccccc
}
    .dataTable {
        background-color:#f3f1ed;
    }
    .dataTable tbody tr {
        padding:0;
        margin:0;
    }
    .dataTable tbody tr td {
        padding:0;
        margin:0;
        font-size:13px;
    }
    .dataTable tbody tr td a {
        font-size:13px;
    }
    .dataTables_filter, .dataTables_info {
    display: inline;
}

    .shistDv{
        width:75%;
        position:absolute;
        padding:6px;
        border:2px solid grey;
        left:100px;
        top:15%;
        background-color:rgba(255,255,255,0.8);
        max-height:500px;
        overflow:auto;
    }
.loader {
  border: 6px solid #f3f3f3; /* Light grey */
  border-top: 6px solid #3498db; /* Blue */
  border-radius: 50%;
  width: 20px;
  height: 20px;
  animation: spin 2s linear infinite;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

</style>
<script type="text/javascript">
    function showData() {
        alert('sh');

    }

        function UCatgEvent(eventData) {
            // Handle the event here
    }
</script>

<div style=" position:absolute;left:50%; ">
    <i class="fa fa-spinner fa-spin" style="font-size:20px;  color:darkblue ; display:none;" ></i>
</div>

<div style="width:inherit; width:100%; z-index:0; margin-top:8px; font-size:11px;">
    <div style="line-height:30px; font-size:13px; margin:0!important; padding:6px; border-radius:5px; vertical-align:middle;background-color:#bec5f1;">
        <asp:Label ID="Label13" runat="server" Text="Search Catalogs"></asp:Label>
        &nbsp;&nbsp;<asp:Label ID="labUCMesg" runat="server" Font-Bold="true"></asp:Label>
    </div>
    Find Accn No(Suggestions) : <asp:TextBox ID="txtAccnNoSuggUC" runat="server"  Width="100" ></asp:TextBox>
    Find Accn No(for large Db) : <asp:TextBox ID="txtAccnNoUC" runat="server"  Width="100" ></asp:TextBox>
      <table class="table-condensed no-more-tables" style="border: 1px solid grey;">
      <tr>
          <td>
              <asp:Label ID="Label1" runat="server" Text="Accession No Fr"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txtAccFUC" runat="server" CssClass="inpUc" Width="100"></asp:TextBox>
              <asp:CheckBox ID="chkAccnUC" Font-Size="10" runat="server" Checked="true" Text="This Only?" ToolTip="Search Records having this number (wild card search), e.g. '2025' search will include '102025','20250' etc. Other conditions Not considered. " />

          </td>
          <td>
              <asp:Label ID="Label2" runat="server" Text=" Accession No To"></asp:Label>

          </td>
          <td>
              <asp:TextBox ID="txtAccTUC" runat="server" CssClass="inpUc" Width="100"></asp:TextBox>

          </td>

      </tr>
      <tr>
          <td>
              <asp:Label ID="Label3" runat="server" Text="Title"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txTitleUC" runat="server" CssClass="inpUc" Width="200"></asp:TextBox>
          </td>
          <td>
              <asp:Label ID="Label4" runat="server" Text="Author"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txAuthUC" runat="server" CssClass="inpUc" Width="200"></asp:TextBox>
          </td>

      </tr>
      <tr>
          <td>
              <asp:Label ID="Label5" runat="server" Text="Category"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txCategUC" runat="server" CssClass="inpUc" Width="200"></asp:TextBox>
          </td>
          <td>
              <asp:Label ID="Label6" runat="server" Text="Department"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txDeptUC" runat="server" CssClass="inpUc" Width="200"></asp:TextBox>
          </td>
      </tr>
      <tr>
          <td>
              <asp:Label ID="Label7" runat="server" Text="Catalog Date Fr"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txCatDFUC" runat="server" CssClass="inpUc" Width="100"></asp:TextBox>
          </td>
          <td>
              <asp:Label ID="Label8" runat="server" Text="Catalog Date To"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txCatDUUC" runat="server" CssClass="inpUc" Width="100"></asp:TextBox>
          </td>
      </tr>
      <tr>
          <td>
              <asp:Label ID="Label9" runat="server" Text="Subject"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txSubjUC" runat="server" CssClass="inpUc" Width="200"></asp:TextBox>
          </td>
          <td>
              <asp:Label ID="Label10" runat="server" Text="Publisher"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txPublUC" runat="server" CssClass="inpUc" Width="200"></asp:TextBox>
          </td>
      </tr>
      <tr>
          <td>
              <asp:Label ID="Label11" runat="server" Text="Set No(of 500)"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txSetNoUC" CssClass="inpUc" runat="server" ToolTip="Each set is of 500 records,enter 'Set Number', if given only this condition will be applied in search." Width="100"></asp:TextBox>?
          </td>
          <td>
              <span style="color: black; font-size: 16px">","</span><asp:Label ID="Label12" runat="server" Text="Separated Keywords"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txKeysUC" CssClass="inpUc" runat="server" Width="200"></asp:TextBox>
          </td>

      </tr>
      <tr>
          <td colspan="1">
              <span style="color: black; font-size: 16px">","</span><asp:Label ID="Label14" runat="server" Text="Search Text"></asp:Label>

          </td>
          <td>

              <asp:TextBox ID="txSearchTextUC" CssClass="inpUc" runat="server" Width="200"></asp:TextBox>
              <asp:CheckBox ID="chkExactUC" runat="server" Text="Exact" />
          </td>
          <td>
              <asp:CheckBox ID="chkEmptCallNo" runat="server" Text="Empty Call No" />
          </td>
          <td colspan="1">
              <asp:CheckBox ID="chkEmptSubj" runat="server" Text="Empty Subject" />
          </td>
      </tr>
      <tr>
          <td colspan="4" style="text-align: center">
              <asp:Button ID="btnUCPost" runat="server" CssClass="btn btn-primary btn-sm btn-dark" Text="Show" OnClientClick="return PostBac();" />
              <asp:Button ID="btnUCSh" runat="server" Style="display: none" OnClick="btnUCSh_Click" />
              <asp:Button ID="btnUCRes" runat="server" CssClass="btn btn-primary btn-sm btn-dark" Text="Reset" OnClick="btnUCRes_Click" />
              <asp:Button ID="btnUCShHidd" runat="server" CssClass="btn btn-primary btn-sm btn-dark" Text="Show Hidden Cols" OnClientClick="return ShowHidCols();" />
              <asp:Button ID="btnUCExp2Exc" runat="server" CssClass="btn btn-primary btn-sm btn-dark" Text="Export 2 Excel" OnClientClick="return Exp2Exc()" />
              <asp:Button ID="btnUCSavMarc" runat="server" CssClass="btn btn-primary btn-sm btn-dark" Text="Save Selected in Marc21" OnClick="btnUCSavMarc_Click" />
              <asp:CheckBox ID="chkUCShoSH" runat="server" Text="Search History*" ToolTip="Reset will refresh search history" onclick="return shoSerH(this)" />
          </td>

      </tr>
  </table>

</div>
<script type="text/javascript">
    function PostBac() {
        $(".loader").css('display', 'block');
        $("[id$='btnUCSh']").click();
        return false;
    }

    function Exp2Exc() {
        $('[id$="grdDataUC"]').DataTable().destroy();
        //        $('[id$="grdData"] thead th').removeClass('UcHideCol');
        //       $('[id$="grdData"] tbody td').removeClass('UcHideCol');

        $("[id$='grdDataUC']").table2excel();
        $('[id$="grdDataUC"]').dataTable().api();
        return false;
    }

    $(function () {
        Mesg();
    })

    $(document).ready(function () {
        try {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(Mesg);

        } catch { }
    })
    function Mesg() {
        $('[id$=txtAccnNoSuggUC]').on('keyup', function () {
            AccnJSuggUC($(this));
        });
        $('[id$="txCatDFUC"],[id$="txCatDUUC"] ').datepicker(
            {
                dateFormat: 'dd-M-yy',
                changeMonth: true,
                changeYear: true,
            });
        $('.inpUc').on('keypress', function (event) {
            let ke = event.keyCode;
            if (ke == 13) {
                event.preventDefault();
                PostBac(event);
            }
        });
        try {
            let c = $("[id$='grdDataUC'] > thead > tr").length;
            if (c == 0) {
                $('[id$="grdDataUC"] tbody').before('<thead><tr></tr></thead>');
                $("[id$='grdDataUC'] thead tr").append($("[id$='grdDataUC'] th"));
                $("[id$='grdDataUC'] tbody tr:first").remove();
            }
            let cb = $("[id$='grdDataUC'] tbody tr").length;
            if (cb > 0) {

                $("[id$='grdDataUC']").DataTable();
            }

        } catch (er) {
            alert(er + '; Make sure grid has data');
        }
        $('#dvSearchH').draggable();
        let hsSerH = $('[id$=hdShoUC]').val();//hdShoUC
        if (hsSerH == 'y') {
            $('[id$=hdShoUC]').val('');
            $('#dvSearchH').css('display', 'block');
        }
    }
    function AccnJSuggUC(ele) {
        let nombre = $(ele).val();
        $('[id$=txtAccnNoSuggUC]').autocomplete({
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
                let vdata2 = vdata.split('|');
               
                UCCtrlSentAccn(vdata2[1]);
            },
            minLength: 1
        }).focus(function () {
            $(this).autocomplete("search");
        });
    }
    function shoSerH(chk) {
        let chkv = $(chk).is(':checked');
        if (chkv == true)
            $('#dvSearchH').css('display', 'block');
        else
            $('#dvSearchH').css('display', 'none');

    }
    function UCclosit(sp) {
        $('#dvSearchH').css('display', 'none');
        $('[id$=chkUCShoSH]').prop('checked', false);
    }

    function UCAccnSentLocal(ele, e) {
        e.preventDefault();
        let hdctrl = $(ele).prev().val();
        UCCtrlSent(hdctrl);
        return false;
    }
    function UCCtrlSentAccn(ele) {
        UCCtrlSent(ele);
        return false;
    }
    function ShowHidCols() {
        $('[id$="grdDataUC"]').DataTable().destroy();
        $('[id$="grdDataUC"] thead th').removeClass('UcHideCol');
        $('[id$="grdDataUC"] tbody td').removeClass('UcHideCol');
        $('[id$="grdDataUC"]').dataTable().api();
        return false;
    }

    function sel10(txt) {
        let digt = $(txt).val();
        if (!isFinite(digt)) {
            $(txt).val('');
            return;
        }
        let d2 = digt * 1;

    }
</script>

        <style>
            .UcGrid {
                /*dummy for getting grid*/
            }
            /*set font small for jquery.datatable - if parent grid is disturbed then change this style*/
               table.UcGrid th{
                    font-size:11px;
                }
               table.UcGrid td{
                    font-size:11px;
                    padding:6px 2px !important;
                }
               table.UcGrid td span{
                    font-size:11px;
                }

               .UcHideCol{
                   display:none;
               }

               .inpUc {
                    border-radius:4px;
                    border:1px solid grey;
               }
        </style>
<div class="allgriddiv" >
    <asp:GridView ID="grdDataUC" runat="server"  EnableTheming="false"  AllowPaging="false"   AutoGenerateColumns="false">
        <Columns>
               <asp:TemplateField>
                <HeaderTemplate>
                    Select
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSav" Text="" Width="50" AutoPostBack="false"  runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    Accn No
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hdCtrlNo" runat="server" Value='<%# Eval("ctrl_no") %>' />
                    <asp:LinkButton ID="lnkAccn" Width="60" runat="server" OnClientClick="return UCAccnSentLocal(this,event)" Text='<%# Eval("accessionnumber") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Title
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labT" runat="server" Width="250" Text='<%# Eval("booktitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    CpNo
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labCp"  runat="server" Width="25" Text='<%# Eval("copynumber") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Author
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labAuth" Width="130" runat="server" Text='<%# Eval("author") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Catalog Date
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labCatdt" runat="server" Width="90" Text='<%# Eval("catdate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField >
                <HeaderTemplate>
                    Publisher
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labPubl" Width="250" runat="server" Text='<%# Eval("publisher") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  >
                <HeaderTemplate>
                    Subject
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labSubj" runat="server" Text='<%# Eval("subject") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          
        </Columns>
       
    </asp:GridView>

   <%-- <EmptyDataTemplate>
            No Records
        </EmptyDataTemplate> 
        <asp:TemplateField>
                <HeaderTemplate>
                    Edition
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labEdition" runat="server" Text='<%# Eval("edition") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Call No
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labCallno" runat="server" Text='<%# Eval("callno") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
       <asp:TemplateField  HeaderStyle-CssClass="UcHideCol" ItemStyle-CssClass="UcHideCol"  >
                <HeaderTemplate>
                    Department
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labDept" runat="server" Text='<%# Eval("departmentname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderStyle-CssClass="UcHideCol" ItemStyle-CssClass="UcHideCol" >
                <HeaderTemplate>
                    Category
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labCateg" runat="server" Text='<%# Eval("Category_LoadingStatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderStyle-CssClass="UcHideCol" ItemStyle-CssClass="UcHideCol" >
                <HeaderTemplate>
                    Status
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labStat" runat="server" Text='<%# Eval("ItemStatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>

</div>
<div id="dvSearchH" class="shistDv" style="display:none;" >
    <asp:HiddenField ID="hdShoUC" runat="server" />
    <b>Search History(drag)</b>
    <asp:Button ID="btnDelChkUC" runat="server" CssClass="btn btn-primary btn-sm" Text="Delete Checked" OnClick="btnDelChkUC_Click"/>
    <asp:TextBox ID="txSlFrUC" runat="server" Width="60" placeholder="SnFr"></asp:TextBox>
    <asp:TextBox ID="txSlToUC" runat="server" Width="60" placeholder="SnTo"></asp:TextBox>
    <asp:Button ID="btnDelSelUC" runat="server" CssClass="btn btn-primary btn-sm" Text="Delete Sno-range" OnClick="btnDelSelUC_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold" onclick="UCclosit()"  >X</span>
    <asp:GridView ID="grdSHUC" runat="server" Font-Size="11" AutoGenerateColumns="false" EnableTheming="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>

                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="btnSer" runat="server" Font-Size="10" Text="Search" OnClick="btnSer_Click" />
                    <asp:HiddenField ID="hdhistid" runat="server" Value='<%# Eval("searchid") %>' />
                    <asp:HiddenField ID="hdSerchCond" runat="server" Value='<%# Eval("SearchCondition") %>' />
                    <asp:HiddenField ID="hdInternCond" runat="server" Value='<%# Eval("interncond") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Sn
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labSno" Font-Bold="false" Font-Size="10" runat="server" Text='<%# Eval("sno") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Search Text
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labStext" runat="server" Font-Bold="false" Font-Size="10" Width="550" Text='<%# Eval("searchtext") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Date
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labdat"  Font-Bold="false" Font-Size="10" runat="server" Text='<%# string.Format("{0:dd-MMM-yyyy hh:mm}",Convert.ToDateTime(Eval("sdate")))   %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnDel" style="display:none" runat="server"  Text="Del" OnClick="btnDel_Click" />
                    <asp:CheckBox ID="chkDelete" runat="server" data-deletesh="1" Font-Bold="false" Font-Size="9" ToolTip="Delete" ForeColor="Red" Text="D" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
