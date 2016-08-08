using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using ProxyChecker;

namespace ProxyChecker
{
    public class Checker
    {
       
         public Checker(List<string> logger, List<UserProxy> proxies, string uri)
         {
           Proxies = proxies;
             Uri = uri;
         }

         //    static readonly Uri[] Uris = Enumerable.Range(0, 250).Select(_ => new Uri("http://ya.ru/")).ToArray();
        //    static readonly ConcurrentQueue<Uri> Queue = new ConcurrentQueue<Uri>();
       // public List<string> Logger { get; set; }
        public static List<UserProxy> Proxies { get; set; }
        public static string Uri { get; set; }
        public ConcurrentQueue<UserProxy> ProxiesQueue;
        //private List<UserProxy> _workProxies =  new List<UserProxy>();

        public bool  Exec(string name, Action<Action,IProgress<string>,IProgress<UserProxy>> run, IProgress<string> logger, IProgress<UserProxy> workProxyes)
        {
            var watch = Stopwatch.StartNew();

            var completed = new ManualResetEvent(false);
            run(() => completed.Set(),logger,workProxyes);
            //DoWorkAsync_proxy();
            completed.WaitOne();

            watch.Stop();
            return true;
            //Console.WriteLine("{0} took {1} ms", name, watch.ElapsedMilliseconds);
        }
        public static void DoWorkAsync_proxy(Action whenDone, IProgress<string> logger, IProgress<UserProxy> workProxyes)
        {
            var syncRoot = new object();
            var counter = Proxies.Count;
            var completed = new ManualResetEvent(false);
            
            Action handler = () =>
            {
                lock (syncRoot)
                {
                    counter--;
                    if (counter > 0)
                    {
                        return;
                    }
                }

                completed.Set();
            };

            try
            {
                foreach (var proxy in Proxies)
                {
                    LoadUriAsync_proxyes(proxy, Success_proxy, Failure_proxy, handler,logger,workProxyes);
                }

                completed.WaitOne();
                whenDone();
            }
            catch (Exception)
            {
                whenDone();
            }
        }

        private static void LoadUriAsync_proxyes(UserProxy proxy, Action<UserProxy, string, IProgress<string>, IProgress<UserProxy>> success, Action<UserProxy, Exception, IProgress<string>> failure, Action completed, IProgress<string> logger, IProgress<UserProxy> workProxyes)
        {
            var request = CreateRequest(proxy);
            request.BeginGetResponse(asyncResult =>
            {
                try
                {
                    using (var response = (HttpWebResponse) request.EndGetResponse(asyncResult))
                    {
                        response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                        using (var stream = response.GetResponseStream())
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding(1251)))
                        {
                            var dataContent = reader.ReadToEnd();
                            var headers = response.Headers.ToString();
                            success(proxy, headers + dataContent,logger,workProxyes);
                        }
                    }
                }
                catch (Exception e)
                {
                    failure(proxy,e,logger);
                }
                finally
                {
                    completed();
                }
            }, null);
        }

        private static HttpWebRequest CreateRequest(UserProxy proxy)
        {
            
            var request = (HttpWebRequest) WebRequest.Create(Uri);
            request.Proxy = new WebProxy(proxy.ToString());
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = false;
            request.Timeout = 500;
            request.UserAgent =
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.9.0.19) Gecko/2010031422 Firefox/3.0.19";
            return request;
        }

        private static void Success_proxy(UserProxy proxy, string content,IProgress<string> logger, IProgress<UserProxy> workProxyes)
        {
            //_workProxies.Add(proxy);           
            //Logger.Add($"{content} with proxy:{proxy.ToFile()}");
            workProxyes.Report(proxy);
            logger.Report($"{content} with proxy:{proxy.ToFile()}");
        }

        private static void Failure_proxy(UserProxy proxy,Exception e,IProgress<string> logger )
        {
        //    Logger.Add($"{e.Message} with proxy:{proxy.ToFile()}");
            logger.Report($"{e.Message} with proxy:{proxy.ToFile()}");
        }
}
}
