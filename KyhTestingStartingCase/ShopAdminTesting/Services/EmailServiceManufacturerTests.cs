using MimeKit;
using ShopAdmin.Services;

namespace ShopAdminTesting.Services
{
    public class FakeEmailService : IEmailService
    {
        public bool IsThisMethodCalled = false;
        public List<MimeMessage> CreatingEmails(List<string> listOfEmailsAdresses, List<string> listOfNames)
        {
            throw new NotImplementedException();
        }

        public void SendingEmails(List<MimeMessage> listOfEmails)
        {
            IsThisMethodCalled = true;
        }
    }

    [TestClass]
    public class EmailServiceManufacturerTests
    {
        private readonly EmailServiceManufacturer sut;
        private readonly FakeEmailService fakeEmailService;

        public EmailServiceManufacturerTests()
        {
            sut = new EmailServiceManufacturer();
            fakeEmailService = new FakeEmailService();
        }

        [TestMethod]        
        public void Check_When_Its_The_Third_Of_The_Month_Returns_True()
        {            
            var testDate = new DateTime(2023, 03, 03);
            
            var result = sut.CheckIfThirdOfTheMonth(testDate);
            
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
        public void When_Email_Sent_Verify_That_The_Method_Is_Called()
        {
            List<string> emailAdresses = new List<string>();
            var manufacturerList = new List<string>();

            var listOfEmails = sut.CreatingEmails(emailAdresses, manufacturerList);

            fakeEmailService.IsThisMethodCalled = false;
            fakeEmailService.SendingEmails(listOfEmails);

            Assert.IsTrue(fakeEmailService.IsThisMethodCalled);
        }
    }
}
