using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            LblUser.Text = "Welcome" + " " + Session["UserName"].ToString();
        }
    }
    protected void lnbLogout_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        Response.Redirect("~/Home.aspx");
    }
    protected void btnPostEvent_Click(object sender, EventArgs e)
    {

    }
}
