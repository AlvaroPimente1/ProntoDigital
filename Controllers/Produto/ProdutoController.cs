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

        public ProdutoController(ProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Create()
        {
            var categorias = await _produtoService.GetAllActiveCategorias();
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

            if (ModelState.IsValid)
            {

                var produto = new Produto
                {
                    PROD_NOME = produtoViewModel.PROD_NOME,
                    PROD_PRECO = produtoViewModel.PROD_PRECO,
                    PROD_QTD = produtoViewModel.PROD_QTD,
                    CAT_ID = produtoViewModel.CAT_ID
                };

                await _produtoService.CreateProduct(produto);
                return RedirectToAction(nameof(Index));
            }

            var categorias = await _produtoService.GetAllActiveCategorias();
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
