using APIBiblioteca.Data;
using APIBiblioteca.Services;
using APIBiblioteca.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Runtime.CompilerServices;

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

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IReaderService, ReaderService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
        }

    }
}
