﻿
using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using System.Text;

namespace ShopAdmin.Commands
{
    public class VerifyImage : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        public VerifyImage(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);
            List<string> listOfProductsWithoutImage = new List<string>();

            foreach (var product in _context.Products)
            {
                if (product.ImageUrl is null)
                {
                    listOfProductsWithoutImage.Add(product.Id.ToString());
                }
            }

            File.WriteAllLines($"\\outfiles\\products\\missingimages-{GetDateToday()}.txt", listOfProductsWithoutImage);
        }

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