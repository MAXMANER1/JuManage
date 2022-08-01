using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JuPrinterTest
{
    public partial class Printer : System.Web.UI.Page
    {
        int type1Price = 800;
        int type2Price = 600;
        String oringinFileName = "C:\\Users\\u10168\\Downloads\\貨運單\\大榮test.docx";
        DBHelper dBHelper;
        List<HtmlInputText> recipient_ControlList;
        List<HtmlInputText> sender_ControlList;
        protected void Page_Load(object sender, EventArgs e)
        {
            dBHelper = new DBHelper();
            recipient_ControlList = new List<HtmlInputText> { Text_recipientName, Text_recipientPhone, Text_recipientAddress };
            sender_ControlList = new List<HtmlInputText> { Text_senderName, Text_senderPhone, Text_senderAddress };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">切換模式: 1為收件人,2為寄件人</param>
        private void AutoFill(int type) 
        {            
            var controlList = recipient_ControlList;
            string query = "";

            switch (type)
            {
                case 1:
                    controlList = recipient_ControlList;
                    break;
                case 2:
                    controlList = sender_ControlList;
                    //若為寄件人並且搜尋條件為null，則取id=1 預設為老朱訊息
                    query = "SELECT * FROM custom WHERE id = 1";
                    break;
            }

            if (controlList[0].Value != "")
            {
                query = string.Format("SELECT * FROM custom WHERE name = '{0}'", controlList[0].Value);
            }
            else if (controlList[1].Value != "")
            {
                query = string.Format("SELECT * FROM custom WHERE phone = '{0}'", controlList[2].Value);
            }            
            
            DataTable table = new DataTable();
            table = dBHelper.DoQuery(query);
            if (table == null || table.Rows.Count == 0)
            {
                //TODO:找不到資料的反饋
                Console.WriteLine("null");
            }
            else
            {
                DataRow dr;
                dr = table.Rows[0];
                controlList[0].Value = dr.Field<String>("name");
                controlList[1].Value = dr.Field<String>("phone");
                controlList[2].Value = dr.Field<String>("address");
                             
            }            
        }
        //return index
        private int CheckCustomerExist(string pName,string pPhone,string pAddress) 
        {
            //先搜尋是否存在資料
            string searchQuery = string.Format("SELECT 1 FROM custom WHERE name = '{0}' and phone ='{1}' and address = '{2}'  LIMIT 1", pName, pPhone, pAddress);                        
            DataTable table = dBHelper.DoQuery(searchQuery);
            //取得custom id
            string secondQuery;
            if (table == null || table.Rows.Count == 0)
            {
                secondQuery = string.Format("INSERT INTO custom (NAME,phone,address) VALUES('{0}','{1}','{2}'   );", pName, pPhone, pAddress);
                secondQuery += " SELECT LAST_INSERT_ID();";                
            }
            else 
            {
                secondQuery = string.Format("SELECT id FROM custom Where name = '{0}' and phone ='{1}' and address = '{2}'",pName,pPhone,pAddress);
            }
            table = dBHelper.DoQuery(secondQuery);
            return int.Parse(table.Rows[0][0].ToString());            
        }
        protected void ButtonPrinter_Click(object sender, EventArgs e)
        {
            int recipientId = CheckCustomerExist(recipient_ControlList[0].Value.ToString(), recipient_ControlList[1].Value.ToString(), recipient_ControlList[2].Value.ToString());
            int senderId = CheckCustomerExist(sender_ControlList[0].Value.ToString(), sender_ControlList[1].Value.ToString(), sender_ControlList[2].Value.ToString());
            
            //CreateOrder(recipientId, senderId);
            PrintShipment();

            ClearControl(recipient_ControlList);
            ClearControl(sender_ControlList);
        }

        protected void Button_FillSender_Click(object sender, EventArgs e)
        {
            AutoFill(2);
        }

        protected void Button_FillRecipient_Click(object sender, EventArgs e)
        {
            AutoFill(1);            
        }        
        

        private void ClearControl(List<HtmlInputText> htmlInputTexts)
        {
            foreach(HtmlInputText htmlInputText in htmlInputTexts)
            {
                htmlInputText.Value = "";
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
                FindAndReplace(wordApp, "Recipient_name", Text_recipientName.Value.ToString());
                FindAndReplace(wordApp, "Recipient_phone", Text_recipientPhone.Value.ToString());
                FindAndReplace(wordApp, "Recipient_address", Text_recipientAddress.Value.ToString());

                FindAndReplace(wordApp, "Sender_name", Text_senderName.Value.ToString());
                FindAndReplace(wordApp, "Sender_phone", Text_senderPhone.Value.ToString());
                FindAndReplace(wordApp, "Sender_address", Text_senderAddress.Value.ToString());
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

        protected void Button_Clear_Click(object sender, EventArgs e)
        {
            ClearControl(recipient_ControlList);
            ClearControl(sender_ControlList);
        }
        private bool CreateOrder(int pRecipientId, int pSenderId, String pDateTime, int pType1Quantity, int pType2Quantity)
        {
            string comment = Text_comment.Value;

            int totalPrice = pType1Quantity * type1Price + pType2Quantity * type2Price;
            string query_createOrder = string.Format("INSERT INTO ju_order (recipient_id,sender_id,shipment_date,total_price,comment) VALUES({0},{1},'{2}',{3},'{4}');", pRecipientId, pSenderId, pDateTime, totalPrice, comment);
            query_createOrder += " SELECT LAST_INSERT_ID();";
            DataTable table = dBHelper.DoQuery(query_createOrder);
            int order_id= int.Parse(table.Rows[0][0].ToString());            
            if (order_id >= 0)
            {
                //將訂單詳細品項數目寫入OrderDtl資料表
                if (pType1Quantity > 0)
                    FillOrderDtl(order_id, 1, pType1Quantity);
                if (pType2Quantity > 0)
                    FillOrderDtl(order_id, 2, pType1Quantity);
            }
            else
            {
                //TODO: 若未成功INSERT ju_order 的回傳值
                return false;
            }
            return true;
        }

        private void FillOrderDtl(int order_id, int item_id, int typeQuantity)
        {
            string query_createOrder_dtl = string.Format("INSERT INTO ju_order_dtl (order_id,item_id,quantity) VALUES ({0},{1},{2})", order_id, item_id, typeQuantity);
            dBHelper.DoQuery(query_createOrder_dtl);
        }

    }
}