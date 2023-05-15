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
}