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

        List<Admin> test = LoginAccess.LoadAll("admindata.json");



        Console.Clear();
        Say("1", "Log In");
        Say("2", "Go back");
        string answer = Console.ReadLine();

        if (answer == "1")
        {
            //dit codeblock zal veranderd worden wanneer de json file uitlezen werkt, dit is een tijdelijke "oplossing"
            if (test.Count == 1)
            {
                Console.WriteLine("No Admin accounts found. Would you like to register a new one? (y/n)", Color.Green);
                string inp = Console.ReadLine().ToLower();
                if (inp == "y")
                {
                    Console.WriteLine("Username?");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password?");
                    string password = Console.ReadLine();
                    Admin newAdmin = new(username, password);
                    test.Add(newAdmin);
                    LoginAccess.WriteAll(test);
                    Console.WriteLine("Added account succesfully! You can login now.");
                    Thread.Sleep(5000);
                    Start();
                }

                else if (inp == "n")
                {
                    Start();
                }

                else
                {
                    Console.WriteLine("Invalid input! Please enter 'y' or 'n'.", Color.Red);
                    Thread.Sleep(4000);
                    Console.Clear();
                    Start();
                }
            }
            else if (test.Count > 1)
            {
                Console.WriteLine("Username?");
                string username = Console.ReadLine();
                Console.WriteLine("Password?");
                string password = Console.ReadLine();

                foreach (Admin admin in test)
                {
                    if (admin.UserName == username && admin.Password == password)
                    {
                        Console.WriteLine($"User {username} logged in succesfully!");
                        Environment.Exit(0);
                    }
                }
                Console.WriteLine("No users found with the matching credentials!", Color.Red);
                Thread.Sleep(5000);
            }
        }

        else if (answer == "2")
        {
            Console.Clear();
            MainMenu.Start();
        }

    }
}