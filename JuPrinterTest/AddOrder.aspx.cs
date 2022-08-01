using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JuPrinterTest
{
    public partial class AddOrder : System.Web.UI.Page
    {
        String oringinFileName = "C:\\Users\\u10168\\Downloads\\貨運單\\大榮test.docx";
        DBHelper dBHelper;
        protected void Page_Load(object sender, EventArgs e)
        {
            dBHelper = new DBHelper();
            if (!IsPostBack) 
            {

                HttpContext CurrContext = HttpContext.Current;
                if (CurrContext.Items["order_id"] != null)
                {
                    int order_id = (int)CurrContext.Items["order_id"];
                    ViewState["updateMode"] = true;
                    ViewState["order_id"] = order_id;
                    LoadOrder(order_id);
                }

                
            }
        }

        protected void Button_Fill_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.ID) 
            {
                case "Button_FillRecipient":
                    AutoFill(inputRecipientName, inputRecipientPhone, inputRecipientAddress);
                    break;
                case "Button_FillSender":
                    AutoFill(inputSenderName, inputSenderPhone, inputSenderAddress);
                    break;
            }         
        }

        private void LoadOrder(int order_id) 
        {            
            string query;
            DataTable dataTable;
            query = String.Format("SELECT r.name AS recipient_name," +
                "r.phone AS recipient_phone," +
                "r.company AS recipient_company," +
                "r.address AS recipient_address, " +
                "s.name AS sender_name," +
                "s.phone AS sender_phone," +
                "s.company AS sender_company," +
                "s.address AS sender_address, " +
                "o.order_id," +
                "DATE_FORMAT(o.shipment_date, \"%Y-%m-%d\") AS shipment_date, " +
                "o.total_price," +
                "o.`comment` " +
                "FROM ju_order o ,customer r, customer s WHERE o.recipient_id = r.customer_id  and o.sender_id = s.customer_id and o.order_id = {0}",order_id);
            dataTable = dBHelper.DoQuery(query);
            inputRecipientName.Value = dataTable.Rows[0]["recipient_name"].ToString();
            inputRecipientPhone.Value = dataTable.Rows[0]["recipient_phone"].ToString();
            inputRecipientAddress.Value = dataTable.Rows[0]["recipient_address"].ToString();
            inputSenderName.Value = dataTable.Rows[0]["sender_name"].ToString();
            inputSenderPhone.Value = dataTable.Rows[0]["sender_phone"].ToString();
            inputSenderAddress.Value = dataTable.Rows[0]["sender_address"].ToString();
            datePicker.Value = dataTable.Rows[0]["shipment_date"].ToString();
            totalPrice.Value = dataTable.Rows[0]["total_price"].ToString();
            inputComment.Value = dataTable.Rows[0]["comment"].ToString();
            //代入數量
            query = String.Format("SELECT * from ju_order_dtl WHERE order_id = {0}",order_id);
            dataTable = dBHelper.DoQuery(query);
            foreach (DataRow dr in dataTable.Rows)
            {
                switch (dr["item_id"])
                {
                    case 1:
                        inputnumLV1.Value = dr["quantity"].ToString();
                        break;
                    case 2:
                        inputnumLV2.Value = dr["quantity"].ToString();
                        break;
                }
            }
            
        }
        protected void Save_Click(object sender, EventArgs e) 
        {
            //ClientScript.RegisterStartupScript(GetType(), "hwa", "test()", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "confirm", "reset_order();", true);
            
            int recipientId = CheckCustomerExist(inputRecipientName.Value, inputRecipientPhone.Value, inputRecipientAddress.Value);
            int senderId = CheckCustomerExist(inputSenderName.Value, inputSenderPhone.Value, inputSenderAddress.Value);
            if (ViewState["updateMode"]!=null && (bool)ViewState["updateMode"])
            {
                UpdateOrder((int)ViewState["order_id"], recipientId, senderId, datePicker.Value.ToString(), int.Parse(inputnumLV1.Value), int.Parse(inputnumLV2.Value), int.Parse(totalPrice.Value));
                ViewState["updateMode"] = false;
                ViewState["order_id"] = 0;
                Server.Transfer("OrderList.aspx");
            }
            else
            {
                bool createSuccess = CreateOrder(recipientId, senderId, datePicker.Value.ToString(), int.Parse(inputnumLV1.Value), int.Parse(inputnumLV2.Value), int.Parse(totalPrice.Value));
                //TODO:應可改至Javascript執行
                if (createSuccess)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "reset_order()", true);
                    inputRecipientName.Value = "";
                    inputRecipientPhone.Value = "";
                    inputRecipientAddress.Value = "";
                    inputSenderName.Value = "";
                    inputSenderPhone.Value = "";
                    inputSenderAddress.Value = "";
                    datePicker.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    inputnumLV1.Value = "0";
                    inputnumLV2.Value = "0";
                    totalPrice.Value = "0";
                    inputComment.Value = "";
                }
            }


            //PrintShipment();

        }


        private int CheckCustomerExist(string pName, string pPhone, string pAddress)
        {
            //先搜尋是否存在資料
            string searchQuery = string.Format("SELECT 1 FROM customer WHERE name = '{0}' and phone ='{1}' and address = '{2}'  LIMIT 1", pName, pPhone, pAddress);
            DataTable table = dBHelper.DoQuery(searchQuery);
            //取得custom id
            string secondQuery;
            if (table == null || table.Rows.Count == 0)
            {
                secondQuery = string.Format("INSERT INTO customer (NAME,phone,address) VALUES('{0}','{1}','{2}');", pName, pPhone, pAddress);
                secondQuery += " SELECT LAST_INSERT_ID();";
            }
            else
            {
                secondQuery = string.Format("SELECT customer_id FROM customer Where name = '{0}' and phone ='{1}' and address = '{2}'", pName, pPhone, pAddress);
            }
            table = dBHelper.DoQuery(secondQuery);
            return int.Parse(table.Rows[0][0].ToString());
        }


        private bool CreateOrder(int pRecipientId, int pSenderId, String pDateTime, int pType1Quantity, int pType2Quantity,int pTotalPrice)
        {
            string comment = inputComment.Value;
            String now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //int totalPrice = pType1Quantity * type1Price + pType2Quantity * type2Price;
            string query_createOrder = string.Format("INSERT INTO ju_order (recipient_id,sender_id,shipment_date,total_price,comment,create_date) VALUES({0},{1},'{2}',{3},'{4}','{5}');", pRecipientId, pSenderId, pDateTime, pTotalPrice, comment, now);
            query_createOrder += " SELECT LAST_INSERT_ID();";
            DataTable table = dBHelper.DoQuery(query_createOrder);
            int order_id = int.Parse(table.Rows[0][0].ToString());
            if (order_id >= 0)
            {
                //將訂單詳細品項數目寫入OrderDtl資料表
                if (pType1Quantity > 0)
                    FillOrderDtl(order_id, 1, pType1Quantity);
                if (pType2Quantity > 0)
                    FillOrderDtl(order_id, 2, pType2Quantity);
            }
            else
            {
                Response.Write("<Script language='JavaScript'>alert('建立訂單失敗！');</Script>");
                return false;
            }
            return true;
        }

        private bool UpdateOrder(int order_id ,int pRecipientId, int pSenderId, String pDateTime, int pType1Quantity, int pType2Quantity, int pTotalPrice)
        {
            string comment = inputComment.Value;

            //int totalPrice = pType1Quantity * type1Price + pType2Quantity * type2Price;
            string query = string.Format("UPDATE ju_order SET recipient_id = {0},sender_id = {1},shipment_date = '{2}',total_price = {3},comment = '{4}' WHERE order_id = {5}", pRecipientId, pSenderId, pDateTime, pTotalPrice, comment, order_id);
            DataTable table = dBHelper.DoQuery(query);
            //將訂單詳細品項數目更新到OrderDtl資料表
            FillOrderDtl(true, order_id, 1, pType1Quantity);
            FillOrderDtl(true, order_id, 2, pType2Quantity);
            return true;
        }

        private void FillOrderDtl(int order_id, int item_id, int typeQuantity)
        {
            string query_createOrder_dtl = string.Format("INSERT INTO ju_order_dtl (order_id,item_id,quantity) VALUES ({0},{1},{2})", order_id, item_id, typeQuantity);
            dBHelper.DoQuery(query_createOrder_dtl);
        }

        private void FillOrderDtl(bool update,int order_id, int item_id, int typeQuantity)
        {
            string query;
            if (typeQuantity == 0)
            {
                query = string.Format("DELETE FROM ju_order_dtl WHERE order_id ={0} and item_id = {1}", order_id, item_id);
            }
            else
            {
                query = string.Format("SELECT * FROM ju_order_dtl WHERE order_id ={0} and item_id = {1}", order_id, item_id);
                if (dBHelper.DoQuery(query).Rows.Count != 0)
                {
                    query = string.Format("UPDATE ju_order_dtl SET quantity = {2} WHERE order_id ={0} and item_id = {1}", order_id, item_id, typeQuantity);
                }
                else
                {
                    query = string.Format("INSERT INTO ju_order_dtl (order_id,item_id,quantity) VALUES ({0},{1},{2})", order_id, item_id, typeQuantity);
                }
                                
            }            
            dBHelper.DoQuery(query);
        }


        private void AutoFill(HtmlInputText name, HtmlInputGenericControl phone, HtmlInputText address)
        {
            
            string query;

            //若沒有填寫欄位值則帶入預設資料(id=1)
            if (name.Value == "" && phone.Value == "" && address.Value == "")
            {
                query = "SELECT * FROM customer WHERE customer_id = 1";
            }
            else 
            {
                query = "SELECT * FROM customer WHERE 1=1 ";
                if (name.Value != "")
                {
                    query += string.Format("AND name = '{0}'", name.Value);
                }
                if (phone.Value != "")
                {
                    query += string.Format("AND phone = '{0}'", phone.Value);
                }
            }
            
            DataTable table = new DataTable();
            table = dBHelper.DoQuery(query);
            if (table == null || table.Rows.Count == 0)
            {
                Response.Write("<Script language='JavaScript'>alert('無此顧客資料！');</Script>");
            }
            else
            {
                DataRow dr;
                dr = table.Rows[0];
                name.Value = dr.Field<String>("name");
                phone.Value = dr.Field<String>("phone");
                address.Value = dr.Field<String>("address");
            }
        }

        //印出出貨單
        private void PrintShipment()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = true };
                Microsoft.Office.Interop.Word.Document copyDoc = wordApp.Documents.Open(oringinFileName, ReadOnly: false, Visible: true);
                //ref  CompatibilityMode);            
                copyDoc.Activate();
                //replace data
                FindAndReplace(wordApp, "Recipient_name", inputRecipientName.Value);
                FindAndReplace(wordApp, "Recipient_phone", inputRecipientPhone.Value);
                FindAndReplace(wordApp, "Recipient_address", inputRecipientAddress.Value);

                FindAndReplace(wordApp, "Sender_name", inputSenderName.Value);
                FindAndReplace(wordApp, "Sender_phone", inputSenderPhone.Value);
                FindAndReplace(wordApp, "Sender_address", inputSenderAddress.Value);
                /*
                copyDoc.PrintOut();
                wordApp.Quit(false);            
                */
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        private void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace           
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}