using Menu_item_creëren;
using Newtonsoft.Json;
using Project.Olivier_Reservations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
public static class OrderFood
{
    static string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
    static string JSONString = File.ReadAllText(filePath);
    public static List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
    public static Dictionary<string, int> orders = new Dictionary<string, int>();
    public static double AmountToPay;
    public static void Start(bool isGuest)
    {
        Console.Clear();
        WriteLogo();
        Say("1", "Make order");
        Say("2", "Show menu");
        Say("3", "Check order-basket");
        Say("4", "Check-out");
        Say("5", "Remove item from order");
        Say("6", "Go back to the main menu");

        int firstinput;
        while (true)
        {
            try
            {
                // Read user input as an integer
                firstinput = int.Parse(Console.ReadLine());

                // Validate user input
                if (firstinput < 1 || firstinput > 6)
                {
                    string message = ("Please enter a valid number between 1 and 5.");
                    Helper.Error(message);
                    Start(isGuest);
                }
                break;
            }
            catch (FormatException)
            {
                string message = ("Please enter a valid number between 1 and 5.");
                Helper.Error(message);
                Start(isGuest);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Helper.Error(message);
                Start(isGuest);
            }
        }

        if (firstinput == 1)
        {
            bool found = false;
            Console.Clear();
            Helper.Say("!", "Type '/back' to go back");
            Console.WriteLine("Please enter your reservation code: ");
            string code = Console.ReadLine();
            List<Project.Olivier_Reservations.Reservation> reservations = SaveReservations.LoadAll();
            if (code == "/back")
            {
                Start(isGuest);
            }
            foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
            {
                // Check if the reservation code exists
                if (reservation.Code == code)
                {
                    found = true;
                }
            }
            if (found == false)
            {
                Console.WriteLine("Reservation code invalid\nPress enter to go back...");
                Console.ReadKey();
                Console.Clear();
            }
            OptionOne(code, isGuest);
        }
        else if (firstinput == 2)
        {
            Console.Clear();
            MenuItem.Start();
            Helper.ContinueDisplay();
            Start(isGuest);
        }
        else if (firstinput == 3)
        {
            // Show order basket
            Console.Clear();
            Console.WriteLine("Please enter your reservation code: ");
            string code = Console.ReadLine();
            TotalPrice(code);
            Helper.ContinueDisplay();
            Start(isGuest);
        }
        else if (firstinput == 4)
        {
            Helper.Say("!", "Type '/back' to go back");
            Console.WriteLine("Please enter your reservation code: ");
            string code = Console.ReadLine();
            if (code == "/back")
            {
                Start(isGuest);
            }
            // Checking if the bill is open or not
            if (!ReservationSystem.IsReservationPaid(code))
            {
                if (ReservationSystem.IsAnythingOrderd(code))
                {
                    TotalPrice(code);
                }
                else
                {
                    Console.WriteLine("\nYou have not ordered anything yet.");
                    Helper.ContinueDisplay();
                    Start(isGuest);
                }
            }
            else
            {
                Console.WriteLine("\nYour reservation has already been paid for.");
                Helper.ContinueDisplay();
                Start(isGuest);
            }
            Console.WriteLine("\nPress a key to continue to the payment...");
            Console.ReadLine();
            Console.Clear();
            Payment.AskPay(AmountToPay, code);
        }
        else if (firstinput == 5)
        {
            Console.Clear();
            Console.WriteLine("Please enter your reservation code: ");
            string code = Console.ReadLine();
            RemoveItemFromOrder(code);
            Helper.ContinueDisplay();
            Start(isGuest);
        }
        else if (firstinput == 6)
        {
            Console.Clear();
            FoodMenu.Start(isGuest);
        }
        else
        {
            Console.WriteLine("Invalid input. Try again please");
            Helper.ContinueDisplay();
        }
    }
    public static void AddOrderJSON(string orderCode, int itemId, int quantity)
    {
        // Define the file path
        string filePath = Path.Combine("..", "..", "..", "DataSources", "Orders.json");

        // Deserialize the existing JSON data
        List<Dictionary<string, List<Dictionary<string, int>>>> jsonData;
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, int>>>>>(jsonString);
        }
        else
        {
            jsonData = new List<Dictionary<string, List<Dictionary<string, int>>>>();
        }

        // Find the dictionary with the given order code or create a new one
        Dictionary<string, List<Dictionary<string, int>>> orderData = jsonData.FirstOrDefault(d => d.ContainsKey(orderCode));
        if (orderData == null)
        {
            orderData = new Dictionary<string, List<Dictionary<string, int>>>
            {
                { orderCode, new List<Dictionary<string, int>>() }
            };
            jsonData.Add(orderData);
        }

        // Check if the item already exists in the order and update its quantity
        Dictionary<string, int> existingItem = orderData[orderCode].FirstOrDefault(item => item["itemId"] == itemId);
        if (existingItem != null)
        {
            existingItem["quantity"] += quantity;
        }
        else
        {
            // Add the new item to the order data
            Dictionary<string, int> itemData = new Dictionary<string, int>
            {
                { "itemId", itemId },
                { "quantity", quantity }
            };
            orderData[orderCode].Add(itemData);
        }

        // Serialize the updated JSON data and write it back to the file
        string updatedJsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        File.WriteAllText(filePath, updatedJsonString);
    }

    public static void TotalPrice(string code)
    {
        bool found = false;
        Console.Clear();

        List<Project.Olivier_Reservations.Reservation> reservations = SaveReservations.LoadAll();
        foreach (Project.Olivier_Reservations.Reservation reservation in reservations)
        {
            if (reservation.Code == code)
            {
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Reservation code invalid\nPress enter to go back...");
            Console.ReadKey();
            Console.Clear();
            Start(false);
        }
        string filePath = Path.Combine("..", "..", "..", "DataSources", "Orders.json");

        // Deserialize the existing JSON data
        List<Dictionary<string, List<Dictionary<string, int>>>> jsonData;
        string jsonString = File.ReadAllText(filePath);
        jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, int>>>>>(jsonString);

        Console.Clear();
        Console.Clear();
        Console.WriteLine("Your cart:");
        Console.WriteLine("--------------");
        double totalprice = 0;

        foreach (Dictionary<string, List<Dictionary<string, int>>> dict in jsonData)
        {
            // Check if the current dictionary contains the specified code
            if (dict.ContainsKey(code))
            {
                // Get the list of item dictionaries from the current dictionary
                List<Dictionary<string, int>> itemList = dict[code];

                // Iterate over each item dictionary in the list
                foreach (Dictionary<string, int> itemDict in itemList)
                {
                    // Get the itemId and quantity values from the current item dictionary
                    int itemId = itemDict["itemId"];
                    int quantity = itemDict["quantity"];
                    Item item = menu.FirstOrDefault(i => i.Id == itemId);
                    if (item != null)
                    {
                        double itemprice = item.Price * quantity;
                        totalprice += itemprice;
                        Console.WriteLine($"{quantity}x {item.Name} = {itemprice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")).Replace("?", "€")}");

                    }
                }

                // Exit the loop after finding the specified code

                Console.WriteLine("--------------");
                Console.WriteLine($"Total Price: ${totalprice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US"))}");
                AmountToPay = totalprice;
                break;
            }
        }
    }

    public static void OptionOne(string code, bool isGuest)
    {
        MenuItem.Start();
        Helper.Say("!", "type '/back' to go back");
        Console.WriteLine("What would you like to order? Select the number.");

        string inputstr = Console.ReadLine();

        if (inputstr == "/back")
        {
            Start(isGuest); // Go back to the previous menu if '/back' is entered
        }

        bool check = int.TryParse(inputstr, out int input);

        if (!check)
        {
            Console.WriteLine("Input format incorrect.");
            Console.WriteLine("Click enter to go back.");
            Console.ReadLine();
            Console.Clear();
            OptionOne(code, isGuest); // Recursively call the method if input format is incorrect
        }

        Item item = menu.FirstOrDefault(i => i.Id == input);

        if (item != null)
        {
            Console.WriteLine($"You have selected {item.Name}. How many would you like to order?");
            string quantitystr = Console.ReadLine();
            check = int.TryParse(quantitystr, out int quantity);

            if (!check)
            {
                Console.WriteLine("Input format incorrect.");
                Console.WriteLine("Click enter to go back.");
                Console.ReadLine();
                Console.Clear();
                OptionOne(code, isGuest); // Recursively call the method if input format is incorrect
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                Console.WriteLine("Click enter to go back.");
                Console.ReadLine();
                Console.Clear();
                OptionOne(code, isGuest); // Recursively call the method if quantity is invalid
            }

            if (orders.ContainsKey(item.Name))
            {
                orders[item.Name] += quantity;
            }
            else
            {
                orders[item.Name] = quantity;
            }

            ReservationSystem.SetHasOrderdAnything(code, true);
            Console.WriteLine($"Successfully added {quantity}x {item.Name} to your cart.");
            AddOrderJSON(code, item.Id, quantity);
            Console.WriteLine("Would you like to continue ordering? Y/N");
            string choices;
            bool checkloop = true;

            try
            {
                choices = Console.ReadLine();

                if (choices.ToUpper() != "Y" && choices.ToUpper() != "N")
                {
                    string message = "Please enter a valid answer: Y or N.";
                    Helper.Error(message);
                    OptionOne(code, isGuest); // Recursively call the method if invalid input is entered
                }
                else if (choices.ToUpper() == "N")
                {
                    checkloop = false;
                }
            }
            catch (FormatException)
            {
                string message = "Please enter a valid answer: Y or N.";
                Helper.Error(message);
                OptionOne(code, isGuest); // Recursively call the method if invalid input format is entered
            }

            if (!checkloop)
            {
                Start(isGuest); // Go back to the main menu if user chooses not to continue ordering
            }
            OptionOne(code, isGuest); // Call the method if option is Y. Go back to ordering. 
        }
        else
        {
            Console.WriteLine("Invalid Item ID...");
            Helper.ContinueDisplay();
            Console.Clear();
            OptionOne(code, isGuest); // Recursively call the method if invalid item ID is entered
        }
    }
    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Green);
        Console.WriteLine("] " + message);
    }


    public static void RemoveItemFromOrder(string code)
    {
        // Retrieve the order data from the JSON file
        string filePath = Path.Combine("..", "..", "..", "DataSources", "Orders.json");
        List<Dictionary<string, List<Dictionary<string, int>>>> jsonData;
        string jsonString = File.ReadAllText(filePath);
        jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, int>>>>>(jsonString);

        // Find the dictionary with the given order code
        Dictionary<string, List<Dictionary<string, int>>> orderData = jsonData.FirstOrDefault(d => d.ContainsKey(code));
        if (orderData != null)
        {
            // Get the list of item dictionaries from the order data
            List<Dictionary<string, int>> itemList = orderData[code];

            Console.WriteLine("Your order basket:");
            Console.WriteLine("--------------");

            // Display the items in the order basket
            for (int i = 0; i < itemList.Count; i++)
            {
                Dictionary<string, int> itemDict = itemList[i];
                int itemId = itemDict["itemId"];
                int quantity = itemDict["quantity"];
                Item item = menu.FirstOrDefault(item => item.Id == itemId);

                if (item != null)
                {
                    Console.WriteLine($"{i + 1}. {item.Name} (Quantity: {quantity})");
                }
            }

            Console.WriteLine("--------------");

            try
            {
                // Prompt the user to select an item to remove
                Console.WriteLine("Enter the number of the item to remove:");
                int selectedIndex = int.Parse(Console.ReadLine());

                if (selectedIndex >= 1 && selectedIndex <= itemList.Count)
                {
                    Dictionary<string, int> selectedItem = itemList[selectedIndex - 1];
                    Item item = menu.FirstOrDefault(i => i.Id == selectedItem["itemId"]);
                    int currentQuantity = selectedItem["quantity"];

                    Console.WriteLine($"Enter the quantity to remove (1-{currentQuantity}):");
                    int quantityToRemove = int.Parse(Console.ReadLine());

                    if (quantityToRemove >= 1 && quantityToRemove <= currentQuantity)
                    {
                        if (quantityToRemove == currentQuantity)
                        {
                            itemList.RemoveAt(selectedIndex - 1);
                        }
                        else
                        {
                            selectedItem["quantity"] -= quantityToRemove;
                        }

                        // Serialize the updated JSON data and write it back to the file
                        string updatedJsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
                        File.WriteAllText(filePath, updatedJsonString);

                        Console.WriteLine("Item(s) removed from the order.");
                        
                    }
                    else
                    {

                        Console.WriteLine("Invalid quantity to remove."); // Error message and sending user back to the start of the removeItemFromOrder method
                        RemoveItemFromOrder(code);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid item selection."); // Error message and sending user back to the start of the removeItemFromOrder method
                    RemoveItemFromOrder(code);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid number."); // Error message and sending user back to the start of the removeItemFromOrder method
                RemoveItemFromOrder(code);
            }
        }
        else
        {
            Console.WriteLine("Order not found. Try again"); // Error message and sending user back to the start of the removeItemFromOrder method
            RemoveItemFromOrder(code);
        }
    }


    public static void WriteLogo()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(@"   ___          _           _             
  / _ \ _ __ __| | ___ _ __(_)_ __   __ _ 
 | | | | '__/ _` |/ _ \ '__| | '_ \ / _` |
 | |_| | | | (_| |  __/ |  | | | | | (_| |
  \___/|_|  \__,_|\___|_|  |_|_| |_|\__, |
                                    |___/                                                    
");
    Console.ResetColor();


}
}
        