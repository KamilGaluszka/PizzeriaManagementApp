using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;
using PizzeriaManagementApp.Utility;
using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly PizzeriaDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(PizzeriaDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [Authorize]
        public IActionResult Index()
        {
            List<Order> orders = new List<Order>();
            if(User.IsInRole(WC.CustomerRole))
            {
                orders = _dbContext.Orders
                    .Include(x => x.Pizzeria)
                    .Include(x => x.CartOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.Address)
                    .Where(x => x.CustomerId == _userManager.GetUserId(User))
                    .OrderByDescending(x => x.CreatedOn)
                    .ThenBy(x => x.Address)
                    .ToList();
            }
            else if(User.IsInRole(WC.EmployeeRole))
            {
                List<Guid> pizzeriasIds = _dbContext.PizzeriaEmployees
                    .Where(x => x.EmployeeId == _userManager.GetUserId(User))
                    .Select(x => x.PizzeriaId)
                    .Distinct()
                    .ToList();
                orders = _dbContext.Orders
                    .Include(x => x.Pizzeria)
                    .Include(x => x.CartOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.Customer)
                    .Include(x => x.Address)
                    .Where(x => pizzeriasIds.Contains(x.PizzeriaId))
                    .Where(x => x.Status != WC.Done)
                    .OrderBy(x => x.CreatedOn)
                    .ThenBy(x => x.Address)
                    .ToList();
            }
            else if (User.IsInRole(WC.ManagerRole))
            {
                List<Guid> pizzeriasIds = _dbContext.Pizzerias
                    .Where(x => x.IdManager == _userManager.GetUserId(User))
                    .Select(x => x.Id)
                    .Distinct()
                    .ToList();
                orders = _dbContext.Orders
                    .Include(x => x.Pizzeria)
                    .Include(x => x.CartOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.Customer)
                    .Include(x => x.Address)
                    .Where(x => pizzeriasIds.Contains(x.PizzeriaId))
                    .OrderBy(x => x.CreatedOn)
                    .ThenBy(x => x.Address)
                    .ToList();
            }
            else
            {
                orders = _dbContext.Orders
                    .Include(x => x.Pizzeria)
                    .Include(x => x.CartOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.Customer)
                    .Include(x => x.Address)
                    .OrderBy(x => x.CreatedOn)
                    .ThenBy(x => x.Address)
                    .ToList();
            }

            return View(orders);
        }

        public IActionResult Create()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) is not null
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
            foreach (Guid pizzaId in pizzasFromCart)
            {
                pizzas.Add(_dbContext.Pizzas.Where(x => x.Id == pizzaId).FirstOrDefault());
            }

            float totalPrice = 0;
            foreach (Pizza pizza in pizzas)
            {
                totalPrice += pizza.Price;
            }

            OrderCreateVM orderCreateVM = new()
            {
                Payments = WC.GetPayments(),
                TotalPrice = totalPrice,
                Address = new OrderAddress()
            };

            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _dbContext.ApplicationUsers
                    .Where(x => x.Id == _userManager.GetUserId(User))
                    .Include(x => x.Address)
                    .FirstOrDefault();
                orderCreateVM.Address = new OrderAddress()
                {
                    PostalCode = user.Address.PostalCode,
                    Street = user.Address.Street,
                    HouseNumber = user.Address.HouseNumber,
                    ApartmentNumber = user.Address.ApartmentNumber,
                    Country = user.Address.Country,
                    Town = user.Address.Town
                };
            }

            return View(orderCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderCreateVM orderCreateVM)
        {
            if (ModelState.IsValid)
            {
                List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
                if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) is not null
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

                IEnumerable<IGrouping<Pizza, Pizza>> groupsByPizza = pizzas.GroupBy(x => x);
                List<PizzaCartItemVM> pizzasWithAmount = new List<PizzaCartItemVM>();
                foreach (IGrouping<Pizza, Pizza> groupByPizza in groupsByPizza)
                {
                    pizzasWithAmount.Add(new PizzaCartItemVM()
                    {
                        Pizzeria = pizzeria,
                        Pizza = groupByPizza.Key,
                        Amount = groupByPizza.Count()
                    });
                }

                float totalPrice = 0;
                foreach (Pizza pizza in pizzas)
                {
                    totalPrice += pizza.Price;
                }

                var orderAddress = _dbContext.Add(orderCreateVM.Address);

                Order order = new Order()
                {
                    CreatedOn = DateTime.Now,
                    CustomerId = _userManager.GetUserId(User),
                    Payment = orderCreateVM.Payment,
                    Status = WC.Ordered,
                    TotalPrice = totalPrice,
                    PizzeriaId = pizzeria.Id,
                    OrderAddressId = orderAddress.Entity.Id
                };
                var addedOrder = _dbContext.Add(order);
                foreach (PizzaCartItemVM pizzaWithAmount in pizzasWithAmount)
                {
                    CartOrders cartOrder = new()
                    {
                        OrderId = addedOrder.Entity.Id,
                        Amount = pizzaWithAmount.Amount,
                        PizzaId = pizzaWithAmount.Pizza.Id
                    };
                    _dbContext.Add(cartOrder);
                }
                _dbContext.SaveChanges();
                HttpContext.Session.Set(WC.SessionCart, new List<ShoppingCart>());
                return RedirectToAction("Status", new { id = order.Id });
            }
            return View(orderCreateVM);
        }

        public IActionResult Status(Guid? id)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Order order = _dbContext.Orders
                .Include(x => x.CartOrders)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.Customer)
                .Include(x => x.Address)
                .Include(x => x.Pizzeria)
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult StatusChange(Guid? id, string status)
        {
            if (id == Guid.Empty || id is null)
            {
                return NotFound();
            }

            Order order = _dbContext.Orders
                .Include(x => x.CartOrders)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.Customer)
                .Include(x => x.Address)
                .Include(x => x.Pizzeria)
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (order is null)
            {
                return NotFound();
            }

            switch (status)
            {
                case WC.Ordered:
                    order.Status = WC.Ordered;
                    break;
                case WC.InProgress:
                    order.Status = WC.InProgress;
                    break;
                case WC.Baking:
                    order.Status = WC.Baking;
                    break;
                case WC.Delivering:
                    order.Status = WC.Delivering;
                    break;
                case WC.Done:
                    order.Status = WC.Done;
                    break;
                default:
                    return NotFound();
            }

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return RedirectToAction("Status", new { id = order.Id });
        }
    }
}
