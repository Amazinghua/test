<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Ajax_Newtest.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script src="Check.js"></script>
    <script>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>管理员界面</label>

            <div>
                您的姓名:<input id="name" type="text" />
                <br />
                您的密码:<input id="password" type="text" />
                <br />
                您的id:<input id="ider" type="text" />
               <br />
            </div>
            <input type="button" value="查询" onclick="Check()" />
            <br />
            <input type="button" value="添加" onclick="add()" />
            <br />
            <input type="button" value="更改" onclick="Update()" />
            <div id="haha"> </div>
        </div>
    </form>
</body>
</html>
