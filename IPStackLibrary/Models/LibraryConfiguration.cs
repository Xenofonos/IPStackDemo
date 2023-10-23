using IPStackLibrary.Models;

namespace IPStack.API.Models
{
    public class LibraryConfiguration : ILibraryConfiguration
    {
        private string apiKey;
        private string baseUrl;

        public LibraryConfiguration(string apiKey, string baseUrl)
        {
            this.apiKey = apiKey;
            this.baseUrl = baseUrl;
        }

        public string GetApiKey()
        {
            return apiKey;
        }

        public string GetBaseUrl()
        {
            return baseUrl;
        }
    }
}
