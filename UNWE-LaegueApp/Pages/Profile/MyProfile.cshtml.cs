using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Profile
{
    [Authorize] 
    public class MyProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MyProfileModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            UserProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);

            if (UserProfile == null)
            {
                UserProfile = new UserProfile { UserId = userId };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);

            var existingProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);

            if (existingProfile == null)
            {
                UserProfile.UserId = userId;
                _context.UserProfiles.Add(UserProfile);
            }
            else
            {
                existingProfile.FirstName = UserProfile.FirstName;
                existingProfile.MiddleName = UserProfile.MiddleName;
                existingProfile.LastName = UserProfile.LastName;
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Профилът е обновен успешно!";

            return RedirectToPage();
        }
    }
}