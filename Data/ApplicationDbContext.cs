using Microsoft.EntityFrameworkCore;
using MiniBankApi.models;

namespace MiniBankApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions {get; set;}
    }
}