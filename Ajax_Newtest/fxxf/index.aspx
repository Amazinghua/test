<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ajax_Newtest.fxxf.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>企业信息</title>
    <script src="../jquery.min.js"></script>
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../MJ_JSComment.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: ghostwhite;
        }

        * {
            margin: 0;
            padding: 0;
        }

        .h-name {
            font-size: 14px;
            color: #222222;
        }

        #foundItems > li {
            color: #98989f;
            font-size: 12px;
            padding: 6px 8px;
            line-height: 200%;
            border-bottom: 1px solid #E6E9EE;
        }

        .h5-body ul {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .h-info {
            align-items: center;
        }

            .h-info div {
                width: 50%;
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
            }

        #foundItems > li .h-info > div {
            width: 45%;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            display: inline-block;
        }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .highlight {
            color: red;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border-bottom: 1px solid #ddd;
        }
    </style>
    <script>
        $().ready(function () {
            Loading();
        });
    </script>
</head>
<body>
    <div class="searchPage">
        <table class="table">
            <tr>
                <td style="vertical-align: middle">
                    <input id="search" class="form-control" type="text" placeholder="商家名称" />
                </td>
                <td id="btn1" style="vertical-align: middle;">
                    <input type="button" class="form-control btn btn-primary" value="查询" onclick="findCompany()" />
                </td>
            </tr>
        </table>
        
        <div id="pagebody" style="transition-timing-function: cubic-bezier(0.1, 0.57, 0.1, 1); transition-duration: 0ms; transform: translate(0px, 0px) translateZ(0px);">
            <div class="h-body" style="padding-top: 0;">
                <ul id="foundItems">
                </ul>
            </div>
        </div>
        <div id="dispaly" class="page">
            <input id="num" type="hidden" value="1" />
            <input id="allpage" type="hidden" />
            <a href="javascript:void(0)" onclick="up()">上页</a><a href="javascript:void(0)" onclick="down()">下页</a><span
                id="page"></span>
        </div>
    </div>
    <script>
        var total = "";
        var table = "";
        var str = "";
        var tbody = document.getElementById("foundItems");
        var content = "";

        //页面加载时加载数据
        function Loading() {

            var jsonObj = {
                type: "all",
                pageNow: "1"
            }
            var postStr = JSON.stringify(jsonObj);

            mj_ajax("../Getdata2.ashx", "json", postStr, Addmsg);

        }
        //回调函数加载页面
        function Addmsg(json) {
            total = parseInt(json.total);
            table = json.data;
            str = "";

            if (total == 0) {
                str = '<li style="border-bottom: none;padding-top: 5%;"><img  src="imges/show.png" style="display: block;width: 21%;margin: 8% auto;"/><div style="text-align: center;font-size: 15px;">抱歉，没有查询到此企业</div></li>'
            } else {

                for (var i in table) {
                    str += '<li data-id="' + table[i].id + '">' + '<div class="h-name">' + table[i].name + '</div>' + '<div class="h-info">' + '<div>企业信用代码：' + table[i].creditCode + '</div>' + '<div>登记状态：' + table[i].regStatus + '</div>' + '<div>成立日期：' + table[i].regDate + '</div>' + '</div>' + '</li>';
                }

            }

            tbody.innerHTML = str;

        }
        //通过关键字搜索公司
        function findCompany() {

            content = $("#search").val();
            if (content != "" && content != "null") {

                var jsonObj = {
                    type: "search",
                    content: content
                }
                var postStr = JSON.stringify(jsonObj);

                mj_ajax("../Getdata2.ashx", "json", postStr, Addmsg);
                //     $("#foundItems").html(highlight($("#foundItems"), content));
            } else {
                //nothing
                Loading();
            }
        }

        //分页查询
        function getMessage(pagecurrent, typename) {
            var jsonObj = {
                type: typename,
                pageNow: pagecurrent
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("../Getdata2.ashx", "json", postStr, Addmsg);
        }

        //搜索关键字,高亮显示
        function highlight(text, words, tag) {
            // 默认的标签，如果没有指定，使用span
            tag = tag || 'span';
            var i, len = words.length, re;
            for (i = 0; i < len; i++) {
                // 正则匹配所有的文本
                re = new RegExp(words[i], 'g');
                if (re.test(text)) {
                    text = text.replace(re, '<' + tag + ' class="highlight">$&</' + tag + '>');
                }
            }
            return text;
        }

        //上一页
        function up() {
            if (parseInt($('#num').attr("value")) > 1) {
                $('#num').attr("value", (parseInt($('#num').attr("value")) - 1));
                var count = parseInt($('#num').attr("value"));
                getMessage(count, "all");
            }
        }

        //下一页
        function down() {
            //      if (parseInt($('#num').attr("value")) < parseInt($('#allpage').attr("value"))) {
            $('#num').attr("value", (parseInt($('#num').attr("value")) + 1));
            var count = parseInt($('#num').attr("value"));
            getMessage(count, "all");
            //      }
        }

        //为每条信息增加跳转到详情页面
        $("#foundItems").delegate('li', 'click', function () {
            var id = $(this).data('id');
            console.log(id);
            if (id) {
                window.location.href = "http://localhost:51112/fxxf/details.aspx?id=" + id;
                console.log(id);
            }
        });
    </script>
</body>
</html>
