using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.Security;




public partial class MasterPage : System.Web.UI.MasterPage
{
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BloodGroupAndLocation.dataload(ddl_bloodgroup, ddl_location);
            EventLoad();
        }

    }

   

    protected void btn_search_Click(object sender, EventArgs e)
    {
        int BloodId = ddl_bloodgroup.SelectedIndex;
        int LocationID = ddl_location.SelectedIndex;
        Response.Redirect("DonarList.aspx?Location=" + LocationID+"&BloodGroup=" + BloodId);
    }

    protected void btn_Login_Click(object sender, EventArgs e)
    {
        if (Membership.ValidateUser(txtUsername.Text, txtPassword.Text))
        {
            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
            Session["UserName"] = txtUsername.Text;
            Response.Redirect("/user/UserProfile.aspx");
        }
    }
    BLLEvent ble = new BLLEvent();
    protected void EventLoad()
    {
        {
            DataTable dt = ble.GetEvent_Today();
            if (dt.Rows.Count > 0)
            {
                rptEvent.DataSource = dt;
                rptEvent.DataBind();
            }
        }
    }


    protected void btnPostEvent_Click(object sender, EventArgs e)
    {
        Response.Redirect("User/PostEventForm.aspx");
        //Server.Transfer("~/User/PostEventForm.aspx", true);
    }
}

    