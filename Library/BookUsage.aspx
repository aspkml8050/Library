<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="BookUsage.aspx.cs" Inherits="Library.BookUsage" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content id="Head" runat="server" ContentPlaceHolderID="head">


</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

                         <INPUT id="xCoordHolder" style="WIDTH: 8px; HEIGHT: 12px" type="hidden" size="1" value="0"
							name="xCoordHolder" runat="server">
		<INPUT id="yCoordHolder" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1" value="0"
							name="yCoordHolder" runat="server">
                                <INPUT id="Hidden1" style="WIDTH: 16px; HEIGHT: 22px"
				type="hidden" name="Hidden1" runat="server">
                                    <input id="hdCulture" runat="server" style="width: 16px" type="hidden" />
                                    <input id="hrDate"
                                        runat="server" 
                                        type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" />
			
				<div class="container tableborderst"  >                    
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left">&nbsp;
                        <asp:label id="lblTitle"    style="display:none"  runat="server">
                                  Books Usage During Period</asp:label></div>
                   <div style="float:right;vertical-align:top">
					<a id="lnkHelp" href="#"     style="display:none" onclick="ShowHelp('Help/Audit Trai1OrderAudit.mht')"> <img alt="Help?" height="15" src="help.jpg"  /></a>
					</div></div>
               
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID="cmdPrint" />
						
						</Triggers>
                            <ContentTemplate>
        <table  class="no-more-tables table-condensed GenTable1">
            <tr>

                <td  style="text-align:center" colspan="4" >
                    <asp:RadioButtonList ID="rbldetsum" runat="server" style="margin:0px;padding:0px;color:black" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" >Detail</asp:ListItem>
                        <asp:ListItem>Summary</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                  <asp:Label ID="Label1" runat="server" Text='<%$Resources : ValidationResources,LFrom%>'></asp:Label>
         </td>
                <td>
<%--pushpendra singh--%>
 <asp:TextBox ID="txtFrom" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFrom" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>
                    
              <%--      <asp:TextBox ID="txtFrom" runat="server"  Width="117px"></asp:TextBox>
                    <input id="btndate1" accesskey="D" onclick="pickDate('txtFrom');" 
                        style="background-position: center center;
                                            background-image: url(cal.gif); width: 27px; height:20px ; background-color: black" 
                        type="button" runat="server" />
--%>



                </td>
                <td>
                
                    <asp:Label ID="Label2" runat="server" Text='<%$Resources : ValidationResources,LTo%>'></asp:Label>
                    
                    </td>
                <td>
                    
<%--pushpendra singh--%>
 <asp:TextBox ID="txtTo" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTo" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>



                  <%--  
                    <asp:TextBox ID="txtTo" runat="server"  Width="117px"></asp:TextBox>
                    <input id="btndate2" accesskey="D" onclick="pickDate('txtTo');" 
                        style="background-position: center center;
                                            background-image: url(cal.gif); width: 27px; height:20px; background-color: black" 
                        type="button" runat="server" />

--%>

                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center">
                  <%--<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID ="UpdatePanel1" runat="server">
                                                <progresstemplate>
                                                    <div id="IMGDIV" runat="server" align="center" 
                                                        style="position: absolute;left: 35%;top: 25%;visibility:visible;vertical-align:middle;" 
                                                        valign="middle">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/ajax-progress.gif" />
                                                    </div>
                                                </progresstemplate>
                                            </asp:UpdateProgress>--%>
                    <asp:Button ID="cmdPrint"  runat="server" CssClass="btn btn-primary" Text="<%$Resources : ValidationResources,iprint%>" OnClick="cmdPrint_Click" />
                    <asp:Button ID="BtnReset" runat="server" CssClass="btn btn-primary" Text="<%$Resources : ValidationResources,bReset%>" OnClick="BtnReset_Click" />
<%--                    <asp:Button ID="cmdPrint" runat="server" CssClass="btnstyle" Text='<%$Resources : ValidationResources,iprint%>'  OnClick="cmdPrint_Click" 
                       /> <asp:Button ID="BtnReset" runat="server" Text='<%$Resources : ValidationResources,bReset%>' 
                         OnClick="BtnReset_Click" CssClass="btnstyle"/> --%>
                </td>
                 
            </tr>
        </table>
      
                           
					 
                   
				
                 <%--   <CR:CrystalReportViewer ID="CrystRptSchlr_MultiOpt" runat="server" 
                        AutoDataBind="true" />--%>
					
			  </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
                }
            });
        };
        function SetDatePicker() {

            $("[id$=txtTo],[id$=txtFrom]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });
            $('#txtFrom').click(function () {
                $('#txtFrom').datepicker('show');
            });
            //$('#ui-datepicker-div').show();
            //$('#txtDateFrom').datepicker('show');
        }

    </script>
</asp:Content>
