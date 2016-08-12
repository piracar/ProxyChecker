#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace ProxyChecker
{
    public partial class Form1 : Form
    {
       // private readonly List<string> _logger = new List<string>();
        private readonly List<UserProxy> _notBanProxyList = new List<UserProxy>();
       // private string _inputPatch;
       // private string _outPatch = "d:/OutProxy.txt";
        private List<UserProxy> _proxyList = new List<UserProxy>();
        private WebRequest _webRequest;
        //private int _proxyChecked = 0;
        private bool _windsUp = true;
        private List<UserProxy> _workProxyList = new List<UserProxy>();

        public Form1(/*string outPatch, string inputPatch*/)
        {
            //_outPatch = outPatch;
            //_inputPatch = inputPatch;
            InitializeComponent();
            label3.Text = $"Рабочих: 0";
        }

        //ButtonStart
        private void button1_Click(object sender, EventArgs e)
        {
            string url =
                $"https://www-cdn.jtvnw.net/swflibs/TwitchPlayer.r1805fdf8cec14e5e658b83faaf6f985233b9432e.swf?channel={textBoxChannelName.Text}&amp;playerType=faceboo";
            // _proxyChecked = 0;
            var proxygood = 0;
            var _proxyChecked = 0;
            if (_proxyList != null)
            {
                var progressLog = new Progress<string>(s =>
                {
                    listBox3.Items.Add(s);
                    label2.Text = $"Пришло {_proxyChecked++.ToString()}ответов и ошибок";
                });
                var progressProxy = new Progress<UserProxy>(s =>
                {
                    label3.Text = proxygood++.ToString();
                    _workProxyList.Add(s);
                    listBox2.Items.Add(s);
                });
                StartRequest(progressLog, progressProxy, new Checker(_proxyList, url));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var i = 0;
            string url = $"http://api.twitch.tv/api/channels/{textBoxChannelName.Text}/access_token.json";
            if (_workProxyList != null)
            {
                var progressLog = new Progress<string>(s =>
                {
                    //_logger.Add(s);
                    listBox3.Items.Add(s);
                });
                var progressProxy = new Progress<UserProxy>(s =>
                {
                    _notBanProxyList.Add(s);
                    listBox4.Items.Add(s);
                    label6.Text = $"Пришло {i++}";
                });
                StartRequest(progressLog, progressProxy, new Checker(_workProxyList, url));
            }
        }


        public void StartRequest(Progress<string> progressLog, Progress<UserProxy> progressProxy, Checker checker)
        {
            Task.Factory.StartNew(() =>
                checker.Exec("Kappa", progressLog, progressProxy), TaskCreationOptions.LongRunning);
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            _proxyList?.Clear();
            listBox1.Items?.Clear();
            try
            {
                var patch = givePatch();
                if (patch != "")
                {
                    _proxyList = new List<UserProxy>();
                    File.ReadAllLines(patch).ToList().ForEach(x => _proxyList.Add(new UserProxy(x)));
                    if (_proxyList.Count > 500)
                        for (var i = 0; i < 499; i++)
                            listBox1.Items.Add(_proxyList[i].ToString());
                    else
                        _proxyList.ForEach(x => listBox1.Items.Add(x.ToString()));
                    label1.Text = $"Загрузилось: {_proxyList.Count}";
                }
            }

            catch (
                Exception exception
                )
            {
                listBox3.Items.Add(exception.Message + "  \n ProxyInputFail");
            }
        }

        private string givePatch()
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileName;
                }
                return "";
            }
        }


        private void buttonOutput_Click(object sender, EventArgs e)
        {
            var patch = givePatch();
            if (patch != "")
            {
                WriteInFile(patch, _workProxyList);
            }
         }

        private void WriteInFile(string patch, List<UserProxy> proxyList)
        {
            var task = new Task(() =>
            {
                using (var sw = new StreamWriter(patch))
                {
                    var workProxyToFile = proxyList;
                    foreach (var userProxy in workProxyToFile)
                    {
                        sw.WriteLine(userProxy.ToFile());
                    }
                }
            });

            task.Start();
        }

        #region oldCode

        private void buttonAddViewers_Click(object sender, EventArgs e)
        {
            //#region coseFuckYou

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

            //#endregion

            //#region MyVersion

//            var nowChecked = _workProxyList;
//            WindsUp = true;
//            var uri = $"https://api.twitch.tv/api/channels/{textBoxChannelName}?as3=t&oauth_token=3vxrmk0h14vkgbai50bpiautpbbek52";
//            //new Thread(() => { 

//                try
//                {


//                  //  while (WindsUp)
//                    {
//                        foreach (var userProxy in nowChecked)
//                        {
//                            UpThemViewers(userProxy, uri);
//                        }
//                        listBox3.Items.Add("SLEEPING");
//                        Thread.Sleep(5000);
//                    }
//                }
//                catch (Exception exception)
//                {
//                    listBox3.Items.Add($"{exception.Message}");
//                }


////            });

//            #endregion
//        }
        }

        //private async bool GetResponse2(string url, UserProxy userProxy)
        //{
        //    try
        //    {
        //        using (HttpClientHandler httpClientHandler = new HttpClientHandler()
        //        { Proxy = new WebProxy(userProxy.ToString(), false), UseProxy = true })
        //        {
        //            using (HttpClient httpClient = new HttpClient(httpClientHandler))
        //            {
        //                using (HttpResponseMessage response = await httpClient.GetAsync(url))
        //                {
        //                    using (HttpContent content = response.Content)
        //                    {
        //                        string message = await content.ReadAsStringAsync();

        //                        listBox3.Items.Add(message);
        //                        // labelProxyCheckerd.Text = $"{proxyChecked++}";
        //                        if (response.IsSuccessStatusCode)
        //                        {
        //                            //    lapp += $"{message}; \n";
        //                            //    textBoxChannelName.Text = lapp;
        //                            listBox2.Items.Add(userProxy);
        //                            _workProxyList.Add(userProxy);
        //                            label3.Text = $"Рабочих: {_workProxyList.Count}";
        //                            return true;
        //                        }
        //                        return false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        listBox3.Items.Add($"Trouble with proxy{userProxy.ToString()} {exception.Message}");
        //        labelProxyCheckerd.Text = $"{proxyChecked++}";
        //    }


        //    return false;
        //}

        //private async Task<string> CreateMultipleTasksAsync(string url, UserProxy userProxy)
        //{
        //    using (
        //        HttpClientHandler httpClientHandler = new HttpClientHandler()
        //        {
        //            Proxy = new WebProxy(userProxy.ToString(), false),
        //            UseProxy = true
        //        })
        //    {
        //        try
        //        {
        //            HttpClient client = new HttpClient(httpClientHandler);
        //            Task<bool> isSuccessTask = ProcessURLAsync(url, client);
        //            bool isGoodProxy = await isSuccessTask;
        //            if (isGoodProxy)
        //            {
        //                _workProxyList.Add(userProxy);
        //                return userProxy.ToFile();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return "";
        //        }

        //        return "";
        //    }

        //}
        //async Task<bool> ProcessURLAsync(string url, HttpClient client)
        //{

        //    var returnString = await client.GetAsync(url);
        //    //DisplayResults(url, byteArray);
        //    if (returnString.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<HttpResponseMessage> Request( UserProxy proxy,string uri)
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
        //            return await client.GetAsync(uri);

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

        //private async void GetResponse(string url, UserProxy userProxy)
        //{
        //    try
        //    {
        //        using (HttpClientHandler httpClientHandler = new HttpClientHandler()
        //        {Proxy = new WebProxy(userProxy.ToString(), false), UseProxy = true})
        //        {
        //            using (HttpClient httpClient = new HttpClient(httpClientHandler))
        //            {
        //                using (HttpResponseMessage response = await httpClient.GetAsync(url))
        //                {
        //                    using (HttpContent content = response.Content)
        //                    {
        //                        string message = await content.ReadAsStringAsync();

        //                        listBox3.Items.Add(message);
        //                       // labelProxyCheckerd.Text = $"{proxyChecked++}";
        //                        if (response.IsSuccessStatusCode)
        //                        {
        //                        //    lapp += $"{message}; \n";
        //                        //    textBoxChannelName.Text = lapp;
        //                            listBox2.Items.Add(userProxy);
        //                            _workProxyList.Add(userProxy);
        //                            label3.Text = $"Рабочих: {_workProxyList.Count}";
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        listBox3.Items.Add($"Trouble with proxy{userProxy.ToString()} {exception.Message}");
        //        labelProxyCheckerd.Text = $"{proxyChecked++}";
        //    }


        //}

        //private async void UpThemViewers(UserProxy userProxy, string url)
        //{
        //    try
        //    {
        //        using (
        //            HttpClientHandler httpClientHandler = new HttpClientHandler()
        //            {
        //                // Proxy = new WebProxy(userProxy.ToString(), false),
        //                // UseProxy = true
        //            })
        //        {
        //            using (HttpClient httpClient = new HttpClient(httpClientHandler))
        //            {
        //                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
        //                {
        //                    request.Headers.Add("Connection", "keep-alive");
        //                    request.Headers.Add("User - Agent",
        //                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
        //                    request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        //                    request.Headers.Host = "api.twitch.tv";
        //                    request.Headers.Add("If-None-Match", "da8c62af533b83107bc205a550bc650c");
        //                    using (HttpResponseMessage response = await httpClient.SendAsync(request))
        //                    {

        //                        using (HttpContent content = response.Content)
        //                        {
        //                            var responseString = await content.ReadAsStringAsync();
        //                            listBox3.Items.Add($"{responseString}");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        listBox3.Items.Add($"Exeption {exception.Message} with proxy{userProxy.ToString()}  ");


        //    }
        //}

        #endregion

        private void buttonStopAddWiewers_Click(object sender, EventArgs e)
        {
            _windsUp = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _workProxyList?.Clear();
            listBox2.Items?.Clear();
            try
            {
                var patch = givePatch();
                if (patch != "")
                {
                    _workProxyList = new List<UserProxy>();
                    File.ReadAllLines(patch).ToList().ForEach(x => _workProxyList.Add(new UserProxy(x)));
                    _workProxyList.ForEach(x => listBox2.Items.Add(x.ToString()));
                    label3.Text = $"Загрузилось: {_workProxyList.Count}";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message + "  \n ProxyInputFail");
            }
            GC.Collect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            _proxyList.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            _workProxyList.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var task = new Task(() =>
            {
                string url =
                    $"https://www-cdn.jtvnw.net/swflibs/TwitchPlayer.r1805fdf8cec14e5e658b83faaf6f985233b9432e.swf?channel={textBoxChannelName.Text}&amp;playerType=faceboo";
            });
            task.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var patch = givePatch();
            if (patch != "")
            {
                WriteInFile(patch, _notBanProxyList);
            }
        }

        private void showLogButton_Click(object sender, EventArgs e)
        {
            GC.Collect();
            listBox3.Visible = !listBox3.Visible;
            if (listBox3.Visible)
            {
                showLogButton.Text = "Убрать лог";
            }
            else
            {
                showLogButton.Text = "Показать лог";
            }
        }

        //private void button9_Click(object sender, EventArgs e)
        //{
        //  Task task1= new Task(() =>
        //  {
        //      try
        //    {
        //        OpenFileDialog ofd = new OpenFileDialog();
        //        if (ofd.ShowDialog() == DialogResult.OK)
        //        {
        //            _inputPatch = ofd.FileName;
        //             System.IO.File.ReadAllLines(_inputPatch).ToList().ForEach(x => _proxyList.Add(new UserProxy(x)));
        //            _proxyList.ForEach(x => listBox1.Items.Add(x.ToString()));
        //            label1.Text = $"Загрузилось: {_proxyList.Count}";
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        listBox3.Items.Add(exception.Message + "  \n ProxyInputFail");
        //    }
        //  });
        //    task1.RunSynchronously();
        //    GC.Collect();

        //}
    }
}