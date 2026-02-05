using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory Item");
            Console.WriteLine("S. Add an item to the shopping cart");
            Console.WriteLine("P. Print out your shopping cart");
            Console.WriteLine("E. Remove an item from your shopping cart");
            Console.WriteLine("Q. Quit");


            List<Product?> list = ProductServiceProxy.Current.Products;
            List<Product?> shopping = ShoppingServiceProxy.Current.Products;


            //if i move an item from the shopping cart to the inventory, it loses the price

            char choice;
            do
            {
                string? input = Console.ReadLine().ToUpper();

                choice = input[0];

                switch (choice)
                {
                    case 'C':
                    case 'c':
                        Console.WriteLine("Enter the name:");
                        var nameOf = Console.ReadLine();
                        Console.WriteLine("Enter the price");
                        var prices = Console.ReadLine();
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = nameOf,
                            cost = Convert.ToDouble(prices)
                        });
                        break;

                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;

                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        if (selectedProd != null)
                        {
                            Console.WriteLine("Enter the new product name");
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            Console.WriteLine("Enter the new product price");
                            prices = Console.ReadLine();
                            selectedProd.cost = Convert.ToDouble(prices);
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;

                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;

                    case 'S':
                    case 's':
                        Console.WriteLine("Which product would you like to buy?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        if (selectedProd != null && selectedProd == list.FirstOrDefault(p => p.Id == selection))
                        {
                            ShoppingServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        else
                        {
                            Console.WriteLine("That product doesnt exist. Try another");
                        }
                        break;

                    case 'P':
                    case 'p':
                        shopping.ForEach(Console.WriteLine);
                        break;

                    case 'E':
                    case 'e':
                        Console.WriteLine("Which product would you like to remove from your cart?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = shopping.FirstOrDefault(p => p.Id == selection);
                        if (selectedProd != null && selectedProd == shopping.FirstOrDefault(p => p.Id == selection))
                        {

                            ShoppingServiceProxy.Current.Delete(selection);

                        }
                        else
                        {
                            Console.WriteLine("That product isnt in your shopping cart");
                        }
                        break;


                    case 'Q':
                    case 'q':
                        Console.WriteLine("Your reciept is:");
                        shopping.ForEach(Console.WriteLine);
                        double total = 0;
                        for (int i = 0; i < shopping.Count; i++)
                        {
                            total += shopping[i].cost ?? 0;
                        }
                        Console.WriteLine("");
                        Console.WriteLine($"Your total is ${Math.Round(total * 1.07)}. Are you sure you want to check out? (y/n)");
                        string decision = Console.ReadLine();
                        if (decision == "y")
                        {
                        }
                        else if (decision == "n")
                        {
                            choice = 'x';
                        }
                        else
                        {
                            Console.WriteLine("Sorry, didnt quite get that. Please check out again");
                            choice = 'x';
                        }
                        break;

                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');
            Console.ReadLine();


        }


    }
}

