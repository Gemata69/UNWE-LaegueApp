using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Team
{
    [Authorize(Roles = "User")] 
    public class CreateTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateTeamModel(ApplicationDbContext context,
                               UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public UNWE_LaegueApp.Models.Team Team { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login", new { area = "Identity" });

            var newCaptain = new Player
            {
                Name = user.Email!, 
                UserId = user.Id,  
                Captain = true
            };

            Team.Players.Add(newCaptain);

            _context.Teams.Add(Team);
            await _context.SaveChangesAsync();

            Team.CaptainId = newCaptain.Id;
            await _context.SaveChangesAsync();

            if (!await _roleManager.RoleExistsAsync("Captain"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Captain"));
            }

            await _userManager.AddToRoleAsync(user, "Captain");
            if (await _userManager.IsInRoleAsync(user, "User"))
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
            }

            return RedirectToPage("/Admin/AdminIndex");
        }
    }
}