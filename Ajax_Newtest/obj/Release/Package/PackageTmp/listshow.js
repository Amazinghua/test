function listshow() {
    var jsonObj = {
        type: "show"
    }
    var postStr = JSON.stringify(jsonObj);
    mj_ajax("Default3.aspx", "json", postStr, adding);
}
function adding(data) {
    var show = data;
    alert(show);
}