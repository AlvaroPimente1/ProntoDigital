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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Método para consultar categorias ATIVAS
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaServices.GetAllActiveCategorias();
            return View(categorias);
        }

        // Método para consultar categorias INATIVAS
        [HttpGet]
        public async Task<IActionResult> Inactives()
        {
            var categorias = await _categoriaServices.GetAllInactiveCategorias();
            return View(categorias);
        }

        // Método para CRIAR categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string catNome)
        {
            if (ModelState.IsValid)
            {
                await _categoriaServices.AddCategoria(catNome);
                return RedirectToAction(nameof(Index));
            }
            return View(catNome);
        }

        // Método para DESATIVAR categoria
        [HttpPost]
        public async Task<IActionResult> Inactivate(int id)
        {
            try
            {
                await _categoriaServices.InactivateCategoria(id);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Ocorreu um erro ao desativar a categoria." });
            }
        }

        // Método para ATIVAR categoria
        [HttpPost]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                await _categoriaServices.ActivateCategoria(id);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Ocorreu um erro ao ativar a categoria." });
            }
        }

        // Método para exibir a view de edição de categoria
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaServices.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // Método para processar a edição de categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaServices.UpdateCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoriaServices.DeleteCategoriaAndProducts(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocorreu um erro ao excluir a categoria e os produtos associados." });
            }
        }

    }
}
