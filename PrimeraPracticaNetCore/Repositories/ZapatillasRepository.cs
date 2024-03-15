using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PrimeraPracticaNetCore.Data;
using PrimeraPracticaNetCore.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PrimeraPracticaNetCore.Repositories
{
    public class ZapatillasRepository
    {
        #region PROCEDURES

//        create procedure SP_IMAGENES_ZAPA
//(@posicion int, @idzapa int, @registros int out)
//AS
//    select @registros=count(IDIMAGEN)

//    from IMAGENESZAPASPRACTICA
//    where IDPRODUCTO = @idzapa;

//        select IDIMAGEN, IDPRODUCTO, IMAGEN from(
//        select cast(ROW_NUMBER() over (order by IMAGEN) as int) as posicion, 
//	IDIMAGEN,IDPRODUCTO,IMAGEN
//    from IMAGENESZAPASPRACTICA
//    where IDPRODUCTO = @idzapa
//	) as query where posicion >= @posicion and posicion<(@posicion+1)
//GO

        #endregion
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
            string sql = "SP_IMAGENES_ZAPA @posicion, @idzapa, @registros out";
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
        }

        
    }
}
