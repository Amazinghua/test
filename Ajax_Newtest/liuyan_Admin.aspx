<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="liuyan_Admin.aspx.cs" Inherits="Ajax_Newtest.liuyan_Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>

    <style>
        .row {
            padding: 10px;
        }

        th {
            text-align: center;
        }
    </style>
    <script>
        window.onload= function listing() {
                var jsonObj = {
                    type: "list"
                }
                var postStr = JSON.stringify(jsonObj);
                mj_ajax("Getdata2.ashx", "json", postStr, list);
            }

        function list(data) {
            var tbody = window.document.getElementById("tbody-result");
            var str = "";
            if (data.result == "888") {
                var data = data.foodid_dt;
                for (i in data) {
                    str += "<tr>" +
                        "<td>" + data[i].id + "</td>" +
                        "<td>" + data[i].contend + "</td>" +
                        "<td>" + data[i].name + "</td>" +
                        "<td>" + data[i].time + "</td>" +
                        "<td><a>删除</a></td>"+
                        "</tr>";
                }
                tbody.innerHTML = str;
            }
        }
    </script>
</head>
<body>
    <div class="row">

        <table class="table-bordered table" id="table-result">
            <thead>
                <tr>
                    <th style="width:20%">id</th>
                    <th style="width:30%">内容</th>
                    <th style="width:10%">姓名</th>
                    <th style="width:20%">时间</th>
                    <th style="width:20%">删除</th>
                </tr>
            </thead>
            <tbody id="tbody-result">
            </tbody>
        </table>

    </div>
</body>
</html>
