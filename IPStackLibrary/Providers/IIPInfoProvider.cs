using IPStackLibrary.Models;

namespace IPStackLibrary.Providers
{
    public interface IIPInfoProvider
    {
        Task<IPDetails> GetDetails(string ip);
    }
}