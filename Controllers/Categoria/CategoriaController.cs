using Microsoft.AspNetCore.Mvc;
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
            var categorias = await _categoriaServices.GetAllCategorias();
            return View(categorias);
        }

        // Ação GET para exibir o formulário de criação
        public IActionResult Create()
        {
            return View();
        }

        // Ação POST para criar uma nova categoria
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
    }
}
