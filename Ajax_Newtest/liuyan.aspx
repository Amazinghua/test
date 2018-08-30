<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="liuyan.aspx.cs" Inherits="Ajax_Newtest.liuyan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script>
        function tijiao() {
            var name = $("#name").val();
            var contend = $("#content").val();
            var jsonObj = {
                type: "liuyan",
                name: name,
                contend: contend
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("Getdata2.ashx", "json", postStr, callBack);
        }
        function callBack(data) {
            if (data.result == "111") {
                alert("留言成功");
            } else {
                alert("操作失败");
            }
        }
    </script>
</head>
<body>
    <div class="row">
           姓名：
            <input id="name" type="text" />
        <br />
           内容：
            <textarea id="content" cols="80" rows="12"></textarea>
    </div>
    <input type="button" value="提交" onclick="tijiao()" />
</body>
</html>
