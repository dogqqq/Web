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
            sweetList.SelectedIndex = 0;
            iceList.SelectedIndex = 0;
        }

        private void initial()
        {
            drinkList.SelectedIndex = 0;
            cupList.SelectedIndex = 0;
            sweetList.SelectedIndex = 0;
            iceList.SelectedIndex = 0;
            label_drinkPrice.Text = "";
            label_drinkQT.Text = "";
            Image_drink.ImageUrl = "./pic/未選取.jpg";

            Btn_order.Text = "前往選購";
            Btn_order.Enabled = true;

            cupLB.Visible = false;
            cupList.Visible = false;
            sweetList.Visible = false;
            iceList.Visible = false;
            addDrinkBtn.Visible = false;
            addDrinkBtn.Enabled = false;
            orderItemGridView1.Visible = false;
            totalLabel.Visible = false;
            checkBtn.Visible = false;
            cancelBtn.Visible = false;
            errorLabel.Visible = false;
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
                int tmp;
                label_drinkPrice.Text = drinkDetailsView.Rows[0].Cells[1].Text + " 元";
                label_drinkQT.Text = "\t庫存: " + drinkDetailsView.Rows[1].Cells[1].Text + " 個";
                addDrinkBtn.Enabled = true;
                label_drinkPrice.ForeColor = System.Drawing.Color.Black;
                label_drinkQT.ForeColor = System.Drawing.Color.Black;

                tmp = Convert.ToInt32(Session["deposit"]);

                if (Convert.ToInt32(Session["deposit"]) < Convert.ToInt32(drinkDetailsView.Rows[0].Cells[1].Text))
                {
                    label_drinkPrice.ForeColor = System.Drawing.Color.Red;
                    label_drinkPrice.Text += " 餘額不足";
                    addDrinkBtn.Enabled = false;
                }

                if (Convert.ToInt32(drinkDetailsView.Rows[0].Cells[1].Text) <= 0)
                {
                    label_drinkQT.ForeColor = System.Drawing.Color.Red;
                    label_drinkQT.Text += " 庫存不足";
                    addDrinkBtn.Enabled = false;
                }
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

            initial();

        }

        protected void addDrinkBtn_Click(object sender, EventArgs e)
        {
            orderItemDataSource.Insert();
            if (!orderItemGridView1.Visible)
            {
                orderItemGridView1.Visible = true;
            }
            cupList.SelectedIndex = 0;
            totalLabel.Visible = true;
            checkBtn.Visible = true;
            cancelBtn.Visible = true;
        }

        protected void orderItemGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            errorLabel.Text = "";
            errorLabel.Visible = false;
            checkBtn.Enabled = true;
            countTotal();
            qtCheck();
            qtCheckGridView.DataBind();
        }

        private void qtCheck()
        {
            int num;
            int qt;
            bool isError = false;
            for(int i = 0; i < qtCheckGridView.Rows.Count; i++)
            {
                if (qtCheckGridView.Rows[i].Cells[1].FindControl("QtNameLabel") != null &&
                    qtCheckGridView.Rows[i].Cells[2].FindControl("totalNumLabel") != null &&
                    qtCheckGridView.Rows[i].Cells[3].FindControl("checkQtLabel") != null)
                {
                    using (Label qtNameLabel = (Label)qtCheckGridView.Rows[i].Cells[1].FindControl("QtNameLabel"),
                        totalNumLabel = (Label)qtCheckGridView.Rows[i].Cells[2].FindControl("totalNumLabel"),
                        checkQtLabel = (Label)qtCheckGridView.Rows[i].Cells[3].FindControl("checkQtLabel"))
                    {
                        num = Convert.ToInt32(totalNumLabel.Text);
                        qt = Convert.ToInt32(checkQtLabel.Text);
                        if(num > qt)
                        {
                            errorLabel.Text += "<br>" + qtNameLabel.Text + "庫存不足(剩下" + qt + "杯)";
                            errorLabel.Visible = true;
                            checkBtn.Enabled = false;
                            isError = true;
                        }
                    }

                }
            }
        }

        private void countTotal()
        {
            int total = 0;
            string warnMessage = "";
            for(int i = 0; i < orderItemGridView1.Rows.Count; i++)
            {
                if(orderItemGridView1.Rows[i].Cells[4].FindControl("subTotalLabel") != null)
                {
                    total += Convert.ToInt32(((Label)orderItemGridView1.Rows[i].Cells[4].FindControl("subTotalLabel")).Text);
                }
                cupEditCheck(ref warnMessage, i);
            }
            totalLabel.Text = warnMessage + "總價格: " + total + "元";

            Session["totalMoney"] = total;

            if( 0 == total){
                checkBtn.Enabled = false;
            }
            if (Convert.ToInt32(Session["deposit"]) < total)
            {
                checkBtn.Enabled = false;
                errorLabel.Text = "餘額不足";
                errorLabel.Visible = true;

            }
        }

        private void cupEditCheck(ref string message, int i)
        {
            if (orderItemGridView1.Rows[i].Cells[3].FindControl("itemCupLabel") != null)
            {
                using (Label tempCupLabel = (Label)orderItemGridView1.Rows[i].Cells[3].FindControl("itemCupLabel"))
                {
                    if(tempCupLabel.Text == "0")
                    {
                        message = "(錯誤的杯數)";
                        tempCupLabel.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        tempCupLabel.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        protected void orderItemGridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if(orderItemGridView1.Rows.Count == 1)
            {
                totalLabel.Visible = false;
                errorLabel.Visible = false;
                checkBtn.Enabled = false;
            }
        }

        protected void editCupTextBox_TextChanged(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (char ch in ((TextBox)sender).Text)
            {
                flag = 1;
                if (!Char.IsDigit(ch))
                {
                    ((TextBox)sender).Text = "0";
  
                }
            }
            if(flag == 0 && ((TextBox)sender).Text != "0")
            {
                ((TextBox)sender).Text = "0";
            }
        }

        protected void checkBtn_Click(object sender, EventArgs e)
        {
            Session["deposit"] = Convert.ToInt32(Session["deposit"]) - Convert.ToInt32(Session["totalMoney"]);
            Lable_userShow.Text = Session["name"] + "歡迎光臨<br>您還剩下: " + Session["deposit"] + "元";
            clientDataSource.Update();
            updateQt();
            initial();
        }

        private void updateQt()
        {
            int num;
            int qt;
            bool isError = false;
            for (int i = 0; i < qtCheckGridView.Rows.Count; i++)
            {
                if (qtCheckGridView.Rows[i].Cells[0].FindControl("QtIdLabel") != null &&
                    qtCheckGridView.Rows[i].Cells[2].FindControl("totalNumLabel") != null &&
                    qtCheckGridView.Rows[i].Cells[3].FindControl("checkQtLabel") != null)
                {
                    using (Label QtIdLabel = (Label)qtCheckGridView.Rows[i].Cells[0].FindControl("QtIdLabel"),
                        totalNumLabel = (Label)qtCheckGridView.Rows[i].Cells[2].FindControl("totalNumLabel"),
                        checkQtLabel = (Label)qtCheckGridView.Rows[i].Cells[3].FindControl("checkQtLabel"))
                    {
                        num = Convert.ToInt32(totalNumLabel.Text);
                        qt = Convert.ToInt32(checkQtLabel.Text);
                        Session["updateQtId"] = Convert.ToInt32(QtIdLabel.Text);
                        Session["updateQtNum"] = qt - num;
                        drinkQTDataSource.Update();
                    }

                }
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            drinkDataselect.Delete();
            cancelOrderDataSource.Delete();
            initial();
        }
    }
}