using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Presentation
{
    class Program
    {
        static ReservationSystem system = new ReservationSystem();

        static void Main(string[] args)
        {

            ReservationSystem system = new ReservationSystem();

            string name;
            while (true)
            {
                Console.WriteLine("Enter your name: ");
                try
                {
                    name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        throw new Exception("Name cannot be empty or null.");
                    }
                    else if (name.Any(char.IsDigit))
                    {
                        throw new Exception("Name cannot contain numbers.");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // load reservations and get total party size.
            //list<String> ListGuests = system.LoadReservations();

            int totalCapacity = 100;
            int totalGuests = 0;
            int maxGuests = totalGuests > 90 ? totalCapacity - totalGuests : 10;
            Console.WriteLine($"Enter the size of your party (1-{maxGuests}): ");
            int partySize;
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


            Console.WriteLine("Choose a reservation time:");
            Console.WriteLine("1. 12:30-15:00");
            Console.WriteLine("2. 15:00-17:30");
            Console.WriteLine("3. 17:30-20:00");
            Console.WriteLine("4. 20:00-22:30");
            Console.Write("Enter your choice (1-4): ");

            int choice;
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
            switch (choice)
            {
                case 1:
                    timeSlot = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0);
                    break;
                case 2:
                    timeSlot = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                    break;
                case 3:
                    timeSlot = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0);
                    break;
                case 4:
                    timeSlot = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            bool success = system.MakeReservation(name, partySize, timeSlot);
            if (success)
            {
                system.SaveReservations();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }

        }
    }

    public class Reservation
    {
        public string Name { get; set; }
        public int PartySize { get; set; }
        public DateTime TimeSlot { get; set; }
    }

    public class ReservationSystem
{

    private List<Reservation> reservations = new List<Reservation>();

    public bool MakeReservation(string name, int partySize, DateTime timeSlot)
    {
        // Add reservation to the list
        reservations.Add(new Reservation { Name = name, PartySize = partySize, TimeSlot = timeSlot });

        Console.WriteLine($"Reservation made for {partySize} people at {timeSlot:t} under the name {name}.");
        return true;
    }

    public void SaveReservations()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");

        // Saves new reservation to the json
        var FormatContent = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, FormatContent);
        }
        else
        {
            File.AppendAllText(filePath, FormatContent);
        }
    }

    public void LoadReservations()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");

        if (File.Exists(filePath))
        {   
            // Load all reservations and returns them in a list
            string JustText = File.ReadAllText(filePath);
            List<String> ReservationsList = JsonConvert.DeserializeObject<List<String>>(JustText) ?? new List<String>();
        }
    }
}
}
