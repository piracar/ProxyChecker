using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyChecker
{
    
    

    public partial class Form1 : Form
    {
        private string _inputPatch;
        private string _outPatch = "d:/OutProxy.txt";
        private List<Proxy> _proxyList;
        private List<Proxy> _workProxyList = new List<Proxy>(); 
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

            string url = $"{"https://"}api.twitch.tv/api/channels/{textBoxChannelName.Text}/access_token.json";
            textBoxChannelName.Text = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (_proxyList != null)
                foreach (var proxy in _proxyList)
                {
                  WebProxy myproxy = new WebProxy(proxy.Adress, [your proxy port number]);
            myproxy.BypassProxyOnLocal = false;
            request.Proxy = myproxy;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                }
          

        }

        public bool IsConnectible(string url)
        {
            //Если возвращает говно  json, то идёт нахуй.
            //запрос к jsonу выше.
            return false;
        } 

        private void buttonInput_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        _inputPatch = ofd.FileName;
                        _proxyList = new List<Proxy>();
                        System.IO.File.ReadAllLines(_inputPatch).ToList().ForEach(x =>_proxyList.Add(new Proxy(x)));
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
        }
    }

    public class Proxy
    {
       public string Port { get; set; }
       public string Adress { get; set; }

        public Proxy(string proxyLineFromFile)
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
            return $"{Adress}:{Port}";
        }

    }
}
