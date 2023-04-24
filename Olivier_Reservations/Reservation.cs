using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Project.Olivier_Reservations
{
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
                        throw new Exception("Name cannot be empty or null, please enter a valid name.");
                    }
                    else if (name.Any(char.IsDigit))
                    {
                        throw new Exception("Name cannot contain numbers, please enter a valid name.");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                        throw new Exception("Last name cannot be empty or null, please enter a valid last name.");
                    }
                    else if (lastname.Any(char.IsDigit))
                    {
                        throw new Exception("Last name cannot contain numbers, please enter a valid last name.");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DateTime twoWeeksAway = DateTime.Today.AddDays(14);

            Console.WriteLine($"Choose a reservation date by entering a date in the following format (dd-mm-yyyy). The latest date you can book is: {twoWeeksAway:dd-MM-yyyy}");

            string inputDate;
            DateTime reservationDate;

            do
            {
                Console.Write("Enter reservation date: ");
                inputDate = Console.ReadLine();

                if (!DateTime.TryParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out reservationDate))
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in the format (dd-mm-yyyy).");
                    continue;
                }

                if (reservationDate == DateTime.Today)
                {
                    Console.WriteLine("Reservations for the current day cannot be made. Please enter a date in the future.");
                    continue;
                }

                if (reservationDate > twoWeeksAway)
                {
                    Console.WriteLine($"Reservation date must be on or before {twoWeeksAway:dd-MM-yyyy}. Please enter a valid reservation date.");
                    continue;
                }

            } while (reservationDate == DateTime.MinValue);

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
                        Console.WriteLine($"Enter a number between 1 and 4.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number between 1 and 4.");
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
                if (reservation.TimeSlot.Day == timeSlot.Day)
                {
                    totalGuests += reservation.PartySize;
                }
                
            }
            // If restaurant is fully booked for your timeslot you get notified 
            if (totalGuests == 100)
            {
                System.Console.WriteLine("We are fully booked at this time");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);

            }

            int totalCapacity = 100;
            int maxGuests = totalGuests > 90 ? totalCapacity - totalGuests : 10;
            Console.WriteLine($"Enter the size of your party (1-{maxGuests}): ");
            int partySize;
            // Input checks
            while (true)
            {
                try
                {
                    partySize = int.Parse(Console.ReadLine());
                    if (partySize < 1 || partySize > maxGuests)
                    {
                        Console.WriteLine($"Enter a number between 1 and {maxGuests} as you're not allowed to make a reservation for a party of this size.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number between 1 and 10 to make your reservation.");
                }
            }


            

            bool success = system.MakeReservation(name, lastname, partySize, timeSlot);
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
        public int PartySize { get; set; }
        public string Code { get; set; }
        public DateTime TimeSlot { get; set; }
    }


    public class ReservationSystem
    {

        public List<Reservation> reservations = new List<Reservation>();
        public string GenerateRandomString()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";

            Random random = new Random();

            char[] chars = new char[6];
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    chars[i] = letters[random.Next(letters.Length)];
                }
                else
                {
                    chars[i] = digits[random.Next(digits.Length)];
                }
            }

            string randomString = new string(chars);
            return randomString;
        }
        public bool MakeReservation(string name, string lastname,int partySize, DateTime timeSlot)
        {
            string code = GenerateRandomString();
            // Add reservation to the list
            reservations.Add(new Reservation { Name = name, LastName = lastname, PartySize = partySize, TimeSlot = timeSlot, Code = code});

            Console.WriteLine($"Reservation made for {partySize} people on {timeSlot:dd-MM-yyyy} at {timeSlot:t} under the name {name} {lastname}.");
            Console.WriteLine($"Reservation code: {code}");
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

            existingReservations.AddRange(NewReservations);

            string updatedJSONString = JsonConvert.SerializeObject(existingReservations, Formatting.Indented);

            File.WriteAllText(filePath, updatedJSONString);
        }
    }
}
