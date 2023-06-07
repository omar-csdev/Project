using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class AdminOldReservationsView
{
    public static List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();

    public static void ViewOldReservations()
    {
        Console.WriteLine("1, View All Old Reservations");
        Console.WriteLine("2, View Reservations from 1 year ago");
        Console.WriteLine("3, View Reservations from 2 years ago");
        Console.WriteLine("4, View Reservations from 3 years ago");
        Console.WriteLine("5, View Reservations from 4 years ago");
        Console.WriteLine("6, View Reservations from 5 years ago");
        Console.WriteLine("7, View Reservations from more than 5 years ago");

        int choice;
        // Input checks
        while (true)
        {
            try
            {
                choice = int.Parse(Console.ReadLine());
                if (choice < 1 || choice > 7)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Enter a number between 1 and 4.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number between 1 and 4.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        switch (choice)
        {
            case 1:
                string filePath2 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1YearAgo.json");

                break;

        }

    }

   

}

