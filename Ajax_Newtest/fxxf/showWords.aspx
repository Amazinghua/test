<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showWords.aspx.cs" Inherits="Ajax_Newtest.fxxf.words" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>留言展示</title>
    <script src="../jquery.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../MJ_JSComment.js"></script>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-size: 12px;
            font-family: "微软雅黑", "黑体", "Helvetica", "sans-serif";
        }

        .h5-topic {
            padding-top: 28px;
            padding-bottom: 16px;
        }

        .h5-left-hint {
            font-size: 18px;
            border-top-right-radius: 30px;
            border-bottom-right-radius: 30px;
            background-color: #3583e0;
            color: white;
            padding: 3px 24px 3px 12px;
        }

        .h5-send-comments {
            position: fixed;
            font-size: 18px;
            bottom: 0;
            left: 0;
            right: 0;
            text-align: center;
            line-height: 58px;
            background-color: #e5e5e5;
            color: #333333;
        }

        .h5-body ul.h5-comments {
            padding: 0;
        }

        .h5-head {
        }

            .h5-head > img {
                display: block;
                width: 100%;
            }

        .h5-body {
            padding: 0px;
        }

            .h5-body ul > li.h5-summary {
                display: -webkit-flex;
                -webkit-flex-flow: row nowrap;
                -ms-flex-flow: row nowrap;
                flex-flow: row nowrap;
                -webkit-align-items: stretch;
                -ms-flex-align: stretch;
                /* align-items: stretch; */
                padding: 8px 16px;
                border-bottom: 1px solid #c8c8c8;
            }

        .h5-summary .h5-profile {
            width: 12%;
        }

        .h5-summary .h5-desc {
            -webkit-flex: 1;
            -ms-flex: 1;
            flex: 1;
            padding-left: 15px;
            display: -webkit-flex;
            -webkit-flex-flow: column nowrap;
            -ms-flex-flow: column nowrap;
            flex-flow: column nowrap;
            -webkit-justify-content: space-between;
            -ms-flex-pack: justify;
            justify-content: space-between;
        }

        .h5-summary .h5-profile > img {
            width: 100%;
        }

        .h5-news {
            font-size: 14px;
            color: #2d2d2d;
        }

        .h5-news-desc {
            color: #333333;
            display: -webkit-box;
            overflow: hidden;
            text-overflow: ellipsis;
            -webkit-line-clamp: 4;
            -webkit-box-orient: vertical;
            line-height: 20px;
            font-size: 14px;
        }

        .h5-render {
            font-size: 10px;
            color: #af7f56;
            text-align: right;
            line-height: 24px;
        }


        .load-more-container {
            text-align: center;
            padding: 8px 5px 80px 5px;
            font-size: 12px;
        }

        a, img, button, input, textarea, select {
            -webkit-tap-highlight-color: rgba(255, 255, 255, 0);
        }

        .h5-root {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
        }


        .h5-head {
        }

        .h5-body {
            padding: 15px;
            font-family: "Microsoft YaHei", "微软雅黑", "黑体", "Helvetica", "sans-serif";
            overflow: hidden;
            font-size: 14px;
        }

            .h5-body ul {
                list-style: none;
                padding: 0;
                margin: 0;
            }

                .h5-body ul > li {
                    display: block;
                    padding-bottom: 18px;
                }
    </style>
</head>
<body>
    <div class="h5-root" id="comments">
        <div class="h5-head" id="warning">
            <p>只显示审批通过的留言</p>
        </div>
        <div class="h5-body">
            <div class="h5-topic">
                <span>全部留言</span>
            </div>
            <div>
                <ul id="commentItems" class="h5-comments">
                    <%--<li class="h5-summary">
                        <div class="h5-profile">
                            <img src="http://thirdwx.qlogo.cn/mmopen/vi_32/DYAIOgq83eqJbGiafDhYzb07GlMd1b6DR2tPzvIaYzibxvHib8iaicnqDiaicbibcHTgWkUfT3ibCwo6s24aJzPgtBPFk9Q/132"/>
                        </div>
                        <div class="h5-desc">
                            <div class="h5-news-desc">等待更强大的服务功能开通！等待更强大的服务功能开通！等待更强大的服务功能开通！等待更强大的服务功能开通！等待更强大的服务功能开通！等待更强大的服务功能开通！等待更强大的服务功能开通！</div>
                            <div class="h5-render">周诚敢&nbsp;&nbsp;2017-03-03 09:37:48</div>
                        </div>
                    </li>--%>
                </ul>
            </div>
        </div>
        <div class="footer" style="position: absolute; bottom: 0; width: 100%; clear: both;">
            <input type="button" class="btn btn-primary form-control" style="margin-bottom:10px;" value="我要留言" />
        </div>
    </div>
    <script>
        $().ready(function () {
            test();
        });
        function test() {
            var table = 2;
            var ul = document.getElementById("commentItems");
            for (var i = 0; i < 3; i++) {


                var div_news_desc = document.createElement("div");
                div_news_desc.setAttribute("class", "h5-news-desc");
                div_news_desc.innerHTML = "内容";
                var div_h5_render = document.createElement("div");
                div_h5_render.setAttribute("class", "h5-render");
                div_h5_render.innerHTML = "人名" + "&nbsp;&nbsp;" + "2018-06-19";
                var div_h5_desc = document.createElement("div");
                div_h5_desc.setAttribute("class", "h5-desc");
                div_h5_desc.appendChild(div_news_desc);
                div_h5_desc.appendChild(div_h5_render);

                var img = document.createElement("img");
                img.setAttribute("src", "http://thirdwx.qlogo.cn/mmopen/vi_32/DYAIOgq83eqJbGiafDhYzb07GlMd1b6DR2tPzvIaYzibxvHib8iaicnqDiaicbibcHTgWkUfT3ibCwo6s24aJzPgtBPFk9Q/132");

                var div_h5_profile = document.createElement("div");
                div_h5_profile.setAttribute("class", "h5-profile");
                div_h5_profile.appendChild(img);

                var li = document.createElement("li");
                li.setAttribute("class", "h5-summary");
                li.appendChild(div_h5_profile);
                li.appendChild(div_h5_desc);
                ul.appendChild(li);


            }
        }
    </script>
</body>
</html>
