﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mediklaud.Context;
using mediklaud.Models;
using Microsoft.AspNetCore.Authorization;
using mediklaud.DTOs;
using mediklaud.Services;

namespace mediklaud.Controllers
{

    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: Doctors
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorService.GetDoctorByIdAsync(id.Value); // Use Value property for non-nullable int

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, MobileNo, Email, Specialization, ConsultationFee")] DoctorDto doctorDto)
        {
            if (ModelState.IsValid)
            {
                var createdDoctor = await _doctorService.CreateDoctorAsync(doctorDto);
                if (createdDoctor == null)
                {
                    // Handle potential creation failure (optional)
                    return View(doctorDto);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctorDto);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorService.GetDoctorByIdAsync(id.Value);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId, Name, MobileNo, Email, Specialization, ConsultationFee")] DoctorDto doctorDto)
        {
            if (id != doctorDto.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctorDto);
                if (updatedDoctor == null)
                {
                    // Handle potential update failure (optional)
                    return View(doctorDto);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctorDto);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorService.GetDoctorByIdAsync(id.Value);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }


    //public class DoctorsController : Controller
    //{
    //    private readonly ApplicationDbContext _context;

    //    public DoctorsController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: Doctors
    //    [Authorize]
    //    public async Task<IActionResult> Index()
    //    {
    //          return View(await _context.Doctors.ToListAsync());


    //    }

    //    // GET: Doctors/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null || _context.Doctors == null)
    //        {
    //            return NotFound();
    //        }

    //        var doctor = await _context.Doctors
    //            .FirstOrDefaultAsync(m => m.DoctorId == id);
    //        if (doctor == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(doctor);
    //    }

    //    // GET: Doctors/Create
    //    public IActionResult Create()
    //    {
    //        return View();


    //    }

    //    // POST: Doctors/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("DoctorId,Name,MobileNo,Email,Specialization,ConsultationFee")] Doctor doctor)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(doctor);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(doctor);
    //    }

    //    // GET: Doctors/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null || _context.Doctors == null)
    //        {
    //            return NotFound();
    //        }

    //        var doctor = await _context.Doctors.FindAsync(id);
    //        if (doctor == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(doctor);
    //    }

    //    // POST: Doctors/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("DoctorId,Name,MobileNo,Email,Specialization,ConsultationFee")] Doctor doctor)
    //    {
    //        if (id != doctor.DoctorId)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(doctor);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!DoctorExists(doctor.DoctorId))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(doctor);
    //    }

    //    // GET: Doctors/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null || _context.Doctors == null)
    //        {
    //            return NotFound();
    //        }

    //        var doctor = await _context.Doctors
    //            .FirstOrDefaultAsync(m => m.DoctorId == id);
    //        if (doctor == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(doctor);
    //    }

    //    // POST: Doctors/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        if (_context.Doctors == null)
    //        {
    //            return Problem("Entity set 'ApplicationDbContext.Doctors'  is null.");
    //        }
    //        var doctor = await _context.Doctors.FindAsync(id);
    //        if (doctor != null)
    //        {
    //            _context.Doctors.Remove(doctor);
    //        }

    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool DoctorExists(int id)
    //    {
    //      return _context.Doctors.Any(e => e.DoctorId == id);
    //    }
    //}
}
