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
        for (; ; )
        {
            Console.Clear();
            WriteLogo();
            AdminMenuView.WriteToConsole(1, "View Whole Menu");
            AdminMenuView.WriteToConsole(2, "View Menu by Type");
            AdminMenuView.WriteToConsole(3, "View Menu by Catagory");
            AdminMenuView.WriteToConsole(4, "Back to Menu Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                MenuItem.DisplayFullMenu();
                Console.WriteLine("\nClick enter to go back");
                Console.ReadLine();

            }
            else if (input == 2)
            {
                AdminMenuView.WriteToConsole(1, "Food");
                AdminMenuView.WriteToConsole(2, "Drink");
                string type = Convert.ToInt32(Console.ReadLine()) == 1 ? "Food" : "Drink";

                for (; ; )
                {
                    if (type.ToLower() == "food")
                    {
                        MenuItem.DisplayFood();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else if (type.ToLower() == "drink")
                    {
                        MenuItem.DisplayDrink();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                        Thread.Sleep(1500);
                    }
                }
            }
            else if (input == 3)
            {
                AdminMenuView.WriteToConsole(1, "General");
                AdminMenuView.WriteToConsole(2, "Alcoholic");
                AdminMenuView.WriteToConsole(3, "Non-Alcoholic");
                AdminMenuView.WriteToConsole(4, "Halal");
                AdminMenuView.WriteToConsole(5, "Vega");
                AdminMenuView.WriteToConsole(6, "Vegan");
                int type = Convert.ToInt32(Console.ReadLine());

                for (; ; )
                {
                    if (type == 1)
                    {
                        MenuItem.DisplayGeneral();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else if (type == 2)
                    {
                        MenuItem.DisplayAlcoholic();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else if (type == 3)
                    {
                        MenuItem.DisplayNonAlcoholic();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else if (type == 4)
                    {
                        MenuItem.DisplayHalal();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else if (type == 5)
                    {
                        MenuItem.DisplayVega();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else if (type == 6)
                    {
                        MenuItem.DisplayVegan();
                        Console.WriteLine("\nClick enter to go back");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                        Thread.Sleep(1500);
                    }
                }
            }
            else if (input == 4)
            {
                FoodMenu.Start();
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                Thread.Sleep(1500);
            }
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

