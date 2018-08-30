<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Ajax_Newtest.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script src="bootstrap/js/bootstrap.js"></script>
    <script>
        //$(document).ready(function () {
        //    showlist();
        //});
        function showlist() {
            var jsonObj = {
                type: "list"
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("WebForm2.aspx", "json", postStr, list);
        }
        function list(data) {
            var tbody = window.document.getElementById("tbody-result");
            var str = "";
            if (data.result == "888") {
                var data = data.table;
                for (i in data) {
                    str += "<tr>" +
                        "<td>" + data[i]["name"] + "</td>" +
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
    <form id="form1" runat="server">
        <div>
            <input type="button"onclick=" showlist()" />
<table id="tbody-result" class="table table-striped"></table>
            
        </div>
        <div id="page" class=""style="float:right;">
        </div>
    </form>
</body>
</html>
