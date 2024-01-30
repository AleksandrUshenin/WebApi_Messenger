using MessageManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.DataBase
{
    public class ContextDataBase : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly string _sourceDB;
        public ContextDataBase(string sourceDB)
        {
            _sourceDB = sourceDB;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_sourceDB}");
        }
    }
}
