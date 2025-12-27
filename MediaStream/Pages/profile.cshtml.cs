using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediaStream.Pages
{
    public class profileModel : PageModel
    {
        public void OnGet()
        {

        }

        public string? id { get; set; }
    }
}
