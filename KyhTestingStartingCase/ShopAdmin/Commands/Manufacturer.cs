using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ShopAdmin.Commands
{
    public class Manufacturer
    {
        private readonly ApplicationDbContext _context;
        public Manufacturer(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);
        }

        public void Sendreport()
        {
            var listOfEmails = _context.Manufacturers.Select(manu => manu.EmailReport).ToList();
            var listOfManufacturerNames = _context.Manufacturers.Select(manu => manu.Name).ToList();
            var message = new MimeMessage();
            var i = 0;
            var correctMonth = DateTime.Now.AddMonths(-1).ToString("MMMM");           


            foreach(var email in listOfEmails)
            {
                message.From.Add(new MailboxAddress("Stefan SuperShop", "larissa.steuber@ethereal.email"));
                message.To.Add(new MailboxAddress(listOfManufacturerNames[i], email));
                i++;

                message.Subject = $"Försäljningsrapport {correctMonth}";
            }
            


        }
    }
}
