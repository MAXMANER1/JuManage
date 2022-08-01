using JuManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JuPrinterTest
{
    public partial class OrderList : System.Web.UI.Page
    {
        DBHelper dBHelper;
        protected void Page_Load(object sender, EventArgs e)
        {
            dBHelper = new DBHelper();
            if (!IsPostBack)
            {
                LoadGridView();
            }
        }

        private void LoadGridView()
        {
            string queryData = "SELECT r.name AS recipient_name," +
                              "r.phone AS recipient_phone," +
                              "r.company AS recipient_company," +
                              "r.address AS recipient_address, " +
                              "s.name AS sender_name," +
                              "s.phone AS sender_phone," +
                              "s.company AS sender_company," +
                              "s.address AS sender_address, " +
                              "o.order_id," +
                              "o.shipment_date, " +
                              "o.item," +
                              "o.total_price," +
                              "o.`comment`" +
                              "FROM (SELECT o.*,t.item from ju_order o,(SELECT order_id ,GROUP_CONCAT(DISTINCT item.name,':  ',quantity,' 箱 ') AS item " +
                              "FROM ju_order_dtl  ,item WHERE ju_order_dtl.item_id = item.item_id GROUP BY order_id) AS t  WHERE o.order_id = t.order_id  ) AS o,customer r,customer s " +
                              "WHERE o.recipient_id = r.customer_id  and o.sender_id = s.customer_id ORDER BY order_id desc";
            DataTable dataTable = dBHelper.DoQuery(queryData);
            GridView_data.DataSource = dataTable;
            GridView_data.DataBind();
        }

        private void Search(String startDate, String endDate, String name)
        {            
            string queryData = "SELECT r.name AS recipient_name," +
                              "r.phone AS recipient_phone," +
                              "r.company AS recipient_company," +
                              "r.address AS recipient_address, " +
                              "s.name AS sender_name," +
                              "s.phone AS sender_phone," +
                              "s.company AS sender_company," +
                              "s.address AS sender_address, " +
                              "o.order_id," +
                              "o.shipment_date, " +
                              "o.item," +
                              "o.total_price," +
                              "o.`comment`" +
                              "FROM (SELECT o.*,t.item from ju_order o,(SELECT order_id ,GROUP_CONCAT(DISTINCT item.name,':  ',quantity,' 箱 ') AS item " +
                              "FROM ju_order_dtl  ,item WHERE ju_order_dtl.item_id = item.item_id GROUP BY order_id) AS t  WHERE o.order_id = t.order_id  ) AS o,customer r,customer s " +
                              "WHERE o.recipient_id = r.customer_id  and o.sender_id = s.customer_id ORDER BY order_id desc";
            String query;
            if (name != "")
            { 
                query = string.Format("SELECT A.* FROM ({0}) AS A WHERE A.shipment_date >'{1}' AND A.shipment_date <'{2}' AND (A.recipient_name ='{3}' OR A.sender_name ='{3}')", queryData, startDate, endDate, name); 
            }
            else
            {
                query = string.Format("SELECT A.* FROM ({0}) AS A WHERE A.shipment_date >'{1}' AND A.shipment_date <'{2}'", queryData, startDate, endDate);
            }            
            DataTable dataTable = dBHelper.DoQuery(query);
            GridView_data.DataSource = dataTable;
            GridView_data.DataBind();
        }
        protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditID":
                    int id = Convert.ToInt32(e.CommandArgument);
                    HttpContext CurrContext = HttpContext.Current;
                    CurrContext.Items.Add("order_id", id);
                    Server.Transfer("AddOrder.aspx", false);
                    break;
                case "DeleteID":
                    DeleteOrder(Convert.ToInt32(e.CommandArgument));
                    break;
                case "PrintID":
                    int rowindex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string sender_name = row.Cells[8].Text;
                    string sender_phone = row.Cells[9].Text;                    
                    string sender_address = row.Cells[10].Text;

                    string recipient_name = row.Cells[4].Text;
                    string recipient_phone = row.Cells[5].Text;
                    string recipient_company = row.Cells[6].Text;
                    string recipient_address = row.Cells[7].Text;

                    //PrinterHelper printer = new PrinterHelper();
                    //printer.Print(recipient_name, recipient_phone, recipient_company, recipient_address, sender_name, sender_phone,"", sender_address);
                    break;
                default:
                    break;

            }
            LoadGridView();
        }

        private void DeleteOrder(int orderId)
        {
            string query = string.Format("Delete FROM ju_order WHERE order_id = {0}", orderId);
            dBHelper.DoQuery(query);
            string query2 = string.Format("Delete FROM ju_order_dtl WHERE order_id = {0}", orderId);
            dBHelper.DoQuery(query2);
        }
        protected void Button_Search_Click(object sender, EventArgs e)
        {
            Search(startDate.Value, endDate.Value, name.Value);
        }
    }
}