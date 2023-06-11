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
    public static void GenerateRevenue()
    {
        Console.WriteLine("dawdawdad");
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\PaidOrders.json");

        string jsonString = File.ReadAllText(filePath);

        List<PaidOrder> existingData = JsonConvert.DeserializeObject<List<PaidOrder>>(jsonString) ?? new List<PaidOrder>();

        Dictionary<DateTime, double> revenueByMonth = new Dictionary<DateTime, double>();

        foreach (PaidOrder revenueData in existingData)
        {
            DateTime paidTime = revenueData.PaidTime;
            DateTime monthStart = new DateTime(paidTime.Year, paidTime.Month, 1);

            if (!revenueByMonth.ContainsKey(monthStart))
            {
                revenueByMonth[monthStart] = revenueData.TotalPrice;
            }
            else
            {
                revenueByMonth[monthStart] += revenueData.TotalPrice;
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
        table.Write();
        Console.ReadLine();
    }
}
