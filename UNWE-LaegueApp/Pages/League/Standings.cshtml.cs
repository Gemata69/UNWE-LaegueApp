using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.League
{
    public class StandingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StandingsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TeamStandingViewModel> Standings { get; set; } = new();

        public async Task OnGetAsync()
        {
            var teams = await _context.Teams.ToListAsync();

            var games = await _context.Games
                .Where(g => g.IsPlayed == true)
                .ToListAsync();

            var unsortedStandings = new List<TeamStandingViewModel>();

            foreach (var team in teams)
            {
                var teamGames = games.Where(g => g.TeamOneId == team.Id || g.TeamTwoId == team.Id).ToList();

                var stats = new TeamStandingViewModel
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    MatchesPlayed = teamGames.Count
                };

                foreach (var game in teamGames)
                {
                    bool isTeamOne = game.TeamOneId == team.Id;
                    int goalsScored = isTeamOne ? game.TeamOneScore : game.TeamTwoScore;
                    int goalsConceded = isTeamOne ? game.TeamTwoScore : game.TeamOneScore;

                    stats.GoalsFor += goalsScored;
                    stats.GoalsAgainst += goalsConceded;

                    if (goalsScored > goalsConceded)
                    {
                        stats.Wins++;
                        stats.Points += 3; 
                    }
                    else if (goalsScored == goalsConceded)
                    {
                        stats.Draws++;
                        stats.Points += 1; 
                    }
                    else
                    {
                        stats.Losses++; 
                    }
                }

                unsortedStandings.Add(stats);
            }

            Standings = unsortedStandings
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalDifference)
                .ThenByDescending(s => s.GoalsFor)
                .ToList();

            for (int i = 0; i < Standings.Count; i++)
            {
                Standings[i].Rank = i + 1;
            }
        }
    }
}