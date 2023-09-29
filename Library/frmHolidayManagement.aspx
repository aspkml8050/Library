<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="frmHolidayManagement.aspx.cs" Inherits="Library.frmHolidayManagement" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="HoliHead" runat="server" ContentPlaceHolderID="head">
    <Link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >
    <script type="text/javascript">
        function valiFT(fNam) {
            var _validFileExtensions = [ "pdf"];
            var fNMx = fNam.split(".");
            var f = _validFileExtensions.indexOf(fNMx[1]);
            if (f < 0) {
                alert("Valid file is .pdf");
                return false;
            }
            $('[id$=txSplPdf]').val(fNam.substring(fNam.lastIndexOf('\\')+1));
            //            $("[id$='lblMsg']").text("");  
            return true;
        }

        function SplLoad(fu) {
            var fil = fu.files[0];librar
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
                $("[id$='hdSplHoliDs']").val(imG2);
            }
            fr.readAsDataURL(fil);

            return true;
        }

        function delholiday (btn){
            let a = confirm("Are you sure?");

            return a;
        }
    </script>
</asp:Content>
<asp:Content ID="HoliMain" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="container tableborderst" >
          <div style="width:100%;display:none" class="title">
               <div style="width:89%;float:left;display:none" > &nbsp;
                   <asp:label id="Label7" runat="server" 
							Width="100%" >   Holiday Details</asp:label>

                   </div>
               <div style="float:right;vertical-align:top"> 
                    <a id="lnkHelp" style="display:none" href="#" onclick="ShowHelp('Help/Masters-holiday management.htm')"><img height="15" src="help.jpg" alt="Help"/></a>

                   </div>
              </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnDNPdf" />
             </Triggers>
            <ContentTemplate>
                <div class="no-more-tables" style="width:100%">
                    <table id="Table1"  class="table-condensed GenTable1 ">
                        <tr>
                            <td colSpan="4">
                                <asp:label id="msglabel" runat="server" Font-Bold="True" Font-Size="X-Small" Font-Names="Lucida Sans Unicode"
										 ForeColor="Red"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:label id="Label3" runat="server"
										 CssClass="span" Text ="<%$ Resources:ValidationResources, LHolidate %>"> </asp:label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHolidayDate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                 <asp:label id="Label1" runat="server" ForeColor="Red">*</asp:label>

                            </td>
                            <td>
                                    <asp:label id="Label4" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LDes %>"></asp:label></td>
                            <td>
                                <INPUT class="txt10" id="txtDescription" onblur="this.className='blur'" onfocus="this.className='focus'"  
										type="text" name="txtDescription" runat="server" maxlength="55">
									<asp:label id="Label2" runat="server" ForeColor="Red">*</asp:label>
                            </td>


                        </tr>
                        <tr>
                            <td style="width:17%">
                                 <asp:label id="Label5" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LSch %>"></asp:label>
                            </td>
                            <td>
                                <asp:dropdownlist id="cmbdepartmentcode"  Height="30" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'  runat="server"  CssClass="txt10">
                                    <asp:ListItem Value="Yes" Text ="<%$ Resources:ValidationResources, LYes %>"></asp:ListItem>
										<asp:ListItem Value="No" Text ="<%$ Resources:ValidationResources, Lno %>"></asp:ListItem>
									</asp:dropdownlist>
                            </td>
                            <td style="width:17%"></td>
                                <td  style="width:33%"></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:center" >
                                <INPUT id="cmdsave" type="button" value="<%$ Resources:ValidationResources, bSave %>" name="cmdsave" runat="server" accesskey="S" style="display:none;" class="btnstyle">
                                <asp:Button ID="cmdsave1" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="cmdsave1_Click" />

											<INPUT id="cmdreset" type="button" value="<%$ Resources:ValidationResources, bReset %>" name="cmdreset" runat="server" accesskey="E" style="display:none;" class="btnstyle">
                                <asp:Button ID="cmdreset1" CssClass="btn btn-primary" runat="server" Text="Reset" OnClick="cmdreset1_Click"/>
                                

											<INPUT id="cmddelete" type="button" value="<%$ Resources:ValidationResources, bDelete %>" name="cmddelete" runat="server" onclick="if (DoConfirmation() == false) return false;" accesskey="X" class="btnstyle" style="display:none;">

                                <asp:Button ID="cmddelete1" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="cmddelete1_Click"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                  <asp:label id="Label6" runat="server" Font-Bold="True"
										 CssClass="showBoldExist" Text ="<%$ Resources:ValidationResources, ExistHoliDet %>">  </asp:label></td>

                        </tr>
                    </table>
                    </div>
                <div id="divH" class="allgrid" style="margin-bottom:5px">
                     <asp:label id="Label8" runat="server" Font-Bold="True" Text ="Academic Session in Library Parameter set based">  </asp:label>
                                    <br />
                      <div class="allgriddiv"  id="dvgrd" style="width:100%;max-height:250px;overflow:auto;" runat="server">

                        <asp:datagrid id="DataGrid1" OnItemCommand="DataGrid1_ItemCommand"  runat="server" CssClass="allgrid GenTable1"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
    <Columns>
											<asp:ButtonColumn Text="Holiday Date" HeaderText="<%$ Resources:ValidationResources, LHolidate %>" DataTextFormatString="<%$ Resources:ValidationResources, GridDateF %>" CommandName="Select"  DataTextField="h_date">
                                               
                                            </asp:ButtonColumn>
											<asp:BoundColumn DataField="h_date"   HeaderText="<%$ Resources:ValidationResources, LHolidate %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>" Visible="False"></asp:BoundColumn>
											<asp:BoundColumn DataField="description" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, Descrpt %>" >
												<HeaderStyle Width="100px"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
										
									</asp:datagrid>
                          <asp:HiddenField runat="server" ID="hdnGrdId" />
                      </div>
                    </div>
                <table class="table-condensed no-more-tables GenTable1"  >
                        <tr>
                            <td colspan="4">Generate weekly holidays for a Session.</td>
                        </tr>
                        <tr>
                            <td><asp:DropDownList ID="ddlSess" Height="30" runat="server" AutoPostBack="true"   ></asp:DropDownList></td>
                            <td><asp:DropDownList ID="ddlWeekds" Height="30" runat="server"></asp:DropDownList></td>
                        
                            <td><asp:Button ID="btnGene" runat="server" CssClass="btn btn-primary" style="text-align:center" Text="Generate" OnClick="btnGene_Click" ToolTip="This button will generate list of holidays and show in the table above." /></td>
                            <td><asp:Button ID="btnGSave" runat="server"  CssClass="btn btn-primary" style="text-align:center" Text="Save Generate List"/></td>
                        </tr>
                        </table> 

                 <div style="display:none">
                      <input id="txtholydayid" style="width: 40px" type="hidden" runat="server"  /><input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 63px" /><input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 92px" /><input id="hdCulture" runat="server" style="width: 72px" type="hidden" />
                                <input id="hdTop" runat="server" name="hdTop" size="1" style="width: 32px; height: 22px"
                            type="hidden" />

                                 <asp:HiddenField ID="hdSplHId" runat="server" />
                                <asp:HiddenField ID="hdSplHoliDs" runat="server" />
                            </div>
                      <div class="confirmPanel"  style="margin-top:30px">
                          <table class="table-condensed no-more-tables GenTable1">
                              <tr>
                                  <td colspan="2">
                                      <asp:Label runat="server" ID="Label12" Text="Special Holidays" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="width:17%">

                                  </td>
                                  <td style="width:31.5%">

                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <asp:Label runat="server" ID="Datef" Text="Date From"></asp:Label>
                                     
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txSDf" runat="server" ></asp:TextBox>
                                  </td>
                                  <td>
                                       <asp:Label runat="server" ID="Label9" Text="Date Upto"></asp:Label>
                                      
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txSDu" runat="server" ></asp:TextBox>
                                  </td>
                                  </tr>
                              <tr>
                                  <td style="width:20%">
                                      <asp:Label runat="server" ID="Label10" Text="Description"></asp:Label>
                                      
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txSDesc" runat="server"   TextMode="MultiLine" Height="50" MaxLength="500" ></asp:TextBox>

                                  </td>
                                  <td>
                                      <asp:Button ID="btnSplSav" CssClass="btn btn-primary" runat="server" ToolTip="This will override regular holiday saved data" Text="Save" OnClick="btnSplSav_Click"/>
                                  </td>
                                  <td></td>
                              </tr>
                              <tr>
                                  <td>
                                  <asp:Label runat="server" ID="Label11" Text="Upload Pdf Doc"></asp:Label>

                                  </td>
                                  <td>
                                    <asp:FileUpload ID="fuSpl" runat="server"   onchange="SplLoad(this)" Width="90%"  />
                                  </td>
                                  <td colspan="2">
                                      <table style="width:100%">
                                          <tr>
                                              <td>
                          <asp:TextBox ID="txSplPdf" runat="server"></asp:TextBox>

                                  </td>
                                              <td>
                          <asp:Button ID="btnDNPdf" CssClass="btn btn-primary" runat="server" Text="DownLoad Pdf" OnClick="btnDNPdf_Click" />

                                  </td>
                                              </tr>
                                          </table>
                                      </td>
                                  </tr>
                             
                                      </table>
                                      
                 </div>
                <table class="table-condensed GenTable1">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkShow" runat="server" Text="Show Existing" OnCheckedChanged="chkShow_CheckedChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                    </table>
                <div class="allgriddiv">
                     <asp:GridView ID="grdSplHs" runat="server"  AutoGenerateColumns="false" CssClass="allgrid GenTable1">
                          <Columns>
                                  <asp:TemplateField HeaderText="Date From">
                                          <ItemTemplate>
                                          <asp:LinkButton ID="lnkDf" runat="server"  OnClick="lnkDf_Click"></asp:LinkButton>
 
                                          <asp:HiddenField ID="hdid" runat="server" Value='<%# Eval("Splholidayid") %>' />
                                          <asp:HiddenField ID="hdstrdoc" runat="server" Value='<%# Eval("strdocument") %>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lnkDf" runat="server"  OnClick="lnkDf_Click" Text='<%# String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("datefrom")))  %>' ></asp:LinkButton>
                                          <asp:HiddenField ID="hdid" runat="server" Value='<%# Eval("Splholidayid") %>' />
                                          <asp:HiddenField ID="hdstrdoc" runat="server" Value='<%# Eval("strdocument") %>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                               <asp:TemplateField HeaderText="Date Upto">
                                    
                                      <ItemTemplate>
                                          <asp:Label ID="labdu" runat="server" Font-Bold="false" Text='<%# String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("dateupto"))) %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              <asp:TemplateField>
                                      <HeaderTemplate>
                                          Description
                                      </HeaderTemplate>
                                      <ItemTemplate>
                                          <asp:Label ID="labdesc" runat="server" Font-Bold="false"  Width="400" Text='<%# Eval("description") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField >  
                                  <asp:TemplateField HeaderText="Pdf doc">
                                  
                                      <ItemTemplate>
                                          <asp:Label ID="labpdf" runat="server" Font-Bold="false"  Text='<%# Eval("filename") %>'></asp:Label>
                                          <asp:Button ID="btnDel" runat="server" Text="Del" OnClientClick="return delholiday(this);" OnClick="btnDel_Click" />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                         </asp:GridView>
                </div>
            </ContentTemplate>
         </asp:UpdatePanel>

     </div>
     <INPUT id="txtholidayid" 
				type="text" runat="server">
                                    <INPUT id="Hidden2" 
				type="hidden" name="Hidden2" runat="server">
                       <INPUT id="Hidden3" 
				type="hidden" size="1" name="Hidden2" runat="server">      
                <INPUT id="Hidden1" 
				type="hidden" size="1" name="Hidden1" runat="server">

    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();
            ForDataTable();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
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

        function SetDatePicker() {

            $("[id$=txtHolidayDate], [id$=txSDf], [id$=txSDu]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });
        }

    </script>	
<style>
    .confirmPanel
    {
    background-color: #f6f6f6 ;padding:10px ;box-shadow:0px 0px 8px 0px #a1a1a1 ;border:solid 1px #cccccc;border:0
    }
    
         .dataTables_scrollBody
         {
                height:auto!important;
                max-height:300px!important;
                
         }
    </style>
</asp:Content>
