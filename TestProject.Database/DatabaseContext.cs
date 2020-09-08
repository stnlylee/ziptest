using Microsoft.EntityFrameworkCore;
using TestProject.Domain.Models;

namespace TestProject.Database
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Account> Account { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne<User>(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
