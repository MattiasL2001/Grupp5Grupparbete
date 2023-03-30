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
    }
}
