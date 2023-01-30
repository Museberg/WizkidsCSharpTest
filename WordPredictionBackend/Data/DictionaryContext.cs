using Microsoft.EntityFrameworkCore;
using WordPredictionBackend.Data.Models;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WordPredictionBackend.Data
{
    public class DictionaryContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public string DbPath { get; }
        private readonly IConfiguration _configuration;

        public DictionaryContext(IConfiguration configuration)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            DbPath = Path.Join(currentDirectory, "..", "Dictionary.db");
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }
}
