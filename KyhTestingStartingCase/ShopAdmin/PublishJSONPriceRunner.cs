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
            Product p2 = new Product();
            p1.Name = "p1";
            p1.BasePrice = 100;
            p2.Name = "p2";
            p2.BasePrice = 50;
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            products.Add(p1);
            products.Add(p2);
            List<string[]> result = new List<string[]>();

            foreach (Product product in products)
            {
                result.Add(ProductToPriceRunner(product));
            }

            var JSONString = JsonSerializer.Serialize<List<string[]>>(result, options);

            CreateFile(JSONString);
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
            string[] images = {};
            images.Append(p.ImageUrl);

            string[] stringResult =
            {
                "id: " + id,
                "title: " + title,
                "description: " + description,
                "price: " + price,
                "discountPercentage: " + discountPercentage,
                "rating: " + rating,
                "stock: " + stock,
                "brand: " + brand,
                "category: " + category,
                "images: " + images.ToString()
            };

            return stringResult;
        }

        string ReturnJSON(Product p)
        {
            JsonSerializer.Serialize(p);
            Console.WriteLine(p);
            return $"id: {p.Id}";
        }
    }

}
