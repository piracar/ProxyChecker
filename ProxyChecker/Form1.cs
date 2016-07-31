using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyChecker
{
    
    

    public partial class Form1 : Form
    {
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

            string url = $"{"http://"}api.twitch.tv/api/channels/{textBoxChannelName.Text}/access_token.json";
            //textBoxChannelName.Text = url;
           
            if (_proxyList != null)
                foreach (var proxy in _proxyList)
                {
                   GetResponse(url,proxy);
                    //_workProxyList.Add(proxy);
                    //listBox1.Items.Add(proxy.ToString());
                    //listBox3.Items.Add($"Proxy Work! {proxy.ToString()}");
                }
          

        }

        public void IsConnectible(string url, UserProxy userProxy)
        {
            #region sinchronus
            //_webRequest = (HttpWebRequest)WebRequest.Create(url);
            //    WebProxy myproxy = new WebProxy(UserProxy.ToString()) {BypassProxyOnLocal = false};
            //_webRequest.UserProxy = myproxy;
            //_webRequest.Method = "GET";
            //try
            //{
            //    HttpWebResponse response = (HttpWebResponse)_webRequest.GetResponse();
            //    return true;
            //    //_workProxyList.Add(UserProxy);
            //    //listBox1.Items.Add(UserProxy.ToString());
            //    //listBox3.Items.Add($"UserProxy Work! {UserProxy.ToString()}");

            //}
            //catch (Exception e)
            //{
            //    listBox3.Items.Add($"{e.Message} with {UserProxy.ToString()}");
            //    return false;
            //}
            #endregion
            #region Assync

           

#endregion

        }

        async  void GetResponse(string url, UserProxy userProxy)
        {
            try
            {
              using (HttpClientHandler httpClientHandler = new HttpClientHandler()
                                                    { Proxy = new WebProxy(userProxy.ToString(),false), UseProxy = true})
                        {
                            using (HttpClient httpClient = new HttpClient(httpClientHandler))
                            {
                                using (HttpResponseMessage response = await httpClient.GetAsync(url))
                                {
                                    using (HttpContent content = response.Content)
                                    {
                                        string message = await content.ReadAsStringAsync();
                                        listBox3.Items.Add(message);
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

        private void buttonInput_Click(object sender, EventArgs e)
        {
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
            //  data.ForEach(Console.WriteLine);
            System.IO.File.WriteAllText(_outPatch, String.Join("\n",_workProxyList.Select(x=>x.ToString())));
        }

        private void buttonAddViewers_Click(object sender, EventArgs e)
        {
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
        }
    }

    public class UserProxy
    {
       public string Port { get; set; }
       public string Adress { get; set; }

        public UserProxy(string proxyLineFromFile)
        {
            try
            {
                //var bar = proxyLineFromFile.Split(new char[] {'\n'}, 100, StringSplitOptions.RemoveEmptyEntries);
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

    }
}
