using Microsoft.EntityFrameworkCore;
using Mirea_Avia.Models.Users;


namespace Mirea_Avia.Database
{
    /**
    * <summary>Предоставление интерфейса по работе с БД</summary>
    */
    public class ApplicationContext : DbContext
    {
        /**
        * <summary>Таблица БД с пользователями</summary>
        */
        public DbSet<User> Users { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        /**
        * <summary>Конфигурация подключения с БД</summary>
        */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; port=5432; Database=mirea_avia; Username=postgres; Password=root");
        }
    }
}
