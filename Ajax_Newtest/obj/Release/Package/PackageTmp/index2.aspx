<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="Ajax_Newtest.index2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="jquery.min.js"></script>
    <script src="MJ_JSComment.js"></script>
    <script src="gai.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>姓名;</label><input type="text"  id="name"/>
            <br />
            <label>密码;</label><input type="text"  id="pw"/>
            <br />
            <label>年龄;</label><input type="text" id="year" />
            <br />
            <label>性别;</label><input type="text" id="sex" />
            <br />
        </div>
        <div class="row">
            <p>添加一条信息</p>
            <input type="button" value="添加" class="col-xs-4" onclick="add()"/>
            <br />
           <p>删除指定的姓名</p> 
              <input type="button" value="删除" class="col-xs-4" onclick="Dele()"/>
            <br />
            <p>更改指定姓名的密码</p>
              <input type="button" value="更改" class="col-xs-4" onclick="gai()"/>
            <br />
            <p>查询指定的内容</p>
              <input type="button" value="查询" class="col-xs-4"onclick="check()" />
            <br />
            <p>展示列表</p>
            <input type="button" value="列表" onclick="listing()" />

        </div>
        <div id="show"></div>
            <div class="table-container">  
        <table class="ui nine column table celled table-result" id="table-result">  
            <thead>  
            <tr>  
                <th>姓名</th>  
                <th>密码</th>  
                <th>年龄</th>  
                <th>性别</th>  
            </tr>  
            </thead>  
            <tbody id="tbody-result">  
            </tbody>  
        </table>  
    </div> 
        <div id="zanprint">111</div>

    </form>
</body>
</html>
