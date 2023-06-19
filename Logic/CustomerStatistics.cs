using ConsoleTables;
using Newtonsoft.Json;
using Project.Presentation;
using System.Globalization;

public class CustomerStatistics 
{
    public static void GenerateCustomerVisits()
    {
        // Define file paths and directories
        string mainDirectory = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\");
        string reservationsFilePath = Path.Combine(mainDirectory, "Reservations.json");
        string oldReservationsDirectory = Path.Combine(mainDirectory, "oldReservations");

        // Create a list of JSON file paths
        List<string> jsonFiles = new List<string>
    {
        reservationsFilePath,
        Path.Combine(oldReservationsDirectory, "oldReservationsWithin1YearAgo.json"),
        Path.Combine(oldReservationsDirectory, "oldReservationsWithin1To2YearsAgo.json"),
        Path.Combine(oldReservationsDirectory, "oldReservationsWithin2To3YearsAgo.json"),
        Path.Combine(oldReservationsDirectory, "oldReservationsWithin3To4YearsAgo.json"),
        Path.Combine(oldReservationsDirectory, "oldReservationsWithin4To5YearsAgo.json"),
        Path.Combine(oldReservationsDirectory, "oldReservationsMoreThan5YearsAgo.json")
    };

        // Create a dictionary to store customer visits
        Dictionary<int, int> customerVisits = new Dictionary<int, int>();

        // Get current date and calculate past years' dates
        DateTime dateNow = DateTime.Now;
        DateTime date1 = dateNow.AddYears(-1);
        DateTime date2 = dateNow.AddYears(-2);
        DateTime date3 = dateNow.AddYears(-3);
        DateTime date4 = dateNow.AddYears(-4);
        DateTime date5 = dateNow.AddYears(-5);
        DateTime date5plus = dateNow.AddYears(-6);

        // Process each JSON file
        foreach (string file in jsonFiles)
        {
            // Read JSON file content
            string jsonString = File.ReadAllText(file);

            // Deserialize JSON content into a list of Reservation objects
            List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();

            // Process each reservation
            foreach (Reservation reservation in reservations)
            {
                // Extract visit date and group size from each reservation
                DateTime visitDate = reservation.TimeSlot.Date;
                int groupSize = reservation.groupSize;

                // Determine the visit date's year and update customerVisits dictionary accordingly
                if (visitDate.Year == dateNow.Year)
                {
                    if (!customerVisits.ContainsKey(dateNow.Year))
                    {
                        customerVisits[dateNow.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[dateNow.Year] += groupSize;
                    }
                }
                else if (visitDate.Year == date1.Year)
                {
                    if (!customerVisits.ContainsKey(date1.Year))
                    {
                        customerVisits[date1.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[date1.Year] += groupSize;
                    }
                }
                else if (visitDate.Year == date2.Year)
                {
                    if (!customerVisits.ContainsKey(date2.Year))
                    {
                        customerVisits[date2.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[date2.Year] += groupSize;
                    }
                }
                else if (visitDate.Year == date3.Year)
                {
                    if (!customerVisits.ContainsKey(date3.Year))
                    {
                        customerVisits[date3.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[date3.Year] += groupSize;
                    }
                }
                else if (visitDate.Year == date4.Year)
                {
                    if (!customerVisits.ContainsKey(date4.Year))
                    {
                        customerVisits[date4.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[date4.Year] += groupSize;
                    }
                }
                else if (visitDate.Year == date5.Year)
                {
                    if (!customerVisits.ContainsKey(date5.Year))
                    {
                        customerVisits[date5.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[date5.Year] += groupSize;
                    }
                }
                else if (visitDate.Year < date5plus.Year)
                {
                    if (!customerVisits.ContainsKey(date5plus.Year))
                    {
                        customerVisits[date5plus.Year] = groupSize;
                    }
                    else
                    {
                        customerVisits[date5plus.Year] += groupSize;
                    }
                }
            }
        }

        // Create a console table for displaying results
        ConsoleTable table = new ConsoleTable("Year", "Total Visits");

        // Add rows to the table for each year and its corresponding total visits
        table.AddRow(dateNow.Year, customerVisits.GetValueOrDefault(dateNow.Year, 0));
        table.AddRow(date1.Year, customerVisits.GetValueOrDefault(date1.Year, 0));
        table.AddRow(date2.Year, customerVisits.GetValueOrDefault(date2.Year, 0));
        table.AddRow(date3.Year, customerVisits.GetValueOrDefault(date3.Year, 0));
        table.AddRow(date4.Year, customerVisits.GetValueOrDefault(date4.Year, 0));
        table.AddRow(date5.Year, customerVisits.GetValueOrDefault(date5.Year, 0));
        table.AddRow(date5plus.Year, customerVisits.GetValueOrDefault(date5plus.Year, 0));

        // Configure the table's options
        table.Configure(o => o.EnableCount = false);

        // Write the table to the console
        table.Write();
    }

    public static void GenerateVisitsPerMonth()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\Reservations.json");
        string jsonString = File.ReadAllText(filePath);
        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString) ?? new List<Reservation>();

        Dictionary<DateTime, int> visitsByMonth = new Dictionary<DateTime, int>();

        // Initialize the visitsByMonth dictionary with all months of the current year
        DateTime currentDate = DateTime.Now;
        DateTime startDate = new DateTime(currentDate.Year, 1, 1);
        DateTime endDate = new DateTime(currentDate.Year, 12, 31);
        for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
        {
            visitsByMonth[date] = 0;
        }

        // Count the visits for each month
        foreach (Reservation reservation in reservations)
        {
            DateTime visitDate = reservation.TimeSlot.Date;

            // Check if the visit is from the current year
            if (visitDate.Year == currentDate.Year)
            {
                DateTime monthStart = new DateTime(visitDate.Year, visitDate.Month, 1);
                visitsByMonth[monthStart] += reservation.groupSize;
            }
        }

        ConsoleTable table = new ConsoleTable("Month", "Total Visits");

        // Populate the table with data from visitsByMonth dictionary
        foreach (var kvp in visitsByMonth)
        {
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key.Month);
            table.AddRow(monthName, kvp.Value);
        }

        // Print the Table with the visits
        table.Configure(o => o.EnableCount = false);
        table.Write();
    }

}