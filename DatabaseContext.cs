namespace Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DbSet<FoodItem> FoodItems { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data source=fooditem.db");
        }
    }
}
