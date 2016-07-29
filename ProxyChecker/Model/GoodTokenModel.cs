using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyChecker.Model
{
    class GoodTokenModel
    {
        public class Geo
        {
            public string continent_code { get; set; }
            public string country_code { get; set; }
        }

        public class Chansub
        {
            public int view_until { get; set; }
            public List<object> restricted_bitrates { get; set; }
        }

        public class Private
        {
            public bool allowed_to_view { get; set; }
        }

        public class Token
        {
            public Geo geo { get; set; }
            public int user_id { get; set; }
            public string channel { get; set; }
            public int expires { get; set; }
            public Chansub chansub { get; set; }
            public Private @private { get; set; }
            public object privileged { get; set; }
            public bool source_restricted { get; set; }
            public bool https_required { get; set; }
            public bool show_ads { get; set; }
            public string device_id { get; set; }
        }

        public class RootObject
        {
            public Token token { get; set; }
            public string sig { get; set; }
            public bool mobile_restricted { get; set; }
        }
    }
}
