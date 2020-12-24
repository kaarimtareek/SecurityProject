using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using SecurityProject.Constants;
using SecurityProject.Models;
using SecurityProject.Services;
using SecurityProject.ViewModels;

namespace SecurityProject.Pages.Courses
{
    public class CreateCorseModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryService coursesService;

        public CreateCorseModel(IHttpContextAccessor httpContextAccessor, IRepositoryService coursesService)
        {

            _httpContextAccessor = httpContextAccessor;
            this.coursesService = coursesService;
        }

        public IActionResult OnGet()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if(cookieValueFromContext==null)
            {
                return RedirectToPage("../NotAuthenticated");
            }
            int userId = int.Parse(cookieValueFromContext);
            var user = coursesService.GetUser(userId);
            if (user == null || user.Type != USERTYPES.TEACHER)
                return RedirectToPage("../NotAuthorized");
            return Page();
        }

        [BindProperty]
        public CreateCourseVM Course { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (cookieValueFromContext == null)
                return RedirectToPage("../NotAuthenticated");
            int userId = int.Parse(cookieValueFromContext);
            var user = coursesService.GetUser(userId);
            if (user == null || user.Type != USERTYPES.TEACHER)
                return RedirectToPage("../NotAuthorized");
            Course course = new Course
            {
                Name = Course.Name,
                Description = Course.Description,
                Price = Course.Price,
                TeacherId = user.Id
            };
            coursesService.AddCourse(course,user);
           
            return RedirectToPage("./Index");
        }
    }
}
