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
     class Checker
    {
       
         public Checker(List<string> logger, List<UserProxy> proxies, string uri)
         {
             Logger = logger;
             Proxies = proxies;
             this.uri = uri;
         }

         //    static readonly Uri[] Uris = Enumerable.Range(0, 250).Select(_ => new Uri("http://ya.ru/")).ToArray();
        //    static readonly ConcurrentQueue<Uri> Queue = new ConcurrentQueue<Uri>();
        public List<string> Logger { get; set; }
        public List<UserProxy> Proxies { get; set; }
        public string uri { get; set; }
        public ConcurrentQueue<UserProxy> ProxiesQueue;
        private List<UserProxy> _workProxies =  new List<UserProxy>();

        public List<UserProxy>  Exec(string name, Action<Action> run)
        {
            var watch = Stopwatch.StartNew();

            var completed = new ManualResetEvent(false);
            run(() => completed.Set());
            completed.WaitOne();

            watch.Stop();
            return _workProxies;
            //Console.WriteLine("{0} took {1} ms", name, watch.ElapsedMilliseconds);
        }

         public ProxyCallBack GetStats()
         {
            return new ProxyCallBack(_workProxies,Logger);
         }
        public void DoWorkAsync_proxy(Action whenDone)
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
                    LoadUriAsync_proxyes(proxy, Success_proxy, Failure_proxy, handler);
                }

                completed.WaitOne();
                whenDone();
            }
            catch (Exception)
            {
                whenDone();
            }
        }

        private void LoadUriAsync_proxyes(UserProxy proxy, Action<UserProxy, string> success, Action<UserProxy, Exception> failure, Action completed)
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
                            success(proxy, headers + dataContent);
                        }
                    }
                }
                catch (Exception e)
                {
                    failure(proxy,e);
                }
                finally
                {
                    completed();
                }
            }, null);
        }

        private HttpWebRequest CreateRequest(UserProxy proxy)
        {
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Proxy = new WebProxy(proxy.ToString());
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = false;
            request.Timeout = 500;
            request.UserAgent =
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.9.0.19) Gecko/2010031422 Firefox/3.0.19";
            return request;
        }

        private void Success_proxy(UserProxy proxy, string content)
        {
            _workProxies.Add(proxy);           
            Logger.Add($"{content} with proxy:{proxy.ToFile()}");
        }

        private void Failure_proxy(UserProxy proxy,Exception e)
        {
            Logger.Add($"{e.Message} with proxy:{proxy.ToFile()}");
        }
    }
}

class ProxyCallBack
{
    public List<UserProxy> Proxies { get; set; }
    public List<string> Logger { get; set; }

    public ProxyCallBack(List<UserProxy> proxies, List<string> logger)
    {
        this.Proxies = proxies;
        this.Logger = logger;
    }
}