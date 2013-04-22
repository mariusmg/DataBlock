<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="search.aspx.vb" Inherits="search" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <span style="font-size: 10pt; font-family: Verdana"> &nbsp;&nbsp;<br />
        <div style="z-index: 101; left: 215px; width: 417px; position: absolute; top: 200px;
            height: 1px">
            <table>
                <tr>
                    <td style="width: 898px; height: 26px">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Search for book"></asp:Label></td>
                    <td style="width: 100px; height: 26px">
        <asp:TextBox ID="textSearch" runat="server" Width="224px"></asp:TextBox></td>
                    <td style="width: 134px; height: 26px">
                        <asp:Button
            ID="buttonSearch" runat="server" OnClick="buttonSearch_Click" Text="Search" Width="56px" /></td>
                </tr>
            </table>
        </div>
        <br />
        &nbsp; &nbsp;<br />
        <br />
        </span>
        <table>
            <tr>
                <td style="width: 77px">
                    <span style="font-size: 10pt; font-family: Verdana">&nbsp;
                        <asp:Label ID="labelTitleResults" runat="server" Text="Results :" Visible="False" Width="82px"></asp:Label></span>
                </td>
                <td style="width: 304px" nowrap="noWrap" unselectable="on">
                    <asp:Label ID="labelResult" runat="server" Visible="False" Width="168px" Font-Names="Verdana" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <asp:GridView ID="gridViewResults" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" AutoGenerateColumns="False" OnRowCommand="gridViewResults_RowCommand">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:ButtonField CommandName="View" Text="View" />
                <asp:BoundField DataField="Name" />
            </Columns>
        </asp:GridView>
    &nbsp;
    
</asp:Content>

