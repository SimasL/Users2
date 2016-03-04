<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Users.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="DB_Label" runat="server" Text="DB: " ></asp:Label>
        <asp:TextBox ID="DB_TextBox" runat="server"></asp:TextBox>
        <asp:Label ID="User_Label" runat="server" Text="User: " style="margin-left: 20px"></asp:Label>
        <asp:TextBox ID="User_TextBox" runat="server"></asp:TextBox>
        <asp:Label ID="Pass_Label" runat="server" Text="Pass: " style="margin-left: 20px"></asp:Label>
        <input id="Pass_TextBox" type="password" runat="server" />
        <asp:Button ID="Connection_Button" runat="server" Text="Test Connection" style="margin-left: 20px" OnClick="Connection_Button_Click" />
        <asp:Label ID="Status_Label" runat="server" Text="" style="margin-left: 20px"></asp:Label>

    </div>
    <div>
    
    </div>
    <div>
    
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
