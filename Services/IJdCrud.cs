using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jikandesu.Services
{
    public interface IJdCrud
    {
        IDbConnection GetOpenConnection();
        Task<IEnumerable<T>> QueryAsync<T>(
            string sql, object parameters = null);
        Task<T> GetAsync<T>(int id);
        Task<IEnumerable<T>> GetListAsync<T>(
            string where, object parameters = null,
            IDbTransaction trn = null, int? timeout = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null);
        Task<int> ExecuteAsync(string sql, object param);
        Task<int?> InsertAsync<T>(object obj,
            IDbTransaction trn = null, int? timeout = null);
    }
}
