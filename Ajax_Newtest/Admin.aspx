<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Ajax_Newtest.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
		<style>
			*{
				margin:0;
				padding:0;
			}
			.starts{			
				padding-left: 40%;
				padding-top:100px;			
			}
			.starts ul{
				float:left;
			}
			.starts ul li{
				list-style: none;
				width:32px;
				height:21px;
				float:left;
				background:url(images/stark2.png) no-repeat;
				
			}
			.starts ul li.on{
				background:url(images/stars2.png) no-repeat;
			}
			.starts ul span{
				display:inline;
				float:left;
				padding-left:10px;
				height:21px;
				line-height:21px;
			}
		</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>管理员界面</label>

            <div>
                您的姓名:<input id="name" type="text" />
                <br />
                您的密码:<input id="password" type="text" />
                <br />
                您的id:<input id="ider" type="text" />
                <br />
            </div>
            <input type="button" value="查询" onclick="Check()" />
            <script>
                function Check() {
                    var jsonObj = {
                        type: "gstest"
                    }
                    var postStr = JSON.stringify(jsonObj);
                    mj_ajax("Getdata2.ashx", "json", postStr, callBack);
                }
                function callBack(data) {
                    alert("chenggong ");
                }
            </script>
            <br />
            <input type="button" value="添加" onclick="add()" />
            <br />
            <input type="button" value="更改" onclick="Update()" />
            <div id="haha"></div>
        </div>
        <hr />
        <asp:TextBox ID="tb" runat="server" Width="500" Height="300" TextMode="multiLine"></asp:TextBox> 


		<div class="starts">
			<ul id = "pingStar">
				<li rel = "1" title = "特别差，给1分"></li>
				<li rel = "2" title = "很差，给2分"></li>
				<li rel = "3" title = "一般，给3分"></li>
				<li rel = "4" title = "很好，给4分"></li>
				<li rel = "5" title = "非常好，给5分"></li>
				<span id="dir"></span>
			</ul>
			<input type="hidden" value="" id = "startP" />
		</div>
        <input type="button" onclick="star()"value="点击" />
        <script>
            function star() {
                input = document.getElementById("startP");//保存所选值
                var a = input.value;
                console.log(a);
                if (a == "" || a == null) {
                    alert("请评分！");
                }
            }
        </script>
<script>
    window.onload = function () {
        var s = document.getElementById("pingStar");
        m = document.getElementById("dir"),
            n = s.getElementsByTagName("li"),
            input = document.getElementById("startP");//保存所选值
        clearAll = function () {
            for (var i = 0; i < n.length; i++) {
                n[i].className = "";
            }
        }
        for (var i = 0; i < n.length; i++) {
            n[i].onclick = function () {
                var q = this.getAttribute("rel");
                clearAll();
                input.value = q;
                for (var i = 0; i < q; i++) {
                    n[i].className = "on";
                }
                m.innerHTML = this.getAttribute("title");
            }
            n[i].onmouseover = function () {
                var q = this.getAttribute("rel");
                clearAll();
                for (var i = 0; i < q; i++) {
                    n[i].className = "on";
                }
                m.innerHTML = this.getAttribute("title");
            }
            n[i].onmouseout = function () {
                clearAll();
                for (var i = 0; i < input.value; i++) {
                    n[i].className = "on";
                }

            }
        }
    }

		</script>
    </form>
</body>
</html>
