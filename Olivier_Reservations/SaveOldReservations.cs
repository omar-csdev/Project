using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Olivier_Reservations
{
    internal class SaveOldReservations
    {

        public static void WriteOldReservationsToJSON()
        {

            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");

            string jsonString = File.ReadAllText(filePath);

            List<Reservation> existingReservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();

            List<Reservation> removedReservations1 = new List<Reservation>();
            List<Reservation> removedReservations2 = new List<Reservation>();
            List<Reservation> removedReservations3 = new List<Reservation>();
            List<Reservation> removedReservations4 = new List<Reservation>();
            List<Reservation> removedReservations5 = new List<Reservation>();
            List<Reservation> removedReservations6 = new List<Reservation>();

            // Loop through reservations from back to front
            for (int i = existingReservations.Count - 1; i >= 0; i--)
            {
                DateTime currentDate = DateTime.Now;
                DateTime oneYearAgo = currentDate.AddYears(-1);
                DateTime twoYearsAgo = currentDate.AddYears(-2);
                DateTime threeYearsAgo = currentDate.AddYears(-3);
                DateTime fourYearsAgo = currentDate.AddYears(-4);
                DateTime fiveYearsAgo = currentDate.AddYears(-5);


                Reservation reservation = existingReservations[i];

                if (reservation.TimeSlot >= oneYearAgo && reservation.TimeSlot < currentDate)
                {
                    removedReservations1.Add(reservation);
                    existingReservations.RemoveAt(i);

                    string filePath2 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1YearAgo.json");
                    string newJSONstring = JsonConvert.SerializeObject(removedReservations1, Formatting.Indented);
                    File.WriteAllText(filePath2, newJSONstring);
                }
                else if (reservation.TimeSlot >= twoYearsAgo && reservation.TimeSlot < oneYearAgo)
                {
                    removedReservations2.Add(reservation);
                    existingReservations.RemoveAt(i);

                    string filePath3 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin1To2YearsAgo.json");
                    string newJSONstring1 = JsonConvert.SerializeObject(removedReservations2, Formatting.Indented);
                    File.WriteAllText(filePath3, newJSONstring1);
                }
                else if (reservation.TimeSlot >= threeYearsAgo && reservation.TimeSlot < twoYearsAgo)
                {
                    removedReservations3.Add(reservation);
                    existingReservations.RemoveAt(i);

                    string filePath4 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin2To3YearsAgo.json");
                    string newJSONstring2 = JsonConvert.SerializeObject(removedReservations3, Formatting.Indented);
                    File.WriteAllText(filePath4, newJSONstring2);
                }
                else if (reservation.TimeSlot >= fourYearsAgo && reservation.TimeSlot < threeYearsAgo)
                {
                    removedReservations4.Add(reservation);
                    existingReservations.RemoveAt(i);

                    string filePath5 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin3To4YearsAgo.json");
                    string newJSONstring3 = JsonConvert.SerializeObject(removedReservations4, Formatting.Indented);
                    File.WriteAllText(filePath5, newJSONstring3);
                }
                else if (reservation.TimeSlot >= fiveYearsAgo && reservation.TimeSlot < fourYearsAgo)
                {
                    removedReservations5.Add(reservation);
                    existingReservations.RemoveAt(i);


                    string filePath6 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsWithin4To5YearsAgo.json");
                    string newJSONstring4 = JsonConvert.SerializeObject(removedReservations5, Formatting.Indented);
                    File.WriteAllText(filePath6, newJSONstring4);
                }
                else if (reservation.TimeSlot <= fiveYearsAgo)
                {
                    removedReservations6.Add(reservation);
                    existingReservations.RemoveAt(i);

                    string filePath7 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Oldreservations\oldReservationsMoreThan5YearsAgo.json");
                    string newJSONstring5 = JsonConvert.SerializeObject(removedReservations6, Formatting.Indented);
                    File.WriteAllText(filePath7, newJSONstring5);

                }


            }

            string updatedJSONString = JsonConvert.SerializeObject(existingReservations, Formatting.Indented);
            File.WriteAllText(filePath, updatedJSONString);


        }
    }
}
