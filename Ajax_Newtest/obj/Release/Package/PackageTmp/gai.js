
//添加
function add() {
    var namevalue = $("#name").val();
    var passwordvalue = $("#pw").val();
    var yearvalue = $("#year").val();
    var sexvalue = $("#sex").val();
    var jsonObj = {
        type: "getSql",
        namevalue: namevalue,
        passwordvalue: passwordvalue,
        yearvalue: yearvalue,
        sexvalue: sexvalue
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("Getdata2.ashx", "json", postStr, adding);
}
function adding(data) {
    if (data.result == "000") {
        alert("添加成功");
    } else if (data.result == "111") {
        alert("添加失败!");
    }
}
//删除
function Dele() {
    var namevalue = $("#name").val();
    var jsonObj = {
        type: "dele",
        namevalue: namevalue,
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("Getdata2.ashx", "json", postStr, deleing);
}
function deleing(data) {
    if (data.result == "333") {
        alert("删除成功");
    } else if (data.result == "444") {
        alert("删除失败");
    }
}
//修改密码
function gai() {
    var namevalue = $("#name").val();
    var passwordvalue = $("#pw").val();
    var jsonObj = {
        type: "gai",
        namevalue: namevalue,
        passwordvalue: passwordvalue,
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("Getdata2.ashx", "json", postStr, gaiing);
}
function gaiing(data) {
    if (data.result == "555") {
        alert("修改成功");
    } else if (data.result == "666") {
        alert("修改失败");
    }
}
//查询指定内容
function check() {
    var namevalue = $("#name").val();
    var jsonObj = {
        type: "check",
        namevalue: namevalue,
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("Getdata2.ashx", "json", postStr, cheing);
}
function cheing(data) {
    if (data.result == "777") {
        var foodid_dt = data.foodid_dt[0];
        var div = document.getElementById("show");
        var p1 = document.createElement("p");
        p1.setAttribute("style", "font-weight:bold;");
        p1.innerHTML = "姓名：" + foodid_dt["name"];
        div.appendChild(p1);

        var p2 = document.createElement("p");
        var time = foodid_dt["password"];
        p2.innerHTML = "密码：" + time;
        div.appendChild(p2);

        var p3 = document.createElement("p");
        p3.innerHTML = "年龄：" + foodid_dt["year"];
        div.appendChild(p3);

        var p3 = document.createElement("p");
        p3.innerHTML = "性别：" + foodid_dt["sex"];
        div.appendChild(p3);

    }

}
//展示
function listing() {
    var namevalue = $("#name").val();
    var passwordvalue = $("#pw").val();
    var yearvalue = $("#year").val();
    var sexvalue = $("#sex").val();
    var jsonObj = {
        type: "list",
        namevalue: namevalue,
        passwordvalue: passwordvalue,
        yearvalue: yearvalue,
        sexvalue: sexvalue
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("Getdata2.ashx", "json", postStr, list);
}
function list(data) {
    var tbody = window.document.getElementById("tbody-result");  
    var str = "";
    if (data.result == "888") {
        var data = data.foodid_dt; 
        for (i in data) {
            str += "<tr>" +
                "<td>" + data[i].name + "</td>" +
                "<td>" + data[i].password + "</td>" +
                "<td>" + data[i].year + "</td>" +
                "<td>" + data[i].sex + "</td>" +
                "</tr>";
        }
        tbody.innerHTML = str;  
    }
}