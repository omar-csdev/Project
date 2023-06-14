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
                            if (CancelationFee(existingReservations[i].TimeSlot))
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
        private static bool CancelationFee(DateTime matchedReservation)
        {
            TimeSpan difference = matchedReservation - DateTime.Now;
            if (difference.TotalHours < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your reservation is within 1 hour. If you continue the cancellation, we will have to fine you a late cancellation fee of €10,- .");
                Console.WriteLine("To continue, write 'y'; to keep the reservation, write 'n'.");
                Console.ResetColor();

                try
                {
                    string choice = Console.ReadLine()?.ToLower();
                    ValidateCancellationChoice(choice);

                    if (choice == "y")
                    {
                        // Perform cancellation logic
                        Console.WriteLine("Cancellation fee will be sent to your personal adress.");
                        return true;
                    }
                    else if (choice == "n")
                    {
                        // Keep the reservation
                        Console.WriteLine("Reservation kept.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Failed to process the cancellation choice. Please try again.");
                    Console.ResetColor();
                }
            }
            return true;
        }

        private static void ValidateCancellationChoice(string choice)
        {
            if (choice != "y" && choice != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Invalid cancellation choice. Please enter 'y' to cancel or 'n' to keep the reservation.");
                Console.ResetColor();
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
