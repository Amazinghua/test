<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page-fenye.aspx.cs" Inherits="Ajax_Newtest.page_fenye" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>分页test</title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="layui/css/layui.css" rel="stylesheet" />
    <script src="jquery.min.js"></script>
        <script src="layui/layui.all.js"></script>
    <script src="layui/lay/modules/laypage.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script>
        $(document).ready(function () {
            showlist()
        });
        function showlist() {
            var jsonObj = {
                type: "list"
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("dataso.ashx", "json", postStr, list);
        }

        function list(data) {
            var tb = window.document.getElementById("tbody-result");
            var str = "";
            if (data.result == "888") {
                var data = data.table;
                //调用分页
                layui.use(['laypage', 'layer'], function () {
                    var laypage = layui.laypage
                        , layer = layui.layer;
                    //调用分页
                    laypage.render({
                        elem: 'page'
                        , count: data.length
                        , limit:2
                        , jump: function (obj) {
                            //模拟渲染
                           
                            tb.innerHTML = function () {
                                var arr = []
                                    , thisData = data.concat().splice(obj.curr * obj.limit - obj.limit, obj.limit);
                                
                                layui.each(thisData, function (i, data) {
                                    arr.push("<tr>" +
                                        "<td>" + thisData[i].id + "</td>" +
                                        "<td>" + thisData[i].stuNo + "</td>" +
                                        "<td>" + thisData[i].courseName + "</td>" +
                                        "<td>" + thisData[i].courseScore + "</td>" +
                                        "</tr>");
                                });
                                return arr.join('');
                                console.log(arr);
                                tb.appendChild(arr);
                            }();
                        }
                    });
                });

            }
        }
    </script>
</head>
<body>
    <div class="table-container">
        <table class="table table-striped" id="table-result">
            <thead>
                <tr>
                    <th>姓名</th>
                    <th>密码</th>
                    <th>年龄</th>
                    <th>性别</th>
                </tr>
            </thead>
            <tbody id="tbody-result">
            </tbody>
        </table>
    </div>
    <div value="1 0"></div>
    <div id="page" class="page_div"></div>
<%--        <script>
            var data = [
                '北京',
                '上海',
                '广州',
                '深圳',
                '杭州',
                '长沙',
                '合肥',
                '宁夏',
                '成都',
                '西安',
                '南昌',
                '上饶',
                '沈阳',
                '济南',
                '厦门',
                '福州',
                '九江',
                '宜春',
                '赣州',
                '宁波',
                '绍兴',
                '无锡',
                '苏州',
                '徐州',
                '东莞',
                '佛山',
                '中山',
                '成都',
                '武汉',
                '青岛',
                '天津',
                '重庆',
                '南京',
                '九江',
                '香港',
                '澳门',
                '台北'
            ];
            layui.use(['laypage', 'layer'], function () {
                var laypage = layui.laypage
                    , layer = layui.layer;
                //调用分页
                laypage.render({
                    elem: 'page'
                    , count: data.length
                    , jump: function (obj) {
                        //模拟渲染
                        var tb = document.getElementById("tbody-result");
                        tb.innerHTML = function () {
                            var arr = []
                                , thisData = data.concat().splice(obj.curr * obj.limit - obj.limit, obj.limit);
                            layui.each(thisData, function (i, data) {
                                arr.push(("<tr>" +
                                    "<td>" + data[i].name + "</td>" +
                                    "<td>" + data[i].password + "</td>" +
                                    "<td>" + data[i].year + "</td>" +
                                    "<td>" + data[i].sex + "</td>" +
                                    "</tr>");
                            });
                            return arr.join('');
                        }();
                    }
                });
            });
    </script>--%>
    <script>


        //分页
        //$("#page").paging({
        //    pageNo: 5,
        //    totalPage: 9,
        //    totalSize: 300,
        //    callback: function (num) {
        //        alert(num)
        //    }
        //})

	    /*
		 模拟ajax数据用以下方法，方便用户更好的掌握用法
		参数为当前页
		ajaxTest(1);
		
		function ajaxTest(num) {
			$.ajax({
				url: "table.json",
				type: "get",
				data: {},
				dataType: "json",
				success: function(data) {
					console.log(data);
					分页
					$("#page").paging({
						pageNo: num,
						totalPage: data.totalPage,
						totalSize: data.totalSize,
						callback: function(num) {
							ajaxTest(num)
						}
					})
				}
			})
		}
		*/

    </script>
</body>
</html>
