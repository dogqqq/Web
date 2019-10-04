<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="Web_practice.Store" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body style="background-position: center center; background-image: url('https://localhost:44344/Husky.jpg'); background-size:cover; background-repeat: no-repeat; background-attachment: fixed;">
    <form id="form1" runat="server">
        <div class="auto-style13" style="color: #0066FF; font-weight: 700; font-size: xx-large; background-color: #FFFFCC; text-align: center;">
            歡迎來到茶水間</div>

        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Label ID="Lable_userShow" runat="server" style="background-color: #FFFFFF" Text="顯示使用者資料"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="drinkList" runat="server" AutoPostBack="True" DataSourceID="drinkData" DataTextField="drink_name" DataValueField="drink_id" OnSelectedIndexChanged="drinkList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="label_drinkPrice" runat="server" style="background-color: #FFFFFF" Text="價格"></asp:Label>
                    <asp:Label ID="label_drinkQT" runat="server" style="background-color: #FFFFFF" Text="庫存"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image_drink" runat="server" Height="400px" Width="700px" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:SqlDataSource ID="drinkData" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [drink_name], [drink_id] FROM [drinkData]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="drinkDataselect" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [drink_price], [drink_qt] FROM [drinkData] WHERE ([drink_id] = @drink_id)">
            <SelectParameters>
                <asp:ControlParameter ControlID="drinkList" Name="drink_id" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:DetailsView ID="drinkDetailsView" runat="server" AutoGenerateRows="False" DataSourceID="drinkDataselect" Height="50px" Visible="False" Width="125px">
            <Fields>
                <asp:BoundField DataField="drink_price" HeaderText="drink_price" SortExpression="drink_price" />
                <asp:BoundField DataField="drink_qt" HeaderText="drink_qt" SortExpression="drink_qt" />
            </Fields>
        </asp:DetailsView>

    </form>
</body>
</html>
