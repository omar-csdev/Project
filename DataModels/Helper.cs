using System.Drawing;

public class Helper
{
    public static void ErrorDisplay(string options)
    {
        Console.WriteLine($"Invalid input. Please enter {options}");
        Console.WriteLine("Press a key to go back...");
        Console.ReadKey();
    }

    public static void ContinueDisplay()
    {
        Console.WriteLine("Press a key to continue...");
        Console.ReadKey();
    }
    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }

    public static void WriteLogo(string logo)
    {
        Console.WriteLine(logo, Color.Wheat);
    }
    public static void DisplayReservation(Project.Olivier_Reservations.Reservation reservation)
    {
        Console.WriteLine();
        Console.WriteLine("Name: " + reservation.Name + " " + reservation.LastName);
        Console.WriteLine("Group size: " + reservation.groupSize);
        Console.WriteLine("Time slot: " + reservation.TimeSlot);
        Console.WriteLine();
    }

}