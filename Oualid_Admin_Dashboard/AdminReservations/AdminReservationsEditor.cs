using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

static class AdminReservationsEditor
{

    public static List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();

    static public void Run()
    {
        for (; ; )
        {   
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "Add Reservation");
            WriteToConsole(2, "Delete Reservation");
            WriteToConsole(3, "Edit Reservation");
            WriteToConsole(4, "Back to Reservations Menu");
            string ? input = Console.ReadLine();
            try
            {
                if (input == "1")
                {
                    while (true)
                    {
                        try
                        {
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
                else if (input == "2")
                {
                    Console.WriteLine("THIS FEATURE IS NOT YET IMPLEMENTED", Color.Blue);
                    Thread.Sleep(1500);
                    AdminReservationsEditor.Run();
                }
                else if (input == "3")
                {
                    Console.WriteLine("THIS FEATURE IS NOT YET IMPLEMENTED", Color.Blue);
                    Thread.Sleep(1500);
                    AdminReservationsEditor.Run();
                }
                else if (input == "4")
                {
                    AdminDashboardReservationsDashboard.DisplayReservationsDashboard();
                }
                else
                {
                    Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = Color.Red;
                Console.WriteLine("Something went wrong!" + " " + ex.Message, Color.Red);
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
  ____                                _   _               _____    _ _ _             
 |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __   | ____|__| (_) |_ ___  _ __ 
 | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \  |  _| / _` | | __/ _ \| '__|
 |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | | | |__| (_| | | || (_) | |   
 |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_| |_____\__,_|_|\__\___/|_|                                                                                                    
";

        Console.WriteLine(logo, Color.Wheat);
    }
}