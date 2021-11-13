using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;

namespace PizzeriaManagementApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class AddressInfo : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PizzeriaDbContext _dbContext;
        private readonly ILogger<RegisterModel> _logger;

        public AddressInfo(UserManager<ApplicationUser> userManager, ILogger<RegisterModel> logger, PizzeriaDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public string Country { get; set; }
            [Required]
            [Display(Name = "Postal code")]
            public string PostalCode { get; set; }
            [Required]
            public string Town { get; set; }
            [Required]
            public string Street { get; set; }
            [Required]
            [Display(Name = "House number")]
            public string HouseNumber { get; set; }
            [Display(Name = "Apartment number")]
            public string ApartmentNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Address address = _dbContext.Addresses.Find(user.AddressId);
            Input = new InputModel()
            {
                Country = address.Country,
                PostalCode = address.PostalCode,
                Town = address.Town,
                ApartmentNumber = address.ApartmentNumber,
                HouseNumber = address.HouseNumber,
                Street = address.Street
            };
            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Identity/Account/Manage");
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                Address address = _dbContext.Addresses.Find(user.AddressId);
                address.Country = Input.Country;
                address.PostalCode = Input.PostalCode;
                address.Town = Input.Town;
                address.ApartmentNumber = Input.ApartmentNumber;
                address.HouseNumber = Input.HouseNumber;
                address.Street = Input.Street;
                _dbContext.Addresses.Update(address);
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User info updated.");
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
