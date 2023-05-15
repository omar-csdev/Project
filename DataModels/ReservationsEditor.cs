using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;

public class ReservationsEditor
{
    public static bool AddReservation(Project.Olivier_Reservations.Reservation reservation)
    {
        try
        {
            if (IsValidReservation(reservation))
            {
                // Load existing reservations
                List<Project.Olivier_Reservations.Reservation> reservations = GetReservations();

                // Add the new reservation
                reservations.Add(reservation);

                // Save the updated reservations to the JSON file
                string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("reservations.json", json);

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while adding the reservation: " + ex.Message);
            return false;
        }
    }

    public static bool RemoveReservation(string code)
    {
        try
        {
            // Load existing reservations
            List<Project.Olivier_Reservations.Reservation> reservations = GetReservations();

            // Find the reservation to remove
            Project.Olivier_Reservations.Reservation reservationToRemove = reservations.Find(r => r.Code == code);

            if (reservationToRemove != null)
            {
                // Remove the reservation
                reservations.Remove(reservationToRemove);

                // Save the updated reservations to the JSON file
                string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("reservations.json", json);

                Console.WriteLine("Reservation removed successfully.");
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

    public static bool UpdateReservation(string code, Project.Olivier_Reservations.Reservation reservation)
    {
        try
        {
            if (IsValidReservation(reservation))
            {
                // Load existing reservations
                List<Project.Olivier_Reservations.Reservation> reservations = GetReservations();

                // Find the reservation to update
                Project.Olivier_Reservations.Reservation existingReservation = reservations.Find(r => r.Code == code);

                if (existingReservation != null)
                {
                    // Update the existing reservation
                    existingReservation.Name = reservation.Name;
                    existingReservation.LastName = reservation.LastName;
                    existingReservation.PartySize = reservation.PartySize;
                    existingReservation.Code = reservation.Code;
                    existingReservation.TimeSlot = reservation.TimeSlot;

                    // Save the updated reservations to the JSON file
                    string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                    File.WriteAllText("reservations.json", json);

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

    private static bool IsValidReservation(Project.Olivier_Reservations.Reservation reservation)
    {
        if (string.IsNullOrEmpty(reservation.Name) ||
            reservation.PartySize <= 0 ||
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

    private static List<Project.Olivier_Reservations.Reservation> GetReservations(){
        List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();
        return reservations;
    }
}