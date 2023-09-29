<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="PreCatalogUserWiseRegister1.aspx.cs" Inherits="Library.PreCatalogUserWiseRegister1" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>



<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">

    <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css">
    <script src="FormScripts/multiselect.js"></script>

</asp:Content>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Button ID="btnPB" runat="server" Style="display: none" />
    <div class="container tableborderst">
        <div style="width: 100%; display: none" class="title">
            <div style="width: 89%; float: left">
                <asp:Label ID="lblTitle" runat="server" Width="100%"></asp:Label>
            </div>
            <div style="float: right; vertical-align: top">
                <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Acquisitioning-PreCatalogueItemAccessionRegister.htm')">
                    <img alt="Help?" height="15" src="help.jpg" /></a>
            </div>

        </div>

              <asp:UpdatePanel ID="UpdatePanel" runat="server">
                  <ContentTemplate>
                             <div class="no-more-tables">
                            <table id="Table2" class="table-condensed" >
                                <tr>
                                    <td colspan="4" >
                                        <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                </tr>

                                <tr style="display: none;">
                                    <td>
                                        <asp:Label ID="bblpg" runat="server" Text="<% $ Resources:ValidationResources,LPageSize %>"></asp:Label>
                                    </td>
                                    <td colspan="3" >
                                        <asp:RadioButton ID="a3" runat="server" Checked="True" CssClass="opt"
                                            GroupName="a" Text="Page(A4)"/>
                                        <asp:RadioButton ID="a4" runat="server" CssClass="opt" GroupName="a"
                                            Text="Page(A3)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" >
                                        <asp:CheckBoxList ID="chkreport" runat="server" AutoPostBack="true"
                                            ForeColor="#CC3300"  OnSelectedIndexChanged="chkreport_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" >
                                            <asp:ListItem Selected="True" Text='<%$Resources : ValidationResources,uncatelogueditems%>'></asp:ListItem>
                                            <asp:ListItem Selected="True" Text='<%$Resources : ValidationResources,catalogueditems%>'></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text='<%$Resources : ValidationResources,accessioningreport%>' ></asp:Label>
                                    </td>
                                    <td colspan="3" >
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Book" Value="Book"></asp:ListItem>
                                            <%--<asp:ListItem Text="Journal" Value="Journal"></asp:ListItem>--%>
                                            <asp:ListItem Text="Thesis" Value="Thesis"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="4" ><hr /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Accession Prefix"></asp:Label></td>
                                    <td>
                                        <input id="txtAcc_Prefix" runat="server" type="text" class="txt10"  /></td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Accession Suffix"></asp:Label></td>
                                    <td>
                                        <input id="txtAcc_Suffix" runat="server" type="text" class="txt10" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="From Accession No"></asp:Label></td>
                                    <td>
                                        <input id="txtFAcNo" runat="server" type="text" class="txt10"/></td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="To Accession No"></asp:Label></td>
                                    <td>
                                        <input id="txtTAcNo" runat="server" type="text" class="txt10" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddldttype" runat ="server"  ><asp:ListItem Selected ="True" Text ="Accessioned" Value ="0" ></asp:ListItem><asp:ListItem Text ="Cataloged" Value ="1"></asp:ListItem></asp:DropDownList>
                                        </td>
                                    <td style="                                            width: 30%
                                    "></td>
                                    <td style="width:20%"></td>
                                    <td style="width:30%"></td>
                                       </tr>
                                <tr> <td>  <asp:Label ID="Label4" runat="server" Text="<%$Resources:ValidationResources,LFromD %>"> </asp:Label>
                                    </td>
                                    <td colspan="2">


<%--pushpendra singh--%>
 <asp:TextBox ID="txtfromdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>

                                     <%--   <input class="txt10" id="txtfromdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
                                            onfocus="this.className='focus'" type="text" size="10" name="txtindentdate" runat="server"><input id="btnDate" accesskey="D" onclick="pickDate('txtfromdate');" style="background-position: center center; background-image: url(cal.gif); width: 20px; height: 20px; background-color: black"
                                                type="button" />--%>




                                        <%--<asp:label id="Label7" runat="server" Width="1px" CssClass="star">*</asp:label>&nbsp;&nbsp;--%>
                                    </td>
                                    <td ></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label5" runat="server" Text="<%$ Resources:ValidationResources,LtoD %>"></asp:Label>
                                    </td>
                                    <td colspan="2">

                                        
<%--pushpendra singh--%>
 <asp:TextBox ID="txttodate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>

<%--                                        <input class="txt10" id="txttodate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
                                            onfocus="this.className='focus'" type="text" size="10" name="txtindentdate" runat="server"><input id="Button1" accesskey="D" onclick="pickDate('txttodate');" style="background-position: center center; background-image: url(cal.gif); width: 20px; height: 20px; background-color: black"
                                                type="button" />--%>



                                        <%-- <asp:label id="Label8" runat="server" Width="1px" CssClass="star">*</asp:label>--%>
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="txt10" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>">
                                            <asp:ListItem Value="AND" Text="<%$ Resources:ValidationResources,AND %>"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="<%$ Resources:ValidationResources,LOr %>"></asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>

                                <tr>
                                    <td ><asp:Label ID="Label6" runat="server" Text="<%$ Resources:ValidationResources,LbDepartment %>" ></asp:Label></td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbdept" runat="server" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                                            onblur="this.className='blur'" onfocus="this.className='focus'" CssClass="txt10" >
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="Label54" runat="server" BackColor="Yellow"
                                            Text="Special Filters&lt;&lt;"></asp:Label>
                                    </td>
                                    <td colspan="2" >
                                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="txt10"
                                            Font-Names="<%$ Resources:ValidationResources, TextBox1 %>">
                                            <asp:ListItem Text="<%$ Resources:ValidationResources, AND %>" Value="AND"></asp:ListItem>
                                            <asp:ListItem Text="<%$ Resources:ValidationResources, LOr %>" Value="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td><asp:CheckBox ID="chkWritOff" runat="server" Text="Include Written-Off Books" /> </td>
                                </tr>
                                     <tr>
                                    <td><asp:Label ID="Label10" runat="server" Text="Bill No From" ></asp:Label></td>
                                    <td><asp:TextBox ID="txtBnoF" runat="server" CssClass="txt10"></asp:TextBox></td>
                                    <td><asp:Label ID="lblBDt" runat="server" Text="Bill No Upto" ></asp:Label>      </td>
                                    <td><asp:TextBox ID="txtBnoU" runat="server" CssClass="txt10"></asp:TextBox>
                                        
                                    </td>
                                </tr>

                                <tr>
                                     
                                                <td>
                                                    <asp:DropDownList ID="ddl1" runat="server" AutoPostBack="True" CssClass="txt10"
                                                        Font-Names="<%$Resources:ValidationResources,TextBox1 %>" 
                                                        OnSelectedIndexChanged="ddl1_SelectedIndexChanged" >
                                                        <asp:ListItem Text="---Select---"></asp:ListItem>
                                                        <asp:ListItem Text="<%$ Resources:ValidationResources, LTitle %>" Value="Title"></asp:ListItem>
                                                        <%--<asp:ListItem Text="<%$ Resources:ValidationResources, AccNo %>" Value="AccNo"></asp:ListItem>--%>
                                                         <asp:ListItem Text="Accession No." Value="AccNo"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td colspan="2">
                                                    <asp:TextBox ID="txttitle" runat="server" class="txt10"
                                                        onblur="this.className='blur'" onfocus="this.className='focus'" size="55"
                                                        Style="<%$ Resources: ValidationResources, TextBox2 %>" type="text" />
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/sugg.png" />
                                                  <%--  <Custom:AutoSuggestMenu ID="asmTxtSearch1" runat="server" KeyPressDelay="10"
                                                        MaxSuggestChars="100" OnClientTextBoxUpdate="onTextBoxUpdate2"
                                                        OnGetSuggestions="SuggService.GetSuggestSearch" ResourcesDir="~/asm_includes"
                                                        TargetControlID="txttitle" UpdateTextBoxOnUpDown="True"
                                                        UsePageMethods="false" />--%>
                                                </td>
                                    <td >
                                            
                                    </td>
                                </tr>
                                <tr>
                                     
                                                <td><asp:Label ID="lblAcc" runat="server" Text="Accession No."></asp:Label></td>
                                                
                                                <td colspan="3"><asp:TextBox ID="txtAcc" runat="server" TextMode="MultiLine" CssClass="txt10" ></asp:TextBox></td>
                                            
                                </tr>
                                <tr>
                                    <td colspan="4"><hr /></td>
                                </tr>
                                <tr>
                                    <td colspan="4" >
                                        <asp:RadioButton ID="optPreAccNo" runat="server" Checked="True"  GroupName="b" OnCheckedChanged="optPreAccNo_CheckedChanged"
                                            Text="<%$ Resources:ValidationResources,AccNoWise %>" 
                                            AutoPostBack="True" ForeColor="#FF9900" /></td>
                                     
                                </tr>
                                <tr>
                                    <td colspan="4" >
                                        <asp:RadioButton ID="optPreDept" runat="server"  GroupName="b"  OnCheckedChanged="optPreDept_CheckedChanged"
                                            Text="<%$ Resources:ValidationResources,DeptWis %>" 
                                            AutoPostBack="True" ForeColor="#FF9900" /></td>
                                     
                                </tr>
                                <tr>
                                    <td colspan="4" >
                                        <asp:RadioButton ID="optprePubwise" runat="server"  GroupName="b" OnCheckedChanged="optprePubwise_CheckedChanged"
                                            Text="<%$ Resources:ValidationResources,Pubwise %>" 
                                            AutoPostBack="True" ForeColor="#FF9900" /></td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:RadioButton ID="OptpreUserWise" runat="server" AutoPostBack="True"  OnCheckedChanged="OptpreUserWise_CheckedChanged"
                                            GroupName="b" Text="<%$ Resources:ValidationResources,UserWise %>"
                                           ForeColor="#FF9900" /></td>
                                     
                                </tr>
                                <tr>
                                    <td colspan="4" >
                                        <asp:RadioButton ID="RBBill" runat="server" AutoPostBack="True" 
                                            GroupName="b" Text="Bill No"
                                           ForeColor="#FF9900" /></td>
                                     
                                </tr>
                                <tr>
                                    <td colspan="4" ><hr /></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:ValidationResources,LUserName %>" ></asp:Label></td>
                                    <td colspan="3" >
                                        <input id="txtIndentSearch" cssclass="txt10" runat="server"  type="text" class="txt10" />
                                        <input id="btnCategoryFilter" runat="server" name="btnCategoryFilter"
                                           type="submit" class="btnstyle"  value="<%$Resources:ValidationResources,bSearch %>"
                                            /></td>
                                </tr>
                                <tr>
                                    <td colspan="4"> 
									<asp:CheckBox ID="chkSelectAll" runat="server"  CssClass="opt" AutoPostBack="True"
                                        Text="<%$Resources:ValidationResources,CnkSelectA %>" ></asp:CheckBox></td>
                                     
                                </tr>
                               </table>

                             </div> (1)
                      <div class="allgriddivmid">
          <asp:DataGrid ID="grdUsers" CssClass="allgrid" runat="server" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                <Columns>
                    <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrSel %>">
                        <ItemTemplate>
                            &nbsp;
												<asp:CheckBox ID="Chkselect" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="UserId" HeaderText="<%$ Resources:ValidationResources, LUserName %>"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
    </div> (2)
        <table class="table-condensed no-more-tables">
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label58" runat="server" Text='Select Font Size for Print'></asp:Label><br />
            </td>
        <td style="width:40%"> <asp:DropDownList ID="ddlsize" runat="server" >
                <asp:ListItem Text="7"></asp:ListItem>
                <asp:ListItem Text="8"></asp:ListItem>
                <asp:ListItem Selected="True" Text="10"></asp:ListItem>
                <asp:ListItem Text="11"></asp:ListItem>
                <asp:ListItem Text="12"></asp:ListItem>
                <asp:ListItem Text="13"></asp:ListItem>
                <asp:ListItem Text="14"></asp:ListItem>
                <asp:ListItem Text="15"></asp:ListItem>
                <asp:ListItem Text="16"></asp:ListItem>
                <asp:ListItem Text="17"></asp:ListItem>
                <asp:ListItem Text="18"></asp:ListItem>
                <asp:ListItem Text="19"></asp:ListItem>
                <asp:ListItem Text="20"></asp:ListItem>
                <asp:ListItem Text="21"></asp:ListItem>
                <asp:ListItem Text="22"></asp:ListItem>
                <asp:ListItem Text="23"></asp:ListItem>
                <asp:ListItem Text="24"></asp:ListItem>
                <asp:ListItem Text="25"></asp:ListItem>
                <asp:ListItem Text="26"></asp:ListItem>
                <asp:ListItem Text="27"></asp:ListItem>
                <asp:ListItem Text="28"></asp:ListItem>
                <asp:ListItem Text="29"></asp:ListItem>
                <asp:ListItem Text="30"></asp:ListItem>
                <asp:ListItem Text="31"></asp:ListItem>
                <asp:ListItem Text="32"></asp:ListItem>
                <asp:ListItem Text="33"></asp:ListItem>
                <asp:ListItem Text="34"></asp:ListItem>
                <asp:ListItem Text="35"></asp:ListItem>
                <asp:ListItem Text="36"></asp:ListItem>
                <asp:ListItem Text="37"></asp:ListItem>
                <asp:ListItem Text="38"></asp:ListItem>
                <asp:ListItem Text="39"></asp:ListItem>
                <asp:ListItem Text="40"></asp:ListItem>
                <asp:ListItem Text="41"></asp:ListItem>
                <asp:ListItem Text="42"></asp:ListItem>
                <asp:ListItem Text="43"></asp:ListItem>
                <asp:ListItem Text="44"></asp:ListItem>
                <asp:ListItem Text="45"></asp:ListItem>
                <asp:ListItem Text="46"></asp:ListItem>
                <asp:ListItem Text="47"></asp:ListItem>
                <asp:ListItem Text="48"></asp:ListItem>
                <asp:ListItem Text="49"></asp:ListItem>
                <asp:ListItem Text="50"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td colspan="2" >
            <asp:ImageButton ID="ImageButton2" runat="server" BorderStyle="Groove" style="vertical-align:bottom" OnClick="ImageButton2_Click"
                 ImageUrl="images/1_1.gif" SkinID="Click Explore Field Width Setting"  />
            <asp:Label ID="wlbl" runat="server" Text="Explore Column Width"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
  </table>(3)
    <asp:Panel ID="pnl" runat="server">
   
       <div class="allgriddivmid">
                <asp:DataGrid ID="grdfield" runat="server"
                    Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                    HorizontalAlign="Left" CssClass="allgrid">
                    <Columns>
                        <asp:BoundColumn DataField="ColNameHeader" HeaderText="Column/Field Name"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Width">
                            <ItemTemplate>
                                <asp:TextBox ID="txtwidth" runat="server"
                                    
                                    ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
           </div>
           <table class="table-condensed">
        <tr>
            <td style="text-align:center">
                <asp:Button ID="cmdupdatewidth" runat="server" CssClass="btn btn-primary" OnClick="cmdupdatewidth_Click" Text='<%$Resources : ValidationResources,bUpdate%>'  />
            </td>
        </tr>
    </table>
</asp:Panel> (4)
                        <table  class="no-more-tables table-condensed">
     
    <tr>
        <td colspan="4" >
            <table id="tt1" runat="server" class="table-condensed" style="border:0" visible="true" bgcolor="#ffffff">
                <tr>
                    <td >
                        <asp:ListBox ID="ListBox2" runat="server" Rows="15" SelectionMode="Multiple" style="height:140px!important"
                           ></asp:ListBox></td>
                    <td>
                        <asp:Button ID="btnaddall" runat="server" CssClass="btnstyle" OnClick="btnaddall_Click"
                            Text=">>"  UseSubmitBehavior="False" /><br />
                        <asp:Button ID="btnadditem" runat="server" CssClass="btnstyle" OnClick="btnadditem_Click"
                            Text=">"  UseSubmitBehavior="False" /><br />
                        <asp:Button ID="btnremoveitem" runat="server" CssClass="btnstyle" OnClick="btnremoveitem_Click"
                            Text="<" UseSubmitBehavior="False" /><br />
                        <asp:Button ID="btnremoveall" runat="server" CssClass="btnstyle" OnClick="btnremoveall_Click"
                            Text="<<" UseSubmitBehavior="False" /></td>
                    <td >
                        <asp:ListBox ID="ListBox1" runat="server" Rows="15" SelectionMode="Multiple" style="height:140px!important"
                            ></asp:ListBox></td>
                    <td>
                        <asp:Button ID="btnUp" CssClass="btnstyle" runat="server"  OnClick="btnUp_Click" Text='<%$Resources : ValidationResources,sym%>'  />
                        <asp:Button ID="btnDn" CssClass="btnstyle" runat="server" OnClick="btnDn_Click" Text='<%$Resources : ValidationResources,symb%>'  /></td>
                </tr>
            </table>
            <div id="divP" >
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:CheckBox ID="chk_List" runat="server" CssClass="opt"
                Text="<%$Resources:ValidationResources,chkRmbField %>" />
        </td>
    </tr>
     
    <tr>
        <td colspan="4" style="text-align:center">
            <asp:Button ID="cmdShow"  CssClass="btn btn-primary"  Text="<%$Resources:ValidationResources,BPrintR %>" AccessKey="X" ToolTip="Press Alt+X" OnClick="cmdShow_Click" runat="server" />
            <asp:Button ID="btnExportExcel" CssClass="btn btn-primary" runat="server" Text="Export to Excel" style="width:auto"/>
            <input id="cmdreset" runat="server" name="cmdreset" type="submit" value="<%$Resources:ValidationResources,bReset %>"  class="btnstyle" accesskey="R" title="Press Alt+R" />
            <asp:Button ID="cmdreset2" runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,bReset %>" OnClick="cmdreset2_Click" />
            <asp:HiddenField id="hidbase64" runat="server" />
            <div style="display:none;"><asp:Button ID="btnExport" runat="server" /></div>
        </td>
        
    </tr>

    
</table> (5)
                       <input id="cmdPrint" type="submit" value="button" runat="server" style="visibility: hidden" />

 <div id="divgvSearchContent" runat="server" class="no-more-tables" style="display:none; overflow: auto;">                                
     <table class="table-condensed" >
         <tr>
             <td >
                 <asp:Label ID="lblGram" Font-Bold="true" runat="server"></asp:Label>
             </td>
             <td colspan="2">
                 <asp:Label ID="lblinstName" Font-Bold="true" runat="server"></asp:Label>
             </td>
             <td >
                 <asp:Label ID="lblPhone" Font-Bold="true" runat="server"></asp:Label>
             </td>
         </tr>
         <tr>
             <td>
                 <asp:Label ID="lblemail" Font-Bold="true" runat="server"></asp:Label>
             </td>
             <td ></td>
             <td ></td>
             <td >
                 <asp:Label ID="lblFax" Font-Bold="true" runat="server"></asp:Label>
             </td>
         </tr>
        
     </table>        
     <div class="allgriddiv">
     <asp:GridView ID="gvSearchContent" runat="server" CssClass="allgrid" AllowPaging="false" SkinID="none">                                    
     </asp:GridView></div>
     <table class="table-condensed no-more-tables">
         
         <tr>
             <td colspan="2">
                 <asp:Label ID="lbltAmount" Font-Bold="true" runat="server"></asp:Label></td>
              
         </tr>
         <tr>
             <td ><asp:Label ID="lblTrecords" Font-Bold="true" runat="server"></asp:Label></td>
              
             <td ><asp:Label ID="lblTrecordsTxt" Font-Bold="true" runat="server"></asp:Label></td>
              
         </tr>
     </table>
 </div>
                  </ContentTemplate>
                  <Triggers>
                      <asp:PostBackTrigger ControlID="cmdShow" />
                  </Triggers>
            </asp:UpdatePanel>

    </div> 
<script type="text/javascript">
    //On Page Load.
    $(function () {
        SetDatePicker();
//        SetListBox();
    });

    //On UpdatePanel Refresh.
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                SetDatePicker();
                SetListBox();
            }
        });
    };
    function SetDatePicker() {

        $("[id$=txtfromdate],[id$=txttodate]").datepicker({
            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });
    }

    function SetListBox() {

        $('[id*=cmbdept],[id*=cmbCourse1]').multiselect({
            enableCaseInsensitiveFiltering: true,
            buttonWidth: '95%',
            includeSelectAllOption: true,
            maxHeight: 200,
            width: 315,
            enableFiltering: true,
            filterPlaceholder: 'Search'

        });

    }
</script>	

    </asp:Content>
