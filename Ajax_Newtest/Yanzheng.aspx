﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Yanzheng.aspx.cs" Inherits="Ajax_Newtest.Yanzheng" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新用户注册</title>
    <script>
        function check_input() {
            var username = document.getElementById("username").value;

            var password = document.getElementById("password").value;
            var confirm = document.getElementById("confirm").value;
            var email = document.getElementById("email").value;
            var phone = document.getElementById("phone").value;
            var birthday = document.getElementById("birthday").value;
            var gender; //性别


            //“^”是代表以什么开头，“$”是代表以什么结尾。比如：/^\dA$/ //这个就匹配，以数字开头，并且一大写字母A结尾的字符串。
            //var reg_username = /[A-Za-z][A-Za-z0-9_]{6,16}/;
            var reg_username = /^[a-zA-Z][a-zA-Z0-9]{5,16}$/;
            var reg_password = /^[a-zA-Z0-9_]{6,12}$/;
            var reg_mail = /^\w+@\w+(\.[a-zA-Z]{2,3}){1,2}$/;
            var reg_phone = /^1\d{10}$/;
            var reg_birthday = /^([12]\d{3})-(([1-9])|(1[012])|(0[1-9]))-(([1-9])|([12]\d)|(3[01]))$/;
            //身份证校验： reg=/\d{17}[0-9xX]/ 前17位为数字第18位为数字或者字母X或x          

            if (!reg_username.test(username)) //用户名判断
            {
                alert("用户名输入有误，只能是字母数字下划线，6-16位");
            }
            else if (!reg_password.test(password)) {
                alert("密码输入有误，只能是字母数字下划线6-12位");
            }
            else if (confirm != password) {
                alert("两次密码输入的不一致");
            }
            else if (!reg_mail.test(email)) {
                alert("邮箱输入有误")
            }
            else if (!reg_phone.test(phone)) {
                alert("手机号输入有误");
            }
            else if (!reg_birthday.test(birthday)) {
                alert("日期输入不正确");
            }
            else if (!checkGender()) {
                alert("请选择性别");
            } else if (!checkHobbys()) {
                alert("请至少选择一项兴趣");
            }
            else {
                var gender = getGender();
                var interests = getInterest();
                var city = getCity();
                var date = new Date();
                alert("注册成功");
                document.write("<h1>欢迎您，  " + username + "</h1><br/>");
                document.write("用户名：  " + username + "<br/>");
                document.write("密码：    " + password + "<br/>");
                document.write("邮箱：    " + email + "<br/>");
                document.write("手机：    " + phone + "<br/>");
                document.write("出生日期：" + birthday + "<br/>");
                document.write("性别：    " + gender + "<br/>");
                document.write("兴趣：    " + interests + "<br/>");
                document.write("城市：    " + city + "<br/>");
                document.write("注册时间：" + date.toLocaleString() + "<br/>");
            }
        }
        //性别检测
        function checkGender() {
            var man = document.getElementById("man");
            var women = document.getElementById("women")
            if (man.checked || women.checked) {
                return true;
            } else {
                return false;
            }
        }
        //性别检测另一种方法
        function checkSex() {
            //获取页面中同名的元素，返回一个元素数组
            var gender = document.getElementsByName("gender");
            //gender.length获取数组的长度
            //循环获取当前选中状态的性别的值
            for (var i = 0; i < gender.length; i++) {
                //判断某项的checked是否被选中，选中返回true
                if (gender[i].checked) {
                    alert(gender[i].value);
                    break;
                }
            }
        }
        //兴趣检测  
        function checkHobbys() {
            var interests = document.getElementsByName("interest");
            var flag = false;
            //只要有兴趣项被选中即可
            for (var i = 0; i < interests.length; i++) {
                if (interests[i].checked) {
                    flag = true;
                }
            }
            return flag;
        }
        //获取选中的性别
        function getGender() {
            var man = document.getElementById("man");
            var women = document.getElementById("women")
            if (man.checked) {
                return man.value;
            } else if (women.checked) {
                return women.value;
            } else {
                return "";
            }
        }
        //获取选中的爱好选项
        function getInterest() {
            var result = "";
            var interests = document.getElementsByName("interest");
            for (var i = 0; i < interests.length; i++) {
                if (interests[i].checked) {
                    result += interests[i].value + "  ";
                }
            }
            return result;
        }
        //获取选中的城市
        function getCity() {
            var city = "";
            var citys = document.getElementById("city");
            for (var i = 0; i < citys.length; i++) {
                if (citys[i].selected) {
                    city = citys[i].text;
                }
            }
            return city;
        }
        //产生测试数据
        function Generate_data() {
            document.getElementById("username").value = "QQ764073542";
            document.getElementById("password").value = "SoftStar";
            document.getElementById("confirm").value = "SoftStar";
            document.getElementById("email").value = "QQ764073542@163.com";
            document.getElementById("phone").value = "15194070000";
            document.getElementById("birthday").value = "2015-11-11";
            document.getElementsByName("gender")[0].checked = "checked";
            document.getElementsByName("interest")[0].checked = "checked";
            document.getElementsByName("interest")[1].checked = "checked";
            document.getElementById("city").text = "山东";
            //document.getElementById("usernameSpan").innerHTML = "格式要求2";
        }
    </script>   
</head>
<body>
    <form id="form1" runat="server">
        <div>
                            <table border="1"style="width:60%  cellpadding="5" cellspacing="0"" >
                    <tr>
                        <td style="width:20%" >
                            用户名:
                        </td>
                        <td>
                            <input type="text" name="username" id="username"/>字母数字下划线[6-16位]，字母开头<span id="usernameSpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            密码:
                        </td>
                        <td>
                            <input type="password" name="password" id="password"/>字母数字下划线[6-12位]<span id="passwordSpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            确认密码:
                        </td>
                        <td>
                            <input type="password" name="confirm" id="confirm"/><span id="confirmSpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            邮箱:
                        </td>
                        <td>
                            <input type="text" name="email" id="email"/><span id="emailSpan"></span>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            手机:
                        </td>
                        <td>
                            <input type="text" name="phone" id="phone"/><span id="phonelSpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            出生日期:
                        </td>
                        <td>
                            <input type="text" name="birthday" id="birthday"/>例如：2015-8-8<span id="birthdaySpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            性别:
                        </td>
                        <td>
                            <input type="radio" id="man" name="gender" value="male"/>男<input type="radio" id="women" name="gender" value="female"/>女<span id="genderSpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            兴趣:
                        </td>
                        <td>
                            <input type="checkbox" name="interest" value="java"/>Java <input type="checkbox" name="interest" value="HTML"/>HTML 
       <input type="checkbox" name="interest" value="javascript"/>JavaScript<span id="interestSpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            城市:
                        </td>
                        <td>
                            <select id="city">                                
                                <option value="sd">山东</option>
                                <option value="sh">上海</option>
                                <option value="bj" >北京</option>
                                <option value="gz">广州</option>
                                <option value="cd">成都</option>
                                <option value="qt">其他</option>
                            </select>
       <span id="citySpan"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" >
                            <input type="button" onclick="check_input()" value="新用户注册"/>
                            <input type="button" onclick="Generate_data()" value="产生测试数据"/>
                        </td>
                    </tr>
                </table>
        </div>
    </form>
</body>
</html>
