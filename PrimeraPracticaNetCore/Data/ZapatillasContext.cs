using Microsoft.EntityFrameworkCore;
using PrimeraPracticaNetCore.Models;

namespace PrimeraPracticaNetCore.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options)
            :base(options)
        {
            
        }

        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenesZapa> Imagenes { get; set; }
    }
}
