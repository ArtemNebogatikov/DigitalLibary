using Microsoft.EntityFrameworkCore;

namespace DigitalLibary
{
    public class AppContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS01;Database=DigitalLibary;Trusted_Connection=True;Encrypt=false;");
        }
    }
}
