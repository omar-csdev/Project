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
        Say("2", "Remove user");
        Say("3", "Go back");
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
                        Environment.Exit(0); //oualid kan hier de startfunctie van zijn dashboard callen.
                    }
                }
                Console.WriteLine("No users found with the matching credentials!", Color.Red);
                Thread.Sleep(3000);
            }
        }
        else if (answer == "2")
        {
            List<Admin> updatedList = LoginAccess.LoadAll("admindata.json");
            int x = 1;
            foreach (Admin admin in updatedList)
            {
                Console.WriteLine(x, admin.UserName);
            }

            Console.WriteLine("Enter the username you'd like to remove:");
            string Name = Console.ReadLine();
            for (int i = 0; i < updatedList.Count; i++;)
            {
                if (updatedList[i].UserName == Name)
                {
                    updatedList.Remove(updatedList[i]);
                    Console.WriteLine("User succesfully removed!");
                    LoginAccess.WriteAll(updatedList);
                    Thread.Sleep(3000)
                    Start();
                }

            }
            Console.WriteLine("User not found.");
            Thread.Sleep(3000)
            Start();
        }
        else if (answer == "3")
        {
            Console.Clear();
            MainMenu.Start();
        }

    }
}