<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="AdminPermission.aspx.cs" Inherits="Library.AdminPermission" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="container tableborderst">
        <div style="width: 100%; display: none" class="title">
            <div style="width: 89%; float: left">
                &nbsp;
                      <asp:Label ID="lblTitle" Style="display: none" runat="server" Font-Names="Lucida Sans Unicode"
                          BorderColor="Black" Font-Bold="True"
                          Font-Size="X-Small"></asp:Label>
            </div>
            <div style="float: right; vertical-align: top">
                <a id="lnkHelp" href="#" style="display: none" onclick="ShowHelp('Help/Administration-Security-FormLevelPermissionforUserType.htm')">
                    <img height="15" src="help.jpg" /></a>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="plogin" runat="server">
                    <table class="no-more-tables tdmgr">
                        <tr>
                            <td>
                                <asp:Label ID="lbluser" runat="server" Text="<%$ Resources:ValidationResources, LUId %>">></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtusername" runat="server" Style="width: 90%; height: 25px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:ValidationResources, Passwrd %>">></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Style="width: 90%; height: 25px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="2">
                                <asp:Button ID="cmdlogin" runat="server" CssClass="btn btn-primary" OnClick="cmdlogin_Click"
                                    Text="<%$ Resources:ValidationResources, LLogin %>" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
						<asp:Panel ID="padmin" runat="server" Visible="false" class="no-more-tables">
							<asp:Menu ID="MMenu1" Orientation="Horizontal" BorderStyle="Inset"  OnMenuItemClick="MMenu1_MenuItemClick" runat="server">
		                    <Items>
		                    <asp:MenuItem Text="Menu Permission" Value="0"></asp:MenuItem>
		                    <asp:MenuItem Text="Features Permission" Value="1"></asp:MenuItem>		                       
		                    </Items>
		                    </asp:Menu>
		                   
		                   <asp:MultiView ID="MV1" ActiveViewIndex="0" runat="server">
		                   <asp:View ID="View1" runat="server">
		                   
                               <table id="Table1" class="no-more-tables table-condensed">
                                   <tr>
                                       <td style="width:23%"></td>
                                       <td>
                                           <asp:Label ID="msglabel" runat="server" Font-Names="Lucida Sans Unicode" Font-Bold="True"
                                              CssClass="err"></asp:Label></td>
                                   </tr>
                                   <tr>
                                       <td ><asp:Label ID="lblusertype" runat="server" 
                                            CssClass="span" Text="<%$ Resources:ValidationResources, LUserType %>"></asp:Label></td>
                                       <td>
                                           <input id="txtusertype"
                                               onblur="this.className='blur'" onfocus="this.className='focus'" type="text"
                                               runat="server" class="txt10" 
                                               style="font-family:Arial Unicode MS;width:90%" disabled="disabled"
                                               value="Admin">
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">
                                           <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources, CnkSelectA %>" OnCheckedChanged="chkAll_CheckedChanged" /></td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">
                                           <div class="allgriddiv confirmPanel" style="background-color:white; height:350px;">
                                               <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All" style="width:90%" ShowLines="True" ExpandDepth="2" MaxDataBindDepth="2" OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                               </asp:TreeView>
                                           </div>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td  colspan="2" style="text-align:center">
                                         
                                                    <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" Text="<%$ Resources:ValidationResources, bSave %>"  UseSubmitBehavior="False" CssClass="btn btn-primary"></asp:Button>
                                                    <asp:Button ID="cmdreset" runat="server" Text="<%$ Resources:ValidationResources, bReset %>" UseSubmitBehavior="False" CssClass="btn btn-primary" />
                                                    <asp:Button ID="cmddelete" runat="server" Text="<%$ Resources:ValidationResources, bdelete %>"  UseSubmitBehavior="False" CssClass="btn btn-primary" /> 
                                            
                                                    <asp:Button ID="cmdnext" runat="server" Text="<%$ Resources:ValidationResources, Next %>" Visible="False" OnClick="cmdnext_Click" UseSubmitBehavior="False" CssClass="btn btn-primary"></asp:Button></td>
                                            </tr>
                                         
                                   <tr>
                                       <td colspan="2">
                                           <asp:CheckBox ID="Chksearch" runat="server" OnCheckedChanged="Chksearch_CheckedChanged" AutoPostBack="True" Text="<%$ Resources:ValidationResources, bSearch %>" CssClass="span" /></td>
                                       
                                   </tr>
                                   <tr>
                                       <td>
                                           <asp:Label ID="Label2" runat="server" Text="<%$ Resources:ValidationResources, LUserType %>"  CssClass="span"></asp:Label></td>
                                       <td>
                                           <input id="txtCategory" onblur="this.className='blur'" onfocus="this.className='focus'" name="txtCategory" type="text" runat="server" class="txt10" style='font-family: Arial Unicode MS' />
                                           <asp:Button id="btnCategoryFilter2" OnClick="btnCategoryFilter_Click"  Text="<%$ Resources:ValidationResources, bSearch %>" runat="server"  class="btn btn-primary" />

                                       </td>
                                   </tr>
                                   <tr>
                                       <td></td>
                                       <td>
                                           <asp:ListBox ID="lstAllCategory" runat="server"  Style='font-family: Arial Unicode MS'></asp:ListBox></td>
                                   </tr>
                                   
                                   <tr>
                                       <td style="vertical-align:top" colspan="2">
                                           <asp:HiddenField ID="Hidden1" runat="server" />
                                           <input id="Hidden2" type="hidden" runat="server" />&nbsp;
                        <input id="txtusertypecode" name="txtusertypecode" type="hidden" runat="server" />
                                           <input id="yCoordHolder" runat="server" name="Hidden3" size="1" style="width: 22px; height: 22px"
                                               type="hidden" />
                                           <input id="xCoordHolder" runat="server" name="Hidden2" size="1" style="width: 22px; height: 22px"
                                               type="hidden" />
                                           <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                               ShowMessageBox="True" ShowSummary="False" />
                                          
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusertype"
                            ErrorMessage=" <%$ Resources:ValidationResources, EUsrTyp %>" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                       </td>
                                   </tr>
                                   <input id="hSubmit1" runat="server" name="hSubmit1" size="1" 
                                       type="hidden" value="0" />
                                   <asp:ListBox ID="hlstAllCategory" runat="server" Height="0px" Width="32px" Style="visibility: hidden"></asp:ListBox>
                                   <caption>
                                       <input id="Button2" runat="server" class="btnH" name="Button1" size="20"
                                           style="width: 1px; height: 1px" type="button" value="Button" />
                                   </caption>
                               </table>
                            
							
							</asp:View>
							
							<asp:View ID="View2" runat="server">
							<asp:UpdatePanel ID="UPF1" runat="server">
							<ContentTemplate>
							
							<br /><br />
							<asp:Panel ID="Featrures" Width="100%" runat="server">
							<div>
							<asp:CheckBox ID="chkAllF" AutoPostBack="true" runat="server" Text="Select All" />
							</div>
							<%--<br />
							<asp:CheckBoxList ID="FPer" runat="server">
							<asp:ListItem Text="Enable Photo Capturing by WebCamp" Value="1"></asp:ListItem>
							<asp:ListItem Text="Enable Signature Capturing by WebCamp" Value="2"></asp:ListItem>
							<asp:ListItem Text="Enable Thumb Capturing by Biometric Device" Value="3"></asp:ListItem>
							<asp:ListItem Text="Enable Add Copy New In Cataloging" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Enable Register Service for E-News Clipping " Value="5"></asp:ListItem>
                            <asp:ListItem Text="Enable Register Service for E-files " Value="6"></asp:ListItem> 
                             <asp:ListItem Text="Enable Register Service for E-Documents" Value="7"></asp:ListItem>
                              <asp:ListItem Text="Enable Register Service for E-Drawings " Value="8"></asp:ListItem>
                              <asp:ListItem Text="Enable Register Service for E-Equipment Mannual " Value="9"></asp:ListItem>
                              <asp:ListItem Text="Active MSSPL CATALOGING" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Do you want logo on Every Report?" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Get Book Class Number suggestions from MSSPL" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Enable RFID Process Using Database" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Enable RFID Process Tag User Area" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Manage Book Number of Call Number store with Individual Book Copy" Value="15"></asp:ListItem>
                                <asp:ListItem Text="Write EPC Card And Database" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Show Horizontal Menu Path at Top" Value="17"></asp:ListItem>
                                <asp:ListItem Text="Horizontal Menu Path at Top Full Text" Value="18"></asp:ListItem>
                            </asp:CheckBoxList>--%>
                                <asp:Panel runat="server" ID="pnlPer" style="border:1px solid grey;background-color:white;">
                                    <%--Important Note: Checkbox text e.g. 04,10 etc and Label id e.g. lab04, lab10 must match--%>
                         <table id="tblFeaturePrm"  data-page-length="25">
                             <thead>
                                 <tr>
                                     <th  data-orderable="false">
                                         Apply
                                     </th>
                                     <th>
                                         Option / Item
                                     </th>
                                 </tr>
                             </thead>
                             <tbody>
<tr>	
<td><asp:CheckBox ID="FPCheckBox1" runat="server" Text="01" /></td><td>	<asp:Label ID="lab01" runat="server" Text="Enable Photo Capturing by WebCamp"></asp:Label> </td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox2" runat="server" Text="02"  /></td><td>		<asp:Label ID="lab02" runat="server" Text="Enable Signature Capturing by WebCamp"></asp:Label></td>
</tr>
<tr>	
	<td><asp:CheckBox ID="FPCheckBox3" runat="server"  Text="03" /></td><td>	<asp:Label ID="lab03" runat="server" Text="Enable Thumb Capturing by Biometric Device"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox4" runat="server"  Text="04" /></td><td>		<asp:Label ID="lab04" runat="server" Text="Enable Add Copy New In Cataloging"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox5" runat="server"  Text="05" /></td>	<td>	<asp:Label ID="lab05" runat="server" Text="Enable Register Service for E-News Clipping"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox6" runat="server"  Text="06" /></td><td>		<asp:Label ID="lab06" runat="server" Text="Enable Register Service for E-files"></asp:Label></td>
</tr>
<tr>	
	<td><asp:CheckBox ID="FPCheckBox7" runat="server"  Text="07" /></td><td>	<asp:Label ID="lab07" runat="server" Text="Enable Register Service for E-Documents"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox8" runat="server"  Text="08" /></td><td>		<asp:Label ID="lab08" runat="server" Text="Enable Register Service for E-Drawings"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox9" runat="server"  Text="09" /></td>	<td>	<asp:Label ID="lab09" runat="server" Text="Enable Register Service for E-Equipment Mannual"></asp:Label> </td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox10" runat="server"  Text="10" /></td><td>		<asp:Label ID="lab10" runat="server" Text="Active MSSPL CATALOGING"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox11" runat="server"  Text="11" /></td>	<td>	<asp:Label ID="lab11" runat="server" Text="Do you want logo on Every Report?"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox12" runat="server"  Text="12" /></td><td>		<asp:Label ID="lab12" runat="server" Text="Get Book Class Number suggestions from MSSPL"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox13" runat="server"  Text="13" /></td>	<td>	<asp:Label ID="lab13" runat="server" Text="Enable RFID Process Using Database"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox14" runat="server"  Text="14" /></td><td>		<asp:Label ID="lab14" runat="server" Text="Enable RFID Process Tag User Area"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox15" runat="server"  Text="15" /></td>	<td>	<asp:Label ID="lab15" runat="server" Text="Manage Book Number of Call Number store with Individual Book Copy"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox16" runat="server"  Text="16" /></td><td>		<asp:Label ID="lab16" runat="server" Text="Write RFID EPC Card And Database"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox17" runat="server"  Text="17" /></td>	<td>	<asp:Label ID="lab17" runat="server" Text="Show Horizontal Menu Path at Top"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox18" runat="server"  Text="18" /></td><td>		<asp:Label ID="lab18" runat="server" Text="Horizontal Menu Path at Top Full Text"></asp:Label></td>
</tr>
<tr>	
<td><asp:CheckBox ID="FPCheckBox19" runat="server"  Text="19" /></td><td>		<asp:Label ID="Lab19" runat="server" Text="Record Marc 21 during Catalog Save"></asp:Label></td>
</tr>
                                 <tr>	
<td><asp:CheckBox ID="FPCheckBox20" runat="server"  Text="20" /></td><td>		<asp:Label ID="Lab20" runat="server" Text="Enable RFID EPC Read/Delete at Server Not Local. RfidApi will be used to Record EPC"></asp:Label></td>
</tr>
                                 <tr>	
<td><asp:CheckBox ID="FPCheckBox21" runat="server"  Text="21" /></td><td>		<asp:Label ID="Lab21" runat="server" Text="If RFID EPC Read/Delete at Server,Use separate server Conn. &quot;RemoteRfidConns&quot; If Not Checked normal connection string is used "></asp:Label></td>
</tr>

                             </tbody>
                         </table>
                                                                    </asp:Panel>

							<div  style="border:solid" >
                                <asp:Label ID="lblBasicCatalog" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="Medium" Text="Opac Search Permission:-"></asp:Label></br>
                                <asp:CheckBox ID="rblCallNo" Text="Call No" runat="server" />                      
                                <asp:CheckBox ID="CpyInform" Text="Copies Information" runat="server" />                         
                                <asp:CheckBox ID="Reserve" Text="Reserve" runat="server" />
                                <asp:CheckBox ID="Content" Text="Content" runat="server" />     </br>                      
                                <asp:CheckBox ID="AddKey" Text="Add Keyword(s)" runat="server" />                   
                                <asp:CheckBox ID="AddCart" Text="Add To Cart" runat="server" />   
                                <asp:CheckBox ID="AuthorIfo" Text="Author Info" runat="server" />
                                <asp:CheckBox ID="BookInfo" Text="Book Info" runat="server" />
							</div>
							<div style="width:100%;text-align:center">
							<asp:Button ID="btnsubmitF" CssClass="btn btn-primary" OnClick="btnsubmitF_Click" runat="server" Text="Submit" />
							</div>
							</asp:Panel>
							
							</ContentTemplate>
							</asp:UpdatePanel>
							
							
							</asp:View>
		                   
		                   
		                   
							
		                   </asp:MultiView>
						
							
							</asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
