<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HomeMultimediaLibrary.Search" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel DefaultButton="searchButton" runat="server">
        <div>
            <h2>Search</h2>
            <h3>Here you can have a look into the contents of your multimedia library.
                <br/><br/>
                You can enter multiple search values, separated with a comma.
                <br/><br/>
                Example: If you enter Name = "textbook, script" and Author = "Smith, Doe", the search engine will return items 
                with a name containing 'textbook' or 'script' with the author's name containing 'Smith' or 'Doe'.
            </h3>
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
                    <table class="library-item-table" border="3">
                        <tr>
                            <th>Type
                            </th>
                            <th>Name
                            </th>
                            <th>Author id
                            </th>
                            <th>Publisher
                            </th>
                            <th>Length
                            </th>
                            <th>Summary
                            </th>
                            <th>ISBN
                            </th>
                            <th>Image
                            </th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                        <tr>
                            <td colspan="3" class="result-pages">
                                <asp:DataPager ID="DataPager" runat="server" PagedControlID="ItemListView" PageSize="5">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                            ShowNextPageButton="false" />
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
                        <asp:TextBox BorderWidth="0" SkinID="textBoxCell" ReadOnly="true" ID="TypeTextBox" runat="server" />
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
                        <asp:TextBox BorderWidth="0" SkinID="textBoxCell" ReadOnly="true" ID="lengthTextBox" runat="server" />
                    </td>
                    <td>
                        <%# Eval("Summary") %>
                    </td>
                    <td>
                        <asp:TextBox BorderWidth="0" SkinID="textBoxCell" ReadOnly="true" ID="ISBNTextBox" runat="server" />
                    </td>
                    <td>
                        <asp:Image CssClass="item-image" ID="itemImage" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="editButton" Visible="false" runat="server" Text='Edit' CommandName="Edit" />
                        <asp:Button ID="deleteButton" Visible="false" runat="server" Text='Delete' CommandName="Delete" CssClass="btn-danger" />
                    </td>
                </ItemTemplate>
                <EditItemTemplate>
                    <td>
                        <asp:TextBox BorderWidth="0" ReadOnly="true" ID="TypeTextBox" runat="server" />
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
                        <asp:TextBox ID="lengthTextBox" runat="server" type="number" />
                    </td>
                    <td>
                        <asp:TextBox ID="editSummaryTextBox" TextMode="MultiLine" runat="server" Text='<%# Eval("Summary") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox BorderWidth="0" ID="ISBNTextBox" runat="server" />
                    </td>
                    <td>
                        <asp:Image CssClass="item-image" ID="itemImage" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="updateButton" Visible="false" runat="server" Text='Update' CommandName="Update" />
                        <asp:Button ID="cancelButton" Visible="false" runat="server" Text='Cancel' CommandName="Cancel" />
                    </td>
                </EditItemTemplate>
            </asp:ListView>
        </div>
    </asp:Panel>
</asp:Content>
