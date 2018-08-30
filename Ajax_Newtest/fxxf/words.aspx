<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="words.aspx.cs" Inherits="Ajax_Newtest.fxxf.words1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>用户留言</title>
    <script src="../jquery.min.js"></script>
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../MJ_JSComment.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        input {
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <div class="h5-root">
        <div class="h5-head">
            <%--   <img src="imges/show.png" />--%>
        </div>
        <div class="container">
            <div class="h5-body" style="width: 90%; margin: 0 auto">
                <div class="form-group" style="margin-top: 30px">
                    <input type="text" class="form-control" id="input_name" placeholder="请输入真实姓名" />
                    <input type="text" class="form-control" id="input_phone" placeholder="输入正确的手机号" />
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <input type="text" class="form-control" id="input_code" placeholder="请输入验证码" />
                    </div>
                    <div class="col-xs-4">
                    </div>
                </div>
                <textarea class="form-control" rows="4" style="resize: none; margin-bottom: 30px;" id="areas" placeholder="请填写留言内容，不超过120字"></textarea>
                <input type="button" class="btn btn-success form-control" value="提交留言" onclick="commit()" />
            </div>
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"> <!-- 提示模态框-->
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h5 class="modal-title" id="myModalLabel">友情提示:
                            </h5>
                        </div>
                        <div class="modal-body">
                            <p id="test">请输入</p>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal -->
           
        </div>
            </div>
    </div>
    <script>
        function commit() {
            var name = $("#input_name").val();
            var phone = $("#input_phone").val();
            var code = $("#input_code").val();
            var areas = $("#areas").val();
            if (name == "" || name == null) {
                document.getElementById("test").innerHTML = "请输入姓名";
                $('#myModal').modal('show');
            } else if (phone == "" || phone == null) {
                document.getElementById("test").innerHTML = "请输入手机号";
                $('#myModal').modal('show');
            } else if (code == "" || code == null) {
                document.getElementById("test").innerHTML = "请输入验证码";
                $('#myModal').modal('show');
            } else if (areas == "" || areas == null) {
                document.getElementById("test").innerHTML = "请输入投诉内容";
                $('#myModal').modal('show');
            } else {

            }
        }
    </script>
</body>
</html>
