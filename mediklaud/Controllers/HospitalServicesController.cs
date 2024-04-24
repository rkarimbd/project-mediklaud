using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mediklaud.Context;
using mediklaud.Models;

namespace mediklaud.Controllers
{
    public class HospitalServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HospitalServices
        public async Task<IActionResult> Index()
        {
              return View(await _context.HospitalServices.ToListAsync());
        }

        // GET: HospitalServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HospitalServices == null)
            {
                return NotFound();
            }

            var hospitalService = await _context.HospitalServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (hospitalService == null)
            {
                return NotFound();
            }

            return View(hospitalService);
        }

        // GET: HospitalServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HospitalServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,HospitalId,Status")] HospitalService hospitalService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospitalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalService);
        }

        // GET: HospitalServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HospitalServices == null)
            {
                return NotFound();
            }

            var hospitalService = await _context.HospitalServices.FindAsync(id);
            if (hospitalService == null)
            {
                return NotFound();
            }
            return View(hospitalService);
        }

        // POST: HospitalServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,HospitalId,Status")] HospitalService hospitalService)
        {
            if (id != hospitalService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitalService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalServiceExists(hospitalService.ServiceId))
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
            return View(hospitalService);
        }

        // GET: HospitalServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HospitalServices == null)
            {
                return NotFound();
            }

            var hospitalService = await _context.HospitalServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (hospitalService == null)
            {
                return NotFound();
            }

            return View(hospitalService);
        }

        // POST: HospitalServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HospitalServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HospitalServices'  is null.");
            }
            var hospitalService = await _context.HospitalServices.FindAsync(id);
            if (hospitalService != null)
            {
                _context.HospitalServices.Remove(hospitalService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalServiceExists(int id)
        {
          return _context.HospitalServices.Any(e => e.ServiceId == id);
        }
    }
}
