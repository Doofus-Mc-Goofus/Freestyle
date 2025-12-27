using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MediaStream.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("watch")]
        public IActionResult Get(string id = "", string quality = "720", string creator = "")
        {
            string jsonfilePath = "D:\\Freestyle\\Debug\\net6.0\\data\\vidmeta\\" + creator + "\\" + id;
            FileStream stream = new(jsonfilePath, FileMode.Open, FileAccess.Read);
            byte[] result = new byte[stream.Length];
            _ = stream.Read(result, 0, (int)stream.Length);
            stream.Close();
            stream.Dispose();
            string data = System.Text.Encoding.UTF8.GetString(result);
            JObject json = JObject.Parse(data);
            bool visible = true;
            if (json["public"] != null && json["public"].ToString() == "private")
            {
                visible = false;
            }
            string username = string.Empty;
            if (Request.Cookies["username"] != null)
            {
                username = Request.Cookies["username"].ToString();
            }
            string filePath = "D:\\Freestyle\\Debug\\net6.0\\data\\vids\\" + creator + "\\" + id + "_" + quality + ".mp4";
            if (System.IO.File.Exists(filePath) && (visible || json["creator"].ToString() == username))
            {
                FileStream vidstream = new(filePath, FileMode.Open, FileAccess.Read);
                return File(vidstream, "video/mp4", enableRangeProcessing: true);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("caption")]
        public IActionResult GetCaption(string id = "", string quality = "720", string creator = "")
        {
            string jsonfilePath = "D:\\Freestyle\\Debug\\net6.0\\data\\vidmeta\\" + creator + "\\" + id;
            FileStream stream = new(jsonfilePath, FileMode.Open, FileAccess.Read);
            byte[] result = new byte[stream.Length];
            _ = stream.Read(result, 0, (int)stream.Length);
            stream.Close();
            stream.Dispose();
            string data = System.Text.Encoding.UTF8.GetString(result);
            JObject json = JObject.Parse(data);
            bool visible = true;
            if (json["public"] != null && json["public"].ToString() == "private")
            {
                visible = false;
            }
            string username = string.Empty;
            if (Request.Cookies["username"] != null)
            {
                username = Request.Cookies["username"].ToString();
            }
            string filePath = "D:\\Freestyle\\Debug\\net6.0\\data\\vidcaption\\" + creator + "\\" + id;
            if (System.IO.File.Exists(filePath) && (visible || json["creator"].ToString() == username))
            {
                FileStream vidstream = new(filePath, FileMode.Open, FileAccess.Read);
                byte[] vidresult = new byte[vidstream.Length];
                _ = vidstream.Read(vidresult, 0, (int)vidstream.Length);
                vidstream.Close();
                vidstream.Dispose();
                string viddata = System.Text.Encoding.UTF8.GetString(vidresult);
                return Content(viddata, "text/vtt");
            }
            return BadRequest();
        }
    }
}
