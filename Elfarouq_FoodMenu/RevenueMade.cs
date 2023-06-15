using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;


public class RevenueMade
{
    public static void GenerateRevenuePerMonth()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\PaidOrders.json");

        string jsonString = File.ReadAllText(filePath);

        List<PaidOrder> existingData = JsonConvert.DeserializeObject<List<PaidOrder>>(jsonString) ?? new List<PaidOrder>();

        Dictionary<DateTime, double> revenueByMonth = new Dictionary<DateTime, double>();

        foreach (PaidOrder revenueData in existingData)
        {
            DateTime paidTime = revenueData.PaidTime;

            // Check if the order is from the current year
            if (paidTime.Year == DateTime.Now.Year)
            {
                DateTime monthStart = new DateTime(DateTime.Now.Year, paidTime.Month, 1);

                if (!revenueByMonth.ContainsKey(monthStart))
                {
                    revenueByMonth[monthStart] = revenueData.TotalPrice;
                }
                else
                {
                    revenueByMonth[monthStart] += revenueData.TotalPrice;
                }
            }
        }

        ConsoleTable table = new ConsoleTable("Month", "Revenue");

        // Populate the table with data from revenueByMonth dictionary
        foreach (var kvp in revenueByMonth)
        {
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key.Month);
            table.AddRow(monthName, kvp.Value);
        }

        // Printing the Table with the revenues
        table.Configure(o => o.EnableCount = false);
        table.Write();

        RevenuePerYear();
        Console.ReadLine();
    }

    public static void RevenuePerYear()
    {
        //voeg misschien nog amount of visitors toe.
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\PaidOrders.json");

        string jsonString = File.ReadAllText(filePath);
        double yearNow = 0;
        double oneYearAgo = 0;
        double twoYearAgo = 0;
        double threeYearAgo = 0;
        double fourYearAgo = 0;
        double fiveYearAgo = 0;
        double aboveFiveYearAgo = 0;
        DateTime dateNow = DateTime.Now;
        DateTime date1 = dateNow.AddYears(-1);
        DateTime date2 = dateNow.AddYears(-2);
        DateTime date3 = dateNow.AddYears(-3);
        DateTime date4 = dateNow.AddYears(-4);
        DateTime date5 = dateNow.AddYears(-5);
        DateTime date5plus = dateNow.AddYears(-6);

        List<PaidOrder> existingData = JsonConvert.DeserializeObject<List<PaidOrder>>(jsonString) ?? new List<PaidOrder>();
        Dictionary<DateTime, Double> RevenuePerYear = new Dictionary<DateTime, double>();
        List<double> YearlyRevenue = new List<double>();
        foreach(PaidOrder order in existingData) 
        {
           if (order == null) continue;
            if (order.PaidTime.Year == dateNow.Year) 
            {
                yearNow += order.TotalPrice;
                continue;
            }
            if (order.PaidTime.Year == date1.Year)
            {
                oneYearAgo += order.TotalPrice;
                continue;
            }
            if (order.PaidTime.Year == date2.Year)
            {
                twoYearAgo += order.TotalPrice;
                continue;
            }
            if (order.PaidTime.Year == date3.Year)
            {
                threeYearAgo += order.TotalPrice;
                continue;
            }
            if (order.PaidTime.Year == date4.Year)
            {
                fourYearAgo += order.TotalPrice;
                continue;
            }
            if (order.PaidTime.Year == date5plus.Year)
            {
                aboveFiveYearAgo += order.TotalPrice;
                continue;
            }
            else { continue; }
            
        }
        ConsoleTable table = new ConsoleTable("Year", "RevenueMade");
        table.AddRow(dateNow.Year, yearNow);
        table.AddRow(date1.Year, oneYearAgo);
        table.AddRow(date2.Year, twoYearAgo);
        table.AddRow(date3.Year, threeYearAgo);
        table.AddRow(date4.Year, fourYearAgo);
        table.AddRow(date5.Year, fiveYearAgo);
        table.AddRow(date5plus.Year, aboveFiveYearAgo);
        table.Configure(o => o.EnableCount = false);
        table.Write();
    }

}
