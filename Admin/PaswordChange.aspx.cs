using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PaswordChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblUsername.Text = Session["UserName"].ToString();
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        SqlConnection con = ConnectionHelper.GetConnection(); 
        string sql="Update TblUserApproval set Password='"+txtNewPassword.Text+"' where UserName='"+lblUsername.Text+"' and Password='"+txtOldPassword.Text+"'";
        try
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Your Password is successfully changed.');", true);
            Response.Redirect("UserManagement.aspx");
        }
        catch (Exception E)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error:'"+E.Message.ToString()+"');", true);
        }
        finally
        {
            con.Close();
        }
    }
}