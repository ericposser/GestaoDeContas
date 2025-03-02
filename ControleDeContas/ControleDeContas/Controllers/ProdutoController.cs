using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleDeContas.Models;

namespace ControleDeContas.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Contexto _context;

        public ProdutoController(Contexto context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            var produtosOrdenados = await _context.Produto
                .OrderByDescending(p => p.Valor)
                .Include(p => p.Categoria)
                .ToListAsync();

            return View(produtosOrdenados);
        }




        // GET: Produto/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = new SelectList(await _context.Categoria.ToListAsync(), "Id", "Nome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(int Categoria, [Bind("Id,Nome,Valor,Data")] Produto produto)
        {
            if (Categoria == 0)
            {
                TempData["Alerta"] = "Selecione uma categoria!";
                return RedirectToAction(nameof(Index));
            }

            //// Converte a string de data em um objeto DateTime
            //if (!DateTime.TryParse(produto.Data, out DateTime dataProduto))
            //{
            //    TempData["Alerta"] = "Formato de data inválido!";
            //    return RedirectToAction(nameof(Index));
            //}

            if (produto.Valor < 0)
            {
                TempData["Alerta"] = "Não é possível cadastrar um produto com valor menor que zero!";
                return RedirectToAction(nameof(Index));
            }

            //// Verifica se a data do produto é uma data futura
            //if (dataProduto > DateTime.Today)
            //{
            //    TempData["Alerta"] = "Não é possível cadastrar um produto com uma data futura!";
            //    return RedirectToAction(nameof(Index));
            //}

            // Buscar a mercadoria e o cliente selecionados no banco de dados
            var categoria = await _context.Categoria.FindAsync(Categoria);
            // Associar a mercadoria e o cliente à venda
            produto.Categoria = categoria;

            // Adicionar a venda ao contexto e salvar as alterações
            _context.Add(produto);
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Produto cadastrado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categorias = new SelectList(await _context.Categoria.ToListAsync(), "Id", "Nome");

            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool Pago)
        {
            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            // Verificar se o produto já foi pago
            if (produto.Pago)
            {
                TempData["MensagemW"] = "Produto ja foi pago!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Atualizar apenas o valor de "Pago"
                produto.Pago = Pago;

                _context.Update(produto);
                await _context.SaveChangesAsync();

                TempData["Mensagem"] = "Produto pago com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }



        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produto == null)
            {
                return Problem("Entity set 'Contexto.Produto'  is null.");
            }
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Produto excluido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return (_context.Produto?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #region MEUS METODOS
        public ActionResult ExcluirTodosProdutos()
        {
            try
            {
                // Carregar todos os produtos do banco de dados
                var produtos = _context.Produto.ToList();

                // Remover todos os produtos da tabela
                _context.Produto.RemoveRange(produtos);

                // Salvar as alterações no banco de dados
                _context.SaveChanges();

                TempData["Mensagem"] = "TODOS Produtos excluidos com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu um erro ao excluir os produtos: {ex.Message}";
            }

            // Redirecionar para a página inicial ou para onde desejar após a exclusão
            return RedirectToAction("Index", "Produto"); // Substitua "Index" e "Home" pelas suas respectivas ações e controladores.
        }

        public IActionResult ContarProdutos()
        {
            var totalProdutos = _context.Produto.Count();
            return Json(totalProdutos);
        }

        #endregion


    }
}
