using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Team
{
    [Authorize]
    public class JoinTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public JoinTeamModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public int SelectedTeamId { get; set; }

        public List<SelectListItem> TeamOptions { get; set; } = new List<SelectListItem>();
        public bool IsSuccess { get; set; } = false;

        public bool IsAlreadyInTeam { get; set; } = false;
        public string CurrentTeamName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Index");

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (profile == null || string.IsNullOrEmpty(profile.FirstName) || string.IsNullOrEmpty(profile.LastName))
            {
                TempData["ErrorMessage"] = "⚠️ Моля, попълнете своя профил (Име и Фамилия), преди да кандидатствате за отбор!";
                return RedirectToPage("/Profile/MyProfile");
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (player != null)
            {
                IsAlreadyInTeam = true;
                CurrentTeamName = player.Team?.Name ?? "Неизвестен";
            }

            var teams = await _context.Teams.ToListAsync();
            TeamOptions = teams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Index");

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (profile == null || string.IsNullOrEmpty(profile.FirstName) || string.IsNullOrEmpty(profile.LastName))
            {
                TempData["ErrorMessage"] = "⚠️ Моля, попълнете своя профил (Име и Фамилия), преди да кандидатствате за отбор!";
                return RedirectToPage("/Profile/MyProfile");
            }

            bool isPlayer = await _context.Players.AnyAsync(p => p.UserId == user.Id);
            if (isPlayer)
            {
                ModelState.AddModelError(string.Empty, "Вече сте част от отбор и не можете да кандидатствате за други!");
                var teams = await _context.Teams.ToListAsync();
                TeamOptions = teams.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name }).ToList();
                return Page();
            }

            bool alreadyRequested = await _context.JoinRequests
                .AnyAsync(r => (r.UserEmail == user.Email || r.UserId == user.Id) && r.TeamId == SelectedTeamId);

            if (alreadyRequested)
            {
                ModelState.AddModelError(string.Empty, "Вече сте изпратили заявка за този отбор!");
                var teams = await _context.Teams.ToListAsync();
                TeamOptions = teams.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name }).ToList();
                return Page();
            }

            var request = new JoinRequest
            {
                UserEmail = user.Email!,
                UserId = user.Id,
                UserFullName = $"{profile.FirstName} {profile.LastName}",
                TeamId = SelectedTeamId,
                Status = "Pending"
            };

            _context.JoinRequests.Add(request);
            await _context.SaveChangesAsync();

            IsSuccess = true;
            return Page();
        }
    }
}