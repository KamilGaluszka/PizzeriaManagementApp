using Microsoft.AspNetCore.Mvc;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class PizzaProductsController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public PizzaProductsController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index(Guid id)
        {
            ICollection<PizzaProducts> pizzaProducts = _dbContext.PizzaProducts.Where(x => x.IdPizza == id).ToList();
            foreach (PizzaProducts pizzaProduct in pizzaProducts)
            {
                pizzaProduct.Product = _dbContext.Products.FirstOrDefault(u => u.Id == pizzaProduct.IdProduct);
            }
            if (!pizzaProducts.Any())
            {
                pizzaProducts.Add(new PizzaProducts
                {
                    IdPizza = id
                });
            }
            return View(pizzaProducts);
        }

        public IActionResult Create(Guid id)
        {
            Guid[] existingProductsIds = _dbContext.PizzaProducts.Where(x => x.IdPizza == id).Select(x => x.IdProduct).ToArray();
            PizzaProductsViewModel pizzaProductsViewModel = new()
            {
                IdPizza = id,
                ProductsCheckBoxList = _dbContext.Products.Where(x => !existingProductsIds.Contains(x.Id)).Select(x => new CheckBoxItem
                {
                    Id = x.Id,
                    Object = x,
                    IsChecked = false
                }).ToList()
            };
            return View(pizzaProductsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaProductsViewModel pizzaProductsViewModel)
        {
            foreach (CheckBoxItem pizzaProductCheckBox in pizzaProductsViewModel.ProductsCheckBoxList)
            {
                if (pizzaProductCheckBox.IsChecked)
                {
                    PizzaProducts pizzaProduct = new()
                    {
                        IdPizza = pizzaProductsViewModel.IdPizza,
                        IdProduct = pizzaProductCheckBox.Id
                    };
                    _dbContext.Add(pizzaProduct);
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { id = pizzaProductsViewModel.IdPizza });
        }

        public IActionResult Delete(Guid id)
        {
            PizzaProducts pizzaProduct = _dbContext.PizzaProducts.Find(id);
            pizzaProduct.Pizza = _dbContext.Pizzas.Find(pizzaProduct.IdPizza);
            pizzaProduct.Product = _dbContext.Products.Find(pizzaProduct.IdProduct);
            if (pizzaProduct == null)
            {
                return NotFound();
            }
            return View(pizzaProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid id)
        {
            PizzaProducts pizzaProduct = _dbContext.PizzaProducts.Find(id);
            if (pizzaProduct == null)
            {
                return NotFound();
            }
            _dbContext.Remove(pizzaProduct);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new { id = pizzaProduct.IdPizza });
        }
    }
}
