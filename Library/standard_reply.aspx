<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="standard_reply.aspx.cs" Inherits="Library.standard_reply" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="SreplHead" runat="server" ContentPlaceHolderID="head">
	<Link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >

</asp:Content>

<asp:Content ID="SreplMain" runat="server" ContentPlaceHolderID="MainContent">
	<asp:UpdateProgress ID="UpPorg1" runat="server">
		  <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
		</asp:UpdateProgress>

	<div class="container tableborderst"> 
		<div style="width:100%;display:none" class="title">
			<div style="width:89%;float:left" >
				<asp:label id="lbltitle" runat="server" Width="100%"  ForeColor="White"> Standard Reply</asp:label>
				</div>
			 <div style="float:right;vertical-align:top"> 
				  <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters Standard Reply.htm')"> <img alt="Help?" height="15" src="help.jpg"  /></a>
				 </div>
			</div>
		<asp:UpdatePanel ID="UpdatePanel" runat="server">

			<ContentTemplate>
				 <div class="no-more-tables" style="width:100%">
					 	<TABLE id="Table1" class="table-condensed tdmgr">
							 <TR>
									<TD colSpan="2"><asp:label id="msglabel" runat="server"  CssClass="err" Font-Bold="True" ForeColor="Red"
											Font-Size="X-Small" Font-Names="Lucida Sans Unicode"></asp:label></TD>
								</TR>
							 <tr>
								 <TD><asp:label id="Label9" runat="server" Text ="<%$ Resources:ValidationResources,Title_stdRply %>" ></asp:label></TD>
									<TD ><INPUT class="txt10" id="txtreply" onblur="this.className='blur'"
											onfocus="this.className='focus'" 
                                            type="text" name="txtreply" runat="server" maxlength="50">
										<asp:label id="Label1" runat="server" Width="1px" CssClass="star" Height="16px">*</asp:label>

									</TD>
							 </tr>
							 <TR>
									<TD style="width:30%">
                                       <input id="hd_name" runat="server" type="hidden" /></TD>
									<TD >

										<TD >
										<asp:button id="cmdsave" runat="server" Text="<%$ Resources:ValidationResources,bSave %>" AccessKey="S" UseSubmitBehavior="False" CssClass="btn btn-primary" OnClick="cmdsave_Click"></asp:button>
												<asp:button id="cmdreset" runat="server"  Text="<%$ Resources:ValidationResources, bReset %>" AccessKey="E" UseSubmitBehavior="False" CssClass="btn btn-primary" OnClick="cmdreset_Click"></asp:button>

												<INPUT id="cmddelete" type="button" value="<%$ Resources:ValidationResources, bDel %>" style="display:none;" name="cmddelete" runat="server"  class="btn btn-primary">

											<asp:Button ID="cmddelete1" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="cmddelete1_Click"/>
										
									</TD>
							</TR>
							  <tr>
                                    <td colspan="2" >
                                        <asp:Label ID="Label14" CssClass="showBoldExist" runat="server"  Text ="<%$ Resources:ValidationResources,ExistSubDetail %>"></asp:Label></td>
                                </tr>
							 </TABLE>
					 </div>
				<div class="tdmgr">
				  <div id="dvgrd" class="allgriddiv" >
					  <asp:HiddenField runat="server" ID="hdnGrdId" />

               <asp:datagrid id="grd_reply" OnItemCommand="grd_reply_ItemCommand"  runat="server" CssClass="allgrid GenTable1" DataKeyField="reply_id">
					 <Columns>
                                                <asp:ButtonColumn CommandName="Select" DataTextField="reply" HeaderText="<%$ Resources:ValidationResources,Title_stdRply %>" >
                                                   
                                                </asp:ButtonColumn>
												<asp:BoundColumn Visible="False" DataField="reply_id"></asp:BoundColumn>
											</Columns>
					</asp:datagrid>
					  </div>
					</div>
				<INPUT id="hdUnableMsg" type="hidden" runat="server" ><INPUT id="Hidden3"  type="hidden" name="Hidden3"
							runat="server"><INPUT id="Hdsave"  type="hidden" name="Hdsave"
							runat="server"><INPUT id="xCoordHolder"  type="hidden" size="1" value="0"
							name="xCoordHolder" runat="server"><INPUT id="yCoordHolder" type="hidden" value="0" name="yCoordHolder" runat="server"><INPUT id="Hdaccession" type="hidden" name="Hdaccession"
							runat="server"><INPUT id="hdTop" type="hidden" runat="server" >
						<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtreply" Display="None"
							ErrorMessage="<%$ Resources:ValidationResources, MsgStndRpy %>" SetFocusOnError="True"></asp:requiredfieldvalidator><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
							ShowSummary="False"></asp:validationsummary>


				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
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


