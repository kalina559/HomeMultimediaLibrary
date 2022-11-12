<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="HomeMultimediaLibrary.AddItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Add a new item</h2>
        <h3>You can add a new item here.</h3>
    </div>

    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="itemTypeDropDown" Text="Item type:"></asp:Label>
        <asp:DropDownList ID="itemTypeDropDown" runat="server" CssClass="form-control item-input" OnSelectedIndexChanged="OnItemTypeChanged" AutoPostBack="true"></asp:DropDownList>

        <asp:Label runat="server" AssociatedControlID="nameTextBox" Text="Name:"></asp:Label>
        <asp:TextBox ID="nameTextBox" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Label runat="server" AssociatedControlID="authorTextBox" Text="Author:"></asp:Label>
        <asp:TextBox ID="authorTextBox" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Label runat="server" AssociatedControlID="publisherTextBox" Text="Publisher:"></asp:Label>
        <asp:TextBox ID="publisherTextBox" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Label ID="ISBNLabel" runat="server" AssociatedControlID="ISBNTextBox" Text="ISBN:"></asp:Label>
        <asp:TextBox ID="ISBNTextBox" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Label ID="pagesLabel" runat="server" AssociatedControlID="pagesTextBox" Text="Pages:"></asp:Label>
        <asp:TextBox ID="pagesTextBox" runat="server" type="number" CssClass="form-control item-input"></asp:TextBox>

        <asp:Label ID="lengthMinutesLabel" runat="server" AssociatedControlID="lengthMinutesTextBox" Text="Length in minutes:"></asp:Label>
        <asp:TextBox ID="lengthMinutesTextBox" runat="server" type="number" CssClass="form-control item-input"></asp:TextBox>

        <asp:Label runat="server" AssociatedControlID="summaryTextBox" Text="Summary:"></asp:Label>
        <asp:TextBox ID="summaryTextBox" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Label runat="server" AssociatedControlID="keywordsTextBox" Text="Keywords (separate with a comma):"></asp:Label>
        <asp:TextBox ID="keywordsTextBox" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Label runat="server" AssociatedControlID="tableOfContentsTextBox" Text="Table of contents:"></asp:Label>
        <asp:TextBox ID="tableOfContentsTextBox" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Button SkinID="buttonTxt" ID="addItemButton" runat="server" OnClick="AddItemClick" Text="Add" CssClass="btn btn-default save-button" />

    </div>
</asp:Content>
