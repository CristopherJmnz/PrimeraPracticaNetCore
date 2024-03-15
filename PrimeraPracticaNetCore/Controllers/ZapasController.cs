using Microsoft.AspNetCore.Mvc;
using PrimeraPracticaNetCore.Models;
using PrimeraPracticaNetCore.Repositories;

namespace PrimeraPracticaNetCore.Controllers
{
    public class ZapasController : Controller
    {
        private ZapatillasRepository repo;
        public ZapasController(ZapatillasRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapas = await this.repo.GetZapasAsync();
            return View(zapas);
        }

        public async Task<IActionResult> Details(int idZapa,int? posicion)
        {
            if (posicion==null)
            {
                posicion = 1;
            }
            ModelZapasImagen model= await this.repo
                .FindZapaConIamgenesAsync(idZapa,posicion.Value);
            return View(model);
        }
    }
}
