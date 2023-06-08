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
            var GeneralItems = allMenuItems.Where(item => item.Type == "General").ToList();
            createAndDisplayTable(GeneralItems, "General Menu");

            var drinkItems = allMenuItems.Where(item => item.Type == "Drink").ToList();
            createAndDisplayTable(drinkItems, "Drink Menu");

            var alcoholicItems = allMenuItems.Where(item => item.Category == "Alcoholic").ToList();
            createAndDisplayTable(alcoholicItems, "Alcoholic Menu");

            var halalItems = allMenuItems.Where(item => item.Category == "Halal").ToList();
            createAndDisplayTable(halalItems, "Halal Menu");

            var vegaItems = allMenuItems.Where(item => item.Category == "vega").ToList();
            createAndDisplayTable(vegaItems, "Vega Menu");

            var veganItems = allMenuItems.Where(item => item.Category == "vegan").ToList();
            createAndDisplayTable(veganItems, "Vegan Menu");

            var NonAlcoholicItems = allMenuItems.Where(item => item.Category == "Non-Alcoholic").ToList();
            createAndDisplayTable(veganItems, "Non-Alcoholic Menu");
            var FullMenuItems = allMenuItems.Where(item => item.Type == "Drink" || item.Type == "Food").ToList();
            createAndDisplayTable(FullMenuItems, "Whole Menu");

        }

        public static void DisplayFood()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Type == "Food").ToList();
            createAndDisplayTable(foodItems, "Food Menu");
        }
        
        public static List<Item> SortingProcess(List<Item> items, int sort)
        {
            if (sort == 1)
            {
                items = items.OrderBy(x => x.Price).ToList();
                return items;
            }

            else if (sort == 2)
            {
                items = items.OrderBy(x => x.Price).Reverse().ToList();
                return items;
            }

            else if (sort == 3)
            {
                items = items.OrderBy(x => x.Name).ToList();
                return items;
            }

            else if (sort == 4)
            {
                items = items.OrderBy(x => x.Name).Reverse().ToList();
                return items;
            }
            return items;
        }
        public static void DisplayFood(int sort)
        {
            var allMenuItems = getMenuItems();

            var foodItems = allMenuItems.Where(item => item.Type == "Food").ToList();
            
            foodItems = SortingProcess(foodItems, sort);

            createAndDisplayTable(foodItems, "Food Menu");
        }

        public static void DisplayAlcoholic()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Alcoholic").ToList();
            createAndDisplayTable(foodItems, "Alcoholic Menu");
        }

        public static void DisplayAlcoholic(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Alcoholic").ToList();
            foodItems = SortingProcess(foodItems, sort);
            createAndDisplayTable(foodItems, "Alcoholic Menu");

        }

        public static void DisplayNonAlcoholic()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Non-Alcoholic").ToList();
            createAndDisplayTable(foodItems, "Non-Alcoholic Menu");
        }

        public static void DisplayNonAlcoholic(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Non-Alcoholic").ToList();
            foodItems = SortingProcess(foodItems, sort);
            createAndDisplayTable(foodItems, "Non-Alcoholic Menu");
        }

        public static void DisplayHalal()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Halal").ToList();
            createAndDisplayTable(foodItems, "Halal Menu");
        }

        public static void DisplayHalal(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Halal").ToList();
            foodItems = SortingProcess(foodItems, sort);
            createAndDisplayTable(foodItems, "Halal Menu");
        }

        public static void DisplayVega()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Vega").ToList();
            createAndDisplayTable(foodItems, "Vega Menu");
        }

        public static void DisplayVega(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Vega").ToList();
            foodItems = SortingProcess(foodItems, sort);
            foodItems = SortingProcess(foodItems, sort);
            createAndDisplayTable(foodItems, "Vega Menu");
        }

        public static void DisplayVegan()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Vegan").ToList();
            createAndDisplayTable(foodItems, "Vegan Menu");
        }

        public static void DisplayVegan(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "Vegan").ToList();
            foodItems = SortingProcess(foodItems, sort);
            createAndDisplayTable(foodItems, "Vegan Menu");
        }

        public static void DisplayGeneral()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "General").ToList();
            createAndDisplayTable(foodItems, "General Menu");
        }

        public static void DisplayGeneral(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var foodItems = allMenuItems.Where(item => item.Category == "General").ToList();
            foodItems = SortingProcess(foodItems, sort);
            createAndDisplayTable(foodItems, "General Menu");
        }

        public static void DisplayDrink()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var drinkItems = allMenuItems.Where(item => item.Type == "Drink").ToList();
            createAndDisplayTable(drinkItems, "Drink Menu");
        }

        public static void DisplayDrink(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var drinkItems = allMenuItems.Where(item => item.Type == "Drink").ToList();
            drinkItems = SortingProcess(drinkItems, sort);
            createAndDisplayTable(drinkItems, "Drink Menu");
        }

        public static void DisplayFullMenu()
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var FullMenuItems = allMenuItems.Where(item => item.Type == "Drink" || item.Type == "Food").ToList();
            createAndDisplayTable(FullMenuItems, "Whole Menu");
        }

        public static void DisplayFullMenu(int sort)
        {
            var allMenuItems = getMenuItems();

            // Create and display tables for food and drinks
            var FullMenuItems = allMenuItems.Where(item => item.Type == "Drink" || item.Type == "Food").ToList();
            FullMenuItems = SortingProcess(FullMenuItems, sort);
            createAndDisplayTable(FullMenuItems, "Whole Menu");
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
                string formattedPrice = string.Format("${00:N2}", item.Price);
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


