<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EmployeeManagmentSystem.AddEmployee" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="container">
            <div class="page-inner">
                <div class="row">
                    <form runat="server">
                       
                        <div class="col-md-4">
                            <label for="validationCustom01" class="form-label">Employee Name</label>
                            <asp:TextBox ID="EmployeeName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <label for="validationCustomUsername" class="form-label">Username</label>
                            <div class="input-group has-validation">
                                <span class="input-group-text" id="inputGroupPrepend">@</span>
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="validationCustom03" class="form-label">Password</label>
                            <asp:TextBox ID="Password" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="validationCustom03" class="form-label">Phone</label>
                            <asp:TextBox ID="Phone" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="validationCustom03" class="form-label">Email</label>
                            <asp:TextBox ID="Email" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="validationCustom03" class="form-label">Joining Date</label>
                            <asp:TextBox ID="JoiningDate" runat="server" CssClass="form-control" required="true" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Gender</label><br />
                            <div class="d-flex">
                                <div class="form-check">
                                    <asp:RadioButton ID="GenderMale" runat="server" CssClass="form-check-input" GroupName="Gender" />
                                    <label class="form-check-label" for="GenderMale">Male</label>
                                </div>
                                <div class="form-check">
                                    <asp:RadioButton ID="GenderFemale" runat="server" CssClass="form-check-input" GroupName="Gender" />
                                    <label class="form-check-label" for="GenderFemale">Female</label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="validationCustom03" class="form-label">Assign Manager</label>
                            <asp:DropDownList ID="managerDropdownList"   AppendDataBoundItems="True" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                           
                        </div>

                        <div class="col-12">
                            <div class="form-check">
                                <asp:CheckBox ID="IsManager" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label" for="IsManager">Is a Manager</label>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="validationCustom03" class="form-label">Salary</label>
                            <asp:TextBox ID="Salary" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>

                        <div class="col-12">
                            <asp:Button ID="SubmitButton" runat="server" Text="Submit form" OnClick="SubmitButton_Click" CssClass="btn btn-primary" />
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
