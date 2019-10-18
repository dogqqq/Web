<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_practice.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style10 {
            width: 44px;
            height: 28px;
        }
        .auto-style12 {
            height: 28px;
            width: 45px;
        }
        .auto-style13 {
            text-align: center;
        }
        .auto-style23 {
            margin-left: 0px;
        }
        .auto-style32 {
            width: 1170px;
            height: 28px;
            text-align: center;
        }
        .auto-style34 {
            width: 44px;
            text-align: center;
            height: 26px;
        }
        .auto-style35 {
            width: 1170px;
            text-align: center;
            height: 26px;
        }
        .auto-style36 {
            text-align: center;
            width: 45px;
            height: 26px;
        }
        .auto-style37 {
            width: 1170px;
            height: 28px;
        }
    </style>
</head>
<body style="background-position: center center; background-image: url('https://localhost:44344/Husky.jpg'); background-size:cover; background-repeat: no-repeat; background-attachment: fixed;">
    <form id="form1" runat="server">
        <div class="auto-style13" style="color: #0066FF; font-weight: 700; font-size: xx-large; background-color: #FFFFCC">
            歡迎來到茶水間</div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style34"></td>
                <td class="auto-style35">
                    <asp:Label ID="lable_id" runat="server" Text="帳號: "></asp:Label>
                    <asp:TextBox ID="text_id" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style36"></td>
            </tr>
            <tr>
                <td class="auto-style34"></td>
                <td class="auto-style35">
                    <asp:Label ID="lable_passwd" runat="server" Text="密碼: "></asp:Label>
                    <asp:TextBox ID="text_passwd" runat="server" OnTextChanged="text_passwd_TextChanged" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style36"></td>
            </tr>
            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style32">
                    <asp:Button ID="btn_login" runat="server" Height="21px" OnClick="Button1_Click" Text="登入" Width="46px" />
                    <asp:LinkButton ID="LinkBtn_store" runat="server" OnClick="LinkBtn_store_Click" Visible="False" PostBackUrl="~/Store.aspx">進入商店</asp:LinkButton>
                </td>
                <td class="auto-style12"></td>
            </tr>
            <tr style="position: relative">
                <td class="auto-style10"></td>
                <td class="auto-style37">
                    <asp:DetailsView ID="DetailsView_account" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSource_user" EmptyDataText="帳號密碼錯誤" Height="50px" Visible="False" Width="152px" CssClass="auto-style23">
                        <Fields>
                            <asp:BoundField DataField="user_name" HeaderText="user_name" SortExpression="user_name" />
                         <asp:BoundField DataField="user_deposit" HeaderText="user_deposit" SortExpression="user_deposit" />
                            <asp:BoundField DataField="user_phone" HeaderText="user_phone" SortExpression="user_phone" />
                      </Fields>
                    </asp:DetailsView>
                </td>
                <td class="auto-style12"></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource_user" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT user_name, user_deposit, user_phone FROM userData WHERE (user_name = @user_name) AND (user_password = @user_password)">
            <SelectParameters>
                <asp:ControlParameter ControlID="text_id" Name="user_name" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="text_passwd" Name="user_password" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
