using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class PizzeriaController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PizzeriaController(PizzeriaDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IActionResult Index()
        {
            List<Pizzeria> pizzerias = new();
            if(User.IsInRole(WC.AdminRole))
            {
                pizzerias = _dbContext.Pizzerias
                    .Include(x => x.PizzeriaPizzas)
                    .Include(x => x.PizzeriaEmployees)
                    .Include(x => x.Manager).ToList();
            }
            else
                pizzerias = _dbContext.Pizzerias
                .Include(x => x.PizzeriaPizzas)
                .Include(x => x.PizzeriaEmployees)
                .ThenInclude(x => x.Employee)
                .Include(x => x.Manager)
                .Where(x => x.IdManager == _userManager.GetUserId(User))
                .ToList();
            return View(pizzerias);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> menagersDropDown = _dbContext.Employees.Where(x => x.IdManager == null).Select(x => new SelectListItem
            {
                Text = $"{ x.FirstName } { x.LastName }",
                Value = x.Id.ToString()
            });

            PizzeriaCreateVM pizzeriaCreateVM = new()
            {
                Pizzeria = new Pizzeria(),
                Managers = menagersDropDown
            };

            return View(pizzeriaCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizzeria pizzeria)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Pizzerias.Add(pizzeria);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizzeria);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Pizzeria pizzeria = _dbContext.Pizzerias.Find(id);
            if (pizzeria is null)
            {
                return NotFound();
            }
            Address address = _dbContext.Addresses.Find(pizzeria.AddressId);
            pizzeria.Address = address;

            IEnumerable<SelectListItem> menagersDropDown = _dbContext.Employees.Where(x => x.IdManager == null).Select(x => new SelectListItem
            {
                Text = $"{ x.FirstName } { x.LastName }",
                Value = x.Id.ToString()
            });

            PizzeriaCreateVM pizzeriaCreateVM = new()
            {
                Pizzeria = pizzeria,
                Managers = menagersDropDown
            };

            return View(pizzeriaCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pizzeria pizzeria)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Employee> employees = _dbContext.PizzeriaEmployees
                    .Where(x => x.PizzeriaId == pizzeria.Id)
                    .Include(x => x.Employee)
                    .Select(x => x.Employee);
                foreach (Employee employee in employees)
                {
                    employee.IdManager = pizzeria.IdManager;
                }
                _dbContext.Employees.UpdateRange(employees);
                _dbContext.Pizzerias.Update(pizzeria);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizzeria);
        }

        public IActionResult AssignManager(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Pizzeria pizzeria = _dbContext.Pizzerias.Find(id);
            if (pizzeria is null)
            {
                return NotFound();
            }
            Address address = _dbContext.Addresses.Find(pizzeria.AddressId);
            pizzeria.Address = address;

            IEnumerable<SelectListItem> menagersDropDown = _dbContext.Employees.Where(x => x.IdManager == null).Select(x => new SelectListItem
            {
                Text = $"{ x.FirstName } { x.LastName }",
                Value = x.Id.ToString()
            });

            PizzeriaCreateVM pizzeriaCreateVM = new()
            {
                Pizzeria = pizzeria,
                Managers = menagersDropDown
            };

            return View(pizzeriaCreateVM);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Pizzeria pizzeria = _dbContext.Pizzerias.Find(id);
            if (pizzeria is null)
            {
                return NotFound();
            }
            Address address = _dbContext.Addresses.Find(pizzeria.AddressId);
            pizzeria.Address = address;

            return View(pizzeria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid? id)
        {
            Pizzeria pizzeria = _dbContext.Pizzerias.Find(id);
            if (pizzeria == null)
            {
                return NotFound();
            }
            Address address = _dbContext.Addresses.Find(pizzeria.AddressId);
            _dbContext.Pizzerias.Remove(pizzeria);
            _dbContext.Addresses.Remove(address);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
