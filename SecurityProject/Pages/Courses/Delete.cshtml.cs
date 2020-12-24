using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecurityProject.Models;
using SecurityProject.Services;

namespace SecurityProject.Pages.Courses
{
    public class DeleteCourseModel : PageModel
    {
        private readonly SecurityProject.Models.SecurityProjectDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryService coursesService;
        public DeleteCourseModel(SecurityProject.Models.SecurityProjectDbContext context, IHttpContextAccessor httpContextAccessor, IRepositoryService coursesService)
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
            int userId = int.Parse(cookieValueFromContext);
            var user = coursesService.GetUser(userId);
            if (user == null )
                return RedirectToPage("../NotAuthenticated");
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (cookieValueFromContext == null)
            {
                return RedirectToPage("../NotAuthenticated");
            }
            int userId = int.Parse(cookieValueFromContext);
            var user = coursesService.GetUser(userId);
            if (user == null)
                return RedirectToPage("../NotAuthenticated");
            if (id == null)
            {
                return NotFound();
            }
            Course = coursesService.GetCourse(id.Value, user);
            if (Course == null)
                return NotFound();

         var result =  coursesService.DeleteCourse(Course, user);
            if (result)
            {

            }
            return RedirectToPage("./Index");
        }
    }
}
