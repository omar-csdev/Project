using Newtonsoft.Json;
using Project.Olivier_Reservations;

public class Payment
{
    public static void AskPay(double amount, string reservationCode)
    {
        try
        {
            Console.WriteLine("How would you like to pay?");
            OrderFood.Say("1", "Card");
            OrderFood.Say("2", "Cash");
            OrderFood.Say("3", "Go back");
            string input = Console.ReadLine();
            Console.Clear();
            int customerID = ReservationSystem.GetCustomerIdFromReservation(reservationCode);
            if (input == "1")
            {
                Console.WriteLine("Please enter your card number:");
                string cardInput = Console.ReadLine();
                if (IsValidCard(cardInput))
                {
                    ReservationSystem.SetReservationStatusToPaid(reservationCode, true);
                    WriteData(customerID, reservationCode, amount);
                    Console.WriteLine($"You have successfully paid €{amount}");
                    Helper.ContinueDisplay();
                    Console.Clear();
                    OrderFood.Start();
                }
                else
                {
                    Console.WriteLine("Card invalid please try again...");
                    Console.ReadLine();
                    Console.Clear();
                    AskPay(amount, reservationCode);
                }
            }
            else if (input == "2")
            {
                ReservationSystem.SetReservationStatusToPaid(reservationCode, true);
                WriteData(customerID, reservationCode, amount);
                Console.WriteLine($"You have successfully paid €{amount}");
                Helper.ContinueDisplay();
                Console.Clear();
                OrderFood.Start();
            }
            else if (input == "3")
            {
                Console.Clear();
                OrderFood.Start();
            }
            else
            {
                Helper.ErrorDisplay("1, 2 or 3");

            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Helper.ContinueDisplay();
            AskPay(amount, reservationCode);
        }
    }


    // Luhn Algorithm
    // valid credit card: 378282246310005
    // invalid credit card: 4111111111111112
    public static bool IsValidCard(string number)
    {
        if (string.IsNullOrEmpty(number))
        {
            return false;
        }

        int[] digits = new int[number.Length];
        for (int i = 0; i < number.Length; i++)
        {
            if (!int.TryParse(number[i].ToString(), out digits[i]))
            {
                return false;
            }
        }

        int sum = 0;
        bool isEvenIndex = false;
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            if (isEvenIndex)
            {
                int doubledDigit = digits[i] * 2;
                sum += doubledDigit > 9 ? doubledDigit - 9 : doubledDigit;
            }
            else
            {
                sum += digits[i];
            }
            isEvenIndex = !isEvenIndex;
        }

        return sum % 10 == 0;
    }

    public static void WriteData(int customerID, string reservationCode, double totalPrice)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\PaidOrders.json");

        List<PaidOrder> paidOrders;

        // Check if the file exists
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            paidOrders = JsonConvert.DeserializeObject<List<PaidOrder>>(jsonString);
        }
        else
        {
            paidOrders = new List<PaidOrder>(); // Create a new list if the file doesn't exist
        }

        var paidOrder = paidOrders.FirstOrDefault(o => o.ReservationCode == reservationCode);

        if (paidOrder != null)
        {
            paidOrder.CustomerId = customerID;
            paidOrder.PaidTime = DateTime.Now;
            paidOrder.TotalPrice = totalPrice;
        }
        else
        {
            // Create a new PaidOrder object and add it to the list
            var newPaidOrder = new PaidOrder
            {
                ReservationCode = reservationCode,
                CustomerId = customerID,
                PaidTime = DateTime.Now,
                TotalPrice = totalPrice
            };
            paidOrders.Add(newPaidOrder);
        }

        // Serialize the updated paid orders list back to JSON
        string updatedJsonString = JsonConvert.SerializeObject(paidOrders, Formatting.Indented);

        // Write the updated JSON back to the file
        File.WriteAllText(filePath, updatedJsonString);
    }





}