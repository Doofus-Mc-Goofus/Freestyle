using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediaStream.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        public void Signin_Click()
        {
            Response.Redirect("~/watch");
        }
    }
}
