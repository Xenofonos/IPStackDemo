using IPStack.API.DbContexts;
using IPStack.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPStack.API.Services
{
    public class IPDetailsRepository : IIPDetailsRepository
    {
        private readonly IPStackContext context;

        public IPDetailsRepository(IPStackContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IPDetailsEntity?> GetIPDetailsAsync(string ip)
        {
            return await context.IPDetailsSet.Where(c => c.Ip == ip).FirstOrDefaultAsync();
        }

        public async Task<bool> AddIPDetailsAsync(IPDetailsEntity ipDetails)
        {
            context.IPDetailsSet.Add(ipDetails);
            return (await context.SaveChangesAsync() >= 0);
        }
    }
}
