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
            if (drinkList.SelectedItem == null)
            {
                Image_drink.ImageUrl = "./pic/未選取.jpg";
            }
            else
            {
                Image_drink.ImageUrl = "./pic/" + drinkList.SelectedItem.Text.ToString() + ".jpg";
            }
            //Image_drink.ImageUrl = "./pic/未選取.jpg";
            //for (int i = 0; i < 50; i++)
            //{
            //    cupList.Items.Insert(i, new ListItem("" + (i + 1), "" + (i + 1)));
            //}
            //cupList.SelectedIndex = 0;
        }

        protected void drinkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image_drink.ImageUrl = "./pic/" + drinkList.SelectedItem.Text.ToString() + ".jpg";
            if (0 == drinkList.SelectedIndex)
            {
                label_drinkPrice.Text = "";
                label_drinkQT.Text = "";
                addDrinkBtn.Enabled = false;
            }
            else {
                label_drinkPrice.Text = drinkDetailsView.Rows[0].Cells[1].Text + " 元";
                label_drinkQT.Text = "\t庫存: " + drinkDetailsView.Rows[1].Cells[1].Text + " 個";
                addDrinkBtn.Enabled = true;
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
                Btn_order.Enabled = false;

                cupLB.Visible = true;
                cupList.Visible = true;
                sweetList.Visible = true;
                iceList.Visible = true;
                addDrinkBtn.Visible = true;

            }
            for (int i = 0; i < 50; i++)
            {
                cupList.Items.Insert(i, new ListItem("" + (i + 1), "" + (i + 1)));
            }
        }

        protected void Btn_refresh_Click(object sender, EventArgs e)
        {
            SqlConnection orderConnect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\accountDatabase.mdf;Integrated Security=True");
            orderConnect.Open();
            SqlCommand orderRefreshCmd = new SqlCommand("TRUNCATE TABLE orderData", orderConnect);
            orderRefreshCmd.ExecuteNonQuery();
            SqlCommand orderItemRefreshCmd = new SqlCommand("TRUNCATE TABLE order_itemData", orderConnect);
            orderItemRefreshCmd.ExecuteNonQuery();
            Btn_order.Text = "前往選購";
            Btn_order.Enabled = true;

            cupLB.Visible = false;
            cupList.Visible = false;
            sweetList.Visible = false;
            iceList.Visible = false;
            addDrinkBtn.Visible = false;
            orderItemGridView1.Visible = false;

        }

        protected void addDrinkBtn_Click(object sender, EventArgs e)
        {
            orderItemDataSource.Insert();
            if (!orderItemGridView1.Visible)
            {
                orderItemGridView1.Visible = true;
            }
            cupList.SelectedIndex = 0;
        }
    }
}