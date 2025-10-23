using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIBiblioteca.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Reader> Readers { get; set; }
    }
}
