using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Team
{
    [Authorize]
    public class NotificationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<JoinRequest> MyNotifications { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                MyNotifications = await _context.JoinRequests
                    .Include(r => r.Team)
                    .Where(r => r.UserId == user.Id && r.Status != "Pending")
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var notifications = await _context.JoinRequests
                    .Where(r => r.UserId == user.Id && r.Status != "Pending")
                    .ToListAsync();

                _context.JoinRequests.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}