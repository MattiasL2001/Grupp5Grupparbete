
using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using System.Text;

namespace ShopAdmin.Commands
{
    public class VerifyImage : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        public VerifyImage(DbContextOptions<ApplicationDbContext> janne)
        {
            _context = new ApplicationDbContext(janne);

            foreach (var product in _context.Products)
            {
                if (product.ImageUrl is null)
                {
                    File.WriteAllText($"\\outfiles\\products\\missingimages-{GetDateToday()}.txt", product.Id.ToString());
                }
            }
        }

        //public void VerifyTheseImages(List<Product> productList)
        //{
        //    foreach (var product in productList)
        //    {
        //        if (product.ImageUrl is null)
        //        {
        //            File.WriteAllText($"\\outfiles\\products\\missingimages-{GetDateToday()}.txt", product.Id.ToString());
        //        }
        //    }
        //}

        public string GetDateToday()
        {
            var yyyyMMdd = new StringBuilder();
            yyyyMMdd.Append(DateTime.Now.Year);
            yyyyMMdd.Append(DateTime.Now.Month);
            yyyyMMdd.Append(DateTime.Now.Day);
            return yyyyMMdd.ToString();
        }
        

    }
}
