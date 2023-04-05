using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

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
            WriteToConsole(2, "View Menu by Category");
            WriteToConsole(3, "Back to Menu Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                displayAllMenuItems();
                Thread.Sleep(4000);

            }
            else if (input == 2)
            {
                Console.WriteLine("Enter the category you want to view: (Food or Drink)");
                string category = Console.ReadLine();
                for(; ; ){
                    if(category.ToLower() == "food"){
                        displayFoodMenuItems();
                        Thread.Sleep(4000);
                        break;
                    }
                    else if(category.ToLower() == "drink"){
                        displayDrinkMenuItems();
                        Thread.Sleep(4000);

                        break;
                    }else{
                        Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                        Thread.Sleep(1500);
                    }
                }
            }
            else if (input == 3)
            {
                AdminDashboardMenuDashboard.DisplayMenuDashboard();
            }
            else
            {
                Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                Thread.Sleep(1500);
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
        string filePath = Path.Combine(Environment.CurrentDirectory, @".\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);


        List<Item> menuFromJson = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
        menu = menuFromJson.ToArray();
    }

    public static void displayAllMenuItems(){
        Console.WriteLine("Menu Items:");
        foreach (Item item in menu)
        {
            Console.WriteLine($"{item.Name} - {item.Price}eu - {item.Category}");
        }
        Console.ReadKey();  
    }
    
    public static void displayFoodMenuItems(){
        Console.WriteLine("Food Menu Items:");
        foreach (Item item in menu)
        {
            if(item.Category == "Food"){
                Console.WriteLine($"{item.Name} - {item.Price}eu - {item.Category}");
            }
        }
        Console.ReadKey();
    }

    public static void displayDrinkMenuItems(){
        Console.WriteLine("Drink Menu Items:");
        foreach (Item item in menu)
        {
            if(item.Category == "Drink"){
                Console.WriteLine($"{item.Name} - {item.Price}eu - {item.Category}");
            }
        }
        Console.ReadKey();
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