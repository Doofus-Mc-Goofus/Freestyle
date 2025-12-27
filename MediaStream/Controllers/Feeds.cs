using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MediaStream.Controllers
{
    public class Feeds : Controller
    {
        [HttpGet]
        [Route("api/randomVideo")]
        public async Task<IActionResult> Post()
        {
            string id = await RandomPublicVideoV2();
            return Content(id);
        }

        [HttpGet]
        [Route("api/randomGroup")]
        public async Task<IActionResult> Pist()
        {
            string id = await RandomPublicGroup();
            return Content(id);
        }

        public static async Task<string> RandomPublicVideo()
        {
            Console.WriteLine("WARNING: THIS FUNCTION IS OUT OF DATE. IT WILL BE DEPRECATED IN THE NEAR FUTURE. PLEASE USE V2 OF THIS FUNCTION INSTEAD.");
            Random random = new();
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            DirectoryInfo directoryInfo = new(datapath + @"vidmeta\");
            FileInfo[] files = directoryInfo.GetFiles();
            int id = random.Next(0, files.Count());
            FileStream stream = new(files[id].FullName, FileMode.Open, FileAccess.Read);
            byte[] result = new byte[stream.Length];
            _ = await stream.ReadAsync(result, 0, (int)stream.Length);
            string data = System.Text.Encoding.UTF8.GetString(result);
            JObject json = JObject.Parse(data);
            stream.Close();
            stream.Dispose();
            return json["public"] == null || json["public"].ToString() == "public" ? files[id].Name : await RandomPublicVideo();
        }

        public static async Task<string> RandomPublicVideoV2()
        {
            Random random = new();
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            DirectoryInfo DIDdirectoryInfo = new(datapath + @"profiles\");
            FileInfo[] DIDfiles = DIDdirectoryInfo.GetFiles();
            int DIDid = random.Next(0, DIDfiles.Count());
            DirectoryInfo directoryInfo = new(datapath + @"vidmeta\" + DIDfiles[DIDid].Name + @"\");
            FileInfo[] files = directoryInfo.GetFiles();
            if (files.Count() > 0)
            {
                int id = random.Next(0, files.Count());
                FileStream stream = new(files[id].FullName, FileMode.Open, FileAccess.Read);
                byte[] result = new byte[stream.Length];
                _ = await stream.ReadAsync(result, 0, (int)stream.Length);
                string data = System.Text.Encoding.UTF8.GetString(result);
                JObject json = JObject.Parse(data);
                stream.Close();
                stream.Dispose();
                return json["public"] == null || json["public"].ToString() == "public" ? "{\"videoID\":\"" + files[id].Name + "\",\"creatorDID\":\"" + json["creator"].ToString() + "\"}" : await RandomPublicVideoV2();
            }
            return await RandomPublicVideoV2();
        }

        public static async Task<string> RandomPublicGroup()
        {
            Random random = new();
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            DirectoryInfo directoryInfo = new(datapath + @"groups\");
            FileInfo[] files = directoryInfo.GetFiles();
            int id = random.Next(0, files.Count());
            FileStream stream = new(files[id].FullName, FileMode.Open, FileAccess.Read);
            byte[] result = new byte[stream.Length];
            _ = await stream.ReadAsync(result, 0, (int)stream.Length);
            string data = System.Text.Encoding.UTF8.GetString(result);
            JObject json = JObject.Parse(data);
            stream.Close();
            stream.Dispose();
            return json["public"] == null || json["public"].ToString() == "public" ? files[id].Name : await RandomPublicGroup();
        }
    }
}
