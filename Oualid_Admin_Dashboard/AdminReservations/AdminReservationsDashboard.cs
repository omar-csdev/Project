using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;
using Project.Oualid_Admin_Dashboard.AdminReservations;

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
            WriteToConsole(1, "View Available Seats On Date");
            WriteToConsole(2, "View Reservations");
            WriteToConsole(3, "View Old Reservations");
            WriteToConsole(4, "Edit Reservations");
            WriteToConsole(5, "Back to Dashboard");
            string? input = Console.ReadLine();
            if (input == "1")
            {
                AdminAvailableSeatsOnDate.CheckSeats();
            }
            if (input == "2")
            {
                AdminReservationsView.Run();
            }
            else if (input == "3")
            {
                AdminOldReservationsView.ViewOldReservations();
            }
            else if (input == "4")
            {
                AdminReservationsEditor.Run();
            }
            else if (input == "5")
            {
                AdminDashboard.DisplayDashboard();
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                Console.WriteLine();
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
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