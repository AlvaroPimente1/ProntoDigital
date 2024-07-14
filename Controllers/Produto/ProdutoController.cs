using Microsoft.AspNetCore.Mvc;
using ProdutoProntoDigital.Models;
using ProdutoProntoDigital.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutoProntoDigital.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        public async Task<IActionResult> Create()
        {
            var categorias = await _produtoService.GetAllCategories();
            ViewBag.Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.CAT_ID.ToString(),
                Text = c.CAT_NOME
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoCreateViewModel produtoViewModel)
        {
            _logger.LogInformation("Create method called");

            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is valid");

                var produto = new Produto
                {
                    PROD_NOME = produtoViewModel.PROD_NOME,
                    PROD_PRECO = produtoViewModel.PROD_PRECO,
                    PROD_QTD = produtoViewModel.PROD_QTD,
                    CAT_ID = produtoViewModel.CAT_ID
                };

                await _produtoService.CreateProduct(produto);
                _logger.LogInformation("Product created successfully");
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("ModelState is invalid");
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogWarning("Validation error on {Field}: {Error}", state.Key, error.ErrorMessage);
                }
            }

            var categorias = await _produtoService.GetAllCategories();
            ViewBag.Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.CAT_ID.ToString(),
                Text = c.CAT_NOME
            }).ToList();

            return View(produtoViewModel);
        }

        public async Task<IActionResult> Index(string produtoFiltrado)
        {
            var produtos = await _produtoService.GetAllProducts();

            if (!string.IsNullOrEmpty(produtoFiltrado))
            {
                produtos = produtos.Where(p => p.PROD_NOME.Contains(produtoFiltrado) ||
                                               p.NomeCategoria.Contains(produtoFiltrado)).ToList();
            }

            return View(produtos);
        }
    }
}
