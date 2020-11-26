namespace solution
{
    internal class Result
    {
        public int[] data { get; set; }

        public Result(int[] data)
        {
            this.data = data;
        }
    }

    internal class Random
    {
        public Result random { get; set; }

        public Random(Result random)
        {
            this.random = random;
        }
    }

    public class RandomOrgRng
    {
        private readonly IJsonRpcTransport _jsonRpcTransport;
        private readonly string _apiKey;

        public RandomOrgRng(IJsonRpcTransport jsonRpcTransport, string apiKey)
        {
            _jsonRpcTransport = jsonRpcTransport;
            _apiKey = apiKey;
        }

        public int[] GetInts(int n, int min, int max)
        {
            var result = _jsonRpcTransport.Invoke<Random>("generateIntegers",
                new {apiKey = _apiKey, n, min, max, replacement = true});
            return result.random.data;
        }
    }
}
