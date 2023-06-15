using System.Drawing;
using Console = Colorful.Console;
public static class AdminLogin
{

    public static void Start()
    {

        List<Admin> test = LoginAccess.LoadAll();
        foreach (Admin accounts in test) 
        {
            accounts.IsLoggedIn = false;
        }
        LoginAccess.WriteAll(test);


        Console.Clear();
        Console.WriteLine("ADMIN:", Color.RebeccaPurple);
        Helper.Say("1", "Log In");
        Helper.Say("2", "Go back");
        string answer = Console.ReadLine();

        if (answer == "1")
        {
            Console.Clear();   
            Console.WriteLine("LOGGING IN:", Color.RebeccaPurple);

            //geen accounts in de json:
            //vergelijking is 0
            if (test.Count == 0)
            {
                Console.WriteLine("No Admin accounts found. Would you like to register a new one? (y/n)", Color.Green);
                string inp = Console.ReadLine().ToLower();
                if (inp == "y")
                {
                    Helper.Say("!", "Type '/back' to go back to the admin menu");
                    Console.WriteLine("Username:");
                    string username = Console.ReadLine();
                    if (username == "/back")
                    {
                        MainMenu.NewStart();
                    }
                    //checken of gebruikersnaam al ingenomen is
                    foreach (Admin admin in test)
                    {
                        if (admin.UserName == username)
                        {
                            Console.WriteLine("Username already taken!", Color.Red);
                            Helper.ContinueDisplay();
                            Start();
                        }
                    }

                    //checks op het wachtwoord dat aangemaakt wordt
                    Console.WriteLine("Password:");
                    List<char> symbols = new List<char>() { '!', '@', '?', '#', '&' };

                    bool creatingAccount = true;
                    while (creatingAccount)
                    {
                        int checking = 0;
                        Helper.Say("!", "The password has got to contain 1 number and 1 symbol (!, @, ?, #, &)");
                        string password = Console.ReadLine();
                        foreach (char character in password)
                        {
                            if (symbols.Contains(character))
                            {
                                checking += 1;
                            }
                        }
                        bool containsInt = password.Any(char.IsDigit);

                        if (containsInt && checking > 0)
                        {
                            Admin newAdmin = new(username, password);
                            test.Add(newAdmin);
                            LoginAccess.WriteAll(test);
                            creatingAccount = false;
                        }

                        else
                        {
                            Helper.Say("!", "Password does not meet criteria");
                            Helper.ContinueDisplay();
                            Start();

                        }
                    }
                    Console.WriteLine("Added admin succesfully! You can login now.");
                    Helper.ContinueDisplay();
                    Start();
                }

                else if (inp == "n")
                {
                    Start();
                }

                else
                {
                    Console.WriteLine("Invalid input! Please enter 'y' or 'n'.", Color.Red);
                    Helper.ContinueDisplay();
                    Console.Clear();
                    Start();
                }
            }

            //wel accounts in de json: door naar inloggen
            else if (test.Count >= 1)
            {
                Helper.Say("!", "Type '/back' to go back to the admin menu");
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                int check = 0;
                if (username == "/back")
                {
                    MainMenu.NewStart();
                }
                foreach (Admin i in test) 
                {
                    if (i.UserName == username)
                    {
                        check += 1;
                    }
                }

                //check of gebruikersnaam bestaat
                if (check == 0)
                {
                    Console.WriteLine("Admin doesn't exist!", Color.Red);
                    Helper.ContinueDisplay();
                    Start();
                }

                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                

                //kijken in de json of de gegeven combinatie van wachtwoord en gebruikersnaam bestaat.
                foreach (Admin admin in test)
                {
                    if (admin.UserName == username && admin.Password == password)
                    {
                        admin.IsLoggedIn = true;
                        LoginAccess.WriteAll(test);
                        Console.WriteLine($"Admin {username} logged in succesfully!");
                        Helper.ContinueDisplay();
                        AdminDashboard.DisplayDashboard(); //oualid kan hier de startfunctie van zijn dashboard callen.
                    }
                }
                Console.WriteLine("No users found with the matching credentials!", Color.Red);
                Helper.ContinueDisplay();
                Start();
            }
        }
       

        //terug naar startscherm
        else if (answer == "2")
        {
            Console.Clear();
            MainMenu.NewStart(true);
        }

    }
}