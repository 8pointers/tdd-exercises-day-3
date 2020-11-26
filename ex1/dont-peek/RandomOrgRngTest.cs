using Moq;
using NUnit.Framework;

namespace solution
{
    internal class RandomOrgRngTest
    {
        private Mock<IJsonRpcTransport> stubJsonRpcTransport;
        private RandomOrgRng _randomOrgRng;

        [SetUp]
        public void SetUp()
        {
            stubJsonRpcTransport = new Mock<IJsonRpcTransport>();
            _randomOrgRng = new RandomOrgRng(stubJsonRpcTransport.Object, "some-api-key");
        }

        [Test]
        public void Should_be_able_to_get_three_random_integers_between_0_and_100()
        {
            stubJsonRpcTransport.Setup(
                transport => transport.Invoke<Random>(
                    "generateIntegers",
                    new {apiKey="some-api-key", n = 3, min = 0, max = 100, replacement = true}
                )
            ).Returns(new Random(new Result(new []{10, 20, 30})));

            Assert.AreEqual(new []{10, 20, 30}, _randomOrgRng.GetInts(3, 0, 100));
        }

        [Test]
        public void Should_propagate_rpc_errors()
        {
            var expectedError = new RpcErrorException(200, "Invalid params");
            stubJsonRpcTransport.Setup(
                transport => transport.Invoke<Random>(
                    "generateIntegers",
                    new {apiKey="some-api-key", n = 3, min = 0, max = 100, replacement = true}
                )
            ).Throws(expectedError);

            var receivedError = Assert.Throws<RpcErrorException>(() => _randomOrgRng.GetInts(3, 0, 100));
            Assert.AreEqual(expectedError, receivedError);
        }
    }
}
