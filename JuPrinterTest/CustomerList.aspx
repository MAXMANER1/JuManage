<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="JuPrinterTest.CustomerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客戶資料管理</title>

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
            <asp:GridView ID="GridView_data" runat="server" class="table table-striped table-bordered"  CellPadding="4" ForeColor="#333333" GridLines="None" 
                OnRowCommand="GridView_OnRowCommand" AutoGenerateColumns="False" AutoGenerateEditButton="True" 
                OnRowEditing="TaskGridView_RowEditing" OnRowUpdating="TaskGridView_RowUpdating" OnRowCancelingEdit="TaskGridView_RowCancelingEdit" OnPageIndexChanging="TaskGridView_PageIndexChanging">
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
                <asp:TemplateField ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonEdit" runat="server" CausesValidation="false" CommandName="EditID" 
                            Text="修改" CommandArgument='<%# Eval("customer_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ButtonDelete" runat="server" CausesValidation="false" CommandName="DeleteID"
                            Text="刪除" CommandArgument='<%# Eval("customer_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="customer_id" HeaderText="客戶編號" Visible="true" />
                    <asp:BoundField DataField="name" HeaderText="姓名"/>
                    <asp:BoundField DataField="phone" HeaderText="電話" />
                    <asp:BoundField DataField="company" HeaderText="公司" Visible="False" />
                    <asp:BoundField DataField="address" HeaderText="地址" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
