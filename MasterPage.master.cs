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
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Text;




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
        DataTable dt1 = BLLUser.LoginUser(txtUsername.Text.Trim(), txtPassword1.Text.Trim());
        if (dt1.Rows.Count>0)
        {
            int usergroupid =Convert .ToInt32( dt1.Rows[0]["UserGroupId"].ToString());
          //  FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
            Session["UserName"] = txtUsername.Text;
            if (usergroupid == 3)
            {
                Response.Redirect("/User/DonarDetails.aspx");
            }
            else if(usergroupid==2)
            {
                Response.Redirect("/Admin/UserManagement.aspx");
            }

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
        if (Session["UserName"]==null)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('You dont have permission to post Events. Please Login.');", true);
        }
        else
        {
            if(BLLUser. CheckIsApproved(Session["Username"].ToString()))
            {
                 Response.Redirect("User/PostEventForm.aspx");
            }
            else
            {
                Response.Redirect("User/DonarDetails.aspx");
            }

        }
       
        //Server.Transfer("~/User/PostEventForm.aspx", true);
    }
  
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUser(TxtFirstName.Text.Trim() + " " + TxtLastName.Text.Trim(),txtuser.Text.Trim(), TxtEmail.Text.Trim(), TxtPassword.Text.Trim(), Txtmobile.Text.Trim());
         if(   BLLUser.LoginUser(txtuser.Text.Trim(), TxtPassword.Text.Trim()).Rows.Count>0)
         {
             Session["UserName"] = txtuser.Text.Trim();
             Session["EmailId"] = TxtEmail.Text.Trim();
            
             Response.Redirect("User/DonarDetails.aspx",false);
             
         }
          
           
        }

        catch(Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
        }

    }




    public void InsertUser(string name, string username, string emailid, string password, string contactno)
    {
        SqlConnection con = ConnectionHelper.GetConnection();

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd = new SqlCommand("insert into TblUserApproval (FullName,EmailAddress,Password,Mobilenumber,UserGroupid,UserName) values (@Name,@EmailId,@Pasword,@ContactNo,@usergroupid,@username) ", con);

            cmd.Parameters.AddWithValue("@Name", name.Trim());
            cmd.Parameters.AddWithValue("@EmailId", emailid.Trim());
            cmd.Parameters.AddWithValue("@Pasword", password.Trim());
            cmd.Parameters.AddWithValue("@ContactNo", contactno.Trim());
            cmd.Parameters.AddWithValue("@usergroupid", 3);
            cmd.Parameters.AddWithValue("@username", username.Trim());
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
    }
    
   
    private static string PostSendSMS(string from, string token, string to, string text)
    {
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["from"] = from;
            values["token"] = token;
            values["to"] = to;
            values["text"] = text;
            var response = client.UploadValues("http://api.sparrowsms.com/v2/sms/", "Post", values);
            var responseString = Encoding.Default.GetString(response);
            return responseString;
        }
    }
}

    