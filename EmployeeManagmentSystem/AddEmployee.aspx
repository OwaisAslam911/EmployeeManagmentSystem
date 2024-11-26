<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EmployeeManagmentSystem.AddEmployee" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
  <div class="container">
                <div class="page-inner">

                    <div class="row">
                        <form class="row g-3 needs-validation" novalidate>
                            <div class="col-md-4">
                              <label for="validationCustom01" class="form-label">First name</label>
                              <input type="text" class="form-control" id="validationCustom01"  required>
                              <div class="valid-feedback">
                                Looks good!
                              </div>
                            </div>
                            <div class="col-md-4">
                              <label for="validationCustom02" class="form-label">Last name</label>
                              <input type="text" class="form-control" id="validationCustom02"  required>
                              <div class="valid-feedback">
                                Looks good!
                              </div>
                            </div>
                            <div class="col-md-4">
                              <label for="validationCustomUsername" class="form-label">Username</label>
                              <div class="input-group has-validation">
                                <span class="input-group-text" id="inputGroupPrepend">@</span>
                                <input type="text" class="form-control" id="validationCustomUsername" aria-describedby="inputGroupPrepend" required>
                                <div class="invalid-feedback">
                                  Please choose a username.
                                </div>
                              </div>
                            </div>
                            <div class="col-md-6">
                                <label for="validationCustom03" class="form-label">Email</label>
                                <input type="email" class="form-control" id="validationCustom031" required>
                                <div class="invalid-feedback">
                                  Please provide a valid Email.
                                </div>
                              </div>
                              <div class="col-md-6">
                                <label for="validationCustom03" class="form-label">Joining Date</label>
                                <input type="date" class="form-control" id="validationCustom03" required>
                                <div class="invalid-feedback">
                                  Please provide Joining Date.
                                </div>
                              </div>
                            <div class="form-group">
                                <label>Gender</label><br />
                                <div class="d-flex">
                                  <div class="form-check">
                                    <input
                                      class="form-check-input"
                                      type="radio"
                                      name="flexRadioDefault"
                                      id="flexRadioDefault1"
                                    />
                                    <label
                                      class="form-check-label"
                                      for="flexRadioDefault1"
                                    >
                                      Male
                                    </label>
                                  </div>
                                  <div class="form-check">
                                    <input class="form-check-input"  type="radio"
                                      name="flexRadioDefault"
                                      id="flexRadioDefault2"
                                      
                                    />
                                    <label
                                      class="form-check-label"
                                      for="flexRadioDefault2"
                                    >
                                      Female
                                    </label>
                                  </div>
                                </div>
                              </div>
                           
                            <div class="col-12">
                              <div class="form-check">
                                <input class="form-check-input" type="checkbox" value="" id="invalidCheck" required>
                                <label class="form-check-label" for="invalidCheck">
                                  Is a Manager
                                </label>
                               
                              </div>
                            </div>
                            <div class="col-12">
                              <button class="btn btn-primary" type="submit">Submit form</button>
                            </div>
                          </form>
                    </div>
                </div>
            </div>

        
    </main>
</asp:Content>