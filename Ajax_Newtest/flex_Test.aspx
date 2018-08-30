<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="flex_Test.aspx.cs" Inherits="Ajax_Newtest.flex_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .demo {
            display: flex;
            flex-direction: column;
        }

        .main {
            flex: 1;
        }

        .footer {
            width: 100%;
            height: 120px;
        }
    </style>
</head>
<body>
 <div class="demo">
   <div class="main">这是主要内容</div>
   <div class="footer">这是底部</div>
</div>
</body>
</html>
