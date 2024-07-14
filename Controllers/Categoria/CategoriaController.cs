using Microsoft.AspNetCore.Mvc;
using ProdutoProntoDigital.Models;
using ProdutoProntoDigital.Services;
using System.Threading.Tasks;

namespace ProdutoProntoDigital.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaServices _categoriaServices;

        public CategoriaController(CategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaServices.GetAllActiveCategorias();
            return View(categorias);
        }

        public async Task<IActionResult> Inactives()
        {
            var categorias = await _categoriaServices.GetAllInactiveCategorias();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string catNome)
        {
            if (ModelState.IsValid)
            {
                await _categoriaServices.AddCategoria(catNome);
                return RedirectToAction(nameof(Index));
            }
            return View(catNome);
        }

        public async Task<IActionResult> Inactivate(int id)
        {
            await _categoriaServices.InactivateCategoria(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activate(int id)
        {
            await _categoriaServices.ActivateCategoria(id);
            return RedirectToAction(nameof(Inactives));
        }

    }
}
