using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;

public class ReservationsEditor
{
    public static bool RemoveReservation(string code)
    {
        try
        {
            // Load existing reservations
            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");
            string JSONString = File.ReadAllText(filePath);
            List<Project.Olivier_Reservations.Reservation> reservations = JsonConvert.DeserializeObject<List<Project.Olivier_Reservations.Reservation>>(JSONString) ?? new List<Project.Olivier_Reservations.Reservation>();




            // Find the reservation to remove
            Project.Olivier_Reservations.Reservation? reservationToRemove = reservations.FirstOrDefault(r => r.Code == code);


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

    public static bool UpdateReservation( Project.Olivier_Reservations.Reservation reservation)
    {
        try
        {
            if (IsValidReservation(reservation))
            {
                // Load existing reservations
                string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");
                string JSONString = File.ReadAllText(filePath);
                List<Project.Olivier_Reservations.Reservation> reservations = JsonConvert.DeserializeObject<List<Project.Olivier_Reservations.Reservation>>(JSONString) ?? new List<Project.Olivier_Reservations.Reservation>();

                // Find the reservation to update
                Project.Olivier_Reservations.Reservation? existingReservation = reservations.FirstOrDefault(r => r.Code == reservation.Code);


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

    private static bool IsValidReservation(Project.Olivier_Reservations.Reservation reservation)
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

    private static List<Project.Olivier_Reservations.Reservation> GetReservations(){
        List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();
        return reservations;
    }
}
