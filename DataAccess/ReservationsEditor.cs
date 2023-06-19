using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;
using Project.Presentation;

public class ReservationsEditor
{
    public static bool RemoveReservation(string code)
    {
        try
        {
            // Load existing reservations
            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");
            string JSONString = File.ReadAllText(filePath);
            List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(JSONString) ?? new List<Reservation>();




            // Find the reservation to remove
            Reservation? reservationToRemove = reservations.FirstOrDefault(r => r.Code == code);


            if (reservationToRemove != null)
            {
                // Remove the reservation
                reservations.Remove(reservationToRemove);


                // Save the updated reservations to the JSON file
                string updatedJSONString = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText(filePath, updatedJSONString);

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while removing the reservation: " + ex.Message);
            return false;
        }
    }

    public static bool UpdateReservation( Reservation reservation)
    {
        try
        {
            if (IsValidReservation(reservation))
            {
                // Load existing reservations
                string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");
                string JSONString = File.ReadAllText(filePath);
                List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(JSONString) ?? new List<Reservation>();

                // Find the reservation to update
                Reservation? existingReservation = reservations.FirstOrDefault(r => r.Code == reservation.Code);


                if (existingReservation != null)
                {
                    // Update the existing reservation
                    existingReservation.Name = reservation.Name;
                    existingReservation.LastName = reservation.LastName;
                    existingReservation.groupSize = reservation.groupSize;
                    existingReservation.Code = reservation.Code;
                    existingReservation.TimeSlot = reservation.TimeSlot;

                    // Save the updated reservations to the JSON file
                    string updatedJSONString = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                    File.WriteAllText(filePath, updatedJSONString);

                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while updating the reservation: " + ex.Message);
            return false;
        }
    }

    private static bool IsValidReservation(Reservation reservation)
    {
        if (string.IsNullOrEmpty(reservation.Name) ||
            reservation.groupSize <= 0 ||
            string.IsNullOrEmpty(reservation.Code) ||
            reservation.TimeSlot == null)

        {
            return false;
        }

        if (!string.IsNullOrEmpty(reservation.LastName) && !IsValidString(reservation.LastName))
        {
            return false;
        }

        return IsValidString(reservation.Name) && IsValidString(reservation.Code);
    }

    private static bool IsValidString(string value)
    {
        return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
    }

    private static List<Reservation> GetReservations(){
        List<Reservation> reservations = SaveReservations.LoadAll();
        return reservations;
    }
}
