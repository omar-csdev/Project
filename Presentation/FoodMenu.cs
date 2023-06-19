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
    public static void Start(bool isGuest)
    {
        int choice;
        Console.Clear();
        WriteLogo();
        Helper.Say("1", "View menu");
        Helper.Say("2", "Order");
        Helper.Say("3", "Go back");

        while (true)
        {
            try
            {
                choice = int.Parse(Console.ReadLine());

                if (choice < 1 || choice > 3)
                {
                    string message = "Please enter a valid number between 1 and 3.";
                    Helper.Error(message);
                    Start(isGuest);
                }

                break;
            }
            catch (FormatException)
            {
                string message = "Please enter a valid number between 1 and 3.";
                Helper.Error(message);
                Start(isGuest);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Helper.Error(message);
                Start(isGuest);
            }
        }

        if (choice == 1)
        {
            Console.Clear();
            MenuChoices.DisplayMenuOptions(isGuest);
        }
        else if (choice == 2)
        {
            Console.Clear();
            OrderFood.Start(isGuest);
            if (isGuest)
            {
                
                Console.Clear();
                MainMenu.NewStart(isGuest);
            }
        }
        else if (choice == 3)
        {
            if (isGuest)
            {
                MainMenu.NewStart(isGuest);
            }
            else
            {
                CustomerDashboard.DisplayDashboard();
            }
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
