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
            WriteToConsole(3, "Add Item");
            WriteToConsole(3, "Back to Menu Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                foreach (Item item in menu)
                {
                    WriteToConsole(item.Id, item.Name);
                }
                Console.WriteLine("Enter item ID to remove: ");
                int itemIndex = Convert.ToInt32(Console.ReadLine());
                JSONEditor.RemoveItem(itemIndex);
            }
            else if (input == 2)
            {
                foreach (Item item in menu)
                {
                    WriteToConsole(item.Id, item.Name);
                }
                Console.WriteLine("Enter the number of the item you want to edit: ");
                int itemIndex = Convert.ToInt32(Console.ReadLine());
                if (menu.All(i => i.Id != itemIndex))
                {
                    Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                    Thread.Sleep(1500);
                }else
                {   
                    Item ? chosenItem = menu.FirstOrDefault(i => i.Id == itemIndex); 
                    if(chosenItem != null){
                        Console.WriteLine("Name: " + chosenItem.Name);
                        Console.WriteLine("Price: " + chosenItem.Price);
                        Console.WriteLine("Category: " + chosenItem.Category);
                    }
                    
                    Console.WriteLine("Enter the new name of the item: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter the new price of the item (0.00) : ");
                    double price = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the new category of the item (Food or Drink): ");
                    string category = Console.ReadLine();
                    if (category.ToLower() == "food")
                    {
                        Food food = new Food(name, price, category);
                        bool success = JSONEditor.UpdateItem(itemIndex, food);
                        if(success){
                            Console.WriteLine("Item edited successfully!", Color.Green);
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong try again", Color.Red);
                            Thread.Sleep(1500);
                        }
                    }
                    else if (category.ToLower() == "drink")
                    {
                        Drink drink = new Drink(name, price, category);
                        bool success = JSONEditor.UpdateItem(itemIndex, drink);
                        if(success){
                            Console.WriteLine("Item edited successfully!", Color.Green);
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong try again", Color.Red);
                            Thread.Sleep(1500);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error! Please choose a valid option (Food or Drink)!", Color.Red);
                        Thread.Sleep(1500);
                    }
                }
            }
            else if (input == 3)
            {
                Console.WriteLine("Enter the name of the item: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the price of the item: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the category of the item: ");
                string category = Console.ReadLine();
                if(category.ToLower() == "food"){
                    Food food = new Food(name, price, category);
                    JSONEditor.AddItem(food);
                    Console.WriteLine("Item added successfully!", Color.Green);
                    Thread.Sleep(1500);
                }else if (category.ToLower() == "drink"){
                    Drink drink = new Drink(name, price, category);
                    JSONEditor.AddItem(drink);
                    Console.WriteLine("Item added successfully!", Color.Green);
                    Thread.Sleep(1500);
                }
                else{
                    Console.WriteLine("Error! Please choose a valid option (Food or Drink)!", Color.Red);
                    Thread.Sleep(1500);
                }
            }
            else if (input == 4)
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