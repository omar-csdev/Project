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
    static List<string> Orders = new List<string>();
    static List<string> RawOrders = new List<string>();
    public static void Start()
    {
        WriteLogo();
        Say("1", "Make order");
        Say("2", "Check order-basket");
        Say("3", "Show food-menu");
        Say("4", "Go back to the mainenu");

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
                foreach (Item item in menu)
                {
                    if (input == item.Id)
                    {
                        if (Orders.Contains(item.Name))
                        {
                            Console.WriteLine($"Would you like to proceed adding {item.Name} with id: {item.Id} for a second time? Y/N");
                            string inpt = Console.ReadLine();
                            if (inpt.ToLower() == "y")
                            {
                                RawOrders.Add(item.Name);
                                Orders.Add($"{item.Name} x2");
                                Orders.Remove(item.Name);
                                Console.WriteLine($"Succesfully added {item.Name} with id: {item.Id} x2");
                                break;
                            }
                            else if ((inpt.ToLower() == "n"))
                            {
                                Console.WriteLine(item.Name + " has not been added.");
                                break;
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Try again please");
                            }
                        }
                        else
                        {
                            RawOrders.Add(item.Name);
                            Orders.Add(item.Name);
                            Console.WriteLine($"Succesfully added {item.Name} to your cart");
                            break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("No such id is found. Try again please");
                    }

                }
                
                Console.WriteLine("Click enter to go back.");
                Console.ReadLine();



            }
            else if (firstinput == "2")
            {
                Console.Clear();
                double totalprice = 0;
                string orderPrint = string.Join("\n", Orders);
                Console.WriteLine($"Your cart:\n{orderPrint}\n");

                foreach (string priceperitem in RawOrders)
                {
                    foreach (Item item in menu)
                    {
                        if (item.Name == priceperitem)
                        {
                            totalprice += item.Price;
                        }
                    }
                }
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
                Thread.Sleep(2000);
            }
            Thread.Sleep(2000);
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
        