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
            var products = _context.Products.ToList();
            var manufacturers = _context.Manufacturers.ToList();
            var categories = _context.Categories.ToList();
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var JSONString = "";
            JSONString = CreateJsonString(products, manufacturers, categories);
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
        public string CreateJsonString(List<ShopGeneral.Data.Product> products, 
                                        List<ShopGeneral.Data.Manufacturer> manufacturers, 
                                        List<ShopGeneral.Data.Category>categories)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n\"products\":[");

            foreach (var product in products)
            {
                var manufacturerName = manufacturers.FirstOrDefault(x => x.Name == product.Manufacturer.Name).Name;
                var categoryName = categories.FirstOrDefault(x => x.Name == product.Category.Name).Name;

                stringBuilder.Append("\n{");
                stringBuilder.Append($"\n\"id\":{product.Id},\n");
                stringBuilder.Append($"\"title\":\"{product.Name}\",\n");
                stringBuilder.Append($"\"description\":\" \",\n");
                stringBuilder.Append($"\"price\":{product.BasePrice},\n");
                stringBuilder.Append($"\"discountPercentage\":0,\n");
                stringBuilder.Append($"\"rating\":0,\n");
                stringBuilder.Append($"\"stock\":0,\n");
                stringBuilder.Append($"\"brand\":\"{manufacturerName}\",\n");
                stringBuilder.Append($"\"category\":\"{categoryName}\",\n");
                stringBuilder.Append($"\"images\":[{product.ImageUrl}]\n");
                stringBuilder.Append("},");
            }

            stringBuilder.Append("\"total\": 100,\r\n  \"skip\": 0,\r\n  \"limit\": 30\n}");
            return stringBuilder.ToString();
        }

        public void WriteToFilePricerunner(string result)
        {
            string path = "..\\outfiles\\pricerunner";
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
