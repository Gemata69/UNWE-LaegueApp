using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.League
{
    public class ScheduleModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ScheduleModel(ApplicationDbContext context) => _context = context;

        public List<IGrouping<int, Game>> Weeks { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "asc";

        public async Task OnGetAsync()
        {
            var gamesQuery = _context.Games
                .Include(g => g.TeamOne)
                .Include(g => g.TeamTwo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                gamesQuery = gamesQuery.Where(g =>
                    g.TeamOne.Name.Contains(SearchTerm) ||
                    g.TeamTwo.Name.Contains(SearchTerm));
            }

            var games = await gamesQuery.ToListAsync();

            if (SortOrder == "desc")
            {
                Weeks = games.OrderByDescending(g => g.Round)
                             .GroupBy(g => g.Round)
                             .ToList();
            }
            else
            {
                Weeks = games.OrderBy(g => g.Round)
                             .GroupBy(g => g.Round)
                             .ToList();
            }
        }

        public async Task<IActionResult> OnPostUpdateScoreAsync(int gameId, int s1, int s2, DateTime? startTime)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game != null)
            {
                game.TeamOneScore = s1;
                game.TeamTwoScore = s2;
                game.StartTime = startTime;
                game.IsPlayed = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { searchTerm = SearchTerm, sortOrder = SortOrder });
        }
    }
}