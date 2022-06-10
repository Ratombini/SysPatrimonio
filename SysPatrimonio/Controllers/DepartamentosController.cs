using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysPatrimonio.Models;

namespace SysPatrimonio.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly Context _context;

        public DepartamentosController(Context context)
        {
            _context = context;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
            List<DtoDepartamento> list = (from d in _context.departamentos
                                         join l in _context.locais on d.idlocal equals l.id
                                         select new DtoDepartamento
                                         {
                                             id = d.id,
                                             nomedepartamento = d.nomedepartamento,
                                             descricaodepartamento = d.descricaodepartamento,
                                             nomelocal = l.nomelocal
                                         }).ToList();

              return _context.departamentos != null ? 
                          View(list) :
                          Problem("Entity set 'Context.Departamentos'  is null.");
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.departamentos == null)
            {
                return NotFound();
            }

            var dbDepartamento = await _context.departamentos
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbDepartamento == null)
            {
                return NotFound();
            }

            return View(dbDepartamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {

            ViewBag.Local = new SelectList(_context.locais, "id", "nomelocal");

            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nomedepartamento,descricaodepartamento,idlocal")] DbDepartamento dbDepartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbDepartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbDepartamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.departamentos == null)
            {
                return NotFound();
            }

            var dbDepartamento = await _context.departamentos.FindAsync(id);
            if (dbDepartamento == null)
            {
                return NotFound();
            }
            return View(dbDepartamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nomedepartamento,descricaodepartamento")] DbDepartamento dbDepartamento)
        {
            if (id != dbDepartamento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbDepartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbDepartamentoExists(dbDepartamento.id))
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
            return View(dbDepartamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.departamentos == null)
            {
                return NotFound();
            }

            var dbDepartamento = await _context.departamentos
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbDepartamento == null)
            {
                return NotFound();
            }

            return View(dbDepartamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.departamentos == null)
            {
                return Problem("Entity set 'Context.Departamentos'  is null.");
            }
            var dbDepartamento = await _context.departamentos.FindAsync(id);
            if (dbDepartamento != null)
            {
                _context.departamentos.Remove(dbDepartamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbDepartamentoExists(int id)
        {
          return (_context.departamentos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
