using Project.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic
{
    internal class AdminAvailableSeatsOnDate
    {
        public static void CheckSeats()
        {

            DateTime twoWeeksAway = DateTime.Today.AddDays(14);

            Console.WriteLine($"Choose a reservation date by entering a date in the following format (dd-mm-yyyy). The latest reservation date available is: {twoWeeksAway:dd-MM-yyyy}");

            string inputDate;
            DateTime reservationDate;

            while (true)
            {
                try
                {
                    Console.Write("Enter reservation date: ");
                    inputDate = Console.ReadLine();

                    reservationDate = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

                    if (reservationDate > twoWeeksAway)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception($"There are no reservations past: {twoWeeksAway:dd-MM-yyyy}. Please enter a valid reservation date.");
                        Console.ResetColor();
                    }

                    if (reservationDate < DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception($"Please enter a valid reservation date, this date has already passed.");
                        Console.ResetColor();
                    }
                    break;

                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please enter a valid date in the format (dd-mm-yyyy).");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }

            Console.WriteLine($"Reservation date set to: {reservationDate:dd-MM-yyyy}");


            Console.WriteLine("Choose a reservation time:");
            Console.WriteLine("1. 12:30-15:00");
            Console.WriteLine("2. 15:00-17:30");
            Console.WriteLine("3. 17:30-20:00");
            Console.WriteLine("4. 20:00-22:30");
            Console.Write("Enter your choice (1-4): ");

            int choice;
            // Input checks
            while (true)
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice < 1 || choice > 4)
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

            DateTime timeSlot;
            TimeSpan timeSlotTime1 = new TimeSpan(12, 30, 0);
            TimeSpan timeSlotTime2 = new TimeSpan(15, 00, 0);
            TimeSpan timeSlotTime3 = new TimeSpan(17, 30, 0);
            TimeSpan timeSlotTime4 = new TimeSpan(20, 00, 0);
            switch (choice)
            {
                case 1:
                    timeSlot = new DateTime(reservationDate.Year, reservationDate.Month, reservationDate.Day, timeSlotTime1.Hours, timeSlotTime1.Minutes, timeSlotTime1.Seconds);
                    break;
                case 2:
                    timeSlot = new DateTime(reservationDate.Year, reservationDate.Month, reservationDate.Day, timeSlotTime2.Hours, timeSlotTime2.Minutes, timeSlotTime2.Seconds);
                    break;
                case 3:
                    timeSlot = new DateTime(reservationDate.Year, reservationDate.Month, reservationDate.Day, timeSlotTime3.Hours, timeSlotTime3.Minutes, timeSlotTime3.Seconds);
                    break;
                case 4:
                    timeSlot = new DateTime(reservationDate.Year, reservationDate.Month, reservationDate.Day, timeSlotTime4.Hours, timeSlotTime4.Minutes, timeSlotTime4.Seconds);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            // load reservations and get total party size seperated by timeslot and date.
            List<Reservation> reservations = SaveReservations.LoadAll();
            int totalGuests = 0;
            foreach (Reservation reservation in reservations)
            {
                if (reservation.TimeSlot.Day == timeSlot.Day && reservation.TimeSlot.TimeOfDay == timeSlot.TimeOfDay)
                {
                    totalGuests += reservation.groupSize;
                }

            }
            int RemainingSeats = 100 - totalGuests;

            Console.WriteLine($"The amount of remaining seats for this date is: {RemainingSeats}.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
