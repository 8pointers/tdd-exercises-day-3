using System.Net.Http;
using Moq;
using Moq.Contrib.HttpClient;
using NUnit.Framework;

namespace solution
{
    public class JsonRpcTransportTest
    {
        internal class SomeResult
        {
            public int someNumber { get; set; }
            public string someString { get; set; }
        }

        private const string Endpoint = "http://myendpoint.com/";
        private JsonRpcTransport _jsonRpcTransport = new JsonRpcTransport(Endpoint);
        private Mock<HttpMessageHandler> _handler;

        [SetUp]
        public void SetUp()
        {
            _handler = new Mock<HttpMessageHandler>();
            _jsonRpcTransport = new JsonRpcTransport(Endpoint, _handler.CreateClient());
        }

        [Test]
        public void ShouldBeAbleToInvokeMethodSuccessfully()
        {
            _handler.SetupRequest(
                    HttpMethod.Post,
                    Endpoint,
                    async request => (await request.Content.ReadAsStringAsync()) == @"{""jsonrpc"":""2.0"",""method"":""aMethod"",""params"":{""aNumber"":2,""aString"":""World""},""id"":0}"
                )
                .ReturnsResponse(@"{""jsonrpc"":""2.0"",""id"":0,""result"":{""someNumber"":12345,""someString"":""Hello World!""}}", "application/json");

            SomeResult result = _jsonRpcTransport.Invoke<SomeResult>("aMethod", new {aNumber = 2, aString = "World"});

            Assert.AreEqual(12345, result.someNumber);
            Assert.AreEqual("Hello World!", result.someString);
        }

        [Test]
        public void ShouldThrowRpcExceptionWhenErrorIsReturned()
        {
            _handler.SetupRequest(
                    HttpMethod.Post,
                    Endpoint,
                    async request => (await request.Content.ReadAsStringAsync()) == @"{""jsonrpc"":""2.0"",""method"":""aMethod"",""params"":{""aNumber"":2,""aString"":""World""},""id"":0}"
                )
                .ReturnsResponse(@"{""jsonrpc"":""2.0"",""id"":0,""error"":{""code"":12,""message"":""No can do!""}}", "application/json");

            var exceptionThrown = Assert.Throws<RpcErrorException>(() => _jsonRpcTransport.Invoke<SomeResult>("aMethod", new {aNumber = 2, aString = "World"}));

            Assert.AreEqual("No can do!", exceptionThrown.ErrorMessage);
        }
    }
}
