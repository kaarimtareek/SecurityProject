using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecurityProject.Models;

namespace SecurityProject.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly SecurityProject.Models.SecurityProjectDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DetailsModel(SecurityProject.Models.SecurityProjectDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string userIdValue = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (userIdValue == null)
            {
                return RedirectToPage("../NotAuthenticated");
            }
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses
                .Include(c => c.Teacher).FirstOrDefaultAsync(m => m.Id == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
