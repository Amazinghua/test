<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="GetMealTicket.QRCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>餐票发放</title>
    <link href="js/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/jquery.cookie.js"></script>
    <script src="js/jquery.qrcode.min.js"></script>
    <script src="js/bootstrap/js/bootstrap.min.js"></script>
    <script src="js/html2canvas.js"></script>
    <script src="js/commen_js/MJ_JSComment.js"></script>
    <script src="js/QRcode.js"></script>
    <style type="text/css">
        p{
            margin-top:-10px;
        }
    </style>
    <script  type="text/javascript">
        document.addEventListener("plusready", onPlusReady, false);
        var r = null;
        // 扩展API加载完毕，现在可以正常调用扩展API 
        function onPlusReady() {
        }
        function savePicture(url) {
            plus.gallery.save("img/gmcc.png", function () {
                alert("保存图片到相册成功");
            });
        }
    </script>
</head>
<body>
    <div class="container-fluid" style="margin-top:5%">
        <div class="row text-center">
            <img src="img/logo透明.png" style="width:150px;height:50px"/>
        </div>
        <div class="row text-center" id="tip" style="display:none">
            <label style="color: red">请及时截图保存到手机</label><br />
            <label style="color:red">同一手机只能获取一个二维码</label><br />

        </div>
        <div class="row text-center" id="qrcode"></div>
        <div class="row text-center" style="margin-top:20px">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="explain">
            </div>
        </div>
        <div class="row text-center">
                        <input type="button" class="btn btn-primary" value="保存到手机" onclick="loadimg()"/>
        </div>
    </div>
</body>
</html>
