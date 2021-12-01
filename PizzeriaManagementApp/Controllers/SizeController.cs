using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class SizeController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public SizeController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index()
        {
            IEnumerable<Size> sizes = _dbContext.Sizes;
            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Size size)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Sizes.Add(size);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(size);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Size size = _dbContext.Sizes.Find(id);
            if (size is null)
            {
                return NotFound();
            }

            return View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Size size)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Sizes.Update(size);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(size);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Size size = _dbContext.Sizes.Find(id);
            if (size is null)
            {
                return NotFound();
            }

            return View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid? id)
        {
            Size size = _dbContext.Sizes.Find(id);
            if (size == null)
            {
                return NotFound();
            }

            _dbContext.Sizes.Remove(size);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
