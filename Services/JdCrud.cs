using System.Data;
using System.Data.SqlClient;

namespace Jikandesu.Services
{
    public class JdCrud : IJdCrud
    {
        public JdCrud() { }

        public IDbConnection GetOpenConnection()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = UserInfo.Server,
                UserID = UserInfo.UserID,
                Password = UserInfo.Password,
                InitialCatalog = UserInfo.Database
            };

            var con = new SqlConnection(builder.ConnectionString);
            con.Open();
            return con;
        }
    }
}
