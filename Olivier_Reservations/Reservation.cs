using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Data;

namespace Project.Olivier_Reservations {
    internal class Reservations
    {

        public static void Reservationstart()
        {

            ReservationSystem system = new ReservationSystem();

            string name;
            while (true)
            {

                Console.WriteLine("Enter your first name for your reservation: ");
                // Input checks
                try
                {
                    name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Name cannot be empty or null, please enter a valid name.");
                        Console.ResetColor();
                    }
                    else if (name.Any(char.IsDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Name cannot contain numbers, please enter a valid name.");
                        Console.ResetColor();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }

            string lastname;
            while (true)
            {

                Console.WriteLine("Enter your last name for your reservation: ");
                // Input checks
                try
                {
                    lastname = Console.ReadLine();
                    if (string.IsNullOrEmpty(lastname))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Last name cannot be empty or null, please enter a valid last name.");
                        Console.ResetColor();
                    }
                    else if (lastname.Any(char.IsDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Last name cannot contain numbers, please enter a valid last name.");
                        Console.ResetColor();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }

            DateTime twoWeeksAway = DateTime.Today.AddDays(14);

            Console.WriteLine($"Choose a reservation date by entering a date in the following format (dd-mm-yyyy). The latest date you can book is: {twoWeeksAway:dd-MM-yyyy}");

            string inputDate;
            DateTime reservationDate;

            while (true)
            {
                try
                {
                    Console.Write("Enter reservation date: ");
                    inputDate = Console.ReadLine();

                    reservationDate = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

                    if (reservationDate == DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Reservations for the current day cannot be made. Please enter a date in the future.");
                        Console.ResetColor();
                    }

                    if (reservationDate > twoWeeksAway)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception($"Reservation date must be on or before {twoWeeksAway:dd-MM-yyyy}. Please enter a valid reservation date.");
                        Console.ResetColor();
                    }

                    if (reservationDate < DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception($"Reservation date must be on or before {twoWeeksAway:dd-MM-yyyy}. Please enter a valid reservation date.");
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
                    timeSlot = new DateTime(reservationDate.Year, reservationDate.Month, reservationDate.Day, timeSlotTime3.Hours, timeSlotTime3.Minutes, timeSlotTime3.Seconds);
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
            // If restaurant is fully booked for your timeslot you get notified 
            if (totalGuests == 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("We are fully booked at this time");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);

            }

            int totalCapacity = 100;
            int maxGuests = totalGuests > 90 ? totalCapacity - totalGuests : 10;
            Console.WriteLine($"Enter the size of your group (1-{maxGuests}): ");
            int groupSize;
            // Input checks
            while (true)
            {
                try
                {
                    groupSize = int.Parse(Console.ReadLine());
                    if (groupSize < 1 || groupSize > maxGuests)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Enter a number between 1 and {maxGuests} as you're not allowed to make a reservation for a party of this size.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid number between 1 and 10 to make your reservation.");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }




            bool success = system.MakeReservation(name, lastname, groupSize, timeSlot);
            if (success)
            {
                SaveReservations.WriteAll(system.reservations);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }

        }
    }

    public class Reservation
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int groupSize { get; set; }
        public string Code { get; set; }
        public DateTime TimeSlot { get; set; }
    }


    public class ReservationSystem
    {

        public List<Reservation> reservations = new List<Reservation>();
        public string GenerateRandomCode()
        {
            // Define the sets of letters and digits that can be used to generate the code.
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";

            // Create a new random number generator.
            Random random = new Random();

            // Generate the random code by concatenating the letters and digits in a fixed order.
            string randomString = string.Format("{0}{1}{2}{3}{4}{5}",
                letters[random.Next(letters.Length)],
                letters[random.Next(letters.Length)],
                digits[random.Next(digits.Length)],
                digits[random.Next(digits.Length)],
                digits[random.Next(digits.Length)],
                digits[random.Next(digits.Length)]);

            // Return the random code.
            return randomString;
        }
        public bool MakeReservation(string name, string lastname, int groupSize, DateTime timeSlot)
        {
            string code = GenerateRandomCode();
            // Add reservation to the list
            reservations.Add(new Reservation { Name = name, LastName = lastname, groupSize = groupSize, TimeSlot = timeSlot, Code = code });

            Console.WriteLine($"Reservation made for {groupSize} people on {timeSlot:dd-MM-yyyy} at {timeSlot:HH:mm} under the name {name} {lastname}.");
            Console.Write($"Reservation code: ");
            //write code in red
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{code}.");
            Console.ResetColor();
            Console.Write("Please keep this code for future use.\n");

            return true;
        }

    }

    public static class SaveReservations
    {

        public static List<Reservation> LoadAll()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");
            string JSONString = File.ReadAllText(filePath);

            List<Reservation> Allreservations = JsonConvert.DeserializeObject<List<Reservation>>(JSONString) ?? new List<Reservation>();
            return Allreservations;
        }


        public static void WriteAll(List<Reservation> NewReservations)
        {

            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");

            string jsonString = File.ReadAllText(filePath);

            List<Reservation> existingReservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();

            for (int i = existingReservations.Count - 1; i >= 0; i--)
            {
                if (existingReservations[i].TimeSlot < DateTime.Now)
                {
                    existingReservations.RemoveAt(i);
                }
            }

            existingReservations.AddRange(NewReservations);

            string updatedJSONString = JsonConvert.SerializeObject(existingReservations, Formatting.Indented);

            File.WriteAllText(filePath, updatedJSONString);
        }
    }
}