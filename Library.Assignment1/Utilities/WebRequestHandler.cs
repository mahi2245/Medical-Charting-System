using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Assignment1.Utilities
{
    public class WebRequestHandler
    {
        private string host = "localhost";
        private string port = "5180";

        private string BuildUrl(string url) => $"http://{host}:{port}{url}";

        public async Task<string?> Get(string url)
        {
            var fullUrl = BuildUrl(url);

            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                    return null;

                var body = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(body))
                    return null;

                return body;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GET ERROR → {ex.Message}");
                return null;
            }
        }

        public async Task<string?> Delete(string url)
        {
            var fullUrl = BuildUrl(url);

            try
            {
                using var client = new HttpClient();
                var response = await client.DeleteAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                    return null;

                var body = await response.Content.ReadAsStringAsync();

                return string.IsNullOrWhiteSpace(body) ? null : body;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DELETE ERROR → {ex.Message}");
                return null;
            }
        }

        public async Task<string?> Post(string url, object obj)
        {
            var fullUrl = BuildUrl(url);

            try
            {
                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(fullUrl, content);

                if (!response.IsSuccessStatusCode)
                    return null;

                var body = await response.Content.ReadAsStringAsync();

                return string.IsNullOrWhiteSpace(body) ? null : body;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST ERROR → {ex.Message}");
                return null;
            }
        }
    }
}
