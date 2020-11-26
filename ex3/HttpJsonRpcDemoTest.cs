using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HttpJsonRpc;
using NUnit.Framework;

namespace jsonrpc
{
    [JsonRpcClass("aService")]
    public class HttpJsonRpcDemoTest
    {
        private const string Endpoint = "http://127.0.0.1:5678/";

        [JsonRpcMethod]
        public static Task aMethod(int aParameter)
        {
            return Task.FromResult(aParameter + 1);
        }

        [JsonRpcMethod]
        public static Task aBadMethod(int aParameter)
        {
            throw new Exception("We have a problem!");
        }

        [SetUp]
        public void SetUp()
        {
            JsonRpc.Start(Endpoint);
        }

        [TearDown]
        public void TearDown()
        {
            JsonRpc.Stop();
        }

        [Test]
        public async Task Should_make_sure_that_our_mini_rpc_server_is_working_correctly()
        {
            var httpClient = new HttpClient();
            var httpContent =
                new StringContent(
                    @"{""jsonrpc"":""2.0"",""method"":""aService.aMethod"",""params"":{""aParameter"":123},""id"":0}");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PostAsync(Endpoint, httpContent);
            var content = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("{\n  \"jsonrpc\": \"2.0\",\n  \"id\": 0,\n  \"result\": 124\n}", content);
        }
    }
}
