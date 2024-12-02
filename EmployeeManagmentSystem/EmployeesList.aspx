<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesList.aspx.cs" Inherits="EmployeeManagmentSystem.EmployeeList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">

     

        <form runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" BorderWidth="1px" BorderStyle="Solid" BorderColor="black"
        GridLines="both" Width="882px" CssClass="grid-table">
        <Columns>
            <asp:BoundField DataField="EmployeeId" HeaderText="Employee id" SortExpression="EmployeeId" />
            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" />
            <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date" SortExpression="JoiningDate" />
            <asp:BoundField DataField="ManagerName" HeaderText="Manager Name" SortExpression="ManagerName" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" SortExpression="Salary" />
             <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />
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
