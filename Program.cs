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

namespace Project.Presentation
{
    class Program
    {
        static ReservationSystem system = new ReservationSystem();

        static void Main(string[] args)
        {

            ReservationSystem system = new ReservationSystem();

            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the size of your party (1-10): ");
            int partySize = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose a reservation time:");
            Console.WriteLine("1. 12:30-15:00");
            Console.WriteLine("2. 15:00-17:30");
            Console.WriteLine("3. 17:30-20:00");
            Console.WriteLine("4. 20:00-22:30");
            Console.Write("Enter your choice (1-4): ");
            int choice = int.Parse(Console.ReadLine());

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
                // JSON NOT FUNCTIONAL YET
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
    private const int MAX_CAPACITY = 100;
    private const int MAX_PARTY_SIZE = 10;
    private const int MAX_DAYS_AHEAD = 1;

    private List<Reservation> reservations = new List<Reservation>();

    public bool MakeReservation(string name, int partySize, DateTime timeSlot)
    {
        // Check if party size is too big
        if (partySize > MAX_PARTY_SIZE)
        {
            Console.WriteLine($"Sorry, we cannot accommodate parties larger than {MAX_PARTY_SIZE} people.");
            return false;
        }

        // Check if restaurant is already at maximum capacity
        int totalReserved = reservations.Where(r => r.TimeSlot == timeSlot).Sum(r => r.PartySize);
        if (totalReserved + partySize > MAX_CAPACITY)
        {
            Console.WriteLine($"Sorry, we are fully booked for the size of your party at this: {timeSlot:t} time slot.");
            if (MAX_CAPACITY - totalReserved != 0)
            {
                Console.WriteLine($"We do have {MAX_CAPACITY - totalReserved} spots left.");

            }
            Console.WriteLine($"");
            return false;
        }

        // Check if reservation is within maximum days ahead
        if ((timeSlot.Date - DateTime.Now.Date).TotalDays > MAX_DAYS_AHEAD)
        {
            Console.WriteLine($"Sorry, reservations can only be made up to {MAX_DAYS_AHEAD} days in advance.");
            return false;
        }

        // Add reservation to the list
        reservations.Add(new Reservation { Name = name, PartySize = partySize, TimeSlot = timeSlot });

        Console.WriteLine($"Reservation made for {partySize} people at {timeSlot:t} under the name {name}.");
        return true;
    }

    public void SaveReservations()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");


        var FormatContent = JsonConvert.SerializeObject(reservations);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, FormatContent);
        }
        else
        {
            File.Delete(filePath);
            File.WriteAllText(filePath, FormatContent);
        }
    }

    public void LoadReservations()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");

        if (File.Exists(filePath))
        {
            string JustText = File.ReadAllText(filePath);
            reservations = JsonConvert.DeserializeObject<List<Reservation>>(JustText);
        }
    }
}
}
