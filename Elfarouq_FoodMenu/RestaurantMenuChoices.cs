using Menu_item_creëren;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MenuChoices
{
    public static Item[] menu = new Item[0];

    static public void DisplayMenuOptions()
    {
        int choice;
        Console.Clear();
        WriteLogo();
        AdminMenuView.WriteToConsole(1, "View Whole Menu");
        AdminMenuView.WriteToConsole(2, "View Menu by Type");
        AdminMenuView.WriteToConsole(3, "View Menu by Catagory");
        AdminMenuView.WriteToConsole(4, "Back to Menu Dashboard");
        while (true)
        {
            try
            {
                choice = int.Parse(Console.ReadLine());
                if (choice < 1 || choice > 3)
                {
                    string message = ("Please enter a valid number between 1 and 3.");
                    Helper.Error(message);
                    DisplayMenuOptions();
                }
                break;
            }
            catch (FormatException)
            {
                string message = ("Please enter a valid number between 1 and 3.");
                Helper.Error(message);
                DisplayMenuOptions();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Helper.Error(message);
                DisplayMenuOptions();
            }
        }
        if (choice == 1)
        {
            MenuItem.DisplayFullMenu();
            Console.WriteLine("\nClick enter to go back");
            Console.ReadLine();

        }
        else if (choice == 2)
        {
            AdminMenuView.WriteToConsole(1, "Food");
            AdminMenuView.WriteToConsole(2, "Drink");
            int type;

            while (true)
            {
                try
                {
                    type = int.Parse(Console.ReadLine());
                    if (type < 1 || type > 3)
                    {
                        string message = ("Please enter a valid number between 1 and 2.");
                        Helper.Error(message);
                        DisplayMenuOptions();
                    }
                    break;
                }
                catch (FormatException)
                {
                    string message = ("Please enter a valid number between 1 and 2.");
                    Helper.Error(message);
                    DisplayMenuOptions();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    Helper.Error(message);
                    DisplayMenuOptions();
                }
            }
            if (type == 1)
            {
                MenuItem.DisplayFood();
                Console.WriteLine("\nClick enter to go back");
                Console.ReadLine();

            }
            else if (type == 2)
            {
                MenuItem.DisplayDrink();
                Console.WriteLine("\nClick enter to go back");
                Console.ReadLine();


            }
            else
            {
                string message = "Error! Please choose a valid option!";
                Helper.Error(message);

            }
        }
        else if (choice == 3)
        {
            AdminMenuView.WriteToConsole(1, "General");
            AdminMenuView.WriteToConsole(2, "Alcoholic");
            AdminMenuView.WriteToConsole(3, "Non-Alcoholic");
            AdminMenuView.WriteToConsole(4, "Halal");
            AdminMenuView.WriteToConsole(5, "Vega");
            AdminMenuView.WriteToConsole(6, "Vegan");
            int type;
            while (true)
            {
                try
                {
                    type = int.Parse(Console.ReadLine());
                    if (type < 1 || type > 6)
                    {
                        string message = ("Please enter a valid number between 1 and 6.");
                        Helper.Error(message);
                        DisplayMenuOptions();
                    }
                    break;
                }
                catch (FormatException)
                {
                    string message = ("Please enter a valid number between 1 and 6.");
                    Helper.Error(message);
                    DisplayMenuOptions();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    Helper.Error(message);
                    DisplayMenuOptions();
                }
            }
            switch (type)
            {
                case 1:
                    MenuItem.DisplayGeneral();
                    Console.WriteLine("\nClick enter to go back");
                    Console.ReadLine();
                    break;
                case 2:

                    MenuItem.DisplayAlcoholic();
                    Console.WriteLine("\nClick enter to go back");
                    Console.ReadLine();
                    break;
                case 3:
                    MenuItem.DisplayNonAlcoholic();
                    Console.WriteLine("\nClick enter to go back");
                    Console.ReadLine();
                    break;
                case 4:
                    MenuItem.DisplayHalal();
                    Console.WriteLine("\nClick enter to go back");
                    Console.ReadLine();
                    break;
                case 5:
                    MenuItem.DisplayVega();
                    Console.WriteLine("\nClick enter to go back");
                    Console.ReadLine();
                    break;
                case 6:
                    MenuItem.DisplayVegan();
                    Console.WriteLine("\nClick enter to go back");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Unknown command please try again");
                    break;
            }
        }
        else if (choice == 4)
        {
            FoodMenu.Start();
        }
        else
        {
            string message = "Error! Please choose a valid option!";
            Helper.Error(message);
        }
    }
    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }
    public static void WriteLogo()
    {
        string logo = @"
  __  __                    ____  _           _             
 |  \/  | ___ _ __  _   _  |  _ \(_)___ _ __ | | __ _ _   _ 
 | |\/| |/ _ \ '_ \| | | | | | | | / __| '_ \| |/ _` | | | |
 | |  | |  __/ | | | |_| | | |_| | \__ \ |_) | | (_| | |_| |
 |_|  |_|\___|_| |_|\__,_| |____/|_|___/ .__/|_|\__,_|\__, |
                                       |_|            |___/ 
                     
";

        Console.WriteLine(logo, Color.Wheat);
    }
}

