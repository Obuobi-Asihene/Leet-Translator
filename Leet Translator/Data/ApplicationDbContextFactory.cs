using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Leet_Translator.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<LeetTranslatorDbContext>
    {
        public LeetTranslatorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeetTranslatorDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EcommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new LeetTranslatorDbContext(optionsBuilder.Options);
        }
    }
}
