using System;
using System.Collections.Specialized;
using System.Web;
using Moq;

namespace LeadManagement.Tests
{
    public class HttpContextTestHelper
    {
        public const string BaseRequestUrl = "http://unittest.example.com/";
        
        public static HttpContextBase FakeHttpContext(string requestUrl = null)
        {
            var request = MockHttpRequest(requestUrl);
            var response = FakeHttpResponse();
            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response);
            context.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);
            return context.Object;
        }

        public static Mock<HttpRequestBase> MockHttpRequest(string requestUrl = null, string userAgent = null)
        {
            Uri absoluteUri;
            string relativeUrl;

            if (string.IsNullOrWhiteSpace(requestUrl))
            {
                relativeUrl = "~/";
                absoluteUri = new Uri(BaseRequestUrl, UriKind.Absolute);
            }
            else if (requestUrl.StartsWith("~/"))
            {
                relativeUrl = requestUrl;
                absoluteUri = new Uri(requestUrl.Replace("~/", BaseRequestUrl), UriKind.Absolute);
            }
            else
            {
                absoluteUri = new Uri(requestUrl, UriKind.Absolute);
                relativeUrl = "~/" + absoluteUri.AbsolutePath;
            }

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.IsSecureConnection).Returns(absoluteUri.Scheme.Equals("https", StringComparison.InvariantCultureIgnoreCase));
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.AppRelativeCurrentExecutionFilePath).Returns(relativeUrl);
            request.Setup(x => x.PathInfo).Returns(string.Empty);
            request.SetupGet(x => x.Url).Returns(absoluteUri);
            request.SetupGet(x => x.ServerVariables).Returns(new NameValueCollection());
            request.SetupGet(x => x.Headers).Returns(new NameValueCollection());
            request.SetupGet(x => x.Form).Returns(new NameValueCollection());
            request.SetupGet(x => x.QueryString).Returns(new NameValueCollection());
            request.SetupGet(x => x.Cookies).Returns(new HttpCookieCollection());
            request.SetupGet(p => p[It.IsAny<string>()]).Returns(string.Empty);

            return request;
        }

        public static HttpResponseBase FakeHttpResponse()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string url) => url);
            return response.Object;
        }
    }
}
