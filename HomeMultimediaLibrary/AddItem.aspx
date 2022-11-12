<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="HomeMultimediaLibrary.AddItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Add a new item</h2>
        <h3>You can add a new item here.</h3>
    </div>

    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="itemType" Text="Item type:"></asp:Label>
        <asp:DropDownList ID="itemType" runat="server" CssClass="form-control item-type-drop-down"></asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="name" Text="Name:"></asp:Label>
        <asp:TextBox ID="name" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="author" Text="Author:"></asp:Label>
        <asp:TextBox ID="author" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="publisher" Text="Publisher:"></asp:Label>
        <asp:TextBox ID="publisher" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
</asp:Content>
