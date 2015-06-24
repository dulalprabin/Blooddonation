using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DonarDetails : System.Web.UI.Page
{
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //userid = Request.QueryString["VerCode"];
        //emailaddress = Request.QueryString["EmailId"];
        //if (!IsPostBack)
        //{
        //    if (Session["UserName"] != null)
        //    {
        //        txtname.Text = Session["UserName"].ToString();
        //        txtemail.Text = Session["Emailid"].ToString();
        //    }
        //    else
        //    {
        //        txtname.Text = emailaddress;

        //    }
        //}
        //if (!IsPostBack)
        //{
        //    if (emailaddress != null)
        //    {
        //        ValidateUser();
        //    }
        //}
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
        string name = dt.Rows[0]["FullName"].ToString();
        int usergroupid = Convert.ToInt32(dt.Rows[0]["UserGroupId"].ToString());
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
            if (usergroupid == 1 || usergroupid == 2)
            {

            }
            else if (usergroupid == 3)
            {
                txtname.Text = Session["UserName"].ToString();
                // Response.Redirect("~/User/DonarDetails.aspx");
            }
            else
            {
                Response.Redirect("Home.aspx");
            }

            txtname.Text = name;
            txtemail.Text = emailaddress;

        }




    }
    public bool CheckUserValidation(int id)
    {
        SqlConnection conn = ConnectionHelper.GetConnection();
        SqlCommand cmd = new SqlCommand();
        string select = string.Format("Select IsApproved  from TblUserApproval where UserID={0}", id);
        cmd.CommandText = select;
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);

        string status = dt.Rows[0]["IsApproved"].ToString();
        if (status == "true")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    public void SendVerificationEmail()
    {
        try
        {
            MailMessage msg;

            string ActivationUrl = string.Empty;
            string emailId = string.Empty;

            //Sending activation link in the email
            msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string emailid = Session["EmailId"].ToString();
            string name = Session["UserName"].ToString();
            emailId = emailid.Trim();
            //sender email address
            msg.From = new MailAddress("dulalprabin@gmail.com");
            //Receiver email address
            msg.To.Add(emailId);
            msg.Subject = "Confirmation email for account activation";

            //For testing replace the local host path with your lost host path and while making online replace with your website domain name
            ActivationUrl = Server.HtmlEncode("http://localhost:32057/DonarDetails.aspx?VerCode=" + FetchUserId(emailId) + "&EmailId=" + emailId);

            msg.Body = "Hi " + name.Trim() + "!\n" +
                  "Thanks for registering in <a href='Home.aspx'>oursite<a> " +
                  " Please <a href='" + ActivationUrl + "'>click here to activate</a>  your account and enjoy our services. \nThanks!";
            msg.IsBodyHtml = true;
            smtp.Credentials = new NetworkCredential("dulalprabin@gmail.com", "dudl1234");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Confirmation Link to activate your account has been sent to your email address');", true);
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
            //btnVerify.Enabled = true;
            return;
        }

    }
    private string FetchUserId(string emailId)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = ConnectionHelper.GetConnection();

        cmd = new SqlCommand("SELECT UserId FROM TblUserApproval WHERE EmailAddress=@EmailId", con);
        cmd.Parameters.AddWithValue("@EmailId", emailId);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string UserID = Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        cmd.Dispose();
        return UserID;
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        btnVerify.Enabled = false;
        if (RadioButtonEmail.Checked)
        {
            
            try
            {

          SendVerificationEmail();
          

            }
            catch (Exception ex)
            {


            }
        }
        else
        {
           
            //try
            //{

            //    string message = "Your Verification Code is" + "\n" + FetchUserId(Session["EmailId"].ToString()) + "\n" + "Enter it into the required Field";

            //    var response = PostSendSMS("Demo", "R7xN6drJikuo3SPZmXOA", Txtmobile.Text.Trim(), message);
            //    var data = JObject.Parse(response);
            //    var responsecode = data["response_code"];
            //    int responseid = Convert.ToInt32(responsecode);
            //    if (responseid == 200)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Verification Code has been sent to your mobile number');", true);

            //        MultiView1.ActiveViewIndex = 2;
            //        //}
            //        //else
            //        //{
            //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error in Sending Verification Code');", true);
            //        //    btnVerify.Enabled = true;
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            MultiView1.ActiveViewIndex = 1;
        }
    }

    protected void btnsend_Click(object sender, EventArgs e)
    {
        int vericode = Convert.ToInt32(TxtVerificationCode.Text.Trim());
        CheckVerificationCode(vericode);
    }

    public void CheckVerificationCode(int vercode)
    {
        SqlConnection con = ConnectionHelper.GetConnection();
        string commandtext = string.Format("Select UserId,UserGroupId, Pasword,FullName from TblUserApproval where EmailAddress='{0}'", Session["Emailid"].ToString());
        SqlCommand cmd = new SqlCommand(commandtext, con);
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        string password = dt.Rows[0]["Pasword"].ToString();
        string name = dt.Rows[0]["FullName"].ToString();
        int verificationcode = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
        int usergroupid = Convert.ToInt32(dt.Rows[0]["UserGroupId"].ToString());
        if (verificationcode == vercode)
        {

            string update = string.Format("Update TblUserApproval set IsApproved='True' where UserId={0}", verificationcode);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = update;
            cmd1.Connection = con;
            cmd1.CommandType = CommandType.Text;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Acoount Verified Succesfully');", true);
            txtname.Text = Session["UserName"].ToString();
            txtemail.Text = Session["Emailid"].ToString();
               MultiView1.ActiveViewIndex=2;
            }

        }
    }
