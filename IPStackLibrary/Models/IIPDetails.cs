namespace IPStackLibrary.Models
{
    public interface IIPDetails
    {
        string Ip { get; set; }
        string? City { get; set; }
        string? Continent { get; set; }
        string? Country { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}