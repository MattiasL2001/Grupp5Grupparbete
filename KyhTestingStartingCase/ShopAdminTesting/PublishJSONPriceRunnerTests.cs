using MvcSuperShop;
using ShopAdmin;
using ShopGeneral.Data;

namespace ShopAdminTesting
{
    [TestClass]
    public class PublishJSONPriceRunnerTests
    {
        private readonly ShopAdmin.Commands.Product sut;
        public PublishJSONPriceRunnerTests() { sut = new PublishJSONPriceRunner(); }

        [TestMethod]
        public void ReturnJSON()
        {
            var p = new Product();
            var pString = $"id: {p.Id} \ntitle: {p.Name} \ndescription: ";
            Console.WriteLine(pString);
            Assert.AreEqual(0, 0);
        }
    }
}
