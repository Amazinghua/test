<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fenye.aspx.cs" Inherits="Ajax_Newtest.fenye" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="bootstrap-paginator.min.js"></script>
    <script src="Comment.js"></script>

    <script>
        //$(document).ready(function () {
        //    queryUser();
        //    function queryUser() {
        //        $.ajax({
        //            async: true,
        //            type: "post",
        //            url: "action.ashx",//向后台发送请求，后台为stuts2框架
        //            dataType: "json",
        //            data: { page: '1' },
        //            cache: false,
        //            success: function (data) {
        //                var result = JSON.parse(data.json_data);   //data.json_data为后台返回的JSON字符串，这里需要将其转换为JSON对象


        //                tbody = "<tr style='background:#fff;'><th >姓名</th><th>密码</th>" +
        //                    "<th >年龄</th><th>性别</th><th>联系方式</th></tr>";
        //                for (var i = 0; i < result.list.length; i++) {//拼接对应<th>需要的值
        //                    var trs = "";
        //                    trs += '<tr ><td >' + result.list[i].name
        //                        + '</td><td >' + result.list[i].password
        //                        + '</td><td >' + result.list[i].year
        //                        + '</td><td>' + result.list[i].sex
        //                        + '</td><td>' + ""
        //                        + '</td></tr>';
        //                    tbody += trs;
        //                };
        //                $("#userTable").html(tbody);

        //                var currentPage = result.CurrentPage; //当前页数
        //                var pageCount = result.pageCount; //总页数
        //                var options = {
        //                    bootstrapMajorVersion: 3, //版本

        //                    currentPage: currentPage, //当前页数

        //                    totalPages: pageCount, //总页数

        //                    numberOfPages: 5,
        //                    shouldShowPage: true,//是否显示该按钮

        //                    itemTexts: function (type, page, current) {

        //                        switch (type) {

        //                            case "first":

        //                                return "首页";

        //                            case "prev":

        //                                return "上一页";

        //                            case "next":

        //                                return "下一页";

        //                            case "last":

        //                                return "末页";

        //                            case "page":

        //                                return page;

        //                        }

        //                    },//点击事件，用于通过Ajax来刷新整个list列表
        //                    onPageClicked: function (event, originalEvent, type, page) {
        //                        $.ajax({
        //                            async: true,
        //                            url: "action.ashx",
        //                            type: "post",
        //                            dataType: "json",
        //                            data: { page: page },
        //                            cache: false,
        //                            success: function (data) {
        //                                var result = JSON.parse(data.msg);

        //                                tbody = "<tr style='background:#fff;'><th >姓名</th><th>密码</th>" +
        //                                    "<th >年龄</th><th性别</th><th>联系方式</th></tr>";
        //                                for (var i = 0; i < result.list.length; i++) {

        //                                    var trs = "";
        //                                    trs += '<tr ><td >' + result.list[i].name
        //                                        + '</td><td >' + result.list[i].password
        //                                        + '</td><td >' + result.list[i].year
        //                                        + '</td><td>' + result.list[i].sex
        //                                        + '</td><td>' + ""
        //                                        + '</td></tr>';
        //                                    tbody += trs;

        //                                };
        //                                $("#userTable").html(tbody);

        //                            }/*success*/
        //                        });

        //                    }

        //                };
        //                $('#useroption').bootstrapPaginator(options);
        //            }/*success*/

        //        });
        //    }

        //});
    </script>
    <script>
        $(document).ready(function () {

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <ol id="content">

             </ol>
           <ul id="pagination" pageSize="2">
    </ul>
        <script>
            var options = {
                bootstrapMajorVersion: 3,
                alignment: 'center',
                currentPage: 1,
                numberOfPages: 5,
                totalPages: 10,
            };
            $(document).ready(function () {
                $("#pagination").bootstrapPaginator(options);
            });
</script>
        <script>
            var totalPages;
            var pageSize = $("#pagination").attr("pageSize");
            $.ajax({
                url: "action.ashx",
                type: "POST",
                dataType: "json",
                async: false,
                success: function (data) {
                    window.totalPages = Math.ceil(data / pageSize);
                },
                error: function (error) {
                    alert("error");
                }
            });
        </script>
    </form>
</body>
</html>
