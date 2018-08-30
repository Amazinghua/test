<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testGetdate.aspx.cs" Inherits="Ajax_Newtest.testGetdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script>
        function login() {
            var name = $("#name").val();
            var test = $("#test").val();
            var jsonObj = {
                type: "checknot",
                name: name,
                id: test
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("getdate.aspx", "json", postStr, callBack);
        }
        function callBack(data) {
            if (data.code == "1") {
                alert("登陆成功！")
                document.getElementById("show").innerHTML = "";
                var foodid_dt = data.date[0];
                var div = document.getElementById("show");
                var p1 = document.createElement("p");
                p1.setAttribute("style", "font-weight:bold;");
                p1.innerHTML = "姓名：" + foodid_dt["name"];
                div.appendChild(p1);

                var p2 = document.createElement("p");
                var time = foodid_dt["Password"];
                p2.innerHTML = "密码：" + time;
                div.appendChild(p2);

                var p3 = document.createElement("p");
                p3.innerHTML = "ID：" + foodid_dt["id"];
                div.appendChild(p3);

                var p3 = document.createElement("p");
                p3.innerHTML = "路径1：" + foodid_dt["image1"];
                div.appendChild(p3);
                //window.location.href = 'MainGetdate.aspx?id=' + data[0].id;
            } else {
                alert("登陆失败！")
            }
        }
    </script>
    <script>
        var image = '';
        function selectImage(file) {
            if (!file.files || !file.files[0]) {
                return;
            }
            var reader = new FileReader();
            reader.onload = function (evt) {
                document.getElementById('image').src = evt.target.result;
                image = evt.target.result;
            }
            console.log(file.files[0]);
            File = file.files[0];
            reader.readAsDataURL(file.files[0]);
         
        }
        function uploadImage() {
            var jsonObj = {
                type: "sh",
                image: image,
                id: "0"
              
            }
            var postStr = JSON.stringify(jsonObj);
            mj_ajax("getdate.aspx", "json", postStr, back);

        }
        function back(data) {
            if (data.code == "3") {
                alert("上传成功功");
            }
        }
    </script>
</head>
<body>
    姓名：<input id="name" type="text" />
    id:<input id="test" type="text" />
    <input type="button" onclick="login()" value="点击" />
    <div class="show row" id="show">
    </div>
    <div class="test">
        <img id="image" src="" />
        <br />
        <input type="file" onchange="selectImage(this);" />
        <br />
        <input type="button" onclick="uploadImage();" value="提交" />
    </div>
    <div class="row">
        <div class="col-xs-4">
            输入：
        </div>
        <div class="col-xs-8">
            <input type="text" />
        </div>
    </div>
</body>
</html>
