using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

public static class CustomerDashboard
{

    
    private static CustomerAccount customer { get; set; }    
    public static void DisplayDashboard()
    {
        for (; ; )
            {
                Console.Clear();
                WriteLogo();
                GetLoggedInCustomer();
                WriteToConsole(1, "View upcoming reservations");
                WriteToConsole(2, "View all reservations");
                WriteToConsole(3, "Edit account");
                WriteToConsole(4, "Log out");
                string? input = Console.ReadLine();
                if (input == "1")
                {
                    ClearScreen();
                    Console.WriteLine("This feature is not implemented yet!", Color.Green);
                    Helper.ContinueDisplay();
                    CustomerDashboard.DisplayDashboard();

                }
                else if (input == "2")
                {
                    ClearScreen();
                    Console.WriteLine("This feature is not implemented yet!", Color.Green);
                    Helper.ContinueDisplay();
                    CustomerDashboard.DisplayDashboard();
                }
                else if (input == "3")
                {
                    ClearScreen();
                    Console.WriteLine("This feature is not implemented yet!", Color.Green);
                    Helper.ContinueDisplay();
                    CustomerDashboard.DisplayDashboard();
                }
                else if (input == "4")
                {
                    //Log out
                    LogUserOut();
                }
                else
                {
                    Console.WriteLine("Error! Please choose a valid option!", Color.Green);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                }
            }
        }

        public static void WriteToConsole(int prefix, string message)
        {
            Console.Write("[");
            Console.Write(prefix, Color.Red);
            Console.WriteLine("] " + message);
        }

        public static void WriteLogo()
        {
            string logo = @"
  ____            _     _                         _ 
 |  _ \  __ _ ___| |__ | |__   ___   __ _ _ __ __| |
 | | | |/ _` / __| '_ \| '_ \ / _ \ / _` | '__/ _` |
 | |_| | (_| \__ \ | | | |_) | (_) | (_| | | | (_| |
 |____/ \__,_|___/_| |_|_.__/ \___/ \__,_|_|  \__,_|                                   
";

            Console.WriteLine(logo, Color.Wheat);
        }

        public static void ClearScreen()
        {
            Console.Clear();
            WriteLogo();
        }

        public static void GetLoggedInCustomer()
        {
            List<CustomerAccount> accounts = CustomerAccess.LoadAll();
            CustomerAccount loggedInCustomer = accounts.FirstOrDefault(x => x.IsLoggedIn == true);
        if (loggedInCustomer != null)
        {
            customer = loggedInCustomer;
            Console.WriteLine("Logged in as: " + customer.UserNameSetter, Color.Green);
        }

        else {
            Console.WriteLine("Something went wrong! You will be redirected back to the log in page.");
            Console.ReadKey();
            AccountManager.LogIn();
        }
        }

        public static void LogUserOut()
        {
            List<CustomerAccount> accounts = CustomerAccess.LoadAll();
            CustomerAccount loggedInCustomer = accounts.FirstOrDefault(x => x.IsLoggedIn == true);
            if (loggedInCustomer != null)
            {
                loggedInCustomer.IsLoggedIn = false;
                CustomerAccess.WriteAll(accounts);
                MainMenu.NewStart();
            }   
            else
            {
                Console.WriteLine("Something went wrong! You will be redirected back to the log in page.");
                Console.ReadKey();
                AccountManager.LogIn();
            }
        }
     
}
