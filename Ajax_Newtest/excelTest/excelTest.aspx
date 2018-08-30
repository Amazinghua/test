<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="excelTest.aspx.cs" Inherits="Ajax_Newtest.excelTest.excelTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="../MJ_JSComment.js"></script>
            <script>
                function tryTest() {
                    var jsonObj = {
                        type: "excel"
                    }
                    var postStr = JSON.stringify(jsonObj);
                    mj_ajax("Getdata2.ashx", "json", postStr, backto);
                }
                function backto(data) {
                    console.log(data);
                }
    </script>
</head>
<body>
    <div id="test">
        <input type="button" value="导出" onclick="tryTest()" />
    </div>

</body>
</html>
