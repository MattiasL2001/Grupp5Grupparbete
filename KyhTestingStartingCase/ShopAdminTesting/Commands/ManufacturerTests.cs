using Microsoft.EntityFrameworkCore;
using ShopAdmin.Commands;
using ShopGeneral.Data;

namespace ShopAdminTesting.Commands
{
    [TestClass]
    public class ManufacturerTests
    {
        private readonly ShopAdmin.Commands.Manufacturer sut;
        public ManufacturerTests()
        {
            sut = new ShopAdmin.Commands.Manufacturer(new DbContextOptions<ApplicationDbContext>());
        }

        [TestMethod]
        public void Check_When_Its_The_Third_Of_The_Month_Returns_True()
        {
            //Arrange
            var testDate = new DateTime(2023, 03, 03);

            //Act
            var result = sut.CheckIfThirdOfTheMonth(testDate);

            //Assert
            Assert.AreEqual(true, result);
        }

        //[TestMethod]
        //public void When_Creating_Emails_Returns_List_Of_Emails()
        //{

        //}
        //[TestMethod]
        //public void Verify_When_Sending_Emails_That_Its_Received()
        //{

        //}

    }
}
