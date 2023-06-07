using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Data;

public static class FoodMenu
{
    public static void Start()
    {

        int choice;
        // Input checks
            Console.Clear();
            WriteLogo();
            Helper.Say("1", "Show food-menu");
            Helper.Say("2", "Order food");
            Helper.Say("3", "Go back");
        while (true)
        {
            
            try
            {
                choice = int.Parse(Console.ReadLine());
                if (choice < 1 || choice > 3)
                {
                    string message = ("Please enter a valid number between 1 and 3.");
                    Helper.Error(message);
                    Start();
                }
                break;
            }
            catch (FormatException)
            {
                string message = ("Please enter a valid number between 1 and 3.");
                Helper.Error(message);
                Start();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Helper.Error(message);
                Start();
            }
        }
            if (choice == 1) 
            {
                Console.Clear();
                MenuChoices.DisplayMenuOptions();
                Console.WriteLine("\nClick enter to go back");
                Console.ReadLine();
                Console.Clear();
            }
            if (choice == 2) 
            {
                Console.Clear();
                OrderFood.Start();
            }
            if (choice == 3) 
            {
                MainMenu.LoggedInUser();
            }
        }
        

    public static void WriteLogo()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"  _____               _ __  __                  
 |  ___|__   ___   __| |  \/  | ___ _ __  _   _ 
 | |_ / _ \ / _ \ / _` | |\/| |/ _ \ '_ \| | | |
 |  _| (_) | (_) | (_| | |  | |  __/ | | | |_| |
 |_|  \___/ \___/ \__,_|_|  |_|\___|_| |_|\__,_|
                                                                                                
");
        Console.ResetColor();

        
    }
}
