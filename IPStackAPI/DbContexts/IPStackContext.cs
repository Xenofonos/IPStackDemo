using IPStack.API.Entities;
using IPStackLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IPStack.API.DbContexts
{
    public class IPStackContext : DbContext
    {
        public DbSet<IPDetailsEntity> IPDetailsSet { get; set; } = null!;

        public IPStackContext(DbContextOptions<IPStackContext> options) : base(options)
        {

        }
    }
}
