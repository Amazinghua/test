<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dianzan.aspx.cs" Inherits="Ajax_Newtest.dianzan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div>  
            <%=ArticleModel.ArticleContent %></div>  
        <div>  
            <br />  
            <br />  
            <img src="/img/xihuan.gif" id="like" />  
            <a id="qwlike">  
                <%=ArticleModel.ZanNum%></a></div>  
        <script type="text/javascript">  
                //赞  
                $(function () {  
                    $("#like").click(function () {  
                        $.ajax({  
                            type:'post',  
                            url:'add_like.ashx',  
                            data:{"ArticleId":<%=ArticleModel.ArticleId%>},  
                            dataType:'text',  
                            beforeSend:function(){  
                                },  
                            success:function(data){  
                            if (data==="ok") {  
                            var a = parseInt($("#qwlike").html());  
                                a++;  
                                $("#qwlike").html(a);  
                                showmes("点赞成功！");  
                                }  
                             else {  
                                    showmes(data);  
                                    return;}  
                                },  
                            complete:function(){  
                                 }  
                        })  
                    })  
                })  
        </script>  
        </div>
    </form>
</body>
</html>
