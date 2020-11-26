# JSON RPC Client

Your task is to test-drive the implementation of the JsonRpcClient class. The class should implement IJsonRpcClient interface (inferred in the previous exercise) in a way that it makes it possible to make JSON RPC calls to remote API endpoints.

To accomplish that, use Moq.Contrib.HttpClient extension for Moq. The _MoqContribHttpClientDemoTest.cs_ contains a usage example. More detailed documentation can be found here:

https://github.com/maxkagamine/Moq.Contrib.HttpClient

Make sure your class implements both the "happy-path" and the failure scenarios.
