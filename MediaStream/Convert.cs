using System;

namespace MediaStream
{
    public class Convert
    {
        public static string GetTime(string a)
        {
            TimeSpan time = DateTime.UtcNow - DateTime.Parse(a);
            if (time.TotalSeconds < 60)
            {
                return Math.Floor(time.TotalSeconds).ToString() + " seconds ago";
            }
            else if (time.TotalMinutes < 60)
            {
                return Math.Floor(time.TotalMinutes).ToString() + " minutes ago";
            }
            else if (time.TotalHours < 24)
            {
                return Math.Floor(time.TotalHours).ToString() + " hours ago";
            }
            else if (time.TotalDays < 30)
            {
                return Math.Floor(time.TotalDays).ToString() + " days ago";
            }
            else if (time.TotalDays < 365)
            {
                return Math.Floor(time.TotalDays / 30).ToString() + " months ago";
            }
            else
            {
                return DateTime.UtcNow.ToShortDateString();
            }
        }

        public static string GetSeenTime(string a)
        {
            TimeSpan time = DateTime.UtcNow - DateTime.Parse(a);
            if (time.TotalSeconds < 300)
            {
                return "Now!";
            }
            else if (time.TotalMinutes < 60)
            {
                return Math.Floor(time.TotalMinutes).ToString() + " minutes ago";
            }
            else if (time.TotalHours < 2)
            {
                return "An hour ago";
            }
            else if (time.TotalHours < 24)
            {
                return Math.Floor(time.TotalHours).ToString() + " hours ago";
            }
            else if (time.TotalDays < 2)
            {
                return "A day ago";
            }
            else if (time.TotalDays < 30)
            {
                return Math.Floor(time.TotalDays).ToString() + " days ago";
            }
            else if (time.TotalDays < 60)
            {
                return "About a month ago";
            }
            else if (time.TotalDays < 365)
            {
                return "About " + Math.Floor(time.TotalDays / 30).ToString() + " months ago";
            }
            else
            {
                return DateTime.UtcNow.ToShortDateString();
            }
        }

        public static string ShortenNumber(string a)
        {
            long number = long.Parse(a);
            if (number < 1000)
            {
                return number.ToString();
            }
            else if (number < 10000)
            {
                return (number / 1000).ToString("F2") + "K";
            }
            else if (number < 100000)
            {
                return (number / 1000).ToString("F1") + "K";
            }
            else if (number < 1000000)
            {
                return (number / 1000).ToString("F0") + "K";
            }
            else if (number < 10000000)
            {
                return (number / 1000000).ToString("F2") + "M";
            }
            else if (number < 100000000)
            {
                return (number / 1000000).ToString("F1") + "M";
            }
            else if (number < 1000000000)
            {
                return (number / 1000000).ToString("F0") + "M";
            }
            else if (number < 10000000000)
            {
                return (number / 1000000000).ToString("F2") + "B";
            }
            else if (number < 100000000000)
            {
                return (number / 1000000000).ToString("F1") + "B";
            }
            else if (number < 1000000000000)
            {
                return (number / 1000000000).ToString("F0") + "B";
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
