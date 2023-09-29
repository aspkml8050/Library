<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master"  CodeBehind="CategoryLoadingStatus.aspx.cs" Inherits="Library.CategoryLoadingStatus" %>



<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="CatHead" runat="server" ContentPlaceHolderID="head">
        
		<script type="text/javascript">
            function chk() {
                if (document.Form1.hdTop.value == "top") {
                    window.scrollTo(0, 0);
                    document.Form1.txtCLStatus.focus();
                    document.Form1.hdTop.value = 0;
                }
            }


        </script>
    
    <style>
        .btnstyle{
                background: #0486b5;
    border-radius: 8px;
    color: white;
    font-size: 15px;
    padding-left: 12px;
    padding-right: 12px;
    padding-top: 6px;
    padding-bottom: 6px;
    border-color: #e3e7f1;
        }
        #spSelTitle{
            font-size: 17px;
    font-weight: bold;
    color: #933f03;
    background-color: #ffdcbe;
    padding: 2px 7px;
    border: 1px solid;
    border-radius: 12px;
        }
    </style>

</asp:Content>
<asp:Content ID="CatBody" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
           <div class="container tableborderst">

			  <div style="width:100%;display:none;display:none" class="title">
                    <div style="width:89%;float:left;display:none;" > &nbsp;
		             <asp:label id="lblTitle" runat="server"  Width="100%" style="text-align:center" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:label>
                       </div>
                  <div style="float:right;vertical-align:top"> &nbsp;
                        <a id="lnkHelp" href="#" style="display:none" onclick="ShowHelp('Help/Masters-itemcategory.htm')"><img height="15" src="help.jpg" alt="Help" /></a>
                </div></div>
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
                                             <asp:Label ID="lblCLStatus" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,CatName %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                         <td>
                                             <input class="txt10" onkeypress="disallowSingleQuote(this);" id="txtCLStatus" onblur="this.className='blur'"
                                                 onfocus="this.className='focus'" type="text" name="txtServiceName" runat="server" maxlength="50">
                                             <asp:Label ID="Label8" runat="server" CssClass="star">*</asp:Label></td>

                                         <td style="width: 30%">
                                             <asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Abbr %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                         <td>
                                             <input class="txt10" onkeypress="disallowSingleQuote(this);" id="txtAbbreviation" onblur="this.className='blur'"
                                                 onfocus="this.className='focus'" type="text" name="txtAbbreviation" runat="server" maxlength="10">
                                             <asp:Label ID="Label2" runat="server" CssClass="star">*</asp:Label></td>
                                     </tr>
                                     <tr style="display: none">
                                         <td>
                                             <asp:Label ID="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,CatIcon %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                         <td>
                                             <asp:FileUpload ID="File1" Width="90%" runat="server" CssClass="txt10" onblur="this.className='blur';InputValidation(this,'File1');"
                                                 onfocus="this.className='focus'" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' /></td>
                                     </tr>
                                     <tr>
                                         <td></td>
                                         <td>
                                             <img id="image1" src="" alt="" runat="server" border="1" class="responsiveImage_150" style="display: none" />
                                             <input id="display" style="display: none" runat="server" name="display" type="button" value="<%$ Resources:ValidationResources,bDisplay %>" causesvalidation="false" class="btnstyle" />
                                         </td>
                                     </tr>

                                     <tr>

                                         <td colspan="4" style="text-align: center">
                                            <%-- <input id="cmdsave" type="button" value="<%$ Resources:ValidationResources,bSave %>" name="cmdsave"
                                                 runat="server" accesskey="S" class="btnstyle" />
                                             <input id="cmdreset" type="button" value="<%$ Resources:ValidationResources,bReset %>" name="cmdreset"
                                                 runat="server" accesskey="E" class="btnstyle">
                                             <input id="cmddelete" onclick="if (DoConfirmation() == false) return false;" type="button" value="<%$ Resources:ValidationResources,bDelete%>" name="cmddelete" runat="server" accesskey="X" class="btnstyle">--%>

                            <asp:Button id="cmdSave" class="btnstyle" runat="server" Text="Submit" OnClick="cmdSave_Click"/>
                                 <input id="Button1" type="button" style="display:none;" value="<%$ Resources:ValidationResources,bSave %>" name="cmdsave"
                                                 runat="server" accesskey="S" class="btnstyle" />          

                                             <asp:Button id="cmdreset" class="btnstyle"  runat="server" Text="Reset" OnClick="cmdreset_Click"/>
                                           

                                             <asp:Button ID="cmdDelete" runat="server" Class="btnstyle" text="Delete" OnClick="cmddelete_Click" />
                                              <input id="Button" onclick="if (DoConfirmation() == false) return false;" type="button" value="<%$ Resources:ValidationResources,bDelete%>" name="cmddelete" runat="server" style="display:none" accesskey="X" cssClass="btnstyle">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td colspan="4">
                                             <asp:Label ID="Label14" CssClass="showBoldExist" runat="server" Text="<%$ Resources:ValidationResources,CatExist %>" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                     </tr>
                                 </table>
                                 </div>
                                <div class="tdmgr">
                                <div class="allgriddiv " style="max-height:300px" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                <asp:datagrid id="grdCLStatus" runat="server" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' >
											
											<Columns>
												<asp:ButtonColumn DataTextField="Category_LoadingStatus" HeaderText="<%$ Resources:ValidationResources, LCat %>"
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
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCLStatus"
                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, ReqEnterCatN%>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAbbreviation"
                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, ReqEnterAbbr%>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        &nbsp;&nbsp;&nbsp;
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" />
                        &nbsp;&nbsp;
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
                      //try {
                      //    var grdId = $("[id$=hdnGrdId]").val();
                      //    //alert(grdId);
                      //    $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                      //    ThreeLevelSearch($('#'+ grdId), "[id$=dvgrd]");
                      //}
                      //catch (err) {
                      //}
                  }


              </script>                         

</asp:Content>