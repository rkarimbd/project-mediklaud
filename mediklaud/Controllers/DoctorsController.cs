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


    
}
