using Microsoft.EntityFrameworkCore;
using PrimeraPracticaNetCore.Data;
using PrimeraPracticaNetCore.Models;

namespace PrimeraPracticaNetCore.Repositories
{
    public class ZapatillasRepository
    {
        private ZapatillasContext context;
        public ZapatillasRepository(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> FindZapaByIdAsync(int idZapa)
        {
            return await this.context.Zapatillas.
                FirstOrDefaultAsync(x=>x.IdProducto==idZapa);
        }

        public async Task<List<ImagenesZapa>> GetImagenesByZapaAsync(int idZapa)
        {
            return await this.context.Imagenes
                .Where(x=>x.IdProducto== idZapa).ToListAsync();
        }

        public async Task<ModelZapasImagen> FindZapaConIamgenesAsync(int idZapa)
        {
            Zapatilla zapa = await this.FindZapaByIdAsync(idZapa);
            List<ImagenesZapa> imagenes = await this.GetImagenesByZapaAsync(idZapa);
            return new ModelZapasImagen
            {
                ImagenesZapa = imagenes,
                Zapatilla=zapa
            };
        }
    }
}
