using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IPStackLibrary.Models
{
    public class IPDetails : IIPDetails
    {
        [Required]
        public string Ip { get; set; }

        public string? City { get; set; }

        [JsonPropertyName("country_name")]
        public string? Country { get; set; }

        [JsonPropertyName("continent_name")]
        public string? Continent { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IPDetails(string ip)
        {
            Ip = ip;
        }
    }
}