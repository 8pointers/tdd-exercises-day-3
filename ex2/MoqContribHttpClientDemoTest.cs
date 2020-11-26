using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Contrib.HttpClient;
using NUnit.Framework;

namespace jsonrpc
{
    public class MoqContribHttpClientDemoTest
    {
        [Test]
        public async Task Should_understand_how_to_use_Moq_to_stub_all_get_requests()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupRequest(HttpMethod.Get, "https://www.google.com/")
                .ReturnsResponse("<html><body>Hello World</body></html>");
            var httpClient = handler.CreateClient();

            var response = await httpClient.GetAsync("https://www.google.com");
            var content = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("<html><body>Hello World</body></html>", content);
        }

        [Test]
        public async Task Should_understand_how_to_use_Moq_to_stub_matching_post_requests()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupRequest(
                    HttpMethod.Post,
                    "https://www.google.com/",
                    async request => (await request.Content.ReadAsStringAsync()) == "PING"
                )
                .ReturnsResponse("PONG");
            handler.SetupRequest(
                    HttpMethod.Post,
                    "https://www.google.com/",
                    async request => (await request.Content.ReadAsStringAsync()) == "PONG"
                )
                .ReturnsResponse("PING");
            var httpClient = handler.CreateClient();

            var response1 = await httpClient.PostAsync("https://www.google.com", new StringContent("PING"));
            var content1 = await response1.Content.ReadAsStringAsync();
            Assert.AreEqual("PONG", content1);

            var response2 = await httpClient.PostAsync("https://www.google.com", new StringContent("PONG"));
            var content2 = await response2.Content.ReadAsStringAsync();
            Assert.AreEqual("PING", content2);
        }
    }
}
