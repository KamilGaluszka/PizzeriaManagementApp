using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public PizzaController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index()
        {
            IEnumerable<Pizza> pizzas = _dbContext.Pizzas.Include(x => x.PizzaProducts).ThenInclude(x => x.Product);
            return View(pizzas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Pizzas.Add(pizza);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        public IActionResult Edit(Guid? id)
        {
            if(id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Pizza pizza = _dbContext.Pizzas.Find(id);
            if(pizza is null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Pizzas.Update(pizza);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Pizza pizza = _dbContext.Pizzas.Find(id);
            if (pizza is null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid? id)
        {
            Pizza pizza = _dbContext.Pizzas.Find(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _dbContext.Pizzas.Remove(pizza);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
