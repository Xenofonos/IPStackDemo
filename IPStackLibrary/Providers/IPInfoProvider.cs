using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using IPStackLibrary.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace IPStackLibrary.Providers
{
    public class IPInfoProvider : IIPInfoProvider
    {
        private readonly HttpClient httpClient;
        private readonly string hostName;
        private readonly string apiKey;

        //public string? hostName;
        //public string? apiKey;

        public IPInfoProvider(HttpClient httpClient, string hostName, string apiKey)
        {
            if (string.IsNullOrEmpty(hostName))
            {
                throw new ArgumentException($"'{nameof(hostName)}' cannot be null or empty.", nameof(hostName));
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException($"'{nameof(apiKey)}' cannot be null or empty.", nameof(apiKey));
            }

            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.hostName = hostName;
            this.apiKey = apiKey;
        }

        public async Task<IPDetails> GetDetails(string ip)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{hostName}{ip}?access_key={apiKey}");
            var response = await httpClient.SendAsync(request);
            var detailsResponse = await response.Content.ReadFromJsonAsync<IPDetails>();

            if (detailsResponse is not null && detailsResponse.Ip is not null)
            {
                return detailsResponse;
            }
            else
            {
                throw new Exception("IPServiceNotAvailableException");
            }
        }
    }
}
