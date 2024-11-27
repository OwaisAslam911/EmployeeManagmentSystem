<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AuthenticationLayout.Master" CodeBehind="Login.aspx.cs" Inherits="EmployeeManagmentSystem.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main aria-labelledby="title">
       
        <div class="container d-flex justify-content-center align-items-center" style="height: 100vh;">
            
            <div class="form-container " style="width:400px;">
                <h2>Login</h2>
        <form id="loginForm" runat="server">
            <div class="form-group">
                <label for="username">Username:</label>
                <asp:TextBox ID="usernameTextBox" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="password">Password:</label>
                <asp:TextBox ID="passwordTextBox" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="form-group">
                <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="LoginButton_Click" CssClass="btn btn-primary" />
            </div>
            <asp:Label ID="errorMessage" runat="server" ForeColor="Red" />
        </form></div>
        </div>
    </main>
</asp:Content>
