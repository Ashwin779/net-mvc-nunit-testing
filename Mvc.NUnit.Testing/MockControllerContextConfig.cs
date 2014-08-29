using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mvc.NUnit.Testing
{
    public class MockControllerContextConfig
    {
        public MockControllerContextConfig()
        {
            this.Url = "http://localhost";
            this.ApplicationPath = "/";
            this.Cookies = new HttpCookieCollection();
            this.ServerVariables = new NameValueCollection();

        }
        public string Url { get; set; }

        public NameValueCollection ServerVariables { get; set; }

        public string ApplicationPath { get; set; }

        public HttpCookieCollection Cookies { get; set; }
    }
}
