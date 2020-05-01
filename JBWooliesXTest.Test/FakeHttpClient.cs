using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace JBWooliesXTest.Test
{
    static class FakeHttpClient
    {
        public static HttpClient Create_WithContent(string content)
        {
            var httpClient = new HttpClient(CreateFakeHandler(content))
            {
                BaseAddress = new Uri("http://fake")
            };
            return httpClient;
        }

        private static HttpMessageHandler CreateFakeHandler(string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                });
            return handlerMock.Object;
        }
    }
}
