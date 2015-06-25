using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        
        if (Session["UserName"] == null)
        {
            this.Page.MasterPageFile = "~/MasterPage.master";
        }
        else 
        {
            DataTable dt = BLLUser.SelectField(Session["UserName"].ToString());
            int usergroupid = Convert.ToInt32(dt.Rows[0]["UserGroupId"].ToString());
            if (usergroupid==2)
            {
                this.Page.MasterPageFile = "~/Admin/Admin.master";
                
            }
            else if(usergroupid==3)
            {
                this.Page.MasterPageFile = "~/User/User.master";
                
            }
            
        }
        
    }
   
}