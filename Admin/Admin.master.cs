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
       
            LblUser.Text = "Welcome" + " " + Session["UserName"].ToString();
        
    }
    protected void btnPostEvent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/User/PostEventForm.aspx");
    }

    protected void lnkSignOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Home.aspx");
    }

    protected void lnkChangePass_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/PaswordChange.aspx");
    }
}
