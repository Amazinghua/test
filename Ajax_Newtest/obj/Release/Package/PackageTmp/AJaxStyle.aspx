<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AJaxStyle.aspx.cs" Inherits="Ajax_Newtest.AJaxStyle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="jquery-1.9.1.js"></script>
    <script>
        function postAjax() {
            var VerifyCodeValue = $("#txtVerifyCode").val();
            $.ajax({
                type: 'POST',
                dataType: "text",
                url: "AJaxStyle.aspx",
                data: "action=comparison&VerifyCode=" + VerifyCodeValue,
                cache: false,
                async: false,
                success: function (data) {
                   
                    alert("您好，" + data);
                    
                },

                error: function (err) {
                    alert("err:" + err);
                }
            })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text"  id="txtVerifyCode"/>
            <br />
            <input type="button" value="提交"  onclick="postAjax()"/>
        </div>
    </form>
</body>
</html>
