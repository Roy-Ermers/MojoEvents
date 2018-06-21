using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents
{
    public static class Sql
    {
        #region Connection
        const string _DBConnection = @"Server=185.56.145.130; Port=3306; Database=visumo1q_project; Uid=visumo1q_project; Pwd=L?um.Q=NZdmv; Encrypt=false;";
        #endregion
        public static MySqlDataReader Query(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                using (MySqlConnection con = new MySqlConnection(_DBConnection))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Prepare();
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        con.Close();
                        if (ds.Tables.Count > 0)
                            return rdr;
                        else
                            //table is empty
                            return null;
                    }
                }
            }
            catch (Exception Exception)
            {
                throw new Exception(query, Exception);
            }
        }
        public static object ScalarQuery(string query)
        {
            var SQLquery = Query(query);
            if (SQLquery != null && SQLquery.HasRows)
            {
                SQLquery.Read();
                return SQLquery.GetValue(0);
            }
            else return null;
        }
        public static DataTableReader Procedure(string name, params SqlParameter[] parameter)
        {
            try
            {
                DataSet ds = new DataSet();
                using (MySqlConnection con = new MySqlConnection(_DBConnection))
                {
                    using (MySqlCommand cmd = new MySqlCommand(name, con) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddRange(parameter);
                        cmd.Prepare();
                        con.Open();
                        MySqlDataAdapter Adapter = new MySqlDataAdapter(cmd);
                        Adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count == 0)
                            return null;
                        return ds.CreateDataReader();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(name, exception);
            }
        }
    }

}
