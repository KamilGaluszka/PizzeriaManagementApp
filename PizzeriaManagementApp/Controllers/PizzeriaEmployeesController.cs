using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    [Authorize(Roles = "Manager")]
    public class PizzeriaEmployeesController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PizzeriaEmployeesController(PizzeriaDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IActionResult Index(Guid id)
        {
            ICollection<PizzeriaEmployee> pizzeriaEmployees = _dbContext.PizzeriaEmployees
                .Where(x => x.PizzeriaId == id)
                .ToList();
            Pizzeria pizzeria = _dbContext.Pizzerias
                .Where(x => x.Id == id)
                .FirstOrDefault();
            foreach (PizzeriaEmployee pizzeriaEmployee in pizzeriaEmployees)
            {
                pizzeriaEmployee.Employee = _dbContext.Employees
                    .FirstOrDefault(u => u.Id == pizzeriaEmployee.EmployeeId);
            }
            if (!pizzeriaEmployees.Any())
            {
                pizzeriaEmployees.Add(new PizzeriaEmployee
                {
                    PizzeriaId = id
                });
            }
            PizzeriaEmployeesIndexVM pizzeriaEmployeesVM = new()
            {
                PizzeriaName = pizzeria.Name,
                PizzeriaEmployees = pizzeriaEmployees
            };
            return View(pizzeriaEmployeesVM);
        }

        public IActionResult Create(Guid id)
        {
            string[] existingEmployeesIds = _dbContext.PizzeriaEmployees
                .Where(x => x.PizzeriaId == id)
                .Select(x => x.EmployeeId)
                .ToArray();
            PizzeriaEmployeesVM pizzeriaPizzasVM = new()
            {
                IdPizzeria = id,
                EmployeesCheckBoxList = _dbContext.Employees
                    .Where(x => !existingEmployeesIds.Contains(x.Id))
                    .Where(x => x.IdManager != null)
                    .Where(x => x.IdManager == _userManager.GetUserId(User))
                    .Select(x => new CheckBoxItem<string>
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
        public IActionResult Create(PizzeriaEmployeesVM pizzeriaEmployeesVM)
        {
            foreach (CheckBoxItem<string> pizzeriaEmployeeVM in pizzeriaEmployeesVM.EmployeesCheckBoxList)
            {
                if (pizzeriaEmployeeVM.IsChecked)
                {
                    PizzeriaEmployee pizzeriaEmployee = new()
                    {
                        PizzeriaId = pizzeriaEmployeesVM.IdPizzeria,
                        EmployeeId = pizzeriaEmployeeVM.Id
                    };
                    _dbContext.Add(pizzeriaEmployee);
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { id = pizzeriaEmployeesVM.IdPizzeria });
        }

        public IActionResult Delete(Guid id)
        {
            PizzeriaEmployee pizzeriaEmployee = _dbContext.PizzeriaEmployees.Find(id);
            pizzeriaEmployee.Employee = _dbContext.Employees.Find(pizzeriaEmployee.EmployeeId);
            pizzeriaEmployee.Pizzeria = _dbContext.Pizzerias.Find(pizzeriaEmployee.PizzeriaId);
            if (pizzeriaEmployee == null)
            {
                return NotFound();
            }
            return View(pizzeriaEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid id)
        {
            PizzeriaEmployee pizzeriaEmployee = _dbContext.PizzeriaEmployees.Find(id);
            if (pizzeriaEmployee == null)
            {
                return NotFound();
            }
            _dbContext.Remove(pizzeriaEmployee);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new { id = pizzeriaEmployee.PizzeriaId });
        }
    }
}
