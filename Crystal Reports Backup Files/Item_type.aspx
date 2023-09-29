<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="Item_type.aspx.cs" Inherits="Library.Item_type" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
      <script type="text/javascript" >
      var imG;
      function PhotUpld(fu) {
          var fil = fu.files[0];
          var fNm = $(fu).val();
          if (valiFT(fNm) == false) {
              $(fu).val(null);
              return false;
          }
          //            $("[id$='hdMemImg']").val(fNm.split(".")[1]); //?????
          var fr = new FileReader();
          fr.onload = function (event) {
              imG = event.target.result;
              var imG2 = imG.substring(imG.indexOf(',') + 1);
              let pref = imG.substring(0, imG.indexOf(','));
              console.log(pref);
              $("[id$='hdMemImg']").val(imG2);
          }
          fr.readAsDataURL(fil);
          setTimeout("ShowImg();", 100);

          return true;
          //imgPhot fuPH

      }
      function ShowImg() {
          $("[id$='image1']").attr('src', imG);
      }
      function valiFT(fNam) {
          var _validFileExtensions = ["jpg", "jpeg", "bmp", "gif", "png"];
          var fNMx = fNam.split(".");
          var f = _validFileExtensions.indexOf(fNMx[1]);
          if (f < 0) {
              alert("Valid files are .jpg, .jpeg .bmp .gif .png");
              return false;
          }
      }


      function validateform() {
          let v1 = $('[id$=txtCLStatus]').val().trim();
          if (v1 == '') {
              alert('Item Type Name required.');
              return false;
          }
          let v2 = $('[id$=txtAbbreviation]').val().trim();
          if (v2 == '') {
              alert('Abbreivation Name required.');
              return false;

          }
          return true;
      }

      </script>
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
                   // ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
                }
                catch (err) {
                }
            }


        </script>       
					
  <style>
     .dataTables_scrollBody
        {
            height:auto!important;
            max-height:300px!important;
        }
</style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <asp:UpdateProgress ID="UpPorg1" runat="server">
<ProgressTemplate>
<NN:Mak ID="FF1" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
        <asp:UpdatePanel ID="upsdas" runat="server">
            <ContentTemplate>
                 
            <div class="container tableborderst">
   
       
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
			 <asp:label id="lblTitle" runat="server"  Width="100%" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:label>
                 </div>   
               <div style="float:right;vertical-align:top"> 
                 <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-itemcategory.htm')"><img height="15" src="help.jpg" alt="Help"/></a>
              </div>
             </div>
                               
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                             <Triggers>

<asp:PostBackTrigger ControlID="display" />

</Triggers>
                            <ContentTemplate>
                              <div class="no-more-tables" style="width:100%">
                                  <table id="Table1" class="table-condensed GenTable1">
                                      <tr>
                                          <td colspan="4">
                                              <asp:Label ID="msglabel" runat="server" CssClass="err" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblCLStatus" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,ItemCats %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                          <td>
                                              <input class="txt10" onkeypress="disallowSingleQuote(this);" id="txtCLStatus" onblur="this.className='blur'"
                                                  onfocus="this.className='focus'" type="text" name="txtServiceName" runat="server" maxlength="50">
                                              <asp:Label ID="Label8" runat="server" CssClass="star">*</asp:Label></td>
                                     
                                          <td>
                                              <asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Abbr %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                          <td>
                                              <input class="txt10" onkeypress="disallowSingleQuote(this);" id="txtAbbreviation" onblur="this.className='blur'"
                                                  onfocus="this.className='focus'" type="text" name="txtAbbreviation" runat="server" maxlength="10">
                                              <asp:Label ID="Label2" runat="server" CssClass="star">*</asp:Label></td>
                                      </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LItemIcon %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                          <td>
                                              <asp:FileUpload ID="File1" runat="server" onchange="return PhotUpld(this)" CssClass="txt10"
                                                  onblur="this.className='blur';"
                                                  onfocus="this.className='focus'"
                                                  text="<%$ Resources:ValidationResources, Browse %>" Width="90%" /></td>
                                     
                                         
                                          <td colspan="2">
                                              <img id="image1" src="~/images/AddImage.jpg" style="width: 40px; height: 40px" alt="Display" runat="server" border="1" class="responsiveImage_150" />
                                              <input id="display" runat="server" style="display: none" name="display" type="button" value="<%$ Resources:ValidationResources,bDisplay %>" causesvalidation="false" class="btnstyle" />
                                              <asp:HiddenField ID="hdMemImg" runat="server" />
                                              <%--<asp:Button runat="server" OnClick="display_ServerClick" ID ="clisk" style="display:none" />--%>
                                          </td>
                                      </tr>

                                      <tr>

                                          <td colspan="4" style="text-align: center">
                                              <asp:Button ID="cmdsave2" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:ValidationResources,bSave %>"
                                                   OnClick="cmdsave2_Click" />
                                              <asp:Button ID="cmdreset2" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:ValidationResources,bReset %>"
                                                   OnClick="cmdreset2_Click" />
                                              <asp:Button ID="cmddelete2" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:ValidationResources,bDelete%>"
                                                   OnClick="cmddelete2_Click" />
                                          </td>
                                      </tr>
                                      <tr>
                                          <td colspan="4">
                                              <asp:Label ID="Label14" runat="server" CssClass="showBoldExist" Text="<%$ Resources:ValidationResources,LItemExist %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                      </tr>


                                  </table></div>

                                <div>
                                  <div class="allgriddiv" style="max-height:300px; " id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                <asp:datagrid id="grdCLStatus" runat="server" OnItemCommand="grdCLStatus_ItemCommand" Width="100%" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' >
											
											<Columns>
												<asp:ButtonColumn DataTextField="Item_Type" HeaderText="<%$ Resources:ValidationResources, ItemCats %>"
													CommandName="Select" >
                                                    
                                                </asp:ButtonColumn>
												<asp:BoundColumn Visible="False" DataField="Id" HeaderText="<%$ Resources:ValidationResources, HCtgLdgStId %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Abbreviation" HeaderText="<%$ Resources:ValidationResources, Abbr %>" ></asp:BoundColumn>
											</Columns>
											</asp:datagrid>
                </div>
                                    </div>

                                 <input id="hd_name" runat="server" style="width: 24px" type="hidden" /><input id="hd_id" runat="server" style="width: 16px; height: 16px" type="hidden" /><input id="hd_short" runat="server" style="width: 24px" type="hidden" /><input id="Hidden5" style="width: 27px" type="hidden" runat="server" /><INPUT id="xCoordHolder" type="hidden" name="Hidden5" runat="server" style="width: 27px"><INPUT id="hdTop" type="hidden" name="hdTop" runat="server" style="width: 34px"><INPUT id="hdUnableMsg" style="WIDTH: 36px; HEIGHT: 22px" type="hidden" size="1" name="hdUnableMsg"
							runat="server"><INPUT id="txtCLStatusCode" style="WIDTH: 34px; HEIGHT: 22px" type="hidden"
							name="Hidden4" runat="server"><input id="Hidden6" runat="server" style="width: 39px" type="hidden" />
                        <input id="hdabbreviation" runat="server" name="hdabbreviation" style="width: 15px"
                            type="hidden" /><INPUT id="Hidden1" style="WIDTH: 13px; HEIGHT: 22px"
				type="hidden" name="Hidden1" runat="server"><INPUT id="Hidden2" style="WIDTH: 27px; HEIGHT: 22px"
				type="hidden" name="Hidden2" runat="server"><INPUT id="Hidden3" style="WIDTH: 16px; HEIGHT: 22px"
				type="hidden" name="Hidden3" runat="server"><INPUT id="Hidden4" style="WIDTH: 28px; HEIGHT: 22px"
				type="hidden" name="Hidden4" runat="server"><INPUT id="yCoordHolder" style="width: 33px;"
				type="hidden" name="Hidden5" runat="server">
                       
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" />
                            </ContentTemplate>
                        </asp:UpdatePanel></div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
