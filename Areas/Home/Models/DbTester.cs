using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Jikandesu.Areas.Home.Models
{
    public class DbTester
    {
        public List<int> Run()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder();
                builder.DataSource = UserInfo.Server;
                builder.UserID = UserInfo.UserID;
                builder.Password = UserInfo.Password;
                builder.InitialCatalog = UserInfo.Database;

                var lst = new List<int>();
                using (var con = new SqlConnection(builder.ConnectionString))
                {
                    con.Open();
                    var query = @"SELECT * FROM testTable";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lst.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
                return lst;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
