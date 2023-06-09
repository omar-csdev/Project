public class Payment
{
    public static void AskPay(double amount)
    {
        try
        {
            Console.WriteLine("How would you like to pay?");
            OrderFood.Say("1", "Card");
            OrderFood.Say("2", "Cash");
            OrderFood.Say("3", "Go back");
            string input = Console.ReadLine();
            Console.Clear();
            if (input == "1")
            {
                Console.WriteLine("Please enter your card number:");
                string cardInput = Console.ReadLine();
                if (IsValidCard(cardInput))
                {
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
                    AskPay(amount);
                }
            }
            else if (input == "2")
            {
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
            AskPay(amount);
        }
    }

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


}