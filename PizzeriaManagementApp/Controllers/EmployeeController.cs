using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class EmployeeController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeController(PizzeriaDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> employees;
            if (User.IsInRole(WC.AdminRole))
            {
                employees = _dbContext.Employees.Where(e => e.IdManager != null);
            }
            else
            {
                employees = _dbContext.Employees.Where(e => e.IdManager == _userManager.GetUserId(User));
            }
            return View(employees);
        }

        public IActionResult ManagerIndex()
        {
            IEnumerable<Employee> employees = _dbContext.Employees.Where(e => e.IdManager == null);
            return View(employees);
        }

        public IActionResult Edit(string id)
        {
            if(string.IsNullOrEmpty(id) || id is null)
            {
                return NotFound();
            }

            Employee employee = _dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Update(employee);
                _dbContext.SaveChanges();
                if(employee.IdManager is null)
                {
                    return RedirectToAction("ManagerIndex");
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id is null)
            {
                return NotFound();
            }

            Employee employee = _dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(string id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            if(employee == null)
            {
                return NotFound();
            }
            Address address = _dbContext.Addresses.Find(employee.AddressId);
            if(address is not null)
            {
                _dbContext.Addresses.Remove(address);
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            if (employee.IdManager is null)
            {
                return RedirectToAction("ManagerIndex");
            }
            return RedirectToAction("Index");
        }
    }
}
