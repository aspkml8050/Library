<%@ Page Language="C#"  MasterPageFile="~/LibraryMain.master"  AutoEventWireup="true" CodeBehind="frm_mediatype.aspx.cs" Inherits="Library.frm_mediatype" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="mtHead" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">

        function validateform() {
            let v1 = $('[id$=txtmedianame]').val().trim();
            if (v1 == '') {
                alert('Media Name required.');
                return false;
            }
            let v2 = $('[id$=txtshortname]').val().trim();
            if (v2 == '') {
                alert('Media short Name required.');
                return false;

            }
            return true;
        }

    </script>


    </asp:Content>

<asp:Content ID="mtBody" runat="server" ContentPlaceHolderID="MainContent">
          <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="container tableborderst">
			<div style="width:100%;display:none;display:none" class="title">
                    <div style="width:89%;float:left" >
                        <asp:label id="lbltitle" runat="server"   style="text-align:center" Width="100%"> Media Type </asp:label>
                       </div>
                <div style="float:right;vertical-align:top"> 
                        <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-Media Types.htm')">
                        <img height="15" src="help.jpg"  alt="Help"  /></a>
						</div></div>


         <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
 <div class="no-more-tables" style="width:100%">
                                     <table id="Table1" class="table-condensed GenTable1">
                                         <tr>
                                             <td colspan="4">
                                                 <asp:Label ID="msglabel" runat="server" CssClass="err" Font-Bold="True" ForeColor="Red"
                                                     Font-Size="X-Small" Font-Names="Lucida Sans Unicode"></asp:Label></td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:Label ID="Label9" runat="server" Text="<%$ Resources:ValidationResources,LMeTy %>"></asp:Label>

                                             </td>
                                             <td>
                                                 <input class="txt10" id="txtmedianame" onblur="this.className='blur'" style='<%$ Resources: ValidationResources, TextBox2 %>'
                                                     onfocus="this.className='focus'" type="text" size="37" name="txtdate" runat="server" maxlength="40">&nbsp;
										<asp:Label ID="Label1" runat="server" CssClass="star">*</asp:Label></td>
                                       
                                             <td>
                                                 <asp:Label ID="Label8" runat="server" Text="<%$ Resources:ValidationResources,LShortName %>"></asp:Label></td>
                                             <td>
                                                 <input class="txt10" id="txtshortname" onblur="this.className='blur'" style="<%$ Resources: ValidationResources, TextBox2 %>;"
                                                     onfocus="this.className='focus'" type="text" maxlength="10" name="txtdate"
                                                     runat="server">&nbsp;
										<asp:Label ID="Label2" runat="server" CssClass="star">*</asp:Label></td>
                                         </tr>
                                         <tr>

                                             <td colspan="4" style="text-align: center">
                                                 <%--<asp:Button ID="cmdsave" runat="server" Text="<%$ Resources:ValidationResources,bSave %>" AccessKey="S" UseSubmitBehavior="False" OnClientClick=" if ( validateform()==false ) return false; else true ;" CssClass="btnstyle"></asp:Button>--%>

                        <asp:Button ID="cmdsave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdsave_Click" />                                                 <%--<asp:Button ID="cmdreset" runat="server" Text="<%$ Resources:ValidationResources,bReset%>" AccessKey="E" UseSubmitBehavior="False" CssClass="btnstyle"></asp:Button>--%>
                        <asp:Button ID="cmdreset" runat="server" CssClass="btn btn-primary" OnClick="cmdreset_Click" Text="Reset"/>
                                                 <%--<input id="cmddelete" onclick="if (DoConfirmation() == false) return false;"
                                                     type="button" value="<%$ Resources:ValidationResources,bDelete%>" name="cmddelete" runat="server" accesskey="X" class="btnstyle">--%>
                                                 <asp:Button id="cmddelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="cmddelete_Click"/>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="4">
                                                 <asp:Label ID="Label14" runat="server" CssClass="showBoldExist" Text="<%$ Resources:ValidationResources,MTExistDetail%>"></asp:Label></td>
                                         </tr>


                                     </table>
                             </div>
                              <div class="tdmgr">

                                 <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                <asp:datagrid id="grd_media" runat="server" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' OnItemCommand="grd_media_ItemCommand" OnPageIndexChanged="grd_media_PageIndexChanged" >
											
											<Columns>
												<asp:ButtonColumn Text="<%$ Resources:ValidationResources, LMeTy %>" HeaderText="<%$ Resources:ValidationResources, LMeTy %>" CommandName="Select" DataTextField="media_name" >
                                                    
                                                </asp:ButtonColumn>
												<asp:BoundColumn DataField="media_name" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LMeTy %>" Visible="False" ></asp:BoundColumn>
												<asp:BoundColumn DataField="short_name" HeaderText="<%$ Resources:ValidationResources, LShortName %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>" ></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="media_id" HeaderText="<%$ Resources:ValidationResources, DMediaid %>"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>

                                    
                                </div>


                                     <INPUT id="hdUnableMsg" type="hidden" runat="server" style="width: 37px; height: 16px;"><INPUT id="Hidden3" style="WIDTH: 54px; HEIGHT: 16px" type="hidden" name="Hidden3"
							runat="server"><INPUT id="Hdsave" style="WIDTH: 36px; HEIGHT: 16px" type="hidden" name="Hdsave"
							runat="server"><INPUT id="xCoordHolder" style="WIDTH: 12px; HEIGHT: 16px" type="hidden" value="0"
							name="xCoordHolder" runat="server"><INPUT id="yCoordHolder" type="hidden" value="0" name="yCoordHolder" runat="server" style="width: 37px; height: 8px;"><INPUT id="Hdaccession" style="WIDTH: 36px; HEIGHT: 16px" type="hidden" name="Hdaccession"
							runat="server"><input id="Hd_name" runat="server" style="width: 52px; height: 16px" type="hidden" /><input id="hd_short" runat="server" style="width: 52px; height: 16px" type="hidden" />
                                <INPUT id="hdTop" type="hidden" runat="server" style="width: 10px; height: 8px;">
                                </ContentTemplate>
             </asp:UpdatePanel>
         </div>
    </asp:Content>