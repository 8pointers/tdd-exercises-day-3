# JSON RPC Client

Your task is to use NUnit to write some integration tests for the JSON RPC Client you implemented.

To do that, use HttpJsonRpc package - it allows you to define JSON RPC quickly define and expose  methods that you can then invoke by your integration tests. The _HttpJsonRpcDemoTest.cs_ contains a usage example. More detailed documentation can be found here:

https://github.com/httpjsonrpcnet/httpjsonrpcnet

Make sure your class implements both the "happy-path" and the failure scenarios.

Once you get a working JsonRpcClient, write an end-to-end test for the RandomOrgRng.
