using Menu_item_creëren;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

public static class OrderFood
{
    static string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
    static string JSONString = File.ReadAllText(filePath);
    static List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
    static Dictionary<string, int> orders = new Dictionary<string, int>();
    public static void Start()
    {
        WriteLogo();
        Say("1", "Make order");
        Say("2", "Check order-basket");
        Say("3", "Show menu");
        Say("4", "Go back to the main menu");

        string firstinput = Console.ReadLine();
        bool running = true;
        while (running)
        {
            if (firstinput == "1")
            {
                Console.Clear();
                MenuItem.Start();
                Console.WriteLine("What would you like to order? Select the number.");
                string inputstr = Console.ReadLine();
                bool check = int.TryParse(inputstr, out int input);
                if (!check)
                {
                    Console.WriteLine("Input format incorrect.");
                    Console.WriteLine("Click enter to go back.");
                    Console.ReadLine();
                    Console.Clear();
                    Start();

                }
                Item item = menu.FirstOrDefault(i => i.Id == input);
                if (item != null)
                {
                    Console.WriteLine($"You have selected {item.Name}. How many would you like to order?");
                    string quantitystr = Console.ReadLine();
                    check = int.TryParse(quantitystr, out int quantity);
                    if (!check)
                    {
                        Console.WriteLine("Input format incorrect.");
                        Console.WriteLine("Click enter to go back.");
                        Console.ReadLine();
                        Console.Clear();
                        Start();
                    }
                    if (quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        Console.WriteLine("Click enter to go back.");
                        Console.ReadLine();
                        Console.Clear();
                        Start();
                    }
                    if (orders.ContainsKey(item.Name))
                    {
                        orders[item.Name] += quantity;
                    }
                    else
                    {
                        orders[item.Name] = quantity;
                    }
                    Console.WriteLine($"Succesfully added {quantity}x {item.Name} to your cart.");
                }
                else
                {
                    Console.WriteLine("No item found with the specified ID.");
                }
                Console.WriteLine("Click enter to go back.");
                Console.ReadLine();
            }
            else if (firstinput == "2")
            {
                Console.Clear();
                Console.WriteLine("Your cart:");
                Console.WriteLine("--------------");
                double totalprice = 0;
                foreach (var order in orders)
                {
                    Item item = menu.FirstOrDefault(i => i.Name == order.Key);
                    if (item != null)
                    {
                        double itemprice = item.Price * order.Value;
                        totalprice += itemprice;
                        Console.WriteLine($"{order.Value}x {item.Name} = €{itemprice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"))}");
                    }
                }
                Console.WriteLine("--------------");
                Console.WriteLine($"Total Price: €{totalprice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"))}");
                Console.WriteLine("Click enter to go back.");
                Console.ReadLine();
            }
            else if (firstinput == "3")
            {
                Console.Clear();
                MenuItem.Start();
                Console.WriteLine("Click enter to go back.");
                Console.ReadLine();
            }
            else if (firstinput == "4") 
            {
                Console.Clear();
                FoodMenu.Start();
            }
            else
            {
                Console.WriteLine("Invalid input. Try again please");
            }
            Console.Clear();
                WriteLogo();
                Say("1", "Make order");
                Say("2", "Check order-basket");
                Say("3", "Show food-menu");
                Say("4", "Go back");
            firstinput = Console.ReadLine();

        }
        
    }
    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Green);
        Console.WriteLine("] " + message);
    }

    public static void WriteLogo()
    {
        string logo = @"   ___          _           _             
  / _ \ _ __ __| | ___ _ __(_)_ __   __ _ 
 | | | | '__/ _` |/ _ \ '__| | '_ \ / _` |
 | |_| | | | (_| |  __/ |  | | | | | (_| |
  \___/|_|  \__,_|\___|_|  |_|_| |_|\__, |
                                    |___/                                                    
";

        Console.WriteLine(logo, Color.Wheat);
    }
}
        