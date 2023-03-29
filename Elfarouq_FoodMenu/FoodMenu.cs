using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using Menu_item_creëren;
using System.Drawing;
using Console = Colorful.Console;

public static class FoodMenu
{
    public static void Start()
    {
        Console.WriteLine("What would you like to do");
        Say("1", "SHOW food-menu");
        Say("2", "Order food");
        Say("3", "GO back");

        string input = Console.ReadLine();
        if (input == "1")
        {
            MenuItem.Start();
            Console.WriteLine("\nClick enter to go back");
            Console.ReadLine();
            Start();
        }
        else if (input == "2")
        {
            MenuItem.Start();
            OrderFood.Start();
        }
        else if (input == "3") 
        {
            MainMenu.Start();
        }
        else { Console.WriteLine("Geef een valide optie"); FoodMenu.Start(); }
    }

    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }

}
