using System.Drawing;
using Console = Colorful.Console;
public static class AdminLogin
{

    public static void Loading()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                Console.Write("Loading");
                Thread.Sleep(1000);
            }

            else
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
        }
        AdminLogin.Start();
    }
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
        Say("3", "Add user");
        Say("4", "Go back");
        string answer = Console.ReadLine();

        if (answer == "1")
        {
            Console.Clear();   
            Console.WriteLine("LOGGING IN:");
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
                    Thread.Sleep(3000);
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
            Console.Clear();
            Console.WriteLine("REMOVING AN USER:");
            List<Admin> updatedList = LoginAccess.LoadAll("admindata.json");
            int x = 1;
            foreach (Admin admin in updatedList)
            {
                if (admin.UserName != null)
                {
                    Console.WriteLine($"{x} {admin.UserName}");
                    x += 1;
                }
            }

            Console.WriteLine("Enter the ID of the username you'd like to remove:");
            string id = Console.ReadLine();
            int ID = Convert.ToInt32(id);
            if (ID < updatedList.Count && ID > 0)
            {
                Console.WriteLine($"Succesfully removed user {updatedList[ID].UserName}!");
                updatedList.Remove(updatedList[ID]);
                LoginAccess.WriteAll(updatedList);
                Loading();
            }
            Console.Clear();
            Console.WriteLine("ID not found.", Color.Red);
            Thread.Sleep(3000);
            Start();
        }

        else if (answer == "3")
        {
            Console.Clear();
            Console.WriteLine("ADDING AN USER:");
            List<Admin> updatedList = LoginAccess.LoadAll("admindata.json");
            Console.WriteLine("Username?");
            string username = Console.ReadLine();
            Console.WriteLine("Password?");
            string password = Console.ReadLine();

            Admin newAcc = new(username, password);
            foreach (Admin admin in updatedList) 
            {
                if (admin.UserName == newAcc.UserName)
                {
                    Console.WriteLine("Username was already taken.");
                    Thread.Sleep(3000);
                    Start();
                }
            }
            updatedList.Add(newAcc);
            LoginAccess.WriteAll(updatedList);
            Console.WriteLine("User added!");
            Loading();
        }
        else if (answer == "4")
        {
            Console.Clear();
            MainMenu.Start();
        }

    }
}