using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using SecurityProject.Services;

namespace SecurityProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryService coursesService;
        private readonly IAuthenticationService authenticationService;
        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor, IRepositoryService coursesService, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            this.coursesService = coursesService;
            this.authenticationService = authenticationService;
        }

        public IActionResult OnGet()
        {
           

           
           var userIdValue = _httpContextAccessor.HttpContext.Request.Cookies["userId"];

            if(userIdValue==null)
            {
                ViewData["IsLogined"] = false;
               return RedirectToPage("./Register");
            }
            ViewData["IsLogined"] = true;
            return RedirectToPage("./Courses/Index");

        }
    }
}
