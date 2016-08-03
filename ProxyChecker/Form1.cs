using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyChecker
{
    
    

    public partial class Form1 : Form
    {
        private int proxyChecked = 0;
        private bool WindsUp = true;
        private string _inputPatch;
        private string _outPatch = "d:/OutProxy.txt";
        private List<UserProxy> _proxyList;
        private List<UserProxy> _workProxyList = new List<UserProxy>();
        private WebRequest _webRequest;
        public Form1(string outPatch, string inputPatch)
        {
            _outPatch = outPatch;
            _inputPatch = inputPatch;
            InitializeComponent();
            label3.Text = $"Рабочих: 0";
        }
        //ButtonStart
        private void button1_Click(object sender, EventArgs e)
        {
            string url =
                $"https://www-cdn.jtvnw.net/swflibs/TwitchPlayer.r1805fdf8cec14e5e658b83faaf6f985233b9432e.swf?channel={textBoxChannelName.Text}&amp;playerType=faceboo";
            string url1 = $"http://api.twitch.tv/api/channels/{textBoxChannelName.Text}/access_token.json";
            //textBoxChannelName.Text = url;
            proxyChecked = 0;
            if (_proxyList != null)
            {
                foreach (var proxy in _proxyList)
                {
                    GetResponse(url, proxy);
                    //_workProxyList.Add(proxy);
                    //listBox1.Items.Add(proxy.ToString());
                    //istBox3.Items.Add($"Proxy Work! {proxy.ToString()}");
                }
            }
        }

        //public void StartRequest(List<UserProxy> proxyListRequest,string url)
        //{
        //    foreach (var proxy in proxyListRequest)
        //    {
        //        GetResponse2(url, proxy);
        //    }

        //}
        //Send request to twitch
      
        //Version 2 of GetResponse
        async void GetResponse2(string url, UserProxy userProxy)
        {
            try
            {
                using (HttpClientHandler httpClientHandler = new HttpClientHandler()
                { Proxy = new WebProxy(userProxy.ToString(), false), UseProxy = true })
                {
                    using (HttpClient httpClient = new HttpClient(httpClientHandler))
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", "api.twitch.tv");
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
                        //using (HttpResponseMessage response = await OptionsAsync(url,httpClient))
                        //{

                        //    using (HttpContent content = response.Content)
                        //    {
                        //        string message = await content.ReadAsStringAsync();
                        //        listBox3.Items.Add(message);
                        //        if (response.IsSuccessStatusCode)
                        //        {
                        //            listBox2.Items.Add(userProxy);
                        //            _workProxyList.Add(userProxy);
                        //            label3.Text = $"Рабочих: {_workProxyList.Count}";
                        //        }

                        //    }
                        //}
                    }
                }
            }
            catch (Exception exception)
            {
                listBox3.Items.Add($"Trouble with proxy{userProxy.ToString()} {exception.Message}");
            }


        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            _proxyList?.Clear();
            listBox1.Items?.Clear();
                try
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        _inputPatch = ofd.FileName;
                        _proxyList = new List<UserProxy>();
                        System.IO.File.ReadAllLines(_inputPatch).ToList().ForEach(x =>_proxyList.Add(new UserProxy(x)));
                        _proxyList.ForEach(x=>listBox1.Items.Add(x.ToString()));
                        label1.Text = $"Загрузилось: {_proxyList.Count}";
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message+"  \n ProxyInputFail");
                    throw;
                }
            }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _outPatch = ofd.FileName;
            }
            WriteInFile(_outPatch);
        }

        private void WriteInFile(string patch)
        {
            
            Task task = new Task(() =>
            {
                using (StreamWriter sw = new StreamWriter(patch))
                {
                    var _workProxyToFile = _workProxyList;
                    foreach (var userProxy in _workProxyToFile)
                    {
                        sw.WriteLine(userProxy.ToFile());
                    }
                }

            });
            
            task.Start();
        }

        private void buttonAddViewers_Click(object sender, EventArgs e)
        {
            #region coseFuckYou

            /*
              Dim request As HttpWebRequest = DirectCast(WebRequest.Create("https://api.twitch.tv/api/channels/" & ChannelName & "?as3=t&oauth_token=3vxrmk0h14vkgbai50bpiautpbbek52"), HttpWebRequest)
            Dim response As HttpWebResponse
            request.KeepAlive = True
            request.Method = "GET"
            request.Timeout = 2000
            request.UserAgent = "Mozilla/1.0 (Windows NT 6.1; WOW64) AppleWebKit/527.36 (KHTML, like Gecko) Chrome/12.0.1700.102 Safari/538.36"
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            Console.WriteLine(response)
            response.Close() 
             */
            //_webRequest = (HttpWebRequest)WebRequest.Create(url);
            //WebProxy myproxy = new WebProxy(proxy.ToString()) { BypassProxyOnLocal = false };
            //_webRequest.Proxy = myproxy;
            //_webRequest.Method = "GET";
            //try
            //{
            //    HttpWebResponse response = (HttpWebResponse)_webRequest.GetResponse();
            //    _logger.Items.Add($"Work! {proxy.ToString()}");
            //}
            //catch (Exception e)
            //{
            //    _logger.Items.Add($"{e.Message} with {proxy.ToString()}");
            //    return false;
            //}

            #endregion

            #region MyVersion

            var nowChecked = _workProxyList;
            WindsUp = true;
            var uri = $"https://api.twitch.tv/api/channels/{textBoxChannelName}?as3=t&oauth_token=3vxrmk0h14vkgbai50bpiautpbbek52";
            //new Thread(() => { 
            
                try
                {


                  //  while (WindsUp)
                    {
                        foreach (var userProxy in nowChecked)
                        {
                            UpThemViewers(userProxy, uri);
                        }
                        listBox3.Items.Add("SLEEPING");
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception exception)
                {
                    listBox3.Items.Add($"{exception.Message}");
                }


//            });

            #endregion
        }

        private async void GetResponse(string url, UserProxy userProxy)
        {
            try
            {
                using (HttpClientHandler httpClientHandler = new HttpClientHandler()
                {Proxy = new WebProxy(userProxy.ToString(), false), UseProxy = true})
                {
                    using (HttpClient httpClient = new HttpClient(httpClientHandler))
                    {

                        using (HttpResponseMessage response = await httpClient.GetAsync(url))
                        {

                            using (HttpContent content = response.Content)
                            {
                                string message = await content.ReadAsStringAsync();
                                listBox3.Items.Add(message);
                                labelProxyCheckerd.Text = $"{proxyChecked++}";
                                if (response.IsSuccessStatusCode)
                                {
                                    listBox2.Items.Add(userProxy);
                                    _workProxyList.Add(userProxy);
                                    label3.Text = $"Рабочих: {_workProxyList.Count}";
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                listBox3.Items.Add($"Trouble with proxy{userProxy.ToString()} {exception.Message}");
            }


        }

        private async void UpThemViewers(UserProxy userProxy, string url)
        {
            try
            {
                using (
                    HttpClientHandler httpClientHandler = new HttpClientHandler()
                    {
                        // Proxy = new WebProxy(userProxy.ToString(), false),
                        // UseProxy = true
                    })
                {
                    using (HttpClient httpClient = new HttpClient(httpClientHandler))
                    {
                        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                        {
                            request.Headers.Add("Connection", "keep-alive");
                            request.Headers.Add("User - Agent",
                                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
                            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                            request.Headers.Host = "api.twitch.tv";
                            request.Headers.Add("If-None-Match", "da8c62af533b83107bc205a550bc650c");
                            using (HttpResponseMessage response = await httpClient.SendAsync(request))
                            {
                              
                                using (HttpContent content = response.Content)
                                {
                                    var responseString = await content.ReadAsStringAsync();
                                    listBox3.Items.Add($"{responseString}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                listBox3.Items.Add($"Exeption {exception.Message} with proxy{userProxy.ToString()}  ");


            }
        }

        private void buttonStopAddWiewers_Click(object sender, EventArgs e)
        {
            WindsUp = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _workProxyList?.Clear();
            listBox2.Items?.Clear();
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _inputPatch = ofd.FileName;
                    _proxyList = new List<UserProxy>();
                    System.IO.File.ReadAllLines(_inputPatch).ToList().ForEach(x => _workProxyList.Add(new UserProxy(x)));
                    _workProxyList.ForEach(x => listBox2.Items.Add(x.ToString()));
                    label3.Text = $"Загрузилось: {_proxyList.Count}";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message + "  \n ProxyInputFail");
                throw;
            }
        }

        //public Task<HttpResponseMessage> OptionsAsync(string requestUri,HttpClient httpClient)
        //{
        //    return httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Options, requestUri));
        //} 

        //private async void AddViewers()
        //{
        //                 string url = $"http:/{"/"}player.twitch.tv/?channel={textBoxChannelName}&html5";
        //    try
        //    {
        //        using (HttpClientHandler httpClientHandler = new HttpClientHandler())
        //        {
        //            using (HttpClient httpClient = new HttpClient(httpClientHandler))
        //            {

        //                using (HttpResponseMessage response = await OptionsAsync(url,httpClient))
        //                {

        //                    using (HttpContent content = response.Content)
        //                    {
        //                        string message = await content.ReadAsStringAsync();
        //                        listBox3.Items.Add(message);


        //                    }
        //                }
        //            }
        //        }
        //    } 
        //    catch (Exception exception)
        //    {
        //        listBox3.Items.Add($"ADDVIVER MESSAGE {exception.Message}");
        //    }
        //}
    }


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

    }
}
