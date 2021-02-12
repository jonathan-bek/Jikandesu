using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

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

                using (IDbConnection con = new SqlConnection(builder.ConnectionString))
                {
                    con.Open();
                    var query = @"SELECT * FROM testTable";
                    var result = con.Query<int>(query);
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
