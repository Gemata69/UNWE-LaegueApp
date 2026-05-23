using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageSeasonModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ManageSeasonModel(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> OnPostGenerateAsync()
        {
            var teamIds = await _context.Teams
                .Where(t => !string.IsNullOrEmpty(t.Name)) 
                .Select(t => t.Id)
                .ToListAsync();

            if (teamIds.Count < 2) return Page();

            if (teamIds.Count % 2 != 0) teamIds.Add(-1);

            int numTeams = teamIds.Count;
            int numRounds = numTeams - 1;
            DateTime startDate = new DateTime(2024, 9, 1);

            _context.ChangeTracker.Clear();

            for (int round = 0; round < numRounds; round++)
            {
                for (int i = 0; i < numTeams / 2; i++)
                {
                    int home = (round + i) % (numTeams - 1);
                    int away = (numTeams - 1 - i + round) % (numTeams - 1);

                    if (i == 0) away = numTeams - 1;

                    if (teamIds[home] != -1 && teamIds[away] != -1)
                    {
                        var game = new Game
                        {
                            TeamOneId = teamIds[home],
                            TeamTwoId = teamIds[away],
                            Round = round + 1,
                            StartTime = startDate.AddDays(round * 7),
                            TeamOneScore = 0,
                            TeamTwoScore = 0
                        };
                        _context.Games.Add(game);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/League/Schedule");
        }
    }
}