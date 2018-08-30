<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CursortPosition.aspx.cs" Inherits="Ajax_Newtest.CursortPosition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>js光标控制</title>
    <script src="jquery.min.js"></script>
    <script>
        function dealKeyup(x) {
            var input_val = document.getElementById(x).value
            var input_arr = input_val.split("-");
            console.log(input_arr);
            for (var i = 0; i < input_arr.length; i++) {
                if (input_arr[i] == ' ') {
                    setCaretPosition(x, i * 2);
                    return;
                }
                //var y = document.getElementById(x).value
                //document.getElementById(x).value = y.toUpperCase()
            }
        }
    </script>
</head>
<body>
    <input type="text" class="put" id="number-password-input" maxlength="12" tabindex="6" onkeyup="dealKeyup(this.id);" />
    <script>
        // 光标定位
        // 获取光标位置
        function getCursortPosition(ctrl) {
            var CaretPos = 0;   // IE Support
            if (document.selection) {
                ctrl.focus();
                var Sel = document.selection.createRange();
                Sel.moveStart('character', -ctrl.value.length);
                CaretPos = Sel.text.length;
            }
            // Firefox support
            else if (ctrl.selectionStart || ctrl.selectionStart == '0')
                CaretPos = ctrl.selectionStart;
            return (CaretPos);
        }

        // 设置光标位置
        function setCaretPosition(ctrl, pos) {
            if (ctrl.setSelectionRange) {
                ctrl.focus();
                ctrl.setSelectionRange(pos, pos);
            }
            else if (ctrl.createTextRange) {
                var range = ctrl.createTextRange();
                range.collapse(true);
                range.moveEnd('character', pos);
                range.moveStart('character', pos);
                range.select();
            }
        }
    </script>
</body>
</html>
