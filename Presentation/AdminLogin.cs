using System.Drawing;
using Console = Colorful.Console;
public static class AdminLogin
{
    public static void Say(string prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }
    public static void Start()
    {

        //List<Admin> test = LoginAccess.LoadAll("admindata.json");
        //foreach (Admin admin in test) 
        //{
        //    Console.WriteLine("hi");
        //}
        Console.Clear();
        Say("1", "Log In");
        Say("2", "Go back");
        string answer = Console.ReadLine();

        if (answer == "1")
        {
            // deze codeblock zal veranderd worden wanneer de json file uitlezen werkt, dit is een tijdelijke "oplossing"
            Console.WriteLine("Username?", Color.Blue);
            string Username = Console.ReadLine();
            Console.WriteLine("Password?", Color.Blue);
            string Password = Console.ReadLine();
            Admin newAdmin = new(Username, Password);
            newAdmin.LogIn(Username, Password);
            Environment.Exit(0);
            // hier verder met admin authoriteit
            // bijvoorbeeld: AdminAuthority.Start()
        }

        else if (answer == "2") 
        {
            Console.Clear();
            MainMenu.Start();
        }
        
    }
}