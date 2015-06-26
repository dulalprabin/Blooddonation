using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BloodDonation : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
         LblUser.Text = "Welcome"+" " + Session["UserName"].ToString();
         DataTable dt = BLLUser.SelectField(Session["UserName"].ToString());
         Session["UserGroupID"] = dt.Rows[0]["UserGroupId"].ToString();
         int usergroupid = Convert.ToInt32(Session["UserGroupID"]);
         if (usergroupid == 2) 
            {
                menuPostEvent.Visible = true;
                menuProfile.Visible = true;
                menuAdminUserManagement.Visible = true;
                menuAdminEventVerify.Visible = true;
                menuAdminVerifyAdvBldReq.Visible = true;
                menuAdminSetting.Visible = true;
                menuAdvBldReq.Visible = false;
            }
         else if (usergroupid == 3)
           {
                  menuPostEvent.Visible = true;
                  menuProfile.Visible = true;
                  menuAdminUserManagement.Visible = false;
                  menuAdminEventVerify.Visible = false;
                  menuAdminVerifyAdvBldReq.Visible = false;
                  menuAdminSetting.Visible = false;    
           }
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
        if (Convert.ToInt32(Session["UserGroupID"].ToString()) == 2)
            Response.Redirect("~/Admin/PaswordChange.aspx");
        else
        Response.Redirect("~/User/PaswordChange.aspx");
    }
}
