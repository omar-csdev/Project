using Project.Olivier_Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class StatisticsMenu
{
    public static void Start()
    {
        int choice;
        while (true)
        {
            SaveOldReservations.WriteOldReservationsToJSON();
            Console.Clear();
            Helper.DisplayRestaurantLogo();
            Helper.Say("1", "Check revenue per month");
            Helper.Say("2", "Check revenue per year");
            Helper.Say("3", "Check amount of customers per month");
            Helper.Say("4", "Check amount of customers per year");
            Helper.Say("5", "Exit");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RevenueMade.GenerateRevenuePerMonth();
                    break;
                case 2:
                    RevenueMade.RevenuePerYear();

                    // Handle Option 2
                    break;
                case 3:
                    // Handle Option 3
                    break;
                case 4:
                    // Handle Option 4
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    return; // Breaks out of the method, terminating the program
                default:
                    Helper.ErrorDisplay("1, 2, 3, 4 or 5");
                    break;
            }

            Console.WriteLine(); // Empty line for spacing
        }
    }
}