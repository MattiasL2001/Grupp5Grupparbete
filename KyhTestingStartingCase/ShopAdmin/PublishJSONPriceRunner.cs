using ShopGeneral.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using System.Collections;

namespace ShopAdmin
{
    public class PublishJSONPriceRunner
    {
        public void Run()
        {
            List<Product> products = new List<Product>();
            Product p1 = new Product();
            p1.Manufacturer = new Manufacturer();
            Product p2 = new Product();
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

            //foreach (Product product in products)
            //{
            //    result.Add(ProductToPriceRunner(product));
            //}
            //JSONString = JsonSerializer.Serialize(result, options);

            JSONString = CreateJsonString(products);

            CreateFile(JSONString);

            //var JSONString = JsonSerializer.Serialize<List<string[]>>(result, options);
            //var deSerialized = JsonSerializer.Deserialize<List<string[]>>(JSONString);
        }

        void CreateFile(string result)
        {
            string path = ".\\outfiles\\pricerunner";
            string today = DateTime.Today.ToString("yyyyMMdd");
            string filePath = $"{path}\\{today}.txt";

            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            File.WriteAllText(filePath, result);
        }

        string[] ProductToPriceRunner(Product p)
        {
            var id = Convert.ToString(p.Id);
            var title = p.Name;
            var description = "";
            var price = Convert.ToString(p.BasePrice);
            var discountPercentage = Convert.ToString(0);
            var rating = Convert.ToString(0);
            var stock = Convert.ToString(0);
            var brand = Convert.ToString(p.Manufacturer);
            var category = Convert.ToString(p.Category);
            var images = new List<string>();
            images.Add(p.ImageUrl);
            var imageString = "";

            foreach (var image in images)
            {
                imageString += image;
            }

            string[] stringResult =
            {
                "id: " + id,
                "title: " + title,
                "decription:" + description,
                "price: " + price,
                "discountPercentage: " + discountPercentage,
                "rating: " + rating,
                "stock: " + stock,
                "brand: " + brand,
                "category: " + category,
                "images: " + imageString
            };

            return stringResult;
        }
        public string CreateJsonString(List<ShopGeneral.Data.Product> products)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n\"products\":[");

            foreach (var product in products)
            {
                product.Manufacturer = new Manufacturer();
                product.Manufacturer.Name = "sdgdfhdgfg";
                product.Category = new Category();
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
    }
}
