using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SecurityProject.Services;

namespace SecurityProject.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryService coursesService;

        public LogoutModel(IHttpContextAccessor httpContextAccessor, IRepositoryService coursesService)
        {
            _httpContextAccessor = httpContextAccessor;
            this.coursesService = coursesService;
        }

        public IActionResult OnGet()
        {
            string userIdValue = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (userIdValue == null)
            {
                return RedirectToPage("./Index");
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("userId");

            return RedirectToPage("./Login");

        }
     
    }
}
