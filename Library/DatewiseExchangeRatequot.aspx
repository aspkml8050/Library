<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="DatewiseExchangeRatequot.aspx.cs" Inherits="Library.DatewiseExchangeRatequot" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="ExrHead" runat="server" ContentPlaceHolderID="head">
    <script src="FormScripts/ExportToPDF.js" type="text/javascript"></script>
        <script type="text/javascript">

            function pdfClick() {
                ExportToPDF($('[id$=divshow]'), [], 'Currency wise Exchange Rates', PDFPageType.Portrait, 'CurrencyWiseExchangeRates');
                setTimeout('delayt();', 6000);
            }
        </script>

</asp:Content>
<asp:Content ID="ExrMain" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress D="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>

    </asp:UpdateProgress>
 <div class="container tableborderst">
      <div style="width:100%;display:none" class="title">
          <div style="width:89%;float:left" >
            <asp:label id="lblTitle" runat="server" style="display:none" Width="100%" ></asp:label>
          </div>

                            <div style="float:right;vertical-align:top"> &nbsp;
             <a id="lnkHelp" href="#" style="display:none"  onclick="ShowHelp('Help/Acquisitioning-datewiseItemstock.htm')"> <img alt="Help?" height="15" src="help.jpg" /></a>
                            </div>
      </div>
     <asp:Label ID="labMesg" runat="server" Font-Bold="true"></asp:Label>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <Triggers>
             <asp:PostBackTrigger ControlID="cmdSearch" />
         </Triggers>
         <ContentTemplate>
             <div class="no-more-tables" style="width:100%">
                 <table id="Table2" class="table-condensed GenTable1">
                     <tr style="display:none">
                         <td colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err"></asp:label>

                         </td>
                     </tr>
                     <tr>
                         <td><asp:label id="Label4" runat="server" Text="<%$ Resources:ValidationResources,LFromD %>"></asp:label></td>
                         <td><asp:TextBox ID="txtfromdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/>
                             <asp:label id="Label7" runat="server" Width="1px" CssClass="star">*</asp:label>
                         </td>
                         <td>
                             
                         </td>
                         <td><asp:label id="Label5" runat="server"  Text="<%$ Resources:ValidationResources,LToD %>"></asp:label></td>
                         <td><asp:TextBox ID="txttodate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                             <asp:label id="Label8" runat="server" Width="1px" CssClass="star">*</asp:label>
                         </td>

                     </tr>

                     <tr>
                         <td><asp:checkbox id="chkSelectAll" runat="server"  CssClass="opt"  Text="<%$ Resources:ValidationResources, CnkSelectA %>"
											AutoPostBack="True"></asp:checkbox>

                         </td>
                         <td colspan="3" style="text-align:center">
                             <INPUT id="cmdSearch" type="button" value="<%$ Resources:ValidationResources,bShwRpt %>"
														name="cmdSearch" style="display:none;" runat="server" class="btnstyle">

                             <asp:Button ID="cmdSearch1" runat="server" Text="Show Report" CssClass="btn btn-primary" onclick="cmdSearch1_Click" />

                                              
                                                    <input id="cmdreset" runat="server"  name="cmdreset" type="button" style="display:none;"
                                                        value="<%$Resources:ValidationResources,bReset %>" class="btnstyle" />

                              <asp:Button ID="cmdreset1" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset1_Click"/>

											 <input id="nreco" runat="server" class="btnstyle" name="rept" type="button" value="Pdf-Print"  Style="display:inline-block;display:none;"/>

                             <asp:Button ID="nreco1" runat="server" Text="Pdf-Print" CssClass="btn btn-primary" OnClick="nreco1_Click"/>

                         </td>
                     </tr>

                 </table>

                 </div>
              <div class="allgriddiv">
                  <asp:datagrid id="grdCurrency" runat="server" CssClass="allgrid GenTable1"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                      <Columns>
                          <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,GrSel %>">
													<HeaderStyle Width="3%"></HeaderStyle>

                              <ItemTemplate>
														&nbsp;
														<asp:CheckBox id="Chkselect" runat="server"></asp:CheckBox>
													</ItemTemplate>
        </asp:TemplateColumn>
                          <asp:BoundColumn Visible="False" DataField="CurrencyCode" HeaderText="<%$ Resources:ValidationResources,LCurCode %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="CurrencyName" HeaderText="<%$ Resources:ValidationResources,LCurrName %>">
													<HeaderStyle Width="7%"></HeaderStyle>													
												</asp:BoundColumn>

                      </Columns>
                      </asp:datagrid>
                  </div>
              <div style="width:100%;display:none" >
                   <div id="divshow" runat="server" > 
                    <div style="width:100%;float:left" id="hedd"  >
                        <table style="width:100%">
                            <tr>
                                <td style="font-size:10px;" colspan="3">
                                     <div  style="text-align:center;font-size:22px;padding-bottom:15px"><asp:Label runat="server" ID="lblvp"/></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size:14px;width:41%">
                                     Gram : <asp:Label runat="server" ID="grm"></asp:Label>
                                </td>
                                <td style="font-size:14px; width:29%">
                                     Fax: <asp:Label runat="server" ID="fx"></asp:Label>
                                </td>
                                <td style="font-size:14px; width:30%;text-align:right">
                                  PhoneNo: <asp:Label runat="server" ID="phn"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size:14px;">
              E-Mail :<asp:Label runat="server" ID="eml"></asp:Label> </td>
                                <td>&nbsp;</td>
     <td>&nbsp;</td> 
                            </tr>
                            <tr>
                                <td style="font-size:18px;" colspan="3">
         <div style="text-align:center">
             <asp:Label runat="server" ID="ins"></asp:Label> <br /> </div>

     </td>

                            </tr>
                            <tr>
                                <td style="font-size:15px;" colspan="3">
         <div  style="text-align:center">
             <asp:Label runat="server" ID="libranm"></asp:Label>

         </div>

     </td>

                            </tr>
                            <tr>
                                <td style="font-size:14px;" colspan="3">
     <div  style="text-align:right">
         <asp:Label runat="server" ID="addr"></asp:Label> &nbsp <asp:Label runat="server" ID="cit"></asp:Label> &nbsp <asp:Label runat="server" ID="pin"></asp:Label> &nbsp <asp:Label runat="server" ID="stat"></asp:Label>

     </div>

     </td>

                            </tr>
                            <tr>
                                <td> 
         Date:<span style="color:green"> <asp:Label runat="server" ID="curdt"></asp:Label> </span>

     </td>
     <td> 
         <%--(As on) :<span style="color:green"> <asp:Label runat="server" ID="curdt"></asp:Label> </span>--%>

       </td>
     <td>&nbsp</td> 

                            </tr>
                            <tr>
                                <td colspan="3">
                         <hr />
                     </td>
                            </tr>
                        </table>
                    </div>
                        <asp:GridView runat="server" ID="grdrep"   BorderStyle="None" Width="100%" AllowPaging="false" EnableTheming="false"  HeaderStyle-BackColor="#336699" HeaderStyle-ForeColor="White" >
           
          </asp:GridView>
                                            <br />
                   </div>


              </div>
         </ContentTemplate>
     </asp:UpdatePanel>


     </div>
    <input id="hdMes" runat="server" name="hdMes" style="width: 19px" type="hidden"/>
    <input id="hdCulture" runat="server" style="width: 18px" type="hidden"/>
    <input id="hrDate" runat="server" type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 19px"/>
     <INPUT id="yCoordHolder" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1" value="0" name="yCoordHolder" runat="server">
    <INPUT id="xCoordHolder" style="WIDTH: 8px; HEIGHT: 12px" type="hidden" size="1" value="0" name="xCoordHolder" runat="server">
    <INPUT id="Hidden1" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1" runat="server">

     <script type="text/javascript">
         //On Page Load.
         $(function () {
             SetDatePicker();
         });

         //On UpdatePanel Refresh.
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         if (prm != null) {
             prm.add_endRequest(function (sender, e) {
                 if (sender._postBackSettings.panelsToUpdate != null) {
                     SetDatePicker();
                 }
             });
         };
         function SetDatePicker() {

             $("[id$=txttodate],[id$=txtfromdate]").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 yearRange: '1999:2030',
                 dateFormat: 'dd-M-yy',
                 minDate: new Date(1999, 1 - 1, 1)
             });
         }

     </script>




</asp:Content>
