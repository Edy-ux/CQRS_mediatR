using CQRS_mediatR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


public class GamePlayerDbContextFactory : IDesignTimeDbContextFactory<GamePlayerDbContext>
{
    public GamePlayerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // precisa ser o projeto que contém o appsettings.json
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<GamePlayerDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new GamePlayerDbContext(optionsBuilder.Options);
    }
}
