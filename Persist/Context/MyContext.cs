using Dominio.models;
using Microsoft.EntityFrameworkCore;
using Persist.Configuration;

namespace Persist.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Tarefa> tarefas { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TarefaMap());
            //modelBuilder.Entity<Tarefa>().HasIndex(t => t.OrdemApresentacao).IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = DbConf.Conect();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
