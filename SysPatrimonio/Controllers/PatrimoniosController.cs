﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysPatrimonio.Models;

namespace SysPatrimonio.Controllers
{
    public class PatrimoniosController : Controller
    {
        private readonly Context _context;

        public PatrimoniosController(Context context)
        {
            _context = context;
        }

        // GET: Patrimonios
        public async Task<IActionResult> Index()
        {
            List<DtoPatrimonio> list = (from p in _context.patrimonios
                                          join c in _context.categorias on p.idcategoria equals c.id
                                          join l in _context.locais on p.idlocal equals l.id
                                          join d in _context.departamentos on p.iddepartamento equals d.id
                                          join f in _context.fornecedores on p.idfornecedor equals f.id
                                          select new DtoPatrimonio
                                          {
                                              id = p.id,
                                              numetiqueta = p.numetiqueta,
                                              nomepatrimonio = p.nomepatrimonio,
                                              valorpatrimonio = p.valorpatrimonio,
                                              situacao = p.situacao,
                                              nomecategoria = c.descricaocategoria,
                                              nomelocal = l.nomelocal,
                                              nomedepartamento = d.nomedepartamento,
                                              nomefornecedor = f.nomefornecedor
                                          }).ToList();

            return _context.patrimonios != null ?
                          View(list) :
                          Problem("Entity set 'Context.Departamentos'  is null.");
        }

        // GET: Patrimonios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.patrimonios == null)
            {
                return NotFound();
            }

            var dbPatrimonio = await _context.patrimonios
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbPatrimonio == null)
            {
                return NotFound();
            }

            return View(dbPatrimonio);
        }

        // GET: Patrimonios/Create
        public IActionResult Create()
        {
            ViewBag.Categoria = new SelectList(_context.categorias, "id", "descricaocategoria");
            ViewBag.Local = new SelectList(_context.locais, "id", "nomelocal");
            ViewBag.Departamento = new SelectList(_context.departamentos, "id", "nomedepartamento", "descricaodepartamento");
            ViewBag.Fornecedor = new SelectList(_context.fornecedores, "id", "nomefornecedor", "endereco", "fone");

            
            return View();
        }

        // POST: Patrimonios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id," +
                                                        "numetiqueta," +
                                                        "nomepatrimonio," +
                                                        "descricaopatrimonio," +
                                                        "valorpatrimonio," +
                                                        "marcamodelo," +
                                                        "dataaquisicao," +
                                                        "databaixa," +
                                                        "numnf," +
                                                        "numserie," +
                                                        "situacao," +
                                                        "idcategoria," +
                                                        "idlocal," +
                                                        "iddepartamento," +
                                                        "idfornecedor")] DbPatrimonio dbPatrimonio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbPatrimonio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbPatrimonio);
        }

        // GET: Patrimonios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.patrimonios == null)
            {
                return NotFound();
            }

            var dbPatrimonio = await _context.patrimonios.FindAsync(id);
            if (dbPatrimonio == null)
            {
                return NotFound();
            }
            return View(dbPatrimonio);
        }

        // POST: Patrimonios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,numetiqueta,nomepatrimonio,descricaopatrimonio,valorpatrimonio,marcamodelo,dataaquisicao,databaixa,numnf,numserie,situacao")] DbPatrimonio dbPatrimonio)
        {
            if (id != dbPatrimonio.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbPatrimonio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbPatrimonioExists(dbPatrimonio.id))
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
            return View(dbPatrimonio);
        }

        // GET: Patrimonios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.patrimonios == null)
            {
                return NotFound();
            }

            var dbPatrimonio = await _context.patrimonios
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbPatrimonio == null)
            {
                return NotFound();
            }

            return View(dbPatrimonio);
        }

        // POST: Patrimonios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.patrimonios == null)
            {
                return Problem("Entity set 'Context.Patrimonios'  is null.");
            }
            var dbPatrimonio = await _context.patrimonios.FindAsync(id);
            if (dbPatrimonio != null)
            {
                _context.patrimonios.Remove(dbPatrimonio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbPatrimonioExists(int id)
        {
          return (_context.patrimonios?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
