using Microsoft.AspNetCore.Mvc;

namespace MediaStream.Controllers
{
    public class Generator : Controller
    {
        private readonly Random random = new();
        private readonly string[] videoIDchars = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "-", "_" };
        [HttpGet]
        [Route("api/videoID")]
        public IEnumerable<BasicView> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new BasicView
            {
                Data = GenerateVideoID(),
            })
            .ToArray();
        }

        [HttpGet]
        [Route("api/did")]
        public IEnumerable<BasicView> DIDGet()
        {
            return Enumerable.Range(1, 1).Select(index => new BasicView
            {
                Data = GenerateDID(),
            })
            .ToArray();
        }

        [HttpGet]
        [Route("api/getSerVer")]
        public IActionResult ServGet()
        {
            return Content(Program.serverVersion);
        }

        public string GenerateVideoID()
        {
            string ID = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                ID += videoIDchars[random.Next(64)];
            }
            return ID;
        }

        public string GenerateDID()
        {
            string ID = string.Empty;
            for (int i = 0; i < 20; i++)
            {
                ID += videoIDchars[random.Next(64)];
            }
            return ID;
        }
    }
}
