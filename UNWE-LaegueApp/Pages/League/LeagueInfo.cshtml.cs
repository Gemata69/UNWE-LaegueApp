using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.League
{
    [Authorize] 
    public class LeagueInfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public LeagueInfoModel(ApplicationDbContext context) => _context = context;

        public List<Models.League> Leagues { get; set; } = new();

        public async Task OnGetAsync()
        {
            Leagues = await _context.Leagues
                .Include(l => l.SeasonOne)
                .Include(l => l.SeasonTwo)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteLeagueAsync(int leagueId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var league = await _context.Leagues
                .Include(l => l.SeasonOne)
                .Include(l => l.SeasonTwo)
                .FirstOrDefaultAsync(l => l.Id == leagueId);

            if (league != null)
            {
                var season1 = league.SeasonOne;
                var season2 = league.SeasonTwo;

                _context.Leagues.Remove(league);

                if (season1 != null) _context.Seasons.Remove(season1);
                if (season2 != null) _context.Seasons.Remove(season2);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostExportToExcelAsync()
        {
            var leagues = await _context.Leagues
                .Include(l => l.SeasonOne)
                .Include(l => l.SeasonTwo)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LeagueData");
                var row = 1;

                worksheet.Cell(row, 1).Value = "Лига";
                worksheet.Cell(row, 2).Value = "Сезон 1 (Старт)";
                worksheet.Cell(row, 3).Value = "Сезон 1 (Край)";
                worksheet.Cell(row, 4).Value = "Сезон 2 (Старт)";
                worksheet.Cell(row, 5).Value = "Сезон 2 (Край)";

                foreach (var l in leagues)
                {
                    row++;
                    worksheet.Cell(row, 1).Value = l.Name;
                    worksheet.Cell(row, 2).Value = l.SeasonOne?.StartDate.Year > 1 ? l.SeasonOne?.StartDate.ToShortDateString() : "-";
                    worksheet.Cell(row, 3).Value = l.SeasonOne?.EndDate.Year > 1 ? l.SeasonOne?.EndDate.ToShortDateString() : "-";
                    worksheet.Cell(row, 4).Value = l.SeasonTwo?.StartDate.Year > 1 ? l.SeasonTwo?.StartDate.ToShortDateString() : "-";
                    worksheet.Cell(row, 5).Value = l.SeasonTwo?.EndDate.Year > 1 ? l.SeasonTwo?.EndDate.ToShortDateString() : "-";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "League_Info.xlsx");
                }
            }
        }
    }
}