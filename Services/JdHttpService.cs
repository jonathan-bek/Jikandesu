using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;

namespace Jikandesu.Services
{
    public class JdHttpService : IJdHttpService
    {
        private RestClient _client = new RestClient();

        public JdHttpService()
        {
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> AsyncGet(string url)
        {
            try
            {
                var request = new RestRequest(url, DataFormat.Json);
                var response = await _client.GetAsync<IRestResponse>(request);
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
