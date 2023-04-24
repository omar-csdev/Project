using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

static class AdminReservationsView
{

    public static List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();

    static public void Run()
    {
        for (; ; )
        {   
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "View All Reservations");
            WriteToConsole(2, "View Reservations by Date");
            WriteToConsole(3, "View Reservations by Name");
            WriteToConsole(4, "View Reservation by Code");
            WriteToConsole(5, "Back to Reservations Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                foreach(Project.Olivier_Reservations.Reservation reservation in reservations)
                {
                    Console.WriteLine("Name: " + reservation.Name);
                    Console.WriteLine("Party size: " + reservation.PartySize);
                    Console.WriteLine("Time slot: " + reservation.TimeSlot);
                    Console.WriteLine();
                }
                Console.WriteLine("Press enter to return...");
                Console.ReadKey();
                Console.Clear();
                AdminReservationsView.Run();
            }
            else if (input == 2)
            {
                Console.WriteLine("THIS FEATURE IS NOT YET IMPLEMENTED", Color.Blue);
                Thread.Sleep(1500);
                AdminReservationsView.Run();
            }
            else if (input == 3)
            {
                Console.WriteLine("THIS FEATURE IS NOT YET IMPLEMENTED", Color.Blue);
                Thread.Sleep(1500);
                AdminReservationsView.Run();
            }
            else if (input == 4)
            {
                Console.WriteLine("THIS FEATURE IS NOT YET IMPLEMENTED", Color.Blue);
                Thread.Sleep(1500);
                AdminReservationsView.Run();
            }
            else if (input == 5)
            {
                AdminDashboardReservationsDashboard.DisplayReservationsDashboard();
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
  ____                                _   _                   ____  _           _             
 |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __  ___  |  _ \(_)___ _ __ | | __ _ _   _ 
 | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __| | | | | / __| '_ \| |/ _` | | | |
 |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \ | |_| | \__ \ |_) | | (_| | |_| |
 |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/ |____/|_|___/ .__/|_|\__,_|\__, |
                                                                         |_|            |___/                   
";

        Console.WriteLine(logo, Color.Wheat);
    }
}