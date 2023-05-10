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
            string? input = Console.ReadLine();

            if (input == "1")
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
                            continue;
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
                    string category = Int32.TryParse(Console.ReadLine(), out int categoryInput) ? categoryInput == 1 ? "Food" : "Drink" : Console.ReadLine();

                    if (category.ToLower() == "food")
                    {
                        Food food = new Food(name, price, category);
                        bool success = JSONEditor.UpdateItem(itemIndex, food);
                        if (success)
                        {
                            Console.WriteLine("Item edited successfully!", Color.Red);
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
                    else if (category.ToLower() == "drink")
                    {
                        Drink drink = new Drink(name, price, category);
                        bool success = JSONEditor.UpdateItem(itemIndex, drink);
                        if (success)
                        {
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
            else if (input == "3")
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

                Console.WriteLine("Any value other than 1 will be recorded as Drink. ");
                WriteToConsole(1, "Food");
                WriteToConsole(2, "Drink");
                string category = Int32.TryParse(Console.ReadLine(), out int categoryInput) ? categoryInput == 1 ? "Food" : "Drink" : Console.ReadLine();
                
                if(category == null){
                    Console.WriteLine("Error! Please choose a valid option (Food or Drink)!", Color.Red);
                }
                else if(category.ToLower() == "food"){
                    Food food = new Food(name, price, category);
                    JSONEditor.AddItem(food);
                    Console.WriteLine("Item added successfully!", Color.Red);
                }else if (category.ToLower() == "drink"){
                    Drink drink = new Drink(name, price, category);
                    JSONEditor.AddItem(drink);
                    Console.WriteLine("Item added successfully!", Color.Red);
                }
                else{
                    Console.WriteLine("Error! Please enter a value!", Color.Green);
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                AdminMenuEditor.DisplayMenuEditOptions();

            }
            else if (input == "4")
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