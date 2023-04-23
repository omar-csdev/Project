using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

static class AdminDashboardReservationsDashboard
{
    public static Item[] menu = new Item[0];

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void DisplayReservationsDashboard()
    {
        for (; ; )
        {
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "View Reservations");
            WriteToConsole(2, "Edit Reservations");
            WriteToConsole(3, "Back to Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                
            }
            else if (input == 2)
            {
                //
            }
            else if (input == 3)
            {
                AdminDashboard.DisplayDashboard();
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
        string logo = @"
  ____                                _   _                 
 |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __  ___ 
 | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
 |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
 |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/                       
";

        Console.WriteLine(logo, Color.Wheat);
    }
}