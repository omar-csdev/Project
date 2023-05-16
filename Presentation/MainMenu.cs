using Project.Olivier_Reservations;
using System.Drawing;
using Console = Colorful.Console;

static class MainMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    public static void NewStart()
    {
        int choice;

        while (true)
        {
            Console.Clear();
            WriteLogo();
            Say("1", "Customer Login");
            Say("2", "Continue as Guest");
            Say("3", "Create Account");
            Say("4", "Admin");
            Say("5", "Exit");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Handle Option 1
                    //Sign the User in

                    //SignIn();
                    //invoke LoggedInUser();
                    LoggedInUser();
                    break;
                case 2:
                    // Handle Option 2
                    break;
                case 3:
                    // Handle Option 3
                    break;
                case 4:
                    // Handle Option 4
                    AdminLogin.Start();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    return; // Breaks out of the method, terminating the program
                default:
                    Helper.ErrorDisplay("1, 2, 3, 4 or 5");
                    break;
            }

            Console.WriteLine(); // Empty line for spacing
        }
    }
    public static void LoggedInUser()
    {
        int choice;

        while (true)
        {
            Console.Clear();
            WriteLogo();
            Say("1", "Make a Reservation");
            Say("2", "View Menu");
            Say("3", "Sign Out");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    // Handle Option 1
                    Reservations.Reservationstart();
                    break;
                case 2:
                    // Handle Option 2
                    Console.Clear();
                    FoodMenu.Start();
                    break;
                case 3:
                    //Sign the User out in the JSON
                    // Return the User to the start screen
                    NewStart();
                    break;
                default:
                    Helper.ErrorDisplay("1, 2 or 3");
                    break;
            }

            Console.WriteLine(); // Empty line for spacing
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
        string logo = @"  ____           _                              _   
 |  _ \ ___  ___| |_ __ _ _   _ _ __ __ _ _ __ | |_ 
 | |_) / _ \/ __| __/ _` | | | | '__/ _` | '_ \| __|
 |  _ <  __/\__ \ || (_| | |_| | | | (_| | | | | |_ 
 |_| \_\___||___/\__\__,_|\__,_|_|  \__,_|_| |_|\__|
                                                    
";

        Console.WriteLine(logo, Color.Wheat);
    }
}