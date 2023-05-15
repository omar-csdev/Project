using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;
using Project.Olivier_Reservations;

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
                            //Project.Olivier_Reservations.AdminReservations.AdminReservationstart();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }

                }
                else if (input == "2")
                {
                    while(true){
                        Console.WriteLine("Enter the code of the reservation you want to delete: ");
                        string ? code = Console.ReadLine();
                        if (code == null)
                        {
                            Console.WriteLine("Error! Please enter a valid code!", Color.Red);
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return...");
                            Console.ReadKey();
                        }
                        else
                        {
                            bool succes = ReservationsEditor.RemoveReservation(code);
                            if (succes)
                            {
                                Console.WriteLine("Reservation removed successfully.");
                                Console.WriteLine();
                                Console.WriteLine("Press any key to return...");
                                Console.ReadKey();
                                AdminReservationsEditor.Run();
                            }
                            else
                            {
                                Console.WriteLine($"No reservation found under: {code} !", Color.Red);
                                Console.WriteLine();
                                Console.WriteLine("Press any key to return...");
                                Console.ReadKey();
                                AdminReservationsEditor.Run();
                            }
                        }

                    }
                }
                else if (input == "3")
                {
                    while (true)
                    {
                        Console.WriteLine("Enter the code of the reservation you want to edit: ");
                        string? code = Console.ReadLine();
                        if (code == null)
                        {
                            Console.WriteLine("Error! Please enter a valid code!", Color.Red);
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Project.Olivier_Reservations.Reservation reservationToUpdate = reservations.FirstOrDefault(r => r.Code == code);
                            if(reservations != null)
                            {
                                Console.WriteLine("Reservation Name: " + reservationToUpdate.Name);
                                Console.WriteLine("Reservation lastname: " + reservationToUpdate.LastName);
                                Console.WriteLine("Reservation group size: " + reservationToUpdate.groupSize);

                                while (true)
                                {

                                    try
                                    {
                                        Console.WriteLine("Enter the new name (press Enter to keep the old name): ");
                                        string ? newName = Console.ReadLine();
                                        newName = newName == "" ? reservationToUpdate.Name : newName;


                                        Console.WriteLine("Enter the new lastname (press Enter to keep the old lastname): ");
                                        string? newLastName = Console.ReadLine();
                                        newLastName = newLastName == "" ? reservationToUpdate.LastName : newLastName;

                                        Console.WriteLine("Enter the new group size (press Enter to keep the old group size): ");
                                        string ? newPartySize = Console.ReadLine();
                                        bool success = int.TryParse(newPartySize, out int number);
                                        if (success || newPartySize == "")
                                        {
                                            reservationToUpdate.Name = newName;
                                            reservationToUpdate.LastName = newLastName;
                                            reservationToUpdate.PartySize = newPartySize == "" ? reservationToUpdate.PartySize : number ;
                                            bool successfullyUpdated = ReservationsEditor.UpdateReservation(reservationToUpdate);
                                            if (successfullyUpdated)
                                            {
                                                Console.WriteLine("Reservation updated successfully.");
                                                Console.WriteLine();
                                                Console.WriteLine("Press any key to return...");
                                                Console.ReadKey();
                                                AdminReservationsEditor.Run();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Something went wrong updating the reservation!", Color.Red);
                                                Console.WriteLine();
                                                Console.WriteLine("Press any key to return...");
                                                Console.ReadKey();
                                                AdminReservationsEditor.Run();
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Error! Please enter a valid number!", Color.Red);
                                            Console.WriteLine();
                                            Console.WriteLine("Press any key to return...");
                                            Console.ReadKey();

                                        }
                                       
                                    }
                                    catch(Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }   
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No reservation found under: {code} !", Color.Red);
                                Console.WriteLine();
                                Console.WriteLine("Press any key to return...");
                                Console.ReadKey();
                                AdminReservationsEditor.Run();
                            }
                        }

                    }
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
