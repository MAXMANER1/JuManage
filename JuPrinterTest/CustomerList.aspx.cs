using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JuPrinterTest
{
    public partial class CustomerList : System.Web.UI.Page
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
            string queryData = "SELECT * FROM customer ORDER BY customer_id desc";
            DataTable dataTable = dBHelper.DoQuery(queryData);

            GridView_data.DataSource = dataTable;
            GridView_data.DataBind();
        }

        protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditID":
                    /*
                    int id = Convert.ToInt32(e.CommandArgument);
                    HttpContext CurrContext = HttpContext.Current;
                    CurrContext.Items.Add("order_id", id);
                    Server.Transfer("AddOrder.aspx", false);
                    */
                    break;
                case "DeleteID":
                    DeleteOrder(Convert.ToInt32(e.CommandArgument));
                    LoadGridView();
                    break;
                default:
                    break;

            }
            //LoadGridView();
        }
        private void DeleteOrder(int customerId)
        {
            string query = string.Format("Delete FROM customer WHERE customer_id = {0}", customerId);
            dBHelper.DoQuery(query);            
        }
        protected void TaskGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_data.PageIndex = e.NewPageIndex;
            //Bind data to the GridView control.
            LoadGridView();
        }

        protected void TaskGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            GridView_data.EditIndex = -1;
            //Bind data to the GridView control.
            LoadGridView();
        }

        protected void TaskGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            GridView_data.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            LoadGridView();
        }

        protected void TaskGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_data.Rows[e.RowIndex];
            string customer_id = ((TextBox)(row.Cells[3].Controls[0])).Text;
            string name = ((TextBox)(row.Cells[4].Controls[0])).Text;
            string phone = ((TextBox)(row.Cells[5].Controls[0])).Text;
            string company = ((TextBox)(row.Cells[6].Controls[0])).Text;
            string address = ((TextBox)(row.Cells[7].Controls[0])).Text;
            String query = String.Format("UPDATE customer SET name ='{1}', phone = '{2}',company = '{3}',address = '{4}' WHERE customer_id = {0}",customer_id,name,phone,company,address);
            dBHelper.DoQuery(query);
            //Reset the edit index.
            GridView_data.EditIndex = -1;
            LoadGridView();
            /*
            //Retrieve the table from the session object.
            DataTable dt = (DataTable)Session["TaskTable"];
            //Update the values.
            GridViewRow row = GridView_data.Rows[e.RowIndex];
            dt.Rows[row.DataItemIndex]["Id"] = ((TextBox)(row.Cells[1].Controls[0])).Text;
            dt.Rows[row.DataItemIndex]["Description"] = ((TextBox)(row.Cells[2].Controls[0])).Text;
            dt.Rows[row.DataItemIndex]["IsComplete"] = ((CheckBox)(row.Cells[3].Controls[0])).Checked;
            
            //Reset the edit index.
            TaskGridView.EditIndex = -1;

            //Bind data to the GridView control.
            BindData();
            */
        }
    }

}