using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;

namespace UNWE_LaegueApp.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string Name, string Email, string Subject, string Message)
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("system@unweleague.com", "UNWE League System");
                mail.To.Add("dimitargemedjiev@gmail.com");
                mail.Subject = $"Контактна форма: {Subject}";
                mail.Body = $"Име: {Name}\nЕмейл: {Email}\n\nСъобщение:\n{Message}";
                mail.IsBodyHtml = false;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("YOUR_SYSTEM_EMAIL", "YOUR_APP_PASSWORD");
                    smtp.EnableSsl = true;                 
                }

                TempData["MessageSent"] = true;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Грешка при изпращането: " + ex.Message);
                return Page();
            }

            return RedirectToPage();
        }
    }
}