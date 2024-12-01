<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarkAttendence.aspx.cs" Inherits="EmployeeManagmentSystem.MarkAttendence" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
            <form  runat="server">
        <div>
            <label for="AttendanceType">Select Attendance Type:</label>
           <asp:DropDownList ID="AttendanceType" runat="server">
             <asp:ListItem Text="Present" Value="Present"></asp:ListItem>
             <asp:ListItem Text="Absent" Value="Absent"></asp:ListItem>
              <asp:ListItem Text="Leave" Value="Leave"></asp:ListItem>
                </asp:DropDownList>

            <br /><br />
            <asp:Button ID="MarkAttendanceButton" runat="server" Text="Mark Attendance" OnClick="MarkAttendanceButton_Click" />
            <br /><br />
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
    </main>
</asp:Content>
