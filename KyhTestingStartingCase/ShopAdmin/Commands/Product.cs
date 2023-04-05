using Humanizer;
using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ShopAdmin.Commands
{
    public class Product : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        public Product(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);            
        }

        public void Verifyimage()
        {
            List<string> listOfProductsWithoutImage = new List<string>();

            foreach (var product in _context.Products)
            {
                if (!DoesImageExist(product.ImageUrl)) { listOfProductsWithoutImage.Add(product.Id.ToString()); }
            }

            WriteToFile(listOfProductsWithoutImage);
        }

        public void Export()
        {
            var products = new List<ShopGeneral.Data.Product>();
            var p1 = new ShopGeneral.Data.Product();
            p1.Manufacturer = new Manufacturer();
            var p2 = new ShopGeneral.Data.Product();
            p2.Manufacturer = new Manufacturer();
            p1.Name = "p1";
            p1.BasePrice = 100;
            p1.Manufacturer.Name = "Apple";
            p2.Name = "p2";
            p2.BasePrice = 50;
            p1.Manufacturer.Name = "Samsung";
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            products.Add(p1);
            products.Add(p2);
            var result = new List<string[]>();
            var JSONString = "";
            JSONString = CreateJsonString(products);
            WriteToFilePricerunner(JSONString);
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
        public string CreateJsonString(List<ShopGeneral.Data.Product> products)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n\"products\":[");

            foreach (var product in products)
            {
                product.Manufacturer = new Manufacturer();
                product.Manufacturer.Name = "sdgdfhdgfg";
                product.Category = new ShopGeneral.Data.Category();
                product.Category.Name = "vcxbcvbcv";
                product.ImageUrl = "www.google.se";

                stringBuilder.Append("\n{");
                stringBuilder.Append($"\n\"id\":{product.Id},\n");
                stringBuilder.Append($"\"title\":\"{product.Name}\",\n");
                stringBuilder.Append($"\"description\":\" \",\n");
                stringBuilder.Append($"\"price\":{product.BasePrice},\n");
                stringBuilder.Append($"\"discountPercentage\":0,\n");
                stringBuilder.Append($"\"rating\":0,\n");
                stringBuilder.Append($"\"stock\":0,\n");
                stringBuilder.Append($"\"brand\":\"{product.Manufacturer.Name}\",\n");
                stringBuilder.Append($"\"category\":\"{product.Category.Name}\",\n");
                stringBuilder.Append($"\"images\":[{product.ImageUrl}]\n");
                stringBuilder.Append("},");
            }

            stringBuilder.Append("\"total\": 100,\r\n  \"skip\": 0,\r\n  \"limit\": 30\n}");
            return stringBuilder.ToString();
        }

        public void WriteToFilePricerunner(string result)
        {
            string path = ".\\outfiles\\pricerunner";
            string today = DateTime.Today.ToString("yyyyMMdd");
            string filePath = $"{path}\\{today}.txt";

            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            File.WriteAllText(filePath, result);
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
