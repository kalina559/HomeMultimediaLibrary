<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HomeMultimediaLibrary.Search" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel DefaultButton="searchButton" runat="server">
        <div>
            <h2>Search</h2>
            <h3>Here you can have a look into the contents of your multimedia library.</h3>
        </div>
        <div class="search-results-div">
            <!-- filters -->
            <div>
                <asp:Panel ID="filtersLayout" runat="server" Visible="false">
                    <asp:Label runat="server" AssociatedControlID="itemTypeDropDown" Text="Item type:"></asp:Label>
                    <asp:DropDownList ID="itemTypeDropDown" runat="server" CssClass="form-control item-input" AutoPostBack="true"></asp:DropDownList>

                    <asp:Label runat="server" AssociatedControlID="nameTextBox" Text="Name:"></asp:Label>
                    <asp:TextBox ID="nameTextBox" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label runat="server" AssociatedControlID="authorTextBox" Text="Author:"></asp:Label>
                    <asp:TextBox ID="authorTextBox" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label runat="server" AssociatedControlID="keywordsTextBox" Text="Keywords:"></asp:Label>
                    <asp:TextBox ID="keywordsTextBox" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label runat="server" AssociatedControlID="publisherTextBox" Text="Publisher:"></asp:Label>
                    <asp:TextBox ID="publisherTextBox" runat="server" CssClass="form-control"></asp:TextBox>

                </asp:Panel>
                <asp:Button SkinID="buttonTxt" ID="setFiltersButton" runat="server" OnClick="OnSetFiltersClick" Text="Set filters" CssClass="btn btn-default" />

                <asp:Button SkinID="buttonTxt" ID="searchButton" runat="server" OnClick="OnSearchButtonClick" Text="Search" CssClass="btn btn-default" />
            </div>
            <asp:ListView ID="ItemListView" runat="server" ItemPlaceholderID="itemPlaceholder" 
                OnItemEditing="OnItemEditing" OnItemDataBound="OnItemDataBound" 
                OnItemCanceling="OnItemCanceling" OnItemUpdating="OnItemUpdating" OnItemDeleting="OnItemDeleting" OnPagePropertiesChanging="OnPageChanging">
                <LayoutTemplate>
                    <table class="library-item-table">
                        <tr>
                            <th>Type
                            </th>
                            <th>Name
                            </th>
                            <th>Author id
                            </th>
                            <th>Publisher
                            </th>
                            <th>Summary
                            </th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                        <tr>
                            <td colspan="3" class="result-pages">
                                <asp:DataPager ID="DataPager" runat="server" PagedControlID="ItemListView" PageSize="5">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                            ShowNextPageButton="false"/>
                                        <asp:NumericPagerField ButtonType="Link" />
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td>
                        <%# "type" %>
                    </td>
                    <td>
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <%# Eval("Author") %>
                    </td>
                    <td>
                        <%# Eval("Publisher") %>
                    </td>
                    <td>
                        <%# Eval("Summary") %>
                    </td>
                    <td>
                        <asp:Button ID="editButton" runat="server" Text='Edit' CommandName="Edit" />
                        <asp:Button ID="deleteButton" runat="server" Text='Delete' CommandName="Delete" CssClass="btn-danger" />
                    </td>
                </ItemTemplate>
                <EditItemTemplate>
                    <td>
                        <asp:TextBox ID="editTypeTextBox" runat="server" Enabled="false" Text='type'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="editNameTextBox" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="editAuthorTextBox" runat="server" Text='<%# Eval("Author") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="editPublisherTextBox" runat="server" Text='<%# Eval("Publisher") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="editSummaryTextBox" TextMode="MultiLine" runat="server" Text='<%# Eval("Summary") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="updateButton" runat="server" Text='Update' CommandName="Update" />
                        <asp:Button ID="cancelButton" runat="server" Text='Cancel' CommandName="Cancel" />
                    </td>
                </EditItemTemplate>
            </asp:ListView>
        </div>
    </asp:Panel>
</asp:Content>
