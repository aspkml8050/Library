<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" MasterPageFile="~/LibraryMain.master"  Inherits="Library.Subscription" %>

<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css" >
            .Publ
            {
                width:300px;
                max-height:250px;
                overflow:auto;
                margin:0;
                padding:0;
                font-size:13px;
                 color:black;
                border:2px solid black;
            }
        </style>
</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container tableborderst">   
        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
       <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>--%>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
           <asp:Label ID="Label1" runat="server"  Text="Subscription" 
            Width="100%"></asp:Label>
            </div></div>
 <table class="table-condensed no-more-tables GenTable1">
           
            <tr>
                <td style="vertical-align:top">
                    <asp:Label ID="lblsubs" runat="server" Text="Subscriber Code"></asp:Label>
                </td>
                <td colspan="3">
                   <%-- <custom:AutoSuggestBox ID="txtCmbMemid" runat="server" autopostback="True" 
                        borderwidth="1px" columns="30" CssClass="FormCtrl" datatype="City" 
                        maxlength="20" onkeydown="txtuserid_onkeydown()" 
                        onkeypress="disallowSingleQuote(this)" OnTextChanged="txtCmbMemid_TextChanged" 
                        resourcesdir="asb_includes" >
                        

                    </custom:AutoSuggestBox>--%>
                    <asp:TextBox ID="txtCmbMemid" runat="server" Ontextchanged="txtCmbMemid_TextChanged" />
                      <ajax:AutoCompleteExtender ID="ExtVend" runat="server" TargetControlID="txtCmbMemid"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="MssplSugg.asmx"
                                        ServiceMethod="GetMemberID"
                                        OnClientItemSelected="GetVendJs">
                                    </ajax:AutoCompleteExtender>
                                    <asp:HiddenField ID="hdUid" runat="server" /><br />
                    <asp:Label ID="lbls" runat ="server" Text="(Please Use Enter key to Select Member ID as Subscriber Code)" 
                        ForeColor="#B37212"></asp:Label>
                </td>
            </tr>
             <tr>
                 <td >
                     <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                 </td>
                 <td>



                     <%--pushpendra singh--%>
 <asp:TextBox ID="txtDate" runat="server"  />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>


<%--                     <asp:TextBox ID="txtDate" runat="server" Width="90px"></asp:TextBox>
                     <input ID="btnDate" runat="server" accesskey="D" onclick="pickDate('txtDate');" 
                         style="background-position: center center; background-image: url(cal.gif); width: 25px; background-color: black; height: 21px;" 
                         type="button" />--%>





                 </td>
              
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="Label4" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                </td>
             
                <td >
                    <asp:Label ID="Label5" runat="server" Text="Rate"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:19%">
                    <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmt" runat="server"></asp:TextBox>
                </td>
             
                <td >
                    <asp:Label ID="Label7" runat="server" Text="Period"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPeriod" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                 <td >
                     <asp:Label ID="Label8" runat="server" Text="Todate"></asp:Label>
                 </td>
                 <td>
                     <%--pushpendra singh--%>
 <asp:TextBox ID="txtDate1" runat="server"  />
 <%--<ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDate1" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>


<%--                     <asp:TextBox ID="txtDate1" runat="server" Width="90px"></asp:TextBox>
                     <input ID="btnDate1" runat="server" accesskey="D" 
                         onclick="pickDate('txtDate1');" 
                         style="background-position: center center; background-image: url(cal.gif); width: 25px; background-color: black; height: 21px;" 
                         type="button" />--%>





                 </td>
                 <td></td>
                 <td></td>
             </tr>
            <tr>
                
                <td colspan="4" style="text-align:center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click"/>
                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnReset_Click"/>
                </td>
            </tr>
             <tr style="display:none">
                 <td colspan="4">
                     <asp:Label ID="Label9" runat="server" Text="Show Existing:"></asp:Label>
                 </td>
                 <td>
                    </td>
             </tr>
            
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>

          <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
              <%--OnItemCommand="DataGrid1_ItemCommand1"--%>
    <asp:datagrid id="DataGrid1" runat="server" CssClass="allgrid GenTable1"  Font-Names ='<%$resources: ValidationResources, TextBox1 %>' Width="100%"  AutoGenerateColumns="False">
											<Columns>
												<asp:ButtonColumn Text=" Item" DataTextField="Item" 
													HeaderText="Item(s)" CommandName="Select">
                                                 </asp:ButtonColumn>
												<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id"></asp:BoundColumn>
												<asp:BoundColumn DataField="Quantity" ReadOnly="True" HeaderText="Quantity"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Rate" HeaderText="Rate" >
                                                </asp:BoundColumn>
                                                	<asp:ButtonColumn Text="Drop" FooterStyle-ForeColor ="Coral"  	HeaderText="Drop" CommandName="Del">
                                                 </asp:ButtonColumn>
											</Columns>
											
										</asp:datagrid>
            </div></div>
     <script type="text/javascript">
         //On Page Load.
         $(function () {
             SetDatePicker();
             ForDataTable();
         });
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
                 $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                 $('#' + grdId + ' tr:first td').contents().unwrap().wrap('<th></th>');
                 ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
             }
             catch (err) {
             }
         }
         function SetDatePicker() {
             $("[id$=txtDate],[id$=txtDate1]").datepicker({
                 changeMonth: true,//this option for allowing user to select month
                 changeYear: true, //this option for allowing user to select from year range
                 dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
             });

         }
         function GetVendJs(sender, arg) {
             let id = arg.get_value();
             $('[id$=hdUid]').val(id);
             //$('[id$=cmdcheck]').click();
         }
     </script>
</asp:Content>


