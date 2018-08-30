
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grid_Test.aspx.cs" Inherits="Ajax_Newtest.Grid_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .wrapper {
            display: grid;
            grid-template-columns: 100px 100px 100px;
            grid-template-rows: 100px 100px 100px;
        }

        .item1 {
            grid-column-start: 1;
            grid-column-end: 3;
        }

        .item3 {
            grid-row-start: 2;
            grid-row-end: 4;
        }

        .item4 {
            grid-column-start: 2;
            grid-column-end: 4;
        }
    </style>
</head>
<body>
<div class="wrapper">
  <div class="item1">1</div>
  <div class="item2">2</div>
  <div class="item3">3</div>
  <div class="item4">4</div>
  <div class="item5">5</div>
  <div class="item6">6</div>
</div>
</body>
</html>
