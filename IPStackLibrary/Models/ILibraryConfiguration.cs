namespace IPStackLibrary.Models
{
    public interface ILibraryConfiguration
    {
        string GetApiKey();
        string GetBaseUrl();
    }
}