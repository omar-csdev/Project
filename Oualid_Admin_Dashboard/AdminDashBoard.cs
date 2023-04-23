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
        for (; ; )
        {
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "Reservations");
            WriteToConsole(2, "Menu");
            WriteToConsole(3, "Log out");
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
                //Log out
                List<Admin> test = LoginAccess.LoadAll("admindata.json");
                foreach (Admin admin in test)
                {
                    admin.IsLoggedIn = false;
                }
                LoginAccess.WriteAll(test);
                
                Console.Clear();
                Console.WriteLine("Logged out succesfully!");
                Thread.Sleep(1500);
                AdminLogin.Start();
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                Thread.Sleep(1500);
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