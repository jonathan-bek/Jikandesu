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

        public Task<IEnumerable<T>> QueryAsync<T>(
            string sql, object param = null)
        {
            return _con.QueryAsync<T>(sql, param);
        }

        public Task<T> GetAsync<T>(int id)
        {
            return _con.GetAsync<T>(id);
        }

        public Task<IEnumerable<T>> GetListAsync<T>(
            string where, object param = null,
            IDbTransaction trn = null, int? timeout = null)
        {
            return _con.GetListAsync<T>(where, param, trn, timeout);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            return _con.ExecuteScalarAsync<T>(sql, param);
        }

        public Task<int> ExecuteAsync(string sql, object param = null)
        {
            return _con.ExecuteAsync(sql, param);
        }

        public Task<int?> InsertAsync<T>(T obj,
            IDbTransaction trn = null, int? timeout = null)
        {
            return _con.InsertAsync(obj, trn, timeout);
        }
    }
}
