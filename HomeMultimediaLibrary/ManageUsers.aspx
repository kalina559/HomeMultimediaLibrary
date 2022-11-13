<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="HomeMultimediaLibrary.ManageUsers" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server">
        <div>
            <h2>Manage users</h2>
            <h3>Here you can change the role of users or remove them.</h3>
        </div>
        <div class="search-results-div">
           
            <asp:ListView ID="UsersListView" runat="server" ItemPlaceholderID="userPlaceholder"
                OnItemEditing="OnItemEditing" OnItemDataBound="OnItemDataBound"
                OnItemCanceling="OnItemCanceling" OnItemUpdating="OnItemUpdating" OnItemDeleting="OnItemDeleting" OnPagePropertiesChanging="OnPageChanging">
                <LayoutTemplate>
                    <table class="library-item-table">
                        <tr>
                            <th>Name
                            </th>
                            <th>Role
                            </th>                            
                        </tr>
                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                        <tr>
                            <td colspan="3" class="result-pages">
                                <asp:DataPager ID="DataPager" runat="server" PagedControlID="UsersListView" PageSize="5">
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
                        <asp:PlaceHolder runat="server" ID="userPlaceholder"></asp:PlaceHolder>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td>
                        <%# Eval("UserName") %>
                    </td>
                    <td>
                        <asp:TextBox BorderWidth="0" ID="roleTextbox" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>                    
                    <td>
                        <asp:Button ID="editButton" runat="server" Text='Edit' CommandName="Edit" />
                        <asp:Button ID="deleteButton" runat="server" Text='Delete' CommandName="Delete" CssClass="btn-danger" />
                    </td>
                </ItemTemplate>
                <EditItemTemplate>
                    <td>
                        <asp:TextBox ID="editNameTextBox" runat="server" Text='<%# Eval("UserName") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="editRoleDropDownList"></asp:DropDownList>
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
