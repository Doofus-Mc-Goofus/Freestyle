using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MediaStream.Controllers
{
    public class GetLang : Controller
    {
        [HttpGet]
        [Route("api/getLangDisplayName")]
        public IActionResult Get(string techName, string lang = "en")
        {
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            return Content(GetDisplayName(techName, lang));
        }

        public static string GetDisplayName(string techName, string lang = "en")
        {
            if (lang == "en")
            {
                switch (techName)
                {
                    case "auto":
                        return "Autos & Vehicles";
                    case "comedy":
                        return "Comedy";
                    case "education":
                        return "Education";
                    case "entertain":
                        return "Entertainment";
                    case "film":
                        return "Film & Animation";
                    case "tutorial":
                        return "Tutorials & Style";
                    case "music":
                        return "Music";
                    case "news":
                        return "News & Politics";
                    case "blog":
                        return "People & Blogs";
                    case "pet":
                        return "Pets & Animals";
                    case "tech":
                        return "Science & Technology";
                    case "sports":
                        return "Sports";
                    case "travel":
                        return "Travel & Events";
                    case "none":
                        return "Unspecified Genre";
                    case "draft":
                        return "Draft";
                    default:
                        return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
