using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace solution
{
    class Error
    {
        public int code { get; set; }
        public string message { get; set; }
    }
    class Response<T> where T: class
    {
        public string jsonrpc { get; set; }
        public T? result { get; set; }
        public Error? error { get; set; }
        public int id { get; set; }
    }
    public class JsonRpcTransport
    {
        private readonly HttpClient _client;

        private string Endpoint { get; set;  }

        public JsonRpcTransport(string endpoint, HttpClient client = null)
        {
            Endpoint = endpoint;
            _client = client == null ? new HttpClient() : client;
        }

        public async Task<Result> InvokeAsync<Result>(string method, object @params) where Result: class
        {
            var content =
                new ByteArrayContent(
                    JsonSerializer.SerializeToUtf8Bytes(new {jsonrpc = "2.0", method, @params, id = 0}));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(Endpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var rpcResponse = JsonSerializer.Deserialize<Response<Result>>(responseContent);
            if (rpcResponse.error != null)
                throw new RpcErrorException(rpcResponse.error.code, rpcResponse.error.message);
            return rpcResponse.result;
        }

        public Result Invoke<Result>(string method, object @params) where Result: class
        {
            return InvokeAsync<Result>(method, @params).GetAwaiter().GetResult();
        }
    }
}
