using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Web_practice
{
    public partial class Store : System.Web.UI.Page
    {
        private object drinkDetaisView;

        protected void Page_Load(object sender, EventArgs e)
        {
            drinkDetailsView.DataBind();
            Lable_userShow.Text = Session["name"] + "歡迎光臨<br>您還剩下: " + Session["deposit"] + "元";
            label_drinkPrice.Text = "";
            label_drinkQT.Text = "";
            Image_drink.ImageUrl = "./pic/未選取.jpg";
        }

        protected void drinkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image_drink.ImageUrl = "./pic/" + drinkList.SelectedItem.Text.ToString() + ".jpg";
            if (0 == drinkList.SelectedIndex)
            {
                label_drinkPrice.Text = "";
                label_drinkQT.Text = "";
            }
            else {
                label_drinkPrice.Text = drinkDetailsView.Rows[0].Cells[1].Text + " 元";
                label_drinkQT.Text = "\t庫存: " + drinkDetailsView.Rows[1].Cells[1].Text + " 個";
            }
        }

        protected void Btn_order_Click(object sender, EventArgs e)
        {
            drinkDataselect.Insert();
            SqlConnection orderConnect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\accountDatabase.mdf;Integrated Security=True");
            orderConnect.Open();
            SqlCommand orderCmd = new SqlCommand("SELECT top 1 order_id FROM [orderData] ORDER BY order_id DESC", orderConnect);
            SqlDataReader orderReader = orderCmd.ExecuteReader();
            if (orderReader.Read())
            {
                Session["order_id"] = orderReader["order_id"];
                Btn_order.Text = orderReader["order_id"] + " 號訂單";
                //Btn_order.Enabled = false;
            }
        }

        protected void Btn_refresh_Click(object sender, EventArgs e)
        {
            SqlConnection orderConnect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\accountDatabase.mdf;Integrated Security=True");
            orderConnect.Open();
            SqlCommand orderRefreshCmd = new SqlCommand("TRUNCATE TABLE orderData", orderConnect);
            orderRefreshCmd.ExecuteNonQuery();
            Btn_order.Text = "前往選購";
        }
    }
}