using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.League
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context) => _context = context;

        public UNWE_LaegueApp.Models.Team Team { get; set; }
        public List<UserProfile> PlayerProfiles { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Team == null) return NotFound();

            var userIds = Team.Players.Select(p => p.UserId).ToList();
            PlayerProfiles = await _context.UserProfiles
                .Where(up => userIds.Contains(up.UserId))
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostKickPlayerAsync(int playerId, int teamId)
        {
            var player = await _context.Players.FindAsync(playerId);

            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = teamId });
        }
    }
}