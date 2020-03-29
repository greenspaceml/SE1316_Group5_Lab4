<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReturnGUI.aspx.cs" Inherits="SE1316_Group5_Lab4.GUI.ReturnGUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 184px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="lbTitle" runat="server" Font-Bold="True" Font-Size="Large" Text="Return a copy"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <table>
            <tr>
                <td>Borower Number: </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                </td>
                <td><asp:Button ID="Button1" runat="server" Text="Check member" OnClick="Button1_Click" /></td>
            </tr>
            <tr>
                <td>Name: </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>
    </p>
    <p>
        <asp:GridView runat="server" Width="491px" AllowPaging="True" AutoGenerateColumns="False" PageSize="5" ID="GridView1">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("ID") %>' OnClick="LinkButton1_Click">Select</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="copyNumber" HeaderText="Copy Number" />
                <asp:BoundField DataField="borrowerNumber" HeaderText="Borrower Number" />
                <asp:BoundField DataField="borrowedDate" HeaderText="Borrowed Date" />
                <asp:BoundField DataField="dueDate" HeaderText="Due Date" />
                <asp:BoundField DataField="returnedDate" HeaderText="Returned Date" />
                <asp:BoundField DataField="fineAmount" HeaderText="Fine Amount" />
            </Columns>
            <SelectedRowStyle BackColor="#FFCC00" />
        </asp:GridView>
    </p>
    <p>
        Return date:
    </p>
    <p>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="312px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
            <DayStyle Width="14%" />
            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
            <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
            <TodayDayStyle BackColor="#CCCC99" />
        </asp:Calendar>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="Fine amount:"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="197px" ReadOnly="True"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm fine" Width="161px" Enabled="False" OnClick="btnConfirm_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnReturn" runat="server" Text="Return" Width="108px" Enabled="False" OnClick="btnReturn_Click" />
    </p>
    <p>
    </p>
</asp:Content>
