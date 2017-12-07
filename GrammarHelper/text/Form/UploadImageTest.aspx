<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImageTest.aspx.cs" Inherits="text.Form.UploadImageTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:fileupload ID="Fileupload1" runat="server"></asp:fileupload>
      <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <br />
        <br />
        1<br />
        <asp:Image ID="Image1" runat="server" />
        <br />
        <br />
        2<br />
        <asp:Image ID="Image2" runat="server" />
    </div>
    </form>
</body>
</html>
