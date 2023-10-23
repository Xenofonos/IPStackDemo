using System.Net.Http.Json;
using IPStackLibrary.Models;
using System.Text.Json;

namespace IPStackLibrary.Providers
{
    public class IPInfoProvider : IIPInfoProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILibraryConfiguration configuration;

        public IPInfoProvider(HttpClient httpClient, ILibraryConfiguration configuration)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IPDetails> GetDetails(string ip)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{configuration.GetBaseUrl()}{ip}?access_key={configuration.GetApiKey()}");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(responseContent))
                    {
                        if (doc.RootElement.TryGetProperty("ip", out _))
                        {
                            // This is the format with IP information
                            // Process the data accordingly
                            return await response.Content.ReadFromJsonAsync<IPDetails>() ?? 
                                throw new Exception("IPServiceNotAvailableException");
                        }
                        else if (doc.RootElement.TryGetProperty("error", out _))
                        {
                            // This is the error format
                            // Handle the error data
                            throw new Exception("IPServiceNotAvailableException");
                        }
                        else
                        {
                            // Neither format was detected
                            // Handle unexpected response
                            throw new Exception("IPServiceNotAvailableException");
                        }
                    }
                }
                catch (JsonException)
                {
                    // JSON parsing failed, handle this error
                    throw new Exception("IPServiceNotAvailableException");
                }
            }
            else
            {
                throw new Exception("IPServiceNotAvailableException");
            }
        }
    }
}
