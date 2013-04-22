<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bookdetails.aspx.vb" Inherits="bookdetails" Title="Book details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span style="font-size: 10pt; font-family: Verdana"></span>
    <asp:Label ID="labelAction" runat="server" Font-Names="Verdana" Font-Size="10pt"
        Width="184px" Font-Bold="True"></asp:Label>&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
    <div style="z-index: 101; left: 24px; width: 544px; position: absolute; top: 208px;
        height: 336px">
        <asp:Label ID="labelError" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
            ForeColor="Red" Text="Label" Visible="False" Width="488px"></asp:Label>
        <br />
        <br />
        <table>
            <tr>
                <td style="width: 37px">
                    <span style="font-size: 10pt; font-family: Verdana">
                        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label></span></td>
                <td style="width: 324px">
                    <asp:TextBox ID="textBoxName" runat="server" Width="352px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 37px">
                    <span style="font-size: 10pt; font-family: Verdana">
                        <asp:Label ID="Label2" runat="server" Text="Pages "></asp:Label></span></td>
                <td style="width: 324px">
                    <asp:TextBox ID="textBoxPage" runat="server" Width="352px" ReadOnly="True" Wrap="False" EnableTheming="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 37px">
                    <asp:Label ID="Label3" runat="server" Font-Size="10pt" Text="ISBN"></asp:Label>
                </td>
                <td style="width: 324px">
                    <asp:TextBox ID="textBoxISBN" runat="server" Width="352px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 37px">
                    <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="10pt" Text="Genre"></asp:Label></td>
                <td style="width: 324px">
                    <asp:TextBox ID="textBoxGenre" runat="server" Width="352px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 37px">
                    <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="10pt" Text="Grade"></asp:Label></td>
                <td style="width: 324px">
                    <asp:TextBox ID="textBoxGrade" runat="server" Width="352px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 37px; height: 21px;">
                    <span style="font-size: 10pt; font-family: Verdana">
                        <asp:Label ID="labelAuthors" runat="server" Font-Bold="False" Text="Authors"></asp:Label><br />
                    </span></td>
                <td style="width: 324px; height: 21px;">
                    <asp:CheckBoxList ID="listCheckboxes" runat="server" Height="1px" Width="304px">
                    </asp:CheckBoxList></td>
            </tr>
        </table>
        <br />
    <br />
    <span style="font-size: 10pt; font-family: Verdana">
        <br />
        <br />
        <br />
    <asp:Button ID="buttonSubmit" runat="server" Text="Submit" OnClick="buttonSubmit_Click" Width="64px" /></span></div>
    <br />
    &nbsp; <span style="font-size: 10pt; font-family: Verdana">&nbsp;</span><span style="font-size: 10pt;
            font-family: Verdana">
            <br />
            &nbsp;&nbsp;</span><br /><span style="font-size: 10pt; font-family: Verdana"></span><br />
    <br />
    <div style="text-align: center">
        &nbsp;</div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

