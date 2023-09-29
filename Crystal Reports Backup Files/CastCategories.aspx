<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="CastCategories.aspx.cs" Inherits="Library.CastCategories" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="hd" runat="server" ContentPlaceHolderID="head">
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
              $('#' + grdId + ' tr:first td').contents().unwrap().wrap('<th></th>');
              //ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]",200);
          }
          catch (err) {
          }
          }
      </script>
          <script type="text/javascript">

              function chk() {
            if (document.Form1.hdTop.value == "top") {
                  window.scrollTo(0, 0);
              document.Form1.hdTop.value = 0;
            }
        }
              function txtshortname_OnKeypress() {
            // to disallow - sign
            if (window.event.keyCode == 45) {
                  window.event.keyCode = 0;
            }
        }
              function validateform() {
                  let v1 = $('[id$=txtStatus]').val().trim();
              if (v1 == '') {
                  alert('Caste  Name required.');
              return false;
            }
              let v2 = $('[id$=txtshortname]').val().trim();
              if (v2 == '') {
                  alert('Caste short Name required.');
              return false;

            }
              return true;
        }
      </script>

</asp:Content>

<asp:Content ID="main" runat="server" ContentPlaceHolderID="MainContent">
       <div>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
        Font-Size="11px" Height="90px" ShowMessageBox="True" ShowSummary="False" Style="z-index: 101;
        left: 392px; position: absolute; top: 296px" Width="8px" />
           <span>delete not done</span>
    <div class="container tableborderst">
   
 
         <div style="width:100%;display:none;display:none" class="title">
                <div style="width:89%;float:left" >
                <asp:Label ID="lblt1" runat="server" Width="100%" style="text-align:center"></asp:Label>
                    </div>
             <div style="float:right;vertical-align:top">
                     <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-caste categories.htm')">
                         <img alt="Help?" height="15" src="help.jpg"  />

                     </a>
                 </div></div>
  <asp:UpdatePanel runat="server" ID="upds">
      <ContentTemplate>

                                <div class="no-more-tables" style="width:100%">  
                    <table id="Table1" class="table-condensed tdmgr GenTable1" >
                      
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td >
                             <asp:Label ID="lbldepartmentname" runat="server" CssClass="span"
                                     Text ="<%$ Resources:ValidationResources, LCastcat %>"></asp:Label></td>
                            <td >
                                <input id="txtStatus" runat="server"   name="txtdepartmentname"
                                   
                                    type="text" maxlength="40" />
                                <asp:Label ID="Label8" runat="server" CssClass="star" >*</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="class20">
                               <asp:Label ID="lblshortname" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LShortName %>"></asp:Label></td>
                            <td  >
                                <input id="txtshortname" runat="server" class="txt10" maxlength="3" name="txtshortname"
                                    onblur="this.className='blur'"  type="text" />
                                <asp:Label ID="Label1" runat="server" CssClass="star" >*</asp:Label></td>
                        </tr>
                        <tr>
                           
                            <td  colspan="2" style="text-align:center">
                               <asp:Button ID="cmdsaved" runat="server"  CssClass="btn btn-primary" Text="Save" OnClick="cmdsave_Click" />
                                
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnReset_Click" />
                                  <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="btnDelete_Click" />
                                   
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                            <asp:Label ID="Label14" runat="server" CssClass="showBoldExist" Text ="<%$ Resources:ValidationResources, CastCatExist %>"></asp:Label></td>
                        </tr>
                      
                    </table></div>
                             <div class="allgriddiv" id="dvgrd"  runat="server">                                  
                <asp:HiddenField runat="server" ID="hdnGrdId" />
         <asp:DataGrid ID="DataGrid1"  OnItemCommand="DataGrid1_ItemCommand" runat="server" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' >
                                    <Columns>
                                        <asp:ButtonColumn CommandName="Select" DataTextField="cat_name" HeaderText="<%$ Resources:ValidationResources, LCastcat %>"
                                             Text="<%$ Resources:ValidationResources, Grcatname %>">
                                         </asp:ButtonColumn>
                                        <asp:BoundColumn DataField="cat_id" HeaderText="<%$ Resources:ValidationResources, HCatId%>" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="shortname" HeaderText="<%$ Resources:ValidationResources, LShortName%>" ReadOnly="True" >
                                        </asp:BoundColumn>
                                    </Columns>
                                    
                                </asp:DataGrid></div>

      </ContentTemplate>
  </asp:UpdatePanel>            
         </div>
        
     <input id="hdUnableMsg"
                                runat="server" name="hdUnableMsg" 
                                type="hidden" />
                                <input id="hdTop" runat="server" name="hdTop"  type="hidden" />
                                <input id="txtdepartmentcode"
                    runat="server" name="Hidden4" size="1"  type="hidden" />
                                <input id="yCoordHolder" runat="server" name="Hidden5" type="hidden"  />
                    <input id="xCoordHolder" runat="server" name="Hidden5" 
                        type="hidden" />
    <input id="Hidden1" runat="server" name="Hidden1" size="1" type="hidden" />
    <input id="Hidden2" runat="server" name="Hidden2" size="1" type="hidden" />
    <input id="Hidden3" runat="server" name="Hidden3" size="1" type="hidden" />
    <input id="Hidden4" runat="server" name="Hidden4" size="1" type="hidden" />
    <asp:RequiredFieldValidator ID="depart" runat="server" ControlToValidate="txtStatus"
        Display="None" ErrorMessage="<%$ Resources:ValidationResources, ReqEnterCastCat%>" Font-Size="11px" SetFocusOnError="True"
       ></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtshortname"
        Display="None" EnableViewState="False" ErrorMessage="<%$ Resources:ValidationResources, ReqEnterShortN%> " Font-Size="11px"
        SetFocusOnError="True" ></asp:RequiredFieldValidator>
</div>
</asp:Content>