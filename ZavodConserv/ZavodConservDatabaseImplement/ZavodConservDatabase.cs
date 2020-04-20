using Microsoft.EntityFrameworkCore;
using ZavodConservDatabaseImplement.Models;

namespace ZavodConservDatabaseImplement
{
    class ZavodConservDatabase : DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-S4UPN1H\SQLEXPRESS;Initial Catalog=ZavodConservDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Component> Components { set; get; }

        public DbSet<Conserv> Conservs { set; get; }

        public DbSet<ConservComponent> ConservComponents { set; get; }

        public DbSet<Order> Orders { set; get; }

        public DbSet<Client> Clients { set; get; }
    }
}