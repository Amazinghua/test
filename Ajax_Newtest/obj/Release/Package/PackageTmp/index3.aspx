﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index3.aspx.cs" Inherits="Ajax_Newtest.index3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript"> 


        var InterValObj; //timer变量，控制时间
        var count = 60; //间隔函数，1秒执行
        var curCount;//当前剩余秒数
        function sendMessage() {
            curCount = count;
            // 设置button效果，开始计时
            document.getElementById("btnSendCode").setAttribute("disabled", "true");//设置按钮为禁用状态
            //document.getElementById("btnSendCode").value = "请在" + curCount + "后再次获取";//更改按钮文字
            InterValObj = window.setInterval(SetRemainTime, 1000); // 启动计时器timer处理函数，1秒执行一次
            // 向后台发送处理数据
            $.ajax({
                type: "POST", // 用POST方式传输
                dataType: "text", // 数据格式:JSON
                url: "forgetPasswdServlet", // 目标地址
                data: "flag=2",
                success: function (data) {
                    data = parseInt(data, 10);
                    if (data == 1) {
                        $("#jbPhoneTip").html("<font color='#339933'>√ 短信验证码已发到您的手机,请查收</font>");
                    } else if (data == 0) {
                        $("#jbPhoneTip").html("<font color='red'>× 短信验证码发送失败，请重新发送</font>");
                    }
                }
            });
        }

        //timer处理函数

        function SetRemainTime() {
            if (curCount == 0) {
                window.clearInterval(InterValObj);// 停止计时器
                document.getElementById("btnSendCode").removeAttribute("disabled");//移除禁用状态改为可用
                document.getElementById("btnSendCode").value = "重新发送验证码";
            } else {
                curCount--;
                document.getElementById("btnSendCode").value = "请在" + curCount + "秒后再次获取";
            }
        }

        //验证短信验证码
        function doCompare() {

            var codelast = document.getElementById("codelast").value;//获取输入的验证码
            if (codelast == null || codelast == '') {
                alert("验证码不能为空！");
            } else {

                $.ajax({
                    type: "POST", // 用POST方式传输
                    dataType: "text", // 数据格式:JSON
                    url: "forgetPasswdServlet", // 目标地址
                    data: "flag=4&codelast=" + codelast,
                    success: function (data) {
                        data = parseInt(data, 10);
                        if (data == 1) {
                            window.location.href = '/aoyi/forgetpasswd/forgetpasswd3.jsp';//验证成功
                        } else if (data == 0) {
                            $("#jbPhoneTip").html("<font color='red'>×短信验证码不正确请重新输入</font>");
                        } else if (data == 2) {
                            $("#jbPhoneTip").html("<font color='red'>×验证码已失效请重新获取验证码</font>");
                        }
                    }
                });
            }
        }

</script> 
</head>
<body>
    <form id="form1" runat="server">
        <table>
             <tr>
              <td>
                             <input id="btnSendCode" name="btnSendCode" type="button"   value="点击获取验证码" onclick="sendMessage();" />
              </td>

  </tr>

  <tr>

             <td align="center"  style="font-size: 18px;font-weight:bold; ">
                 输入验证码：<input style="height: 25px" type="text" name="codelast" id="codelast"/>
             </td>
</tr>

<tr>             
              <td align="center"><span id="jbPhoneTip"></span></td>
 </tr>

 <tr>

              <td align="center"><input type="button" onclick="doCompare();" value="下一步"  class="button4"/></td>

 </tr>
       </table>
    </form>
</body>
</html>
