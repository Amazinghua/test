<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PicTest.aspx.cs" Inherits="Ajax_Newtest.PicTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2" style="height: 21px">
                     
                </td>
            </tr>
            <tr>
                <td style="width: 400px">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                     <asp:Label ID="label1" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td style="width: 80px">
                    <asp:Button ID="UploadButton" runat="server" Text="上传图片" OnClick="UploadButton_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <br />
                    <asp:Image ID="Image1" runat="server" Height="118px" Width="131px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
