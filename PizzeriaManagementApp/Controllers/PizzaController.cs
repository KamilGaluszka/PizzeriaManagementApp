using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;

        public PizzaController(PizzeriaDbContext dbContext, IWebHostEnvironment webHost)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
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
            if (ModelState.IsValid)
            {
                IFormFileCollection files = HttpContext.Request.Form.Files;
                string webRootPath = _webHost.WebRootPath;
                string uploadPath = $"{ webRootPath }{ WC.PizzaImagePath }";
                string extension = Path.GetExtension(files[0].FileName);

                if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".gif")
                {
                    string imageName = $"{ Guid.NewGuid() }{ extension }";
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, imageName), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    pizza.Image = imageName;

                    _dbContext.Pizzas.Add(pizza);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Image", "File has to be an image");
                return View(pizza);
            }
            return View(pizza);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id is null)
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
            ModelState["Image"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                Pizza oldPizza = _dbContext.Pizzas.AsNoTracking().FirstOrDefault(x => x.Id == pizza.Id);
                pizza.Image = oldPizza.Image;
                IFormFileCollection files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webRootPath = _webHost.WebRootPath;
                    string uploadPath = $"{ webRootPath }{ WC.PizzaImagePath }";
                    string extension = Path.GetExtension(files[0].FileName);

                    if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".gif")
                    {
                        string oldFilePath = Path.Combine(uploadPath, pizza.Image);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }

                        string imageName = $"{ Guid.NewGuid() }{ extension }";
                        using (var fileStream = new FileStream(Path.Combine(uploadPath, imageName), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        pizza.Image = imageName;

                        _dbContext.Pizzas.Update(pizza);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("Image", "File has to be an image");
                    return View(pizza);
                }
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

            string webRootPath = _webHost.WebRootPath;
            string uploadPath = $"{ webRootPath }{ WC.PizzaImagePath }";
            string oldFilePath = Path.Combine(uploadPath, pizza.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            _dbContext.Pizzas.Remove(pizza);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
