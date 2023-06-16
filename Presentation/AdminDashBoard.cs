using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

public static class AdminDashboard
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static void DisplayDashboard()
    {
        while(true)
        {
            try
            {
                Console.Clear();
                WriteLogo();
                WriteToConsole(1, "Reservations");
                WriteToConsole(2, "Menu");
                WriteToConsole(3, "Manage Accounts");
                WriteToConsole(4, "Manage statistics");
                WriteToConsole(5, "Log out");
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    AdminDashboardReservationsDashboard.DisplayReservationsDashboard();
                }
                else if (input == 2)
                {
                    AdminDashboardMenuDashboard.DisplayMenuDashboard();
                }
                else if (input == 3)
                {
                    AdminManager.Start();
                }
                else if (input == 4)
                {
                    StatisticsMenu.Start();
                }
                else if (input == 5)
                {
                    // Log out
                    List<Admin> test = LoginAccess.LoadAll();
                    foreach (Admin admin in test)
                    {
                        admin.IsLoggedIn = false;
                    }
                    LoginAccess.WriteAll(test);

                    Console.Clear();
                    Console.WriteLine("Logged out successfully!");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    AdminLogin.Start();
                }
                else
                {
                    throw new Exception("Invalid input, please choose from option 1-5");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.WriteLine();
                Helper.ContinueDisplay();
            }
        }
    }

    public static void WriteToConsole(int prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }

    public static void WriteLogo()
    {
        string logo = @"     _       _           _       
    / \   __| |_ __ ___ (_)_ __  
   / _ \ / _` | '_ ` _ \| | '_ \ 
  / ___ \ (_| | | | | | | | | | |
 /_/   \_\__,_|_| |_| |_|_|_| |_|
                                 ";

        Console.WriteLine(logo, Color.Wheat);
    }

}