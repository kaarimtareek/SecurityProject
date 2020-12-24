using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SecurityProject.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PrivacyModel(ILogger<PrivacyModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["useId"];

            //read cookie from Request object  
            string cookieValueFromReq = Request.Cookies["userId"];
        }
    }
}
