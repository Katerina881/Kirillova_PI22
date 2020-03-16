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

        public virtual DbSet<Component> Components { set; get; }

        public virtual DbSet<Conserv> Conservs { set; get; }

        public virtual DbSet<ConservComponent> ConservComponents { set; get; }

        public virtual DbSet<Order> Orders { set; get; }
    }
}