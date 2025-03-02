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
    public class TarefaController : Controller
    {
        private readonly Contexto _context;

        public TarefaController(Contexto context)
        {
            _context = context;
        }

        // GET: Tarefa
        public async Task<IActionResult> Index()
        {
            var tarefas = _context.Tarefa;

            if (tarefas != null)
            {
                // Ordenar as tarefas por Data
                var tarefasOrdenadas = await tarefas.OrderBy(t => t.Data).ToListAsync();

                return View(tarefasOrdenadas);
            }
            else
            {
                return Problem("Entity set 'Contexto.Tarefa' is null.");
            }
        }


        // GET: Tarefa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Data")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Data")] Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarefa == null)
            {
                return Problem("Entity set 'Contexto.Tarefa'  is null.");
            }
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefa.Remove(tarefa);
            }
            
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Tarefa excluída com sucesso!";
            TempData["MessageType"] = "alert-danger";
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
          return (_context.Tarefa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
