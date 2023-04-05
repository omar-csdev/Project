using System.Drawing;
using Console = Colorful.Console;

static class MainMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        for (; ; )
        {
            Console.Clear();
            WriteLogo();
            Say("1", "Make a Reservation");
            Say("2", "View Food Menu");
            Say("3", "Admin Login");
            Say("4", "Quit");
            Say("5", "Add menu item");
            string input = Console.ReadLine();
            if (input == "1")
            {
                // Make a reservation
            }
            else if (input == "2")
            {
                Console.Clear();
                FoodMenu.Start();
            }
            else if (input == "3")
            {
                AdminDashboard.DisplayDashboard();
                Environment.Exit(1);
                // Admin Login
            }
            else if (input == "4")
            {
                // Quit
                Environment.Exit(1);
            }
            else if (input == "5")
            {
                Console.WriteLine("Enter Item name ('Food' or 'Drink'):\n\r");
                string itemName = Console.ReadLine();
                Console.WriteLine("Enter Item price:\r\n");
                int inputPrice;
                bool isValidInput = int.TryParse(Console.ReadLine(), out inputPrice);
                if (isValidInput)
                {
                    if (itemName == "Food")
                    {
                        Console.WriteLine("Enter food name:\n\r");
                        string foodName = Console.ReadLine();
                        Food newFood = new Food(foodName, inputPrice, "Food");
                        JSONEditor.AddItem(newFood);
                    }
                    else if (itemName == "Drink")
                    {
                        Console.WriteLine("Enter drink name:\n\r");
                        string drinkName = Console.ReadLine();
                        Drink newDrink = new Drink(drinkName, inputPrice, "Drink");
                        JSONEditor.AddItem(newDrink);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter either 'Food' or 'Drink'.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);

                Thread.Sleep(3000);
            }
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
        string logo = @"  ____           _                              _   
 |  _ \ ___  ___| |_ __ _ _   _ _ __ __ _ _ __ | |_ 
 | |_) / _ \/ __| __/ _` | | | | '__/ _` | '_ \| __|
 |  _ <  __/\__ \ || (_| | |_| | | | (_| | | | | |_ 
 |_| \_\___||___/\__\__,_|\__,_|_|  \__,_|_| |_|\__|
                                                    
";

        Console.WriteLine(logo, Color.Wheat);
    }
}