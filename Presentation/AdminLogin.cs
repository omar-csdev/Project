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
        Console.WriteLine("ADMIN MENU:", Color.RebeccaPurple);
        Helper.Say("1", "Log In");
        Helper.Say("2", "Remove user");
        Helper.Say("3", "Add user");
        Helper.Say("4", "Go back");
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
                    Helper.Say("!", "Type '/back' to go back to the main menu");
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
                    Console.WriteLine("Added account succesfully! You can login now.");
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
                Helper.Say("!", "Type '/back' to go back to the main menu");
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
                    Console.WriteLine("Username doesn't exist!", Color.Red);
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
                        Console.WriteLine($"User {username} logged in succesfully!");
                        Helper.ContinueDisplay();
                        AdminDashboard.DisplayDashboard(); //oualid kan hier de startfunctie van zijn dashboard callen.
                    }
                }
                Console.WriteLine("No users found with the matching credentials!", Color.Red);
                Helper.ContinueDisplay();
                Start();
            }
        }

        //verwijderen van een gebruiker
        else if (answer == "2")
        {
            Console.Clear();
            Console.WriteLine("REMOVING AN USER:", Color.RebeccaPurple);


            //bij de functies verwijderen en toevoegen van een gebruiker altijd nieuwe list van de json
            //als we de oude "test" list gebruiken en er zijn hiervoor gebruiker toegevoegd worden deze
            //niet aangetoond bij het verwijderen
            List<Admin> updatedList = LoginAccess.LoadAll();


            if (updatedList.Count == 1)
            {
                Console.WriteLine("No users to remove!", Color.Red);
                Helper.ContinueDisplay();
            }

            int x = 1;
            foreach (Admin admin in updatedList)
            {
                if (admin.UserName != null)
                {
                    Helper.Say($"{x}", $"{admin.UserName}");
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
                Helper.ContinueDisplay();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("ID not found.", Color.Red);
                Helper.ContinueDisplay();   
                Start();
            }
        }


        //toevoegen van een gebruiker
        else if (answer == "3")
        {
            Console.Clear();
            Console.WriteLine("ADDING AN USER:", Color.RebeccaPurple);
            List<Admin> updatedList = LoginAccess.LoadAll();
            Helper.Say("!", "Type '/back' to go back to the main menu");
            Console.WriteLine("Username:");
            string username = Console.ReadLine();
            if (username == "/back")
            {
                MainMenu.NewStart();
            }
            foreach (Admin admin in updatedList)
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
                    updatedList.Add(newAdmin);
                    LoginAccess.WriteAll(updatedList);
                    creatingAccount = false;
                }

                else
                {
                    Helper.Say("!", "Password does not meet criteria");
                    Helper.ContinueDisplay();
                }
            }
            Console.WriteLine("Added account succesfully! You can login now.");
            Helper.ContinueDisplay();
            Start();
        }

        //terug naar startscherm
        else if (answer == "4")
        {
            Console.Clear();
            MainMenu.NewStart();
        }

    }
}