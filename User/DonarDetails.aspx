<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="DonarDetails.aspx.cs" Inherits="DonarDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="~/Assets/Styles/Css/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="~/Assets/Styles/Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Assets/Styles/Css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Assets/Js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Assets/Js/jquery-1.7.2.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".text").hide();
            $("#<%= RadioButtonEmail.ClientID %>").click(function () {
                $("#<%= Txtmobile.ClientID %>").hide();
            });
            $("#<%= RadioButtonPhone.ClientID %>").click(function () {
                $("#<%= Txtmobile.ClientID %>").show();
            });
           
        });
    </script>
    <br />
   <div>
    
        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
            
            <asp:View ID="view1" runat="server">
                <asp:Panel ID ="Pnl1" runat="server">
                    <p>Congratulations your account has been created.You have to verify your account to use our services
                        <a href="#" data-toggle="modal" data-target="#SignUpmodel" >Click here to verify your account</a>
                    </p>
                     <div class="modal fade" id="SignUpmodel" style="align-self:center" tabindex="-1" 
role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width:300px;height:900px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;
                            </span><span class="sr-only">
                                     Close</span></button>
                        <h4 class="modal-title" id="myModalLabel">
                            Verify Your Account Using</h4>
                    </div>
                    <div class="modal-body">
                          <div class="input-group">
            <asp:RadioButton CssClass="radio radio-inline" GroupName="a" Text="Phone" ID="RadioButtonPhone" Checked="true" runat="server" />
            
        </div>
        <p>
        </p>
                        <div class="input-group">
           <asp:TextBox ID="Txtmobile"  class="form-control" placeholder="Enter your Mobile number" runat="server"></asp:TextBox>
            
        </div>
        <p>
        </p>
        <div class="input-group">
            <asp:RadioButton  CssClass="radio radio-inline" Text="EmailAddress" GroupName="a" ID="RadioButtonEmail" runat="server"  />
            
        </div>
        <p>
        </p>  
                        <asp:Button id="btnVerify" OnClick="btnVerify_Click" runat="server" Text="Verify" CssClass="btn btn-default"/>
                        <button type="button" 
                        class="btn btn-default" data-dismiss="modal">
                            Close</button>

                    </div>
                   
                </div>
            </div>
        </div>

                </asp:Panel>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
            
                <table class="table table-bordered">
                    <tr>
                        <td colspan="2" style="text-align:center">Please Enter The VerificationCode you Received </td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style4">VerificationCode</td>
                        <td>
                            <asp:TextBox ID="TxtVerificationCode" runat="server" CssClass="form-control" Width="102px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button  ID="btnsend" CssClass="btn btn-default" runat="server" Text="Submit" OnClick="btnsend_Click"/>
                            </td>
                        
                    </tr>
                </table>
            
            </div>
            </asp:View>
            <asp:View ID="ViewInsert" runat="server">
                
               
           
        <table class="table table-bordered" >
            <tr>
                <td colspan="2">

                    <p style="text-align:center; font-weight:bold;"> Please Complete Your Profile </p></td>
            </tr>
            <tr>
                <td >Name</td>
                <td class="auto-style2" >
                    <asp:TextBox ID="txtname" CssClass="form-control" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >Email Address</td>
                <td class="auto-style2" >
                    <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Contactno</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtContactNo" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >District</td>
                <td class="auto-style2" >
                    <asp:DropDownList ID="DropDownListDistrict" runat="server" CssClass="form-control" Width="146px" OnSelectedIndexChanged="DropDownListDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>CurrentAddress</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtCurrentAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >BloodGroup</td>
                <td class="auto-style2" >
                    <asp:DropDownList ID="DropDownBldGrp" CssClass="form-control" runat="server" Width="139px">
                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Photo</td>
                <td class="auto-style2">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>DonatedDate</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtCalender" CssClass="form-control" runat="server" Width="177px" Height="20px"></asp:TextBox>
                    <asp:Image ID="ImageCalender" runat="server" Height="30px" Width="30px" ImageUrl="~/Assets/Images/calender.png" />
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
    TargetControlID="txtCalender" PopupButtonID="Image1">
</ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Best Time To Contact</td>
                <td class="auto-style2">
                    <asp:CheckBoxList ID="chkContactTime" runat="server">
                        <asp:ListItem>Anytime</asp:ListItem>
                        <asp:ListItem>Morning</asp:ListItem>
                        <asp:ListItem>Afternoon</asp:ListItem>
                        <asp:ListItem>Evening</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>Gender</td>
                <td class="auto-style2">
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="a" Text="Male" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="a" Text="Female" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" CssClass="btn-default" runat="server" Text="Submit" OnClick="btnSubmit_Click" Height="33px" />
                </td>
                <td class="auto-style2">
                    <asp:Button ID="btnCancel" CssClass="btn-default" runat="server" Text="Cancel" Height="31px" />
                </td>
            </tr>
        </table>
                   
            
            </asp:View>
            <asp:View ID="ViewDisplay"  runat="server">
                <%--<div style="width:800px; margin:0 auto;">
                    <table>
                        <tr>
                            <td>
                                <p style="text-align:center; font-weight:bold;">My Profile</p>
                                </td>
                            
                        </tr>
                        <tr>
               <asp:DetailsView ID="DetailsView1" runat="server"   Height="50px" Width="516px" AllowPaging="True"
        AutoGenerateRows="False" DataSourceID="SqlDataSource1" DataKeyNames="DonorID" BackColor="White" BorderColor="#575757" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                   <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <Fields>
            
            
            <asp:BoundField DataField="DName" HeaderText="DName" SortExpression="DName" />
            <asp:BoundField DataField="DAddress" HeaderText="DAddress" SortExpression="DAddress" />
            <asp:BoundField DataField="DContactNo" HeaderText="DContactNo" SortExpression="DContactNo" />
            <asp:BoundField DataField="EmailID" HeaderText="EmailID" SortExpression="EmailID" />
            <asp:BoundField DataField="BloodGroup" HeaderText="BloodGroup" SortExpression="BloodGroup" />
            <asp:TemplateField>
                <HeaderTemplate>Photo</HeaderTemplate>
            <ItemTemplate>
                  <img src="<%# Eval("DonorPhotoUrl") %>" height="100" width="100" alt="photo not available" /> 
                  </ItemTemplate>
                </asp:TemplateField>
            
            <asp:BoundField DataField="DonatedDate" HeaderText="DonatedDate" SortExpression="DonatedDate" />
            <asp:BoundField DataField="DonorMessage" HeaderText="DonorMessage" SortExpression="DonorMessage" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
            
        </Fields>
                   <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                   <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
    </asp:DetailsView>
                            </tr>
                        </table>
                    </div>--%>
            </asp:View>
            <asp:View ID="ViewEdit" runat="server"></asp:View>
        </asp:MultiView>
    </div>

</asp:Content>

