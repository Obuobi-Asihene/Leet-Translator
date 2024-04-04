using Leet_Translator.Models;
using Microsoft.EntityFrameworkCore;

namespace Leet_Translator.Data
{
    public class LeetTranslatorDbContext : DbContext
    {
        public LeetTranslatorDbContext(DbContextOptions<LeetTranslatorDbContext> options) : base(options)
        {
        }

        public DbSet<TranslationRecord> TranslationRecords { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
