<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newTest.aspx.cs" Inherits="Ajax_Newtest.newTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script src="bootstrap/js/bootstrap.js"></script>
        <script>
            $(document).ready(function () {
                showlist();
            });
        function showlist() {
            var jsonObj = {
                type: "list"
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("dataso.ashx", "json", postStr, list);
        }
        function list(data) {
            var tbody = window.document.getElementById("tbody-result");
            var str = "";
            if (data.result == "888") {
                var data = data.table;
                for (i in data) {
                    str += "<tr>" +
                        "<td>" + data[i].name + "</td>" +
                        "<td>" + data[i].password + "</td>" +
                        "<td>" + data[i].year + "</td>" +
                        "<td>" + data[i].sex + "</td>" +
                        "</tr>";
                }
                tbody.innerHTML = str;
            }
        }
    </script>
</head>
<body>
    <div >
        <table id="tbody-result" class="table table-striped"></table>
    </div>
<table class="txt" width="100%" border="0">
     <tbody>
    <tr>
     <td>
     <asp:hyperlink id="lnkPrev" runat="server">上页</asp:hyperlink>
    <asp:hyperlink id="lnkNext" runat="server">下页</asp:hyperlink>
     第<asp:label id="lblCurrentPage" runat="server"></asp:label>页
     共<asp:label id="lblTotalPage" runat="server"></asp:label>页
    </td>
    </tr>
     </tbody>
     </table>
</body>
</html>
