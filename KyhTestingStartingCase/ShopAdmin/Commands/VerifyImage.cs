using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using System.Net;
using System.Text;

namespace ShopAdmin.Commands
{
    public class Verifyimage : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        public Verifyimage(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);            
        }

        public void Verifyimagesofproducts()
        {
            List<string> listOfProductsWithoutImage = new List<string>();

            foreach (var product in _context.Products)
            {
                if (!DoesImageExist(product.ImageUrl)) { listOfProductsWithoutImage.Add(product.Id.ToString()); }
            }

            WriteToFile(listOfProductsWithoutImage);
        }

        private bool DoesImageExist(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if(response.StatusCode is HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        private void WriteToFile(List<string> listOfProductsWithoutImage)
        {
            var folderPath = "..\\outfiles\\products\\";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            File.WriteAllLines($"{folderPath}missingimages-{GetDateToday()}.txt", listOfProductsWithoutImage);
        }

        private string GetDateToday()
        {
            var yyyyMMdd = new StringBuilder();
            yyyyMMdd.Append(DateTime.Now.Year);
            yyyyMMdd.Append(DateTime.Now.Month);
            yyyyMMdd.Append(DateTime.Now.Day);
            return yyyyMMdd.ToString();
        }       

    }
}
