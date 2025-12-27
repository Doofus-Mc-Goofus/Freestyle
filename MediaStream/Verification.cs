using Newtonsoft.Json.Linq;

namespace MediaStream
{
    public class Verification
    {
        public static bool CorrectAccountDetails(IRequestCookieCollection cookies)
        {
            string datapath = @"D:\Freestyle\Debug\net6.0\data\user\" + cookies["username"];
            if (cookies != null && File.Exists(datapath))
            {
                FileStream stream = new(datapath, FileMode.Open, FileAccess.Read);
                byte[] result = new byte[stream.Length];
                _ = stream.Read(result, 0, (int)stream.Length);
                string data = System.Text.Encoding.UTF8.GetString(result);
                JObject json = JObject.Parse(data);
                stream.Close();
                stream.Dispose();
                return json["password"].ToString() == cookies["password"];
            }
            else
            {
                return false;
            }
        }

    }
}
