﻿using System.Drawing;

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

    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message+ " Press any button to continue.");
        Console.ResetColor();
        Console.ReadLine();
    }
    public static void Succes(string message) 
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}