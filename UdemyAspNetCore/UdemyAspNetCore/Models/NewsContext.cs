using System.Collections.Generic;

namespace UdemyAspNetCore.Models
{
    public static class NewsContext
    {
        public static List<News> List = new List<News>()
        {
            new News {Title="Haber 1"},
            new News {Title="Haber 2"}
        };
    }
}
