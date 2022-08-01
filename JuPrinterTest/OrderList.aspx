<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="JuPrinterTest.OrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>訂單清單</title>

    <link href="../Bootstrap4/css/reset.css" rel="stylesheet" />
    <link href="../Bootstrap4/css/bootstrap.min.css" rel="stylesheet" />    
    <link href="../fontawesome/css/all.css" rel="stylesheet" />

    <script type="text/javascript" src="../Bootstrap4/js/jquery.min.js"></script>
    <script type="text/javascript" src="../Bootstrap4/js/popper.min.js"></script>
    <script type="text/javascript" src="../Bootstrap4/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../fontawesome/js/all.min.js"></script>

    <link rel="stylesheet" href="../css/style2.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<div class="row">--%>
                <div class="col-md-6">
                <label for="startDate" class="form-label">起始日期</label>
                  <div>
                      <input runat="server" class="form-control" type="date" value="2022-01-01" autocomplete="off" id="startDate" />
                  </div>
                </div>
                <div class="col-md-6">
                    <label for="endDate" class="form-label">結束日期</label>
                      <div>
                          <input runat="server" class="form-control" type="date" value="2022-12-31" autocomplete="off" id="endDate" />
                      </div>
                </div>
                <div class="col-md-6">
                    <label for="name" class="form-label">姓名</label>
                      <div>
                          <input runat="server" class="form-control" type="text"  autocomplete="off" id="name" />
                      </div>
                </div>
            </div>
            <div class="row">
                <asp:Button ID="Button_Search" class="btn btn-primary d-grid w-50" runat="server" Text="搜尋" OnClick="Button_Search_Click"/>
            </div>
            
            <asp:GridView ID="GridView_data" class="table table-striped table-bordered" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView_OnRowCommand" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
                <Columns>                
                    <asp:BoundField DataField="order_id" HeaderText="訂單編號" Visible="False" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonEdit" runat="server" CausesValidation="false" CommandName="EditID"
                            Text="修改" CommandArgument='<%# Eval("order_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonDelete" runat="server" CausesValidation="false" CommandName="DeleteID"
                            Text="刪除" CommandArgument='<%# Eval("order_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonPrint" runat="server" CausesValidation="false" CommandName="PrintID"
                            Text="列印" CommandArgument='<%# Eval("order_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>      
                    <asp:BoundField HeaderText="收件人" DataField="recipient_name" />
                    <asp:BoundField DataField="recipient_phone" HeaderText="收件人電話" />
                    <asp:BoundField DataField="recipient_company" HeaderText="收件人公司" Visible="False" />
                    <asp:BoundField DataField="recipient_address" HeaderText="收件地址" />
                    <asp:BoundField DataField="sender_name" HeaderText="寄件人" />
                    <asp:BoundField DataField="sender_phone" HeaderText="寄件人電話" />
                    <asp:BoundField DataField="sender_address" HeaderText="寄件人地址"  />
                <asp:BoundField DataField="shipment_date" HeaderText="出貨日期" DataFormatString = "{0:MM月dd日yyyy年}" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="item" HeaderText="品項" />
                    <asp:BoundField DataField="total_price" HeaderText="總金額" />
                    <asp:BoundField DataField="comment" HeaderText="備註" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        document.getElementById('endDate').valueAsDate = new Date();
    </script>
</body>
</html>
