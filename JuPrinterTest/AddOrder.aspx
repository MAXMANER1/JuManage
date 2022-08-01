<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="JuPrinterTest.AddOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新增訂單</title>
    <link href="Bootstrap4/css/reset.css" rel="stylesheet" />
    <link href="Bootstrap4/css/bootstrap.min.css" rel="stylesheet" />    
    <link href="fontawesome/css/all.css" rel="stylesheet" />

    <script type="text/javascript" src="Bootstrap4/js/jquery.min.js"></script>
    <script type="text/javascript" src="Bootstrap4/js/popper.min.js"></script>
    <script type="text/javascript" src="Bootstrap4/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="fontawesome/js/all.min.js"></script>

    <link rel="stylesheet" href="./css/style2.css">
    <style>
        .float_btn {
            position: fixed;
            bottom: 3rem;
            right: 1.625rem;
            z-index: 999999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="row">
        <div class="col-md-6">
          <div class="card mb-4">
            <h5 class="card-header">收件人資訊</h5>
            <div class="card-body">
                <div class="col-md-6">
                    <label for="inputRecipientName" class="form-label">收件人姓名</label>
                    <div class="input-group">
                        <input type="text"  runat="server" class="form-control" autocomplete="off" id="inputRecipientName"/>
                        <asp:Button ID="Button_FillRecipient" runat="server" Text="自動帶入" OnClick="Button_Fill_Click"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="inputRecipientPhone" class="form-label">收件人電話</label>
                    <input type="tel"  runat="server" class="form-control" autocomplete="off" id="inputRecipientPhone"/>
                </div>
                <div class="col-12">
                    <label for="inputRecipientAddress" class="form-label">收件人地址</label>
                    <input type="text"  runat="server" class="form-control" autocomplete="off" id="inputRecipientAddress"/>
                </div>
            </div>
        </div>
      </div>
        <div class="col-md-6">
          <div class="card mb-4">
            <h5 class="card-header">寄件人資訊</h5>
            <div class="card-body">
                <div class="col-md-6">
                    <label for="inputSenderName" class="form-label">寄件人姓名</label>
                    <div class="input-group">
                        <input type="text"  runat="server" class="form-control" autocomplete="off" id="inputSenderName"/>
                        <asp:Button ID="Button_FillSender" runat="server" Text="自動帶入" OnClick="Button_Fill_Click"/>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <label for="inputSenderPhone" class="form-label">寄件人電話</label>
                    <input type="tel"  runat="server" class="form-control" autocomplete="off" id="inputSenderPhone"/>
                  </div>
                  <div class="col-12">
                    <label for="inputSenderAddress" class="form-label">寄件人地址</label>
                    <input type="text"  runat="server" class="form-control" autocomplete="off" id="inputSenderAddress"/>
                  </div>
            </div>
        </div>
      </div>
      <div class="col-md-6">
        <div class="card mb-4">
          <h5 class="card-header">訂單資訊</h5>
          <div class="card-body">
            <div class="col-md-6">
                <label for="dateinput" class="form-label">預計出貨日期</label>
                  <div>
                      <input runat="server" class="form-control" type="date" value="2022-06-18" autocomplete="off" id="datePicker" />
                  </div>
            </div>
            <div class="col-md-3">
                <label for="inputnumLV1" class="form-label">特級數量</label>
                <input type="number"  runat="server" class="form-control" value="0" min="0" id="inputnumLV1"/>
            </div>
            <div class="col-md-3">
                <label for="inputnumLV2" class="form-label">優級數量</label>
                <input type="number"  runat="server" class="form-control" value="0" min="0" id="inputnumLV2"/>
            </div>
            <div class="col-md-3">
                <label for="totalPrice" class="form-label">總金額</label>
                <input type="text"  runat="server" class="form-control" value="0"  id="totalPrice"/>
            </div>
            <div class="col-12">
                <label for="inputComment" class="form-label">備註</label>
                <input type="text"  runat="server" class="form-control" id="inputComment"/>
              </div>            
          </div>
        </div>
      </div>
    </div>        
    <div >
        <asp:Button ID="Button_save" class="btn btn-success d-grid float_btn btn-lg" runat="server" Text="儲存" OnClick="Save_Click" />
    </div>

    <div >
        <input type="button" class="btn btn-danger d-grid btn-lg" value="重填" onclick="reset_order()"/>
    </div>
    </form>
    
        <script type="text/javascript">
            const lv1 = document.getElementById('inputnumLV1');
            const lv2 = document.getElementById('inputnumLV2');
            lv1.addEventListener('change', updatePrice);
            lv2.addEventListener('change', updatePrice);
            if (document.getElementById("inputRecipientName").value == ""){
                document.getElementById('datePicker').valueAsDate = new Date();
            };
            function test() {
                alert('Hello World');
            };
            function reset_order() {
                document.getElementById("inputRecipientName").value = "";
                document.getElementById("inputRecipientPhone").value = "";
                document.getElementById("inputRecipientAddress").value = "";
                document.getElementById("inputSenderName").value = "";
                document.getElementById("inputSenderPhone").value = "";
                document.getElementById("inputSenderAddress").value = "";
                document.getElementById('datePicker').valueAsDate = new Date();
                document.getElementById("inputnumLV1").value = 0;
                document.getElementById("inputnumLV2").value = 0;
                document.getElementById("totalPrice").value = 0;
                document.getElementById("inputComment").value = "";
            };

            function updatePrice(e) {
                document.getElementById("totalPrice").value = document.getElementById("inputnumLV1").value * 800 + document.getElementById("inputnumLV2").value * 600;
            };
        </script>
</body>

</html>
