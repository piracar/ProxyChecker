using System;

namespace ProxyChecker
{
    public class UserProxy
    {
       
        public string Port { get; set; }
        public string Adress { get; set; }

        public UserProxy(string proxyLineFromFile)
        {
            try
            {
                var foo = proxyLineFromFile.Split(new char[] { ':' });
                Adress = foo[0];
                Port = foo[1];
            }
            catch (Exception exception) 
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            
        }

        public override string ToString()
        {
            return $"http://{Adress}:{Port}";
        }
        public string ToFile()
        {
            return $"{Adress}:{Port}";
        }

        //public async Task<HttpResponseMessage> Request(string url)
        //{
        //    using (
        //        HttpClientHandler httpClientHandler = new HttpClientHandler()
        //        {
        //            Proxy = new WebProxy(this.ToString(), false),
        //            UseProxy = true
        //        })
        //    {
        //        try
        //        {
        //            HttpClient client = new HttpClient(httpClientHandler);
        //            return await client.GetAsync(url);
        //            #region oldCode
        //            //DisplayResults(url, byteArray);
        //            //if (returnString.IsSuccessStatusCode)
        //            //{
        //            //    return true;
        //            //}
        //            //return false;
        //            //Task<bool> isSuccessTask = ProcessURLAsync(url, client);
        //            //bool isGoodProxy = await isSuccessTask;
        //            //if (isGoodProxy)
        //            //{
        //            //    _workProxyList.Add(userProxy);
        //            //    return userProxy.ToFile();
        //            //}
        //            #endregion
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }

        //    }
        //}

    }
}