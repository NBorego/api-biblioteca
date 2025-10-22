using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Reader> Readers { get; set; }
    }
}
