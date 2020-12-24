using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecurityProject.Models;

namespace SecurityProject.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly SecurityProjectDbContext _context;

        public IndexModel(SecurityProject.Models.SecurityProjectDbContext context)
        {
            _context = context;
        }

        public Models.User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
