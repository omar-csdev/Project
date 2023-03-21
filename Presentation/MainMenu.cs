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
            Say("2", "View Menu");
            Say("3", "Admin Login");
            Say("4", "Quit");
            string input = Console.ReadLine();
            if (input == "1")
            {
                // Make a reservation
            }
            else if (input == "2")
            {
                // View Menu
            }
            else if (input == "3")
            {
                // Admin Login
            }
            else if (input == "4")
            {
                // Quit
                Environment.Exit(1);
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
        string logo = @"  ____           _                              _   
 |  _ \ ___  ___| |_ __ _ _   _ _ __ __ _ _ __ | |_ 
 | |_) / _ \/ __| __/ _` | | | | '__/ _` | '_ \| __|
 |  _ <  __/\__ \ || (_| | |_| | | | (_| | | | | |_ 
 |_| \_\___||___/\__\__,_|\__,_|_|  \__,_|_| |_|\__|
                                                    
";

        Console.WriteLine(logo, Color.Wheat);
    }
}