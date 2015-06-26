﻿<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="PaswordChange.aspx.cs" Inherits="User_PaswordChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table class="table table-bordered">
     <tr>
                <td class="col-sm-3">User Name</td>
                <td>
                    <asp:Label runat="server" ID="lblUsername"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Old Password</td>
                <td>
                    <asp:TextBox ID="TxtOldPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>New Password</td>
                <td>
                    <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Conform Password</td>
                <td>
                    <asp:TextBox ID="txtConformPassword" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
            <td colspan='2' align="center"><asp:Button runat="server" CssClass="btn btn-default" ID="btnChange" Text="Change" OnClick="btnChange_Click" /></td>
            </tr>
    </table>
</asp:Content>

