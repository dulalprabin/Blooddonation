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
        
       



        userid = Request.QueryString["VerCode"];
        emailaddress = Request.QueryString["EmailId"];
        if (!IsPostBack)
        {
            List<DistrictInfo> li = BLLDistrict.GetAllDistrict();
            DropDownListDistrict.DataSource = li;
            DropDownListDistrict.DataTextField = "DistrictName";
            DropDownListDistrict.DataValueField = "DistrictId";
            DropDownListDistrict.DataBind();
            DropDownListDistrict.Items.Insert(0, "Choose District");
            DropDownBldGrp.DataSource = BLLBloodGroup.GetAllBloodGroup();
            DropDownBldGrp.DataTextField = "BloodGroup";
            DropDownBldGrp.DataValueField = "BloodGroupId";
            DropDownBldGrp.DataBind();
            DropDownBldGrp.Items.Insert(0, "Blood Type");
            DataTable dt= BLLUser.SelectField(Session["UserName"].ToString());
            txtname.Text = dt.Rows[0]["FullName"].ToString();
            txtemail.Text = dt.Rows[0]["EmailAddress"].ToString();
        }
        if (!IsPostBack)
        {
            if (emailaddress != null)
            {
                ValidateUser();
            }
        }
        if (BLLUser.CheckIsApproved(Session["UserName"].ToString()))
        {
            MultiView1.ActiveViewIndex = 2;
        }
        else
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
    string emailaddress;
    private void ValidateUser()
    {

      DataTable dt = BLLUser.SelectFields(userid);
      string password = dt.Rows[0]["Pasword"].ToString();
        string name = dt.Rows[0]["UserName"].ToString();
        string fname = dt.Rows[0]["FullName"].ToString();
        int usergroupid = Convert.ToInt32(dt.Rows[0]["UserGroupId"].ToString());
        BLLUser.UpdateUserStatus(userid);
        if (BLLUser.LoginUser(name, password).Rows.Count>0)
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

            txtname.Text = fname;
            txtemail.Text = emailaddress;

        }




    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveToDatabase();  
    }
    void SaveToDatabase()
    {
        DataTable dt = BLLUser.SelectField(Session["UserName"].ToString());

        int userid = Convert.ToInt32( dt.Rows[0]["UserId"].ToString());
        string besttime="";
        foreach (ListItem item in chkContactTime.Items)
        {
            if (item.Selected)
            {
                 besttime+=item.Value.ToString()+"/n";
              
            }

        }
         string profilepicture="";
        if (FileUpload1.HasFile)
        {
            FileUpload1.SaveAs(Server.MapPath("~/Assets/Images/Donor/" + FileUpload1.FileName));
            profilepicture = "~/Assets/Images/Donor/" + FileUpload1.FileName;
        }
        string gender="";
        if (RadioButton1 .Checked)
        {gender=RadioButton1.Text;}
        else{gender=RadioButton2.Text;}
        MemberInfo member = new MemberInfo(userid,txtname.Text.Trim(), txtCurrentAddress.Text.Trim(), Convert.ToInt32(DropDownListDistrict.SelectedValue.ToString()), Convert.ToInt32(DropDownBldGrp.SelectedValue.ToString()), gender, besttime, txtContactNo.Text.Trim(), txtemail.Text.Trim(), profilepicture);
        BLLUser.SaveMemberToDatabase(member);


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
            ActivationUrl = Server.HtmlEncode("http://localhost:32057/User/DonarDetails.aspx?VerCode=" + FetchUserId(emailId) + "&EmailId=" + emailId);

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
        string commandtext = string.Format("Select UserId,UserGroupId, Password,FullName from TblUserApproval where EmailAddress='{0}'", Session["Emailid"].ToString());
        SqlCommand cmd = new SqlCommand(commandtext, con);
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        string password = dt.Rows[0]["Password"].ToString();
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
    protected void DropDownListDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
