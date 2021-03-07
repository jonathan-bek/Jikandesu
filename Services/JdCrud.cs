using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Jikandesu.Services
{
    public class JdCrud : IJdCrud
    {
        private IDbConnection _con;

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

            _con = new SqlConnection(builder.ConnectionString);
            _con.Open();
            return _con;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            return _con.QueryAsync<T>(sql);
        }
    }
}
