<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HomeMultimediaLibrary._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Home Multimedia Library</h1>
        <p class="lead">Home Multimedia Library allows you to store and view the contents of your personal multimedia library.</p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Getting started</h2>
            <p>
                You need to create an account to be able to view the contents of the library. If you want to be able to add items to the
                library, you need to reach out the the administrators.
            </p>
        </div>
        <div class="col-md-6">
            <h2>Looking for a specific item?</h2>
            <p>
                It's possible to filter the library items to make it a lot easier to find what you're looking for!
            </p>
        </div>
    </div>

</asp:Content>
