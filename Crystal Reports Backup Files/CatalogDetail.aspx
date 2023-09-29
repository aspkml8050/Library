<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="CatalogDetail.aspx.cs" Inherits="Library.CatalogDetail1" %>


<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <script src="LibUtils.js"></script>
		<script  type="text/javascript">
            $(document).ready(function () {
                $("input[id$='btnPopu']").click(function () {
                    var Grid = $("table[id$='grdcopy'] ")
                    var Locc = $("[id$='txtlocation']").val();
                    var Locc2 = $("[id$='txtLoc2']").val();
                    var Editionyear = $("[id$='txteditionyear']").val();
                    var Publication = $("[id$='txtPubYear']").val();
                    $("[id$='txtCpLoc']").val(Locc + ',' + Locc2);  //txtlocation txtLoc2
                    $("[id$='hdnCpLocId']").val(Locc2);  //txtlocation txtLoc2 - not used
                    $("[id$='txtEdYear']").val(Editionyear);
                    $("[id$='txtPubYear']").val(Publication);
                    //alert(Grid.t);
                });
            });
            function PrepareMARC_21() {//debugger;			       
                return true;
                var ctrl_no = document.getElementById('<%=hdCtrlNo.ClientID%>').value;
                var IsMarc21 = document.getElementById('<%=hdIsMarc21.ClientID%>').value;
                var confirm = document.getElementById('<%=hdConfirm.ClientID%>').value;
                if (ctrl_no != '' && IsMarc21 == 'Y' && confirm != 'N') {
                    //CallMarcWebMethod('ctrl_no:'+ctrl_no);
                }
                else { return false; }
            }
            //+++++++++++++++++++Binds function "starttime" on page load+++++++++++++++++++++


        </script>
		<script type="text/javascript" >
            function TLocSel(source, eventArgs) {
                var lloc = eventArgs.get_value()
                var lloc2 = lloc.split(",");
                //            alert(lloc2[1]);

                document.getElementById("<%=txtLoc2.ClientID%>").value = lloc2[1];
                //            alert(lloc);
                //           alert(document.getElementById("txtLoc2").value);

            }
            function callUValkeywords() {
                var str;

                //convert to window.open see code
//                str = window.showModalDialog("Journal_Addkeyword.aspx?id=" + document.getElementById("<%=hdCtrlNo.ClientID%>").value + "&txtsTitle=" + document.getElementById("<%=txtacc%>").value, "User Validation", "dialogHeight:380px;dialogWidth:660px,dialogHide:true;help:no;status:no;");
                str = window.open("Journal_Addkeyword.aspx?id=" + document.getElementById("<%=hdCtrlNo.ClientID%>").value + "&txtsTitle=" + document.getElementById("<%=txtacc%>").value, "User Validation", "height=380px,width:760px");

            }

            function ConfirmbeforeSave() {
                if (window.confirm("Items already existing under same call No! \n Do you want to continue?") == true) {
                    //                    document.Form1.hdConfirm.value = "Y";
                    $('[id$=hdConfirm]').val('Y');
                    //                    document.Form1.cmdsave.click();
                    $('[id$=cmdsave]').click();
                    return true;
                }
                else {
                    //                    document.Form1.hdConfirm.value = "N";
                    $('[id$=hdConfirm]').val('N');
                    //document.Form1.hdIsMarc21.value = "N"
                    $('[id$=hdIsMarc21]').val('N');
                    //                    document.Form1.txtclassno.focus();

                    return false;
                }
            }

            function showattach(lab) {
                $(lab).next().css('display', 'block');
            }
            function closit(elem) {
                $(elem).parent().css('display', 'none');
            }

            function PublSel(sender, arg) {
                $('[id$=hdPublisherId]').val(arg.get_value());
            }

        </script>
		  <style type="text/css">
            .TLocation {
              width:300px !important;
              font-size:13px;
              overflow:scroll;
              height:400px !important;
              margin:0px;
              background-color:red;
              padding:1px;
              border:1px solid green;
    }
		      .locSt
		      {     
                  height: 300px !important; display:block;
                    font-family:Arial;
                    padding:1px;
                  overflow:scroll !important;
                  margin:0px;
                  font-size:.8em;
		      }
		      .locIt
		      {
                 cursor:pointer;
                 background-color:white;
		      }
		          .locIt2
		          {
                 background-color:wheat;
		          }
            .style1
            {
                height: 23px;
            }
            .head
            {
                height: 19px;
            }
          
        
            .style1
            {
                height: 156px;
            }
            .stylebox
            {
             background-color:White  ;
            display:block ;
             color:Black ;
             azimuth:inherit ;
             padding:10px;
              /*width:100px ;*/
              text-decoration:none;
               background-repeat:repeat-x ;
               font-style:normal ;
               font-weight:normal;
           
            }
             .stylebox1
            {
        
             background-color:White ;
             display:block ;
             color:Black ;
             azimuth:inherit ;
               font-weight:normal;

           
            }
            .stylebox:Hover
            {
              cursor:pointer;
   
            }
             .stylebox1:Hover
            {
            	 background:#0099CC;
   
            }
 
       
 
        </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdMast" runat="server" />
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />

    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
       <ContentTemplate>
                <asp:HiddenField ID="hdCtrlNo" runat="server" />
                <asp:HiddenField ID="hdIsMarc21" runat="server" />
                <asp:HiddenField ID="hdConfirm" runat="server" />
                <asp:TextBox ID="txtacc" style="display:none" runat="server" ></asp:TextBox>
             <asp:HiddenField ID="hdnInsUpd" runat="server" />
             <asp:HiddenField ID="hdBookNumAccn" runat="server" />
<%--                            This field is not emptied - stores whether booknumber to be stored in accessionmaster and so on--%>
            <asp:HiddenField ID="hdFirstLoad" runat="server" />
           <div class="container tableborderst" style="width:80%;margin-top:20px">  
               <div style="width:100%;display:none" class="title">
    <div style="width:92%;float:left">
        <asp:label id="lblTitleCatalogue" runat="server"  Text="<%$ Resources:ValidationResources, Title_Catdet %>"></asp:label></div>
				  <div style="float:right;vertical-align:top">
                <asp:label id="msglabel" runat="server"  CssClass="err" Height="8px"></asp:label>
                </div>

</div>
               								<TABLE id="Table10" class="no-more-tables GenTable1" style="width:100%"  >
                                  <tr><td colspan="4">
                                      <table class="GenTable1" style="width:100%"><tr><td>  
                                            <asp:Label ID="LabelA" runat="server" 
                                                Text="<%$ Resources:ValidationResources, LblTtlAccsnd %>" ></asp:Label>
                                            </td><td>
                                                <asp:Label ID="lblAccession" runat="server" BackColor="Transparent" 
                                                    BorderColor="Black" BorderWidth="1px" Font-Bold="True" ForeColor="Red" 
                                                  ></asp:Label>
                                            </td><td>
                                                <asp:Label ID="lbltot1" runat="server" 
                                                    Text="<%$ Resources:ValidationResources, LblTtlCat %>"></asp:Label>
                                            </td><td>
                                                <asp:Label ID="lbltotentry" runat="server" BackColor="Transparent" 
                                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                    ForeColor="Red" ></asp:Label>
                                            </td><td>
                                                <asp:Label ID="lbltot0" runat="server" 
                                                    Text="<%$ Resources:ValidationResources, Uncatalogued_book %>" ></asp:Label>
                                            </td><td>
                                                <asp:Label ID="lbltotentryun" runat="server" BackColor="Transparent" 
                                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                    ForeColor="Red"  Width="112px"></asp:Label>
                                            </td></tr>  </table> </td> </tr>
                                    <tr>
                                        <td>
                                           <asp:Label ID="Label1" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, RecTp %>"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="cmbtype" runat="server" CssClass="txt10" Height="30" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" AutoPostBack="true" 
                                                  onblur="this.className='blur'" onfocus="this.className='focus'"
                                               >
                                                <asp:ListItem Value="Articles"></asp:ListItem>
                                                <asp:ListItem Value="Books"></asp:ListItem>
                                                <asp:ListItem Value="Project Reports"></asp:ListItem>
                                                <asp:ListItem Value="Thesis"></asp:ListItem>
                                                                                         <asp:ListItem Value="Non Print"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                           <asp:Label ID="Label6jk" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, lblstatus %>"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="cmbStatus" AutoPostBack ="true"    runat="server" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                                                Height="30" >
                                                <asp:ListItem Value="D"></asp:ListItem>
                                                <asp:ListItem Value="Rs"></asp:ListItem>
                                                <asp:ListItem Value="Rf"></asp:ListItem>
                                                <asp:ListItem Value="Sh"></asp:ListItem>
                                                <asp:ListItem Value="TB"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:label id="Label4" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources, LBCatlg %>"><u>D</u>ate </asp:label></td>
                                        <td>
                                        
                                            <%--pushpendra singh--%>
 <asp:TextBox ID="txtdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>

                                           <%-- 
                                            
                                             
                                            <INPUT id="txtdate"   onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" onfocus="this.className='focus'"
												type="text" name="txtdate" runat="server" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; height: 20px; width: 115px;"><input id="btnDate" type="submit" onclick="pickDate('txtdate');" runat="server" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 27px; background-color: black" />
                                            
                                            --%>
                                            
                                            
                                            
                                            <asp:label id="Label131" runat="server"  CssClass="star">*</asp:label></td>
                                        <td>
                                           <asp:Label ID="Labelb" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LBaDa %>"></asp:Label></td>
                                        <td >
                                            <input id="txtrelease" runat="server" name="txtdate" onblur="this.className='blur';"
                                                onfocus="this.className='focus'" 
                                                type="text" />
                                            <input id="Button1" runat="server" visible="false" accesskey="D" onclick="pickDate('txtrelease');"
                                                style="background-position: center center; background-image: url(cal.gif); width: 25px;
                                                height: 21px; background-color: black" type="button" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:Label ID="LabelN" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LbDocNo %>"></asp:Label></td>
                                        <td>
                                            <input id="txtDocNo" runat="server" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" style="border-right: black 1px solid; border-top: black 1px solid;
                                                border-left: black 1px solid; border-bottom: black 1px solid;"
                                                type="text" /></td>
                                        <td>
                                           <asp:Label ID="Label9N" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LDocDt %>"></asp:Label></td>
                                        <td >




                                            <%--pushpendra singh--%>
 <asp:TextBox ID="txtDocDate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDocDate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>



                                           <%-- <input id="txtDocDate" runat="server" name="txtdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
                                                onfocus="this.className='focus'" style="border-right: black 1px solid; border-top: black 1px solid;
                                                border-left: black 1px solid; width: 113px; border-bottom: black 1px solid; height: 20px"
                                                type="text" /><input id="btnDate1" runat="server" accesskey="D" onclick="pickDate('txtDocDate');"
                                                style="background-position: center center; background-image: url(cal.gif); width: 25px;
                                                height: 21px; background-color: black" type="button" />--%>





                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                          <asp:label id="Label5" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources, AccNo %>"></asp:label></td>
                                        <td >
                                            <INPUT id="Text1" readOnly type="text" size="14" name="txtclassno" runat="server" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; ">
                                            <asp:Label ID="Label8" runat="server" CssClass="star" >*</asp:Label>
                                            </td>
                                        <td >
                                           <asp:Label ID="Label26" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, CpyNo %>"></asp:Label></td>
                                        <td  >
                        <INPUT onkeypress="IntegerNumber(this)" id="txtCopyNo" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid;  BORDER-BOTTOM: black 1px solid;"
														onfocus="this.className='focus'" type="text" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                          <asp:label id="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, ClasNo %>"></asp:label></td>
                                        <td>
                                            <INPUT id="txtclassno" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;"
														onfocus="this.className='focus'" type="text" runat="server">
                                            </td>
                                        <td>
                                            <asp:label id="Label2" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LBBookNo %>"></asp:label></td>
                                        <td >
                                            <INPUT id="txtbookno" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;"
														onfocus="this.className='focus'" type="text" runat="server">
                                             <asp:Label ID="labBookN" tooltip="Based on feature setting this value can be stored with individual Accession copy " runat="server" Text="Note*" ></asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           <asp:Label ID="Label39" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LBVolPart %>"></asp:Label></td>
                                        <td>
                                            <INPUT onkeypress="IntegerNumber(this)" id="txtVolumeNo" onblur="this.className='blur'"
														style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;"
														onfocus="this.className='focus'" type="text" runat="server">
                                            <INPUT onkeypress="IntegerNumber(this)" id="txtPart" onblur="this.className='blur'"
														style="BORDER-RIGHT: black 1px solid;margin-top:3px; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;"
														onfocus="this.className='focus'" type="text" runat="server" name="txtPart"></td>
                                        <td>
                                           <asp:label id="Label127" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LEdition %>"></asp:label></td>
                                        <td >
                                            <INPUT id="txtedition" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;"
														onfocus="this.className='focus'" type="text" runat="server" size="10"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:label id="Label129" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LPubY %>"></asp:label></td>
                                        <td>
                                            <INPUT id="txtPubYear" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; "
														onkeypress="IntegerNumber(this);" onfocus="this.className='focus'" type="text" runat="server" maxlength="4" size="8">
                                            </td>
                                        <td>
                                                <asp:Label ID="Label128" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LEditionY %>"></asp:Label>
                                            </td>
                                        <td>
                                                <input id="txteditionyear" runat="server" maxlength="4" onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="IntegerNumber(this);"  style="border: 1px solid black; margin-left: 0px;" type="text"/>
                                        </td>
                                        </tr>
                                    <tr>
                                        <td>
                                          <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                                           </td>
                                        <td >
                                            <asp:TextBox ID="txtlocation" runat="server" BorderColor="Black" ></asp:TextBox>
                                            <ajax:AutoCompleteExtender ID="ExtLocation" runat="server" TargetControlID="txtLocation"
                                                 MinimumPrefixLength="0"
                                               CompletionInterval="10"
                                               CompletionSetCount="50"
                                               FirstRowSelected="true"
                                               CompletionListCssClass="locSt"  
                                               OnClientItemSelected="TLocSel"
                                               ServicePath="MssplSugg.asmx"
                                                  EnableCaching="true" 
                                               ServiceMethod="GetLocation2" >
                                              </ajax:AutoCompleteExtender>
                                           </td>
                                        <td colspan="2">
                                            <table style="width:100%">
                                                <tr>
                                                    <td>
                                                        <input id="btnPopu" type="button" class="btnstyle" style="width:150px" value="Apply To Copies" />
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtLoc2" runat="server" style=" visibility:hidden;" Text="" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                             
                                             
</td>
                                    </tr>
                                    <tr>
                                        <td >
                                         <asp:Label ID="lblCourse1" runat="server" Text="<%$ Resources:ValidationResources,RBCourse %>"></asp:Label></td>
                                        <td >
                                            <asp:DropDownList ID="cmbCourse1" runat="server" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'" AutoPostBack="true" 
                                               >
                                            </asp:DropDownList></td>
                                        <td >
                                            <input id="cmdPrint" runat="server" name="cmdPrint" 
                                                type="submit" value="Show Card" class="btnstyle" visible="false" /></td>
                                        <td >
                                            <input id="cmdKeywords" runat="server" accesskey="Y" class="btnstyle" 
                                                name="cmdKeywords"  type="submit" style="width:150px"
                                                value="<%$ Resources:ValidationResources, AddKeyWd %>"/></input>
                                            <div style="display:none;position:relative;" id="dvattachments" runat="server"  >
                                                <asp:Label ID="lablHasFiles" onmouseover="showattach(this)" runat="server">Attachments*</asp:Label>
                                                <div id="dvattachlists" runat="server" style="position:absolute;display:none; padding:4px 4px; border:1px solid grey; background-color:white;">
                                                    <span style="font-weight: bold;cursor:pointer;" onclick="closit(this)">X</span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
								</TABLE>
						<div style="width:100%;overflow:auto">
                            <table class="no-more-tables GenTable1" >
                            <tr>
                          <td>  <asp:Button ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" class="stylebox"  Text="General Info."   Font-Bold="True" BorderStyle="None" ToolTip="<%$ Resources:ValidationResources,Lblgeneralinformation%>" BackColor="#0099CC"  ForeColor="White"></asp:Button>

                          </td>
                             <td> 
                                 <asp:Button ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" class="stylebox"  Text="Statement of Resp."   Font-Bold="True" BorderStyle="None" ToolTip="Statement of Responsibility" BackColor="#0099CC"  ForeColor="White"></asp:Button>

                             </td>                
                                 <td>  
                                     <asp:Button ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" class="stylebox"  Text="Physical Desc."   Font-Bold="True" BorderStyle="None" ToolTip="Physical Description" BackColor="#0099CC"  ForeColor="White"></asp:Button>

                                 </td>
                                 <td>  
                                     <asp:Button ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" class="stylebox"  Text="Series Desc."   Font-Bold="True" BorderStyle="None" ToolTip="Series Description" BackColor="#0099CC"  ForeColor="White"></asp:Button></td> 
                                 <td > 
                                     <asp:Button ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" class="stylebox"  Text="Notes Section"   Font-Bold="True" BorderStyle="None" ToolTip="Notes Section" BackColor="#0099CC"  ForeColor="White"></asp:Button></td>
                                 
                         
                                    <td> 
                                        <asp:Button ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" class="stylebox"  Text="Standard No.(s)&Subject(s)"   Font-Bold="True" BorderStyle="None" ToolTip="Standard No.(s)&amp; Subject(s)" BackColor="#0099CC"  ForeColor="White"></asp:Button>

                                    </td>
                                 <td> 
                                     <asp:Button ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" class="stylebox"  Text="Copies Info."   Font-Bold="True" BorderStyle="None" ToolTip="Copies Information" BackColor="#0099CC"  ForeColor="White"></asp:Button>

                                 </td>
                                 <td> 
                                     <asp:Button ID="LinkButton8" runat="server" OnClick="LinkButton8_Click" class="stylebox"  Text="Thesis Related Info."   Font-Bold="True" BorderStyle="None" ToolTip="Thesis Related Information" BackColor="#0099CC"  ForeColor="White"></asp:Button>

                                 </td>
                               </tr>
                            </table>
                           </div>  

            grdpanel
               <asp:Panel ID="grdPanel" runat="server" style="width:100%" ScrollBars="Both">
									<asp:Panel ID="panel1" style="width:100%" runat="server" >
                                        <table id="Table11" cellspacing="0" style="width:100%"class="no-more-tables GenTable1" cellpadding="0" style="text-align:center" >
                                            
                                                <%--<TD style="HEIGHT: 8px" style="text-align:center" colSpan="4"></TD>--%>
                                               
                                            <tr>
                                                <td style="text-align:center" colspan="5">
                                                     <div style="width:80%;display:none" class="title">
                                                    <asp:Label ID="Label60" runat="server" Text="<%$ Resources:ValidationResources,LBGenlDescpt%>"></asp:Label>
                                                         </div>


                                                </td>
                                                <%-- <td style="text-align:center" colspan="1" style="height: 8px">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <%--<TD style="HEIGHT: 8px" colSpan="4"></TD>--%>
                                                <td colspan="5"></td>
                                            </tr>
                                            <tr>
                                                <td>

                                                    <asp:Label ID="lbl37" runat="server" Text="<%$ Resources:ValidationResources, LnbspOrgCurrency %>"></asp:Label>
                                                </td>
                                                <td >
                                                    <asp:DropDownList ID="cmbcurr" runat="server" CssClass="txt10" Height="30" AutoPostBack="true" OnSelectedIndexChanged="cmbcurr_SelectedIndexChanged"
                                                        Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" 
                                                        onblur="this.className='blur'" onfocus="this.className='focus'">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl35" runat="server" Text="<%$ Resources:ValidationResources,LBOrigPrice%>"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <input id="txtForeignPrice" runat="server" class="txt10" maxlength="7"
                                                        onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="decimalNumber(this);"
                                                        size="9" style="<%$ resources: ValidationResources, TextBox2 %>"
                                                        type="text" />
                                                </td>
                                                <%--<td style="width: 147px; height: 3px">
                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label82" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LPrc%>"></asp:Label>

                                                </td>
                                                <td>
                                                    <input id="txtbookprice" onkeypress="decimalNumber(this);" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" runat="server"><asp:Label ID="Label1028" runat="server" CssClass="star">*</asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="Label190" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSPrice%>"></asp:Label>

                                                </td>
                                                <td colspan="2">
                                                    <input id="txtSpecialPrice" onkeypress="decimalNumber(this);" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" runat="server"></td>
                                                <%--<td style="width: 147px; height: 3px">
                    </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="Hyperlink1" runat="server" Text="<%$ Resources:ValidationResources,LCat %>" onclick="openNewForm('btnFillPub','CategoryLoadingStatus','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>

                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="cmbbookcategory" runat="server" Height="30" class="txt10" AutoPostBack="true" OnSelectedIndexChanged="cmbbookcategory_SelectedIndexChanged"
                                                        onblur="this.className='blur'" onfocus="this.className='focus'" Style="font-size: 12px;">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="Label1008" runat="server" CssClass="star">*</asp:Label>

                                                </td>
                                                <td colspan="1">
                                                    

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,rptDepartment%>"></asp:Label>

                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="cmbdept" runat="server" CssClass="text10" Height="30" AutoPostBack="true" OnSelectedIndexChanged="cmbdept_SelectedIndexChanged"
                                                         onblur="this.className='blur'" onfocus="this.className='focus'">
                                                    </asp:DropDownList>
                                                      <asp:Label ID="Label59" runat="server" CssClass="star">*</asp:Label>

                                                </td>
                                                <td colspan="1">
                                                  

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, rptTitle %>"></asp:Label>



                                                </td>
                                                <td colspan="3">
                                                    <input class="text1" id="txttitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" size="61" runat="server">
                                                     <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/sugg.png" Width="16px" />
                                                </td>
                                                <td colspan="1">
                                                   

                                                </td>
                                            </tr>
                                            <tr>


                                                <td>
                                                    <asp:Label ID="Label61" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LSTitle %>"></asp:Label>



                                                </td>
                                                <td colspan="4">
                                                    <input class="text1" id="txtSubtitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" size="61" runat="server"></td>
                                                <%--<td colspan="1" style="width: 149px; height: 15px">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label63" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>">Parallel Title</asp:Label>



                                                </td>
                                                <td colspan="4">
                                                    <input class="text1" id="txtParallelTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" size="61" runat="server"></td>
                                                <%--<td colspan="1" style="width: 149px; height: 15px">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1jai" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBUnifTtl %>"></asp:Label>

                                                </td>
                                                <td colspan="4">
                                                    <input class="text1" id="txtUniformTitle" onblur="this.className='blur'" style="                                                            border-right: black 1px solid;
                                                            border-top: black 1px solid;
                                                            border-left: black 1px solid;
                                                            border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" size="61" runat="server"></td>
                                                <%--<td colspan="1" style="width: 149px; height: 15px">
                        </td>--%>
                                            </tr>
                                            <tr>
                                                <td>


                                                    <%-- 
                        <asp:Label ID="Label7" runat="server" CssClass="span" Width="86px" ToolTip="Press Alt+L to add New Language" Text ="<%$ Resources:ValidationResources, LnbspLanguage %>"></asp:Label>
                        
                                                    --%>
                                                    <asp:HyperLink ID="HypLang" Text="<%$ Resources:ValidationResources,LLanguage %>" runat="server" onclick="openNewForm('btnFillPub','TranslationLanguages','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>

                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmbLanguage" runat="server" CssClass="txt10" Height="30" onblur="this.className='blur'"
                                                        onfocus="this.className='focus'">
                                                    </asp:DropDownList>

                                                    <asp:Label ID="Label10" runat="server" CssClass="star">*</asp:Label>
                                                </td>
                                                <td>
                                                    
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label130" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LMediaType %>"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbMediaType" runat="server" CssClass="txt10" Height="30" onblur="this.className='blur'"
                                                        onfocus="this.className='focus'">
                                                    </asp:DropDownList>


                                                    <asp:Label ID="Label9oip" runat="server" CssClass="star">*</asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="Label38" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Lform %>"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbboundind" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                        runat="server" CssClass="text1">
                                                        <asp:ListItem Value="s" Text="<%$ Resources:ValidationResources,LISftBound %>"></asp:ListItem>
                                                        <asp:ListItem Value="h" Text="<%$ Resources:ValidationResources,LIHrdBound %>"></asp:ListItem>
                                                        <asp:ListItem Value="pbk" Text="<%$ Resources:ValidationResources,LIPBK %>"></asp:ListItem>
                                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <%-- asp:label id="Label65" runat="server" CssClass="span" ToolTip="Press Alt+P to add New Publisher" Text ="<%$ Resources:ValidationResources, LnbspPublisher %>"></asp:label> --%>

                                                    <asp:HyperLink ID="Hyperlink2" runat="server" Text="<%$ Resources:ValidationResources,rptpublisher %>" onclick="openNewForm('btnFillPub','PublisherMaster','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>

                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtCmbPublisher" runat="server" CssClass="txt10"
                                                        Columns="30"></asp:TextBox>
                                                  <ajax:AutoCompleteExtender ID="ExtPubl" runat="server" TargetControlID="txtCmbPublisher"
          MinimumPrefixLength="0"
          CompletionInterval="50"
          CompletionSetCount="50"
          FirstRowSelected="true" 
          CompletionListCssClass="locSt"
          ServicePath="MssplSugg.asmx"
         OnClientItemSelected="PublSel"
          EnableCaching="true" 
          ServiceMethod="GetPubl" >
     </ajax:AutoCompleteExtender>
                                <input id="hdPublisherId" runat="server"  type="hidden" />


<%--                                                    <Custom:AutoSuggestMenu ID="asmpublisher" runat="server" KeyPressDelay="1" MaxSuggestChars="100"
                                                        OnGetSuggestions="TopSearchService.GetSuggestionsPublisher" ResourcesDir="~/asm_includes"
                                                        TargetControlID="txtCmbPublisher" UpdateTextBoxOnUpDown="false" UsePageMethods="false"></Custom:AutoSuggestMenu>--%>

 <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" Width="16px" />*
                                                </td>
                                                <td >
                                                    <asp:Label ID="Label1009" runat="server" CssClass="star">
                                                       
                                                    </asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td >
                                                    <%--asp:label id="Label_9" runat="server" CssClass="span" ToolTip="Press Alt+V to add New Vendor" Text ="<%$ Resources:ValidationResources, LnbspVendor %>"></asp:label> --%>

                                                    <asp:HyperLink ID="Hyperlink3" runat="server" Text="<%$ Resources:ValidationResources,LVen %>" onclick="openNewForm('btnFillPub','VendorMaster','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtCmbVendor" runat="server" CssClass="txt10" BorderWidth="1px"
                                                        Columns="30"></asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCmbVendor"
          MinimumPrefixLength="0"
          CompletionInterval="50"
          CompletionSetCount="50"
          FirstRowSelected="true" 
          CompletionListCssClass="locSt"
          ServicePath="MssplSugg.asmx"
          EnableCaching="true" 
          ServiceMethod="GetVendor" >
     </ajax:AutoCompleteExtender>

<%--                                                    <Custom:AutoSuggestMenu ID="asmvendor" runat="server" KeyPressDelay="1" MaxSuggestChars="100"
                                                        OnGetSuggestions="TopSearchService.GetSuggestionsVendor" ResourcesDir="~/asm_includes"
                                                        TargetControlID="txtCmbVendor" UpdateTextBoxOnUpDown="false" UsePageMethods="false"></Custom:AutoSuggestMenu>--%>
                                                    <%--<asp:DropDownList ID="cmbvendor" runat="server" CssClass="txt10" onblur="this.className='blur'"
                            onfocus="this.className='focus'" Width="398px"></asp:DropDownList>--%>

                                                     <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/sugg.png" Width="16px" />
                                                </td>
                                                <td>
                                                   
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1876" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBCverPgImg%>"></asp:Label>

                                                </td>
                                                <td colspan="4" >
                                                    <table style="width: 100%" class="no-more-tables GenTable1">
                                                        <tr>
                                                            <td>
                                                                <asp:FileUpload ID="Fileupload" runat="server" onfocus="this.className='focus'" onblur="this.className='blur';InputValidation(this,'Fileupload');" CssClass="txt10" style="width:100%"/>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <img id="image1" height="78" width="99" runat="server" border="1" />
                                                                 <input id="upload" class="btnstyle" runat="server" type="submit" value="Upload" causesvalidation="False" />
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </td>


                                            </tr>
                                            <tr>
                                                <td colspan="5" style="text-align:center">
                                                                <asp:Button ID="cmdsave1_2" runat="server" CssClass="btn btn-primary"
                                                                     Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdSave7_2_Click" />
                                       
                                                                <input id="cmdsave1" onclick="PrepareMARC_21();" class="btnstyle" type="submit" value="Submit"
                                                                    runat="server"><input id="cmdReset1" class="btnstyle" type="submit" style="display:none;" value="Reset"
                                                                        runat="server">

                                                    <asp:Button ID="cmdReset1_1" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="cmdReset1_1_Click" />

                                                    <asp:Button ID="cmdReturn1_2" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="cmdReturn1_2_Click" />
                                                                <input id="cmdReturn1" class="btnstyle" type="submit" value="Back" style="display  :none"
                                                                    runat="server">

                                                         

                                                </td>
                                            </tr>
                                         

                                        </table>						
                            </asp:Panel>
    								<asp:Panel id="panel2"  style="width:100%" runat="server" >
                                       <table id="Table6" style="width:100%" class="no-more-tables GenTable1"   >
                                        
                                          
                                           <tr>
                                               <td style="text-align:center" colspan="4">
                                                   <div style="width:80%;display:none" class="title">
                                                   <asp:Label ID="Label70" runat="server"  Text="<%$ Resources:ValidationResources,LBStmntResA %>"></asp:Label>
                                                       </div>

                                               </td>
                                           </tr>
                                          
                                           <tr>
                                               <td  >
                                                   <asp:Label ID="Label55" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>

                                                   <asp:DropDownList ID="cboAuthorETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                       runat="server"  CssClass="txt10">
                                                       <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                                                       <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                                                   </asp:DropDownList>



                                               </td>
                                               <td >
												<asp:Label ID="Label110" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Auth %>"></asp:Label>
              </td>
                                               <td ></td>
                                               <td ></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:right"></td>
                                               <td >
                                                   <asp:Label ID="Label45" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label>

                                               </td>
                                               <td >
                                                   <asp:Label ID="Label46" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label>

                                               </td>
                                               <td >
                                                   <asp:Label ID="Label47" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label>

                                               </td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label111" runat="server" CssClass="span">1.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="txtau1firstnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="21" runat="server"></td>
                                               <td >
                                                   <input class="text1" id="txtau1midnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="20" runat="server"></td>
                                               <td ><input class="text1" id="txtau1surnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="16" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label112" runat="server" CssClass="span">2.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="txtau2firstnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td>
                                                   <input class="text1" id="txtau2midnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="20" runat="server"></td>
                                               <td ><input class="text1" id="txtau2surnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="16" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label113" runat="server" CssClass="span">3.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="txtau3firstnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td >
                                                   <input class="text1" id="txtau3midnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="17" runat="server"></td>
                                               <td ><input class="text1" id="txtau3surnm" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="16" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td>
                                                   <asp:Label ID="Label62" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>




                                                   <asp:DropDownList ID="cboEditorETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                       runat="server"  CssClass="txt10">
                                                       <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                                                       <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                                                   </asp:DropDownList>



                                               </td>
                                               <td ><asp:Label ID="Label43" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEditor %>"></asp:Label>



                                               </td>
                                               <td ></td>
                                               <td ></td>
                                           </tr>
                                          
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label44" runat="server" CssClass="span">1.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="editor1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="21" runat="server"></td>
                                               <td >
                                                   <input class="text1" id="editor1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="editor1Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label48" runat="server" CssClass="span">2.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="editor2fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td style="text-align:center">
                                                   <input class="text1" id="editor2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td   ><input class="text1" id="editor2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label49" runat="server" CssClass="span">3.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="editor3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td >
                                                   <input class="text1" id="editor3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="editor3Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="17" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td >
                                                   <asp:Label ID="Label42" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>




                                                   <asp:DropDownList ID="cboCompilerETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                       runat="server" CssClass="txt10">
                                                       <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                                                       <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                                                   </asp:DropDownList>



                                               </td>
                                               <td ><asp:Label ID="Label50" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LComp %>"></asp:Label>



                                               </td>
                                               <td ></td>
                                               <td ></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label21" runat="server" CssClass="span">1.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="compiler1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td>
                                                   <input class="text1" id="compiler1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="compiler1Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label22" runat="server" CssClass="span">2.</asp:Label>
                         </td>
                                               <td >
                                                   <input class="text1" id="compiler2Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td >
                                                   <input class="text1" id="compiler2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="compiler2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center">
                                                   <asp:Label ID="Label27" runat="server" CssClass="span">3.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="compiler3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="compiler3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="compiler3Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td>
                                                   <asp:Label ID="Label72" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
 <asp:DropDownList ID="cboILLustratorETAL" onblur="this.className='blur'" onfocus="this.className='focus'" Height="30"
                                                       runat="server"  CssClass="txt10">
                                                       <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                                                       <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                                                   </asp:DropDownList>



                                               </td>
                                               <td ><asp:Label ID="Label51" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LIllus%>"></asp:Label>



                                               </td>
                                               <td  ></td>
                                               <td ></td>
                                           </tr>
                                           <tr>
                                               <td  style="text-align:center">
                                                   <asp:Label ID="Label66" runat="server" CssClass="span">1.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="Illustrator1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="Illustrator1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="Illustrator1Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td  style="text-align:center">
                                                   <asp:Label ID="Label67" runat="server" CssClass="span">2.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="Illustrator2Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="Illustrator2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="Illustrator2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td  style="text-align:center">
                                                   <asp:Label ID="Label68" runat="server" CssClass="span">3.</asp:Label>
</td>
                                               <td >
                                                   <input class="text1" id="Illustrator3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="Illustrator3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="Illustrator3lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td>
                                                   <asp:Label ID="Label73" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal%>"></asp:Label>




                                                   <asp:DropDownList ID="cboTranslatorETAL" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                       runat="server"  CssClass="txt10">
                                                       <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN%>"></asp:ListItem>
                                                       <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY%>"></asp:ListItem>
                                                   </asp:DropDownList>



                                               </td>
                                               <td ><asp:Label ID="Label35" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LITransltr%>"> </asp:Label>



                                               </td>
                                               <td  ></td>
                                               <td ></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center" >
                                                   <asp:Label ID="Label69" runat="server" CssClass="span">1.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="Translator1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="Translator1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="Translator1Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center" >
                                                   <asp:Label ID="Label29" runat="server" CssClass="span">2.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="Translator2Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="Translator2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="Translator2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td style="text-align:center" >
                                                   <asp:Label ID="Label37" runat="server" CssClass="span">3.</asp:Label>



                                               </td>
                                               <td >
                                                   <input class="text1" id="Translator3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td  >
                                                   <input class="text1" id="Translator3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                       onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                               <td ><input class="text1" id="Translator3Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                   onfocus="this.className='focus'" type="text" size="26" runat="server"></td>
                                           </tr>
                                           <tr>
                                               <td  colspan="4">
												<asp:Label ID="Label64" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LBCorpAutM%>"></asp:Label>

                                               </td>
                                           </tr>
                                           <tr>
                                               <td  ></td>
                                               <td  colspan="3">
                                                   <textarea id="txtConferenceName" runat="server" onfocus="this.className='focus'" onblur="this.className='blur'" class="txt10" ></textarea>

                                               </td>
                                           </tr>
                                           <tr>
                                               <td  colspan="4">
												<asp:Label ID="Label126" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LPlcAndDt%>"></asp:Label>

                                               </td>
                                              
                                           </tr>
                                           <tr>
                                               <td  ></td>
                                               <td   colspan="3">
                                                   <input class="text1" id="txtConferenceYear" onblur="this.className='blur'"
                                                       style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                       onfocus="this.className='focus'" type="text" runat="server"></td>
                                              
                                           </tr>
                                         
                                          
                                           <tr>
                                               <td style="text-align:center" colspan="4">
                                                
                                                  <asp:Button ID="cmdsave4_2" runat="server" CssClass="btn btn-primary"
                                                                     Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdSave7_2_Click" />

                                                               <input id="cmdsave4" style="display:none" onclick="PrepareMARC_21();" class="btnstyle"  type="submit" value="Submit"
                                                                   runat="server">
                                                               <input id="cmdReset4" style="display:none"  type="submit" class="btnstyle" value="Reset"
                                                                   runat="server">
                                                  <asp:Button ID="cmdReset4_2" runat="server" CssClass="btn btn-primary"
                                                                     Text="Reset" OnClick="cmdReset4_2_Click"/>

                                               
                                                  <asp:Button ID="cmdReturn4_2" runat="server" CssClass="btn btn-primary"
                                                                     Text="Back" OnClick="cmdReturn4_2_Click"/>
                                                               <input id="cmdReturn4" class="btnstyle" type="submit" style="display:none" value="Back"
                                                                   runat="server">

                                             
                                                      
                                               </td>
                                           </tr>
                                       
                                       </table>
						</asp:Panel>
                   				<asp:Panel ID="panel3" style="width:100%" runat="server">
        <table id="Table3" style="width:100%;" class="no-more-tables GenTable1"  >
           
            <tr>
                <td  style="text-align:center" colspan="4">
                    <div style="width:80%;display:none" class="title">
                    <asp:Label ID="Label843" runat="server"  Text="<%$ Resources:ValidationResources,LPhyDescCAr%>"></asp:Label>
                        </div>


                </td>
            </tr>
            
            <tr>
                <td >
											<asp:Label ID="Label9rt" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LInPage%>"></asp:Label>

                </td>
                <td >
                    <input id="txtinitpages" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                        onfocus="this.className='focus'" type="text" runat="server"></td>
                <td>
                    <asp:Label ID="Label123412" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LTotlPage%>"></asp:Label>

                </td>
                <td >
                    <input id="txtpages" onkeypress="IntegerNumber(this)" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
            </tr>
            <tr>
                <td >
											<asp:Label ID="Label33" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LVolPage%>"></asp:Label>

                </td>
                <td  >
                    <input id="txtvolpages" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                        onfocus="this.className='focus'" type="text" runat="server"></td>
                <td >
                    <asp:Label ID="Label32" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBibliPages%>"></asp:Label>

                </td>
                <td >
                    <input id="txtbiblpages" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
            </tr>
            <tr>
                <td>
											<asp:Label ID="Label30" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBkSize%>"></asp:Label>

                </td>
                <td  >
                    <input id="txtbooksize" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
                <td >
                    <asp:Label ID="Labelt338" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVols%>"></asp:Label>

                </td>
                <td >
                    <input onkeypress="IntegerNumber(this)" id="txtvolno" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
            </tr>
            <tr>
                <td >
											<asp:Label ID="Label13e34" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,Prts%>"></asp:Label>

                </td>
                <td  >
                    <input id="txtparts" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
                <td >
                    <asp:Label ID="Label56" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LMaps%>"></asp:Label>

                </td>
                <td >
                    <input id="txtmaps" onkeypress="IntegerNumber(this)" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="Label40" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LILLust%>"></asp:Label>

                </td>
                <td >
                    <asp:DropDownList ID="cboIllistration" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
                        runat="server" CssClass="txt10" >
                        <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN%>"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources, LIY%>"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td >
                    <asp:Label ID="Label36" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBkIndx%>"></asp:Label>

                </td>
                <td >
                    <asp:DropDownList ID="cboBookIndex" onblur="this.className='blur'" onfocus="this.className='focus'"
                        runat="server" CssClass="txt10" Height="30">
                        <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN%>"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources, LIY%>"></asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td >
											<asp:Label ID="Label41" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LVarPagings%>"></asp:Label>

                </td>
                <td  >
                    <asp:DropDownList ID="cbovariouspaging" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
                        runat="server" CssClass="txt10">
                        <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources, LIN%>"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources, LIY%>"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td >
                    <asp:Label ID="Label34" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources, LFldLeaves%>"></asp:Label>

                </td>
                <td>
                    <input id="txtleaves" onkeypress="IntegerNumber(this)" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                        onfocus="this.className='focus'" type="text" runat="server"></td>
            </tr>
           
            <tr>
                <td>
											<asp:Label ID="Label81" runat="server"  CssClass="span" Text="Accompany Material Information"></asp:Label>

                </td>
                <td colspan="3">
                    <textarea class="txt10" id="txtmaterialinfo" onblur="this.className='blur'" 
                        onfocus="this.className='focus'" rows="5" cols="70" runat="server"></textarea>
                    </td>
            </tr>
         
           
            
           
            
            <tr>
                <td style="text-align:center" colspan="5">
                               <asp:Button ID="cmdsave3_2" runat="server" CssClass="btn btn-primary"
                                      Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdSave7_2_Click" />

                                <input id="cmdsave3" style="display:none" onclick="PrepareMARC_21();" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave%>"
                                    runat="server">
                               <asp:Button ID="cmdReset3_2" runat="server" CssClass="btn btn-primary"
                                      Text="<%$Resources:ValidationResources,bReset %>" OnClick="cmdReset3_2_Click" />
                                <input id="cmdReset3" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset%>"
                                    runat="server">

                         
                               <asp:Button ID="cmdReturn3_2" runat="server" CssClass="btn btn-primary"
                                      Text="Back" OnClick="cmdReturn3_2_Click" />
                    
                                <input id="cmdReturn3" class="btnstyle" type="submit" value="Back" style="display:none"
                                    runat="server">

                     
                </td>
            </tr>
        </table>
</asp:Panel>
                   <asp:Panel ID="panel4" runat="server">
    <table id="Tabl" style="width:100%"class="no-more-tables GenTable1"  >
       
        <tr>
            <td style="text-align:center" colspan="4" >
                 <div style="width:80%;display:none" class="title">
                <asp:Label ID="Label91" runat="server"  BorderWidth="1px" Text="<%$ Resources:ValidationResources, LSerAndSerEd%>"></asp:Label>
                     </div></td>
        
        </tr>
       
        <tr>
            <td  colspan="4">
                <asp:Label ID="Label14" runat="server"  Text="<%$ Resources:ValidationResources,LMnSeriesDt%>"></asp:Label></td>
        </tr>
        <tr>
            <td >
										<asp:Label ID="Label52" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerTtl%>"></asp:Label></td>
            <td colspan="3">
                <input class="text1" id="txtseriesname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="65" runat="server"></td>
        </tr>
        <tr>
            <td >
										<asp:Label ID="Label84" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl%>"></asp:Label></td>
            <td  colspan="3">
                <input class="text1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="65" runat="server" id="txtPTitle"></td>
        </tr>
        <tr>
            <td >
										<asp:Label ID="Label53" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeriesNo%>"></asp:Label></td>
            <td >
                <input id="txtseriesno" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="13" runat="server"></td>
            <td >
                <asp:Label ID="Label54" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSersPart%>"></asp:Label><input id="txtseriespart" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="5" runat="server"></td>
            <td colspan="3">
                <asp:Label ID="Label85" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSerVol%>"></asp:Label><input id="txtSVolume" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="2" runat="server"></td>
        </tr>
        <tr>
            <td >
										<asp:Label ID="Label123" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LISSNNo %>"></asp:Label></td>
            <td  colspan="2">
                <input id="txtMainissn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="42" runat="server"></td>
            <td  colspan="3"></td>
        </tr>
        <tr>
            <td  colspan="4">
										<asp:Label ID="Label90" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LEdits %>"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
										<asp:Label ID="Label86" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                <asp:DropDownList ID="status" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                    CssClass="txt10">
                    <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                    <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                </asp:DropDownList></td>
            <td >
                <asp:Label ID="Label87" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label></td>
            <td  >
                <asp:Label ID="Label88" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label></td>
            <td >
                <asp:Label ID="Label89" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label></td>
            <td ></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label93" runat="server"  CssClass="span">1.</asp:Label></td>
            <td >
                <input class="txt10" id="af1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="am1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="al1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  style="text-align:right">
                <asp:Label ID="Label94" runat="server"  CssClass="span">2.</asp:Label></td>
            <td >
                <input class="txt10" id="af2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  >
                <input class="txt10" id="am2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="al2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label95" runat="server"  CssClass="span">3.</asp:Label></td>
            <td >
                <input class="txt10" id="af3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  >
                <input class="txt10" id="am3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="text1" id="al3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  bgcolor="black" colspan="5"></td>
        </tr>
        <tr>
            <td  colspan="5">
                <asp:Label ID="Label15" runat="server"   Text="<%$ Resources:ValidationResources,LSubSeDt %>"></asp:Label></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label16" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerTtl %>"></asp:Label></td>
            <td colspan="3">
                <input class="text1" id="txtSubseriesname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="65" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label17" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>"></asp:Label></td>
            <td  colspan="3">
                <input class="text1" id="txtSubPTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="65" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td >
										<asp:Label ID="Label18" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeriesNo %>"></asp:Label></td>
            <td>
                <input id="txtSubseriesno" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="13" runat="server"></td>
            <td  >
                <asp:Label ID="Label19" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSersPart %>"></asp:Label><input id="txtSubseriespart" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="5" runat="server"></td>
            <td >
                <asp:Label ID="Label114" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSerVol %>"></asp:Label><input id="txtSubSVolume" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;" 
                    onfocus="this.className='focus'" type="text" size="2" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label124" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISSNNo %>"></asp:Label></td>
            <td  colspan="2">
                <input id="txtSubissn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="42" runat="server"></td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label115" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LEdits %>"></asp:Label></td>
            <td ></td>
            <td  ></td>
            <td ></td>
            <td></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label122" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                <asp:DropDownList ID="Substatus" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                    CssClass="txt10">
                    <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                    <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                </asp:DropDownList></td>
            <td >
                <asp:Label ID="Label119" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label></td>
            <td  >
                <asp:Label ID="Label120" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label></td>
            <td >
                <asp:Label ID="Label121" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label></td>
            <td></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label116" runat="server"  CssClass="span">1.</asp:Label></td>
            <td >
                <input class="txt10" id="Subaf1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  >
                <input class="txt10" id="Subam1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="Subal1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td ></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label117" runat="server"  CssClass="span">2.</asp:Label></td>
            <td >
                <input class="txt10" id="Subaf2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  >
                <input class="txt10" id="Subam2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="Subal2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label118" runat="server" CssClass="span">3.</asp:Label></td>
            <td >
                <input class="txt10" id="Subaf3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  >
                <input class="txt10" id="Subam3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="text1" id="Subal3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  bgcolor="black" colspan="4" ></td>
        </tr>
        <tr>
            <td  colspan="4" >
                <asp:Label ID="Label101" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,LSecdSrDt %>"></asp:Label></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label96" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSerTtl %>"></asp:Label></td>
            <td colspan="3">
                <input class="text1" id="txtSecondSeriesTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="64" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label97" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>"></asp:Label></td>
            <td  colspan="3">
                <input class="text1" id="txtSecondParallelTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="64" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label98" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeriesNo %>"></asp:Label></td>
            <td >
                <input class="text1" id="txtSecondSeriesNo" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="16" runat="server"></td>
            <td>
                <asp:Label ID="Label99" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSersPart %>"></asp:Label><input class="text1" id="txtSecondSeriesPart" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="4" runat="server"></td>
            <td colspan="3">
                <asp:Label ID="Label100" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerVol %>"></asp:Label><input class="text1" id="txtsecSeriesVol" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; " onfocus="this.className='focus'"
                    type="text" size="2" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  >
										<asp:Label ID="Label125" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LISSNNo %>"></asp:Label>
            </td>
            <td  colspan="2">
                <input id="txtSecondissn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="41" runat="server"></td>
            <td colspan="3"></td>
            <td></td>
        </tr>
        <tr>
            <td  colspan="5">
										<asp:Label ID="Label102" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LEdits %>"></asp:Label></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label103" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                <asp:DropDownList ID="cmbSecetal" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
                    runat="server"  CssClass="txt10">
                    <asp:ListItem Value="N" Text="<%$ Resources:ValidationResources,LIN %>"></asp:ListItem>
                    <asp:ListItem Value="Y" Text="<%$ Resources:ValidationResources,LIY %>"></asp:ListItem>
                </asp:DropDownList></td>
            <td >
                <asp:Label ID="Label104" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label></td>
            <td>
                <asp:Label ID="Label105" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label></td>
            <td  colspan="3">
                <asp:Label ID="Label106" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label></td>
            <td></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label107" runat="server"  CssClass="span">1.</asp:Label></td>
            <td >
                <input class="txt10" id="txtSecFirstName1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="txtSecMidName1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  colspan="3">
                <input class="txt10" id="txtSecLastName1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td  align="right">
                <asp:Label ID="Label108" runat="server"  CssClass="span">2.</asp:Label></td>
            <td >
                <input class="txt10" id="txtSecFirstName2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="txtSecMidName2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  colspan="3">
                <input class="txt10" id="txtSecLastName2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label109" runat="server"  CssClass="span">3.</asp:Label></td>
            <td >
                <input class="txt10" id="txtSecFirstName3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td >
                <input class="txt10" id="txtSecMidName3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td  colspan="3">
                <input class="txt10" id="txtSecLastName3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                    onfocus="this.className='focus'" type="text" size="14" runat="server"></td>
            <td></td>
        </tr>
       
       
       
        <tr>
            <td style="text-align:center" colspan="7">
                <table id="Table5" style="width:100%;text-align:center"class="no-more-tables"  bgcolor="aliceblue"
                    >
                    <tr>
                        <td   style="text-align:center">
                                           <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary"
                        Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdSave7_2_Click" />

                            <input id="cmdsave" onclick="PrepareMARC_21();" style="display:none" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bsave %>"
                                runat="server">
                                           <asp:Button ID="cmdReset_2" runat="server" CssClass="btn btn-primary"
                        Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdReset1_1_Click" />

                            <input id="cmdReset" class="btnstyle" type="submit" style="display:none" value="<%$ Resources:ValidationResources,bReset %>"
                                runat="server"></td>
                    </tr>
                    
                    <tr>
                        <td  style="text-align:center" colspan="2">
                                           <asp:Button ID="Button7" runat="server" CssClass="btn btn-primary"
                        Text="<%$Resources:ValidationResources,bBack %>" OnClick="cmdBack7_2_Click" />
                            <input id="cmdReturn" class="btnstyle" type="submit" style="display:none" value="<%$ Resources:ValidationResources,bBack %>"
                                runat="server"></td>
                    </tr>
                    <%--<TR>
												<TD style="WIDTH: 72px"  style="text-align:center" colSpan="2"><INPUT id="cmdReturnMain" style="WIDTH: 149px; HEIGHT: 24px" type="button" value="<%$ Resources:ValidationResources,bReturn %>" runat="server" accesskey="H"></TD>
											</TR>--%>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
                   								<asp:Panel ID="Panel5" runat="server" >
                                    <table id="Table1" class="no-more-tables" style="width:100%">
                                        

                                        <tr>
                                            <td style="text-align:center" colspan="4">
                                                <div style="width: 80%; display: none" class="title">
                                                    <asp:Label ID="Label57" runat="server" Text="<%$ Resources:ValidationResources,TabNoteSec %>"></asp:Label>
                                                </div>


                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="Label75" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBibliography %>"></asp:Label>



                                            </td>
                                            <td colspan="3">
                                                <textarea id="txtBN" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" rows="2" cols="56"
                                                    runat="server" class="txt10"></textarea>



                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label76" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LContent %>"></asp:Label>



                                            </td>
                                            <td colspan="3">
                                                <textarea id="txtcn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" rows="2" cols="56"
                                                    runat="server" class="txt10"></textarea>



                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label77" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBBkInfo %>"></asp:Label>



                                            </td>
                                            <td colspan="3">
                                                <textarea id="txtgn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" rows="2" cols="56"
                                                    runat="server" class="txt10"></textarea>



                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label78" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVolume %>"></asp:Label>



                                            </td>
                                            <td colspan="3">
                                                <textarea id="txtvn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" rows="2" cols="56"
                                                    runat="server" class="txt10"></textarea>



                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label79" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSponsor %>"></asp:Label>



                                            </td>
                                            <td colspan="3">
                                                <textarea id="txtsn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" rows="2" cols="56"
                                                    runat="server" class="txt10"></textarea>



                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label92" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LAnalytical %>"></asp:Label>



                                            </td>
                                            <td colspan="3">
                                                <textarea id="txtan" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" rows="2" cols="56"
                                                    runat="server" class="txt10"></textarea>



                                            </td>
                                        </tr>



                                        <tr>
                                            <td style="text-align:center" colspan="5">
                                               
                                                            <input id="cmdsave2" onclick="PrepareMARC_21();" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                runat="server"><input id="cmdReset2" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                    runat="server">

                                                            <input id="cmdReturn2" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bBack %>"
                                                                runat="server">

                                            </td>
                                        </tr>
                                    </table>
</asp:Panel>
                   									<asp:Panel ID="panel6" runat="server" >
                                        <table id="Table7" style="width: 100%;"class="no-more-tables" >
                                         

                                            <tr>
                                                <td style="text-align:center" colspan="2">
                                                    <div style="width: 80%; display: none" class="title">
                                                        <asp:Label ID="Label83" runat="server" Text="<%$ Resources:ValidationResources,LStdnosub %>"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" rowspan="1">
                                                    <asp:Label ID="Label71" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISBN %>"></asp:Label>

                                                </td>
                                                <td >
                                                    <input id="txtisbn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSub1 %>"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSub11" runat="server" CssClass="txt10" ></asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtSub11"
          MinimumPrefixLength="0"
          CompletionInterval="50"
          CompletionSetCount="50"
          FirstRowSelected="true" 
          CompletionListCssClass="locSt"
          ServicePath="MssplSugg.asmx"
          EnableCaching="true" 
          ServiceMethod="GetSubject" >
     </ajax:AutoCompleteExtender>

<%--                                                    <Custom:AutoSuggestBox ID="txtsub11" runat="server" BorderWidth="1px"
                                                        CssClass="FormCtrl" DataType="City" MaxLength="100" Columns="30" ResourcesDir="asb_includes" IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100" MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..." NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSub2 %>"></asp:Label>



                                                </td>
                                                <td  >
                                                    <asp:TextBox ID="txtsub2" runat="server" ></asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtsub2"
          MinimumPrefixLength="0"
          CompletionInterval="50"
          CompletionSetCount="50"
          FirstRowSelected="true" 
          CompletionListCssClass="locSt"
          ServicePath="MssplSugg.asmx"
          EnableCaching="true" 
          ServiceMethod="GetSubject" >
     </ajax:AutoCompleteExtender>

<%--                                                    <Custom:AutoSuggestBox ID="txtsub2" runat="server" BorderWidth="1px"
                                                        Columns="30" CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                        IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100" MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..." NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSub3 %>"></asp:Label>



                                                </td>
                                                <td  >
                                                    <asp:TextBox ID="txtsub3" runat="server" ></asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtsub3"
          MinimumPrefixLength="0"
          CompletionInterval="50"
          CompletionSetCount="50"
          FirstRowSelected="true" 
          CompletionListCssClass="locSt"
          ServicePath="MssplSugg.asmx"
          EnableCaching="true" 
          ServiceMethod="GetSubject" >
     </ajax:AutoCompleteExtender>
<%--                                                    <Custom:AutoSuggestBox ID="txtsub3" runat="server" BorderWidth="1px"
                                                        Columns="30" CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                        IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100" MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..." NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label31" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LLccn %>"></asp:Label>

                                                </td>
                                                <td  >
                                                    <input id="txtlccn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        onfocus="this.className='focus'" type="text" runat="server">
                                                    </td>
                                                <tr>
                                                    <td>
                                                    <asp:Label ID="Label74" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LIsn %>">

                                                    </asp:Label>
                                                        </td>

                                                    <td>
                                                    <input id="txtIssnNo" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                        runat="server" onfocus="this.className='focus'" type="text"></td>
                                            </tr>
                                            <tr>
                                                <td  style="text-align:center" colspan="2">
                                                    
                                                                <input id="cmdsave5" onclick="PrepareMARC_21();" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                    runat="server">
                                                                <input id="cmdReset5" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                    runat="server">

                                                
                                                                <input id="cmdReturn5" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bBack %>"
                                                                    runat="server">

                                                </td>
                                            </tr>
                                        </table>		
								</asp:Panel>
                   									<asp:Panel ID="panel7" style="width:100%"  runat="server" >
									
									<TABLE id="Table9" class="no-more-tables table-condensed">
											<tr>
											<td>	
												<TABLE id="Table18" style="width:100%">
														<TR>
															<TD style="text-align:center">
                                                                <INPUT id="Button3" onclick="PrepareMARC_21();"  class="btnstyle" type="submit" value="<%$Resources:ValidationResources,bSave %>"
																	runat="server">
                                                                <INPUT id="Button4"  type="submit" class="btnstyle" value="<%$Resources:ValidationResources,bReset%>"
																	runat="server">
                                                                <INPUT id="Button5"  class="btnstyle" type="submit" value="<%$Resources:ValidationResources,bBack%>"
																	runat="server"></TD>
														</TR>
														<%--<TR>
															<TD style="WIDTH: 72px"  style="text-align:center" colSpan="2"><INPUT id="cmdReturn7" style="WIDTH: 149px; HEIGHT: 24px" type="button" value="<%$Resources:ValidationResources,bReturn%>" runat="server" accesskey="H"></TD>
														</TR>--%>
													</TABLE>
											</td>
											</tr>
                                          <tr>
                        <td>
                          <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources,CnkSelectA %>"
                          /></TD>
											</TR>
											</table>
												<div class="allgriddiv" style=" max-height:500px" >
													<asp:datagrid id="grdcopy" CssClass="allgriddiv GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' runat="server" Height="0px" BorderWidth="0px" BorderStyle="None" AllowPaging="false" 
														AutoGenerateColumns="False" CellPadding="0" GridLines="Horizontal" BorderColor="#E7E7FF" BackColor="White">
														<SelectedItemStyle CssClass="GridSelectedItemStyle"></SelectedItemStyle>
														<EditItemStyle CssClass="GridEditedItemStyle"></EditItemStyle>
														<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
														<ItemStyle CssClass="GridItem"></ItemStyle>
														<HeaderStyle CssClass="GridHeader"></HeaderStyle>
														<Columns>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,GrSel %>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkcheck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
															<asp:BoundColumn DataField="accessionnumber" HeaderText="<%$ Resources:ValidationResources,rptAccNo %>"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,DocNo1 %>">
                                                                <ItemTemplate>
                                                                <INPUT type="text" runat="server" id="txtDocNoC" onkeypress="IntegerNumber(this);" size="5" value='<%#Eval("DocNo") %>'>
                                                                   <asp:HiddenField ID="hdaccn" runat="server" Value='<%# Eval("accessionnumber") %>' />
                                                                         
                                                                   <asp:HiddenField ID="hddocdate" runat="server" Value='<%# Eval("DocDate") %>' />
                                                                   <asp:HiddenField ID="hddocno" runat="server" Value='<%# Eval("DocNo") %>' />
                                                                   <asp:HiddenField ID="hdcopyno" runat="server" Value='<%# Eval("copyno") %>' />
                                                                     <asp:HiddenField ID="hdyear" runat="server" Value='<%# Eval("year") %>' />
                                                                     <asp:HiddenField ID="hdpubyear" runat="server" Value='<%# Eval("pubyear") %>' />
                                                                     <asp:HiddenField ID="hdorigprice" runat="server" Value='<%# Eval("bookPrice") %>' />
                                                                     <asp:HiddenField ID="hdcatsource" runat="server" Value='<%# Eval("cat_source") %>' />
                                                                     <asp:HiddenField ID="hdcatalogdate" runat="server" Value='<%# Eval("catalogdate") %>' />
                                                                     <asp:HiddenField ID="hdloc_id" runat="server" Value='<%# Eval("loc_id") %>' />
                                                                     <asp:HiddenField ID="hdcmbcourse" runat="server" Value='' />
                                                                     <asp:HiddenField ID="hdcmbStatus" runat="server" Value='' />
                                                                     <asp:HiddenField ID="hdCmbGrdDept" runat="server" Value='' />
                                                                     <asp:HiddenField ID="hdCmbItemType" runat="server" Value='' />
                                                                     <asp:HiddenField ID="hdCatItemType" runat="server" Value='' />
                                                                    
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,GrDocDt %>">
                                                                <ItemTemplate>
                                                                    <INPUT type = "text"  runat="server" ID="txtDocDateC" Style="position: relative;width:80px" onblur="checkdate2(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" value='<%#DataBinder.Eval(Container.DataItem, "DocDate", hrDate1.Value)%>'/>
                                                                   
                                                                 </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,CpyNo %>">
                                                                <ItemTemplate>
                                                              <INPUT type="text" runat="server" id="txtCopyInfo" onkeypress="IntegerNumber(this);" size="5" value='<%# DataBinder.Eval(Container, "DataItem.copyno") %>'>
                                                               <%-- <asp:TextBox ID="txtCopyInfo" runat="server" BorderWidth="1px"   Text='<%# DataBinder.Eval(Container, "DataItem.copyno") %>' Width="61px"></asp:TextBox>--%>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,LEditionY %>">
                                                                <ItemTemplate>                                                                    
                                                                        <INPUT type="text" runat="server" id="txtEdYear" maxlength="4" value='<%# DataBinder.Eval(Container, "DataItem.year") %>' onkeypress="IntegerNumber(this);" size="5" >
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,GrPubYr %>">
                                                                <ItemTemplate>                                                                  
                                                                         <INPUT type="text" runat="server" id="txtPubYear" maxlength="4" value='<%# DataBinder.Eval(Container, "DataItem.pubYear") %>' onkeypress="IntegerNumber(this);" size="5" >
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,LnbspOrgCurrency %>">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="cmbOriCurrency" runat="server" Height="30"  Width="88px" OnSelectedIndexChanged="cmbOriCurrency_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,LBOrigPrice %>">
                                                                <ItemTemplate>
                                                                  <%--  <asp:TextBox ID="txtOriPriceC" runat="server" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.OriginalPrice") %>'
                                                                        Width="61px"></asp:TextBox>--%>
                                                                        <INPUT type="text" runat="server" id="txtOriPriceC" onkeypress="decimalNumber(this);" size="5" value='<%# DataBinder.Eval(Container, "DataItem.OriginalPrice") %>'>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,GrBookPrice %>">
                                                                <ItemTemplate>
                                                                   <%-- <asp:TextBox ID="txtPrice" runat="server" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.bookprice") %>'
                                                                        Width="61px"></asp:TextBox>--%>
                                                                        <INPUT type="text" runat="server" id="txtPrice" onkeypress="decimalNumber(this);" size="5" value='<%# DataBinder.Eval(Container, "DataItem.bookprice") %>'>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,LSPrice %>">
                                                                <ItemTemplate>
                                                                   <%-- <asp:TextBox ID="txtsplPrice" runat="server" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.specialprice") %>'
                                                                        Width="61px"></asp:TextBox>--%>
                                                                        <INPUT type="text" runat="server" id="txtsplPrice" onkeypress="decimalNumber(this);" size="5" value='<%# DataBinder.Eval(Container, "DataItem.specialprice") %>'>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,LCatSource %>">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCmbVendor" runat="server" Text='<%# Eval("cat_source") %>' ></asp:TextBox>
<%--                                                                     <Custom:AutoSuggestBox ID="txtCmbVendor" runat="server" AutoPostBack="false" BorderWidth="1px"
                                                                      Columns="30" CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                              Width="200px" Height="20px" Text='<%#Eval("cat_source") %>'></Custom:AutoSuggestBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="catalogdate" HeaderText="<%$ Resources:ValidationResources,GrCatalogD %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>                                                            
                                                            <asp:TemplateColumn HeaderText="Location">
                                                                <ItemTemplate>
                                            <asp:TextBox ID="hdnCpLocId" runat="server" Text='<%# Eval("loc_id") %>' style=" display:none"></asp:TextBox>
                                                                    <asp:TextBox ID="txtCpLoc" ToolTip="Please note- internal LocationId is at the end of location after comma ',' - do not change it"  runat="server" Width="180" Text='<%# Eval("location")%>'></asp:TextBox>?
                                                <ajax:AutoCompleteExtender ID="ExtLocationCP" runat="server" TargetControlID="txtCpLoc"
                                                 MinimumPrefixLength="0"
                                               CompletionInterval="50"
                                               CompletionSetCount="50"
                                               FirstRowSelected="true"
                                               CompletionListCssClass="locSt"  CompletionListItemCssClass="locIt" CompletionListHighlightedItemCssClass="locIt2"
                                               ServicePath=""
                                                  EnableCaching="true" 
                                               ServiceMethod="GetLocation3" >
                                              </ajax:AutoCompleteExtender>
                                            <script type="text/javascript">
                                                //                                                OnClientItemSelected = "GetLocationCP"
                                                function GetLocationCP(source, eventArgs) { //dropped
                                                    var lloc = eventArgs.get_value()
                                                    var lloc2 = lloc.split(",");

                                                    alert(lloc2[1]);
                                                    //		        alert(source.get_value());
                                                    var ele = source.get_element().id;
                                                    var ele2 = source.get_element();
                                                    //                                                    $(ele2).parent().find("[id$=hdnCpLocId]").val(lloc2[1]);
                                                    //                                                    $("[id$=grdcopy] tbody tr td" ).find("[id$=hdnCpLocId]").val(lloc2[1]);
                                                    var sufPos = ele.indexOf('_txtCpLoc');
                                                    var indx = ele.substring(29, sufPos);
                                                    alert(indx);
                                                    document.getElementById("ctl00_MainContent_grdcopy_ctl" + indx + "_hdnCpLocId").value = lloc2[1];

                                                }
                                            </script>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>

                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,RBCourse %>">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="cmbCourse" runat="server" Height="30"  onblur="this.className='blur'" onfocus="this.className='focus'" OnSelectedIndexChanged="cmbCourse_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,lblstatus %>">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="cmbStatus" AutoPostBack ="true" Height="30"  runat="server"  onblur="this.className='blur'" onfocus="this.className='focus'" OnSelectedIndexChanged="cmbStatus_SelectedIndexChanged1">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,rptDepartment %>">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="CmbGrdDept" runat="server" Height="30"  onblur="this.className='blur'" onfocus="this.className='focus'" OnSelectedIndexChanged="CmbGrdDept_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,ItemCats %>">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="CmbItemType" runat="server" Height="30"  onblur="this.className='blur'" onfocus="this.className='focus'" OnSelectedIndexChanged="CmbItemType_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Item Category">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="CatItemType" runat="server" Height="30"  onblur="this.className='blur'" onfocus="this.className='focus'" OnSelectedIndexChanged="CatItemType_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Author">
                                                                <ItemTemplate>
                                                        <asp:TextBox runat ="server"  ID="txtauthorgrd" placeholder="Author" Width ="150px" Text='<%# Eval("Author") %>' ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C83" BackColor="AliceBlue" Mode="NumericPages"></PagerStyle>
													</asp:datagrid>
													</div>
													
											</TABLE>
													<TABLE id="Table12" class="no-more-tables table-condensed">
														<TR>
															<TD style="text-align:center">
                                                                                                                                            <span title="Save Catalog Data in Marc 21 also as set in Admin Permission" >Note*</span>
                                                                <asp:Button ID="cmdSave7_2" runat="server" CssClass="btn btn-primary"
                                                                     Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdSave7_2_Click" />

                                                                <INPUT id="cmdSave7" onclick="PrepareMARC_21();" class="btnstyle" type="submit" style="display:none" value="<%$Resources:ValidationResources,bSave %>"
																	runat="server">
                                                                <INPUT id="cmdreset7" style="display:none" class="btnstyle" type="submit" value="<%$Resources:ValidationResources,bReset%>"
																	runat="server">
                                                                <asp:Button ID="cmdreset7_2" runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,bReset%>"
                                                                     OnClick="cmdreset7_2_Click" />
                                                                <asp:Button ID="cmdBack7_2" runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,bReset%>"
                                                                     OnClick="cmdBack7_2_Click" />
                                                                <INPUT id="cmdBack7" class="btnstyle" style="display:none" type="submit" value="<%$Resources:ValidationResources,bBack%>"
																	runat="server"></TD>
														</TR>
														<%--<TR>
															<TD style="WIDTH: 72px"  style="text-align:center" colSpan="2"><INPUT id="cmdReturn7" style="WIDTH: 149px; HEIGHT: 24px" type="button" value="<%$Resources:ValidationResources,bReturn%>" runat="server" accesskey="H"></TD>
														</TR>--%>
													</TABLE>
												
										
										
									</asp:Panel>
                	<asp:Panel ID="panel8" style="width:100%"  runat="server" >
								<TABLE id="Table13"  class="no-more-tables table-condensed">
											<TBODY>
                        
                        <tr>
                            <td colspan="4">
                                 <div style="width:80%;display:none" class="title">
                            <asp:label id="Label80" runat="server"  Text="<%$Resources:ValidationResources,TabTsRltInfo %>"></asp:label>
                                     </div></td>
                        </tr>
                       
                       
                        <tr>
                            <td >
                                <asp:Label ID="Label" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,LProCourse %>"></asp:Label></td>
                            <td colspan="2">
                                <input id="txtProgram" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" style="<%$resources: ValidationResources, TextBox2 %>; "
                                    type="text" readonly="readOnly" /></td>
                            <td >
                            </td>
                        </tr>
												<TR>
													<TD>
                               <asp:Label ID="Label132" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,AdvrOrGuide %>"></asp:Label></TD>
													<TD></TD>
                            <td >
                            </td>
                            <td >
                            </td>
												</TR>
                        <tr>
                            <td >
                            </td>
                            <td >
                                <asp:Label ID="Label133" runat="server" CssClass="opt"  Text="<%$Resources:ValidationResources,LFname %>"></asp:Label></td>
                            <td >
                                <asp:Label ID="Label134" runat="server" CssClass="opt" Text="<%$Resources:ValidationResources,LMname %>"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label135" runat="server" CssClass="opt"  Text="<%$Resources:ValidationResources,LLname %>"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label136" runat="server" CssClass="span" >1</asp:Label></td>
                            <td >
                                <input id="txtaname1" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="13" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                            <td >
                                <input id="txtaname2" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="13" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                            <td>
                                <input id="txtaname3" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="11" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label137" runat="server" CssClass="span">2</asp:Label></td>
                            <td >
                                <input id="txtfname2" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="13" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                            <td >
                                <input id="txtmname2" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="13" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                            <td >
                                <input id="txtlname2" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="11" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label138" runat="server" CssClass="span">3</asp:Label></td>
                            <td >
                                <input id="txtfname3" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="13" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                            <td>
                                <input id="txtmname3" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="13" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                            <td >
                                <input id="txtaname9" runat="server" class="txt10" onblur="this.className='blur'"
                                    onfocus="this.className='focus'" size="11" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                    type="text" /></td>
                        </tr>
                       
                        <tr>
                            <td>
                                <asp:Label ID="Label139" runat="server" CssClass="span"  Text="<%$resources: ValidationResources, Abst %>"></asp:Label></td>
                            <td colspan="3">
                                <textarea id="txtnarration"  onfocus="this.className='focus'" onblur="this.className='blur'"  runat="server" class="txt10" cols="55" style='<%$ Resources:ValidationResources, TextBox2 %>'></textarea></td>
                        </tr>
                       
                        <tr>
                            <td colspan="4" >
                                <table id="Table14"  class="table-condensed no-more-tables">
                                    <tr>
                                        <td style="text-align:center">
                                       <asp:Button ID="cmdSaveT_2" runat="server" CssClass="btn btn-primary"
                                                      Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdSave7_2_Click" />

                                            <input id="cmdSaveT" runat="server" style="display:none" class="btnstyle"
                                                type="submit" value="<%$resources: ValidationResources,bSave %>" />
                                       <asp:Button ID="cmdResetT_2" runat="server" CssClass="btn btn-primary"
                                                     Text="<%$Resources:ValidationResources,bReset %>" OnClick="cmdResetT_2_Click" />

 
                                            <input id="cmdResetT" runat="server" class="btnstyle"
                                                type="submit" value="<%$resources: ValidationResources,bReset %>"  style="display:none" />
                                       <asp:Button ID="cmdReturnt_2" runat="server" CssClass="btn btn-primary"
                                                     Text="<%$Resources:ValidationResources,bBack %>" OnClick="cmdReturnt_2_Click" />
                                    
                                            <input id="cmdReturnt" runat="server" class="btnstyle" style="display:none"
                                                type="submit" value="<%$resources: ValidationResources,bBack %>" /></td>
                                    </tr>
                                    <%--<tr>
                                        <td style="text-align:center" colspan="2" style="width: 72px" >
                                            <input id="cmdReturnMainT" runat="server" style="width: 149px;
                                                height: 24px" type="button" value="<%$resources: ValidationResources,bReturn %>" accesskey="H" /></td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
											</TBODY>
										</TABLE>
								</asp:Panel>
                   </asp:Panel>
                   <input id="HComboSelect" runat="server" style="<%$resources: ValidationResources, TextBox2 %>"
        type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
   
<INPUT id="Hidden5" type="hidden" name="hdIsMarc21" runat="server">
<INPUT id="xCoordHolder" type="hidden" size="1" value="0" name="xCoordHolder" runat="server"><INPUT id="Hidden6" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden7" type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="Hidden8" type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="yCoordHolder" type="hidden" size="1" value="0" name="yCoordHolder" runat="server"><INPUT id="hdForMesage" type="hidden" size="1" name="hdForMesage" runat="server"><input id="hrDate1" runat="server" value="<%$ Resources:ValidationResources, GridDateF %>" style="<%$ Resources:ValidationResources, TextBox2 %>; width: 8px;" type="hidden" />
         	<asp:label id="lblTemp" runat="server" CssClass="err"  Visible="False"></asp:label>
                <div style="display:none">
           <input id="HdBookTitle" runat="server" type="hidden" />
					
               <input id="hdacc"    runat=server  name="hdacc"  type="hidden" />
               <input id="hdCheck" runat="server"  type="hidden" />
               <input id="hdctrlStatus"    runat=server  name="hdctrlStatus"  type="hidden" /><input
                   id="hrDate" runat="server" style='<%$ Resources:ValidationResources, TextBox2 %>'
                   type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" /><input
                       id="js1" runat="server" style='<%$ Resources:ValidationResources, TextBox2 %>' type="hidden"
                       value="<%$ Resources:ValidationResources, js1 %>" /><input id="hdCulture" runat="server"
                            type="hidden" />
               <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
              <input id="Hidden11" runat="server" name="hdConfirm"
                       size="1"  type="hidden" />
               <input id="confirmBefSave" runat="server" causesvalidation="false" onclick="if (ConfirmbeforeSave() == false) return false;"
                   style="width: 6px; height: 1px" type="submit" value="button" />
               <input id="hdpublisheridNew" runat="server" type="hidden" />
               <input id="hddeptcode" runat="server" type="hidden" />
               <input id="hdCatalogCard" runat="server" type="hidden" />
               <input id="btnven" accesskey="V" style="width: 1px; height: 1px" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');" type="button"
                   value="button" runat="server" />
 </div>
            </div>   <%--class="container tableborderst"   main--%>

      </ContentTemplate>
        <Triggers >
<asp:PostBackTrigger ControlID ="cmdPrint" />
                        <asp:PostBackTrigger ControlID ="upload" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton2" />
             <asp:AsyncPostBackTrigger ControlID ="LinkButton3" />
<asp:AsyncPostBackTrigger ControlID ="LinkButton4"  />
<asp:AsyncPostBackTrigger ControlID ="LinkButton5"  />
            <asp:AsyncPostBackTrigger ControlID ="LinkButton6"  />
<asp:AsyncPostBackTrigger ControlID ="LinkButton7"  />
<asp:AsyncPostBackTrigger ControlID ="LinkButton8"  />
             <asp:AsyncPostBackTrigger ControlID="chkselect" />
        </Triggers>
      </asp:UpdatePanel>    
      <div style="display:none">				<input id="btnPub" runat="server" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="P" style="width: 1px; height: 1px;" type="button"
                              value="button" />
                              <%--<input id="btnMedia" runat="server" onclick="openNewForm('btnFillPub','frm_mediatype','HNewForm','HWhichFill','HCondition');" accesskey="M" style="width: 1px; height: 1px;" type="button"
                              value="button" />--%>
                              <input id="btnLanguage" runat="server" onclick="openNewForm('btnFillPub', 'TranslationLanguages', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="L" style="width: 1px; height: 1px;" type="button"
                              value="button" />
                              <input id="btnCategory" runat="server" onclick="openNewForm('btnFillPub', 'CategoryLoadingStatus', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="G" style="width: 1px; height: 1px;" type="button"
                              value="button" />
                              <input id="Button2" runat="server" onclick="openNewForm('btnFillPub', 'ExchangeMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="C" style="width: 1px; height: 1px;" type="button"
                              value="button" />
                          <input id="HNewForm" runat="server"  type="hidden" />
                          <input id="HWhichFill" runat="server"  type="hidden" />
                          <input id="HCondition" runat="server"  type="hidden" />
                          <input id="btnFillPub" runat="server" style="width: 1px; height: 1px;" type="submit" value="button" causesvalidation="false" />
  </div>
        <script type="text/javascript">
            //On Page Load.
            $(function () {
                SetdatePicker();
            });

            //On UpdatePanel Refresh.
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetdatePicker();
                    }
                });
            };
            function SetdatePicker() {
                $("[id$=txtdate],[id$=txtrelease],[id$=txtDocDate]").datepicker({
                    changeMonth: true,//this option for allowing user to select month
                    changeYear: true, //this option for allowing user to select from year range
                    dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
                });

            }
        </script>	
	<INPUT id="Hidden1" style="Z-INDEX: 102; WIDTH: 3px; POSITION: absolute; TOP: 216px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden1" runat="server" /><INPUT id="Hidden2" style="Z-INDEX: 103; LEFT: 408px; WIDTH: 5px; POSITION: absolute; TOP: 168px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden2" runat="server" /><INPUT id="Hidden3" style="Z-INDEX: 104; LEFT: 408px; WIDTH: 5px; POSITION: absolute; TOP: 240px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden3" runat="server" /> <INPUT id="Hidden4" style="Z-INDEX: 105; LEFT: 408px; WIDTH: 5px; POSITION: absolute; TOP: 272px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden4" runat="server" />
</asp:Content>