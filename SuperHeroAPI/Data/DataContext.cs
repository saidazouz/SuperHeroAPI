using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Modeles;

namespace SuperHeroAPI.Data
{
    public class DataContext:DbContext
    {
        public  DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeros { get; set; }
    }
}
