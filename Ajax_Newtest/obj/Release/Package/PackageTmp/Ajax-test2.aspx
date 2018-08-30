<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajax-test2.aspx.cs" Inherits="Ajax_Newtest.Ajax_test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="jquery-1.9.1.js"></script>
    <script>

        function aa() {
            $.ajax({
                type: 'POST',
                url: 'Handler1.ashx',
                data: { "name": $("#name").val() },
                success: function (data) {
                    if (1 == data)
                        alert('Login success');
                    else
                        alert('login fail');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" name="name" id="name" />
            <input type="button" name="test" id="test" value="validate" onclick="aa()" />
        </div>
    </form>
</body>
</html>
