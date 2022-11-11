<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contents.aspx.cs" Inherits="HomeMultimediaLibrary.Contents" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2><%: Title %>.</h2>
        <h3>Here you can have a look into the contents of your multimedia library.</h3>
    </div>
    <div class="search-results-div">
        <asp:ListView ID="ItemListView" runat="server" ItemPlaceholderID="itemPlaceholder" >
            <LayoutTemplate>
                <table class="library-item-table">
                    <tr>
                        <th>Item id
                        </th>
                        <th>Name
                        </th>
                        <th>Author id
                        </th>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                    <tr>
                        <td colspan="3" class="result-pages">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ItemListView" PageSize="5">
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
                    <%# Eval("Id") %>
                </td>
                <td>
                    <%# Eval("Name") %>
                </td>
                <td>
                    <%# Eval("Author") %>
                </td>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
