using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;
using Menu_item_creëren;

static class AdminMenuView
{
    public static Item[] menu = new Item[0];

    static public void DisplayMenuDisplayOptions()
    {
        for (; ; )
        {   
            getMenuItems();
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "View Whole Menu");
            WriteToConsole(2, "View Menu by Type");
            WriteToConsole(3, "Back to Menu Dashboard");

            string? input = Console.ReadLine();

            if (input == "1")
            {
                displayAllMenuItems();
                Thread.Sleep(4000);

            }
            else if (input == "2")
            {
                WriteToConsole(1, "Food");
                WriteToConsole(2, "Drink");
                string category = Convert.ToInt32(Console.ReadLine()) == 1 ? "Food" : "Drink";

                for(; ; ){
                    if(category.ToLower() == "food"){
                        displayFoodMenuItems();
                        break;
                    }
                    else if(category.ToLower() == "drink"){
                        displayDrinkMenuItems();
                        break;
                    }else{
                        Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                        Thread.Sleep(1500);
                    }
                }
            }
            else if (input == "3")
            {
                AdminDashboardMenuDashboard.DisplayMenuDashboard();
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);
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

    public static void getMenuItems()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);


        List<Item> menuFromJson = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
        menu = menuFromJson.ToArray();
    }

    public static void displayAllMenuItems(){
        MenuItem.Start();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        DisplayMenuDisplayOptions();
    }
    
    public static void displayFoodMenuItems(){
        MenuItem.DisplayFood();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        DisplayMenuDisplayOptions();
    }

    public static void displayDrinkMenuItems(){
        MenuItem.DisplayDrink();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        DisplayMenuDisplayOptions();
    }
    public static void WriteLogo()
    {
        string logo = @"
  __  __                    ____  _           _             
 |  \/  | ___ _ __  _   _  |  _ \(_)___ _ __ | | __ _ _   _ 
 | |\/| |/ _ \ '_ \| | | | | | | | / __| '_ \| |/ _` | | | |
 | |  | |  __/ | | | |_| | | |_| | \__ \ |_) | | (_| | |_| |
 |_|  |_|\___|_| |_|\__,_| |____/|_|___/ .__/|_|\__,_|\__, |
                                       |_|            |___/               
";

        Console.WriteLine(logo, Color.Wheat);
    }
}