<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndentFrom.aspx.cs" MasterPageFile="~/LibraryMain.master" Inherits="Library.IndentFrom" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="IFHead" runat="server" ContentPlaceHolderID="head">

     <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>

    
    <script>
        $(function () {
            let hdr = $("[id*=tblHeader]");
            if (isNaN(hdr)){
//                GoHome();
            }
            if (hdr.attr('id') == undefined) {

            }
        })
    </script>

</asp:Content>

<asp:Content ID="IFMain" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
            <div class="container tableborderst">   
       
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > 
                       <asp:label id="lblTitle" runat="server" style="display:none"  DESIGNTIMEDRAGDROP="286" >Print Indent</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-Indents-Indent-Form.htm')"><img alt="Help?" height="15" src="help.jpg" width="20" /></a></td>
              </div></div>
                       
                       
				
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers><asp:PostBackTrigger ControlID="cmdprint" /></Triggers>
                            <ContentTemplate>
                                <div class="no-more-tables">
                                <table id="Table2" class="table-condensed  GenTable1" >
                                    <tr>
                                        <td colspan="4" style="text-align:center">
                                            <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                    <tr>
                                     
                                        <td colspan="2">
                                            <asp:RadioButton ID="optNormal" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,RBNormalI %>"  Checked="True"
                                                GroupName="r" AutoPostBack="True"></asp:RadioButton> 
                                        
                                            <asp:RadioButton ID="optGift" runat="server" CssClass="opt"  Text="<%$ Resources:ValidationResources,RBGiftI %>" GroupName="r"
                                                AutoPostBack="True"></asp:RadioButton></td>
                                   
                                        <td colspan="2">
                                            <asp:RadioButton ID="rdCurrent" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,RBCurrI %>" Checked="True" GroupName="order"
                                                AutoPostBack="True"></asp:RadioButton> 

                                            <asp:RadioButton ID="rdall" runat="server" CssClass="opt" GroupName="order" Text="<%$ Resources:ValidationResources,RBAllI %>" AutoPostBack="True"></asp:RadioButton> 
                                        
                                            <asp:RadioButton ID="rdDate" runat="server" CssClass="opt" GroupName="order" Text="<%$ Resources:ValidationResources,RBDateR %>" AutoPostBack="True"></asp:RadioButton></td>
                                    </tr>
                                    
                                    <tr>
                                       <td style="width:15%">
                                            <asp:Label ID="Label7" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,LFromD %>"></asp:Label></td>
                                        <td>

                                            <%--pushpendra singh--%>
                                            <asp:TextBox ID="txtfromdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                           

                                        </td>
                                        <td >
                                            <asp:Label ID="Label6" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,LToD %>"></asp:Label></td>
                                        <td>
                                            <%--pushpendra singh--%>
                                            <asp:TextBox ID="txttodate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                       



                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:ValidationResources,LDeptm %>">Department</asp:Label></td>
                                        <td colspan="3">
                                    <asp:ListBox ID="cmbdept" Width="100%" runat="server" AutoPostBack="true" SelectionMode="Single" ></asp:ListBox>
                                            <input type="hidden" runat="server" id="dnewcombo" style="height:0px;width:0px" />
                                         <asp:Label ID="Label36" runat="server" CssClass="star" Width="1px">*</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align:top">
                                            <asp:Label ID="Label10" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LINumber %>">Indent Number</asp:Label></td>
                                        <td  colspan="3">
                                            <asp:TextBox onkeypress="disallowSingleQuote(this);" ID="txtindentnumber" onkeydown="txtindentnumber_OnKeydown()"
                                                onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" CssClass="txt10" BorderWidth="1px"></asp:TextBox><%--<input id="cmdGet" type="button" value="<%$Resources:ValidationResources,BEnter %>" name="cmdGet" runat="server" class="btnstyle">--%>
                                            <asp:Button Id="cmdGet" runat="server" Text="Search Indent Record" CssClass="btn btn-primary"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="<%$ Resources:ValidationResources,CnkSelectA %>"
                                                AutoPostBack="True"></asp:CheckBox></td>
                                      <td colspan="2">
                                            <%--<input id="cmdprint" type="button" value="<%$Resources:ValidationResources,iprint %>" name="cmdSearch"
                                                runat="server" class="btnstyle">--%>
                                          <asp:Button Id="cmdprint" runat="server" Text="Print" CssClass="btn btn-primary"/>
                                            <%--<input id="cmdreset"
                                                type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset" runat="server" causesvalidation="false" class="btnstyle">--%>
                                          <asp:Button Id="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary"/>
                                      </td>
                                    </tr>


                                </table>
                                    </div>
                                  <div style="width:100%;overflow:auto;margin-top:10px " id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                                <asp:datagrid id="grdOrder" runat="server" Width="100%" CssClass="GenTable1"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
											
											<Columns>
											<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrSel %>">
													<HeaderStyle Width="2%"></HeaderStyle>
													
													<ItemTemplate>
														
														<asp:CheckBox id="Chkselect" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="indentnumber" HeaderText="<%$ Resources:ValidationResources, LINumber %>">
													<HeaderStyle Width="5%"></HeaderStyle>
													
												</asp:BoundColumn>
												<asp:BoundColumn DataField="indentdate" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>" HeaderText="<%$ Resources:ValidationResources, IDt %>">
													<HeaderStyle Width="3%"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											
										</asp:datagrid>
                                
                                   </div>  
					 <INPUT id="Hidden3" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
								runat="server"><INPUT id="hdForMesage" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" name="hdForMesage"
								runat="server"><INPUT id="Hidden7" type="hidden" runat="server" style="width: 22px"><input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 27px" /><input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 31px" /><input id="hdCulture" runat="server" style="width: 19px" type="hidden" /><input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 28px" /><input id="Hidden2" runat="server" style="width: 20px" type="hidden" />
			<INPUT id="Hidden1" style="WIDTH: 24px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server">
				

                                <asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
				
				
                            </ContentTemplate>
                        </asp:UpdatePanel>   </div>

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
                ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
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

    </script>
     <script>
         $(function () {
             //ForDataTable();
             SetListBox();
         });
         var prm = Sys.WebForms.PageRequestManager.getInstance();

         if (prm != null) {
             prm.add_endRequest(function (sender, e) {

                 if (sender._postBackSettings.panelsToUpdate != null) {

                     //ForDataTable();
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
             $('[id*=cmbdept]').multiselect({
                 enableCaseInsensitiveFiltering: true,
                 buttonWidth: '80%',
                 includeSelectAllOption: true,
                 maxHeight: 200,
                 width: 315,
                 enableFiltering: true,
                 filterPlaceholder: 'Search'
             });
         }
     </script>
</asp:Content>
