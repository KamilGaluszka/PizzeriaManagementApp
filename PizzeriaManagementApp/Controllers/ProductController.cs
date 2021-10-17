using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public ProductController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products= _dbContext.Products;
            foreach (Product product in products)
            {
                product.Category = _dbContext.Categories.FirstOrDefault(x => x.Id == product.CategoryId);
            }
            return View(products);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryDropDown = _dbContext.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ProductViewModel productViewModel = new()
            {
                Product = new Product(),
                Categories = categoryDropDown
            };

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(Guid? id)
        {
            if(id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Product product = _dbContext.Products.Find(id);
            if(product is null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> categoryDropDown = _dbContext.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ProductViewModel productViewModel = new()
            {
                Product = product,
                Categories = categoryDropDown
            };
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Product product = _dbContext.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }
            product.Category = _dbContext.Categories.Find(product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Guid? id)
        {
            Product product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
