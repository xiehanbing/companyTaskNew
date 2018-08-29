using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumExcly
{
    public class ConcurrentBagService
    {
        private static Dictionary<string, string[]> _contentEmulation = new Dictionary<string, string[]>();

        static async Task RunProgram()
        {
            var bag = new ConcurrentBag<CrawlingTask>();
            string[] urls = new[]
            {
                "http://microsoft.com/",
                "http://google.com/",
                "http://facebook.com/",
                "http://twitter.com/"
            };
            var crawlers=new Task[4];
            for (int i = 1; i <=4; i++)
            {
                string crawlerName = "Crawler " + i.ToString();
                bag.Add(new CrawlingTask()
                {
                    UrlToCarwl = urls[i-1],
                    ProducerName = "root"
                });
                crawlers[i-1]=Task.Run(()=>);
            }
        }

        static async Task Crawl(ConcurrentBag<CrawlingTask> bag, string crawlerName)
        {
            CrawlingTask task;
            while (bag.TryTake(out task))
            {
                IEnumerable<string>urls=await 
            }
        }

        static async Task<IEnumerable<string>> GetLinksFromContent(CrawlingTask task)
        {
           await  
        }

        static void CreateLinks()
        {
            _contentEmulation["http://microsoft.com/"] = new[]
            {
                "http://microsoft.com/a.html",
                "http://microsoft.com/b.html"
            };
            _contentEmulation["http://microsoft.com/a.html"] = new[]
            {
                "http://microsoft.com/c.html",
                "http://microsoft.com/d.html"
            };
            _contentEmulation["http://microsoft.com/b.html"] = new[]
            {
                "http://microsoft.com/e.html"
            };

            _contentEmulation["http://google.com/"] = new[]
            {
                "http://google.com/a.html",
                "http://google.com/b.html"
            };
            _contentEmulation["http://google.com/a.html"] = new[]
            {
                "http://google.com/c.html",
                "http://google.com/d.html"
            };
            _contentEmulation["http://google.com/b.html"] = new[]
            {
                "http://google.com/e.html",
                "http://google.com/f.html"
            };
            _contentEmulation["http://google.com/c.html"] = new[]
            {
                "http://google.com/h.html",
                "http://google.com/i.html"
            };
        }
    }

    public class CrawlingTask
    {
        public string  UrlToCarwl { get; set; }
        public string  ProducerName { get; set; }
    }
}
