using APIBiblioteca.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace APIBiblioteca.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Database");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(
                    connectionString ?? throw new InvalidOperationException("ConnectionString não encontrada."))));
        }
    }
}
