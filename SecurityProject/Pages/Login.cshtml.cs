using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SecurityProject.Models;
using SecurityProject.ViewModels;

namespace SecurityProject.Pages
{
    public class LoginModel : PageModel
    {
  
        private readonly Services.IAuthenticationService authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginModel(Services.IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {

            this.authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            var userIdValue = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
            if (userIdValue != null)
                return RedirectToPage("./Index");
            return Page();
        }

        [BindProperty]
        public LoginVM User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           var result =  authenticationService.Login(User.Email, User.Password);
           if(result!=null)
            {
                Response.Cookies.Append("userId", result.Id.ToString());
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
