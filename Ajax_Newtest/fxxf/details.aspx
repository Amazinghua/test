<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="Ajax_Newtest.fxxf.details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>企业详情页面</title>
    <script src="../jquery.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../MJ_JSComment.js"></script>
    <style>
        body{
            background-color:ghostwhite;
        }
        * {
            padding: 0;
            margin: 0;
        }

        .weui-cell {
            padding: 10px 15px;
            position: relative;
            display: flex;
            align-items: center;
            border-bottom: 1px solid #e5e5e5;
        }

        .weui-cells {
            font-size: 14px;
            margin-top: 1.2em;
            background-color: #FFFFFF;
            line-height: 19.7px;
            display:block;
            position:relative;
        }

        .weui-cell__ft {
            text-align: right;
            color: #999999;
        }

        .weui-cell__bd {
            -webkit-box-flex: 1;
            -webkit-flex: 1;
            flex: 1;
        }
    </style>
    <script>
        var id = "<%=GetId%>";
        console.log(id);
        var str = "";
        var table = "";

        $().ready(function () {
            SearchId();
        });
        function SearchId() {
            var jsonObj = {
                type: "byId",
                id: id
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("../Getdata2.ashx", "json", postStr, showDetails);
        }
        //回调函数显示企业详情
        function showDetails(json) {
            str = "";
            total = parseInt(json.total);
            if (total == 0) {

            } else {
                table = json.data[0];
                
                var showList = ["企业名称", "企业信用代码", "登记状态", "成立日期"];
                var test = ["name", "creditCode", "regStatus", "regDate"];
                var $cells = document.getElementById("cells");
                for (var i = 0; i < showList.length; i++) {
                    var p = document.createElement("p");
                    p.setAttribute("style","margin:0")
                    p.innerHTML = showList[i];
                    var div_bd = document.createElement("div");
                    div_bd.setAttribute("class", "weui-cell__bd");
                    div_bd.appendChild(p);
                    var div_cell = document.createElement("div");
                    div_cell.setAttribute("class", "weui-cell");
                    div_cell.appendChild(div_bd);
                    var div_ft = document.createElement("div");
                    div_ft.setAttribute("class", "weui-cell__ft");
                    div_ft.innerHTML = table[test[i]];
                    div_cell.appendChild(div_ft);
                    $cells.appendChild(div_cell);
                }
            }
        }
    </script>
</head>
<body>
    <div id="showDetails" class="h5-root">
        <div id="pageBody">
            <div class="h5-head"></div>
            <div class="h5-body">
                <div id="article">
                    <div class="weui-cells" id="cells">

                    </div>

                </div>
            </div>
        </div>
    </div>
    <div style="margin: 10px;">
        <input type="button" class="btn btn-primary form-control" value="返回首页"/>
    </div>

</body>
</html>
