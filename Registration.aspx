<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            top: 0px;
            left: 0px;
            width: 202px;
            float: left;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style2 {
            width: 202px;
        }
        .auto-style3 {
            width: 407px;
        }
        .auto-style4 {
            width: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:MultiView ID="MultiView2" ActiveViewIndex="0" runat="server">
            <asp:View ID="ViewRegistration" runat="server">
<table id="table1" runat="server" class="table table-bordered">

<tr><td colspan="2"><b>Registration Form</b><br /><asp:Label ID="lblResult" Text=" " runat="server" ForeColor="Red"></asp:Label></td></tr>
<tr><td class="auto-style1">First Name</td><td class="auto-style3"><asp:TextBox ID="TxtFirstName" cssClass="form-control" runat="server"></asp:TextBox></td></tr>
<tr><td class="auto-style2">Last Name</td><td class="auto-style3"><asp:TextBox ID="TxtLastName" cssClass="form-control" runat="server"></asp:TextBox></td></tr>
<tr><td class="auto-style2">Email Id</td><td class="auto-style3"><asp:TextBox ID="TxtEmail" cssClass="form-control" runat="server"></asp:TextBox></td></tr>
<tr><td class="auto-style2">Mobile Number</td><td class="auto-style3"> <asp:TextBox ID="Txtmobile" cssClass="form-control" runat="server"></asp:TextBox></td></tr>

<tr><td class="auto-style2">Password</td><td class="auto-style3"> <asp:TextBox ID="TxtPassword" cssClass="form-control" Type="Password" runat="server"></asp:TextBox></td></tr>

<tr><td> Confirm Password</td>
    <td>
        <asp:TextBox ID="txtConfirmPassword" cssClass="form-control" runat="server"></asp:TextBox>
    </td>
</tr>

<tr><td colspan="2" align="center"> <asp:Button ID="btnsubmit" 
        cssClass="btn btn-default" runat="server" Text="Submit" 
        onclick="btnsubmit_Click" /> <asp:Button ID="Button1" cssClass="btn btn-default" runat="server" Text="Cancel" /></td></tr>

</table>
        </asp:View>
         <asp:View ID="View1" runat="server">
    <asp:Panel ID="Panel1" CssClass="form-group" runat="server" Width="366px" Height="141px">
              <p style="width: 357px">How do you want to verify your acount??</p>
           <asp:RadioButton CssClass="radio-inline" ID="RadioButtonEmail"  GroupName="a" runat="server" Text="By Email" /><br/>
           <asp:RadioButton CssClass="radio-inline" ID="RadioButtonPhone" GroupName="a" Text="By Phone" runat="server" /><br />
           <asp:Button ID="btnVerify" CssClass="btn btn-default" runat="server" Text="Verify" CausesValidation="false" OnClick="btnVerify_Click" />
       </asp:Panel>
       </asp:View>
        <asp:View ID="view2" runat="server">
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
                            <asp:Button  ID="btnsend" CssClass="btn btn-default" runat="server" Text="Submit" OnClick="Button2_Click"/>
                            </td>
                        
                    </tr>
                </table>
            
            </div>
        </asp:View>
            
        </asp:MultiView>

 
</asp:Content>

