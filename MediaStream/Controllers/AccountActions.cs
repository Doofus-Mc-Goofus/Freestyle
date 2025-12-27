using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MediaStream.Controllers
{
    public class AccountActions : Controller
    {
        [HttpPost]
        [Route("api/follow")]
        public async Task<IActionResult> Post(HttpClient httpClient)
        {
            Stream req = Request.Body;
            string data = string.Empty;
            using (StreamReader reader = new(req, Encoding.UTF8, true, 1024, true))
            {
                data = await reader.ReadToEndAsync();
            }
            JObject jObject = JObject.Parse(data);
            if (Request.Cookies["username"] != jObject["username"].ToString())
            {
                string id = jObject["username"].ToString();
                string myid = Request.Cookies["username"];
                string datapath = @"D:\Freestyle\Debug\net6.0\data\";
                if (System.IO.File.Exists(datapath + @"profiles\" + id) && System.IO.File.Exists(datapath + @"profiles\" + myid))
                {
                    FileStream stream = new(datapath + @"profiles\" + id, FileMode.Open, FileAccess.Read);
                    FileStream mystream = new(datapath + @"profiles\" + myid, FileMode.Open, FileAccess.Read);
                    byte[] result = new byte[stream.Length];
                    byte[] myresult = new byte[mystream.Length];
                    _ = await stream.ReadAsync(result, 0, (int)stream.Length);
                    _ = await mystream.ReadAsync(myresult, 0, (int)mystream.Length);
                    string jsondata = Encoding.UTF8.GetString(result);
                    string myjsondata = Encoding.UTF8.GetString(myresult);
                    stream.Close();
                    stream.Dispose();
                    mystream.Close();
                    mystream.Dispose();
                    JObject json = JObject.Parse(jsondata);
                    JObject myjson = JObject.Parse(myjsondata);
                    JArray jarray = JArray.Parse(json["followersList"].ToString());
                    JArray myjarray = JArray.Parse(myjson["followingList"].ToString());
                    if (!jarray.ToString().Contains("\"" + myid + "\""))
                    {
                        jarray.Add(myid);
                        myjarray.Add(id);
                        json["followersList"] = jarray;
                        myjson["followingList"] = myjarray;
                        json["followers"] = jarray.Count;
                        myjson["following"] = myjarray.Count;
                        using (FileStream fs = System.IO.File.Create(datapath + @"profiles\" + id))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(json.ToString());
                            fs.Write(info, 0, info.Length);
                        }
                        using (FileStream fs = System.IO.File.Create(datapath + @"profiles\" + myid))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(myjson.ToString());
                            fs.Write(info, 0, info.Length);
                        }
                        return Content("{\"redirect\":\"" + Verification.CorrectAccountDetails(Request.Cookies) + "\"}");
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
                else
                {
                    return myid == null || myid == string.Empty
    ? Content("{\"redirect\":\"true\"" + "}")
    : StatusCode(400);
                }
            }
            else
            {
                return StatusCode(400);
            }
            return StatusCode(400);

        }

        [HttpPost]
        [Route("api/groupfollow")]
        public async Task<IActionResult> GroupPost(HttpClient httpClient)
        {
            Stream req = Request.Body;
            string data = string.Empty;
            using (StreamReader reader = new(req, Encoding.UTF8, true, 1024, true))
            {
                data = await reader.ReadToEndAsync();
            }
            JObject jObject = JObject.Parse(data);
            string id = jObject["username"].ToString();
            string myid = Request.Cookies["username"];
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            if (System.IO.File.Exists(datapath + @"groups\" + id) && System.IO.File.Exists(datapath + @"profiles\" + myid))
            {
                FileStream stream = new(datapath + @"groups\" + id, FileMode.Open, FileAccess.Read);
                FileStream mystream = new(datapath + @"profiles\" + myid, FileMode.Open, FileAccess.Read);
                byte[] result = new byte[stream.Length];
                byte[] myresult = new byte[mystream.Length];
                _ = await stream.ReadAsync(result, 0, (int)stream.Length);
                _ = await mystream.ReadAsync(myresult, 0, (int)mystream.Length);
                string jsondata = Encoding.UTF8.GetString(result);
                string myjsondata = Encoding.UTF8.GetString(myresult);
                stream.Close();
                stream.Dispose();
                mystream.Close();
                mystream.Dispose();
                JObject json = JObject.Parse(jsondata);
                JObject myjson = JObject.Parse(myjsondata);
                if (myjson["groupfollowing"] == null)
                {
                    myjson.Add("groupfollowing", 0);
                }
                if (myjson["groupfollowingList"] == null)
                {
                    myjson.Add("groupfollowingList", new JArray());
                }
                JArray jarray = JArray.Parse(json["followersList"].ToString());
                JArray myjarray = JArray.Parse(myjson["groupfollowingList"].ToString());
                if (!jarray.ToString().Contains("\"" + myid + "\""))
                {
                    jarray.Add(myid);
                    myjarray.Add(id);
                    json["followersList"] = jarray;
                    myjson["groupfollowingList"] = myjarray;
                    json["followers"] = jarray.Count;
                    myjson["groupfollowing"] = myjarray.Count;
                    using (FileStream fs = System.IO.File.Create(datapath + @"groups\" + id))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(json.ToString());
                        fs.Write(info, 0, info.Length);
                    }
                    using (FileStream fs = System.IO.File.Create(datapath + @"profiles\" + myid))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(myjson.ToString());
                        fs.Write(info, 0, info.Length);
                    }
                    return Content("{\"redirect\":\"" + Verification.CorrectAccountDetails(Request.Cookies) + "\"}");
                }
                else
                {
                    return StatusCode(400);
                }
            }
            else
            {
                return myid == null || myid == string.Empty
? Content("{\"redirect\":\"true\"" + "}")
: StatusCode(400);
            }
            return StatusCode(400);

        }


        [HttpPost]
        [Route("api/unfollow")]
        public async Task<IActionResult> Unpost(HttpClient httpClient)
        {
            Stream req = Request.Body;
            string data = string.Empty;
            using (StreamReader reader = new(req, Encoding.UTF8, true, 1024, true))
            {
                data = await reader.ReadToEndAsync();
            }
            JObject jObject = JObject.Parse(data);
            if (Request.Cookies["username"] != jObject["username"].ToString())
            {
                string id = jObject["username"].ToString();
                string myid = Request.Cookies["username"];
                string datapath = @"D:\Freestyle\Debug\net6.0\data\";
                if (System.IO.File.Exists(datapath + @"profiles\" + id) && System.IO.File.Exists(datapath + @"profiles\" + myid))
                {
                    FileStream stream = new(datapath + @"profiles\" + id, FileMode.Open, FileAccess.Read);
                    FileStream mystream = new(datapath + @"profiles\" + myid, FileMode.Open, FileAccess.Read);
                    byte[] result = new byte[stream.Length];
                    byte[] myresult = new byte[mystream.Length];
                    _ = await stream.ReadAsync(result, 0, (int)stream.Length);
                    _ = await mystream.ReadAsync(myresult, 0, (int)mystream.Length);
                    string jsondata = Encoding.UTF8.GetString(result);
                    string myjsondata = Encoding.UTF8.GetString(myresult);
                    stream.Close();
                    stream.Dispose();
                    mystream.Close();
                    mystream.Dispose();
                    JObject json = JObject.Parse(jsondata);
                    JObject myjson = JObject.Parse(myjsondata);
                    JArray jarray = JArray.Parse(json["followersList"].ToString());
                    JArray myjarray = JArray.Parse(myjson["followingList"].ToString());
                    if (jarray.ToString().Contains("\"" + myid + "\""))
                    {
                        JToken fish = jarray.FirstOrDefault(arr => arr.Type == JTokenType.String && arr.Value<string>() == myid);
                        JToken myfish = myjarray.FirstOrDefault(arr => arr.Type == JTokenType.String && arr.Value<string>() == id);
                        _ = jarray.Remove(fish);
                        _ = myjarray.Remove(myfish);
                        json["followersList"] = jarray;
                        myjson["followingList"] = myjarray;
                        json["followers"] = jarray.Count;
                        myjson["following"] = myjarray.Count;
                        using (FileStream fs = System.IO.File.Create(datapath + @"profiles\" + id))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(json.ToString());
                            fs.Write(info, 0, info.Length);
                        }
                        using (FileStream fs = System.IO.File.Create(datapath + @"profiles\" + myid))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(myjson.ToString());
                            fs.Write(info, 0, info.Length);
                        }
                        return Content("{\"redirect\":\"" + Verification.CorrectAccountDetails(Request.Cookies) + "\"}");
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
                else
                {
                    return myid == null || myid == string.Empty
                        ? Content("{\"redirect\":\"true\"" + "}")
                        : StatusCode(400);
                }
            }
            else
            {
                return StatusCode(400);
            }
            return StatusCode(400);
        }

        [HttpPost]
        [Route("api/groupunfollow")]
        public async Task<IActionResult> GroupUnpost(HttpClient httpClient)
        {
            Stream req = Request.Body;
            string data = string.Empty;
            using (StreamReader reader = new(req, Encoding.UTF8, true, 1024, true))
            {
                data = await reader.ReadToEndAsync();
            }
            JObject jObject = JObject.Parse(data);
            string id = jObject["username"].ToString();
            string myid = Request.Cookies["username"];
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            if (System.IO.File.Exists(datapath + @"groups\" + id) && System.IO.File.Exists(datapath + @"profiles\" + myid))
            {
                FileStream stream = new(datapath + @"groups\" + id, FileMode.Open, FileAccess.Read);
                FileStream mystream = new(datapath + @"profiles\" + myid, FileMode.Open, FileAccess.Read);
                byte[] result = new byte[stream.Length];
                byte[] myresult = new byte[mystream.Length];
                _ = await stream.ReadAsync(result, 0, (int)stream.Length);
                _ = await mystream.ReadAsync(myresult, 0, (int)mystream.Length);
                string jsondata = Encoding.UTF8.GetString(result);
                string myjsondata = Encoding.UTF8.GetString(myresult);
                stream.Close();
                stream.Dispose();
                mystream.Close();
                mystream.Dispose();
                JObject json = JObject.Parse(jsondata);
                JObject myjson = JObject.Parse(myjsondata);
                JArray jarray = JArray.Parse(json["followersList"].ToString());
                JArray myjarray = JArray.Parse(myjson["groupfollowingList"].ToString());
                if (jarray.ToString().Contains("\"" + myid + "\""))
                {
                    JToken fish = jarray.FirstOrDefault(arr => arr.Type == JTokenType.String && arr.Value<string>() == myid);
                    JToken myfish = myjarray.FirstOrDefault(arr => arr.Type == JTokenType.String && arr.Value<string>() == id);
                    _ = jarray.Remove(fish);
                    _ = myjarray.Remove(myfish);
                    json["followersList"] = jarray;
                    myjson["groupfollowingList"] = myjarray;
                    json["followers"] = jarray.Count;
                    myjson["groupfollowing"] = myjarray.Count;
                    using (FileStream fs = System.IO.File.Create(datapath + @"groups\" + id))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(json.ToString());
                        fs.Write(info, 0, info.Length);
                    }
                    using (FileStream fs = System.IO.File.Create(datapath + @"profiles\" + myid))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(myjson.ToString());
                        fs.Write(info, 0, info.Length);
                    }
                    return Content("{\"redirect\":\"" + Verification.CorrectAccountDetails(Request.Cookies) + "\"}");
                }
                else
                {
                    return StatusCode(400);
                }
            }
            else
            {
                return myid == null || myid == string.Empty
                    ? Content("{\"redirect\":\"true\"" + "}")
                    : StatusCode(400);
            }
            return StatusCode(400);
        }

        [HttpGet]
        [Route("api/profileCSS")]
        public IActionResult Get(string ID)
        {
            string datapath = @"D:\Freestyle\Debug\net6.0\data\";
            if (System.IO.File.Exists(datapath + @"profiles\css\" + ID))
            {
                FileStream stream = new(datapath + @"profiles\css\" + ID, FileMode.Open, FileAccess.Read);
                byte[] result = new byte[stream.Length];
                _ = stream.Read(result, 0, (int)stream.Length);
                string jsondata = Encoding.UTF8.GetString(result);
                stream.Close();
                stream.Dispose();
                return Content(jsondata, "text/css");
            }
            return Content(string.Empty, "text/css");
        }

        [HttpPost]
        [Route("api/signout")]
        public IActionResult Post(string ID)
        {
            if (Request.Cookies["username"] != null)
            {
                Response.Cookies.Delete("username");
                Response.Cookies.Delete("password");
                Response.Cookies.Delete("session");
                return Content("{\"redirect\":\"true\"" + "}");
            }
            return BadRequest();
        }
    }
}