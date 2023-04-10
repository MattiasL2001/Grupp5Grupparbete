using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using ShopAdmin.Services;

namespace ShopAdmin.Commands
{
    public class Manufacturer : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailServiceManufacturer _emailManufacturer;
        public Manufacturer(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);
            _emailManufacturer = new EmailServiceManufacturer();
        }

        public void Sendreport()
        {            
            var sendTheReport = _emailManufacturer.CheckIfThirdOfTheMonth(DateTime.Now);
            if (sendTheReport is true)
            {
                var listOfEmailAdresses = _context.Manufacturers.Select(manu => manu.EmailReport.Replace(" ", "_")).ToList();
                var listOfManufacturerNames = _context.Manufacturers.Select(manu => manu.Name).ToList();

                var listOfEmails = _emailManufacturer.CreatingEmails(listOfEmailAdresses, listOfManufacturerNames);
                _emailManufacturer.SendingEmails(listOfEmails);
            }            
        }        
    }
}
