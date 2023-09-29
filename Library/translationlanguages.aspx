<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master"  CodeBehind="translationlanguages.aspx.cs" Inherits="Library.translationlanguages" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="tranLangHead" runat="server" ContentPlaceHolderID="head">
	           <script type="text/javascript">
                   //On Page Load.
                   $(function () {
                       ForDataTable();
                   });
                   //On UpdatePanel Refresh.
                   var prm = Sys.WebForms.PageRequestManager.getInstance();
                   if (prm != null) {
                       prm.add_endRequest(function (sender, e) {
                           if (sender._postBackSettings.panelsToUpdate != null) {
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


               </script> 

</asp:Content>

<asp:Content ID="tranLangBody" runat="server" ContentPlaceHolderID="MainContent">

               <div class="container tableborderst" style="width:50%;margin-top:20px;padding:0;">
  
			 <div style="width:100%;display:none" class="title">
                    <div style="width:89%;float:left; display:none;" > &nbsp;
                        <asp:label id="lbltitle" runat="server" Width="100%" style="text-align:center" ></asp:label>
                        </div>
                  <div style="float:right;vertical-align:top"> &nbsp;
                        <a id="lnkHelp" style="display:none" href="#" onclick="ShowHelp('Help/Masters-translation languages.htm')"><img height="15" src="help.jpg" alt="Help" width="20" /></a>
               </div></div>
						<P>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     
                                <ContentTemplate>

       <div class="no-more-tables" style="width:100%">
							<TABLE id="Table1" class="col-md-12 GenTable1" >
								<TR>
									<TD colSpan="2"><asp:label id="msglabel" runat="server" CssClass="err"></asp:label></TD>
								</TR>
								<TR>
									<TD style="width:20%"><asp:label id="Label1" runat="server"  CssClass="span"  Text ="<%$ Resources:ValidationResources,LangName %>"></asp:label></TD>
									<TD ><INPUT class="txt10" id="txtLanguageName" onblur="this.className='blur'" onkeydown="txtLanguageName_onkeydown()"  
											onfocus="this.className='focus'" type="text" name="txtDescription" runat="server" maxlength="50">
                                        <asp:Label
                                                ID="Label69" runat="server" CssClass="star">*</asp:Label></TD>
								</TR>
                                <tr>
                                    <td>
                                       <asp:Label ID="Label2" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources,LShortName %>"></asp:Label></td>
                                    <td align="left"><INPUT class="txt10" id="txtShortName" onblur="this.className='blur'" onkeydown="txtLanguageName_onkeydown()"  
											onfocus="this.className='focus'" type="text" name="txtDescription" runat="server" maxlength="10"></td>
                                </tr>
								<TR>
								
									<TD colspan="2" style="text-align:center">
									<%--	<INPUT id="cmdsave"  type="button" value="<%$ Resources:ValidationResources,bSave %>" name="cmdsave"
														runat="server" accesskey="S" class="btnstyle">--%>
                                    <asp:Button id="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave_Click"/>
										<%--<INPUT id="cmdreset"  type="button" value="<%$ Resources:ValidationResources,bReset %>" name="cmdreset"
														runat="server" accesskey="E" class="btnstyle">--%>

                                        <asp:Button id="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset_Click"/>
												<%--<INPUT id="cmddelete" onclick="if (DoConfirmation() == false) return false;" type="button"
														value="<%$ Resources:ValidationResources,bdelete %>" name="cmddelete" runat="server" accesskey="X" class="btnstyle">--%>
                                        <asp:Button ID="cmddelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="cmddelete_Click"/>
										
									</TD>
								</TR>
								<TR>
									<TD colSpan="2"><asp:label id="Label14" runat="server"  CssClass="showBoldExist" Text="<%$ Resources:ValidationResources, ExistLang%>"></asp:label></TD>
								</TR>
								
								<tr><td>  
							</TABLE></div>

									 <div style="width:100%; overflow:auto; max-height:250px; text-align:center;" id="dvgrd" runat="server">                               <%-- OnPageIndexChanged="dgLanguage_PageIndexChanged1"   --%>
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                 <asp:datagrid id="dgLanguage"  runat="server" Width="100%"  OnItemCommand="dgLanguage_ItemCommand1" OnSortCommand="dgLanguage_SortCommand" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
											
											<Columns>
												<asp:ButtonColumn DataTextField="Language_Name" HeaderText="<%$ Resources:ValidationResources, LangName %>"
													CommandName="Select" SortExpression="Language_Name">                                                   
                                                </asp:ButtonColumn>
												<asp:BoundColumn Visible="False" DataField="Language_Id" HeaderText="<%$ Resources:ValidationResources, GrLangId %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Font_Name" HeaderText="<%$ Resources:ValidationResources, LShortName %>" SortExpression="Font_Name"></asp:BoundColumn>
											</Columns>
											</asp:datagrid></div>
                                      <INPUT id="txtLanguageID" type="hidden" runat="server" style="WIDTH: 16px; HEIGHT: 22px"><INPUT id="hdUnableMsg" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" name="hdUnableMsg"
							runat="server">
                        <input id="hdTop" runat="server" style="width: 8px" type="hidden" />
                        <input id="Hidden3" runat="server" type="hidden" style="width: 16px" /><INPUT id="Hidden2" type="hidden" runat="server" style="width: 16px"><INPUT id="xCoordHolder" type="hidden" name="Hidden5" runat="server" style="width: 16px"><INPUT id="yCoordHolder" type="hidden" name="Hidden5" runat="server" style="width: 24px"></td></tr>
							<INPUT id="Hidden1" type="hidden" runat="server" style="WIDTH: 17px; HEIGHT: 22px" size="1">
                 
							       	         </ContentTemplate>
                            </asp:UpdatePanel>
                           
					
							
						</P>
                    
             </div>
              
</asp:Content>
