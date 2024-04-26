using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mediklaud.Context;
using mediklaud.Models;
using Microsoft.AspNetCore.Authorization;

namespace mediklaud.Controllers
{
    public class BillingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Billings

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Billings.Include(b => b.Appointment).Include(b => b.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Billings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Billings == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                .Include(b => b.Appointment)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BillNo == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // GET: Billings/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId");
            ViewData["ServiceId"] = new SelectList(_context.HospitalServices, "ServiceId", "ServiceName");
            return View();
        }

        // POST: Billings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillNo,BillDate,AppointmentId,ServiceId,BillAmount,IsPaid,Comments")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", billing.AppointmentId);
            ViewData["ServiceId"] = new SelectList(_context.HospitalServices, "ServiceId", "ServiceName", billing.ServiceId);
            return View(billing);
        }

        // GET: Billings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Billings == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", billing.AppointmentId);
            ViewData["ServiceId"] = new SelectList(_context.HospitalServices, "ServiceId", "ServiceName", billing.ServiceId);
            return View(billing);
        }

        // POST: Billings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillNo,BillDate,AppointmentId,ServiceId,BillAmount,IsPaid,Comments")] Billing billing)
        {
            if (id != billing.BillNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingExists(billing.BillNo))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", billing.AppointmentId);
            ViewData["ServiceId"] = new SelectList(_context.HospitalServices, "ServiceId", "ServiceName", billing.ServiceId);
            return View(billing);
        }

        // GET: Billings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Billings == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                .Include(b => b.Appointment)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BillNo == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // POST: Billings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Billings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Billings'  is null.");
            }
            var billing = await _context.Billings.FindAsync(id);
            if (billing != null)
            {
                _context.Billings.Remove(billing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingExists(int id)
        {
          return _context.Billings.Any(e => e.BillNo == id);
        }
    }
}
