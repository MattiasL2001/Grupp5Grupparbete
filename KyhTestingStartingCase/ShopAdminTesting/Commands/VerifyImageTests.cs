using Microsoft.EntityFrameworkCore;
using ShopAdmin.Commands;
using ShopGeneral.Data;


namespace ShopAdminTesting.Commands
{
    [TestClass]
    public class VerifyImageTests
    {
        private readonly Verifyimage sut;
        public VerifyImageTests()
        {            
            sut = new Verifyimage(new DbContextOptions<ApplicationDbContext>());
        }

        [TestMethod]
        public void Check_date_of_today_is_in_correct_format()
        {
            var result = sut.GetDateToday();
            var expected = DateTime.Now.ToString("yyyyMMdd");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void When_no_image_exists_on_product_should_write_to_file()
        {
            List<string> productIdList = new List<string>{"12"};

            sut.WriteToFile(productIdList);
            var folderPath = "..\\outfiles\\products\\";            
            var result = File.ReadAllLines($"{folderPath}missingimages-{sut.GetDateToday()}.txt");

            Assert.AreEqual("12", result[0]);
        }

        [TestMethod]
        public void When_image_returns_404_should_return_false()
        {
            var fakeUrl = "http://example.com/invalid-image.jpg";

            var result = sut.DoesImageExist(fakeUrl);

            Assert.AreEqual(false, result);
        }
    }
}
