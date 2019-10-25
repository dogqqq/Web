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
        .auto-style2 {
            font-size: large;
        }
        .auto-style3 {
            height: 32px;
        }
        .auto-style4 {
            font-size: large;
            background-color: #FFFFFF;
        }
        .auto-style6 {
            width: 619px;
        }
        .auto-style7 {
            background-color: #FFFFFF;
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
                    <asp:Label ID="Lable_userShow" runat="server" style="background-color: #FFFFFF" Text="顯示使用者資料" CssClass="auto-style2"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="Btn_order" runat="server" CssClass="auto-style2" Text="前往選購" OnClick="Btn_order_Click" />
                    <asp:Button ID="Btn_refresh" runat="server" CssClass="auto-style2" Text="清空表單" OnClick="Btn_refresh_Click" />
                </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="drinkList" runat="server" AutoPostBack="True" DataSourceID="drinkData" DataTextField="drink_name" DataValueField="drink_id" OnSelectedIndexChanged="drinkList_SelectedIndexChanged" CssClass="auto-style2">
                    </asp:DropDownList>
                    <asp:Label ID="label_drinkPrice" runat="server" style="background-color: #FFFFFF" Text="價格:X元" CssClass="auto-style2"></asp:Label>
                    <asp:Label ID="label_drinkQT" runat="server" style="background-color: #FFFFFF" Text="庫存:X個" CssClass="auto-style2"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style6">
                    <asp:DropDownList ID="cupList" runat="server" CssClass="auto-style2" Visible="False">
                    </asp:DropDownList>
                    <asp:Label ID="cupLB" runat="server" CssClass="auto-style4" Text="杯" Visible="False"></asp:Label>
                    <asp:DropDownList ID="iceList" runat="server" CssClass="auto-style2" Visible="False">
                        <asp:ListItem>正常</asp:ListItem>
                        <asp:ListItem>少冰</asp:ListItem>
                        <asp:ListItem>小碎冰</asp:ListItem>
                        <asp:ListItem>完全去冰</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="sweetList" runat="server" CssClass="auto-style2" Visible="False">
                        <asp:ListItem>正常</asp:ListItem>
                        <asp:ListItem>少糖</asp:ListItem>
                        <asp:ListItem>半糖</asp:ListItem>
                        <asp:ListItem>微糖</asp:ListItem>
                        <asp:ListItem>無糖</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="addDrinkBtn" runat="server" CssClass="auto-style2" Enabled="False" OnClick="addDrinkBtn_Click" Text="添加" Visible="False" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Image ID="Image_drink" runat="server" Height="400px" Width="700px" />
                </td>
                <td>
                    <asp:GridView ID="orderItemGridView1" runat="server" AutoGenerateColumns="False" CssClass="auto-style7" DataKeyNames="編號" DataSourceID="orderItemDataSource">
                        <Columns>
                            <asp:BoundField DataField="編號" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="編號" />
                            <asp:BoundField DataField="飲料" HeaderText="飲料" SortExpression="飲料" />
                            <asp:BoundField DataField="甜度" HeaderText="甜度" SortExpression="甜度" />
                            <asp:BoundField DataField="冰塊" HeaderText="冰塊" SortExpression="冰塊" />
                            <asp:BoundField DataField="數量" HeaderText="數量" SortExpression="數量" />
                            <asp:BoundField DataField="小計" HeaderText="小計" ReadOnly="True" SortExpression="小計" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="orderItemDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" InsertCommand="INSERT INTO order_itemData(drink_id, order_id, num, sweet, ice) VALUES (@drink_id, @order_id, @num, @sweet, @ice)" SelectCommand="SELECT order_itemData.orderItem_id AS 編號, drinkData.drink_name AS 飲料, order_itemData.sweet AS 甜度, order_itemData.ice AS 冰塊, order_itemData.num AS 數量, order_itemData.num * drinkData.drink_price AS 小計 FROM drinkData INNER JOIN order_itemData ON drinkData.drink_id = order_itemData.drink_id WHERE (order_itemData.order_id = @order_id)" OnSelecting="orderItemDataSource_Selecting">
                        <InsertParameters>
                            <asp:ControlParameter ControlID="drinkList" Name="drink_id" PropertyName="SelectedIndex" />
                            <asp:SessionParameter Name="order_id" SessionField="order_id" />
                            <asp:ControlParameter ControlID="cupList" Name="num" PropertyName="SelectedValue" Size="999" />
                            <asp:ControlParameter ControlID="sweetList" Name="sweet" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="iceList" Name="ice" PropertyName="SelectedValue" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="order_id" SessionField="order_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <br />
        <asp:SqlDataSource ID="drinkData" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT drink_name, drink_id FROM [drinkData]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="drinkDataselect" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT drink_price, drink_qt FROM [drinkData] WHERE (drink_id = @drink_id)" InsertCommand="INSERT INTO orderData(order_time, order_phone) VALUES (GETDATE(), @user_phone)" >
            <InsertParameters>
                <asp:SessionParameter Name="user_phone" SessionField="phone" />
            </InsertParameters>
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
