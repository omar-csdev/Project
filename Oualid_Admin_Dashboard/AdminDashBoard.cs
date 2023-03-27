using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

static class AdminDashboard
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void DisplayDashboard()
    {
        for (; ; )
        {
            Console.Clear();
            WriteLogo();
            WriteToConsole(1, "View / create / delete Reservations");
            WriteToConsole(2, "View / edit Menu");
            WriteToConsole(3, "Log out");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                // view / create / delete reservations
            }
            else if (input == 2)
            {
                getMenuItems();
            }
            else if (input == 3)
            {
                //Log out
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


        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
        displayMenuItems(menu);
    }

    public static void displayMenuItems(List<Item> menuToDisplay){
        Console.WriteLine("Menu Items:");
        foreach (Item item in menuToDisplay)
        {
            Console.WriteLine($"{item.Name} - {item.Price}eu");
        }
        Console.ReadKey();  
    }
    public static void WriteLogo()
    {
        string logo = @"     _       _           _       
    / \   __| |_ __ ___ (_)_ __  
   / _ \ / _` | '_ ` _ \| | '_ \ 
  / ___ \ (_| | | | | | | | | | |
 /_/   \_\__,_|_| |_| |_|_|_| |_|
                                 ";

        Console.WriteLine(logo, Color.Wheat);
    }
}