<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="Subject_master.aspx.cs" Inherits="Library.Subject_master" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="SubjHead" runat="server" ContentPlaceHolderID="head">

</asp:Content>
<asp:Content ID="SubjBody" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="container tableborderst" style="width:50%;margin-top:20px;padding:0;">
         <div style="width:100%;display:none" class="title">
              <div style="width:89%;float:left; display:none;" >
                  <asp:label id="lbltitle" runat="server" Width="100%" style="text-align:center" > Media Type </asp:label>
                  </div>
             <div style="float:right;vertical-align:top; display:none;">
                  <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-Subjects.htm')"><img height="15" src="help.jpg" /></a>
                 </div>
             </div>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                  <div class="no-more-tables" style="width:100%">
                      <table id="Table1" class="GenTable1" style="width:100%">
                          <tr>
                              <td colSpan="2"><asp:label id="msglabel" runat="server" ></asp:label></td>
                          </tr>
                          <tr>
                              <td class="class20"><asp:label id="Label9" runat="server"  Text="<%$ Resources:ValidationResources,LSubName %>" ></asp:label></td>
                              <td>
                                  <INPUT class="txt10" id="txtsubject" onblur="this.className='blur'" style='<%$ Resources:ValidationResources, TextBox2%>;'
											onfocus="this.className='focus'" type="text" name="txtservice" runat="server" >
										<asp:Image ID="Image6" Style="display:none" runat="server" ImageUrl="~/Images/sugg.png" />
                                      <asp:label id="Label1" runat="server" CssClass="star">*</asp:label>
                              </td>
                          </tr>
                          <tr>
                              <td colspan="2" style="text-align:center" >
                                  <asp:button id="cmdsave" runat="server" Text="<%$ Resources:ValidationResources,bSave%>" AccessKey="S"   OnClick="cmdsave_Click" CssClass="btn btn-primary"></asp:button>
                                  

                                  <asp:button id="cmdreset" runat="server" Text="<%$ Resources:ValidationResources,bReset%>" AccessKey="E" UseSubmitBehavior="False" Style="display:none;" CssClass="btnstyle"></asp:button>
                                 <asp:Button ID="cmdreset1" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset1_Click"/>

                                  <INPUT id="cmddelete"  onclick="if (DoConfirmation() == false) return false;" type="button" value="<%$ Resources:ValidationResources,bdelete%>" name="cmddelete" runat="server" accesskey="X" Style="display:none;" class="btnstyle">

                                  <asp:Button ID="cmddelete1" runat="server" Text="Delete" class="btn btn-primary" OnClick="cmddelete1_Click"/>

                                  </td>
                          </tr>

                          <tr>
                                    <td colspan="2" >
                                        <asp:Label ID="Label14" runat="server" CssClass="showBoldExist"  Text ="<%$ Resources:ValidationResources,ExistSubDetail%>"></asp:Label></td>
                                </tr>
                      </table>
                      </div>
                  <div style="max-height:300px" class="allgriddiv" id="dvgrd" runat="server"> 
                      <asp:HiddenField runat="server" ID="hdnGrdId" />
                      <asp:datagrid id="grd_media" OnItemCommand="grd_media_ItemCommand" runat="server" Width="100%" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>

                          <Columns>
                                                
                                                <asp:ButtonColumn CommandName="Select" DataTextField="subject" HeaderText="<%$ Resources:ValidationResources, Lsubj %>"
                                                    Text="Subject" SortExpression="Subject">
                                                    </asp:ButtonColumn>
												<asp:BoundColumn Visible="False" DataField="subject_id" HeaderText="<%$ Resources:ValidationResources, GrSubId %>"></asp:BoundColumn>
											</Columns>
                     </asp:datagrid>
                      </div>
                  <input id="hd_name" runat="server"  type="hidden" />
                              <asp:Button ID="Button4" runat="server" CausesValidation="False" style="visibility:hidden "
                                   Text="Go" UseSubmitBehavior="False" class="btnH"
                                   Visible="true"  />
                            &nbsp;</P>

                    <INPUT id="hdUnableMsg" type="hidden" runat="server" style="width: 55px"><INPUT id="Hidden3" style="WIDTH: 29px; HEIGHT: 22px" type="hidden" name="Hidden3"
							runat="server"><INPUT id="Hdsave" style="WIDTH: 27px; HEIGHT: 22px" type="hidden" name="Hdsave"
							runat="server"><INPUT id="xCoordHolder" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="xCoordHolder" runat="server"><INPUT id="yCoordHolder" type="hidden" value="0" name="yCoordHolder" runat="server" style="width: 25px"><INPUT id="Hdaccession" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="Hdaccession"
							runat="server"><INPUT id="hdTop" type="hidden" runat="server" style="width: 44px">

                   <asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
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
                ThreeLevelSearch($('#'+ grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }

    
    </script>
</asp:Content>
   
