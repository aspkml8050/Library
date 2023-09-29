<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/LibraryMain.master" CodeBehind="orderform.aspx.cs" Inherits="Library.orderform" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="POrdHead" runat="server" ContentPlaceHolderID="head">
    <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
    </asp:Content>
<asp:Content ID="POrdMain" runat="server" ContentPlaceHolderID="MainContent">
    <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>
    <style>
        .btn-group,
        .btn-group-vertical {
            width:95% !important;
        }
        </style>
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="container tableborderst">   
         <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
                      <asp:label id="lblTitle" runat="server" style="display:none;" Width="100%" >Print Order</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none;"  onclick="ShowHelp('Help/acq-Orders-PrintForm.htm')"><img src="help.jpg" height="15" alt="Help" /></a>
              </div>
                  </div>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <Triggers>
                     <asp:PostBackTrigger ControlID="cmdSearch" />
                 </Triggers>
                 <ContentTemplate>
                     <table id="Table2" class="table-condensed GenTable1" style="width: 100%">
                         <tr>
                             <td colspan="4">
                                 <asp:Label ID="msglabel" runat="server" CssClass="err" Visible="False"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="4" style="text-align: center">
                                 <asp:RadioButton ID="rdCurrent" runat="server" CssClass="opt" GroupName="order" Text="<%$Resources:ValidationResources,RBCurrO %>"
                                                    AutoPostBack="True"></asp:RadioButton>

                                                <asp:RadioButton ID="rdall" Checked="True" runat="server" CssClass="opt" GroupName="order" Text="<%$Resources:ValidationResources,RBAllO %>" AutoPostBack="True"></asp:RadioButton>


                                                <asp:RadioButton ID="rdDate" runat="server" CssClass="opt" GroupName="order" Text="<%$Resources:ValidationResources,RBDateR %>" AutoPostBack="True"></asp:RadioButton>
                             </td>
                         </tr>
                         <tr style="font-weight: bold; color: #000000">
                             <td>
                                 <asp:Label ID="Label7" runat="server" CssClass="head1" Text="<%$Resources:ValidationResources,LFromD %>"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtfromdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="90%" />
                             </td>
                             <td style="color: #000000">
                                 <asp:Label ID="Label6" runat="server" CssClass="head1" Text="<%$Resources:ValidationResources,LToD %>"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txttodate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="80%" />
                             </td>
                         </tr>
                         <tr>
                             <td style="width:10%">
                                 <asp:Label ID="Label2" runat="server" Text="<%$Resources:ValidationResources,LVen %>"></asp:Label>
                             </td>
                             <td>
                                <asp:DropDownList ID="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                    AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10 ">
                                                </asp:DropDownList>

                                                <asp:Label ID="Label36" runat="server" CssClass="star">*</asp:Label>
                             </td>
                             <td style="width:8%">
                                 <asp:Label ID="Label3" runat="server" Text="<%$Resources:ValidationResources,LDeptm %>"></asp:Label>
                             </td>
                             <td>
                                 <asp:ListBox ID="cmbdept" runat="server" AutoPostBack="true" SelectionMode="Single"></asp:ListBox>
                                                <asp:Label ID="Label1" runat="server" CssClass="star">*</asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="2">
                                 <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="opt"
                                                    AutoPostBack="True" Text="<%$Resources:ValidationResources,CnkSelectA %>"></asp:CheckBox>
                             </td>
                             <td colspan="2" >
                                 <input id="cmdSearch" type="submit" value="<%$Resources:ValidationResources,iprint %>"
                                                    name="cmdSearch" runat="server" causesvalidation="true" class="btnstyle">

                                                <input id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>"
                                                    name="cmdSearch" runat="server" causesvalidation="false" class="btnstyle">
                             </td>
                         </tr>
                     </table>
                      <div class="allgriddiv" id="dvgrd" runat="server"> 
                           <asp:HiddenField runat="server" ID="hdnGrdId" />
                                    <asp:datagrid id="grdOrder" CssClass="allgrid GenTable1" runat="server"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AccessKey="h" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle  Width="2%" />
                                                <ItemTemplate>
                                                    <asp:CheckBox id="Chkselect" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ordernumber" HeaderText="<%$ Resources:ValidationResources, GrOdrN %>">
												<HeaderStyle ></HeaderStyle>
												
											</asp:BoundColumn>
                                            <asp:BoundColumn DataField="orderdate" HeaderText="<%$ Resources:ValidationResources, LOrderD %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>">
												
											</asp:BoundColumn>

                                        </Columns>
                                        </asp:datagrid>
                         
                      </div>
                     <div>
                         <input id="yCoordHolder" style="width: 29px; height: 16px" type="hidden" size="1" value="0"
                                                    name="yCoordHolder" runat="server"><input id="Hidden7" type="hidden" runat="server" style="width: 81px"><input id="Hidden2" runat="server" style="width: 34px" type="hidden" /><input id="Hidden1" style="width: 34px; height: 22px" type="hidden" name="Hidden1"
                                                        runat="server">&nbsp;
                            <input id="hdCulture" runat="server" style="width: 72px" type="hidden" /><input id="Hidden5" runat="server" style="width: 47px" type="hidden" /><input id="HComboSelect" runat="server"
                                type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 27px" /><input id="xCoordHolder" style="width: 8px; height: 12px" type="hidden" size="1" value="0"
                                    name="xCoordHolder" runat="server"><input id="hrDate" runat="server"
                                        type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 27px" /><input id="js1" runat="server"
                                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 27px" />
                     </div>

                 </ContentTemplate>
             </asp:UpdatePanel>
             </div>
        </div>
    
    <script type="text/javascript">
    //On Page Load.
    $(function () {
        SetDatePicker();
        ForDataTable();
    });

    //On UpdatePanel Refresh.
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                SetDatePicker();
                ForDataTable();
            }
        });
    };
     function ForDataTable() {
            try {
                var grdId = $("[id$=hdnGrdId]").val();
                //alert(grdId);
                $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                ThreeLevelSearch($('#'+ grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }
    function SetDatePicker() {

        $("[id$=txtfromdate],[id$=txttodate]").datepicker({
            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }

</script>\
    <script>
        $(function () {
            //ForDataTable();
            SetListBox();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                if (sender._postBackSettings.panelsToUpdate != null) {

                    //  ForDataTable();
                    SetListBox();
                }
            });
        };
        function ForDataTable() {
            try {
                var grdId = $("[id$=hdnGrdId]").val();
                //alert(grdId);
                $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]", 200);
            }
            catch (err) {
            }
        }
        function SetListBox() {

            $('[id*=cmbdept],[id*=cmbvendor]').multiselect({
                enableCaseInsensitiveFiltering: true,
                buttonWidth: '90%',
                includeSelectAllOption: true,
                maxHeight: 200,
                width: 315,
                enableFiltering: true,
                filterPlaceholder: 'Search'

            });

        }
    </script>


</asp:Content>

