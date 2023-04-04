using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using MailKit.Net.Smtp;
using MailKit;
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
            var listOfEmails = CreatingEmails();
            SendingEmails(listOfEmails);
        }
        public List<MimeMessage> CreatingEmails()
        {
            var listOfEmails = _context.Manufacturers.Select(manu => manu.EmailReport.Replace(" ","_")).ToList();
            var listOfManufacturerNames = _context.Manufacturers.Select(manu => manu.Name).ToList();
            
            var listOfMessage = new List<MimeMessage>();
            var i = 0;
            var correctMonth = DateTime.Now.AddMonths(-1).ToString("MMMM");

            foreach (var email in listOfEmails)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Stefan SuperShop", "antonetta.emard22@ethereal.email"));
                message.To.Add(new MailboxAddress(listOfManufacturerNames[i], email));
                i++;

                message.Subject = $"Försäljningsrapport {correctMonth}";
                message.Body = new TextPart("plain")
                {
                    Text = @"Test"
                };
                listOfMessage.Add(message);
            }

            return listOfMessage;
        }
        public void SendingEmails(List<MimeMessage> listOfEmails)
        {
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.ethereal.email", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("antonetta.emard22@ethereal.email", "rFzkgaH8sbGYfdM4DK");

                foreach (var email in listOfEmails)
                {
                    client.Send(email);
                } 
                
                client.Disconnect(true);
            }
        }
    }
}
