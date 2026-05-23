using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Data;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Pages.Team
{
    [Authorize(Roles = "Captain,Admin")]
    public class ManageRequestsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ManageRequestsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<JoinRequest> Requests { get; set; } = new();
        public UNWE_LaegueApp.Models.Team? MyTeam { get; set; }
        public Dictionary<string, UserProfile> Profiles { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            MyTeam = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Players.Any(p => p.UserId == user.Id && p.Captain));

            if (MyTeam != null)
            {
                Requests = await _context.JoinRequests
                    .Where(r => r.TeamId == MyTeam.Id && r.Status == "Pending")
                    .ToListAsync();

                var requesterIds = Requests.Select(r => r.UserId).ToList();
                Profiles = await _context.UserProfiles
                    .Where(up => requesterIds.Contains(up.UserId))
                    .ToDictionaryAsync(up => up.UserId);
            }
        }

        public async Task<IActionResult> OnPostAcceptAsync(int requestId)
        {
            var request = await _context.JoinRequests.FindAsync(requestId);
            if (request == null) return RedirectToPage();

            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == request.TeamId);
            if (team != null)
            {
                var newPlayer = new Player
                {
                    UserId = request.UserId,
                    Name = request.UserEmail,
                    TeamId = team.Id,
                    Captain = false
                };

                _context.Players.Add(newPlayer);

                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Player");
                    if (await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.RemoveFromRoleAsync(user, "User");
                    }
                }

                request.Status = "Approved";
                _context.JoinRequests.Update(request);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(int requestId)
        {
            var request = await _context.JoinRequests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = "Rejected";
                _context.JoinRequests.Update(request);

                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}