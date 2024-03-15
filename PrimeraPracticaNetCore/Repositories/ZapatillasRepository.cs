using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrimeraPracticaNetCore.Data;
using PrimeraPracticaNetCore.Models;
using System.Data;

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

        public async Task<ModelZapasImagen> FindZapaConIamgenesAsync
            (int idZapa,int posicion)
        {
            string sql = "SP_GRUPO_EMPLEADOS_DEPT @posicion, @idzapa, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamId = new SqlParameter("@idzapa", idZapa);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = ParameterDirection.Output;
            var consulta = this.context.Imagenes.FromSqlRaw(sql, pamPosicion, pamId, pamRegistros);
            ModelZapasImagen model = new ModelZapasImagen
            {
                Zapatilla= await this.FindZapaByIdAsync(idZapa),
                ImagenZapa= consulta.AsEnumerable().FirstOrDefault(),
                NumeroRegistros = (int)pamRegistros.Value
            };
            return model;



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
