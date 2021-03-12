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
            GetConnection();
            _con.Open();
            return _con;
        }

        private void GetConnection()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = UserInfo.Server,
                UserID = UserInfo.UserID,
                Password = UserInfo.Password,
                InitialCatalog = UserInfo.Database
            };

            _con = new SqlConnection(builder.ConnectionString);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            return _con.QueryAsync<T>(sql);
        }

        public Task<T> GetAsync<T>(int id)
        {
            return _con.GetAsync<T>(id);
        }
    }
}
