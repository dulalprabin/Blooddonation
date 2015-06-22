using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BloodGroupAndLocation
/// </summary>
public class BloodGroupAndLocation
{
	public BloodGroupAndLocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet Location()
    {
        using (SqlConnection con = ConnectionHelper.GetConnection())
        {
            SqlDataAdapter daa = new SqlDataAdapter("select LocationName from TblLocation", con);
            DataSet dss = new DataSet();
            daa.Fill(dss, "tbl_locationnn");

            return dss;
           
        }
    }

    public static DataSet BloodGroup()
    { 
        using (SqlConnection con= ConnectionHelper.GetConnection())
        {
            string query = "Select BloodGroup from TblBloodGroup";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_fill");
             return ds;
        }
    }
    public static void  dataload(DropDownList bloodgroup, DropDownList location)
    {
        bloodgroup.DataSource = BloodGroup();
        bloodgroup.DataTextField = "BloodGroup";
        bloodgroup.DataBind();
        bloodgroup.Items.Insert(0, "Blood Type");


        location.DataSource = BLLLocation.GetAllLocation();
        location.DataTextField = "LocationName";
        location.DataBind();
        location.Items.Insert(0, "Choose Location");


    }
    public static void bloodgroupLoad(DropDownList bloodgroup)
    {
        bloodgroup.DataSource = BloodGroup();
        bloodgroup.DataTextField = "BloodGroup";
        bloodgroup.DataBind();
        bloodgroup.Items.Insert(0, "Blood Type");
    }
}