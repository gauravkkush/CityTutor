using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks; 

namespace CityTutor1.App_Code
{
    public class DBMANAGER
    {
        SqlConnection con;
        SqlCommand cmd;
        public string cmdtxt;
        public DBMANAGER()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConString"].ToString());        
        }
        public object GetSingleValue()
        {
            cmd = new SqlCommand(cmdtxt, con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            Object value = cmd.ExecuteScalar();
            con.Close();
            return value;
        }
        public Boolean ExecuteInsertUpdateDelete()
        {
            cmd = new SqlCommand(cmdtxt, con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            return n > 0 ? true : false;
        }
        public DataTable GetBulkData()
        {
            SqlDataAdapter da = new SqlDataAdapter(cmdtxt, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}