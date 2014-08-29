using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace Mvc.NUnit.Testing
{
    public class MockControllerContextFactory
    {
        public static Mock<ControllerContext> FactoryMethod(MockControllerContextConfig config)
        {
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpRequest.SetupGet(m => m.Url).Returns(new Uri(config.Url));
            mockHttpRequest.SetupGet(x => x.ServerVariables).Returns(config.ServerVariables);
            mockHttpRequest.SetupGet(x => x.ApplicationPath).Returns(config.ApplicationPath);
            mockHttpRequest.SetupGet(x => x.Cookies).Returns(config.Cookies);

            // http://stackoverflow.com/questions/1367616/asp-net-mvc-mock-controller-url-action
            var mockHttpResponse = new Mock<HttpResponseBase>();
            mockHttpResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns("This is a URL");
            mockHttpResponse.Setup(m => m.AppendCookie(It.IsAny<HttpCookie>()));
            mockHttpResponse.Setup(m => m.SetCookie(It.IsAny<HttpCookie>()));

            var mockSession = new Mock<HttpSessionStateBase>();

            var mockHttpServer = new Mock<HttpServerUtilityBase>();

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.Setup(c => c.Response).Returns(mockHttpResponse.Object);
            mockHttpContext.Setup(c => c.Server).Returns(mockHttpServer.Object);
            mockHttpContext.Setup(c => c.Session).Returns(mockSession.Object);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(m => m.HttpContext).Returns(mockHttpContext.Object);
            mockControllerContext.Setup(cts => cts.HttpContext.Request).Returns(mockHttpRequest.Object);
            mockControllerContext.Setup(cts => cts.HttpContext.Response).Returns(mockHttpResponse.Object);
            mockControllerContext.Setup(cts => cts.HttpContext.Session).Returns(mockSession.Object);
            mockControllerContext.Setup(cts => cts.HttpContext.Server).Returns(mockHttpServer.Object);

            return mockControllerContext;
        }
        
    }
}
