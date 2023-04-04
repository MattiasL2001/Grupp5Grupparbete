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

        [TestMethod]
        public void When_Creating_Emails_Returns_List_Of_Emails()
        {
            List<string> emailAdresses = new List<string>();
            string testEmail1 = "Hej123@fake.se";
            string testEmail2 = "hej534@fake.se";
            emailAdresses.Add(testEmail1);
            emailAdresses.Add(testEmail2);
            var manufacturerList = new List<string>() { "rolls royce", "volvo" };


            var result = sut.CreatingEmails(emailAdresses, manufacturerList);
            var resultManufacturer1 = result[0].To.ToString();
            var manafacturerNameAndEmail = resultManufacturer1.Split("<");
            var resultManufacturer1EmailAdress = manafacturerNameAndEmail[1].Replace(">", "");
            

            Assert.AreEqual(testEmail1, resultManufacturer1EmailAdress);
        }
        [TestMethod]
        public void Verify_When_Sending_Emails_That_Its_Received()
        {
            //Arrage
            List<string> emailAdresses = new List<string>();
            string testEmail1 = "Hej123@fake.se";
            string testEmail2 = "hej534@fake.se";
            emailAdresses.Add(testEmail1);
            emailAdresses.Add(testEmail2);
            var manufacturerList = new List<string>() { "rolls royce", "volvo" };

            var listOfEmails = sut.CreatingEmails(emailAdresses, manufacturerList);
            //Act
            sut.SendingEmails(listOfEmails);
            //Assert

        }

    }
}
