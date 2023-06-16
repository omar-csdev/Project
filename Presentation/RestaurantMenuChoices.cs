using Newtonsoft.Json;
using Project.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public class MenuChoices
{
    public static Item[] menu = new Item[0];

    // Method to display menu options based on user role
    static public void DisplayMenuOptions(bool isGuest)
    {
        int choice;
        Console.Clear();  // Clearing the console screen
        WriteLogo();  // Calling the WriteLogo method to display a logo
        AdminMenuView.WriteToConsole(1, "View Whole Menu");  
        AdminMenuView.WriteToConsole(2, "View Menu by Type");  
        AdminMenuView.WriteToConsole(3, "View Menu by Catagory");  
        AdminMenuView.WriteToConsole(4, "Back to Menu Dashboard");  

        while (true)
        {
            try
            {
                choice = int.Parse(Console.ReadLine());  // Reading the user's choice from the console
                if (choice < 1 || choice > 4)
                {
                    string message = "Please enter a valid number between 1 and 3.";  // Error message for invalid input
                    Helper.Error(message);  // Displaying the error message
                    DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                }
                break;
            }
            catch (FormatException)
            {
                string message = "Please enter a valid number between 1 and 3.";  // Error message for invalid format
                Helper.Error(message);  // Displaying the error message
                DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
            }
            catch (Exception ex)
            {
                string message = ex.Message;  // Error message for other exceptions
                Helper.Error(message);  // Displaying the error message
                DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
            }
        }

        if (choice == 1)
        {
            int sorttype = MenuItem.AskSort();
            MenuItem.DisplayFullMenu(sorttype);  // Calling the method to display the full menu
            Helper.ContinueDisplay();  // Prompting the user to continue
            DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
        }
        else if (choice == 2)
        {
            AdminMenuView.WriteToConsole(1, "Food");  // Displaying the sub-menu option to view food items
            AdminMenuView.WriteToConsole(2, "Drink");  // Displaying the sub-menu option to view drink items
            int type;

            while (true)
            {
                try
                {
                    type = int.Parse(Console.ReadLine());  // Reading the user's choice for type from the console
                    if (type < 1 || type > 2)
                    {
                        string message = "Please enter a valid number between 1 and 2.";  // Error message for invalid input
                        Helper.Error(message);  // Displaying the error message
                        DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                    }
                    break;
                }
                catch (FormatException)
                {
                    string message = "Please enter a valid number between 1 and 2.";  // Error message for invalid format
                    Helper.Error(message);  // Displaying the error message
                    DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                }
                catch (Exception ex)
                {
                    string message = ex.Message;  // Error message for other exceptions
                    Helper.Error(message);  // Displaying the error message
                    DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                }
            }

            if (type == 1)
            {
                int sorttype = MenuItem.AskSort();
                MenuItem.DisplayFood(sorttype);  // Calling the method to display food items
                Helper.ContinueDisplay();  // Prompting the user to continue
                DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
            }
            else if (type == 2)
            {
                int sorttype = MenuItem.AskSort();
                MenuItem.DisplayDrink(sorttype);  // Calling the method to display drink items
                Helper.ContinueDisplay();  // Prompting the user to continue
                DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
            }
            else
            {
                string message = "Error! Please choose a valid option!";  // Error message for invalid option
                Helper.Error(message);  // Displaying the error message
            }
        }
        else if (choice == 3)
        {
            AdminMenuView.WriteToConsole(1, "General");  // Displaying the sub-menu option to view general category
            AdminMenuView.WriteToConsole(2, "Alcoholic");  // Displaying the sub-menu option to view alcoholic category
            AdminMenuView.WriteToConsole(3, "Non-Alcoholic");  // Displaying the sub-menu option to view non-alcoholic category
            AdminMenuView.WriteToConsole(4, "Halal");  // Displaying the sub-menu option to view halal category
            AdminMenuView.WriteToConsole(5, "Vega");  // Displaying the sub-menu option to view vega category
            AdminMenuView.WriteToConsole(6, "Vegan");  // Displaying the sub-menu option to view vegan category
            int type;

            while (true)
            {
                try
                {
                    type = int.Parse(Console.ReadLine());  // Reading the user's choice for type from the console
                    if (type < 1 || type > 6)
                    {
                        string message = "Please enter a valid number between 1 and 6.";  // Error message for invalid input
                        Helper.Error(message);  // Displaying the error message
                        DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                    }
                    break;
                }
                catch (FormatException)
                {
                    string message = "Please enter a valid number between 1 and 6.";  // Error message for invalid format
                    Helper.Error(message);  // Displaying the error message
                    DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                }
                catch (Exception ex)
                {
                    string message = ex.Message;  // Error message for other exceptions
                    Helper.Error(message);  // Displaying the error message
                    DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                }
            }
            int sorttype = MenuItem.AskSort();
            switch (type)
            {
                case 1:
                    MenuItem.DisplayGeneral(sorttype);  // Calling the method to display general category items
                    Helper.ContinueDisplay();  // Prompting the user to continue
                    DisplayMenuOptions(isGuest);  // Calling the DisplayMenuOptions method recursively to prompt for input again
                    break;
                case 2:
                    MenuItem.DisplayAlcoholic(sorttype);  // Calling the method to display alcoholic category items
                    Helper.ContinueDisplay();  
                    DisplayMenuOptions(isGuest);  
                    break;
                case 3:
                    MenuItem.DisplayNonAlcoholic(sorttype);  // Calling the method to display non-alcoholic category items
                    Helper.ContinueDisplay();  
                    DisplayMenuOptions(isGuest);  
                    break;
                case 4:
                    MenuItem.DisplayHalal(sorttype);  // Calling the method to display halal category items
                    Helper.ContinueDisplay();  
                    DisplayMenuOptions(isGuest);  
                    break;
                case 5:
                    MenuItem.DisplayVega(sorttype);  // Calling the method to display vega category items
                    Helper.ContinueDisplay();  
                    DisplayMenuOptions(isGuest);  
                    break;
                case 6:
                    MenuItem.DisplayVegan(sorttype);  // Calling the method to display vegan category items
                    Helper.ContinueDisplay();  
                    DisplayMenuOptions(isGuest);  
                    break;
                default:
                    string message = "Error! Please choose a valid option!";  // Error message for invalid option
                    Helper.Error(message);  // Displaying the error message
                    break;
            }
        }
        else if (choice == 4)
        {
            FoodMenu.Start(isGuest);
        }
        else
        {
            string message = "Error! Please choose a valid option!";  // Error message for invalid option
            Helper.Error(message);  // Displaying the error message
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

