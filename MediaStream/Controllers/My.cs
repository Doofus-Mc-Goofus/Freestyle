using Microsoft.AspNetCore.Mvc;

namespace MediaStream.Controllers
{
    public class My : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public My(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("my")]
        public RedirectResult Get(string q = "")
        {
            string username = _httpContextAccessor.HttpContext.Request.Cookies["username"];
            if (username == null)
            {
                return new RedirectResult("~/login", true);
            }
            else
            {
                return new RedirectResult("~/profile?id=" + username + ((q == "") ? "" : "&page=" + q), true);
            }
        }
    }
}
