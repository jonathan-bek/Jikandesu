using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;

namespace Jikandesu.Services
{
    public class JdHttpService : IJdHttpService
    {
        private RestClient _client = new RestClient();

        public JdHttpService() { }

        public async Task<string> AsyncGet(string url)
        {
            try
            {
                var request = new RestRequest(url, DataFormat.Json);
                var response = await _client.ExecuteAsync(request);
                return response.Content;
            }
            catch (HttpRequestException e)
            {
                Trace.TraceWarning(e.Message);
                throw;
            }
        }
    }
}
