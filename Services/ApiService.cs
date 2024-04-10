using Ategest.Library.Util;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorApp01.Services
{
    public static class ApiService
    {
        private static readonly string apiUrl = "https://ategestapi.azurewebsites.net/api/";
        private static readonly string apiAppsUrl = "https://ategestapps.azurewebsites.net/api/";

        public static async Task<HttpResponseMessage?> MakeRequestAsync(string? url, HttpMethod? method = null, object? data = null, string? token = null)
        {
            try
            {
                if (method == null)
                    method = HttpMethod.Get;
                HttpRequestMessage request = new() { RequestUri = new Uri(apiUrl + url), Method = method };

                if (data != null)
                {
                    StringContent content = new(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                    request.Content = content;
                }
                request.Headers.Add("Accept", "application/json");
                token ??= UtilsMethods.MySK();
                AuthenticationHeaderValue authHeader = new("bearer", token);
                HttpClient client = new();
                client.DefaultRequestHeaders.Authorization = authHeader;
                client.BaseAddress = request.RequestUri;

                return await client.SendAsync(request);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
