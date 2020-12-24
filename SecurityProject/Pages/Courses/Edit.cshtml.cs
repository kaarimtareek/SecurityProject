using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using SecurityProject.Constants;
using SecurityProject.Models;
using SecurityProject.Services;

namespace SecurityProject.Pages.Courses
{
    public class UpdateCourseModel : PageModel
    {
        private readonly SecurityProject.Models.SecurityProjectDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryService coursesService;
        public UpdateCourseModel(SecurityProject.Models.SecurityProjectDbContext context, IHttpContextAccessor httpContextAccessor, IRepositoryService coursesService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            this.coursesService = coursesService;
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (cookieValueFromContext == null)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (cookieValueFromContext == null)
            {
                return RedirectToPage("../NotAuthenticated");
            }
            int userId = int.Parse(cookieValueFromContext);
            var user = coursesService.GetUser(userId);
            if (user == null )
                return RedirectToPage("../NotAuthenticated");
            coursesService.UpdateCourse(Course,user);
            return RedirectToPage("./Index");
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
