using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IPStack.API.Entities
{
    public class IPDetailsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Ip { get; set; }

        public string? City { get; set; }

        [JsonPropertyName("country_name")]
        public string? Country { get; set; }

        [JsonPropertyName("continent_name")]
        public string? Continent { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IPDetailsEntity(string ip)
        {
            Ip = ip;
        }
    }
}
