
//切换验证码
function ToggleCode(obj, codeurl) {
    $("#" + obj).attr("src", codeurl + "?time=" + Math.random());
}
//ajax提交后台验证

//获取当前地址中的参数
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}
function online() {

}
function getMsg() {
    var VerifyCodeValue = $("#txtVerifyCode").val();
    var NameIdValue = $("#NameId").val();
    var PassWordValue = $("#PassWord").val();
    $.ajax({
        type: 'post',
        dataType: "text",
        url: "index.aspx",
        data: "action=comparison&VerifyCode=" + VerifyCodeValue,
        cache: false,
        async: false,
        success: function (msg) {
            if (VerifyCodeValue.length <= 0) {
                alert("请输入验证码!");
            }
            else if (msg == "false") {
                alert("验证失败!sorry");
                ToggleCode("Verify_codeImag", "VerifyCode.ashx");
                $("#txtVerifyCode").val("");
            }
            else {
                var jsonObj = {
                    type: "getdata",
                    NameIdValue: NameIdValue,
                    PassWordValue: PassWordValue
                }
                var postStr = JSON.stringify(jsonObj);
                mj_ajax("getdata.ashx", "json", postStr, callBack);
            }
        }
    });

}
function callBack(data) {
    if (data.result == "000") {
        var foodid_dt = data.foodid_dt[0];
        if (foodid_dt["id"] == 0) {
            alert("管理员" + foodid_dt["sex"]);
            alert("性别:" + foodid_dt["sex"]);
        }
        else {
            alert("会员:" + foodid_dt["name"]);
        }
    }
    else if (data.result == "222") {
        alert("您还没注册");
    }
}
