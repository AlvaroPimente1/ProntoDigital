using Microsoft.AspNetCore.Mvc;
using ProdutoProntoDigital.Services;
using System.Threading.Tasks;

namespace ProdutoProntoDigital.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.GetAllProducts();
            return View(produtos);
        }
    }
}
