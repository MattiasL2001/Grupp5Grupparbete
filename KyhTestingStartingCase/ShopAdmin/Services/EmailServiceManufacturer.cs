using MimeKit;
using MailKit.Net.Smtp;

namespace ShopAdmin.Services
{
    public class EmailServiceManufacturer : IEmailService
    {
        public List<MimeMessage> CreatingEmails(List<string> listOfEmailsAdresses, List<string> listOfNames)
        {
            var listOfMessage = new List<MimeMessage>();

            var i = 0;
            var correctMonth = DateTime.Now.AddMonths(-1).ToString("MMMM");

            foreach (var email in listOfEmailsAdresses)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Stefan SuperShop", "antonetta.emard22@ethereal.email"));
                message.To.Add(new MailboxAddress(listOfNames[i], email));
                i++;

                message.Subject = $"Sales Report {correctMonth}";
                // Leaving this commented, so you can later add sales data to each email
                //message.Body = new TextPart("plain")
                //{
                //    Text = @"Här skriver du innehållet i emailet"
                //};
                listOfMessage.Add(message);
            }

            return listOfMessage;
        }
        public void SendingEmails(List<MimeMessage> listOfEmails)
        {
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.ethereal.email", 587, false);

                client.Authenticate("antonetta.emard22@ethereal.email", "rFzkgaH8sbGYfdM4DK");

                listOfEmails.ForEach(email => { client.Send(email); });

                client.Disconnect(true);
            }
        }
        public bool CheckIfThirdOfTheMonth(DateTime dtNow)
        {
            if (dtNow.Day is not 3) { return false; }
            return true;
        }
    }
}
