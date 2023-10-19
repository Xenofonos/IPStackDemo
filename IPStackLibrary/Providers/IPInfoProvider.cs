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

        public IPInfoProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IPDetails> GetDetails(string ip)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.ipstack.com/{ip}?access_key=3108de2b12844c839c6066fad9c656da");
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
