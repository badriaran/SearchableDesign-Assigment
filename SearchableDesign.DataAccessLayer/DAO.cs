using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Infrastructure
{
    public static class DAO
    {
        public static int IDU(string sql, CommandType cmdType, SqlParameter[] param)
        {
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        return i;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
        public static DataTable GetTable(string sql, CommandType cmdType, SqlParameter[] param)
        {
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        public static OutputMessage ExecuteStoredProcedure(string StoredProcName, SqlParameter[] param)
        {
            OutputMessage outputMessage = new OutputMessage();
            try
            {
                using (SqlConnection con = ConnectionString.GetConnection())
                {
                    try
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = StoredProcName;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (param != null)
                            {
                                cmd.Parameters.AddRange(param);
                            }
                            cmd.Parameters["@v_out_status"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@v_out_message"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            outputMessage.Msg_Params = cmd.Parameters;
                            outputMessage.Status = cmd.Parameters["@v_out_status"].Value.ToString();
                            outputMessage.Message = cmd.Parameters["@v_out_message"].Value.ToString();
                            outputMessage.ErrorMessage = cmd.Parameters["@v_out_message"].Value.ToString();
                            return outputMessage;
                        }
                    }
                    catch (Exception ex)
                    {
                        outputMessage.Status = "FAILED";
                        outputMessage.ErrorMessage = ex.Message.ToString();
                        return outputMessage;
                    }
                    finally
                    {
                        con.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                outputMessage.ErrorMessage = ex.Message;
                return outputMessage;
            }
        }
        public static DataSet GetDataSet(string sql, CommandType cmdType, SqlParameter[] param)
        {
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

    }
}
