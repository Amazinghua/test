<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="z-index_Test.aspx.cs" Inherits="Ajax_Newtest.z_index_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .z1, .z2, .z3 {
            position: absolute;
            width: 200px;
            height: 100px;
            padding: 5px 10px;
            color: #fff;
           
        }

        .z1 {
            z-index: 1;
            background: #000;
        }

        .z2 {
            z-index: 2;
            top: 30px;
            left: 30px;
            background: #C00;
        }

        .z3 {
            z-index: 3;
            top: 60px;
            left: 60px;
            background: #999;
        }
    </style>
</head>
<body>
    <div class="z1"><a>z-index:1</a></div>
    <div class="z2"><a>z-index:2</a></div>
    <div class="z3"><a>z-index:3</a></div>
</body>
</html>
