using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.Utility;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PizzeriaDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, PizzeriaDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Pizzeria> pizzerias = _dbContext.Pizzerias
                    .Include(x => x.Address)
                    .Include(x => x.PizzeriaPizzas)
                    .ThenInclude(x => x.Pizza)
                    .ThenInclude(x => x.PizzaProducts)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category);
            IEnumerable<string> towns = _dbContext.Pizzerias
                .Include(x => x.Address)
                .OrderBy(x => x.Address.Country)
                .ThenBy(x => x.Address.Town)
                .Select(x => x.Address.Town)
                .Distinct()
                .ToList();
            HomeVM homeVM = new()
            {
                Pizzerias = pizzerias,
                Towns = towns
            };
            return View(homeVM);
        }

        public IActionResult Menu(Guid id, bool? isAdded)
        {
            Pizzeria pizzeria = _dbContext.Pizzerias.Where(x => x.Id == id).FirstOrDefault();
            IEnumerable<Pizza> pizzas = _dbContext.PizzeriaPizzas
                .Include(x => x.Pizza)
                .ThenInclude(x => x.PizzaProducts)
                .ThenInclude(x => x.Product)
                .Where(x => x.PizzeriaId == id)
                .Select(x => x.Pizza)
                .OrderBy(x => x.Price);
            PizzeriaMenuVM pizzeriaMenuVM = new()
            {
                Pizzas = pizzas,
                Pizzeria = pizzeria
            };
            if(isAdded is not null)
            {
                pizzeriaMenuVM.IsAdded = (bool)isAdded;
            };
            return View(pizzeriaMenuVM);
        }

        public IActionResult ShoppingCartAdd(Guid id, Guid pizzaId)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) is not null 
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Any())
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            foreach (ShoppingCart shoppingCart in shoppingCartList)
            {
                if (shoppingCart.PizzeriaId != id)
                {
                    shoppingCartList = new List<ShoppingCart>();
                    break;
                }
            }
            shoppingCartList.Add(new ShoppingCart
            {
                PizzaId = pizzaId,
                PizzeriaId = id
            });
            
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction("Menu", new { id, isAdded = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
