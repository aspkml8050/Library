 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="upload.aspx.cs" Inherits="Library.upload" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="uplHead" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 156px;
        }
        .style2
        {
            height: 36px;
            width: 156px;
        }
    </style>
    	<script type="text/javascript">
            function onTextBoxUpdate2(evt) {

                var textBoxID = evt.source.textBoxID;
                if (evt.selMenuItem != null) {
                    document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
                    document.getElementById("cmdsearch").click();
                }
                evt.preventDefault();
            }

            function validateform() {
                let v1 = $('[id$=Txtgroup]').val().trim();
                if (v1 == '') {
                    alert('Group required.');
                    return false;
                }
                let v2 = $('[id$=TxtTitle]').val().trim();
                if (v2 == '') {
                    alert('Title required.');
                    return false;

                }
                return true;
            }
        </script>

    <Link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >
</asp:Content>

<asp:Content ID="uplBody" runat="server" ContentPlaceHolderID="MainContent">
   <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

           <div class="container tableborderst" >   
        
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
   
       <asp:Label ID="lbltitle" runat="server" ForeColor="White"  Text ="File Uploads"  Width="100%"></asp:Label>
       </div>
                  <div style="float:right;vertical-align:top"> 
                    <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-File-UpLoad.htm')">
                        <img height="15" src="help.jpg" alt="Help" /></a>
          </div></div>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
<asp:PostBackTrigger ControlID="btnupld" />
</Triggers>
                            <ContentTemplate>
                                 <div class="no-more-tables" style="width:100%">
              <table id="TABLE1" class="table-condensed tdmgr GenTable1">
                            <tr>
                                <td colspan="4"  >
                                    <asp:Label ID="LblMessage" runat="server"  ForeColor="Red"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width:34%">
                                   <asp:Label ID="lblfiletitle0" runat="server" CssClass="span" 
                                        Text=" Group" ></asp:Label>
                                </td>
                                <td >
                                    <input ID="Txtgroup" runat="server" class="txt10" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" 
                                        onkeypress="disallowSingleQuote(this);" 
                                          
                                        type="text" />
<%--                                        <Custom:AutoSuggestMenu ID="AutoSuggestMenu1" runat="server" 
                                                 KeyPressDelay="10" MaxSuggestChars="100" 
                                                OnClientTextBoxUpdate="onTextBoxUpdate2" 
                                                OnGetSuggestions="GetSuggestions_restype1" ResourcesDir="~/asm_includes" 
                                                SelectedValue=" " TargetControlID="Txtgroup" UpdateTextBoxOnUpDown="False" />--%>
                                        
                                 
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" />
                                </td>
                                <td>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblfiletitle" runat="server" CssClass="span" 
                                        Text="<%$ Resources:ValidationResources, BkTtl %>"></asp:Label>
                                </td>
                                <td>
                                    <input ID="TxtTitle" runat="server" class="txt10" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" 
                                        onkeypress="disallowSingleQuote(this);" 
                                        
                                        type="text" />
                                    <asp:Label ID="Lblmnd1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                   <asp:Label ID="lblupld" runat="server" Text="<% $ Resources:ValidationResources, lblUploadFile %>" CssClass="span"></asp:Label></td>
                                <td colspan="2">
                                    <asp:FileUpload ID="FileUpld" onfocus="this.className='focus'" onblur="this.className='blur'" runat="server" Width="90%" />
                                
                                   <asp:Label ID="lblmnd2" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblupld0" runat="server"  
                                        Text="Show In Web-Opac"></asp:Label>
                                </td>
                                <td  colspan="2">
                                    <asp:DropDownList ID="DropDownList1" Height="30" runat="server">
                                    <asp:ListItem Selected ="True" Value ='Y' Text ="Yes"></asp:ListItem>
                                    <asp:ListItem Value ='N' Text ="No"></asp:ListItem>
                                    
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                         <tr>
                                <td >
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" DisplayMode="List" />
                                </td>
                               
                                       
                                            <td colspan="3" >
                                                <%--<input id="btnupld" type="button" value="<%$ Resources:ValidationResources,FileUpld %>"  runat="server" causesvalidation="true" onclick =" if ( validateform()==false ) return false; else true ;" onserverclick="btnupld_ServerClick" class="btnstyle" />--%>
                               <asp:Button id="btnupld" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnupld_Click"/>            
                                               <%-- <input id="btnreset" type="button" value="<%$ Resources:ValidationResources,bReset%>"  runat="server" causesvalidation="false" onserverclick="btnreset_ServerClick" class="btnstyle"/>--%>
                        <asp:Button id="btnreset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btnreset_Click"/>                 
                                                <%--<input id="btndelete" type="button" value="<%$ Resources:ValidationResources,bdelete %>"  runat="server" onclick="if (DoConfirmation() == false) return false;" causesvalidation="false" onserverclick="btndelete_ServerClick" class="btnstyle"/>--%>
                        <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-primary" Onclick="btndelete_Click"/>               
                                   
                              
                               
                                </td>
                            </tr>
                            <tr>
                                <td  colspan="4">
                                   <asp:Label ID="Label1" runat="server" Text="<%$ Resources:ValidationResources,ExistDownloads %>" CssClass="head1"></asp:Label></td>
                            </tr>
                        
                  
                           
                        </table></div>          

 <div class="allgriddiv" id="dvgrd" runat="server"> 

                    <asp:HiddenField runat="server" ID="hdnGrdId" />
            <asp:GridView ID="Grddownld" runat="server" AllowPaging="false" OnRowCommand="Grddownld_RowCommand" OnSelectedIndexChanged="Grddownld_SelectedIndexChanged" OnSorting="Grddownld_Sorting"
                OnPageIndexChanging="Grddownld_PageIndexChanging" AutoGenerateColumns="False" CssClass="allgrid GenTable1"   
                GridLines="None" AllowSorting="True" PageSize="1000000">
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:ValidationResources,LTitle %>" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnktitle" runat="server" CausesValidation="False"  CommandName="show" CommandArgument='<%#Eval("id") %>' Text='<%#Eval("title") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:BoundField DataField="file_url" HeaderText="<%$ Resources:ValidationResources,FName %>" >
                                                </asp:BoundField>
                                                <asp:BoundField DataField="group_name" HeaderText="Group Name">
                                                </asp:BoundField>
                                        </Columns>
                                        
                                    </asp:GridView>
                              </div>
                                   <input id="HDtitleid" runat="server" style="width: 14px" type="hidden" />

                          <img ID="image1" runat="server" src=""  alt="img"
                                        style="border-style: solid; border-color: inherit; border-width: 1px; width: 2px; height: 8px;visibility:hidden" />

                                <script type="text/javascript">
                                    function selGrp(gs) {
                                        document.getElementById("ctl00_MainContent_Txtgroup").value = gs.text;
                                        return false;
                                    }
                                </script>

  <div class="allgrid">
      <asp:Label ID="Label2" runat="server" Text="Groups to Select"  Font-Bold="true" ></asp:Label>
      <div class="allgriddiv">
          <asp:GridView ID="grdGrp" runat="server" CssClass="allgrid GenTable1" AutoGenerateColumns="false">
          <Columns>
              <asp:TemplateField>
                  <HeaderTemplate>
                      Group
                  </HeaderTemplate>
                  <ItemTemplate>
                      <asp:HiddenField ID="hdGrpid" runat="server" Value='<%# Eval("id") %>' /> 
                      <asp:LinkButton ID="lnkGrp"  OnClientClick="return selGrp(this);" runat="server" Text='<%# Eval("group_name") %>'></asp:LinkButton>
                  </ItemTemplate>
              </asp:TemplateField>
              
          </Columns>
      </asp:GridView>
  </div>
      </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <asp:Button ID="cmdsearch" runat="server" CausesValidation="False"
                                    Height="1px" Text="Go" UseSubmitBehavior="False" class="btnH"
                                    Width="1px" />
                    <%--</p>--%>
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
                      //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                      ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
                  }
                  catch (err) {
                  }
              }


          </script>  
</asp:Content>


