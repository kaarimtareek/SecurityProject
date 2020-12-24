using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SecurityProject.Constants;
using SecurityProject.Services;
using SecurityProject.ViewModels;

namespace SecurityProject.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService authenticationService;

        public RegisterModel(IHttpContextAccessor httpContextAccessor, IAuthenticationService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            this.authenticationService = authenticationService;
        }

        public IActionResult OnGet()
        {
            var userIdValue = _httpContextAccessor.HttpContext.Request.Cookies["useId"];
            if (userIdValue != null)
                return RedirectToPage("./Index");
            return Page();
        }

        [BindProperty]
        public UserVM User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = authenticationService.Register(User.Name, User.Email, User.Password, User.IsAdmin ? USERTYPES.ADMIN : USERTYPES.TEACHER);
            if (result != null)
            {
                Response.Cookies.Append("userId", result.Id.ToString());

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}