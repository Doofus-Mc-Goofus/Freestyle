using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediaStream.Pages
{
    public class playModel : PageModel
    {
        public void OnGet()
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hello");
        }
    }
}
