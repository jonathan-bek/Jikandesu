using System.Data;

namespace Jikandesu.Services
{
    public interface IJdCrud
    {
        IDbConnection GetOpenConnection();
    }
}