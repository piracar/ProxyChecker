using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProxyChecker;

namespace ProxyChecker
{
    public class Checker
    {
       
         public Checker(List<UserProxy> proxies, string uri)
         {
           Proxies = proxies;
             Uri = uri;
         }
        
        public static List<UserProxy> Proxies { get; set; }
        public static string Uri { get; set; }
        public ConcurrentQueue<UserProxy> ProxiesQueue;
       
        public void  Exec(string name,  IProgress<string> logger, IProgress<UserProxy> workProxyes)
        {
          
            DoWorkAsync_proxy(logger,workProxyes);
            
            
        }
        public static  void DoWorkAsync_proxy(/*Action whenDone,*/ IProgress<string> logger, IProgress<UserProxy> workProxyes)
        {
           try
            {
              
                foreach (var proxy in Proxies)
                {
                  //  Thread.Sleep(10);
                    LoadUriAsycnNew_proxy(proxy, Success_proxy, Failure_proxy, /*handler,*/logger,workProxyes);
                }
            }
           catch (Exception)
           {
               // ignored
           }
        }
        
        private static async void LoadUriAsycnNew_proxy(UserProxy proxy,
            Action<UserProxy, string, IProgress<string>, IProgress<UserProxy>> success,
            Action<UserProxy, Exception, IProgress<string>> failure, /*Action completed,*/ IProgress<string> logger,
            IProgress<UserProxy> workProxyes)
        {
            try
            {
                
                using (HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                   
                    Proxy = new WebProxy(proxy.ToString(), false),
                    UseProxy = true
                })
                {
                    using (
                        HttpClient httpClient = new HttpClient(httpClientHandler)
                        {
                            Timeout = new TimeSpan(0, 0, 5, 10)
                        })
                    {
                        using (HttpResponseMessage response = await httpClient.GetAsync(Uri))
                        {
                            using (HttpContent content = response.Content)
                            {
                                var headers = content.Headers.ToString();
                                var x = response.Headers.Connection.ToString();
                                if (response.IsSuccessStatusCode && x == "keep-alive")
                                    success(proxy, headers + "", logger, workProxyes);
                                else
                                    throw new Exception();
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                failure(proxy, e, logger);
            }
            finally
            {
               
            }
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
