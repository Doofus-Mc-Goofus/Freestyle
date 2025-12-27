using System;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace MediaStream.Controllers
{
    public class RSS : Controller
    {
        [HttpGet]
        [Route("rss")]
        public IActionResult Get()
        {
            var feed = GetBlog();
            StringBuilder output = new StringBuilder();
            XmlWriter rssWriter = XmlWriter.Create(output, new XmlWriterSettings { Indent = true });
            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed.Feed);
            rssFormatter.WriteTo(rssWriter);
            rssWriter.Close();
            return Content(output.ToString().Replace("utf-16", "utf-8"), "application/rss+xml");
        }

        public Rss20FeedFormatter GetBlog()
        {
            SyndicationFeed feed = new SyndicationFeed("RSS Feedstyle - Latest", "See the latest videos in your feed", new Uri("https://www.thefreestyle.net"));
            feed.Authors.Add(new SyndicationPerson("The Freestyle Team"));
            List<SyndicationItem> items = new List<SyndicationItem>();

            for (int i = 0; i < 10; i++)
            {
                SyndicationItem item = new SyndicationItem(
                (i + 1).ToString() + ". " + "Video Title",
                "description",
                new Uri("https://www.thefreestyle.net/play?id=" + "id"),
                i+"id",
                DateTime.Now);

                items.Add(item);
            }

            feed.Items = items;

            return new Rss20FeedFormatter(feed);
        }
    }
}
