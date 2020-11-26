namespace solution
{
    public interface IJsonRpcTransport
    {
        public Result Invoke<Result>(string method, object @params) where Result : class;
    }
}
