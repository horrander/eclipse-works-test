using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.DbAdapter;

public class AppDbContext : DbContext
{
    private readonly ILogger _logger;

    public AppDbContext(ILogger logger)
    {
        _logger = logger;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = Assembly.GetAssembly(typeof(AppDbContext));

        if (assembly == null)
        {
            _logger.LogError("Erro ao obter Assembly para mapeamento de entidades");

            throw new NullReferenceException(nameof(assembly));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        _logger.LogInformation("Aplicando mapeamento de entidades para o banco de dados");

        base.OnModelCreating(modelBuilder);
    }
}
