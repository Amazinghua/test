<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="web_js_xslx.aspx.cs" Inherits="Ajax_Newtest.Export.web_js_xslx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../jquery.min.js"></script>
    <script src="js/xlsx.full.min.js"></script>
    <script src="../bootstrap/js/bootstrap.min.js"></script>
 
    <link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script>
        $(function () {

            $('#excel-file').change(function (e) {

                var files = e.target.files;
                var fileReader = new FileReader();
                fileReader.onload = function (ev) {
                    try {
                        var data = ev.target.result,
                            workbook = XLSX.read(data, {
                                type: 'binary'
                            }), // 以二进制流方式读取得到整份excel表格对象
                            persons = []; // 存储获取到的数据
                    } catch (e) {
                        console.log('文件类型不正确');
                        return;
                    }

                    // 表格的表格范围，可用于判断表头是否数量是否正确
                    var fromTo = '';
                    // 遍历每张表读取
                    for (var sheet in workbook.Sheets) {
                        if (workbook.Sheets.hasOwnProperty(sheet)) {
                            fromTo = workbook.Sheets[sheet]['!ref']; // output: A1:D5
                            //一共4列数据
                            if (fromTo[0] === 'A' && fromTo[3] === 'D') {
                                persons = persons.concat(XLSX.utils.sheet_to_json(workbook.Sheets[sheet]));
                                break;//只取第一个Sheets
                            }
                        }
                    }

                    print_data(persons);

                };

                fileReader.readAsBinaryString(files[0]);// 以二进制方式打开文件
            });
        });

        //处理格式化后的json数据
        function print_data(data) {
            $.each(data, function (key, val) {

                $.each(val, function (k, v) {

                });
            });
            var output = JSON.stringify(data, 2, 2);
             list = JSON.parse(output);
             document.getElementById('out').innerText = output;
        }


    </script>
    <script>

            function hahas() {
                var tbody = document.getElementById('tbMain');

                for (var i = 0; i < list.length; i++) { //遍历一下json数据  
                    var trow = getDataRow(list[i]); //定义一个方法,返回tr数据
                    
                    tbody.appendChild(trow);
                }

            }
            function getDataRow(h) {
                var row = document.createElement('tr'); //创建行

                var idCell = document.createElement('td'); //创建第一列id
                idCell.innerHTML = h.姓名; //填充数据
                row.appendChild(idCell); //加入行  ，下面类似

                var nameCell = document.createElement('td');//创建第二列name
                nameCell.innerHTML = h.年龄;
                row.appendChild(nameCell);

                var jobCell = document.createElement('td');//创建第三列job
                jobCell.innerHTML = h.学号;
                row.appendChild(jobCell);

                var fourCell = document.createElement('td');//创建第三列job
                fourCell.innerHTML = h.性别;
                row.appendChild(fourCell);

                //到这里，json中的数据已经添加到表格中，下面为每行末尾添加删除按钮

                var delCell = document.createElement('td');//创建第四列，操作列
                row.appendChild(delCell);
                var btnDel = document.createElement('input'); //创建一个input控件
                btnDel.setAttribute('type', 'button'); //type="button"
                btnDel.setAttribute('value', '删除');
                btnDel.setAttribute("class", "btn");

                //删除操作
                btnDel.onclick = function () {
                    if (confirm("确定删除这一行嘛？")) {
                //找到按钮所在行的节点，然后删掉这一行  
                this.parentNode.parentNode.parentNode.removeChild(this.parentNode.parentNode);
                        //btnDel - td - tr - tbody - 删除(tr)  
                        //刷新网页还原。实际操作中，还要删除数据库中数据，实现真正删除  
                    }
                }
                delCell.appendChild(btnDel);  //把删除按钮加入td，别忘了  

                return row; //返回tr数据      
            }

        </script>

</head>
<body>
    <div>
        <p>
            <input type="file" id="excel-file"/>
            click here to select a file</p>
       

        <table class="table table-bordered table-hover table-condensed">
            <thead>
                <tr>
                    <td>表头1</td>
                    <td>表头2</td>
                    <td>表头3</td>
                    <td>表头4</td>
                    <td>操作</td>
                </tr>
            </thead>
            <tbody  id="tbMain"></tbody>
        </table>
        <pre id="out">

</pre>
        <br />
        <input type="button" onclick="hahas()" value="haha" />
    </div>
</body>
</html>
