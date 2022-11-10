<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HomeMultimediaLibrary.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>

    <div class="form-group">
        <asp:Label runat="server" SkinId="labelTxt" AssociatedControlID="PreferredColor" CssClass="col-md-2 control-label">Preferred color</asp:Label>
        <div class="col-md-4">
            <asp:DropDownList ID="PreferredColor" runat="server"></asp:DropDownList>
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <asp:Button SkinId="buttonTxt" runat="server" OnClick="SetPreferredColor_Click" Text="Set" CssClass="btn btn-default" />
        </div>
    </div>

</asp:Content>
