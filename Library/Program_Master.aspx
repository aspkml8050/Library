<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="Program_Master.aspx.cs" Inherits="Library.Program_Master" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="pmHead" runat="server" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content ID="pmMain" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
         <div class="container tableborderst" >
      
		<div style="width:100%;display:none;display:none" class="title"> &nbsp;
                   <div style="width:89%;float:left;display:none" >
                       <asp:label id="lbltitle" runat="server" Width="100%" > Media Type </asp:label>
                       </div>
               <div style="float:right;vertical-align:top; display:none;">
                       <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-course.htm')">
                           <img height="18" src="help.jpg" alt="Help?"/></a>
              </div></div>
                      
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                               <ContentTemplate>
                                     <div class="no-more-tables" style="width:100%">
                                   <table id="Table1" class="col-md-12 table-condensed cf GenTable1">
                                       <tr>
                                           <td colspan="2">
                                               <asp:Label ID="msglabel" runat="server" CssClass="err" Font-Bold="True" ForeColor="Red"
                                                   Font-Size="X-Small" Font-Names="Lucida Sans Unicode"></asp:Label></td>
                                       </tr>
                                       <tr>
                                           <td colspan="2" style="text-align:center;">
                                               <asp:RadioButton ID="RdCourse" runat="server" AutoPostBack="True" Checked="True"
                                                   Font-Bold="True" GroupName="CourseDesignationGroup" Text="<%$ Resources:ValidationResources,RBCourse %>"
                                                   CssClass="opt" />
                                               &nbsp;
                                           
                                               <asp:RadioButton ID="RdDesignation" runat="server" AutoPostBack="True" Font-Bold="True" GroupName="CourseDesignationGroup" Text="<%$ Resources:ValidationResources,RBDesignation %>" CssClass="opt" />

                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="class20">
                                               <asp:Label ID="Label9" runat="server" Text="<%$ Resources:ValidationResources,Lcd %>"></asp:Label>
                                           </td>
                                           <td>
                                               <input class="txt10" id="txtprogramname" onblur="this.className='blur'" style="<%$ Resources: ValidationResources, TextBox2 %>"
                                                   onfocus="this.className='focus'" type="text" name="txtdate" runat="server" maxlength="50">
									<asp:Label ID="Label1" runat="server" CssClass="star">*</asp:Label></td>
                                       </tr>
                                       <tr>
                                           <td>
                                               <asp:Label ID="Label8" runat="server" Text="<%$ Resources:ValidationResources,LShortName %>"></asp:Label></td>
                                           <td>
                                               <input class="txt10" id="txtshortname" onblur="this.className='blur'" style="<%$ Resources: ValidationResources, TextBox2 %>;"
                                                   onfocus="this.className='focus'" type="text" maxlength="10" name="txtdate"
                                                   runat="server">
									<asp:Label ID="Label2" runat="server" CssClass="star">*</asp:Label></td>
                                       </tr>
                                       <tr>
                                           <td>
                                               <asp:Label ID="Label11" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LDeptName %>"></asp:Label></td>
                                           <td>
                                               <%-- jayant 22/2/2021--%>
                                          <%-- <asp:DropDownList ID="cmbdept" runat="server" CssClass="txt10" Height="30" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                                                   Font-Size="X-Small" onblur="this.className='blur'" onfocus="this.className='focus'">
                                               </asp:DropDownL--%>
                                              
                                              
                                               <asp:DropDownList ID="cmbdept"  runat="server"   ></asp:DropDownList>
                                               <asp:Label ID="Label3" runat="server" CssClass="star">*</asp:Label>
                                           </td>
                                       </tr>
                                       <tr>

                                           <td colspan="2">
                                               <asp:CheckBox ID="Chkimport" AutoPostBack="true" runat="server" Text='<%$Resources : ValidationResources,importindestination%>'
                                                   Font-Bold="True" Font-Names="Verdana" Font-Size="9pt" ForeColor="#009900" />
                                           </td>
                                       </tr>
                                       <tr>
                                           <%--Add colspan and textalign--%>
                                           <td id="Td1" colspan="2" style="text-align:center">
                                               <input id="Hd_name" runat="server"
                                                   type="hidden" />
                                               <input id="hd_short" runat="server"
                                                   type="hidden" />
                                           

                                               <asp:Button ID="cmdsave" runat="server" AccessKey="S" OnClick="cmdsave_Click"  CssClass="btn btn-primary"  
                                                   Text="<%$ Resources:ValidationResources,bSave %>"  />

                                               <asp:Button ID="cmdreset" OnClick="cmdreset_Click" runat="server" AccessKey="E"  CssClass="btn btn-primary"
                                                   Text="<%$ Resources:ValidationResources,bReset %>"  />

                                               <asp:Button ID="cmddelete2" runat="server" CssClass="btn btn-primary"
                                                    Text="<%$ Resources:ValidationResources,bDelete %>" OnClick="cmddelete2_Click" />
                                               <input id="cmddelete" runat="server" accesskey="X" style="display:none"  CssClass="btn btn-primary"

                                                   onclick="if (DoConfirmation() == false) return false;"
                                                   type="submit" value="<%$ Resources:ValidationResources,bDelete %>" />
                                               </input>
                                              
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="2">
                                               <asp:Label ID="Label4" runat="server" Text="<%$ Resources:ValidationResources,ExistCDdetail %>" CssClass="showBoldExist"></asp:Label></td>
                                       </tr>

                                   </table></div>
                                    <div class="allgriddiv" id="dvgrd" runat="server">                                  
                   <asp:HiddenField runat="server" ID="hdnGrdId" />
                 <asp:DataGrid ID="grd_media" Width="100%"  runat="server" OnItemCommand="grd_media_ItemCommand" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' >
                                          
                                           <Columns>
                                              <asp:ButtonColumn Text="<%$ Resources:ValidationResources, Grtxtdate %>" DataTextField="program_name"  HeaderText="<%$ Resources:ValidationResources, Lcd %>" CommandName="Select">
                                                
                                              </asp:ButtonColumn>
                                              <asp:BoundColumn DataField="short_name" HeaderText="<%$ Resources:ValidationResources, LShortName %>" >
                                                                                                  
                                               </asp:BoundColumn>
                                               <asp:BoundColumn DataField="program_id" HeaderText="<%$ Resources:ValidationResources, GrProgId %>" Visible="False" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="department" HeaderText="<%$ Resources:ValidationResources, LDeptm %>" >
                                               </asp:BoundColumn>
                                           </Columns>
                                       </asp:DataGrid>
                 </div>
                                   <asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
						ShowSummary="False"></asp:validationsummary>    
                                       <INPUT id="hdUnableMsg" type="hidden" runat="server" >
                                       <INPUT id="Hidden3"  type="hidden" name="Hidden3" runat="server">
                                       <INPUT id="Hdsave"  type="hidden" size="1" name="Hdsave" runat="server">
                                       <INPUT id="xCoordHolder"  type="hidden" size="1" value="0"
						name="xCoordHolder" runat="server">
                                       <INPUT id="yCoordHolder" type="hidden" size="1" value="0" name="yCoordHolder" runat="server">
                                       <INPUT id="Hdaccession"  type="hidden" size="1" name="Hdaccession"
						runat="server">
                                       <INPUT id="hdTop" type="hidden" runat="server" >
                                       <input id="HComboSelect" runat="server"
                           type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>"  class="btn" />
                               </ContentTemplate>
                           </asp:UpdatePanel>		
            </div>
   <script type="text/javascript">
       //On Page Load.
       $(function () {
           ForDataTable();
//SetListBox();
       });

       //On UpdatePanel Refresh.
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       if (prm != null) {
           prm.add_endRequest(function (sender, e) {
               if (sender._postBackSettings.panelsToUpdate != null) {
                   ForDataTable();
                 //  SetListBox();
               }
           });
       };
       function ForDataTable() {
           try {
               let grd = $("[id$='MainContent_grd_media']");
               let leng = grd.find('tr').length;
               if (leng > 0) {
                   $("#MainContent_grd_media tbody").before("<thead><tr></tr></thead>");
                   var cols = $("#MainContent_grd_media tbody tr:first td");
                   let colsh = '';
                   for (let x = 0; x < cols.length; x++) {
                       colsh += '<th>' + $(cols[x]).text() + '</th>';
                   }
                   $("#MainContent_grd_media thead tr").append(colsh);
                   //$("#MainContent_DataGrid1 tbody tr:first").remove();
                   $("#MainContent_grd_media").DataTable();
               }
           } catch (er) {
               alert(er + '; Make sure grid has data');
           }
          // try {
            //   var grdId = $("[id$=hdnGrdId]").val();
              // //alert(grdId);
             //  $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
             ////  ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]",200);
          // }
           //catch (err) {
           //}
       }
       function SetListBox() {
           $('[id*=cmbdept]').multiselect({
               enableCaseInsensitiveFiltering: true,
               buttonWidth: '90%',
               includeSelectAllOption: true,
               maxHeight: 150,
               width: 315,
               enableFiltering: true,
               filterPlaceholder: 'Search'
           });
       }


   </script> </asp:Content>