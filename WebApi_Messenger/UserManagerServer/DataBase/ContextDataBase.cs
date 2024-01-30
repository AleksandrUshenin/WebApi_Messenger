using Messenger.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.DataBase
{
    public class ContextDataBase : DbContext
    {
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
