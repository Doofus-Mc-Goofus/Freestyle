using Microsoft.AspNetCore.Mvc;

namespace MediaStream.Controllers
{
    public class ThumbGet : Controller
    {
        [HttpGet]
        [Route("api/thumb")]
        public IActionResult Get(string id = "", string type = "")
        {
            string filePath = "D:\\Freestyle\\Debug\\net6.0\\data\\thumbs\\" + type + "_" + id + ".jpg";
            string PNGfilePath = "D:\\Freestyle\\Debug\\net6.0\\data\\thumbs\\" + type + "_" + id + ".png";
            if (System.IO.File.Exists(filePath))
            {
                FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(stream, "image/jpeg");
            }
            else
            {
                FileStream stream = System.IO.File.Exists(PNGfilePath)
                    ? new(PNGfilePath, FileMode.Open, FileAccess.Read)
                    : new("D:\\Freestyle\\Debug\\net6.0\\data\\thumbs\\thumb.png", FileMode.Open, FileAccess.Read);
                return new FileStreamResult(stream, "image/png");
            }

        }
    }
}
