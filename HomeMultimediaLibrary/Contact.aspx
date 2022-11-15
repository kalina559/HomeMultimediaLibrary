<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HomeMultimediaLibrary.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Contact information</h3>
    <address>
        plac Politechniki 1<br />
        00-661 Warszawa<br />
        <abbr title="Phone">P:</abbr>
        +48 123456789
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">01122348@pw.edu.pl</a><br />
    </address>

</asp:Content>
