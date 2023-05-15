using Menu_item_creëren;
using Newtonsoft.Json;
using Project.Olivier_Reservations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

public static class OrderFood
{
    static string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
    static string JSONString = File.ReadAllText(filePath);
    public static List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
    public static Dictionary<string, int> orders = new Dictionary<string, int>();
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
                bool found = false;
                Console.Clear();
                Console.WriteLine("Please enter your reservation code: ");
                string code = Console.ReadLine();
                List<Project.Olivier_Reservations.Reservation> reservations = SaveReservations.LoadAll();
                foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
                {
                    if (reservation.Code == code)
                    {
                        found = true;
                    }
                }
                if (found == false)
                {
                    Console.WriteLine("Reservation code invalid\nPress enter to go back...");
                    Console.ReadKey();
                    Console.Clear();
                    Start();
                }
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
                    AddOrderJSON(code, item.Id, quantity);
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
                bool found = false;
                Console.Clear();
                Console.WriteLine("Please enter your reservation code: ");
                string code = Console.ReadLine();
                List<Project.Olivier_Reservations.Reservation> reservations = SaveReservations.LoadAll();
                foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
                {
                    if (reservation.Code == code)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Reservation code invalid\nPress enter to go back...");
                    Console.ReadKey();
                    Console.Clear();
                    Start();
                }
                string filePath = Path.Combine("..", "..", "..", "DataSources", "Orders.json");

                // Deserialize the existing JSON data
                List<Dictionary<string, List<Dictionary<string, int>>>> jsonData;
                string jsonString = File.ReadAllText(filePath);
                jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, int>>>>>(jsonString);

                Console.Clear();
                Console.Clear();
                Console.WriteLine("Your cart:");
                Console.WriteLine("--------------");
                double totalprice = 0;

                foreach (Dictionary<string, List<Dictionary<string, int>>> dict in jsonData)
                {
                    // Check if the current dictionary contains the specified code
                    if (dict.ContainsKey(code))
                    {
                        // Get the list of item dictionaries from the current dictionary
                        List<Dictionary<string, int>> itemList = dict[code];

                        // Iterate over each item dictionary in the list
                        foreach (Dictionary<string, int> itemDict in itemList)
                        {
                            // Get the itemId and quantity values from the current item dictionary
                            int itemId = itemDict["itemId"];
                            int quantity = itemDict["quantity"];
                            Item item = menu.FirstOrDefault(i => i.Id == itemId);
                            if (item != null)
                            {
                                double itemprice = item.Price * quantity;
                                totalprice += itemprice;
                                Console.WriteLine($"{quantity}x {item.Name} = {itemprice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")).Replace("?", "€")}");
 
                            }
                        }

                        // Exit the loop after finding the specified code

                        Console.WriteLine("--------------");
                        Console.WriteLine($"Total Price: €{totalprice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"))}");
                        Console.WriteLine("Click enter to go back.");
                        Console.ReadLine();
                        break;
                    }
                }
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
    public static void AddOrderJSON(string orderCode, int itemId, int quantity)
    {
        // Define the file path
        string filePath = Path.Combine("..", "..", "..", "DataSources", "Orders.json");

        // Deserialize the existing JSON data
        List<Dictionary<string, List<Dictionary<string, int>>>> jsonData;
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, int>>>>>(jsonString);
        }
        else
        {
            jsonData = new List<Dictionary<string, List<Dictionary<string, int>>>>();
        }

        // Find the dictionary with the given order code or create a new one
        Dictionary<string, List<Dictionary<string, int>>> orderData = jsonData.FirstOrDefault(d => d.ContainsKey(orderCode));
        if (orderData == null)
        {
            orderData = new Dictionary<string, List<Dictionary<string, int>>>();
            orderData.Add(orderCode, new List<Dictionary<string, int>>());
            jsonData.Add(orderData);
        }

        // Check if the item already exists in the order and update its quantity
        Dictionary<string, int> existingItem = orderData[orderCode].FirstOrDefault(item => item["itemId"] == itemId);
        if (existingItem != null)
        {
            existingItem["quantity"] += quantity;
        }
        else
        {
            // Add the new item to the order data
            Dictionary<string, int> itemData = new Dictionary<string, int>();
            itemData.Add("itemId", itemId);
            itemData.Add("quantity", quantity);
            orderData[orderCode].Add(itemData);
        }

        // Serialize the updated JSON data and write it back to the file
        string updatedJsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        File.WriteAllText(filePath, updatedJsonString);
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
        