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
            WriteToConsole(4, "Back to Menu Dashboard");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {
                foreach (Item item in menu)
                {
                    WriteToConsole(item.Id, item.Name);
                }
                Console.WriteLine("Enter item ID to remove: ");
                int itemIndex = Convert.ToInt32(Console.ReadLine());
                bool success = JSONEditor.RemoveItem(itemIndex);
                Loading("Deleting item");
                if (success)
                {
                    Console.WriteLine("Item Deleted!", Color.Green);
                }
                else
                {
                    Console.WriteLine("Something went wrong try again", Color.Red);
                }
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
                    Loading();
                    Console.WriteLine("Error! Please choose a valid option!", Color.Red);
                }
                else
                {
                    Item? chosenItem = menu.FirstOrDefault(i => i.Id == itemIndex);
                    if (chosenItem != null)
                    {
                        Console.WriteLine("Name: " + chosenItem.Name);
                        Console.WriteLine("Price: " + chosenItem.Price);
                        Console.WriteLine("Type: " + chosenItem.Type);
                    }

                    Console.WriteLine("Enter the new name of the item (Press Enter key to keep the old value): ");
                    string name = Console.ReadLine();
                    name = name == "" ? chosenItem.Name : name;

                    Console.WriteLine("Enter the new price of the item (0.00) (Press Enter key to keep the old value) : ");
                    double price = Double.TryParse(Console.ReadLine(), out double priceInput) ? priceInput : chosenItem.Price;
                    WriteToConsole(1, "Food");
                    WriteToConsole(2, "Drink");
                    string type = Int32.TryParse(Console.ReadLine(), out int typeInput) ? typeInput == 1 ? "Food" : "Drink" : Console.ReadLine();

                    WriteToConsole(1, "Halal");
                    WriteToConsole(2, "Vegan");
                    WriteToConsole(3, "Vega");
                    string categoryFood = Int32.TryParse(Console.ReadLine(), out int categoryInput) ? categoryInput == 1 ? "Halal" : categoryInput == 2 ? "Vegan" : categoryInput == 3 ? "Vega" : Console.ReadLine() : Console.ReadLine();
                    if (type.ToLower() == "food")
                    {
                        Food food = new Food(name, price, type, categoryFood);
                        bool success = JSONEditor.UpdateItem(itemIndex, food);
                        if (success)
                        {
                            Loading("Editing item");
                            Console.WriteLine("Item edited successfully!", Color.Green);
                        }
                        else
                        {
                            Loading("Editing item");
                            Console.WriteLine("Something went wrong try again", Color.Red);
                        }
                    }
                    else if (type.ToLower() == "drink")
                    {
                        WriteToConsole(1, "Non-Alcoholic");
                        WriteToConsole(2, "Alcoholic");
                        string categoryDrink = Int32.TryParse(Console.ReadLine(), out int categoryDrinkInput) ? categoryDrinkInput == 1 ? "Non-Alcoholic" : "Alcoholic" : Console.ReadLine();
                        Drink drink = new Drink(name, price, type, categoryDrink);
                        bool success = JSONEditor.UpdateItem(itemIndex, drink);
                        if (success)
                        {
                            Loading("Editing item");
                            Console.WriteLine("Item edited successfully!", Color.Green);
                        }
                        else
                        {
                            Loading("Editing item");
                            Console.WriteLine("Something went wrong try again", Color.Red);
                        }
                    }
                    else
                    {
                        Loading();
                        Console.WriteLine("Error! Please choose a valid option (Food or Drink)!", Color.Red);
                    }
                }
            }
            else if (input == 3)
            {
                Console.WriteLine("Enter the name of the item: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the price of the item: ");
                double price = Convert.ToDouble(Console.ReadLine());
                WriteToConsole(1, "Food");
                WriteToConsole(2, "Drink");
                string type = Console.ReadLine();
                if (type.ToLower() == "1")
                {
                    WriteToConsole(1, "General");
                    WriteToConsole(2, "Halal");
                    WriteToConsole(3, "Vega");
                    WriteToConsole(4, "Vegan");
                    string categoryFood;
                    if (Int32.TryParse(Console.ReadLine(), out int categoryInput))
                    {
                        if (categoryInput == 1)
                        {
                            categoryFood = "General";
                        }
                        else if (categoryInput == 2)
                        {
                            categoryFood = "Halal";
                        }
                        else if (categoryInput == 3)
                        {
                            categoryFood = "Vega";
                        }
                        else if (categoryInput == 4)
                        {
                            categoryFood = "Vegan";
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                            categoryFood = Console.ReadLine();
                        }
                    }
                    else
                    {

                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                        categoryFood = Console.ReadLine();
                    }
                    Food food = new Food(name, price, "Food", categoryFood);
                    JSONEditor.AddItem(food);
                    Loading("Adding item");
                    Console.WriteLine("Item added successfully!", Color.Green);
                    Console.WriteLine();
                    Helper.ContinueDisplay();
                }
                else if (type.ToLower() == "2")
                {
                    WriteToConsole(1, "Non-Alcoholic");
                    WriteToConsole(2, "Alcoholic");
                    string categoryDrink;
                    if (Int32.TryParse(Console.ReadLine(), out int categoryInput))
                    {
                        if (categoryInput == 1)
                        {
                            categoryDrink = "Non-Alcoholic";
                        }
                        else if (categoryInput == 2)
                        {
                            categoryDrink = "Alcoholic";
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 1 or 2.");
                            categoryDrink = Console.ReadLine();
                        }
                    }
                    else
                    {

                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                        categoryDrink = Console.ReadLine();
                    }
                    Drink drink = new Drink(name, price, "Drink", categoryDrink);
                    JSONEditor.AddItem(drink);
                    Loading("Adding item");
                    Console.WriteLine("Item added successfully!", Color.Green);
                    Console.WriteLine();
                    Helper.ContinueDisplay();
                }

            }
        }
    }

    public static void WriteToConsole(int prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }

    
    public static void Loading(string message="Loading")
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                Console.Write(message + ".");
                Thread.Sleep(100);
            }
            else if(i == 3){
                Console.WriteLine(".");
            }
            else
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
        }
    }

    public static void getMenuItems()
    {  
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
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