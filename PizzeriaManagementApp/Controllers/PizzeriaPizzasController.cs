using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class PizzeriaPizzasController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public PizzeriaPizzasController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index(Guid id)
        {
            ICollection<PizzeriaPizza> pizzeriaPizzas = _dbContext.PizzeriaPizzas.Where(x => x.PizzeriaId == id).ToList();
            Pizzeria pizzeria = _dbContext.Pizzerias.Where(x => x.Id == id).FirstOrDefault();
            foreach (PizzeriaPizza pizzeriaPizza in pizzeriaPizzas)
            {
                pizzeriaPizza.Pizza = _dbContext.Pizzas
                    .Include(x => x.Size)
                    .Include(x => x.Thickness)
                    .FirstOrDefault(u => u.Id == pizzeriaPizza.PizzaId);
            }
            if (!pizzeriaPizzas.Any())
            {
                pizzeriaPizzas.Add(new PizzeriaPizza
                {
                    PizzeriaId = id
                });
            }
            PizzeriaPizzaIndexVM pizzeriaPizzasVM = new()
            {
                PizzeriaName = pizzeria.Name,
                PizzeriaPizzas = pizzeriaPizzas
            };
            return View(pizzeriaPizzasVM);
        }

        public IActionResult Create(Guid id)
        {
            Guid[] existingPizzasIds = _dbContext.PizzeriaPizzas.Where(x => x.PizzeriaId == id).Select(x => x.PizzaId).ToArray();
            PizzeriaPizzaVM pizzeriaPizzasVM = new()
            {
                IdPizzeria = id,
                PizzasCheckBoxList = _dbContext.Pizzas
                    .Where(x => !existingPizzasIds.Contains(x.Id))
                    .Include(x => x.Size)
                    .Include(x => x.Thickness)
                    .Select(x => new CheckBoxItem<Guid>
                {
                    Id = x.Id,
                    Object = x,
                    IsChecked = false
                }).ToList()
            };
            return View(pizzeriaPizzasVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzeriaPizzaVM pizzeriaPizzasVM)
        {
            foreach (CheckBoxItem<Guid> pizzeriaPizzaVM in pizzeriaPizzasVM.PizzasCheckBoxList)
            {
                if (pizzeriaPizzaVM.IsChecked)
                {
                    PizzeriaPizza pizzeriaPizza = new()
                    {
                        PizzeriaId = pizzeriaPizzasVM.IdPizzeria,
                        PizzaId = pizzeriaPizzaVM.Id
                    };
                    _dbContext.Add(pizzeriaPizza);
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { id = pizzeriaPizzasVM.IdPizzeria });
        }

        public IActionResult Delete(Guid id)
        {
            PizzeriaPizza pizzeriaPizza = _dbContext.PizzeriaPizzas.Find(id);
            pizzeriaPizza.Pizza = _dbContext.Pizzas.Find(pizzeriaPizza.PizzaId);
            pizzeriaPizza.Pizzeria = _dbContext.Pizzerias.Find(pizzeriaPizza.PizzeriaId);
            if (pizzeriaPizza == null)
            {
                return NotFound();
            }
            return View(pizzeriaPizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid id)
        {
            PizzeriaPizza pizzeriaPizza = _dbContext.PizzeriaPizzas.Find(id);
            if (pizzeriaPizza == null)
            {
                return NotFound();
            }
            _dbContext.Remove(pizzeriaPizza);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new { id = pizzeriaPizza.PizzeriaId });
        }
    }
}
