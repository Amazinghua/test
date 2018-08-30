<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ajax_Newtest.Ajax_demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="jquery.min.js"></script>
    <script src="YanZheng.js"></script>
    <script src="Comment.js"></script>
    <script src="MJ_JSComment.js"></script>
</head>
<body>
    <script src="MJ_JSComment.js"></script>
    <form id="form1" runat="server">
        <div>
             <div>
            <label>姓名：</label>
            <input type="text" id="NameId" />
        </div>
        <br />
        <div>
            <label>
                密码：
            </label>
            <input type="text"  id="PassWord" />
        </div>
        <br />
        <div>
            <label>验证：</label>
            <input type="text" id="txtVerifyCode" />
            <img src="index.ashx" id="Verify_codeImag" alt="点击切换验证码" style="cursor: pointer" width="55" height="25" title="点击切换验证码" onclick="ToggleCode(this.id, 'index.ashx');return false;" />
        </div>
        <br />
        <input type="button" value="验证" onclick="postAjax()" />
        <br />
            <input type="button" value="登陆" onclick="getMsg()" />
            <br />
            <input type="button" value="注册" onclick="" />
            <div id="ab"></div>
            <div  id="bc"></div>
            <div id="ac"></div>
        </div>
    </form>
</body>
</html>
