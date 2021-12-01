using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ThicknessController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public ThicknessController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index()
        {
            IEnumerable<Thickness> thicknesses = _dbContext.Thicknesses;
            return View(thicknesses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Thickness thickness)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Thicknesses.Add(thickness);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thickness);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Thickness thickness = _dbContext.Thicknesses.Find(id);
            if (thickness is null)
            {
                return NotFound();
            }

            return View(thickness);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Thickness thickness)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Thicknesses.Update(thickness);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thickness);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Thickness thickness = _dbContext.Thicknesses.Find(id);
            if (thickness is null)
            {
                return NotFound();
            }

            return View(thickness);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid? id)
        {
            Thickness thickness = _dbContext.Thicknesses.Find(id);
            if (thickness == null)
            {
                return NotFound();
            }

            _dbContext.Thicknesses.Remove(thickness);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
