using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_practice
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DetailsView_account.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DetailsView_account.Visible = false;
            LinkBtn_store.Visible = false;
            if (DetailsView_account.DataItemCount == 1)
            {
                Session["name"] = DetailsView_account.Rows[0].Cells[1].Text;
                Session["deposit"] = DetailsView_account.Rows[1].Cells[1].Text;
                LinkBtn_store.Visible = true;
            }
            else {
                Session["name"] = null;
                Session["deposit"] = null;
                DetailsView_account.Visible = true;
            }
        }

        protected void text_passwd_TextChanged(object sender, EventArgs e)
        {

        }

        protected void LinkBtn_store_Click(object sender, EventArgs e)
        {

        }
    }
}