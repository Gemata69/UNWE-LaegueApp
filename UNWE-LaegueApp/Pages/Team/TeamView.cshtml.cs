using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Team
{
    public class TeamViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TeamViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Team Team { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Players)
                .Include(t => t.Captain)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            Team = team;
            return Page();
        }
    }
}