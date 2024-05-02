using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dal
{
    public class Context : DbContext
    {
        public Context()
            : base() { }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Cooking;Username=postgres;Password=7076;Include Error Detail=true;");

            optionsBuilder.LogTo((text) => System.Diagnostics.Debug.Write(text));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Entities.File> Files { get; set; }

        public DbSet<Ingridient> Ingridients { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ReplacementProduct> ReplacementProducts { get; set; }

        public DbSet<Publication> Publications { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<User> Users { get; set; }
    }

}
