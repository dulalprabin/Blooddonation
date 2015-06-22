<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DonarDetails.aspx.cs" Inherits="DonarDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
   <div>
    
        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
            <asp:View ID="ViewInsert" runat="server">
                
               
            <asp:ScriptManager ID="Scriptmanager1" runat="server"></asp:ScriptManager>
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
                <td >Address</td>
                <td class="auto-style2" >
                    <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >BloodGroup</td>
                <td class="auto-style2" >
                    <asp:DropDownList ID="DropDownBldGrp" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True">SELECT GROUP</asp:ListItem>
                        <asp:ListItem>O+</asp:ListItem>
                        <asp:ListItem>O-</asp:ListItem>
                        <asp:ListItem>A+</asp:ListItem>
                        <asp:ListItem>B+</asp:ListItem>
                        <asp:ListItem>B-</asp:ListItem>
                        <asp:ListItem>AB+</asp:ListItem>
                        <asp:ListItem>AB-</asp:ListItem>
                        <asp:ListItem>A-</asp:ListItem>
                        
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
                <td>Gender</td>
                <td class="auto-style2">
                    <asp:RadioButton ID="RadioButton1" GroupName="a" runat="server" Text="Male" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton2" GroupName="a" runat="server" Text="Female" />
                </td>
            </tr>
            <tr>
                <td>Message To Convey</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" Height="41px"  TextMode="MultiLine" Width="233px" BorderColor="#575757" BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" CssClass="btn-default" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
                <td class="auto-style2">
                    <asp:Button ID="btnCancel" CssClass="btn-default" runat="server" Text="Cancel" Height="34px" />
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

