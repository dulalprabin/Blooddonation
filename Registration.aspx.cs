using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Registration : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //TxtPassword.Attributes["type"] = "password";
        txtConfirmPassword.Attributes["type"] = "password";
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {


        MultiView2.ActiveViewIndex = 1;
    }
    public void InsertUser(string name, string emailid, string password, string contactno)
    {
        SqlConnection con = ConnectionHelper.GetConnection();

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd = new SqlCommand("insert into TblUserApproval (FullName,EmailAddress,Pasword,Mobilenumber) values (@Name,@EmailId,@Pasword,@ContactNo) ", con);

            cmd.Parameters.AddWithValue("@Name", name.Trim());
            cmd.Parameters.AddWithValue("@EmailId", emailid.Trim());
            cmd.Parameters.AddWithValue("@Pasword", password.Trim());
            cmd.Parameters.AddWithValue("@ContactNo", contactno.Trim());
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
            string emailid = TxtEmail.Text.Trim();
            string name = TxtFirstName.Text.Trim();
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
            btnVerify.Enabled = true;
            return;
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
    //private void clear_controls()
    //{
    //    txtName.Text = string.Empty;
    //    txtEmailId.Text = string.Empty;
    //    txtContactNo.Text = string.Empty;
    //    txtName.Focus();


    //}

    protected void CreateMembers()
    {
        //MemberInfo _member = new MemberInfo();
        //_member.FullName = TxtFirstName.Text;
        //_member.MobileNo = Txtmobile.Text;
        //_member.Email = TxtEmail.Text;
        //_member.BloodGroupId = Ddlbloodgroup.SelectedIndex;


        //BLLUser.CreateUser(_member);

    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        btnVerify.Enabled = false;
        if (RadioButtonEmail.Checked)
        {
            try
            {
                InsertUser(TxtFirstName.Text.Trim() + " " + TxtLastName.Text.Trim(), TxtEmail.Text.Trim(), TxtPassword.Text.Trim(), Txtmobile.Text.Trim());
                SendVerificationEmail();
                Panel1.Visible = false;
            }
            catch (Exception ex)
            {


            }

        }
        else
        {
            try
            {
                //InsertUser(TxtFirstName.Text.Trim() + " " + TxtLastName.Text.Trim(), TxtEmail.Text.Trim(), TxtPassword.Text.Trim(), Txtmobile.Text.Trim());
                //string message = "Your Verification Code is" + "\n" + FetchUserId(TxtEmail.Text.Trim()) + "\n" + "Enter it into the required Field";

                //var response = PostSendSMS("Demo", "R7xN6drJikuo3SPZmXOA", Txtmobile.Text.Trim(), message);
                //var data = JObject.Parse(response);
                //var responsecode = data["response_code"];
                //int responseid = Convert.ToInt32(responsecode);
                //if (responseid == 200)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Verification Code has been sent to your mobile number');", true);
                Panel1.Visible = false;
                MultiView2.ActiveViewIndex = 2;
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error in Sending Verification Code');", true);
                //    btnVerify.Enabled = true;
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int vericode = Convert.ToInt32(TxtVerificationCode.Text.Trim());
        CheckVerificationCode(vericode);
    }
    public void CheckVerificationCode(int vercode)
    {
       SqlConnection con = ConnectionHelper.GetConnection();
        string commandtext = string.Format("Select UserId,UserGroupId, Pasword,FullName from TblUserApproval where EmailAddress='{0}'", TxtEmail.Text.Trim());
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
            if (BLLUser.LoginUser(TxtEmail.Text.Trim(), password))
            {
                Session["UserName"] = name;
                Session["Emailid"] = TxtEmail.Text.Trim();

                if (usergroupid == 1 || usergroupid == 2)
                {

                }
                else if (usergroupid == 3)
                {
                    
                    Response.Redirect( string.Format("~/User/DonarDetails.aspx?Vercode={0}",verificationcode));
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }

        }
    }
}