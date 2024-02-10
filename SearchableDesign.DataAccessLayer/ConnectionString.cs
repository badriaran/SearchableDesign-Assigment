using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Infrastructure
{
    public static class ConnectionString
    {
        public static string? DBConnectionString { get; set; }

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection con = new SqlConnection(DBConnectionString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                return con;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
