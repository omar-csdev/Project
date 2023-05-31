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
            WriteToConsole(1, "Add Item");
            WriteToConsole(2, "Edit Item");
            WriteToConsole(3, "Delete Item");
            WriteToConsole(4, "Back to Menu Dashboard");
            string? input = Console.ReadLine();

            // input for "3"
            if (input == "3")
            {
                int itemIndexToDelete;
                bool success = false;
                while (true)
                {
                    Console.Clear();
                    WriteLogo();


                    foreach (Item item in menu)
                    {
                        WriteToConsole(item.Id, item.Name);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Enter item ID to remove: ");

                    try
                    {
                        bool isInt = int.TryParse(Console.ReadLine(), out itemIndexToDelete);
                        if (isInt && !menu.All(i => i.Id != itemIndexToDelete))
                        {
                            success = JSONEditor.RemoveItem(itemIndexToDelete);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error! Please enter a valid option!", Color.Red);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (success)
                    {
                        Console.WriteLine("Item Deleted!", Color.Red);
                        Console.WriteLine("Error! Please enter a valid option!", Color.Red);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        AdminMenuEditor.DisplayMenuEditOptions();
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong try again", Color.Green);
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        Console.Clear();
                        AdminMenuEditor.DisplayMenuEditOptions();
                    }

                }
            }
            else if (input == "2")
            {
                int itemIndex;
                while (true)
                {
                    Console.Clear();
                    WriteLogo();
                    foreach (Item item in menu)
                    {
                        WriteToConsole(item.Id, item.Name);
                    }


                    Console.WriteLine();
                    Console.WriteLine("Enter the number of the item you want to edit: ");
                    try
                    {
                        bool isInt = int.TryParse(Console.ReadLine(), out itemIndex);
                        if (isInt && !menu.All(i => i.Id != itemIndex))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error! Please enter a valid option!", Color.Red);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }  
                
                Item ? chosenItem = menu.FirstOrDefault(i => i.Id == itemIndex); 
                if(chosenItem != null){
                    Console.WriteLine("Name: " + chosenItem.Name);
                    Console.WriteLine("Price: " + chosenItem.Price);
                    Console.WriteLine("Type: " + chosenItem.Type);
                }
                else
                {
                    Console.WriteLine("Something went wrong finding the chosen item.", Color.Green);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    AdminMenuEditor.DisplayMenuEditOptions();
                }
                    
                Console.WriteLine("Enter the new name of the item (Press Enter key to keep the old value): ");
                string name = Console.ReadLine();
                name = name == "" ? chosenItem.Name : name;

                Console.WriteLine("Enter the new price of the item (0.00) (Press Enter key to keep the old value): ");
                double price = Double.TryParse(Console.ReadLine(), out double priceInput) ? priceInput : chosenItem.Price;

                while (true)
                {
                    Console.WriteLine("Any other value than 1 will lead to the item category being drink.");
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
                            Console.WriteLine("Something went wrong try again", Color.Green);
                        }
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        Console.Clear();
                        AdminMenuEditor.DisplayMenuEditOptions();
                        break;
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
                            Console.WriteLine("Something went wrong try again", Color.Red);
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        Console.Clear();
                        AdminMenuEditor.DisplayMenuEditOptions();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error! Please enter something.", Color.Red);
                        continue;
                    }
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                AdminMenuEditor.DisplayMenuEditOptions();
            }
            // input for "1"
            else if (input == "1")
            {
                Console.Clear();
                WriteLogo();
                Console.WriteLine("Enter the name of the item: ");
                string name = Console.ReadLine();
                
                
                double price;
                while (true)
                {
                    Console.WriteLine("Enter the price of the item: ");
                    try
                    {
                        bool isDouble = double.TryParse(Console.ReadLine(), out price);
                        if (isDouble)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error! Please enter a valid option!", Color.Red);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        }
                    }catch(Exception ex) { Console.WriteLine(ex.Message); }
                }

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