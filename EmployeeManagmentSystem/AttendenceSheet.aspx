<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AttendenceSheet.aspx.cs" Inherits="EmployeeManagmentSystem.AttendenceSheet" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <form runat="server">
  <asp:GridView ID="AttendanceGridView" runat="server" AutoGenerateColumns="False" 
    CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped">
    <Columns>
        <asp:BoundField DataField="AttendanceId" HeaderText="Attendance ID" SortExpression="AttendanceId" />
        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" />
        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
        <asp:BoundField DataField="AttendanceType" HeaderText="Attendance Type" SortExpression="AttendanceType" />
        
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
    </Columns>
</asp:GridView>

            </form>

    <style>
.grid-table td, .grid-table th {
    border: 1px solid #cccccc; 
    padding: 8px; 
}

.grid-table th {
    background-color: #f4f4f4; 
    font-weight: bold;
}


.grid-table tr:hover {
    background-color: #e9e9e9; 
}
</style>
    </main>
</asp:Content>
