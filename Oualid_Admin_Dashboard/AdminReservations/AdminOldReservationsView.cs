﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static class AdminOldReservationsView
{
    public static List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();
    private static Dictionary<int, string> filePaths = new Dictionary<int, string>()
    {
        { 2, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1YearAgo.json" },
        { 3, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1To2YearsAgo.json" },
        { 4, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin2To3YearsAgo.json" },
        { 5, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1To2YearsAgo.json" },
        { 6, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin2To3YearsAgo.json" },
        { 7, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1To2YearsAgo.json" },
    };

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
                    Console.WriteLine($"Enter a number between 1 and 7.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number between 1 and 7.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        if (choice == 1)
        {
            // View all old reservations
            var allReservations = GetAllOldReservations();
            foreach (var reservation in allReservations)
            {
                PrintReservation(reservation);
            }
        }
        else if (filePaths.ContainsKey(choice))
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, filePaths[choice]);
            string jsonString = File.ReadAllText(filePath);
            List<Reservation> existingReservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();

            foreach (var reservation in existingReservations)
            {
                PrintReservation(reservation);
            }
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private static List<Reservation> GetAllOldReservations()
    {
        var allReservations = new List<Reservation>();

        foreach (var filePath in filePaths.Values)
        {
            string fullPath = Path.Combine(Environment.CurrentDirectory, filePath);
            string jsonString = File.ReadAllText(fullPath);
            List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();
            allReservations.AddRange(reservations);
        }

        return allReservations;
    }

    private static void PrintReservation(Reservation reservation)
    {
        Console.WriteLine("");
        Console.WriteLine("Name: " + reservation.Name + " " + reservation.LastName);
        Console.WriteLine("Group size: " + reservation.GroupSize);
        Console.WriteLine("Reservation date and time: *NOT WORKING?*");
        Console.WriteLine("Reservation Code: " + reservation.Code);
        Console.WriteLine("");
    }
}