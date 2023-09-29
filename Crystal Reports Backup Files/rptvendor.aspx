<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="rptvendor.aspx.cs" Inherits="Library.rptvendor" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="rptVHead" runat="server" ContentPlaceHolderID="head">
        <script type="text/javascript">

            function pdfClick() {
                //  $("[id$='nreco']").click();
                setTimeout('delayt();', 6000);

                //}

                //function delayt() {

                ExportToPDF($('[id$=divshow]'), [], 'Vendor Report', PDFPageType.Portrait, 'VendorReport');
            }
            function GetVendJs(sender, arg) {
                let id = arg.get_value();
                $('[id$=hdVid]').val(id);
                $('[id$=btnSb]').click();
            }


        </script>
<style type="text/css" >
            .Publ
            {
                width:300px;
                max-height:250px;
                overflow:auto;
                margin:0;
                padding:0;
                font-size:13px;
                 color:black;
                border:2px solid black;
            }
        </style>
    <Link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >

</asp:Content>
<asp:Content ID="rptVMain" runat="server" ContentPlaceHolderID="MainContent">
       <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
        <div class="container tableborderst no-more-tables" >
            <div style="width: 100%; display: none" class="title">
                <div style="width: 89%; float: left">
                    <asp:Label ID="lblTitle" Style="display: none" runat="server" Width="100%">Vendor Detail Report</asp:Label>
                </div>
                <div style="float: right; vertical-align: top">
                    &nbsp;
                       <a id="lnkHelp" href="#" style="display: none" onclick="ShowHelp('Help/Acquisitioning-VendorReport.htm')">
                           <img alt="Help?" height="15" src="help.jpg" /></a>
                </div>

            </div>
            <p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="cmdshow2" />

                    </Triggers>
                    <ContentTemplate>

                        <table id="Table2" class="table-condensed tdmgr GenTable1">
                            <tr>
                                <td colspan="3">
                                     <asp:Label ID="msglabel" runat="server" CssClass="err">Label</asp:Label>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ValidationResources,LVenC %>"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtcodeVen" runat="server" BorderWidth="1"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtcodeVen"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="MssplSugg.asmx"
                                        ServiceMethod="GetVendorCode"
                                        OnClientItemSelected="GetVendJs">
                                    </ajax:AutoCompleteExtender>

                                     <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" />
                                    </td>
                                    <%--                                        <Custom:AutoSuggestBox ID="txtcodeVen" runat="server" AutoPostBack="false" BorderWidth="1px"
                                            Columns="30" CssClass="FormCtrl" DataType="City" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                                            Height="20px" MaxLength="100" ResourcesDir="asb_includes"></Custom:AutoSuggestBox></TD>--%>
                                <td>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td style="width:27%"></td>
                                <td>
                                    <asp:DropDownList ID="Dd1" runat="server" CssClass="txt10" Height="30" Width="28%"
                                        Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                                        >
                                        <asp:ListItem Text="<%$Resources:ValidationResources,LOr %>" Value="OR"></asp:ListItem>
                                        <asp:ListItem Text="<%$Resources:ValidationResources,AND %>" Value="AND"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top">
                                    <asp:Label ID="Label2" runat="server"
                                        Text="<%$ Resources:ValidationResources,LVenN %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnameVen" runat="server" BorderWidth="1"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="ExtVend" runat="server" TargetControlID="txtnameVen"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="MssplSugg.asmx"
                                        ServiceMethod="GetVendor"
                                        OnClientItemSelected="GetVendJs">
                                    </ajax:AutoCompleteExtender>
                                     <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/sugg.png" />
                                    <asp:HiddenField ID="hdVid" runat="server" />
                                    <asp:Button ID="btnSb" runat="server" OnClick="btnSb_Click" style="visibility:hidden"/>
                                    <%--                                       
    <Custom:AutoSuggestBox ID="txtnameVen" runat="server" AutoPostBack="false" 
                                            BorderWidth="1px" Columns="30" CssClass="FormCtrl" DataType="City" 
                                            Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" Height="20px" 
                                            MaxLength="100" ResourcesDir="asb_includes"></Custom:AutoSuggestBox>--%>
                              
                                    </td>
                                <td>
                                    
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3" style="text-align: center">

                                    <asp:Button ID="cmdshow2" runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,BPrintR %>"
                                         OnClick="cmdshow2_Click" />
                                    <input id="cmdshow" type="button" style="display:none" value="<%$Resources:ValidationResources,BPrintR %>"
                                        name="cmdshow" runat="server" class="btnstyle">
                                    <asp:Button ID="cmdReset2" runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,bReset %>"
                                         OnClick="cmdReset2_Click" />
                                    <input id="cmdReset" type="button" style="display:none" value="<%$Resources:ValidationResources,bReset %>"
                                        name="Button3" runat="server" class="btnstyle">


                                    <input id="nreco" runat="server" class="btnstyle" visible="false" name="ItextReport" type="button"
                                        value="Pdf-Print" />
                                </td>
                            </tr>
                        </table>

                        <div style="width: 100%; display: none">
                            <div id="divshow" runat="server">
                                <div style="width: 100%; float: left" id="hedd">
                                    <table class="GenTable1" style="width: 100%">
                                        <tr>
                                            <td style="font-size: 16px; width: 41%">Gram :
                                                <asp:Label runat="server" ID="grm"></asp:Label>

                                            </td>
                                            <td style="font-size: 16px; width: 29%">Fax:
                                                <asp:Label runat="server" ID="fx"></asp:Label>

                                            </td>
                                            <td style="font-size: 16px; width: 30%; text-align: right">PhoneNo:
                                                <asp:Label runat="server" ID="phn"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="font-size: 16px;">E-Mail :<asp:Label runat="server" ID="eml"></asp:Label>

                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>

                                        </tr>
                                        <tr>
                                            <td style="font-size: 21px;" colspan="3">
                                                <div style="text-align: center">
                                                    <asp:Label runat="server" ID="ins"></asp:Label>
                                                    <br />
                                                </div>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="font-size: 18px;" colspan="3">
                                                <div style="text-align: center">
                                                    <asp:Label runat="server" ID="libranm"></asp:Label>

                                                </div>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="font-size: 16px;" colspan="3">
                                                <div style="text-align: right">
                                                    <asp:Label runat="server" ID="addr"></asp:Label>
                                                    &nbsp
                                                    <asp:Label runat="server" ID="cit"></asp:Label>
                                                    &nbsp
                                                    <asp:Label runat="server" ID="pin"></asp:Label>
                                                    &nbsp
                                                    <asp:Label runat="server" ID="stat"></asp:Label>

                                                </div>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="font-size: 16px;" colspan="3">
                                                <div style="text-align: center"></div>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="font-size: 16px;">Date:<span style="color: green">
                                                <asp:Label runat="server" ID="curdt"></asp:Label>
                                            </span>

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
                                <asp:GridView runat="server" ID="grdrep" BorderStyle="None" Width="100%" AllowPaging="false" EnableTheming="false" HeaderStyle-BackColor="#336699" HeaderStyle-ForeColor="White" CellPadding="5" Font-Size="17px">
                                </asp:GridView>
                                <table>
                                    <tr>
                                        <td style="font-size: 17px; font-weight: bold; width: 30%">
                                            <span style="color: #0d4260">Total Records:</span>
                                            <asp:Label runat="server" ID="TotaCou"></asp:Label>

                                        </td>

                                    </tr>

                                </table>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </p>
                 </div>
    
						<%--<CR:CrystalReportViewer ID="Crvendor" runat="server" AutoDataBind="true" />--%>
<INPUT id="hdUnableMsg" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" runat="server">
                                <input id="Hidden1" runat="server" style="width: 23px" type="hidden" />
            


</asp:Content>
