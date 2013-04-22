<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Books.aspx.vb" Inherits="Books" Title="Books" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td style="width: 302px; height: 21px">
                <strong><span style="font-size: 11pt; font-family: Verdana">Books &nbsp; &nbsp; &nbsp;&nbsp;</span></strong></td>
            <td style="width: 97px; height: 21px">
                &nbsp;<asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/f_hot.gif"
            Width="16px" />
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Verdana" Font-Size="10pt"
            NavigateUrl="~/bookdetails.aspx?new=?">New book</asp:HyperLink></td>
        </tr>
    </table>
    <asp:GridView ID="gridViewBooks" runat="server" BackColor="White" BorderColor="#CCCCCC"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="157px" Width="410px" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="New_RowCommand" >
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <RowStyle ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:ButtonField ShowHeader="True" Text="View selected book" CommandName="View" ImageUrl="~/images/annoicon.gif" ButtonType="Image" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Wrap="False" />
                <HeaderStyle Wrap="False" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="Edit" ImageUrl="~/images/f_moved.gif"
                ShowHeader="True" Text="Edit selected book">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Wrap="False" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="delete" ImageUrl="~/images/softicon2.gif"
                Text="Delete selected book">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Wrap="False" />
            </asp:ButtonField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
        </Columns>
    </asp:GridView>
    <div style="text-align: left">
        &nbsp;</div>
</asp:Content>

