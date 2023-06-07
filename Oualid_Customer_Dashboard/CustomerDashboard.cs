using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

public static class CustomerDashboard
{

    
    private static CustomerAccount customer { get; set; }    
    private static List<Project.Olivier_Reservations.Reservation> upcomingReservations { get; set; }
    public static void DisplayDashboard()
    {
        GetLoggedInCustomer();
        while(customer != null)
        {
            Console.Clear();
            WriteLogo();
            GetReservationsForCustomer();
            WriteToConsole(1, "Make new reservation");
            WriteToConsole(2, "View upcoming reservations");
            WriteToConsole(3, "View all reservations");
            WriteToConsole(4, "Edit account");
            WriteToConsole(5, "Log out");
            string ? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Project.Olivier_Reservations.Reservations.Reservationstart();
                    break;
                case "2":
                    DisplayReservations(upcomingReservations);
                    break;
                case "3":
                    ClearScreen();
                    Console.WriteLine("This feature is not implemented yet!", Color.Green);
                    Helper.ContinueDisplay();
                    CustomerDashboard.DisplayDashboard();
                    break;
                case "4":
                    CustomerProfileEditor.DisplayDashboard();
                    break;
                case "5":
                    LogUserOut();
                    break;
                default:
                    Console.WriteLine("Error! Please choose a valid option!", Color.Green);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;
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
  ____             __ _ _      
 |  _ \ _ __ ___  / _(_) | ___ 
 | |_) | '__/ _ \| |_| | |/ _ \
 |  __/| | | (_) |  _| | |  __/
 |_|   |_|  \___/|_| |_|_|\___|                                                                
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
        public static void GetReservationsForCustomer()
        {
            List<Project.Olivier_Reservations.Reservation> allreservations = Project.Olivier_Reservations.SaveReservations.LoadAll();
            upcomingReservations = allreservations.Where(r => r.Name == customer.UserNameSetter).ToList();
        }
        public static void DisplayReservations(List<Project.Olivier_Reservations.Reservation> reservations)
        {
            if(reservations.Count == 0) {
                ClearScreen();
                Console.WriteLine("No upcoming reservations!");
                Helper.ContinueDisplay();
                CustomerDashboard.DisplayDashboard();
            }
            else
            {
                ClearScreen();
                foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
                {
                    Helper.DisplayReservation(reservation);
                }
                Console.WriteLine();
                Helper.ContinueDisplay();
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
