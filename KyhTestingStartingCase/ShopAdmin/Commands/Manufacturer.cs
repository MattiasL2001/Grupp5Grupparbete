using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using MailKit.Net.Smtp;
using MimeKit;

namespace ShopAdmin.Commands
{
    public class Manufacturer : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        public Manufacturer(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);
        }

        public void Sendreport()
        {            
            var sendTheReport = CheckIfThirdOfTheMonth(DateTime.Now);
            if (sendTheReport is true)
            {
                var listOfEmailAdresses = _context.Manufacturers.Select(manu => manu.EmailReport.Replace(" ", "_")).ToList();
                var listOfManufacturerNames = _context.Manufacturers.Select(manu => manu.Name).ToList();

                var listOfEmails = CreatingEmails(listOfEmailAdresses, listOfManufacturerNames);
                SendingEmails(listOfEmails);
            }
            
        }
        public List<MimeMessage> CreatingEmails(List<string> listOfEmails, List<string> listOfManufacturerNames)
        {                        
            var listOfMessage = new List<MimeMessage>();

            var i = 0;
            var correctMonth = DateTime.Now.AddMonths(-1).ToString("MMMM");

            foreach (var email in listOfEmails)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Stefan SuperShop", "antonetta.emard22@ethereal.email"));
                message.To.Add(new MailboxAddress(listOfManufacturerNames[i], email));
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
