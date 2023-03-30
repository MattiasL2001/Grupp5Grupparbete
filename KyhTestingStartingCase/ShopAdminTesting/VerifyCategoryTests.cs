using Microsoft.EntityFrameworkCore;
using ShopAdmin.Commands;
using ShopGeneral.Data;

namespace ShopAdminTesting
{
    [TestClass]
    public class VerifyCategoryTests
    {
        private readonly Verifycategory sut;
        public VerifyCategoryTests()
        {
            sut = new Verifycategory(new DbContextOptions<ApplicationDbContext>());
        }

        [TestMethod]
        public void Check_date_of_today_is_in_correct_format()
        {
            var result = sut.GetDateToday();
            var expected = DateTime.Now.ToString("yyyyMMdd");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void When_category_has_no_products_should_write_to_file()
        {
            List<string> categoryNameList = new List<string> { "Wagon" };

            sut.WriteToFile(categoryNameList);
            var folderPath = "..\\outfiles\\category\\";
            var result = File.ReadAllLines($"{folderPath}missingproducts-{sut.GetDateToday()}.txt");

            Assert.AreEqual("Wagon", result[0]);
        }

        [TestMethod]
        public void If_category_has_no_products_should_return_list_of_category_name()
        {
            List<Category> listCategory = new List<Category>();
            var cate1 = new Category { Name = "cate1" };
            listCategory.Add(cate1);
            var cate2 = new Category { Name = "cate2" };
            listCategory.Add(cate2);

            List<Product> listProduct = new List<Product>();
            var prod1 = new Product { Name = "1", Category = cate2};
            listProduct.Add(prod1);

            var resultList = sut.ListCategoriesWithNoProductMatch(listCategory, listProduct);

            Assert.AreEqual("cate1", resultList[0]);
        }
    }
}
