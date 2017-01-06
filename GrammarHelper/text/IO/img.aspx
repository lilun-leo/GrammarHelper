<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="img.aspx.cs" Inherits="text.IO.img" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <img id="demo" alt="看不清，换一张" title="看不清，换一张" src="../IO/ValidateCode.aspx" style="cursor:pointer" onclick="this.src=this.src+'?r='+Math.random()" />
    </div>
    </form>
</body>
</html>
