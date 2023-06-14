using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Olivier_Reservations
{
    internal class CancelReservation
    {
        public static void CancelNow()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");

            string jsonString = File.ReadAllText(filePath);

            List<Reservation> existingReservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();

            Console.WriteLine("Enter the reservation code of the reservation you want to cancel");

                try
                {
                    string reservationCode = Console.ReadLine().ToUpper();
                    ValidateReservationCode(reservationCode);

                    bool codeFound = false;
                    for (int i = existingReservations.Count - 1; i >= 0; i--)
                    {
                        if (existingReservations[i].Code == reservationCode)
                        {
                            existingReservations.RemoveAt(i);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Reservation successfully canceled");
                            Console.ResetColor();
                            WriteToJson(existingReservations);
                            codeFound = true;
                            Console.WriteLine("Press any key to exit...");
                            Console.ReadKey();
                            break;
                        }
                    }

                    if (!codeFound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Could not find a reservation linked to this code.");
                        Console.WriteLine("Check if you have the correct code and try again.");
                        Console.ResetColor();
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Failed to cancel the reservation, Check your input and try again.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }

        private static void WriteToJson(List<Reservation> existingReservations)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\reservations.json");
            string updatedJSONString = JsonConvert.SerializeObject(existingReservations, Formatting.Indented);

            File.WriteAllText(filePath, updatedJSONString);
        }

        private static void ValidateReservationCode(string reservationCode)
        {
            if (reservationCode.Length != 6 || !reservationCode.All(char.IsLetterOrDigit))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Invalid reservation code format. The code should consist of 6 alphanumeric characters.");
                Console.ResetColor();
            }
        }
    }
}
