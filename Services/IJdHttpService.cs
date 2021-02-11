using System.Threading.Tasks;

namespace Jikandesu.Services
{
    public interface IJdHttpService
    {
        Task<string> AsyncGet(string url);
    }
}