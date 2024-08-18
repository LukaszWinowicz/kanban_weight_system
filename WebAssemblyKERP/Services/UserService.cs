using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAssemblyKERP.Database;
using WebAssemblyKERP.Models;

namespace WebAssemblyKERP.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveUserAsync(ClaimsPrincipal principal)
        {
            var googleId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = principal.FindFirst(ClaimTypes.Name)?.Value;
            var picture = principal.FindFirst("picture")?.Value;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId);

            if (user == null)
            {
                user = new User
                {
                    GoogleId = googleId,
                    Email = email,
                    Name = name,
                    PictureUrl = picture
                };
                _context.Users.Add(user);
            }
            else
            {
                // Zaktualizuj istniejącego użytkownika, jeśli jest to konieczne
                user.Email = email;
                user.Name = name;
                user.PictureUrl = picture;
            }

            await _context.SaveChangesAsync();
        }
    }
}
