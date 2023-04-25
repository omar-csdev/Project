using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;
using System.Globalization;

static class AdminReservationsView
{

    public static List<Project.Olivier_Reservations.Reservation> reservations = Project.Olivier_Reservations.SaveReservations.LoadAll();

    static public void Run()
    {
        for (; ; )
        {   
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "View All Reservations");
            WriteToConsole(2, "View Reservations by Date");
            WriteToConsole(3, "View Reservations by Name");
            WriteToConsole(4, "View Reservation by Code");
            WriteToConsole(5, "Back to Reservations Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                //Alle reserveringen die gevonden zijn in de json bij het laden van de applicatie worden getoond door het aanroepen
                //van de DisplayAllReservations functie. Daarna wordt de optie gegeven om terug te keren door "Any key" in te drukken.
                DisplayAllReservations();
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                AdminReservationsView.Run();
            }
            //Vraagt de admin voor datum checkt deze datum en roept de functie aan die alle reserveringen op deze datum naar de console schrijft.
            else if (input == 2)
            {
                string date;
                while (true)
                {
                    Console.WriteLine("Enter the date to filter the reservations on (dd-mm-yyyy):");
                    string ? tempDate = Console.ReadLine();
                    if (IsValidDateFormat(tempDate))
                    {
                        date = tempDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong format or is before 01-01-2023. Try again");
                        Thread.Sleep(2000);
                        continue;
                    }
                }
                Console.Clear();
                

                DisplayAllReservationsByDate(date);

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                AdminReservationsView.Run();

            }
            //Vraagt de admin voor naam en achternaam. Roept de functie die alle reserveringen checkt op de gekregen parameters (name, lastname)
            //en de reserveringen met dezelfde naam en achternaam naar de console schrijft.
            else if (input == 3)
            {
                Console.WriteLine("First name");
                string ? firstName = Console.ReadLine();
                Console.WriteLine("Last name (if no last name press enter.)");
                string ? lastName = Console.ReadLine();

                DisplayAllReservationsByName(firstName, lastName);

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                AdminReservationsView.Run();
            }
            //Vraagt de admin voor de reserverings code. Roept de functie aan die alle reserveringen filtert op deze reserveringscode.
            //De functie schrijft of de reserverings informatie naar de console of meldt dat er geen reservering gevonden is op deze code.
            else if (input == 4)
            {
                string code;
                while (true)
                {
                    Console.WriteLine("Code: ");
                    string ? tempCode = Console.ReadLine();
                    if (!string.IsNullOrEmpty(tempCode))
                    {
                        code = tempCode;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have to enter a code.");
                        continue;
                    }
                }

                DisplayAllReservationsByCode(code);

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                AdminReservationsView.Run();
            }
            else if (input == 5)
            {
                AdminDashboardReservationsDashboard.DisplayReservationsDashboard();
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                Thread.Sleep(1500);
            }
        }
    }

    // Toont alle reserveringen gevonden in de json file reservations.json
    public static void DisplayAllReservations()
    {
        foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
        {
            Console.WriteLine("Name: " + reservation.Name + " " + reservation.LastName);
            Console.WriteLine("Party size: " + reservation.PartySize);
            Console.WriteLine("Time slot: " + reservation.TimeSlot);
            Console.WriteLine();
        }
    }

    //Toont alle reserveringen die zich plaats vinden op de gekregen parameter 'date'
    public static void DisplayAllReservationsByDate(string date)
    {
        DateTime inputDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
        {
            if (reservation.TimeSlot.Date == inputDate)
            {
                Console.WriteLine("Name: " + reservation.Name + " " + reservation.LastName);
                Console.WriteLine("Party size: " + reservation.PartySize);
                Console.WriteLine("Time slot: " + reservation.TimeSlot);
                Console.WriteLine();
            }
        }
    }

    //Toont alle reserveringen op basis van gekregen 'name' en 'lastName' als deze geen null is.
    public static void DisplayAllReservationsByName(string name, string ? lastName)
    {
        if(!string.IsNullOrEmpty(lastName)){
            foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
            {
                if (reservation.Name.ToLower() == name.ToLower() && reservation.LastName.ToLower() == lastName.ToLower())
                {
                    Console.WriteLine("Name: " + reservation.Name + " " + reservation.LastName);
                    Console.WriteLine("Party size: " + reservation.PartySize);
                    Console.WriteLine("Time slot: " + reservation.TimeSlot);
                    Console.WriteLine();
                }
            }

        }
        else
        {
            foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
            {
                if (reservation.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine("Name: " + reservation.Name + " " + reservation.LastName);
                    Console.WriteLine("Party size: " + reservation.PartySize);
                    Console.WriteLine("Time slot: " + reservation.TimeSlot);
                    Console.WriteLine();
                }
            }
        }
    }

    //Toont de reservering waar de reserverings code hetzelfde is als de gekregen 'code' parameter. Als deze niet gevonden is wordt dat gemeld.
    public static void DisplayAllReservationsByCode(string code)
    {
        Project.Olivier_Reservations.Reservation foundReservation = null;
        
        foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
        {
            if (reservation.Code == code)
            {
                foundReservation = reservation;
            }
        }

        if(foundReservation != null)
        {
            Console.WriteLine();
            Console.WriteLine("Name: " + foundReservation.Name + " " + foundReservation.LastName);
            Console.WriteLine("Party size: " + foundReservation.PartySize);
            Console.WriteLine("Time slot: " + foundReservation.TimeSlot);
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("No reservation found with code: " + code);
        }
    }

    //Checkt of datum in juiste format is en of het een reeele en relevante datum is.
    public static bool IsValidDateFormat(string date)
    {
        // Check if the input string matches the format "dd-mm-yyyy"
        if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return false;
        }

        // Check if the day is valid for the relevant month
        int day = parsedDate.Day;
        int month = parsedDate.Month;
        int year = parsedDate.Year;
        bool isLeapYear = DateTime.IsLeapYear(year);
        bool isDayValid = (month == 2 && isLeapYear && day <= 29) ||
                          (month == 2 && !isLeapYear && day <= 28) ||
                          (month == 4 || month == 6 || month == 9 || month == 11) && day <= 30 ||
                          (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day <= 31;
        if (!isDayValid)
        {
            return false;
        }

        // Check if the month is valid
        if (month > 12)
        {
            return false;
        }

        // Check if the year is valid
        if (year != 2023)
        {
            return false;
        }

        // If all checks pass, the date is valid
        return true;
    }

    //Schrijft iets naar de console op basis van de gekregen input (int, string)
    public static void WriteToConsole(int prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }

    //Schrijft de logo van de pagina waar de gebruiker zich bevindt.
    public static void WriteLogo()
    {
        string logo = @"
  ____                                _   _                   ____  _           _             
 |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __  ___  |  _ \(_)___ _ __ | | __ _ _   _ 
 | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __| | | | | / __| '_ \| |/ _` | | | |
 |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \ | |_| | \__ \ |_) | | (_| | |_| |
 |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/ |____/|_|___/ .__/|_|\__,_|\__, |
                                                                         |_|            |___/                   
";

        Console.WriteLine(logo, Color.Wheat);
    }
}