using Microsoft.EntityFrameworkCore;
using Mirea_Avia.Models.Users;

namespace Mirea_Avia.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; port=5432; Database=mirea_avia; Username=postgres; Password=root");
        }
    }
}
