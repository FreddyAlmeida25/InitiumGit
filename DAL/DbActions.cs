using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DbActions
    {
        private static string sqlconn = "Data Source=localhost;Initial Catalog=DBInitium;Integrated Security=SSPI";
        public static DataSet GetDataSet(string spname, List<SqlParameter> paramList)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var con = new SqlConnection(sqlconn))
                {
                    using (SqlCommand cmd = new SqlCommand(spname, con))
                    {
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            foreach (var item in paramList)
                            {
                                cmd.Parameters.Add(new SqlParameter(item.ParameterName, item.Value));
                            }
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
            return ds;
        }
        public static string InsertUpdate(string spname, List<SqlParameter> paramList)
        {
            string result = "";
            try
            {
                using (var conn = new SqlConnection(sqlconn))
                using (var command = new SqlCommand(spname, conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    foreach (var item in paramList)
                    {
                        command.Parameters.Add(new SqlParameter(item.ParameterName, item.Value));
                    }
                    conn.Open();
                    int res = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
    }
}
