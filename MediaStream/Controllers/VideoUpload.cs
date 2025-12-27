using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace MediaStream.Controllers
{
    public class VideoUpload : Controller
    {
        [HttpPost]
        [Route("api/uploadVideo")]
        public async Task<IActionResult> Upload()
        {
            HttpRequest request = HttpContext.Request;

            if (request.Cookies["username"] != null)
            {
                // validation of Content-Type
                // 1. first, it must be a form-data request
                // 2. a boundary should be found in the Content-Type
                if (!request.HasFormContentType ||
                    !MediaTypeHeaderValue.TryParse(request.ContentType, out MediaTypeHeaderValue? mediaTypeHeader) ||
                    string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
                {
                    return new UnsupportedMediaTypeResult();
                }

                string boundary = HeaderUtilities.RemoveQuotes(mediaTypeHeader.Boundary.Value).Value;
                MultipartReader reader = new(boundary, request.Body);
                MultipartSection? section = await reader.ReadNextSectionAsync();

                // This sample try to get the first file from request and save it
                // Make changes according to your needs in actual use
                while (section != null)
                {
                    bool hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                        out ContentDispositionHeaderValue? contentDisposition);

                    if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data") &&
                        !string.IsNullOrEmpty(contentDisposition.FileName.Value))
                    {
                        // Don't trust any file name, file extension, and file data from the request unless you trust them completely
                        // Otherwise, it is very likely to cause problems such as virus uploading, disk filling, etc
                        // In short, it is necessary to restrict and verify the upload
                        // Here, we just use the temporary folder and a random file name

                        // Get the temporary folder, and combine a random file name with it
                        Generator generator = new();
                        string username = request.Cookies["username"].ToString();
                        string videoID = generator.GenerateVideoID();
                        string saveToPath = Path.Combine("D:\\Freestyle\\Debug\\net6.0\\data\\vids\\" + username + @"\", videoID + "_" + "720" + ".mp4");
                        using (FileStream targetStream = System.IO.File.Create(saveToPath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                        string datapath = @"D:\Freestyle\Debug\net6.0\data\";
                        using (FileStream fs = System.IO.File.Create(datapath + @"vidmeta\" + username + @"\" + videoID))
                        {
                            byte[] info = new System.Text.UTF8Encoding(true).GetBytes("{\"title\":\"" + contentDisposition.FileName.Value + "\",\"desc\":\"" + "\",\"creator\":\"" + username + "\",\"date\":\"" + DateTime.UtcNow.ToString("s") + "\",\"views\":\"0\",\"public\":\"private\",\"draft\":\"true\"}");
                            fs.Write(info, 0, info.Length);
                        }
                        FileStream stream = new(datapath + @"user\" + username, FileMode.Open, FileAccess.Read);
                        byte[] result = new byte[stream.Length];
                        _ = await stream.ReadAsync(result.AsMemory(0, (int)stream.Length));
                        string data = System.Text.Encoding.UTF8.GetString(result);
                        JObject json = JObject.Parse(data);
                        JArray aaaaaaaaa = JArray.Parse(json["videosMade"].ToString());
                        aaaaaaaaa.AddFirst(videoID);
                        json["videosMade"] = aaaaaaaaa;
                        stream.Close();
                        stream.Dispose();
                        using (FileStream fs = new(datapath + @"user\" + username, FileMode.Open, FileAccess.Write))
                        {
                            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(json.ToString());
                            fs.Write(info, 0, info.Length);
                        }
                        // _ = Process.UploadVideo(saveToPath);
                        return Redirect("/edit?id=" + videoID + "&creator=" + username);
                    }

                    section = await reader.ReadNextSectionAsync();
                }

                // If the code runs to this location, it means that no files have been saved
                return BadRequest("No video data in the request.");
            }
            return BadRequest(400);
        }
    }
}
