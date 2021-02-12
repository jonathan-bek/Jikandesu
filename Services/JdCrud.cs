using System.Data;
using System.Data.SqlClient;

namespace Jikandesu.Services
{
    public class JdCrud : IJdCrud
    {
        private IDbConnection _con;

        public JdCrud() { }

        public IDbConnection GetOpenConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = UserInfo.Server;
            builder.UserID = UserInfo.UserID;
            builder.Password = UserInfo.Password;
            builder.InitialCatalog = UserInfo.Database;

            var con = new SqlConnection(builder.ConnectionString);
            con.Open();
            _con = con;
            return con;
        }
    }
}
