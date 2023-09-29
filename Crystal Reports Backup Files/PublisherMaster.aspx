<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="PublisherMaster.aspx.cs" Inherits="Library.PublisherMaster" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">

        <style>
    </style>
		<script type="text/javascript" >

            $(function () {
                let m = $('[id$=hdMast]').val();
                if (m == 'y') {
                    $('[id*=tblHeader], [id*=horizontal]').css('display', 'none');

                }
                $('[id$=chkSame]').on('click', function () {
                    chkSame_onclick();
                });
            })
            function CallerUpd() {
                let idcaller = $('[id$=hdIdcaller]').val();
                let txtcaller = $('[id$=hdTxtcaller]').val();
                let valueid = $('[id$=hdIdvalue]').val();
                let valuetxt = $('[id$=hdTxtvalue]').val();
                if (valueid != '') {
                    window.opener.document.getElementById(idcaller).value = valueid;
                    window.opener.document.getElementById(txtcaller).value = valuetxt;

                    window.close();

                }

            }
            function onTextBoxUpdate2(evt) {

                var textBoxID = evt.source.textBoxID;
                if (evt.selMenuItem != null) {
                    document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
                    document.getElementById("Button4").click();
                }
                evt.preventDefault();
            }

            function setfocus() {
                document.Form1.txtfirstname.focus();
            }


            function cmdsave_ServerClick() {



            }
            function chkSame_onclick() {

//                alert('232');
                let AllDeac = document.getElementById('<%=chkSame.ClientID%>').checked;
                if (AllDeac== true) {
                    document.getElementById('<%=txtpaddress.ClientID%>').value = document.getElementById('<%=txtladd.ClientID%>').value
                    document.getElementById('<%=txtpcity.ClientID%>').value = document.getElementById('<%=txtlcity.ClientID%>').value
                    document.getElementById('<%=txtpstate.ClientID%>').value = document.getElementById('<%=txtlstate.ClientID%>').value
                    document.getElementById('<%=txtppincode.ClientID%>').value = document.getElementById('<%=txtlpincode.ClientID%>').value
                    document.getElementById('<%=txtpcountry.ClientID%>').value = document.getElementById('<%=txtlcountry.ClientID%>').value
                   
                }
                else {
                    document.getElementById('<%=txtpaddress.ClientID%>').value =''
                    document.getElementById('<%=txtpcity.ClientID%>').value =''
                    document.getElementById('<%=txtpstate.ClientID%>').value =''
                    document.getElementById('<%=txtppincode.ClientID%>').value =''
                    document.getElementById('<%=txtpcountry.ClientID%>').value = ''
                }
            }

            function chk() {
                if (document.Form1.hdTop.value == "top") {
                    window.scrollTo(0, 0);
                    document.Form1.hdTop.value = 0;
                    document.Form1.txtpubcode.focus();
                }

            }

            function validateform() {
                let v1 = $('[id$=txtpubcode]').val().trim();
                if (v1 == '') {
                    alert('Publisher Code required.');
                    return false;
                }
                let v2 = $('[id$=txtfirstname]').val().trim();
                if (v2 == '') {
                    alert('Publisher Name required.');
                    return false;

                }
                let v3 = $('[id$=txtpcity]').val().trim();
                if (v3 == '') {
                    alert('Publisher City required.');
                    return false;

                }
                return true;
            }


        </script>
		
		
		<script language="javascript" type="text/javascript">
            function SetCaller(fldNM, fldId) {
                try {
                    var pName = document.getElementById('txtfirstname').value + ', ' + document.getElementById('txtpcity').value;
                    var pId = document.getElementById('txtpublisherid').value;
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        window.returnValue = pName + ':' + pId;
                    } else {
                        window.opener.document.getElementById(fldNM).value = pName;
                        window.opener.document.getElementById(fldId).value = pId;
                    }
                    window.close();
                }
                catch (ex) {
                    alert(ex.message);
                }
            }

            function txtpubcode1_OnKeyDown() {
                if (window.event.keyCode == 13) {
                    window.document.Form1.cmdsearch.focus();
                }
            }
            function txtpubname_OnKeyDown() {
                if (window.event.keyCode == 13) {
                    window.document.Form1.cmdsearch.focus();
                }
            }
        </script>


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdMast" runat="server" />
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />

    </ProgressTemplate>
    </asp:UpdateProgress>
    <span style="color:red;">delete not done </span>
         <div class="container tableborderst" style="padding:0;">     
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <script>
                                    $(document).ready(function () {
                                        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(CallerUpd);
                                    })
                                </script>
<asp:HiddenField ID="hdIdcaller" runat="server" />
<asp:HiddenField ID="hdTxtcaller" runat="server" />
<asp:HiddenField ID="hdIdvalue" runat="server" />
<asp:HiddenField ID="hdTxtvalue" runat="server" />
 <div class="no-more-tables" style="width:100%">
                                                 <TABLE id="Table1"  class="table-condensed">
												
												<tr>
                                                    <td style="width:20%"></td>
                                                    <td colspan="3">
                                                        <asp:Label ID="msglabel" runat="server" CssClass="err" Visible="False" ></asp:Label>
                                                    </td>
                                                </tr>
												<TR>
													<TD ><asp:label id="Label1" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LpubCode %>"></asp:label></TD>
													<TD ><INPUT onkeypress="disallowSingleQuote(this);" onfocus="this.className='focus'" onblur="this.className='blur'" id="txtpubcode"  type="text" maxLength="20" name="txtpubcode" runat="server" class="txt10">
														</TD>
												 
													<TD ><asp:label id="Label13" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LpubName %> "> </asp:label></TD>
													<TD  ><INPUT onkeypress="disallowSingleQuote(this);" id="txtfirstname" onblur="this.className='blur'"
															
															onfocus="this.className='focus'" type="text" name="txtfirstname" runat="server" class="txt10" maxlength="100">
														<asp:label id="Label6" runat="server" CssClass="star" >*</asp:label></TD>
												</TR>
												<TR>
													<TD ><asp:label id="Label7" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources, LpubType %>"> </asp:label></TD>
													<TD ><asp:dropdownlist id="ddlPublisherType" onblur="this.className='blur'" onfocus="this.className='focus'"
															runat="server" CssClass="txt10"  Height="30" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
															<asp:ListItem Value="Indian" Selected="True" Text ="<%$ Resources:ValidationResources, LILocal %>"></asp:ListItem>
															<asp:ListItem Value="Foreigner" Text ="<%$ Resources:ValidationResources, LIForeigner %>"></asp:ListItem>
														</asp:dropdownlist></TD>
                                                    <td style="width:20%"></td>
                                                    <td style="width:30%"></td>
												</TR>
												<TR>
													<TD colspan="2">
                                                        <fieldset>
                                                            <legend>
                                                                <asp:label id="Label11" runat="server"  Font-Bold="true" Font-Size="12px" Text="<%$ Resources:ValidationResources, LLocalAddr %>"> </asp:label>
                                                            </legend>
                                                        
														<TABLE class="table-condensed" id="Table6" >
															 
															<TR>
																<TD >
                                                                  <asp:label id="Label27" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LHoNo %>"> </asp:label></TD>
															</TR>
															<TR>
																<TD ><TEXTAREA id="txtladd" onblur="this.className='blur'" rows="5" cols="5" onfocus="this.className='focus'"
																		name="txtladd" runat="server"  ></TEXTAREA></TD>
															</TR>
															<TR>
																<TD>
                                                                    <asp:label id="Label20" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, Lcity %>"></asp:label>
                                                                 <%--   <autosuggestmenu id="asmJTitle1" runat="server" height="18px" 
                                                                        keypressdelay="10" maxsuggestchars="100" 
                                                                        onclienttextboxupdate="onTextBoxUpdate2" ongetsuggestions="GetSuggestions1" 
                                                                        resourcesdir="~/asm_includes" targetcontrolid="txtlcity" 
                                                                        updatetextboxonupdown="False" width="155px">
                                                                      
                                                                                    </autosuggestmenu>--%>
                                                                              
                                                                </TD>
															</TR>
															<TR>
																<TD >
                                                                    <asp:TextBox ID="txtlcity" runat="server"  > </asp:TextBox>
                                                               
                                                                </TD>
															</TR>
															<TR>
																<TD ><asp:label id="Label9" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LState%>">State</asp:label></TD>
															</TR>
															<TR>
																<TD >
                                                                    <input id="txtlstate" runat="server" class="txt10" name="txtlstate0" 
                                                                        onblur="this.className='blur'" onfocus="this.className='focus'" size="20" 
                                                                       
                                                                        type="text" /></TD>
															</TR>
															<TR>
																<TD ><asp:label id="Label10" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LPinCo%>"> </asp:label></TD>
															</TR>
															<TR>
																<TD ><INPUT id="txtlpincode" onblur="this.className='blur'"  
																		onfocus="this.className='focus'" type="text" maxLength="15" name="txtlpincode" runat="server" class="txt10"></TD>
															</TR>
															<TR>
																<TD ><asp:label id="Label12" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, LCountry%>"></asp:label></TD>
															</TR>
															<TR>
																<TD ><input 
                                                                        id="txtlcountry" runat="server" class="txt10" name="txtlcountry" 
                                                                        onblur="this.className='blur'" onfocus="this.className='focus'" size="20" 
                                                                        
                                                                        type="text" /> </TD>
															</TR>
														</TABLE>
                                                            </fieldset>
													</TD>
													<TD colspan="2">
                                                        <fieldset>
                                                            <legend>
                                                               <asp:label id="lblper" runat="server"  Font-Bold="true" Font-Size="12px" Text="Permanent Address"> </asp:label> 
                                                            </legend>
														<TABLE  id="Table7" class="table-condensed" style="margin-top:-35px">
															 <TR>
                                                                 <TD>
                                                                      <asp:checkbox id="chkSame" runat="server"   Text="Same as Local Add" Font-Size="12px" ></asp:checkbox>
                                                                     </TD>
                                                                 </TR>
															<TR>
																<TD >
                                                                  <asp:label id="Label30" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LHoNo %>"></asp:label></TD>
															</TR>
															<TR>
																<TD ><textarea id="txtpaddress" runat="server" class="txt10" rows="5" cols="5" name="txtladd" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'"  ></textarea>
																	</TD>
															</TR>
															<TR>
																<TD><asp:label id="Label15" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, Lcity%>"></asp:label></TD>
															</TR>
															<TR>
																<TD ><INPUT id="txtpcity" onblur="this.className='blur'" 
																		onfocus="this.className='focus'" type="text" name="txtpcity" runat="server" class="txt10" maxlength="35">
																	<asp:label id="Label28" runat="server" CssClass="star">*</asp:label>
                                                               <%--     <autosuggestmenu id="asmJTitle2" runat="server" height="18px" 
                                                                        keypressdelay="10" maxsuggestchars="100" 
                                                                        onclienttextboxupdate="onTextBoxUpdate2" ongetsuggestions="GetSuggestions1" 
                                                                        resourcesdir="~/asm_includes" targetcontrolid="txtlcity" 
                                                                        updatetextboxonupdown="False" width="155px">
                                                                    </autosuggestmenu>--%>
                                                                </TD>
															</TR>
															<TR>
																<TD><asp:label id="Label16" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, LState%>"></asp:label></TD>
															</TR>
															<TR>
																<TD ><INPUT id="txtpstate" onblur="this.className='blur'" 
																		onfocus="this.className='focus'" type="text" name="txtpstate" runat="server" class="txt10" maxlength="35">
																	</TD>
															</TR>
															<TR>
																<TD><asp:label id="Label17" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, LPinCo%>"> </asp:label></TD>
															</TR>
															<TR>
																<TD ><INPUT id="txtppincode" onblur="this.className='blur'" 
																		onfocus="this.className='focus'" type="text" maxLength="15" name="txtppincode" runat="server" class="txt10"></TD>
															</TR>
															<TR>
																<TD ><asp:label id="Label18" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, LCountry%>"></asp:label></TD>
															</TR>
															<TR>
																<TD ><INPUT id="txtpcountry" onblur="this.className='blur'" 
																		onfocus="this.className='focus'" type="text" name="txtpcountry" runat="server" class="txt10" maxlength="35">
																	</TD>
															</TR>
														</TABLE>
                                                            </fieldset>
													</TD>
												</TR>
												<TR>
													<TD ><asp:label id="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LConNo%>"></asp:label></TD>
													<TD><INPUT id="txtphone1" onblur="this.className='blur'" 
															onfocus="this.className='focus'" type="text" maxLength="50" size="20" name="txtphone1" runat="server" class="txt10"></TD>
												 
													<TD ><asp:label id="Label2" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LFax%>"></asp:label></TD>
													<TD><INPUT id="txtphone2" onblur="this.className='blur'" 
															onfocus="this.className='focus'" type="text" maxLength="50" size="20" name="txtphone2"
															runat="server" class="txt10"></TD>
												</TR>
												<TR>
													<TD ><asp:label id="Label5" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LEmail%>"> </asp:label></TD>
													<TD ><INPUT id="txtemail" onblur="this.className='blur'" 
															onfocus="this.className='focus'" type="text" maxLength="50" name="txtemail" runat="server" class="txt10">
                                                      
														</TD>
												 
													<TD ><asp:label id="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LWebAddr%>"> </asp:label></TD>
													<TD><INPUT id="txtwebadd" onblur="this.className='blur'" onfocus="this.className='focus'" type="text" name="txtwebadd" runat="server" class="txt10"></TD>
													 
												</TR>
                                                
                                            <tr>
                                                <td colspan="4">
                                                  <asp:CheckBox ID="chkvendor" runat="server" CssClass="opt"
                                                        Font-Size="16px" 
Text="<%$ Resources:ValidationResources, ChkSelPubDirect%>" /></td>
                                                 
                                            </tr>
												<TR>
													 
													<TD colspan="4" style="text-align:center">                         
                                                                    <input id="cmdsave" runat="server" style="display:none" accesskey="S" class="btnstyle" name="cmdsave"  onclick =" if ( validateform()==false ) return false; else true ;"
                                                                        type="button" 
                                                                        value="<%$ Resources:ValidationResources, bSave%>" />
                                                                    </input>
                                                        <asp:Button ID="cmdsave2"  runat="server" CssClass="btn btn-primary" Text="<%$ Resources:ValidationResources, bSave%>"
                                                             OnClick="cmdsave2_Click" />

                                                               <asp:Button ID="btnreset2" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:ValidationResources, bReset%>" OnClick="btnreset2_Click" />
                                                                 
                                                        <asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary" Text="Delete"
                                                             OnClick="btndelete_Click" />
                                                        <input id="cmddelete" runat="server" style="display:none;" accesskey="X" class="btnstyle" name="cmddelete" 
                                                                        onclick="if (DoConfirmation() == false) return false;" 
                                                                        type="button" value="<%$ Resources:ValidationResources, bdelete%>" />
                                                                    </input>
                                                                
                                                    </TD>
												</TR>
											</TABLE>
                                             <TABLE id="Table8" class="table-condensed no-more-tables GenTable1">
												<TR>
                                                   <%-- Add  colspan=4---priyanka--%>
													<TD colspan="4">
                                                      <asp:label id="Label31" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources, LpubSearch%>"></asp:label></TD>
												</TR>
												<TR>
													<TD >
                                                       <asp:label id="Label19" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, LpubCode%>"> </asp:label></TD>
													<TD ><INPUT id="txtpubcode1" onkeydown="txtpubcode1_OnKeyDown();" onblur="this.className='blur'"
															
															onfocus="this.className='focus'" type="text" name="txtpubcode1" runat="server" class="txt10" maxlength="20"></TD>
												 
													<TD >
                                                      <asp:label id="Label22" runat="server" CssClass="span" 
Text="<%$ Resources:ValidationResources, LpubName %>"></asp:label></TD>
													<TD><INPUT id="txtpubname" onblur="this.className='blur'" 
															onfocus="this.className='focus'" type="text" name="txtpubname" runat="server" class="txt10" maxlength="100"></TD>
												</TR>
												<TR>
													
													<TD colspan="4" style="text-align:center">
                                                        <INPUT id="cmdsearch"  type="button" style="display:none" value=
"<%$ Resources:ValidationResources, bSearch%>" name="cmdsearch"
															runat="server" class="btnstyle">
                                                        <asp:Button ID="btnsearch2" runat="server" Text="<%$ Resources:ValidationResources, bSearch%>"
                                                             CssClass="btn btn-primary"  OnClick="btnsearch2_Click" />
                                                             
                                                        <INPUT id="cmdshowall"  type="button" value="<%$ Resources:ValidationResources, bShoeAll%>"
															name="cmdshowall" runat="server" class="btnstyle"></TD>
												</TR>
												</table>
                     <div class="allgriddiv" style="max-height:400px;" id="dvgrd" runat="server">                                  
<asp:HiddenField runat="server" ID="hdnGrdId" />
                             <asp:DataGrid ID="grdsearch" runat="server"  OnItemCommand="grdsearch_ItemCommand"
                                        Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" 
                                         CssClass="allgrid GenTable1" Width="100%">
                                        <Columns>
                                            <asp:ButtonColumn CommandName="Select" DataTextField="publishercode" 
                                                HeaderText="<%$ Resources:ValidationResources, LPubC%>" 
                                                 Text="publishercode"></asp:ButtonColumn>
                                            <asp:BoundColumn DataField="publishercode" 
                                                HeaderText="<%$ Resources:ValidationResources, LPubC %>" 
                                                 Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="publishername" 
                                                HeaderText=" <%$ Resources:ValidationResources, LName%>" 
                                                ></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                         <INPUT id="txtpublisherid" type="hidden" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: Black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 56px; BORDER-BOTTOM: black 1px solid; HEIGHT: 22px"
	name="txtpublisherid" runat="server">
                        </div>
				 <input id="txtvendor_C" runat="server" type="hidden" />
                 
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
                                           //   ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]", 200);
                                          }
                                          catch (err) {
                                          }
                                      }


                                  </script>
								        </ContentTemplate>
                            </asp:UpdatePanel>       
                                </div>

</asp:Content>