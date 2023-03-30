using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;

namespace ShopAdmin.Commands
{
    public class Verifycategory : ConsoleAppBase
    {
        private readonly ApplicationDbContext _context;
        public Verifycategory(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);
        }

        public void Verifycatagorieshasproducts()
        {
            WriteToFile(ListCategoriesWithNoProductMatch(_context.Categories.ToList(), _context.Products.ToList()));
        }
        public List<string> ListCategoriesWithNoProductMatch(List<Category> categoryList, List<Product> products)
        {
            List<string> listOfCategoriesWithNoProducts = new List<string>();
            foreach (var category in categoryList)
            {
                var thisManyProductsInThisCategory = products.Where(pr => pr.Category.Name == category.Name).Count();

                if (thisManyProductsInThisCategory == 0) { listOfCategoriesWithNoProducts.Add(category.Name); }               
            }

            return listOfCategoriesWithNoProducts;
        }
        public void WriteToFile(List<string> listOfCategoriesWithNoProducts)
        {
            var folderPath = "..\\outfiles\\category\\";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            File.WriteAllLines($"{folderPath}missingproducts-{GetDateToday()}.txt", listOfCategoriesWithNoProducts);
        }

        public string GetDateToday()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
    }
}
