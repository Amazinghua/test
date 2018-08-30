
//生成二维码
function createQR(data) {
    var str = toUtf8(data);
    $('#qrcode').qrcode({
        render: "canvas", //table方式 
        width: 100, //宽度 
        height: 100, //高度 
        text: str //任意内容 
    });
}
//获取当前地址中的参数
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}
//转成utf8
function toUtf8(str) {
    var out, i, len, c;
    out = "";
    len = str.length;
    for (i = 0; i < len; i++) {
        c = str.charCodeAt(i);
        if ((c >= 0x0001) && (c <= 0x007F)) {
            out += str.charAt(i);
        } else if (c > 0x07FF) {
            out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
            out += String.fromCharCode(0x80 | ((c >> 6) & 0x3F));
            out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
        } else {
            out += String.fromCharCode(0xC0 | ((c >> 6) & 0x1F));
            out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
        }
    }
    return out;
}
$(document).ready(function () {
    getMealTicket();
})
//获取信息
function getMealTicket() {
    var worderid = GetQueryString("worderid");
    var order = GetQueryString("order");
    var phone = GetQueryString("phone");
    var jsonObj = {
        type:"getfoodid",
        worderid: worderid,
        order: order,
        phone:phone
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("getdata.ashx", "json", postStr, callBack);
}
//回调函数
function callBack(data) {
    if (data.result == "000") {
        var foodid_dt = data.foodid_dt[0];
        var div = document.getElementById("explain");        
        var p1 = document.createElement("p");
        p1.setAttribute("style", "font-weight:bold;");
        p1.innerHTML = "餐票编号：" + foodid_dt["foodid"].substring(foodid_dt["foodid"].length - 6, foodid_dt["foodid"]);
        div.appendChild(p1);

        var p2 = document.createElement("p");
        var time = foodid_dt["eattime"].substring(0, 10);
        p2.innerHTML = "时间和餐次：" + time + foodid_dt["eattype"];
        div.appendChild(p2);

        var p3 = document.createElement("p");
        p3.innerHTML = "用餐地点：" + foodid_dt["eatplace"];
        div.appendChild(p3);

        var p4 = document.createElement("p");
        p4.innerHTML = "类型：" + foodid_dt["type"];
        div.appendChild(p4);

        var p5 = document.createElement("p");
        p5.innerHTML = "项目：" + foodid_dt["name"];
        div.appendChild(p5);

        var p6 = document.createElement("p");
        p6.innerHTML = "电话：" + foodid_dt["phone"];
        div.appendChild(p6);

        var p7 = document.createElement("p");
        p7.innerHTML = "餐票当天有效，不回收";
        div.appendChild(p7);

        var qrcode = foodid_dt["foodid"];
        createQR(qrcode);
        $("#tip").show();
    }
    else if (data.result == "222") {
        $("#qrcode").html("餐票已被领完！");
    }
    else if (data.result == "333") {
        $("#qrcode").html("领取失败！");
    }
}
//保存到手机上
function loadimg() {
    html2canvas(document.body, {
        allowTaint: true,
        taintTest: false,
        onrendered: function (canvas) {
            canvas.id = "mycanvas";
            //document.body.appendChild(canvas);  
            //生成base64图片数据  
            var dataUrl = canvas.toDataURL();
            //var newImg = document.createElement("img");
            //newImg.src = dataUrl;
            //document.body.appendChild(newImg);
            savePicture(dataUrl);
        }
    });
}

