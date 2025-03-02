using ControleDeContas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ControleDeContas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _context;

        public HomeController(ILogger<HomeController> logger, Contexto context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Calcula a soma dos valores dos produtos
            float somaValores = _context.Produto.Sum(p => p.Valor);

            string somaValoresFormatada = somaValores.ToString("C2");

            float diferenca = 1500 - somaValores;

            // Formata a diferença para exibição em reais
            string diferencaFormatada = diferenca.ToString("C2");

            // Passa a diferença formatada para a view
            ViewBag.DiferencaValores = diferencaFormatada;
            ViewBag.SomaValoresProdutos = somaValoresFormatada;


            var categoriasComQuantidade = _context.Produto
            .GroupBy(p => p.Categoria.Nome)
            .Select(g => new
            {
                Nome = g.Key,
                Quantidade = g.Count()
            })
            .ToList();

            ViewBag.CategoriasComQuantidade = categoriasComQuantidade;

            var ultimasCompras = _context.Produto.Include(c => c.Categoria)
                                        .OrderByDescending(c => c.Id)
                                        .Take(3)
                                        .ToList();
            ViewBag.UltimasCompras = ultimasCompras;

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
