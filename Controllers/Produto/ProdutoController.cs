using Microsoft.AspNetCore.Mvc;
using ProdutoProntoDigital.Models;
using ProdutoProntoDigital.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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

        // Método para exibir a view de criação de produtos
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var categorias = await _produtoService.GetAllActiveCategorias();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.CAT_ID.ToString(),
                    Text = c.CAT_NOME
                }).ToList();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao carregar as categorias.";
                return View("Error");
            }
        }

        // Método para criar um novo produto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoCreateViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar um novo produto.");
                    ViewBag.ErrorMessage = "Ocorreu um erro ao criar o produto.";
                    return View("Error");
                }
            }

            try
            {
                var categorias = await _produtoService.GetAllActiveCategorias();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.CAT_ID.ToString(),
                    Text = c.CAT_NOME
                }).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao carregar as categorias.";
                return View("Error");
            }

            return View(produtoViewModel);
        }

        // Método para exibir a lista de produtos
        [HttpGet]
        public async Task<IActionResult> Index(string produtoFiltrado)
        {
            try
            {
                var produtos = await _produtoService.GetAllProducts();

                if (!string.IsNullOrEmpty(produtoFiltrado))
                {
                    produtos = produtos.Where(p => p.PROD_NOME.Contains(produtoFiltrado) ||
                                                   p.NomeCategoria.Contains(produtoFiltrado)).ToList();
                }

                return View(produtos);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao carregar os produtos.";
                return View("Error");
            }
        }

        // Método para DELETAR um produto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _produtoService.DeleteProduct(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar o produto.");
                return Json(new { success = false, message = "Ocorreu um erro ao deletar o produto." });
            }
        }

        // Método para exibir a view de edição de produto
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var produto = await _produtoService.GetProductById(id);
                if (produto == null)
                {
                    return NotFound();
                }

                var categorias = await _produtoService.GetAllActiveCategorias();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.CAT_ID.ToString(),
                    Text = c.CAT_NOME
                }).ToList();

                var produtoViewModel = new ProdutoCreateViewModel
                {
                    PROD_ID = produto.PROD_ID,
                    PROD_NOME = produto.PROD_NOME,
                    PROD_PRECO = produto.PROD_PRECO,
                    PROD_QTD = produto.PROD_QTD,
                    CAT_ID = produto.CAT_ID
                };

                return View(produtoViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar o produto para edição.");
                ViewBag.ErrorMessage = "Ocorreu um erro ao carregar o produto.";
                return View("Error");
            }
        }

        // Método para editar um produto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProdutoCreateViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var produto = new Produto
                    {
                        PROD_ID = produtoViewModel.PROD_ID,
                        PROD_NOME = produtoViewModel.PROD_NOME,
                        PROD_PRECO = produtoViewModel.PROD_PRECO,
                        PROD_QTD = produtoViewModel.PROD_QTD,
                        CAT_ID = produtoViewModel.CAT_ID
                    };

                    await _produtoService.UpdateProduct(produto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao editar o produto.");
                    ViewBag.ErrorMessage = "Ocorreu um erro ao editar o produto.";
                    return View("Error");
                }
            }

            try
            {
                var categorias = await _produtoService.GetAllActiveCategorias();
                ViewBag.Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.CAT_ID.ToString(),
                    Text = c.CAT_NOME
                }).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao carregar as categorias.";
                return View("Error");
            }

            return View(produtoViewModel);
        }
    }
}
