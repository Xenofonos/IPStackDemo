using IPStack.API.Entities;

namespace IPStack.API.Services
{
    public interface IIPDetailsRepository
    {
        Task<IPDetailsEntity?> GetIPDetailsAsync(string ip);
        Task<bool> AddIPDetailsAsync(IPDetailsEntity ipDetails);
    }
}