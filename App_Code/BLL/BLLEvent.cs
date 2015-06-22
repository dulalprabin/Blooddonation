using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BLLEvent
/// </summary>
public class BLLEvent
{
	public BLLEvent()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable GetEvent_ByEventId(int EventId)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@eventId", EventId)
        };

         return ConnectionHelper.GetTable("SELECT * FROM TblEvent where EventId=@EventId", param);
    }


    public static DataTable GetEvent_ByUserId(int UserId)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@userId", UserId)
        };

        return ConnectionHelper.GetTable("SELECT * FROM TblEvent where UserId=@userId", param);
    }

    public DataTable GetEvent_All()
    {
        return ConnectionHelper.GetTable("SELECT * FROM TblEvent", null);
    }
    public DataTable GetEvent_Today()
    {
        return ConnectionHelper.GetTable("SELECT * FROM TblEvent", null);
    }

    public static int CreateEvent(EventInfo _Event)
    {
        try
        {
            using (SqlConnection con = ConnectionHelper.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "Usp_Event_Add";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@eventtitle", _Event.EventTitle);
                    cmd.Parameters.AddWithValue("@date", _Event.Date);
                    cmd.Parameters.AddWithValue("@startingtime", _Event.StartingTime);
                    cmd.Parameters.AddWithValue("@endtime", _Event.EndingTime);
                    cmd.Parameters.AddWithValue("@venue", _Event.Venue);
                    cmd.Parameters.AddWithValue("@postedby", _Event.PostedBy);
                    cmd.Parameters.AddWithValue("@description", _Event.Description);
                    cmd.Parameters.AddWithValue("@status", _Event.Status);
                    cmd.Parameters.AddWithValue("@image", _Event.Image);
                    cmd.Parameters.AddWithValue("@issuedate", _Event.IssueDate);

                    return cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int AllowEvent(int id, bool Status)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@EventId",id),
            new SqlParameter("@Status",Status)
            
            

        };
        return ConnectionHelper.ExecuteProcedure("Update TblEvent set Status=@Status Where EventID=@EventId", param);
    }

    

}
