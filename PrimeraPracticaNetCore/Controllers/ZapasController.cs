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

        public async Task<IActionResult> _ImagenesPartial(int idZapa, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            ModelZapasImagen model = await this.repo
                .FindZapaConIamgenesAsync(idZapa, posicion.Value);
            int siguiente = posicion.Value + 1;
            if (siguiente > model.NumeroRegistros)
            {
                siguiente = 1;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = model.NumeroRegistros;
            }
            ViewData["POSICION"] = posicion;
            ViewData["ÚLTIMO"] = model.NumeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            return View(model);
        }

        public async Task<IActionResult> SubirImagenes()
        {
            List<Zapatilla> zapas=await this.repo.GetZapasAsync();
            return View(zapas);
        }
        [HttpPost]
        public async Task<IActionResult> SubirImagenes(int idZapa,List<string>imagen)
        {
            foreach (string img in imagen)
            {
                await this.repo.SubirImagen(img, idZapa);
            }
            return RedirectToAction("Index");
        }
    }
}
