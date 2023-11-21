using Microsoft.EntityFrameworkCore;


namespace BackEnd_KanBan.Repository;

public class ApplicationDbContext : DbContext{
    public DbSet<Models.UserModels.User> Users { get; set; }
    public DbSet<Models.BoardModels.Board> Boards { get; set; }
    public DbSet<Models.ColumnModels.Column> Columns { get; set; }
    public DbSet<Models.CardModels.Card> Cards { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(150);
    }
}
