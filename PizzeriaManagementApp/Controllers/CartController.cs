using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.Utility;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class CartController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;

        public CartController(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) is not null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Any())
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            Pizzeria pizzeria = new Pizzeria();
            if (shoppingCartList.Any())
            {
                pizzeria = _dbContext.Pizzerias.Find(shoppingCartList[0].PizzeriaId);
            }
            
            List<Guid> pizzasFromCart = shoppingCartList.Select(x => x.PizzaId).ToList();
            List<Pizza> pizzas = new List<Pizza>();
            foreach (var item in pizzasFromCart)
            {
                pizzas.Add(_dbContext.Pizzas.Where(x => x.Id == item).FirstOrDefault());
            }
             
            var test = pizzas.GroupBy(x => x);
            List<PizzaCartItemVM> pizzasWithAmount = new List<PizzaCartItemVM>();
            foreach (var item in test)
            {
                pizzasWithAmount.Add(new PizzaCartItemVM()
                {
                    Pizzeria = pizzeria,
                    Pizza = item.Key,
                    Amount = item.Count()
                });
            }

            return View(pizzasWithAmount);
        }

        public IActionResult Delete(Guid id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) is not null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Any())
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            ShoppingCart pizzaToDelete = shoppingCartList.Where(x => x.PizzaId == id).FirstOrDefault();
            shoppingCartList.Remove(pizzaToDelete);
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction("Index");
        }
    }
}
