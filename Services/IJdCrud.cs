using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jikandesu.Services
{
    public interface IJdCrud
    {
        IDbConnection GetOpenConnection();
        Task<IEnumerable<T>> QueryAsync<T>(string sql);
    }
}
