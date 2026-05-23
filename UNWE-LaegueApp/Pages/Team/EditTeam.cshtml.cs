using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models; 

namespace UNWE_LaegueApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditTeamModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Team Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var teamToUpdate = await _context.Teams.FindAsync(Team.Id);

            if (teamToUpdate == null) return NotFound();

            teamToUpdate.Name = Team.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teams.Any(e => e.Id == Team.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("/League/Details", new { id = Team.Id });
        }
    }
}