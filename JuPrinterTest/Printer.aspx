<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Printer.aspx.cs" Inherits="JuPrinterTest.Printer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title></title>

    <link href="Bootstrap4/css/reset.css" rel="stylesheet" />
    <link href="Bootstrap4/css/bootstrap.min.css" rel="stylesheet" />    
    <link href="fontawesome/css/all.css" rel="stylesheet" />

    <script type="text/javascript" src="Bootstrap4/js/jquery.min.js"></script>
    <script type="text/javascript" src="Bootstrap4/js/popper.min.js"></script>
    <script type="text/javascript" src="Bootstrap4/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="fontawesome/js/all.min.js"></script>

    <link rel="stylesheet" href="./css/style2.css">
    

</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:lavender">            
            <asp:Label ID="Label1" runat="server" Text="收件人&emsp; &nbsp;&nbsp;&nbsp;:"></asp:Label>
            <input id="Text_recipientName" runat="server" type="text" /><asp:Button ID="Button_FillSender" runat="server" OnClick="Button_FillRecipient_Click" Text="自動帶入" style ="margin-left: 10px; "/>
            <br /><br />
            <asp:Label ID="Label2" runat="server" Text="收件人電話:" ></asp:Label>
            <input id="Text_recipientPhone" runat="server" type="text" />
            <br /><br />
             <asp:Label ID="Label3" runat="server" Text="收件人地址:"></asp:Label>
            <input id="Text_recipientAddress"  runat="server" type="text" />
        </div>

        <div style="background-color:darkkhaki">
            <asp:Label ID="Label4" runat="server" Text="寄件人&emsp; &nbsp;&nbsp;&nbsp;:"></asp:Label>
            <input id="Text_senderName" runat="server" type="text" /><asp:Button ID="Button_FillRecipient" runat="server" OnClick="Button_FillSender_Click" Text="自動帶入" style ="margin-left: 10px; "/>
            <br /><br />
            <asp:Label ID="Label6" runat="server" Text="寄件人電話:" ></asp:Label>
            <input id="Text_senderPhone" runat="server" type="text" />
            <br /><br />
            <asp:Label ID="Label8" runat="server" Text="寄件人地址:" ></asp:Label>
            <input id="Text_senderAddress" runat="server" type="text" />
            <br /><br />
        </div>
        <div style="background-color:burlywood">
            <asp:Label ID="Labe19" runat="server" Text="特級數量:" ></asp:Label>
            <asp:TextBox TextMode="Number" runat="server" min="0" max="100" step="1">0</asp:TextBox>
            <br /><br />
            <asp:Label ID="Label10" runat="server" Text="優級數量:" ></asp:Label>            
            <asp:TextBox TextMode="Number" runat="server" min="0" max="100" step="1">0</asp:TextBox>
            <br /><br />
            <asp:Label ID="Label12" runat="server" Text="出貨日期:" ></asp:Label>
            <input id="Text_datetimepicker" runat="server" type="text" />
            <br /><br />
            <asp:Label ID="Label11" runat="server" Text="備註:" ></asp:Label>
            <input id="Text_comment" runat="server" type="text" multiple="multiple" />
        </div>
        
        <div>       
            <asp:Button ID="Button_printer" class="btn btn-info editElement" runat="server" OnClick="ButtonPrinter_Click" Text="列印" style ="margin-left: 100px; "/>
            <asp:Button ID="Button_Clear"  class="btn btn-primary mdc-ripple-upgraded" runat="server" OnClick="Button_Clear_Click" Text="清空全部" style ="margin-left: 60px;"/>        
        </div>
    </form>
</body>
</html>
