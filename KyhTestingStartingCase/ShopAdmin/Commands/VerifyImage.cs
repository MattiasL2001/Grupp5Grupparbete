using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using System.Net;

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

        public bool DoesImageExist(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode is HttpStatusCode.NotFound) { return false; }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void WriteToFile(List<string> listOfProductsWithoutImage)
        {
            var folderPath = "..\\outfiles\\products\\";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            File.WriteAllLines($"{folderPath}missingimages-{GetDateToday()}.txt", listOfProductsWithoutImage);
        }

        public string GetDateToday()
        {            
            return DateTime.Now.ToString("yyyyMMdd");
        }       

    }
}
