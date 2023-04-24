using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_item_creëren
{
    static class MenuItem
    {
        public static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Get all menu items
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Type == "Food").ToList();
            createAndDisplayTable(foodItems, "Food Menu");

            var drinkItems = allMenuItems.Where(item => item.Type == "Drink").ToList();
            createAndDisplayTable(drinkItems, "Drink Menu");
        }

        public static void DisplayFood()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Type == "Food").ToList();
            createAndDisplayTable(foodItems, "Food Menu");
        }

        public static void DisplayDrink()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var drinkItems = allMenuItems.Where(item => item.Type == "Drink").ToList();
            createAndDisplayTable(drinkItems, "Drink Menu");
        }

        private static List<Item> getMenuItems()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
            string JSONString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
        }

        private static void createAndDisplayTable(List<Item> items, string tableName)
        {
            var table = new ConsoleTable("Number", "Name", "Price");

            // Add rows to table
            foreach (var item in items)
            {
                string formattedPrice = string.Format("€{0:N2}", item.Price);
                table.AddRow(item.Id, item.Name, formattedPrice);

            }

            // Display table with given name
            Console.WriteLine(tableName + ":");
            table.Configure(o => o.EnableCount = false);
            table.Write(Format.Default);
            Console.WriteLine();
        }
    }
}