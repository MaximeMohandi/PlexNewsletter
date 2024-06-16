using System.Net;
using System.Text;
using Moq;
using Moq.Protected;

namespace MediahubNewsletter.Tests.Catalog;

public class MockHttpRequestHandler {

    public static HttpClient MockResponse(HttpStatusCode responseCode, string responseContent)
    {
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            });

        return new HttpClient(mockHttpMessageHandler.Object);
    }
}
