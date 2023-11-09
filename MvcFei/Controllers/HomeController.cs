using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFei.Data;
using MvcFei.Models;

namespace MvcFei.Controllers
{
    public class HomeController : Controller
    {
        private readonly MvcFeiContext _context;

        public HomeController(MvcFeiContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index(string alumnoCarrera, string searchString)
        {
            IQueryable<string> carreraQuery = from m in _context.Alumno
                                                orderby m.Carrera
                                                select m.Carrera;
            var alumnos = from m in _context.Alumno
                          select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                alumnos = alumnos.Where(s => s.Nombre!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(alumnoCarrera))
            {
                alumnos = alumnos.Where(x => x.Carrera == alumnoCarrera);
            }

            var alumnoCarreraVM = new AlumnoCarreraViewModel
            {
                Carreras = new SelectList(await carreraQuery.Distinct().ToListAsync()),
                Alumnos = await alumnos.ToListAsync()
            };

            return View(alumnoCarreraVM);
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno
                .FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlumnoId,Nombre,FechaIngreso,Carrera,Promedio")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AlumnoId,Nombre,FechaIngreso,Carrera,Promedio")] Alumno alumno)
        {
            if (id != alumno.AlumnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.AlumnoId))
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
            return View(alumno);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno
                .FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Alumno == null)
            {
                return Problem("Entity set 'MvcFeiContext.Alumno'  is null.");
            }
            var alumno = await _context.Alumno.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumno.Remove(alumno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(string id)
        {
          return (_context.Alumno?.Any(e => e.AlumnoId == id)).GetValueOrDefault();
        }

        public IActionResult Acerca()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
