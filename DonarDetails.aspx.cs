using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DonarDetails : System.Web.UI.Page
{
    string userid;
    string name;
    protected void Page_Load(object sender, EventArgs e)
    {
        userid = Request.QueryString["VerCode"];
        emailaddress = Request.QueryString["EmailId"];
       
       
        if (!IsPostBack)
        {
            ValidateUser();
            
           
        }
    }
    string emailaddress;
    private void ValidateUser()
    {
        
        SqlConnection conn = ConnectionHelper.GetConnection();
        SqlCommand cmd = new SqlCommand();
        string select = string.Format("Select FullName,Pasword,UserGroupId from TblUserApproval where UserID={0}", Convert.ToInt32(userid));
        cmd.CommandText = select;
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);

        string password = dt.Rows[0]["Pasword"].ToString();
         name = dt.Rows[0]["FullName"].ToString();
       int usergroupid =Convert.ToInt32( dt.Rows[0]["UserGroupId"].ToString());
        string update = string.Format("Update TblUserApproval set IsApproved='True' where UserId={0}", Convert.ToInt32(userid));
        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandText = update;
        cmd1.Connection = conn;
        cmd1.CommandType = CommandType.Text;
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        cmd1.ExecuteNonQuery();
        conn.Close();
        if (BLLUser.LoginUser(emailaddress, password))
        {
            Session["UserName"] = name;
            if(usergroupid==1 || usergroupid==2)
            {

            }
            else if(usergroupid==3)
            {
                txtname.Text = Session["UserName"].ToString();
                txtemail.Text = emailaddress;

               // Response.Redirect("~/User/DonarDetails.aspx");
                
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
            
           

        }




    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}