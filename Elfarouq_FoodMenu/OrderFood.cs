using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class OrderFood
{
    static List<string> Orders = new List<string>();
    public static void Start()
    {
        Console.WriteLine("What would you like to do?");
        Say("1", "Make order");
        Say("2", "Check Order-Basket");
        Say("3", "Go back to the MainMenu");
        string firstinput = Console.ReadLine();
        bool running = true;
        while (running)
        {
            if (firstinput == "1")
            {
                Console.WriteLine("What would you like to order? Choose by the name of the id");
                int input = Convert.ToInt32(Console.ReadLine());
                string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
                string JSONString = File.ReadAllText(filePath);
                List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
                bool foundItem = false;
                foreach (Item item in menu)
                {
                    if (input == item.Id)
                    {
                        if (Orders.Contains(item.Name))
                        {
                            Console.WriteLine($"Would you like to proceed adding {item.Name} with id: {item.Id} for a second time?");
                            Console.WriteLine("Hier verder");
                        }
                        else
                        {
                            Orders.Add(item.Name);
                            foundItem = true;
                            break;
                        }
                    }
                }
                if (!foundItem)
                {
                    Console.WriteLine("No such id is found. Try again please");
                    Thread.Sleep(3000);
                }
            }
            else if (firstinput == "2")
            {
                string orderPrint = string.Join(",", Orders);
                Console.WriteLine($"Uw WinkelWagen: {orderPrint}\n");
                Thread.Sleep(5000);
            }
            else if (firstinput == "3")
            {

                FoodMenu.Start();
            }
            else
            {
                Console.WriteLine("Invalid input. Try again please");
                Thread.Sleep(3000);
            }
            Console.WriteLine("What would you like to do next?");
            Say("1", "Make order");
            Say("2", "Check Order-Basket");
            Say("3", "Go back to the MainMenu");
            firstinput = Console.ReadLine();
        }
    }
    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }
}

