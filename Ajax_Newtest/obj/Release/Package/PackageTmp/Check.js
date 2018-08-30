
var namevalue = $("#name").val();


//查询
    function Check() {
        var namevalue = $("#name").val();
        var passwordvalue = $("#password").val();
        var idvalue = $("#ider").val();
        var jsonObj = {
            type: "getSql",
            namevalue: namevalue,
            passwordvalue: passwordvalue,
            idvalue: idvalue
        }
        var postStr = JSON.stringify(jsonObj);
            mj_ajax("getSql.ashx", "json", postStr, callBack);
}
function callBack(data) {
    if (data.result == "000") {
        var foodid_dt = data.foodid_dt[1];
        alert("管理员" + foodid_dt["name"]);
        alert("密码:" + foodid_dt["Password"]);
    }
    if (data.result == "111") {
        var foodid_dt = data.foodid_dt[0];
        alert("管理员" + foodid_dt["name"]);
        alert("会员无此功能");
    }
}

//添加
function add() {
    var namevalue = $("#name").val();
    var passwordvalue = $("#password").val();
    var idvalue = $("#ider").val();
    var jsonObj = {
        type: "getSql",
        namevalue: namevalue,
        passwordvalue: passwordvalue,
        idvalue: idvalue
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("getSql.ashx", "json", postStr, adding);
}
function adding(data) {
    if (data.result == "222") {
        var foodid_dt = data.foodid_dt;
        var a = foodid_dt[0];
        alert(a["name"]);
        alert("添加成功");
       
        alert("您的信息:" + foodid_dt[0]["name"] + " " + foodid_dt["Password"] + " " + foodid_dt["id"]);
    } else if (data.result == "333") {
        alert("添加失败,用户已存在!");
    }
}


//更新

function Update() {
    var namevalue = $("#name").val();
    var passwordvalue = $("#password").val();
    var idvalue = $("#ider").val();
    var jsonObj = {
        type: "getSql",
        namevalue: namevalue,
        passwordvalue: passwordvalue,
        idvalue: idvalue
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("getSql.ashx", "json", postStr, Uping);
}
function Uping(data) {
    if (data.result == "000") {
        alert("更新成功!");
    } else if (data.result == "555") {
        alert("更新失败!");
    }
}
