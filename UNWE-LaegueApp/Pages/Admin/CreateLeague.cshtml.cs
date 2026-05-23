using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CreateLeagueModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateLeagueModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] public string LeagueName { get; set; }
        [BindProperty] public int MaxTeams { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Season1Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Season1End { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Season2Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Season2End { get; set; }

        public void OnGet()
        {
            Season1Start = DateTime.Today;
            Season1End = DateTime.Today.AddMonths(3);
            Season2Start = DateTime.Today.AddMonths(4);
            Season2End = DateTime.Today.AddMonths(7);
            MaxTeams = 10;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var season1 = new Season
            {
                StartDate = Season1Start,
                EndDate = Season1End,
                ModifiedOn_22180023 = DateTime.Now
            };
            _context.Seasons.Add(season1);

            var season2 = new Season
            {
                StartDate = Season2Start,
                EndDate = Season2End,
                ModifiedOn_22180023 = DateTime.Now
            };
            _context.Seasons.Add(season2);

            await _context.SaveChangesAsync();

            var league = new Models.League
            {
                Name = LeagueName,
                MaxTeams = MaxTeams,
                SeasonOneId = season1.Id,
                SeasonTwoId = season2.Id,
                ModifiedOn_22180023 = DateTime.Now
            };

            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();

            return RedirectToPage("./AdminIndex");
        }
    }
}