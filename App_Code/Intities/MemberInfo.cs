using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MemberInfo
/// </summary>
public class MemberInfo
{
    public MemberInfo()
    {

    }
	public MemberInfo(int memberid,string fullname,string currentaddress,int districtid,
        int bloodgroupid,string gender,string besttime,string mobileno,string email,string profilepic)
	{
        MemberId = memberid;
        FullName = fullname;
        CurrentAddress = currentaddress;
        District = districtid;
        BloodGroupId = bloodgroupid;
        Gender = gender;
        BestTime = besttime;
        MobileNo = mobileno;
        Email = email;
        ProfilePicture = profilepic;
	}
    public int MemberId { get; set; }
    public string FullName { get; set; }
    public string CurrentAddress { get; set; }
    public int District { get; set; }
  //  public DateTime DOB { get; set; }
    public int BloodGroupId { get; set; }
    public string Gender { get; set; }
    public string BestTime { get; set; }
    public string MobileNo { get; set; }
   // public string PhoneNo { get; set; }
    public string Email { get; set; }
  //  public bool AccountStatus { get; set; }
   // public bool RoleId { get; set; }
    public string ProfilePicture { get; set; }
}