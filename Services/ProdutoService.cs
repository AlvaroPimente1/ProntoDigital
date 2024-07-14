﻿using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Data;
using ProdutoProntoDigital.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutoProntoDigital.Services
{
    public class ProdutoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProdutoService> _logger;

        public ProdutoService(ApplicationDbContext context, ILogger<ProdutoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Produto>> GetAllProducts()
        {
            var produtosDto = await _context.Set<ProdutoDTO>()
                .FromSqlRaw("EXEC GetAllProducts")
                .ToListAsync();

            var produtos = produtosDto.Select(dto => new Produto
            {
                PROD_ID = dto.PROD_ID,
                PROD_NOME = dto.PROD_NOME ?? string.Empty,
                PROD_PRECO = dto.PROD_PRECO,
                PROD_QTD = dto.PROD_QTD,
                CAT_ID = dto.CAT_ID,
                NomeCategoria = dto.NomeCategoria ?? string.Empty
            }).ToList();

            return produtos;
        }

        public async Task CreateProduct(Produto produto)
        {
            _logger.LogInformation("CreateProduct method called with parameters: {PROD_NOME}, {PROD_PRECO}, {PROD_QTD}, {CAT_ID}",
                produto.PROD_NOME, produto.PROD_PRECO, produto.PROD_QTD, produto.CAT_ID);

            await _context.Database.ExecuteSqlRawAsync("EXEC InsertProduto @p0, @p1, @p2, @p3",
                produto.PROD_NOME, produto.PROD_PRECO, produto.PROD_QTD, produto.CAT_ID);

            _logger.LogInformation("Product inserted into database");
        }

        public async Task<List<Categoria>> GetAllCategories()
        {
            return await _context.Categorias.ToListAsync();
        }
    }
}
