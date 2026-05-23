using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AdminIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Models.Team> Teams { get; set; } = new List<Models.Team>();

        public async Task OnGetAsync()
        {
            Teams = await _context.Teams
                .Include(t => t.Captain)
                .Include(t => t.Players)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}