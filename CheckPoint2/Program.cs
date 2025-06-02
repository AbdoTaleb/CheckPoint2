using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


class Product
{
    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{Category} - {Name} - {Price}";    
    }

    class Program
    {
        static List<Product> products = new List<Product>();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Product App");

            AddProductLoop();

            while (true)
            {
                Console.Write("Type 'add' to add more, 'search' to search, or 'q' to quit: ");
                string command = Console.ReadLine().ToLower();

                if (command == "q") break;
                else if (command == "add")
                {
                    AddProductLoop();
                    continue;
                }
                else if (command == "search")
                {
                    Search();
                    continue;
                }
                else
                {
                    Console.WriteLine("Unknown command. Please try again.");
                }
            }
        }

        static void AddProductLoop()
        {
            while (true)
            {

                Console.Write("Enter Category (or 'q' to stop adding): ");
                string category = Console.ReadLine();
                if (category.ToLower() == "q") break;

                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine();

                decimal price;
                while (true)
                {
                    Console.Write("Enter Price: ");
                    if (decimal.TryParse(Console.ReadLine(), out price))
                    {
                        break;  // valid price, break out of price loop
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Please enter a valid number.");
                    Console.ResetColor();

                }

                products.Add(new Product { Category = category, Name = name, Price = price });
                Console.WriteLine("Product added!");
                

            }
        }

        static void DisplayProductList(string highlight = null)
        {
            Console.WriteLine("\n--- Product List ---");
            Console.WriteLine("{0,-15} {1,-20} {2,10}", "Category", "Name", "Price");
            Console.WriteLine(new string('-', 50));

            var sortedProducts = products.OrderBy(p => p.Price).ToList();
            decimal total = 0;

            foreach (var product in sortedProducts)
            {
                bool isMatch = !string.IsNullOrEmpty(highlight) &&
                               (product.Name.ToLower().Contains(highlight) ||
                                product.Category.ToLower().Contains(highlight));

                if (isMatch)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("{0,-15} {1,-20} {2,10} SEK",
                                  product.Category,
                                  product.Name,
                                  product.Price);

                Console.ResetColor();
                total += product.Price;
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Total Price: {total} SEK");
            Console.WriteLine();
        }

        static void Search()
        {
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine().ToLower();
            DisplayProductList(searchTerm);
        }


    }

}