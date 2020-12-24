using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using SecurityProject.Constants;
using SecurityProject.Models;
using SecurityProject.Services;
using SecurityProject.ViewModels;

namespace SecurityProject.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly IRepositoryService coursesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IRepositoryService coursesService, IHttpContextAccessor httpContextAccessor)
        {
            this.coursesService = coursesService;
            _httpContextAccessor = httpContextAccessor;
        }

       

        public IList<CourseVM> Courses { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if(cookieValueFromContext==null)
            {
                return RedirectToPage("../NotAuthenticated");
            }
            int userId = int.Parse(cookieValueFromContext);
            var user =  coursesService.GetUser(userId);
            if(user.Type == USERTYPES.ADMIN)
            {
                ViewData["IsAdmin"] = true;
            }
            else
            {
                ViewData["IsAdmin"] = false;
            }
            Courses = coursesService.GetCourses(user).Select(c=> new CourseVM { Id = c.Id ,Description = c.Description ,Price = c.Price ,Name = c.Name ,TeacherId = c.TeacherId ,TeacherName = c.Teacher.Name }).ToList();
            return Page();
            //_httpContextAccessor.HttpContext.
            // Course = coursesService.GetCourses(); 
        }
    }
}
