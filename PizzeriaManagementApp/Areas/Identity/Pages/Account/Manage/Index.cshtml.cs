using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzeriaManagementApp.Data;
using PizzeriaManagementApp.Models;

namespace PizzeriaManagementApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PizzeriaDbContext _dbContext;

        public IndexModel(UserManager<IdentityUser> userManager, PizzeriaDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser user = (ApplicationUser)await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Address address = _dbContext.Addresses.Find(user.AddressId);
            user.Address = address;
            ApplicationUser = user;

            return Page();
        }
    }
}
