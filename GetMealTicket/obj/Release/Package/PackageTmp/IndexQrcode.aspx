<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexQrcode.aspx.cs" Inherits="GetMealTicket.IndexQrcode" %>

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
    <script src="js/commen_js/MJ_JSComment.js"></script>
    <script src="js/checkCode.js"></script>
</head>
<body>
    <div class="container-fluid">

        <div class="row text-center">
            <img src="img/logo透明.png" />
        </div>

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                <label style="font-size: x-large">餐票领取</label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                <input type="text" placeholder="手机号码" class="form-control input-lg" id="phone"/>
            </div>
        </div>
        <div class="row" style="padding-top: 10px">
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">

                <input type="text" placeholder="验证码" class="form-control input-lg" id="inputcoede"/>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-4 col-xs-4">

                <input type="button" id="code" onclick="createCode()" class="btn btn-lg btn-default" />
            </div>
        </div>
        <div class="row" style="padding-top: 10px">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                <input type="button" class="btn btn-info col-xs-12" value="验证" onclick="validate()"/>
            </div>
        </div>
        <div class="row" style="padding-top: 10px">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <label>说明：每个手机号码只能领取一次</label>
            </div>
            
        </div>
    </div>
                <div id="footer" class="container">
                <nav class="navbar navbar-default navbar-fixed-bottom" style="background-color:rgb(234,233,234)">
                    <div class="navbar-inner navbar-content-center">
                        <p class="text-muted credit" style="padding: 10px;text-align:center">
                            中国移动江门公司
                            粤ICP备05103820号-26.
                        </p>
                    </div>
                </nav>
            </div>
</body>
</html>
