<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="EmployeeManagmentSystem.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        
        <div>
              <div class="profile-container">
        <div class="profile-header">
            
            <div class="profile-info">
                <h1>John Doe</h1>
                <p>Web Developer | Tech Enthusiast</p>
            </div>
        </div>

        <div class="bio">
            <h2>About Me</h2>
            <p>I am a passionate web developer with a focus on creating responsive and user-friendly websites. I love exploring new technologies and solving complex problems.</p>
        </div>

        <div class="social-links">
            <h2>Connect with me:</h2>
            <ul>
                <li><a href="#">LinkedIn</a></li>
                <li><a href="#">GitHub</a></li>
                <li><a href="#">Twitter</a></li>
            </ul>
        </div>
    </div>
        </div>
   
    <style>
        /* Resetting some default styles */
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

/* Body and general layout */
body {
    font-family: Arial, sans-serif;
    background-color: #f4f4f9;
    padding: 20px;
}

/* Profile container */
.profile-container {
    max-width: 800px;
    margin: 0 auto;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    padding: 20px;
}

/* Profile header section */
.profile-header {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
}

.profile-img {
    width: 150px;
    height: 150px;
    border-radius: 50%;
    object-fit: cover;
    margin-right: 20px;
}

.profile-info h1 {
    font-size: 28px;
    color: #333;
}

.profile-info p {
    color: #777;
    font-size: 16px;
}

/* Bio section */
.bio {
    margin-bottom: 20px;
}

.bio h2 {
    font-size: 24px;
    margin-bottom: 10px;
    color: #333;
}

.bio p {
    font-size: 16px;
    color: #555;
    line-height: 1.6;
}

/* Social links section */
.social-links h2 {
    font-size: 24px;
    margin-bottom: 10px;
    color: #333;
}

.social-links ul {
    list-style-type: none;
}

.social-links ul li {
    margin-bottom: 10px;
}

.social-links a {
    text-decoration: none;
    font-size: 16px;
    color: #0077b5;
    transition: color 0.3s ease;
}

.social-links a:hover {
    color: #005b8b;
}

    </style>
    </main>

</asp:Content>
