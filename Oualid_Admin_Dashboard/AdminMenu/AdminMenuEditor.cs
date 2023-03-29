using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

static class AdminMenuEditor
{
    public static Item[] menu = new Item[0];

    static public void DisplayMenuEditOptions()
    {
        for (; ; )
        {   
            getMenuItems();
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "Delete Item");
            WriteToConsole(2, "Edit Item");
            WriteToConsole(3, "Back to Menu Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                int index = 0;
                foreach (Item item in menu)
                {
                    WriteToConsole(index, item.Name);
                    index++;
                }
                Console.WriteLine("Enter the index of the item you want to delete: ");
                int itemIndex = Convert.ToInt32(Console.ReadLine());
                if (itemIndex < menu.Length)
                {   
                    //Delete item als item bestaat
                    JSONEditor.RemoveItem(itemIndex);
                    Console.WriteLine("Item deleted successfully!", Color.Green);
                    Thread.Sleep(1500);

                }
                else
                {
                    //Error als item niet bestaat
                    Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                    Thread.Sleep(1500);
                }
            }
            else if (input == 2)
            {
                int index = 0;
                foreach (Item item in menu)
                {
                    WriteToConsole(index, item.Name);
                    index++;
                }
                Console.WriteLine("Enter the index of the item you want to edit: ");
                int itemIndex = Convert.ToInt32(Console.ReadLine());
                if (itemIndex < menu.Length)
                {
                    //Edit item
                    Console.WriteLine("Item edited successfully!", Color.Green);
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                    Thread.Sleep(1500);
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

    
    public static void WriteLogo()
    {
        string logo = @"
  __  __                    _____    _ _ _             
 |  \/  | ___ _ __  _   _  | ____|__| (_) |_ ___  _ __ 
 | |\/| |/ _ \ '_ \| | | | |  _| / _` | | __/ _ \| '__|
 | |  | |  __/ | | | |_| | | |__| (_| | | || (_) | |   
 |_|  |_|\___|_| |_|\__,_| |_____\__,_|_|\__\___/|_|   
                                                                       
";

        Console.WriteLine(logo, Color.Wheat);
    }
}